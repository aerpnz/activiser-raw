<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConsultantSubForm
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim ConsultantNumberLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConsultantSubForm))
        Dim LastSyncTimeLabel As System.Windows.Forms.Label
        Dim DomainUserLabel As System.Windows.Forms.Label
        Dim MinSyncTimeLabel As System.Windows.Forms.Label
        Dim AdministrationLabel As System.Windows.Forms.Label
        Dim ManagementLabel As System.Windows.Forms.Label
        Dim IsActiviserUserLabel As System.Windows.Forms.Label
        Dim MobileAlertLabel As System.Windows.Forms.Label
        Dim MobilePhoneLabel As System.Windows.Forms.Label
        Dim EmailAddressLabel As System.Windows.Forms.Label
        Dim NameLabel As System.Windows.Forms.Label
        Me.ConsultantInfoPanel = New System.Windows.Forms.TableLayoutPanel
        Me.UsernameTextBox = New System.Windows.Forms.TextBox
        Me.ConsultantBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CoreDataSet = New activiser.Library.activiserWebService.activiserDataSet
        Me.SyncInterval1 = New activiser.Console.DurationPicker
        Me.DomainLogonTextBox = New System.Windows.Forms.TextBox
        Me.AdministrationCheckBox = New System.Windows.Forms.CheckBox
        Me.ManagementCheckBox = New System.Windows.Forms.CheckBox
        Me.IsActiviserUserCheckBox = New System.Windows.Forms.CheckBox
        Me.MobileAlertCheckBox = New System.Windows.Forms.CheckBox
        Me.MobilePhoneTextBox = New System.Windows.Forms.TextBox
        Me.EmailAddressTextBox = New System.Windows.Forms.TextBox
        Me.NameTextBox = New System.Windows.Forms.TextBox
        Me.ConsultantNumberTextBox = New System.Windows.Forms.TextBox
        Me.LastSyncDateTimePicker = New activiser.Library.DateTimePicker
        Me.UserNameLabel = New System.Windows.Forms.Label
        Me.ColorDialog = New System.Windows.Forms.ColorDialog
        Me.ToolTipProvider = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.GeneralTab = New System.Windows.Forms.TabPage
        Me.ChangePasswordButton = New System.Windows.Forms.Button
        Me.UndoButton = New System.Windows.Forms.Button
        Me.SaveButton = New System.Windows.Forms.Button
        Me.TrackingTab = New System.Windows.Forms.TabPage
        Me.GpsBrowser = New System.Windows.Forms.WebBrowser
        ConsultantNumberLabel = New System.Windows.Forms.Label
        LastSyncTimeLabel = New System.Windows.Forms.Label
        DomainUserLabel = New System.Windows.Forms.Label
        MinSyncTimeLabel = New System.Windows.Forms.Label
        AdministrationLabel = New System.Windows.Forms.Label
        ManagementLabel = New System.Windows.Forms.Label
        IsActiviserUserLabel = New System.Windows.Forms.Label
        MobileAlertLabel = New System.Windows.Forms.Label
        MobilePhoneLabel = New System.Windows.Forms.Label
        EmailAddressLabel = New System.Windows.Forms.Label
        NameLabel = New System.Windows.Forms.Label
        Me.ConsultantInfoPanel.SuspendLayout()
        CType(Me.ConsultantBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CoreDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.GeneralTab.SuspendLayout()
        Me.TrackingTab.SuspendLayout()
        Me.SuspendLayout()
        '
        'ConsultantNumberLabel
        '
        resources.ApplyResources(ConsultantNumberLabel, "ConsultantNumberLabel")
        ConsultantNumberLabel.Name = "ConsultantNumberLabel"
        '
        'LastSyncTimeLabel
        '
        resources.ApplyResources(LastSyncTimeLabel, "LastSyncTimeLabel")
        LastSyncTimeLabel.Name = "LastSyncTimeLabel"
        '
        'DomainUserLabel
        '
        resources.ApplyResources(DomainUserLabel, "DomainUserLabel")
        DomainUserLabel.Name = "DomainUserLabel"
        '
        'MinSyncTimeLabel
        '
        resources.ApplyResources(MinSyncTimeLabel, "MinSyncTimeLabel")
        MinSyncTimeLabel.Name = "MinSyncTimeLabel"
        '
        'AdministrationLabel
        '
        resources.ApplyResources(AdministrationLabel, "AdministrationLabel")
        AdministrationLabel.Name = "AdministrationLabel"
        '
        'ManagementLabel
        '
        resources.ApplyResources(ManagementLabel, "ManagementLabel")
        ManagementLabel.Name = "ManagementLabel"
        '
        'IsActiviserUserLabel
        '
        resources.ApplyResources(IsActiviserUserLabel, "IsActiviserUserLabel")
        IsActiviserUserLabel.Name = "IsActiviserUserLabel"
        '
        'MobileAlertLabel
        '
        resources.ApplyResources(MobileAlertLabel, "MobileAlertLabel")
        MobileAlertLabel.Name = "MobileAlertLabel"
        '
        'MobilePhoneLabel
        '
        resources.ApplyResources(MobilePhoneLabel, "MobilePhoneLabel")
        MobilePhoneLabel.Name = "MobilePhoneLabel"
        '
        'EmailAddressLabel
        '
        resources.ApplyResources(EmailAddressLabel, "EmailAddressLabel")
        EmailAddressLabel.Name = "EmailAddressLabel"
        '
        'NameLabel
        '
        resources.ApplyResources(NameLabel, "NameLabel")
        NameLabel.Name = "NameLabel"
        '
        'ConsultantInfoPanel
        '
        resources.ApplyResources(Me.ConsultantInfoPanel, "ConsultantInfoPanel")
        Me.ConsultantInfoPanel.Controls.Add(Me.UsernameTextBox, 1, 1)
        Me.ConsultantInfoPanel.Controls.Add(LastSyncTimeLabel, 0, 4)
        Me.ConsultantInfoPanel.Controls.Add(DomainUserLabel, 0, 11)
        Me.ConsultantInfoPanel.Controls.Add(Me.SyncInterval1, 1, 3)
        Me.ConsultantInfoPanel.Controls.Add(MinSyncTimeLabel, 0, 3)
        Me.ConsultantInfoPanel.Controls.Add(Me.DomainLogonTextBox, 1, 11)
        Me.ConsultantInfoPanel.Controls.Add(AdministrationLabel, 0, 10)
        Me.ConsultantInfoPanel.Controls.Add(Me.AdministrationCheckBox, 1, 10)
        Me.ConsultantInfoPanel.Controls.Add(ManagementLabel, 0, 9)
        Me.ConsultantInfoPanel.Controls.Add(Me.ManagementCheckBox, 1, 9)
        Me.ConsultantInfoPanel.Controls.Add(IsActiviserUserLabel, 0, 8)
        Me.ConsultantInfoPanel.Controls.Add(Me.IsActiviserUserCheckBox, 1, 8)
        Me.ConsultantInfoPanel.Controls.Add(MobileAlertLabel, 0, 7)
        Me.ConsultantInfoPanel.Controls.Add(Me.MobileAlertCheckBox, 1, 7)
        Me.ConsultantInfoPanel.Controls.Add(MobilePhoneLabel, 0, 6)
        Me.ConsultantInfoPanel.Controls.Add(Me.MobilePhoneTextBox, 1, 6)
        Me.ConsultantInfoPanel.Controls.Add(EmailAddressLabel, 0, 5)
        Me.ConsultantInfoPanel.Controls.Add(Me.EmailAddressTextBox, 1, 5)
        Me.ConsultantInfoPanel.Controls.Add(NameLabel, 0, 2)
        Me.ConsultantInfoPanel.Controls.Add(Me.NameTextBox, 1, 2)
        Me.ConsultantInfoPanel.Controls.Add(ConsultantNumberLabel, 0, 0)
        Me.ConsultantInfoPanel.Controls.Add(Me.ConsultantNumberTextBox, 1, 0)
        Me.ConsultantInfoPanel.Controls.Add(Me.LastSyncDateTimePicker, 1, 4)
        Me.ConsultantInfoPanel.Controls.Add(Me.UserNameLabel, 0, 1)
        Me.ConsultantInfoPanel.Name = "ConsultantInfoPanel"
        '
        'UsernameTextBox
        '
        resources.ApplyResources(Me.UsernameTextBox, "UsernameTextBox")
        Me.UsernameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ConsultantBindingSource, "Username", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.UsernameTextBox.Name = "UsernameTextBox"
        '
        'ConsultantBindingSource
        '
        Me.ConsultantBindingSource.AllowNew = False
        Me.ConsultantBindingSource.DataMember = "Consultant"
        Me.ConsultantBindingSource.DataSource = Me.CoreDataSet
        '
        'CoreDataSet
        '
        Me.CoreDataSet.DataSetName = "activiserCoreDataSet"
        Me.CoreDataSet.Locale = New System.Globalization.CultureInfo(Global.activiser.Console.My.Resources.Resources.Credential2KHelpInfo)
        Me.CoreDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SyncInterval1
        '
        resources.ApplyResources(Me.SyncInterval1, "SyncInterval1")
        Me.SyncInterval1.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.ConsultantBindingSource, "MinSyncTime", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.SyncInterval1.MinimumSize = New System.Drawing.Size(140, 22)
        Me.SyncInterval1.Name = "SyncInterval1"
        Me.SyncInterval1.Value = 0
        '
        'DomainLogonTextBox
        '
        resources.ApplyResources(Me.DomainLogonTextBox, "DomainLogonTextBox")
        Me.DomainLogonTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ConsultantBindingSource, "DomainLogon", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.DomainLogonTextBox.Name = "DomainLogonTextBox"
        '
        'AdministrationCheckBox
        '
        Me.AdministrationCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.ConsultantBindingSource, "Administration", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.AdministrationCheckBox, "AdministrationCheckBox")
        Me.AdministrationCheckBox.Name = "AdministrationCheckBox"
        '
        'ManagementCheckBox
        '
        Me.ManagementCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.ConsultantBindingSource, "Management", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.ManagementCheckBox, "ManagementCheckBox")
        Me.ManagementCheckBox.Name = "ManagementCheckBox"
        '
        'IsActiviserUserCheckBox
        '
        Me.IsActiviserUserCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.ConsultantBindingSource, "IsActiviserUser", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.IsActiviserUserCheckBox, "IsActiviserUserCheckBox")
        Me.IsActiviserUserCheckBox.Name = "IsActiviserUserCheckBox"
        '
        'MobileAlertCheckBox
        '
        Me.MobileAlertCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.ConsultantBindingSource, "MobileAlert", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.MobileAlertCheckBox, "MobileAlertCheckBox")
        Me.MobileAlertCheckBox.Name = "MobileAlertCheckBox"
        '
        'MobilePhoneTextBox
        '
        resources.ApplyResources(Me.MobilePhoneTextBox, "MobilePhoneTextBox")
        Me.MobilePhoneTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ConsultantBindingSource, "MobilePhone", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.MobilePhoneTextBox.Name = "MobilePhoneTextBox"
        '
        'EmailAddressTextBox
        '
        resources.ApplyResources(Me.EmailAddressTextBox, "EmailAddressTextBox")
        Me.EmailAddressTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ConsultantBindingSource, "EmailAddress", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.EmailAddressTextBox.Name = "EmailAddressTextBox"
        '
        'NameTextBox
        '
        resources.ApplyResources(Me.NameTextBox, "NameTextBox")
        Me.NameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ConsultantBindingSource, "Name", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.NameTextBox.Name = "NameTextBox"
        '
        'ConsultantNumberTextBox
        '
        Me.ConsultantNumberTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ConsultantBindingSource, "ConsultantNumber", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.ConsultantNumberTextBox, "ConsultantNumberTextBox")
        Me.ConsultantNumberTextBox.Name = "ConsultantNumberTextBox"
        '
        'LastSyncDateTimePicker
        '
        resources.ApplyResources(Me.LastSyncDateTimePicker, "LastSyncDateTimePicker")
        Me.LastSyncDateTimePicker.DataBindings.Add(New System.Windows.Forms.Binding("DBValue", Me.ConsultantBindingSource, "LastSyncTime", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.LastSyncDateTimePicker.Name = "LastSyncDateTimePicker"
        Me.LastSyncDateTimePicker.ShowTime = False
        '
        'UserNameLabel
        '
        resources.ApplyResources(Me.UserNameLabel, "UserNameLabel")
        Me.UserNameLabel.Name = "UserNameLabel"
        '
        'ColorDialog
        '
        Me.ColorDialog.AllowFullOpen = False
        Me.ColorDialog.SolidColorOnly = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.GeneralTab)
        Me.TabControl1.Controls.Add(Me.TrackingTab)
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        '
        'GeneralTab
        '
        Me.GeneralTab.Controls.Add(Me.ChangePasswordButton)
        Me.GeneralTab.Controls.Add(Me.UndoButton)
        Me.GeneralTab.Controls.Add(Me.SaveButton)
        Me.GeneralTab.Controls.Add(Me.ConsultantInfoPanel)
        resources.ApplyResources(Me.GeneralTab, "GeneralTab")
        Me.GeneralTab.Name = "GeneralTab"
        Me.GeneralTab.UseVisualStyleBackColor = True
        '
        'ChangePasswordButton
        '
        resources.ApplyResources(Me.ChangePasswordButton, "ChangePasswordButton")
        Me.ChangePasswordButton.Name = "ChangePasswordButton"
        Me.ChangePasswordButton.UseVisualStyleBackColor = True
        '
        'UndoButton
        '
        Me.UndoButton.Image = Global.activiser.Console.My.Resources.Resources.Edit_UndoHS
        resources.ApplyResources(Me.UndoButton, "UndoButton")
        Me.UndoButton.Name = "UndoButton"
        Me.UndoButton.UseVisualStyleBackColor = True
        '
        'SaveButton
        '
        Me.SaveButton.Image = Global.activiser.Console.My.Resources.Resources.saveHS
        resources.ApplyResources(Me.SaveButton, "SaveButton")
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'TrackingTab
        '
        Me.TrackingTab.Controls.Add(Me.GpsBrowser)
        resources.ApplyResources(Me.TrackingTab, "TrackingTab")
        Me.TrackingTab.Name = "TrackingTab"
        Me.TrackingTab.UseVisualStyleBackColor = True
        '
        'GpsBrowser
        '
        Me.GpsBrowser.AllowNavigation = False
        Me.GpsBrowser.AllowWebBrowserDrop = False
        resources.ApplyResources(Me.GpsBrowser, "GpsBrowser")
        Me.GpsBrowser.IsWebBrowserContextMenuEnabled = False
        Me.GpsBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.GpsBrowser.Name = "GpsBrowser"
        Me.GpsBrowser.WebBrowserShortcutsEnabled = False
        '
        'ConsultantSubForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl1)
        Me.MinimumSize = New System.Drawing.Size(640, 480)
        Me.Name = "ConsultantSubForm"
        Me.ConsultantInfoPanel.ResumeLayout(False)
        Me.ConsultantInfoPanel.PerformLayout()
        CType(Me.ConsultantBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CoreDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.GeneralTab.ResumeLayout(False)
        Me.TrackingTab.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CoreDataSet As Library.activiserWebService.activiserDataSet
    Friend WithEvents ConsultantBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ConsultantInfoPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DomainLogonTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AdministrationCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ManagementCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents IsActiviserUserCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents MobileAlertCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents MobilePhoneTextBox As System.Windows.Forms.TextBox
    Friend WithEvents EmailAddressTextBox As System.Windows.Forms.TextBox
    Friend WithEvents NameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ConsultantNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SyncInterval1 As activiser.Console.DurationPicker
    Friend WithEvents ColorDialog As System.Windows.Forms.ColorDialog
    Friend WithEvents ToolTipProvider As System.Windows.Forms.ToolTip
    Friend WithEvents LastSyncDateTimePicker As activiser.Library.DateTimePicker
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents GeneralTab As System.Windows.Forms.TabPage
    Friend WithEvents TrackingTab As System.Windows.Forms.TabPage
    Friend WithEvents UserNameLabel As System.Windows.Forms.Label
    Friend WithEvents UsernameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents GpsBrowser As System.Windows.Forms.WebBrowser
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents UndoButton As System.Windows.Forms.Button
    Friend WithEvents ChangePasswordButton As System.Windows.Forms.Button

End Class
