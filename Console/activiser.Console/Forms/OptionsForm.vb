Imports System.Windows.Forms
Imports activiser.Library.activiserWebService

Public Class OptionsForm

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DoneButton.Click
        If Save() Then
            Console.ConsoleData.Polling = Me.EnableRefreshCheckBox.Checked
            Console.ConsoleData.PollInterval = CInt(RefreshInterval.Value)
            'If Me.EnableRefreshCheckBox.Checked Then
            '    Console.ConsoleData.StartPolling()
            'Else
            '    Console.ConsoleData.StopPolling()
            'End If

            Console.ConsoleData.Notify = Me.NotifyOnUpdatesCheckBox.Checked
            'Console.ConsoleData.StartRefresh()

            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AbortButton.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.EventTypeBindingSource.CancelEdit()
        Me.Close()
    End Sub

    Private Sub OptionsForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Terminology.LoadLabels(Me)
        Terminology.LoadToolTips(Me, ToolTipProvider)
        Dim mc As Cursor = Me.Cursor
        Try
            Me.NotifyOnUpdatesCheckBox.Checked = Console.ConsoleData.Notify
            Me.ServerUrlTextBox.Text = My.Settings.activiserServerUrl
            Me.IgnoreCertificateErrorsCheckBox.Enabled = Me.ServerUrlTextBox.Text.StartsWith("https://", StringComparison.Ordinal)
            Me.EnableRefreshCheckBox.Checked = Console.ConsoleData.Polling
            Me.RefreshInterval.Value = Math.Max(Console.ConsoleData.PollInterval, 60)
            Dim t As Integer = My.Settings.WebServiceTimeout
            For i As Integer = 0 To Me.ServerTimeoutUpDown.Items.Count - 1
                Dim itemValue As Integer = CInt(Me.ServerTimeoutUpDown.Items.Item(i))
                If itemValue = t Then
                    Me.ServerTimeoutUpDown.SelectedIndex = i
                ElseIf itemValue > t Then
                    If i > 0 Then
                        Me.ServerTimeoutUpDown.SelectedIndex = i - 1
                    Else
                        Me.ServerTimeoutUpDown.SelectedIndex = 0
                    End If
                End If
            Next
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        Finally
            Me.Cursor = mc
        End Try


        '         Me.ServerTimeoutUpDown.Text = CStr(My.Settings.WebServiceTimeout)

    End Sub

    Private Function Save() As Boolean
        Me.EventTypeBindingSource.EndEdit()
        Try
            If Me.EventLogDataSet.HasChanges Then
                If Not ConsoleData.WebService.ConsoleUpdateEventLog(DeviceId, ConsoleUser.ConsultantUID, Me.EventLogDataSet) Then
                    MessageBox.Show(My.Resources.OptionsFormErrorUpdatingEventDescriptions)
                    Return False
                End If
            End If

            If My.Settings.activiserServerUrl <> Me.ServerUrlTextBox.Text Then
                My.Settings.activiserServerUrl = Me.ServerUrlTextBox.Text
                ConsoleData.WebService.Url = My.Settings.activiserServerUrl
            End If
            Return True

        Catch ex As Exception
            Throw
        End Try
        Return False
    End Function

    Private Sub EventTypeToolbarSaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EventTypeToolbarSaveButton.Click
        Save()
    End Sub

    'Private _urlTestOk As Boolean
    ' Navigates to the given URL if it is valid.
    Private Function Navigate(ByVal address As String) As Boolean

        If String.IsNullOrEmpty(address) Then Return False
        If address.Equals("about:blank") Then Return False
        If Not address.StartsWith("http://", StringComparison.Ordinal) AndAlso Not address.StartsWith("https://", StringComparison.Ordinal) Then
            MessageBox.Show(My.Resources.OptionsFormUrlPrefixMessage)
            Return False
        End If

        Try
            'Dim uri As New Uri(address)

            Dim wc As New Net.WebClient
            wc.UseDefaultCredentials = True
            wc.Encoding = System.Text.Encoding.UTF8
            Dim result As String = wc.DownloadString(address)

            Me.WebBrowser1.AllowNavigation = True

            Me.WebBrowser1.Document.OpenNew(True)
            Me.WebBrowser1.Document.Write(result)
            'Application.DoEvents()
            Me.Refresh()
            If Me.WebBrowser1.Document.Title = My.Resources.OptionsFormUrlTestKeyPhrase Then
                '_urlTestOk = True
                Me.TestResultGroupLabel.ForeColor = Control.DefaultForeColor
            Else
                '_urlTestOk = False
                Me.TestResultGroupLabel.ForeColor = Color.Red
            End If

        Catch ex As Net.WebException
            '_urlTestOk = False
            Me.TestResultGroupLabel.ForeColor = Color.Red
            Me.WebBrowser1.AllowNavigation = True
            Me.WebBrowser1.Document.OpenNew(True)
            Me.WebBrowser1.Document.Write(ex.Message)
            'Application.DoEvents()
            Me.Refresh()
            Return False

        Catch ex As Exception
            '_urlTestOk = False
            Me.TestResultGroupLabel.ForeColor = Color.Red
            Me.WebBrowser1.AllowNavigation = True
            Me.WebBrowser1.Document.OpenNew(True)
            Me.WebBrowser1.Document.Write(ex.Message)
            'Application.DoEvents()
            Me.Refresh()
            Return False
        End Try
        Me.WebBrowser1.AllowNavigation = False
    End Function

    Private Sub UrlTestButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServerUrlTestButton.Click
        '_urlTestOk = False
        Me.ServerUrlTextBox.Enabled = False

        Try
            Navigate(Me.ServerUrlTextBox.Text)
        Catch ex As Exception
            Me.TestResultGroupLabel.ForeColor = Color.Red
        End Try
        Me.ServerUrlTextBox.Enabled = True
    End Sub

    Private Sub WebBrowser1_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles WebBrowser1.Navigated
    End Sub

    Private Sub ServerTimeoutUpDown_SelectedItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ServerTimeoutUpDown.SelectedItemChanged
        My.Settings.WebServiceTimeout = CInt(Me.ServerTimeoutUpDown.SelectedItem)
    End Sub

    Private Sub ServerUrlTextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ServerUrlTextBox.TextChanged
        Me.IgnoreCertificateErrorsCheckBox.Enabled = Me.ServerUrlTextBox.Text.StartsWith("https://", StringComparison.Ordinal)
    End Sub

    Private Sub IgnoreCertificateErrorsCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IgnoreCertificateErrorsCheckBox.CheckedChanged
        If Not My.Settings.IgnoreServerCertificateErrors = Me.IgnoreCertificateErrorsCheckBox.Checked Then My.Settings.IgnoreServerCertificateErrors = Me.IgnoreCertificateErrorsCheckBox.Checked
    End Sub
End Class
