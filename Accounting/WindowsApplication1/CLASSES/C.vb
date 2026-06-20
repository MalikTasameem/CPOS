Imports System.Data.SqlClient

Public Class C

    Public Con As New SqlConnection()
    Public Com As New SqlCommand
    Public Da As New SqlDataAdapter
    Public Dr As SqlDataReader
    Public Ds As New DataSet
    Public Dt As New DataTable
    Public DV As DataView
    Public Str As String

    Public Sub New()

        Con = New SqlConnection(MY_Settings.SqlConStr)

        'Con = New SqlConnection("Data Source=.\TEST_2 ;initial catalog=Tree_Test;Integrated Security=True;")
        'MY_Settings.SqlConStr = "Data Source=.\TEST_2 ;initial catalog=Tree_Test;Integrated Security=True;"

    End Sub

End Class
