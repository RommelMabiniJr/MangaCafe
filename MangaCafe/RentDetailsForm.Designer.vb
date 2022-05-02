<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RentDetailsForm
    Inherits MaterialSkin.Controls.MaterialForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ReceiptID = New MaterialSkin.Controls.MaterialLabel()
        Me.NameOfCustomer = New MaterialSkin.Controls.MaterialLabel()
        Me.MaterialListView1 = New MaterialSkin.Controls.MaterialListView()
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader()
        Me.StatusSelection = New MaterialSkin.Controls.MaterialComboBox()
        Me.ConfrmChanges = New MaterialSkin.Controls.MaterialButton()
        Me.MaterialCard1 = New MaterialSkin.Controls.MaterialCard()
        Me.MaterialLabel4 = New MaterialSkin.Controls.MaterialLabel()
        Me.MaterialLabel3 = New MaterialSkin.Controls.MaterialLabel()
        Me.MaterialLabel1 = New MaterialSkin.Controls.MaterialLabel()
        Me.MaterialLabel2 = New MaterialSkin.Controls.MaterialLabel()
        Me.SetAllCollection = New MaterialSkin.Controls.MaterialComboBox()
        Me.SetAllBtn = New MaterialSkin.Controls.MaterialButton()
        Me.SaveBtn = New MaterialSkin.Controls.MaterialButton()
        Me.MaterialCard1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ReceiptID
        '
        Me.ReceiptID.AutoSize = True
        Me.ReceiptID.Depth = 0
        Me.ReceiptID.Font = New System.Drawing.Font("Roboto", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.ReceiptID.FontType = MaterialSkin.MaterialSkinManager.fontType.H5
        Me.ReceiptID.Location = New System.Drawing.Point(179, 121)
        Me.ReceiptID.MouseState = MaterialSkin.MouseState.HOVER
        Me.ReceiptID.Name = "ReceiptID"
        Me.ReceiptID.Size = New System.Drawing.Size(117, 29)
        Me.ReceiptID.TabIndex = 0
        Me.ReceiptID.Text = "Receipt ID:"
        '
        'NameOfCustomer
        '
        Me.NameOfCustomer.AutoSize = True
        Me.NameOfCustomer.Depth = 0
        Me.NameOfCustomer.Font = New System.Drawing.Font("Roboto", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.NameOfCustomer.FontType = MaterialSkin.MaterialSkinManager.fontType.H5
        Me.NameOfCustomer.Location = New System.Drawing.Point(179, 83)
        Me.NameOfCustomer.MouseState = MaterialSkin.MouseState.HOVER
        Me.NameOfCustomer.Name = "NameOfCustomer"
        Me.NameOfCustomer.Size = New System.Drawing.Size(77, 29)
        Me.NameOfCustomer.TabIndex = 0
        Me.NameOfCustomer.Text = "Name: "
        '
        'MaterialListView1
        '
        Me.MaterialListView1.AutoSizeTable = False
        Me.MaterialListView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MaterialListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.MaterialListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.MaterialListView1.Depth = 0
        Me.MaterialListView1.FullRowSelect = True
        Me.MaterialListView1.Location = New System.Drawing.Point(37, 163)
        Me.MaterialListView1.MinimumSize = New System.Drawing.Size(200, 100)
        Me.MaterialListView1.MouseLocation = New System.Drawing.Point(-1, -1)
        Me.MaterialListView1.MouseState = MaterialSkin.MouseState.OUT
        Me.MaterialListView1.Name = "MaterialListView1"
        Me.MaterialListView1.OwnerDraw = True
        Me.MaterialListView1.Size = New System.Drawing.Size(549, 229)
        Me.MaterialListView1.TabIndex = 1
        Me.MaterialListView1.UseCompatibleStateImageBehavior = False
        Me.MaterialListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "ID"
        Me.ColumnHeader1.Width = 100
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Manga Title"
        Me.ColumnHeader2.Width = 250
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Quantity"
        Me.ColumnHeader3.Width = 100
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Rent Status"
        Me.ColumnHeader4.Width = 150
        '
        'StatusSelection
        '
        Me.StatusSelection.AutoResize = False
        Me.StatusSelection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.StatusSelection.Depth = 0
        Me.StatusSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.StatusSelection.DropDownHeight = 174
        Me.StatusSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StatusSelection.DropDownWidth = 121
        Me.StatusSelection.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.StatusSelection.ForeColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.StatusSelection.FormattingEnabled = True
        Me.StatusSelection.IntegralHeight = False
        Me.StatusSelection.ItemHeight = 43
        Me.StatusSelection.Items.AddRange(New Object() {"Active", "Unreturned", "Returned"})
        Me.StatusSelection.Location = New System.Drawing.Point(17, 70)
        Me.StatusSelection.MaxDropDownItems = 4
        Me.StatusSelection.MouseState = MaterialSkin.MouseState.OUT
        Me.StatusSelection.Name = "StatusSelection"
        Me.StatusSelection.Size = New System.Drawing.Size(146, 49)
        Me.StatusSelection.StartIndex = 0
        Me.StatusSelection.TabIndex = 2
        '
        'ConfrmChanges
        '
        Me.ConfrmChanges.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ConfrmChanges.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.[Default]
        Me.ConfrmChanges.Depth = 0
        Me.ConfrmChanges.Enabled = False
        Me.ConfrmChanges.HighEmphasis = True
        Me.ConfrmChanges.Icon = Nothing
        Me.ConfrmChanges.Location = New System.Drawing.Point(11, 151)
        Me.ConfrmChanges.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.ConfrmChanges.MouseState = MaterialSkin.MouseState.HOVER
        Me.ConfrmChanges.Name = "ConfrmChanges"
        Me.ConfrmChanges.NoAccentTextColor = System.Drawing.Color.Empty
        Me.ConfrmChanges.Size = New System.Drawing.Size(158, 36)
        Me.ConfrmChanges.TabIndex = 3
        Me.ConfrmChanges.Text = "Confirm Changes"
        Me.ConfrmChanges.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained
        Me.ConfrmChanges.UseAccentColor = False
        Me.ConfrmChanges.UseVisualStyleBackColor = True
        '
        'MaterialCard1
        '
        Me.MaterialCard1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MaterialCard1.Controls.Add(Me.StatusSelection)
        Me.MaterialCard1.Controls.Add(Me.ConfrmChanges)
        Me.MaterialCard1.Controls.Add(Me.MaterialLabel4)
        Me.MaterialCard1.Depth = 0
        Me.MaterialCard1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.MaterialCard1.Location = New System.Drawing.Point(603, 163)
        Me.MaterialCard1.Margin = New System.Windows.Forms.Padding(14)
        Me.MaterialCard1.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialCard1.Name = "MaterialCard1"
        Me.MaterialCard1.Padding = New System.Windows.Forms.Padding(14)
        Me.MaterialCard1.Size = New System.Drawing.Size(180, 222)
        Me.MaterialCard1.TabIndex = 4
        '
        'MaterialLabel4
        '
        Me.MaterialLabel4.AutoSize = True
        Me.MaterialLabel4.Depth = 0
        Me.MaterialLabel4.Font = New System.Drawing.Font("Roboto", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.MaterialLabel4.FontType = MaterialSkin.MaterialSkinManager.fontType.H5
        Me.MaterialLabel4.Location = New System.Drawing.Point(45, 25)
        Me.MaterialLabel4.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialLabel4.Name = "MaterialLabel4"
        Me.MaterialLabel4.Size = New System.Drawing.Size(89, 29)
        Me.MaterialLabel4.TabIndex = 0
        Me.MaterialLabel4.Text = "STATUS"
        '
        'MaterialLabel3
        '
        Me.MaterialLabel3.AutoSize = True
        Me.MaterialLabel3.Depth = 0
        Me.MaterialLabel3.Font = New System.Drawing.Font("Roboto", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.MaterialLabel3.FontType = MaterialSkin.MaterialSkinManager.fontType.H5
        Me.MaterialLabel3.Location = New System.Drawing.Point(613, 121)
        Me.MaterialLabel3.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialLabel3.Name = "MaterialLabel3"
        Me.MaterialLabel3.Size = New System.Drawing.Size(159, 29)
        Me.MaterialLabel3.TabIndex = 0
        Me.MaterialLabel3.Text = "Make Changes"
        '
        'MaterialLabel1
        '
        Me.MaterialLabel1.AutoSize = True
        Me.MaterialLabel1.Depth = 0
        Me.MaterialLabel1.Font = New System.Drawing.Font("Roboto", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.MaterialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.H5
        Me.MaterialLabel1.Location = New System.Drawing.Point(37, 83)
        Me.MaterialLabel1.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialLabel1.Name = "MaterialLabel1"
        Me.MaterialLabel1.Size = New System.Drawing.Size(77, 29)
        Me.MaterialLabel1.TabIndex = 0
        Me.MaterialLabel1.Text = "Name: "
        '
        'MaterialLabel2
        '
        Me.MaterialLabel2.AutoSize = True
        Me.MaterialLabel2.Depth = 0
        Me.MaterialLabel2.Font = New System.Drawing.Font("Roboto", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.MaterialLabel2.FontType = MaterialSkin.MaterialSkinManager.fontType.H5
        Me.MaterialLabel2.Location = New System.Drawing.Point(37, 121)
        Me.MaterialLabel2.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialLabel2.Name = "MaterialLabel2"
        Me.MaterialLabel2.Size = New System.Drawing.Size(117, 29)
        Me.MaterialLabel2.TabIndex = 0
        Me.MaterialLabel2.Text = "Receipt ID:"
        '
        'SetAllCollection
        '
        Me.SetAllCollection.AutoResize = False
        Me.SetAllCollection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SetAllCollection.Depth = 0
        Me.SetAllCollection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.SetAllCollection.DropDownHeight = 174
        Me.SetAllCollection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SetAllCollection.DropDownWidth = 121
        Me.SetAllCollection.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.SetAllCollection.ForeColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SetAllCollection.FormattingEnabled = True
        Me.SetAllCollection.IntegralHeight = False
        Me.SetAllCollection.ItemHeight = 43
        Me.SetAllCollection.Items.AddRange(New Object() {"Active", "Unreturned", "Returned"})
        Me.SetAllCollection.Location = New System.Drawing.Point(179, 414)
        Me.SetAllCollection.MaxDropDownItems = 4
        Me.SetAllCollection.MouseState = MaterialSkin.MouseState.OUT
        Me.SetAllCollection.Name = "SetAllCollection"
        Me.SetAllCollection.Size = New System.Drawing.Size(182, 49)
        Me.SetAllCollection.StartIndex = 0
        Me.SetAllCollection.TabIndex = 5
        '
        'SetAllBtn
        '
        Me.SetAllBtn.AutoSize = False
        Me.SetAllBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.SetAllBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.[Default]
        Me.SetAllBtn.Depth = 0
        Me.SetAllBtn.HighEmphasis = True
        Me.SetAllBtn.Icon = Nothing
        Me.SetAllBtn.Location = New System.Drawing.Point(37, 414)
        Me.SetAllBtn.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.SetAllBtn.MouseState = MaterialSkin.MouseState.HOVER
        Me.SetAllBtn.Name = "SetAllBtn"
        Me.SetAllBtn.NoAccentTextColor = System.Drawing.Color.Empty
        Me.SetAllBtn.Size = New System.Drawing.Size(117, 50)
        Me.SetAllBtn.TabIndex = 6
        Me.SetAllBtn.Text = "Set all as"
        Me.SetAllBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained
        Me.SetAllBtn.UseAccentColor = False
        Me.SetAllBtn.UseVisualStyleBackColor = True
        '
        'SaveBtn
        '
        Me.SaveBtn.AutoSize = False
        Me.SaveBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.SaveBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.[Default]
        Me.SaveBtn.Depth = 0
        Me.SaveBtn.HighEmphasis = True
        Me.SaveBtn.Icon = Nothing
        Me.SaveBtn.Location = New System.Drawing.Point(613, 414)
        Me.SaveBtn.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.SaveBtn.MouseState = MaterialSkin.MouseState.HOVER
        Me.SaveBtn.Name = "SaveBtn"
        Me.SaveBtn.NoAccentTextColor = System.Drawing.Color.Empty
        Me.SaveBtn.Size = New System.Drawing.Size(159, 49)
        Me.SaveBtn.TabIndex = 6
        Me.SaveBtn.Text = "Save"
        Me.SaveBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained
        Me.SaveBtn.UseAccentColor = False
        Me.SaveBtn.UseVisualStyleBackColor = True
        '
        'RentDetailsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 473)
        Me.Controls.Add(Me.SaveBtn)
        Me.Controls.Add(Me.SetAllBtn)
        Me.Controls.Add(Me.SetAllCollection)
        Me.Controls.Add(Me.MaterialCard1)
        Me.Controls.Add(Me.MaterialLabel3)
        Me.Controls.Add(Me.MaterialListView1)
        Me.Controls.Add(Me.MaterialLabel1)
        Me.Controls.Add(Me.NameOfCustomer)
        Me.Controls.Add(Me.MaterialLabel2)
        Me.Controls.Add(Me.ReceiptID)
        Me.Name = "RentDetailsForm"
        Me.Text = "RentDetailsForm"
        Me.MaterialCard1.ResumeLayout(False)
        Me.MaterialCard1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ReceiptID As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents NameOfCustomer As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents MaterialListView1 As MaterialSkin.Controls.MaterialListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents StatusSelection As MaterialSkin.Controls.MaterialComboBox
    Friend WithEvents ConfrmChanges As MaterialSkin.Controls.MaterialButton
    Friend WithEvents MaterialCard1 As MaterialSkin.Controls.MaterialCard
    Friend WithEvents MaterialLabel3 As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents MaterialLabel4 As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents MaterialLabel1 As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents MaterialLabel2 As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents SetAllCollection As MaterialSkin.Controls.MaterialComboBox
    Friend WithEvents SetAllBtn As MaterialSkin.Controls.MaterialButton
    Friend WithEvents SaveBtn As MaterialSkin.Controls.MaterialButton
End Class
