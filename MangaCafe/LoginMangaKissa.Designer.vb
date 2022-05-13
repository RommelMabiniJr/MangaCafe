<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginMangaKissa
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoginMangaKissa))
        Me.UserEmailTxt = New MaterialSkin.Controls.MaterialTextBox()
        Me.MaterialLabel1 = New MaterialSkin.Controls.MaterialLabel()
        Me.MaterialLabel2 = New MaterialSkin.Controls.MaterialLabel()
        Me.MaterialCard1 = New MaterialSkin.Controls.MaterialCard()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.MaterialLabel3 = New MaterialSkin.Controls.MaterialLabel()
        Me.MaterialLabel4 = New MaterialSkin.Controls.MaterialLabel()
        Me.ShowPass = New MaterialSkin.Controls.MaterialCheckbox()
        Me.LoginMKissaBtn = New MaterialSkin.Controls.MaterialButton()
        Me.PassTxt = New MaterialSkin.Controls.MaterialTextBox2()
        Me.MaterialCard1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UserEmailTxt
        '
        Me.UserEmailTxt.AnimateReadOnly = False
        Me.UserEmailTxt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.UserEmailTxt.Depth = 0
        Me.UserEmailTxt.Font = New System.Drawing.Font("Roboto", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.UserEmailTxt.LeadingIcon = Nothing
        Me.UserEmailTxt.Location = New System.Drawing.Point(559, 276)
        Me.UserEmailTxt.MaxLength = 50
        Me.UserEmailTxt.MouseState = MaterialSkin.MouseState.OUT
        Me.UserEmailTxt.Multiline = False
        Me.UserEmailTxt.Name = "UserEmailTxt"
        Me.UserEmailTxt.Size = New System.Drawing.Size(240, 50)
        Me.UserEmailTxt.TabIndex = 0
        Me.UserEmailTxt.Text = ""
        Me.UserEmailTxt.TrailingIcon = Nothing
        '
        'MaterialLabel1
        '
        Me.MaterialLabel1.AutoSize = True
        Me.MaterialLabel1.Depth = 0
        Me.MaterialLabel1.Font = New System.Drawing.Font("Roboto Medium", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.MaterialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.H6
        Me.MaterialLabel1.Location = New System.Drawing.Point(359, 291)
        Me.MaterialLabel1.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialLabel1.Name = "MaterialLabel1"
        Me.MaterialLabel1.Size = New System.Drawing.Size(172, 24)
        Me.MaterialLabel1.TabIndex = 1
        Me.MaterialLabel1.Text = "USERNAME/EMAIL"
        '
        'MaterialLabel2
        '
        Me.MaterialLabel2.AutoSize = True
        Me.MaterialLabel2.Depth = 0
        Me.MaterialLabel2.Font = New System.Drawing.Font("Roboto Medium", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        Me.MaterialLabel2.FontType = MaterialSkin.MaterialSkinManager.fontType.H6
        Me.MaterialLabel2.Location = New System.Drawing.Point(397, 368)
        Me.MaterialLabel2.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialLabel2.Name = "MaterialLabel2"
        Me.MaterialLabel2.Size = New System.Drawing.Size(108, 24)
        Me.MaterialLabel2.TabIndex = 1
        Me.MaterialLabel2.Text = "PASSWORD"
        '
        'MaterialCard1
        '
        Me.MaterialCard1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MaterialCard1.Controls.Add(Me.PictureBox1)
        Me.MaterialCard1.Controls.Add(Me.PictureBox2)
        Me.MaterialCard1.Depth = 0
        Me.MaterialCard1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.MaterialCard1.Location = New System.Drawing.Point(49, 120)
        Me.MaterialCard1.Margin = New System.Windows.Forms.Padding(14)
        Me.MaterialCard1.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialCard1.Name = "MaterialCard1"
        Me.MaterialCard1.Padding = New System.Windows.Forms.Padding(14)
        Me.MaterialCard1.Size = New System.Drawing.Size(241, 303)
        Me.MaterialCard1.TabIndex = 3
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(44, 20)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(151, 158)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(55, 169)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(133, 113)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 0
        Me.PictureBox2.TabStop = False
        '
        'MaterialLabel3
        '
        Me.MaterialLabel3.AutoSize = True
        Me.MaterialLabel3.Depth = 0
        Me.MaterialLabel3.Font = New System.Drawing.Font("Roboto Light", 96.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.MaterialLabel3.FontType = MaterialSkin.MaterialSkinManager.fontType.H1
        Me.MaterialLabel3.Location = New System.Drawing.Point(348, 64)
        Me.MaterialLabel3.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialLabel3.Name = "MaterialLabel3"
        Me.MaterialLabel3.Size = New System.Drawing.Size(524, 115)
        Me.MaterialLabel3.TabIndex = 4
        Me.MaterialLabel3.Text = "MangaKissa"
        '
        'MaterialLabel4
        '
        Me.MaterialLabel4.AutoSize = True
        Me.MaterialLabel4.Depth = 0
        Me.MaterialLabel4.Font = New System.Drawing.Font("Roboto Light", 60.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.MaterialLabel4.FontType = MaterialSkin.MaterialSkinManager.fontType.H2
        Me.MaterialLabel4.Location = New System.Drawing.Point(534, 175)
        Me.MaterialLabel4.MouseState = MaterialSkin.MouseState.HOVER
        Me.MaterialLabel4.Name = "MaterialLabel4"
        Me.MaterialLabel4.Size = New System.Drawing.Size(123, 72)
        Me.MaterialLabel4.TabIndex = 5
        Me.MaterialLabel4.Text = "Cafe"
        '
        'ShowPass
        '
        Me.ShowPass.AutoSize = True
        Me.ShowPass.Depth = 0
        Me.ShowPass.Location = New System.Drawing.Point(560, 415)
        Me.ShowPass.Margin = New System.Windows.Forms.Padding(0)
        Me.ShowPass.MouseLocation = New System.Drawing.Point(-1, -1)
        Me.ShowPass.MouseState = MaterialSkin.MouseState.HOVER
        Me.ShowPass.Name = "ShowPass"
        Me.ShowPass.ReadOnly = False
        Me.ShowPass.Ripple = True
        Me.ShowPass.Size = New System.Drawing.Size(149, 37)
        Me.ShowPass.TabIndex = 6
        Me.ShowPass.Text = "Show Password"
        Me.ShowPass.UseVisualStyleBackColor = True
        '
        'LoginMKissaBtn
        '
        Me.LoginMKissaBtn.AutoSize = False
        Me.LoginMKissaBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.LoginMKissaBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.[Default]
        Me.LoginMKissaBtn.Depth = 0
        Me.LoginMKissaBtn.HighEmphasis = True
        Me.LoginMKissaBtn.Icon = Nothing
        Me.LoginMKissaBtn.Location = New System.Drawing.Point(559, 468)
        Me.LoginMKissaBtn.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.LoginMKissaBtn.MouseState = MaterialSkin.MouseState.HOVER
        Me.LoginMKissaBtn.Name = "LoginMKissaBtn"
        Me.LoginMKissaBtn.NoAccentTextColor = System.Drawing.Color.Empty
        Me.LoginMKissaBtn.Size = New System.Drawing.Size(237, 54)
        Me.LoginMKissaBtn.TabIndex = 7
        Me.LoginMKissaBtn.Text = "LOGIN"
        Me.LoginMKissaBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained
        Me.LoginMKissaBtn.UseAccentColor = False
        Me.LoginMKissaBtn.UseVisualStyleBackColor = True
        '
        'PassTxt
        '
        Me.PassTxt.AnimateReadOnly = False
        Me.PassTxt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PassTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.PassTxt.Depth = 0
        Me.PassTxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.PassTxt.HideSelection = True
        Me.PassTxt.LeadingIcon = Nothing
        Me.PassTxt.Location = New System.Drawing.Point(559, 354)
        Me.PassTxt.MaxLength = 32767
        Me.PassTxt.MouseState = MaterialSkin.MouseState.OUT
        Me.PassTxt.Name = "PassTxt"
        Me.PassTxt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.PassTxt.PrefixSuffixText = Nothing
        Me.PassTxt.ReadOnly = False
        Me.PassTxt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PassTxt.SelectedText = ""
        Me.PassTxt.SelectionLength = 0
        Me.PassTxt.SelectionStart = 0
        Me.PassTxt.ShortcutsEnabled = True
        Me.PassTxt.Size = New System.Drawing.Size(240, 48)
        Me.PassTxt.TabIndex = 8
        Me.PassTxt.TabStop = False
        Me.PassTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.PassTxt.TrailingIcon = Nothing
        Me.PassTxt.UseSystemPasswordChar = False
        '
        'LoginMangaKissa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(917, 540)
        Me.Controls.Add(Me.PassTxt)
        Me.Controls.Add(Me.LoginMKissaBtn)
        Me.Controls.Add(Me.ShowPass)
        Me.Controls.Add(Me.MaterialLabel4)
        Me.Controls.Add(Me.MaterialLabel3)
        Me.Controls.Add(Me.MaterialCard1)
        Me.Controls.Add(Me.MaterialLabel2)
        Me.Controls.Add(Me.MaterialLabel1)
        Me.Controls.Add(Me.UserEmailTxt)
        Me.Name = "LoginMangaKissa"
        Me.Text = "MangaKissa Login"
        Me.MaterialCard1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UserEmailTxt As MaterialSkin.Controls.MaterialTextBox
    Friend WithEvents MaterialLabel1 As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents MaterialLabel2 As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents MaterialCard1 As MaterialSkin.Controls.MaterialCard
    Friend WithEvents MaterialLabel3 As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents MaterialLabel4 As MaterialSkin.Controls.MaterialLabel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents ShowPass As MaterialSkin.Controls.MaterialCheckbox
    Friend WithEvents LoginMKissaBtn As MaterialSkin.Controls.MaterialButton
    Friend WithEvents PassTxt As MaterialSkin.Controls.MaterialTextBox2
End Class
