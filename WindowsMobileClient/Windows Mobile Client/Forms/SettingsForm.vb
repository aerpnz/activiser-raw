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

    Private noShortCuts As String = Terminology.GetString(MODULENAME, RES_SettingsTasksEmpty)

    Private mboolDirty As Boolean

    Private Sub LoadTimeSelectors()
        Me.SyncStartTimeSelector.Items.Clear()
        Me.SyncFinishTimeSelector.Items.Clear()
        For h As Integer = 23 To 0 Step -1
            For m As Integer = 45 To 0 Step -15
                Dim dt As New DateTime(1900, 1, 1, h, m, 0)
                Me.SyncStartTimeSelector.Items.Add(New SelectorItem(Of TimeSpan)(dt.TimeOfDay, dt.ToShortTimeString))
                Me.SyncFinishTimeSelector.Items.Add(New SelectorItem(Of TimeSpan)(dt.TimeOfDay, dt.ToShortTimeString))
            Next
        Next
    End Sub

    ' load up in reverse so the 'Up/Down' buttons make sense.
    Private Shared ReadOnly timeIncrementOptions As Integer() = New Integer() {60, 30, 15, 10, 5, 1}
    Private Shared ReadOnly durationOptions As Integer() = New Integer() {120, 60, 45, 30, 15, 10, 5, 0}
    Private Shared ReadOnly travelTimeOptions As Integer() = New Integer() {60, 45, 30, 15, 10, 5, 0}

    Private Sub LoadOffsetSelectors()
        Me.JobDefaultDurationSelector.Items.Clear()
        Me.JobDefaultTravelTimeSelector.Items.Clear()
        Me.JobDefaultStartOffsetSelector.Items.Clear()
        Me.JobTimeIntervalSelector.Items.Clear()

        ' load up in reverse so the 'Up/Down' buttons make sense.
        For m As Integer = 60 To -60 Step -15
            Me.JobDefaultStartOffsetSelector.Items.Add(New SelectorItem(Of Integer)(m, m.ToString()))
        Next

        For Each m As Integer In durationOptions
            Me.JobDefaultDurationSelector.Items.Add(New SelectorItem(Of Integer)(m, m.ToString()))
        Next

        For Each m As Integer In travelTimeOptions
            Me.JobDefaultTravelTimeSelector.Items.Add(New SelectorItem(Of Integer)(m, m.ToString()))
        Next

        For Each m As Integer In timeIncrementOptions
            Me.JobTimeIntervalSelector.Items.Add(New SelectorItem(Of Integer)(m, m.ToString()))
        Next
    End Sub

    Private Shared ReadOnly historyNumberOptions As Integer() = New Integer() {100, 50, 25, 10, 5}
    Private Sub LoadHistoryNumbers()
        For Each m As Integer In historyNumberOptions
            Me.HistoryNumberSelector.Items.Add(New SelectorItem(Of Integer)(m, m.ToString()))
        Next
    End Sub
    Private Sub LoadHistoryAges()
        HistoryAgeSelector.Items.Clear()
        HistoryAgeSelector.Items.Add(New SelectorItem(Of Integer)(0, Terminology.GetString(MODULENAME, RES_JobHistoryLimitNone)))
        HistoryAgeSelector.Items.Add(New SelectorItem(Of Integer)(183, Terminology.GetString(MODULENAME, RES_JobHistoryLimit183Days)))
        HistoryAgeSelector.Items.Add(New SelectorItem(Of Integer)(92, Terminology.GetString(MODULENAME, RES_JobHistoryLimit92Days)))
        HistoryAgeSelector.Items.Add(New SelectorItem(Of Integer)(31, Terminology.GetString(MODULENAME, RES_JobHistoryLimit31Days)))
        HistoryAgeSelector.Items.Add(New SelectorItem(Of Integer)(14, Terminology.GetString(MODULENAME, RES_JobHistoryLimit14Days)))
        HistoryAgeSelector.Items.Add(New SelectorItem(Of Integer)(7, Terminology.GetString(MODULENAME, RES_JobHistoryLimit7Days)))
    End Sub

    Private Sub LoadShortCuts()
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
    End Sub


    Private Sub LoadHistoryNumber()
#If FRAMEWORK_VERSION_GE35 Then
        Dim i As Integer = HistoryNumberSelector.Items.IndexOf(Of Integer)(ConsultantConfig.JobHistoryLimit)
#Else
        Dim i As Integer = IndexOf(Of Integer)(HistoryNumberSelector.Items, ConsultantConfig.JobHistoryLimit)
#End If
        If i <> -1 Then
            HistoryNumberSelector.SelectedIndex = i
        Else
            HistoryNumberSelector.SelectedIndex = 0
        End If
    End Sub

    Private Sub LoadHistoryAge()
#If FRAMEWORK_VERSION_GE35 Then
        Dim i As Integer = HistoryAgeSelector.Items.IndexOf(Of Integer)(ConsultantConfig.JobHistoryAgeLimit)
#Else
        Dim i As Integer = IndexOf(Of Integer)(HistoryAgeSelector.Items, ConsultantConfig.JobHistoryAgeLimit)
#End If
        If i <> -1 Then
            HistoryAgeSelector.SelectedIndex = i
        Else
            HistoryAgeSelector.SelectedIndex = 0
        End If
    End Sub


    Private Sub LoadDefaultJobDuration()
        ' Load Default Job Duration
        Try
            Dim ddj As Integer = AppConfig.GetSetting(My.Resources.AppConfigDefaultJobDurationKey, 15)
#If FRAMEWORK_VERSION_GE35 Then
            Dim i As Integer = JobDefaultDurationSelector.Items.IndexOf(Of Integer)(ddj)
#Else
            Dim i As Integer = IndexOf(Of Integer)(JobDefaultDurationSelector.Items, ddj)
#End If

            If i = -1 Then
#If FRAMEWORK_VERSION_GE35 Then
                Me.JobDefaultDurationSelector.SelectedIndex = Me.JobDefaultDurationSelector.Items.IndexOf(Of Integer)(15)
#Else
                Me.JobDefaultDurationSelector.SelectedIndex = IndexOf(Of Integer)(Me.JobDefaultDurationSelector.Items, 15)
#End If

            Else
                Me.JobDefaultDurationSelector.SelectedIndex = i
            End If
        Catch ex As Exception
            LogError(MODULENAME, "LoadDefaultJobDuration", ex, False, RES_ErrorLoadingDefaultJobDuration)

        End Try
    End Sub


    Private Sub LoadTimePickerInterval()
        ' Load Time Picker Interval
        Try
            Dim jti As Integer = AppConfig.GetSetting(My.Resources.AppConfigTimePickerIntervalKey, 5)
#If FRAMEWORK_VERSION_GE35 Then
            Dim i As Integer = Me.JobTimeIntervalSelector.Items.IndexOf(Of Integer)(jti)
#Else
            Dim i As Integer = IndexOf(Of Integer)(Me.JobTimeIntervalSelector.Items, jti)
#End If
            If i = -1 Then
#If FRAMEWORK_VERSION_GE35 Then
                Me.JobTimeIntervalSelector.SelectedIndex = Me.JobTimeIntervalSelector.Items.IndexOf(Of Integer)(5)
#Else
                Me.JobTimeIntervalSelector.SelectedIndex = IndexOf(Of Integer)(Me.JobTimeIntervalSelector.Items, 5)
#End If
            Else
                Me.JobTimeIntervalSelector.SelectedIndex = i
            End If
        Catch ex As Exception
            LogError(MODULENAME, "LoadTimePickerInterval", ex, False, RES_ErrorLoadingJobTimeInterval)

        End Try
    End Sub


    Private Sub LoadDefaultJobStartTimeOffset()
        ' Load Default Job Start Time Offset
        Try
            Dim jso As Integer = AppConfig.GetSetting(My.Resources.AppConfigDefaultJobStartKey, 0)
#If FRAMEWORK_VERSION_GE35 Then
            Dim i As Integer = Me.JobDefaultStartOffsetSelector.Items.IndexOf(Of Integer)(jso)
#Else
            Dim i As Integer = IndexOf(Of Integer)(Me.JobDefaultStartOffsetSelector.Items, jso)
#End If
            If i = -1 Then
#If FRAMEWORK_VERSION_GE35 Then
                Me.JobDefaultStartOffsetSelector.SelectedIndex = Me.JobDefaultStartOffsetSelector.Items.IndexOf(Of Integer)(0)
#Else
                Me.JobDefaultStartOffsetSelector.SelectedIndex = IndexOf(Of Integer)(Me.JobDefaultStartOffsetSelector.Items, 0)
#End If
            Else
                Me.JobDefaultStartOffsetSelector.SelectedIndex = i
            End If
        Catch ex As Exception
            LogError(MODULENAME, "LoadDefaultJobStartTimeOffset", ex, False, RES_ErrorLoadingJobStartOffset)

        End Try
    End Sub


    Private Sub LoadDefaultTravelTime()
        ' Load Default Travel Time
        Try
            Dim tto As Integer = AppConfig.GetSetting(My.Resources.AppConfigDefaultTravelTimeKey, 15)
#If FRAMEWORK_VERSION_GE35 Then
            Dim i As Integer = Me.JobDefaultTravelTimeSelector.Items.IndexOf(Of Integer)(tto)
#Else
            Dim i As Integer = IndexOf(Of Integer)(Me.JobDefaultTravelTimeSelector.Items, tto)
#End If
            If i = -1 Then
#If FRAMEWORK_VERSION_GE35 Then
                Me.JobDefaultTravelTimeSelector.SelectedIndex = Me.JobDefaultTravelTimeSelector.Items.IndexOf(Of Integer)(15)
#Else
                Me.JobDefaultTravelTimeSelector.SelectedIndex = IndexOf(Of Integer)(Me.JobDefaultTravelTimeSelector.Items, 15)
#End If
            Else
                Me.JobDefaultTravelTimeSelector.SelectedIndex = i
            End If
        Catch ex As Exception
            LogError(MODULENAME, "LoadDefaultTravelTime", ex, False, RES_ErrorLoadingDefaultTravelTime)

        End Try
    End Sub


    '
    Private Sub LoadAutoSyncStartTime()
        ' Load Auto Sync Start Time
        Try
#If FRAMEWORK_VERSION_GE35 Then
            Dim i As Integer = Me.SyncStartTimeSelector.Items.IndexOf(Of TimeSpan)(gSyncStartTime.TimeOfDay)
#Else
            Dim i As Integer = IndexOf(Of TimeSpan)(Me.SyncStartTimeSelector.Items, gSyncStartTime.TimeOfDay)
#End If

            If i <> -1 Then
                Me.SyncStartTimeSelector.SelectedIndex = i
            Else
                Me.SyncStartTimeSelector.SelectedIndex = 0
            End If

        Catch ex As Exception
            LogError(MODULENAME, "LoadAutoSyncStartTime", ex, False, RES_ErrorLoadingSyncStartTime)

        End Try
    End Sub


    Private Sub LoadAutoSyncFinishTime()
        'Load Auto Sync Finish Time
        Try
#If FRAMEWORK_VERSION_GE35 Then
            Dim i As Integer = Me.SyncFinishTimeSelector.Items.IndexOf(Of TimeSpan)(gSyncFinishTime.TimeOfDay)
#Else
            Dim i As Integer = IndexOf(Of TimeSpan)(Me.SyncFinishTimeSelector.Items, gSyncFinishTime.TimeOfDay)
#End If
            If i <> -1 Then
                Me.SyncFinishTimeSelector.SelectedIndex = i
            Else
                Me.SyncFinishTimeSelector.SelectedIndex = 0
            End If

        Catch ex As Exception
            LogError(MODULENAME, "LoadAutoSyncFinishTime", ex, False, RES_ErrorLoadingSyncFinishTime)
        End Try
    End Sub

    Private Sub LoadAutoSyncSettings()
        ' Load Auto Sync Settings
        Try
            Me.AutoSyncCheckBox.Checked = gAutoSync
            If gAutoSyncInterval >= 5 Then
                Me.SyncIntervalNumberBox.Value = (gAutoSyncInterval \ 5) * 5
            Else
                Me.SyncIntervalNumberBox.Value = 60
            End If
        Catch ex As Exception
            LogError(MODULENAME, "LoadAutoSyncSettings", ex, False, RES_ErrorLoadingAutoSyncInterval)

        End Try

        Try
            Me.MondayCheckbox.Checked = gSyncDays.IndexOf("M", StringComparison.OrdinalIgnoreCase) <> -1
            Me.TuesdayCheckbox.Checked = gSyncDays.IndexOf("T", StringComparison.OrdinalIgnoreCase) <> -1
            Me.WednesdayCheckbox.Checked = gSyncDays.IndexOf("W", StringComparison.OrdinalIgnoreCase) <> -1
            Me.ThursdayCheckbox.Checked = gSyncDays.IndexOf("H", StringComparison.OrdinalIgnoreCase) <> -1
            Me.FridayCheckbox.Checked = gSyncDays.IndexOf("F", StringComparison.OrdinalIgnoreCase) <> -1
            Me.SaturdayCheckbox.Checked = gSyncDays.IndexOf("S", StringComparison.OrdinalIgnoreCase) <> -1
            Me.SundayCheckbox.Checked = gSyncDays.IndexOf("U", StringComparison.OrdinalIgnoreCase) <> -1
        Catch ex As Exception
            LogError(MODULENAME, "LoadAutoSyncSettings", ex, False, RES_ErrorLoadingSyncDays)
        End Try

        LoadAutoSyncStartTime()

        LoadAutoSyncFinishTime()
    End Sub


    Private Sub LoadTextSizeSettings()
        ' Load Text Size Settings
        Try
            Me.TextSizePicker.Value = AppConfig.GetSetting(My.Resources.AppConfigTextSizeKey, 8)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadSettings()
        Const METHODNAME As String = "LoadSettings"
        Try
            'Me.DeviceIDTextBox.Text = GetFormattedDeviceID()

            LoadShortCuts()
            LoadHistoryNumber()
            LoadHistoryAge()
            LoadDefaultJobDuration()
            LoadTimePickerInterval()
            LoadDefaultJobStartTimeOffset()
            LoadDefaultTravelTime()
            LoadAutoSyncSettings()
            LoadTextSizeSettings()

            mboolDirty = False
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
    End Sub

    Private Shared Sub SaveSelectorValue(ByVal selector As DomainUpDown, ByVal key As String, ByVal warningMessage As String)
        Dim item As SelectorItem(Of Integer) = TryCast(selector.Items(selector.SelectedIndex), SelectorItem(Of Integer))
        If item IsNot Nothing Then
            AppConfig.SaveSetting(key, item.Value)
        Else
            MessageBox.Show(warningMessage)
        End If
    End Sub

    Private Shared Function SaveTimeSelectorValue(ByVal selector As DomainUpDown, ByVal key As String, ByVal warningMessage As String) As DateTime
        Dim item As SelectorItem(Of TimeSpan) = TryCast(selector.Items(selector.SelectedIndex), SelectorItem(Of TimeSpan))
        If item IsNot Nothing Then
            AppConfig.SaveSetting(key, item.Value.ToString())
            Return New DateTime(1900, 1, 1).Add(item.Value)
        Else
            MessageBox.Show(warningMessage)
            Return New DateTime(1900, 1, 1, 0, 0, 0)
        End If
    End Function

    Private Sub SaveShortcuts()
        ' Save Shortcuts
        If Me.ShortCutList.Text <> noShortCuts Then
            ConsultantConfig.UpdateShortcuts(Me.ShortCutList.Text)
        End If
    End Sub


    Private Sub SaveJobHistoryQuantityLimit()
        ' Save Job History Quantity Limit
        Dim item As SelectorItem(Of Integer) = TryCast(HistoryNumberSelector.Items(HistoryNumberSelector.SelectedIndex), SelectorItem(Of Integer))
        If item IsNot Nothing Then
            ConsultantConfig.JobHistoryLimit = item.Value
        Else
            ConsultantConfig.JobHistoryLimit = 5
        End If

        gintJobHistoryCount = ConsultantConfig.JobHistoryLimit
    End Sub


    Private Sub SaveJobHistoryAgeLimit()
        ' Save Job History Age Limit
        Dim item As SelectorItem(Of Integer) = TryCast(HistoryAgeSelector.Items(HistoryAgeSelector.SelectedIndex), SelectorItem(Of Integer))
        If item IsNot Nothing Then
            ConsultantConfig.JobHistoryAgeLimit = item.Value
        End If

        gintJobHistoryAge = ConsultantConfig.JobHistoryAgeLimit
    End Sub


    Private Sub SaveDefaultJobDuration()
        ' Save Default Job Duration
        SaveSelectorValue(Me.JobDefaultDurationSelector, _
              My.Resources.AppConfigDefaultJobDurationKey, _
              "Warning: unable to set default job duration (internal list error)")
    End Sub


    Private Sub SaveTimePickerInterval()
        ' Save Time Picker Interval
        SaveSelectorValue(Me.JobTimeIntervalSelector, _
              My.Resources.AppConfigTimePickerIntervalKey, _
              "Warning: unable to set job time picker interval (internal list error)")
    End Sub


    Private Sub SaveDefaultJobStartTimeOffset()
        ' Save Job Default Start Time Offset
        SaveSelectorValue(Me.JobDefaultStartOffsetSelector, _
              My.Resources.AppConfigDefaultJobStartKey, _
              "Warning: unable to set default job start offset(internal list error)")
    End Sub


    Private Sub SaveDefaultTravelTime()
        ' Save Default Travel Time
        SaveSelectorValue(Me.JobDefaultTravelTimeSelector, _
              My.Resources.AppConfigDefaultTravelTimeKey, _
              "Warning: unable to set default travel time (internal list error)")
    End Sub


    Private Sub SaveAutoSyncSettings()
        ' Save Auto Sync Settings
        Try
            Dim autoSyncInterval As Integer = CInt(Me.SyncIntervalNumberBox.Value)
            AppConfig.SaveSetting(My.Resources.AppConfigAutoSyncIntervalKey, autoSyncInterval)
            AppConfig.SaveSetting(My.Resources.AppConfigAutoSyncKey, Me.AutoSyncCheckBox.Checked)
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
        AppConfig.SaveSetting(My.Resources.AppConfigSyncDaysKey, syncDayList)

        gSyncStartTime = SaveTimeSelectorValue(Me.SyncStartTimeSelector, My.Resources.AppConfigSyncStartTimeKey, _
                              "Warning: unable to save auto-sync start time")

        gSyncFinishTime = SaveTimeSelectorValue(Me.SyncFinishTimeSelector, My.Resources.AppConfigSyncFinishTimeKey, _
                              "Warning: unable to save auto-sync finish time")
        'gSyncStartTime = CDate(Me.SyncStartTimeSelector.Text)
        'gSyncFinishTime = CDate(Me.SyncFinishTimeSelector.Text)
        'AppConfig.SaveSetting(My.Resources.AppConfigSyncStartTimeKey, gSyncStartTime.ToShortTimeString)
        'AppConfig.SaveSetting(My.Resources.AppConfigSyncFinishTimeKey, gSyncFinishTime.ToShortTimeString)
    End Sub


    Private Sub SaveTextSizeSettings()
        'Save Text Size Settings
        AppConfig.SaveSetting(My.Resources.AppConfigTextSizeKey, Me.TextSizePicker.Value)
    End Sub

    Sub SaveSettings()
        Const METHODNAME As String = "SaveSettings"
        Try
            SaveShortcuts()
            SaveJobHistoryQuantityLimit()
            SaveJobHistoryAgeLimit()
            SaveDefaultJobDuration()
            SaveTimePickerInterval()
            SaveDefaultJobStartTimeOffset()
            SaveDefaultTravelTime()
            SaveAutoSyncSettings()
            SaveTextSizeSettings()

            mboolDirty = False

        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, Nothing)
        End Try
        AppConfig.Save()
        ConsultantConfig.SavePending()
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

        Me.LoadTimeSelectors()
        Me.LoadOffsetSelectors()
        Me.LoadHistoryNumbers()
        Me.LoadHistoryAges()

        Me.LoadSettings()

#If MINORPLANETCLIENT Then
        EnableContextMenus(Me.Controls)
#End If

    End Sub

    Private Sub mnuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainMenuClose.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub MakeDirty(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShortCutList.TextChanged, JobTimeIntervalSelector.SelectedItemChanged, JobDefaultStartOffsetSelector.SelectedItemChanged, JobDefaultDurationSelector.SelectedItemChanged, AutoSyncCheckBox.Click
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
                InputPanel.Enabled = AppConfig.GetSetting(MODULENAME & My.Resources.InputPanelEnabledRegValueName, False)
                Me.InputPanelPanel.Visible = InputPanel.Enabled
                Me.InputPanelPanel.Height = InputPanel.Bounds.Height
            Else
                Me.InputPanelPanel.Height = InputPanel.Bounds.Height
                Me.InputPanelPanel.Visible = InputPanel.Enabled
                AppConfig.SaveSetting(MODULENAME & My.Resources.InputPanelEnabledRegValueName, Me.InputPanel.Enabled)
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
        Me.TextSizeExample.Font = New Font(Me.TextSizeExample.Font.Name, Me.TextSizePicker.Value, FontStyle.Regular)
    End Sub
End Class
