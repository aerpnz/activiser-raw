Imports System.Reflection
Imports System.ComponentModel

Module Utilities
    Public Const GetXRatio As Integer = 1
    Public Const GetYRatio As Integer = 1

    Public MyDpiX As Double = 96.0!
    Public MyDpiY As Double = 96.0!

    Public isLandScape As Boolean = False

    Public Function GetEnumDescription(ByVal e As [Enum]) As String
        Dim EnumInfo As FieldInfo = e.GetType().GetField(e.ToString())
        Dim EnumAttributes() As DescriptionAttribute = CType(EnumInfo.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())

        If (EnumAttributes.Length > 0) Then
            Return EnumAttributes(0).Description
        End If

        Return e.ToString()
    End Function

    'Me.StyleSelector.Items.AddRange(System.Enum.GetNames(GetType(ControlType)))
    Public Function GetEnumDescriptions(ByVal eType As Type) As String()
        Dim fields() As String = [Enum].GetNames(eType)
        Dim result() As String
        ReDim result(UBound(fields))

        Dim descAttr As Type = GetType(DescriptionAttribute)

        For i As Integer = 0 To UBound(fields)
            Dim e As String = fields(i)
            result(i) = e

            'Dim EnumInfo As FieldInfo = eType.GetField(e)
            'Dim descriptions As Object() = EnumInfo.GetCustomAttributes(descAttr, False)
            'If descriptions IsNot Nothing Then
            '    Dim EnumAttributes() As DescriptionAttribute = CType(descriptions, DescriptionAttribute())
            '    result(i) = If((EnumAttributes.Length > 0), EnumAttributes(0).Description, e)
            'Else
            '    result(i) = e
            'End If

        Next
        Array.Sort(result)
        Return result
    End Function

    Public Function ExpandName(ByVal value As String, Optional ByVal stripPrefix As Boolean = False) As String
        Dim sStrippedName As String

        If stripPrefix Then
            sStrippedName = StripPrefixFromName(value)
        Else
            sStrippedName = value
        End If

        Dim source() As Char = sStrippedName.ToCharArray
        Dim result As New System.Text.StringBuilder(source.Length)

        Dim lastC As Char = " "c
        Dim top As Integer = source.Length - 1
        For i As Integer = 0 To top
            Dim c As Char = source(i)
            Dim nextC As Char = Char.MinValue
            If i < top Then
                nextC = source(i + 1)
            End If
            If (Char.IsWhiteSpace(c) OrElse Char.IsUpper(c) OrElse Char.IsNumber(c)) Then
                If Not nextC = Char.MinValue Then
                    If Not (Char.IsUpper(nextC) OrElse Char.IsNumber(nextC)) AndAlso Not Char.IsWhiteSpace(nextC) Then
                        result.Append(" "c)
                    End If
                End If
                If Not Char.IsWhiteSpace(c) Then
                    result.Append(c)
                End If
            Else
                result.Append(c)
            End If
            lastC = c
        Next
        Return result.ToString.Trim
    End Function

    Public Function StripPrefixFromName(ByVal value As String) As String
        Dim source() As Char = value.ToCharArray

        For i As Integer = 0 To source.GetUpperBound(0)
            If Char.IsUpper(source(i)) Then
                Return New String(source, i, source.Length - i)
            End If
        Next

        ' if we get here, there would appear to be no prefix, just return the original string
        Return value
    End Function

    Public Function GetPrefixFromName(ByVal value As String) As String
        Dim source() As Char = value.ToCharArray

        For i As Integer = 0 To source.GetUpperBound(0)
            If Char.IsUpper(source(i)) Then
                Return New String(source, 0, i)
            End If
        Next

        ' if we get here, there would appear to be no prefix, just return an empty string
        Return String.Empty
    End Function

    Public Function IIf(ByVal b As Boolean, ByVal x As Integer, ByVal y As Integer) As Integer
        If b Then
            Return x
        Else
            Return y
        End If
    End Function

    Public Function IIf(ByVal b As Boolean, ByVal x As Byte, ByVal y As Byte) As Byte
        If b Then
            Return x
        Else
            Return y
        End If
    End Function
End Module
