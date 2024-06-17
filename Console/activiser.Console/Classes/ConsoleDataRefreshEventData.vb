Public Class ConsoleDataRefreshEventArgs
    Inherits EventArgs

    Private _updates As DataSet

    'Private _eventLogTableUpdated As Integer
    'Private _consultantTableUpdated As Integer
    'Private _clientSiteStatusTableUpdated As Integer
    'Private _clientSiteTableUpdated As Integer
    'Private _requestStatusTableUpdated As Integer
    'Private _requestTableUpdated As Integer
    'Private _jobStatusTableUpdated As Integer
    'Private _jobTableUpdated As Integer
    'Private _customDataUpdated As Integer

    'Public Property EventLogTableUpdated() As Integer
    '    Get
    '        Return _EventLogTableUpdated
    '    End Get
    '    Private Set(ByVal Value As Integer)
    '        _EventLogTableUpdated = Value
    '    End Set
    'End Property

    'Public Property ConsultantTableUpdated() As Integer
    '    Get
    '        Return _ConsultantTableUpdated
    '    End Get
    '    Private Set(ByVal Value As Integer)
    '        _ConsultantTableUpdated = Value
    '    End Set
    'End Property

    'Public Property ClientSiteStatusTableUpdated() As Integer
    '    Get
    '        Return _ClientSiteStatusTableUpdated
    '    End Get
    '    Private Set(ByVal Value As Integer)
    '        _ClientSiteStatusTableUpdated = Value
    '    End Set
    'End Property

    'Public Property ClientSiteTableUpdated() As Integer
    '    Get
    '        Return _ClientSiteTableUpdated
    '    End Get
    '    Private Set(ByVal Value As Integer)
    '        _ClientSiteTableUpdated = Value
    '    End Set
    'End Property

    'Public Property RequestStatusTableUpdated() As Integer
    '    Get
    '        Return _RequestStatusTableUpdated
    '    End Get
    '    Private Set(ByVal Value As Integer)
    '        _RequestStatusTableUpdated = Value
    '    End Set
    'End Property

    'Public Property RequestTableUpdated() As Integer
    '    Get
    '        Return _RequestTableUpdated
    '    End Get
    '    Private Set(ByVal Value As Integer)
    '        _RequestTableUpdated = Value
    '    End Set
    'End Property

    'Public Property JobStatusTableUpdated() As Integer
    '    Get
    '        Return _JobStatusTableUpdated
    '    End Get
    '    Private Set(ByVal Value As Integer)
    '        _JobStatusTableUpdated = Value
    '    End Set
    'End Property

    'Public Property JobTableUpdated() As Integer
    '    Get
    '        Return _JobTableUpdated
    '    End Get
    '    Private Set(ByVal value As Integer)
    '        _JobTableUpdated = value
    '    End Set
    'End Property

    'Public Property CustomDataUpdated() As Integer
    '    Get
    '        Return _customDataUpdated
    '    End Get
    '    Private Set(ByVal value As Integer)
    '        _customDataUpdated = value
    '    End Set
    'End Property

    Public Property Updates() As DataSet
        Get
            Return _updates
        End Get
        Set(ByVal value As DataSet)
            _updates = value
        End Set
    End Property

    Private Sub New()
        MyBase.New()
    End Sub

    Sub New(ByVal updates As DataSet)
        MyBase.New()
        Me.Updates = updates
    End Sub

    'Public Sub New(ByVal eventLogTableUpdated As Integer, ByVal consultantTableUpdated As Integer, ByVal clientSiteStatusTableUpdated As Integer, ByVal clientSiteTableUpdated As Integer, ByVal requestStatusTableUpdated As Integer, ByVal requestTableUpdated As Integer, ByVal jobStatusTableUpdated As Integer, ByVal jobTableUpdated As Integer, ByVal customDataUpdated As Integer)
    '    MyBase.New()
    '    Me.EventLogTableUpdated = eventLogTableUpdated
    '    Me.ConsultantTableUpdated = consultantTableUpdated
    '    Me.ClientSiteStatusTableUpdated = clientSiteStatusTableUpdated
    '    Me.ClientSiteTableUpdated = clientSiteTableUpdated
    '    Me.RequestStatusTableUpdated = requestStatusTableUpdated
    '    Me.RequestTableUpdated = requestTableUpdated
    '    Me.JobStatusTableUpdated = jobStatusTableUpdated
    '    Me.JobTableUpdated = jobTableUpdated
    '    Me.CustomDataUpdated = customDataUpdated
    'End Sub
End Class
