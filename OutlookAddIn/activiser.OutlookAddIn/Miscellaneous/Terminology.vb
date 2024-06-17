Option Strict On
Option Explicit On
Option Compare Text

Imports activiser.Library.activiserWebService

Public NotInheritable Class Terminology
    Friend Const ClientKey As Integer = 3
    Private Shared _stringList As New Generic.Dictionary(Of String, Collections.Specialized.StringDictionary)
    Private Shared _sharedMessages As New Collections.Specialized.StringDictionary

    Private Shared _dataSet As New LanguageDataSet

    Private Sub New()

    End Sub

    Private Shared Sub primeStrings()
        _stringList.Clear()
        _sharedMessages.Clear()
        For Each lr As LanguageDataSet.StringValueRow In _dataSet.StringValue
            If lr.ModuleName = My.Resources.SharedMessagesKey Then
                If _sharedMessages.ContainsKey(lr.StringName) Then
                    _sharedMessages.Item(lr.StringName) = lr.Value
                Else
                    _sharedMessages.Add(lr.StringName, lr.Value)
                End If
            Else
                Dim fd As Specialized.StringDictionary
                If _stringList.ContainsKey(lr.ModuleName) Then
                    fd = _stringList.Item(lr.ModuleName)
                Else
                    fd = New Specialized.StringDictionary()
                    _stringList.Add(lr.ModuleName, fd)
                End If
                If Not fd.ContainsKey(lr.StringName) Then
                    fd.Add(lr.StringName, lr.Value)
                End If
            End If
        Next
    End Sub

    Shared Sub Load(ByVal ds As LanguageDataSet)
        _dataSet.Clear()
        If ds IsNot Nothing Then
            Merge(ds)
        End If
        primeStrings()
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")> _
    Shared Sub Merge(ByVal ds As LanguageDataSet)
        If ds IsNot Nothing Then
            _dataSet.Merge(ds)
        End If
    End Sub

    Private Shared Sub LoadChildLabels(ByVal parent As Control, ByRef sd As Specialized.StringDictionary, Optional ByVal tt As ToolTip = Nothing)
        For Each c As Control In parent.Controls
            'Debug.WriteLine(String.Format("checking for custom term entry for control '{0}'", c.Name))
            If Not String.IsNullOrEmpty(c.Name) Then
                If sd.ContainsKey(c.Name) Then
                    Dim newText As String = sd.Item(c.Name)
                    Try
                        c.Text = newText
                    Catch
                        'c.Text = originalText
                    End Try
                Else
                    Dim dgv As DataGridView = TryCast(c, DataGridView)
                    If dgv IsNot Nothing Then
                        For Each dgc As DataGridViewColumn In dgv.Columns
                            If sd.ContainsKey(dgc.Name) Then
                                dgc.HeaderText = sd.Item(dgc.Name)
                            End If
                        Next
                    End If
                End If
                If tt IsNot Nothing Then
                    Dim ttText As String = String.Format("{0}ToolTip", c.Name)
                    If sd.ContainsKey(ttText) Then
                        Dim newText As String = sd.Item(ttText)
                        tt.SetToolTip(c, newText)
                    End If
                End If
            End If

            Dim ts As ToolStrip = TryCast(c, ToolStrip)
            If ts IsNot Nothing Then
                For Each tsi As ToolStripItem In ts.Items
                    Dim ttText As String = String.Format("{0}ToolTip", tsi.Name)
                    If sd.ContainsKey(ttText) Then
                        Dim newText As String = sd.Item(ttText)
                        tsi.ToolTipText = newText
                    End If
                Next
            End If

            If c.HasChildren Then
                If _stringList.ContainsKey(c.Name) Then
                    Dim childSD As Specialized.StringDictionary = _stringList(c.Name)
                    LoadChildLabels(c, childSD, getToolTip(c))
                Else
                    LoadChildLabels(c, sd, getToolTip(c))
                End If
            End If

        Next
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")> _
    Public Shared Sub LoadLabels(ByVal parent As Control)

        If _stringList IsNot Nothing Then
            If _stringList.ContainsKey(parent.Name) Then
                Dim SD As Specialized.StringDictionary = _stringList.Item(parent.Name)
                If SD IsNot Nothing Then
                    If SD.ContainsKey("$FormTitle") Then
                        parent.Text = SD.Item("$FormTitle")
                    End If
                    If Not parent.HasChildren Then Exit Sub
                    LoadChildLabels(parent, SD, getToolTip(parent))
                End If
            End If
        End If
    End Sub

    Private Shared Function getToolTip(ByVal target As Control) As ToolTip
        Dim objPIs() As Reflection.PropertyInfo
        objPIs = target.GetType().GetProperties(Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.Instance)

        For Each pi As Reflection.PropertyInfo In objPIs
            If pi.PropertyType.ToString = GetType(Windows.Forms.ToolTip).FullName Then
                Return CType(target.GetType().GetProperty(pi.Name, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.Instance).GetValue(target, New Object() {}), ToolTip)
            End If
        Next

        Return Nothing
    End Function

    Private Shared Function FindChild(ByVal parent As Control, ByVal childName As String) As Control
        If parent.Controls.ContainsKey(childName) Then
            Return parent.Controls(childName)
        Else
            Dim result As Control = Nothing
            For Each c As Control In parent.Controls
                If c.HasChildren Then
                    result = FindChild(c, childName)
                    If result IsNot Nothing Then
                        Return result
                    End If
                End If
            Next
            Return Nothing
        End If
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")> _
    Public Shared Sub LoadToolTips(ByVal parent As Control, ByVal toolTipProvider As ToolTip)
        If Not parent.HasChildren Then Exit Sub

        If _stringList IsNot Nothing Then
            If _stringList.ContainsKey(parent.Name) Then
                Dim fd As Specialized.StringDictionary = _stringList.Item(parent.Name)
                If fd IsNot Nothing Then
                    For Each key As String In fd.Keys
                        If key.EndsWith("ToolTip", StringComparison.OrdinalIgnoreCase) Then
                            Dim childName As String = key.Substring(0, key.IndexOf("ToolTip", StringComparison.OrdinalIgnoreCase))
                            Dim c As Control = FindChild(parent, childName)
                            If c IsNot Nothing Then
                                ToolTipProvider.SetToolTip(c, fd.Item(key)) '  nvp.Value)
                            End If
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Public Shared Function GetString(ByVal moduleName As String, ByVal stringName As String) As String
        Dim result As String = String.Empty
        Dim fd As Specialized.StringDictionary
        If moduleName = My.Resources.SharedMessagesKey Then ' explicitly shared list.
            fd = _sharedMessages
        ElseIf _stringList.ContainsKey(moduleName) AndAlso _stringList.Item(moduleName).ContainsKey(stringName) Then ' explicit private
            fd = _stringList.Item(moduleName)
        ElseIf _sharedMessages.ContainsKey(stringName) Then ' implicitly shared list.
            fd = _sharedMessages
        ElseIf _stringList.ContainsKey(moduleName) Then ' implicitly private
            fd = _stringList.Item(moduleName)
        Else ' implicitly private
            fd = New Specialized.StringDictionary
            _stringList.Add(moduleName, fd)
        End If

        If fd.ContainsKey(stringName) Then
            Return fd.Item(stringName)
        End If

        Try
            result = My.Resources.ResourceManager.GetString(moduleName & stringName)
            If result Is Nothing Then result = My.Resources.ResourceManager.GetString(stringName)
            If result Is Nothing Then result = String.Format(WithoutCulture, My.Resources.StringNotFound, moduleName, stringName)
        Catch ex As Exception
            result = stringName
        End Try
        ' cache result
        fd.Add(stringName, result)
        Return result
    End Function

    Public Shared Function GetFormattedString(ByVal formName As String, ByVal messageId As String, ByVal ParamArray args() As Object) As String
        Dim format As String = GetString(formName, messageID)
        If Not String.IsNullOrEmpty(format) Then
            Return String.Format(format, args)
        Else
            Return String.Empty
        End If
    End Function

    Public Shared Sub DisplayMessage(ByVal formName As String, ByVal messageId As String, ByVal style As MessageBoxIcon)
        MessageBox.Show(GetString(formName, messageID), My.Resources.activiserFormTitle, MessageBoxButtons.OK, style, MessageBoxDefaultButton.Button1)
    End Sub

    Public Shared Sub DisplayFormattedMessage(ByVal formName As String, ByVal messageId As String, ByVal style As MessageBoxIcon, ByVal ParamArray args() As Object)
        MessageBox.Show(GetFormattedString(formName, messageID, args), My.Resources.activiserFormTitle, MessageBoxButtons.OK, style, MessageBoxDefaultButton.Button1)
    End Sub

    Public Shared Function AskQuestion(ByVal formName As String, ByVal messageId As String, ByVal buttons As MessageBoxButtons, ByVal defaultButton As MessageBoxDefaultButton) As DialogResult
        Return MessageBox.Show(GetString(formName, messageID), My.Resources.activiserFormTitle, buttons, MessageBoxIcon.Question, defaultButton)
    End Function

    Public Shared Function AskFormattedQuestion(ByVal formName As String, ByVal messageId As String, ByVal buttons As MessageBoxButtons, ByVal defaultButton As MessageBoxDefaultButton, ByVal ParamArray args() As Object) As DialogResult
        Return MessageBox.Show(GetFormattedString(formName, messageID, args), My.Resources.activiserFormTitle, buttons, MessageBoxIcon.Question, defaultButton)
    End Function
End Class
