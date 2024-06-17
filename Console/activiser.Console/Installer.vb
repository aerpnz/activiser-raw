Imports System.ComponentModel
Imports System.Configuration.Install

Public Class Installer

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent

    End Sub

    Private Sub Installer_AfterInstall(ByVal sender As Object, ByVal e As System.Configuration.Install.InstallEventArgs) Handles Me.AfterInstall
        If Not String.IsNullOrEmpty(Context.Parameters("SERVERURL")) Then
            My.Settings.activiserServerUrl = Context.Parameters("SERVERURL")
            My.Settings.Save()

            Dim appSettings As New Xml.XmlDocument()
            Dim appConfigFileName As String = IO.Path.Combine(My.Application.Info.DirectoryPath, My.Application.Info.AssemblyName & ".exe.config")
            Dim appConfigFI As New IO.FileInfo(appConfigFileName)
            If Not appConfigFI.Exists Then
                Throw New IO.FileNotFoundException("Unable to save server location", appConfigFileName)
            End If
            If appConfigFI.IsReadOnly Then
                appConfigFI.IsReadOnly = False
            End If
            ' MsgBox(appConfigFileName)
            appSettings.Load(appConfigFileName)
            Dim serverUrlNode As Xml.XmlNode
            Dim valueNode As Xml.XmlNode
            Try
                serverUrlNode = appSettings.SelectSingleNode("/configuration/userSettings/activiser.Console.My.MySettings/setting[@name=""activiserServerUrl""]")
                If serverUrlNode IsNot Nothing Then
                    valueNode = serverUrlNode.SelectSingleNode("value")
                    If valueNode Is Nothing Then
                        valueNode = serverUrlNode.AppendChild(appSettings.CreateElement("value"))
                    End If
                    valueNode.InnerText = My.Settings.activiserServerUrl
                    Dim comment As Xml.XmlComment = appSettings.CreateComment(String.Format("server Url specified by setup at {0}: {1}", DateTime.UtcNow.ToString("u"), My.Settings.activiserServerUrl))
                    serverUrlNode.AppendChild(comment)
                    Dim xw As New Xml.XmlTextWriter(appConfigFileName, Nothing)
                    xw.Formatting = Xml.Formatting.Indented
                    appSettings.Save(xw)
                End If
            Catch ex As Exception
                MsgBox(ex.StackTrace)
                Throw
            End Try


            '<configuration>
            ' <userSettings>
            ' <activiser.Console.My.MySettings>
            '   <setting name="activiserServerUrl" serializeAs="String">
            '     <value>http://172.27.172.130/activiser/activiser.asmx</value>
            '   </setting>
            ' </activiser.Console.My.MySettings>
            ' </userSettings>
            '</configuration>
        Else
            Dim args As String = String.Empty
            For Each de As DictionaryEntry In Context.Parameters
                args &= CStr(de.Key) & "=" & CStr(de.Value) & " "
            Next
            InputBox("Context", "", args)
            Throw New ArgumentException("Server URL not passed to installer")
        End If
    End Sub
End Class
