Imports System.Runtime.CompilerServices

#If WINDOWSMOBILE Then
Imports Microsoft.WindowsMobile

Module Phone
    Private Const MODULENAME As String = "Phone"
    Private ReadOnly minimumVersion As System.Version = New Version(5, 0)
    Private ReadOnly versionOk As Boolean = Environment.OSVersion.Version >= minimumVersion

    Public Sub MakePhoneCall(ByVal phoneNumber As String)
        If Not versionOk Then
            Throw New NotSupportedException()
            Return
        ElseIf Not Microsoft.WindowsMobile.Status.SystemState.PhoneRadioPresent Then
            ' display no phone message
            Throw New InvalidOperationException("No Radio Present")
            Return
        ElseIf Microsoft.WindowsMobile.Status.SystemState.PhoneNoService Then
            ' display no service message
            Terminology.DisplayMessage(Nothing, MODULENAME, "NoService", MessageBoxIcon.Asterisk)
            Return
        Else
            Dim prompt As String
            If ValidatePhoneNumber(phoneNumber) Then
                prompt = "ThisNumberPrompt"
            Else
                prompt = "ParseProblem"
            End If

            phoneNumber = Terminology.AskQuestion(Nothing, MODULENAME, prompt, ParsePhoneNumber(phoneNumber), False, 30)

            If Not String.IsNullOrEmpty(phoneNumber) Then
                Dim phone As New Microsoft.WindowsMobile.Telephony.Phone
                phone.Talk(phoneNumber, False)
            End If

        End If
    End Sub

    Public Sub MakePhoneCall(ByVal textBox As TextBox)
        If textBox Is Nothing Then Return
        If Not String.IsNullOrEmpty(textBox.SelectedText) Then
            MakePhoneCall(textBox.SelectedText.Trim)
        ElseIf Not String.IsNullOrEmpty(textBox.Text) Then
            MakePhoneCall(textBox.Text)
        Else
            ' no number to call
        End If
    End Sub

    ''TODO: Finish this.
    'Public Sub SendSms(ByVal phoneNumber As String)
    '    If Not versionOk Then
    '        Return
    '    ElseIf Not Microsoft.WindowsMobile.Status.SystemState.PhoneRadioPresent Then
    '        ' display no phone message
    '        Return
    '    ElseIf Microsoft.WindowsMobile.Status.SystemState.PhoneNoService Then
    '        ' display no service message
    '        Return
    '    Else

    '        Dim prompt As String
    '        If ValidatePhoneNumber(phoneNumber) Then
    '            prompt = "ThisNumberPrompt"
    '        Else
    '            prompt = "ParseProblem"
    '        End If

    '        phoneNumber = Terminology.AskQuestion(Nothing, "PhoneCall", prompt, ParseAlphaPhoneNumber(phoneNumber), False, 30)

    '        If Not String.IsNullOrEmpty(phoneNumber) Then
    '            Dim msg As New PocketOutlook.SmsMessage

    '        End If


    '    End If
    'End Sub

    'Public Sub SendSms(ByVal textbox As TextBox)

    'End Sub

    Public Function HavePhone() As Boolean
        If Not Environment.OSVersion.Platform = PlatformID.WinCE Then
            Return False
        End If

#If DEBUG Then
        Return versionOk
#Else
        Return versionOk AndAlso Microsoft.WindowsMobile.Status.SystemState.PhoneRadioPresent 
#End If
    End Function

    Public Function CanPhone() As Boolean
        If Not Environment.OSVersion.Platform = PlatformID.WinCE Then
            Return False
        End If

#If DEBUG Then
        Return versionOk
#Else
        Return versionOk _
            AndAlso Microsoft.WindowsMobile.Status.SystemState.PhoneRadioPresent _
            AndAlso Not Microsoft.WindowsMobile.Status.SystemState.PhoneNoService
#End If
    End Function

    Public Function CanPhone(ByVal phoneNumber As String) As Boolean
        Return CanPhone() AndAlso ValidatePhoneNumber(phoneNumber)
    End Function

    Public Function CanPhone(ByVal textBox As TextBox) As Boolean
        If Not Environment.OSVersion.Platform = PlatformID.WinCE Then
            Return False
        End If
        If textBox Is Nothing Then Return False
        If Environment.OSVersion.Version < minimumVersion Then Return False
        If Not Microsoft.WindowsMobile.Status.SystemState.PhoneRadioPresent _
            OrElse Microsoft.WindowsMobile.Status.SystemState.PhoneNoService Then
            Return False
        Else
            If String.IsNullOrEmpty(textBox.SelectedText) Then Return False
            Return ValidatePhoneNumber(textBox.SelectedText.Trim)
        End If
    End Function

#Region "Phone Number Validation"
    Const RES_PhoneRegex As String = "((?<idd>(\+\d{1,3}))(-| )?)?(?<areaprefix>\(\d\)?)?(-| )?(?<area>(\d{1,5})|(\(?\d{2,6}\)?))(-| )?(?<number>((\d|\w|-| )?)+)((x|ext) ?(?<ext>\d{1,5})){0,1}"
    Private regex As New System.Text.RegularExpressions.Regex(RES_PhoneRegex)

    Public Function ValidatePhoneNumber(ByVal phoneNumber As String) As Boolean
        If String.IsNullOrEmpty(phoneNumber) Then Return False
        Return regex.IsMatch(phoneNumber)
    End Function

    Public Function ParsePhoneNumber(ByVal phoneNumber As String) As String
        Dim result As New System.Text.StringBuilder(phoneNumber.Length)

        For Each thisChar As Char In phoneNumber
            If thisChar.InSet("1234567890+()*#") Then
                result.Append(thisChar)
            Else
                thisChar = Char.ToLowerInvariant(thisChar)
                Select Case thisChar
                    Case "a"c, "b"c, "c"c
                        result.Append("2"c)
                    Case "d"c, "e"c, "f"c
                        result.Append("3"c)
                    Case "g"c, "h"c, "i"c
                        result.Append("4"c)
                    Case "j"c, "k"c, "l"c
                        result.Append("5"c)
                    Case "m"c, "n"c, "o"c
                        result.Append("6"c)
                    Case "p"c, "q"c, "r"c, "s"c
                        result.Append("7"c)
                    Case "t"c, "u"c, "v"c
                        result.Append("8"c)
                    Case "w"c, "x"c, "y"c, "z"c
                        result.Append("9"c)
                End Select
            End If
        Next

        Return result.ToString()
    End Function
#End Region

End Module

#End If