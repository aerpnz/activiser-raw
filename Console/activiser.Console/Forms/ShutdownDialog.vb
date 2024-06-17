Imports System.Windows.Forms

Public Class ShutdownDialog

    Private _finalUploadComplete As Boolean

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AbortButton.Click
        Console.ConsoleData.AbortPolling()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ShutdownDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Timer1.Start()
        Me.BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private _goingDown As Boolean

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Not ConsoleData.RefreshInProgress AndAlso _finalUploadComplete Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
        If Not _goingDown Then
            Me.ProgressBar1.Value += Me.ProgressBar1.Step
            If Me.ProgressBar1.Value >= Me.ProgressBar1.Maximum Then
                _goingDown = True
            End If
        Else
            Me.ProgressBar1.Value -= Me.ProgressBar1.Step
            If Me.ProgressBar1.Value <= 0 Then
                _goingDown = False
            End If
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        ConsoleData.StopPolling()
        Dim retry As Boolean
        Do
            retry = False
            ConsoleData.UploadChanges()
            If ConsoleData.LastRefreshError IsNot Nothing Then
                If MessageBox.Show("An error occurred while uploading unsaved changes, do you want to retry or cancel the upload?", My.Resources.activiserFormTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, False) = Windows.Forms.DialogResult.Retry Then
                    retry = True
                End If
            End If
        Loop While retry
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        _finalUploadComplete = True
    End Sub
End Class
