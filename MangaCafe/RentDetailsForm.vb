Imports MaterialSkin

Public Class RentDetailsForm
    Public rcptItemListIDs() As String

    Public Sub createItemListofIDs(num As String)
        ReDim rcptItemListIDs(num)
    End Sub
    Private Sub RentDetailsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim SkinManager As MaterialSkinManager = MaterialSkinManager.Instance
        SkinManager.AddFormToManage(Me)
        SkinManager.Theme = MaterialSkinManager.Themes.DARK
        SkinManager.ColorScheme = New ColorScheme(Primary.Brown800, Primary.Brown900, Primary.Brown500, Accent.Amber700, TextShade.WHITE)
    End Sub

    Private Sub MaterialListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MaterialListView1.SelectedIndexChanged
        Try
            ConfrmChanges.Enabled = True
            If MaterialListView1.SelectedItems.Count = 0 Then
                Return
            Else
                StatusSelection.Text = MaterialListView1.SelectedItems(0).SubItems(3).Text
            End If
        Catch ex As Exception
            ConfrmChanges.Enabled = False
        End Try
    End Sub

    Private Sub ConfrmChanges_Click(sender As Object, e As EventArgs) Handles ConfrmChanges.Click
        MaterialListView1.SelectedItems(0).SubItems(3).Text = StatusSelection.Text
    End Sub

    Private Sub SetAllBtn_Click(sender As Object, e As EventArgs) Handles SetAllBtn.Click
        For i = 0 To MaterialListView1.Items.Count - 1
            MaterialListView1.Items(i).SubItems(3).Text = SetAllCollection.Text
        Next
    End Sub

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        Form1.queryFromRentDetailsForm()
    End Sub
End Class