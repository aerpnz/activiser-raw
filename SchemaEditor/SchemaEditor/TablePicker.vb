Imports System.Windows.Forms
Imports activiser.SchemaEditor.CandidateEntityDataSet

Public Class TablePicker
    Public Sub New(ByVal TableList As CandidateEntityDataTable)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        For Each dr As CandidateEntityRow In TableList
            Dim lvi As New ListViewItem(New String() {dr.EntityName, dr.SchemaName, dr.SqlObjectType})
            lvi.Tag = dr
            Me.ListView1.Items.Add(lvi)
        Next
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public ReadOnly Property SelectedTables() As CandidateEntityRow()
        Get
            Dim drlist As New List(Of CandidateEntityRow)
            For Each lvi As ListViewItem In Me.ListView1.CheckedItems
                Dim tr As CandidateEntityRow = TryCast(lvi.Tag, CandidateEntityRow)
                If tr IsNot Nothing Then
                    drlist.Add(tr)
                End If
            Next
            Return drlist.ToArray()
        End Get
    End Property

    Public ReadOnly Property SelectedTable() As CandidateEntityRow
        Get
            For Each lvi As ListViewItem In Me.ListView1.SelectedItems
                Dim tr As CandidateEntityRow = TryCast(lvi.Tag, CandidateEntityRow)
                If tr IsNot Nothing Then
                    Return tr
                End If
            Next
            Return Nothing
        End Get
    End Property

    Private _multiSelect As Boolean
    Public Property MultiSelect() As Boolean
        Get
            Return _multiSelect
        End Get
        Set(ByVal value As Boolean)
            _multiSelect = value
            Me.ListView1.MultiSelect = value
            Me.ListView1.CheckBoxes = value
        End Set
    End Property

    Private Sub ListView1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class
