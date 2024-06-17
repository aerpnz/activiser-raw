<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomFormPanel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomFormPanel))
        Me.CustomFormBorder = New System.Windows.Forms.GroupBox
        Me.CustomControlPanel = New System.Windows.Forms.Panel
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.AddRecordButton = New System.Windows.Forms.ToolStripButton
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel
        Me.DeleteRecordButton = New System.Windows.Forms.ToolStripButton
        Me.MoveFirstButton = New System.Windows.Forms.ToolStripButton
        Me.MovePreviousButton = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.MoveNextButton = New System.Windows.Forms.ToolStripButton
        Me.MoveLastButton = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.UndoChangesButton = New System.Windows.Forms.ToolStripButton
        Me.CustomFormBorder.SuspendLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CustomFormBorder
        '
        Me.CustomFormBorder.Controls.Add(Me.CustomControlPanel)
        Me.CustomFormBorder.Controls.Add(Me.BindingNavigator1)
        Me.CustomFormBorder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CustomFormBorder.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.CustomFormBorder.Location = New System.Drawing.Point(0, 0)
        Me.CustomFormBorder.Margin = New System.Windows.Forms.Padding(4)
        Me.CustomFormBorder.Name = "CustomFormBorder"
        Me.CustomFormBorder.Padding = New System.Windows.Forms.Padding(8, 16, 8, 8)
        Me.CustomFormBorder.Size = New System.Drawing.Size(320, 520)
        Me.CustomFormBorder.TabIndex = 0
        Me.CustomFormBorder.TabStop = False
        '
        'CustomControlPanel
        '
        Me.CustomControlPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CustomControlPanel.AutoScroll = True
        Me.CustomControlPanel.Location = New System.Drawing.Point(8, 16)
        Me.CustomControlPanel.Name = "CustomControlPanel"
        Me.CustomControlPanel.Size = New System.Drawing.Size(304, 468)
        Me.CustomControlPanel.TabIndex = 3
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Me.AddRecordButton
        Me.BindingNavigator1.BindingSource = Me.BindingSource1
        Me.BindingNavigator1.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1.DeleteItem = Nothing
        Me.BindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MoveFirstButton, Me.MovePreviousButton, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.MoveNextButton, Me.MoveLastButton, Me.BindingNavigatorSeparator2, Me.UndoChangesButton, Me.AddRecordButton, Me.DeleteRecordButton})
        Me.BindingNavigator1.Location = New System.Drawing.Point(8, 487)
        Me.BindingNavigator1.MoveFirstItem = Me.MoveFirstButton
        Me.BindingNavigator1.MoveLastItem = Me.MoveLastButton
        Me.BindingNavigator1.MoveNextItem = Me.MoveNextButton
        Me.BindingNavigator1.MovePreviousItem = Me.MovePreviousButton
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Me.BindingNavigatorPositionItem
        Me.BindingNavigator1.Size = New System.Drawing.Size(304, 25)
        Me.BindingNavigator1.TabIndex = 0
        Me.BindingNavigator1.Text = "BindingNavigator1"
        '
        'AddRecordButton
        '
        Me.AddRecordButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AddRecordButton.Image = CType(resources.GetObject("AddRecordButton.Image"), System.Drawing.Image)
        Me.AddRecordButton.Name = "AddRecordButton"
        Me.AddRecordButton.RightToLeftAutoMirrorImage = True
        Me.AddRecordButton.Size = New System.Drawing.Size(23, 22)
        Me.AddRecordButton.Text = "Add new"
        '
        'BindingSource1
        '
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(36, 22)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'DeleteRecordButton
        '
        Me.DeleteRecordButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.DeleteRecordButton.Image = CType(resources.GetObject("DeleteRecordButton.Image"), System.Drawing.Image)
        Me.DeleteRecordButton.Name = "DeleteRecordButton"
        Me.DeleteRecordButton.RightToLeftAutoMirrorImage = True
        Me.DeleteRecordButton.Size = New System.Drawing.Size(23, 22)
        Me.DeleteRecordButton.Text = "Delete"
        '
        'MoveFirstButton
        '
        Me.MoveFirstButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MoveFirstButton.Image = CType(resources.GetObject("MoveFirstButton.Image"), System.Drawing.Image)
        Me.MoveFirstButton.Name = "MoveFirstButton"
        Me.MoveFirstButton.RightToLeftAutoMirrorImage = True
        Me.MoveFirstButton.Size = New System.Drawing.Size(23, 22)
        Me.MoveFirstButton.Text = "Move first"
        '
        'MovePreviousButton
        '
        Me.MovePreviousButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MovePreviousButton.Image = CType(resources.GetObject("MovePreviousButton.Image"), System.Drawing.Image)
        Me.MovePreviousButton.Name = "MovePreviousButton"
        Me.MovePreviousButton.RightToLeftAutoMirrorImage = True
        Me.MovePreviousButton.Size = New System.Drawing.Size(23, 22)
        Me.MovePreviousButton.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 21)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'MoveNextButton
        '
        Me.MoveNextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MoveNextButton.Image = CType(resources.GetObject("MoveNextButton.Image"), System.Drawing.Image)
        Me.MoveNextButton.Name = "MoveNextButton"
        Me.MoveNextButton.RightToLeftAutoMirrorImage = True
        Me.MoveNextButton.Size = New System.Drawing.Size(23, 22)
        Me.MoveNextButton.Text = "Move next"
        '
        'MoveLastButton
        '
        Me.MoveLastButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MoveLastButton.Image = CType(resources.GetObject("MoveLastButton.Image"), System.Drawing.Image)
        Me.MoveLastButton.Name = "MoveLastButton"
        Me.MoveLastButton.RightToLeftAutoMirrorImage = True
        Me.MoveLastButton.Size = New System.Drawing.Size(23, 22)
        Me.MoveLastButton.Text = "Move last"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'UndoChangesButton
        '
        Me.UndoChangesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.UndoChangesButton.Image = Global.activiser.Console.My.Resources.Resources.Edit_UndoHS
        Me.UndoChangesButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UndoChangesButton.Name = "UndoChangesButton"
        Me.UndoChangesButton.Size = New System.Drawing.Size(23, 22)
        Me.UndoChangesButton.Text = "Undo"
        '
        'CustomFormPanel
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.Controls.Add(Me.CustomFormBorder)
        Me.Name = "CustomFormPanel"
        Me.Size = New System.Drawing.Size(320, 520)
        Me.CustomFormBorder.ResumeLayout(False)
        Me.CustomFormBorder.PerformLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CustomFormBorder As System.Windows.Forms.GroupBox
    Friend WithEvents CustomControlPanel As System.Windows.Forms.Panel
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents BindingNavigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents AddRecordButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents DeleteRecordButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MoveFirstButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MovePreviousButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MoveNextButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents MoveLastButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents UndoChangesButton As System.Windows.Forms.ToolStripButton

End Class
