Public Class MainForm
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents grpLicenseDetails As System.Windows.Forms.GroupBox
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents lblUsers As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblModules As System.Windows.Forms.Label
    Friend WithEvents lblProduct As System.Windows.Forms.Label
    Friend WithEvents ExpiryDatePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnGenerateKey As System.Windows.Forms.Button
    Friend WithEvents grpExtractDetails As System.Windows.Forms.GroupBox
    Friend WithEvents btnGenerateDetails As System.Windows.Forms.Button
    Friend WithEvents lblResults As System.Windows.Forms.Label
    Friend WithEvents ProductComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Users As System.Windows.Forms.ComboBox
    Friend WithEvents Modules As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblClientName As System.Windows.Forms.Label
    Friend WithEvents ClientName As System.Windows.Forms.TextBox
    Friend WithEvents txtClientNameCheck As System.Windows.Forms.TextBox
    Friend WithEvents lblLicenseKey As System.Windows.Forms.Label
    Friend WithEvents txtLicenseKeyCheck As System.Windows.Forms.TextBox
    Friend WithEvents GeneratedLicenseKey As System.Windows.Forms.TextBox
    Friend WithEvents lblClientNameCheck As System.Windows.Forms.Label
    Friend WithEvents cmbProductCheck As System.Windows.Forms.ComboBox
    Friend WithEvents lblProductCheck As System.Windows.Forms.Label
    Friend WithEvents btnCopyClientName As System.Windows.Forms.Button
    Friend WithEvents btnCopyLicenseKey As System.Windows.Forms.Button
    Friend WithEvents EncodeProductSet As activiserProductSet
    Friend WithEvents DecodeProductSet As actliviser.licensing.activiserProductSet
    Friend WithEvents Version As System.Windows.Forms.NumericUpDown
    Friend WithEvents CopyGeneratedKey As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Addressee As System.Windows.Forms.TextBox
    Friend WithEvents MessageText As System.Windows.Forms.RichTextBox
    Friend WithEvents MessageGroup As System.Windows.Forms.GroupBox
    Friend WithEvents CopyMessageToClipboard As System.Windows.Forms.Button
    Friend WithEvents MakeMessageButton As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.grpLicenseDetails = New System.Windows.Forms.GroupBox
        Me.MakeMessageButton = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.CopyGeneratedKey = New System.Windows.Forms.Button
        Me.Addressee = New System.Windows.Forms.TextBox
        Me.Version = New System.Windows.Forms.NumericUpDown
        Me.GeneratedLicenseKey = New System.Windows.Forms.TextBox
        Me.ClientName = New System.Windows.Forms.TextBox
        Me.lblClientName = New System.Windows.Forms.Label
        Me.Modules = New System.Windows.Forms.NumericUpDown
        Me.ProductComboBox = New System.Windows.Forms.ComboBox
        Me.EncodeProductSet = New actliviser.licensing.activiserProductSet
        Me.btnGenerateKey = New System.Windows.Forms.Button
        Me.Users = New System.Windows.Forms.ComboBox
        Me.ExpiryDatePicker = New System.Windows.Forms.DateTimePicker
        Me.lblProduct = New System.Windows.Forms.Label
        Me.lblModules = New System.Windows.Forms.Label
        Me.lblVersion = New System.Windows.Forms.Label
        Me.lblUsers = New System.Windows.Forms.Label
        Me.lblDate = New System.Windows.Forms.Label
        Me.grpExtractDetails = New System.Windows.Forms.GroupBox
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnCopyLicenseKey = New System.Windows.Forms.Button
        Me.cmbProductCheck = New System.Windows.Forms.ComboBox
        Me.DecodeProductSet = New actliviser.licensing.activiserProductSet
        Me.lblProductCheck = New System.Windows.Forms.Label
        Me.lblLicenseKey = New System.Windows.Forms.Label
        Me.txtClientNameCheck = New System.Windows.Forms.TextBox
        Me.lblClientNameCheck = New System.Windows.Forms.Label
        Me.lblResults = New System.Windows.Forms.Label
        Me.btnGenerateDetails = New System.Windows.Forms.Button
        Me.txtLicenseKeyCheck = New System.Windows.Forms.TextBox
        Me.btnCopyClientName = New System.Windows.Forms.Button
        Me.MessageText = New System.Windows.Forms.RichTextBox
        Me.MessageGroup = New System.Windows.Forms.GroupBox
        Me.CopyMessageToClipboard = New System.Windows.Forms.Button
        Me.grpLicenseDetails.SuspendLayout()
        CType(Me.Version, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Modules, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EncodeProductSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpExtractDetails.SuspendLayout()
        CType(Me.DecodeProductSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MessageGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpLicenseDetails
        '
        Me.grpLicenseDetails.Controls.Add(Me.MakeMessageButton)
        Me.grpLicenseDetails.Controls.Add(Me.Label2)
        Me.grpLicenseDetails.Controls.Add(Me.Label1)
        Me.grpLicenseDetails.Controls.Add(Me.CopyGeneratedKey)
        Me.grpLicenseDetails.Controls.Add(Me.Addressee)
        Me.grpLicenseDetails.Controls.Add(Me.Version)
        Me.grpLicenseDetails.Controls.Add(Me.GeneratedLicenseKey)
        Me.grpLicenseDetails.Controls.Add(Me.ClientName)
        Me.grpLicenseDetails.Controls.Add(Me.lblClientName)
        Me.grpLicenseDetails.Controls.Add(Me.Modules)
        Me.grpLicenseDetails.Controls.Add(Me.ProductComboBox)
        Me.grpLicenseDetails.Controls.Add(Me.btnGenerateKey)
        Me.grpLicenseDetails.Controls.Add(Me.Users)
        Me.grpLicenseDetails.Controls.Add(Me.ExpiryDatePicker)
        Me.grpLicenseDetails.Controls.Add(Me.lblProduct)
        Me.grpLicenseDetails.Controls.Add(Me.lblModules)
        Me.grpLicenseDetails.Controls.Add(Me.lblVersion)
        Me.grpLicenseDetails.Controls.Add(Me.lblUsers)
        Me.grpLicenseDetails.Controls.Add(Me.lblDate)
        Me.grpLicenseDetails.Location = New System.Drawing.Point(8, 8)
        Me.grpLicenseDetails.Name = "grpLicenseDetails"
        Me.grpLicenseDetails.Size = New System.Drawing.Size(352, 236)
        Me.grpLicenseDetails.TabIndex = 0
        Me.grpLicenseDetails.TabStop = False
        Me.grpLicenseDetails.Text = "License Details"
        '
        'MakeMessageButton
        '
        Me.MakeMessageButton.Location = New System.Drawing.Point(231, 207)
        Me.MakeMessageButton.Name = "MakeMessageButton"
        Me.MakeMessageButton.Size = New System.Drawing.Size(104, 23)
        Me.MakeMessageButton.TabIndex = 18
        Me.MakeMessageButton.Text = "Make Message >"
        Me.MakeMessageButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 184)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Addressee:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 132)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "License Key:"
        '
        'CopyGeneratedKey
        '
        Me.CopyGeneratedKey.Location = New System.Drawing.Point(231, 155)
        Me.CopyGeneratedKey.Name = "CopyGeneratedKey"
        Me.CopyGeneratedKey.Size = New System.Drawing.Size(104, 23)
        Me.CopyGeneratedKey.TabIndex = 15
        Me.CopyGeneratedKey.Text = "Copy to Clipboard"
        '
        'Addressee
        '
        Me.Addressee.Location = New System.Drawing.Point(94, 184)
        Me.Addressee.Name = "Addressee"
        Me.Addressee.Size = New System.Drawing.Size(241, 20)
        Me.Addressee.TabIndex = 17
        '
        'Version
        '
        Me.Version.DecimalPlaces = 1
        Me.Version.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.Version.Location = New System.Drawing.Point(285, 79)
        Me.Version.Maximum = New Decimal(New Integer() {255, 0, 0, 65536})
        Me.Version.Minimum = New Decimal(New Integer() {11, 0, 0, 65536})
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(50, 20)
        Me.Version.TabIndex = 7
        Me.Version.Value = New Decimal(New Integer() {34, 0, 0, 65536})
        '
        'GeneratedLicenseKey
        '
        Me.GeneratedLicenseKey.Location = New System.Drawing.Point(94, 129)
        Me.GeneratedLicenseKey.Name = "GeneratedLicenseKey"
        Me.GeneratedLicenseKey.ReadOnly = True
        Me.GeneratedLicenseKey.Size = New System.Drawing.Size(241, 20)
        Me.GeneratedLicenseKey.TabIndex = 13
        '
        'ClientName
        '
        Me.ClientName.Location = New System.Drawing.Point(94, 24)
        Me.ClientName.Name = "ClientName"
        Me.ClientName.Size = New System.Drawing.Size(241, 20)
        Me.ClientName.TabIndex = 1
        Me.ClientName.Text = "Valued activiser™ Client"
        '
        'lblClientName
        '
        Me.lblClientName.AutoSize = True
        Me.lblClientName.Location = New System.Drawing.Point(16, 27)
        Me.lblClientName.Name = "lblClientName"
        Me.lblClientName.Size = New System.Drawing.Size(68, 13)
        Me.lblClientName.TabIndex = 0
        Me.lblClientName.Text = "Client Name:"
        '
        'Modules
        '
        Me.Modules.Location = New System.Drawing.Point(285, 103)
        Me.Modules.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.Modules.Name = "Modules"
        Me.Modules.Size = New System.Drawing.Size(50, 20)
        Me.Modules.TabIndex = 11
        Me.Modules.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'ProductComboBox
        '
        Me.ProductComboBox.DataSource = Me.EncodeProductSet
        Me.ProductComboBox.DisplayMember = "Product.Name"
        Me.ProductComboBox.Location = New System.Drawing.Point(94, 50)
        Me.ProductComboBox.Name = "ProductComboBox"
        Me.ProductComboBox.Size = New System.Drawing.Size(241, 21)
        Me.ProductComboBox.TabIndex = 3
        Me.ProductComboBox.ValueMember = "Product.ProductID"
        '
        'EncodeProductSet
        '
        Me.EncodeProductSet.DataSetName = "ProductSet"
        Me.EncodeProductSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'btnGenerateKey
        '
        Me.btnGenerateKey.Location = New System.Drawing.Point(94, 155)
        Me.btnGenerateKey.Name = "btnGenerateKey"
        Me.btnGenerateKey.Size = New System.Drawing.Size(104, 23)
        Me.btnGenerateKey.TabIndex = 14
        Me.btnGenerateKey.Text = "Generate Key"
        '
        'Users
        '
        Me.Users.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Users.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Users.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Users.Location = New System.Drawing.Point(94, 103)
        Me.Users.Name = "Users"
        Me.Users.Size = New System.Drawing.Size(101, 21)
        Me.Users.TabIndex = 9
        '
        'ExpiryDatePicker
        '
        Me.ExpiryDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.ExpiryDatePicker.Location = New System.Drawing.Point(94, 77)
        Me.ExpiryDatePicker.Name = "ExpiryDatePicker"
        Me.ExpiryDatePicker.Size = New System.Drawing.Size(101, 20)
        Me.ExpiryDatePicker.TabIndex = 5
        '
        'lblProduct
        '
        Me.lblProduct.AutoSize = True
        Me.lblProduct.Location = New System.Drawing.Point(16, 53)
        Me.lblProduct.Name = "lblProduct"
        Me.lblProduct.Size = New System.Drawing.Size(48, 13)
        Me.lblProduct.TabIndex = 2
        Me.lblProduct.Text = "Product:"
        '
        'lblModules
        '
        Me.lblModules.AutoSize = True
        Me.lblModules.Location = New System.Drawing.Point(206, 105)
        Me.lblModules.Name = "lblModules"
        Me.lblModules.Size = New System.Drawing.Size(51, 13)
        Me.lblModules.TabIndex = 10
        Me.lblModules.Text = "Package:"
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(206, 81)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(46, 13)
        Me.lblVersion.TabIndex = 6
        Me.lblVersion.Text = "Version:"
        '
        'lblUsers
        '
        Me.lblUsers.AutoSize = True
        Me.lblUsers.Location = New System.Drawing.Point(15, 105)
        Me.lblUsers.Name = "lblUsers"
        Me.lblUsers.Size = New System.Drawing.Size(38, 13)
        Me.lblUsers.TabIndex = 8
        Me.lblUsers.Text = "Users:"
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(16, 81)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(67, 13)
        Me.lblDate.TabIndex = 4
        Me.lblDate.Text = "Expiry Date:"
        '
        'grpExtractDetails
        '
        Me.grpExtractDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpExtractDetails.Controls.Add(Me.btnClear)
        Me.grpExtractDetails.Controls.Add(Me.btnCopyLicenseKey)
        Me.grpExtractDetails.Controls.Add(Me.cmbProductCheck)
        Me.grpExtractDetails.Controls.Add(Me.lblProductCheck)
        Me.grpExtractDetails.Controls.Add(Me.lblLicenseKey)
        Me.grpExtractDetails.Controls.Add(Me.txtClientNameCheck)
        Me.grpExtractDetails.Controls.Add(Me.lblClientNameCheck)
        Me.grpExtractDetails.Controls.Add(Me.lblResults)
        Me.grpExtractDetails.Controls.Add(Me.btnGenerateDetails)
        Me.grpExtractDetails.Controls.Add(Me.txtLicenseKeyCheck)
        Me.grpExtractDetails.Controls.Add(Me.btnCopyClientName)
        Me.grpExtractDetails.Location = New System.Drawing.Point(8, 250)
        Me.grpExtractDetails.Name = "grpExtractDetails"
        Me.grpExtractDetails.Size = New System.Drawing.Size(352, 244)
        Me.grpExtractDetails.TabIndex = 1
        Me.grpExtractDetails.TabStop = False
        Me.grpExtractDetails.Text = "Check Key"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(288, 104)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(48, 23)
        Me.btnClear.TabIndex = 9
        Me.btnClear.Text = "Clear"
        '
        'btnCopyLicenseKey
        '
        Me.btnCopyLicenseKey.Location = New System.Drawing.Point(288, 75)
        Me.btnCopyLicenseKey.Name = "btnCopyLicenseKey"
        Me.btnCopyLicenseKey.Size = New System.Drawing.Size(48, 23)
        Me.btnCopyLicenseKey.TabIndex = 7
        Me.btnCopyLicenseKey.Text = "Copy"
        '
        'cmbProductCheck
        '
        Me.cmbProductCheck.DataSource = Me.DecodeProductSet
        Me.cmbProductCheck.DisplayMember = "Product.Name"
        Me.cmbProductCheck.Location = New System.Drawing.Point(95, 24)
        Me.cmbProductCheck.Name = "cmbProductCheck"
        Me.cmbProductCheck.Size = New System.Drawing.Size(241, 21)
        Me.cmbProductCheck.TabIndex = 1
        Me.cmbProductCheck.ValueMember = "Product.ProductID"
        '
        'DecodeProductSet
        '
        Me.DecodeProductSet.DataSetName = "ProductSet"
        Me.DecodeProductSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'lblProductCheck
        '
        Me.lblProductCheck.AutoSize = True
        Me.lblProductCheck.Location = New System.Drawing.Point(16, 27)
        Me.lblProductCheck.Name = "lblProductCheck"
        Me.lblProductCheck.Size = New System.Drawing.Size(48, 13)
        Me.lblProductCheck.TabIndex = 0
        Me.lblProductCheck.Text = "Product:"
        '
        'lblLicenseKey
        '
        Me.lblLicenseKey.AutoSize = True
        Me.lblLicenseKey.Location = New System.Drawing.Point(16, 80)
        Me.lblLicenseKey.Name = "lblLicenseKey"
        Me.lblLicenseKey.Size = New System.Drawing.Size(67, 13)
        Me.lblLicenseKey.TabIndex = 5
        Me.lblLicenseKey.Text = "License Key:"
        '
        'txtClientNameCheck
        '
        Me.txtClientNameCheck.Location = New System.Drawing.Point(95, 51)
        Me.txtClientNameCheck.Name = "txtClientNameCheck"
        Me.txtClientNameCheck.Size = New System.Drawing.Size(185, 20)
        Me.txtClientNameCheck.TabIndex = 3
        Me.txtClientNameCheck.Text = "Valued activiser™ Client"
        '
        'lblClientNameCheck
        '
        Me.lblClientNameCheck.AutoSize = True
        Me.lblClientNameCheck.Location = New System.Drawing.Point(16, 54)
        Me.lblClientNameCheck.Name = "lblClientNameCheck"
        Me.lblClientNameCheck.Size = New System.Drawing.Size(68, 13)
        Me.lblClientNameCheck.TabIndex = 2
        Me.lblClientNameCheck.Text = "Client Name:"
        '
        'lblResults
        '
        Me.lblResults.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblResults.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.lblResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblResults.Font = New System.Drawing.Font("Lucida Console", 8.0!)
        Me.lblResults.Location = New System.Drawing.Point(16, 132)
        Me.lblResults.Name = "lblResults"
        Me.lblResults.Size = New System.Drawing.Size(320, 101)
        Me.lblResults.TabIndex = 10
        '
        'btnGenerateDetails
        '
        Me.btnGenerateDetails.Location = New System.Drawing.Point(16, 106)
        Me.btnGenerateDetails.Name = "btnGenerateDetails"
        Me.btnGenerateDetails.Size = New System.Drawing.Size(104, 23)
        Me.btnGenerateDetails.TabIndex = 8
        Me.btnGenerateDetails.Text = "Generate Details"
        '
        'txtLicenseKeyCheck
        '
        Me.txtLicenseKeyCheck.Location = New System.Drawing.Point(95, 77)
        Me.txtLicenseKeyCheck.Name = "txtLicenseKeyCheck"
        Me.txtLicenseKeyCheck.Size = New System.Drawing.Size(185, 20)
        Me.txtLicenseKeyCheck.TabIndex = 6
        '
        'btnCopyClientName
        '
        Me.btnCopyClientName.Location = New System.Drawing.Point(288, 49)
        Me.btnCopyClientName.Name = "btnCopyClientName"
        Me.btnCopyClientName.Size = New System.Drawing.Size(48, 23)
        Me.btnCopyClientName.TabIndex = 4
        Me.btnCopyClientName.Text = "Copy"
        '
        'MessageText
        '
        Me.MessageText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MessageText.Location = New System.Drawing.Point(6, 19)
        Me.MessageText.Name = "MessageText"
        Me.MessageText.Size = New System.Drawing.Size(339, 424)
        Me.MessageText.TabIndex = 0
        Me.MessageText.Text = ""
        Me.MessageText.WordWrap = False
        '
        'MessageGroup
        '
        Me.MessageGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MessageGroup.Controls.Add(Me.CopyMessageToClipboard)
        Me.MessageGroup.Controls.Add(Me.MessageText)
        Me.MessageGroup.Location = New System.Drawing.Point(366, 8)
        Me.MessageGroup.Name = "MessageGroup"
        Me.MessageGroup.Size = New System.Drawing.Size(351, 486)
        Me.MessageGroup.TabIndex = 2
        Me.MessageGroup.TabStop = False
        Me.MessageGroup.Text = "Message to Client"
        '
        'CopyMessageToClipboard
        '
        Me.CopyMessageToClipboard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CopyMessageToClipboard.Location = New System.Drawing.Point(235, 452)
        Me.CopyMessageToClipboard.Name = "CopyMessageToClipboard"
        Me.CopyMessageToClipboard.Size = New System.Drawing.Size(110, 23)
        Me.CopyMessageToClipboard.TabIndex = 1
        Me.CopyMessageToClipboard.Text = "Copy to Clipboard"
        Me.CopyMessageToClipboard.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(729, 507)
        Me.Controls.Add(Me.MessageGroup)
        Me.Controls.Add(Me.grpExtractDetails)
        Me.Controls.Add(Me.grpLicenseDetails)
        Me.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(700, 500)
        Me.Name = "frmMain"
        Me.Text = "activiser™ License Key Generator"
        Me.grpLicenseDetails.ResumeLayout(False)
        Me.grpLicenseDetails.PerformLayout()
        CType(Me.Version, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Modules, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EncodeProductSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpExtractDetails.ResumeLayout(False)
        Me.grpExtractDetails.PerformLayout()
        CType(Me.DecodeProductSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MessageGroup.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnGenerateKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateKey.Click

        'Byte 0 : Day
        'Byte 1 : Month
        'Byte 2 : Year
        'Byte 3 : Version
        'Byte 4 : Users 
        'Byte 5 : Package
        'byte 6 : License Version
        'Byte 7 : Reserved
        'Byte 8 : Parity
        'Byte 9 : Checksum

        Dim gAssemblyGuid As Guid = CType(Me.ProductComboBox.SelectedValue, Guid)

        Try
            Dim licenseCode() As Byte = New Byte(9) {}

            licenseCode(0) = CByte(ExpiryDatePicker.Value.Day)
            licenseCode(1) = CByte(ExpiryDatePicker.Value.Month)
            licenseCode(2) = CByte(ExpiryDatePicker.Value.Year - 2005) '2005 is our base year

            licenseCode(3) = CByte(CInt(Me.Version.Value * 10))
            licenseCode(4) = CType(Me.Users.SelectedItem, UserCount).Key
            licenseCode(5) = CByte(CInt(Me.Modules.Value))

            licenseCode(6) = 1

            Dim i As Integer
            Dim bParity As Byte

            For i = 0 To 7
                bParity = bParity Xor licenseCode(i)
            Next

            licenseCode(8) = bParity
            licenseCode(9) = GetArrayCheckByte(licenseCode)

            For i = 0 To 8
                licenseCode(i) = licenseCode(i) Xor licenseCode(9)
            Next

            Dim gAssemblyByteArray As Byte() = gAssemblyGuid.ToByteArray()

            For i = 0 To UBound(licenseCode)
                licenseCode(i) = (Not gAssemblyByteArray(i)) Xor licenseCode(i)
            Next

            Dim clientCodeByteArray As Byte() = StringTo10ByteArray(Me.ClientName.Text)

            For i = 0 To UBound(licenseCode)
                licenseCode(i) = clientCodeByteArray(i) Xor licenseCode(i)
            Next

            Dim s As New activiser.library.Base32
            GeneratedLicenseKey.Text = activiser.Library.Base32.Encode(licenseCode)

        Catch ex As InvalidCastException
            MsgBox("Cannot convert string to Integer", MsgBoxStyle.Critical)
        Catch ex As Exception
            MsgBox("Error", MsgBoxStyle.Critical)
        End Try

    End Sub

    Function GetArrayCheckByte(ByVal Array() As Byte) As Byte
        Dim HexString As String
        Dim lngRunningTotal As Long
        Dim MyResult As Byte
        Dim iOpratorLoopCount As Integer = 0
        Dim i As Integer

        Try
            If Array Is Nothing Then
                Return 0
            End If

            lngRunningTotal = 0
            HexString = ""

            For i = 0 To UBound(Array)
                HexString &= Hex(Array(i) * CInt((2 ^ i)))
            Next

            HexString = "g" & HexString

            For Each C As Char In HexString.ToCharArray
                If iOpratorLoopCount >= 3 Then iOpratorLoopCount = 0

                Select Case iOpratorLoopCount
                    Case 0
                        lngRunningTotal = lngRunningTotal + AscW(C)
                    Case 1
                        lngRunningTotal = lngRunningTotal * AscW(C)
                        lngRunningTotal = lngRunningTotal Mod Int32.MaxValue
                    Case 2
                        lngRunningTotal = lngRunningTotal - AscW(C)
                End Select

                iOpratorLoopCount += 1

            Next

            MyResult = CByte(lngRunningTotal Mod 251)

            Return MyResult
        Catch ex As Exception
            'Catch all exceptions
            MessageBox.Show(ex.Message & vbCrLf & _
            vbCrLf & _
            ex.GetType.FullName & vbCrLf & _
            vbCrLf & _
            "Location:" & ex.StackTrace, ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'Clean up Code

        End Try
    End Function

    Function StringTo10ByteArray(ByVal strText As String) As Byte()
        Dim MyArray(9) As Byte
        Dim intLen As Integer
        Dim FCASCII As Integer
        Dim LCASCII As Integer
        Dim ASCIISUM As Long
        Dim XCASCII As Integer
        Dim HexSum As Long
        Dim SumArray As Integer
        Dim byteOpratorLoopCount As Byte

        Try
            If strText Is Nothing OrElse strText = "" Then
                Return MyArray
            End If

            intLen = Len(strText)
            FCASCII = AscW(Microsoft.VisualBasic.Left(strText, 1))
            LCASCII = AscW(Microsoft.VisualBasic.Right(strText, 1))

            For Each C As Char In strText.ToCharArray

                If byteOpratorLoopCount >= 3 Then byteOpratorLoopCount = 0

                Select Case byteOpratorLoopCount
                    Case 0
                        ASCIISUM = ASCIISUM + AscW(C)
                    Case 1
                        ASCIISUM = ASCIISUM * AscW(C)
                        ASCIISUM = ASCIISUM Mod Int32.MaxValue
                    Case 2
                        ASCIISUM = ASCIISUM - AscW(C)
                End Select

                byteOpratorLoopCount += CByte(1)
            Next

            For Each C As Char In Hex(ASCIISUM).ToCharArray
                HexSum = HexSum + AscW(C)
            Next

            If intLen > 5 Then
                XCASCII = AscW(Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(strText, 3), 1))
            ElseIf intLen > 3 Then
                XCASCII = LCASCII
            Else
                XCASCII = FCASCII
            End If

            MyArray(0) = CByte((intLen + HexSum - FCASCII) Mod 256)
            MyArray(1) = CByte((LCASCII * FCASCII + (ASCIISUM - FCASCII)) Mod 256)
            MyArray(2) = CByte((LCASCII * ASCIISUM + (intLen Xor HexSum)) Mod 256)
            MyArray(3) = CByte((ASCIISUM + intLen) Mod 256)
            MyArray(4) = CByte((HexSum Xor ASCIISUM) Mod 256)
            MyArray(5) = CByte(XCASCII Mod 256)
            MyArray(6) = CByte((intLen + ASCIISUM + XCASCII) Mod 256)
            MyArray(7) = CByte(HexSum Mod 256)
            MyArray(8) = CByte((intLen * 217 + ASCIISUM) Mod 256)
            SumArray = (CInt(MyArray(0)) + CInt(MyArray(1)) + _
            CInt(MyArray(2)) + CInt(MyArray(3)) + CInt(MyArray(4)) _
            + CInt(MyArray(5)) + CInt(MyArray(6)) + CInt(MyArray(7)) + _
            CInt(MyArray(8)))
            MyArray(9) = CByte(SumArray Mod 256)

            Return MyArray
        Catch ex As Exception
            'Catch all exceptions
            MessageBox.Show(ex.Message & vbCrLf & _
            vbCrLf & _
            ex.GetType.FullName & vbCrLf & _
            vbCrLf & _
            "Location:" & ex.StackTrace, ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'Clean up Code

        End Try
    End Function

    Private Sub btnGenerateDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateDetails.Click
        Try

            If Not txtLicenseKeyCheck.Text.Length < 1 Then
                Dim s As New activiser.library.Base32
                Dim i As Integer

                Dim licenseCode As Byte() = DecodeFixed(txtLicenseKeyCheck.Text)
                Dim clientCodeByteArray As Byte() = StringTo10ByteArray(Me.txtClientNameCheck.Text)

                For i = 0 To UBound(licenseCode)
                    licenseCode(i) = clientCodeByteArray(i) Xor licenseCode(i)
                Next

                Dim gAssemblyGuid As Guid = CType(Me.cmbProductCheck.SelectedValue, Guid)

                Dim stLicense As New activiser.Licensing.LicenseInfo(txtLicenseKeyCheck.Text, Me.txtClientNameCheck.Text, gAssemblyGuid)

                Dim strResult As String = ""
                strResult &= "Expiry Date:" & stLicense.ExpiryDate.ToLongDateString & vbCrLf
                strResult &= "Version:" & (stLicense.Version / 10).ToString("0.0") & vbCrLf
                strResult &= "Users:" & stLicense.Users & vbCrLf
                strResult &= "Package:" & stLicense.Modules & vbCrLf

                lblResults.Text = strResult
            End If
        Catch ex As Exception
            MsgBox("Error decoding product key.", MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub btnCopyClientName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyClientName.Click
        Me.txtClientNameCheck.Text = Me.ClientName.Text
    End Sub

    Private Sub btnCopyLicenseKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyLicenseKey.Click
        Me.txtLicenseKeyCheck.Text = Me.GeneratedLicenseKey.Text
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.lblResults.Text = ""
    End Sub

    Private Function DecodeFixed(ByVal strSource As String) As Byte()
        If strSource.Length <> 16 Then
            Return Nothing
        End If

        Dim lookupTable() As Char = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray

        Dim result(9) As Byte
        Dim rb(7) As Byte
        Dim lb As Long
        Dim si, ri, x, i As Integer

        For x = 0 To 1
            lb = 0
            For i = 0 To 7
                For t As Integer = 0 To 31
                    If lookupTable(t) = strSource.Chars(si + i) Then
                        lb = lb Or (CType(t, Long) << (i * 5))
                        Exit For
                    End If
                Next
            Next

            rb = System.BitConverter.GetBytes(lb)

            For i = 0 To 4
                result(ri + i) = rb(i)
            Next
            ri = 5
            si = 8
        Next
        Return result
    End Function

    Private MessageTemplate As String

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.EncodeProductSet.ReadXml(My.Settings.ProductList)
        Me.DecodeProductSet.ReadXml(My.Settings.ProductList)
        MessageTemplate = IO.File.ReadAllText(My.Settings.TemplateMessage, System.Text.Encoding.Unicode)
        Me.MessageText.Text = MessageTemplate
        Me.ProductComboBox.SelectedIndex = 0
        If Me.EncodeProductSet.ProductVersion.Count <> 0 Then
            Dim productVersions As activiserProductSet.ProductVersionRow() = Me.EncodeProductSet.Product(0).GetProductVersionRows()
            If productVersions.Length <> 0 Then
                Me.Version.Value = CDec(productVersions(0).VersionNumber)
            End If
        End If

        Dim thisDayNextMonth As DateTime = Date.Today.AddMonths(1)
        Dim lastDayOfNextMonth As DateTime
        lastDayOfNextMonth = New DateTime(thisDayNextMonth.Year, thisDayNextMonth.Month, DateTime.DaysInMonth(thisDayNextMonth.Year, thisDayNextMonth.Month))
        Me.ExpiryDatePicker.Value = lastDayOfNextMonth
        Me.LoadUserCounts()
        Me.Users.SelectedIndex = 0
    End Sub

    Private Sub CopyGeneratedKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyGeneratedKey.Click
        Dim LicenseInfo As String
        LicenseInfo = "Client Name: " & Me.ClientName.Text & vbCrLf
        LicenseInfo &= "Product: " & Me.ProductComboBox.Text & vbCrLf
        LicenseInfo &= "License Key: " & Me.GeneratedLicenseKey.Text & vbCrLf
        LicenseInfo &= "Version V" & Me.Version.Value.ToString("0.0") & vbCrLf
        LicenseInfo &= "Users: " & CInt(Me.Users.Text).ToString & vbCrLf
        Try
            Clipboard.Clear()
            Clipboard.SetText(LicenseInfo)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MakeMessageButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakeMessageButton.Click
        Dim message As New System.Text.StringBuilder(MessageTemplate)
        message.Replace("{Addressee}", Me.Addressee.Text)
        message.Replace("{ClientName}", Me.ClientName.Text)
        message.Replace("{ProductName}", Me.ProductComboBox.Text)
        message.Replace("{Users}", Me.Users.Text)
        message.Replace("{Expiry}", Me.ExpiryDatePicker.Value.ToLongDateString)
        message.Replace("{Version}", Me.Version.Value.ToString("0.0"))
        message.Replace("{LicenseKey}", Me.GeneratedLicenseKey.Text)
        message.Replace("{Modules}", Me.Modules.Value.ToString("0"))

        Me.MessageText.Text = message.ToString
    End Sub

    Private Sub CopyMessageToClipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyMessageToClipboard.Click
        Try
            Clipboard.Clear()
            Clipboard.SetData(System.Windows.Forms.DataFormats.Rtf, Me.MessageText.Rtf)
            Clipboard.SetText(Me.MessageText.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private userList As New Collections.Generic.List(Of UserCount)

    Private Sub LoadUserCounts()
        Try
            Dim bases() As Integer = {5, 65, 320, 1550}
            Dim units() As Integer = {1, 5, 20, 50}

            Me.userList.Clear()
            Me.Users.Items.Clear()
            For b As Integer = 0 To 255
                Dim group As Integer = b >> 6
                Dim key As Integer = b And &H3F

                Dim userCountItem As actliviser.licensing.MainForm.UserCount = New UserCount(CByte(b), bases(group) + (key * units(group)))
                If (b <> 255) Then
                    If group = 0 AndAlso userCountItem.Users > 60 Then Continue For
                    If group = 1 AndAlso userCountItem.Users > 300 Then Continue For
                    If group = 2 AndAlso userCountItem.Users > 1500 Then Continue For
                    If group = 3 AndAlso userCountItem.Users > 4500 Then Continue For
                End If

                Me.userList.Add(userCountItem)
            Next

            Me.Users.Items.AddRange(Me.userList.ToArray())
            Me.Users.DisplayMember = "Users"
            Me.Users.ValueMember = "Key"
        Catch ex As Exception
            Debug.Print(ex.ToString)
        End Try

    End Sub

    Private Class UserCount
        Public Key As Byte
        Public Users As Integer

        Public Sub New(ByVal key As Byte, ByVal users As Integer)
            Me.Key = key
            Me.Users = users
        End Sub

        Public Overrides Function ToString() As String
            If Key <> 255 Then Return CStr(Me.Users)
            Return "<Unlimited>"
        End Function
    End Class

End Class
