Imports activiser.Library.activiserWebService

Public Class JobSupportInfoForm

    Private Const MODULENAME As String = "JobTrackingInfoForm"

    Private _job As activiserDataSet.JobRow

    Private _originalStatusIndex As Integer
    'Private _originalEmailStatus As Integer

    Private jobStatusList As New CategoryObjectCollection(MODULENAME & ":" & "jobStatusList")

    Sub New(ByVal job As activiserDataSet.JobRow)
        MyBase.New()
        Me.InitializeComponent()
        Me.LoadJobStatusList()
        Terminology.LoadLabels(Me)
        Terminology.LoadToolTips(Me, Me.ToolTipProvider)
        Me.Job = job
    End Sub

    Public Property Job() As activiserDataSet.JobRow
        Get
            Return _job
        End Get
        Set(ByVal value As activiserDataSet.JobRow)
            If _job Is value Then
                Return
            End If
            _job = value
            JobNumberTextBox.Text = _job.JobNumber
            JobIDTextBox.Text = If(_job.IsJobIDNull, My.Resources.None, CStr(_job.JobID))
            JobUIDTextBox.Text = _job.JobUID.ToString()
            ConsultantUidTextBox.Text = If(_job.IsConsultantUIDNull, My.Resources.None, _job.ConsultantUID.ToString())
            ConsultantJobIDTextBox.Text = If(_job.IsConsultantJobIDNull, My.Resources.None, CStr(_job.ConsultantJobID))
            RequestUidTextBox.Text = If(_job.IsRequestUIDNull, My.Resources.None, _job.RequestUID.ToString())
            If _job.IsClientSiteUIDNull Then
                If _job.RequestRow IsNot Nothing Then
                    ClientSiteUidTextBox.Text = If(_job.RequestRow.IsClientSiteUIDNull, My.Resources.None, _job.RequestRow.ClientSiteUID.ToString())
                Else
                    ClientSiteUidTextBox.Text = My.Resources.None
                End If
            Else
                ClientSiteUidTextBox.Text = _job.ClientSiteUID.ToString()
            End If
            FlagTextBox.Text = If(_job.IsFlagNull, My.Resources.None, CStr(_job.Flag))
            EMailStatusPicker.Value = If(_job.IsEmailStatusNull, 0, _job.EmailStatus)
            'LoadJobStatusList()
            Dim lJobStatusListIndexOf As Integer = jobStatusList.IndexOf(_job.JobStatusID)
            JobStatusPicker.SelectedIndex = lJobStatusListIndexOf ' .SelectedItem = Me.jobStatusList.Item(lJobStatusListIndexOf)
            _originalStatusIndex = JobStatusPicker.SelectedIndex
            '_originalEmailStatus = _job.EmailStatus

            TrackingInfoTextBox.Text = If(_job.IsTrackingInfoNull, My.Resources.None, _job.TrackingInfo)
        End Set
    End Property

    Private Sub JobTrackingInfoForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Sub LoadJobStatusList()
        CategoryObjectCollection.PopulateList(Me.JobStatusPicker, Me.jobStatusList, "JobStatus", "JobStatusID", "Description", "JobStatusID", Nothing, Nothing, Nothing)
    End Sub

    Private Sub AbortButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AbortButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DoneButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DoneButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        'save form data
        If _originalStatusIndex <> Me.JobStatusPicker.SelectedIndex OrElse Me.EMailStatusPicker.Value <> Job.EmailStatus Then
            _job.JobStatusID = Me.jobStatusList(Me.JobStatusPicker.SelectedIndex).ObjectId.Value
            _job.EmailStatus = CByte(Me.EMailStatusPicker.Value)
            ConsoleData.StartRefresh()
        End If

        Me.Close()
    End Sub

    'Private Sub JobStatusIDNumericUpDown_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
    '    If CurrentJob IsNot Nothing Then
    '        If Not CurrentJob.IsJobStatusIDNull AndAlso CurrentJob.JobStatusID = 0 Then
    '            If CurrentJob.JobStatusID = CType(Me.JobStatusPicker.SelectedItem, CategoryObjectListItem).ObjectId.Value Then Return
    '            Dim message As String = _
    '                "Warning: incorrectly changing a job status out of draft can cause corruption of client data. " & vbCrLf & _
    '                "Are you sure you want to do this ?"
    '            If MessageBox.Show(message, My.Resources.activiserFormTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.No Then
    '                e.Cancel = True
    '            Else
    '                CurrentJob.JobStatusID = CType(Me.JobStatusPicker.SelectedItem, CategoryObjectListItem).ObjectId.Value
    '                If CurrentJob.JobStatusID < 5 AndAlso CurrentJob.JobStatusID > 0 Then MainForm.NavigationTree.SelectedNode = MainForm.allCompleteNode
    '                MainForm.RefreshNavTree()
    '                EnableForm((ConsoleUser.Administration OrElse ConsoleUser.Management OrElse _
    '                              (Not Me._currentJob.IsConsultantUIDNull AndAlso (Me._currentJob.ConsultantUID = ConsoleUser.ConsultantUID))), True)
    '                RefreshMe()
    '            End If
    '        End If
    '        '
    '    End If
    'End Sub
End Class