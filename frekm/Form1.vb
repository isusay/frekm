Imports MySql.Data.MySqlClient
'Imports Emgu.CV
'Imports WebCam_Capture
'Imports MessagingToolkit.QRCode.Codec
Imports System.IO
Imports System.IO.Ports
Imports System.Threading
Imports System.Net

'Public Event PinChanged As SerialPinChangedEventHandler

Public Class Form1

    Dim myPort As Array
    Dim readBuffer As String
    Dim readBuffer2 As String
    Dim cals1 As String
    Dim cals2 As String
    Dim cals3 As String
    Dim cals4 As String
    Dim maxcal As String
    Dim vers1 As String
    Dim vers2 As String
    Dim vers3 As String
    Dim vers4 As String
    Dim vers5 As String
    Dim vers6 As String
    Dim iter As String
    Dim pmax As Integer
    Dim sk1 As Double
    Dim sk2 As Double
    Dim sk3 As Double
    Dim sk4 As Double
    Dim sk5 As Double
    Dim sk6 As Double
    Dim scal1 As Double
    Dim scal2 As Double
    Dim scal3 As Double
    Dim scal4 As Double
    Dim a1 As Integer = 0
    Dim a2 As Integer = 0
    Dim a3 As Integer = 0
    Dim a4 As Integer = 0
    Dim a5 As Integer = 0
    Dim a6 As Integer = 0
    Dim a7 As Integer = 0
    Dim b1 As Integer = 0
    Dim b2 As Integer = 0
    Dim b3 As Integer = 0
    Dim b4 As Integer = 0
    Dim b5 As Integer = 0
    Dim b6 As Integer = 0
    Dim b7 As Integer = 0
    Dim serial As Integer = 0
    Dim dbcenter As Integer = 0
    Dim dbdiameter As Integer = 0
    Dim sticker As String
    Dim jignya As String
    Dim tim As Integer = 0
    Dim tim_start As Integer = 0
    Dim status_rasp As Integer = 0
    Dim stasus_ptc As Integer = 0
    Dim no_cell As String


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        WebKitBrowser1.Navigate("http://10.100.0.104/spk/index.php/login/logout")
        tim_start = 1

        'Me.Close()
        'Form7.Show()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.Text = "" Then
            MsgBox("kosong")
        Else

            Call tampildata()
            Call style3()
            Call serial_no()
            If status_rasp = 1 Then
                Call pin_led()
            End If

        End If
        WebKitBrowser1.Select()



    End Sub

    Public Sub pin_led()
        If Microsoft.VisualBasic.Left(Label40.Text, 1) = "B" Then
            Call r8on()
            Call r7off()
            Call r9off()
            Call r10off()

        End If

        If Microsoft.VisualBasic.Left(Label40.Text, 1) = "A" Then
            Call r7on()
            Call r8off()
            Call r9off()
            Call r10off()

        End If

        If Microsoft.VisualBasic.Left(Label40.Text, 1) = "C" Then
            Call r9on()
            Call r8off()
            Call r7off()
            Call r10off()

        End If
        If Microsoft.VisualBasic.Left(Label40.Text, 1) = "D" Then
            Call r10on()
            Call r8off()
            Call r9off()
            Call r7off()

        End If
    End Sub
    Private Sub serial_no()
        Call serial_terakhir()
        Form2.TextBox20.Text = Label28.Text
        'serial = serial + 1
        'Label28.Text = Format(Now, "yyMMdd") & "" & Label27.Text & "" & serial.ToString("000")

    End Sub

    Private Sub cek_varian()

        Dim lokasi As String = My.Application.Info.DirectoryPath
        Dim fileReader As String

        fileReader = My.Computer.FileSystem.ReadAllText(lokasi & "\alamat_server.txt")
        Dim str As String = fileReader
        Dim str2() As String = Split(str, ",")

        Dim host As String = str2(5)
        Dim file As String = str2(6)

        Dim address As String = host & "/" & file
        Dim client As WebClient = New WebClient()
        Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
        TextBox4.Text = reader.ReadToEnd
        Dim str3() As String = Split(TextBox4.Text, ",")
        'TextBox2.Text = str3(1)
        no_cell = str3(0)
        If no_cell = Microsoft.VisualBasic.Right(Label27.Text, 1) Then
            TextBox2.Text = str3(1)
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lbjam.Text = Format(Now, "dd-MM-yyyy") & " " & Format(Now, "hh:mm:ss")
        Call cek_varian()

        If tim_start = 1 Then
            tim = tim + 1
            If tim = 5 Then
                'Me.Close()
                Application.Exit()

                'Form7.Show()
                tim_start = 0
                tim = 0
            End If
        End If
    End Sub

    Private Sub serial_terakhir()
        Try
            Call koneksine()
            Dim str1 As String

            str1 = "SELECT * FROM testvolt2 ORDER BY no DESC LIMIT 1"
            cmd = New MySqlCommand(str1, conn)
            rd = cmd.ExecuteReader
            rd.Read()

            If rd.HasRows Then
                Dim serial_sebelumnya As String = rd.Item("serialno")
                'Label28.Text = Val(serial_sebelumnya) + 1

                Dim hari_lalu As String = Microsoft.VisualBasic.Left(serial_sebelumnya, 8)
                Dim hari_ini As String = Format(Now, "yyMMdd") & "" & Label27.Text & ""


                'Label28.Text = hari_lalu

                If hari_ini = hari_lalu Then
                    Label28.Text = Val(serial_sebelumnya) + 1
                Else
                    Label28.Text = Format(Now, "yyMMdd") & "" & Label27.Text & "" & "001"
                End If

                conn.Close()

                End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub tampildata()
        Try
            Call koneksine()
            Dim str1 As String

            str1 = "SELECT * FROM test_e1 WHERE ordering_code = '" & TextBox2.Text & "'"
            cmd = New MySqlCommand(str1, conn)
            rd = cmd.ExecuteReader
            rd.Read()

            If rd.HasRows Then

                Label3.Text = rd.Item("ordering_code") & rd.Item("style") & ".jpg"
                TextBox1.Text = rd.Item("ordering_code") & rd.Item("style") & ".jpg"
                Label29.Text = rd.Item("style")
                Label1.Text = rd.Item("max_cal")
                Label2.Text = rd.Item("cal1")
                cals1 = rd.Item("cal1")
                cals2 = rd.Item("cal2")
                cals3 = rd.Item("cal3")
                cals4 = rd.Item("cal4")
                maxcal = rd.Item("max_cal")
                vers1 = rd.Item("vera1")
                vers2 = rd.Item("vera2")
                vers3 = rd.Item("vera3")
                vers4 = rd.Item("vera4")
                vers5 = rd.Item("vera5")
                vers6 = rd.Item("vera6")
                iter = rd.Item("iterasi")
                pmax = rd.Item("point_max")
                sk1 = rd.Item("sk1")
                sk2 = rd.Item("sk2")
                sk3 = rd.Item("sk3")
                sk4 = rd.Item("sk4")
                sk5 = rd.Item("sk5")
                sk6 = rd.Item("sk6")
                scal1 = rd.Item("vkal1")
                scal2 = rd.Item("vkal2")
                scal3 = rd.Item("vkal3")
                scal4 = rd.Item("vkal4")
                dbcenter = rd.Item("center")
                dbdiameter = rd.Item("diameter")
                jignya = rd.Item("jig")
                Label40.Text = rd.Item("jig")
                relay.Text = Microsoft.VisualBasic.Left(rd.Item("relay"), 4)
                tborderingcode.Text = rd.Item("ordering_code")
                tbcodegroup.Text = rd.Item("code_group")
                tbprdgroup.Text = rd.Item("product_group")
                tbprdvarian.Text = rd.Item("product_varian")
                tbiec.Text = rd.Item("iec")
                tbfreq.Text = rd.Item("freq")
                tbva.Text = rd.Item("int_cons")
                tbweight.Text = rd.Item("weight")
                tbbrand.Text = rd.Item("brand")
                tbcategori.Text = rd.Item("category")
                tbsize.Text = rd.Item("size")
                tbsignal.Text = rd.Item("signal")
                tbisntrumen.Text = rd.Item("instrument")
                tbtype.Text = rd.Item("type")
                tbclass.Text = rd.Item("class")
                Label26.Text = rd.Item("max_cal")
                lbsticker.Text = rd.Item("sticker_wiring")
                lbsignal.Text = rd.Item("signal")

                If rd.Item("cal1") > 0 Then a1 = 1
                If rd.Item("cal2") > 0 Then a2 = 1
                If rd.Item("cal3") > 0 Then a3 = 1
                If rd.Item("cal4") > 0 Then a4 = 1
                If rd.Item("cal5") > 0 Then a5 = 1
                If rd.Item("cal6") > 0 Then a6 = 1
                a7 = a1 + a2 + a3 + a4 + a5 + a6
                Label18.Text = a7 'point calibrasi

                If rd.Item("vera1") > 0 Then b1 = 1
                If rd.Item("vera2") > 0 Then b2 = 1
                If rd.Item("vera3") > 0 Then b3 = 1
                If rd.Item("vera4") > 0 Then b4 = 1
                If rd.Item("vera5") > 0 Then b5 = 1
                If rd.Item("vera6") > 0 Then b6 = 1
                b7 = b1 + b2 + b3 + b4 + b5 + b6
                Label20.Text = b7 'verifikasi

                Dim sat As String = Microsoft.VisualBasic.Left(tbprdgroup.Text, 4)
                'Label21.Text = sat
                If sat = "Volt" Then
                    Label21.Text = "Voltage"
                    Form2.DGV1.Columns(0).HeaderCell.Value = "Volt"
                    Form2.DGV1.Columns(3).Visible = False
                    'Call acvolton()


                End If
                If sat = "Amme" Then
                    Label21.Text = "Current"
                    Form2.DGV1.Columns(0).HeaderCell.Value = "Arus"
                    Form2.DGV1.Columns(3).Visible = False
                    'Call acammoff()


                End If



                conn.Close()



            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub cek_jig()
        If jignya = "B (72)" Then
            sr_ard.Open()
            sr_ard.WriteLine("6off")
            sr_ard.Close()
            sr_ard.Open()
            sr_ard.WriteLine("5on")
            sr_ard.Close()
        End If
        If jignya = "A (96)" Then
            sr_ard.Open()
            sr_ard.WriteLine("5off")
            sr_ard.Close()
            sr_ard.Open()
            sr_ard.WriteLine("6on")
            sr_ard.Close()
        End If
    End Sub
    Private Sub style3()

        'Dim lokasi As String = My.Application.Info.DirectoryPath
        'Dim folder As String = lokasi & "\picstyle\"
        'Dim folder As String = "C:\Users\solo\Documents\Visual Studio 2017\project\Senzo\WindowsApp3\picstyle\"
        Form2.Show()
        Form2.AGauge1.BackgroundImage = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData("http://" & server & "/senzo/assets/picstyle/" & Label3.Text)))

        'Form2.AGauge1.BackgroundImage = Image.FromFile(folder & Label3.Text & ".jpg")
        Form2.AGauge1.Center = New System.Drawing.Point(dbcenter, dbcenter)
        Form2.AGauge1.NeedleRadius = dbdiameter
        Form2.TextBox21.Text = Form7.Label4.Text
        'Form2.Label59.Text = jignya
        Form2.Label62.Text = jignya

        ' Call cek_jig()




        'If jignya = "B (72)" Then
        '    Form2.jig1.Visible = True
        '    Form2.jig1.ForeColor = Color.LimeGreen
        '    Form2.jig2.Visible = True
        '    Form2.jig2.ForeColor = Color.LightSlateGray
        '    Form2.jig3.Visible = True
        '    Form2.jig3.ForeColor = Color.LightSlateGray
        '    Form2.jig4.Visible = True
        '    Form2.jig4.ForeColor = Color.LightSlateGray
        'End If

        'If jignya = "A (96)" Then
        '    Form2.jig1.Visible = True
        '    Form2.jig1.ForeColor = Color.LightSlateGray
        '    Form2.jig2.Visible = True
        '    Form2.jig2.ForeColor = Color.LimeGreen
        '    Form2.jig3.Visible = True
        '    Form2.jig3.ForeColor = Color.LightSlateGray
        '    Form2.jig4.Visible = True
        '    Form2.jig4.ForeColor = Color.LightSlateGray
        'End If

        'If jignya = "C (Dirrect)" Then
        '    Form2.jig1.Visible = True
        '    Form2.jig1.ForeColor = Color.LightSlateGray
        '    Form2.jig2.Visible = True
        '    Form2.jig2.ForeColor = Color.LightSlateGray
        '    Form2.jig3.Visible = True
        '    Form2.jig3.ForeColor = Color.LimeGreen
        '    Form2.jig4.Visible = True
        '    Form2.jig4.ForeColor = Color.LightSlateGray
        'End If

        'If jignya = "D (DV)" Then
        '    Form2.jig1.Visible = True
        '    Form2.jig1.ForeColor = Color.LightSlateGray
        '    Form2.jig2.Visible = True
        '    Form2.jig2.ForeColor = Color.LightSlateGray
        '    Form2.jig3.Visible = True
        '    Form2.jig3.ForeColor = Color.LimeGreen
        '    Form2.jig4.Visible = True
        '    Form2.jig4.ForeColor = Color.LightSlateGray
        'End If


    End Sub

    Private Sub style2()
        Dim folder As String = "C:\Users\solo\Documents\Visual Studio 2017\project\Senzo\WindowsApp3\picstyle\"

        Form4.AGauge1.BackgroundImage = Image.FromFile(folder & Label3.Text & ".jpg")
        Form4.AGauge2.BackgroundImage = Image.FromFile(folder & Label3.Text & ".jpg")

        Form4.DGV1.Rows.Add(12)
        Form4.DGV2.Rows.Add(b7 * 2)
        Form4.DGSK1.Rows.Add(a7)
        Form4.DGSK2.Rows.Add(a7)

        'DataGridView1.Rows(0).Cells(0).Style.ForeColor = Color.Lime
        For x3 As Integer = 0 To a7 + 1
            Form4.DGV1.Rows(x3).Cells(0).Style.ForeColor = Color.Black
        Next

        'Form4.AGauge2.MinValue = vers6
        'Form4.AGauge2.MaxValue = vers1
        Form4.AGauge2.NeedleRadius = 290
        Form4.AGauge2.BaseArcColor = Color.Transparent
        Form4.AGauge2.ScaleLinesMajorColor = Color.Transparent
        Form4.AGauge2.ScaleLinesMinorColor = Color.Transparent
        Form4.AGauge2.ScaleNumbersColor = Color.Transparent
        Form4.AGauge2.ScaleLinesInterColor = Color.Transparent

        Form4.AGauge1.MinValue = vers6
        Form4.AGauge1.MaxValue = vers1
        Form4.AGauge1.NeedleRadius = 290
        Form4.AGauge1.BaseArcColor = Color.Transparent
        Form4.AGauge1.ScaleLinesMajorColor = Color.Transparent
        Form4.AGauge1.ScaleLinesMinorColor = Color.Transparent
        Form4.AGauge1.ScaleNumbersColor = Color.Transparent
        Form4.AGauge1.ScaleLinesInterColor = Color.Transparent

    End Sub

    Private Sub style()

        Dim folder As String = "C:\Users\solo\Documents\Visual Studio 2017\project\Senzo\WindowsApp3\picstyle\"
        Form2.Show()
        Form2.AGauge1.BackgroundImage = Image.FromFile(folder & Label3.Text & ".jpg")
        Form2.calss1.Text = cals1
        Form2.calss2.Text = cals2
        Form2.calss3.Text = cals3
        Form2.calss4.Text = cals4
        Form2.lblmaxcal.Text = maxcal

        Form2.vers1.Text = vers1
        Form2.vers2.Text = vers2
        Form2.vers3.Text = vers3
        Form2.vers4.Text = vers4
        Form2.vers5.Text = vers5
        Form2.vers6.Text = vers6
        Form2.iters.Text = iter
        Form2.pmax.Text = pmax

        Form2.skv1.Text = sk1
        Form2.skv2.Text = sk2
        Form2.skv3.Text = sk3
        Form2.skv4.Text = sk4
        Form2.skv5.Text = sk5
        Form2.skv6.Text = sk6

        Form2.scals1.Text = scal1
        Form2.scals2.Text = scal2
        Form2.scals3.Text = scal3
        Form2.scals4.Text = scal4

        Form2.AGauge1.MaxValue = Val(Form2.pmax.Text)
        Form2.AGauge1.MinValue = 0
        Form2.AGauge1.Value = 0
        Form2.Label5.Text = Form2.AGauge1.MaxValue

        Form2.DGV1.Rows(0).Cells(0).Value = vers1
        Form2.DGV1.Rows(1).Cells(0).Value = vers2
        Form2.DGV1.Rows(2).Cells(0).Value = vers3
        Form2.DGV1.Rows(3).Cells(0).Value = vers4
        Form2.DGV1.Rows(4).Cells(0).Value = vers5
        Form2.DGV1.Rows(5).Cells(0).Value = vers6
        Form2.DGV1.Rows(6).Cells(0).Value = vers1
        Form2.DGV1.Rows(7).Cells(0).Value = vers2
        Form2.DGV1.Rows(8).Cells(0).Value = vers3
        Form2.DGV1.Rows(9).Cells(0).Value = vers4
        Form2.DGV1.Rows(10).Cells(0).Value = vers5
        Form2.DGV1.Rows(11).Cells(0).Value = vers6

    End Sub

    Private Sub alamat_spk()
        Dim lokasi As String = My.Application.Info.DirectoryPath
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(lokasi & "\alamat_server.txt")
        Dim str As String = fileReader
        Dim str2() As String = Split(str, ",")

        WebKitBrowser1.Navigate(str2(4))
    End Sub

    Private Sub nomor_cell()
        Dim lokasi As String = My.Application.Info.DirectoryPath
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(lokasi & "\setport.txt")
        Dim str As String = fileReader
        Dim str2() As String = Split(str, ",")
        Label27.Text = str2(6)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.ControlBox = False
        GroupBox5.Location = New Point((Me.Width \ 2) - (GroupBox5.Width \ 2), (Me.Height \ 2) - (GroupBox5.Height \ 2))

        Call alamat_spk()
        Call serial_no()
        Call nomor_cell()

        WebKitBrowser1.Select()
        If Form7.Label5.Text = "3" Then
            'Me.Hide()
            'Me.Size = New Size(300, 180)

            Me.CenterToScreen()
            TabControl1.Size = New Size(298, 196)
            'Button2.Select()
            WebKitBrowser1.Select()
            TabControl1.TabPages(1).Enabled = False
            TabControl1.TabPages(2).Enabled = False
            Call hide_form()
        End If

        If Form7.Label5.Text = "1" Then
            'Me.Hide()
            Label27.Text = "1"
            Call tampilproductgroup()
            Call tampilproductvarian()
            Call tampiltype()
            Call tampilinstrument()
            Call tampilclass()

        End If

        Call baca_setting2()


    End Sub
    Sub hide_form()
        Label38.Visible = True
        Label33.Visible = False
        Label34.Visible = False
        Label35.Visible = False
        Label36.Visible = False
        Label37.Visible = False
        Label38.Visible = True
        Label39.Visible = True
        ComboBox1.Visible = False
        ComboBox2.Visible = False
        ComboBox3.Visible = False
        ComboBox4.Visible = False
        ComboBox5.Visible = False
        Button6.Visible = False
        Button7.Visible = False
        Button8.Visible = False
        Button9.Visible = False
        Button10.Visible = False
        Button11.Visible = False

    End Sub
    Sub tampilclass()
        Call koneksine()
        Dim str As String
        str = "select class from test_e1 group by class"
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader
        If rd.HasRows Then
            Do While rd.Read
                ComboBox5.Items.Add(rd("class"))
            Loop
        Else
        End If
        conn.Close()
    End Sub
    Sub tampilinstrument()
        Call koneksine()
        Dim str As String
        str = "select instrument from test_e1 group by instrument"
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader
        If rd.HasRows Then
            Do While rd.Read
                ComboBox4.Items.Add(rd("instrument"))
            Loop
        Else
        End If
        conn.Close()
    End Sub
    Sub tampilproductvarian()
        Call koneksine()
        Dim str As String
        str = "select product_varian from test_e1 group by product_varian"
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader
        If rd.HasRows Then
            Do While rd.Read
                ComboBox2.Items.Add(rd("product_varian"))
            Loop
        Else
        End If
        conn.Close()
    End Sub
    Sub tampiltype()
        Call koneksine()
        Dim str As String
        str = "select type from test_e1 group by type"
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader
        If rd.HasRows Then
            Do While rd.Read
                ComboBox3.Items.Add(rd("type"))
            Loop
        Else
        End If
        conn.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call cek_varian()


        'Dim address As String = "http://isusay.my.id/song/id/oke.txt"
        'Dim client As WebClient = New WebClient()
        'Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
        'TextBox4.Text = reader.ReadToEnd
        'Dim str3() As String = Split(TextBox4.Text, ",")
        'TextBox2.Text = str3(1)



    End Sub

    Private Sub konek_modul()
        If Label23.Text = "Connected" And Label25.Text = "Connected" Then
            Dim modul As String = "All modul connected"

        ElseIf Label23.Text = "Connected" And Label25.Text = "Disconnect" Then
            MsgBox("PTC Tidak Connect")
        ElseIf Label23.Text = "Disconnect" And Label25.Text = "Connected" Then
            MsgBox("Modul Pin Tidak Connect")
        ElseIf Label23.Text = "Disconnect" And Label25.Text = "Disconnect" Then
            MsgBox("Modul Pin dan PTC Tidak Connect")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Form3.Show()
        'Simpan
        Call koneksine()
        Try
            Dim str As String
            'str = "UPDATE tb_calibrasi SET sdcal1 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"

        str = "UPDATE test_E1 SET pic_style = '" & Label3.Text & "' WHERE ordering_code = '" & TextBox2.Text & "'"

            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Insert Data Berhasil Dilakukan")

        Catch ex As Exception
            MessageBox.Show("Insert data gagal dilakukan.")
        End Try

    End Sub

    Private Sub bt_connect_Click(sender As Object, e As EventArgs) Handles bt_connect.Click
        Form3.Show()

        'sr_ard.PortName = ComboBox1.Text
        'sr_ard.BaudRate = ComboBox2.Text
        'sr_ard.Open()
        'bt_connect.Enabled = False

    End Sub

    Private Sub pin()
        If Label21.Text = "Voltage" Then
            sr_ard.Open()
            sr_ard.Write("10on")
            sr_ard.Close()
            sr_ard.Open()
            sr_ard.Write("9off")
            sr_ard.Close()

        End If
        If Label21.Text = "Current" Then
            sr_ard.Open()
            sr_ard.Write("10off")
            sr_ard.Close()
            sr_ard.Open()
            sr_ard.Write("9on")
            sr_ard.Close()
        End If
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Label32.Text = Val(tbclass.Text) / 100
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub sr_ard_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles sr_ard.DataReceived
        Try
            readBuffer = sr_ard.ReadLine()
            'data to UI thread 
            Me.Invoke(New EventHandler(AddressOf DoUpdate))
            'bootstat.SelectColor2 = True
            sr_ard.Close()
        Catch ex As Exception
            MsgBox("read " & ex.Message)
        End Try
    End Sub

    Public Sub DoUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
        TextBox3.Text = readBuffer
        'dtarray = Split(tb1.Text)
        'sr_ard.Close()



    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Public Sub baca_setting2()
        Dim lokasi As String = My.Application.Info.DirectoryPath
        Dim fileReader12 As String

        fileReader12 = My.Computer.FileSystem.ReadAllText(lokasi & "\setport.txt")
        Dim str As String = fileReader12
        Dim str2() As String = Split(str, ",")

        Label43.Text = str2(7)
        Label44.Text = str2(8)

        sr_ptc.PortName = str2(0)
        sr_ptc.BaudRate = str2(1)
        sr_ard.PortName = str2(2)
        sr_ard.BaudRate = str2(3)
        stasus_ptc = str2(4)
        status_rasp = str2(5)

        If str2(4) = 0 Then
            CheckBox1.Checked = False
        End If
        If str2(4) = 1 Then
            CheckBox1.Checked = True
        End If

        If str2(5) = 0 Then
            CheckBox2.Checked = False
        End If
        If str2(5) = 1 Then
            CheckBox2.Checked = True
        End If
        'cmbPort1.Enabled = False
        'cmbBaudrate1.Enabled = False
        'sr_ptc.DtrEnable = True
        'sr_ptc.RtsEnable = True
        'sr_ard.DtrEnable = True
        'sr_ard.RtsEnable = True
        'sr_ard.Open()

        Label31.Text = "ptc " & str2(0)
        Label30.Text = "ard " & str2(2)

    End Sub

    Private Sub sr_ptc_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles sr_ptc.DataReceived
        'Try
        '    readBuffer2 = sr_ptc.ReadLine()
        '    'data to UI thread 
        '    'Me.Invoke(New EventHandler(AddressOf DoUpdate))
        '    'bootstat.SelectColor2 = True
        '    Label25.Text = "Connected"

        'Catch ex As Exception
        '    MsgBox("read " & ex.Message)
        'End Try
    End Sub



    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click

        Call r1on()

    End Sub
    Public Sub r1on()
        sr_ard.Open()
        sr_ard.WriteLine("1on")
        sr_ard.Close()
    End Sub
    Public Sub r1off()
        sr_ard.Open()
        sr_ard.WriteLine("1off")
        sr_ard.Close()
    End Sub
    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        Call r1off()

    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        'Form6.Show()
        sr_ard.Open()
        sr_ard.WriteLine("a")
        sr_ard.Close()

    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        Call r2on()

    End Sub
    Public Sub r2on()
        sr_ard.Open()
        sr_ard.WriteLine("2on")
        sr_ard.Close()
    End Sub
    Public Sub r2off()
        sr_ard.Open()
        sr_ard.WriteLine("2off")
        sr_ard.Close()
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Call r2off()

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Call r3on()

    End Sub
    Public Sub r3on()
        sr_ard.Open()
        sr_ard.WriteLine("3on")
        sr_ard.Close()
    End Sub
    Public Sub r3off()
        sr_ard.Open()
        sr_ard.WriteLine("3off")
        sr_ard.Close()
    End Sub
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Call r3off()

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Call r4on()

    End Sub
    Public Sub r4on()
        sr_ard.Open()
        sr_ard.WriteLine("4on")
        sr_ard.Close()
    End Sub
    Public Sub r4off()
        sr_ard.Open()
        sr_ard.WriteLine("4off")
        sr_ard.Close()
    End Sub
    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Call r4off()

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Call r5on()

    End Sub
    Public Sub r5on()
        sr_ard.Open()
        sr_ard.WriteLine("5on")
        sr_ard.Close()
    End Sub
    Public Sub r5off()
        sr_ard.Open()
        sr_ard.WriteLine("5off")
        sr_ard.Close()
    End Sub
    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Call r5off()

    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Call r6on()

    End Sub

    Public Sub r6on()
        sr_ard.Open()
        sr_ard.WriteLine("6on")
        sr_ard.Close()
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Call r6off()

    End Sub

    Public Sub r6off()
        sr_ard.Open()
        sr_ard.WriteLine("6off")
        sr_ard.Close()
    End Sub

    Public Sub r7on()
        sr_ard.Open()
        sr_ard.WriteLine("7on")
        sr_ard.Close()
    End Sub
    Public Sub r7off()
        sr_ard.Open()
        sr_ard.WriteLine("7off")
        sr_ard.Close()
    End Sub
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Call r7on()

    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        Call r7off()

    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Call r8on()

    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        Call r8off()

    End Sub
    Public Sub r8on()
        sr_ard.Open()
        sr_ard.WriteLine("8on")
        sr_ard.Close()
    End Sub
    Public Sub r8off()
        sr_ard.Open()
        sr_ard.WriteLine("8off")
        sr_ard.Close()
    End Sub
    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Call r9on()

    End Sub
    Public Sub r9on()
        sr_ard.Open()
        sr_ard.WriteLine("9on")
        sr_ard.Close()
    End Sub
    Public Sub r9off()
        sr_ard.Open()
        sr_ard.WriteLine("9off")
        sr_ard.Close()
    End Sub
    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        Call r9off()

    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        Call r10on()

    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Call r10off
    End Sub
    Public Sub r10on()
        sr_ard.Open()
        sr_ard.WriteLine("aon")
        sr_ard.Close()
    End Sub
    Public Sub r10off()
        sr_ard.Open()
        sr_ard.WriteLine("aoff")
        sr_ard.Close()
    End Sub
    Private Sub r11on()
        sr_ard.Open()
        sr_ard.WriteLine("bon")
        sr_ard.Close()
    End Sub
    Private Sub r11off()
        sr_ard.Open()
        sr_ard.WriteLine("boff")
        sr_ard.Close()
    End Sub

    Sub tampilproductgroup()
        Call koneksine()
        Dim str As String
        str = "select product_group from test_e1 group by product_group"
        cmd = New MySqlCommand(str, conn)
        rd = cmd.ExecuteReader
        If rd.HasRows Then
            Do While rd.Read
                ComboBox1.Items.Add(rd("product_group"))
            Loop
        Else
        End If
        conn.Close()
    End Sub

    Private Sub ComboBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles ComboBox1.MouseClick
        'Call tampilproductgroup()

    End Sub

    Private Sub ComboBox2_MouseClick(sender As Object, e As MouseEventArgs) Handles ComboBox2.MouseClick
        'Call tampilproductvarian()

    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        'Label41.Text = WebKitBrowser1.Url.ToString()



    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Dim xx As Integer = 0
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        xx = xx + 1
        If xx = 1 Then
            WebKitBrowser2.Navigate("http://10.100.0.104/senzo/trigger.php?id=1&varian= ")
        End If
        If xx = 5 Then
            Timer2.Enabled = False
            xx = 0
        End If
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        WebKitBrowser1.Navigate("http://10.100.0.104/spk/index.php/logins")
    End Sub
End Class
