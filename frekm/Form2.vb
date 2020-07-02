Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.IO.Ports
Imports System.Threading

Public Class Form2
    Dim x As Double = 0
    Dim xz As Double = 0
    Dim fun As Integer = 0
    Dim stat As Integer = 0
    Dim kal As Integer = 0
    Dim totiter As Integer = 0
    Dim error1 As Integer
    Dim error2 As Integer
    Dim cal As Boolean = False
    Dim resver1 As Integer = 0
    Dim resver2 As Integer = 0
    Dim resver3 As Integer = 0
    Dim resver4 As Integer = 0
    Dim resver5 As Integer = 0
    Dim resver6 As Integer = 0
    Dim fungsi As Integer = 0
    Dim iterasi As Integer
    Dim toleransi1 As Integer
    Dim toleransi2 As Integer
    Dim ver1 As Integer = 0
    Dim max As Integer
    Dim min As Integer
    Dim jmlrow As Integer = 0
    Dim i As Integer = 0
    Dim iterasine As Integer = 0
    Dim testpoint As Integer = 0
    Dim selisih As Integer = 0
    Dim realpoint As Double = 0
    Dim mode As Integer = 0
    Dim ct_hard As Integer = 0
    Dim ct_soft As Integer = 0
    Dim ct As Double = 0.00
    Dim ct_up As Integer = 1
    Dim ct_down As Integer = 0
    Dim querysimpan As String
    Dim komane As Integer = 0
    Dim komane1 As Double = 0.00
    Dim komanea As Double = 0.0000
    Dim ltolptc As Double = 1

    Dim span As Double = 0
    Dim lsl1 As Double = 0
    Dim usl1 As Double = 0
    Dim kelase As Double = 0

    Dim stx As Integer = 2
    Dim fl As Integer = 29
    Dim fh As Integer = 0
    Dim cmdp As Integer = 98
    Dim dot1 As Integer = 46
    Dim dot2 As Integer = 46
    Dim dot3 As Integer = 46
    Dim dot4 As Integer = 46
    Dim dv1, dv2, dv3, dv4, dv5 As Integer
    Dim da1, da2, da3, da4, da5 As Integer
    Dim df1, df2, df3, df4 As Integer
    Dim dt1, dt2, dt3, dt4 As Integer
    Dim sys As Integer
    Dim cs As Integer
    Dim etx As Integer = 3
    Dim totdata As Integer = 1279
    Dim jigke As Integer = 0

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.Location = New Point(Screen.PrimaryScreen.WorkingArea.Width - 284, Screen.PrimaryScreen.WorkingArea.Height - 261)
        'Me.Location = New Point(0, 0)
        Form1.Hide()
        WebKitBrowser2.Navigate("http://10.100.0.104/spk/index.php/logins")
        Dim scr As Screen = Screen.FromPoint(Cursor.Position)

        Me.Location = New Point(scr.WorkingArea.Right - Me.Width, scr.WorkingArea.Bottom - Me.Height)

        'DGV1.Rows.Add(12)
        If Form1.Label27.Text = "1" Then
            Button4.Visible = True
            Label31.Visible = True
        End If

        Label32.Text = Form1.Label20.Text + 1
        Label31.Text = ""
        jmlrow = Label32.Text


        tbvolt.Text = "000.00"
        tbcurrent.Text = "0.0000"
        tbteta.Text = "000.0"

        'DGV1.Rows(2).Visible = False
        'Call setting_fjarum()
        ComboBox2.Items.Add("Normal")
        ComboBox2.Items.Add("Ketat")
        ComboBox2.Text = ComboBox2.Items(0)
        ComboBox2.Enabled = False

        Button8.Enabled = False
        Button4.Enabled = False
        If Form7.Label5.Text = "3" Then
            'Me.Hide()
            Form1.Hide()
            GroupBox5.Text = ""
            'GroupBox3.Visible = False
            tbstcal.Visible = False
            tbstver.Visible = False
            stkalibrasi.Text = "JIG : " & Form1.tbsize.Text
            stverifikasi.Text = Form1.tbiec.Text
            ComboBox2.Visible = False
            Button7.Visible = True
            Button7.Text = "Start"
            GroupBox3.Text = ""
            GroupBox9.Visible = True
            GroupBox9.Text = "Jig Status"
            Label60.Visible = True
            Label60.Text = "DB"
            Label58.Visible = True
            Label58.Text = "96"
            Label61.Visible = True
            Label61.Text = "72"
            Label63.Visible = True
            Label63.Text = "DR"
            'Label60.Text = "Product NULL"
            GroupBox11.Visible = False
            Button8.Visible = False
            Button6.Visible = False
            Me.DGV1.Columns(1).Visible = False
            Me.DGV1.Columns(2).Visible = False
            DGV1.AutoSizeColumnsMode = DGV1.AutoSizeColumnsMode.DisplayedCells
            'Form1.sr_ard.Open()
            'Form1.sr_ard.WriteLine("jigalloff")
            'Form1.sr_ard.Close()




        End If


    End Sub



    Sub Hitung_DATAGRIDVIEW()
        Dim total As Double
        total = 0
        For t As Integer = 0 To DGV1.Rows.Count - 1
            total = total + Val(DGV1.Rows(t).Cells(9).Value)

        Next
        Label37.Text = total
    End Sub

    Private Sub saveData()

        Call koneksine()
        Try
            For i As Integer = 0 To DGV1.Rows.Count - 1 Step +1

                Dim str As String
                Dim serialno1 As String = Form1.Label28.Text
                Dim kode1 As String = Form1.TextBox2.Text
                Dim test1 As Integer = DGV1.Rows(i).Cells(0).Value.ToString
                Dim volt1 As Integer = tbvolt.Text
                Dim measure1 As Double = DGV1.Rows(i).Cells(8).Value.ToString
                Dim error1 As Double = DGV1.Rows(i).Cells(10).Value.ToString
                Dim status1 As String = DGV1.Rows(i).Cells(2).Value.ToString
                Dim jam1 As String = Format(Now, "hh:mm:ss")
                Dim operator1 As String = TextBox21.Text
                Dim mesine As String = Form1.Label27.Text
                str = "INSERT INTO testvolt2(serialno, kode, test, volt, measure, error, status, jam, operator, no_mesin) VALUES ('" & serialno1 & "','" & kode1 & "', '" & test1 & "', '" & volt1 & "', '" & measure1 & "', '" & error1 & "','" & status1 & "','" & jam1 & "','" & operator1 & "','" & mesine & "')"
                cmd = New MySqlCommand(str, conn)
                cmd.ExecuteNonQuery()

            Next
            'MessageBox.Show("Insert Data Berhasil Dilakukan")
        Catch ex As Exception
            'MessageBox.Show("Insert data gagal dilakukan.")

        End Try
    End Sub


    Private Sub AGauge1_MouseClick(sender As Object, e As MouseEventArgs) Handles AGauge1.MouseClick
        Select Case e.Button
            Case MouseButtons.Left
                fun = fun + 1
                Label1.Text = fun
                If fun = 1 And fungsi = 0 And Label31.Text = "" Then
                    'fungsi = 1
                    'Label31.Text = "Kalibrasi"
                    'DGV1.Rows.Add(jmlrow)
                    'Call show_kalibrasi()

                    'If Form1.tbisntrumen.Text = "Ammeter" Then
                    'Call arus_prd_varian()
                    'Call isi_data()
                    'End If


                End If

                'Verifikasi

                '    If fun = 1 And fungsi = 2 Then
                '    DGV1.Rows.Clear()
                '    Label31.Text = "Verifikasi"
                '    DGV1.Rows.Add(jmlrow * 2)
                '    Button4.Text = "Next"
                '    Button4.Enabled = False
                '    Button5.Enabled = False
                '    Call show_verifikasi()
                '    Call isi_data()

                'End If

                'If fungsi = 2 Then

                '    i = i + 1
                '    If fun = i Then

                '        If i = (jmlrow * 2) + 1 Then
                '            i = 0
                '            fun = 0
                '            iterasine = iterasine + 2
                '            Label34.Text = iterasine & " " & iterasi
                '            If iterasine = iterasi Then
                '                MsgBox("selesai.. Sudah 2 kali iterasi.. ")
                '                'Hasil Verifikasi
                '                Call Hitung_DATAGRIDVIEW()
                '                If Label37.Text < 12 Then
                '                    tbstver.ForeColor = Color.Red
                '                    tbstver.Text = "Fail"

                '                End If
                '                If Label37.Text = 12 Then
                '                    tbstver.ForeColor = Color.Lime
                '                    tbstver.Text = "Pass"
                '                    Call saveData()
                '                    Button8.Enabled = True
                '                    Label31.Text = "Printing"
                '                    Form3.Show()
                '                End If
                '                fungsi = 3
                '                fun = 0
                '            End If
                '        Else
                '            DGV1.ClearSelection()
                '            DGV1.Rows(i - 1).Selected = True
                '            Label53.Text = i & "  --  " & fun
                '            DGV1.Rows(i - 1).Cells(0).Style.ForeColor = Color.Lime
                '            AGauge1.Value = Me.DGV1.SelectedCells(1).Value.ToString
                '            testpoint = Val(Me.DGV1.SelectedCells(0).Value.ToString) / 1.733333
                '            realpoint = Val(Me.DGV1.SelectedCells(0).Value.ToString) / Val(Label57.Text)
                '            If Form1.tbisntrumen.Text = "Voltmeter" Then tbvolt.Text = testpoint.ToString("000.00")
                '            If Form1.tbisntrumen.Text = "Ammeter" Then tbcurrent.Text = realpoint.ToString("0.0000")

                '            'If Me.DGV1.SelectedCells(2).Value Is Nothing Then

                '            '    Label53.Text = i & "  --  " & fun
                '            'Else


                '            '    Label53.Text = i & "  --  " & fun
                '            'End If

                '        End If

                '    End If

                'End If

                If fungsi = 3 And tbstver.Text = "Pass" Then
                    Form3.Show()
                End If

                If Label31.Text = "Setting" Then
                    DGV1.Rows.Add(jmlrow)
                    Call show_data_setting()
                    Button4.Enabled = True
                End If

            Case MouseButtons.Right

                Label2.Text = "right"
                If Label31.Text = "" Then
                    Label31.Text = "Setting"
                    Button4.Text = "Save Setting"
                    fungsi = 0
                End If

                If Label31.Text = "Verifikasi" Then
                    komane = komane + 1
                    If komane = 1 Then
                        komane1 = 0.01
                        komanea = 0.0001
                        Label56.Text = "Soft"
                    End If
                    If komane = 2 Then
                        komane1 = 0.1
                        komanea = 0.001
                        Label56.Text = "Hard"
                    End If
                    If komane = 3 Then
                        komane = 0
                        komanea = 0
                        Label56.Text = "Off"
                    End If
                End If

                'stat = stat + 1
                'If stat = 1 And Label3.Text = "Verifikasi" Then
                '    xz = 1
                '    Label1.Text = "hard"
                'End If

                'If stat = 2 And Label3.Text = "Verifikasi" Then
                '    xz = 0.1
                '    Label1.Text = "soft"
                '    stat = 0
                'End If

            Case MouseButtons.Middle
                Label2.Text = "midle"
                mode = mode + 1
                Label35.Text = selisih & "  " & mode & "  " & ct & "  " & ct_up
                If mode = 1 Then
                    ct = 0.1

                    ct_up = 100
                End If
                If mode = 2 Then
                    ct = 0.01
                    ct_up = 1
                End If
                If mode = 3 Then mode = 0

                'fun = 0
            Case Else
                Label2.Text = "liane"
        End Select
    End Sub

    Private Sub show_data_setting()
        Try
            Call koneksine()
            Dim str1 As String

            str1 = "SELECT * FROM test_e1 WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            cmd = New MySqlCommand(str1, conn)
            rd = cmd.ExecuteReader
            rd.Read()

            If rd.HasRows Then
                iterasi = rd.Item("iterasi")
                toleransi1 = rd.Item("btol1")
                toleransi2 = rd.Item("btol2")
                max = rd.Item("ver0")
                min = rd.Item("ver5")

                For x5 As Integer = 0 To Val(Form1.Label20.Text)
                    DGV1.Rows(x5).Cells(0).Value = rd.Item("ver" & x5 & "")
                    DGV1.Rows(x5).Cells(3).Value = rd.Item("sdver" & x5 & "")
                Next

                conn.Close()

            End If
            AGauge1.MaxValue = 1000

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub show_verifikasi()
        Try
            Call koneksine()
            Dim str1 As String

            str1 = "SELECT * FROM test_e1 WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            cmd = New MySqlCommand(str1, conn)
            rd = cmd.ExecuteReader
            rd.Read()

            If rd.HasRows Then
                iterasi = rd.Item("iterasi")
                toleransi1 = rd.Item("btol1")
                toleransi2 = rd.Item("btol2")
                max = rd.Item("ver0")
                min = rd.Item("ver4")
                span = max - min
                kelase = Val(Form1.tbclass.Text) / 100

                For x5 As Integer = 0 To (Val(Form1.Label20.Text) * 2) + 1
                    If x5 < 6 Then
                        DGV1.Rows(x5).Cells(0).Value = rd.Item("ver" & x5 & "")
                        DGV1.Rows(x5).Cells(1).Value = rd.Item("sdver" & x5 & "")
                        If ComboBox2.Text = "Normal" Then
                            lsl1 = Val(rd.Item("ver" & x5 & "")) - (kelase * span)
                            usl1 = Val(rd.Item("ver" & x5 & "")) + (kelase * span)
                            DGV1.Rows(x5).Cells(6).Value = lsl1
                            DGV1.Rows(x5).Cells(7).Value = usl1
                        End If
                        If ComboBox2.Text = "Ketat" Then
                            lsl1 = Val(rd.Item("ver" & x5 & "")) - (Val(rd.Item("ver" & x5 & "")) * kelase)
                            usl1 = Val(rd.Item("ver" & x5 & "")) + (Val(rd.Item("ver" & x5 & "")) * kelase)
                            DGV1.Rows(x5).Cells(6).Value = lsl1
                            DGV1.Rows(x5).Cells(7).Value = usl1
                        End If

                    End If

                        If x5 >= 6 Then
                        DGV1.Rows(x5).Cells(0).Value = rd.Item("ver" & x5 - 6 & "")
                        DGV1.Rows(x5).Cells(1).Value = rd.Item("sdver" & x5 - 6 & "")
                        If ComboBox2.Text = "Normal" Then
                            lsl1 = Val(rd.Item("ver" & x5 - 6 & "")) - (kelase * span)
                            usl1 = Val(rd.Item("ver" & x5 - 6 & "")) + (kelase * span)
                            DGV1.Rows(x5).Cells(6).Value = lsl1
                            DGV1.Rows(x5).Cells(7).Value = usl1
                        End If
                        If ComboBox2.Text = "Ketat" Then
                            lsl1 = Val(rd.Item("ver" & x5 - 6 & "")) - (Val(rd.Item("ver" & x5 - 6 & "")) * kelase)
                            usl1 = Val(rd.Item("ver" & x5 - 6 & "")) + (Val(rd.Item("ver" & x5 - 6 & "")) * kelase)
                            DGV1.Rows(x5).Cells(6).Value = lsl1
                            DGV1.Rows(x5).Cells(7).Value = usl1
                        End If
                    End If

                Next

                conn.Close()

            End If
            AGauge1.MaxValue = 1000

            'fun = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub show_kalibrasi()

        Try
            Call koneksine()
            Dim str1 As String

            str1 = "SELECT * FROM test_e1 WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            cmd = New MySqlCommand(str1, conn)
            rd = cmd.ExecuteReader
            rd.Read()

            If rd.HasRows Then
                iterasi = rd.Item("iterasi")
                toleransi1 = rd.Item("btol1")
                toleransi2 = rd.Item("btol2")
                max = rd.Item("ver0")
                min = rd.Item("ver5")

                For x5 As Integer = 0 To Val(Form1.Label20.Text)
                    DGV1.Rows(x5).Cells(0).Value = rd.Item("ver" & x5 & "")
                    DGV1.Rows(x5).Cells(3).Value = rd.Item("sdver" & x5 & "")
                Next

                conn.Close()

            End If
            AGauge1.MaxValue = 1000

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGV1_MouseWheel(sender As Object, e As MouseEventArgs) Handles DGV1.MouseWheel
        If e.Delta > 0 And Label31.Text = "Setting" Then
            DGV1.Rows(Val(Label47.Text)).Cells(3).Value = DGV1.Rows(Val(Label47.Text)).Cells(3).Value + ct_up
            AGauge1.Value = DGV1.Rows(Val(Label47.Text)).Cells(3).Value

        End If

        If e.Delta < 0 And Label31.Text = "Setting" Then
            DGV1.Rows(Val(Label47.Text)).Cells(3).Value = DGV1.Rows(Val(Label47.Text)).Cells(3).Value - ct_up
            AGauge1.Value = DGV1.Rows(Val(Label47.Text)).Cells(3).Value

        End If

        If e.Delta > 0 And Label31.Text = "Verifikasi" And fungsi = 2 Then
            If Form1.tbisntrumen.Text = "Voltmeter" Or Form1.tbisntrumen.Text = "Double Voltmeter" Then
                'AGauge1.Value = DGV1.Rows(e.RowIndex).Cells(3).Value
                AGauge1.Value = DGV1.Rows(cell_index).Cells(1).Value
                tbvolt.Text = Format(tbvolt.Text + komane1, "000.00")
                'tbvolt.Text = va.ToString("000.00")
                DGV1.Rows(cell_index).Cells(4).Value = tbvolt.Text
                DGV1.Rows(cell_index).Cells(5).Value = Val(DGV1.Rows(cell_index).Cells(1).Value) - Val(DGV1.Rows(cell_index).Cells(4).Value)

                DGV1.Rows(cell_index).Cells(8).Value = Format(Val(tbvolt.Text) * 1.733333, "000.00")
                Dim err As Double = Math.Abs(DGV1.Rows(cell_index).Cells(0).Value - DGV1.Rows(cell_index).Cells(8).Value)
                DGV1.Rows(cell_index).Cells(10).Value = Format(err, "000.00")
                'Call cek_error()
                Call cek_error2()
                Call isi_data()
                If Val(Label37.Text) = 6 * 2 Then
                    'Button4.Enabled = True
                    Call finish()
                End If

            End If

            If Form1.tbisntrumen.Text = "Ammeter" Then
                DGV1.Rows(cell_index).Cells(4).Value = Format(Val(tbcurrent.Text), "0.0000")
                tbcurrent.Text = Format(Val(tbcurrent.Text) + komanea, "0.0000")
                If Form1.tbtype.Text = "Direct Connection" Then
                    DGV1.Rows(cell_index).Cells(8).Value = Format(Val(tbcurrent.Text) * ((Val(Label57.Text)) + 1), "0.0000")
                Else
                    DGV1.Rows(cell_index).Cells(8).Value = Format(Val(tbcurrent.Text) * (Val(Label57.Text)), "0.0000")
                End If
                'DGV1.Rows(cell_index).Cells(8).Value = Format(Val(tbcurrent.Text) * (Val(Label57.Text)), "0.0000")
                Dim err As Double = Math.Abs(DGV1.Rows(cell_index).Cells(0).Value - DGV1.Rows(cell_index).Cells(8).Value)
                DGV1.Rows(cell_index).Cells(10).Value = Format(err, "0.0000")

                Call cek_error2()
                Call isi_data()

                If Val(Label37.Text) = 6 * 2 Then
                    'Button4.Enabled = True
                    Call finish()
                End If

            End If
        End If

        If e.Delta < 0 And Label31.Text = "Verifikasi" And fungsi = 2 Then
            If Form1.tbisntrumen.Text = "Voltmeter" Or Form1.tbisntrumen.Text = "Double Voltmeter" Then
                AGauge1.Value = Val(DGV1.Rows(cell_index).Cells(1).Value)

                If Val(DGV1.Rows(cell_index).Cells(0).Value) = 0 And Val(DGV1.Rows(cell_index).Cells(10).Value) = 0 Then
                    Format(tbvolt.Text - 0, "000.00")
                Else
                    tbvolt.Text = Format(tbvolt.Text - komane1, "000.00")
                End If

                'tbvolt.Text = va.ToString("000.00")
                DGV1.Rows(cell_index).Cells(4).Value = tbvolt.Text
                DGV1.Rows(cell_index).Cells(5).Value = Val(DGV1.Rows(cell_index).Cells(0).Value) - Val(DGV1.Rows(cell_index).Cells(4).Value)
                DGV1.Rows(cell_index).Cells(8).Value = Format(Val(tbvolt.Text) * 1.733333, "000.00")

                Dim err As Double = Math.Abs(DGV1.Rows(cell_index).Cells(0).Value - DGV1.Rows(cell_index).Cells(8).Value)
                DGV1.Rows(cell_index).Cells(10).Value = Format(err, "000.00")
                'Call cek_error()
                Call cek_error2()
                Call isi_data()

            End If

            If Form1.tbisntrumen.Text = "Ammeter" Then
                'AGauge1.Value = Val(DGV1.Rows(fun - 1).Cells(1).Value)
                'Dim va As Double = Val(tbcurrent.Text) / 100 - 0.0001
                'tbcurrent.Text = va
                'DGV1.Rows(fun - 1).Cells(4).Value = tbvolt.Text
                'DGV1.Rows(fun - 1).Cells(5).Value = Val(DGV1.Rows(fun - 1).Cells(0).Value) - Val(DGV1.Rows(fun - 1).Cells(4).Value)

                tbcurrent.Text = Format(Val(tbcurrent.Text) - komanea, "0.0000")
                DGV1.Rows(cell_index).Cells(4).Value = Format(Val(tbcurrent.Text), "0.0000")
                If Form1.tbtype.Text = "Direct Connection" Then
                    DGV1.Rows(cell_index).Cells(8).Value = Format(Val(tbcurrent.Text) * ((Val(Label57.Text)) + 1), "0.0000")
                Else
                    DGV1.Rows(cell_index).Cells(8).Value = Format(Val(tbcurrent.Text) * (Val(Label57.Text)), "0.0000")
                End If
                'DGV1.Rows(cell_index).Cells(8).Value = Format(Val(tbcurrent.Text) * (Val(Label57.Text)), "0.0000")

                Dim err As Double = Math.Abs(DGV1.Rows(cell_index).Cells(0).Value - DGV1.Rows(cell_index).Cells(8).Value)
                DGV1.Rows(cell_index).Cells(10).Value = Format(err, "0.0000")

                Call cek_error2()

                Call isi_data()

            End If

        End If

        'DGV1.Rows(x5).Cells(3).Value = "2"
    End Sub


    Private Sub cek_error()

        If DGV1.Rows(fun - 1).Cells(8).Value < DGV1.Rows(fun - 1).Cells(6).Value Or DGV1.Rows(fun - 1).Cells(8).Value > DGV1.Rows(fun - 1).Cells(7).Value Then
            DGV1.Rows(fun - 1).Cells(2).Style.ForeColor = Color.Red
            DGV1.Rows(fun - 1).Cells(2).Value = "Fail"
            DGV1.Rows(fun - 1).Cells(9).Value = "0"
            Button4.Enabled = True
        Else
            DGV1.Rows(fun - 1).Cells(2).Style.ForeColor = Color.Lime
            DGV1.Rows(fun - 1).Cells(2).Value = "Pass"
            DGV1.Rows(fun - 1).Cells(9).Value = "1"
            Button4.Enabled = True
        End If

    End Sub

    Private Sub cek_error2()

        If DGV1.Rows(cell_index).Cells(8).Value < DGV1.Rows(cell_index).Cells(6).Value Or DGV1.Rows(cell_index).Cells(8).Value > DGV1.Rows(cell_index).Cells(7).Value Then
            'DGV1.Rows(cell_index).Cells(2).Style.ForeColor = Color.Red
            DGV1.Rows(cell_index).Cells(2).Value = "Fail"
            DGV1.Rows(cell_index).Cells(9).Value = "0"
            'Button4.Enabled = True
            Call Hitung_DATAGRIDVIEW()
        Else
            'DGV1.Rows(cell_index).Cells(2).Style.ForeColor = Color.Lime
            DGV1.Rows(cell_index).Cells(2).Value = "Pass"
            DGV1.Rows(cell_index).Cells(9).Value = "1"
            'Button4.Enabled = True
            Call Hitung_DATAGRIDVIEW()
        End If

    End Sub

    Private Sub cek_error1()

        If DGV1.Rows(i - 1).Cells(8).Value < DGV1.Rows(i - 1).Cells(6).Value Or DGV1.Rows(i - 1).Cells(8).Value > DGV1.Rows(i - 1).Cells(7).Value Then
            'DGV1.Rows(i - 1).Cells(2).Style.ForeColor = Color.Red
            DGV1.Rows(i - 1).Cells(2).Value = "Fail"
            DGV1.Rows(i - 1).Cells(9).Value = "0"
            Button4.Enabled = True
        Else
            'DGV1.Rows(i - 1).Cells(2).Style.ForeColor = Color.Lime
            DGV1.Rows(i - 1).Cells(2).Value = "Pass"
            DGV1.Rows(i - 1).Cells(9).Value = "1"
            Button4.Enabled = True
        End If

    End Sub

    Private Sub AGauge1_MouseWheel(sender As Object, e As MouseEventArgs) Handles AGauge1.MouseWheel

        If Form7.Label5.Text = "1" Then
            'If e.Delta > 0 And Label31.Text = "Verifikasi" And fungsi = 2 Then

            '    If Form1.tbisntrumen.Text = "Voltmeter" Then

            '        AGauge1.Value = Val(DGV1.Rows(fun - 1).Cells(1).Value)
            '        tbvolt.Text = Format(tbvolt.Text + komane1, "000.00")
            '        'tbvolt.Text = va.ToString("000.00")
            '        DGV1.Rows(fun - 1).Cells(4).Value = tbvolt.Text
            '        DGV1.Rows(fun - 1).Cells(5).Value = Val(DGV1.Rows(fun - 1).Cells(1).Value) - Val(DGV1.Rows(fun - 1).Cells(4).Value)

            '        DGV1.Rows(fun - 1).Cells(8).Value = Format(Val(tbvolt.Text) * 1.733333, "000.00")
            '        Dim err As Double = Math.Abs(DGV1.Rows(fun - 1).Cells(0).Value - DGV1.Rows(fun - 1).Cells(8).Value)
            '        DGV1.Rows(fun - 1).Cells(10).Value = Format(err, "000.00")
            '        'Call cek_error()
            '        Call cek_error()
            '        Call isi_data()

            '    End If

            '    If Form1.tbisntrumen.Text = "Ammeter" Then
            '        DGV1.Rows(fun - 1).Cells(4).Value = Format(Val(tbcurrent.Text), "0.0000")
            '        tbcurrent.Text = Format(Val(tbcurrent.Text) + komanea, "0.0000")
            '        DGV1.Rows(fun - 1).Cells(8).Value = Format(Val(tbcurrent.Text) * (Val(Label57.Text)), "0.0000")
            '        Dim err As Double = Math.Abs(DGV1.Rows(fun - 1).Cells(0).Value - DGV1.Rows(fun - 1).Cells(8).Value)
            '        DGV1.Rows(fun - 1).Cells(10).Value = Format(err, "0.0000")

            '        Call cek_error()
            '        Call isi_data()


            '    End If

            'End If

            'If e.Delta < 0 And Label31.Text = "Verifikasi" And fungsi = 2 Then
            '    'error1 = Val(vers2.Text) * (1.5 / 100)

            '    If Form1.tbisntrumen.Text = "Voltmeter" Then
            '        AGauge1.Value = Val(DGV1.Rows(fun - 1).Cells(1).Value)

            '        If Val(DGV1.Rows(fun - 1).Cells(0).Value) = 0 And Val(DGV1.Rows(fun - 1).Cells(10).Value) = 0 Then
            '            Format(tbvolt.Text - 0, "000.00")
            '        Else
            '            tbvolt.Text = Format(tbvolt.Text - komane1, "000.00")
            '        End If

            '        'tbvolt.Text = va.ToString("000.00")
            '        DGV1.Rows(fun - 1).Cells(4).Value = tbvolt.Text
            '        DGV1.Rows(fun - 1).Cells(5).Value = Val(DGV1.Rows(fun - 1).Cells(0).Value) - Val(DGV1.Rows(fun - 1).Cells(4).Value)
            '        DGV1.Rows(fun - 1).Cells(8).Value = Format(Val(tbvolt.Text) * 1.733333, "000.00")

            '        Dim err As Double = Math.Abs(DGV1.Rows(fun - 1).Cells(0).Value - DGV1.Rows(fun - 1).Cells(8).Value)
            '        DGV1.Rows(fun - 1).Cells(10).Value = Format(err, "000.00")
            '        'Call cek_error()
            '        Call cek_error()
            '        Call isi_data()

            '    End If

            '    If Form1.tbisntrumen.Text = "Ammeter" Then
            '        'AGauge1.Value = Val(DGV1.Rows(fun - 1).Cells(1).Value)
            '        'Dim va As Double = Val(tbcurrent.Text) / 100 - 0.0001
            '        'tbcurrent.Text = va
            '        'DGV1.Rows(fun - 1).Cells(4).Value = tbvolt.Text
            '        'DGV1.Rows(fun - 1).Cells(5).Value = Val(DGV1.Rows(fun - 1).Cells(0).Value) - Val(DGV1.Rows(fun - 1).Cells(4).Value)

            '        tbcurrent.Text = Format(Val(tbcurrent.Text) - komanea, "0.0000")
            '        DGV1.Rows(fun - 1).Cells(4).Value = Format(Val(tbcurrent.Text), "0.0000")
            '        DGV1.Rows(fun - 1).Cells(8).Value = Format(Val(tbcurrent.Text) * (Val(Label57.Text)), "0.0000")
            '        Dim err As Double = Math.Abs(DGV1.Rows(fun - 1).Cells(0).Value - DGV1.Rows(fun - 1).Cells(8).Value)
            '        DGV1.Rows(fun - 1).Cells(10).Value = Format(err, "0.0000")

            '        Call cek_error()
            '        Call isi_data()

            '    End If

            'End If
        End If

    End Sub


    Private Sub AGauge1_ValueChanged(sender As Object, e As EventArgs) Handles AGauge1.ValueChanged
        TextBox1.Text = AGauge1.Text
    End Sub


    Private Sub DGV1_MouseClick(sender As Object, e As MouseEventArgs) Handles DGV1.MouseClick
        Select Case e.Button
            Case MouseButtons.Left
                Label2.Text = "left"
                fun = fun + 1
                Label1.Text = fun
                If fun = 1 And fungsi = 0 And Label31.Text = "" Then
                    'fungsi = 1
                    'Label31.Text = "Kalibrasi"
                    'DGV1.Rows.Add(jmlrow)
                    'Call show_kalibrasi()

                    'If Form1.tbisntrumen.Text = "Ammeter" Then
                    '    Call arus_prd_varian()
                    '    'Call isi_data()
                    'End If


                End If

                If fungsi = 3 And tbstver.Text = "Pass" Then
                    Form3.Show()
                End If

                If Label31.Text = "Setting" Then
                    'DGV1.Rows.Add(jmlrow)
                    'Call show_data_setting()

                End If

            Case MouseButtons.Right
                Label2.Text = "right"
                mode = mode + 1
                Label35.Text = selisih & "  " & mode & "  " & ct & "  " & ct_up
                If mode = 1 Then
                    ct_up = 50
                    Label56.Text = "Hard"
                End If
                If mode = 2 Then
                    mode = 0
                    ct_up = 1
                    Label56.Text = "soft"
                End If

                Label2.Text = "right"
                If Label31.Text = "" Then
                    Label31.Text = "Setting"
                    Button4.Text = "Save"
                    DGV1.Rows.Add(jmlrow)
                    Call show_data_setting()
                    fungsi = 0
                End If

                If Label31.Text = "Verifikasi" Then
                    komane = komane + 1
                    If komane = 1 Then
                        komane1 = 0.01
                        komanea = 0.0001
                        Label56.Text = "Soft"
                    End If
                    If komane = 2 Then
                        komane1 = 0.1
                        komanea = 0.001
                        Label56.Text = "Hard"
                    End If
                    If komane = 3 Then
                        komane = 0
                        komanea = 0
                        Label56.Text = "Off"
                    End If
                End If


            Case MouseButtons.Middle
                Label2.Text = "midle"
                mode = mode + 1
                Label35.Text = selisih & "  " & mode & "  " & ct & "  " & ct_up
                If mode = 1 Then ct_up = 50
                If mode = 2 Then ct_up = 1
                If mode = 3 Then mode = 0

            Case Else
                Label2.Text = "liane"

        End Select

    End Sub

    Private Sub relay_off()
        Call Form1.r1off()
        Call Form1.r2off()
        Call Form1.r3off()
        Call Form1.r4off()
        Call Form1.r5off()
        Call Form1.r6off()

    End Sub

    Private Sub led_off()
        Call Form1.r7off()
        Call Form1.r8off()
        Call Form1.r9off()
        Call Form1.r10off()
    End Sub

    Private Sub alamat_spk1()
        Dim lokasi As String = My.Application.Info.DirectoryPath
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(lokasi & "\alamat_server.txt")
        Dim str As String = fileReader
        Dim str2() As String = Split(str, ",")

        Form1.WebKitBrowser1.Navigate(str2(4))
        Form1.WebKitBrowser1.Navigate(str2(5) & "trigger.php?id=1&varian= ")
    End Sub

    Private Sub kosongi_varian()

        Dim lokasi As String = My.Application.Info.DirectoryPath
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(lokasi & "\alamat_server.txt")
        Dim str As String = fileReader
        Dim str2() As String = Split(str, ",")

        WebKitBrowser1.Navigate(str2(5) & "trigger.php?id=1&varian= ")

    End Sub
    Dim xx As Integer = 0


    Private Sub close_form2()
        fun = 0
        fungsi = 0
        tbvolt.Text = "000.00"
        tbcurrent.Text = "0.0000"

        Call isi_data()
        If Form1.CheckBox2.Checked = True Then
            Call relay_off()
            Call led_off()

        End If


        'Call kosongi_varian()

        'Call alamat_spk1()

        'Form1.sr_ard.Open()
        'Form1.sr_ard.Write("ef")
        'Form1.sr_ard.Close()
        If Form7.Label5.Text = "3" Then
            'Me.Hide()
            Form1.Show()

        End If
        Form3.Close()
        Form1.Show()

        Close()
        Form1.WebKitBrowser1.Select()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Timer1.Enabled = True

        conn.Close()
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        tbfrek.Text = ComboBox1.Text
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        jigke = 1

        If Form1.relay.Text = "RL 1" Then
            jig2.ForeColor = Color.LimeGreen
            jig1.ForeColor = Color.LightSlateGray
            jig3.ForeColor = Color.LightSlateGray
            jig4.ForeColor = Color.LightSlateGray
            If Form1.CheckBox2.Checked = True Then
                Call Form1.r1on()
                Call Form1.r2off()
                Call Form1.r3off()
                Call Form1.r4off()
                Call Form1.r5off()
                Call Form1.r6off()
            End If
        End If

        If Form1.relay.Text = "RL 2" Then
            jig2.ForeColor = Color.LimeGreen
            jig1.ForeColor = Color.LightSlateGray
            jig3.ForeColor = Color.LightSlateGray
            jig4.ForeColor = Color.LightSlateGray
            If Form1.CheckBox2.Checked = True Then
                Call Form1.r2on()
                Call Form1.r1off()
                Call Form1.r3off()
                Call Form1.r4off()
                Call Form1.r5off()
                Call Form1.r6off()
            End If
        End If

        If Form1.relay.Text = "RL 3" Then
            jig1.ForeColor = Color.LimeGreen
            jig2.ForeColor = Color.LightSlateGray
            jig3.ForeColor = Color.LightSlateGray
            jig4.ForeColor = Color.LightSlateGray
            If Form1.CheckBox2.Checked = True Then
                Call Form1.r3on()
                Call Form1.r2off()
                Call Form1.r1off()
                Call Form1.r4off()
                Call Form1.r5off()
                Call Form1.r6off()
            End If
        End If

        If Form1.relay.Text = "RL 4" Then
            jig1.ForeColor = Color.LimeGreen
            jig2.ForeColor = Color.LightSlateGray
            jig3.ForeColor = Color.LightSlateGray
            jig4.ForeColor = Color.LightSlateGray
            If Form1.CheckBox2.Checked = True Then
                Call Form1.r4on()
                Call Form1.r2off()
                Call Form1.r3off()
                Call Form1.r1off()
                Call Form1.r5off()
                Call Form1.r6off()
            End If
        End If

        If Form1.relay.Text = "RL 5" Then
            jig4.ForeColor = Color.LimeGreen
            jig1.ForeColor = Color.LightSlateGray
            jig2.ForeColor = Color.LightSlateGray
            jig3.ForeColor = Color.LightSlateGray
            If Form1.CheckBox2.Checked = True Then
                Call Form1.r5on()
                Call Form1.r2off()
                Call Form1.r3off()
                Call Form1.r4off()
                Call Form1.r1off()
                Call Form1.r6off()
            End If
        End If

        If Form1.relay.Text = "RL 6" Then
            jig3.ForeColor = Color.LimeGreen
            jig2.ForeColor = Color.LightSlateGray
            jig1.ForeColor = Color.LightSlateGray
            jig4.ForeColor = Color.LightSlateGray
            If Form1.CheckBox2.Checked = True Then
                Call Form1.r6on()
                Call Form1.r2off()
                Call Form1.r3off()
                Call Form1.r4off()
                Call Form1.r1off()
                Call Form1.r5off()
            End If
        End If




        'If Microsoft.VisualBasic.Left(Form1.Label40.Text, 1) = "B" Then
        '    jig1.ForeColor = Color.LimeGreen
        '    jig2.ForeColor = Color.LightSlateGray
        '    jig3.ForeColor = Color.LightSlateGray
        '    jig4.ForeColor = Color.LightSlateGray
        '    If Form1.CheckBox2.Checked = True Then
        '        Form1.sr_ard.Open()
        '        Form1.sr_ard.WriteLine("2on")
        '        Form1.sr_ard.Close()
        '    End If

        'End If


        'If Microsoft.VisualBasic.Left(Form1.Label40.Text, 1) = "A" Then

        '    jig1.ForeColor = Color.LightSlateGray
        '    jig2.ForeColor = Color.LimeGreen
        '    jig3.ForeColor = Color.LightSlateGray
        '    jig4.ForeColor = Color.LightSlateGray
        '    If Form1.CheckBox2.Checked = True Then
        '        Form1.sr_ard.Open()
        '        Form1.sr_ard.WriteLine("1on")
        '        Form1.sr_ard.Close()
        '    End If
        'End If

        'If Microsoft.VisualBasic.Left(Form1.Label40.Text, 1) = "C" Then
        '    jig1.ForeColor = Color.LightSlateGray
        '    jig2.ForeColor = Color.LightSlateGray
        '    jig3.ForeColor = Color.LimeGreen
        '    jig4.ForeColor = Color.LightSlateGray
        '    If Form1.CheckBox2.Checked = True Then
        '        Form1.sr_ard.Open()
        '        Form1.sr_ard.WriteLine("3off")
        '        Form1.sr_ard.Close()
        '    End If
        'End If

        'If Microsoft.VisualBasic.Left(Form1.Label40.Text, 1) = "D" Then
        '    jig1.ForeColor = Color.LightSlateGray
        '    jig2.ForeColor = Color.LightSlateGray
        '    jig3.ForeColor = Color.LimeGreen
        '    jig4.ForeColor = Color.LightSlateGray
        '    If Form1.CheckBox2.Checked = True Then
        '        Form1.sr_ard.Open()
        '        Form1.sr_ard.WriteLine("4off")
        '        Form1.sr_ard.Close()
        '    End If
        'End If

        If Button7.Text = "Save" Then
            ComboBox2.Enabled = False
            Button7.Text = "Edit"
        ElseIf Button7.Text = "Edit" Then
            ComboBox2.Enabled = True
            Button7.Text = "Save"
        End If




    End Sub

    Private Sub pesan()
        Dim b As Object = MessageBox.Show("Pass tekan Yes, Fail tekan No, Cancel untuk mengulang", "Hasil Titik Kalibrasi", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        If b = Windows.Forms.DialogResult.Yes Then
            Label7.Text = "Pass"
        ElseIf b = Windows.Forms.DialogResult.No Then
            Label7.Text = "Fail"
        Else
            Label7.Text = "Cancel"
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Form3.Show()

    End Sub

    Private Sub tbstcal_TextChanged(sender As Object, e As EventArgs) Handles tbstcal.TextChanged, TextBox9.TextChanged, TextBox8.TextChanged, TextBox7.TextChanged, TextBox11.TextChanged, TextBox10.TextChanged, TextBox17.TextChanged, TextBox16.TextChanged, TextBox15.TextChanged, TextBox14.TextChanged, TextBox13.TextChanged, TextBox12.TextChanged, TextBox18.TextChanged, tbcenter.TextChanged, tbdiameter.TextChanged, TextBox21.TextChanged, TextBox20.TextChanged
        If tbstcal.Text = "  Fail" Then tbstcal.ForeColor = Color.Red
    End Sub

    Private Sub GroupBox6_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim bytes() As Byte = {&H2, &H1D, &H0, &H62, &H33, &H30, &H30, &H2E, &H30, &H30, &H30, &H2E, &H30, &H30, &H30, &H30, &H36, &H30, &H2E, &H30, &H30, &H30, &H30, &H30, &H2E, &H30, &H0, &H5A, &H3}

        'Label49.Text = bytes(0).ToString

        Call koneksine()
        Try

            Dim str As String
            str = "UPDATE test_e1 SET diameter = '" & tbdiameter.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"

            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Insert jarum Berhasil Dilakukan")

        Catch ex As Exception
            MessageBox.Show("Insert jarum gagal dilakukan.")
        End Try
    End Sub

    Private Sub cekerror()
        'vers1.ForeColor = Color.Black
        error1 = Val(vers2.Text) * (0.5 / 100)
        Label8.Text = error1
        If Val(TextBox2.Text) > Val(skv1.Text) + error1 And ver1 = 1 And Label3.Text = "Verifikasi" Then
            resver1 = 1
            Label9.Text = Val(skv1.Text) + error1
            Label10.Text = "Fail"
            vers1.ForeColor = Color.Red
            resver1 = 1
        ElseIf Val(TextBox2.Text) < Val(skv1.Text) - error1 And ver1 = 1 And Label3.Text = "Verifikasi" Then
            resver1 = 1
            Label9.Text = Val(skv1.Text) + error1
            Label10.Text = "Fail"
            vers1.ForeColor = Color.Red
            resver1 = 1
        ElseIf ver1 = 1 And Label3.Text = "Verifikasi" Then
            resver1 = 2
            Label10.Text = "Pass"
            vers1.ForeColor = Color.Lime
        End If

        If Val(TextBox2.Text) > Val(skv2.Text) + error1 And ver1 = 1 And Label3.Text = "Verifikasi" Then
            resver2 = 1
            Label9.Text = Val(skv2.Text) + error1
            Label10.Text = "Fail"
            'vers2.ForeColor = Color.Red
        ElseIf Val(TextBox2.Text) < Val(skv2.Text) - error1 And ver1 = 1 And Label3.Text = "Verifikasi" Then
            resver2 = 1
            Label9.Text = Val(skv2.Text) + error1
            Label10.Text = "Fail"
            'vers2.ForeColor = Color.Red
        ElseIf ver1 = 1 And Label3.Text = "Verifikasi" Then
            resver2 = 2
            Label10.Text = "Pass"
            'vers2.ForeColor = Color.Lime
        End If

        If Val(TextBox2.Text) > Val(skv3.Text) + error1 And ver1 = 1 And Label3.Text = "Verifikasi" Then
            resver3 = 1
            Label9.Text = Val(skv3.Text) + error1
            Label10.Text = "Fail"
            'vers3.ForeColor = Color.Red
        ElseIf Val(TextBox2.Text) < Val(skv3.Text) - error1 And ver1 = 1 And Label3.Text = "Verifikasi" Then
            resver3 = 1
            Label9.Text = Val(skv3.Text) + error1
            Label10.Text = "Fail"
            'vers3.ForeColor = Color.Red
        ElseIf ver1 = 1 And Label3.Text = "Verifikasi" Then
            resver3 = 2
            Label10.Text = "Pass"
            'vers3.ForeColor = Color.Lime
        End If

        If resver1 = 0 Then vers1.ForeColor = Color.Black
        If resver1 = 1 Then vers1.ForeColor = Color.Red
        If resver1 = 2 Then vers1.ForeColor = Color.Lime
        If resver2 = 0 Then vers2.ForeColor = Color.Black
        If resver2 = 1 Then vers2.ForeColor = Color.Red
        If resver2 = 2 Then vers2.ForeColor = Color.Lime
        If resver3 = 0 Then vers3.ForeColor = Color.Black
        If resver3 = 1 Then vers3.ForeColor = Color.Red
        If resver3 = 2 Then vers3.ForeColor = Color.Lime
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PrintDocument1.Print()

    End Sub



    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click

    End Sub

    Private Sub iter2()
        If fun = 3 And Label3.Text = "Verifikasi" And cal = True And totiter = 2 Then
            'stkalibrasi.Text = "Verifikasi" & "  " & totiter
            AGauge1.Value = Val(skv1.Text)
            'vers1.ForeColor = Color.Lime
            ver1 = 1

        End If
        If fun = 4 And Label3.Text = "Verifikasi" And cal = True And totiter = 2 Then
            'stkalibrasi.Text = "Verifikasi" & "  " & totiter
            vers1.ForeColor = Color.Lime
            AGauge1.Value = Val(skv2.Text)
            'vers2.ForeColor = Color.Lime
            ver1 = 2
            DGV1.Rows(6).Cells(0).Style.ForeColor = Color.Lime
            DGV1.Rows(6).Cells(1).Style.ForeColor = Color.Lime
            DGV1.Rows(6).Cells(1).Value = Label20.Text
            DGV1.Rows(6).Cells(2).Value = Label4.Text

        End If
        If fun = 5 And Label3.Text = "Verifikasi" And cal = True And totiter = 2 Then
            'stkalibrasi.Text = "Verifikasi" & "  " & totiter
            vers2.ForeColor = Color.Lime
            AGauge1.Value = Val(skv3.Text)
            'vers3.ForeColor = Color.Lime
            ver1 = 3
            DGV1.Rows(7).Cells(0).Style.ForeColor = Color.Lime
            DGV1.Rows(7).Cells(1).Style.ForeColor = Color.Lime
            DGV1.Rows(7).Cells(1).Value = Label11.Text
            DGV1.Rows(7).Cells(2).Value = Label4.Text
        End If
        If fun = 6 And Label3.Text = "Verifikasi" And cal = True And totiter = 2 Then
            'stkalibrasi.Text = "Verifikasi" & "  " & totiter
            vers3.ForeColor = Color.Lime
            AGauge1.Value = Val(skv4.Text)
            'vers4.ForeColor = Color.Lime
            ver1 = 4
            DGV1.Rows(8).Cells(0).Style.ForeColor = Color.Lime
            DGV1.Rows(8).Cells(1).Style.ForeColor = Color.Lime
            DGV1.Rows(8).Cells(1).Value = Label16.Text
            DGV1.Rows(8).Cells(2).Value = Label4.Text
        End If
        If fun = 7 And Label3.Text = "Verifikasi" And cal = True And totiter = 2 Then
            'stkalibrasi.Text = "Verifikasi" & "  " & totiter
            vers4.ForeColor = Color.Lime
            AGauge1.Value = Val(skv5.Text)
            'vers5.ForeColor = Color.Lime
            ver1 = 5
            DGV1.Rows(9).Cells(0).Style.ForeColor = Color.Lime
            DGV1.Rows(9).Cells(1).Style.ForeColor = Color.Lime
            DGV1.Rows(9).Cells(1).Value = Label17.Text
            DGV1.Rows(9).Cells(2).Value = Label4.Text
        End If
        If fun = 8 And Label3.Text = "Verifikasi" And cal = True And totiter = 2 Then
            'stkalibrasi.Text = "Verifikasi" & "  " & totiter
            vers5.ForeColor = Color.Lime
            AGauge1.Value = Val(skv6.Text)
            'vers6.ForeColor = Color.Lime
            DGV1.Rows(10).Cells(0).Style.ForeColor = Color.Lime
            DGV1.Rows(10).Cells(1).Style.ForeColor = Color.Lime
            DGV1.Rows(10).Cells(1).Value = Label18.Text
            DGV1.Rows(10).Cells(2).Value = Label4.Text
            'fun = 1
            ver1 = 6

        End If

        If fun = 9 And Label3.Text = "Verifikasi" And cal = True And totiter = 2 Then
            vers6.ForeColor = Color.Lime
            totiter = 3
            'Label14.Text = "2"
            fun = 1
            ver1 = 7
            DGV1.Rows(11).Cells(0).Style.ForeColor = Color.Lime
            DGV1.Rows(11).Cells(1).Style.ForeColor = Color.Lime
            DGV1.Rows(11).Cells(1).Value = Label19.Text
            DGV1.Rows(11).Cells(2).Value = Label4.Text
            If totiter = Val(iters.Text) + 1 Then
                Label14.Text = totiter
                'stkalibrasi.ForeColor = Color.Lime
                'stkalibrasi.Text = "PASS"
                tbstver.ForeColor = Color.Lime
                tbstver.Text = "  PASS"
                Label3.Text = "Print"
                ver1 = 0
            End If
        End If
    End Sub

    Private Sub DGV1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub isi_data()

        'combo frekswensi
        ComboBox1.Items.Add("50.00")
        ComboBox1.Items.Add("60.00")
        ComboBox1.Text = ComboBox1.Items(0)

        'header
        tbheader.Text = stx + fl + fh + cmdp
        Dim head As String = Val(stx) & "," & Val(fl) & "," & Val(fh) & "," & Val(cmdp)
        tbhead.Text = head

        'volt
        Dim value As String = tbvolt.Text
        Dim arrayv() As Byte = System.Text.Encoding.ASCII.GetBytes(value)
        Dim volt As String = (String.Join(",", arrayv.ConvertAll(arrayv, Function(byteValue) byteValue.ToString)))
        tb_volt_hex.Text = volt
        Dim volt_total As Integer = Val(arrayv(0)) + Val(arrayv(1)) + Val(arrayv(2)) + Val(arrayv(3)) + Val(arrayv(4)) + Val(arrayv(5))
        Dim strv As String() = volt.Split(","c)

        'arus
        Dim valarus As String = tbcurrent.Text
        'Dim ba As Byte = valarus
        'tbcurrent.Text = String.Format("{0:0.0000}", ba)
        Dim arrus() As Byte = System.Text.Encoding.ASCII.GetBytes(valarus)
        Dim arus As String = (String.Join(",", arrus.ConvertAll(arrus, Function(byteValue) byteValue.ToString)))
        tb_current_hex.Text = arus
        Dim current_total As Integer = Val(arrus(0)) + Val(arrus(1)) + Val(arrus(2)) + Val(arrus(3)) + Val(arrus(4)) + Val(arrus(5))
        Dim stra As String() = arus.Split(","c)

        'frekwensi
        Dim valfrek As String = tbfrek.Text
        Dim arrfrek() As Byte = System.Text.Encoding.ASCII.GetBytes(valfrek)
        Dim frek As String = (String.Join(",", arrfrek.ConvertAll(arrfrek, Function(byteValue) byteValue.ToString)))
        tb_frek_hex.Text = frek
        Dim frek_total As Integer = Val(arrfrek(0)) + Val(arrfrek(1)) + Val(arrfrek(2)) + Val(arrfrek(3)) + Val(arrfrek(4))
        Dim strf As String() = frek.Split(","c)

        'cosphi
        Dim valteta As String = tbteta.Text
        Dim b As Byte = valteta
        tbteta.Text = String.Format("{0:000.0}", b)
        Dim arrteta() As Byte = System.Text.Encoding.ASCII.GetBytes(tbteta.Text)
        Dim teta As String = (String.Join(",", arrteta.ConvertAll(arrteta, Function(byteValue) byteValue)))
        tb_teta_hex.Text = teta
        Dim cosphi_total As Integer = Val(arrteta(0)) + Val(arrteta(1)) + Val(arrteta(2)) + Val(arrteta(3)) + Val(arrteta(4))
        Dim strt As String() = teta.Split(","c)

        'sys
        tbsys.Text = Val(&H0)
        tb_sys_hex.Text = Val(tbsys.Text)

        'etx
        tbext.Text = etx
        tb_ext_hex.Text = etx

        'hitung_total
        tbtotal.Text = Val(tbheader.Text) + volt_total + current_total + frek_total + cosphi_total + Val(tb_sys_hex.Text) + Val(tb_ext_hex.Text)

        'hitung cs
        tbcs.Text = totdata - Val(tbtotal.Text)

        TextBox27.Text = Val(tbtotal.Text) + Val(tbcs.Text)

        'kirim
        Dim datane() As Byte = {stx, fl, fh, cmdp, strv(0), strv(1), strv(2), strv(3), strv(4), strv(5),
                                stra(0), stra(1), stra(2), stra(3), stra(4), stra(5),
                                strf(0), strf(1), strf(2), strf(3), strf(4), strt(0), strt(1), strt(2), strt(3), strt(4),
                                &H0, Val(tbcs.Text), &H3}

        TextBox19.Text = (String.Join(",", datane.ConvertAll(datane, Function(byteValue) byteValue.ToString)))

        If Form1.CheckBox1.Checked = True Then
            Form1.sr_ptc.Open()
            Form1.sr_ptc.Write(datane, 0, datane.Length)
            Form1.sr_ptc.Close()
        End If


    End Sub

    Private Sub GroupBox8_Enter(sender As Object, e As EventArgs) Handles GroupBox8.Enter

    End Sub

    Dim cell_index As Integer = 0
    Private Sub DGV1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV1.CellClick
        If Label31.Text = "Kalibrasi" Then
            If Form1.tbisntrumen.Text = "Voltmeter" Or Form1.tbisntrumen.Text = "Double Voltmeter" Then
                If fungsi = 1 And jigke = 1 Then
                    AGauge1.Value = DGV1.Rows(e.RowIndex).Cells(3).Value
                    'DGV1.Rows(e.RowIndex).Cells(0).Style.ForeColor = Color.Lime
                    DGV1.Rows(e.RowIndex).Cells(9).Value = "1"
                    tbvolt.Text = Format(DGV1.Rows(e.RowIndex).Cells(0).Value / 1.733333, "000.00")
                    Call isi_data()
                    'Call Hitung_DATAGRIDVIEW()
                    If Val(Label37.Text) = 6 Then
                        Button4.Enabled = True

                    Else
                        Button4.Enabled = False
                    End If
                End If
            End If
            If Form1.tbisntrumen.Text = "Ammeter" Then
                If fungsi = 1 And jigke = 1 Then
                    AGauge1.Value = DGV1.Rows(e.RowIndex).Cells(3).Value
                    'DGV1.Rows(e.RowIndex).Cells(0).Style.ForeColor = Color.Lime
                    DGV1.Rows(e.RowIndex).Cells(9).Value = "1"
                    If Form1.tbtype.Text = "Direct Connection" Then
                        tbcurrent.Text = Format(Val(DGV1.Rows(e.RowIndex).Cells(0).Value) / 6, "0.0000")
                        If Val(tbcurrent.Text) > 6.2 Then
                            tbcurrent.Text = "6.2000"

                        End If
                        Label66.Text = tbcurrent.Text
                    Else
                        tbcurrent.Text = Format(Val(DGV1.Rows(e.RowIndex).Cells(0).Value) / Val(Label57.Text), "0.0000")
                    End If

                    Call isi_data()
                        'Call Hitung_DATAGRIDVIEW()
                        If Val(Label37.Text) = 6 Then
                            Button4.Enabled = True
                        Else
                            Button4.Enabled = False
                        End If
                    End If
                End If

        End If

        If Label31.Text = "Verifikasi" Then
            If Form1.tbisntrumen.Text = "Voltmeter" Or Form1.tbisntrumen.Text = "Double Voltmeter" Then
                If fungsi = 2 And jigke = 1 Then
                    AGauge1.Value = DGV1.Rows(e.RowIndex).Cells(1).Value
                    'DGV1.Rows(e.RowIndex).Cells(0).Style.ForeColor = Color.Lime
                    'DGV1.Rows(e.RowIndex).Cells(9).Value = "1"
                    tbvolt.Text = Format(DGV1.Rows(e.RowIndex).Cells(0).Value / 1.733333, "000.00")
                    cell_index = e.RowIndex
                    'Label58.Text = cell_index
                    Call isi_data()
                    'Call Hitung_DATAGRIDVIEW()
                    If Val(Label37.Text) = 6 * 2 Then
                        Button4.Enabled = True
                        Call finish()
                    Else
                        Button4.Enabled = False
                    End If
                End If
            End If
            If Form1.tbisntrumen.Text = "Ammeter" Then
                If fungsi = 2 And jigke = 1 Then
                    AGauge1.Value = DGV1.Rows(e.RowIndex).Cells(1).Value
                    'DGV1.Rows(e.RowIndex).Cells(0).Style.ForeColor = Color.Lime
                    'DGV1.Rows(e.RowIndex).Cells(9).Value = "1"
                    If Form1.tbtype.Text = "Direct Connection" Then
                        tbcurrent.Text = Format(Val(DGV1.Rows(e.RowIndex).Cells(0).Value) / 6, "0.0000")
                        If Val(tbcurrent.Text) > 6.2 Then
                            tbcurrent.Text = "6.2000"

                        End If
                        Label66.Text = tbcurrent.Text
                    Else
                        tbcurrent.Text = Format(Val(DGV1.Rows(e.RowIndex).Cells(0).Value) / Val(Label57.Text), "0.0000")
                    End If

                    'tbcurrent.Text = Format(Val(DGV1.Rows(e.RowIndex).Cells(0).Value) / Val(Label57.Text), "0.0000")
                    cell_index = e.RowIndex
                    'Label58.Text = cell_index
                    Call isi_data()
                    Call Hitung_DATAGRIDVIEW()
                    If Val(Label37.Text) = 6 * 2 Then
                        Button4.Enabled = True
                        Call finish()
                    Else
                        Button4.Enabled = False
                    End If
                End If
            End If

        End If

        If Label31.Text = "Setting" Then
            AGauge1.Value = DGV1.Rows(e.RowIndex).Cells(3).Value
            Label47.Text = DGV1.CurrentCell.RowIndex.ToString
            DGV1.Rows(e.RowIndex).Cells(0).Style.ForeColor = Color.Lime
        End If

    End Sub

    Private Sub finish()
        tbvolt.Text = "000.00"
        AGauge1.Value = 0
        Label31.Text = "Printing"
        Form3.Show()
        Call saveData()
        Form3.Hide()

    End Sub

    Private Sub arus_prd_varian()
        Dim prodvarian As String = Form1.tbprdvarian.Text

        If prodvarian = "25(50)A" Then
            prodvarian = "25/5A"
            Dim substr As String() = prodvarian.Split("/"c)
            Label57.Text = Val(substr(0)) / Val(Microsoft.VisualBasic.Left(substr(1), 1))
        ElseIf prodvarian = "10(20)A" Then
            prodvarian = "20/5A"
            Dim substr As String() = prodvarian.Split("/"c)
            Label57.Text = Val(substr(0)) / Val(Microsoft.VisualBasic.Left(substr(1), 1))
        ElseIf prodvarian = "40(80)A" Then
            prodvarian = "80/1A"
            Dim substr As String() = prodvarian.Split("/"c)
            Label57.Text = Val(substr(0)) / Val(Microsoft.VisualBasic.Left(substr(1), 1))
        Else
            Dim substr As String() = prodvarian.Split("/"c)
            Label57.Text = Val(substr(0)) / Val(Microsoft.VisualBasic.Left(substr(1), 1))
        End If




    End Sub

    Private Sub DGV1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DGV1.CellContentClick

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Label31.Text = "Kalibrasi"
        fungsi = 1
        jigke = 0
        GroupBox12.Font = New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
        Me.DGV1.Columns(2).Visible = False
        'Me.DGV1.ClearSelection()
        'Me.DGV1.Rows[dgv1.Rows.Count - 1].Selected = True
        If Form1.CheckBox2.Checked = True Then
            Call relay_off()
        End If


        AGauge1.Value = 0
        DGV1.Rows.Clear()
        DGV1.Rows.Add(jmlrow)
        Call show_kalibrasi()
        Me.DGV1.FirstDisplayedScrollingRowIndex = Me.DGV1.Rows.Count - 1
        Me.DGV1.Rows(DGV1.Rows.Count - 1).Selected = True
        tbvolt.Text = Format(0, "000.00")
        tbcurrent.Text = Format(0, "0.0000")
        Call isi_data()
        If Form1.tbisntrumen.Text = "Ammeter" Then
            Call arus_prd_varian()
            'Call isi_data()
            Me.DGV1.FirstDisplayedScrollingRowIndex = Me.DGV1.Rows.Count - 1
            Me.DGV1.Rows(DGV1.Rows.Count - 1).Selected = True
        End If

        Button7.Text = "Start"
        jig1.ForeColor = Color.LightSlateGray
        jig2.ForeColor = Color.LightSlateGray
        jig3.ForeColor = Color.LightSlateGray
        jig4.ForeColor = Color.LightSlateGray
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Label31.Text = "Verifikasi"
        fungsi = 2
        jigke = 0
        GroupBox12.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
        Me.DGV1.Columns(2).Visible = True
        Me.DGV1.ClearSelection()
        AGauge1.Value = 0
        DGV1.Rows.Clear()
        DGV1.Rows.Add(jmlrow * 2)
        'Button4.Text = "Next"
        Button4.Enabled = False
        'Button5.Enabled = False
        Call show_verifikasi()
        tbvolt.Text = Format(0, "000.00")
        tbcurrent.Text = Format(0, "0.0000")
        Call isi_data()
        Button7.Text = "Start"
        If Form1.CheckBox2.Checked = True Then
            Call relay_off()
        End If

        jig1.ForeColor = Color.LightSlateGray
        jig2.ForeColor = Color.LightSlateGray
        jig3.ForeColor = Color.LightSlateGray
        jig4.ForeColor = Color.LightSlateGray
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        jigke = 0
        AGauge1.Value = 0
        jig1.ForeColor = Color.LightSlateGray
        jig2.ForeColor = Color.LightSlateGray
        jig3.ForeColor = Color.LightSlateGray
        jig4.ForeColor = Color.LightSlateGray
        tbcurrent.Text = "0.0000"
        tbvolt.Text = "000.00"


        Label31.Text = "Printing"
        If Form1.CheckBox2.Checked = True Then
            Call relay_off()
        End If

    End Sub

    Private Sub Label53_Click(sender As Object, e As EventArgs) Handles Label53.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        xx = xx + 1
        If xx = 1 Then
            Dim lokasi As String = My.Application.Info.DirectoryPath
            Dim fileReader As String
            fileReader = My.Computer.FileSystem.ReadAllText(lokasi & "\alamat_server.txt")
            Dim str As String = fileReader
            Dim str2() As String = Split(str, ",")

            WebKitBrowser1.Navigate(str2(5) & "trigger.php?id=1&varian= ")

        End If
        If xx = 2 Then
            Call close_form2()
            Timer1.Enabled = False
            xx = 0
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click

        Form3.TabControl1.SelectedIndex = 2
        Form3.tbsn.Text = TextBox20.Text
        Form3.Show()

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        WebKitBrowser2.Navigate("http://10.100.0.104/spk/index.php/logins")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'If Button4.Text = "Start" Then
        '    Label31.Text = "Kalibrasi"
        '    fungsi = 1
        '    DGV1.Rows.Add(jmlrow)
        '    Call show_kalibrasi()
        '    Button4.Text = "Pass (Next)"
        'End If

        'If Label31.Text = "Kalibrasi" And Button4.Text = "Pass (Next)" And Val(Label37.Text) = 6 Then
        '    tbstcal.ForeColor = Color.Lime
        '    tbstcal.Text = "Pass"
        '    fun = 0
        '    fungsi = 2
        'End If

        'If Label31.Text = "Kalibrasi" And tbstcal.Text = "Pass" Then
        '    Label31.Text = "Verifikasi"
        '    DGV1.Rows.Clear()
        '    DGV1.Rows.Add(jmlrow * 2)
        '    Button4.Text = "Next"
        '    Button4.Enabled = False
        '    'Button5.Enabled = False
        '    Call show_verifikasi()
        '    Call isi_data()
        'End If

        If Label31.Text = "Verifikasi" Then
            tbvolt.Text = "000.00"
            tbcurrent.Text = "0.0000"
            AGauge1.Value = 0
            Label31.Text = "Printing"
            Form3.Show()
            Call saveData()

            'kene next
            'GroupBox6.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            'Me.DGV1.Columns(2).Visible = True
            'i = i + 1
            'Call view_print()
            'Call draw_line()

            'If i = (jmlrow * 2) + 1 Then
            '    i = 0
            '    fun = 0
            '    iterasine = iterasine + 2
            '    Label34.Text = iterasine & " " & iterasi
            '    If iterasine = iterasi Then
            '        MsgBox("selesai.. Sudah 2 kali iterasi.. ")
            '        'Hasil Verifikasi
            '        Button4.Enabled = False
            '        Call Hitung_DATAGRIDVIEW()
            '        If Label37.Text < 12 Then
            '            tbstver.ForeColor = Color.Red
            '            tbstver.Text = "Fail"

            '        End If
            '        If Label37.Text = 12 Then
            '            tbstver.ForeColor = Color.Lime
            '            tbstver.Text = "Pass"
            '            Call saveData()
            '            Button8.Enabled = True
            '            Form3.Show()
            '            Label31.Text = "Printing"

            '            Call view_print()
            '            Call draw_line()


            '        End If
            '        fungsi = 3
            '        fun = 0
            '    End If
            'Else

            '    DGV1.ClearSelection()
            '    DGV1.Rows(i - 1).Selected = True
            '    'DGV1.Rows(i - 1).Cells(0).Style.ForeColor = Color.Lime
            '    AGauge1.Value = Me.DGV1.SelectedCells(1).Value.ToString
            '    testpoint = Val(Me.DGV1.SelectedCells(0).Value.ToString) / 1.733333
            '    realpoint = Val(Me.DGV1.SelectedCells(0).Value.ToString) / Val(Label57.Text)
            '    If Form1.tbisntrumen.Text = "Voltmeter" Then tbvolt.Text = testpoint.ToString("000.00")
            '    If Form1.tbisntrumen.Text = "Ammeter" Then tbcurrent.Text = realpoint.ToString("0.0000")
            '    Button4.Enabled = False
            'End If

        End If

        If Label31.Text = "Setting" Then
            'update data

            Call koneksine()
            Try
                'Dim str As String
                'str = "UPDATE tb_calibrasi SET sdcal1 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
                For i As Integer = 0 To Val(Form1.Label20.Text)
                    querysimpan = "UPDATE test_e1 SET sdver" & i & " = '" & DGV1.Rows(i).Cells(3).Value & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"

                    cmd = New MySqlCommand(querysimpan, conn)
                    cmd.ExecuteNonQuery()
                Next
                MessageBox.Show("Insert Data Berhasil Dilakukan")


            Catch ex As Exception
                MessageBox.Show("Insert data gagal dilakukan.")
            End Try

        End If
    End Sub

    Private Sub draw_line()
        Dim blackpen As New Pen(Color.Black, 1)

        Dim g As Graphics = Form3.GroupBox1.CreateGraphics()
        g.DrawLine(blackpen, 26, 46, 320, 46)
        g.DrawLine(blackpen, 26, 71, 320, 71)
        g.DrawLine(blackpen, 26, 101, 320, 101)
        g.DrawLine(blackpen, 26, 126, 320, 126)
        g.DrawLine(blackpen, 26, 151, 320, 151)
        blackpen.Dispose()
    End Sub

    Private Sub view_print()

        Dim str1 As String = Form1.tbprdgroup.Text
        Dim str2 As String() = str1.Split(" "c)
        Form3.Label4.TextAlign = ContentAlignment.MiddleRight

        Form3.Label1.Text = str2(0)
        Form3.Label2.Text = "S/N :   " & Form1.Label28.Text
        Form3.Label3.Text = Form1.tbprdvarian.Text
        Form3.Label4.Text = Form1.tbtype.Text
        Form3.Label5.Text = "Class  : " & Form1.tbclass.Text
        Form3.Label6.Text = Form1.tbsignal.Text
        Form3.Label7.Text = Form1.tbfreq.Text
        Form3.Label8.Text = Form1.tbsize.Text
        Form3.Label9.Text = Form1.Label28.Text

        Form3.Label9.Text = Form1.Label28.Text
        Form3.PictureBox1.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData("http://" & server & "/senzo/assets/picstyle/wiring/" & Form1.lbsticker.Text & ".jpg")))
        Form3.Timer1.Start()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Label31.Text = "rePrinting"
        Form3.Show()
        tbvolt.Text = "000.00"
        tbcurrent.Text = "0.0000"
        AGauge1.Value = 0

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        '[Call saveData() 420,311

        'Dim dc As DataGridViewColumn = DGV1.Columns(i)
        ''dc.HeaderCell.Style.BackColor = Color.FromArgb(196, 196, 196)
        ''dc.HeaderCell.Style.ForeColor = Color.FromArgb(0, 0, 0)
        'dc.HeaderCell.Style.Font = New Font("Times New Roman", 9, FontStyle.Bold)

        'Dim strikethrough_style As New DataGridViewCellStyle
        'strikethrough_style.Font = New Font("Times New Roman", 9, FontStyle.Bold)
        ''strikethrough_style.BackColor = Color.Red


        If Button6.Text = "Advanced Mode" Then
            Me.DGV1.Size = New Size(420, 311)
            Me.DGV1.Columns(8).Visible = True
            'Me.DGV1.Columns(4).Visible = True
            Me.DGV1.Columns(10).Visible = True
            'For Each row As DataGridViewRow In DGV1.Rows
            '    For i = 0 To DGV1.Columns.Count - 1
            '        row.Cells(i).Style = strikethrough_style
            '    Next
            'Next
            Button6.Text = "Simple Mode"
        ElseIf Button6.Text = "Simple Mode" Then
            Me.DGV1.Size = New Size(263, 311)
            Me.DGV1.Columns(8).Visible = False
            'Me.DGV1.Columns(4).Visible = false
            Me.DGV1.Columns(10).Visible = False

            Button6.Text = "Advanced Mode"

        End If


    End Sub

    Private Sub Button4_MouseWheel(sender As Object, e As MouseEventArgs) Handles Button4.MouseWheel
        'If e.Delta > 0 And Label31.Text = "Verifikasi" And fungsi = 2 Then
        '    Call scrool_up()
        'End If

        'If e.Delta < 0 And Label31.Text = "Verifikasi" And fungsi = 2 Then
        '    Call scrool_dw()
        'End If

    End Sub

    Private Sub GroupBox6_MouseWheel(sender As Object, e As MouseEventArgs)
        'Call srool()
        'If e.Delta > 0 And Label31.Text = "Verifikasi" And fungsi = 2 Then
        '    Call scrool_up()
        'End If

        'If e.Delta < 0 And Label31.Text = "Verifikasi" And fungsi = 2 Then
        '    Call scrool_dw()
        'End If

    End Sub

    Private Sub scrool_up()
        If Form1.tbisntrumen.Text = "Voltmeter" Or Form1.tbisntrumen.Text = "Double Voltmeter" Then

            AGauge1.Value = Val(DGV1.Rows(i - 1).Cells(1).Value)
            tbvolt.Text = Format(tbvolt.Text + komane1, "000.00")
            'tbvolt.Text = va.ToString("000.00")
            DGV1.Rows(i - 1).Cells(4).Value = tbvolt.Text
            DGV1.Rows(i - 1).Cells(5).Value = Val(DGV1.Rows(i - 1).Cells(1).Value) - Val(DGV1.Rows(i - 1).Cells(4).Value)

            DGV1.Rows(i - 1).Cells(8).Value = Format(Val(tbvolt.Text) * 1.733333, "000.00")
            Dim err As Double = Math.Abs(DGV1.Rows(i - 1).Cells(0).Value - DGV1.Rows(i - 1).Cells(8).Value)
            DGV1.Rows(i - 1).Cells(10).Value = Format(err, "000.00")
            'Call cek_error()
            Call cek_error1()
            Call isi_data()

        End If

        If Form1.tbisntrumen.Text = "Ammeter" Then
            DGV1.Rows(i - 1).Cells(4).Value = Format(Val(tbcurrent.Text), "0.0000")
            tbcurrent.Text = Format(Val(tbcurrent.Text) + komanea, "0.0000")
            DGV1.Rows(i - 1).Cells(8).Value = Format(Val(tbcurrent.Text) * (Val(Label57.Text)), "0.0000")
            Dim err As Double = Math.Abs(DGV1.Rows(i - 1).Cells(0).Value - DGV1.Rows(i - 1).Cells(8).Value)
            DGV1.Rows(i - 1).Cells(10).Value = Format(err, "0.0000")

            Call cek_error1()
            Call isi_data()


        End If

    End Sub

    Private Sub scrool_dw()
        'error1 = Val(vers2.Text) * (1.5 / 100)
        If Form1.tbisntrumen.Text = "Voltmeter" Or Form1.tbisntrumen.Text = "Double Voltmeter" Then
            AGauge1.Value = Val(DGV1.Rows(i - 1).Cells(1).Value)

            If Val(DGV1.Rows(i - 1).Cells(0).Value) = 0 And Val(DGV1.Rows(i - 1).Cells(10).Value) = 0 Then
                Format(tbvolt.Text - 0, "000.00")
            Else
                tbvolt.Text = Format(tbvolt.Text - komane1, "000.00")
            End If

            'tbvolt.Text = va.ToString("000.00")
            DGV1.Rows(i - 1).Cells(4).Value = tbvolt.Text
            DGV1.Rows(i - 1).Cells(5).Value = Val(DGV1.Rows(i - 1).Cells(0).Value) - Val(DGV1.Rows(i - 1).Cells(4).Value)
            DGV1.Rows(i - 1).Cells(8).Value = Format(Val(tbvolt.Text) * 1.733333, "000.00")

            Dim err As Double = Math.Abs(DGV1.Rows(i - 1).Cells(0).Value - DGV1.Rows(i - 1).Cells(8).Value)
            DGV1.Rows(i - 1).Cells(10).Value = Format(err, "000.00")
            'Call cek_error()
            Call cek_error1()
            Call isi_data()

        End If

        If Form1.tbisntrumen.Text = "Ammeter" Then

            tbcurrent.Text = Format(Val(tbcurrent.Text) - komanea, "0.0000")
            DGV1.Rows(i - 1).Cells(4).Value = Format(Val(tbcurrent.Text), "0.0000")
            DGV1.Rows(i - 1).Cells(8).Value = Format(Val(tbcurrent.Text) * (Val(Label57.Text)), "0.0000")
            Dim err As Double = Math.Abs(DGV1.Rows(i - 1).Cells(0).Value - DGV1.Rows(i - 1).Cells(8).Value)
            DGV1.Rows(i - 1).Cells(10).Value = Format(err, "0.0000")

            Call cek_error1()
            Call isi_data()

        End If
    End Sub

    Private Sub Button4_MouseClick(sender As Object, e As MouseEventArgs) Handles Button4.MouseClick

    End Sub

    Private Sub GroupBox6_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub DGV1_NewRowNeeded(sender As Object, e As DataGridViewRowEventArgs) Handles DGV1.NewRowNeeded

    End Sub

    Private Sub DGV1_Resize(sender As Object, e As EventArgs) Handles DGV1.Resize

    End Sub

    Private Sub DGV1_DragLeave(sender As Object, e As EventArgs) Handles DGV1.DragLeave

    End Sub
End Class