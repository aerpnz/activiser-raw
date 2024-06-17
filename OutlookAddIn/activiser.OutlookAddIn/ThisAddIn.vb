Imports System.Runtime.InteropServices

Public Class ThisAddIn
    Private inspectors As Outlook.Inspectors
    Private explorers As Outlook.Explorers

    Private WithEvents _starterThread As Threading.Thread

    'Friend inspectorList As New Generic.List(Of InspectorWrapper)

    Private Sub ThisAddIn_Startup(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Startup
        TraceInfo(STR_Entered)

        BackgroundStarter()
        'TODO: Work out why this doesn't work!
        '_starterThread = New Threading.Thread(AddressOf BackgroundStarter)
        '_starterThread.Name = "Activiser Add-In startup thread"
        '_starterThread.Start()

        TraceInfo(STR_Done)
    End Sub

    Private Sub ThisAddIn_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
        Return

        TraceInfo(STR_Entered)
        Do While explorerList.Count > 0
            explorerList(0).Dispose()
        Loop
        RemoveHandler Application.Explorers.NewExplorer, AddressOf NewExplorer

        Marshal.FinalReleaseComObject(inspectors)
        Marshal.FinalReleaseComObject(explorers)

        If _starterThread IsNot Nothing AndAlso _starterThread.ThreadState = Threading.ThreadState.Running Then
            _starterThread.Abort()
            _starterThread = Nothing
        End If
        TraceInfo(STR_Done)
    End Sub

    Private Sub Application_Startup() Handles Application.Startup
        TraceInfo(STR_Fired)
    End Sub

    Private Sub BackgroundStarter()
        Try
            ConsoleData.WebService = New Library.activiserWebService.activiser
            ConsoleData.WebService.UserAgent = My.Resources.HttpUserAgentString

            explorers = Application.Explorers
            inspectors = Application.Inspectors

            If My.Settings.LogonAtStartup Then ' try and load language before loading add-in
                LogInOut.Logon()
            End If

            'Dim currentExplorer As Outlook.Explorer = Application.ActiveExplorer
            For Each explorer As Outlook.Explorer In Application.Explorers
                explorerList.Add(New ExplorerWrapper(explorer, explorerList))
            Next

            AddHandler explorers.NewExplorer, AddressOf NewExplorer
            AddHandler inspectors.NewInspector, AddressOf NewInspector


            SetIconState()
            'currentExplorer.Activate()

        Catch ex As Exception
            Terminology.DisplayMessage(My.Resources.SharedMessagesKey, RES_ErrorInitialisingActiviserAddIn, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NewExplorer(ByVal Explorer As Microsoft.Office.Interop.Outlook.Explorer)
        Const STR_TraceTemplate As String = "New Explorer Added: {0}"
        TraceInfo(STR_TraceTemplate, Explorer.Caption)
        explorerList.Add(New ExplorerWrapper(Explorer, explorerList))
        SetIconState()
    End Sub

    Friend Shared NewRequestButtonClicked As Boolean

    Private Sub NewInspector(ByVal Inspector As Microsoft.Office.Interop.Outlook.Inspector)
        TraceInfo(STR_Entered)
        'Dim iw As New InspectorWrapper(Inspector)
        'iw.List = inspectorList

        Try
            Dim appointment As Outlook.AppointmentItem = TryCast(Inspector.CurrentItem, Outlook.AppointmentItem)
            'Return
            If appointment IsNot Nothing Then
                Dim activiserAppointment As AppointmentWrapper
                If (appointment.MessageClass = Terminology.GetString(My.Resources.SharedMessagesKey, RES_OutlookAppointmentClass)) Then
                    activiserAppointment = New AppointmentWrapper(appointment, False)
                ElseIf NewRequestButtonClicked Then
                    activiserAppointment = New AppointmentWrapper(appointment, True)
                End If
                If activiserAppointment IsNot Nothing Then
                    activiserAppointment.ShowRequestDialog() ' this will cancel the default form and load the activiser one.
                End If
                activiserAppointment = Nothing
                appointment = Nothing
                Marshal.FinalReleaseComObject(Inspector)
                Inspector = Nothing
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
        Finally
            NewRequestButtonClicked = False
            TraceInfo(STR_Done)
        End Try
    End Sub

    Friend Shared Sub SetIconState()
        TraceVerbose(STR_Entered)
        If explorerList Is Nothing OrElse explorerList.Count = 0 Then Return

        For Each ew As ExplorerWrapper In explorerList
            If Not LoggedOn Then
                ew.LoginButton.Picture = StdOleImageConverter.GetIPictureDispFromImage(My.Resources.Login)
                ew.LoginButton.Mask = StdOleImageConverter.GetIPictureDispFromImage(My.Resources.LoginMask)
                ew.LoginButton.Caption = Terminology.GetString(My.Resources.SharedMessagesKey, RES_LoginCaption)
            Else
                ew.LoginButton.Picture = StdOleImageConverter.GetIPictureDispFromImage(My.Resources.Logout)
                ew.LoginButton.Mask = StdOleImageConverter.GetIPictureDispFromImage(My.Resources.LogoutMask)
                ew.LoginButton.Caption = Terminology.GetString(My.Resources.SharedMessagesKey, RES_LogoutCaption)
            End If
            Dim newRequestCaption As String = Terminology.GetString(My.Resources.SharedMessagesKey, RES_ToolBarNewRequestCaption)
            ew.NewRequestButton.Caption = newRequestCaption
            Dim scheduleRequestCaption As String = Terminology.GetString(My.Resources.SharedMessagesKey, RES_ToolBarScheduleRequestCaption)
            ew.ScheduleRequestButton.Caption = scheduleRequestCaption
        Next
        TraceVerbose(STR_Done)
    End Sub

End Class
