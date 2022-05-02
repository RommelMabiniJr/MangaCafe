Imports MaterialSkin
Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Imports System.ComponentModel
Imports System.IO


Public Class Form1

    Public dbConn As New MySqlConnection
    Public mangaID As Integer
    Public serviceID As Integer
    Public coverFileName As String
    Dim _bDocumentChanged As Boolean

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

        'To avoid the following events executing even before form load initilizes the connection to database
        AddHandler CheckOrderBy.SelectedIndexChanged, AddressOf CheckOrderBy_SelectedIndexChanged
        AddHandler RentOrderBy.SelectedIndexChanged, AddressOf RentOrderBy_SelectedIndexChanged

        PopulateMgLibrary()
        PopulateServiceLibrary()
        populatePurchaseHistroy("Check In")
        populatePurchaseHistroy("Rent Only")
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        revertMgLibraryUnsavedChanges()
    End Sub















    'LIBRARY TAB OR SECTION CODE AREA
    Public Sub PopulateMgLibrary()
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
        RentListBox.Items.Clear()

        For i = 0 To libraryTable.Rows.Count - 1

            'Make the manga list box from other tab populate as well
            Dim matrlItem As New MaterialListBoxItem With {
                    .Text = libraryTable.Rows(i)("mgTitle")
                }

            RentListBox.Items.Add(matrlItem)


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
            PopulateMgLibrary()

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
        PopulateMgLibrary()
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
                PopulateMgLibrary()
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
            'MsgBox(ex.Message, vbCritical)
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
            'MsgBox(ex.Message, vbCritical)
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

















    'MAKE ORDER TAB AREA
    'CHECK IN CODE PORTION
    Private Sub OrderCheckTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles OrderCheckTxt.KeyPress
        '97 - 122 = Ascii codes for simple letters
        '65 - 90  = Ascii codes for capital letters
        '48 - 57  = Ascii codes for numbers

        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub OrderCheckTxt_TextChanged(sender As Object, e As EventArgs) Handles OrderCheckTxt.TextChanged
        If OrderCheckTxt.Text <> "" Then
            CalcTotalCostCheckIn()
        End If
    End Sub

    Private Sub CheckSelectionOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckSelectionOrder.SelectedIndexChanged
        Try
            Dim sqlQuery As String = "SELECT serviceName from servicelibrary WHERE serviceType = '" & CheckSelectionOrder.Text & "'"
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
            CheckInListBox.Items.Clear()

            For i = 0 To libraryTable.Rows.Count - 1
                Dim matrlItem As New MaterialListBoxItem With {
                    .Text = libraryTable.Rows(i)("serviceName")
                }

                CheckInListBox.Items.Add(matrlItem)
            Next
        Catch ex As Exception
            'MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub CheckInListBox_SelectedIndexChanged_1(sender As Object, selectedItem As MaterialListBoxItem) Handles CheckInListBox.SelectedIndexChanged
        Try
            TxtOrderQuan.Text = "0"
            TxtOrderTot.Text = "0.00"
            Dim itemService As MaterialListBoxItem = CheckInListBox.SelectedItem

            Dim sqlQuery As String = "SELECT * from servicelibrary WHERE serviceName = '" & itemService.Text & "'"
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

            TxtOrderServName.Text = itemService.Text
            TxtOrderServPrice.Text = svDetailsTable.Rows(0)("serviceCharge")
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub TxtOrderQuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtOrderQuan.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub TxtOrderQuan_TextChanged(sender As Object, e As EventArgs) Handles TxtOrderQuan.TextChanged
        Dim quan As Integer

        If TxtOrderQuan.Text = "" Then
            quan = 0
        Else
            quan = CInt(TxtOrderQuan.Text)

            Dim price As Double = CDbl(Val(TxtOrderServPrice.Text)) * quan

            TxtOrderTot.Text = Format(price, "0.00")
        End If
    End Sub

    Dim ListViewCheckInID As Integer = 0
    Private Sub CheckAddToCart_Click(sender As Object, e As EventArgs) Handles CheckAddToCart.Click
        With ListViewCheckInLibrary
            .Items.Add(TxtOrderServName.Text)
            With .Items(.Items.Count - 1).SubItems
                .Add(TxtOrderServPrice.Text)
                .Add(TxtOrderQuan.Text)
                .Add(TxtOrderTot.Text)
            End With
        End With

        CalcTotalCostCheckIn()
    End Sub

    Dim CheckInTotCost As Double
    Public Sub CalcTotalCostCheckIn()

        CalcDurationCostCheckIn()
        CalcAdditionalCostCheckIn()

        FinalTotalCost.Text = CheckInTotCost
    End Sub

    Public Sub CalcDurationCostCheckIn()
        Try
            Dim sqlQuery As String = "SELECT dPrice from durationprices WHERE dName = '" & DurCheckSelection.Text & "'"
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


            Dim cost As Double, prs As String
            prs = durationTable.Rows(0)("dPrice")
            cost = CInt(OrderCheckTxt.Text) * CDbl(Val(prs))

            CheckInTotCost = cost

        Catch ex As Exception
            MsgBox("Please input a duration!")
            CheckInTotCost = 0.00
        End Try
    End Sub

    Public Sub CalcAdditionalCostCheckIn()
        Dim tot As Double

        If ListViewCheckInLibrary.Items.Count - 1 < 0 Then
            tot = 0.00
        Else
            For i = 0 To ListViewCheckInLibrary.Items.Count - 1
                Dim item As String = ListViewCheckInLibrary.Items(i).SubItems(3).Text
                tot = CDbl(Val(item))
                CheckInTotCost += tot
            Next
        End If

    End Sub

    Private Sub CalcTotBtnCheckIn_Click(sender As Object, e As EventArgs) Handles CalcTotBtnCheckIn.Click
        CalcTotalCostCheckIn()
    End Sub


    'IMPORTANT: RECEIPT PORTION THAT INCLUDES JOINED QUERIES FROM MULTIPLE TABLES
    Dim receiptIDCurrent As String
    Private Sub BtnConfrmCheck_Click(sender As Object, e As EventArgs) Handles BtnConfrmCheck.Click
        If OrderCheckTxt.Text = "" Then
            MsgBox("Please fill out the duration box!")
        Else
            'Insert in receipt table 
            Dim sqlQuery As String = "INSERT INTO receipttb(custName, custEmail, custAddress, custPhone, rcptType, durName, durAmount, rcptTotal) VALUES('" & Replace(CustName.Text, "'", "''") & "','" & CustEmail.Text & "','" & CustAddress.Text & "','" & CustPhone.Text & "','" & "Check In" & "','" & DurCheckSelection.Text & "','" & OrderCheckTxt.Text & "','" & FinalTotalCost.Text & "')"
            Dim sqlCommand As New MySqlCommand

            With sqlCommand
                .CommandText = sqlQuery
                .Connection = dbConn
                .ExecuteNonQuery()
            End With


            'First, retrieve the id of receipt we added lately
            Dim sqlQuery1 As String = "SELECT * from receipttb ORDER BY rcptID DESC LIMIT 1"
            Dim sqlAdapter1 As New MySqlDataAdapter
            Dim sqlCommand1 As New MySqlCommand
            Dim rcptlatest As New DataTable

            With sqlCommand1
                .CommandText = sqlQuery1
                .Connection = dbConn
            End With

            With sqlAdapter1
                .SelectCommand = sqlCommand1
                .Fill(rcptlatest)
            End With

            Dim receiptID As String = rcptlatest.Rows(0)("rcptID")
            receiptIDCurrent = receiptID



            For i = 0 To ListViewCheckInLibrary.Items.Count - 1
                Dim itmName, itmPrice, itmQuan, itmTot, itmID As String
                itmName = ListViewCheckInLibrary.Items(i).Text
                itmPrice = ListViewCheckInLibrary.Items(i).SubItems(1).Text
                itmQuan = ListViewCheckInLibrary.Items(i).SubItems(2).Text
                itmTot = ListViewCheckInLibrary.Items(i).SubItems(3).Text


                'Second, search the ID for the following service to use for inserting in rcptitemlist table
                Dim sqlQuery2 As String = "SELECT serviceID from servicelibrary WHERE serviceName = '" & itmName & "'"
                Dim sqlAdapter2 As New MySqlDataAdapter
                Dim sqlCommand2 As New MySqlCommand
                Dim srvTb As New DataTable

                With sqlCommand2
                    .CommandText = sqlQuery2
                    .Connection = dbConn
                End With

                With sqlAdapter2
                    .SelectCommand = sqlCommand2
                    .Fill(srvTb)
                End With

                itmID = srvTb.Rows(0)("serviceID")

                'Lastly, Insert now to rcptitemlist
                Dim sqlQuery3 As String = "INSERT INTO rcptitemlist(rcpt_ID, item_ID, ril_Price, ril_Quan, ril_Total) VALUES('" & receiptID & "','" & itmID & "','" & itmPrice & "','" & itmQuan & "','" & itmTot & "')"
                Dim sqlCommand3 As New MySqlCommand

                With sqlCommand3
                    .CommandText = sqlQuery3
                    .Connection = dbConn
                    .ExecuteNonQuery()
                End With
            Next


            Dim response As Integer = MsgBox("Order purchased succesfully!" + vbCrLf + vbCrLf + "Proceed to print receipt?", vbYesNo, "MangaKissa")
            If response = vbYes Then
                printCheckInReceiptNow()
            End If

            populatePurchaseHistroy("Check In")
        End If
    End Sub

    Public Sub clearCheckIn()
        OrderCheckTxt.Text = ""
        TxtOrderServName.Text = ""
        TxtOrderServPrice.Text = ""
        TxtOrderQuan.Text = "0"
        TxtOrderTot.Text = "0.00"
        ListViewCheckInLibrary.Items.Clear()
        ListViewCheckInLibrary.Refresh()
        FinalTotalCost.Text = "0.00"
    End Sub

    Public Sub printCheckInReceiptNow()
        changePaperHeight()
        PPD.Document = PrintR
        PPD.ShowDialog()
    End Sub

    Dim WithEvents PrintR As New PrintDocument
    Dim PPD As New PrintPreviewDialog
    Dim longpaper As Integer

    Public Sub changePaperHeight()
        Dim rowcount As Integer
        longpaper = 0

        rowcount = ListViewCheckInLibrary.Items.Count
        longpaper = rowcount * 15
        longpaper = longpaper + 280
    End Sub
    Private Sub PrintR_BeginPrint(sender As Object, e As Printing.PrintEventArgs) Handles PrintR.BeginPrint
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("Custom", 350, longpaper)
        'pagesetup.PaperSize = New PaperSize("Custom", 350, 500)
        PrintR.DefaultPageSettings = pagesetup
    End Sub

    Private Sub PrintR_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintR.PrintPage
        Dim sqlQuery As String = "SELECT * from receipttb WHERE rcptID = '" & receiptIDCurrent & "'"
        Dim sqlAdapter As New MySqlDataAdapter
        Dim sqlCommand As New MySqlCommand
        Dim rcptTB As New DataTable

        With sqlCommand
            .CommandText = sqlQuery
            .Connection = dbConn
        End With

        With sqlAdapter
            .SelectCommand = sqlCommand
            .Fill(rcptTB)
        End With

        Dim rcptTimeStamp = rcptTB.Rows(0)("rcptDate")


        Dim f8 As New Font("Calibri", 8, FontStyle.Regular)
        Dim f8b As New Font("Calibri", 8, FontStyle.Bold)
        Dim f10 As New Font("Calibri", 10, FontStyle.Regular)
        Dim f10b As New Font("Calibri", 10, FontStyle.Bold)
        Dim f14 As New Font("Calibri", 14, FontStyle.Bold)

        Dim leftmargin As Integer = PrintR.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = PrintR.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PrintR.DefaultPageSettings.PaperSize.Width

        'font alignment
        Dim right As New StringFormat
        Dim center As New StringFormat
        right.Alignment = StringAlignment.Far
        center.Alignment = StringAlignment.Center

        Dim line As String
        line = "-------------------------------------------------------------------------------------------------------"

        e.Graphics.DrawString("MangaKissa Cafe", f14, Brushes.Black, centermargin, 5, center)
        e.Graphics.DrawString("Gen. Luna Street, Buntay", f10, Brushes.Black, centermargin, 25, center)
        e.Graphics.DrawString("Abuyog, Leyte 6510", f10, Brushes.Black, centermargin, 40, center)
        e.Graphics.DrawString("Tel 0533002140", f8, Brushes.Black, centermargin, 55, center)

        e.Graphics.DrawString("Receipt ID", f8, Brushes.Black, 0, 70)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 70)
        e.Graphics.DrawString(receiptIDCurrent, f8, Brushes.Black, 70, 70)

        e.Graphics.DrawString("Cashier", f8, Brushes.Black, 0, 85)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 85)
        e.Graphics.DrawString("Steven Doe", f8, Brushes.Black, 70, 85)

        e.Graphics.DrawString(rcptTimeStamp, f8, Brushes.Black, 0, 100)
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 110)




        e.Graphics.DrawString("Bill To", f8, Brushes.Black, centermargin + 0, 70)
        e.Graphics.DrawString(":", f8, Brushes.Black, centermargin + 30, 70)
        e.Graphics.DrawString(CustName.Text, f8, Brushes.Black, rightmargin, 70, right)
        e.Graphics.DrawString(CustPhone.Text, f8, Brushes.Black, rightmargin, 85, right)
        e.Graphics.DrawString("Type of Order", f8, Brushes.Black, centermargin + 0, 100)
        e.Graphics.DrawString(":", f8, Brushes.Black, centermargin + 30, 100)
        e.Graphics.DrawString("Check In", f8, Brushes.Black, rightmargin, 100, right)


        e.Graphics.DrawString("SERVICE NAME", f8b, Brushes.Black, 0, 120)
        e.Graphics.DrawString("PRICE", f8b, Brushes.Black, 150, 120)
        e.Graphics.DrawString("QUANTITY", f8b, Brushes.Black, 200, 120)
        e.Graphics.DrawString("TOTAL", f8b, Brushes.Black, rightmargin, 120, right)

        Dim Height As Integer 'DGV Position
        Dim dollarFormat As Decimal

        For i = 0 To ListViewCheckInLibrary.Items.Count - 1

            Height += 15
            e.Graphics.DrawString(ListViewCheckInLibrary.Items(i).Text, f8, Brushes.Black, 0, 130 + Height)
            e.Graphics.DrawString(ListViewCheckInLibrary.Items(i).SubItems(1).Text, f8, Brushes.Black, 150, 130 + Height)
            e.Graphics.DrawString(ListViewCheckInLibrary.Items(i).SubItems(2).Text, f8, Brushes.Black, 200, 130 + Height)
            e.Graphics.DrawString(ListViewCheckInLibrary.Items(i).SubItems(3).Text, f8, Brushes.Black, rightmargin, 130 + Height, right)
        Next

        Dim height2 As Integer
        height2 = 140 + Height
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, height2)
        e.Graphics.DrawString("DURATION", f8b, Brushes.Black, 0, 20 + height2)
        e.Graphics.DrawString(OrderCheckTxt.Text, f8, Brushes.Black, 150, 20 + height2)
        e.Graphics.DrawString(DurCheckSelection.Text, f8, Brushes.Black, 200, 20 + height2)

        CalcDurationCostCheckIn()
        e.Graphics.DrawString(CheckInTotCost, f8, Brushes.Black, rightmargin, 20 + height2, right)
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 40 + height2)
        e.Graphics.DrawString("Total: " + FinalTotalCost.Text, f10b, Brushes.Black, rightmargin, 50 + height2, right)

        e.Graphics.DrawString("~Thanks for shopping~", f10, Brushes.Black, centermargin, 75 + height2, center)
        e.Graphics.DrawString("~MangaKissa~", f10, Brushes.Black, centermargin, 90 + height2, center)
    End Sub










    'RENT ONLY CODE PORTION
    Private Sub RentNumVol_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub RentDuration_KeyPress(sender As Object, e As KeyPressEventArgs) Handles RentDuration.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub RentDuration_TextChanged(sender As Object, e As EventArgs) Handles RentDuration.TextChanged
        If RentDuration.Text <> "" Then
            CalcTotalCostRent()
        End If
    End Sub

    Private Sub RentMgQuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles RentMgQuan.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Public Sub updateCopiesSubRent()
        RentMgQuan.Text = "0"
        RentMgTotal.Text = "0.00"
        Dim itemMg As MaterialListBoxItem = RentListBox.SelectedItem

        Dim sqlQuery As String = "SELECT * from mangalibrary WHERE mgTitle = '" & Replace(itemMg.Text, "'", "''") & "'"
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

        RentMgTitle.Text = itemMg.Text
        RentMgPrice.Text = mgDetailsTable.Rows(0)("mgPrice")
        RentMgCopies.Text = mgDetailsTable.Rows(0)("mgCopies")
        RentMgOnRent.Text = mgDetailsTable.Rows(0)("mgOnRent")

        'Show associated cover of the mg selected
        Dim accessDirectory As String = "D:\MangaCafeSavedImages\"
        Dim fname As String = mgDetailsTable.Rows(0)("mgCover")
        Dim filepath As String = Path.Combine(accessDirectory, fname)
        RentMgCover.Image = Image.FromFile(filepath)
    End Sub
    Private Sub RentListBox_SelectedIndexChanged(sender As Object, selectedItem As MaterialListBoxItem) Handles RentListBox.SelectedIndexChanged
        Try
            updateCopiesSubRent()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub RentMgQuan_TextChanged(sender As Object, e As EventArgs) Handles RentMgQuan.TextChanged
        Dim quan As Integer

        If RentMgQuan.Text = "" Then
            quan = 0
        Else
            Dim copiesSubRent As Integer = CInt(RentMgCopies.Text) - CInt(RentMgOnRent.Text)
            If CInt(RentMgQuan.Text) <= copiesSubRent Then
                quan = CInt(RentMgQuan.Text)

                Dim price As Double = CDbl(Val(RentMgPrice.Text)) * quan
                RentMgTotal.Text = Format(price, "0.00")
            Else
                MsgBox("Not enough copies on hand!" & vbCrLf & " # of Copies available: " & copiesSubRent & "")
            End If
        End If
    End Sub

    Private Sub RentAddToCart_Click(sender As Object, e As EventArgs) Handles RentAddToCart.Click
        Dim sqlQuery As String = "UPDATE mangalibrary SET mgOnRent = mgOnRent + '" & RentMgQuan.Text & "' WHERE mgTitle = '" & RentMgTitle.Text & "'"
        Dim sqlCommand As New MySqlCommand

        With sqlCommand
            .CommandText = sqlQuery
            .Connection = dbConn
            .ExecuteNonQuery()
        End With

        With RentListView
            .Items.Add(RentMgTitle.Text)
            With .Items(.Items.Count - 1).SubItems
                .Add(RentMgPrice.Text)
                .Add(RentMgQuan.Text)
                .Add(RentMgTotal.Text)
            End With
        End With

        Dim intVal As Integer = CInt(RentNumVol.Text) + CInt(RentMgQuan.Text)
        RentNumVol.Text = intVal

        updateCopiesSubRent()
        CalcTotalCostRent()
    End Sub


    Dim RentTotCost As Double

    Public Sub CalcTotalCostRent()
        CalcDurationCostRent()
        CalcAdditionalCostRent()

        RentFinalTot.Text = RentTotCost
    End Sub

    Public Sub CalcDurationCostRent()
        Try
            Dim sqlQuery As String = "SELECT dPrice from durationprices WHERE dName = '" & RentDurSelection.Text & "'"
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


            Dim cost As Double, prs As String
            prs = durationTable.Rows(0)("dPrice")
            cost = CInt(RentDuration.Text) * CDbl(Val(prs))

            RentTotCost = cost

        Catch ex As Exception
            MsgBox("Please input a duration!")
            RentTotCost = 0.00
        End Try
    End Sub

    Public Sub CalcAdditionalCostRent()
        Dim tot As Double
        If RentListView.Items.Count - 1 < 0 Then
            tot = 0.00
        Else
            For i = 0 To RentListView.Items.Count - 1
                Dim item As String = RentListView.Items(i).SubItems(3).Text
                tot = CDbl(Val(item))
                RentTotCost += tot
            Next
        End If
    End Sub

    Private Sub MaterialLabel40_Click(sender As Object, e As EventArgs) Handles MaterialLabel40.Click
        CalcTotalCostRent()
    End Sub

    Public Sub deleteItemInLV(listVW As ListView)
        Dim response As Integer
        For Each i As ListViewItem In listVW.SelectedItems
            response = MsgBox("Are you sure you want to remove item: " & vbCrLf & "     " + listVW.Items(listVW.FocusedItem.Index).Text, vbYesNo, "Confirm Delete")
            If response = vbYes Then

                'In order for us to still have reference to focused item before deletion
                Dim whichListView = listVW.Columns(0).Text

                If whichListView = "Title" Then
                    Dim quanVal, titleVal As String
                    quanVal = listVW.Items(listVW.FocusedItem.Index).SubItems(2).Text
                    titleVal = listVW.Items(listVW.FocusedItem.Index).Text

                    Dim sqlQuery As String = "UPDATE mangalibrary SET mgOnRent = mgOnRent - '" & quanVal & "' WHERE mgTitle = '" & titleVal & "'"
                    Dim sqlCommand As New MySqlCommand

                    With sqlCommand
                        .CommandText = sqlQuery
                        .Connection = dbConn
                        .ExecuteNonQuery()
                    End With

                    Dim intVal As Integer = CInt(RentNumVol.Text) - CInt(quanVal)
                    RentNumVol.Text = intVal

                End If

                listVW.Items.Remove(i)

                If whichListView = "Title" Then
                    updateCopiesSubRent()
                    CalcTotalCostRent()
                Else
                    CalcTotalCostCheckIn()
                End If
            End If

        Next
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click

        'for dynamic retrieval of listview name where the contextmenustrip is opened
        Dim lvNameCms As ContextMenuStrip = CType(RemoveToolStripMenuItem.Owner, ContextMenuStrip)
        deleteItemInLV(lvNameCms.SourceControl)
    End Sub

    Public Sub revertMgLibraryUnsavedChanges()
        If RentListView.Items.Count > 0 Then
            Dim response As Integer
            response = MsgBox("There are unsaved changes in your document, confirm exit?", vbYesNo, "Unsaved Changes in Document")

            If response = vbYes Then
                For i = 0 To RentListView.Items.Count - 1
                    Dim onRent As String = RentListView.Items(i).SubItems(2).Text
                    Dim mangTitle As String = RentListView.Items(i).Text

                    Dim sqlQuery As String = "UPDATE mangalibrary SET mgOnRent = mgOnRent - '" & onRent & "' WHERE mgTitle = '" & mangTitle & "'"
                    Dim sqlCommand As New MySqlCommand

                    With sqlCommand
                        .CommandText = sqlQuery
                        .Connection = dbConn
                        .ExecuteNonQuery()
                    End With
                Next
            End If
        End If
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        DateLbl.Text = Format(Now, "yyyy-mm-dd hh:mm:ss")
    End Sub

    Private Sub ConfirmOrdRent_Click(sender As Object, e As EventArgs) Handles ConfirmOrdRent.Click
        If RentDuration.Text = "" Then
            MsgBox("Please fill out the duration box!")
        Else
            'Insert in receipt table 
            Dim sqlQuery As String = "INSERT INTO receipttb(custName, custEmail, custAddress, custPhone, rcptType, durName, durAmount, rcptTotal) VALUES('" & Replace(CustName.Text, "'", "''") & "','" & CustEmail.Text & "','" & CustAddress.Text & "','" & CustPhone.Text & "','" & "Rent Only" & "','" & RentDurSelection.Text & "','" & RentDuration.Text & "','" & RentFinalTot.Text & "')"
            Dim sqlCommand As New MySqlCommand

            With sqlCommand
                .CommandText = sqlQuery
                .Connection = dbConn
                .ExecuteNonQuery()
            End With


            'First, retrieve the id of receipt we added lately
            Dim sqlQuery1 As String = "SELECT * from receipttb ORDER BY rcptID DESC LIMIT 1"
            Dim sqlAdapter1 As New MySqlDataAdapter
            Dim sqlCommand1 As New MySqlCommand
            Dim rcptlatest As New DataTable

            With sqlCommand1
                .CommandText = sqlQuery1
                .Connection = dbConn
            End With

            With sqlAdapter1
                .SelectCommand = sqlCommand1
                .Fill(rcptlatest)
            End With

            Dim receiptID As String = rcptlatest.Rows(0)("rcptID")
            receiptIDCurrent = receiptID



            For i = 0 To RentListView.Items.Count - 1
                Dim itmName, itmPrice, itmQuan, itmTot, itmID As String
                itmName = RentListView.Items(i).Text
                itmPrice = RentListView.Items(i).SubItems(1).Text
                itmQuan = RentListView.Items(i).SubItems(2).Text
                itmTot = RentListView.Items(i).SubItems(3).Text


                'Second, search the ID for the following manga to use for inserting in rcptitemlist table
                Dim sqlQuery2 As String = "SELECT mgID from mangalibrary WHERE mgTitle = '" & itmName & "'"
                Dim sqlAdapter2 As New MySqlDataAdapter
                Dim sqlCommand2 As New MySqlCommand
                Dim mgTb As New DataTable

                With sqlCommand2
                    .CommandText = sqlQuery2
                    .Connection = dbConn
                End With

                With sqlAdapter2
                    .SelectCommand = sqlCommand2
                    .Fill(mgTb)
                End With

                itmID = mgTb.Rows(0)("mgID")

                'Lastly, Insert now to rcptitemlist
                Dim sqlQuery3 As String = "INSERT INTO rcptitemlist(rcpt_ID, item_ID, ril_Price, ril_Quan, ril_Total) VALUES('" & receiptID & "','" & itmID & "','" & itmPrice & "','" & itmQuan & "','" & itmTot & "')"
                Dim sqlCommand3 As New MySqlCommand

                With sqlCommand3
                    .CommandText = sqlQuery3
                    .Connection = dbConn
                    .ExecuteNonQuery()
                End With
            Next


            Dim response As Integer = MsgBox("Order purchased succesfully!" + vbCrLf + vbCrLf + "Proceed to print receipt?", vbYesNo, "MangaKissa")
            If response = vbYes Then
                printRentOnlyReceiptNow()
            End If

            populatePurchaseHistroy("Rent Only")
        End If
    End Sub


    Public Sub printRentOnlyReceiptNow()
        changePaperHeightRent()
        PPDR.Document = PrintRentMgRcpt
        PPDR.ShowDialog()
    End Sub

    Dim WithEvents PrintRentMgRcpt As New PrintDocument
    Dim PPDR As New PrintPreviewDialog
    Dim longpaper2 As Integer

    Public Sub changePaperHeightRent()
        Dim rowcount As Integer
        longpaper2 = 0

        rowcount = RentListView.Items.Count
        longpaper2 = rowcount * 15
        longpaper2 = longpaper2 + 280
    End Sub
    Private Sub PrintRentMgRcpt_BeginPrint(sender As Object, e As Printing.PrintEventArgs) Handles PrintRentMgRcpt.BeginPrint
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("Custom", 350, longpaper2)
        'pagesetup.PaperSize = New PaperSize("Custom", 350, 500)
        PrintRentMgRcpt.DefaultPageSettings = pagesetup
    End Sub

    Private Sub PrintRentMgRcpt_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintRentMgRcpt.PrintPage
        Dim sqlQuery As String = "SELECT * from receipttb WHERE rcptID = '" & receiptIDCurrent & "'"
        Dim sqlAdapter As New MySqlDataAdapter
        Dim sqlCommand As New MySqlCommand
        Dim rcptTB As New DataTable

        With sqlCommand
            .CommandText = sqlQuery
            .Connection = dbConn
        End With

        With sqlAdapter
            .SelectCommand = sqlCommand
            .Fill(rcptTB)
        End With

        Dim rcptTimeStamp = rcptTB.Rows(0)("rcptDate")


        Dim f8 As New Font("Calibri", 8, FontStyle.Regular)
        Dim f8b As New Font("Calibri", 8, FontStyle.Bold)
        Dim f10 As New Font("Calibri", 10, FontStyle.Regular)
        Dim f10b As New Font("Calibri", 10, FontStyle.Bold)
        Dim f14 As New Font("Calibri", 14, FontStyle.Bold)

        Dim leftmargin As Integer = PrintRentMgRcpt.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = PrintRentMgRcpt.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PrintRentMgRcpt.DefaultPageSettings.PaperSize.Width

        'font alignment
        Dim right As New StringFormat
        Dim center As New StringFormat
        right.Alignment = StringAlignment.Far
        center.Alignment = StringAlignment.Center

        Dim line As String
        line = "-------------------------------------------------------------------------------------------------------"

        e.Graphics.DrawString("MangaKissa Cafe", f14, Brushes.Black, centermargin, 5, center)
        e.Graphics.DrawString("Gen. Luna Street, Buntay", f10, Brushes.Black, centermargin, 25, center)
        e.Graphics.DrawString("Abuyog, Leyte 6510", f10, Brushes.Black, centermargin, 40, center)
        e.Graphics.DrawString("Tel 0533002140", f8, Brushes.Black, centermargin, 55, center)

        e.Graphics.DrawString("Receipt ID", f8, Brushes.Black, 0, 70)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 70)
        e.Graphics.DrawString(receiptIDCurrent, f8, Brushes.Black, 70, 70)

        e.Graphics.DrawString("Cashier", f8, Brushes.Black, 0, 85)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 85)
        e.Graphics.DrawString("Steven Doe", f8, Brushes.Black, 70, 85)

        e.Graphics.DrawString(rcptTimeStamp, f8, Brushes.Black, 0, 100)
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 110)




        e.Graphics.DrawString("Bill To", f8, Brushes.Black, centermargin + 0, 70)
        e.Graphics.DrawString(":", f8, Brushes.Black, centermargin + 30, 70)
        e.Graphics.DrawString(CustName.Text, f8, Brushes.Black, rightmargin, 70, right)
        e.Graphics.DrawString(CustPhone.Text, f8, Brushes.Black, rightmargin, 85, right)
        e.Graphics.DrawString("Type", f8, Brushes.Black, centermargin + 0, 100)
        e.Graphics.DrawString(":", f8, Brushes.Black, centermargin + 30, 100)
        e.Graphics.DrawString("Rent Only", f8, Brushes.Black, rightmargin, 100, right)


        e.Graphics.DrawString("MANGA NAME", f8b, Brushes.Black, 0, 120)
        e.Graphics.DrawString("PRICE", f8b, Brushes.Black, 150, 120)
        e.Graphics.DrawString("QUANTITY", f8b, Brushes.Black, 200, 120)
        e.Graphics.DrawString("TOTAL", f8b, Brushes.Black, rightmargin, 120, right)

        Dim Height As Integer 'DGV Position
        Dim dollarFormat As Decimal

        For i = 0 To RentListView.Items.Count - 1

            Height += 15
            e.Graphics.DrawString(RentListView.Items(i).Text, f8, Brushes.Black, 0, 130 + Height)
            e.Graphics.DrawString(RentListView.Items(i).SubItems(1).Text, f8, Brushes.Black, 150, 130 + Height)
            e.Graphics.DrawString(RentListView.Items(i).SubItems(2).Text, f8, Brushes.Black, 200, 130 + Height)
            e.Graphics.DrawString(RentListView.Items(i).SubItems(3).Text, f8, Brushes.Black, rightmargin, 130 + Height, right)
        Next


        Dim height2 As Integer
        height2 = 140 + Height
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, height2)
        e.Graphics.DrawString("DURATION", f8b, Brushes.Black, 0, 20 + height2)
        e.Graphics.DrawString(RentDuration.Text, f8, Brushes.Black, 150, 20 + height2)
        e.Graphics.DrawString(RentDurSelection.Text, f8, Brushes.Black, 200, 20 + height2)

        CalcDurationCostRent()
        e.Graphics.DrawString(RentTotCost, f8, Brushes.Black, rightmargin, 20 + height2, right)
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 40 + height2)
        e.Graphics.DrawString("Total: " + RentFinalTot.Text, f10b, Brushes.Black, rightmargin, 50 + height2, right)

        e.Graphics.DrawString("~Thanks for shopping~", f10, Brushes.Black, centermargin, 75 + height2, center)
        e.Graphics.DrawString("~MangaKissa~", f10, Brushes.Black, centermargin, 90 + height2, center)
    End Sub






    'PURCHASE HISTORY TAB PORTION
    Private Sub populatePurchaseHistroy(type As String)
        Dim sqlQuery As String = "SELECT rcptID, custName, rcptType, rcptDate, rcptTotal FROM receipttb Where rcptType = '" & type & "'"
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
        If type = "Check In" Then
            CheckHistoryLV.Items.Clear()
        Else
            RentHistoryLV.Items.Clear()
        End If


        For i = 0 To libraryTable.Rows.Count - 1
            Dim IsATypeOf As String = libraryTable.Rows(i)("rcptType")
            If type = "Check In" Then
                With CheckHistoryLV
                    .Items.Add(libraryTable.Rows(i)("rcptID"))
                    With .Items(.Items.Count - 1).SubItems
                        .Add(libraryTable.Rows(i)("custName"))
                        .Add(libraryTable.Rows(i)("rcptDate"))
                        .Add(Format(libraryTable.Rows(i)("rcptTotal"), "$#,##0.00"))
                    End With
                End With

            Else
                With RentHistoryLV
                    .Items.Add(libraryTable.Rows(i)("rcptID"))
                    With .Items(.Items.Count - 1).SubItems
                        .Add(libraryTable.Rows(i)("custName"))
                        .Add(libraryTable.Rows(i)("rcptDate"))
                        .Add(Format(libraryTable.Rows(i)("rcptTotal"), "$#,##0.00"))
                    End With
                End With
            End If
        Next
    End Sub

    Private Sub ViewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewToolStripMenuItem.Click
        Dim lvNameCms As ContextMenuStrip = CType(ViewToolStripMenuItem.Owner, ContextMenuStrip)
        ShowReceiptNow(lvNameCms.SourceControl)
    End Sub

    Private Sub ReturnByToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnByToolStripMenuItem.Click
        Dim lvNameCms As ContextMenuStrip = CType(ViewToolStripMenuItem.Owner, ContextMenuStrip)
        calcDateSincePurchased(lvNameCms.SourceControl)
    End Sub

    Public Sub calcDateSincePurchased(lv As ListView)
        rcptID = lv.SelectedItems(0).Text
        Dim sqlQuery As String = "SELECT durName, durAmount, rcptDate from receipttb WHERE rcptID = '" & rcptID & "'"
        Dim sqlAdapter As New MySqlDataAdapter
        Dim sqlCommand As New MySqlCommand
        Dim rcptTB As New DataTable

        With sqlCommand
            .CommandText = sqlQuery
            .Connection = dbConn
        End With

        With sqlAdapter
            .SelectCommand = sqlCommand
            .Fill(rcptTB)
        End With

        Dim dateEntered As Date = rcptTB.Rows(0)("rcptDate")
        'Dim date2 As Date = Date.Parse(dateEntered)
        'Dim date1 As Date = Now
        Dim dName = rcptTB.Rows(0)("durName")
        Dim dAmount = rcptTB.Rows(0)("durAmount")

        Dim iFormat As String
        Select Case dName
            Case "Months"
                iFormat = "m"
            Case "Weeks"
                iFormat = "ww"
            Case "Days", "Overnight"
                iFormat = "d"
            Case "Hours"
                iFormat = "h"
            Case "Minutes"
                iFormat = "n"
            Case "Seconds"
                iFormat = "s"
        End Select

        Dim expiration As Date = DateAdd(iFormat, dAmount, dateEntered)
        Dim diff1 As TimeSpan = expiration - Now
        MsgBox("Duration: " & dAmount & " " & dName & vbCrLf & "Return By: " & expiration & vbCrLf & "Time Left:" & vbCrLf & vbTab & diff1.Days & " days, " & diff1.Hours & " hours, " & diff1.Minutes & " minutes, " & diff1.Seconds & " seconds")

    End Sub

    Dim rcptID As String
    Dim currentLV As ListView
    Public Sub ShowReceiptNow(lv As ListView)
        rcptID = lv.SelectedItems(0).Text
        currentLV = lv

        changePaperHeightShow(lv)
        PPDS.Document = PDS
        PPDS.ShowDialog()

    End Sub

    Dim WithEvents PDS As New PrintDocument
    Dim PPDS As New PrintPreviewDialog
    Dim lpaper As Integer

    Public Sub changePaperHeightShow(lstview As ListView)
        Dim rowcount As Integer
        lpaper = 0

        rowcount = lstview.Items.Count
        lpaper = rowcount * 15
        lpaper = lpaper + 280
    End Sub
    Private Sub PDS_BeginPrint(sender As Object, e As Printing.PrintEventArgs) Handles PDS.BeginPrint
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("Custom", 350, lpaper)
        'pagesetup.PaperSize = New PaperSize("Custom", 350, 500)
        PDS.DefaultPageSettings = pagesetup
    End Sub

    Private Sub PDS_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PDS.PrintPage
        Dim sqlQuery As String = "SELECT * from receipttb WHERE rcptID = '" & rcptID & "'"
        Dim sqlAdapter As New MySqlDataAdapter
        Dim sqlCommand As New MySqlCommand
        Dim rcptTB As New DataTable

        With sqlCommand
            .CommandText = sqlQuery
            .Connection = dbConn
        End With

        With sqlAdapter
            .SelectCommand = sqlCommand
            .Fill(rcptTB)
        End With

        Dim rcptTimeStamp = rcptTB.Rows(0)("rcptDate")
        Dim NameOfCust = rcptTB.Rows(0)("custName")
        Dim PhoneOfCust = rcptTB.Rows(0)("custPhone")
        Dim NameOfDuration = rcptTB.Rows(0)("durName")
        Dim AmountOfDuration = rcptTB.Rows(0)("durAmount")
        Dim TotalCostRcpt = rcptTB.Rows(0)("rcptTotal")
        Dim TypeofRcpt = rcptTB.Rows(0)("rcptType")

        Dim headercolumn1 As String
        If currentLV.Name = "CheckHistoryLV" Then
            headercolumn1 = "SERVICE NAME"
        Else
            headercolumn1 = "MANGA TITLE"
        End If

        Dim f8 As New Font("Calibri", 8, FontStyle.Regular)
        Dim f8b As New Font("Calibri", 8, FontStyle.Bold)
        Dim f10 As New Font("Calibri", 10, FontStyle.Regular)
        Dim f10b As New Font("Calibri", 10, FontStyle.Bold)
        Dim f14 As New Font("Calibri", 14, FontStyle.Bold)

        Dim leftmargin As Integer = PDS.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = PDS.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PDS.DefaultPageSettings.PaperSize.Width

        'font alignment
        Dim right As New StringFormat
        Dim center As New StringFormat
        right.Alignment = StringAlignment.Far
        center.Alignment = StringAlignment.Center

        Dim line As String
        line = "-------------------------------------------------------------------------------------------------------"

        e.Graphics.DrawString("MangaKissa Cafe", f14, Brushes.Black, centermargin, 5, center)
        e.Graphics.DrawString("Gen. Luna Street, Buntay", f10, Brushes.Black, centermargin, 25, center)
        e.Graphics.DrawString("Abuyog, Leyte 6510", f10, Brushes.Black, centermargin, 40, center)
        e.Graphics.DrawString("Tel 0533002140", f8, Brushes.Black, centermargin, 55, center)

        e.Graphics.DrawString("Receipt ID", f8, Brushes.Black, 0, 70)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 70)
        e.Graphics.DrawString(rcptID, f8, Brushes.Black, 70, 70)

        e.Graphics.DrawString("Cashier", f8, Brushes.Black, 0, 85)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 85)
        e.Graphics.DrawString("Steven Doe", f8, Brushes.Black, 70, 85)

        e.Graphics.DrawString(rcptTimeStamp, f8, Brushes.Black, 0, 100)
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 110)




        e.Graphics.DrawString("Bill To", f8, Brushes.Black, centermargin + 0, 70)
        e.Graphics.DrawString(":", f8, Brushes.Black, centermargin + 30, 70)
        e.Graphics.DrawString(NameOfCust, f8, Brushes.Black, rightmargin, 70, right)
        e.Graphics.DrawString(PhoneOfCust, f8, Brushes.Black, rightmargin, 85, right)
        e.Graphics.DrawString("Type", f8, Brushes.Black, centermargin + 0, 100)
        e.Graphics.DrawString(":", f8, Brushes.Black, centermargin + 30, 100)
        e.Graphics.DrawString(TypeofRcpt, f8, Brushes.Black, rightmargin, 100, right)


        e.Graphics.DrawString(headercolumn1, f8b, Brushes.Black, 0, 120)
        e.Graphics.DrawString("PRICE", f8b, Brushes.Black, 150, 120)
        e.Graphics.DrawString("QUANTITY", f8b, Brushes.Black, 200, 120)
        e.Graphics.DrawString("TOTAL", f8b, Brushes.Black, rightmargin, 120, right)





        'Query to get all item in recpitemlist assoicated with the receipt id
        Dim sqlQuery1 As String = "SELECT ril_ID, rcpt_ID, item_ID, ril_Price, ril_Quan, ril_Total FROM rcptitemlist WHERE '" & rcptID & "' = rcpt_ID;"
        Dim sqlAdapter1 As New MySqlDataAdapter
        Dim sqlCommand1 As New MySqlCommand
        Dim rilTB As New DataTable

        With sqlCommand1
            .CommandText = sqlQuery1
            .Connection = dbConn
        End With

        With sqlAdapter1
            .SelectCommand = sqlCommand1
            .Fill(rilTB)
        End With



        Dim tb, tbnamecol, tbID As String
        If TypeofRcpt = "Check In" Then
            tb = "servicelibrary"
            tbID = "serviceID"
            tbnamecol = "serviceName"
        Else
            tb = "mangalibrary"
            tbID = "mgID"
            tbnamecol = "mgTitle"
        End If


        Dim Height As Integer 'DGV Position

        For i = 0 To rilTB.Rows.Count - 1

            Dim itmID = rilTB.Rows(i)("item_ID")

            Dim sqlQuery2 As String = "SELECT " & tbnamecol & " FROM " & tb & " WHERE " & tbID & " = '" & itmID & "';"
            Dim sqlAdapter2 As New MySqlDataAdapter
            Dim sqlCommand2 As New MySqlCommand
            Dim itmTB As New DataTable

            With sqlCommand2
                .CommandText = sqlQuery2
                .Connection = dbConn
            End With

            With sqlAdapter2
                .SelectCommand = sqlCommand2
                .Fill(itmTB)
            End With


            Dim itmName = itmTB.Rows(0)(tbnamecol)
            Dim itmPrice = rilTB.Rows(i)("ril_Price")
            Dim itmQuan = rilTB.Rows(i)("ril_Quan")
            Dim itmTotal = rilTB.Rows(i)("ril_Total")

            Height += 15
            e.Graphics.DrawString(itmName, f8, Brushes.Black, 0, 130 + Height)
            e.Graphics.DrawString(itmPrice, f8, Brushes.Black, 150, 130 + Height)
            e.Graphics.DrawString(itmQuan, f8, Brushes.Black, 200, 130 + Height)
            e.Graphics.DrawString(itmTotal, f8, Brushes.Black, rightmargin, 130 + Height, right)
        Next


        Dim height2 As Integer
        height2 = 140 + Height
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, height2)
        e.Graphics.DrawString("DURATION", f8b, Brushes.Black, 0, 20 + height2)
        e.Graphics.DrawString(AmountOfDuration, f8, Brushes.Black, 150, 20 + height2)
        e.Graphics.DrawString(NameOfDuration, f8, Brushes.Black, 200, 20 + height2)

        CalAmountDuration(NameOfDuration, AmountOfDuration)
        e.Graphics.DrawString(durCost, f8, Brushes.Black, rightmargin, 20 + height2, right)
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 40 + height2)
        e.Graphics.DrawString("Total: " & TotalCostRcpt, f10b, Brushes.Black, rightmargin, 50 + height2, right)

        e.Graphics.DrawString("~Thanks for shopping~", f10, Brushes.Black, centermargin, 75 + height2, center)
        e.Graphics.DrawString("~MangaKissa~", f10, Brushes.Black, centermargin, 90 + height2, center)
    End Sub

    Dim durCost As String
    Public Sub CalAmountDuration(nameDur As String, amountDur As String)
        Dim sqlQuery As String = "SELECT dPrice from durationprices WHERE dName = '" & nameDur & "'"
        Dim sqlAdapter As New MySqlDataAdapter
        Dim sqlCommand As New MySqlCommand
        Dim dTable As New DataTable

        With sqlCommand
            .CommandText = sqlQuery
            .Connection = dbConn
        End With

        With sqlAdapter
            .SelectCommand = sqlCommand
            .Fill(dTable)
        End With

        Dim durPrice As String = dTable.Rows(0)("dPrice")

        durCost = CDbl(durPrice) * CInt(amountDur)
    End Sub

    Private Sub CheckOrderBy_SelectedIndexChanged(sender As Object, e As EventArgs)
        If CheckOrderBy.Text = "Active" Then
            populateCheckHistoryActive("Check In")
        ElseIf CheckOrderBy.Text = "All" Then
            populatePurchaseHistroy("Check In")
        End If
    End Sub

    Private Sub RentOrderBy_SelectedIndexChanged(sender As Object, e As EventArgs)
        If RentOrderBy.Text = "Active" Then
            populateCheckHistoryActive("Rent Only")
        ElseIf RentOrderBy.Text = "All" Then
            populatePurchaseHistroy("Rent Only")
        End If
    End Sub

    Private Sub populateCheckHistoryActive(type As String)
        Dim sqlQuery As String = "SELECT * FROM receipttb Where rcptType = '" & type & "'"
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
        If type = "Check In" Then
            CheckHistoryLV.Items.Clear()
        Else
            RentHistoryLV.Items.Clear()
        End If

        For i = 0 To libraryTable.Rows.Count - 1
            Dim IsATypeOf As String = libraryTable.Rows(i)("rcptType")

            Dim dateEntered As Date = libraryTable.Rows(i)("rcptDate")
            'Dim date2 As Date = Date.Parse(dateEntered)
            'Dim date1 As Date = Now
            Dim dName = libraryTable.Rows(i)("durName")
            Dim dAmount = libraryTable.Rows(i)("durAmount")

            Dim iFormat As String
            Select Case dName
                Case "Months"
                    iFormat = "m"
                Case "Weeks"
                    iFormat = "ww"
                Case "Days", "Overnight"
                    iFormat = "d"
                Case "Hours"
                    iFormat = "h"
                Case "Minutes"
                    iFormat = "n"
                Case "Seconds"
                    iFormat = "s"
            End Select

            Dim expiration As Date = DateAdd(iFormat, dAmount, dateEntered)
            Dim diff1 As TimeSpan = expiration - Now

            If type = "Check In" Then
                If diff1.Days > 0 Or diff1.Hours > 0 Or diff1.Minutes > 0 Or diff1.Seconds > 0 Then
                    With CheckHistoryLV
                        .Items.Add(libraryTable.Rows(i)("rcptID"))
                        With .Items(.Items.Count - 1).SubItems
                            .Add(libraryTable.Rows(i)("custName"))
                            .Add(libraryTable.Rows(i)("rcptDate"))
                            .Add(Format(libraryTable.Rows(i)("rcptTotal"), "$#,##0.00"))
                        End With
                    End With
                End If
            Else
                If diff1.Days > 0 Or diff1.Hours > 0 Or diff1.Minutes > 0 Or diff1.Seconds > 0 Then
                    With RentHistoryLV
                        .Items.Add(libraryTable.Rows(i)("rcptID"))
                        With .Items(.Items.Count - 1).SubItems
                            .Add(libraryTable.Rows(i)("custName"))
                            .Add(libraryTable.Rows(i)("rcptDate"))
                            .Add(Format(libraryTable.Rows(i)("rcptTotal"), "$#,##0.00"))
                        End With
                    End With
                End If
            End If
        Next
    End Sub
End Class

