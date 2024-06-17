Module WebServiceSupport
    Public Function RemoteCertificateValidationCallback( _
        ByVal sender As Object, _
        ByVal certificate As Security.Cryptography.X509Certificates.X509Certificate, _
        ByVal chain As Security.Cryptography.X509Certificates.X509Chain, _
        ByVal sslPolicyErrors As Net.Security.SslPolicyErrors _
    ) As Boolean
        If sslPolicyErrors = Net.Security.SslPolicyErrors.None Then
            Return True
        ElseIf sslPolicyErrors = Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch Then
            Return My.Settings.IgnoreServerCertificateNameMismatch
        Else ' to do, make this cleverer.
            Return My.Settings.IgnoreServerCertificateErrors
            ' Return False
        End If
        'Return True
    End Function
End Module
