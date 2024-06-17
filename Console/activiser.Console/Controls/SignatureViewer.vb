Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.ComponentModel

Public Class SignatureViewer
    Private Shared algorithmChangeDate As New Date(2005, 1, 26)
    Private _value As String = ""
    'Private _signatureString As String = ""
    'Private _signatureDateString As String = ""
    Private _jobUid As System.Guid = Guid.Empty
    Private _signatureBitmap As System.Drawing.Bitmap 'Bitmap image to save the signature into.
    Private _graphics As System.Drawing.Graphics

    'Public ReadOnly Property SignatureDate() As String
    '    Get
    '        Return _signatureDateString
    '    End Get
    'End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _signature.Size = Me.Size
        Me.SizeMode = PictureBoxSizeMode.Zoom
        _signature.Font = New Font(Drawing.FontFamily.GenericSansSerif, 8, FontStyle.Regular, GraphicsUnit.Point)
        _signature.BackColor = SystemColors.Info
        _signature.BorderColor = SystemColors.ControlText
        _signature.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
    End Sub


    Public Sub SetValueAndJobGuid(ByVal value As String, ByVal jobUid As Guid)
        Me._value = value
        Me._jobUid = jobUid
        _needLoad = True
        LoadSignature()
    End Sub

    Private _needLoad As Boolean = True
    <Category("Data"), Bindable(True)> _
    Public Property Value() As String
        Get
            Return _value
        End Get
        Set(ByVal value As String)
            If Not String.Equals(value, _value) Then
                _value = value
                _needLoad = True
            End If
        End Set
    End Property

    <Category("Data"), Bindable(True)> _
    Public Property JobUid() As System.Guid
        Get
            Return _jobUid
        End Get
        Set(ByVal value As System.Guid)
            If Not Guid.Equals(_jobUID, value) Then
                _jobUID = value
                _needLoad = True
            End If
        End Set
    End Property

    Private _signature As New activiser.Library.Forms.Signature()

    Private Sub DrawMessage(ByVal message As String)
        If Not String.IsNullOrEmpty(message) Then
            Dim textSize As Size = _graphics.MeasureString(message, MyBase.Font).ToSize
            Dim textWidth As Integer = CInt(textSize.Width * 1.1)
            Dim textHeight As Integer = textSize.Height
            Dim topOffset As Integer = ((textHeight * 3) \ 2)
            Dim leftOffset As Integer = topOffset
            Dim textRect As New Rectangle(leftOffset, topOffset, textWidth, textHeight)
            Dim textBrush As New SolidBrush(SystemColors.InfoText)
            _graphics.DrawString(message, MyBase.Font, textBrush, textRect)
        End If
        Me.Image = _signatureBitmap
    End Sub

    Private Sub ClearBitmap()
        If _signatureBitmap IsNot Nothing Then
            _signatureBitmap.Dispose()
        End If
        If Me.Image IsNot Nothing Then
            Me.Image.Dispose()
        End If

        _signatureBitmap = New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height)
        _graphics = Graphics.FromImage(_signatureBitmap)
        _graphics.FillRectangle(New SolidBrush(Me.BackColor), New Rectangle(0, 0, _signatureBitmap.Width, _signatureBitmap.Height))
        _graphics.DrawRectangle(New System.Drawing.Pen(Me.BorderColor), New Rectangle(0, 0, _signatureBitmap.Width - 1, _signatureBitmap.Height - 1))
        Me.Image = _signatureBitmap
    End Sub

    Private Sub LoadSignature()
        Me.SuspendLayout()
        ClearBitmap()
        If _needLoad AndAlso Not String.IsNullOrEmpty(Me.Value) AndAlso Value.Length > 10 AndAlso Not Me.JobUid.Equals(Guid.Empty) Then
            Try
                If Value.StartsWith("#V", StringComparison.Ordinal) Then
                    _signature.SignatureString = DecryptSignature(Me._value)
                    _signature.Text = DateTime.SpecifyKind(_signature.Timestamp, DateTimeKind.Utc).ToLocalTime.ToString("G")
                    Me._signatureBitmap = _signature.ToBitmap
                    Me.Image = _signatureBitmap
                Else
                    Throw New FormatException("Value not recognised as an activiser signature")
                End If
            Catch ex As Exception
                Me.DrawMessage(ex.Message)
            End Try
        End If
        Me.Invalidate()
        _needLoad = False
        Me.ResumeLayout()
    End Sub

    <Category("Appearance")> _
    <DefaultValue("Black")> _
    Public Property BorderColor() As Color
        Get
            Return _borderColor
        End Get
        Set(ByVal value As Color)
            _borderColor = value
        End Set
    End Property
    Private _borderColor As Color

    'Windows mobile application guid
    Private gApplicationGuid As Guid = New Guid("F97EB4E7-F684-4402-895D-CFD15CE86498")

    Public Function DecryptSignature(ByVal s As String) As String
        If Not String.IsNullOrEmpty(s) Then

            Dim sigData As String = s.Substring(activiser.Library.Forms.Signature.VersionLength)
            Dim version As String = s.Substring(0, activiser.Library.Forms.Signature.VersionLength)
            Dim result As String
            If version = "#V040#" Then
                result = version & Decrypt(XorGuid(JobUid, gApplicationGuid), sigData)
            ElseIf version = "#V33#:" Then
                result = version & Crypto.DecryptString(sigData, XorGuid(JobUid, gApplicationGuid))
            Else
                'Panic!
                result = String.Empty
            End If

            Return result
        Else
            Return String.Empty
        End If
    End Function

End Class
