
Public Class AppointmentWrapper
    Implements IDisposable
    Private WithEvents _requestDialog As OutlookRequestForm
    Private WithEvents _appointment As Outlook.AppointmentItem

    Private _newRequest As Boolean

    Public Sub New(ByVal appointment As Outlook.AppointmentItem, ByVal newRequest As Boolean)
        _appointment = appointment
        _newRequest = newRequest

        If Not _newRequest AndAlso Not IsGuid(appointment.BillingInformation) Then ' not recognised as a request
            Return
        End If

        If Not LoggedOn Then
            If Not LogInOut.Logon() Then

                Terminology.DisplayMessage(My.Resources.SharedMessagesKey, RES_MustBeLoggedOn, MessageBoxIcon.Exclamation)
                Return
            Else
                ThisAddIn.SetIconState()
            End If
        End If
    End Sub

    Friend Sub ShowRequestDialog()
        Try
            _requestDialog = New OutlookRequestForm(_appointment.Application, _appointment, _newRequest)
            _requestDialog.Show()
            AddHandler _appointment.Open, AddressOf Appointment_Open
        Catch ex As Exception
            RemoveHandler _appointment.Open, AddressOf Appointment_Open
            Terminology.DisplayMessage(My.Resources.SharedMessagesKey, RES_LoadingDefaultAppointmentForm, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub Appointment_Open(ByRef Cancel As Boolean)
        TraceInfo("Fired")
        Cancel = True
        RemoveHandler _appointment.Open, AddressOf Appointment_Open
        _appointment = Nothing
    End Sub

    Private Sub _requestDialog_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _requestDialog.Disposed
        Debug.WriteLine("RequestDialog disposed")
        Me.Dispose(True)
    End Sub

    Private Sub _requestDialog_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles _requestDialog.FormClosed
        Debug.WriteLine("RequestDialog closed")
    End Sub

#Region " IDisposable Support "
    Private disposedValue As Boolean = False        ' To detect redundant calls

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If Me._requestDialog IsNot Nothing AndAlso Not Me._requestDialog.IsDisposed Then
                    If Me._requestDialog.Visible Then
                        Me._requestDialog.Close()
                    End If
                End If
                Me._requestDialog = Nothing
                If Me._appointment IsNot Nothing Then
                    Me._appointment.Close(Outlook.OlInspectorClose.olDiscard)
                    Me._appointment = Nothing
                End If
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Private Sub _appointment_Write(ByRef Cancel As Boolean)
        Debug.WriteLine(_appointment.Start)
    End Sub
End Class
