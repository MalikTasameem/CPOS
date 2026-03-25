Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class ReportConnection

    Public rp As New ReportDocument
    Public crtableLogoninfo As New TableLogOnInfo
    Public crConnectionInfo As New ConnectionInfo
    Public CrTables As CrystalDecisions.CrystalReports.Engine.Tables
    Public CrTable As CrystalDecisions.CrystalReports.Engine.Table

    Public Sub New()
        With crConnectionInfo

            If My_Settings.DB_Authentication = 0 Then
                .ServerName = My_Settings.S_SERVER
                .DatabaseName = MY_Settings.DataBase
                .IntegratedSecurity = True
            Else
                .ServerName = MY_Settings.S_SERVER
                .DatabaseName = MY_Settings.DataBase
                .UserID = MY_Settings.DB_UName
                .Password = MY_Settings.DB_Pass
                .IntegratedSecurity = False
            End If

        End With
    End Sub

    Public Sub LoadTables()
        CrTables = rp.Database.Tables
        For Each Me.CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next
    End Sub

End Class
