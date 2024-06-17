<Serializable()> _
Public Class RequestSaveFailureException
    Inherits System.Exception

    Sub New()
        MyBase.New()
    End Sub

    Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

    Sub New(ByVal innerException As Exception)
        MyBase.New(Terminology.GetString(My.Resources.SharedMessagesKey, RES_ErrorSavingRequest), innerException)
    End Sub

    Sub New(ByVal message As String, ByVal innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

    Public Sub New( _
        ByVal info As System.Runtime.Serialization.SerializationInfo, _
        ByVal context As System.Runtime.Serialization.StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class

<Serializable()> _
Public Class AppointmentSaveFailureException
    Inherits System.Exception

    Sub New(ByVal innerException As Exception)
        MyBase.New(Terminology.GetString(My.Resources.SharedMessagesKey, RES_ErrorSavingAppointmentAndOrRequest), innerException)
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(ByVal message As String, ByVal inner As Exception)
        MyBase.New(message, inner)
    End Sub

    Public Sub New( _
        ByVal info As System.Runtime.Serialization.SerializationInfo, _
        ByVal context As System.Runtime.Serialization.StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class

<Serializable()> _
Public Class NotLoggedOnException
    Inherits System.Exception

    Sub New()
        MyBase.New()
    End Sub

    Sub New(ByVal innerException As Exception)
        MyBase.New(Terminology.GetString(My.Resources.SharedMessagesKey, RES_LoginCaption), innerException)
    End Sub

    Sub New(ByVal message As String, ByVal innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

    Public Sub New( _
    ByVal info As System.Runtime.Serialization.SerializationInfo, _
    ByVal context As System.Runtime.Serialization.StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class