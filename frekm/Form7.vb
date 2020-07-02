Imports MySql.Data.MySqlClient

Imports System.Security.Cryptography
Imports System.Text
Imports System.IO

Public Class Form7

    Dim password As String
    Dim username As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'If tb_username.Text = username And tb_password.Text = password Then
        '    Form1.Show()
        '    Me.Hide()
        'End If

        'Try

        '    Dim MySQLConnection As New MySqlConnection("Server = localhost;Database = zenso; Uid=root; Pwd =  ")

        '    'Dim SqlQuery As String = "SELECT COUNT(*) From tbl_user WHERE email = @Username AND password = MD5(@Password); "
        '    Dim SqlQuery As String = "SELECT COUNT(*) From tbl_user WHERE email = " & tb_username.Text & " AND password = " & GetHashCode.ToString(tb_password.Text) & "; "



        '    MySQLConnection.Open()

        '    Dim Command As New MySqlCommand(SqlQuery, MySQLConnection)

        '    Command.Parameters.Add(New MySqlParameter("@Username", tb_username.Text))
        '    Command.Parameters.Add(New MySqlParameter("@Password", tb_password.Text))

        '    If Command.ExecuteScalar() = 1 Then
        '        MsgBox("Thanks for logging in")
        '    Else
        '        MsgBox("Invalid username or password")
        '    End If

        '    MySQLConnection.Close()

        'Catch ex As Exception

        '    MsgBox(ex.Message)
        'End Try

        If ValidasiEntry() = True Then
            tb_username.Select()
            Exit Sub
        End If

        Call conecDB()      'Open the connection to database
        Call initCMD()      'Initialize the sqlclient command object
        Call CekUser()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()

    End Sub

    Private Sub Panel4_Click(sender As Object, e As EventArgs)
        Form1.Show()
        Me.Hide()
    End Sub

    Private Function ValidasiEntry() As Boolean
        'Make sure that all fields have values
        If Me.tb_username.Text.Trim = "" Or
            Me.tb_password.Text.Trim = "" Then
            MsgBox("Username & Password Harus Diisi!", MsgBoxStyle.Exclamation, "Insufficient Data")
            Return True
        End If
    End Function

    'Load data user
    Private Sub CekUser()
        Try
            str = "SELECT * from tbl_user " &
                 "WHERE is_aktif = 'y' " &
                 "AND email ='" & Me.tb_username.Text & "' " &
                 "AND hint ='" & tb_password.Text & "'"

            With comDB
                .CommandText = str
                .ExecuteNonQuery()
            End With
            rdDB = comDB.ExecuteReader
            rdDB.Read()

            If rdDB.HasRows = True Then
                Label4.Text = rdDB.Item("full_name")
                Label5.Text = rdDB.Item("id_user_level")

                Me.Hide()
                Form1.Show()
                'Form1.lblUser.Text = rdDB!user_username.ToString.Trim
            Else
                MsgBox("Username and or Password are not found", MsgBoxStyle.Exclamation, "Information")
                tb_username.Text = ""
                tb_password.Text = ""
                tb_username.Select()
            End If
            rdDB.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.tb_password.AutoSize = False
        Me.tb_password.Height = 40

        Me.tb_username.AutoSize = False
        Me.tb_username.Height = 40
        'Me.tb_username.TextAlign = VerticalAlignment.Center
        tb_username.Text = "Username"
        tb_password.Text = "Password"
        Button1.Select()


    End Sub

    Private Sub ambil_data()
        Try
            Call koneksine()
            Dim str1 As String

            str1 = "SELECT * FROM tbl_user WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            cmd = New MySqlCommand(str1, conn)
            rd = cmd.ExecuteReader
            rd.Read()

            If rd.HasRows Then
                username = rd.Item("email")
                password = rd.Item("password")

                conn.Close()

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub tb_username_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_username.GotFocus
        tb_username.Text = ""
        tb_username.ForeColor = Color.Black
    End Sub

    Private Sub tb_username_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_username.LostFocus
        If tb_username.Text = "" Then
            tb_username.Text = "Username"
            tb_username.ForeColor = Color.LightGray
        End If
    End Sub

    Private Sub tb_password_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_password.GotFocus
        tb_password.Text = ""
        tb_password.ForeColor = Color.Black
    End Sub

    Private Sub tb_password_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_password.LostFocus
        If tb_password.Text = "" Then
            tb_password.Text = "Password"
            tb_password.ForeColor = Color.LightGray
        End If
    End Sub

    Private Sub Form7_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        tb_password.Text = "password"
        tb_password.ForeColor = Color.LightGray
    End Sub

    Private Sub tb_username_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tb_username.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then tb_password.Select()

    End Sub

    Private Sub tb_password_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tb_password.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            Call conecDB()      'Open the connection to database
            Call initCMD()      'Initialize the sqlclient command object
            Call CekUser()

        End If
    End Sub
End Class