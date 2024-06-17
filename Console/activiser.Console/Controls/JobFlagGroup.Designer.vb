<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JobFlagGroup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(JobFlagGroup))
        Me.JobApprovalGroupBox = New System.Windows.Forms.GroupBox
        Me.ManagementCheckBox = New System.Windows.Forms.CheckBox
        Me.AdministrationCheckBox = New System.Windows.Forms.CheckBox
        Me.JobApprovalGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'JobApprovalGroupBox
        '
        resources.ApplyResources(Me.JobApprovalGroupBox, "JobApprovalGroupBox")
        Me.JobApprovalGroupBox.Controls.Add(Me.ManagementCheckBox)
        Me.JobApprovalGroupBox.Controls.Add(Me.AdministrationCheckBox)
        Me.JobApprovalGroupBox.Name = "JobApprovalGroupBox"
        Me.JobApprovalGroupBox.TabStop = False
        '
        'ManagementCheckBox
        '
        resources.ApplyResources(Me.ManagementCheckBox, "ManagementCheckBox")
        Me.ManagementCheckBox.Name = "ManagementCheckBox"
        '
        'AdministrationCheckBox
        '
        resources.ApplyResources(Me.AdministrationCheckBox, "AdministrationCheckBox")
        Me.AdministrationCheckBox.Name = "AdministrationCheckBox"
        '
        'JobFlagGroup
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.JobApprovalGroupBox)
        Me.Name = "JobFlagGroup"
        Me.JobApprovalGroupBox.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents JobApprovalGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ManagementCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents AdministrationCheckBox As System.Windows.Forms.CheckBox

End Class
