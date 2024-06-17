Option Strict On
Option Explicit On
Option Compare Text

Imports activiser.Library
Imports activiser.Library.WebService

Public NotInheritable Class Terminology
    Friend Const ClientKey As Integer = 1 ' WM5/6 Client.
    Private Shared _stringList As New Generic.Dictionary(Of String, Collections.Specialized.StringDictionary)
    Private Shared _sharedMessages As New Collections.Specialized.StringDictionary

    Private Shared _dataSet As New LanguageDataSet
    Private Shared _filename As String

    Private Sub New()

    End Sub

    'Shared Sub New()
    '    'Load()
    'End Sub

    Public Shared Property FileName() As String
        Get
            If String.IsNullOrEmpty(_filename) Then
                _filename = System.IO.Path.Combine(gDatabaseFolder, gTerminologyFileName & My.Resources.XmlFileType)
            End If
            Return _filename
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(value) Then
                If value.IndexOfAny(New Char() {System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar}) = -1 Then
                    _filename = System.IO.Path.Combine(gDatabaseFolder, value)
                Else
                    _filename = value
                End If
                If Not value.EndsWith(My.Resources.XmlFileType) Then _filename &= My.Resources.XmlFileType
            End If
        End Set
    End Property

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
            For Each cl As LanguageDataSet.StringValueRow In ds.StringValue
                Dim existingCl As LanguageDataSet.StringValueRow = _dataSet.StringValue.FindByStringID(cl.StringID)
                If existingCl IsNot Nothing Then
                    existingCl.Value = cl.Value
                Else
                    _dataSet.StringValue.ImportRow(cl)
                End If
            Next
            _dataSet.AcceptChanges()
            Save()
            primeStrings()
        End If
    End Sub

    Private Shared Sub bgSave()
        Try
            _dataSet.WriteXml(FileName)

        Catch ex As Exception

        End Try
    End Sub

    Shared Sub Save()
        Dim t As New Threading.Thread(AddressOf bgSave)
        t.Start()
    End Sub

    Shared Sub Load()
        If New IO.FileInfo(FileName).Exists Then
            _dataSet.ReadXml(FileName)
            primeStrings()
        End If
    End Sub

    Private Shared Sub LoadChildLabels(ByVal owner As Control, ByRef fd As Specialized.StringDictionary)
        For Each c As Control In owner.Controls
#If DEBUG Then
            Debug.WriteLine(String.Format("checking for custom term entry for control '{0}'", c.Name))
#End If
            If Not String.IsNullOrEmpty(c.Name) Then
                If fd.ContainsKey(c.Name) Then
                    Try
                        Dim newText As New System.Text.StringBuilder(fd.Item(c.Name))
                        If TypeOf c Is System.Windows.Forms.Label Then
                            If Not newText.Chars(newText.Length - 1) = ":"c Then
                                newText.Append(":"c)
                            End If
                        End If
                        c.Text = newText.ToString()
                    Catch
                        'c.Text = originalText
                    End Try
                End If
            End If

            If c.Controls.Count <> 0 Then
                LoadChildLabels(c, fd)
            End If
        Next
    End Sub

    Friend Shared Sub LoadMenuLabels(ByVal owner As Form, ByVal fd As Specialized.StringDictionary)
        Dim parentType As Type
        Dim formProperties() As System.Reflection.PropertyInfo
        Dim formProperty As System.Reflection.PropertyInfo

        parentType = owner.GetType
        formProperties = parentType.GetProperties(Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.CreateInstance Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.DeclaredOnly)

        For Each formProperty In formProperties
            If formProperty.PropertyType.FullName = GetType(Windows.Forms.MenuItem).FullName Then
                Dim menuCandidate As MenuItem = TryCast(formProperty.GetValue(owner, Nothing), MenuItem)
                If menuCandidate IsNot Nothing Then
                    Dim menuName As String = formProperty.Name
#If DEBUGTERMINOLOGY Then
                    Debug.WriteLine(String.Format("checking for custom term entry for menu item '{0}'", menuName))
#End If
                    If Not String.IsNullOrEmpty(menuName) Then
                        If fd.ContainsKey(menuName) Then
                            Dim newText As String = fd.Item(menuName)
                            Try
                                menuCandidate.Text = newText
                            Catch
                            End Try
                        End If
                    End If

                End If
            End If
        Next
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")> _
    Public Shared Sub LoadLabels(ByVal owner As Control)
        If _stringList IsNot Nothing Then
            If _stringList.ContainsKey(owner.Name) Then
                Dim fd As Specialized.StringDictionary = _stringList.Item(owner.Name)
                If fd IsNot Nothing Then
                    LoadChildLabels(owner, fd)

                    Dim f As Form = TryCast(owner, Form)
                    If f IsNot Nothing Then
                        LoadMenuLabels(f, fd)
                    End If
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

    Public Shared Function GetFormattedString(ByVal formName As String, ByVal messageID As String, ByVal ParamArray args() As Object) As String
        Dim format As String = GetString(formName, messageID)
        If Not String.IsNullOrEmpty(format) Then
            Return String.Format(WithCulture, format, args)
        Else
            Return String.Empty
        End If
    End Function

    Private Shared Function ShowDialogBox(ByVal db As DialogBox, ByVal owner As Form) As DialogResult
        Dim result As DialogResult
        result = db.ShowDialog
        db.Close()
        If owner IsNot Nothing Then
            owner.BringToFront()
            owner.Activate()
        End If
        Return result
    End Function

    Private Shared Function ShowInputBox(ByVal db As DialogBox, ByVal owner As Form) As String
        Dim result As String
        If db.ShowDialog = DialogResult.OK Then
            result = db.Input
        Else
            result = String.Empty
        End If
        db.Close()
        If owner IsNot Nothing Then
            owner.BringToFront()
            owner.Activate()
        End If
        Return result
    End Function

    Public Shared Sub DisplayMessage(ByVal owner As Form, ByVal formName As String, ByVal messageID As String, ByVal style As MessageBoxIcon)
        If Environment.OSVersion.Version.Major >= 5 Then
            Using f As New DialogBox(owner, GetString(formName, messageID), MessageBoxButtons.OK, IconList.Question)
                ShowDialogBox(f, owner)
            End Using
        Else
            MessageBox.Show(GetString(formName, messageID), Terminology.GetString(My.Resources.SharedMessagesKey, RES_ActiviserFormTitle), MessageBoxButtons.OK, style, MessageBoxDefaultButton.Button1)
        End If
    End Sub

    Public Shared Sub DisplayFormattedMessage(ByVal owner As Form, ByVal formName As String, ByVal messageID As String, ByVal style As MessageBoxIcon, ByVal ParamArray args() As Object)
        If Environment.OSVersion.Version.Major >= 5 Then
            Using f As New DialogBox(owner, GetFormattedString(formName, messageID, args), MessageBoxButtons.OK, IconList.Question)
                ShowDialogBox(f, owner)
            End Using
        Else
            MessageBox.Show(GetFormattedString(formName, messageID, args), Terminology.GetString(My.Resources.SharedMessagesKey, RES_ActiviserFormTitle), MessageBoxButtons.OK, style, MessageBoxDefaultButton.Button1)
        End If
    End Sub

    Public Shared Function AskQuestion(ByVal owner As Form, ByVal formName As String, ByVal messageID As String, ByVal buttons As MessageBoxButtons, ByVal defaultButton As MessageBoxDefaultButton) As DialogResult
        If Environment.OSVersion.Version.Major >= 5 Then
            Using f As New DialogBox(owner, GetString(formName, messageID), buttons, IconList.Question)
                Return ShowDialogBox(f, owner)
            End Using
        Else
            Return MessageBox.Show(GetString(formName, messageID), Terminology.GetString(My.Resources.SharedMessagesKey, RES_ActiviserFormTitle), buttons, MessageBoxIcon.Question, defaultButton)
        End If
    End Function

    Public Shared Function AskFormattedQuestion(ByVal owner As Form, ByVal formName As String, ByVal messageID As String, ByVal buttons As MessageBoxButtons, ByVal defaultButton As MessageBoxDefaultButton, ByVal ParamArray args() As Object) As DialogResult
        If Environment.OSVersion.Version.Major >= 5 Then
            Using f As New DialogBox(owner, GetFormattedString(formName, messageID, args), buttons, IconList.Question)
                Return ShowDialogBox(f, owner)
            End Using
        Else
            Return MessageBox.Show(GetFormattedString(formName, messageID, args), Terminology.GetString(My.Resources.SharedMessagesKey, RES_ActiviserFormTitle), buttons, MessageBoxIcon.Question, defaultButton)
        End If
    End Function

    Public Shared Function AskQuestion(ByVal owner As Form, ByVal formName As String, ByVal messageID As String, ByVal defaultValue As String) As String
        Return AskQuestion(owner, formName, messageID, defaultValue, True, 0)
    End Function

    Public Shared Function AskQuestion(ByVal owner As Form, ByVal formName As String, ByVal messageID As String, ByVal defaultValue As String, ByVal multiline As Boolean, ByVal maxLength As Integer) As String
        If Environment.OSVersion.Version.Major >= 5 Then
            Using f As New DialogBox(owner, GetString(formName, messageID), defaultValue, IconList.Question, multiline, maxLength)
                Return ShowInputBox(f, owner)
            End Using
        Else
            Return InputBox(GetString(formName, messageID), Terminology.GetString(My.Resources.SharedMessagesKey, RES_ActiviserFormTitle), defaultValue)
        End If
    End Function

    Public Shared Function AskFormattedQuestion(ByVal owner As Form, ByVal formName As String, ByVal messageID As String, ByVal defaultValue As String, ByVal multiline As Boolean, ByVal maxLength As Integer, ByVal ParamArray args() As Object) As String
        If Environment.OSVersion.Version.Major >= 5 Then
            Using f As New DialogBox(owner, GetFormattedString(formName, messageID, args), defaultValue, IconList.Question, multiline, maxLength)
                Return ShowInputBox(f, owner)
            End Using
        Else
            Return InputBox(GetFormattedString(formName, messageID, args), Terminology.GetString(My.Resources.SharedMessagesKey, RES_ActiviserFormTitle), defaultValue)
        End If
    End Function

    Public Shared Function GetMenuName(Of T)(ByVal Container As T) As String
        Dim containerType As Type
        Dim properties() As System.Reflection.PropertyInfo
        Dim PropActual As System.Reflection.PropertyInfo
        Dim MenuActual As Object

        containerType = Container.GetType
        properties = containerType.GetProperties(CType(Reflection.BindingFlags.NonPublic + _
        Reflection.BindingFlags.Public + _
        Reflection.BindingFlags.Instance + _
        Reflection.BindingFlags.DeclaredOnly, Reflection.BindingFlags))

        For Each PropActual In properties
            MenuActual = PropActual.GetValue(Container, Nothing)
            If TypeOf MenuActual Is MenuItem Then
                Return PropActual.Name
            End If
        Next
        Return String.Empty
    End Function
End Class
