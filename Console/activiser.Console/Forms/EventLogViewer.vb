Imports activiser.Library.activiserWebService

Public Class EventLogViewer

    Private LastSync As DateTime = DateTime.MinValue
    Private Sub LoadData()
        Dim nextLastSync As DateTime = DateTime.UtcNow
        Try
            Me.ActiviserConsoleLogs.Merge(ConsoleData.WebService.ConsoleGetEventLog(deviceId, ConsoleUser.ConsultantUID, LastSync))
            LastSync = nextLastSync
        Catch ' ex As Exception

        End Try
    End Sub

    Private Sub ErrorLogViewer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        '"-activiserConsole.XML"
        For Each fileName As String In My.Computer.FileSystem.GetFiles(Environment.GetEnvironmentVariable("TEMP"), FileIO.SearchOption.SearchTopLevelOnly, "*-activiserConsole.XML")
            My.Computer.FileSystem.DeleteFile(fileName)
        Next

    End Sub
    Private Sub ErrorLogViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.ActiviserConsoleLogs = ConsoleData.EventDataSet
        Terminology.LoadLabels(Me)
        LoadData()
    End Sub

    Private Sub BindingNavigatorRefreshItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BindingNavigatorRefreshItem.Click
        LoadData()
    End Sub

    Private Sub ErrorLogBindingSource_PositionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EventLogBindingSource.PositionChanged
        If Me.EventLogBindingSource.Position <> -1 Then
            If Me.EventLogBindingSource.Current IsNot Nothing Then
                Dim drv As DataRowView = CType(Me.EventLogBindingSource.Current, DataRowView)
                Dim dr As EventLogDataSet.EventLogRow = TryCast(drv.Row, EventLogDataSet.EventLogRow)
                If dr IsNot Nothing Then
                    If dr.IsEventDataNull Then
                        Me.DataViewer.Navigate("about:blank")
                        Return
                    End If
                    Static fName As String
                    If Not String.IsNullOrEmpty(fName) Then
                        If My.Computer.FileSystem.FileExists(fName) Then
                            My.Computer.FileSystem.DeleteFile(fName)
                        End If
                    End If
                    fName = My.Computer.FileSystem.GetTempFileName ' & ".XML"
                    Dim newFileName As String = My.Computer.FileSystem.GetFileInfo(fName).Name & "-activiserConsole.XML"
                    My.Computer.FileSystem.RenameFile(fName, newFileName)
                    fName = fName & "-activiserConsole.XML"
                    Dim f As New IO.StreamWriter(fName, False, System.Text.Encoding.UTF8)
                    'f.Write("<?xml version=""1.0"" encoding=""" & System.Text.Encoding.UTF8.WebName & """ ?>")
                    f.Write(dr.EventData)
                    f.Close()
                    Me.DataViewer.Navigate(fName)
                End If
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
