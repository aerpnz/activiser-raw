Public Module Utilities
    'Friend Function IIf(Of t)(ByVal expression As Boolean, ByVal trueValue As t, ByVal falseValue As t) As t
    '    If expression Then
    '        Return trueValue
    '    Else
    '        Return falseValue
    '    End If
    'End Function

    'Public Function FixTime(ByVal value As DateTime, ByVal granularity As Integer) As DateTime
    '    Dim oddMinutes As Integer
    '    value = value.Subtract(New TimeSpan(0, 0, value.Second))
    '    If granularity > 1 Then
    '        oddMinutes = value.Minute Mod granularity
    '        If oddMinutes <= granularity \ 2 Then
    '            value = value.Subtract(New TimeSpan(0, oddMinutes, 0))
    '        Else
    '            value = value.Add(New TimeSpan(0, granularity - oddMinutes, 0))
    '        End If
    '    End If
    '    Return value
    'End Function

    Public Function FixTime(ByVal value As DateTime) As DateTime ' drop seconds and fractions...
        Return New DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0, value.Kind)
    End Function

    Friend Function GetShortTimeFormat(ByVal LongFormat As String) As String
        Dim i As Integer = LongFormat.LastIndexOf(":"c)
        If i = -1 Then i = LongFormat.IndexOf("s")
        If i = -1 Then Return LongFormat
        Dim result As String = LongFormat.Substring(0, i)
        If LongFormat.IndexOfAny("t".ToCharArray) <> -1 Then
            Dim remainder As String = LongFormat.Substring(i)
            Dim j As Integer = remainder.LastIndexOfAny("sS.fF".ToCharArray)
            If j <> -1 AndAlso j < remainder.Length Then
                Dim tail As String = remainder.Substring(j + 1)
                result &= tail
            End If
        End If
        Return result
    End Function

    Friend Sub locateDropDown(ByVal target As Control, ByVal anchor As Control, ByVal preferLeft As Boolean)
        Dim myScreen As Rectangle = Windows.Forms.Screen.FromControl(anchor).WorkingArea

        Dim anchorRect As Rectangle = anchor.ClientRectangle
        Dim targetRect As Rectangle = anchor.RectangleToScreen(anchorRect)
        Dim newLocation As Point
        If preferLeft Then
            ' try anchoring to bottom left
            newLocation = New Point(targetRect.Left, targetRect.Bottom)
        Else
            'try anchoring to bottom right
            newLocation = New Point(targetRect.Right - target.Width, targetRect.Bottom)
        End If

        ' if lost off the bottom, drop it up instead of down!
        If newLocation.Y + target.Height > myScreen.Bottom Then
            If preferLeft Then
                newLocation = New Point(targetRect.Left, targetRect.Top - target.Height)
            Else
                newLocation = New Point(targetRect.Right - target.Width, targetRect.Top - target.Height)
            End If
        End If

        ' if off the edge of the screen, move it over
        If newLocation.X + target.Width > myScreen.Right Then
            newLocation = New Point(myScreen.Right - target.Width, newLocation.Y)
        End If

        ' if off the edge of the screen, move it over
        If newLocation.X < myScreen.Left Then
            newLocation = New Point(myScreen.X, newLocation.Y)
        End If

        'Debug.WriteLine(String.Format("relocating '{0}' to {1}", target.Name, newLocation.ToString))
        target.SuspendLayout()
        target.Location = newLocation
        target.ResumeLayout()
        Application.DoEvents()
    End Sub
End Module
