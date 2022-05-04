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

    Private Sub LoginBtn_Click(sender As Object, e As EventArgs) Handles LoginBtn.Click
        Dim sqlQuery As String = "SELECT employeeID FROM mangalibrary WHERE (empUsername = '" & UserEmailTxt.Text & "' AND empPassword = '" & PassTxt.Text & "') OR (empEmail = '" & UserEmailTxt.Text & "' AND empPassword = '" & PassTxt.Text & "')"
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


        Dim sqlQuery1 As String = "SELECT empID FROM mangalibrary WHERE empUserName = '" & UserEmailTxt.Text & "' OR empUserName = '" & UserEmailTxt.Text & "'"
        Dim sqlAdapter1 As New MySqlDataAdapter
        Dim sqlCommand1 As New MySqlCommand
        Dim accountExist As New DataTable


        With sqlCommand
            .CommandText = sqlQuery
            .Connection = dbConn
        End With

        With sqlAdapter
            .SelectCommand = sqlCommand
            .Fill(accountExist)
        End With

        dbConn.Dispose()

        If accountRes.Rows().Count = 0 Then
            MessageBox.Show("Login Succesful!", "Welcome to MangaKissa")
            Me.Hide()
            Form1.Show()
        Else
            If accountExist.Rows().Count = 0 Then
                MessageBox.Show("Wrong username, email or password details.", "Login Unsuccesful")
            Else
                MessageBox.Show("No such account exist.", "Login Unsuccesful")
            End If
        End If
    End Sub
End Class