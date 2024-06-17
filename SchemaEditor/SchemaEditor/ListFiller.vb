Imports System.ComponentModel

Public Class ListFiller
    Public Key As Object
    Public DisplayValue As String
    Public Sub New(ByVal key As Object, ByVal displayValue As String)
        Me.Key = key
        Me.DisplayValue = displayValue
    End Sub

    Public Overrides Function ToString() As String
        Return DisplayValue
    End Function

    Public Shared Function GetList(ByVal listData As String) As List(Of ListFiller)
        Dim listReader As New IO.StringReader(listData)
        Dim result As New Generic.List(Of ListFiller)
        Dim listItem As String

        Do
            listItem = listReader.ReadLine
            If listItem Is Nothing Then Exit Do
            If Not String.IsNullOrEmpty(listItem.Trim) Then
                Dim listKey, listValue As String
                Dim separatorLocation As Integer = listItem.IndexOf(";"c)
                listKey = listItem.Substring(0, separatorLocation)
                listValue = listItem.Substring(separatorLocation + 1, listItem.Length - separatorLocation)
                result.Add(New ListFiller(listKey, listValue))
            End If
        Loop
        Return result
    End Function

    Public Shared Sub FillListBoxData(ByVal target As ComboBox, ByVal listData As String)
        Dim listReader As New IO.StringReader(listData)
        Dim listItems As New Generic.List(Of String)
        Dim listItem As String

        Do
            listItem = listReader.ReadLine
            If listItem Is Nothing Then Exit Do
            If Not String.IsNullOrEmpty(listItem.Trim) Then
                listItems.Add(listItem)
            End If
        Loop
        For Each listItem In listItems
            Dim listKey, listValue As String
            Dim separatorLocation As Integer = listItem.IndexOf(";"c)
            listKey = listItem.Substring(0, separatorLocation)
            listValue = listItem.Substring(separatorLocation + 1, listItem.Length - separatorLocation)
            If Not String.IsNullOrEmpty(listKey) AndAlso Not String.IsNullOrEmpty(listValue) Then
                target.Items.Add(New ListFiller(listKey, listValue))
            End If
        Next
    End Sub
End Class