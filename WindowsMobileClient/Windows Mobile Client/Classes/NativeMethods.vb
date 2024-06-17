Imports System.Text
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports activiser
Imports activiser.Library
Imports activiser.Library.PlatformInfo

Module NativeMethods
    Private Const MODULENAME As String = "NativeMethods"

    Public Declare Function WCE_PlaySound Lib "CoreDll.dll" Alias "PlaySound" (ByVal szSound As String, ByVal hMod As IntPtr, ByVal flags As Integer) As Integer
    'Public Declare Function WCE_PlaySoundBytes Lib "CoreDll.dll" Alias "PlaySound" (ByVal szSound() As Byte, ByVal hMod As IntPtr, ByVal flags As Integer) As Integer
    Private Declare Function KernelIoControl Lib "CoreDll.dll" (ByVal dwIoControlCode As Int32, ByVal lpInBuf As IntPtr, ByVal nInBufSize As Int32, ByVal lpOutBuf() As Byte, ByVal nOutBufSize As Int32, ByRef lpBytesReturned As Int32) As <MarshalAs(UnmanagedType.U1)> Boolean
    Private Declare Function GetDeviceUniqueID Lib "CoreDll.dll" (ByVal pbApplicationData() As Byte, ByVal cbApplicationData As Integer, ByVal dwDeviceIDVersion As Integer, ByVal pbDeviceIDOutput() As Byte, ByRef pcbDeviceIDOutput As Int32) As Integer

    Public Const DesignDpi As Int32 = 96
    Public MyDpiX As Int32 = GetDpiX()
    'Public MyDpiY As Int32 = GetDpiY()
    Public RunTimeScale As Double = MyDpiX / DesignDpi

    Private md5 As Security.Cryptography.MD5 = md5.Create

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Private Function GetDpiX() As Int32
        Try
            Dim deviceHandle As IntPtr = GetDC(IntPtr.Zero)
            Return GetDeviceCaps(deviceHandle, LOGPIXELSX)

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Function

    '<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    'Private Function GetDpiY() As Int32
    '    Try
    '        Dim deviceHandle As IntPtr = GetDC(IntPtr.Zero)
    '        Return GetDeviceCaps(deviceHandle, LOGPIXELSY)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString())
    '    End Try
    'End Function

    Private Const LOGPIXELSX As Int32 = 88
    Private Const LOGPIXELSY As Int32 = 90
    Private Declare Function GetDC Lib "CoreDll.dll" Alias "GetDC" (ByVal hwnd As IntPtr) As IntPtr
    Private Declare Function GetDeviceCaps Lib "CoreDll.dll" Alias "GetDeviceCaps" (ByVal hdc As IntPtr, ByVal index As Int32) As Int32

    'TODO: Add version information to deviceID
    Private _deviceId As String

    Public Function GetDeviceID() As String
        Const METHODNAME As String = "GetDeviceID"
        If _deviceId Is Nothing Then
            _deviceId = My.Resources.UnsupportedDeviceID
            Select Case PlatformInfo.Info.Platform
                Case Platform.WM5PocketPC, Platform.WM5Smartphone
                    _deviceId = GetWM5DeviceId()
                Case Platform.PocketPC2000, Platform.PocketPC2002, Platform.PocketPC2003, Platform.Smartphone2002, Platform.Smartphone2003
                    _deviceId = GetWM4DeviceID()
                Case Else
                    Try
                        _deviceId = GetWM5DeviceId()
                    Catch ex As Exception
                        Try
                            _deviceId = GetWM4DeviceID()
                        Catch ex2 As Exception
                            Debug.WriteLine(String.Format(WithoutCulture, "{0}:{1}, {2}", MODULENAME, METHODNAME, ex.Message))
                            LogError(MODULENAME, METHODNAME, ex, True, RES_UnsupportedPlatform)
                        End Try
                    End Try
            End Select
        End If
        Return _deviceId
    End Function

    Private _deviceIdFormatted As String
    Private _formattedDeviceId As String

	Public Function GetFormattedDeviceID() As String
		Return GetFormattedDeviceID(_deviceId)
	End Function

    Public Function GetFormattedDeviceID(ByVal deviceId As String) As String
        If _deviceIdFormatted Is Nothing OrElse deviceId <> _formattedDeviceId Then
            If deviceId Is Nothing Then Throw New ArgumentNullException("deviceId")
            If String.IsNullOrEmpty(deviceId) Then Return Terminology.GetString(My.Resources.SharedMessagesKey, RES_UnsupportedDeviceId)
            Dim sb As New StringBuilder(100)
            If deviceId.Length > 6 Then
                sb.Append(deviceId.Substring(0, 6))
                If deviceId.Length >= 12 Then
                    sb.Append(My.Resources.DeviceIdSeparator)
                    sb.Append(deviceId.Substring(6, 6))
                    If deviceId.Length > 12 Then
                        Dim i As Integer
                        For i = 12 To deviceId.Length - 1 Step 5
                            Dim part As String
                            If deviceId.Length - i >= 5 Then
                                part = deviceId.Substring(i, 5)
                            Else
                                part = deviceId.Substring(i)
                            End If
                            If Not String.IsNullOrEmpty(part) Then
                                sb.Append(My.Resources.DeviceIdSeparator)
                                sb.Append(part)
                            End If
                        Next
                    End If
                End If
            Else
                sb.Append(deviceId)
            End If
            _formattedDeviceId = deviceId
            _deviceIdFormatted = sb.ToString()
        End If
        Return _deviceIdFormatted
    End Function

 Public Function GetWM4DeviceID() As String
        Return GetWM4DeviceID(gApplicationGuid)
    End Function

    Public Function GetWM4DeviceID(ByVal hashKey As Guid) As String
        Dim RawDeviceId() As Byte = md5.ComputeHash(GetRawWM4DeviceID()) ' Crypto.Encrypt(hashKey, GetRawWM4DeviceID()) ' hash resultant ID against activiser GUID, for extra security (simulate WM5 technique).

        Dim resultDeviceId(19) As Byte
        resultDeviceId(0) = CByte(Environment.OSVersion.Version.Major And &HFF)
        resultDeviceId(1) = CByte(Environment.OSVersion.Version.Minor And &HFF)
        resultDeviceId(2) = CByte(Environment.OSVersion.Version.Build And &HFF)
        resultDeviceId(3) = CByte(Environment.OSVersion.Version.Revision And &HFF)
        Array.Copy(XorBytes(RawDeviceId, hashKey.ToByteArray()), 0, resultDeviceId, 4, 16)
        'RawDeviceId = XorGuid(New Guid(RawDeviceId), hashKey).ToByteArray
        'Dim resultArray(19) As Byte

        Dim encodedDeviceId As String = Base32.Encode(resultDeviceId, My.Resources.DeviceIdBase32Table.ToCharArray())
        Return encodedDeviceId

        'If RawDeviceId.Length >= 20 Then
        '    Array.Copy(RawDeviceId, resultArray, 20)
        'Else
        '    Array.Copy(RawDeviceId, resultArray, RawDeviceId.Length)
        'End If
        'Dim sb As New StringBuilder(resultArray.Length * 2)
        'For Each b As Byte In resultArray
        '    sb.Append(String.Format(WithoutCulture, "{0:x2}", b))
        'Next
        'Return sb.ToString()
    End Function

    Private Function GetRawWM4DeviceID() As Byte()
        'Dim a As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly
        'Dim v As String = a.GetName.Version.ToString

        Const ERROR_NOT_SUPPORTED As Int32 = &H32
        Const ERROR_INSUFFICIENT_BUFFER As Int32 = &H7A

        Dim METHOD_BUFFERED As Int32 = 0
        Dim FILE_ANY_ACCESS As Int32 = 0
        Dim FILE_DEVICE_HAL As Int32 = &H101
        Dim IOCTL_HAL_GET_DEVICEID As Int32 = &H1010054 ' (&H10000 * FILE_DEVICE_HAL) Or (&H4000 * FILE_ANY_ACCESS) Or (&H4 * 21) Or METHOD_BUFFERED

        ' Initialize the output buffer to the size of a Win32 DEVICE_ID structure

        Dim buffer(19) As Byte
        Dim bytesReturned As Int32 = 0
        Dim done As Boolean = False

        ' Set DEVICEID.dwSize to size of buffer. Some platforms look at this field rather than the nOutBufSize param of KernelIoControl when determining if the buffer is large enough.
        BitConverter.GetBytes(buffer.Length).CopyTo(buffer, 0)

        Try
            done = KernelIoControl(IOCTL_HAL_GET_DEVICEID, IntPtr.Zero, 0, buffer, buffer.Length, bytesReturned)
        Catch ex As Exception
            Throw New NotSupportedException("Unable to get device ID", ex)
        End Try

        Dim win32Error As Integer = Marshal.GetLastWin32Error()
        ' Loop until the device ID is retrieved or an error occurs
        If Not done Then
            If win32Error = ERROR_INSUFFICIENT_BUFFER Then
                ' The buffer is not big enough for the data. The required size is in the first 4 bytes of 
                ' the output buffer (DEVICE_ID.dwSize).
                buffer = New Byte(BitConverter.ToInt32(buffer, 0)) {}
                ' Set DEVICEID.dwSize to size of buffer. Some platforms look at this field 
                ' rather than the nOutBufSize param of KernelIoControl when determining if the buffer is large enough.
                BitConverter.GetBytes(buffer.Length).CopyTo(buffer, 0)
                done = KernelIoControl(IOCTL_HAL_GET_DEVICEID, IntPtr.Zero, 0, buffer, buffer.Length, bytesReturned)
            Else
                ' try exact-length of 16 bytes
                buffer = Guid.Empty.ToByteArray()
                BitConverter.GetBytes(buffer.Length).CopyTo(buffer, 0)
                done = KernelIoControl(IOCTL_HAL_GET_DEVICEID, IntPtr.Zero, 0, buffer, buffer.Length, bytesReturned)

                If done Then
                    Return buffer
                End If
            End If

            If Not done Then
                If win32Error = ERROR_NOT_SUPPORTED Then
                    Throw New NotSupportedException("Unable to get device ID", New Win32Exception(win32Error))
                Else
                    Throw New Win32Exception(win32Error, "Unknown error")
                End If
            End If
        End If

        Dim dwPresetIDOffset As Int32 = BitConverter.ToInt32(buffer, &H4) ' DEVICE_ID.dwPresetIDOffset
        Dim dwPresetIDSize As Int32 = BitConverter.ToInt32(buffer, &H8) ' DEVICE_ID.dwPresetIDSize
        Dim dwPlatformIDOffset As Int32 = BitConverter.ToInt32(buffer, &HC) ' DEVICE_ID.dwPlatformIDOffset
        Dim dwPlatformIDSize As Int32 = BitConverter.ToInt32(buffer, &H10) ' DEVICE_ID.dwPlatformIDBytes

        Dim resultList As New Generic.List(Of Byte)
        Dim result(dwPresetIDSize + dwPlatformIDSize - 1) As Byte
        Array.Copy(buffer, dwPresetIDOffset, result, 0, dwPresetIDSize)
        Array.Copy(buffer, dwPlatformIDOffset, result, dwPresetIDSize, dwPlatformIDSize)

        Return result
    End Function


    Public Function GetWM5DeviceId(ByVal hashKey As Guid) As String
        Dim buffer(19) As Byte
        Dim buflen As Integer = buffer.Length
        Dim hashBytes() As Byte = hashKey.ToByteArray
        Dim hashLen As Integer = hashBytes.Length

        If GetDeviceUniqueID(hashBytes, hashLen, 1, buffer, buflen) = 0 Then
            Dim encodedDeviceId As String = Base32.Encode(buffer, My.Resources.DeviceIdBase32Table.ToCharArray())
            Return encodedDeviceId
            'Dim sb As New StringBuilder()
            'For i As Integer = 0 To 19
            '    sb.Append(String.Format(CultureInfo.InvariantCulture, "{0:x2}", buffer(i)))
            'Next

            'Return sb.ToString()
        Else
            Return Terminology.GetString(My.Resources.SharedMessagesKey, RES_Unknown)
        End If
    End Function

    Public Function GetWM5DeviceId() As String
        Return GetWM5DeviceId(gApplicationGuid)
    End Function

#Region "SystemMetrics"
    Friend Const SM_CXVSCROLL As Integer = 20

    Friend Declare Function GetSystemMetrics Lib "CoreDll.dll" (ByVal nIndex As Int32) As Int32
#End Region


#Region "Memory Status"
    Private Declare Sub GlobalMemoryStatus Lib "CoreDll.dll" (ByRef _MemoryStatus As _MEMORYSTATUS)

    Private Structure _MEMORYSTATUS
        Public dwLength As Int32
        Public dwMemoryLoad As Int32
        Public dwTotalPhys As Int32
        Public dwAvailPhys As Int32
        Public dwTotalPageFile As Int32
        Public dwAvailPageFile As Int32
        Public dwTotalVirtual As Int32
        Public dwAvailVirtual As Int32
    End Structure

    Public Class MemoryStatus
        Public ReadOnly Property TotalPhysicalMemory() As Int32
            Get
                Return _memoryStatus.dwTotalPhys
            End Get
        End Property

        Public ReadOnly Property AvailablePhysicalMemory() As Int32
            Get
                Return _memoryStatus.dwAvailPhys
            End Get
        End Property

        Private _memoryStatus As _MEMORYSTATUS

        Sub New()
            Refresh()
        End Sub

        Public Sub Refresh()
            _memoryStatus.dwLength = System.Runtime.InteropServices.Marshal.SizeOf(_memoryStatus)
            Try
                GlobalMemoryStatus(_memoryStatus)
            Catch ex As Exception

            End Try
        End Sub
    End Class
#End Region

#Region "System Time"

    Public Structure SystemTime
        Public Year As UInt16
        Public Month As UInt16
        Public DayOfWeek As UInt16
        Public Day As UInt16
        Public Hour As UInt16
        Public Minute As UInt16
        Public Second As UInt16
        Public Millisecond As UInt16

        Sub New(ByVal currentTime As DateTime)
            ' since SetSystemTime expects universal time
            If currentTime.Kind = DateTimeKind.Local Then
                currentTime = currentTime.ToUniversalTime
            End If
            Hour = CUShort(currentTime.Hour)
            Minute = CUShort(currentTime.Minute)
            Second = CUShort(currentTime.Second)

            Year = CUShort(currentTime.Year)
            Month = CUShort(currentTime.Month)
            Day = CUShort(currentTime.Day)
            Millisecond = CUShort(currentTime.Millisecond)

            DayOfWeek = CUShort(currentTime.DayOfWeek)
        End Sub
    End Structure

    <DllImport("coredll.dll", SetLastError:=True)> _
    Public Function SetSystemTime(ByRef lpSystemTime As SystemTime) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    Public Sub SetSystemTime(ByVal Value As DateTime)
        Dim newTime As New SystemTime(Value)
        If Not SetSystemTime(newTime) Then
            Dim result As Integer = Marshal.GetLastWin32Error()
            Dim ex As New System.ComponentModel.Win32Exception(result) ' , FormatMessage(result))
            Debug.WriteLine(String.Format("Error setting system time : {0}", ex.Message))
            Throw ex
        End If
    End Sub

#End Region

End Module
