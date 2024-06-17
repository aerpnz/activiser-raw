Option Strict Off ' Implicit casting is not allowed
Option Explicit On ' Objects types required

Imports activiser.Library
Imports activiser.Library.activiserWebService
Imports activiser.Library.activiserWebService.Utility
Imports activiser.Licensing
Imports activiser.Library.activiserWebService.ClientRegistrationDataSet

Public Class ServerProfileForm
    '    Const defaultMailPort As UShort = CUShort(25)

    Private ServerProfileData As Utility.ServerSettingDataTable

    Dim webServiceProductCode As String '= GetCurrentSetting("WebServiceProductCode")
    Dim webServiceGuid As Guid '= If(webServiceProductCode.IsGuid, New Guid(webServiceProductCode), Guid.Empty)

    Private _licensee As String
    Private _licenseKey As String

    Private _smsAddressTemplate As String
    Private _smsMessageTemplate As String
    Private _smsEnabled As Boolean

    Private _mailServer As String
    Private _mailPort As UShort
    Private _mailFrom As String
    Private _mailTo As String
    Private _mailTemplate As String
    Private _subjectTemplate As String
#Region "Form Events"
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult = Windows.Forms.DialogResult.OK Then
            If Not SaveProfile() Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Terminology.LoadLabels(Me)
        Me.ServerProfileData = ConsoleData.WebService.ConsoleGetServerProfile(deviceId, ConsoleUser.ConsultantUID, False)
        Me.ClientRegistrationDataSet1.Merge(ConsoleData.WebService.ConsoleGetClientDevices(deviceId, ConsoleUser.ConsultantUID, False))

        Me.AssigneeBindingSource.DataSource = Console.ConsoleData.CoreDataSet
        Me.AssigneeBindingSource.DataMember = Console.ConsoleData.CoreDataSet.Consultant.TableName
        Me.AssignedToColumn.DataSource = Me.AssigneeBindingSource
        Me.AssignedToColumn.ValueMember = Console.ConsoleData.CoreDataSet.Consultant.ConsultantUIDColumn.ColumnName
        Me.AssignedToColumn.DisplayMember = Console.ConsoleData.CoreDataSet.Consultant.NameColumn.ColumnName

        webServiceProductCode = GetCurrentSetting("WebServiceProductCode")
        webServiceGuid = If(webServiceProductCode.IsGuid, New Guid(webServiceProductCode), Guid.Empty)

        Me.MailTemplateBox.MaxLength = ServerProfileData.ValueColumn.MaxLength
        Me.SubjectTemplateBox.MaxLength = ServerProfileData.ValueColumn.MaxLength
        Me.MailTemplateExplanation.Text = Terminology.GetString(Me.Name, "MailTemplateExplanation")
        LoadRegistrationInfo()
        MobileAlertLoad()
        MailServerLoad()
        Me.Show()
    End Sub
#End Region

    Private WithEvents _consoleData As New ConsoleData("ServerProfileForm")
    Public Property ConsoleData() As ConsoleData
        Get
            Return _consoleData
        End Get
        Set(ByVal value As ConsoleData)
            _consoleData = value
        End Set
    End Property

    Private Function GetCurrentSetting(ByVal name As String) As String
        Try
            If ServerProfileData Is Nothing Then Me.ServerProfileData = ConsoleData.WebService.ConsoleGetServerProfile(deviceId, ConsoleUser.ConsultantUID, False)

            For Each ss As ServerSettingRow In ServerProfileData
                If ss.Name = name AndAlso ss.Status = 0 Then
                    Return ss.Value
                End If
            Next
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function ChangeSetting(ByVal name As String, ByVal value As String) As Boolean
        Try
            Dim now As Date = DateTime.UtcNow
            Dim today As Date = now.Date

            Dim ssq = From ss As ServerSettingRow _
                In ServerProfileData _
                Where ss.Name = name

            Dim top = Aggregate ss As ServerSettingRow In ssq Where ss.Name = name Select ss.Status Into Max()

            For Each ss As ServerSettingRow In ssq
                If ss.Status = 0 Then
                    If ss.Value <> value Then ' current value is changing.
                        ss.Status = top + 1
                        ss.ValidBefore = today
                    Else
                        Return True ' no change required
                    End If
                End If
            Next

            ServerProfileData.AddServerSettingRow(Guid.NewGuid(), name, 0, value, Nothing, today, DateTime.MaxValue, now, My.User.Name, now, My.User.Name)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#Region "Registration"
    'Private _unlicensed As Boolean = False

    Private Sub LoadRegistrationInfo()
        _licenseKey = GetCurrentSetting("LicenseKey")
        _licensee = GetCurrentSetting("Licensee")

        RegistrationDetailsBox.Text = "<Unlicensed>"
        LicenseeTextBox.Text = "<Unlicensed>"
        LicenseKeyTextBox.Text = "<Unlicensed>"

        If String.IsNullOrEmpty(_licensee) OrElse String.IsNullOrEmpty(_licenseKey) OrElse Not webServiceProductCode.IsGuid Then
            Return
        End If

        Try
            Dim license As LicenseInfo = New LicenseInfo(_licenseKey, _licensee, webServiceGuid)

            RegistrationDetailsBox.Text = String.Format(My.Resources.ServerProfileFormLicenseTestTemplate, _
                            license.ExpiryDate, (license.Version / 10).ToString("#0.0"), license.Users.ToString)

            LicenseeTextBox.Text = _licensee
            LicenseKeyTextBox.Text = _licenseKey

            Me._registrationSettingsModified = False

        Catch ex As Exception
            RegistrationDetailsBox.Text = "Warning: unable to parse License information."
        End Try
    End Sub

    Private Function ChangeLicenseInformation() As Boolean
        Dim licenseKey As String = LicenseKeyTextBox.Text.Trim
        Dim licensee As String = LicenseeTextBox.Text.Trim
        Try
            Dim newLicense As LicenseInfo = New LicenseInfo(licenseKey, licensee, webServiceGuid)

            RegistrationDetailsBox.Text = String.Format(Terminology.GetString(Me.Name, "LicenseTestTemplate"), _
                                    newLicense.ExpiryDate, (newLicense.Version / 10).ToString("#0.0"), newLicense.Users.ToString)

            If licensee <> _licensee Then Me.ChangeSetting("Licensee", licensee)
            If licenseKey <> _licenseKey Then Me.ChangeSetting("LicenseKey", licenseKey)

            _licensee = licensee
            _licenseKey = licenseKey

            Return True
        Catch ex As Exception
            RegistrationDetailsBox.Text = "New licence key not registered - Check that your client name and license key exactly match those supplied with your license pack."
            Return False
        End Try
    End Function
#End Region

#Region "Mobile Alert"
    Dim MobileAlertActivated As Boolean = True
    Private Sub MobileAlertLoad()
        _smsAddressTemplate = GetCurrentSetting("SmsAddressTemplate")
        _smsMessageTemplate = GetCurrentSetting("SmsMessageTemplate")
        _smsEnabled = CBool(GetCurrentSetting("SmsNotifyConsultantOfNewRequests"))

        SmsEnabledCheckBox.Checked = _smsEnabled ' MobileAlertActivated
        SmsAddressTemplateTextBox.Text = _smsAddressTemplate
        SmsMessageTemplateTextBox.Text = _smsMessageTemplate
        Me._smsSettingsModified = False
    End Sub

    Private Function SaveSmsSettings() As Boolean
        If SmsAddressTemplateTextBox.Text.IndexOf("<Number>", StringComparison.Ordinal) = -1 Then
            MsgBox("<Number> tag not specified in SMS Provider address", MsgBoxStyle.Exclamation)
            Return False
        End If

        If SmsMessageTemplateTextBox.Text.Trim = "" Then
            MsgBox("No message provided", MsgBoxStyle.Exclamation)
            Return False
        End If

        Try
            ChangeSetting("SmsNotifyConsultantOfNewRequests", SmsEnabledCheckBox.Checked.ToString())
            ChangeSetting("SmsServerAddress", SmsAddressTemplateTextBox.Text)
            ChangeSetting("SmsMessageTemplate", SmsMessageTemplateTextBox.Text)

            _smsEnabled = SmsEnabledCheckBox.Checked
            _smsAddressTemplate = SmsAddressTemplateTextBox.Text
            _smsMessageTemplate = SmsMessageTemplateTextBox.Text

            Return True
        Catch ex As Exception
            DisplayException.Show(ex, Icons.Critical, "Unable to update mobile alert details")
            Return False

        End Try
    End Function
#End Region


#Region "Mail Server"

    Private _mailSettingsModified As Boolean
    Private Sub MailServerLoad()
        _mailServer = GetCurrentSetting("MailServerAddress")
        _mailPort = CUShort(GetCurrentSetting("MailServerPort"))
        _mailFrom = GetCurrentSetting("ActiviserEmailAddress")
        _mailTo = GetCurrentSetting("AdministratorEmailAddress")
        _mailTemplate = GetCurrentSetting("JobEmailBodyTemplate")
        _subjectTemplate = GetCurrentSetting("JobEmailSubjectTemplate")

        Me.MailTestToAddressBox.Text = _mailTo
        Me.MailTestFromAddressBox.Text = _mailFrom
        Me.MailServerAddressBox.Text = _mailServer
        Me.MailServerPortNumber.Value = _mailPort
        Me.MailTemplateBox.Text = _mailTemplate
        Me.SubjectTemplateBox.Text = _subjectTemplate

        Me._mailSettingsModified = False
    End Sub

    Private Function SaveMailSettings() As Boolean
        Try

            ChangeSetting("AdministratorEmailAddress", MailTestToAddressBox.Text)
            _mailTo = MailTestToAddressBox.Text

            ChangeSetting("ActiviserEmailAddress", MailTestFromAddressBox.Text)
            _mailFrom = MailTestFromAddressBox.Text

            ChangeSetting("MailServerAddress", MailServerAddressBox.Text)
            _mailServer = MailServerAddressBox.Text

            ChangeSetting("MailServerPort", Decimal.ToUInt16(Me.MailServerPortNumber.Value).ToString())
            _mailPort = Me.MailServerPortNumber.Value

            ChangeSetting("JobEmailBodyTemplate", MailTemplateBox.Text)
            _mailTemplate = MailTemplateBox.Text

            ChangeSetting("JobEmailSubjectTemplate", SubjectTemplateBox.Text)
            _subjectTemplate = SubjectTemplateBox.Text

            Return True
        Catch ex As Exception
            DisplayException.Show(ex, Icons.Critical, "Unable to update mail server details")
            Return False
        End Try
    End Function

#End Region

    Private Function SaveProfile() As Boolean
        Me.Validate()
        Try
            If Me._smsSettingsModified Then
                If Not Me.SaveSmsSettings() Then
                    Return False
                End If
            End If
            If Me._registrationSettingsModified Then
                If Not Me.ChangeLicenseInformation Then
                    Return False
                End If
            End If
            If Me._mailSettingsModified Then
                If Not Me.SaveMailSettings Then
                    Return False
                End If
            End If
            For Each sr As Utility.ServerSettingRow In Me.ServerProfileData
                If sr.RowState = DataRowState.Modified Then sr.Modified = DateTime.UtcNow
                'If sr.IsNull("ItemType") Then
                '    sr.ItemType = "D"
                'End If
            Next
            Dim serverProfileChanges As ServerSettingDataTable = CType(Me.ServerProfileData.GetChanges, Utility.ServerSettingDataTable)
            If serverProfileChanges IsNot Nothing Then
                ConsoleData.WebService.ConsoleUpdateServerProfile(deviceId, ConsoleUser.ConsultantUID, serverProfileChanges)
                Me.ServerProfileData = ConsoleData.WebService.ConsoleGetServerProfile(deviceId, ConsoleUser.ConsultantUID, False)
            End If

            Dim clientRegistrationChanges As ClientRegistrationDataSet = CType(Me.ClientRegistrationDataSet1.GetChanges, ClientRegistrationDataSet)
            If clientRegistrationChanges IsNot Nothing Then
                ConsoleData.WebService.ConsoleUpdateClientDevices(deviceId, ConsoleUser.ConsultantUID, clientRegistrationChanges)
                Me.ClientRegistrationDataSet1 = ConsoleData.WebService.ConsoleGetClientDevices(deviceId, ConsoleUser.ConsultantUID, False)
            End If

            _smsSettingsModified = False
            _registrationSettingsModified = False
            Return True
        Catch ex As Exception
            DisplayException.Show(ex, Icons.Critical, Terminology.GetString(Me.Name, "SaveFailureMesasge"))
            Return False
        End Try
    End Function

    Private _smsSettingsModified As Boolean
    Private Sub smsSettingsModified(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SmsAddressTemplateTextBox.TextChanged, SmsMessageTemplateTextBox.TextChanged, SmsEnabledCheckBox.CheckedChanged
        _smsSettingsModified = True
    End Sub

    Private _registrationSettingsModified As Boolean
    Private Sub registrationSettingsModified(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LicenseKeyTextBox.TextChanged, LicenseeTextBox.TextChanged
        _registrationSettingsModified = True
    End Sub

    Private Sub checkLicenseKeyButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkLicenseKeyButton.Click
        Dim strLicenseKey As String = LicenseKeyTextBox.Text.Trim
        Dim strClientName As String = LicenseeTextBox.Text.Trim
        Try
            Dim license As LicenseInfo = New LicenseInfo(strLicenseKey, strClientName, webServiceGuid)
            Dim strResult As String = Terminology.GetFormattedString(Me.Name, "LicenseTestTemplate", license.ExpiryDate, (license.Version / 10).ToString("#0.0"), license.Users.ToString)

            If license.ExpiryDate = New DateTime Then
                strResult = String.Empty
                RegistrationDetailsBox.Text = Terminology.GetString(Me.Name, "LicenseInvalid")
                Return
            End If

            RegistrationDetailsBox.Text = strResult
            LicenseKeyTextBox.Text = strLicenseKey
            LicenseeTextBox.Text = strClientName

        Catch ex As Exception
            RegistrationDetailsBox.Text = Terminology.GetString(Me.Name, "LicenseInvalid")
            Return
        End Try

    End Sub

    Private Sub MailServerTestButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MailServerTestButton.Click
        Try
            Me.MailServerTestResults.Text = "Testing..."
            Me.MailServerTestResults.Refresh()
            Me.MailServerTestResults.Text = ConsoleData.WebService.TestEmail(deviceId, Me.MailServerAddressBox.Text, CInt(Me.MailServerPortNumber.Value), Me.MailTestFromAddressBox.Text, Me.MailTestToAddressBox.Text, My.Resources.ServerProfileFormMailServerTestSubject, My.Resources.ServerProfileFormMailServerTestMessage)
            'Dim mc As New System.Net.Mail.SmtpClient(Me.MailServerAddressBox.Text, Decimal.ToInt32(Me.MailServerPortNumber.Value))
            'mc.Send(Me.MailTestFromAddressBox.Text, Me.MailTestToAddressBox.Text, My.Resources.ServerProfileMailServerTestSubject, My.Resources.ServerProfileMailServerTestMessage)
            'Me.MailServerTestResults.Text = My.Resources.ServerProfileMailTestSuccessMessage
        Catch ex As Exception
            Me.MailServerTestResults.Text = Terminology.GetFormattedString(Me.Name, "MailTestFailureMessage", ex.Message)
        End Try
    End Sub

    Private Sub MailSettingsChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MailTestToAddressBox.TextChanged, MailTestFromAddressBox.TextChanged, MailServerAddressBox.TextChanged, MailServerPortNumber.ValueChanged, SubjectTemplateBox.TextChanged, MailTemplateBox.TextChanged
        Me._mailSettingsModified = True
    End Sub

    Private _inAddingNew As Boolean
    Private Sub ClientBindingSource_AddingNew(ByVal sender As Object, ByVal e As System.ComponentModel.AddingNewEventArgs) Handles ClientBindingSource.AddingNew
        If _inAddingNew Then Return
        _inAddingNew = True

        
        Dim drv As DataRowView = DirectCast(ClientBindingSource.List, DataView).AddNew()

        Dim newClient As ClientRow = CType(drv.Row, ClientRow)
        newClient.ClientId = Guid.NewGuid()
        Debug.Print(newClient.ClientId.ToString())
        ClientBindingSource.Position = ClientBindingSource.Count - 1
        ClientBindingSource.ResetCurrentItem()

        e.NewObject = drv

        _inAddingNew = False
    End Sub

    Private Sub BindingSource1_PositionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientBindingSource.PositionChanged
        Dim drv As DataRowView = TryCast(Me.ClientBindingSource.Current, DataRowView)
        If drv IsNot Nothing Then
            Dim sr As Utility.ServerSettingRow = TryCast(drv.Row, Utility.ServerSettingRow)
            If sr IsNot Nothing Then
                'If sr.IsNull("ItemType") Then
                '    sr.ItemType = "D"
                'End If
            End If
        End If
    End Sub

    Private Sub ServerProfileDataGridView_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ServerProfileDataGridView.CellEndEdit
        If sender Is Me.ServerProfileDataGridView Then
            If Me.ServerProfileDataGridView.Columns(e.ColumnIndex) Is Me.SystemIdDataGridViewTextBoxColumn Then
                If Not Me.ServerProfileDataGridView.Item(e.ColumnIndex, e.RowIndex).Value Is DBNull.Value Then
                    Dim deviceId As String = CStr(Me.ServerProfileDataGridView.Item(e.ColumnIndex, e.RowIndex).Value)
                    If Not String.IsNullOrEmpty(deviceId) Then
                        Dim sb As New System.Text.StringBuilder(deviceId.Length)
                        For Each c As Char In deviceId
                            If Not My.Resources.DeviceIdCharsToIgnore.Contains(c) Then
                                sb.Append(c)
                            End If
                        Next
                        deviceId = sb.ToString()
                        Me.ServerProfileDataGridView.Item(e.ColumnIndex, e.RowIndex).Value = deviceId
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ServerProfileDataGridView_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles ServerProfileDataGridView.CellFormatting
        If sender Is Me.ServerProfileDataGridView Then
            If Me.ServerProfileDataGridView.Columns(e.ColumnIndex) Is Me.SystemIdDataGridViewTextBoxColumn Then
                If Not Me.ServerProfileDataGridView.Item(e.ColumnIndex, e.RowIndex).Value Is DBNull.Value Then

                    'End If
                    Dim deviceId As String = CStr(Me.ServerProfileDataGridView.Item(e.ColumnIndex, e.RowIndex).Value)
                    If deviceId IsNot Nothing Then
                        If deviceId.Length = 32 Then ' base32 encoded - post V3.5
                            Dim formattedDeviceId As String = String.Format("{0}-{1}-{2}-{3}-{4}-{5}", _
                            deviceId.Substring(0, 6), _
                            deviceId.Substring(6, 6), _
                            deviceId.Substring(12, 5), _
                            deviceId.Substring(17, 5), _
                            deviceId.Substring(22, 5), _
                            deviceId.Substring(27, 5))
                            e.Value = formattedDeviceId
                            e.FormattingApplied = True
                        ElseIf deviceId.Length = 40 Then ' base16 encoded - pre V3.5
                            Dim formattedDeviceId As String = String.Format("{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}", _
                            deviceId.Substring(0, 5), _
                            deviceId.Substring(5, 5), _
                            deviceId.Substring(10, 5), _
                            deviceId.Substring(15, 5), _
                            deviceId.Substring(20, 5), _
                            deviceId.Substring(25, 5), _
                            deviceId.Substring(30, 5), _
                            deviceId.Substring(35, 5))
                            e.Value = formattedDeviceId
                            e.FormattingApplied = True
                        End If

                    End If
                End If
            End If
        End If
    End Sub

    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cancelDialogButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cancelDialogButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ApplyButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApplyButton.Click
        SaveProfile()
    End Sub

    Private Sub ServerProfileDataGridView_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles ServerProfileDataGridView.DataError
        e.ThrowException = False
    End Sub
End Class
