Imports activiser.Library
Imports activiser.Library.WebService.activiserDataSet
Imports activiser.Library.WebService.ConsultantSettingDataSet

Public Class SetupForm
    Private Const MODULENAME As String = "SetupForm"


    Private _originalConsultantUid As Guid
    Private _consultantUid As Guid

    Private _originalUri As Uri
    Private _originalUsername As String
    Private _originalPassword As String
    Private _originalDomainUsername As String
    Private _originalDomainPassword As String
    Private _originalDomain As String

    Private _uri As Uri
    'Private _credentials As Net.ICredentials

    Private nextString As String = Terminology.GetString(MODULENAME, RES_Next)
    Private testString As String = Terminology.GetString(MODULENAME, RES_Test)
    Private doneString As String = Terminology.GetString(MODULENAME, RES_Done)


    'Private _done As Boolean
    Private _testOk As Boolean

    ''' <summary>
    ''' Load settings from program, registry, app.config, or wherever.
    ''' </summary>
    ''' <remarks>
    ''' Assumes that the original values are valid (well valid enough to load the form).
    ''' </remarks>
    Private _inLoadSettings As Boolean
    Private Sub LoadSettings()
        _inLoadSettings = True

        Me.DeviceIDTextBox.Text = GetFormattedDeviceID()

        Me._originalConsultantUid = gConsultantUID
        Me._consultantUid = gConsultantUID

        SetUrl(gServerUrl)
        Me._originalUri = Me._uri

        Me.ServerUrlTextBox.Text = gServerUrl

        Me.IgnoreCertificateErrorsCheckbox.Checked = ConfigurationSettings.GetValue("IgnoreServerCertificateErrors", False)
        Me.IgnoreCertificateErrorsCheckbox.Enabled = gServerUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase)

        Try
            ServerTimeoutValue.Value = CDec(gWebServiceTimeout) / 1000
        Catch ex As Exception
            ServerTimeoutValue.Value = 120
        End Try

        Me._originalDomainUsername = gDomainUsername
        Me.DomainUsernameTextBox.Text = gDomainUsername

        Me._originalDomainPassword = Decrypt(gDeviceID, gDomainPassword)
        Me.DomainPassword = Me._originalDomainPassword ' Decrypt(gDeviceID, Me._originalDomainPassword)

        Me._originalDomain = gDomain
        Me.DomainNameTextBox.Text = gDomain

        Me.SavePasswordCheckBox.Checked = Not String.IsNullOrEmpty(gDomainPassword)

        Me._originalUsername = gUsername
        Me.UserNameTextBox.Text = gUsername

        Me._originalPassword = gPassword
        Me.PasswordTextBox.Text = gPassword

        Me.UseIntegratedAuthentication = ConfigurationSettings.GetValue(My.Resources.AppConfigUseIntegratedAuthenticationKey, String.IsNullOrEmpty(gUsername)) ' AndAlso String.IsNullOrEmpty(gPassword)

        _inLoadSettings = False
    End Sub

    Private Function Save() As Boolean

        Try
            'Dim sFN As String

            ' delete existing Activiser.ConfigurationSettings data
            'sFN = Path.Combine(gDatabaseFolder, gConfigDbFileName)
            'If (New FileInfo(sFN)).Exists Then
            '    System.IO.File.Delete(sFN)
            'End If

            '' this could break loading of XML if it still exists.
            'sFN = Path.Combine(gTransactionFolder, gConfigDbFileName)
            'If (New FileInfo(sFN)).Exists Then
            '    System.IO.File.Delete(sFN)
            'End If

            Me._originalDomain = Me.Domain
            Me._originalDomainPassword = Me.DomainPassword
            Me._originalDomainUsername = Me.DomainUserName
            Me._originalPassword = Me.Password
            Me._originalUsername = Me.UserName
            Me._originalUri = Me.Uri

            gServerUrl = Me.Url
            ConfigurationSettings.SetValue(My.Resources.AppConfigServerUrlKey, Me.ServerUrlTextBox.Text)
            SetRegistryValue(My.Resources.RegistryServerUrlKey, Me.ServerUrlTextBox.Text)

            Try
                gWebServiceTimeout = Decimal.ToInt32(ServerTimeoutValue.Value)
                ConfigurationSettings.SetValue(My.Resources.AppConfigServerTimeoutKey, gWebServiceTimeout)
                gWebServiceTimeout *= 1000
            Catch ex As FormatException
                ConfigurationSettings.SetValue(My.Resources.AppConfigServerTimeoutKey, 120)
            End Try

            ConfigurationSettings.SetValue(My.Resources.AppConfigIgnoreServerCertificateErrorsKey, Me.IgnoreCertificateErrors)

            gDomainUsername = Me.DomainUserName
            gDomainPassword = Encrypt(gDeviceID, Me.DomainPassword)
            gDomain = Me.Domain
            gSavePassword = Me.SavePassword

            SetRegistryValue(My.Resources.RegistryDomainUserNameKey, gDomainUsername)
            SetRegistryValue(My.Resources.RegistryDomainKey, gDomain)
            If Not String.IsNullOrEmpty(gDomainPassword) AndAlso gSavePassword Then
                SetRegistryValue(My.Resources.RegistryDomainPasswordKey, gDomainPassword)
            Else
                SetRegistryValue(My.Resources.RegistryDomainPasswordKey, String.Empty)
            End If

            gUsername = Me.UserName
            gPassword = Encrypt(Me.ConsultantUid, Me.Password)
            SetRegistryValue(My.Resources.RegistryUserNameKey, gUsername)
            SetRegistryValue(My.Resources.RegistryPasswordKey, gPassword)
            ConfigurationSettings.SetValue(My.Resources.AppConfigUseIntegratedAuthenticationKey, Me.UseIntegratedAuthentication)

            gWebServer = New Library.WebService.activiser(Me.Url)
            gWebServer.Credentials = Me.GetCredentials()
            gWebServer.Timeout = gWebServiceTimeout
            'gWebServer.Url = Me.Url
            'gWebServer.UserAgent = My.Resources.UserAgentString & GetVersion().ToString()

            ' perform a logon, which sets the 'Credentials' header.
            If Me.ConsultantUid <> gConsultantUID AndAlso Me.ConsultantUid <> Guid.Empty Then
                Dim ct As ConsultantDataTable
                ct = gWebServer.ClientGetUserDetails(gDeviceIDString, Me.UserName, Me.Password)
                If ct Is Nothing OrElse ct.Count <> 1 Then
                    Throw New InvalidOperationException("Unable to log on to web service")
                End If

                Dim consultantRow As WebService.activiserDataSet.ConsultantRow = ct(0)
                gConsultantUID = consultantRow.ConsultantUID
                If gClientDataSet IsNot Nothing Then
                    If gClientDataSet.Consultant IsNot Nothing Then
                        gClientDataSet.Consultant.Merge(ct, True) ' .LoadDataRow(consultantRow.ItemArray, True)
                    End If
                End If

                SetStatusMessage(Terminology.GetString(MODULENAME, RES_FetchingProfile))
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
                ConsultantConfig.ConsultantSettingsDataSet.Clear()
                ConsultantConfig.ConsultantSettingsDataSet.Merge(gWebServer.GetUserProfile(gDeviceIDString, gConsultantUID))
                If ConsultantConfig.ConsultantSettingsDataSet.ConsultantSetting.Count = 0 Then
                    ConsultantConfig.SettingsRow = ConsultantConfig.ConsultantSettingsDataSet.ConsultantSetting.AddConsultantSettingRow( _
                       Guid.NewGuid, gConsultantUID, gDeviceID, consultantRow.Name, 5, 183, Nothing, 0, DateTime.MinValue, DateTime.UtcNow, consultantRow.Name, DateTime.UtcNow, consultantRow.Name)
                Else
                    ConsultantConfig.SettingsRow = ConsultantConfig.ConsultantSettingsDataSet.ConsultantSetting(0)
                End If
                ConsultantConfig.SavePending()

                SetStatusMessage(Terminology.GetString(MODULENAME, RES_LoadingProfile))
                'ConsultantConfig.Load() ' reload after save.
                System.Windows.Forms.Cursor.Current = Cursors.Default

            End If

            System.Windows.Forms.Cursor.Current = Cursors.Default
            Return True

        Catch ex As InvalidOperationException
            SetStatusMessage(Terminology.GetFormattedString(MODULENAME, RES_ErrorMessageFormat, ex.Message, ex.ToString()))
            Return False
        End Try
    End Function

    Private Sub SetupForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Me.DialogResult = Windows.Forms.DialogResult.None Then ' Corner 'Ok' pressed
            If Me._testOk Then
                Me.Save()
                Me.DialogResult = Windows.Forms.DialogResult.OK
            ElseIf Dirty Then
                If Terminology.AskQuestion(Me, MODULENAME, RES_CancelChanges, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    e.Cancel = True
                Else
                    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                End If
            Else
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End If
        End If
    End Sub


    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadSettings()
#If Not WINDOWSMOBILE Then
        EnableContextMenus(Me.Controls)
#End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        gLoadSuccessful = False
        Me.Close()
    End Sub

#Region "Input Panel"
#If WINDOWSMOBILE Then
    Private _inPanelSwitch As Boolean

    Private Sub EnableInputPanel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.InputPanelSwitch(True)
    End Sub

    Private Sub InputPanelSwitch(Optional ByVal loadInitial As Boolean = False)
        If Me._inPanelSwitch Then Return
        Me._inPanelSwitch = True
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
            Me.Refresh()
        Catch
            ' don't care
        Finally
            Me._inPanelSwitch = False
        End Try
    End Sub

    Private Sub InputPanel_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InputPanel.EnabledChanged
        If Me._inPanelSwitch Then Return
        InputPanelSwitch(False)
    End Sub
#End If


#End Region

    Private Sub NextTestDoneButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextTestDoneButton.Click
        If NextTestDoneButton.Text = testString Then
            If Me.TestUrl() Then
                Me.NextTestDoneButton.Text = doneString
            End If
        ElseIf NextTestDoneButton.Text = doneString Then
            Me.Save()
            '_done = True
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Else ' NextTestDoneButton.Text == nextString 
            ' select next tab
            Me.TabControl1.SelectedIndex = (Me.TabControl1.SelectedIndex + 1) Mod Me.TabControl1.TabPages.Count
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        'Me.ControlBox = False
        If Me.TabControl1.TabPages(Me.TabControl1.SelectedIndex) Is Me.TestTab Then
            Me.NextTestDoneButton.Text = testString
        Else
            Me.NextTestDoneButton.Text = nextString
        End If
    End Sub

    Private Sub BackButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BackButton.Click
        If Me.TabControl1.SelectedIndex > 0 Then
            Me.TabControl1.SelectedIndex -= 1
        End If
    End Sub

#Region "Properties"

    Public ReadOnly Property Dirty() As Boolean
        Get
            If Me.ConsultantUid <> Me._originalConsultantUid Then Return True
            If Me.Uri <> Me._originalUri Then Return True
            If Me.Domain <> Me._originalDomain Then Return True
            If Me.DomainUserName <> Me._originalDomainUsername Then Return True
            If Me.DomainPassword <> Me._originalDomainPassword Then Return True
            If Me.UserName <> Me._originalUsername Then Return True
            If Me.Password <> Me._originalPassword Then Return True
            Return False
        End Get
    End Property

    Public Property ConsultantUid() As Guid
        Get
            Return _consultantUid
        End Get
        Set(ByVal value As Guid)
            _consultantUid = value
        End Set
    End Property

    Private Sub SetUri(ByVal uri As Uri)
        Me.Uri = uri
    End Sub

    Private Sub SetUrl(ByVal url As String)
        If String.IsNullOrEmpty(url) Then
            SetUri(Nothing)
        Else
            Try
                Dim lUri As Uri = New Uri(url)
                SetUri(lUri)
            Catch ex As UriFormatException
                SetUri(Nothing)
            End Try
        End If
    End Sub

    Public Property Uri() As Uri
        Get
            Return _uri
        End Get
        Set(ByVal value As Uri)
            _uri = value
            If value IsNot Nothing Then
                Me.ServerUrlTextBox.Text = value.ToString
            End If
        End Set
    End Property

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings")> _
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")> _
    Public Property Url() As String
        Get
            If _uri Is Nothing Then
                Return String.Empty
            Else
                Return _uri.ToString
            End If
        End Get
        Set(ByVal value As String)
            SetUrl(value)
        End Set
    End Property

    Public Property IgnoreCertificateErrors() As Boolean
        Get
            Return Me.IgnoreCertificateErrorsCheckbox.Checked
        End Get
        Set(ByVal value As Boolean)
            Me.IgnoreCertificateErrorsCheckbox.Checked = value
        End Set
    End Property

    Public Property UseIntegratedAuthentication() As Boolean
        Get
            Return Me.UseIntegratedAuthenticationCheckbox.Checked
        End Get
        Set(ByVal value As Boolean)
            Me.UseIntegratedAuthenticationCheckbox.Checked = value
        End Set
    End Property

    Public Property DomainUserName() As String
        Get
            Return Me.DomainUsernameTextBox.Text
        End Get
        Set(ByVal value As String)
            Me.DomainUsernameTextBox.Text = value
        End Set
    End Property

    Public Property DomainPassword() As String
        Get
            Return Me.DomainPasswordTextBox.Text
        End Get
        Set(ByVal value As String)
            Me.DomainPasswordTextBox.Text = value
        End Set
    End Property

    Public Property Domain() As String
        Get
            Return Me.DomainNameTextBox.Text
        End Get
        Set(ByVal value As String)
            Me.DomainNameTextBox.Text = value
        End Set
    End Property

    Public Property SavePassword() As Boolean
        Get
            Return Me.SavePasswordCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            Me.SavePasswordCheckBox.Checked = value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return Me.UserNameTextBox.Text
        End Get
        Set(ByVal value As String)
            Me.UserNameTextBox.Text = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return Me.PasswordTextBox.Text
        End Get
        Set(ByVal value As String)
            Me.PasswordTextBox.Text = value
        End Set
    End Property

    Public Sub SetCredentials(ByVal value As Net.NetworkCredential)
        Me.Domain = value.Domain
        Me.DomainUserName = value.UserName
        Me.DomainPassword = value.Password
    End Sub

    Public Function GetCredentials() As Net.NetworkCredential
        Dim creds As Net.NetworkCredential
        If Not String.IsNullOrEmpty(DomainUserName) AndAlso Not String.IsNullOrEmpty(DomainPassword) AndAlso Not String.IsNullOrEmpty(Domain) Then
            creds = New Net.NetworkCredential(Me.DomainUserName, Me.DomainPassword, Me.Domain)
        ElseIf Not String.IsNullOrEmpty(DomainUserName) AndAlso Not String.IsNullOrEmpty(DomainPassword) Then
            creds = New Net.NetworkCredential(Me.DomainUserName, Me.DomainPassword)
        Else
            creds = Nothing
        End If
        Return creds
    End Function
#End Region

    Private Sub SetStatusMessage(ByVal message As String)
        AuthenticationLabel.Text = message
        Me.Refresh()
    End Sub

    Private Sub SetConsultantInfo(ByVal value As ConsultantRow)
        If value Is Nothing Then
            Me.UserInfoTextBox.Text = String.Empty
        Else
            Dim lMobilePhone As String
            If Not value.IsMobilePhoneNull Then
                lMobilePhone = value.MobilePhone
            Else
                lMobilePhone = String.Empty
            End If

            Dim lEmailAddress As String
            If Not value.IsEmailAddressNull Then
                lEmailAddress = value.EmailAddress
            Else
                lEmailAddress = String.Empty
            End If
            Me.UserInfoTextBox.Text = Terminology.GetFormattedString(MODULENAME, RES_UserInfoFormat, value.Username, value.Name, lMobilePhone, lEmailAddress)
        End If
        Me.Refresh()
    End Sub

    Private Function TestUrl() As Boolean
        Dim result As Boolean = False
        Dim previousCursor As Cursor = System.Windows.Forms.Cursor.Current

        Try
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            'Pseudo-code
            '   If AttemptLogon Then
            '       ReadyToGo
            '       Get consultant details from web service
            '       Display consultant details
            '       Change Next/Test/Done button to 'Done'
            '   Else
            '       NotReadyToGo
            '       Attempt access using browser.
            '   Fi
            '
            Me.Uri = New Uri(Me.ServerUrlTextBox.Text)
            Dim deviceIdCheckResult As Boolean

            ' first test device id, if this is OK, don't bother going further
            Using ws As New WebService.activiser(Me.Url)
                ws.Credentials = Me.GetCredentials

                deviceIdCheckResult = Synchronisation.CheckDeviceId(ws)
                SetStatusMessage(Terminology.GetString(MODULENAME, If(deviceIdCheckResult, RES_Authenticated, RES_AuthenticationFailed)))

                If Not deviceIdCheckResult Then
                    If TryBrowse() Then
                        SetStatusMessage(Terminology.GetString(MODULENAME, RES_DeviceIDCheckFailed))
                    End If
                    SetConsultantInfo(Nothing)
                    Exit Try
                End If

                Dim ct As ConsultantDataTable

                If Me.UseIntegratedAuthenticationCheckbox.Checked Then
                    ct = ws.ClientGetUserDetails(gDeviceIDString, String.Empty, String.Empty)
                    If ct Is Nothing OrElse ct.Count <> 1 Then
                        SetStatusMessage(Terminology.GetString(MODULENAME, RES_ActiviserUserNotFound))
                        SetConsultantInfo(Nothing)
                        Exit Try
                    End If
                Else
                    If String.IsNullOrEmpty(Me.UserName) Then
                        SetStatusMessage(Terminology.GetString(MODULENAME, RES_NoActiviserUserNameSupplied))
                        SetConsultantInfo(Nothing)
                        Exit Try
                    End If
                    ct = ws.ClientGetUserDetails(gDeviceIDString, Me.UserName, GetHash(Me.Password))
                    If ct Is Nothing OrElse ct.Count <> 1 Then
                        SetStatusMessage(Terminology.GetString(MODULENAME, RES_ActiviserUserOrPasswordInvalid))
                        SetConsultantInfo(Nothing)
                        Exit Try
                    End If
                End If
                SetConsultantInfo(ct(0))
                Me.ConsultantUid = ct(0).ConsultantUID

            End Using

            result = True

        Catch ex As UriFormatException
            SetStatusMessage(Terminology.GetString(MODULENAME, RES_ServerURLInvalid))
        Catch wex As Net.WebException
            Dim hr As Net.HttpWebResponse = TryCast(wex.Response, Net.HttpWebResponse)
            If hr IsNot Nothing Then
                SetStatusMessage(Terminology.GetFormattedString(MODULENAME, RES_HttpErrorMessage, hr.StatusCode, hr.StatusDescription, hr.ResponseUri.ToString()))
            End If
        Catch ex As Exception
            SetStatusMessage(Terminology.GetFormattedString(MODULENAME, RES_ErrorMessageFormat, ex.Message, ex.ToString()))
        End Try

        If result Then
            Me.NextTestDoneButton.Text = doneString
            'Me.ControlBox = True
        Else
            'Me.ControlBox = False
            If Me.TabControl1.TabPages(Me.TabControl1.SelectedIndex) Is Me.TestTab Then
                Me.NextTestDoneButton.Text = testString
            Else
                Me.NextTestDoneButton.Text = nextString
            End If
        End If
        System.Windows.Forms.Cursor.Current = previousCursor
        Me._testOk = result
        Return result
    End Function

    ''' <summary>
    ''' use browser to connect to web service to ensure that http connectivity is ok to web server
    ''' </summary>
    ''' <returns>true on success, false on failure.</returns>
    ''' <remarks></remarks>
    Private Function TryBrowse() As Boolean
        Dim previousCursor As Cursor = System.Windows.Forms.Cursor.Current

        Try
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim w As System.Net.HttpWebRequest
            w = CType(Net.WebRequest.Create(Me.Uri), Net.HttpWebRequest)
            If w IsNot Nothing Then
                Dim lCredentials As System.Net.NetworkCredential = Me.GetCredentials()
                w.PreAuthenticate = lCredentials IsNot Nothing
                w.Credentials = lCredentials

                Dim r As System.Net.WebResponse
                Dim hr As System.Net.HttpWebResponse
                Try
                    r = w.GetResponse()
                    hr = TryCast(r, Net.HttpWebResponse)
                Catch ex As Net.WebException
                    r = ex.Response
                    hr = TryCast(r, Net.HttpWebResponse)
                    If hr IsNot Nothing AndAlso hr.StatusCode = Net.HttpStatusCode.Unauthorized Then
                        SetStatusMessage(Terminology.GetString(MODULENAME, RES_AuthenticationFailed))
                        Return False
                    Else
                        SetStatusMessage(Terminology.GetFormattedString(MODULENAME, RES_ErrorMessageFormat, ex.Message, ex.ToString()))
                        Return False
                    End If
                End Try

                If hr Is Nothing OrElse hr.StatusCode <> Net.HttpStatusCode.OK Then
                    SetStatusMessage(Terminology.GetString(MODULENAME, RES_AuthenticationFailed))
                    Return False
                Else
                    If r.ContentLength <> 0 Then
                        Dim s As Stream = r.GetResponseStream()
                        Dim sr As New StreamReader(s, True)
                        Dim html As String = sr.ReadToEnd
                        r.Close()
                        Me.WebBrowser.DocumentText = html
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If

        Catch wex As Net.WebException
            SetStatusMessage(Terminology.GetFormattedString(MODULENAME, RES_ErrorMessageFormat, wex.Message, wex.ToString()))
            Return False
        Catch ex As Exception
            SetStatusMessage(Terminology.GetString(MODULENAME, RES_NavigationError))
            Return False
        Finally
            System.Windows.Forms.Cursor.Current = previousCursor
        End Try
    End Function

    Private Sub ServerUrlTextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ServerUrlTextBox.TextChanged
        Me.IgnoreCertificateErrorsCheckbox.Enabled = ServerUrlTextBox.Text.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
    End Sub

    Private Sub ServerUrlTextBox_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ServerUrlTextBox.Validating
        Try
            Me.SetUrl(Me.ServerUrlTextBox.Text)
        Catch ex As Exception
            Terminology.DisplayMessage(Me, MODULENAME, RES_UrlInvalid, MessageBoxIcon.Hand)
            e.Cancel = True
        End Try
    End Sub

    Private Sub IgnoreCertificateErrorsCheckbox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IgnoreCertificateErrorsCheckbox.Click
        Me.IgnoreCertificateErrorsCheckbox.Checked = Not Me.IgnoreCertificateErrorsCheckbox.Checked
        IgnoreCertificateErrors = Me.IgnoreCertificateErrorsCheckbox.Checked
    End Sub

    Private Sub ResetButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetButton.Click
        Me.Uri = Me._originalUri
        Me.UserName = Me._originalUsername
        Me.Password = Me._originalPassword
        Me.Domain = Me._originalDomain
        Me.DomainUserName = Me._originalDomainUsername
        Me.DomainPassword = Me._originalDomainPassword

    End Sub

    Private Sub HttpTestButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HttpTestButton.Click
        Try
            Me.Uri = New Uri(Me.ServerUrlTextBox.Text)
            Me.BrowserTab.Focus()
            Me.TryBrowse()
        Catch ex As Exception
            SetStatusMessage(Terminology.GetString(MODULENAME, RES_ServerURLInvalid))
        End Try
    End Sub

    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Save()

    End Sub

    Private Sub UseIntegratedAuthenticationCheckbox_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles UseIntegratedAuthenticationCheckbox.CheckStateChanged
        If _inLoadSettings Then Return

        If UseIntegratedAuthentication Then
            Me.UserName = String.Empty
            Me.Password = String.Empty
            Me.UserNameTextBox.Enabled = False
            Me.PasswordTextBox.Enabled = False
        Else
            Me.UserName = _originalUsername
            Me.Password = _originalPassword
            Me.UserNameTextBox.Enabled = True
            Me.PasswordTextBox.Enabled = True
        End If
    End Sub
End Class