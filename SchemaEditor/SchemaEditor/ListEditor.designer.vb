<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ListEditor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Cancel = New System.Windows.Forms.Button
        Me.Accept = New System.Windows.Forms.Button
        Me.ListEditorDataSet1 = New ListEditorDataSet
        Me.DataValueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DisplayValueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.ListEditorDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataValueDataGridViewTextBoxColumn, Me.DisplayValueDataGridViewTextBoxColumn})
        Me.DataGridView1.DataMember = "ListItems"
        Me.DataGridView1.DataSource = Me.ListEditorDataSet1
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(292, 227)
        Me.DataGridView1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Cancel)
        Me.GroupBox1.Controls.Add(Me.Accept)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Location = New System.Drawing.Point(0, 227)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(292, 39)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'Cancel
        '
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(149, 10)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 1
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'Accept
        '
        Me.Accept.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Accept.Location = New System.Drawing.Point(68, 10)
        Me.Accept.Name = "Accept"
        Me.Accept.Size = New System.Drawing.Size(75, 23)
        Me.Accept.TabIndex = 0
        Me.Accept.Text = "Ok"
        Me.Accept.UseVisualStyleBackColor = True
        '
        'ListEditorDataSet1
        '
        Me.ListEditorDataSet1.DataSetName = "ListEditorDataSet"
        Me.ListEditorDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DataValueDataGridViewTextBoxColumn
        '
        Me.DataValueDataGridViewTextBoxColumn.DataPropertyName = "DataValue"
        Me.DataValueDataGridViewTextBoxColumn.HeaderText = "DataValue"
        Me.DataValueDataGridViewTextBoxColumn.MaxInputLength = 128
        Me.DataValueDataGridViewTextBoxColumn.Name = "DataValueDataGridViewTextBoxColumn"
        '
        'DisplayValueDataGridViewTextBoxColumn
        '
        Me.DisplayValueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DisplayValueDataGridViewTextBoxColumn.DataPropertyName = "DisplayValue"
        Me.DisplayValueDataGridViewTextBoxColumn.HeaderText = "DisplayValue"
        Me.DisplayValueDataGridViewTextBoxColumn.MaxInputLength = 255
        Me.DisplayValueDataGridViewTextBoxColumn.Name = "DisplayValueDataGridViewTextBoxColumn"
        '
        'ListEditor
        '
        Me.AcceptButton = Me.Accept
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ListEditor"
        Me.Text = "ListEditor"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.ListEditorDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Accept As System.Windows.Forms.Button
    Friend WithEvents ListEditorDataSet1 As ListEditorDataSet
    Friend WithEvents DataValueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DisplayValueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
