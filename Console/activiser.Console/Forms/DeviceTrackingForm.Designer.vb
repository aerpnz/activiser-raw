<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeviceTrackingForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DeviceTrackingForm))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.CurrentTimeZoneLabel = New System.Windows.Forms.Label
        Me.TimeZoneReminderNote = New System.Windows.Forms.Label
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.UntilPicker = New activiser.Library.DateTimePicker
        Me.FilterUntil = New System.Windows.Forms.CheckBox
        Me.FilterUser = New System.Windows.Forms.CheckBox
        Me.FilterSince = New System.Windows.Forms.CheckBox
        Me.FilterAsAt = New System.Windows.Forms.CheckBox
        Me.ConsultantUIDComboBox = New System.Windows.Forms.ComboBox
        Me.FilterDevice = New System.Windows.Forms.CheckBox
        Me.SincePicker = New activiser.Library.DateTimePicker
        Me.AsAtPicker = New activiser.Library.DateTimePicker
        Me.DeviceIdTextBox = New System.Windows.Forms.TextBox
        Me.QueryButton = New System.Windows.Forms.Button
        Me.GpsBrowser = New System.Windows.Forms.WebBrowser
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.CurrentTimeZoneLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TimeZoneReminderNote)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.QueryButton)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GpsBrowser)
        '
        'CurrentTimeZoneLabel
        '
        resources.ApplyResources(Me.CurrentTimeZoneLabel, "CurrentTimeZoneLabel")
        Me.CurrentTimeZoneLabel.Name = "CurrentTimeZoneLabel"
        '
        'TimeZoneReminderNote
        '
        resources.ApplyResources(Me.TimeZoneReminderNote, "TimeZoneReminderNote")
        Me.TimeZoneReminderNote.Name = "TimeZoneReminderNote"
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.UntilPicker, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.FilterUntil, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.FilterUser, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.FilterSince, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.FilterAsAt, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.ConsultantUIDComboBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.FilterDevice, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.SincePicker, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.AsAtPicker, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.DeviceIdTextBox, 1, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'UntilPicker
        '
        resources.ApplyResources(Me.UntilPicker, "UntilPicker")
        Me.UntilPicker.Name = "UntilPicker"
        Me.UntilPicker.ShowCheckBox = False
        '
        'FilterUntil
        '
        resources.ApplyResources(Me.FilterUntil, "FilterUntil")
        Me.FilterUntil.Name = "FilterUntil"
        Me.FilterUntil.UseVisualStyleBackColor = True
        '
        'FilterUser
        '
        resources.ApplyResources(Me.FilterUser, "FilterUser")
        Me.FilterUser.Name = "FilterUser"
        Me.FilterUser.UseVisualStyleBackColor = True
        '
        'FilterSince
        '
        resources.ApplyResources(Me.FilterSince, "FilterSince")
        Me.FilterSince.Name = "FilterSince"
        Me.FilterSince.UseVisualStyleBackColor = True
        '
        'FilterAsAt
        '
        resources.ApplyResources(Me.FilterAsAt, "FilterAsAt")
        Me.FilterAsAt.Name = "FilterAsAt"
        Me.FilterAsAt.UseVisualStyleBackColor = True
        '
        'ConsultantUIDComboBox
        '
        resources.ApplyResources(Me.ConsultantUIDComboBox, "ConsultantUIDComboBox")
        Me.ConsultantUIDComboBox.FormattingEnabled = True
        Me.ConsultantUIDComboBox.Name = "ConsultantUIDComboBox"
        '
        'FilterDevice
        '
        resources.ApplyResources(Me.FilterDevice, "FilterDevice")
        Me.FilterDevice.Name = "FilterDevice"
        Me.FilterDevice.UseVisualStyleBackColor = True
        '
        'SincePicker
        '
        resources.ApplyResources(Me.SincePicker, "SincePicker")
        Me.SincePicker.Name = "SincePicker"
        Me.SincePicker.ShowCheckBox = False
        '
        'AsAtPicker
        '
        resources.ApplyResources(Me.AsAtPicker, "AsAtPicker")
        Me.AsAtPicker.Name = "AsAtPicker"
        Me.AsAtPicker.ShowCheckBox = False
        '
        'DeviceIdTextBox
        '
        resources.ApplyResources(Me.DeviceIdTextBox, "DeviceIdTextBox")
        Me.DeviceIdTextBox.Name = "DeviceIdTextBox"
        '
        'QueryButton
        '
        resources.ApplyResources(Me.QueryButton, "QueryButton")
        Me.QueryButton.Name = "QueryButton"
        Me.QueryButton.UseVisualStyleBackColor = True
        '
        'GpsBrowser
        '
        Me.GpsBrowser.AllowNavigation = False
        Me.GpsBrowser.AllowWebBrowserDrop = False
        resources.ApplyResources(Me.GpsBrowser, "GpsBrowser")
        Me.GpsBrowser.IsWebBrowserContextMenuEnabled = False
        Me.GpsBrowser.MinimumSize = New System.Drawing.Size(23, 22)
        Me.GpsBrowser.Name = "GpsBrowser"
        Me.GpsBrowser.ScriptErrorsSuppressed = True
        Me.GpsBrowser.WebBrowserShortcutsEnabled = False
        '
        'DeviceTrackingForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "DeviceTrackingForm"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DeviceIdTextBox As System.Windows.Forms.TextBox
    Friend WithEvents FilterDevice As System.Windows.Forms.CheckBox
    Friend WithEvents FilterUser As System.Windows.Forms.CheckBox
    Friend WithEvents SincePicker As activiser.Library.DateTimePicker
    Friend WithEvents AsAtPicker As activiser.Library.DateTimePicker
    Friend WithEvents GpsBrowser As System.Windows.Forms.WebBrowser
    Friend WithEvents QueryButton As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents UntilPicker As activiser.Library.DateTimePicker
    Friend WithEvents FilterUntil As System.Windows.Forms.CheckBox
    Friend WithEvents FilterSince As System.Windows.Forms.CheckBox
    Friend WithEvents FilterAsAt As System.Windows.Forms.CheckBox
    Friend WithEvents ConsultantUIDComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents TimeZoneReminderNote As System.Windows.Forms.Label
    Friend WithEvents CurrentTimeZoneLabel As System.Windows.Forms.Label
End Class
