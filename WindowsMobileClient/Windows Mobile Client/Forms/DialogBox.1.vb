Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.ComponentModel
Imports activiser.Library.Forms

Public Enum IconList
    [Error]
    [Info]
    [Warning]
    [Question]
    [NoEntry]
    [Lock]
    [Keys]
    [None] = Integer.MaxValue
End Enum

Public Class DialogBox
    Friend Const MODULENAME As String = "DialogBox"

    Private _controlledClose As Boolean

    Public Sub New(ByVal owner As Form, ByVal message As String, ByVal buttons As Windows.Forms.MessageBoxButtons, ByVal icon As IconList)
        Me.InitializeComponent()
        Me.Message = message
        Me.Buttons = buttons
        Me.IsInputBox = False
        Me.IconStyle = icon
        If owner IsNot Nothing Then
            Me.Owner = owner
        End If
    End Sub

    Public Sub New(ByVal owner As Form, ByVal message As String, ByVal buttons As Windows.Forms.MessageBoxButtons, ByVal icon As Windows.Forms.MessageBoxIcon)
        Me.InitializeComponent()
        Me.Message = message
        Me.Buttons = buttons
        Me.IsInputBox = False
        If owner IsNot Nothing Then
            Me.Owner = owner
        End If
        Select Case icon
            Case MessageBoxIcon.Asterisk
                Me.IconStyle = IconList.Info
            Case MessageBoxIcon.Exclamation
                Me.IconStyle = IconList.Warning
            Case MessageBoxIcon.Hand
                Me.IconStyle = IconList.Error
            Case MessageBoxIcon.Question
                Me.IconStyle = IconList.Question
            Case MessageBoxIcon.None
                Me.IconStyle = IconList.None
        End Select
    End Sub

    Public Sub New(ByVal owner As Form, ByVal message As String, ByVal defaultValue As String)
        Me.New(owner, message, defaultValue, IconList.Question, True, 0)
    End Sub

    Public Sub New(ByVal owner As Form, ByVal message As String, ByVal defaultValue As String, ByVal icon As IconList, ByVal multiline As Boolean, ByVal maxLength As Integer)
        Me.InitializeComponent()
        Me.Message = message
        Me.Buttons = MessageBoxButtons.OKCancel
        Me.IconStyle = icon
        Me.IsInputBox = True
        Me.Multiline = multiline
        Me.MaxLength = maxLength
        Me.Input = defaultValue
        If owner IsNot Nothing Then
            Me.Owner = owner
        End If
    End Sub

    Public Sub New(ByVal owner As Form, ByVal message As String, ByVal defaultValue As String, ByVal icon As MessageBoxIcon, ByVal multiline As Boolean, ByVal maxLength As Integer)
        Me.InitializeComponent()
        Me.Message = message
        Me.Buttons = MessageBoxButtons.OKCancel
        Select Case icon
            Case MessageBoxIcon.Asterisk
                Me.IconStyle = IconList.Info
            Case MessageBoxIcon.Exclamation
                Me.IconStyle = IconList.Warning
            Case MessageBoxIcon.Hand
                Me.IconStyle = IconList.Error
            Case MessageBoxIcon.Question
                Me.IconStyle = IconList.Question
            Case MessageBoxIcon.None
                Me.IconStyle = IconList.None
        End Select
        Me.IsInputBox = True
        Me.Multiline = multiline
        Me.MaxLength = maxLength
        Me.Input = defaultValue
        If owner IsNot Nothing Then
            Me.Owner = owner
        End If
    End Sub

    Private _iconStyle As IconList
    Public Property IconStyle() As IconList
        Get
            Return _iconStyle
        End Get
        Set(ByVal value As IconList)
            _iconStyle = value
            Dim image As Bitmap = Nothing

            If Me.Height > 320 Then
                Select Case value
                    Case IconList.Error
                        image = My.Resources.ErrorIcon48
                    Case IconList.Info
                        image = My.Resources.Info48
                    Case IconList.Question
                        image = My.Resources.QuestionIcon48
                    Case IconList.Warning
                        image = My.Resources.WarningIcon48
                    Case IconList.Keys
                        image = My.Resources.KeysIcon48
                    Case IconList.NoEntry
                        image = My.Resources.NoEntryIcon48
                    Case IconList.Lock
                        image = My.Resources.LockIcon48
                End Select
            Else
                Select Case value
                    Case IconList.Error
                        image = My.Resources.ErrorIcon32
                    Case IconList.Info
                        image = My.Resources.Info32
                    Case IconList.Question
                        image = My.Resources.QuestionIcon32
                    Case IconList.Warning
                        image = My.Resources.WarningIcon32
                    Case IconList.Keys
                        image = My.Resources.KeysIcon32
                    Case IconList.NoEntry
                        image = My.Resources.NoEntryIcon32
                    Case IconList.Lock
                        image = My.Resources.LockIcon32
                End Select
            End If
            If image IsNot Nothing Then
                GraphicsUtilities.RecolorBitmap(image, New Color() {Color.Magenta}, New Color() {Me.IconBox.BackColor})
                Me.IconBox.Image = image
                Me.IconBox.Visible = True
            Else
                Me.IconBox.Visible = False
            End If
        End Set
    End Property


    Private _isInputBox As Boolean
    <DefaultValue(False)> _
    Public Property IsInputBox() As Boolean
        Get
            Return _isInputBox
        End Get
        Set(ByVal value As Boolean)
            _isInputBox = value
            Me.TextInput.Visible = _isInputBox
            If value Then
                Me.Caption.Dock = DockStyle.Top
            Else
                Me.Caption.Dock = DockStyle.Fill
            End If
        End Set
    End Property

    Public Property MaxLength() As Integer
        Get
            Return Me.TextInput.MaxLength
        End Get
        Set(ByVal value As Integer)
            Me.TextInput.MaxLength = value
        End Set
    End Property

    Public Property Multiline() As Boolean
        Get
            Return Me.TextInput.Multiline
        End Get
        Set(ByVal value As Boolean)
            Me.TextInput.Multiline = value
            'SetCaptionHeight()
        End Set
    End Property

    Public Property Input() As String
        Get
            Return Me.TextInput.Text
        End Get
        Set(ByVal value As String)
            Me.TextInput.Text = value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return Caption.Text
        End Get
        Set(ByVal value As String)
            Caption.Text = value
        End Set
    End Property

    Public Property CaptionAlign() As Drawing.ContentAlignment
        Get
            Return Me.Caption.TextAlign
        End Get
        Set(ByVal value As Drawing.ContentAlignment)
            Me.Caption.TextAlign = value
        End Set
    End Property

    Private _buttons As Windows.Forms.MessageBoxButtons
    Friend WithEvents MainMenu As System.Windows.Forms.MenuItem
    Friend WithEvents Button2 As System.Windows.Forms.MenuItem
    Friend WithEvents Button3 As System.Windows.Forms.MenuItem

    Private Delegate Sub SetButtonsDelegate()

    Private Sub AddMenu()
        Me.MainMenu = New System.Windows.Forms.MenuItem
        Me.MainMenu.Text = Terminology.GetString(MODULENAME, RES_Menu)
        Me.MenuStrip.MenuItems.Add(Me.MainMenu)
    End Sub

    Private Sub AddIgnoreButton()
        Me.Button2 = New System.Windows.Forms.MenuItem()
        Me.Button2.Text = Terminology.GetString(MODULENAME, RES_Ignore)
        AddHandler Me.Button2.Click, AddressOf IgnoreButton_Click
        If Me.MainMenu Is Nothing Then
            Me.MenuStrip.MenuItems.Add(Me.Button2)
        Else
            Me.MainMenu.MenuItems.Add(Me.Button2)
        End If
    End Sub

    Private Sub AddNoButton()
        Me.Button2 = New System.Windows.Forms.MenuItem()
        Me.Button2.Text = Terminology.GetString(MODULENAME, RES_No)
        AddHandler Me.Button2.Click, AddressOf NoButton_Click
        If Me.MainMenu Is Nothing Then
            Me.MenuStrip.MenuItems.Add(Me.Button2)
        Else
            Me.MainMenu.MenuItems.Add(Me.Button2)
        End If
    End Sub

    Private Sub AddAbortButton()
        Me.Button3 = New System.Windows.Forms.MenuItem()
        Me.Button3.Text = Terminology.GetString(MODULENAME, RES_Abort)
        AddHandler Me.Button3.Click, AddressOf AbortButton_Click
        If Me.MainMenu Is Nothing Then
            Me.MenuStrip.MenuItems.Add(Button3)
        Else
            Me.MainMenu.MenuItems.Add(Me.Button3)
        End If
    End Sub

    Private Sub AddCancelButton()
        Me.Button3 = New System.Windows.Forms.MenuItem()
        Me.Button3.Text = Terminology.GetString(MODULENAME, RES_Cancel)
        AddHandler Me.Button3.Click, AddressOf CancelButton_Click
        If Me.MainMenu Is Nothing Then
            Me.MenuStrip.MenuItems.Add(Button3)
        Else
            Me.MainMenu.MenuItems.Add(Me.Button3)
        End If
    End Sub

    Private Sub AddOkButton()
        Me.OkYesButton.Text = Terminology.GetString(MODULENAME, RES_OK)
        AddHandler Me.OkYesButton.Click, AddressOf OkButton_Click
    End Sub

    Private Sub AddRetryButton()
        Me.OkYesButton.Text = Terminology.GetString(MODULENAME, RES_Retry)
        AddHandler Me.OkYesButton.Click, AddressOf RetryButton_Click
    End Sub

    Private Sub AddYesButton()
        Me.OkYesButton.Text = Terminology.GetString(MODULENAME, RES_Yes)
        AddHandler Me.OkYesButton.Click, AddressOf YesButton_Click
    End Sub

    Private Sub SetButtons()
        If Me.InvokeRequired Then
            Dim d As New SetButtonsDelegate(AddressOf SetButtons)
            Me.Invoke(d)
            Return
        End If

        Select Case _buttons
            Case MessageBoxButtons.AbortRetryIgnore
                AddRetryButton()
                AddMenu()
                AddIgnoreButton()
                AddAbortButton()
            Case MessageBoxButtons.OK
                AddOkButton()
            Case MessageBoxButtons.OKCancel
                AddOkButton()
                AddCancelButton()
            Case MessageBoxButtons.RetryCancel
                AddRetryButton()
                AddCancelButton()
            Case MessageBoxButtons.YesNo
                AddYesButton()
                AddNoButton()
            Case MessageBoxButtons.YesNoCancel
                AddYesButton()
                AddMenu()
                AddNoButton()
                AddCancelButton()
        End Select
    End Sub

    Public Property Buttons() As Windows.Forms.MessageBoxButtons
        Get
            Return _buttons
        End Get
        Private Set(ByVal value As Windows.Forms.MessageBoxButtons)
            _buttons = value
            'SetButtons()
        End Set
    End Property

    Private Sub OkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ControlledClose(Windows.Forms.DialogResult.OK)
    End Sub

    Private Sub YesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ControlledClose(Windows.Forms.DialogResult.Yes)
    End Sub

    Private Sub RetryButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ControlledClose(Windows.Forms.DialogResult.Retry)
    End Sub

    Private Sub AbortButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ControlledClose(Windows.Forms.DialogResult.Abort)
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ControlledClose(Windows.Forms.DialogResult.Cancel)
    End Sub

    Private Sub NoButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ControlledClose(Windows.Forms.DialogResult.No)
    End Sub

    Private Sub IgnoreButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ControlledClose(Windows.Forms.DialogResult.Ignore)
    End Sub

    Private Sub ControlledClose(ByVal result As DialogResult)
        _controlledClose = True
        Me.DialogResult = result
        If Me.IsInputBox Then
            If result = Windows.Forms.DialogResult.Cancel OrElse result = Windows.Forms.DialogResult.No Then
                Input = String.Empty
            End If

            'Else
            'Me.Close()
        End If
        Me.Hide()
    End Sub

    Private Sub DialogBox_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Not _controlledClose Then ' assume 'OK' button pressed.
            If Me.IsInputBox Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Hide()
            Else
                Dim result As DialogResult
                Select Case _buttons
                    Case MessageBoxButtons.OK
                        result = Windows.Forms.DialogResult.OK
                    Case MessageBoxButtons.OKCancel
                        result = Windows.Forms.DialogResult.OK
                    Case MessageBoxButtons.YesNo
                        result = Windows.Forms.DialogResult.Yes
                    Case MessageBoxButtons.YesNoCancel
                        result = Windows.Forms.DialogResult.Yes
                    Case MessageBoxButtons.AbortRetryIgnore
                        result = Windows.Forms.DialogResult.Retry
                    Case MessageBoxButtons.RetryCancel
                        result = Windows.Forms.DialogResult.Retry
                End Select
                Me.DialogResult = result
            End If
        End If
    End Sub

#If WINDOWSMOBILE Then
    Private Sub InputPanel_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InputPanel.EnabledChanged
        InputPanelSwitch(False)
    End Sub

    Private Sub InputPanelSwitch(Optional ByVal loadInitial As Boolean = False)
        Try
            If loadInitial Then
                InputPanel.Enabled = ConfigurationSettings.GetValue(MODULENAME & My.Resources.InputPanelEnabledRegValueName, False)
                Me.InputPanelPanel.Visible = InputPanel.Enabled
                Me.InputPanelPanel.Height = InputPanel.Bounds.Height
            Else
                Me.InputPanelPanel.Height = InputPanel.Bounds.Height
                Me.InputPanelPanel.Visible = InputPanel.Enabled
                ConfigurationSettings.SetValue(MODULENAME & My.Resources.InputPanelEnabledRegValueName, Me.InputPanel.Enabled)
            End If
            'Application.DoEvents()
            Me.Refresh()
        Catch ex As ObjectDisposedException
            ' who cares
        Catch ex As Exception
            Const METHODNAME As String = "InputPanelSwitch"
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    Private Sub EnableInputPanelOnFormLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InputPanelSwitch(True)
    End Sub
#End If
    Private Sub DialogBox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetButtons()
#If Not WINDOWSMOBILE Then
        EnableContextMenus(Me.Controls)
#End If
        Me.BringToFront()
        Me.Refresh()
    End Sub
End Class