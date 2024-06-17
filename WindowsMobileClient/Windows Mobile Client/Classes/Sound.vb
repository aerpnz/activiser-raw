'======================================
'ICT Project
'Sound play class
'======================================

Imports System
Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports System.Threading
Imports System.IO

Public Class Sound


    Private _buffer() As Byte
    Private m_fileName As String



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


    ' Construct the Sound object to play sound data from the specified file.
    Public Sub New(ByVal fileName As String)
        m_fileName = fileName
    End Sub


    ' Construct the Sound object to play sound data from the specified stream.
    Public Sub New(ByVal stream As Stream)
        ' read the data from the stream
        If stream IsNot Nothing Then
            _buffer = New Byte(CInt(stream.Length)) {}
            stream.Read(_buffer, 0, CInt(stream.Length))
        End If
    End Sub 'New


    ' Play the sound
    Public Sub Play()
        ' If a file name has been registered, call WCE_PlaySound, 
        ' otherwise call WCE_PlaySoundBytes.
        Dim dontCare As Integer
        If Not (m_fileName Is Nothing) Then
            dontCare = WCE_PlaySound(m_fileName, IntPtr.Zero, (Flags.SND_ASYNC Or Flags.SND_FILENAME Or Flags.SND_NOSTOP Or Flags.SND_NOWAIT))
        Else
            dontCare = WCE_PlaySoundBytes(_buffer, IntPtr.Zero, (Flags.SND_ASYNC Or Flags.SND_MEMORY Or Flags.SND_NOSTOP Or Flags.SND_NOWAIT))
        End If
        If dontCare <> 0 Then
            Debug.WriteLine("Error playing sound.")
        End If
    End Sub

    Public Shared Sub Play(ByVal fileName As String)
        Dim s As New Sound(fileName)
        s.Play()
    End Sub

End Class
