Imports System.Drawing
Imports System.Drawing.Imaging

Imports System.ComponentModel

Public Class ErrorDialog
    Public Shared Sub DisplayError(ByVal owner As Form, ByVal ex As Exception)
        Using ed As New ErrorDialog(owner, ex)
            ed.Owner = owner
            ed.ShowDialog()
        End Using
    End Sub

    Public Shared Sub DisplayError(ByVal owner As Form, ByVal ex As Exception, ByVal message As String)
        Using ed As New ErrorDialog(owner, ex, message)
            ed.Owner = owner
            ed.ShowDialog()
        End Using
    End Sub

    Public Shared Sub DisplayError(ByVal owner As Form, ByVal message As String)
        Using ed As New ErrorDialog(owner, message)
            ed.Owner = owner
            ed.ShowDialog()
        End Using
    End Sub

    Public Sub New(ByVal owner As Form, ByVal ex As Exception)
        Me.InitializeComponent()
        Dim em As New ExceptionParser(ex)
        Me.Message = em.ToString
        Me.Owner = owner
    End Sub

    Public Sub New(ByVal owner As Form, ByVal ex As Exception, ByVal message As String)
        Me.InitializeComponent()
        Dim em As New ExceptionParser(ex)
        Me.Message = message & vbNewLine & vbNewLine & em.ToString
        Me.Owner = owner
    End Sub

    Public Sub New(ByVal owner As Form, ByVal message As String)
        Me.InitializeComponent()
        Me.Message = message
        Me.Owner = owner
    End Sub

    Public Property Message() As String
        Get
            Return ErrorMessage.Text
        End Get
        Set(ByVal value As String)
            ErrorMessage.Text = value
        End Set
    End Property

    Private Sub DialogBox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
#If Not WINDOWSMOBILE Then
        EnableContextMenus(Me.Controls)
#End If
        Me.BringToFront()
    End Sub

    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        If SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK AndAlso Not String.IsNullOrEmpty(SaveFileDialog.FileName) Then
            Dim filename As String = Me.SaveFileDialog.FileName
            Dim errorFile As IO.StreamWriter = IO.File.CreateText(filename)
            errorFile.Write(Message)
            errorFile.Close()
        End If
    End Sub

    Private Sub OkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class