Public Class Resizer

        '----------------------------------------------------------
        ' ControlInfo
        ' Structure of original state of all processed controls
        '----------------------------------------------------------
        Private Structure ControlInfo
            Public name As String
            Public parentName As String
            Public leftOffsetPercent As Double
            Public topOffsetPercent As Double
            Public heightPercent As Double
            Public originalHeight As Integer
            Public originalWidth As Integer
            Public widthPercent As Double
            Public originalFontSize As Single
        End Structure

        '-------------------------------------------------------------------------
        ' ctrlDict
        ' Dictionary of (control name, control info) for all processed controls
        '-------------------------------------------------------------------------
    Private ctrlDict As Dictionary(Of String, ControlInfo) = New Dictionary(Of String, ControlInfo)

        '----------------------------------------------------------------------------------------
        ' FindAllControls
        ' Recursive function to process all controls contained in the initially passed
        ' control container and store it in the Control dictionary
        '----------------------------------------------------------------------------------------
        Public Sub FindAllControls(thisCtrl As Control)
        '-- If the current control has a parent, store all original relative position
        '-- and size information in the dictionary.
        '-- Recursively call FindAllControls for each control contained in the
        '-- current Control
        For Each ctl As Control In thisCtrl.Controls


            ' If Not TypeOf ctl Is FSearch_Filter Then
            Try
                If Not IsNothing(ctl.Parent) Then
                    Dim parentHeight = ctl.Parent.Height
                    Dim parentWidth = ctl.Parent.Width

                    ' ctl.Anchor = AnchorStyles.Left + AnchorStyles.Top
                    Dim c As New ControlInfo
                    c.name = ctl.Name


                    c.parentName = ctl.Parent.Name
                    c.topOffsetPercent = Convert.ToDouble(ctl.Top) / Convert.ToDouble(parentHeight)
                    c.leftOffsetPercent = Convert.ToDouble(ctl.Left) / Convert.ToDouble(parentWidth)
                    c.heightPercent = Convert.ToDouble(ctl.Height) / Convert.ToDouble(parentHeight)
                    c.widthPercent = Convert.ToDouble(ctl.Width) / Convert.ToDouble(parentWidth)
                    c.originalFontSize = ctl.Font.Size
                    c.originalHeight = ctl.Height
                    c.originalWidth = ctl.Width
                    ctrlDict.Add(c.name, c)

                End If

            Catch ex As Exception
                Debug.Print(ex.Message)
            End Try

            If ctl.Controls.Count > 0 Then FindAllControls(ctl)



        Next

    End Sub

        '----------------------------------------------------------------------------------------
        ' ResizeAllControls
        ' Recursive function to resize and reposition all controls contained in the Control
        ' dictionary
        '----------------------------------------------------------------------------------------
        Public Sub ResizeAllControls(thisCtrl As Control)

        Dim fontRatioW As Single
        Dim fontRatioH As Single
        Dim fontRatio As Single
        Dim f As Font

        '-- Resize and reposition all controls in the passed control
        For Each ctl As Control In thisCtrl.Controls
            Try
                If Not IsNothing(ctl.Parent) Then
                    Dim parentHeight = ctl.Parent.Height
                    Dim parentWidth = ctl.Parent.Width

                    Dim c As New ControlInfo

                    Dim ret As Boolean = False
                    Try




                        '-- Get the current control's info from the control info dictionary
                        ret = ctrlDict.TryGetValue(ctl.Name, c)


                        '-- If found, adjust the current control based on control relative
                        '-- size and position information stored in the dictionary
                        If (ret) Then
                            '-- Size
                            ctl.Width = Int(parentWidth * c.widthPercent)
                            ctl.Height = Int(parentHeight * c.heightPercent)

                            '-- Position
                            ctl.Top = Int(parentHeight * c.topOffsetPercent)
                            ctl.Left = Int(parentWidth * c.leftOffsetPercent)


                            If is_Screen_Font_Resize = True Then
                                If Not TypeOf ctl Is FSearch_Filter And Not TypeOf ctl Is DateRange_Flate And Not TypeOf ctl Is Zuby.ADGV.AdvancedDataGridViewSearchToolBar Then
                                    f = ctl.Font
                                    fontRatioW = ctl.Width / c.originalWidth
                                    fontRatioH = ctl.Height / c.originalHeight
                                    fontRatio = (fontRatioW +
                            fontRatioH) / Font_Resize_Num '-- average change in control Height and Width
                                    ctl.Font = New Font(f.FontFamily,
                             c.originalFontSize * fontRatio, f.Style)

                                End If
                            End If


                        End If

                    Catch
                    End Try
                End If
            Catch ex As Exception
            End Try

            '-- Recursive call for controls contained in the current control
            If ctl.Controls.Count > 0 And Not TypeOf ctl Is FSearch_Filter And Not TypeOf ctl Is DateRange_Flate Then ResizeAllControls(ctl)

        Next '-- For Each
    End Sub
   
    Public Sub Find_One(ByRef ctl As Control)

        If ctrlDict.ContainsKey(ctl.Name) = False Then

            Dim parentHeight = ctl.Parent.Height
            Dim parentWidth = ctl.Parent.Width

            Dim c As New ControlInfo
            c.name = ctl.Name
            c.parentName = ctl.Parent.Name
            c.topOffsetPercent = Convert.ToDouble(ctl.Top) / Convert.ToDouble(parentHeight)
            c.leftOffsetPercent = Convert.ToDouble(ctl.Left) / Convert.ToDouble(parentWidth)
            c.heightPercent = Convert.ToDouble(ctl.Height) / Convert.ToDouble(parentHeight)
            c.widthPercent = Convert.ToDouble(ctl.Width) / Convert.ToDouble(parentWidth)
            c.originalFontSize = ctl.Font.Size
            c.originalHeight = ctl.Height
            c.originalWidth = ctl.Width
            ctrlDict.Add(c.name, c)
        End If

    End Sub


    'Public Sub FindAllControls_POS(thisCtrl As Control)
    '    '-- If the current control has a parent, store all original relative position
    '    '-- and size information in the dictionary.
    '    '-- Recursively call FindAllControls for each control contained in the
    '    '-- current Control
    '    For Each ctl As Control In thisCtrl.Controls
    '        Try
    '            If Not IsNothing(ctl.Parent) Then
    '                Dim parentHeight = ctl.Parent.Height
    '                Dim parentWidth = ctl.Parent.Width

    '                Dim c As New ControlInfo
    '                c.name = ctl.Name
    '                c.parentName = ctl.Parent.Name
    '                c.topOffsetPercent = Convert.ToDouble(ctl.Top) / Convert.ToDouble(parentHeight)
    '                c.leftOffsetPercent = Convert.ToDouble(ctl.Left) / Convert.ToDouble(parentWidth)
    '                c.heightPercent = Convert.ToDouble(ctl.Height) / Convert.ToDouble(parentHeight)
    '                c.widthPercent = Convert.ToDouble(ctl.Width) / Convert.ToDouble(parentWidth)
    '                c.originalFontSize = ctl.Font.Size
    '                c.originalHeight = ctl.Height
    '                c.originalWidth = ctl.Width
    '                ctrlDict.Add(c.name, c)
    '            End If

    '        Catch ex As Exception
    '            Debug.Print(ex.Message)
    '        End Try

    '        If ctl.Controls.Count > 0 Then FindAllControls(ctl)

    '    Next '-- For Each

    'End Sub

    Public Sub ResizeAllControls_POS(thisCtrl As Control)

        Dim fontRatioW As Single
        Dim fontRatioH As Single
        Dim fontRatio As Single
        Dim f As Font

        '-- Resize and reposition all controls in the passed control
        For Each ctl As Control In thisCtrl.Controls
            Try
                If Not IsNothing(ctl.Parent) Then
                    Dim parentHeight = ctl.Parent.Height
                    Dim parentWidth = ctl.Parent.Width

                    Dim c As New ControlInfo

                    Dim ret As Boolean = False
                    Try
                        '-- Get the current control's info from the control info dictionary
                        ret = ctrlDict.TryGetValue(ctl.Name, c)

                        '-- If found, adjust the current control based on control relative
                        '-- size and position information stored in the dictionary
                        If (ret) Then
                            '-- Size
                            ctl.Width = Int(parentWidth * c.widthPercent)
                            ctl.Height = Int(parentHeight * c.heightPercent)

                            '-- Position
                            ctl.Top = Int(parentHeight * c.topOffsetPercent)
                            ctl.Left = Int(parentWidth * c.leftOffsetPercent)

                            ' -- Font
                            f = ctl.Font
                            fontRatioW = ctl.Width / c.originalWidth
                            fontRatioH = ctl.Height / c.originalHeight
                            fontRatio = (fontRatioW +
                            fontRatioH) / 2 '-- average change in control Height and Width
                            ctl.Font = New Font(f.FontFamily,
                             c.originalFontSize * fontRatio, f.Style)

                        End If
                    Catch
                    End Try
                End If
            Catch ex As Exception
            End Try

            '-- Recursive call for controls contained in the current control
            If ctl.Controls.Count > 0 Then ResizeAllControls_POS(ctl)

        Next '-- For Each
    End Sub



End Class
