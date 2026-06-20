<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NewArchive
    Inherits Base_Form
    'System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewArchive))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnAddDocType = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CircularProgressControl1 = New Accounting.CircularProgressControl()
        Me.lb = New System.Windows.Forms.Label()
        Me.FromScanner = New System.Windows.Forms.Button()
        Me.FromFile = New System.Windows.Forms.Button()
        Me.ezdbox = New System.Windows.Forms.GroupBox()
        Me.txtezd = New Accounting.FSearch_Filter()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.MainBack = New System.ComponentModel.BackgroundWorker()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.AxScanner1 = New AxSCANNERLib.AxScanner()
        Me.fsDocType = New Accounting.FSearch_Filter()
        Me.Panel1.SuspendLayout()
        Me.ezdbox.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.AxScanner1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Multiselect = True
        '
        'btnAddDocType
        '
        Me.btnAddDocType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAddDocType.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnAddDocType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddDocType.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddDocType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnAddDocType.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.btnAddDocType.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnAddDocType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAddDocType.Font = New System.Drawing.Font("Arial Narrow", 14.0!, System.Drawing.FontStyle.Bold)
        Me.btnAddDocType.ForeColor = System.Drawing.Color.Black
        Me.btnAddDocType.Location = New System.Drawing.Point(13, 39)
        Me.btnAddDocType.Name = "btnAddDocType"
        Me.btnAddDocType.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnAddDocType.Size = New System.Drawing.Size(34, 34)
        Me.btnAddDocType.TabIndex = 1132
        Me.btnAddDocType.Tag = "DocumentType"
        Me.btnAddDocType.Text = "🞦"
        Me.btnAddDocType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnAddDocType.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.CircularProgressControl1)
        Me.Panel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Panel1.Location = New System.Drawing.Point(49, 125)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(353, 259)
        Me.Panel1.TabIndex = 6557
        Me.Panel1.Visible = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Sakkal Majalla", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label12.Size = New System.Drawing.Size(349, 29)
        Me.Label12.TabIndex = 211
        Me.Label12.Text = "جاري تحميل الملف ..."
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("JF Flat", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(359, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(200, 34)
        Me.Label1.TabIndex = 6560
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CircularProgressControl1
        '
        Me.CircularProgressControl1.BackColor = System.Drawing.Color.Transparent
        Me.CircularProgressControl1.Interval = 60
        Me.CircularProgressControl1.Location = New System.Drawing.Point(0, 32)
        Me.CircularProgressControl1.MinimumSize = New System.Drawing.Size(28, 28)
        Me.CircularProgressControl1.Name = "CircularProgressControl1"
        Me.CircularProgressControl1.Rotation = Accounting.CircularProgressControl.Direction.CLOCKWISE
        Me.CircularProgressControl1.Size = New System.Drawing.Size(327, 81)
        Me.CircularProgressControl1.StartAngle = 270
        Me.CircularProgressControl1.TabIndex = 0
        Me.CircularProgressControl1.TickColor = System.Drawing.Color.White
        '
        'lb
        '
        Me.lb.Dock = System.Windows.Forms.DockStyle.Top
        Me.lb.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb.ForeColor = System.Drawing.Color.Black
        Me.lb.Location = New System.Drawing.Point(0, 0)
        Me.lb.Name = "lb"
        Me.lb.Size = New System.Drawing.Size(923, 34)
        Me.lb.TabIndex = 6558
        Me.lb.Text = "نوع المستند"
        Me.lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FromScanner
        '
        Me.FromScanner.BackColor = System.Drawing.Color.White
        Me.FromScanner.Cursor = System.Windows.Forms.Cursors.Hand
        Me.FromScanner.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.FromScanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.FromScanner.Font = New System.Drawing.Font("Sakkal Majalla", 18.75!, System.Drawing.FontStyle.Bold)
        Me.FromScanner.ForeColor = System.Drawing.Color.Black
        Me.FromScanner.Image = CType(resources.GetObject("FromScanner.Image"), System.Drawing.Image)
        Me.FromScanner.Location = New System.Drawing.Point(62, 251)
        Me.FromScanner.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.FromScanner.Name = "FromScanner"
        Me.FromScanner.Size = New System.Drawing.Size(334, 100)
        Me.FromScanner.TabIndex = 13
        Me.FromScanner.Text = "حفظ المستند من سكانر"
        Me.FromScanner.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.FromScanner.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.FromScanner.UseVisualStyleBackColor = False
        '
        'FromFile
        '
        Me.FromFile.BackColor = System.Drawing.Color.White
        Me.FromFile.Cursor = System.Windows.Forms.Cursors.Hand
        Me.FromFile.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.FromFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.FromFile.Font = New System.Drawing.Font("Sakkal Majalla", 18.75!, System.Drawing.FontStyle.Bold)
        Me.FromFile.ForeColor = System.Drawing.Color.Black
        Me.FromFile.Image = CType(resources.GetObject("FromFile.Image"), System.Drawing.Image)
        Me.FromFile.Location = New System.Drawing.Point(62, 139)
        Me.FromFile.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.FromFile.Name = "FromFile"
        Me.FromFile.Size = New System.Drawing.Size(334, 102)
        Me.FromFile.TabIndex = 12
        Me.FromFile.Text = "حفظ المستند من ملف "
        Me.FromFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.FromFile.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.FromFile.UseVisualStyleBackColor = False
        '
        'ezdbox
        '
        Me.ezdbox.Controls.Add(Me.txtezd)
        Me.ezdbox.Controls.Add(Me.Button4)
        Me.ezdbox.Location = New System.Drawing.Point(330, 701)
        Me.ezdbox.Name = "ezdbox"
        Me.ezdbox.Size = New System.Drawing.Size(270, 213)
        Me.ezdbox.TabIndex = 178
        Me.ezdbox.TabStop = False
        Me.ezdbox.Text = "مكان الازدواج"
        '
        'txtezd
        '
        Me.txtezd.BackColor = System.Drawing.Color.Gainsboro
        Me.txtezd.Location = New System.Drawing.Point(10, 26)
        Me.txtezd.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtezd.Name = "txtezd"
        Me.txtezd.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtezd.Size = New System.Drawing.Size(255, 34)
        Me.txtezd.SQL_Column = "HealthState"
        Me.txtezd.SQL_ID = Nothing
        Me.txtezd.SQL_IsNumericSearchField = False
        Me.txtezd.SQL_ListSize = 200
        Me.txtezd.SQL_NumberOfRows = 200
        Me.txtezd.SQL_OrderByField = "ID"
        Me.txtezd.SQL_SearchField = "HealthState"
        Me.txtezd.SQL_SearchField_WHERE = ""
        Me.txtezd.SQL_Table = "T2HealthState"
        Me.txtezd.TabIndex = 189
        Me.txtezd.TextMaxLength = 250
        Me.txtezd.Textt = ""
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.Silver
        Me.Button4.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.Button4.Location = New System.Drawing.Point(10, 65)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(100, 35)
        Me.Button4.TabIndex = 187
        Me.Button4.TabStop = False
        Me.Button4.Text = "الغاء الازدواج"
        Me.Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.Button4.UseVisualStyleBackColor = False
        '
        'MainBack
        '
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.Controls.Add(Me.AxScanner1)
        Me.pnlMain.Controls.Add(Me.btnAddDocType)
        Me.pnlMain.Controls.Add(Me.lb)
        Me.pnlMain.Controls.Add(Me.Panel1)
        Me.pnlMain.Controls.Add(Me.fsDocType)
        Me.pnlMain.Controls.Add(Me.FromScanner)
        Me.pnlMain.Controls.Add(Me.FromFile)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(923, 610)
        Me.pnlMain.TabIndex = 179
        '
        'AxScanner1
        '
        Me.AxScanner1.Enabled = True
        Me.AxScanner1.Location = New System.Drawing.Point(446, 41)
        Me.AxScanner1.Name = "AxScanner1"
        Me.AxScanner1.OcxState = CType(resources.GetObject("AxScanner1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxScanner1.Size = New System.Drawing.Size(472, 566)
        Me.AxScanner1.TabIndex = 6564
        '
        'fsDocType
        '
        Me.fsDocType.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fsDocType.Location = New System.Drawing.Point(49, 41)
        Me.fsDocType.Name = "fsDocType"
        Me.fsDocType.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.fsDocType.Size = New System.Drawing.Size(391, 29)
        Me.fsDocType.SQL_Column = "Name"
        Me.fsDocType.SQL_ID = "ID"
        Me.fsDocType.SQL_IsNumericSearchField = False
        Me.fsDocType.SQL_ListSize = 200
        Me.fsDocType.SQL_NumberOfRows = 0
        Me.fsDocType.SQL_OrderByField = "Name"
        Me.fsDocType.SQL_SearchField = "Name"
        Me.fsDocType.SQL_SearchField_WHERE = ""
        Me.fsDocType.SQL_Table = "DocumentType"
        Me.fsDocType.TabIndex = 6563
        Me.fsDocType.TextMaxLength = 250
        Me.fsDocType.Textt = ""
        '
        'NewArchive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.HighlightText
        Me.ClientSize = New System.Drawing.Size(923, 610)
        Me.Controls.Add(Me.ezdbox)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 8, 3, 8)
        Me.Name = "NewArchive"
        Me.ShowIcon = False
        Me.Panel1.ResumeLayout(False)
        Me.ezdbox.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        CType(Me.AxScanner1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents DelImage As Button
    Friend WithEvents FromFile As Button
    Friend WithEvents FromScanner As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents ezdbox As GroupBox
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    'Private WithEvents AxShockwaveFlash1 As FlashControlV71.AxShockwaveFlash
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents MainBack As System.ComponentModel.BackgroundWorker
    Public WithEvents lb As Label
    Public WithEvents Label1 As Label
    Friend WithEvents txtezd As FSearch_Filter
    Friend WithEvents CircularProgressControl1 As CircularProgressControl
    Friend WithEvents fsDocType As FSearch_Filter
    Friend WithEvents btnAddDocType As Button
    Friend WithEvents pnlMain As Panel
    Friend WithEvents AxScanner1 As AxSCANNERLib.AxScanner
End Class
