Imports MaterialSkin
Imports MySql.Data.MySqlClient

Public Class LoginMangaKissa
    Public dbConn As New MySqlConnection
    Private Sub LoginMangaKissa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim SkinManager As MaterialSkinManager = MaterialSkinManager.Instance
        SkinManager.AddFormToManage(Me)
        SkinManager.Theme = MaterialSkinManager.Themes.DARK
        SkinManager.ColorScheme = New ColorScheme(Primary.Brown800, Primary.Brown900, Primary.Brown500, Accent.Amber700, TextShade.WHITE)

        If dbConn.State = ConnectionState.Closed Then
            dbConn.ConnectionString = "SERVER = localhost; USERID = root; PASSWORD=; DATABASE = mangakissadb"
            dbConn.Open()
        End If
    End Sub

    Private Sub LoginMKissaBtn_Click(sender As Object, e As EventArgs) Handles LoginMKissaBtn.Click
        Dim sqlQuery As String = "SELECT * FROM employeetable WHERE (empUsername = '" & UserEmailTxt.Text & "' AND empPassword = '" & PassTxt.Text & "') OR (empEmail = '" & UserEmailTxt.Text & "' AND empPassword = '" & PassTxt.Text & "')"
        Dim sqlAdapter As New MySqlDataAdapter
        Dim sqlCommand As New MySqlCommand
        Dim accountRes As New DataTable


        With sqlCommand
            .CommandText = sqlQuery
            .Connection = dbConn
        End With

        With sqlAdapter
            .SelectCommand = sqlCommand
            .Fill(accountRes)
        End With


        Dim sqlQuery1 As String = "SELECT empID FROM employeetable WHERE empUserName = '" & UserEmailTxt.Text & "' OR empEmail = '" & UserEmailTxt.Text & "'"
        Dim sqlAdapter1 As New MySqlDataAdapter
        Dim sqlCommand1 As New MySqlCommand
        Dim accountExist As New DataTable


        With sqlCommand1
            .CommandText = sqlQuery1
            .Connection = dbConn
        End With

        With sqlAdapter1
            .SelectCommand = sqlCommand1
            .Fill(accountExist)
        End With

        dbConn.Dispose()

        If accountRes.Rows().Count <> 0 Then
            MessageBox.Show("Login Succesful!", "Welcome to MangaKissa")
            Form1.WelcomeMessage.Text = "Welcome " & accountRes.Rows(0)("empUsername")
            Form1.PositionLbl.Text = accountRes.Rows(0)("empPosition")
            Form1.EmpNameLbl.Text = accountRes.Rows(0)("empName")
            Form1.MaterialTabControlCafe.SelectedIndex = 0

            Me.Hide()
            Form1.Show()

            UserEmailTxt.Text = ""
            PassTxt.Text = ""
            ShowPass.Checked = False
        Else
            If accountExist.Rows().Count <> 0 Then
                MessageBox.Show("Wrong username, email or password details.", "Login Unsuccesful")
            Else
                MessageBox.Show("No such account exist.", "Login Unsuccesful")
            End If
        End If
    End Sub

    Private Sub ShowPass_CheckedChanged(sender As Object, e As EventArgs) Handles ShowPass.CheckedChanged
        If ShowPass.Checked = True Then
            PassTxt.PasswordChar = ""
        ElseIf ShowPass.CheckState = CheckState.Unchecked Then
            PassTxt.PasswordChar = "•"
        End If
    End Sub
End Class