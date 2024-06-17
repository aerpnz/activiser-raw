Public Class FieldList
    Public Event FieldRowChanged As EventHandler

    Private _ModuleName As String
    Public Property ModuleName() As String
        Get
            Return _ModuleName
        End Get
        Set(ByVal value As String)
            _ModuleName = value
        End Set
    End Property

    Private _clientKey As Integer
    Public Property ClientKey() As Integer
        Get
            Return _clientKey
        End Get
        Set(ByVal value As Integer)
            _clientKey = value
        End Set
    End Property

    Private _languageId As Integer
    Public Property LanguageId() As Integer
        Get
            Return _languageId
        End Get
        Set(ByVal value As Integer)
            _languageId = value
        End Set
    End Property

    Private _pasteToolTips As Boolean
    Public Property PasteToolTips() As Boolean
        Get
            Return _pasteToolTips
        End Get
        Set(ByVal value As Boolean)
            _pasteToolTips = value
        End Set
    End Property

    Private Sub BindingSource1_CurrentItemChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BindingSource1.CurrentItemChanged
        RaiseEvent FieldRowChanged(Me, e)
    End Sub

    Private Sub CopyToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripButton.Click
        Clipboard.SetDataObject(Me.DataGridView1.GetClipboardContent())
    End Sub

    Private _inPasteRows As Boolean

    Private Sub PasteRows()
        If _inPasteRows Then Return
        Try
            _inPasteRows = True
            Me.BindingSource1.SuspendBinding()
            Me.CustomStringsDataSet.EnforceConstraints = False
            Me.DataGridView1.Enabled = False
            Main.PasteRows()
        Catch ex As Exception

        Finally
            Me.BindingSource1.CurrencyManager.Refresh()
            Me.BindingSource1.ResumeBinding()
            Me.DataGridView1.Enabled = True
            _inPasteRows = False
        End Try
    End Sub

    Private Sub PasteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripButton.Click
        Try
            Me.BindingSource1.SuspendBinding()
            PasteRows()
        Catch ex As Exception

        Finally
            Me.BindingSource1.CurrencyManager.Refresh()
            Me.BindingSource1.ResumeBinding()
        End Try
    End Sub
End Class
