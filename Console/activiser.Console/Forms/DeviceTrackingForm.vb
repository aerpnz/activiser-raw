Imports activiser.Library.activiserWebService

Public Class DeviceTrackingForm
    Private _consultantList As New BusinessObjectCollection("DeviceTrackingConsultantList")
    Private _selectedConsultant As Nullable(Of Guid)

    Dim template As String

    Private Sub DeviceTrackingForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BusinessObjectCollection.PopulateList(Me.ConsultantUIDComboBox, Me._consultantList, "Consultant", "ConsultantUid", "Name")

        template = GpsSupport.GetDeviceTrackingHtml()

        If String.IsNullOrEmpty(template) Then
            MessageBox.Show("Can't find template file to display tracking information, or template file emtpy.", My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Me.Close()
        Else
            Me.GpsBrowser.DocumentText = template
        End If

        Dim now As DateTime = DateTime.Now
        Dim currentTz As TimeZone = TimeZone.CurrentTimeZone
        Dim timezoneName As String
        If now.IsDaylightSavingTime() Then
            timezoneName = currentTz.DaylightName
        Else
            timezoneName = TimeZone.CurrentTimeZone.StandardName
        End If

        Dim currentOffset As TimeSpan = TimeZone.CurrentTimeZone.GetUtcOffset(now)
        Dim offsetHours As Double = currentOffset.TotalHours

        Me.CurrentTimeZoneLabel.Text = String.Format("Current Time Zone: {0} ({1:+00.00;-00.00} hours)", timezoneName, offsetHours)

        'PerformQuery(DateTime.UtcNow, Nothing, Nothing)
    End Sub

    Private Sub FilterAsAt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterAsAt.CheckedChanged
        If FilterAsAt.Checked Then
            If Me.FilterSince.Checked Then Me.FilterSince.Checked = False
            If Me.FilterUntil.Checked Then Me.FilterUntil.Checked = False
        End If
    End Sub

    Private Sub FilterSince_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterUntil.CheckedChanged, FilterSince.CheckedChanged
        If Me.FilterSince.Checked OrElse Me.FilterUntil.Checked Then
            If Me.FilterAsAt.Checked Then Me.FilterAsAt.Checked = False
        End If
    End Sub

    Private Sub QueryButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QueryButton.Click
        If Me.FilterAsAt.Checked Then
            PerformQuery(Me.AsAtPicker.Value, Nothing, Nothing)
        ElseIf Me.FilterSince.Checked AndAlso Me.FilterUntil.Checked Then
            PerformQuery(Nothing, Me.SincePicker.Value, Me.UntilPicker.Value)
        ElseIf Me.FilterSince.Checked Then
            PerformQuery(Nothing, Me.SincePicker.Value, DateTime.MaxValue)
        ElseIf Me.FilterUntil.Checked Then
            PerformQuery(Nothing, DateTime.MinValue, Me.UntilPicker.Value)
        Else
            MessageBox.Show("Please select at least one date filter", My.Resources.activiserFormTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        End If
    End Sub

    Private Sub PerformQuery(ByVal asAt As Nullable(Of DateTime), ByVal since As Nullable(Of DateTime), ByVal until As Nullable(Of DateTime))

        Dim consultantUid As Guid = Guid.Empty

        If Me.FilterUser.Checked Then
            ' _selectedConsultant = CType(Me.ConsultantUIDComboBox.SelectedItem, BusinessObjectListItem).ObjectId
            If _selectedConsultant.HasValue Then
                consultantUid = _selectedConsultant.Value
            End If
        End If

        Dim dtd As DeviceTrackingDataSet.DeviceTrackingDataTable

        If since.HasValue AndAlso until.HasValue Then
            dtd = ConsoleData.WebService.ConsoleGetDeviceTrackingData(deviceId, consultantUid, since.Value, until.Value)
        ElseIf asAt.HasValue Then
            dtd = ConsoleData.WebService.ConsoleGetDeviceTrackingDataAsAt(deviceId, consultantUid, asAt.Value)
        Else
            dtd = Nothing
        End If

        Try
            If dtd Is Nothing OrElse dtd.Count = 0 Then
                MessageBox.Show("No data for selected criteria")
            Else
                DisplayMap(dtd)
            End If

        Catch ex As Exception
            MessageBox.Show(String.Format("Error displaying tracking information: {0}", ex.ToString()))
        End Try

        'Me.GpsBrowser.Refresh()
    End Sub

    Private Sub ConsultantUIDComboBox_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles ConsultantUIDComboBox.SelectionChangeCommitted
        _selectedConsultant = CType(Me.ConsultantUIDComboBox.SelectedItem, BusinessObjectListItem).ObjectId
    End Sub

    Private Sub DisplayMap(ByVal dtd As DeviceTrackingDataSet.DeviceTrackingDataTable)
        Dim username As String
        Dim notes As String
        Dim errorMessage As String = String.Empty
        Dim gi As activiser.Library.Gps.GpsPosition
        Dim baseLat As Double = My.Settings.DefaultLatitude
        Dim baseLong As Double = My.Settings.DefaultLongitude

        'Dim loLat, hiLat, loLong, hiLong As Double

        'loLat = Double.NaN
        'hiLat = Double.NaN
        'loLong = Double.NaN
        'hiLong = Double.NaN

        'For Each dtr As DeviceTrackingDataSet.DeviceTrackingRow In dtd
        '    gi = New Library.Gps.GpsPosition(dtr.TrackingInfo)

        '    If Double.IsNaN(loLat) OrElse loLat > gi.Latitude Then loLat = gi.Latitude
        '    If Double.IsNaN(hiLat) OrElse hiLat < gi.Latitude Then hiLat = gi.Latitude
        '    If Double.IsNaN(loLong) OrElse loLong > gi.Longitude Then loLong = gi.Longitude
        '    If Double.IsNaN(hiLong) OrElse hiLong < gi.Longitude Then hiLong = gi.Longitude
        'Next

        Try
            If dtd.Count > 0 Then
                gi = New Library.Gps.GpsPosition(dtd(0).TrackingInfo)
                baseLat = gi.Latitude
                baseLong = gi.Longitude
            End If
        Catch ex As Exception
            errorMessage = "WARNING: Unable to determine centre latitude/longitude for this dataset"
        End Try

        GpsSupport.ClearMap(Me.GpsBrowser)
        GpsSupport.DisplayMap(Me.GpsBrowser, baseLat, baseLong, Nothing, Nothing)
        Try
            ' Me.GpsBrowser.Document.InvokeScript("LoadMap", New Object() {baseLat, baseLong})

            For Each dtr As DeviceTrackingDataSet.DeviceTrackingRow In dtd
                gi = New Library.Gps.GpsPosition(dtr.TrackingInfo)
                Dim cr As activiserDataSet.ConsultantRow = ConsoleData.CoreDataSet.Consultant.FindByConsultantUID(dtr.ConsultantUid)
                If cr IsNot Nothing Then
                    username = cr.Name
                Else
                    username = "&LT;Unknown&GT;"
                End If

                notes = String.Format("{0}<br>{1}", dtr.SystemId, gi.ToString("L")) '.Replace("'", "\'"))
                GpsSupport.AddPin(Me.GpsBrowser, gi.Latitude, gi.Longitude, username, notes)
                ' Me.GpsBrowser.Document.InvokeScript("AddPin", New Object() {gi.Latitude, gi.Longitude, username, notes})
            Next

            Me.Refresh()

        Catch ex As Exception
            errorMessage = If(String.IsNullOrEmpty(errorMessage), ex.Message, errorMessage & vbNewLine & ex.Message)
            'Me.GpsBrowser.DocumentText = errorMessage
        End Try
    End Sub

End Class