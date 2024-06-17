Imports System.Windows.Forms

Public Class FormTypePicker

    Public Sub New(ByVal tableName As String, ByVal possibleColumnsList As Collections.Specialized.StringCollection)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.TableName = tableName
        Me.PossibleColumnsList = possibleColumnsList
    End Sub


    Private _tableName As String
    Public Property TableName() As String
        Get
            Return _tableName
        End Get
        Set(ByVal value As String)
            _tableName = value
            Me.Label1.Text = String.Format("Please select the core table that '{0}' is related to", value)
        End Set
    End Property

    Private _parentEntity As String
    Public Property ParentEntity() As String
        Get
            Return _parentEntity
        End Get
        Set(ByVal value As String)
            _parentEntity = value
        End Set
    End Property

    Private _foreignKeyColumn As String
    Public ReadOnly Property SelectedColumn() As String
        Get
            Return _foreignKeyColumn
        End Get
    End Property

    Private _possibleColumnsList As System.Collections.Specialized.StringCollection
    Public Property PossibleColumnsList() As System.Collections.Specialized.StringCollection
        Get
            Return _possibleColumnsList
        End Get
        Set(ByVal value As System.Collections.Specialized.StringCollection)
            _possibleColumnsList = value
            For Each s As String In value
                Me.ColumnList.Items.Add(s)
            Next
        End Set
    End Property

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If Me.ColumnList.SelectedItem IsNot Nothing AndAlso CStr(Me.ColumnList.SelectedItem) <> "" Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub RadioCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioRequest.CheckedChanged, RadioJob.CheckedChanged, RadioClient.CheckedChanged
        If Me.RadioClient.Checked Then
            Me._parentEntity = "ClientSite"
        ElseIf Me.RadioRequest.Checked Then
            Me._parentEntity = "Request"
        ElseIf Me.RadioJob.Checked Then
            Me._parentEntity = "Job"
        End If
    End Sub

    Private Sub ColumnList_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ColumnList.SelectedValueChanged
        Me._foreignKeyColumn = CStr(Me.ColumnList.SelectedItem)
    End Sub

    Private Sub FormTypePicker_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
