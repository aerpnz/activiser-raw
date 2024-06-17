Imports activiser.Terminology
Imports activiser.Library.WebService
Imports activiser.Library.WebService.activiserDataSet
Imports System.Collections.Generic
Imports Microsoft.WindowsCE.Forms

Public Class SettingsForm
    Const MODULENAME As String = "SettingsForm"
    Const DateForm_System As String = "<System>"

    Const RES_SettingsTasksEmpty As String = "SettingsTasksEmpty"
    Const RES_JobHistoryLimit7Days As String = "JobHistoryLimit7Days"
    Const RES_JobHistoryLimit14Days As String = "JobHistoryLimit14Days"
    Const RES_JobHistoryLimit31Days As String = "JobHistoryLimit31Days"
    Const RES_JobHistoryLimit92Days As String = "JobHistoryLimit92Days"
    Const RES_JobHistoryLimit183Days As String = "JobHistoryLimit183Days"
    Const RES_JobHistoryLimitNone As String = "JobHistoryLimitNone"
    Const RES_ErrorSavingServerURL As String = "ErrorSavingServerURL"
    Const RES_ErrorSavingServerTimeout As String = "ErrorSavingServerTimeout"
    Const RES_ErrorSavingConfiguration As String = "ErrorSavingConfiguration"
    Const RES_SaveChangesOnCancel As String = "SaveChangesOnCancel"
    Const RES_ColouredTextLabel As String = "ColouredTextLabel"
    Const RES_ColouredBackgroundLabel As String = "ColouredBackgroundLabel"
    Const RES_ErrorCheckingServerURL As String = "Error checking server URL"
    Const RES_InvalidURL As String = "InvalidURL"

    'Dim colouredText As String = Terminology.GetString(MODULENAME, RES_ColouredTextLabel)
    'Dim colouredBackground As String = Terminology.GetString(MODULENAME, RES_ColouredBackgroundLabel)

    Private noShortCuts As String = Terminology.GetString(MODULENAME, RES_SettingsTasksEmpty)

    Private mboolDirty As Boolean
    'Private mboolURLDirty As Boolean

    'Private _originalUrl As String
    'Private _originalUsername As String
    'Private _originalPassword As String
    'Private _originalDomainName As String

    Private Sub LoadTimeSelectors()
        For h As Integer = 23 To 0 Step -1
            For m As Integer = 45 To 0 Step -15
                Dim dt As New DateTime(1900, 1, 1, h, m, 0)
                Me.SyncStartTimeSelector.Items.Add(dt.ToShortTimeString)
                Me.SyncFinishTimeSelector.Items.Add(dt.ToShortTimeString)
            Next
        Next
    End Sub

    Private Sub LoadHistoryAges()

        HistoryAgeOptions.Items.Clear()
        HistoryAgeOptions.Items.Add(Terminology.GetString(MODULENAME, RES_JobHistoryLimitNone))
        HistoryAgeOptions.Items.Add(Terminology.GetString(MODULENAME, RES_JobHistoryLimit183Days))
        HistoryAgeOptions.Items.Add(Terminology.GetString(MODULENAME, RES_JobHistoryLimit92Days))
        HistoryAgeOptions.Items.Add(Terminology.GetString(MODULENAME, RES_JobHistoryLimit31Days))
        HistoryAgeOptions.Items.Add(Terminology.GetString(MODULENAME, RES_JobHistoryLimit14Days))
        HistoryAgeOptions.Items.Add(Terminology.GetString(MODULENAME, RES_JobHistoryLimit7Days))
    End Sub

    Private Sub LoadSettings()
        Const METHODNAME As String = "LoadSettings"
        Try
            'Me.DeviceIDTextBox.Text = GetFormattedDeviceID()

            If ConsultantConfig.Shortcuts.Count > 0 Then
                Dim shortCutStrings As New System.Text.StringBuilder()
                For Each s As String In ConsultantConfig.Shortcuts
                    shortCutStrings.Append(s)
                    shortCutStrings.Append(vbNewLine)
                Next
                Me.ShortCutList.Text = shortCutStrings.ToString
            Else
                Me.ShortCutList.Text = noShortCuts
            End If

            'Me.DateFormatList.Text = ConsultantConfig.DateFormat
            Me.HistoryNumberOptions.Text = ConsultantConfig.JobHistoryLimit.ToString(WithoutCulture)

            Try
                Me.LoadHistoryAges()
                Select Case ConsultantConfig.JobHistoryAgeLimit
                    Case 7 : HistoryAgeOptions.SelectedIndex = HistoryAgeOptions.Items.IndexOf(Terminology.GetString(MODULENAME, RES_JobHistoryLimit7Days))
                    Case 14 : HistoryAgeOptions.SelectedIndex = HistoryAgeOptions.Items.IndexOf(Terminology.GetString(MODULENAME, RES_JobHistoryLimit14Days))
                    Case 31 : HistoryAgeOptions.SelectedIndex = HistoryAgeOptions.Items.IndexOf(Terminology.GetString(MODULENAME, RES_JobHistoryLimit31Days))
                    Case 92 : HistoryAgeOptions.SelectedIndex = HistoryAgeOptions.Items.IndexOf(Terminology.GetString(MODULENAME, RES_JobHistoryLimit92Days))
                    Case 183 : HistoryAgeOptions.SelectedIndex = HistoryAgeOptions.Items.IndexOf(Terminology.GetString(MODULENAME, RES_JobHistoryLimit183Days))
                    Case Else : HistoryAgeOptions.SelectedIndex = HistoryAgeOptions.Items.IndexOf(Terminology.GetString(MODULENAME, RES_JobHistoryLimitNone))
                End Select
            Catch ex As Exception
                LogError(MODULENAME, METHODNAME, ex, False, RES_ErrorLoadingHistoryLimit)

            End Try

            Try
                Dim ddj As Integer = ConfigurationSettings.GetValue(My.Resources.AppConfigDefaultJobDurationKey, 15)
                Dim i As Integer = Me.DefaultJobDurationOptions.Items.IndexOf(CStr(ddj))
                If i = -1 Then
                    Me.DefaultJobDurationOptions.SelectedIndex = Me.DefaultJobDurationOptions.Items.IndexOf("15")
                Else
                    Me.DefaultJobDurationOptions.SelectedIndex = i
                End If
            Catch ex As Exception
                LogError(MODULENAME, METHODNAME, ex, False, RES_ErrorLoadingDefaultJobDuration)

            End Try


            Try
                Dim jti As Integer = ConfigurationSettings.GetValue(My.Resources.AppConfigTimePickerIntervalKey, 15)
                Dim i As Integer = Me.JobTimeIntervalOptions.Items.IndexOf(CStr(jti))
                If i = -1 Then
                    Me.JobTimeIntervalOptions.SelectedIndex = Me.JobTimeIntervalOptions.Items.IndexOf("5")
                Else
                    Me.JobTimeIntervalOptions.SelectedIndex = i
                End If
            Catch ex As Exception
                LogError(MODULENAME, METHODNAME, ex, False, RES_ErrorLoadingJobTimeInterval)

            End Try


            Try
                Dim jso As Integer = ConfigurationSettings.GetValue(My.Resources.AppConfigDefaultJobStartKey, 0)
                Dim i As Integer = Me.DefaultJobStartOffsetOptions.Items.IndexOf(CStr(jso))
                If i = -1 Then
                    Me.DefaultJobStartOffsetOptions.SelectedIndex = Me.DefaultJobStartOffsetOptions.Items.IndexOf("0")
                Else
                    Me.DefaultJobStartOffsetOptions.SelectedIndex = i
                End If
            Catch ex As Exception
                LogError(MODULENAME, METHODNAME, ex, False, RES_ErrorLoadingJobStartOffset)

            End Try

            Try
                Dim tto As Integer = ConfigurationSettings.GetValue(My.Resources.AppConfigDefaultTravelTimeKey, 15)
                Dim i As Integer = Me.DefaultJobStartOffsetOptions.Items.IndexOf(CStr(tto))
                If i = -1 Then
                    Me.DefaultJobStartOffsetOptions.SelectedIndex = Me.DefaultTravelTimeOptions.Items.IndexOf("15")
                Else
                    Me.DefaultJobStartOffsetOptions.SelectedIndex = i
                End If
            Catch ex As Exception
                LogError(MODULENAME, METHODNAME, ex, False, RES_ErrorLoadingDefaultTravelTime)

            End Try

            Try
                Me.AutoSyncCheckBox.Checked = gAutoSync 'ConfigurationSettings.GetBooleanValue(My.Resources.AppConfigAutoSyncKey, False)
                If gAutoSyncInterval >= 5 Then
                    Me.SyncIntervalNumberBox.Value = (gAutoSyncInterval \ 5) * 5
                Else
                    Me.SyncIntervalNumberBox.Value = 60
                End If
            Catch ex As Exception
                LogError(MODULENAME, METHODNAME, ex, False, RES_ErrorLoadingAutoSyncInterval)

            End Try


            Try
                If gSyncDays.IndexOf("M", StringComparison.OrdinalIgnoreCase) <> -1 Then Me.MondayCheckbox.Checked = True
                If gSyncDays.IndexOf("T", StringComparison.OrdinalIgnoreCase) <> -1 Then Me.TuesdayCheckbox.Checked = True
                If gSyncDays.IndexOf("W", StringComparison.OrdinalIgnoreCase) <> -1 Then Me.WednesdayCheckbox.Checked = True
                If gSyncDays.IndexOf("H", StringComparison.OrdinalIgnoreCase) <> -1 Then Me.ThursdayCheckbox.Checked = True
                If gSyncDays.IndexOf("F", StringComparison.OrdinalIgnoreCase) <> -1 Then Me.FridayCheckbox.Checked = True
                If gSyncDays.IndexOf("S", StringComparison.OrdinalIgnoreCase) <> -1 Then Me.SaturdayCheckbox.Checked = True
                If gSyncDays.IndexOf("U", StringComparison.OrdinalIgnoreCase) <> -1 Then Me.SundayCheckbox.Checked = True
            Catch ex As Exception
                LogError(MODULENAME, METHODNAME, ex, False, RES_ErrorLoadingSyncDays)

            End Try

            Try
                For i As Integer = 0 To Me.SyncStartTimeSelector.Items.Count - 1
                    If CDate(Me.SyncStartTimeSelector.Items.Item(i)).TimeOfDay = gSyncStartTime.TimeOfDay Then
                        Me.SyncStartTimeSelector.SelectedIndex = i
                        Exit For
                    End If
                Next
            Catch ex As Exception
                LogError(MODULENAME, METHODNAME, ex, False, RES_ErrorLoadingSyncStartTime)

            End Try


            Try
                For i As Integer = 0 To Me.SyncFinishTimeSelector.Items.Count - 1
                    If CDate(Me.SyncFinishTimeSelector.Items.Item(i)).TimeOfDay = gSyncFinishTime.TimeOfDay Then
                        Me.SyncFinishTimeSelector.SelectedIndex = i
                        Exit For
                    End If
                Next
            Catch ex As Exception
                LogError(MODULENAME, METHODNAME, ex, False, RES_ErrorLoadingSyncFinishTime)

            End Try

            Try
                Me.TextSizePicker.Value = ConfigurationSettings.GetValue(My.Resources.AppConfigTextSizeKey, 8)
            Catch ex As Exception

            End Try

            mboolDirty = False
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    'Commit all settings to XML
    Sub SaveSettings()
        Const METHODNAME As String = "SaveSettings"
        Try
            If Me.ShortCutList.Text <> noShortCuts Then
                ConsultantConfig.UpdateShortcuts(Me.ShortCutList.Text)
            End If

            'If Me.DateFormatList.Text = DateForm_System Then
            '    gDateFormat = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern
            '    ConsultantConfig.DateFormat = String.Empty
            'Else
            '    ConsultantConfig.DateFormat = Me.DateFormatList.Text
            '    gDateFormat = ConsultantConfig.DateFormat
            'End If

            Try
                ConsultantConfig.JobHistoryLimit = CInt(Val(Me.HistoryNumberOptions.Text))
            Catch ex As Exception
                ConsultantConfig.JobHistoryLimit = 5
            End Try

            Select Case Me.HistoryAgeOptions.Text
                Case Terminology.GetString(MODULENAME, RES_JobHistoryLimit7Days) : ConsultantConfig.JobHistoryAgeLimit = 7
                Case Terminology.GetString(MODULENAME, RES_JobHistoryLimit14Days) : ConsultantConfig.JobHistoryAgeLimit = 14
                Case Terminology.GetString(MODULENAME, RES_JobHistoryLimit31Days) : ConsultantConfig.JobHistoryAgeLimit = 31
                Case Terminology.GetString(MODULENAME, RES_JobHistoryLimit92Days) : ConsultantConfig.JobHistoryAgeLimit = 91
                Case Terminology.GetString(MODULENAME, RES_JobHistoryLimit183Days) : ConsultantConfig.JobHistoryAgeLimit = 183
                Case Terminology.GetString(MODULENAME, RES_JobHistoryLimitNone) : ConsultantConfig.JobHistoryAgeLimit = 1000000
            End Select

            gintJobHistoryCount = ConsultantConfig.JobHistoryLimit
            gintJobHistoryAge = ConsultantConfig.JobHistoryAgeLimit
            ConsultantConfig.SavePending()

            Try
                ConfigurationSettings.SetValue(My.Resources.AppConfigDefaultJobDurationKey, CInt(Me.DefaultJobDurationOptions.Text))
            Catch 'ex As Exception
            End Try

            Try
                ConfigurationSettings.SetValue(My.Resources.AppConfigTimePickerIntervalKey, CInt(Me.JobTimeIntervalOptions.Text))
            Catch 'ex As Exception
            End Try

            Try
                ConfigurationSettings.SetValue(My.Resources.AppConfigDefaultJobStartKey, CInt(Me.DefaultJobStartOffsetOptions.Text))
            Catch 'ex As Exception
            End Try

            Try
                ConfigurationSettings.SetValue(My.Resources.AppConfigDefaultTravelTimeKey, If(String.IsNullOrEmpty(Me.DefaultTravelTimeOptions.Text) OrElse Not IsNumeric(Me.DefaultTravelTimeOptions.Text), 0, CInt(Me.DefaultTravelTimeOptions.Text)))
            Catch 'ex As Exception
            End Try

            Try
                Dim autoSyncInterval As Integer = CInt(Me.SyncIntervalNumberBox.Value)
                ConfigurationSettings.SetValue(My.Resources.AppConfigAutoSyncIntervalKey, autoSyncInterval)
                ConfigurationSettings.SetValue(My.Resources.AppConfigAutoSyncKey, Me.AutoSyncCheckBox.Checked)
                Synchronisation.SetAutoSyncInterval(autoSyncInterval, Me.AutoSyncCheckBox.Checked)
            Catch 'ex As Exception
            End Try

            Dim syncDayList As String = String.Empty
            If Me.MondayCheckbox.Checked Then syncDayList &= "M"
            If Me.TuesdayCheckbox.Checked = True Then syncDayList &= "T"
            If Me.WednesdayCheckbox.Checked = True Then syncDayList &= "W"
            If Me.ThursdayCheckbox.Checked = True Then syncDayList &= "H"
            If Me.FridayCheckbox.Checked = True Then syncDayList &= "F"
            If Me.SaturdayCheckbox.Checked = True Then syncDayList &= "S"
            If Me.SundayCheckbox.Checked = True Then syncDayList &= "U"
            ConfigurationSettings.SetValue(My.Resources.AppConfigSyncDaysKey, syncDayList)

            gSyncStartTime = CDate(Me.SyncStartTimeSelector.Text)
            gSyncFinishTime = CDate(Me.SyncFinishTimeSelector.Text)
            ConfigurationSettings.SetValue(My.Resources.AppConfigSyncStartTimeKey, gSyncStartTime.ToShortTimeString)
            ConfigurationSettings.SetValue(My.Resources.AppConfigSyncFinishTimeKey, gSyncFinishTime.ToShortTimeString)

            ConfigurationSettings.SetValue(My.Resources.AppConfigTextSizeKey, Me.TextSizePicker.Value)

            Try
                ConfigurationSettings.Save()
            Catch ex As Exception
                Terminology.DisplayMessage(Me, MODULENAME, RES_ErrorSavingConfiguration, MessageBoxIcon.Hand)
            End Try

            mboolDirty = False

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    'Private Sub SettingsForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    '    InputPanelSwitch(True)
    'End Sub

    Private Sub SettingsForm_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        gHoldAutoSync = False
    End Sub

    Private Sub frmConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gHoldAutoSync = True

        Terminology.LoadLabels(Me)

        'Me.MainFormGridColouringOptions.Items.Clear()
        'Me.MainFormGridColouringOptions.Items.Add(Me.colouredText)
        'Me.MainFormGridColouringOptions.Items.Add(Me.colouredBackground)

        Me.LoadTimeSelectors()

        Me.LoadSettings()

#If Not WINDOWSMOBILE Then
        EnableContextMenus(Me.Controls)
#End If

    End Sub

    Private Sub mnuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuClose.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub MakeDirty(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShortCutList.TextChanged, JobTimeIntervalOptions.SelectedItemChanged, DefaultJobStartOffsetOptions.SelectedItemChanged, DefaultJobDurationOptions.SelectedItemChanged, AutoSyncCheckBox.Click
        mboolDirty = True
    End Sub

    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Const METHODNAME As String = "Form_Closing"
        Try
            If Me.DialogResult = Windows.Forms.DialogResult.Cancel Then
                If mboolDirty Then
                    Select Case Terminology.AskQuestion(Me, MODULENAME, RES_SaveChangesOnCancel, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button1)
                        Case Windows.Forms.DialogResult.Yes
                            SaveSettings()
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        Case Windows.Forms.DialogResult.No
                            Me.DialogResult = Windows.Forms.DialogResult.Cancel
                        Case Windows.Forms.DialogResult.Cancel
                            e.Cancel = True
                            Exit Sub
                    End Select
                Else
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                End If
            Else
                SaveSettings()
            End If
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        Finally
            Startup.InitialiseWebServiceProxy(Me, True)
        End Try
    End Sub

    Private Sub NextMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextButton.Click
        If Me.TabControl.SelectedIndex < Me.TabControl.TabPages.Count - 1 Then
            Me.TabControl.SelectedIndex += 1
        Else
            Me.TabControl.SelectedIndex = 0
        End If
    End Sub

    Private Sub MainMenuChangeServerSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuChangeServerSettings.Click
        Using sf As New SetupForm()
            sf.Owner = Me
            sf.ShowDialog()
        End Using
    End Sub

    Private Sub ShortCutList_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShortCutList.TextChanged
        Me.mboolDirty = True
    End Sub

    Public Sub New(ByVal owner As Form)
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Owner = owner
        Me.SetWindowState()
    End Sub

    Private Sub SettingsForm_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If Me.ShortCutList.Focused Then Return

        If e.KeyCode = Keys.Up Then
            Dim startFrom As Control = GetActiveControl(Me)
            If startFrom Is Nothing Then Return
            Me.SelectNextControl(startFrom, False, True, True, True)
            e.Handled = True
        ElseIf e.KeyCode = Keys.Down Then
            Dim startFrom As Control = GetActiveControl(Me)
            If startFrom Is Nothing Then Return
            Me.SelectNextControl(startFrom, True, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub ChangeServerSettingsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeServerSettingsButton.Click
        Using sf As New SetupForm
            sf.ShowDialog()
        End Using
    End Sub

    Private Sub WebServiceCredentialsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WebServiceCredentialsButton.Click, MainMenuChangeWebServiceCredentials.Click
        Using cd As New CredentialDialog(Me, gDomainUsername, Decrypt(gDeviceID, gDomainPassword), gDomain)
            If cd.ShowDialog = Windows.Forms.DialogResult.OK Then
                gDomainUsername = cd.UserName
                gDomainPassword = Encrypt(gDeviceID, cd.Password)
                gDomain = cd.DomainName
                gSavePassword = cd.SavePassword
                SetRegistryValue(My.Resources.RegistryDomainUserNameKey, gDomainUsername)
                SetRegistryValue(My.Resources.RegistryDomainKey, gDomain)
                If Not String.IsNullOrEmpty(gDomainPassword) AndAlso gSavePassword Then
                    SetRegistryValue(My.Resources.RegistryDomainPasswordKey, gDomainPassword)
                Else
                    SetRegistryValue(My.Resources.RegistryDomainPasswordKey, String.Empty)
                End If
            End If
        End Using
    End Sub

    Private Sub ActiviserPasswordButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActiviserPasswordButton.Click, MainMenuChangeActiviserPassword.Click
        Using cd As New CredentialDialog(Me, gUsername, gPassword)
            If cd.ShowDialog = Windows.Forms.DialogResult.OK Then
                gUsername = cd.UserName
                gPassword = Encrypt(gConsultantUID, cd.Password)
                SetRegistryValue(My.Resources.RegistryUserNameKey, gUsername)
                SetRegistryValue(My.Resources.RegistryPasswordKey, gPassword)
            End If
        End Using
    End Sub

    Private Sub MainMenuFetchUpdatedSchema_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuFetchUpdatedSchema.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Try
            If Synchronisation.GetSchemaAndTerminologyUpdates(force:=True) Then
                Terminology.DisplayMessage(Me, MODULENAME, "SchemaUpdated", MessageBoxIcon.Asterisk)
            Else
                Terminology.DisplayMessage(Me, MODULENAME, "SchemaNotUpdated", MessageBoxIcon.Asterisk)
            End If
        Catch ex As Exception
            Terminology.DisplayMessage(Me, MODULENAME, "SchemaUpdateFailed", MessageBoxIcon.Asterisk)
        Finally
            System.Windows.Forms.Cursor.Current = Cursors.Default
        End Try

        'Dim ds As LanguageDataSet = gWebServer.GetTerminology(gDeviceIDString, gConsultantUID, Terminology.ClientKey, ConfigurationSettings.GetValue("LanguageId", 1), DateTime.MinValue)
        'Terminology.Load(ds)

    End Sub

    Private Sub MainMenuRemoveCompletedRequests_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuRemoveCompletedRequests.Click
        RequestUtilities.RemoveClosedRequests(Me)
    End Sub

    Private Sub SetWindowState()
#If WINDOWSMOBILE Then
        Me.WindowState = Owner.WindowState
        Me.ViewFullScreen.Checked = Me.WindowState = FormWindowState.Maximized
#End If
    End Sub

#If WINDOWSMOBILE Then
#Region "Input Panel Support"

    Private Sub EnableInputPanelOnFormLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InputPanelSwitch(True)
    End Sub

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed
        Me.InputPanel.Dispose()
    End Sub

    Private Sub InputPanel_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InputPanel.EnabledChanged
        If Me.Owner IsNot Nothing Then
            InputPanelSwitch(False)
        End If
    End Sub

    Private Sub InputPanelSwitch(Optional ByVal loadInitial As Boolean = False)
        Const METHODNAME As String = "InputPanelSwitch"
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
            LogError(MODULENAME, METHODNAME, ex, False, String.Empty)
        End Try
    End Sub
#End Region

#Region "Window State Support"

    Private Sub ViewFullScreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewFullScreen.Click
        SetFormState(Me, Me.ViewFullScreen, Not Me.ViewFullScreen.Checked)
    End Sub

    Private Sub ViewOrientation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewRotate0.Click, ViewRotate90.Click, ViewRotate180.Click, ViewRotate270.Click
        If sender Is Me.ViewRotate0 Then
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle0
        ElseIf sender Is Me.ViewRotate90 Then
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle90
        ElseIf sender Is Me.ViewRotate180 Then
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle180
        ElseIf sender Is Me.ViewRotate270 Then
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle270
        End If
        FixOrientationMenu()
    End Sub

    Private Sub FixOrientationMenu()
        Me.ViewRotate0.Checked = False
        Me.ViewRotate90.Checked = False
        Me.ViewRotate180.Checked = False
        Me.ViewRotate270.Checked = False
        Select Case SystemSettings.ScreenOrientation
            Case ScreenOrientation.Angle0
                Me.ViewRotate0.Checked = True
            Case ScreenOrientation.Angle90
                Me.ViewRotate90.Checked = True
            Case ScreenOrientation.Angle180
                Me.ViewRotate180.Checked = True
            Case ScreenOrientation.Angle270
                Me.ViewRotate270.Checked = True
        End Select
    End Sub
#End Region
#End If

    Private Sub SettingsForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Dim x As Integer = (Me.Width - Me.WebServiceCredentialsButton.Width) \ 2

        Me.WebServiceCredentialsButton.Left = x
        Me.ActiviserPasswordButton.Left = x
        Me.ChangeServerSettingsButton.Left = x
    End Sub

    Private Sub TextSizePicker_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextSizePicker.ValueChanged
        Me.ExampleText.Font = New Font(Me.ExampleText.Font.Name, Me.TextSizePicker.Value, FontStyle.Regular)
    End Sub

    Private Sub DayPickerPanel_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DayPickerPanel.GotFocus

    End Sub

    Private Sub MainMenuClearClientInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuClearClientInfo.Click

    End Sub
End Class
