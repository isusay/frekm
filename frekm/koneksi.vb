
Imports MySql.Data.MySqlClient


Module ModuleKoneksiMySQL
    Public conn As New MySql.Data.MySqlClient.MySqlConnection
    Public myError As MySql.Data.MySqlClient.MySqlError
    Public cmd As MySql.Data.MySqlClient.MySqlCommand
    Public rd As MySql.Data.MySqlClient.MySqlDataReader
    Public da As MySql.Data.MySqlClient.MySqlDataAdapter
    Public ds As DataSet
    Public str As String


    Sub koneksine()
        If conn.State = ConnectionState.Closed Then

            Dim lokasi As String = My.Application.Info.DirectoryPath
            Dim fileReader As String

            fileReader = My.Computer.FileSystem.ReadAllText(lokasi & "\alamat_server.txt")
            Dim str As String = fileReader
            Dim str2() As String = Split(str, ",")

            Dim host As String = str2(0)
            Dim user As String = str2(1)
            Dim pass As String = str2(3)
            Dim db As String = str2(2)
            Dim spk As String = str(4)

            Dim mystring As String = "Server=" & host & "; user=" & user & "; password=" & pass & "; database=" & db & ""
            Try
                conn.ConnectionString = mystring
                conn.Open()
            Catch ex As MySql.Data.MySqlClient.MySqlException
                MsgBox(ex.Message)
            End Try
        End If
        'Try
        '    Dim str As String = "Server=localhost;user id=root;password=;database=zenso"
        '    conn = New MySqlConnection(str)
        '    If conn.State = ConnectionState.Closed Then
        '        conn.Open()
        '    End If
        'Catch ex As MySql.Data.MySqlClient.MySqlException
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub



End Module



