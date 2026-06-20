Imports System.Data.SqlClient
Imports System.Threading.Tasks
Module Task_1

    Public Function LoadDataTable(sql As String, sqlconnection As String) As DataTable
        ' NO UI/Control references allowed
        Dim DTA = New DataTable
        Using DBCon As New SqlConnection(sqlconnection)
            Using cmd As New SqlCommand(sql, DBCon)
                DBCon.Open()
                ' DTA.Load(cmd.ExecuteReader())
                cmd.ExecuteReader()
                DBCon.Close()
            End Using
        End Using
        Return DTA
    End Function
    Public Function LoadDataTable(com As SqlCommand, sqlconnection As String) As DataTable
        'NO UI / Control references allowed
        'Dim DTA = New DataTable
        'Using DBCon As New SqlConnection(sqlconnection)
        '    com.Connection = DBCon
        '    Using com
        '        DBCon.Open()
        '        DTA.Load(com.ExecuteReader())
        '        DBCon.Close()
        '    End Using
        'End Using
        'Return DTA
        '------------------------------------------
        Dim C As New C
        com.CommandTimeout = 0
        C.Da = New SqlClient.SqlDataAdapter(com)
        C.Da.Fill(C.Dt)
        Return C.Dt
    End Function
End Module