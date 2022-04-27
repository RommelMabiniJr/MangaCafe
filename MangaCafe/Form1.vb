Imports MaterialSkin
Imports MySql.Data.MySqlClient
Imports System.IO

Public Class Form1

    Public dbConn As New MySqlConnection
    Public mangaID As Integer
    Public serviceID As Integer
    Public coverFileName As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim SkinManager As MaterialSkinManager = MaterialSkinManager.Instance
        SkinManager.AddFormToManage(Me)
        SkinManager.Theme = MaterialSkinManager.Themes.DARK
        SkinManager.ColorScheme = New ColorScheme(Primary.Brown800, Primary.Brown900, Primary.Brown500, Accent.Amber700, TextShade.WHITE)

        'Hide expansion panels by default
        PanelAddToLibrary.Hide()

        'Reset rows to empty or default
        ListViewMangaLibrary.Items.Clear()
        ListViewMangaLibrary.Refresh()

        'Make sure to create connection to database
        If dbConn.State = ConnectionState.Closed Then
            dbConn.ConnectionString = "SERVER = localhost; USERID = root; PASSWORD=; DATABASE = mangakissadb"
            dbConn.Open()
        End If

        PopulateLibrary()
        PopulateServiceLibrary()
    End Sub















    'LIBRARY TAB OR SECTION CODE AREA
    Public Sub PopulateLibrary()
        Dim sqlQuery As String = "SELECT mgID, mgTitle, mgCopies, mgPrice FROM mangalibrary"
        Dim sqlAdapter As New MySqlDataAdapter
        Dim sqlCommand As New MySqlCommand
        Dim libraryTable As New DataTable
        Dim i As Integer


        With sqlCommand
            .CommandText = sqlQuery
            .Connection = dbConn
        End With

        With sqlAdapter
            .SelectCommand = sqlCommand
            .Fill(libraryTable)
        End With

        'To avoid duplicating previously added entries
        ListViewMangaLibrary.Items.Clear()

        For i = 0 To libraryTable.Rows.Count - 1
            With ListViewMangaLibrary
                .Items.Add(libraryTable.Rows(i)("mgID"))
                With .Items(.Items.Count - 1).SubItems
                    .Add(libraryTable.Rows(i)("mgTitle"))
                    .Add(libraryTable.Rows(i)("mgCopies"))
                    .Add(Format(libraryTable.Rows(i)("mgPrice"), "$#,##0.00"))
                End With
            End With
        Next

    End Sub

    Public panelToLibraryShow As Boolean = False
    Private Sub btnAddToLibrary_Click(sender As Object, e As EventArgs) Handles btnAddToLibrary.Click
        If panelToLibraryShow = False Then
            panelToLibraryShow = True
            PanelAddToLibrary.Show()
        Else
            PanelAddToLibrary.Hide()
            panelToLibraryShow = False
        End If
    End Sub

    Private Sub ClearAddMangaTxtBoxes()
        TxtTitleAdd.Text = ""
        TxtCopiesAdd.Text = ""
        TxtPriceAdd.Text = ""
        TxtISBNAdd.Text = ""
    End Sub


    Public Sub SaveImg(imgChosen As PictureBox, dialogUsed As OpenFileDialog)

        If imgChosen.Location.ToString <> String.Empty Then

            Dim saveDirectory As String = "D:\MangaCafeSavedImages\"
            If Not Directory.Exists(saveDirectory) Then
                Directory.CreateDirectory(saveDirectory)
            End If

            Dim image_title As String = Path.GetFileName(coverFileName)
            Dim fileSavePath As String = Path.Combine(saveDirectory, image_title)

            If dialogUsed.FileName <> String.Empty Then
                File.Copy(dialogUsed.FileName, fileSavePath, True)
            End If
        End If
    End Sub

    Private Sub ImgCoverAdd_Click(sender As Object, e As EventArgs) Handles ImgCoverAdd.Click
        With OpenFileDialog1
            If .ShowDialog Then
                If OpenFileDialog1.FileName <> String.Empty Then
                    ImgCoverAdd.Image = Image.FromFile(.FileName)
                    coverFileName = .FileName
                End If
            End If

        End With
    End Sub

    Private Sub BtnAddMangaFinal_Click(sender As Object, e As EventArgs) Handles BtnAddMangaFinal.Click
        Try
            Dim sqlQuery As String = "INSERT INTO mangalibrary(mgTitle, mgCopies, mgPrice, mgISBN, mgCover) VALUES('" & Replace(TxtTitleAdd.Text, "'", "''") & "','" & TxtCopiesAdd.Text & "','" & TxtPriceAdd.Text & "','" & TxtISBNAdd.Text & "', '" & Path.GetFileName(coverFileName) & "')"
            Dim sqlCommand As New MySqlCommand

            With sqlCommand
                .CommandText = sqlQuery
                .Connection = dbConn
                .ExecuteNonQuery()
            End With


            MsgBox("Manga added succesfully!")
            PopulateLibrary()

            PanelAddToLibrary.Hide()
            panelToLibraryShow = False

            SaveImg(ImgCoverAdd, OpenFileDialog1)
            ClearAddMangaTxtBoxes()
            ImgCoverAdd.Image = Nothing
            coverFileName = String.Empty
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ListViewMangaLibrary_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewMangaLibrary.SelectedIndexChanged
        Try
            mangaID = ListViewMangaLibrary.SelectedItems(0).Text
            BtnEditManga.Enabled = True
            BtnDeleteManga.Enabled = True
        Catch ex As Exception
            BtnSaveManga.Enabled = False
            BtnEditManga.Enabled = False
            BtnDeleteManga.Enabled = False
            ImgCoverEdit.Enabled = False
        End Try
    End Sub

    Private Sub BtnSaveManga_Click(sender As Object, e As EventArgs) Handles BtnSaveManga.Click
        Dim sqlQuery As String = "UPDATE mangalibrary SET mgTitle = '" & Replace(TxtTitleEdit.Text, "'", "''") & "', mgCopies = '" & TxtCopiesEdit.Text & "', mgPrice = '" & TxtPriceEdit.Text & "', mgISBN = '" & TxtISBNEdit.Text & "', mgCover = '" & Path.GetFileName(coverFileName) & "' WHERE mgID = '" & mangaID & "'"
        Dim sqlCommand As New MySqlCommand

        With sqlCommand
            .CommandText = sqlQuery
            .Connection = dbConn
            .ExecuteNonQuery()
        End With

        MsgBox("Manga updated succesfully!", MsgBoxStyle.Information)
        PopulateLibrary()
        SaveImg(ImgCoverEdit, OpenFileDialog2)

        coverFileName = String.Empty
    End Sub

    Private Sub BtnEditManga_Click(sender As Object, e As EventArgs) Handles BtnEditManga.Click
        Try
            If mangaID = Nothing Then
                MsgBox("Please select a manga first.", MsgBoxStyle.Exclamation)
            Else
                Dim sqlQuery As String = "SELECT mgTitle, mgCopies, mgPrice, mgISBN, mgCover from mangalibrary WHERE mgID = '" & ListViewMangaLibrary.SelectedItems(0).Text & "'"
                Dim sqlAdapter As New MySqlDataAdapter
                Dim sqlCommand As New MySqlCommand
                Dim mgDetailsTable As New DataTable

                With sqlCommand
                    .CommandText = sqlQuery
                    .Connection = dbConn
                End With

                With sqlAdapter
                    .SelectCommand = sqlCommand
                    .Fill(mgDetailsTable)
                End With

                TxtTitleEdit.Text = mgDetailsTable.Rows(0)("mgTitle")
                TxtCopiesEdit.Text = mgDetailsTable.Rows(0)("mgCopies")
                TxtPriceEdit.Text = mgDetailsTable.Rows(0)("mgPrice")
                TxtISBNEdit.Text = mgDetailsTable.Rows(0)("mgISBN")


                'Locally access the associated picture from the device
                Dim accessDirectory As String = "D:\MangaCafeSavedImages\"
                Dim fname As String = mgDetailsTable.Rows(0)("mgCover")
                Dim filepath As String = Path.Combine(accessDirectory, fname)
                ImgCoverEdit.Image = Image.FromFile(filepath)

                'To make picturebox have a referrence in use after clicking save even if no changes are made
                coverFileName = filepath

                BtnSaveManga.Enabled = True
                ImgCoverEdit.Enabled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub BtnDeleteManga_Click(sender As Object, e As EventArgs) Handles BtnDeleteManga.Click
        If mangaID = Nothing Then
            MsgBox("Please choose a manga item to delete.")
        Else
            Dim confirmDel As DialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo)
            If confirmDel = DialogResult.Yes Then
                Dim sqlQuery As String = "DELETE FROM mangalibrary WHERE mgID = '" & mangaID & "'"
                Dim sqlCommand As New MySqlCommand

                With sqlCommand
                    .CommandText = sqlQuery
                    .Connection = dbConn
                    .ExecuteNonQuery()
                End With

                MsgBox("Manga deleted succesfully!", MsgBoxStyle.Information)
                PopulateLibrary()
            End If

        End If
    End Sub

    Private Sub ImgCoverEdit_Click(sender As Object, e As EventArgs) Handles ImgCoverEdit.Click
        With OpenFileDialog2
            If .ShowDialog Then
                If OpenFileDialog2.FileName <> String.Empty Then
                    ImgCoverEdit.Image = Image.FromFile(.FileName)
                    coverFileName = .FileName
                End If
            End If
        End With
    End Sub















    'SERVICE TAB OR SECTION AREA
    Public Sub PopulateServiceLibrary()
        Dim sqlQuery As String = "SELECT serviceID, serviceName, serviceType, serviceCharge FROM servicelibrary"
        Dim sqlAdapter As New MySqlDataAdapter
        Dim sqlCommand As New MySqlCommand
        Dim libraryTable As New DataTable
        Dim i As Integer


        With sqlCommand
            .CommandText = sqlQuery
            .Connection = dbConn
        End With

        With sqlAdapter
            .SelectCommand = sqlCommand
            .Fill(libraryTable)
        End With

        'To avoid duplicating previously added entries
        ListViewServiceLibrary.Items.Clear()

        For i = 0 To libraryTable.Rows.Count - 1
            With ListViewServiceLibrary
                .Items.Add(libraryTable.Rows(i)("serviceID"))
                With .Items(.Items.Count - 1).SubItems
                    .Add(libraryTable.Rows(i)("serviceName"))
                    .Add(libraryTable.Rows(i)("serviceType"))
                    .Add(Format(libraryTable.Rows(i)("serviceCharge"), "$#,##0.00"))
                End With
            End With
        Next

    End Sub

    Private Sub ClearAddServiceTxtBoxes()
        TxtAddServiceName.Text = ""
        TxtAddServiceCharge.Text = ""
    End Sub

    Private Sub ClearServiceTxtBoxes()
        TxtServiceName.Text = ""
        TxtServiceCharge.Text = ""
    End Sub

    Private Sub BtnServiceCreate_Click(sender As Object, e As EventArgs) Handles BtnServiceCreate.Click
        Try
            Dim sqlQuery As String = "INSERT INTO servicelibrary(serviceName, serviceType, serviceCharge) VALUES('" & Replace(TxtAddServiceName.Text, "'", "''") & "','" & TxtAddServiceType.Text & "','" & TxtAddServiceCharge.Text & "')"
            Dim sqlCommand As New MySqlCommand

            With sqlCommand
                .CommandText = sqlQuery
                .Connection = dbConn
                .ExecuteNonQuery()
            End With


            MsgBox("Service added succesfully!")
            PopulateServiceLibrary()
            ClearAddServiceTxtBoxes()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ListViewServiceLibrary_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewServiceLibrary.SelectedIndexChanged
        Try
            serviceID = ListViewServiceLibrary.SelectedItems(0).Text
            BtnEditService.Enabled = True
            BtnDelService.Enabled = True

            'to avoid confusion, clear the textboxes
            ClearServiceTxtBoxes()
        Catch ex As Exception
            BtnSaveService.Enabled = False
            BtnEditService.Enabled = False
            BtnDelService.Enabled = False
        End Try
    End Sub

    Private Sub BtnEditService_Click(sender As Object, e As EventArgs) Handles BtnEditService.Click
        Try
            If serviceID = Nothing Then
                MsgBox("Please select a service item first.", MsgBoxStyle.Exclamation)
            Else
                Dim sqlQuery As String = "SELECT serviceName, serviceType, serviceCharge from servicelibrary WHERE serviceID = '" & ListViewServiceLibrary.SelectedItems(0).Text & "'"
                Dim sqlAdapter As New MySqlDataAdapter
                Dim sqlCommand As New MySqlCommand
                Dim svDetailsTable As New DataTable

                With sqlCommand
                    .CommandText = sqlQuery
                    .Connection = dbConn
                End With

                With sqlAdapter
                    .SelectCommand = sqlCommand
                    .Fill(svDetailsTable)
                End With

                TxtServiceName.Text = svDetailsTable.Rows(0)("serviceName")
                TxtServiceType.SelectedItem = svDetailsTable.Rows(0)("serviceType")
                TxtServiceCharge.Text = svDetailsTable.Rows(0)("serviceCharge")

                BtnSaveService.Enabled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub BtnSaveService_Click(sender As Object, e As EventArgs) Handles BtnSaveService.Click
        Dim sqlQuery As String = "UPDATE servicelibrary SET serviceName = '" & Replace(TxtServiceName.Text, "'", "''") & "', serviceType = '" & TxtServiceType.Text & "', serviceCharge = '" & TxtServiceCharge.Text & "' WHERE serviceID = '" & serviceID & "'"
        Dim sqlCommand As New MySqlCommand

        With sqlCommand
            .CommandText = sqlQuery
            .Connection = dbConn
            .ExecuteNonQuery()
        End With

        MsgBox("Service item updated succesfully!", MsgBoxStyle.Information)
        PopulateServiceLibrary()
    End Sub

    Private Sub BtnDelService_Click(sender As Object, e As EventArgs) Handles BtnDelService.Click
        If serviceID = Nothing Then
            MsgBox("Please choose a service item to delete.")
        Else
            Dim confirmDel As DialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo)
            If confirmDel = DialogResult.Yes Then
                Dim sqlQuery As String = "DELETE FROM servicelibrary WHERE serviceID = '" & serviceID & "'"
                Dim sqlCommand As New MySqlCommand

                With sqlCommand
                    .CommandText = sqlQuery
                    .Connection = dbConn
                    .ExecuteNonQuery()
                End With

                MsgBox("Service deleted succesfully!", MsgBoxStyle.Information)
                PopulateServiceLibrary()
            End If

        End If
    End Sub


    Private Sub BtnEditRent_Click(sender As Object, e As EventArgs) Handles BtnEditRent.Click
        BtnSaveRent.Enabled = True
    End Sub

    Private Sub BtnEditCheckIn_Click(sender As Object, e As EventArgs) Handles BtnEditCheckIn.Click
        BtnSaveCheckIn.Enabled = True
    End Sub

    Private Sub CheckInSelection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckInSelection.SelectedIndexChanged
        Try
            Dim sqlQuery As String = "SELECT dPrice from durationprices WHERE dName = '" & CheckInSelection.Text & "'"
            Dim sqlAdapter As New MySqlDataAdapter
            Dim sqlCommand As New MySqlCommand
            Dim durationTable As New DataTable

            With sqlCommand
                .CommandText = sqlQuery
                .Connection = dbConn
            End With

            With sqlAdapter
                .SelectCommand = sqlCommand
                .Fill(durationTable)
            End With

            TxtCheckInEdit.Text = durationTable.Rows(0)("dPrice")
            BtnSaveCheckIn.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub RentSelection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RentSelection.SelectedIndexChanged
        Try
            Dim sqlQuery As String = "SELECT dPrice from durationprices WHERE dName = '" & RentSelection.Text & "'"
            Dim sqlAdapter As New MySqlDataAdapter
            Dim sqlCommand As New MySqlCommand
            Dim durationTable As New DataTable

            With sqlCommand
                .CommandText = sqlQuery
                .Connection = dbConn
            End With

            With sqlAdapter
                .SelectCommand = sqlCommand
                .Fill(durationTable)
            End With

            TxtRentEdit.Text = durationTable.Rows(0)("dPrice")
            BtnSaveRent.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub BtnSaveCheckIn_Click(sender As Object, e As EventArgs) Handles BtnSaveCheckIn.Click
        Dim sqlQuery As String = "UPDATE durationprices SET dPrice = '" & TxtCheckInEdit.Text & "' WHERE dName = '" & CheckInSelection.Text & "'"
        Dim sqlCommand As New MySqlCommand

        With sqlCommand
            .CommandText = sqlQuery
            .Connection = dbConn
            .ExecuteNonQuery()
        End With

        MsgBox("Duration item updated succesfully!", MsgBoxStyle.Information)
    End Sub

    Private Sub BtnSaveRent_Click(sender As Object, e As EventArgs) Handles BtnSaveRent.Click
        Dim sqlQuery As String = "UPDATE durationprices SET dPrice = '" & TxtRentEdit.Text & "' WHERE dName = '" & RentSelection.Text & "'"
        Dim sqlCommand As New MySqlCommand

        With sqlCommand
            .CommandText = sqlQuery
            .Connection = dbConn
            .ExecuteNonQuery()
        End With

        MsgBox("Duration item updated succesfully!", MsgBoxStyle.Information)
    End Sub
End Class