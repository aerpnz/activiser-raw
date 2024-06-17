Imports activiser.Library
Imports activiser.Library.WebService.activiserDataSet

Module RequestStatusCodeModule
    Public Structure wsRequestStatusCodes
        Public ReadOnly [New] As Integer
        Public ReadOnly WIP As Integer
        Public ReadOnly Cancelled As Integer
        Public ReadOnly Complete As Integer

        Public Sub New(ByVal iNew As Integer, ByVal iWIP As Integer, _
        ByVal iComplete As Integer, _
        ByVal iCancelled As Integer)
            [New] = iNew
            Cancelled = iCancelled
            Complete = iComplete
            WIP = iWIP
        End Sub
    End Structure

    Public RequestStatusCodes As wsRequestStatusCodes

    Public Sub LoadRequestStatusCodes()
        Dim intNew, intWIP, intComplete, intCancelled As Integer

        For Each dr As RequestStatusRow In gClientDataSet.RequestStatus
            If dr.IsNewStatus AndAlso intNew = 0 Then
                intNew = dr.RequestStatusID
            ElseIf dr.IsInProgressStatus AndAlso intWIP = 0 Then
                intWIP = dr.RequestStatusID
            ElseIf dr.IsCancelledStatus AndAlso intCancelled = 0 Then
                intCancelled = dr.RequestStatusID
            ElseIf dr.IsCompleteStatus AndAlso intComplete = 0 Then
                intComplete = dr.RequestStatusID
            End If
        Next

        If intNew = 0 OrElse intWIP = 0 OrElse intComplete = 0 OrElse intCancelled = 0 Then
            Throw New InvalidOperationException(Terminology.GetString(My.Resources.SharedMessagesKey, RES_RequestStatusDefinitionError))
        End If
        RequestStatusCodes = New wsRequestStatusCodes(intNew, intWIP, intComplete, intCancelled)
    End Sub
End Module
