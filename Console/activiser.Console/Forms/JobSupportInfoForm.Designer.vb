<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JobSupportInfoForm
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
        Me.components = New System.ComponentModel.Container
        Dim EmailStatusLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(JobSupportInfoForm))
        Dim FlagLabel As System.Windows.Forms.Label
        Dim JobStatusIDLabel As System.Windows.Forms.Label
        Dim IDLabel As System.Windows.Forms.Label
        Dim JobNumberLabel As System.Windows.Forms.Label
        Dim ModifiedTimeLabel As System.Windows.Forms.Label
        Dim CreatedTimeLabel As System.Windows.Forms.Label
        Dim ConsultantJobIDLabel As System.Windows.Forms.Label
        Dim UIDLabel As System.Windows.Forms.Label
        Dim ConsultantUidLabel As System.Windows.Forms.Label
        Dim RequestUidLabel As System.Windows.Forms.Label
        Dim ClientSiteUidLabel As System.Windows.Forms.Label
        Me.JobTrackingLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.EMailStatusPicker = New System.Windows.Forms.NumericUpDown
        Me.JobNumberTextBox = New System.Windows.Forms.TextBox
        Me.ConsultantUidTextBox = New System.Windows.Forms.TextBox
        Me.JobStatusPicker = New System.Windows.Forms.DomainUpDown
        Me.JobUIDTextBox = New System.Windows.Forms.TextBox
        Me.JobIDTextBox = New System.Windows.Forms.TextBox
        Me.ConsultantJobIDTextBox = New System.Windows.Forms.TextBox
        Me.FlagTextBox = New System.Windows.Forms.TextBox
        Me.ClientSiteUidTextBox = New System.Windows.Forms.TextBox
        Me.CreatedTimeTextBox = New System.Windows.Forms.TextBox
        Me.RequestUidTextBox = New System.Windows.Forms.TextBox
        Me.ModifiedTimeTextBox = New System.Windows.Forms.TextBox
        Me.TrackingInfoTextBox = New System.Windows.Forms.TextBox
        Me.TrackingInfoLabel = New System.Windows.Forms.Label
        Me.DoneButton = New System.Windows.Forms.Button
        Me.AbortButton = New System.Windows.Forms.Button
        Me.ToolTipProvider = New System.Windows.Forms.ToolTip(Me.components)
        EmailStatusLabel = New System.Windows.Forms.Label
        FlagLabel = New System.Windows.Forms.Label
        JobStatusIDLabel = New System.Windows.Forms.Label
        IDLabel = New System.Windows.Forms.Label
        JobNumberLabel = New System.Windows.Forms.Label
        ModifiedTimeLabel = New System.Windows.Forms.Label
        CreatedTimeLabel = New System.Windows.Forms.Label
        ConsultantJobIDLabel = New System.Windows.Forms.Label
        UIDLabel = New System.Windows.Forms.Label
        ConsultantUidLabel = New System.Windows.Forms.Label
        RequestUidLabel = New System.Windows.Forms.Label
        ClientSiteUidLabel = New System.Windows.Forms.Label
        Me.JobTrackingLayoutPanel.SuspendLayout()
        CType(Me.EMailStatusPicker, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'EmailStatusLabel
        '
        resources.ApplyResources(EmailStatusLabel, "EmailStatusLabel")
        EmailStatusLabel.Name = "EmailStatusLabel"
        '
        'FlagLabel
        '
        resources.ApplyResources(FlagLabel, "FlagLabel")
        FlagLabel.Name = "FlagLabel"
        '
        'JobStatusIDLabel
        '
        resources.ApplyResources(JobStatusIDLabel, "JobStatusIDLabel")
        JobStatusIDLabel.Name = "JobStatusIDLabel"
        '
        'IDLabel
        '
        resources.ApplyResources(IDLabel, "IDLabel")
        IDLabel.Name = "IDLabel"
        '
        'JobNumberLabel
        '
        resources.ApplyResources(JobNumberLabel, "JobNumberLabel")
        JobNumberLabel.Name = "JobNumberLabel"
        '
        'ModifiedTimeLabel
        '
        resources.ApplyResources(ModifiedTimeLabel, "ModifiedTimeLabel")
        ModifiedTimeLabel.Name = "ModifiedTimeLabel"
        '
        'CreatedTimeLabel
        '
        resources.ApplyResources(CreatedTimeLabel, "CreatedTimeLabel")
        CreatedTimeLabel.Name = "CreatedTimeLabel"
        '
        'ConsultantJobIDLabel
        '
        resources.ApplyResources(ConsultantJobIDLabel, "ConsultantJobIDLabel")
        ConsultantJobIDLabel.Name = "ConsultantJobIDLabel"
        '
        'UIDLabel
        '
        resources.ApplyResources(UIDLabel, "UIDLabel")
        UIDLabel.Name = "UIDLabel"
        '
        'ConsultantUidLabel
        '
        resources.ApplyResources(ConsultantUidLabel, "ConsultantUidLabel")
        ConsultantUidLabel.Name = "ConsultantUidLabel"
        '
        'RequestUidLabel
        '
        resources.ApplyResources(RequestUidLabel, "RequestUidLabel")
        RequestUidLabel.Name = "RequestUidLabel"
        '
        'ClientSiteUidLabel
        '
        resources.ApplyResources(ClientSiteUidLabel, "ClientSiteUidLabel")
        ClientSiteUidLabel.Name = "ClientSiteUidLabel"
        '
        'JobTrackingLayoutPanel
        '
        resources.ApplyResources(Me.JobTrackingLayoutPanel, "JobTrackingLayoutPanel")
        Me.JobTrackingLayoutPanel.Controls.Add(EmailStatusLabel, 0, 14)
        Me.JobTrackingLayoutPanel.Controls.Add(Me.EMailStatusPicker, 1, 14)
        Me.JobTrackingLayoutPanel.Controls.Add(JobNumberLabel, 0, 0)
        Me.JobTrackingLayoutPanel.Controls.Add(Me.JobNumberTextBox, 1, 0)
        Me.JobTrackingLayoutPanel.Controls.Add(ConsultantUidLabel, 0, 9)
        Me.JobTrackingLayoutPanel.Controls.Add(Me.ConsultantUidTextBox, 1, 9)
        Me.JobTrackingLayoutPanel.Controls.Add(JobStatusIDLabel, 0, 16)
        Me.JobTrackingLayoutPanel.Controls.Add(Me.JobStatusPicker, 1, 16)
        Me.JobTrackingLayoutPanel.Controls.Add(UIDLabel, 0, 1)
        Me.JobTrackingLayoutPanel.Controls.Add(Me.JobUIDTextBox, 1, 1)
        Me.JobTrackingLayoutPanel.Controls.Add(IDLabel, 0, 2)
        Me.JobTrackingLayoutPanel.Controls.Add(Me.JobIDTextBox, 1, 2)
        Me.JobTrackingLayoutPanel.Controls.Add(ConsultantJobIDLabel, 0, 3)
        Me.JobTrackingLayoutPanel.Controls.Add(Me.ConsultantJobIDTextBox, 1, 3)
        Me.JobTrackingLayoutPanel.Controls.Add(FlagLabel, 0, 4)
        Me.JobTrackingLayoutPanel.Controls.Add(Me.FlagTextBox, 1, 4)
        Me.JobTrackingLayoutPanel.Controls.Add(Me.ClientSiteUidTextBox, 1, 8)
        Me.JobTrackingLayoutPanel.Controls.Add(Me.CreatedTimeTextBox, 1, 11)
        Me.JobTrackingLayoutPanel.Controls.Add(ClientSiteUidLabel, 0, 8)
        Me.JobTrackingLayoutPanel.Controls.Add(CreatedTimeLabel, 0, 11)
        Me.JobTrackingLayoutPanel.Controls.Add(Me.RequestUidTextBox, 1, 6)
        Me.JobTrackingLayoutPanel.Controls.Add(Me.ModifiedTimeTextBox, 1, 10)
        Me.JobTrackingLayoutPanel.Controls.Add(RequestUidLabel, 0, 6)
        Me.JobTrackingLayoutPanel.Controls.Add(ModifiedTimeLabel, 0, 10)
        Me.JobTrackingLayoutPanel.Controls.Add(Me.TrackingInfoTextBox, 1, 17)
        Me.JobTrackingLayoutPanel.Controls.Add(Me.TrackingInfoLabel, 0, 17)
        Me.JobTrackingLayoutPanel.Name = "JobTrackingLayoutPanel"
        '
        'EMailStatusPicker
        '
        resources.ApplyResources(Me.EMailStatusPicker, "EMailStatusPicker")
        Me.EMailStatusPicker.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.EMailStatusPicker.Name = "EMailStatusPicker"
        '
        'JobNumberTextBox
        '
        resources.ApplyResources(Me.JobNumberTextBox, "JobNumberTextBox")
        Me.JobNumberTextBox.Name = "JobNumberTextBox"
        Me.JobNumberTextBox.ReadOnly = True
        '
        'ConsultantUidTextBox
        '
        resources.ApplyResources(Me.ConsultantUidTextBox, "ConsultantUidTextBox")
        Me.ConsultantUidTextBox.Name = "ConsultantUidTextBox"
        Me.ConsultantUidTextBox.ReadOnly = True
        '
        'JobStatusPicker
        '
        resources.ApplyResources(Me.JobStatusPicker, "JobStatusPicker")
        Me.JobStatusPicker.Name = "JobStatusPicker"
        '
        'JobUIDTextBox
        '
        resources.ApplyResources(Me.JobUIDTextBox, "JobUIDTextBox")
        Me.JobUIDTextBox.Name = "JobUIDTextBox"
        Me.JobUIDTextBox.ReadOnly = True
        '
        'JobIDTextBox
        '
        resources.ApplyResources(Me.JobIDTextBox, "JobIDTextBox")
        Me.JobIDTextBox.Name = "JobIDTextBox"
        Me.JobIDTextBox.ReadOnly = True
        '
        'ConsultantJobIDTextBox
        '
        resources.ApplyResources(Me.ConsultantJobIDTextBox, "ConsultantJobIDTextBox")
        Me.ConsultantJobIDTextBox.Name = "ConsultantJobIDTextBox"
        Me.ConsultantJobIDTextBox.ReadOnly = True
        '
        'FlagTextBox
        '
        resources.ApplyResources(Me.FlagTextBox, "FlagTextBox")
        Me.FlagTextBox.Name = "FlagTextBox"
        Me.FlagTextBox.ReadOnly = True
        '
        'ClientSiteUidTextBox
        '
        resources.ApplyResources(Me.ClientSiteUidTextBox, "ClientSiteUidTextBox")
        Me.ClientSiteUidTextBox.Name = "ClientSiteUidTextBox"
        Me.ClientSiteUidTextBox.ReadOnly = True
        '
        'CreatedTimeTextBox
        '
        resources.ApplyResources(Me.CreatedTimeTextBox, "CreatedTimeTextBox")
        Me.CreatedTimeTextBox.Name = "CreatedTimeTextBox"
        Me.CreatedTimeTextBox.ReadOnly = True
        '
        'RequestUidTextBox
        '
        resources.ApplyResources(Me.RequestUidTextBox, "RequestUidTextBox")
        Me.RequestUidTextBox.Name = "RequestUidTextBox"
        Me.RequestUidTextBox.ReadOnly = True
        '
        'ModifiedTimeTextBox
        '
        resources.ApplyResources(Me.ModifiedTimeTextBox, "ModifiedTimeTextBox")
        Me.ModifiedTimeTextBox.Name = "ModifiedTimeTextBox"
        Me.ModifiedTimeTextBox.ReadOnly = True
        '
        'TrackingInfoTextBox
        '
        resources.ApplyResources(Me.TrackingInfoTextBox, "TrackingInfoTextBox")
        Me.TrackingInfoTextBox.Name = "TrackingInfoTextBox"
        '
        'TrackingInfoLabel
        '
        resources.ApplyResources(Me.TrackingInfoLabel, "TrackingInfoLabel")
        Me.TrackingInfoLabel.Name = "TrackingInfoLabel"
        '
        'DoneButton
        '
        resources.ApplyResources(Me.DoneButton, "DoneButton")
        Me.DoneButton.Name = "DoneButton"
        Me.DoneButton.UseVisualStyleBackColor = True
        '
        'AbortButton
        '
        resources.ApplyResources(Me.AbortButton, "AbortButton")
        Me.AbortButton.Name = "AbortButton"
        Me.AbortButton.UseVisualStyleBackColor = True
        '
        'JobSupportInfoForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.AbortButton)
        Me.Controls.Add(Me.DoneButton)
        Me.Controls.Add(Me.JobTrackingLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "JobSupportInfoForm"
        Me.JobTrackingLayoutPanel.ResumeLayout(False)
        Me.JobTrackingLayoutPanel.PerformLayout()
        CType(Me.EMailStatusPicker, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents JobTrackingLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents EMailStatusPicker As System.Windows.Forms.NumericUpDown
    Friend WithEvents JobStatusPicker As System.Windows.Forms.DomainUpDown
    Friend WithEvents JobIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents FlagTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JobNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ModifiedTimeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CreatedTimeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ConsultantJobIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JobUIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ConsultantUidTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RequestUidTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ClientSiteUidTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DoneButton As System.Windows.Forms.Button
    Friend WithEvents AbortButton As System.Windows.Forms.Button
    Friend WithEvents TrackingInfoTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TrackingInfoLabel As System.Windows.Forms.Label
    Friend WithEvents ToolTipProvider As System.Windows.Forms.ToolTip
End Class
