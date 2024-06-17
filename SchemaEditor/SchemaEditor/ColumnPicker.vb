Imports System.Windows.Forms
Imports activiser.SchemaEditor.CandidateEntityDataSet

Public Class ColumnPicker
    Public Sub New(ByVal ColumnList() As CandidateEntityAttributeRow)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.ListView1.Items.Clear()
        For Each dr As CandidateEntityAttributeRow In ColumnList
            Dim lvi As New ListViewItem(New String() {dr.AttributeName, dr.AttributeType, If(dr.AttributeIsPK = 0, "N", "Y")})
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

    Public ReadOnly Property SelectedColumn() As CandidateEntityAttributeRow
        Get
            For Each lvi As ListViewItem In Me.ListView1.SelectedItems
                Dim tr As CandidateEntityAttributeRow = TryCast(lvi.Tag, CandidateEntityAttributeRow)
                If tr IsNot Nothing Then
                    Return tr
                End If
            Next
            Return Nothing
        End Get
    End Property

    Private Sub ListView1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class
