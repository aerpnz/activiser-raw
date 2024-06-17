Public Class SoundPlayer

    Private _fileName As String

    Public Sub New(ByVal fileName As String)
        _fileName = fileName
    End Sub

    Public Sub Play()
        ' If a file name has been registered, call WCE_PlaySound, 
        ' otherwise call WCE_PlaySoundBytes.
        If Not String.IsNullOrEmpty(_fileName) AndAlso System.IO.File.Exists(_fileName) Then
            Dim dontCare As Integer = WCE_PlaySound(_fileName, IntPtr.Zero, (Flags.SND_ASYNC Or Flags.SND_FILENAME Or Flags.SND_NOSTOP Or Flags.SND_NOWAIT))
            If dontCare <> 0 Then
                Debug.WriteLine("Error playing sound.")
            End If
        End If
    End Sub

#Region "SoundFlags"
    Private Enum Flags
        SND_SYNC = &H0 ' play synchronously (default) 
        SND_ASYNC = &H1 ' play asynchronously 
        SND_NODEFAULT = &H2 ' silence (!default) if sound not found 
        SND_MEMORY = &H4 ' pszSound points to a memory file 
        SND_LOOP = &H8 ' loop the sound until next sndPlaySound 
        SND_NOSTOP = &H10 ' don't stop any currently playing sound 
        SND_NOWAIT = &H2000 ' don't wait if the driver is busy 
        SND_ALIAS = &H10000 ' name is a registry alias 
        SND_ALIAS_ID = &H110000 ' alias is a predefined ID 
        SND_FILENAME = &H20000 ' name is file name 
        SND_RESOURCE = &H40004 ' name is resource name or atom 
    End Enum
#End Region
End Class
