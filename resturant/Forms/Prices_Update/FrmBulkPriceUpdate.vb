Imports System.Data.SqlClient

Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices

Public Class FrmBulkPriceUpdate

    Private CON_STR As String = MY_Settings.SqlConStr
    Private dt As DataTable
    ' ==========================================
    ' 🌟 أكواد الشريط العلوي والتحكم بالنافذة 🌟
    ' ==========================================

    ' --- زر الخروج ---
    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    ' --- زر التكبير والاستعادة ---
    Private Sub MaxFormButton_Click(sender As Object, e As EventArgs) Handles MaxFormButton.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.MaximumSize = Screen.FromHandle(Me.Handle).WorkingArea.Size
            Me.WindowState = FormWindowState.Maximized
            MaxFormButton.Text = "❐"
        Else
            Me.WindowState = FormWindowState.Normal
            MaxFormButton.Text = "⬜"
        End If
    End Sub

    ' --- زر التصغير ---
    Private Sub MinFormButton_Click(sender As Object, e As EventArgs) Handles MinFormButton.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    ' --- حركة الفورم (السحب والإفلات بالماوس) ---
    Private drag As Boolean
    Private mouseX As Integer
    Private mouseY As Integer

    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, TopTitle_LB.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, TopTitle_LB.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, TopTitle_LB.MouseUp
        drag = False
    End Sub
    ' تحميل البيانات من الـ View
    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Using con As New SqlConnection(CON_STR)
            Dim sql As String = "
                SELECT 
                    [U_IM_ID],
                    [U_ID],
                    [IM_ID],
                    [IM_Num],
                    [Barcode],
                    [GM_Name],
                    [item_name],
                    [U_NAME],
                    [Price],
                    [Min_SP],
                    [Min_SP_2],
                    [OFFER_PRICE],
                    [GM_ID],
                    [U_Cargo],
                    [is_Default],
                    [UserName],
                    [Cost],
                    [Barcode],
                    [Markter_Val]--,
                   -- '' as Price_New,
                   -- '' as Min_SP_New,
                   -- '' as Min_SP_2_New,
                   -- '' as OFFER_PRICE_New
                FROM [dbo].[IM_Menu_Units_V] WHERE 1=1           
            "

            If GM_Serach.SelectedValue > 0 Then sql += " AND [GM_ID] = " & GM_Serach.SelectedValue

            sql += " ORDER BY [GM_NAME], [item_name], [U_NAME] "
            Using da As New SqlDataAdapter(sql, con)
                dt = New DataTable()
                da.Fill(dt)
            End Using
        End Using

        ' إعداد أعمدة الجريد
        dgvPrices.Columns.Clear()
        dgvPrices.AutoGenerateColumns = False

        ' مفاتيح مخفية
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .Name = "U_IM_ID",
            .DataPropertyName = "U_IM_ID",
            .Visible = False
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .Name = "IM_ID",
            .DataPropertyName = "IM_ID",
            .Visible = False
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .Name = "U_ID",
            .DataPropertyName = "U_ID",
            .Visible = False
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "GM_ID",
            .Name = "GM_ID",
            .DataPropertyName = "GM_ID",
            .Visible = False
        })

        ' بيانات الصنف

        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "باركود",
            .Name = "Barcode",
            .DataPropertyName = "Barcode"
        })


        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "رقم الصنف",
            .Name = "IM_Num",
            .DataPropertyName = "IM_Num"
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "التصنيف",
            .Name = "GM_Name",
            .DataPropertyName = "GM_Name"
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "اسم الصنف",
            .Name = "item_name",
            .DataPropertyName = "item_name",
            .Width = 200
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "الوحدة",
            .Name = "U_NAME",
            .DataPropertyName = "U_NAME"
        })

        ' الأسعار الحالية
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "سعر عادي (حالي)",
            .Name = "Price_Current",
            .DataPropertyName = "Price",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "جملة (حالي)",
            .Name = "Min_SP_Current",
            .DataPropertyName = "Min_SP",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "جملة جملة (حالي)",
            .Name = "Min_SP_2_Current",
            .DataPropertyName = "Min_SP_2",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "عرض (حالي)",
            .Name = "OFFER_PRICE_Current",
            .DataPropertyName = "OFFER_PRICE",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })

        ' الأسعار الجديدة (معاينة فقط)
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "سعر عادي (جديد)",
            .Name = "Price_New",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "جملة (جديد)",
            .Name = "Min_SP_New",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "جملة جملة (جديد)",
            .Name = "Min_SP_2_New",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "عرض (جديد)",
            .Name = "OFFER_PRICE_New",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })

        ' عمود اختيار
        Dim colSel As New DataGridViewCheckBoxColumn()
        colSel.HeaderText = "تعديل؟"
        colSel.Name = "Selected"
        dgvPrices.Columns.Add(colSel)

        dgvPrices.DataSource = dt
    End Sub

    ' دالة مساعدة لقراءة الأرقام مع التعامل مع DBNull
    Private Function GetDecimalCell(row As DataGridViewRow, colName As String) As Decimal
        Dim val = row.Cells(colName).Value
        If val Is Nothing OrElse IsDBNull(val) Then
            Return 0D
        End If
        Return Convert.ToDecimal(val)
    End Function

    ' معاينة الأسعار الجديدة داخل الجريد فقط (لا يوجد أي UPDATE هنا)
    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        If String.IsNullOrWhiteSpace(txtValue.Text) Then
            MessageBox.Show("الرجاء إدخال قيمة أو نسبة التعديل.", "تنبيه")
            Exit Sub
        End If

        Dim v As Decimal
        If Not Decimal.TryParse(txtValue.Text, v) OrElse v <= 0D Then
            MessageBox.Show("قيمة التعديل غير صحيحة.", "خطأ")
            Exit Sub
        End If

        If Not (chkPrice.Checked OrElse chkMinSP.Checked OrElse chkMinSP2.Checked OrElse chkOfferPrice.Checked) Then
            MessageBox.Show("الرجاء اختيار نوع السعر المراد تعديله (عادي/جملة/جملة جملة/عرض).", "تنبيه")
            Exit Sub
        End If

        Dim mode As String = If(rbValue.Checked, "Value", "Percent")
        Dim isIncrease As Boolean = rbIncrease.Checked

        ' دالة لحساب السعر الجديد
        Dim calcNewPrice As Func(Of Decimal, Decimal) =
            Function(oldP As Decimal) As Decimal
                If mode = "Value" Then
                    If isIncrease Then
                        Return oldP + v
                    Else
                        Return oldP - v
                    End If
                Else ' Percent
                    Dim delta As Decimal = oldP * v / 100D
                    If isIncrease Then
                        Return oldP + delta
                    Else
                        Return oldP - delta
                    End If
                End If
            End Function

        For Each row As DataGridViewRow In dgvPrices.Rows
            If row.IsNewRow Then Continue For

            ' لو نطبق فقط على الصفوف المحددة
            If chkOnlySelected.Checked Then
                Dim sel As Boolean = False
                If row.Cells("Selected").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("Selected").Value) Then
                    sel = CBool(row.Cells("Selected").Value)
                End If
                If Not sel Then Continue For
            End If

            Dim curPrice As Decimal = GetDecimalCell(row, "Price_Current")
            Dim curMinSP As Decimal = GetDecimalCell(row, "Min_SP_Current")
            Dim curMinSP2 As Decimal = GetDecimalCell(row, "Min_SP_2_Current")
            Dim curOffer As Decimal = GetDecimalCell(row, "OFFER_PRICE_Current")

            Dim newPrice As Decimal = curPrice
            Dim newMinSP As Decimal = curMinSP
            Dim newMinSP2 As Decimal = curMinSP2
            Dim newOffer As Decimal = curOffer

            If chkPrice.Checked Then
                newPrice = calcNewPrice(curPrice)
            End If
            If chkMinSP.Checked Then
                newMinSP = calcNewPrice(curMinSP)
            End If
            If chkMinSP2.Checked Then
                newMinSP2 = calcNewPrice(curMinSP2)
            End If
            If chkOfferPrice.Checked Then
                newOffer = calcNewPrice(curOffer)
            End If

            ' منع القيم السالبة
            If newPrice < 0D Then newPrice = 0D
            If newMinSP < 0D Then newMinSP = 0D
            If newMinSP2 < 0D Then newMinSP2 = 0D
            If newOffer < 0D Then newOffer = 0D

            row.Cells("Price_New").Value = Math.Round(newPrice, 3)
            row.Cells("Min_SP_New").Value = Math.Round(newMinSP, 3)
            row.Cells("Min_SP_2_New").Value = Math.Round(newMinSP2, 3)
            row.Cells("OFFER_PRICE_New").Value = Math.Round(newOffer, 3)
        Next

        CalculateTotals()

        MessageBox.Show("تم حساب الأسعار الجديدة (معاينة فقط).", "تم")
    End Sub

    ' تأكيد التعديل وتطبيقه على IM_Units مع تسجيل التاريخ
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If MessageBox.Show("هل تريد اعتماد التعديلات وتحديث الأسعار فعليًا؟",
                           "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If

        Dim mode As String = If(rbValue.Checked, "Value", "Percent")
        Dim changeType As String = If(rbIncrease.Checked, "Increase", "Decrease")
        Dim value As Decimal = 0D
        Decimal.TryParse(txtValue.Text, value)
        Dim reason As String = txtReason.Text.Trim()
        Dim batchId As Guid = Guid.NewGuid()
        Dim currentUserId As Integer = 1 ' غيّرها حسب نظامك (المستخدم الحالي)

        Using con As New SqlConnection(CON_STR)
            con.Open()
            Dim tran = con.BeginTransaction()
            Dim DATE_NOW As DateTime = Date.Now
            Try
                For Each row As DataGridViewRow In dgvPrices.Rows
                    If row.IsNewRow Then Continue For

                    ' لو فقط الصفوف المحددة
                    If chkOnlySelected.Checked Then
                        Dim sel As Boolean = False
                        If row.Cells("Selected").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("Selected").Value) Then
                            sel = CBool(row.Cells("Selected").Value)
                        End If
                        If Not sel Then Continue For
                    End If

                    Dim oldPrice As Decimal = GetDecimalCell(row, "Price_Current")
                    Dim oldMinSP As Decimal = GetDecimalCell(row, "Min_SP_Current")
                    Dim oldMinSP2 As Decimal = GetDecimalCell(row, "Min_SP_2_Current")
                    Dim oldOffer As Decimal = GetDecimalCell(row, "OFFER_PRICE_Current")

                    Dim newPrice As Decimal = If(row.Cells("Price_New").Value Is Nothing OrElse IsDBNull(row.Cells("Price_New").Value),
                                                 oldPrice,
                                                 Convert.ToDecimal(row.Cells("Price_New").Value))
                    Dim newMinSP As Decimal = If(row.Cells("Min_SP_New").Value Is Nothing OrElse IsDBNull(row.Cells("Min_SP_New").Value),
                                                 oldMinSP,
                                                 Convert.ToDecimal(row.Cells("Min_SP_New").Value))
                    Dim newMinSP2 As Decimal = If(row.Cells("Min_SP_2_New").Value Is Nothing OrElse IsDBNull(row.Cells("Min_SP_2_New").Value),
                                                  oldMinSP2,
                                                  Convert.ToDecimal(row.Cells("Min_SP_2_New").Value))
                    Dim newOffer As Decimal = If(row.Cells("OFFER_PRICE_New").Value Is Nothing OrElse IsDBNull(row.Cells("OFFER_PRICE_New").Value),
                                                 oldOffer,
                                                 Convert.ToDecimal(row.Cells("OFFER_PRICE_New").Value))

                    ' لو مافيش تغيير فعلي نكمل
                    If newPrice = oldPrice AndAlso newMinSP = oldMinSP AndAlso newMinSP2 = oldMinSP2 AndAlso newOffer = oldOffer Then
                        Continue For
                    End If

                    Dim u_im_id As Integer = Convert.ToInt32(row.Cells("U_IM_ID").Value)
                    Dim im_id As Integer = If(row.Cells("IM_ID").Value Is Nothing OrElse IsDBNull(row.Cells("IM_ID").Value),
                                              0,
                                              Convert.ToInt32(row.Cells("IM_ID").Value))
                    Dim u_id As Integer = If(row.Cells("U_ID").Value Is Nothing OrElse IsDBNull(row.Cells("U_ID").Value),
                                             0,
                                             Convert.ToInt32(row.Cells("U_ID").Value))

                    ' 1) إدخال في جدول التاريخ
                    Dim cmdHist As New SqlCommand("
                        INSERT INTO [dbo].[IM_Units_Prices_History]
                        (
                            U_IM_ID, IM_ID, U_ID,
                            Old_Price, Old_Min_SP, Old_Min_SP_2, Old_OFFER_PRICE,
                            New_Price, New_Min_SP, New_Min_SP_2, New_OFFER_PRICE,
                            ChangeMode, ChangeType, ChangeValue,
                            Reason, BatchId, User_ID ,ChangeDate
                        )
                        VALUES
                        (
                            @U_IM_ID, @IM_ID, @U_ID,
                            @Old_Price, @Old_Min_SP, @Old_Min_SP_2, @Old_OFFER_PRICE,
                            @New_Price, @New_Min_SP, @New_Min_SP_2, @New_OFFER_PRICE,
                            @ChangeMode, @ChangeType, @ChangeValue,
                            @Reason, @BatchId, @User_ID ,@ChangeDate
                        )
                    ", con, tran)

                    cmdHist.Parameters.AddWithValue("@U_IM_ID", u_im_id)
                    cmdHist.Parameters.AddWithValue("@IM_ID", If(im_id = 0, CType(DBNull.Value, Object), im_id))
                    cmdHist.Parameters.AddWithValue("@U_ID", If(u_id = 0, CType(DBNull.Value, Object), u_id))

                    cmdHist.Parameters.AddWithValue("@Old_Price", oldPrice)
                    cmdHist.Parameters.AddWithValue("@Old_Min_SP", If(oldMinSP = 0D, CType(DBNull.Value, Object), oldMinSP))
                    cmdHist.Parameters.AddWithValue("@Old_Min_SP_2", If(oldMinSP2 = 0D, CType(DBNull.Value, Object), oldMinSP2))
                    cmdHist.Parameters.AddWithValue("@Old_OFFER_PRICE", If(oldOffer = 0D, CType(DBNull.Value, Object), oldOffer))

                    cmdHist.Parameters.AddWithValue("@New_Price", newPrice)
                    cmdHist.Parameters.AddWithValue("@New_Min_SP", If(newMinSP = 0D, CType(DBNull.Value, Object), newMinSP))
                    cmdHist.Parameters.AddWithValue("@New_Min_SP_2", If(newMinSP2 = 0D, CType(DBNull.Value, Object), newMinSP2))
                    cmdHist.Parameters.AddWithValue("@New_OFFER_PRICE", If(newOffer = 0D, CType(DBNull.Value, Object), newOffer))

                    cmdHist.Parameters.AddWithValue("@ChangeMode", mode)
                    cmdHist.Parameters.AddWithValue("@ChangeType", changeType)
                    cmdHist.Parameters.AddWithValue("@ChangeValue", value)
                    cmdHist.Parameters.AddWithValue("@Reason", If(String.IsNullOrEmpty(reason), CType(DBNull.Value, Object), reason))
                    cmdHist.Parameters.AddWithValue("@BatchId", batchId)
                    cmdHist.Parameters.AddWithValue("@User_ID", currentUserId)
                    cmdHist.Parameters.AddWithValue("@ChangeDate", DATE_NOW)


                    cmdHist.ExecuteNonQuery()

                    ' 2) تحديث الجدول الرئيسي IM_Units
                    Dim cmdUpd As New SqlCommand("
                        UPDATE [dbo].[IM_Units]
                        SET 
                            [Price]      = @New_Price,
                            [Min_SP]     = @New_Min_SP,
                            [Min_SP_2]   = @New_Min_SP_2,
                            [OFFER_PRICE]= @New_OFFER_PRICE,
                            [User_ID]    = @User_ID
                        WHERE [U_IM_ID] = @U_IM_ID
                    ", con, tran)

                    cmdUpd.Parameters.AddWithValue("@U_IM_ID", u_im_id)
                    cmdUpd.Parameters.AddWithValue("@New_Price", newPrice)
                    cmdUpd.Parameters.AddWithValue("@New_Min_SP", If(newMinSP = 0D, CType(DBNull.Value, Object), newMinSP))
                    cmdUpd.Parameters.AddWithValue("@New_Min_SP_2", If(newMinSP2 = 0D, CType(DBNull.Value, Object), newMinSP2))
                    cmdUpd.Parameters.AddWithValue("@New_OFFER_PRICE", If(newOffer = 0D, CType(DBNull.Value, Object), newOffer))
                    cmdUpd.Parameters.AddWithValue("@User_ID", currentUserId)

                    cmdUpd.ExecuteNonQuery()
                Next

                tran.Commit()

                MessageBox.Show("تم اعتماد التعديلات وتحديث الأسعار بنجاح." &
                                Environment.NewLine &
                                "رقم دفعة التعديل: " & batchId.ToString(),
                                "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                tran.Rollback()
                MessageBox.Show("حدث خطأ أثناء تطبيق التعديلات:" & Environment.NewLine & ex.Message,
                                "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub


    Private Sub dgvPrices_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) _
    Handles dgvPrices.CellFormatting

        Dim colName As String = dgvPrices.Columns(e.ColumnIndex).Name

        If colName.EndsWith("_New") Then
            Dim row = dgvPrices.Rows(e.RowIndex)

            Dim newVal As Decimal = If(IsDBNull(e.Value) OrElse e.Value Is Nothing, 0D, CDec(e.Value))
            Dim curCol As String = colName.Replace("_New", "_Current")
            Dim oldVal As Decimal = If(IsDBNull(row.Cells(curCol).Value) OrElse row.Cells(curCol).Value Is Nothing, 0D, CDec(row.Cells(curCol).Value))

            If newVal > oldVal Then
                e.CellStyle.BackColor = Color.FromArgb(198, 239, 206) ' أخضر فاتح
                e.CellStyle.ForeColor = Color.DarkGreen
            ElseIf newVal < oldVal Then
                e.CellStyle.BackColor = Color.FromArgb(255, 199, 206) ' أحمر فاتح
                e.CellStyle.ForeColor = Color.DarkRed
            Else
                e.CellStyle.BackColor = Color.White
                e.CellStyle.ForeColor = Color.Black
            End If
        End If

    End Sub


    Private Sub dgvPrices_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) _
    Handles dgvPrices.CellValueChanged

        If dgvPrices.Columns(e.ColumnIndex).Name = "Selected" Then
            Dim row = dgvPrices.Rows(e.RowIndex)
            Dim sel As Boolean = False
            If row.Cells("Selected").Value IsNot Nothing Then
                sel = CBool(row.Cells("Selected").Value)
            End If

            If sel Then
                row.DefaultCellStyle.BackColor = Color.LightYellow
            Else
                row.DefaultCellStyle.BackColor = Color.White
            End If
        End If
    End Sub

    Private Sub CalculateTotals()

        Dim totalInc As Decimal = 0D
        Dim totalDec As Decimal = 0D

        For Each row As DataGridViewRow In dgvPrices.Rows
            If row.IsNewRow Then Continue For

            Dim pairs As String()() = New String()() {
            New String() {"Price_Current", "Price_New"},
            New String() {"Min_SP_Current", "Min_SP_New"},
            New String() {"Min_SP_2_Current", "Min_SP_2_New"},
            New String() {"OFFER_PRICE_Current", "OFFER_PRICE_New"}
        }

            For Each p As String() In pairs
                Dim oldVal As Decimal = SafeDec(row.Cells(p(0)).Value)
                Dim newVal As Decimal = SafeDec(row.Cells(p(1)).Value, oldVal)

                Dim diff As Decimal = newVal - oldVal

                If diff > 0D Then
                    totalInc += diff
                ElseIf diff < 0D Then
                    totalDec += Math.Abs(diff)
                End If
            Next
        Next

        lblTotalIncrease.Text = "إجمالي الزيادات: " & totalInc.ToString("N3")
        lblTotalDecrease.Text = "إجمالي النقص: " & totalDec.ToString("N3")
        lblNetChange.Text = "صافي التغير: " & (totalInc - totalDec).ToString("N3")

    End Sub

    Private Function SafeDec(val As Object, Optional defaultValue As Decimal = 0D) As Decimal

        If val Is Nothing OrElse IsDBNull(val) Then
            Return defaultValue
        End If

        Dim s As String = val.ToString().Trim()
        If s = "" Then
            Return defaultValue
        End If

        Dim d As Decimal
        If Decimal.TryParse(s, d) Then
            Return d
        Else
            Return defaultValue
        End If

    End Function

    Private Sub clear_data()
        Using con As New SqlConnection(CON_STR)
            Dim sql As String = "
                SELECT 
                    [U_IM_ID],
                    [U_ID],
                    [IM_ID],
                    [IM_Num],
                    [Barcode],
                    [GM_Name],
                    [item_name],
                    [U_NAME],
                    [Price],
                    [Min_SP],
                    [Min_SP_2],
                    [OFFER_PRICE],
                    [GM_ID],
                    [U_Cargo],
                    [is_Default],
                    [UserName],
                    [Cost],
                    [Barcode],
                    [Markter_Val]--,
                    --'' as Price_New,
                    --'' as Min_SP_New,
                    --'' as Min_SP_2_New,
                    --'' as OFFER_PRICE_New
                FROM [dbo].[IM_Menu_Units_V] WHERE IM_ID = -1           
            "

            'If GM_Serach.SelectedValue > 0 Then sql += " AND [GM_ID] = " & GM_Serach.SelectedValue

            sql += " ORDER BY [GM_NAME], [item_name], [U_NAME] "
            Using da As New SqlDataAdapter(sql, con)
                dt = New DataTable()
                da.Fill(dt)
            End Using
        End Using

        ' إعداد أعمدة الجريد
        dgvPrices.Columns.Clear()
        dgvPrices.AutoGenerateColumns = False

        ' مفاتيح مخفية
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .Name = "U_IM_ID",
            .DataPropertyName = "U_IM_ID",
            .Visible = False
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .Name = "IM_ID",
            .DataPropertyName = "IM_ID",
            .Visible = False
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .Name = "U_ID",
            .DataPropertyName = "U_ID",
            .Visible = False
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "GM_ID",
            .Name = "GM_ID",
            .DataPropertyName = "GM_ID",
            .Visible = False
        })

        ' بيانات الصنف

        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "باركود",
            .Name = "Barcode",
            .DataPropertyName = "Barcode"
        })


        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "رقم الصنف",
            .Name = "IM_Num",
            .DataPropertyName = "IM_Num"
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "التصنيف",
            .Name = "GM_Name",
            .DataPropertyName = "GM_Name"
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "اسم الصنف",
            .Name = "item_name",
            .DataPropertyName = "item_name",
            .Width = 200
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "الوحدة",
            .Name = "U_NAME",
            .DataPropertyName = "U_NAME"
        })

        ' الأسعار الحالية
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "سعر عادي (حالي)",
            .Name = "Price_Current",
            .DataPropertyName = "Price",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "جملة (حالي)",
            .Name = "Min_SP_Current",
            .DataPropertyName = "Min_SP",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "جملة جملة (حالي)",
            .Name = "Min_SP_2_Current",
            .DataPropertyName = "Min_SP_2",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "عرض (حالي)",
            .Name = "OFFER_PRICE_Current",
            .DataPropertyName = "OFFER_PRICE",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })

        ' الأسعار الجديدة (معاينة فقط)
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "سعر عادي (جديد)",
            .Name = "Price_New",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "جملة (جديد)",
            .Name = "Min_SP_New",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "جملة جملة (جديد)",
            .Name = "Min_SP_2_New",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })
        dgvPrices.Columns.Add(New DataGridViewTextBoxColumn() With {
            .HeaderText = "عرض (جديد)",
            .Name = "OFFER_PRICE_New",
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "N3"}
        })

        ' عمود اختيار
        Dim colSel As New DataGridViewCheckBoxColumn()
        colSel.HeaderText = "تعديل؟"
        colSel.Name = "Selected"
        dgvPrices.Columns.Add(colSel)

        dgvPrices.DataSource = dt
    End Sub


    Private Sub FrmBulkPriceUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        clear_data()
        fetch_GM()
    End Sub

    Public Sub fetch_GM()
        GM_Serach.DataSource = GetMailItems_GM()
        GM_Serach.DisplayMember = "name"
        GM_Serach.ValueMember = "ID"
        GM_Serach.SelectedIndex = 0
    End Sub

    Function GetMailItems_GM() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "------- كل التصنيفات -------"))

        Dim c1 As New C
        Dim s As String = "select GM_ID AS ID,GM_NAME AS name from General_menu ORDER By GM_ID ASC"
        c1.Com = New SqlClient.SqlCommand(s, c1.Con)
        c1.Con.Open()
        Try
            c1.Dr = c1.Com.ExecuteReader
            If c1.Dr.HasRows Then
                While c1.Dr.Read
                    mailItems.Add(New MailItem(c1.Dr("ID"), c1.Dr("name")))
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
        Return mailItems

    End Function


    Private Sub btnExportGridProtected_Click(sender As Object, e As EventArgs) Handles btnExportGridProtected.Click

        If dgvPrices.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للتصدير.", "تنبيه")
            Exit Sub
        End If

        Dim xlApp As Excel.Application = Nothing
        Dim xlWB As Excel.Workbook = Nothing
        Dim xlWS As Excel.Worksheet = Nothing

        Try
            xlApp = New Excel.Application
            xlWB = xlApp.Workbooks.Add
            xlWS = CType(xlWB.Sheets(1), Excel.Worksheet)

            xlWS.DisplayRightToLeft = True

            ' ✅ 1) تصدير رؤوس الأعمدة
            For c As Integer = 0 To dgvPrices.Columns.Count - 1
                xlWS.Cells(1, c + 1) = dgvPrices.Columns(c).HeaderText
                xlWS.Cells(1, c + 1).Font.Bold = True
            Next

            ' ✅ 2) تصدير البيانات كما هي
            For r As Integer = 0 To dgvPrices.Rows.Count - 1
                For c As Integer = 0 To dgvPrices.Columns.Count - 1
                    xlWS.Cells(r + 2, c + 1) = dgvPrices.Rows(r).Cells(c).Value
                Next
            Next

            Dim lastRow As Integer = dgvPrices.Rows.Count + 1
            Dim lastCol As Integer = dgvPrices.Columns.Count

            ' ✅ 3) حماية الأعمدة الحالية (التي لا تنتهي بـ _New)
            For c As Integer = 0 To dgvPrices.Columns.Count - 1
                Dim colName As String = dgvPrices.Columns(c).Name.ToLower()

                Dim colRange As Excel.Range =
                xlWS.Range(xlWS.Cells(2, c + 1), xlWS.Cells(lastRow, c + 1))

                If Not colName.EndsWith("_new") Then
                    ' 🔒 قفل العمود
                    colRange.Locked = True
                    colRange.Interior.Color = RGB(230, 230, 230) ' رمادي فاتح
                Else
                    ' ✏️ فتح العمود للتعديل
                    colRange.Locked = False

                    ' 🚫 منع القيم السالبة
                    colRange.Validation.Delete()
                    colRange.Validation.Add(
                    Excel.XlDVType.xlValidateDecimal,
                    Excel.XlDVAlertStyle.xlValidAlertStop,
                    Excel.XlFormatConditionOperator.xlGreaterEqual,
                    0
                )
                    colRange.Validation.ErrorTitle = "قيمة غير صحيحة"
                    colRange.Validation.ErrorMessage = "يجب إدخال رقم أكبر أو يساوي صفر"
                End If
            Next

            ' ✅ 4) حماية الورقة بكلمة مرور
            'xlWS.Protect(Password:="123", DrawingObjects:=True, Contents:=True, Scenarios:=True)

            'xlWS.Columns.AutoFit()
            xlApp.Visible = True

            MessageBox.Show("تم تصدير البيانات مع الحماية بنجاح." & vbCrLf &
                        "عدّل فقط أعمدة الأسعار الجديدة ثم احفظ الملف وأعد استيراده.",
                        "نجاح")

        Catch ex As Exception
            MessageBox.Show("خطأ أثناء التصدير:" & vbCrLf & ex.Message)
        Finally
            If xlWS IsNot Nothing Then Marshal.ReleaseComObject(xlWS)
            If xlWB IsNot Nothing Then Marshal.ReleaseComObject(xlWB)
            If xlApp IsNot Nothing Then Marshal.ReleaseComObject(xlApp)
            xlWS = Nothing : xlWB = Nothing : xlApp = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try

    End Sub


    'Private Sub btnImportFromExcel_Click(sender As Object, e As EventArgs) Handles btnImportFromExcel.Click

    '    If dgvPrices.DataSource Is Nothing Then
    '        MessageBox.Show("الجريد غير مرتبط ببيانات.", "خطأ")
    '        Exit Sub
    '    End If

    '    Dim dt As DataTable = TryCast(dgvPrices.DataSource, DataTable)
    '    If dt Is Nothing Then
    '        MessageBox.Show("مصدر البيانات غير صحيح.", "خطأ")
    '        Exit Sub
    '    End If

    '    Using ofd As New OpenFileDialog()
    '        ofd.Filter = "Excel Files|*.xlsx;*.xls"
    '        If ofd.ShowDialog() <> DialogResult.OK Then Exit Sub

    '        Dim xlApp As Excel.Application = Nothing
    '        Dim xlWB As Excel.Workbook = Nothing
    '        Dim xlWS As Excel.Worksheet = Nothing

    '        Try
    '            xlApp = New Excel.Application
    '            xlWB = xlApp.Workbooks.Open(ofd.FileName)
    '            xlWS = CType(xlWB.Sheets(1), Excel.Worksheet)

    '            ' ✅ تحديد الأعمدة بالاسم العربي كما في ملفك
    '            Dim col_U_IM_ID = GetExcelColIndexByHeader(xlWS, "U_IM_ID")
    '            Dim col_Price_New = GetExcelColIndexByHeader(xlWS, "سعر عادي (جديد)")
    '            Dim col_Min_SP_New = GetExcelColIndexByHeader(xlWS, "جملة (جديد)")
    '            Dim col_Min_SP_2_New = GetExcelColIndexByHeader(xlWS, "جملة جملة (جديد)")
    '            Dim col_OFFER_New = GetExcelColIndexByHeader(xlWS, "عرض (جديد)")

    '            If col_U_IM_ID = -1 Then
    '                MessageBox.Show("لم يتم العثور على العمود U_IM_ID داخل الملف.", "خطأ")
    '                Exit Sub
    '            End If

    '            Dim rowIndex As Integer = 2
    '            Dim updatedCount As Integer = 0

    '            While xlWS.Cells(rowIndex, col_U_IM_ID).Value IsNot Nothing

    '                Dim u_im_id As Integer = SafeInt(xlWS.Cells(rowIndex, col_U_IM_ID).Value)

    '                Dim priceNew = SafeDec(xlWS.Cells(rowIndex, col_Price_New).Value, -1)
    '                Dim minSpNew = SafeDec(xlWS.Cells(rowIndex, col_Min_SP_New).Value, -1)
    '                Dim minSp2New = SafeDec(xlWS.Cells(rowIndex, col_Min_SP_2_New).Value, -1)
    '                Dim offerNew = SafeDec(xlWS.Cells(rowIndex, col_OFFER_New).Value, -1)

    '                ' ✅ تجاهل الصف الذي لا يحتوي أي تعديل
    '                If priceNew < 0 AndAlso minSpNew < 0 AndAlso minSp2New < 0 AndAlso offerNew < 0 Then
    '                    rowIndex += 1
    '                    Continue While
    '                End If

    '                Dim foundRows = dt.Select("U_IM_ID = " & u_im_id)

    '                If foundRows.Length > 0 Then
    '                    If priceNew >= 0 Then foundRows(0)("Price_New") = priceNew
    '                    If minSpNew >= 0 Then foundRows(0)("Min_SP_New") = minSpNew
    '                    If minSp2New >= 0 Then foundRows(0)("Min_SP_2_New") = minSp2New
    '                    If offerNew >= 0 Then foundRows(0)("OFFER_PRICE_New") = offerNew

    '                    If dt.Columns.Contains("Selected") Then
    '                        foundRows(0)("Selected") = True
    '                    End If

    '                    updatedCount += 1
    '                End If

    '                rowIndex += 1
    '            End While

    '            MessageBox.Show("تم استيراد التعديلات بنجاح. عدد الصفوف المعدلة: " & updatedCount, "نجاح")

    '        Catch ex As Exception
    '            MessageBox.Show("خطأ أثناء الاستيراد:" & vbCrLf & ex.Message)
    '        Finally
    '            If xlWB IsNot Nothing Then xlWB.Close(False)
    '            If xlApp IsNot Nothing Then xlApp.Quit()

    '            If xlWS IsNot Nothing Then Marshal.ReleaseComObject(xlWS)
    '            If xlWB IsNot Nothing Then Marshal.ReleaseComObject(xlWB)
    '            If xlApp IsNot Nothing Then Marshal.ReleaseComObject(xlApp)

    '            xlWS = Nothing : xlWB = Nothing : xlApp = Nothing
    '            GC.Collect()
    '        End Try
    '    End Using
    'End Sub


    Private Function GetExcelColIndexByHeader(ws As Object, headerText As String) As Integer
        Dim col As Integer = 1
        While ws.Cells(1, col).Value IsNot Nothing
            If ws.Cells(1, col).Value.ToString().Trim() = headerText.Trim() Then
                Return col
            End If
            col += 1
        End While
        Return -1
    End Function



    Private Sub btnImportFromExcel_Click(sender As Object, e As EventArgs) Handles btnImportFromExcel.Click

        Using ofd As New OpenFileDialog()
            ofd.Filter = "Excel Files|*.xlsx;*.xls"

            If ofd.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim xlApp As Excel.Application = Nothing
            Dim xlWB As Excel.Workbook = Nothing
            Dim xlWS As Excel.Worksheet = Nothing

            Try
                xlApp = New Excel.Application
                xlWB = xlApp.Workbooks.Open(ofd.FileName)
                xlWS = CType(xlWB.Sheets(1), Excel.Worksheet)

                Dim rowIndex As Integer = 2 ' بعد رؤوس الأعمدة

                While xlWS.Cells(rowIndex, 1).Value IsNot Nothing

                    Dim u_im_id As Integer = SafeInt(xlWS.Cells(rowIndex, 1).Value)

                    For Each row As DataGridViewRow In dgvPrices.Rows
                        If SafeInt(row.Cells("U_IM_ID").Value) = u_im_id Then

                            row.Cells("Price_New").Value =
                                SafeDec(xlWS.Cells(rowIndex, dgvPrices.Columns("Price_New").Index + 1).Value)

                            row.Cells("Min_SP_New").Value =
                                SafeDec(xlWS.Cells(rowIndex, dgvPrices.Columns("Min_SP_New").Index + 1).Value)

                            row.Cells("Min_SP_2_New").Value =
                                SafeDec(xlWS.Cells(rowIndex, dgvPrices.Columns("Min_SP_2_New").Index + 1).Value)

                            row.Cells("OFFER_PRICE_New").Value =
                                SafeDec(xlWS.Cells(rowIndex, dgvPrices.Columns("OFFER_PRICE_New").Index + 1).Value)

                            ' ✅ تفعيل الصف تلقائياً للتطبيق
                            If dgvPrices.Columns.Contains("Selected") Then
                                row.Cells("Selected").Value = True
                            End If

                            Exit For
                        End If
                    Next

                    rowIndex += 1
                End While

                MessageBox.Show("تم استيراد الأسعار من Excel بنجاح.", "نجاح")

            Catch ex As Exception
                MessageBox.Show("خطأ أثناء الاستيراد:" & vbCrLf & ex.Message)
            Finally
                If xlWB IsNot Nothing Then xlWB.Close(False)
                If xlApp IsNot Nothing Then xlApp.Quit()

                If xlWS IsNot Nothing Then Marshal.ReleaseComObject(xlWS)
                If xlWB IsNot Nothing Then Marshal.ReleaseComObject(xlWB)
                If xlApp IsNot Nothing Then Marshal.ReleaseComObject(xlApp)

                xlWS = Nothing : xlWB = Nothing : xlApp = Nothing
                GC.Collect()
                GC.WaitForPendingFinalizers()
            End Try

        End Using
    End Sub


End Class
