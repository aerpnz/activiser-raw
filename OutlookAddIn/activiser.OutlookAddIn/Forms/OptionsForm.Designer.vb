<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsForm
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OptionsForm))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.ActiviserTabPage = New System.Windows.Forms.TabPage
        Me.RequestDefaultsGroup = New System.Windows.Forms.GroupBox
        Me.DefaultRequestStatusList = New System.Windows.Forms.ComboBox
        Me.DefaultRequestStatusLabel = New System.Windows.Forms.Label
        Me.UserGroup = New System.Windows.Forms.GroupBox
        Me.LinkUserCheckbox = New System.Windows.Forms.CheckBox
        Me.LoginButton = New System.Windows.Forms.Button
        Me.IntegratedAuthenticationCheckBox = New System.Windows.Forms.CheckBox
        Me.activiserPasswordTextBox = New System.Windows.Forms.TextBox
        Me.ActiviserPasswordLabel = New System.Windows.Forms.Label
        Me.activiserUserNameTextBox = New System.Windows.Forms.TextBox
        Me.ActiviserUsernameLabel = New System.Windows.Forms.Label
        Me.ServerGroup = New System.Windows.Forms.GroupBox
        Me.ServerUrlLabel = New System.Windows.Forms.Label
        Me.ServerUrlTextBox = New System.Windows.Forms.TextBox
        Me.IgnoreCertificateErrorsCheckBox = New System.Windows.Forms.CheckBox
        Me.ServerUrlTestButton = New System.Windows.Forms.Button
        Me.GeneralGroup = New System.Windows.Forms.GroupBox
        Me.AutoLogonCheckBox = New System.Windows.Forms.CheckBox
        Me.CalendarTabPage = New System.Windows.Forms.TabPage
        Me.ClientAddressGroup = New System.Windows.Forms.GroupBox
        Me.PrependAddressCheckBox = New System.Windows.Forms.CheckBox
        Me.AppendAddressCheckBox = New System.Windows.Forms.CheckBox
        Me.ShowTimeAsLabel = New System.Windows.Forms.GroupBox
        Me.RadioOutOfOffice = New System.Windows.Forms.RadioButton
        Me.RadioBusy = New System.Windows.Forms.RadioButton
        Me.RadioTentative = New System.Windows.Forms.RadioButton
        Me.RadioFree = New System.Windows.Forms.RadioButton
        Me.RequestTab = New System.Windows.Forms.TabPage
        Me.NextActionDateEditGroup = New System.Windows.Forms.GroupBox
        Me.RadioNadFinishTime = New System.Windows.Forms.RadioButton
        Me.RadioNadStartTime = New System.Windows.Forms.RadioButton
        Me.RadioNadFirstBusDayAfterFinish = New System.Windows.Forms.RadioButton
        Me.RadioNadNextBusDay = New System.Windows.Forms.RadioButton
        Me.RadioNadToday = New System.Windows.Forms.RadioButton
        Me.RadioNadFinishDate = New System.Windows.Forms.RadioButton
        Me.RadioNadStartDate = New System.Windows.Forms.RadioButton
        Me.NextActionDateCutPasteGroup = New System.Windows.Forms.GroupBox
        Me.RadioMoveNadFinishTime = New System.Windows.Forms.RadioButton
        Me.RadioMoveNadStartTime = New System.Windows.Forms.RadioButton
        Me.RadioMoveNadBusDayAfterFinish = New System.Windows.Forms.RadioButton
        Me.RadioMoveNadBusDayAfterToday = New System.Windows.Forms.RadioButton
        Me.RadioMoveNadToday = New System.Windows.Forms.RadioButton
        Me.RadioMoveNadFinishDay = New System.Windows.Forms.RadioButton
        Me.RadioMoveNadStartDate = New System.Windows.Forms.RadioButton
        Me.NadRulesGroup = New System.Windows.Forms.GroupBox
        Me.RadioMoveNadDoNothing = New System.Windows.Forms.RadioButton
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.ActiviserTabPage.SuspendLayout()
        Me.RequestDefaultsGroup.SuspendLayout()
        Me.UserGroup.SuspendLayout()
        Me.ServerGroup.SuspendLayout()
        Me.GeneralGroup.SuspendLayout()
        Me.CalendarTabPage.SuspendLayout()
        Me.ClientAddressGroup.SuspendLayout()
        Me.ShowTimeAsLabel.SuspendLayout()
        Me.RequestTab.SuspendLayout()
        Me.NextActionDateEditGroup.SuspendLayout()
        Me.NextActionDateCutPasteGroup.SuspendLayout()
        Me.NadRulesGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'OK_Button
        '
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.Name = "OK_Button"
        '
        'Cancel_Button
        '
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Name = "Cancel_Button"
        '
        'TabControl1
        '
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.ActiviserTabPage)
        Me.TabControl1.Controls.Add(Me.CalendarTabPage)
        Me.TabControl1.Controls.Add(Me.RequestTab)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        '
        'ActiviserTabPage
        '
        Me.ActiviserTabPage.Controls.Add(Me.RequestDefaultsGroup)
        Me.ActiviserTabPage.Controls.Add(Me.UserGroup)
        Me.ActiviserTabPage.Controls.Add(Me.ServerGroup)
        Me.ActiviserTabPage.Controls.Add(Me.GeneralGroup)
        resources.ApplyResources(Me.ActiviserTabPage, "ActiviserTabPage")
        Me.ActiviserTabPage.Name = "ActiviserTabPage"
        Me.ActiviserTabPage.UseVisualStyleBackColor = True
        '
        'RequestDefaultsGroup
        '
        Me.RequestDefaultsGroup.Controls.Add(Me.DefaultRequestStatusList)
        Me.RequestDefaultsGroup.Controls.Add(Me.DefaultRequestStatusLabel)
        resources.ApplyResources(Me.RequestDefaultsGroup, "RequestDefaultsGroup")
        Me.RequestDefaultsGroup.Name = "RequestDefaultsGroup"
        Me.RequestDefaultsGroup.TabStop = False
        '
        'DefaultRequestStatusList
        '
        Me.DefaultRequestStatusList.FormattingEnabled = True
        resources.ApplyResources(Me.DefaultRequestStatusList, "DefaultRequestStatusList")
        Me.DefaultRequestStatusList.Name = "DefaultRequestStatusList"
        '
        'DefaultRequestStatusLabel
        '
        resources.ApplyResources(Me.DefaultRequestStatusLabel, "DefaultRequestStatusLabel")
        Me.DefaultRequestStatusLabel.Name = "DefaultRequestStatusLabel"
        '
        'UserGroup
        '
        Me.UserGroup.Controls.Add(Me.LinkUserCheckbox)
        Me.UserGroup.Controls.Add(Me.LoginButton)
        Me.UserGroup.Controls.Add(Me.IntegratedAuthenticationCheckBox)
        Me.UserGroup.Controls.Add(Me.activiserPasswordTextBox)
        Me.UserGroup.Controls.Add(Me.ActiviserPasswordLabel)
        Me.UserGroup.Controls.Add(Me.activiserUserNameTextBox)
        Me.UserGroup.Controls.Add(Me.ActiviserUsernameLabel)
        resources.ApplyResources(Me.UserGroup, "UserGroup")
        Me.UserGroup.Name = "UserGroup"
        Me.UserGroup.TabStop = False
        '
        'LinkUserCheckbox
        '
        resources.ApplyResources(Me.LinkUserCheckbox, "LinkUserCheckbox")
        Me.LinkUserCheckbox.Name = "LinkUserCheckbox"
        Me.LinkUserCheckbox.UseVisualStyleBackColor = True
        '
        'LoginButton
        '
        resources.ApplyResources(Me.LoginButton, "LoginButton")
        Me.LoginButton.BackColor = System.Drawing.Color.GhostWhite
        Me.LoginButton.Image = Global.activiser.OutlookAddIn.My.Resources.Resources.users2
        Me.LoginButton.Name = "LoginButton"
        Me.LoginButton.UseVisualStyleBackColor = False
        '
        'IntegratedAuthenticationCheckBox
        '
        resources.ApplyResources(Me.IntegratedAuthenticationCheckBox, "IntegratedAuthenticationCheckBox")
        Me.IntegratedAuthenticationCheckBox.Checked = Global.activiser.OutlookAddIn.MySettings.Default.AutoDomainLogon
        Me.IntegratedAuthenticationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.IntegratedAuthenticationCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.activiser.OutlookAddIn.MySettings.Default, "AutoDomainLogon", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.IntegratedAuthenticationCheckBox.Name = "IntegratedAuthenticationCheckBox"
        Me.IntegratedAuthenticationCheckBox.UseVisualStyleBackColor = True
        '
        'activiserPasswordTextBox
        '
        resources.ApplyResources(Me.activiserPasswordTextBox, "activiserPasswordTextBox")
        Me.activiserPasswordTextBox.DataBindings.Add(New System.Windows.Forms.Binding("ReadOnly", Global.activiser.OutlookAddIn.MySettings.Default, "AutoDomainLogon", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.activiserPasswordTextBox.Name = "activiserPasswordTextBox"
        Me.activiserPasswordTextBox.ReadOnly = Global.activiser.OutlookAddIn.MySettings.Default.AutoDomainLogon
        '
        'ActiviserPasswordLabel
        '
        resources.ApplyResources(Me.ActiviserPasswordLabel, "ActiviserPasswordLabel")
        Me.ActiviserPasswordLabel.BackColor = System.Drawing.Color.Transparent
        Me.ActiviserPasswordLabel.Name = "ActiviserPasswordLabel"
        Me.ActiviserPasswordLabel.TabStop = True
        '
        'activiserUserNameTextBox
        '
        resources.ApplyResources(Me.activiserUserNameTextBox, "activiserUserNameTextBox")
        Me.activiserUserNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("ReadOnly", Global.activiser.OutlookAddIn.MySettings.Default, "AutoDomainLogon", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.activiserUserNameTextBox.Name = "activiserUserNameTextBox"
        Me.activiserUserNameTextBox.ReadOnly = Global.activiser.OutlookAddIn.MySettings.Default.AutoDomainLogon
        '
        'ActiviserUsernameLabel
        '
        resources.ApplyResources(Me.ActiviserUsernameLabel, "ActiviserUsernameLabel")
        Me.ActiviserUsernameLabel.BackColor = System.Drawing.Color.Transparent
        Me.ActiviserUsernameLabel.Name = "ActiviserUsernameLabel"
        '
        'ServerGroup
        '
        Me.ServerGroup.Controls.Add(Me.ServerUrlLabel)
        Me.ServerGroup.Controls.Add(Me.ServerUrlTextBox)
        Me.ServerGroup.Controls.Add(Me.IgnoreCertificateErrorsCheckBox)
        Me.ServerGroup.Controls.Add(Me.ServerUrlTestButton)
        resources.ApplyResources(Me.ServerGroup, "ServerGroup")
        Me.ServerGroup.Name = "ServerGroup"
        Me.ServerGroup.TabStop = False
        '
        'ServerUrlLabel
        '
        resources.ApplyResources(Me.ServerUrlLabel, "ServerUrlLabel")
        Me.ServerUrlLabel.BackColor = System.Drawing.Color.Transparent
        Me.ServerUrlLabel.Name = "ServerUrlLabel"
        '
        'ServerUrlTextBox
        '
        Me.ServerUrlTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.activiser.OutlookAddIn.MySettings.Default, "activiserServerUrl", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.ServerUrlTextBox, "ServerUrlTextBox")
        Me.ServerUrlTextBox.Name = "ServerUrlTextBox"
        Me.ServerUrlTextBox.Text = Global.activiser.OutlookAddIn.MySettings.Default.activiserServerUrl
        '
        'IgnoreCertificateErrorsCheckBox
        '
        Me.IgnoreCertificateErrorsCheckBox.Checked = Global.activiser.OutlookAddIn.MySettings.Default.IgnoreServerCertificateErrors
        Me.IgnoreCertificateErrorsCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.activiser.OutlookAddIn.MySettings.Default, "IgnoreServerCertificateErrors", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.IgnoreCertificateErrorsCheckBox, "IgnoreCertificateErrorsCheckBox")
        Me.IgnoreCertificateErrorsCheckBox.Name = "IgnoreCertificateErrorsCheckBox"
        Me.IgnoreCertificateErrorsCheckBox.UseVisualStyleBackColor = True
        '
        'ServerUrlTestButton
        '
        resources.ApplyResources(Me.ServerUrlTestButton, "ServerUrlTestButton")
        Me.ServerUrlTestButton.BackColor = System.Drawing.Color.GhostWhite
        Me.ServerUrlTestButton.Image = Global.activiser.OutlookAddIn.My.Resources.Resources.TaskHS
        Me.ServerUrlTestButton.Name = "ServerUrlTestButton"
        Me.ServerUrlTestButton.UseVisualStyleBackColor = False
        '
        'GeneralGroup
        '
        Me.GeneralGroup.Controls.Add(Me.AutoLogonCheckBox)
        resources.ApplyResources(Me.GeneralGroup, "GeneralGroup")
        Me.GeneralGroup.Name = "GeneralGroup"
        Me.GeneralGroup.TabStop = False
        '
        'AutoLogonCheckBox
        '
        resources.ApplyResources(Me.AutoLogonCheckBox, "AutoLogonCheckBox")
        Me.AutoLogonCheckBox.Checked = Global.activiser.OutlookAddIn.MySettings.Default.LogonAtStartup
        Me.AutoLogonCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AutoLogonCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.activiser.OutlookAddIn.MySettings.Default, "LogonAtStartup", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.AutoLogonCheckBox.Name = "AutoLogonCheckBox"
        Me.AutoLogonCheckBox.UseVisualStyleBackColor = True
        '
        'CalendarTabPage
        '
        Me.CalendarTabPage.Controls.Add(Me.ClientAddressGroup)
        Me.CalendarTabPage.Controls.Add(Me.ShowTimeAsLabel)
        resources.ApplyResources(Me.CalendarTabPage, "CalendarTabPage")
        Me.CalendarTabPage.Name = "CalendarTabPage"
        Me.CalendarTabPage.UseVisualStyleBackColor = True
        '
        'ClientAddressGroup
        '
        Me.ClientAddressGroup.Controls.Add(Me.PrependAddressCheckBox)
        Me.ClientAddressGroup.Controls.Add(Me.AppendAddressCheckBox)
        resources.ApplyResources(Me.ClientAddressGroup, "ClientAddressGroup")
        Me.ClientAddressGroup.Name = "ClientAddressGroup"
        Me.ClientAddressGroup.TabStop = False
        '
        'PrependAddressCheckBox
        '
        resources.ApplyResources(Me.PrependAddressCheckBox, "PrependAddressCheckBox")
        Me.PrependAddressCheckBox.Checked = Global.activiser.OutlookAddIn.MySettings.Default.PrependAddressToLongDescription
        Me.PrependAddressCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.PrependAddressCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.activiser.OutlookAddIn.MySettings.Default, "PrependAddressToLongDescription", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.PrependAddressCheckBox.Name = "PrependAddressCheckBox"
        Me.PrependAddressCheckBox.UseVisualStyleBackColor = True
        '
        'AppendAddressCheckBox
        '
        resources.ApplyResources(Me.AppendAddressCheckBox, "AppendAddressCheckBox")
        Me.AppendAddressCheckBox.Checked = Global.activiser.OutlookAddIn.MySettings.Default.AppendAddressToLocation
        Me.AppendAddressCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.activiser.OutlookAddIn.MySettings.Default, "AppendAddressToLocation", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.AppendAddressCheckBox.Name = "AppendAddressCheckBox"
        Me.AppendAddressCheckBox.UseVisualStyleBackColor = True
        '
        'ShowTimeAsLabel
        '
        Me.ShowTimeAsLabel.Controls.Add(Me.RadioOutOfOffice)
        Me.ShowTimeAsLabel.Controls.Add(Me.RadioBusy)
        Me.ShowTimeAsLabel.Controls.Add(Me.RadioTentative)
        Me.ShowTimeAsLabel.Controls.Add(Me.RadioFree)
        resources.ApplyResources(Me.ShowTimeAsLabel, "ShowTimeAsLabel")
        Me.ShowTimeAsLabel.Name = "ShowTimeAsLabel"
        Me.ShowTimeAsLabel.TabStop = False
        '
        'RadioOutOfOffice
        '
        resources.ApplyResources(Me.RadioOutOfOffice, "RadioOutOfOffice")
        Me.RadioOutOfOffice.Name = "RadioOutOfOffice"
        Me.RadioOutOfOffice.Tag = "3"
        Me.RadioOutOfOffice.UseVisualStyleBackColor = True
        '
        'RadioBusy
        '
        resources.ApplyResources(Me.RadioBusy, "RadioBusy")
        Me.RadioBusy.Checked = True
        Me.RadioBusy.Name = "RadioBusy"
        Me.RadioBusy.TabStop = True
        Me.RadioBusy.Tag = "2"
        Me.RadioBusy.UseVisualStyleBackColor = True
        '
        'RadioTentative
        '
        resources.ApplyResources(Me.RadioTentative, "RadioTentative")
        Me.RadioTentative.Name = "RadioTentative"
        Me.RadioTentative.Tag = "1"
        Me.RadioTentative.UseVisualStyleBackColor = True
        '
        'RadioFree
        '
        resources.ApplyResources(Me.RadioFree, "RadioFree")
        Me.RadioFree.Name = "RadioFree"
        Me.RadioFree.Tag = "0"
        Me.RadioFree.UseVisualStyleBackColor = True
        '
        'RequestTab
        '
        Me.RequestTab.Controls.Add(Me.NadRulesGroup)
        resources.ApplyResources(Me.RequestTab, "RequestTab")
        Me.RequestTab.Name = "RequestTab"
        Me.RequestTab.UseVisualStyleBackColor = True
        '
        'NextActionDateEditGroup
        '
        Me.NextActionDateEditGroup.Controls.Add(Me.RadioNadFinishTime)
        Me.NextActionDateEditGroup.Controls.Add(Me.RadioNadStartTime)
        Me.NextActionDateEditGroup.Controls.Add(Me.RadioNadFirstBusDayAfterFinish)
        Me.NextActionDateEditGroup.Controls.Add(Me.RadioNadNextBusDay)
        Me.NextActionDateEditGroup.Controls.Add(Me.RadioNadToday)
        Me.NextActionDateEditGroup.Controls.Add(Me.RadioNadFinishDate)
        Me.NextActionDateEditGroup.Controls.Add(Me.RadioNadStartDate)
        resources.ApplyResources(Me.NextActionDateEditGroup, "NextActionDateEditGroup")
        Me.NextActionDateEditGroup.Name = "NextActionDateEditGroup"
        Me.NextActionDateEditGroup.TabStop = False
        '
        'RadioNadFinishTime
        '
        resources.ApplyResources(Me.RadioNadFinishTime, "RadioNadFinishTime")
        Me.RadioNadFinishTime.Name = "RadioNadFinishTime"
        Me.RadioNadFinishTime.UseVisualStyleBackColor = True
        '
        'RadioNadStartTime
        '
        resources.ApplyResources(Me.RadioNadStartTime, "RadioNadStartTime")
        Me.RadioNadStartTime.Name = "RadioNadStartTime"
        Me.RadioNadStartTime.UseVisualStyleBackColor = True
        '
        'RadioNadFirstBusDayAfterFinish
        '
        resources.ApplyResources(Me.RadioNadFirstBusDayAfterFinish, "RadioNadFirstBusDayAfterFinish")
        Me.RadioNadFirstBusDayAfterFinish.Name = "RadioNadFirstBusDayAfterFinish"
        Me.RadioNadFirstBusDayAfterFinish.UseVisualStyleBackColor = True
        '
        'RadioNadNextBusDay
        '
        resources.ApplyResources(Me.RadioNadNextBusDay, "RadioNadNextBusDay")
        Me.RadioNadNextBusDay.Name = "RadioNadNextBusDay"
        Me.RadioNadNextBusDay.UseVisualStyleBackColor = True
        '
        'RadioNadToday
        '
        resources.ApplyResources(Me.RadioNadToday, "RadioNadToday")
        Me.RadioNadToday.Name = "RadioNadToday"
        Me.RadioNadToday.UseVisualStyleBackColor = True
        '
        'RadioNadFinishDate
        '
        resources.ApplyResources(Me.RadioNadFinishDate, "RadioNadFinishDate")
        Me.RadioNadFinishDate.Name = "RadioNadFinishDate"
        Me.RadioNadFinishDate.UseVisualStyleBackColor = True
        '
        'RadioNadStartDate
        '
        resources.ApplyResources(Me.RadioNadStartDate, "RadioNadStartDate")
        Me.RadioNadStartDate.Checked = True
        Me.RadioNadStartDate.Name = "RadioNadStartDate"
        Me.RadioNadStartDate.TabStop = True
        Me.RadioNadStartDate.UseVisualStyleBackColor = True
        '
        'NextActionDateCutPasteGroup
        '
        Me.NextActionDateCutPasteGroup.Controls.Add(Me.RadioMoveNadDoNothing)
        Me.NextActionDateCutPasteGroup.Controls.Add(Me.RadioMoveNadFinishTime)
        Me.NextActionDateCutPasteGroup.Controls.Add(Me.RadioMoveNadStartTime)
        Me.NextActionDateCutPasteGroup.Controls.Add(Me.RadioMoveNadBusDayAfterFinish)
        Me.NextActionDateCutPasteGroup.Controls.Add(Me.RadioMoveNadBusDayAfterToday)
        Me.NextActionDateCutPasteGroup.Controls.Add(Me.RadioMoveNadToday)
        Me.NextActionDateCutPasteGroup.Controls.Add(Me.RadioMoveNadFinishDay)
        Me.NextActionDateCutPasteGroup.Controls.Add(Me.RadioMoveNadStartDate)
        resources.ApplyResources(Me.NextActionDateCutPasteGroup, "NextActionDateCutPasteGroup")
        Me.NextActionDateCutPasteGroup.Name = "NextActionDateCutPasteGroup"
        Me.NextActionDateCutPasteGroup.TabStop = False
        '
        'RadioMoveNadFinishTime
        '
        resources.ApplyResources(Me.RadioMoveNadFinishTime, "RadioMoveNadFinishTime")
        Me.RadioMoveNadFinishTime.Name = "RadioMoveNadFinishTime"
        Me.RadioMoveNadFinishTime.UseVisualStyleBackColor = True
        '
        'RadioMoveNadStartTime
        '
        resources.ApplyResources(Me.RadioMoveNadStartTime, "RadioMoveNadStartTime")
        Me.RadioMoveNadStartTime.Name = "RadioMoveNadStartTime"
        Me.RadioMoveNadStartTime.UseVisualStyleBackColor = True
        '
        'RadioMoveNadBusDayAfterFinish
        '
        resources.ApplyResources(Me.RadioMoveNadBusDayAfterFinish, "RadioMoveNadBusDayAfterFinish")
        Me.RadioMoveNadBusDayAfterFinish.Name = "RadioMoveNadBusDayAfterFinish"
        Me.RadioMoveNadBusDayAfterFinish.UseVisualStyleBackColor = True
        '
        'RadioMoveNadBusDayAfterToday
        '
        resources.ApplyResources(Me.RadioMoveNadBusDayAfterToday, "RadioMoveNadBusDayAfterToday")
        Me.RadioMoveNadBusDayAfterToday.Name = "RadioMoveNadBusDayAfterToday"
        Me.RadioMoveNadBusDayAfterToday.UseVisualStyleBackColor = True
        '
        'RadioMoveNadToday
        '
        resources.ApplyResources(Me.RadioMoveNadToday, "RadioMoveNadToday")
        Me.RadioMoveNadToday.Name = "RadioMoveNadToday"
        Me.RadioMoveNadToday.UseVisualStyleBackColor = True
        '
        'RadioMoveNadFinishDay
        '
        resources.ApplyResources(Me.RadioMoveNadFinishDay, "RadioMoveNadFinishDay")
        Me.RadioMoveNadFinishDay.Name = "RadioMoveNadFinishDay"
        Me.RadioMoveNadFinishDay.UseVisualStyleBackColor = True
        '
        'RadioMoveNadStartDate
        '
        resources.ApplyResources(Me.RadioMoveNadStartDate, "RadioMoveNadStartDate")
        Me.RadioMoveNadStartDate.Name = "RadioMoveNadStartDate"
        Me.RadioMoveNadStartDate.UseVisualStyleBackColor = True
        '
        'NadRulesGroup
        '
        Me.NadRulesGroup.Controls.Add(Me.NextActionDateEditGroup)
        Me.NadRulesGroup.Controls.Add(Me.NextActionDateCutPasteGroup)
        resources.ApplyResources(Me.NadRulesGroup, "NadRulesGroup")
        Me.NadRulesGroup.Name = "NadRulesGroup"
        Me.NadRulesGroup.TabStop = False
        '
        'RadioMoveNadDoNothing
        '
        resources.ApplyResources(Me.RadioMoveNadDoNothing, "RadioMoveNadDoNothing")
        Me.RadioMoveNadDoNothing.Checked = True
        Me.RadioMoveNadDoNothing.Name = "RadioMoveNadDoNothing"
        Me.RadioMoveNadDoNothing.TabStop = True
        Me.RadioMoveNadDoNothing.UseVisualStyleBackColor = True
        '
        'OptionsForm
        '
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OptionsForm"
        Me.ShowInTaskbar = False
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.ActiviserTabPage.ResumeLayout(False)
        Me.RequestDefaultsGroup.ResumeLayout(False)
        Me.RequestDefaultsGroup.PerformLayout()
        Me.UserGroup.ResumeLayout(False)
        Me.UserGroup.PerformLayout()
        Me.ServerGroup.ResumeLayout(False)
        Me.ServerGroup.PerformLayout()
        Me.GeneralGroup.ResumeLayout(False)
        Me.GeneralGroup.PerformLayout()
        Me.CalendarTabPage.ResumeLayout(False)
        Me.ClientAddressGroup.ResumeLayout(False)
        Me.ClientAddressGroup.PerformLayout()
        Me.ShowTimeAsLabel.ResumeLayout(False)
        Me.ShowTimeAsLabel.PerformLayout()
        Me.RequestTab.ResumeLayout(False)
        Me.NextActionDateEditGroup.ResumeLayout(False)
        Me.NextActionDateEditGroup.PerformLayout()
        Me.NextActionDateCutPasteGroup.ResumeLayout(False)
        Me.NextActionDateCutPasteGroup.PerformLayout()
        Me.NadRulesGroup.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents CalendarTabPage As System.Windows.Forms.TabPage
    Friend WithEvents ActiviserTabPage As System.Windows.Forms.TabPage
    Friend WithEvents AutoLogonCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ShowTimeAsLabel As System.Windows.Forms.GroupBox
    Friend WithEvents RadioOutOfOffice As System.Windows.Forms.RadioButton
    Friend WithEvents RadioBusy As System.Windows.Forms.RadioButton
    Friend WithEvents RadioTentative As System.Windows.Forms.RadioButton
    Friend WithEvents RadioFree As System.Windows.Forms.RadioButton
    Friend WithEvents DefaultRequestStatusLabel As System.Windows.Forms.Label
    Friend WithEvents DefaultRequestStatusList As System.Windows.Forms.ComboBox
    Friend WithEvents ServerGroup As System.Windows.Forms.GroupBox
    Friend WithEvents ServerUrlTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IgnoreCertificateErrorsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ServerUrlTestButton As System.Windows.Forms.Button
    Friend WithEvents RequestDefaultsGroup As System.Windows.Forms.GroupBox
    Friend WithEvents ServerUrlLabel As System.Windows.Forms.Label
    Friend WithEvents IntegratedAuthenticationCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents activiserPasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents activiserUserNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ActiviserUsernameLabel As System.Windows.Forms.Label
    Friend WithEvents ActiviserPasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UserGroup As System.Windows.Forms.GroupBox
    Friend WithEvents LoginButton As System.Windows.Forms.Button
    Friend WithEvents GeneralGroup As System.Windows.Forms.GroupBox
    Friend WithEvents LinkUserCheckbox As System.Windows.Forms.CheckBox
    Friend WithEvents ClientAddressGroup As System.Windows.Forms.GroupBox
    Friend WithEvents PrependAddressCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents AppendAddressCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents RequestTab As System.Windows.Forms.TabPage
    Friend WithEvents NadRulesGroup As System.Windows.Forms.GroupBox
    Friend WithEvents NextActionDateEditGroup As System.Windows.Forms.GroupBox
    Friend WithEvents RadioNadFinishTime As System.Windows.Forms.RadioButton
    Friend WithEvents RadioNadStartTime As System.Windows.Forms.RadioButton
    Friend WithEvents RadioNadFirstBusDayAfterFinish As System.Windows.Forms.RadioButton
    Friend WithEvents RadioNadNextBusDay As System.Windows.Forms.RadioButton
    Friend WithEvents RadioNadToday As System.Windows.Forms.RadioButton
    Friend WithEvents RadioNadFinishDate As System.Windows.Forms.RadioButton
    Friend WithEvents RadioNadStartDate As System.Windows.Forms.RadioButton
    Friend WithEvents NextActionDateCutPasteGroup As System.Windows.Forms.GroupBox
    Friend WithEvents RadioMoveNadFinishTime As System.Windows.Forms.RadioButton
    Friend WithEvents RadioMoveNadStartTime As System.Windows.Forms.RadioButton
    Friend WithEvents RadioMoveNadBusDayAfterFinish As System.Windows.Forms.RadioButton
    Friend WithEvents RadioMoveNadBusDayAfterToday As System.Windows.Forms.RadioButton
    Friend WithEvents RadioMoveNadToday As System.Windows.Forms.RadioButton
    Friend WithEvents RadioMoveNadFinishDay As System.Windows.Forms.RadioButton
    Friend WithEvents RadioMoveNadStartDate As System.Windows.Forms.RadioButton
    Friend WithEvents RadioMoveNadDoNothing As System.Windows.Forms.RadioButton

End Class
