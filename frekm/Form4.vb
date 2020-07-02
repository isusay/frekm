
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.IO.Ports
Imports System.Threading

Public Class Form4

    Dim fun As Integer = 0
    Dim klike As String = "yo"
    Dim x As Double = 0
    Dim fungsi As Integer = 0
    Dim skab As Integer = 0
    Dim ver As Boolean = False
    Dim a As String = "ok"
    Dim input As Boolean = False
    Dim ct As Integer = 0
    Dim res_ver As String = "yo"
    Dim kal As Integer = 0
    Dim it_cal As Integer = 0
    Dim res_cal As Boolean = False
    Dim ver1 As Integer = 0


    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim scr As Screen = Screen.FromPoint(Cursor.Position)
        Me.Location = New Point(scr.WorkingArea.Right - Me.Width, scr.WorkingArea.Bottom - Me.Height)
        'AGauge1.Visible = False
        DGV1.Visible = False
        Label19.Text = ""
        x = 0
        Label30.Text = "0"
        fun = 0

    End Sub



    Private Sub AGauge2_MouseClick(sender As Object, e As MouseEventArgs) Handles AGauge2.MouseClick


        Select Case e.Button
            Case MouseButtons.Left
                klike = "kiri  "
                fun = fun + 1
                kal = kal + 1
                ver1 = ver1 + 1
                Label2.Text = fun & " " & fungsi '& " " & Me.DGV1.SelectedCells(0).Value.ToString
                Label26.Text = it_cal  'iterasi kalibrasi
                If fun = 1 And res_cal = False Then
                    Label2.Text = fun & " " & fungsi & " " & kal
                    GroupBox2.Text = "Kalibrasi"
                    tbstatus.Text = "Kalibrasi"
                    fungsi = 2
                    kal = 0
                    Call tampil_kalibrasi1()
                    'Call kalibrasi2()
                End If



                If fun = 1 And res_cal = True Then
                    Label2.Text = fun & " " & fungsi & " " & ver1
                    GroupBox2.Text = "Verifikasi"
                    tbstatus.Text = "Verifikasi"
                    fungsi = 3
                    ver1 = 0
                    it_cal = 0
                    ct = 1
                    Label19.Text = ""
                    'DGV1.Visible = False
                    'DGSK1.Visible = True
                    'fun = 0
                    Panel1.Visible = False
                    DGV1.Visible = True
                    DGV2.Visible = True

                    Call tampil_verifikasi()
                    'Call catetan()
                    'Call verifikasi()

                    'Call show_needle()

                    'For c As Integer = 0 To 10
                    '    If c = fun Then
                    '        Label24.Text = c
                    '    End If
                    'Next



                End If

                'Call calibrasi()

                If fungsi = 2 And res_cal = False Then
                    Label2.Text = fun & " " & fungsi & " " & kal

                    Call kalibrasi2()

                End If

                'call verifikasi
                If fungsi = 3 And res_cal = True Then
                    Label2.Text = fun & " " & fungsi & " " & kal
                    Call verifikasi()

                End If



            Case MouseButtons.Right
                'tbstatus.Text = "kanan"
                'fungsi = fungsi + 1
                'Label2.Text = fun & " " & fungsi
                'If fungsi = 2 And tb_cal_result.Text = "" Then
                '    Label4.Text = "Kalibrasi"
                '    GroupBox2.Text = "Kalibrasi"
                '    tbstatus.Text = "Kalibrasi"
                '    DGV1.Visible = False
                '    DGSK1.Visible = True
                '    fun = 0
                '    Call tampil_kalibrasi()

                'End If
                'If fungsi = 3 And tb_cal_result.Text = "Pass" Then
                '    Label4.Text = "Verivikasi"
                '    tbstatus.Text = "Verivikasi"
                '    AGauge1.Visible = True
                '    DGV1.Visible = True
                '    DGSK1.Visible = False
                '    fun = 0
                '    GroupBox2.Text = "Real Reference"
                '    Call tampil_verifikasi()

                'End If
                'If fungsi = 4 Then
                '    Label4.Text = ""
                '    fungsi = 0
                'End If

            Case MouseButtons.Middle
                tbstatus.Text = "tengah"
                ct = ct + 1
                If ct = 3 Then ct = 0

            Case Else
                tbstatus.Text = "liane"
        End Select


    End Sub

    Private Sub AGauge2_MouseWheel(sender As Object, e As MouseEventArgs) Handles AGauge2.MouseWheel
        If e.Delta > 0 Then
            If fungsi = 1 And ct = 1 Then
                x = x + 1
                Label3.Text = x
                AGauge2.Value = x
            End If

            If fungsi = 1 And ct = 2 Then
                x = x + 10
                Label3.Text = x
                AGauge2.Value = x
            End If

            If fungsi = 3 And ct = 1 Then
                x = x + 1
                Label3.Text = x
                AGauge1.Value = AGauge1.Value + 1
                nil_jarum.Text = AGauge1.Value
                Dim result1 As Integer = Val(nil_jarum.Text) - AGauge2.Value
                Label23.Text = result1
                If result1 > Val(Err1.Text) Then
                    Label22.Text = "Fail"
                    DGV1.Rows(Val(Label30.Text)).Cells(0).Style.ForeColor = Color.Red
                    DGV1.Rows(Val(Label30.Text)).Cells(1).Style.ForeColor = Color.Red
                    DGV1.Rows(Val(Label30.Text)).Cells(1).Value = Label22.Text
                    DGV1.Rows(Val(Label30.Text)).Cells(2).Value = "0"
                    'tb_ver_result.Text = "Fail.."

                End If
                If result1 < Val(Err1.Text) Then
                    Label22.Text = "Pass"
                    DGV1.Rows(Val(Label30.Text)).Cells(0).Style.ForeColor = Color.Lime
                    DGV1.Rows(Val(Label30.Text)).Cells(1).Style.ForeColor = Color.Lime
                    DGV1.Rows(Val(Label30.Text)).Cells(1).Value = Label22.Text
                    DGV1.Rows(Val(Label30.Text)).Cells(2).Value = "1"
                    'tb_ver_result.Text = "Pass.."

                End If



            End If

        End If

        If e.Delta < 0 Then
            If fungsi = 1 And ct = 1 Then
                x = x - 1
                Label3.Text = x
                AGauge2.Value = x
            End If

            If fungsi = 1 And ct = 2 Then
                x = x - 10
                Label3.Text = x
                AGauge2.Value = x
            End If

            If fungsi = 3 And ct = 1 Then
                x = x - 1
                Label3.Text = x
                AGauge1.Value = AGauge1.Value - 1
                nil_jarum.Text = AGauge1.Value
                Dim result1 As Integer = AGauge2.Value - Val(nil_jarum.Text)
                Label23.Text = result1
                If result1 > Val(Err1.Text) Then
                    Label22.Text = "Fail"
                    DGV1.Rows(Val(Label30.Text)).Cells(0).Style.ForeColor = Color.Red
                    DGV1.Rows(Val(Label30.Text)).Cells(1).Style.ForeColor = Color.Red
                    DGV1.Rows(Val(Label30.Text)).Cells(1).Value = Label22.Text
                    DGV1.Rows(Val(Label30.Text)).Cells(2).Value = "0"
                End If
                If result1 < Val(Err1.Text) Then
                    Label22.Text = "Pass"
                    DGV1.Rows(Val(Label30.Text)).Cells(0).Style.ForeColor = Color.Lime
                    DGV1.Rows(Val(Label30.Text)).Cells(1).Style.ForeColor = Color.Lime
                    DGV1.Rows(Val(Label30.Text)).Cells(1).Value = Label22.Text
                    DGV1.Rows(Val(Label30.Text)).Cells(2).Value = "1"
                End If
            End If
        End If



    End Sub

    Private Sub tampil_kalibrasi1()
        Try
            Call koneksine()
            Dim str1 As String

            str1 = "SELECT * FROM tb_calibrasi WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            cmd = New MySqlCommand(str1, conn)
            rd = cmd.ExecuteReader
            rd.Read()

            If rd.HasRows Then

                'Call cek_jml_calibrasi()
                Label9.Text = rd.Item("ordering_code") & rd.Item("max_cal")
                Label8.Text = rd.Item("max_cal")
                Label13.Text = rd.Item("cal_1") * 2
                AGauge2.MaxValue = rd.Item("cal_1")

                For x5 As Integer = 0 To Val(Form1.Label18.Text)
                    DGSK1.Rows(x5).Cells(0).Value = rd.Item("cal_" & x5 + 1 & "")
                    DGSK2.Rows(x5).Cells(0).Value = rd.Item("sdcal" & x5 + 1 & "")
                Next

                conn.Close()

            End If


            AGauge2.Value = 0
            Label12.Text = AGauge2.MaxValue

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub tampil_kalibrasi()
        Try
            Call koneksine()
            Dim str1 As String

            str1 = "SELECT * FROM tb_calibrasi WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            cmd = New MySqlCommand(str1, conn)
            rd = cmd.ExecuteReader
            rd.Read()

            If rd.HasRows Then

                'Call cek_jml_calibrasi()
                Label9.Text = rd.Item("ordering_code") & rd.Item("max_cal")
                Label8.Text = rd.Item("max_cal")
                Label13.Text = rd.Item("cal_1") * 2
                AGauge2.MaxValue = rd.Item("cal_1")

                For x5 As Integer = 0 To Val(Form1.Label18.Text)
                    DGSK1.Rows(x5).Cells(0).Value = rd.Item("cal_" & x5 + 1 & "")
                    DGSK2.Rows(x5).Cells(0).Value = rd.Item("sdcal" & x5 + 1 & "")
                Next

                conn.Close()

            End If

            For x1 As Integer = 0 To Val(Form1.Label18.Text)
                DGSK1.Rows(x1).Selected = False
            Next

            AGauge2.Value = 0
            Label12.Text = AGauge2.MaxValue

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub tampil_verifikasi()
        Panel1.Visible = False
        Try
            Call koneksine()
            Dim str1 As String

            str1 = "SELECT * FROM tb_verifikasi WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            cmd = New MySqlCommand(str1, conn)
            rd = cmd.ExecuteReader
            rd.Read()

            If rd.HasRows Then
                Label11.Text = rd.Item("iterasi")
                tol1.Text = rd.Item("btol1")
                tol2.Text = rd.Item("btol2")
                For x5 As Integer = 0 To (Val(Form1.Label20.Text) * 2) + 1
                    If x5 > 5 Then
                        DGV1.Rows(x5).Cells(0).Value = rd.Item("ver" & x5 - 6 & "")
                    Else
                        DGV1.Rows(x5).Cells(0).Value = rd.Item("ver" & x5 & "")
                        DGV2.Rows(x5).Cells(0).Value = rd.Item("sdver" & x5 & "")
                    End If


                Next


                conn.Close()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try



    End Sub

    Private Sub pesan1()
        Dim b As Object = MessageBox.Show("Pass tekan Yes, Fail tekan No, Cancel untuk mengulang", "Hasil Titik Kalibrasi Titik : " & Label18.Text & "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        If b = Windows.Forms.DialogResult.Yes Then
            Label7.Text = "Pass"
        ElseIf b = Windows.Forms.DialogResult.No Then
            Label7.Text = "Fail"
            'tb_cal_result.Text = "fail"
            'fungsi = 0
        Else

            Label7.Text = "Cancel"
            'If skab > Val(Label8.Text) Then
            '    tb_cal_result.Text = "Fail"
            '    fungsi = 0
            '    'MessageBox.Show("Anda sudah 3X melakukan Kalibrasi dengan hasil Fail", "Gagal Kalibrasi")
            'End If
            'fun = 1
            'Call tampil_kalibrasi()
            ''Call tampil_verifikasi()

        End If
    End Sub

    Private Sub pesan()
        Dim b As Object = MessageBox.Show("Pass tekan Yes, Fail tekan No, Cancel untuk mengulang", "Hasil Titik Kalibrasi", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        If b = Windows.Forms.DialogResult.Yes Then
            Label7.Text = "Pass"
        ElseIf b = Windows.Forms.DialogResult.No Then
            Label7.Text = "Fail"
            tb_cal_result.Text = "fail"
            fungsi = 0
        Else

            Label7.Text = "Cancel"
            If skab > Val(Label8.Text) Then
                tb_cal_result.Text = "Fail"
                fungsi = 0
                'MessageBox.Show("Anda sudah 3X melakukan Kalibrasi dengan hasil Fail", "Gagal Kalibrasi")
            End If
            fun = 1
            Call tampil_kalibrasi()
            'Call tampil_verifikasi()


        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call koneksine()
        Try
            'Dim str As String
            'str = "UPDATE tb_calibrasi SET sdcal1 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"

        cmd = New MySqlCommand(a, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Insert Data Berhasil Dilakukan")
            Call tampil_kalibrasi()
            Call tampil_verifikasi()

        Catch ex As Exception
            MessageBox.Show("Insert data gagal dilakukan.")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        input = True
        DGV2.Visible = True
        DGSK2.Visible = True
    End Sub

    Private Sub DGSK2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGSK2.CellMouseClick

        Label14.Text = DGSK2.Rows(e.RowIndex).Cells(0).Value
        If input = True And tbstatus.Text = "Kalibrasi" Then
            For i As Integer = 0 To Val(Form1.Label18.Text)
                a = "UPDATE tb_calibrasi SET sdcal" & e.RowIndex & " = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            Next
            'If e.RowIndex = 0 Then a = "UPDATE tb_calibrasi SET sdcal1 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            'If e.RowIndex = 1 Then a = "UPDATE tb_calibrasi SET sdcal2 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            'If e.RowIndex = 2 Then a = "UPDATE tb_calibrasi SET sdcal3 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            'If e.RowIndex = 3 Then a = "UPDATE tb_calibrasi SET sdcal4 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            Label15.Text = e.RowIndex
            Label16.Text = a
        End If
        If input = True And tbstatus.Text = "Verifikasi" Then
            'If e.RowIndex = 0 Then a = "UPDATE tb_verifikasi SET sdver0 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            'If e.RowIndex = 1 Then a = "UPDATE tb_verifikasi SET sdver1 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            'If e.RowIndex = 2 Then a = "UPDATE tb_verifikasi SET sdver2 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            'If e.RowIndex = 3 Then a = "UPDATE tb_verifikasi SET sdver3 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            'If e.RowIndex = 4 Then a = "UPDATE tb_verifikasi SET sdver4 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            'If e.RowIndex = 5 Then a = "UPDATE tb_verifikasi SET sdver5 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
            'Label15.Text = e.RowIndex
            'Label16.Text = a
        End If

    End Sub

    Private Sub cek_jml_calibrasi()
        Dim a1 As Integer = 0
        Dim a2 As Integer = 0
        Dim a3 As Integer = 0
        Dim a4 As Integer = 0
        Dim a5 As Integer = 0
        Dim a6 As Integer = 0
        Dim a7 As Integer = 0
        'Label11.Text = rd.Item("iterasi")
        If rd.Item("cal_1") > 0 Then a1 = 1
        If rd.Item("cal_2") > 0 Then a2 = 1
        If rd.Item("cal_3") > 0 Then a3 = 1
        If rd.Item("cal_4") > 0 Then a4 = 1
        If rd.Item("cal_5") > 0 Then a5 = 1
        If rd.Item("cal_6") > 0 Then a6 = 1
        a7 = a1 + a2 + a3 + a4 + a5 + a6
        jml_titk.Text = a7
        'DGV1.Rows.Add((a7 * 2) + 1)
        'DGSK1.Rows.Add(a7 + 1)
        'DGSK1.Rows.Add(a7 + 1)



    End Sub

    Private Sub dis_dgsk1()
        For x1 As Integer = 0 To Val(Form1.Label18.Text) + 1
            DGSK1.Rows(x1).Selected = False
        Next

    End Sub

    Private Sub show_needle()
        If fungsi = 2 Then
            Label18.Text = Me.DGSK1.SelectedCells.Item(0).RowIndex.ToString()
            AGauge2.Value = Me.DGSK2.SelectedCells(0).Value.ToString
            AGauge1.Value = Me.DGSK2.SelectedCells(0).Value.ToString
            skab = skab + 1
            Label10.Text = skab
        End If
        If fungsi = 3 Then
            Label18.Text = Me.DGV2.SelectedCells.Item(0).RowIndex.ToString()
            AGauge2.Value = Me.DGV2.SelectedCells(0).Value.ToString
            AGauge1.Value = Me.DGV2.SelectedCells(0).Value.ToString
        End If
    End Sub

    Private Sub clear_dbgrid()
        For x As Integer = 0 To Val(Label18.Text)
            DGV1.Rows(x).Selected = False
            DGSK1.Rows(x).Selected = False
            DGSK2.Rows(x).Selected = False
        Next
    End Sub

    Private Sub cek_calibrasi()
        Dim b As Object = MessageBox.Show("Pass tekan Yes, Fail tekan No, Cancel untuk mengulang", "Hasil Titik Kalibrasi", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        If b = Windows.Forms.DialogResult.Yes Then
            'tb_cal_result.Text = "Pass"
            Dim r As Integer = Me.DGSK1.SelectedCells.Item(0).RowIndex.ToString()
            DGSK1.Rows(r).Cells(0).Style.ForeColor = Color.Lime
            Dim d(Val(Label18.Text)) As String
            d(r) = 1

            For Each element As String In d(Val(Label18.Text))
                Label19.Text += element + ","
            Next

        ElseIf b = Windows.Forms.DialogResult.No Then
            Label7.Text = "Fail"
            Dim r As Integer = Me.DGSK1.SelectedCells.Item(0).RowIndex.ToString()
            DGSK1.Rows(r).Cells(0).Style.ForeColor = Color.Red
            Dim d(Val(Label18.Text)) As String
            d(r) = 0

            For Each element As String In d(Val(Label18.Text))
                Label19.Text += element + " "
            Next

        Else
            Label7.Text = "Cancel"
            If skab > Val(Label8.Text) Then
                'tb_cal_result.Text = "Fail"
                fungsi = 0
            End If

        End If
    End Sub

    Private Sub calibrasi()
        If fun = 2 And fungsi = 2 Then
            DGV1.Rows(0).Selected = True
            DGSK1.Rows(0).Selected = True
            DGSK2.Rows(0).Selected = True
            Call show_needle()
            'Call kirim_data()
            Call cek_calibrasi()

        End If

        If fun = 3 And fungsi = 2 Then
            'DGSK1.Rows(0).Selected = False
            DGV1.Rows(1).Selected = True
            DGSK1.Rows(1).Selected = True
            DGSK2.Rows(1).Selected = True
            Call show_needle()
            Call cek_calibrasi()
            'DGSK1.Rows(1).Cells(0).Style.ForeColor = Color.Lime
            'rowindex

        End If

        If fun = 4 And fungsi = 2 Then
            'DGSK1.Rows(0).Selected = False
            DGV1.Rows(2).Selected = True
            DGSK1.Rows(2).Selected = True
            DGSK2.Rows(2).Selected = True
            Call show_needle()
            Call cek_calibrasi()
            'rowindex

        End If

        If fun = 5 And fungsi = 2 Then
            'DGSK1.Rows(0).Selected = False
            DGV1.Rows(3).Selected = True
            DGSK1.Rows(3).Selected = True
            DGSK2.Rows(3).Selected = True
            Call show_needle()
            Call cek_calibrasi()
            'rowindex

            If Val(Label18.Text) = Val(Form1.Label18.Text) Then
                'MsgBox("oke")
                fun = 0
                Call clear_dbgrid()
                Dim parts() As String = Label19.Text.Split(New Char() {" "c})
                Dim prod As Integer = 0
                For i As Integer = 0 To Val(Label18.Text)
                    prod += parts(i)
                    Label20.Text = prod
                Next
                If Val(Label20.Text) = Val(Label18.Text) + 1 Then
                    tb_cal_result.ForeColor = Color.Lime
                    tb_cal_result.Text = "Pass"
                    fungsi = 0
                    fun = 0
                Else
                    tb_cal_result.ForeColor = Color.Red
                    tb_cal_result.Text = "Fail"
                    fungsi = 0
                    fun = 0
                End If
            End If
        End If

        If fun = 6 And fungsi = 2 Then
            'DGSK1.Rows(0).Selected = False
            DGV1.Rows(4).Selected = True
            DGSK1.Rows(4).Selected = True
            DGSK2.Rows(4).Selected = True
            Call show_needle()
            Call cek_calibrasi()
            'rowindex

            If Val(Label18.Text) = Val(Form1.Label18.Text) Then
                'MsgBox("oke")
                fun = 0
                Call clear_dbgrid()
                Dim parts() As String = Label19.Text.Split(New Char() {" "c})
                Dim prod As Integer = 0
                For i As Integer = 0 To Val(Label18.Text)
                    prod += parts(i)
                    Label20.Text = prod
                Next
                If Val(Label20.Text) = Val(Label18.Text) + 1 Then
                    tb_cal_result.ForeColor = Color.Lime
                    tb_cal_result.Text = "Pass"
                    fungsi = 0
                    fun = 0
                Else
                    tb_cal_result.ForeColor = Color.Red
                    tb_cal_result.Text = "Fail"
                    fungsi = 0
                    fun = 0
                End If
            End If

        End If

        If fun = 6 And fungsi = 2 Then
            'DGSK1.Rows(0).Selected = False
            DGV1.Rows(5).Selected = True
            DGSK1.Rows(5).Selected = True
            DGSK2.Rows(5).Selected = True
            Call show_needle()
            Call cek_calibrasi()
            'rowindex

            If Val(Label18.Text) = Val(Form1.Label18.Text) Then
                'MsgBox("oke")
                fun = 0
                Call clear_dbgrid()
                Dim parts() As String = Label19.Text.Split(New Char() {" "c})
                Dim prod As Integer = 0
                For i As Integer = 0 To Val(Label18.Text)
                    prod += parts(i)
                    Label20.Text = prod
                Next
                If Val(Label20.Text) = Val(Label18.Text) + 1 Then
                    tb_cal_result.ForeColor = Color.Lime
                    tb_cal_result.Text = "Pass"
                    fungsi = 0
                    fun = 0
                Else
                    tb_cal_result.ForeColor = Color.Red
                    tb_cal_result.Text = "Fail"
                    fungsi = 0
                    fun = 0
                End If
            End If

        End If
    End Sub

    Private Sub DGV2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGV2.CellMouseClick

        For i As Integer = 0 To Val(Form1.Label20.Text)
            a = "UPDATE tb_verifikasi SET sdver" & e.RowIndex & " = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
        Next
        'If e.RowIndex = 0 Then a = "UPDATE tb_verifikasi SET sdver" & e.RowIndex & " = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
        'If e.RowIndex = 1 Then a = "UPDATE tb_verifikasi SET sdver'" & e.RowIndex & "' = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
        'If e.RowIndex = 2 Then a = "UPDATE tb_verifikasi SET sdver2 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
        'If e.RowIndex = 3 Then a = "UPDATE tb_verifikasi SET sdver3 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
        'If e.RowIndex = 4 Then a = "UPDATE tb_verifikasi SET sdver4 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
        'If e.RowIndex = 5 Then a = "UPDATE tb_verifikasi SET sdver5 = '" & Label3.Text & "' WHERE ordering_code = '" & Form1.TextBox2.Text & "'"
        Label15.Text = e.RowIndex
        Label16.Text = a
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim Str As String = Label19.Text
        Dim strarr() As String
        strarr = Str.Split(" ")
        Dim a As Integer = 0
        'MsgBox(strarr(0))
        For s As Integer = 0 To strarr.Length - 1
            a = a + Val(strarr(s))
        Next
        If a >= 4 Then
            tb_cal_result.ForeColor = Color.Lime
            tb_cal_result.Text = "Pass"
        End If
        If a < 4 Then
            tb_cal_result.ForeColor = Color.Red
            tb_cal_result.Text = "Fail"
        End If


    End Sub

    Private Sub cek_result_kalibrasi()
        Dim Str As String = Label19.Text
        Dim strarr() As String
        strarr = Str.Split(" ")
        Dim a As Integer = 0
        'MsgBox(strarr(0))
        For s As Integer = 0 To strarr.Length - 1
            a = a + Val(strarr(s))
        Next
        If a >= 4 Then
            tb_cal_result.ForeColor = Color.Lime
            tb_cal_result.Text = "Pass"
            'fungsi = 3
            res_cal = True
        End If
        If a < 4 Then
            tb_cal_result.ForeColor = Color.Red
            tb_cal_result.Text = "Fail"
        End If
    End Sub

    Private Sub verifikasi()
        For scal1 = 0 To (Val(Form1.Label20.Text) * Val(Label11.Text)) + 1
            If fun = scal1 + 3 Then
                Label30.Text = scal1
                it_cal = it_cal + 1
                Label26.Text = it_cal
                kal = 0
                DGV1.ClearSelection()
                DGV2.ClearSelection()

                DGV1.Rows(scal1).Selected = True

                'DGV2.Rows(it_cal).Selected = True
                If scal1 < 6 Then
                    DGV2.Rows(scal1).Selected = True
                End If
                If scal1 > 5 Then
                    DGV2.Rows(scal1 - 6).Selected = True
                End If

                Call show_needle()
                'cek verifikasi

                Dim iSales As Integer = 0
                For index As Integer = 0 To DGV1.RowCount - 1
                    iSales += Convert.ToInt32(DGV1.Rows(index).Cells(2).Value)
                Next
                Label35.Text = iSales
            End If
        Next

        If Val(Label35.Text) = 11 And fun > 15 Then
            tb_ver_result.ForeColor = Color.Lime
            tb_ver_result.Text = "Pass"
            ct = 0
        End If

        If Val(Label35.Text) < 11 And fun > 15 Then
            tb_ver_result.ForeColor = Color.Red
            tb_ver_result.Text = "Fail"

        End If
    End Sub

    Private Sub kalibrasi2()
        For scal = 0 To (Val(Form1.Label18.Text) * Val(Label8.Text)) + 2
            If fun = scal + 3 Then
                Label30.Text = scal
                kal = 0
                DGSK1.ClearSelection()
                DGSK2.ClearSelection()
                DGSK1.Rows(it_cal).Selected = True
                DGSK2.Rows(it_cal).Selected = True

                Call show_needle()
                'Call kirim_data()
                Call pesan1()
                Select Case Label7.Text
                    Case "Pass"
                        DGSK1.Rows(it_cal).Cells(1).Style.ForeColor = Color.Lime
                        DGSK1.Rows(it_cal).Cells(1).Value = "Pass"
                        'cek kalibrasi pass
                        Dim r As Integer = Me.DGSK1.SelectedCells.Item(0).RowIndex.ToString()
                        DGSK1.Rows(r).Cells(0).Style.ForeColor = Color.Lime
                        Dim d(Val(Label18.Text)) As String
                        d(r) = 1

                        For Each element As String In d(Val(Label18.Text))
                            Label19.Text += element + " "
                        Next

                        'cek titik terakhir
                        If Label18.Text = Form1.Label18.Text Then
                            Dim hasil As String = "Pass"
                            Dim result As DialogResult = MessageBox.Show("Hasil kalibrasi " & hasil & " Ulang Kalibrasi ?", "Kalibrasi ", MessageBoxButtons.YesNo)

                            If (result = DialogResult.Yes) Then
                                'MsgBox(" baleni ",, "kalibrasi")
                                Label19.Text = ""
                            Else
                                MsgBox(" Kalibrasi Selesai ",, "kalibrasi")
                                fungsi = 0
                                Call cek_result_kalibrasi()
                                fun = 0

                            End If
                        End If

                    Case "Fail"
                        DGSK1.Rows(it_cal).Cells(1).Style.ForeColor = Color.Red
                        DGSK1.Rows(it_cal).Cells(1).Value = "Fail"
                        'cek kalibrasi fail
                        Dim r As Integer = Me.DGSK1.SelectedCells.Item(0).RowIndex.ToString()
                        DGSK1.Rows(r).Cells(0).Style.ForeColor = Color.Red
                        Dim d(Val(Label18.Text)) As String
                        d(r) = 0

                        For Each element As String In d(Val(Label18.Text))
                            Label19.Text += element + " "
                        Next

                    Case "Cancel"
                        fungsi = 2
                        res_cal = False
                        fun = 0
                        it_cal = 0
                        DGSK1.ClearSelection()

                End Select

                'Call cek_calibrasi()

                it_cal = it_cal + 1

                If it_cal = Val(Form1.Label26.Text) + 1 Then
                    it_cal = 0
                    'GroupBox2.Text = "Verifikasi"
                End If

                If scal = (Val(Form1.Label18.Text) * Val(Label8.Text)) + 2 Then
                    MsgBox("Sudah 3x Kalibrasi..",, "Maksimum Kalibrasi !")
                    fungsi = 0
                    tb_cal_result.ForeColor = Color.Red
                    tb_cal_result.Text = "Fail"
                End If

            End If


        Next
    End Sub

    Private Sub catetan()
        For cv As Integer = 0 To (Val(Form1.Label20.Text) * 2) + 1
            Label24.Text = cv
            If fun = cv + 1 And tb_cal_result.Text = "Pass" Then

                If cv < 6 Then
                    DGV1.Rows(cv).Selected = True
                    DGV2.Rows(cv).Selected = True
                    Call show_needle()
                    Dim toleransi1 As Double = Val(tol1.Text)
                    Dim error1 As Double = AGauge1.Value * (toleransi1 / 100)
                    Err1.Text = error1
                    'cek error


                End If

                If cv >= 6 Then
                    DGV2.Rows(cv - 6).Selected = True
                    DGV1.Rows(cv).Selected = True
                    Call show_needle()
                    Dim toleransi1 As Double = Val(tol1.Text)
                    Dim error1 As Double = AGauge1.Value * (toleransi1 / 100)
                    Err1.Text = error1

                End If

                If fun = (Val(Form1.Label20.Text) * 2) + 2 Then
                    fun = 0
                End If
            Else
                If cv < 6 Then
                    DGV1.Rows(cv).Selected = False
                    DGV2.Rows(cv).Selected = False
                End If
                If cv >= 6 Then
                    DGV1.Rows(cv).Selected = False
                    'DGV2.Rows(cv).Selected = False
                End If

            End If
        Next

        'If fungsi = 3 And tb_cal_result.Text = "Pass" Then
        '    Label4.Text = "Verivikasi"
        '    tbstatus.Text = "Verivikasi"
        '    AGauge1.Visible = True
        '    DGV1.Visible = True
        '    DGSK1.Visible = False
        '    fun = 0
        '    GroupBox2.Text = "Real Reference"
        '    Call tampil_verifikasi()

        'End If
    End Sub
End Class