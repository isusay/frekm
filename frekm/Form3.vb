Option Strict Off
Option Explicit On
Imports System.Drawing.Printing
Imports MySql.Data.MySqlClient


Public Class Form3

    Inherits System.Windows.Forms.Form
    'Declaration of Private Subroutine
    Private Declare Sub about Lib "tsclib.dll" ()
    Private Declare Sub openport Lib "tsclib.dll" (ByVal PrinterName As String)
    Private Declare Sub closeport Lib "tsclib.dll" ()
    Private Declare Sub sendcommand Lib "tsclib.dll" (ByVal command_Renamed As String)
    Private Declare Sub setup Lib "tsclib.dll" (ByVal LabelWidth As String, ByVal LabelHeight As String, ByVal Speed As String, ByVal Density As String, ByVal Sensor As String, ByVal Vertical As String, ByVal Offset As String)
    Private Declare Sub downloadpcx Lib "tsclib.dll" (ByVal Filename As String, ByVal ImageName As String)
    Private Declare Sub barcode Lib "tsclib.dll" (ByVal X As String, ByVal Y As String, ByVal CodeType As String, ByVal Height_Renamed As String, ByVal Readable As String, ByVal rotation As String, ByVal Narrow As String, ByVal Wide As String, ByVal Code As String)
    Private Declare Sub printerfont Lib "tsclib.dll" (ByVal X As String, ByVal Y As String, ByVal FontName As String, ByVal rotation As String, ByVal Xmul As String, ByVal Ymul As String, ByVal Content As String)
    Private Declare Sub clearbuffer Lib "tsclib.dll" ()
    Private Declare Sub printlabel Lib "tsclib.dll" (ByVal NumberOfSet As String, ByVal NumberOfCopy As String)
    Private Declare Sub formfeed Lib "tsclib.dll" ()
    Private Declare Sub nobackfeed Lib "tsclib.dll" ()
    Private Declare Sub windowsfont Lib "tsclib.dll" (ByVal X As Short, ByVal Y As Short, ByVal fontheight_Renamed As Short, ByVal rotation As Short, ByVal fontstyle As Short, ByVal fontunderline As Short, ByVal FaceName As String, ByVal TextContent As String)
    Private Declare Sub windowsfontUnicode Lib "tsclib.dll" (ByVal X As Short, ByVal Y As Short, ByVal fontheight_Renamed As Short, ByVal rotation As Short, ByVal fontstyle As Short, ByVal fontunderline As Short, ByVal FaceName As String, ByVal TextContent As Byte())
    Private Declare Sub sendBinaryData Lib "tsclib.dll" (ByVal TextContent As Byte(), ByVal length As Integer)
    Private Declare Function usbportqueryprinter Lib "tsclib.dll" () As Byte


    Dim WithEvents mPrintDocument As New PrintDocument
    Dim mPrintBitMap As Bitmap
    Dim tahun As Date = Now
    Dim bulan As Date = Now
    Dim tabggal As Date
    Dim counter As Integer = 0
    Dim gambare As String
    Dim A1 As String
    Dim A2 As String
    Dim A3 As String
    Dim A4 As String
    Dim A5 As String
    Dim B1 As String
    Dim B2 As String
    Dim B3 As String
    Dim B4 As String
    Dim B5 As String
    Dim C1 As String
    Dim C2 As String
    Dim C3 As String
    Dim C4 As String
    Dim C5 As String
    Dim D1 As String
    Dim D2 As String
    Dim D3 As String
    Dim D4 As String
    Dim D5 As String
    Dim E1 As String
    Dim E2 As String
    Dim E3 As String
    Dim E4 As String
    Dim E5 As String
    Dim printer_label As String = Form1.Label43.Text
    Dim printer_report As String = Form1.Label44.Text

    Private Sub print_label()

        Dim WT1 As String = Label1.Text
        Dim WT2 As String = Label2.Text
        Dim WT3 As String = Label3.Text
        Dim WT4 As String = Label4.Text
        Dim WT5 As String = Label5.Text
        Dim WT6 As String = Label6.Text
        Dim WT7 As String = Label7.Text
        Dim WT8 As String = Label8.Text
        Dim WT9 As String = "GAE"
        Dim status As Byte = 0
        Dim result_unicode() As Byte = System.Text.Encoding.GetEncoding(1200).GetBytes("TEST UNICODE")
        Dim result_utf8() As Byte = System.Text.Encoding.UTF8.GetBytes("TEXT 40,620,""ARIAL.TTF"",0,12,12,""utf8 test Wörter auf Deutsch""")

        'Call about()
        status = usbportqueryprinter() '0 = idle, 1 = head open, 16 = pause, following <ESC>!? command of TSPL manual
        Call openport(printer_label)
        Call setup("58", "35", "4.0", "10", "0", "584", "0")
        Call clearbuffer()
        Call sendcommand("DIRECTION 1")
        Call sendcommand("1")
        'Call downloadpcx(Application.StartupPath & "\AEG.pcx", "AEG.pcx")

        Call printerfont("20", "25", "0", "0", "15", "15", WT1)     'v
        Call sendcommand("BAR 10,70,400,1")
        Call printerfont("20", "80", "0", "0", "10", "10", WT2)     's/n
        Call sendcommand("BAR 10,108,400,1")
        Call printerfont("20", "113", "0", "0", "15", "15", WT3)    'am/vm
        Call printerfont("225", "120", "0", "0", "9", "9", WT4)     'ct
        Call sendcommand("BAR 10,152,400,1")
        Call printerfont("20", "159", "0", "0", "9", "9", WT5)      'Class
        Call printerfont("180", "159", "0", "0", "9", "9", WT6)     'ac
        Call printerfont("343", "159", "0", "0", "9", "9", WT7)     'frek hz
        Call sendcommand("BAR 10,185,400,1")
        Call printerfont("20", "193", "0", "0", "10", "10", WT8)    'size
        Call sendcommand("PUTBMP 335,190,""AEG.BMP""")              'logo AEG

        Call sendcommand("BAR 10,226,400,1")
        Call printlabel("1", "1")
        Call closeport()
    End Sub

    Private Sub print_wiring()
        Dim lokasi As Uri = New Uri("http://" & server & "/senzo/assets/picstyle/wiring/")

        Dim WT2 As String = "S/N : " & Form2.TextBox20.Text
        Dim wiring As String = Form1.lbsticker.Text & ".BMP"

        Dim status As Byte = 0
        Dim result_unicode() As Byte = System.Text.Encoding.GetEncoding(1200).GetBytes("TEST UNICODE")
        Dim result_utf8() As Byte = System.Text.Encoding.UTF8.GetBytes("TEXT 40,620,""ARIAL.TTF"",0,12,12,""utf8 test Wörter auf Deutsch""")

        'Call about()
        status = usbportqueryprinter() '0 = idle, 1 = head open, 16 = pause, following <ESC>!? command of TSPL manual
        Call openport(printer_label)
        Call setup("58", "35", "4.0", "10", "0", "4", "0")

        Call clearbuffer()
        Call sendcommand("DIRECTION 1")
        Call sendcommand("1")
        Call printerfont("15", "25", "0", "0", "10", "10", WT2)  'Serial
        Call sendcommand("PUTBMP 335,25,""AEG.BMP""")           'Logo aeg
        'AC-Voltmeter-Direct
        If Form1.lbsticker.Text = "AC-Voltmeter-Direct" Then Call sendcommand("PUTBMP 5,80,""A1.BMP""")        'Wiring

        'AC-Voltmeter-PT
        If Form1.lbsticker.Text = "AC-Voltmeter-PT" Then Call sendcommand("PUTBMP 5,80,""A2.BMP"",1,80")

        'AC-Ammeter-Direct
        If Form1.lbsticker.Text = "AC-Ammeter-Direct" Then Call sendcommand("PUTBMP 5,80,""4.BMP""")

        'AC-Ammeter-PT
        If Form1.lbsticker.Text = "AC-Ammeter-PT" Then Call sendcommand("PUTBMP 5,80,""A3.BMP""")

        'AC-Double Voltmeter-Direct
        If Form1.lbsticker.Text = "AC-Double Voltmeter-Direct" Then Call sendcommand("PUTBMP 5,80,""A4.BMP""")

        'AC-Double Voltmeter-PT
        If Form1.lbsticker.Text = "AC-Double Voltmeter-PT" Then Call sendcommand("PUTBMP 5,80,""A5.BMP""")

        'AC-ZeroVoltmeter-Direct
        If Form1.lbsticker.Text = "AC-ZeroVoltmeter-Direct" Then Call sendcommand("PUTBMP 5,80,""Zero2.BMP""")

        Call printlabel("1", "1")
        Call closeport()
    End Sub

    Private Sub m_PrintDocument_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles mPrintDocument.PrintPage
        ' Draw the image centered.
        Dim lWidth As Integer = e.MarginBounds.X + (e.MarginBounds.Width - mPrintBitMap.Width) \ 2
        Dim lHeight As Integer = e.MarginBounds.Y + (e.MarginBounds.Height - mPrintBitMap.Height) \ 2
        e.Graphics.DrawImage(mPrintBitMap, lWidth, lHeight)

        ' There's only one page.
        e.HasMorePages = False
    End Sub

    Private Sub posisi()
        Me.Visible = True
        Dim x As Integer
        Dim y As Integer
        x = Screen.PrimaryScreen.WorkingArea.Width
        y = Screen.PrimaryScreen.WorkingArea.Height - Me.Height

        Me.Location = New Point(x, y)

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Form2.Label31.Text = "Printing" Then
            'Call posisi()

            Timer1.Interval = 1000


            'Dim lokasi As String = My.Application.Info.DirectoryPath
            'Dim folder As String = lokasi & "\picstyle\"
            'Dim folder As String = "C:\Users\solo\Documents\Visual Studio 2017\project\Senzo\WindowsApp3\picstyle\"
            'Form2.Show()

            PictureBox1.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData("http://" & server & "/senzo/assets/picstyle/wiring/" & Form1.lbsticker.Text & ".jpg")))

            'Dim folder As String = "C:\Users\solo\Documents\Visual Studio 2017\project\Senzo\WindowsApp3\picstyle\wiring\"
            'Label2.Text = Format(Now, "yyMMdd")
            'Dim folder As String = "C:\Users\solo\Documents\Visual Studio 2017\project\Senzo\WindowsApp3\picstyle\wiring\"
            'PictureBox1.Image = Image.FromFile(folder & "GAE_AC_Voltmeter_Direct_W" & ".BMP")

            'Label2.Text = "S/N :" & " " & Form1.Label28.Text



            Dim str1 As String = Form1.tbprdgroup.Text
            Dim str2 As String() = str1.Split(" "c)
            Label4.TextAlign = ContentAlignment.MiddleRight
            If str2(0) = "Double" Then
                Label1.Text = str1
            ElseIf str2(0) = "Zero" Then
                Label1.Text = str2(0) & " " & str2(1)
            Else
                Label1.Text = str2(0)
            End If


            Label2.Text = "S/N :   " & Form2.TextBox20.Text
            Label3.Text = Form1.tbprdvarian.Text
            Label4.Text = Form1.tbtype.Text
            Label5.Text = "Class  : " & Form1.tbclass.Text
            Label6.Text = Form1.tbsignal.Text
            Label7.Text = Form1.tbfreq.Text
            Label8.Text = Form1.tbsize.Text
            Label9.Text = Form1.Label28.Text

            Timer1.Start()

            'CheckBox1.Checked = True
        End If

        If Form2.Label31.Text = "rePrinting" Then



            'Dim lokasi As String = My.Application.Info.DirectoryPath
            'Dim folder As String = lokasi & "\picstyle\"
            'Dim folder As String = "C:\Users\solo\Documents\Visual Studio 2017\project\Senzo\WindowsApp3\picstyle\"
            'Form2.Show()

            PictureBox1.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData("http://" & server & "/senzo/assets/picstyle/wiring/" & Form1.lbsticker.Text & ".jpg")))

            'Dim folder As String = "C:\Users\solo\Documents\Visual Studio 2017\project\Senzo\WindowsApp3\picstyle\wiring\"
            'Label2.Text = Format(Now, "yyMMdd")
            'Dim folder As String = "C:\Users\solo\Documents\Visual Studio 2017\project\Senzo\WindowsApp3\picstyle\wiring\"
            'PictureBox1.Image = Image.FromFile(folder & "GAE_AC_Voltmeter_Direct_W" & ".BMP")

            'Label2.Text = "S/N :" & " " & Form1.Label28.Text



            Dim str1 As String = Form1.tbprdgroup.Text
            Dim str2 As String() = str1.Split(" "c)
            Label4.TextAlign = ContentAlignment.MiddleRight

            If str2(0) = "Double" Then
                Label1.Text = str1
            Else
                Label1.Text = str2(0)
            End If

            Label2.Text = "S/N :   " & Form2.TextBox20.Text
            Label3.Text = Form1.tbprdvarian.Text
            Label4.Text = Form1.tbtype.Text
            Label5.Text = "Class  : " & Form1.tbclass.Text
            Label6.Text = Form1.tbsignal.Text
            Label7.Text = Form1.tbfreq.Text
            Label8.Text = Form1.tbsize.Text
            Label9.Text = Form1.Label28.Text



            'CheckBox1.Checked = True
        End If

        If tbsn.Text = Form2.TextBox20.Text Then
            GroupBox2.Visible = True

            'Call tampil_data_report()
            Call isi_datagrid()
            Call tampil_data_report()

            Button15.Visible = True
        End If


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim folder As String = "C:\Users\solo\Documents\Visual Studio 2017\project\Senzo\WindowsApp3\picstyle\wiring\"
        PictureBox1.Image = Image.FromFile(folder & "GAE_AC_Voltmeter_Direct_W" & ".BMP")

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click, Label8.Click, Label7.Click, Label6.Click, Label5.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Copy the form's image into a bitmap.
        mPrintBitMap = New Bitmap(Me.Width, Me.Width)
        Dim lRect As System.Drawing.Rectangle
        lRect.Width = Me.Width
        lRect.Height = Me.Width
        Me.DrawToBitmap(mPrintBitMap, lRect)

        ' Make a PrintDocument and print.
        mPrintDocument = New PrintDocument
        mPrintDocument.Print()
    End Sub

    Dim WithEvents printDoc As New Printing.PrintDocument()
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call print_label()

        'printDoc.Print()

    End Sub

    Private Sub PrintImage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles printDoc.PrintPage
        e.Graphics.DrawImage(PictureBox1.Image, e.MarginBounds.Left, e.MarginBounds.Top)
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim newMargins As System.Drawing.Printing.Margins
        newMargins = New System.Drawing.Printing.Margins(0.2, 0.2, 0.2, 0.2)
        PrintDocument1.DefaultPageSettings.Margins = newMargins
        e.Graphics.DrawImage(PictureBox1.Image, 0, 0)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'PrintDocument1.Print()
        Label9.Text = Form1.Label28.Text
        PictureBox1.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData("http://" & server & "/senzo/assets/picstyle/wiring/" & Form1.lbsticker.Text & ".jpg")))

    End Sub

    Private BMP As Bitmap

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        'Call Drawline()

        'Dim pd As New PrintDocument
        'Dim pdialog As New PrintDialog
        'Dim ppd As New PrintPreviewDialog
        'BMP = New Bitmap(GroupBox1.Width, GroupBox1.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        'GroupBox1.DrawToBitmap(BMP, New Rectangle(0, 0, GroupBox1.Width, GroupBox1.Height))
        'AddHandler pd.PrintPage, (Sub(s, args)
        '                              args.Graphics.DrawImage(BMP, 0, 0)
        '                              args.HasMorePages = False
        '                          End Sub)
        ''choose a printer
        'pdialog.ShowDialog(Me)
        'pd.PrinterSettings.PrinterName = pdialog.PrinterSettings.PrinterName

        'If pd.PrinterSettings.CanDuplex.ToString Then
        '    pd.PrinterSettings.Duplex = Duplex.Vertical
        'End If

        ''Preview the document
        'ppd.Document = pd
        'ppd.ShowDialog(Me)

        'pd.Print()      'actually print data



        Timer1.Start()





    End Sub
    Public Sub Drawline()
        Dim blackpen As New Pen(Color.Black, 1)

        Dim g As Graphics = Me.GroupBox1.CreateGraphics()
        g.DrawLine(blackpen, 26, 46, 320, 46)
        g.DrawLine(blackpen, 26, 71, 320, 71)
        g.DrawLine(blackpen, 26, 101, 320, 101)
        g.DrawLine(blackpen, 26, 126, 320, 126)
        g.DrawLine(blackpen, 26, 151, 320, 151)
        blackpen.Dispose()


    End Sub

    Dim x1 As Integer = 101

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim blackpen As New Pen(Color.Black, 1)
        Dim g As Graphics = Me.GroupBox1.CreateGraphics()
        x1 = x1 + 5
        g.DrawLine(blackpen, 26, x1, 320, x1)

        blackpen.Dispose()
        Label11.Text = x1
    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        'Dim blackpen As New Pen(Color.Black, 1)
        'Dim g As Graphics = Me.GroupBox1.CreateGraphics()
        'x1 = x1 - 5
        'g.DrawLine(blackpen, 26, x1, 320, x1)

        'blackpen.Dispose()
        'Label11.Text = x1
        Call print_wiring()


    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Call Drawline()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        counter = counter + 1
        Label12.Text = counter
        If counter = 1 Then Call print_label()
        If counter = 5 Then Call print_wiring()
        If counter = 7 Then
            counter = 0
            Timer1.Stop()

        End If
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub tampil_data()
        Try
            Call koneksine()
            Dim str1 As String

            'str1 = "SELECT * FROM test_e1 WHERE ordering_code = '" & TextBox1.Text & "'"
            str1 = "SELECT * FROM testvolt2 INNER JOIN test_e1 ON test_e1.ordering_code = testvolt2.kode 
                    WHERE testvolt2.serialno = '" & TextBox1.Text & "' GROUP BY testvolt2.serialno"
            cmd = New MySqlCommand(str1, conn)
            rd = cmd.ExecuteReader
            rd.Read()

            If rd.HasRows Then

                Dim str3 As String = rd.Item("product_group")
                Dim str4 As String() = str3.Split(" "c)
                If str4(0) = "Double" Then
                    Label25.Text = str3
                Else
                    Label25.Text = str4(0)
                End If
                'Label25.Text = str4(0)
                Label23.Text = "S/N :   " & rd.Item("serialno")
                Label24.Text = rd.Item("product_varian")
                Label22.Text = rd.Item("type")
                Label21.Text = "Class  : " & rd.Item("class")
                Label19.Text = rd.Item("signal")
                Label18.Text = rd.Item("freq")
                Label20.Text = rd.Item("size")
                Label17.Text = rd.Item("serialno")
                Dim gb As String = rd.Item("sticker_wiring")
                PictureBox6.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData("http://" & server & "/senzo/assets/picstyle/wiring/" & gb & ".jpg")))
                gambare = rd.Item("sticker_wiring")

                conn.Close()


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        GroupBox3.Visible = True
        Call tampil_data()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Call print_label2()

    End Sub

    Private Sub print_label2()

        Dim WT1 As String = Label25.Text
        Dim WT2 As String = Label23.Text
        Dim WT3 As String = Label24.Text
        Dim WT4 As String = Label22.Text
        Dim WT5 As String = Label21.Text
        Dim WT6 As String = Label19.Text
        Dim WT7 As String = Label18.Text
        Dim WT8 As String = Label20.Text
        Dim WT9 As String = "GAE"
        Dim status As Byte = 0
        Dim result_unicode() As Byte = System.Text.Encoding.GetEncoding(1200).GetBytes("TEST UNICODE")
        Dim result_utf8() As Byte = System.Text.Encoding.UTF8.GetBytes("TEXT 40,620,""ARIAL.TTF"",0,12,12,""utf8 test Wörter auf Deutsch""")

        'Call about()
        status = usbportqueryprinter() '0 = idle, 1 = head open, 16 = pause, following <ESC>!? command of TSPL manual
        Call openport(printer_label)
        Call setup("58", "35", "4.0", "10", "0", "584", "0")
        Call clearbuffer()
        Call sendcommand("DIRECTION 1")
        Call sendcommand("1")
        'Call downloadpcx(Application.StartupPath & "\AEG.pcx", "AEG.pcx")

        Call printerfont("20", "25", "0", "0", "15", "15", WT1)     'v
        Call sendcommand("BAR 10,70,400,1")
        Call printerfont("20", "80", "0", "0", "10", "10", WT2)     's/n
        Call sendcommand("BAR 10,108,400,1")
        Call printerfont("20", "113", "0", "0", "15", "15", WT3)    'am/vm
        Call printerfont("225", "120", "0", "0", "9", "9", WT4)     'ct
        Call sendcommand("BAR 10,152,400,1")
        Call printerfont("20", "159", "0", "0", "9", "9", WT5)      'Class
        Call printerfont("180", "159", "0", "0", "9", "9", WT6)     'ac
        Call printerfont("343", "159", "0", "0", "9", "9", WT7)     'frek hz
        Call sendcommand("BAR 10,185,400,1")
        Call printerfont("20", "193", "0", "0", "10", "10", WT8)    'size
        Call sendcommand("PUTBMP 335,190,""AEG.BMP""")              'logo AEG

        Call sendcommand("BAR 10,226,400,1")
        Call printlabel("1", "1")
        Call closeport()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Call print_wiring2()

    End Sub

    Private Sub print_wiring2()
        Dim lokasi As Uri = New Uri("http://" & server & "/senzo/assets/picstyle/wiring/")

        Dim WT2 As String = "S/N : " & Label23.Text
        Dim gbnya As String = gambare
        Dim wiring As String = gbnya & ".BMP"

        Dim status As Byte = 0
        Dim result_unicode() As Byte = System.Text.Encoding.GetEncoding(1200).GetBytes("TEST UNICODE")
        Dim result_utf8() As Byte = System.Text.Encoding.UTF8.GetBytes("TEXT 40,620,""ARIAL.TTF"",0,12,12,""utf8 test Wörter auf Deutsch""")

        'Call about()
        status = usbportqueryprinter() '0 = idle, 1 = head open, 16 = pause, following <ESC>!? command of TSPL manual
        Call openport(printer_label)
        Call setup("58", "35", "4.0", "10", "0", "4", "0")

        Call clearbuffer()
        Call sendcommand("DIRECTION 1")
        Call sendcommand("1")
        Call printerfont("15", "25", "0", "0", "10", "10", WT2)  'Serial
        Call sendcommand("PUTBMP 335,25,""AEG.BMP""")           'Logo aeg
        'AC-Voltmeter-Direct
        If gbnya = "AC-Voltmeter-Direct" Then Call sendcommand("PUTBMP 5,80,""A1.BMP""")        'Wiring

        'AC-Voltmeter-PT
        If gbnya = "AC-Voltmeter-PT" Then Call sendcommand("PUTBMP 5,80,""A2.BMP"",1,80")

        'AC-Ammeter-PT
        If gbnya = "AC-Ammeter-PT" Then Call sendcommand("PUTBMP 5,80,""A3.BMP""")

        'AC-Ammeter-Direct
        If gbnya = "AC-Ammeter-Direct" Then Call sendcommand("PUTBMP 5,80,""4.BMP""")

        'AC-Double Voltmeter-Direct
        If gbnya = "AC-Double Voltmeter-Direct" Then Call sendcommand("PUTBMP 5,80,""A4.BMP""")

        'AC-Double Voltmeter-PT
        If gbnya = "AC-Double Voltmeter-PT" Then Call sendcommand("PUTBMP 5,80,""A5.BMP""")

        'AC-ZeroVoltmeter-Direct
        If gbnya = "AC-ZeroVoltmeter-Direct" Then Call sendcommand("PUTBMP 5,80,""Zero2.BMP""")

        Call printlabel("1", "1")
        Call closeport()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Call print_label2()
        Call print_wiring2()

    End Sub

    Private Sub Form3_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        conn.Close()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        GroupBox2.Visible = True

        'Call tampil_data_report()
        Call isi_datagrid()
        Call tampil_data_report()

        Button15.Visible = True

    End Sub

    Private Sub isi_datagrid()

        Try
            Call koneksine()

            Dim str2 As String
            Dim sqlDataAdapter As New MySqlDataAdapter
            Dim dt As New DataTable

            str2 = "SELECT testvolt2.test AS Verifikasi, FORMAT(ROUND(measure - bt_tol_rm,2),2) as LSL,
                    FORMAT(ROUND(measure + bt_tol_rm,2),2) as USL,
                    FORMAT(testvolt2.measure,2) As Measure,testvolt2.status As Status
                    FROM test_e1 INNER JOIN testvolt2 ON testvolt2.kode = test_e1.ordering_code
                    WHERE testvolt2.serialno ='" & tbsn.Text & "' "

            Dim adapter As New MySqlDataAdapter(str2, conn)

            adapter.Fill(dt)

            DataGridView1.DataSource = dt
            DataGridView1.Columns(0).Width = 50
            DataGridView1.Columns(1).Width = 45
            DataGridView1.Columns(2).Width = 45
            DataGridView1.Columns(3).Width = 50
            DataGridView1.Columns(4).Width = 40
            A1 = DataGridView1.Rows(0).Cells(0).Value.ToString
            B1 = DataGridView1.Rows(0).Cells(1).Value.ToString
            C1 = DataGridView1.Rows(0).Cells(2).Value.ToString
            D1 = DataGridView1.Rows(0).Cells(3).Value.ToString
            E1 = DataGridView1.Rows(0).Cells(4).Value.ToString

            A2 = DataGridView1.Rows(1).Cells(0).Value.ToString
            B2 = DataGridView1.Rows(1).Cells(1).Value.ToString
            C2 = DataGridView1.Rows(1).Cells(2).Value.ToString
            D2 = DataGridView1.Rows(1).Cells(3).Value.ToString
            E2 = DataGridView1.Rows(1).Cells(4).Value.ToString

            A3 = DataGridView1.Rows(2).Cells(0).Value.ToString
            B3 = DataGridView1.Rows(2).Cells(1).Value.ToString
            C3 = DataGridView1.Rows(2).Cells(2).Value.ToString
            D3 = DataGridView1.Rows(2).Cells(3).Value.ToString
            E3 = DataGridView1.Rows(2).Cells(4).Value.ToString

            A4 = DataGridView1.Rows(3).Cells(0).Value.ToString
            B4 = DataGridView1.Rows(3).Cells(1).Value.ToString
            C4 = DataGridView1.Rows(3).Cells(2).Value.ToString
            D4 = DataGridView1.Rows(3).Cells(3).Value.ToString
            E4 = DataGridView1.Rows(3).Cells(4).Value.ToString

            A5 = DataGridView1.Rows(4).Cells(0).Value.ToString
            B5 = DataGridView1.Rows(4).Cells(1).Value.ToString
            C5 = DataGridView1.Rows(4).Cells(2).Value.ToString
            D5 = DataGridView1.Rows(4).Cells(3).Value.ToString
            E5 = DataGridView1.Rows(4).Cells(4).Value.ToString

            conn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Private Sub tampil_data_report()
        Try
            Call koneksine()

            Dim str1 As String
            Dim str2 As String


            str2 = "SELECT test_e1.code_group,test_e1.instrument,testvolt2.serialno,test_e1.variant,
                    test_e1.signal,test_e1.type,test_e1.class,testvolt2.no_mesin,testvolt2.operator,test_e1.size,
                    test_e1.freq, FORMAT(ROUND(measure - bt_tol_rm,2),2) as LSL,
                                  FORMAT(ROUND(measure + bt_tol_rm,2),2) as USL,
                    testvolt2.test,testvolt2.measure,testvolt2.error,testvolt2.status
                    FROM test_e1 INNER JOIN testvolt2 ON testvolt2.kode = test_e1.ordering_code
                    WHERE testvolt2.serialno ='" & tbsn.Text & "' "

            'str1 = "SELECT * FROM test_e1 WHERE ordering_code = '" & TextBox1.Text & "'"
            str1 = "SELECT * FROM testvolt2 INNER JOIN test_e1 ON test_e1.ordering_code = testvolt2.kode 
                    WHERE testvolt2.serialno = '" & TextBox1.Text & "' GROUP BY testvolt2.serialno"
            cmd = New MySqlCommand(str2, conn)
            rd = cmd.ExecuteReader
            rd.Read()

            If rd.HasRows Then
                lbinstrumen.Text = rd.Item("instrument")
                lbsn.Text = "S/N :  " & rd.Item("serialno")
                lbvarian.Text = rd.Item("variant")
                lbsignal.Text = rd.Item("signal")
                lbtype.Text = rd.Item("type")
                lbclass.Text = "Class :  " & rd.Item("class")
                lbmesin.Text = rd.Item("no_mesin")
                lboperator.Text = rd.Item("operator")
                lbsize.Text = rd.Item("size")
                lbfrek.Text = rd.Item("freq")




                conn.Close()


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Call print_report()
    End Sub

    Private Sub print_report()

        Dim WT1 As String = lbinstrumen.Text
        Dim WT2 As String = lbsn.Text

        Dim WT3 As String = lbvarian.Text
        Dim WT4 As String = lbsignal.Text
        Dim WT5 As String = lbtype.Text
        Dim WT6 As String = lbclass.Text

        Dim WT7 As String = "MC No. " & lbmesin.Text
        Dim WT8 As String = lboperator.Text
        Dim WT9 As String = lbsize.Text
        Dim WT10 As String = lbfrek.Text
        Dim WT11 As String = Format(Now, "dd-MM-yyyy")

        Dim WT12 As String = "Verifikasi"
        Dim WT13 As String = "USL"
        Dim WT14 As String = "LSL"
        Dim WT15 As String = "Measure"
        Dim WT16 As String = "Status"

        Dim status As Byte = 0
        Dim result_unicode() As Byte = System.Text.Encoding.GetEncoding(1200).GetBytes("TEST UNICODE")
        Dim result_utf8() As Byte = System.Text.Encoding.UTF8.GetBytes("TEXT 40,620,""ARIAL.TTF"",0,12,12,""utf8 test Wörter auf Deutsch""")

        'Call about()
        status = usbportqueryprinter() '0 = idle, 1 = head open, 16 = pause, following <ESC>!? command of TSPL manual
        Call openport(printer_report)
        Call setup("90", "75", "4.0", "10", "0", "584", "0")
        Call clearbuffer()
        Call sendcommand("DIRECTION 1")
        Call sendcommand("1")
        'Call downloadpcx(Application.StartupPath & "\AEG.pcx", "AEG.pcx")

        Call printerfont("60", "30", "0", "0", "8", "8", WT1)     'instrument
        Call printerfont("240", "30", "0", "0", "8", "8", WT2)     's/n
        Call sendcommand("PUTBMP 430,30,""AEG.BMP""")              'logo AEG

        Call printerfont("60", "60", "0", "0", "8", "8", WT3)    'varian
        Call printerfont("190", "60", "0", "0", "8", "8", WT4)     'signal
        Call printerfont("270", "60", "0", "0", "8", "8", WT5)      'Type
        Call printerfont("440", "60", "0", "0", "8", "8", WT6)     'class

        Call printerfont("60", "90", "0", "0", "8", "8", WT7)     'no mesin
        Call printerfont("170", "90", "0", "0", "8", "8", WT8)    'operator
        Call printerfont("240", "90", "0", "0", "8", "8", WT9)    'size
        Call printerfont("340", "90", "0", "0", "8", "8", WT10)    'frek
        Call printerfont("440", "90", "0", "0", "8", "8", WT11)    'tanggal

        Call printerfont("60", "120", "0", "0", "8", "8", WT12)    'Verifikasi
        Call printerfont("180", "120", "0", "0", "8", "8", WT13)    'USL
        Call printerfont("270", "120", "0", "0", "8", "8", WT14)    'LSL
        Call printerfont("350", "120", "0", "0", "8", "8", WT15)    'Measure
        Call printerfont("460", "120", "0", "0", "8", "8", WT16)    'status

        If A1 <= 100 Then
            Call printerfont("80", "150", "0", "0", "8", "8", A1)    'A1
            Call printerfont("260", "150", "0", "0", "8", "8", C1)    'C1
        Else
            Call printerfont("70", "150", "0", "0", "8", "8", A1)    'A1
            Call printerfont("250", "150", "0", "0", "8", "8", C1)    'C1
        End If

        Call printerfont("170", "150", "0", "0", "8", "8", B1)    'B1

        Call printerfont("360", "150", "0", "0", "8", "8", D1)    'D1
        Call printerfont("470", "150", "0", "0", "8", "8", E1)    'E1

        Call printerfont("80", "180", "0", "0", "8", "8", A2)    'A2
        Call printerfont("170", "180", "0", "0", "8", "8", B2)    'B2
        Call printerfont("260", "180", "0", "0", "8", "8", C2)    'C2
        Call printerfont("360", "180", "0", "0", "8", "8", D2)    'D2
        Call printerfont("470", "180", "0", "0", "8", "8", E2)    'E2

        Call printerfont("80", "210", "0", "0", "8", "8", A3)    'A3
        Call printerfont("170", "210", "0", "0", "8", "8", B3)    'B3
        Call printerfont("260", "210", "0", "0", "8", "8", C3)    'C3
        Call printerfont("360", "210", "0", "0", "8", "8", D3)    'D3
        Call printerfont("470", "210", "0", "0", "8", "8", E3)    'E3

        If A4 < 10 Then
            Call printerfont("90", "240", "0", "0", "8", "8", A4)    'A4
            Call printerfont("180", "240", "0", "0", "8", "8", B4)    'B4
            Call printerfont("270", "240", "0", "0", "8", "8", C4)    'C4
            Call printerfont("370", "240", "0", "0", "8", "8", D4)    'D4
            Call printerfont("470", "240", "0", "0", "8", "8", E4)    'E4
        Else
            Call printerfont("80", "240", "0", "0", "8", "8", A4)    'A4
            Call printerfont("170", "240", "0", "0", "8", "8", B4)    'B4
            Call printerfont("260", "240", "0", "0", "8", "8", C4)    'C4
            Call printerfont("360", "240", "0", "0", "8", "8", D4)    'D4
            Call printerfont("470", "240", "0", "0", "8", "8", E4)    'E4

        End If

        If D4 < 10 Then
            Call printerfont("90", "240", "0", "0", "8", "8", A4)    'A4
            Call printerfont("180", "240", "0", "0", "8", "8", B4)    'B4
            Call printerfont("270", "240", "0", "0", "8", "8", C4)    'C4
            Call printerfont("370", "240", "0", "0", "8", "8", D4)    'D4
            Call printerfont("470", "240", "0", "0", "8", "8", E4)    'E4
        Else
            Call printerfont("80", "240", "0", "0", "8", "8", A4)    'A4
            Call printerfont("170", "240", "0", "0", "8", "8", B4)    'B4
            Call printerfont("260", "240", "0", "0", "8", "8", C4)    'C4
            Call printerfont("360", "240", "0", "0", "8", "8", D4)    'D4
            Call printerfont("470", "240", "0", "0", "8", "8", E4)    'E4

        End If



        If A5 < 10 Then
            Call printerfont("90", "270", "0", "0", "8", "8", A5)    'A5
            Call printerfont("180", "270", "0", "0", "8", "8", B5)    'B5
            Call printerfont("270", "270", "0", "0", "8", "8", C5)    'C5
            Call printerfont("370", "270", "0", "0", "8", "8", D5)    'D5
            Call printerfont("470", "270", "0", "0", "8", "8", E5)    'E5
        Else
            Call printerfont("80", "270", "0", "0", "8", "8", A5)    'A5
            Call printerfont("170", "270", "0", "0", "8", "8", B5)    'B5
            Call printerfont("260", "270", "0", "0", "8", "8", C5)    'C5
            Call printerfont("360", "270", "0", "0", "8", "8", D5)    'D5
            Call printerfont("470", "270", "0", "0", "8", "8", E5)    'E5
        End If

        Call printlabel("1", "1")
        Call closeport()
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub
End Class