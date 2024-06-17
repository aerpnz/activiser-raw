Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.ComponentModel
Imports Outlook = Microsoft.Office.Interop.Outlook
Imports activiser.Library.activiserWebService
Imports activiser.Library.activiserWebService.activiserDataSet

Public Class OutlookRequestForm
    Private Const MODULENAME As String = "OutlookRequestForm"

    Const STR_RequestStatusID As String = "RequestStatusID"

    Private _app As Outlook.Application
    Private _currentRequest As RequestRow ' activiserDb.RequestRow
    Private _appointment As Outlook.AppointmentItem

    Private _initialised As Boolean

    Private _appointmentLength As TimeSpan

    Private consultantList As New BusinessObjectList(STR_ConsultantList)
    Private clientList As New BusinessObjectList(STR_ClientList)
    Private requestList As New BusinessObjectList(STR_RequestList)
    Private requestStatusList As New CategoryObjectCollection(MODULENAME & ":" & "requestStatusList")
    Private consultantStatusList As New CategoryObjectCollection(MODULENAME & ":" & "consultantStatusList")

    Private Enum RequestFormMode
        NewRequest
        ScheduleExistingRequest
        ExistingAppointment
    End Enum

    Private _currentMode As RequestFormMode = RequestFormMode.NewRequest

    Friend Class ReminderTime
        Public Minutes As Integer
        Public Description As String
        Public Overrides Function ToString() As String
            Return Description
        End Function

        Public Sub New(ByVal reminderTimeString As String)
            Const CHAR_Comma As Char = ","c
            Dim s() As String = reminderTimeString.Split(CHAR_Comma)
            Try
                Me.Minutes = Integer.Parse(s(0))
                Me.Description = s(1)
            Catch ex As Exception
                Const STR_reminderTimeParmName As String = "reminderTimeString"
                Throw New ArgumentException(Terminology.GetString(My.Resources.SharedMessagesKey, RES_ReminderTimeParseError), STR_reminderTimeParmName)
            End Try
        End Sub
    End Class

    Private _saveResult As Boolean
    Private _inSaveRequest As Boolean

    Private Function SaveRequest() As Boolean
        ' If _inSaveRequest Then Return False
        Dim mc As Cursor = Me.Cursor
        Try
            _inSaveRequest = True
            Me.Cursor = Cursors.WaitCursor
            '           Me.Enabled = False
            _saveResult = False
            If String.IsNullOrEmpty(Me.ShortDescriptionBox.Text.Trim) Then
                Throw New RequestSaveFailureException(Terminology.GetString(My.Resources.SharedMessagesKey, RES_ShortDescriptionRequired))
            End If
            If Me.ConsultantCombo.SelectedIndex = -1 Then 'orelse me.ConsultantCombo.SelectedValue
                Throw New RequestSaveFailureException(Terminology.GetString(My.Resources.SharedMessagesKey, RES_ConsultantRequired))
            End If
            If Me.ClientCombo.SelectedIndex = -1 Then ' no client selected
                Throw New RequestSaveFailureException(Terminology.GetString(My.Resources.SharedMessagesKey, RES_ClientRequired))
            End If

            Me.RequestBindingSource.EndEdit()

            Dim requestUid As Guid = Me._currentRequest.RequestUID
            Dim dsChanges As activiserDataSet = CType(Me.ClientDataSet.GetChanges(), activiserDataSet)
            Dim results As New UploadResults
            Dim feedbackDs As New DataSet
            If dsChanges IsNot Nothing Then
                UploadRequest(dsChanges, feedbackDs)

                If feedbackDs IsNot Nothing Then
                    Me.ClientDataSet.Merge(feedbackDs)
                Else
                    Throw New RequestSaveFailureException(Terminology.GetString(My.Resources.SharedMessagesKey, RES_NoDataReturnedFromServer), Nothing)
                End If
            End If

            Me.RequestBindingSource.Position = (Me.RequestBindingSource.Find(STR_RequestUID, requestUid))

            _currentRequest.AcceptChanges() ' remove 'modified' flag
            _saveResult = True
        Catch ex As RequestSaveFailureException
            Throw
        Catch ex As System.Exception
            Throw New RequestSaveFailureException(ex)
        Finally
            _inSaveRequest = False
            Me.Cursor = mc
        End Try
        Return _saveResult
    End Function

    Private _dsChanges As activiserDataSet
    Private _results As UploadResults
    Private _feedbackDs As DataSet

    Private Sub UploadRequest(ByVal dsChanges As activiserDataSet, ByRef feedbackDs As DataSet)
        Dim c As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        Try
            dsChanges.EnforceConstraints = False
            dsChanges.JobStatus.Clear()
            dsChanges.RequestStatus.Clear()
            dsChanges.ClientSite.Clear()
            dsChanges.ClientSiteStatus.Clear()
            dsChanges.Consultant.Clear()
            ' clear any lookup tables
            For Each dt As DataTable In dsChanges.Tables
                If dt.Rows.Count <> 0 AndAlso (dt.PrimaryKey.Length <> 0) AndAlso (dt.PrimaryKey(0).DataType IsNot GetType(Guid)) Then ' don't allow upload of non-transaction tables; this should get rid of any lookup tables.
                    dt.Clear()
                End If
            Next

            _dsChanges = dsChanges
            Dim t As New Threading.Thread(AddressOf UploadRequestBG)
            t.Start()

            Do While t IsNot Nothing AndAlso (t.ThreadState And (Threading.ThreadState.Stopped Or Threading.ThreadState.StopRequested Or Threading.ThreadState.Aborted Or Threading.ThreadState.Aborted)) = 0
                Application.DoEvents()
                Threading.Thread.Sleep(100)
            Loop
            If _uploadException IsNot Nothing Then
                'results = Nothing
                feedbackDs = Nothing
                Terminology.DisplayMessage(MODULENAME, RES_SaveRequestErrorKey, MessageBoxIcon.Exclamation)
                'Debug.WriteLine(String.Format("Error saving request: {0}", _uploadException.ToString()))
            Else
                'results = _results
                feedbackDs = _feedbackDs
            End If

        Catch ex As Exception

        Finally
            Me.Cursor = c
        End Try
    End Sub

    Private _uploadException As Exception

    Private Sub UploadRequestBG()
        Try
            'ConsoleUploadDataSetUpdates doesn't fetch the users' last sync time....
            If ConsoleData.WebService.ConsoleUploadDataSetUpdates(deviceId, ConsoleData.ConsoleUser.ConsultantUID, _dsChanges) = 0 Then
                _feedbackDs = ConsoleData.WebService.GetRequestDetails(deviceId, ConsoleData.ConsoleUser.ConsultantUID, _currentRequest.RequestUID)
            Else
                Throw New ApplicationException("Error uploading data set updates")
            End If
        Catch ex As Exception
            _uploadException = ex
        End Try
    End Sub

    Private Sub SetAppointmentInfo()
        Try
            _appointment.BillingInformation = _currentRequest.RequestUID.ToString
            If Not _currentRequest.IsShortDescriptionNull Then
                _appointment.Subject = Terminology.GetFormattedString(MODULENAME, RES_AppointmentSubjectTemplate, _currentRequest.RequestNumber, _currentRequest.ShortDescription)
            Else
                _appointment.Subject = Terminology.GetFormattedString(MODULENAME, RES_AppointmentSubjectTemplate, _currentRequest.RequestNumber, RES_NoDescription)
            End If

            Dim longDescription As String = String.Empty
            If My.Settings.PrependAddressToLongDescription Then
                longDescription = If(_currentRequest.ClientSiteRow.IsSiteAddressNull, String.Empty, _currentRequest.ClientSiteRow.SiteAddress)
            End If

            If Not _currentRequest.IsLongDescriptionNull Then
                longDescription &= _currentRequest.LongDescription
            End If

            _appointment.Body = longDescription
            _appointment.Start = Me.StartTimePicker.Value
            _appointment.End = Me.FinishTimePicker.Value
            _appointment.AllDayEvent = Me.AllDayEventCheckBox.Checked

            If My.Settings.AppendAddressToLocation Then
                _appointment.Location = Terminology.GetFormattedString(RES_AppointmentLocationTemplate, _currentRequest.ClientSiteRow.SiteName, _currentRequest.ClientSiteRow.SiteAddress.Replace(vbCrLf, STR_Comma).Replace(vbLf, STR_Comma))
            Else
                _appointment.Location = _currentRequest.ClientSiteRow.SiteName
            End If
            _appointment.BusyStatus = CType(Me.ShowTimeAsComboBox.SelectedIndex, Outlook.OlBusyStatus)
            _appointment.ReminderMinutesBeforeStart = CInt(Me.ReminderComboBox.SelectedValue)
            _appointment.ReminderSet = Me.ReminderCheckBox.Checked

            _appointment.MessageClass = Terminology.GetString(My.Resources.SharedMessagesKey, RES_OutlookAppointmentClass)
        Catch ex As Exception
            Debug.WriteLine(String.Format("Exception setting Appointment Info: {0}{2}{1}", ex.Message, ex.StackTrace, vbNewLine))
            Throw
        End Try

        Debug.WriteLine("Appointment info set.")
    End Sub

    Private Function SaveAppointment(ByVal close As Boolean) As Boolean
        Try
            If _appointment Is Nothing Then
                _appointment = TryCast(_app.CreateItem(Outlook.OlItemType.olAppointmentItem), Outlook.AppointmentItem)
            End If
            SetAppointmentInfo()
            If close Then
                '_appointment.Delete()
                _appointment.Close(Outlook.OlInspectorClose.olSave)
            Else
                _appointment.Save()
                'Changes Made
                Dim entryID As String
                entryID = Me._appointment.EntryID

                Me._appointment.Close(Outlook.OlInspectorClose.olSave)
                Marshal.FinalReleaseComObject(_appointment)
                _appointment = Nothing

                _appointment = CType(Me._app.GetNamespace(STR_MAPI).GetItemFromID(entryID), Outlook.AppointmentItem)
            End If
        Catch ex As Runtime.InteropServices.COMException
            Throw
        Catch ex As Exception
            Throw New AppointmentSaveFailureException(ex)
        End Try
        Return True
    End Function

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAndCloseButton.Click
        Try
            _closing = True
            If SaveRequest() Then
                If SaveAppointment(True) Then
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                End If
            End If
        Catch ex As RequestSaveFailureException
            _closing = False
            Terminology.DisplayFormattedMessage(MODULENAME, STR_RequestSaveError, MessageBoxIcon.Exclamation, ex.Message)
        Catch ex As Runtime.InteropServices.COMException
            _closing = False
            Terminology.DisplayFormattedMessage(MODULENAME, RES_AppointmentSaveError, MessageBoxIcon.Exclamation, ex.Message)
        Catch ex As AppointmentSaveFailureException
            _closing = False
            Terminology.DisplayFormattedMessage(MODULENAME, RES_AppointmentSaveError, MessageBoxIcon.Exclamation, ex.Message)
        Catch ex As Exception
            _closing = False
            Terminology.DisplayFormattedMessage(MODULENAME, STR_SaveError, MessageBoxIcon.Exclamation, ex.Message)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub New(ByVal app As Outlook.Application, ByVal newRequest As Boolean, ByVal subject As String, ByVal description As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If Not LoggedOn Then

        End If
        _app = app
        If newRequest Then
            Me.InvokeNewRequest(subject, description)
        Else
            Me.ScheduleRequest()
        End If
        Dim nAD As DateTime
        If Me._currentRequest.IsNextActionDateNull Then
            nAD = DateTime.MinValue
        Else
            nAD = Me._currentRequest.NextActionDate
        End If
        Me.StartTimePicker.Value = DateTime.Now
        Me.FinishTimePicker.Value = Me.StartTimePicker.Value.AddMinutes(Me.StartTimePicker.TimeInterval)
        If nAD <> DateTime.MinValue Then
            Me.NextActionDatePicker.Value = nAD
        End If
        _initialised = True
    End Sub

    Public Sub New(ByVal outlookApplication As Outlook.Application, ByVal appointment As Outlook.AppointmentItem, ByVal createNewRequest As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If outlookApplication Is Nothing Then
            Throw New ArgumentNullException("outlookApplication")
        End If
        If appointment Is Nothing Then
            Throw New ArgumentNullException("appointment")
        End If

        _app = outlookApplication
        _appointment = appointment

        Dim ru As String = Nothing
        ru = appointment.BillingInformation

        If String.IsNullOrEmpty(ru) Then
            If createNewRequest Then
                Dim t As New Threading.Thread(AddressOf NewRequest)
                t.Start()
                Do While t IsNot Nothing AndAlso (t.ThreadState And (Threading.ThreadState.Stopped Or Threading.ThreadState.StopRequested Or Threading.ThreadState.Aborted Or Threading.ThreadState.Aborted)) = 0
                    Application.DoEvents()
                    Threading.Thread.Sleep(100)
                Loop
                t = Nothing
            Else
                ScheduleRequest()
                'Dim t As New Threading.Thread(AddressOf ScheduleRequest)
                't.Start()
                'Do While t IsNot Nothing AndAlso (t.ThreadState And (Threading.ThreadState.Stopped Or Threading.ThreadState.StopRequested Or Threading.ThreadState.Aborted Or Threading.ThreadState.Aborted)) = 0
                '    Application.DoEvents()
                '    Threading.Thread.Sleep(100)
                'Loop
                't = Nothing
            End If
        Else
            LoadRequest(ru)
        End If
        Me.StartTimePicker.Value = _appointment.Start
        Me.FinishTimePicker.MinValue = _appointment.Start
        Me.FinishTimePicker.Value = _appointment.End
        Me._appointmentLength = _appointment.End - appointment.Start
        Me.ReminderCheckBox.Checked = _appointment.ReminderSet
        Me.ReminderComboBox.SelectedValue = _appointment.ReminderMinutesBeforeStart
        Me.PastAppointmentPanel.Visible = _appointment.Start < DateTime.Now
        _initialised = True
        'If createNewRequest Then
        '    SetNAD()
        'End If
    End Sub

    Private Sub NewRequest()
        InvokeNewRequest(String.Empty, String.Empty)
    End Sub

    Private Delegate Sub NewRequestDelegate(ByVal subject As String, ByVal description As String)

    Private Sub InvokeNewRequest(ByVal subject As String, ByVal description As String)
        Me._settingCurrentRequest = True
        Me._currentMode = RequestFormMode.NewRequest

        Dim d As New NewRequestDelegate(AddressOf NewRequest)
        d.Invoke(subject, description)
    End Sub

    Private Delegate Sub Callback()

    Public Sub ReloadConsultantList()
        TraceVerbose(STR_Fired)
        BusinessObjectList.PopulateList(Me.ConsultantCombo, Me.consultantList, Me.ClientDataSet.Consultant, STR_ConsultantUid, STR_Name)
    End Sub

    Public Sub ReloadClientList()
        TraceVerbose(STR_Fired)
        BusinessObjectList.PopulateList(Me.ClientCombo, Me.clientList, Me.ClientDataSet.ClientSite, STR_ClientSiteUID, STR_SiteName)
    End Sub

    Public Sub ReloadRequestList()
        TraceVerbose(STR_Fired)
        BusinessObjectList.PopulateList(Me.RequestSelector, Me.requestList, Me.ClientDataSet.Request, STR_RequestUID, STR_RequestNumber, Me.RequestSelectorBindingSource.Filter)
    End Sub

    Private Sub FindClient(ByVal location As String)
        Try
            Dim firstClientGuid As Guid = Guid.Empty
            Dim resultDRs() As DataRow = Me.ClientDataSet.ClientSite.Select(String.Format(STR_SiteNameStartsWithTemplate, location.Replace(STR_Apostrophe, STR_TwoApostrophes)))
            If resultDRs IsNot Nothing And resultDRs.Length <> 0 Then
                firstClientGuid = CType(resultDRs(0).Item(STR_ClientSiteUID), Guid)
            Else
                resultDRs = Me.ClientDataSet.ClientSite.Select(String.Format(STR_SiteNameContainsFilterTemplate, location.Replace(STR_Apostrophe, STR_TwoApostrophes)))
                If resultDRs IsNot Nothing And resultDRs.Length <> 0 Then
                    firstClientGuid = CType(resultDRs(0).Item(STR_ClientSiteUID), Guid)
                Else
                    Dim firstChars As String
                    If location.Length > 4 Then
                        firstChars = location.Substring(0, 4)
                    Else
                        firstChars = location.Substring(0, 1)
                    End If
                    firstChars = firstChars.Replace(STR_Apostrophe, STR_TwoApostrophes)
                    resultDRs = Me.ClientDataSet.ClientSite.Select(String.Format(STR_SiteNameStartsWithTemplate, firstChars), STR_SiteName)
                    If resultDRs IsNot Nothing And resultDRs.Length <> 0 Then
                        firstClientGuid = CType(resultDRs(0).Item(STR_ClientSiteUID), Guid)
                    End If
                End If
            End If
            If firstClientGuid <> Guid.Empty Then
                If Me.InvokeRequired Then
                    Dim d As New SetClientSiteUidDelegate(AddressOf SetClientSiteUid)
                    Me.Invoke(d, firstClientGuid)
                Else
                    SetClientSiteUid(firstClientGuid)
                End If
            End If

        Catch ex As Exception
            TraceError(ex)
        End Try
    End Sub

    Private Sub NewRequest(ByVal subject As String, ByVal description As String)
        Try
            Dim location As String
            If _appointment IsNot Nothing Then
                location = _appointment.Location
            End If

            If String.IsNullOrEmpty(subject) AndAlso _appointment IsNot Nothing Then
                subject = _appointment.Subject
            End If

            Try
                If String.IsNullOrEmpty(description) AndAlso _appointment IsNot Nothing Then
                    description = _appointment.Body ' triggers Outlook security dialog!
                End If
            Catch ex As Exception

            End Try

            Me.RequestSelector.Enabled = False
            Me.RequestNavigator.Visible = False

            Me.SuspendLayout()

            Me.RequestBindingSource.SuspendBinding()

            Me.ClientDataSet.Merge(ConsoleData.WebService.GetNewRequestData(deviceId, OutlookClientGuid))

            Me.RequestBindingSource.ResumeBinding()

            Me.ReloadClientList()
            Me.ReloadConsultantList()
            Me.ReloadStatusList()
            Me.RequestBindingSource.AddNew()
            Me.RequestBindingSource.MoveLast()

            Me.ClientCombo.Enabled = True

            _settingCurrentRequest = False ' unset to allow SetCurrentRequest to work properly.
            If Me.InvokeRequired Then
                Dim rsd As New SetDefaultRequestStatusDelegate(AddressOf SetDefaultRequestStatus)
                Me.Invoke(rsd)
                Dim cb As New Callback(AddressOf ResumeLayout)
                Me.Invoke(cb)
                cb = New Callback(AddressOf SetCurrentRequest)
                Me.Invoke(cb)
                cb = New Callback(AddressOf SetNAD)
                Me.Invoke(cb)
            Else
                SetCurrentRequest()
                SetNAD()
                'Me.RequestStatusCombo.SelectedValue = My.Settings.DefaultNewRequestStatus
                Me.ResumeLayout()
            End If

            _currentRequest.ShortDescription = subject
            _currentRequest.LongDescription = description

            If _appointment IsNot Nothing AndAlso Not String.IsNullOrEmpty(location) AndAlso _currentRequest IsNot Nothing AndAlso _currentRequest.IsClientSiteUIDNull Then
                FindClient(location)
            End If

        Catch ex As Exception
            Debug.Print(ex.ToString)
        Finally

        End Try
    End Sub

    Private _inLoadRequest As Boolean

    Private Sub ScheduleRequest()
        Try
            Me._inLoadRequest = True
            Me._currentMode = RequestFormMode.ScheduleExistingRequest
            Me.RequestSelector.Enabled = True
            Me.RequestNavigator.Visible = True

            Dim ds As DataSet = ConsoleData.WebService.GetShortRequestList(deviceId, ConsoleUser.ConsultantUID.ToString())

            Me.ClientDataSet.Clear()
            Me.ClientDataSet.Merge(ds)
            ReloadConsultantList()
            ReloadClientList()
            ReloadRequestList()
            ReloadStatusList()

            Dim foundRequest As Boolean = False

            If _appointment IsNot Nothing Then
                Dim subject As String = _appointment.Subject
                If subject IsNot Nothing Then
                    For Each r As RequestRow In Me.ClientDataSet.Request
                        If subject.StartsWith(r.RequestNumber) OrElse subject.Contains(r.RequestNumber) Then
                            Me.RequestBindingSource.Position = Me.RequestBindingSource.Find(Me.ClientDataSet.Request.RequestUIDColumn.ColumnName, r.RequestUID)
                            foundRequest = True
                            Exit For
                        End If
                    Next
                    If Not foundRequest Then
                        For Each r As RequestRow In Me.ClientDataSet.Request
                            If r.ShortDescription.ToUpper().Contains(subject.ToUpper()) Then
                                Me.RequestBindingSource.Position = Me.RequestBindingSource.Find(Me.ClientDataSet.Request.RequestUIDColumn.ColumnName, r.RequestUID)
                                foundRequest = True
                                Exit For
                            End If
                        Next
                    End If

                End If
                If Not foundRequest Then
                    Dim location As String
                    location = _appointment.Location
                    If Not String.IsNullOrEmpty(location) Then
                        FindClient(location)
                    Else
                        Me.ClientCombo.SelectedIndex = 0 ' select first client, force request filter...
                    End If
                End If
            End If

            SetCurrentRequest()


        Catch ex As Exception
            Debug.Print(ex.ToString)
        Finally
            Me._inLoadRequest = False
        End Try
    End Sub


    Private Sub LoadRequest(ByVal requestUidString As String)
        Dim requestUid As Guid
        Try
            requestUid = New Guid(requestUidString)
        Catch ex As Exception
#If DEBUG Then
            MsgBox("Error in RequestUid: " & requestUidString)
#End If
            Throw
        End Try
        Try
            _inLoadRequest = True
            Me._currentMode = RequestFormMode.ExistingAppointment
            Dim ds As DataSet = ConsoleData.WebService.GetRequestDetails(deviceId, OutlookClientGuid, requestUid)
            If ds IsNot Nothing Then
                'Dim cDs As New WebService.ClientDataSet
                'cDs.Merge(ds)
                Me.ClientCombo.Enabled = False
                Dim currentconsultants As Utility.ActiveConsultantsDataTable = ConsoleData.WebService.GetCurrentConsultantList(deviceId)
                'cDs.Consultant.Merge(currentconsultants)
                Me.RequestBindingSource.SuspendBinding()
                Me.ClientDataSet.Clear()
                Me.ClientDataSet.Merge(ds)
                Me.ClientDataSet.Consultant.Merge(currentconsultants)

                ' Me.ClientDataSet.EnforceConstraints = True
                Me.ReloadClientList()
                Me.ReloadConsultantList()
                Me.ReloadStatusList()
                Me.RequestBindingSource.ResumeBinding()
                Me.RequestBindingSource.MoveFirst()
                SetCurrentRequest()
                Me.RequestSelector.Enabled = False
                Me.RequestNavigator.Visible = False
            Else
                Throw New ArgumentException(Terminology.GetString(MODULENAME, RES_ErrorCommunicatingWithActiviserWebService))
            End If
        Catch ex As Exception
            Throw
        Finally
            _inLoadRequest = False
        End Try
    End Sub

    Private Sub LoadShowTimeAsList()
        Dim ShowTimeItems(My.Settings.ShowTimeAsList.Count - 1) As String
        My.Settings.ShowTimeAsList.CopyTo(ShowTimeItems, 0)
        Me.ShowTimeAsComboBox.Items.AddRange(ShowTimeItems)
        Me.ShowTimeAsComboBox.SelectedIndex = My.Settings.ShowTimeAsDefault
    End Sub

    Private Sub LoadReminderMinutesList()
        Dim ReminderItems(My.Settings.ReminderTimeList.Count - 1) As ReminderTime
        For i As Integer = 0 To My.Settings.ReminderTimeList.Count - 1
            ReminderItems(i) = New ReminderTime(My.Settings.ReminderTimeList(i))
        Next
        Me.ReminderComboBox.Items.AddRange(ReminderItems)
        Me.ReminderComboBox.ValueMember = STR_Minutes
        Me.ReminderComboBox.DisplayMember = STR_Description
    End Sub

    Private Sub OutlookRequestForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If _appointment IsNot Nothing Then
            Me._appointment.Close(Outlook.OlInspectorClose.olDiscard) ' just in case
            Marshal.FinalReleaseComObject(_appointment)
        End If
        Me._appointment = Nothing
        Me._currentRequest = Nothing
        Me._dsChanges = Nothing
        Me._feedbackDs = Nothing
        Me._app = Nothing
    End Sub

    Private _closing As Boolean
    Private Sub OutlookRequestForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If _closing Then Return ' already in a controlled close.

        _closing = True
        Try
            If Not Me.DialogResult = Windows.Forms.DialogResult.OK AndAlso Me._currentRequest IsNot Nothing AndAlso Me._currentRequest.RowState = DataRowState.Modified Then
                Select Case Terminology.AskQuestion(My.Resources.SharedMessagesKey, RES_DoYouWantToSaveChanges, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3)
                    Case Windows.Forms.DialogResult.Yes
                        If SaveRequest() Then
                            If SaveAppointment(True) Then
                                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                                Return
                            End If
                        End If
                    Case Windows.Forms.DialogResult.No
                    Case Windows.Forms.DialogResult.Cancel
                        e.Cancel = True
                        Return
                End Select
            Else
                Return
            End If

        Catch ex As RequestSaveFailureException
            Terminology.DisplayFormattedMessage(MODULENAME, STR_RequestSaveError, MessageBoxIcon.Exclamation, ex.Message)
            e.Cancel = True
        Catch ex As Runtime.InteropServices.COMException
            Terminology.DisplayFormattedMessage(MODULENAME, RES_AppointmentSaveError, MessageBoxIcon.Exclamation, ex.Message)
            e.Cancel = True
        Catch ex As AppointmentSaveFailureException
            Terminology.DisplayFormattedMessage(MODULENAME, RES_AppointmentSaveError, MessageBoxIcon.Exclamation, ex.Message)
            e.Cancel = True
        Catch ex As Exception
            Terminology.DisplayFormattedMessage(MODULENAME, STR_SaveError, MessageBoxIcon.Exclamation, ex.Message)
            e.Cancel = True
        End Try

    End Sub

    Private Sub OutlookRequestForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim newFormat As String = Terminology.GetFormattedString(My.Resources.SharedMessagesKey, RES_DatePickerFormat, Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern)
        Me.StartTimePicker.CustomDateFormat = newFormat
        Me.FinishTimePicker.CustomDateFormat = newFormat
        LoadShowTimeAsList()
        LoadReminderMinutesList()
        Me.ReminderCheckBox.Checked = False
        Terminology.LoadLabels(Me)

        Me.BringToFront()
    End Sub

    Private Sub BindingSource1_BindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.BindingCompleteEventArgs) Handles RequestBindingSource.BindingComplete
        If e.BindingCompleteState = BindingCompleteState.Success Then
            If e.BindingCompleteContext = BindingCompleteContext.DataSourceUpdate Then
                If Not (Me._inLoadRequest Or Me._inSaveRequest) Then
                    Debug.WriteLine("RequestBindingSource: Binding Complete, setting current request...")
                    SetCurrentRequest()
                End If
            Else
                ' Debug.WriteLine(String.Format("RequestBindingSource.{0}: Binding Complete", e.Binding.Control.Name))
            End If
        End If
    End Sub

    Private Function GetNewStatus() As Integer
        If ClientDataSet.RequestStatus.FindByRequestStatusID(My.Settings.DefaultNewRequestStatus) IsNot Nothing Then
            Return My.Settings.DefaultNewRequestStatus
        End If

        Dim rsRows() As RequestStatusRow = CType(Me.ClientDataSet.RequestStatus.Select(Me.ClientDataSet.RequestStatus.IsNewStatusColumn.ColumnName & " <> 0", Me.ClientDataSet.RequestStatus.DisplayOrderColumn.ColumnName), RequestStatusRow())
        If rsRows.Length <> 0 Then
            Return rsRows(0).RequestStatusID
        End If
        Throw New ArgumentException(Terminology.GetString(My.Resources.SharedMessagesKey, RES_NewStatusForRequestUndefined))
        Return Nothing
    End Function

    Private _settingCurrentRequest As Boolean

    Private Sub SetCurrentRequest()
        If _closing Then Return
        If _settingCurrentRequest Then Return
        _settingCurrentRequest = True
        Try
            Dim drv As DataRowView = TryCast(Me.RequestBindingSource.Current, DataRowView)
            If drv IsNot Nothing Then
                Dim newCurrent As RequestRow = TryCast(drv.Row, RequestRow)
                If newCurrent Is Me._currentRequest Then Return
                Me._currentRequest = newCurrent
                If _currentRequest IsNot Nothing AndAlso _currentRequest.IsNull(STR_RequestUID) Then ' new record
                    _currentRequest.RequestUID = Guid.NewGuid
                    _currentRequest.NextActionDate = Date.Today
                    _currentRequest.RequestStatusID = GetNewStatus()
                    Me.ConsultantCombo.SelectedIndex = -1
                    Me.ClientCombo.SelectedIndex = -1
                ElseIf _currentRequest IsNot Nothing Then
                    Dim requestUid As Guid = _currentRequest.RequestUID
                    Me.RequestSelector.SelectedValue = requestUid
                    If _currentRequest.IsLongDescriptionNull OrElse _currentRequest.GetJobRows.Length = 0 Then
                        Dim mc As Cursor = Me.Cursor
                        Me.Cursor = Cursors.WaitCursor
                        Dim requestDetails As DataSet = ConsoleData.WebService.GetRequestDetails(deviceId, OutlookClientGuid, _currentRequest.RequestUID)
                        If requestDetails IsNot Nothing Then
                            Me.ClientDataSet.Merge(requestDetails)
                            'Application.DoEvents()
                            Me.Refresh()
                        End If
                        Me.Cursor = mc
                    End If
                    If Not Me._currentRequest.IsClientSiteUIDNull Then Me.ClientCombo.SelectedValue = Me._currentRequest.ClientSiteUID
                    If Not Me._currentRequest.IsAssignedToUIDNull Then Me.ConsultantCombo.SelectedValue = Me._currentRequest.AssignedToUID
                End If
                If Not Me._currentRequest.IsRequestStatusIDNull Then Me.RequestStatusCombo.SelectedValue = Me._currentRequest.RequestStatusID
            End If
        Catch ex As Exception
            TraceError(ex)
        Finally
            _settingCurrentRequest = False
        End Try
    End Sub

    Private Sub RequestBindingSource_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RequestBindingSource.PositionChanged
        If Me.RequestBindingSource.Position = -1 Then Exit Sub
        If Not Me._inLoadRequest Then SetCurrentRequest()
    End Sub

    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Try
            If SaveRequest() Then
                SaveAppointment(False)
            End If
        Catch ex As Exception
            Terminology.DisplayFormattedMessage(MODULENAME, STR_SaveError, MessageBoxIcon.Exclamation, ex.Message)
        End Try
    End Sub

    Private Sub AllDayEventCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllDayEventCheckBox.CheckedChanged
        If Me.AllDayEventCheckBox.Checked Then
            Me.FinishTimePicker.Enabled = False
        Else
            Me.FinishTimePicker.Enabled = True
        End If
    End Sub

    Private Sub SetNAD()
        If Not Me._initialised Then Return
        If Me._inLoadRequest Then Return
        If Me._settingCurrentRequest Then Return
        Dim newNAD As DateTime

        Select Case My.Settings.NextActionDateOption
            Case NextActionDateOptions.StartDay
                newNAD = Me.StartTimePicker.Value.Date
            Case NextActionDateOptions.StartTime
                newNAD = Me.StartTimePicker.Value
            Case NextActionDateOptions.FinishDay
                newNAD = Me.FinishTimePicker.Value.Date
            Case NextActionDateOptions.FinishTime
                newNAD = Me.FinishTimePicker.Value '.Date
            Case NextActionDateOptions.FirstBusinessDayAfterFinishDay
                Dim nextBusinessDay As DateTime = Me.FinishTimePicker.Value.Date.AddDays(1)
                If nextBusinessDay.DayOfWeek = DayOfWeek.Saturday Then
                    nextBusinessDay = nextBusinessDay.AddDays(2)
                ElseIf nextBusinessDay.DayOfWeek = DayOfWeek.Sunday Then
                    nextBusinessDay = nextBusinessDay.AddDays(1)
                End If
                newNAD = nextBusinessDay
            Case NextActionDateOptions.NextBusinessDay
                Dim nextBusinessDay As DateTime = Today.AddDays(1)
                If nextBusinessDay.DayOfWeek = DayOfWeek.Saturday Then
                    nextBusinessDay = nextBusinessDay.AddDays(2)
                ElseIf nextBusinessDay.DayOfWeek = DayOfWeek.Sunday Then
                    nextBusinessDay = nextBusinessDay.AddDays(1)
                End If
                newNAD = nextBusinessDay
            Case NextActionDateOptions.Today
                newNAD = Today
        End Select

        Me._currentRequest.NextActionDate = newNAD

    End Sub

    Private _inStartTimePicker_ValueChanged As Boolean
    Private Sub StartTimePicker_ValueChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs) Handles StartTimePicker.PropertyChanged
        If _inStartTimePicker_ValueChanged Then Return
        _inStartTimePicker_ValueChanged = True
        Try
            Me.PastAppointmentPanel.Visible = StartTimePicker.Value.ToLocalTime < DateTime.Now
            Me.FinishTimePicker.MinValue = StartTimePicker.Value
            Me.FinishTimePicker.Value = StartTimePicker.Value.Add(Me._appointmentLength)
            SetNAD()
        Catch ex As Exception
            TraceError(ex)
        Finally
            _inStartTimePicker_ValueChanged = False
        End Try
    End Sub

    Private _inRequestSelectorChange As Boolean
    Private Sub RequestSelector_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RequestSelector.SelectedIndexChanged
        If _closing OrElse _inRequestSelectorChange OrElse _settingCurrentRequest Then Return
        Try
            _inRequestSelectorChange = True
            Dim requestUid As Guid = CType(RequestSelector.SelectedValue, Guid)
            Dim newLocation As Integer = Me.RequestBindingSource.Find(STR_RequestUID, requestUid)
            If newLocation <> -1 Then
                'found one 
                Me.RequestBindingSource.Position = newLocation
                Me.RequestSelector.SelectedValue = requestUid
            Else
                Me.RequestBindingSource.Position = newLocation
            End If
        Catch ex As Exception
            TraceError(ex)
        Finally
            _inRequestSelectorChange = False
        End Try
    End Sub

#Region "Edit functions"

    Private Sub EditToolStripMenuItem_DropDownOpening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.DropDownOpening
        If TypeOf Me.ActiveControl Is TextBoxBase OrElse TypeOf Me.ActiveControl Is ComboBox Then
            Me.CutToolStripButton.Enabled = True
            Me.CutToolStripMenuItem.Enabled = True
            Me.CopyToolStripButton.Enabled = True
            Me.CopyToolStripMenuItem.Enabled = True
            Me.PasteToolStripButton.Enabled = True
            Me.PasteToolStripMenuItem.Enabled = True
            Me.SelectAllToolStripMenuItem.Enabled = True
            Return
        End If

        Me.CutToolStripButton.Enabled = False
        Me.CutToolStripMenuItem.Enabled = False
        Me.CopyToolStripButton.Enabled = False
        Me.CopyToolStripMenuItem.Enabled = False
        Me.PasteToolStripButton.Enabled = False
        Me.PasteToolStripMenuItem.Enabled = False
        Me.SelectAllToolStripMenuItem.Enabled = False
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        Dim tb As TextBoxBase = TryCast(Me.ActiveControl, TextBoxBase)
        If tb IsNot Nothing Then
            tb.SelectAll()
            Return
        End If
        Dim cb As ComboBox = TryCast(Me.ActiveControl, ComboBox)
        If cb IsNot Nothing Then
            cb.SelectAll()
            Return
        End If
    End Sub

    Private Sub CutToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripButton.Click, CutToolStripMenuItem.Click
        Dim tb As TextBoxBase = TryCast(Me.ActiveControl, TextBoxBase)
        If tb IsNot Nothing Then
            Clipboard.SetText(tb.SelectedText)
            tb.SelectedText = String.Empty
            Return
        End If
        Dim cb As ComboBox = TryCast(Me.ActiveControl, ComboBox)
        If cb IsNot Nothing Then
            Clipboard.SetText(cb.SelectedText)
            cb.SelectedText = String.Empty
            Return
        End If
    End Sub

    Private Sub CopyToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripButton.Click, CopyToolStripMenuItem.Click
        Dim tb As TextBoxBase = TryCast(Me.ActiveControl, TextBoxBase)
        If tb IsNot Nothing Then
            Clipboard.SetText(tb.SelectedText)
            Return
        End If
        Dim cb As ComboBox = TryCast(Me.ActiveControl, ComboBox)
        If cb IsNot Nothing Then
            Clipboard.SetText(cb.SelectedText)
            Return
        End If
    End Sub

    Private Sub PasteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripButton.Click, PasteToolStripMenuItem.Click
        Dim tb As TextBoxBase = TryCast(Me.ActiveControl, TextBoxBase)
        If tb IsNot Nothing Then
            tb.SelectedText = Clipboard.GetText()
            Return
        End If

        Dim cb As ComboBox = TryCast(Me.ActiveControl, ComboBox)
        If cb IsNot Nothing Then
            cb.SelectedText = Clipboard.GetText
            Return
        End If
    End Sub

    Private Sub UndoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem.Click
        If Me._currentRequest.RowState = DataRowState.Modified Then
            If Terminology.AskQuestion(My.Resources.SharedMessagesKey, RES_ConfirmUndoChanges, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                Me._currentRequest.RejectChanges()
            End If
        End If
    End Sub

#End Region

#Region "Menu functions"
    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Me._appointment.PrintOut()
    End Sub

    Private Sub FinishTimePicker_ValueChanged(ByVal sender As System.Object, ByVal e As PropertyChangedEventArgs) Handles FinishTimePicker.PropertyChanged
        If _inStartTimePicker_ValueChanged Then Return ' may reset the finishtime value as a side-effect
        Me._appointmentLength = Me.FinishTimePicker.Value - Me.StartTimePicker.Value
        SetNAD()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Using f As New SplashScreen
            f.ShowDialog()
            f.Close()
        End Using
    End Sub
#End Region

    Private Sub ReminderCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReminderCheckBox.CheckedChanged
        Me.ReminderComboBox.Enabled = Me.ReminderCheckBox.Checked
    End Sub
    Private Sub ShortDescriptionBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShortDescriptionBox.TextChanged
        Me.Text = Terminology.GetFormattedString(My.Resources.SharedMessagesKey, RES_RequestDialogTitleFormat, Me.ShortDescriptionBox.Text)
    End Sub

    Private Sub ClientCombo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientCombo.SelectedValueChanged
        If _inRequestSelectorChange OrElse _settingCurrentRequest Then Exit Sub

        If Me._currentMode = RequestFormMode.ScheduleExistingRequest Then
            Me.RequestSelectorBindingSource.Filter = String.Format(STR_ClientSiteUidFilterTemplate, Me.ClientCombo.SelectedValue)
            Me.ReloadRequestList()
            Me.RequestBindingSource.Filter = String.Format(STR_ClientSiteUidFilterTemplate, Me.ClientCombo.SelectedValue)
            Me.RequestBindingSource.MoveFirst()
            If Not Me._inLoadRequest Then SetCurrentRequest()
        ElseIf Me._currentMode = RequestFormMode.ExistingAppointment Then
            If Me._currentRequest IsNot Nothing Then
                Me._currentRequest.ClientSiteUID = CType(Me.ClientCombo.SelectedValue, Guid)
            End If
        ElseIf Me._currentMode = RequestFormMode.NewRequest Then
            If Me._currentRequest IsNot Nothing Then
                Me._currentRequest.ClientSiteUID = CType(Me.ClientCombo.SelectedValue, Guid)
            End If
        End If
    End Sub

    Private Sub RequestStatusCombo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RequestStatusCombo.SelectedValueChanged
        If _inLoadRequest OrElse _inSaveRequest OrElse _inRequestSelectorChange OrElse _settingCurrentRequest Then Return
        If Me.RequestStatusCombo.SelectedValue Is Nothing Then Return
        Me._currentRequest.RequestStatusID = CInt(Me.RequestStatusCombo.SelectedValue)
        If Me._currentRequest.IsConsultantStatusIDNull Then Return
        Me._currentRequest.ConsultantStatusID = Me._currentRequest.RequestStatusID
        '        Me.ConsultantStatusCombo.SelectedValue = Me.RequestStatusCombo.SelectedValue
        'If Me.RequestStatusCombo.SelectedIndex = -1 Then
        '    Me.ConsultantCombo.SelectedIndex = -1
        'Else
        'End If
    End Sub

    Private Delegate Sub SetDefaultRequestStatusDelegate()
    Private Sub SetDefaultRequestStatus()
        Me.RequestStatusCombo.SelectedValue = My.Settings.DefaultNewRequestStatus
    End Sub

    Private Delegate Sub SetClientSiteUidDelegate(ByVal clientSiteUid As Guid)
    Private Sub SetClientSiteUid(ByVal clientSiteUid As Guid)
        Me.ClientCombo.SelectedValue = clientSiteUid
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub ReloadStatusList()
        CategoryObjectCollection.PopulateList(Me.ClientDataSet, Me.RequestStatusCombo, Me.requestStatusList, "RequestStatus", STR_RequestStatusID, STR_Description)
        CategoryObjectCollection.PopulateList(Me.ClientDataSet, Me.ConsultantStatusCombo, Me.consultantStatusList, "RequestStatus", STR_RequestStatusID, STR_Description, String.Empty, Nothing, "<None>")
    End Sub
End Class
