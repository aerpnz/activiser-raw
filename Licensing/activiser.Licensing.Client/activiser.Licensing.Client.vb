Imports System.Globalization

Public Class LicenseInfo
    Private _expiryDate As Date
    Private _version As Integer
    Private _users As Integer
    Private _modules As Integer

    Private _valid As Boolean

    Private Shared ReadOnly _lookupTable As Char() = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray()

    Sub New(ByVal productKey As String, ByVal clientName As String, ByVal assemblyKey As Guid)
        _valid = False

        Dim licenseCode As Byte() = DecodeFixed(productKey)
        Dim hashedClient As Byte() = HashClientName(clientName)
        If hashedClient Is Nothing Then
            Throw New LicenseException("Error Parsing License Information")
        End If

        For i As Integer = 0 To UBound(licenseCode)
            licenseCode(i) = hashedClient(i) Xor licenseCode(i)
        Next

        Dim assemblyByteArray As Byte() = assemblyKey.ToByteArray()

        For i As Integer = 0 To UBound(licenseCode)
            licenseCode(i) = (Not assemblyByteArray(i)) Xor licenseCode(i)
        Next

        Dim checkByte As Byte = licenseCode(9)

        For i As Integer = 0 To 8
            licenseCode(i) = licenseCode(i) Xor checkByte
        Next

        Dim parityByte As Byte
        For i As Integer = 0 To 7
            parityByte = parityByte Xor licenseCode(i)
        Next

        If Not parityByte = licenseCode(8) Then
            Exit Sub
        End If

        licenseCode(9) = 0
        If Not CalculateChecksum(licenseCode) = checkByte Then
            Throw New LicenseException("Error Parsing License Information")
        End If

        Dim expiryDay As Byte = licenseCode(0)
        Dim expiryMonth As Byte = licenseCode(1)
        Dim expiryYear As Byte = licenseCode(2)
        Try
            ExpiryDate = New Date(2005 + expiryYear, expiryMonth, expiryDay)
        Catch ex As ArgumentOutOfRangeException
            Throw New LicenseException("Error Parsing License Information", ex)
        End Try

        Version = licenseCode(3)

        Dim licenseClientVersion As Byte = licenseCode(6)
        If licenseClientVersion = 0 Then
            Users = licenseCode(4)
        Else
            Users = DecodeUsers(licenseCode(4))
        End If
        Modules = licenseCode(5)

        _valid = Users > 0 AndAlso Version > 0 AndAlso Modules <> 0
        If Not _valid Then
            Throw New LicenseException("Error Parsing License Information")
        End If
    End Sub

    Public ReadOnly Property Valid() As Boolean
        Get
            Return _valid
        End Get
    End Property

    Public Property ExpiryDate() As Date
        Get
            Return _expiryDate
        End Get
        Private Set(ByVal value As Date)
            _expiryDate = value
        End Set
    End Property

    Public Property Version() As Integer
        Get
            Return _version
        End Get
        Private Set(ByVal value As Integer)
            _version = value
        End Set
    End Property

    Public Property Users() As Integer
        Get
            Return _users
        End Get
        Private Set(ByVal value As Integer)
            _users = value
        End Set
    End Property

    Public Property Modules() As Integer
        Get
            Return _modules
        End Get
        Private Set(ByVal value As Integer)
            _modules = value
        End Set
    End Property

    Private Shared Function CalculateChecksum(ByVal Array() As Byte) As Byte
        Dim hexString As String
        Dim runningTotal As Long
        Dim operation As Integer = 0
        Dim i As Integer

        Try
            If Array Is Nothing OrElse Array.Length = 0 Then Throw New ArgumentNullException("Array")

            runningTotal = 0
            hexString = ""

            For i = 0 To UBound(Array)
                hexString &= Hex(Array(i) * CInt((2 ^ i)))
            Next

            hexString = "g" & hexString

            For Each C As Char In hexString.ToCharArray
                If operation >= 3 Then operation = 0

                Select Case operation
                    Case 0
                        runningTotal = runningTotal + AscW(C)
                    Case 1
                        runningTotal = runningTotal * AscW(C)
                        runningTotal = runningTotal Mod Int32.MaxValue
                    Case 2
                        runningTotal = runningTotal - AscW(C)
                End Select

                operation += 1

            Next

            Return CByte(runningTotal Mod 251)

        Catch ex As Exception
            MessageBox.Show(String.Format(CultureInfo.CurrentCulture, "{0}{1}{1}{2}{1}{1}Location:{3}", ex.Message, vbCrLf, ex.GetType.FullName, ex.StackTrace), _
                String.Format(CultureInfo.CurrentCulture, "{0}: {1}", ex.Source, ex.Message), _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Error)
            Throw
        Finally
            'Clean up Code

        End Try
    End Function

    Private Shared Function HashClientName(ByVal value As String) As Byte()
        Dim result(9) As Byte
        Dim length As Integer
        Dim FCASCII As Integer
        Dim LCASCII As Integer
        Dim ASCIISUM As Long
        Dim XCASCII As Integer
        Dim HexSum As Long
        Dim SumArray As Integer
        Dim operation As Byte

        Try
            If value Is Nothing OrElse value = "" Then
                Return result
            End If

            length = Len(value)
            FCASCII = AscW(Microsoft.VisualBasic.Left(value, 1))
            LCASCII = AscW(Microsoft.VisualBasic.Right(value, 1))

            For Each C As Char In value.ToCharArray

                If operation >= 3 Then operation = 0

                Select Case operation
                    Case 0
                        ASCIISUM = ASCIISUM + AscW(C)
                    Case 1
                        ASCIISUM = ASCIISUM * AscW(C)
                        ASCIISUM = ASCIISUM Mod Int32.MaxValue
                    Case 2
                        ASCIISUM = ASCIISUM - AscW(C)
                End Select

                operation += CByte(1)
            Next

            For Each C As Char In Hex(ASCIISUM).ToCharArray
                HexSum = HexSum + AscW(C)
            Next

            If length > 5 Then
                XCASCII = AscW(Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(value, 3), 1))
            ElseIf length > 3 Then
                XCASCII = LCASCII
            Else
                XCASCII = FCASCII
            End If

            result(0) = CByte((length + HexSum - FCASCII) Mod 256)
            result(1) = CByte((LCASCII * FCASCII + (ASCIISUM - FCASCII)) Mod 256)
            result(2) = CByte((LCASCII * ASCIISUM + (length Xor HexSum)) Mod 256)
            result(3) = CByte((ASCIISUM + length) Mod 256)
            result(4) = CByte((HexSum Xor ASCIISUM) Mod 256)
            result(5) = CByte(XCASCII Mod 256)
            result(6) = CByte((length + ASCIISUM + XCASCII) Mod 256)
            result(7) = CByte(HexSum Mod 256)
            result(8) = CByte((length * 217 + ASCIISUM) Mod 256)
            SumArray = (CInt(result(0)) + CInt(result(1)) + _
            CInt(result(2)) + CInt(result(3)) + CInt(result(4)) _
            + CInt(result(5)) + CInt(result(6)) + CInt(result(7)) + _
            CInt(result(8)))
            result(9) = CByte(SumArray Mod 256)


        Catch ex As Exception
            'Catch all exceptions
            If System.Environment.UserInteractive Then
                MessageBox.Show(String.Format(CultureInfo.CurrentCulture, "{0}{1}{1}{2}{1}{1}Location:{3}", _
                        ex.Message, _
                                    vbCrLf, _
                                    ex.GetType.FullName, _
                                    ex.StackTrace), _
                    String.Format(CultureInfo.CurrentCulture, "{0}: {1}", ex.Source, ex.Message), _
                    MessageBoxButtons.OK, _
                    MessageBoxIcon.Error)
                Return Nothing
            Else
                Throw New LicenseException("Error Parsing License Information", ex)
            End If
        Finally
            'Clean up Code
        End Try
        Return result
    End Function

    ' base32 decode function.
    Private Shared Function DecodeFixed(ByVal value As String) As Byte()
        value = value.Replace("-", String.Empty)
        If value.Length <> 16 Then
            Return Nothing
        End If

        Dim result(9) As Byte
        Dim rb(7) As Byte
        Dim lb As Long
        Dim si, ri, x, i As Integer

        For x = 0 To 1
            lb = 0
            For i = 0 To 7
                For t As Integer = 0 To 31
                    If _lookupTable(t) = value.Chars(si + i) Then
                        lb = lb Or (CType(t, Long) << (i * 5))
                        Exit For
                    End If
                Next
            Next

            rb = System.BitConverter.GetBytes(lb)

            For i = 0 To 4
                result(ri + i) = rb(i)
            Next
            ri = 5
            si = 8
        Next
        Return result
    End Function

    Private Shared Function DecodeUsers(ByVal value As Byte) As Integer
        Dim bases() As Integer = {5, 65, 320, 1550}
        Dim units() As Integer = {1, 5, 20, 50}
        Dim result As Integer

        Dim group As Integer = value >> 6
        Dim key As Integer = value And &H3F

        result = bases(group) + (key * units(group))
        Return result
    End Function

End Class

