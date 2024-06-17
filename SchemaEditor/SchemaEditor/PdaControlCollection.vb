
Public Class PdaControlCollection
    Inherits Generic.LinkedList(Of CustomControl)

    Private _selectedItem As CustomControl
    Private _parent As Control

    Public Event SelectedItemChanged(ByVal sender As Object, ByVal e As SelectedItemChangedEventArgs)

    Public Property SelectedItem() As CustomControl
        Get
            Return _selectedItem
        End Get
        Set(ByVal value As CustomControl)
            If MyBase.Contains(value) Then
                _selectedItem = value
                RaiseEvent SelectedItemChanged(Me, New SelectedItemChangedEventArgs(value))
            Else
                Throw New ArgumentOutOfRangeException("value", "Control is not in this collection")
            End If
        End Set
    End Property

    Public Property Parent() As Control
        Get
            Return _parent
        End Get
        Set(ByVal value As Control)
            _parent = value
        End Set
    End Property

    Public Sub MoveUp(ByVal value As CustomControl)
        Dim previousControl As Generic.LinkedListNode(Of CustomControl) = Me.Find(value).Previous
        If previousControl IsNot Nothing Then
            If Me.Remove(value) Then
                Me.AddBefore(previousControl, value)
                Dim s1 As Byte = previousControl.Value.FormFieldRow.DisplayOrder
                Dim s2 As Byte = value.FormFieldRow.DisplayOrder
                value.FormFieldRow.DisplayOrder = s1
                previousControl.Value.FormFieldRow.DisplayOrder = s2
            End If
        End If
    End Sub

    Public Sub MoveDown(ByVal value As CustomControl)
        Dim nextControl As Generic.LinkedListNode(Of CustomControl) = Me.Find(value).Next
        If nextControl IsNot Nothing Then
            If Me.Remove(value) Then
                Me.AddAfter(nextControl, value)
                Dim s1 As Byte = nextControl.Value.FormFieldRow.DisplayOrder
                Dim s2 As Byte = value.FormFieldRow.DisplayOrder
                value.FormFieldRow.DisplayOrder = s1
                nextControl.Value.FormFieldRow.DisplayOrder = s2
            End If
        End If
    End Sub

    'Public ReadOnly Property ItemNode(ByVal customControl As CustomControl) As Generic.LinkedListNode(Of CustomControl)
    '    Get
    '        Return Me.Find(customControl)
    '    End Get
    'End Property

    Public Sub ArrangeItems()
        If Me.First Is Nothing Then
            Exit Sub
        End If
        Dim pc As CustomControl
        Dim nextPc As CustomControl '  LinkedListNode(Of PdaControl) = _pdaControlCollection.First
        Dim lastPc As CustomControl
        Dim t As Integer
        Dim l As Integer
        Dim w As Integer
        Dim h As Integer
        Dim g As Integer = 4 ' gap

        Dim n As LinkedListNode(Of CustomControl) = Me.First
        Dim s As Byte = 1
        Do While n IsNot Nothing
            pc = n.Value
            If pc.DisplayOrder <> 0 Then
                pc.DisplayOrder = s
                s += CByte(1)
            End If
            If n.Next IsNot Nothing Then
                nextPc = n.Next.Value
            Else
                nextPc = Nothing
            End If
            If n.Previous IsNot Nothing Then
                lastPc = n.Previous.Value
            Else
                lastPc = Nothing
            End If
            h = pc.GetHeight
            w = pc.GetWidth ' ((240 * pc.WidthPercent) \ 100) + 10 ' ok, so 240 is a hack
            If pc.ControlPosition = ControlPosition.Right Then
                l = 260 - w
            Else
                l = 0
            End If
            pc.Location = New Point(l, t)
            pc.Size = New Size(w, h)

            If nextPc Is Nothing Then Exit Do
            If Not ((pc.ControlPosition = ControlPosition.Left) AndAlso (nextPc.ControlPosition = ControlPosition.Right) AndAlso (nextPc.ControlWidth + pc.ControlWidth <= 100)) Then
                If lastPc IsNot Nothing Then
                    If (pc.ControlPosition = ControlPosition.Right) AndAlso (lastPc.ControlPosition = ControlPosition.Left) AndAlso (lastPc.ControlWidth + pc.ControlWidth <= 100) Then
                        If pc.Height < lastPc.Height Then
                            h = lastPc.Height
                        End If
                    End If
                End If
                t += h + g ' move top up.
            End If
            n = n.Next
        Loop
    End Sub

    Public Overloads Function Contains(ByVal fieldName As String) As Boolean
        Dim n As LinkedListNode(Of CustomControl) = Me.First
        Do While n IsNot Nothing
            Dim pc As CustomControl = n.Value
            If pc.FieldName = fieldName Then Return True
            n = n.Next
        Loop
        Return False
    End Function

    Public ReadOnly Property TopSequenceNumber() As Byte
        Get
            Dim n As LinkedListNode(Of CustomControl) = Me.First
            Dim result As Byte = 0
            Do While n IsNot Nothing
                Dim pc As CustomControl = n.Value
                If pc.DisplayOrder > result Then result = pc.DisplayOrder
                n = n.Next
            Loop
            Return result
        End Get
    End Property
End Class
