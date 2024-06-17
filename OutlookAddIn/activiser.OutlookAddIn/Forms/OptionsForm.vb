Imports System.Windows.Forms

Public Class OptionsForm
    Const MODULENAME As String = "OptionsForm"

    Const STR_RequestStatusID As String = "RequestStatusID"
    Const STR_Description As String = "Description"

    Private _loaded As Boolean

    Friend Enum ShowTimeAsOptions
        ShowTimeAsFree
        ShowTimeAsTentative
        ShowTimeAsBusy
        ShowTimeAsOutOfOffice
    End Enum

    Private requestStatusList As New CategoryObjectCollection("OptionsForm:requestStatusList")

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub OptionsForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult = Windows.Forms.DialogResult.Cancel Then
            My.Settings.Reset()
        Else
            My.Settings.Save()
        End If
    End Sub

    Private Sub Options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Terminology.LoadLabels(Me)
        If My.Computer.Keyboard.ShiftKeyDown Then Return

        If Not LoggedOn Then
            If Not LogInOut.Logon() Then
                Terminology.DisplayFormattedMessage(My.Resources.SharedMessagesKey, RES_MustBeLoggedOn, MessageBoxIcon.Exclamation, RES_UnableToLoadRequestStatusList)
                Return
            Else
                ThisAddIn.SetIconState()
            End If
        End If

        Dim ds As DataSet = ConsoleData.WebService.GetNewRequestData(Main.deviceId, ConsoleData.ConsoleUser.ConsultantUID)
        CategoryObjectCollection.PopulateList(ds, Me.DefaultRequestStatusList, Me.requestStatusList, "RequestStatus", STR_RequestStatusID, STR_Description)

        If Me.DefaultRequestStatusList.Items.Count > 0 Then
            If Me.requestStatusList.Contains(My.Settings.DefaultNewRequestStatus) Then
                Dim item As CategoryObjectItem = Me.requestStatusList.GetItem(My.Settings.DefaultNewRequestStatus)
                Me.DefaultRequestStatusList.SelectedValue = item
            End If
        End If

        Select Case My.Settings.ShowTimeAsDefault
            Case ShowTimeAsOptions.ShowTimeAsBusy
                Me.RadioBusy.Checked = True
            Case ShowTimeAsOptions.ShowTimeAsFree
                Me.RadioFree.Checked = True
            Case ShowTimeAsOptions.ShowTimeAsOutOfOffice
                Me.RadioOutOfOffice.Checked = True
            Case ShowTimeAsOptions.ShowTimeAsTentative
                Me.RadioTentative.Checked = True
            Case Else
                Me.RadioBusy.Checked = True
        End Select

        Select Case My.Settings.NextActionDateOption
            Case NextActionDateOptions.FinishDay
                Me.RadioNadFinishDate.Checked = True
            Case NextActionDateOptions.FirstBusinessDayAfterFinishDay
                Me.RadioNadFirstBusDayAfterFinish.Checked = True
            Case NextActionDateOptions.NextBusinessDay
                Me.RadioNadNextBusDay.Checked = True
            Case NextActionDateOptions.StartDay
                Me.RadioNadStartDate.Checked = True
            Case NextActionDateOptions.Today
                Me.RadioNadToday.Checked = True
            Case Else
                Me.RadioNadStartDate.Checked = True
        End Select

        _loaded = True
    End Sub

    Private Sub Radio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioTentative.CheckedChanged, RadioOutOfOffice.CheckedChanged, RadioFree.CheckedChanged, RadioBusy.CheckedChanged
        If Me.RadioFree.Checked Then
            My.Settings.ShowTimeAsDefault = ShowTimeAsOptions.ShowTimeAsFree
        ElseIf Me.RadioTentative.Checked Then
            My.Settings.ShowTimeAsDefault = ShowTimeAsOptions.ShowTimeAsTentative
        ElseIf Me.RadioBusy.Checked Then
            My.Settings.ShowTimeAsDefault = ShowTimeAsOptions.ShowTimeAsBusy
        ElseIf Me.RadioOutOfOffice.Checked Then
            My.Settings.ShowTimeAsDefault = ShowTimeAsOptions.ShowTimeAsOutOfOffice
        End If
    End Sub

    Private Sub DefaultRequestStatusList_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles DefaultRequestStatusList.SelectionChangeCommitted
        My.Settings.DefaultNewRequestStatus = CInt(Me.DefaultRequestStatusList.SelectedValue)
    End Sub

    Private Sub ServerUrlTestButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServerUrlTestButton.Click
        InteractiveUrlTest(Me.ServerUrlTextBox.Text, Me)
    End Sub

    Private Sub LoginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginButton.Click
        If Not TryLogon(Me.ServerUrlTextBox.Text, Me.IntegratedAuthenticationCheckBox.Checked, Me.activiserUserNameTextBox.Text, Me.activiserPasswordTextBox.Text, Me.LinkUserCheckbox.Checked, Me) Then
            Me.activiserUserNameTextBox.Focus()
            Return
        End If
    End Sub

    'Private _inAutoLogonCheckedChanged As Boolean
    'Private Sub AutoLogonCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AutoLogonCheckBox.CheckedChanged
    '    If _inAutoLogonCheckedChanged Then Return
    '    _inAutoLogonCheckedChanged = True
    '    'If My.Settings.AutoDomainLogon <> AutoLogonCheckBox.Checked Then
    '    My.Settings.AutoDomainLogon = AutoLogonCheckBox.Checked
    '    'End If
    '    _inAutoLogonCheckedChanged = False
    'End Sub

    Private Sub NadRadio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not _loaded Then Return
        Dim rb As RadioButton = TryCast(sender, RadioButton)
        If rb IsNot Nothing Then
            If rb Is Me.RadioNadStartDate Then
                My.Settings.NextActionDateOption = NextActionDateOptions.StartDay
                My.Settings.IncludeTimeInNextActiondate = False
            ElseIf rb Is Me.RadioNadStartTime Then
                My.Settings.NextActionDateOption = NextActionDateOptions.StartTime
                My.Settings.IncludeTimeInNextActiondate = True
            ElseIf rb Is Me.RadioNadFinishDate Then
                My.Settings.NextActionDateOption = NextActionDateOptions.FinishDay
                My.Settings.IncludeTimeInNextActiondate = False
            ElseIf rb Is Me.RadioNadFinishTime Then
                My.Settings.NextActionDateOption = NextActionDateOptions.FinishTime
                My.Settings.IncludeTimeInNextActiondate = True
            ElseIf rb Is Me.RadioNadFirstBusDayAfterFinish Then
                My.Settings.NextActionDateOption = NextActionDateOptions.FirstBusinessDayAfterFinishDay
                My.Settings.IncludeTimeInNextActiondate = False
            ElseIf rb Is Me.RadioNadNextBusDay Then
                My.Settings.NextActionDateOption = NextActionDateOptions.NextBusinessDay
                My.Settings.IncludeTimeInNextActiondate = False
            ElseIf rb Is Me.RadioNadToday Then
                My.Settings.NextActionDateOption = NextActionDateOptions.Today
                My.Settings.IncludeTimeInNextActiondate = False
            End If
        End If
    End Sub
End Class
