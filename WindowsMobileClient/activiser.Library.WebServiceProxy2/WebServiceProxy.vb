Imports System.Reflection

Namespace WebService
    Partial Public Class activiser

        Public Sub New(ByVal url As String)
            Me.Url = url
            Me.UserAgent = String.Format(My.Resources.UserAgentString, AssemblyVersion.ToString(4))
            'Me.EnableDecompression = True
        End Sub

        Private _assemblyName As AssemblyName

        Private ReadOnly Property AssemblyVersion() As Version
            Get
                If _assemblyName Is Nothing Then
                    Dim a As Assembly = Assembly.GetExecutingAssembly()
                    _assemblyName = a.GetName()
                End If
                Return _assemblyName.Version
            End Get
        End Property
    End Class
End Namespace
