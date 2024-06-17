Module GpsSupport

    Friend templateFilename As String
    Friend template As String
    Friend templateUrl As String

    Friend Function GetDeviceTrackingHtml() As String
        If My.Computer.FileSystem.FileExists(My.Settings.DeviceTrackingTemplateFile) Then
            templateFilename = My.Settings.DeviceTrackingTemplateFile
        ElseIf My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CombinePath("HtmlTemplates", My.Settings.DeviceTrackingTemplateFile)) Then
            templateFilename = My.Computer.FileSystem.GetFileInfo(My.Computer.FileSystem.CombinePath("HtmlTemplates", My.Settings.DeviceTrackingTemplateFile)).FullName
        Else
            templateFilename = String.Empty
            template = String.Empty
            templateUrl = "About:Blank"
            Return Nothing
        End If

        template = My.Computer.FileSystem.ReadAllText(templateFilename)
        If templateFilename.StartsWith("http:", StringComparison.InvariantCultureIgnoreCase) OrElse templateFilename.StartsWith("https:", StringComparison.InvariantCultureIgnoreCase) Then
            templateUrl = New Uri(templateFilename).ToString()
        Else
            Dim templateInfo As New IO.FileInfo(templateFilename)
            templateUrl = New Uri(templateInfo.FullName).ToString()
        End If

        Return template
    End Function

    Friend Sub LoadTemplate(ByVal browser As WebBrowser)
        GetDeviceTrackingHtml()
        If String.IsNullOrEmpty(template) Then Throw New IO.FileNotFoundException("template is missing or empty")

        browser.Navigate(templateUrl)
    End Sub

    Friend Sub ClearMap(ByVal browser As WebBrowser)
        If browser.Document Is Nothing Then Return
        If browser.DocumentText <> template Then
            browser.Navigate(templateUrl)
        End If
        browser.Document.InvokeScript("ClearMap")
    End Sub

    Friend Sub DisplayMap(ByVal browser As WebBrowser, ByVal latitude As Double, ByVal longitude As Double, ByVal pinTitle As String, ByVal note As String)
        Try
            If browser.Document Is Nothing Then Return

            'LoadTemplate(browser)
            If String.IsNullOrEmpty(browser.DocumentText) OrElse browser.DocumentText <> template Then
                GetDeviceTrackingHtml()
                browser.Navigate(templateUrl)
            End If

            TraceVerbose("Setting map to lat/long {0}/{1}", latitude, longitude)
            browser.Document.InvokeScript("ShowMap", New Object() {latitude, longitude})

            If Not String.IsNullOrEmpty(pinTitle) OrElse Not String.IsNullOrEmpty(note) Then
                TraceVerbose("Adding pin to map to lat/long {0}/{1}", latitude, longitude)
                browser.Document.InvokeScript("AddPin", New Object() {latitude, longitude, pinTitle, note.Replace(vbCrLf, "<br>").Replace(vbCr, "<br>").Replace(vbLf, "<br>")})
            End If

        Catch ex As Exception
            AddErrorTag(browser, ex, latitude, longitude, pinTitle)
        End Try
    End Sub

    Friend Sub AddPin(ByVal browser As WebBrowser, ByVal latitude As Double, ByVal longitude As Double, ByVal pinTitle As String, ByVal note As String)
        Try
            If String.IsNullOrEmpty(browser.DocumentText) OrElse browser.DocumentText <> template Then
                Throw New ArgumentException("Browser not loaded with map template")
            End If
            browser.Document.InvokeScript("AddPin", New Object() {latitude, longitude, pinTitle, note.Replace(vbCrLf, "<br>").Replace(vbCr, "<br>").Replace(vbLf, "<br>")})

        Catch ex As Exception
            AddErrorTag(browser, ex, latitude, longitude, pinTitle)
        End Try
    End Sub

    Private Sub AddErrorTag(ByVal browser As WebBrowser, ByVal ex As Exception, ByVal latitude As Double, ByVal longitude As Double, ByVal pinTitle As String)
        If String.IsNullOrEmpty(pinTitle) Then pinTitle = "&LT;Not supplied&GT;"
        Dim message As String = String.Format("Error displaying map for location {0}, {1}; pin title: {2}<br>{3}", _
                latitude, longitude, pinTitle, _
                ex.ToString())
        If browser.Document IsNot Nothing Then
            Dim tags As HtmlElementCollection = browser.Document.GetElementsByTagName("Body")
            If tags IsNot Nothing AndAlso tags.Count <> 0 Then
                Dim messageTag As HtmlElement = browser.Document.CreateElement("P")
                messageTag.InnerHtml = message
                tags(0).AppendChild(messageTag)
                Return
            End If
        End If
        browser.DocumentText = String.Format("<html><body><p>Error displaying map for location {0}, {1}; pin title: {2}<br>{3}</p></body></html>", _
                latitude, longitude, pinTitle, _
                ex.ToString())
    End Sub
End Module
