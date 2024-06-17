Imports System.IO
Imports System.Reflection
Imports System.Xml

''' <summary>
''' Custom class that simulates the operation of System.Configuration.ConfigurationSettings.AppSettings
''' with the added benefit of allowing saving of the config as well.
''' </summary>
''' <remarks></remarks>
<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors")> _
Public NotInheritable Class ConfigurationSettings
    Private Const MODULENAME As String = "Configuration"
    Private Const sectionTag As String = "appSettings"
    Private Const RES_ConfigSaveError As String = "ConfigSaveError"
    Private Shared assemblyPath As String = [Assembly].GetCallingAssembly().GetName().CodeBase
    Private Shared configFileUrl As String = assemblyPath + ".config"
    Private Shared configDoc As XmlDocument
    Private Shared htAppsettings As Collections.Generic.Dictionary(Of String, String) = loadSettings() ' As New Collections.Generic.Dictionary(Of String, String) ' Hashtable(10)

    Private Sub New()

    End Sub

    Private Shared Function loadSettings() As Collections.Generic.Dictionary(Of String, String)
        Const METHODNAME As String = "loadSettings"
        Dim result As New Collections.Generic.Dictionary(Of String, String)
        Try
            If configDoc Is Nothing Then
                Dim fi As New IO.FileInfo(configFileUrl)
                If Not fi.Exists Then
                    Throw New FileNotFoundException()
                End If
                If (fi.Attributes And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then
                    fi.Attributes = fi.Attributes And (Not FileAttributes.ReadOnly)
                End If
                configDoc = New XmlDocument()
                configDoc.Load(configFileUrl)

                'Get descendant nodes within the 'sectionTag' tags
                Dim nodes As XmlNodeList = configDoc.GetElementsByTagName(sectionTag)
                Dim node As XmlNode
                For Each node In nodes
                    If node.NodeType <> XmlNodeType.Element Then Continue For
                    Dim childnode As XmlNode
                    For Each childnode In node.ChildNodes
                        If childnode.NodeType <> XmlNodeType.Element Then Continue For
                        Dim attributes As XmlAttributeCollection = childnode.Attributes
                        result.Add(attributes("key").Value, attributes("value").Value)
                    Next childnode
                Next node
            End If

        Catch ex As FileNotFoundException
            LogError(MODULENAME, METHODNAME, ex, True, RES_ConfigurationFileMissing)
        Catch ex As XmlException
            LogError(MODULENAME, METHODNAME, ex, True, RES_ConfigurationFileError)
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, True, RES_ConfigurationFileLoadError)
        End Try
        Return result
    End Function

    'Public Shared ReadOnly Property AppSettings() As Collections.Generic.Dictionary(Of String, String)
    '    Get
    '        ' LoadConfig()
    '        Return htAppsettings
    '    End Get
    'End Property

    Public Shared Function GetValue(ByVal key As String, ByVal defaultValue As String) As String
        If htAppsettings.ContainsKey(key) Then
            Return htAppsettings.Item(key)
        Else
            Return defaultValue
        End If
    End Function

    Public Shared Sub SetValue(ByVal key As String, ByVal value As String)
        Const METHODNAME As String = "SetStringValue"
        Try
            If htAppsettings.ContainsKey(key) Then
                htAppsettings.Remove(key)
            End If
            If value Is Nothing Then value = String.Empty
            htAppsettings.Add(key, value)
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, RES_KeyValuePairDisplayFormat, key, value)
        End Try
    End Sub

    Public Shared Function GetValue(ByVal key As String, ByVal defaultValue As Integer) As Integer
        Dim result As Integer = defaultValue
        If htAppsettings.ContainsKey(key) Then
            Try
                result = Integer.Parse(htAppsettings.Item(key), WithoutCulture)
            Catch
            End Try
        End If
        Return result
    End Function

    Public Shared Sub SetValue(ByVal key As String, ByVal value As Integer)
        Const METHODNAME As String = "SetIntegerValue"
        Try
            If htAppsettings.ContainsKey(key) Then
                htAppsettings.Remove(key)
            End If
            htAppsettings.Add(key, value.ToString(CultureInfo.InvariantCulture))
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, RES_KeyValuePairDisplayFormat, key, value)
        End Try
    End Sub

    Public Shared Function GetValue(ByVal key As String, ByVal defaultValue As Boolean) As Boolean
        Dim result As Boolean = defaultValue
        If htAppsettings.ContainsKey(key) Then
            Try
                result = Boolean.Parse(htAppsettings.Item(key))
            Catch
            End Try
        End If
        Return result
    End Function

    Public Shared Sub SetValue(ByVal key As String, ByVal value As Boolean)
        Const METHODNAME As String = "SetBooleanValue"
        Try
            If htAppsettings.ContainsKey(key) Then
                htAppsettings.Remove(key)
            End If
            htAppsettings.Add(key, value.ToString(CultureInfo.InvariantCulture))
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, RES_KeyValuePairDisplayFormat, key, value)
        End Try
    End Sub

    Public Shared Function GetValue(ByVal key As String, ByVal defaultValue As Date) As Date
        Dim result As Date = defaultValue
        If htAppsettings.ContainsKey(key) Then
            Try
                Dim xmlDate As String = htAppsettings.Item(key)
                If String.IsNullOrEmpty(xmlDate) Then Return defaultValue
                result = Date.ParseExact(xmlDate, "o", CultureInfo.InvariantCulture)
            Catch
            End Try
        End If
        Return result
    End Function

    Public Shared Sub SetValue(ByVal key As String, ByVal value As Date)
        Const METHODNAME As String = "SetDateValue"
        Try
            If htAppsettings.ContainsKey(key) Then
                htAppsettings.Remove(key)
            End If
            htAppsettings.Add(key, value.ToString("o", CultureInfo.InvariantCulture))
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, RES_KeyValuePairDisplayFormat, key, value)
        End Try
    End Sub

    Public Shared Function GetValue(ByVal key As String, ByVal defaultValue As Color) As Color
        Dim result As Color = defaultValue
        If htAppsettings.ContainsKey(key) Then
            Try
                result = Color.FromArgb(Integer.Parse(htAppsettings.Item(key), NumberStyles.HexNumber, WithoutCulture))
            Catch
            End Try
        End If
        Return result
    End Function

    Public Shared Sub SetValue(ByVal key As String, ByVal value As Color)
        Const METHODNAME As String = "SetColorValue"
        Try
            If htAppsettings.ContainsKey(key) Then
                htAppsettings.Remove(key)
            End If
            htAppsettings.Add(key, Hex(value.ToArgb))
        Catch ex As Exception
            LogError(MODULENAME, METHODNAME, ex, False, RES_KeyValuePairDisplayFormat, key, value)
        End Try
    End Sub

    Public Shared Property Item(ByVal key As String) As String
        Get
            If htAppsettings.ContainsKey(key) Then
                Return htAppsettings.Item(key)
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            If htAppsettings.ContainsKey(key) Then
                htAppsettings.Item(key) = value
            Else
                htAppsettings.Add(key, value)
            End If
        End Set
    End Property


    Public Shared Sub Save()
        Const METHODNAME As String = "Save"
        Dim nodes As XmlNodeList = configDoc.GetElementsByTagName(sectionTag)
        Dim node As XmlNode
        For Each node In nodes
            If node.Name = "appSettings" Then
                node.RemoveAll()
                For Each kv As Generic.KeyValuePair(Of String, String) In htAppsettings
                    Dim newNode As XmlNode
                    newNode = configDoc.CreateNode(XmlNodeType.Element, "add", configDoc.NamespaceURI)
                    Dim keyAttribute As XmlAttribute = configDoc.CreateAttribute("key")
                    Dim valueAttribute As XmlAttribute = configDoc.CreateAttribute("value")
                    keyAttribute.Value = kv.Key
                    If kv.Value Is Nothing Then
                        valueAttribute.Value = String.Empty
                    Else
                        valueAttribute.Value = kv.Value
                    End If
                    newNode.Attributes.Append(keyAttribute)
                    newNode.Attributes.Append(valueAttribute)
                    node.AppendChild(newNode)
                Next
            End If
        Next node

        If Not configDoc Is Nothing Then
            Try
                configDoc.Save(configFileUrl)
            Catch ex As Exception
                LogError(MODULENAME, METHODNAME, ex, True, RES_ConfigSaveError)
            End Try
        End If
    End Sub
End Class
