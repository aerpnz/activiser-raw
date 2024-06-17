Imports System.Net
Imports System.Security.Cryptography.X509Certificates

Public Enum CertificateProblemCode As Integer
    None = 0
    Expired = &H800B0101    '' A required certificate is not within its validity period.  
    ValidityPeriodNesting = &H800B0102 '' The validity periods of the certification chain do not nest correctly.
    Role = &H800B0103 '' A certificate that can only be used as an end-entity is being used as a CA or visa versa.
    PathLengthConstraint = &H800B0104 '' A path length constraint in the certification chain has been violated.
    Critical = &H800B0105 '' An extension of unknown type that is labeled 'critical' is present in a certificate.
    Purpose = &H800B0106 '' A certificate is being used for a purpose other than that for which it is permitted.
    IssuerChaining = &H800B0107 '' A parent of a given certificate in fact did not issue that child certificate.
    Malformed = &H800B0108 ' A certificate is missing or has an empty value for an important field, such as a subject or issuer name.
    UntrustedRoot = &H800B0109 '' A certification chain processed correctly but terminated in a root certificate that is not trusted by the trust provider.
    Chaining = &H800B010A '' A chain of certs didn't chain as they should in a certain application of chaining.
    Revoked = &H800B010C '' A certificate was explicitly revoked by its issuer.
    UntrustedTestRoot = &H800B010D '' The root certificate is a testing certificate and the policy settings disallow test certificates.
    RevocationFailure = &H800B010E '' The revocation process could not continue - the certificate(s) could not be checked.
    CNNameMismatch = &H800B010F '' The certificate's CN name does not match the passed value.
    WrongUsage = &H800B0110 '' The certificate is not valid for the requested usage.
    UntrustedCA = &H800B0112 '' The certificate is not issued by a trusted CA.
End Enum


Public Class CertificatePolicy
    Implements ICertificatePolicy
    Const RES_IgnoreServerCertificateErrors As String = "IgnoreServerCertificateErrors"
    Const RES_IgnoreServerCertificateNameMismatch As String = "IgnoreServerCertificateNameMismatch"
    Const RES_Problem As String = "certificateProblem"

    Public Function CheckValidationResult( _
        ByVal srvPoint As ServicePoint, _
        ByVal certificate As X509Certificate, _
        ByVal request As WebRequest, _
        ByVal certificateProblem As Integer) _
       As Boolean Implements ICertificatePolicy.CheckValidationResult

        If certificateProblem = 0 Then Return True

        If AppConfig.GetSetting(RES_IgnoreServerCertificateErrors, False) Then Return True

        Dim certProblem As CertificateProblemCode
        Try
            certProblem = CType(certificateProblem, CertificateProblemCode)
            If certProblem = CertificateProblemCode.CNNameMismatch AndAlso AppConfig.GetSetting(RES_IgnoreServerCertificateNameMismatch, True) Then Return True
            Return False
        Catch ex As InvalidCastException
            Throw New ArgumentOutOfRangeException(RES_Problem)
        End Try

    End Function

    'Private Function GetProblemMessage(ByVal Problem As CertificateProblem) As String
    '    Dim ProblemMessage As String = ""
    '    Dim problemList As New CertificateProblem()
    '    Dim ProblemCodeName As String = Problem.ToString
    '    If Not (ProblemCodeName Is Nothing) Then
    '        ProblemMessage = ProblemMessage & "-Certificateproblem:" & ProblemCodeName
    '    Else
    '        ProblemMessage = "Unknown Certificate Problem"
    '    End If
    '    Return ProblemMessage
    'End Function
End Class
