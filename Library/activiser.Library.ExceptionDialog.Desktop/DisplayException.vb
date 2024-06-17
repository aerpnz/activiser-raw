

Public Class DisplayException
    Private _Exception As Exception

    Private _Caption As String = "Exception Information"
    Private _Tip As String
    Private _ExtraInformation() As String

    Private _Icon As Icons = Icons.None
    Private _Text As String
    Private _WaveFilePath As String = String.Empty

    Private Const GLOBAL_MEMORY_TITLE As String = "Global Memory Status..."
    Private Const GLOBAL_MEMORY_EXTRA_TITLE As String = "Global Memory Extra Status..."


    Private Const FORMAT_LENGTH As Integer = 36
    Private Const FORMAT_NUMBER As String = "#,###,###,###,###"


    Private Const TOTAL_PHYSICAL As String = "Total Physical:"
    Private Const AVAILABLE_PHYSICAL As String = "Available Physical:"
    Private Const TOTAL_PAGE_FILE As String = "Total Page File:"
    Private Const AVAILABLE_PAGE_FILE As String = "Available Page File:"
    Private Const TOTAL_VIRTUAL As String = "Total Virtual:"
    Private Const AVAILABLE_VIRTUAL As String = "Available Virtual:"

    Public Property Exception() As Exception
        Get
            Return _Exception
        End Get
        Set(ByVal Value As Exception)
            _Exception = Value
        End Set
    End Property

    Public Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal Value As String)
            _Text = Value
        End Set
    End Property

    Public Property Caption() As String
        Get
            Caption = _Caption
        End Get
        Set(ByVal Value As String)
            _Caption = Value
        End Set
    End Property

    Public Property Icon() As Icons
        Get
            Return _Icon
        End Get
        Set(ByVal Value As Icons)
            _Icon = Value
        End Set
    End Property

    Public Property WaveFilePath() As String
        Get
            Return _WaveFilePath
        End Get
        Set(ByVal Value As String)
            _WaveFilePath = Value
        End Set
    End Property

    Public Property Tip() As String
        Get
            Tip = _Tip
        End Get
        Set(ByVal Value As String)
            _Tip = Value
        End Set
    End Property

    Public Sub New()
        'Default Constructor
    End Sub

    Public Sub New(ByVal ex As Exception)
        Me._Exception = ex
    End Sub

    Public Sub New(ByVal ex As Exception, ByVal text As String)
        Me._Exception = ex
        If Not text Is Nothing Then
            Me._Text = text
        End If
    End Sub

    Public Sub New(ByVal ex As Exception, ByVal icon As Icons)
        Me._Exception = ex
        Me._Icon = icon
    End Sub

    Public Sub New(ByVal ex As Exception, ByVal text As String, ByVal icon As Icons)
        Me._Exception = ex
        If Not text Is Nothing Then
            Me._Text = text
        End If
        Me._Icon = icon
    End Sub

    Public Sub New(ByVal ex As Exception, ByVal text As String, ByVal caption As String)
        Me._Exception = ex
        If Not text Is Nothing Then
            Me._Text = text
        End If
        If Not caption Is Nothing Then
            Me._Caption = caption
        End If
    End Sub

    Public Sub New(ByVal ex As Exception, ByVal icon As Icons, ByVal caption As String)
        Me._Exception = ex
        If Not caption Is Nothing Then
            Me._Caption = caption
        End If
        Me._Icon = icon
    End Sub

    Public Sub New(ByVal ex As Exception, ByVal text As String, ByVal caption As String, ByVal icon As Icons)
        Me._Exception = ex
        If Not text Is Nothing Then
            Me._Text = text
        End If
        If Not caption Is Nothing Then
            Me._Caption = caption
        End If
        Me._Icon = icon
    End Sub

    Public Sub New(ByVal ex As Exception, ByVal text As String, ByVal caption As String, ByVal icon As Icons, ByVal tip As String)
        Me._Exception = ex
        If Not text Is Nothing Then
            Me._Text = text
        End If
        If Not caption Is Nothing Then
            Me._Caption = caption
        End If
        Me._Icon = icon
        If Not tip Is Nothing Then
            Me._Tip = tip
        End If
    End Sub

    Public Sub Show()
        Try
            Using frmError As New ErrorForm(_Exception, True)
                'Dim theMemoryStatusExtra As New MemoryStatusExtendedClass
                Dim query As New System.Management.ManagementObjectSearcher("select * from win32_processor")

                Try
                    Dim ets As New ErrorTipDataSet

                    ets.ReadXml("errortip.xml")

                    For Each et As ErrorTipDataSet.ErrorTipRow In ets.ErrorTip
                        If et.Exception = _Exception.GetBaseException.GetType.Name Then
                            If _Tip = String.Empty Then
                                _Tip = et.Tip
                            Else
                                _Tip = _Tip & vbNewLine & vbNewLine & et.Tip
                            End If
                            Exit For
                        End If
                    Next
                    ets = Nothing

                Catch ex As Exception
                    'Do nothing - continue
                End Try

                frmError.Text = _Caption
                frmError.Tip = _Tip
                frmError.DisplayIcon = _Icon
                frmError.UserFriendlyMessage = _Text
                frmError.WaveFilePath = _WaveFilePath
                frmError.TotalPhysicalMemory = My.Computer.Info.TotalPhysicalMemory
                frmError.AvailablePhysicalMemory = My.Computer.Info.AvailablePhysicalMemory
                frmError.WindowsUserName = Environment.UserName
                frmError.OperatingSystemVersion = Environment.OSVersion.ToString
                frmError.MachineName = Environment.MachineName()
                frmError.DomainName = Environment.UserDomainName

                For Each cpu As System.Management.ManagementObject In query.Get()
                    frmError.ProcessorType = Trim(CStr(cpu("Name")))
                    Exit For
                Next

                'If _Opacity < 1.0 And _Opacity >= 0 Then
                '    frmError.AllowTransparency = True
                '    frmError.Opacity = _Opacity
                'End If
                frmError.TopMost = True

                frmError.ShowDialog()

            End Using
        Catch ex As Exception
            'Catch all exceptions
            MessageBox.Show(String.Format("{0}{1}{1}{2}{1}{1}StackTrace:{3}", ex.Message, vbCrLf, ex.GetType.FullName, ex.StackTrace), ex.Source & ": " & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'Clean up Code
        End Try
    End Sub

    Public Shared Sub Show(ByVal ex As Exception, ByVal icon As Icons)
        Dim de As New DisplayException(ex, icon)
        de.Show()
    End Sub

    Public Shared Sub Show(ByVal ex As Exception, ByVal text As String, ByVal icon As Icons)
        Dim de As New DisplayException(ex, text, icon)
        de.Show()
    End Sub

    Public Shared Sub Show(ByVal ex As Exception, ByVal text As String, ByVal icon As Icons, ByVal tip As String)
        Dim de As New DisplayException(ex, text, icon)
        de.Tip = tip
        de.Show()
    End Sub

    Public Shared Sub Show(ByVal ex As Exception, ByVal text As String, ByVal icon As Icons, ByVal tip As String, ByVal waveFilePath As String)
        Dim de As New DisplayException(ex, text, icon)
        de.Tip = tip
        de.WaveFilePath = waveFilePath
        de.Show()
    End Sub

    Public Shared Sub Show(ByVal ex As Exception, ByVal icon As Icons, ByVal caption As String)
        Dim de As New DisplayException(ex, icon, caption)
        de.Show()
    End Sub

    Public Shared Sub Show(ByVal ex As Exception, ByVal text As String, ByVal caption As String, ByVal icon As Icons)
        Dim de As New DisplayException(ex, text, caption, icon)
        de.Show()
    End Sub

    Public Shared Sub Show(ByVal ex As Exception, ByVal text As String, ByVal caption As String, ByVal icon As Icons, ByVal tip As String)
        Dim de As New DisplayException(ex, text, caption, icon, tip)
        de.Show()
    End Sub

End Class
