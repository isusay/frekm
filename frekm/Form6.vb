Imports System.IO
Imports System.IO.Ports
Imports System.Threading

Public Class Form6
    Dim myPort As Array
    Dim myPort2 As Array

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        myPort = IO.Ports.SerialPort.GetPortNames()
        ComboBox5.Items.Add(9600)
        ComboBox5.Items.Add(19200)
        ComboBox5.Items.Add(38400)
        ComboBox5.Items.Add(57600)
        ComboBox5.Items.Add(115200)
        ComboBox2.Items.Add(9600)
        ComboBox2.Items.Add(19200)
        ComboBox2.Items.Add(38400)
        ComboBox2.Items.Add(57600)
        ComboBox2.Items.Add(115200)
        For i = 0 To UBound(myPort)
            ComboBox4.Items.Add(myPort(i))
            ComboBox1.Items.Add(myPort(i))
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        'Form1.TextBox3.Text = ComboBox1.Items(0)
        'Form1.TextBox4.Text = ComboBox2.Items(0)
        SerialPort1.Open()
        SerialPort1.Write("ab")
        SerialPort1.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        'Form1.sr_ard.PortName = ComboBox4.Text
        'Form1.sr_ard.BaudRate = ComboBox5.Text
        'Form1.sr_ard.Open()

        'Form1.Button5.Text = "Connected"
        'Form1.Label23.Text = "Connected"
        'Form1.Label25.Text = "Connected"
        Call tulis_setting()



    End Sub

    Private Sub tulis_setting()
        Dim lokasi As String = My.Application.Info.DirectoryPath
        Dim FILE_NAME2 As String = lokasi & "\setport.txt"
        Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME2)
        objWriter2.Write(ComboBox1.Text & "," & ComboBox2.Text & "," & ComboBox4.Text & "," & ComboBox5.Text)
        objWriter2.Close()

    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)

    End Sub
End Class