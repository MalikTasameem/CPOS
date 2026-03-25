Imports System.Data.SqlClient
Public Class Online_C


    Public Con As New SqlConnection()
    Public Com As New SqlCommand
    Public Da As New SqlDataAdapter
    Public Dr As SqlDataReader
    Public Ds As New DataSet
    Public Dt As New DataTable
    Public DV As DataView
    Public Str As String

    Public Sub New()
        Con = New SqlConnection(MY_Settings.Online_Con_Str)
    End Sub


End Class
