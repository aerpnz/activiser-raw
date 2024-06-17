Imports Microsoft.VisualBasic.FileIO

Module Main
    Friend ReplaceOnPaste As Boolean
    Friend DoToolTips As Boolean

    Friend CustomStringsDataSet As CustomStringsDataSet
    Friend CurrentForm As CustomStringsDataSet.StringModuleRow

    Friend Sub PasteRows()
        If CustomStringsDataSet Is Nothing Then Return

        Dim rows As List(Of String()) = GetClipboardData()
        If rows IsNot Nothing AndAlso rows.Count <> 0 Then


            Dim duplicatesIgnored As Integer

            For Each fields As String() In rows
                ' Dim fields() As String = row.Split(New String() {vbTab}, 2, StringSplitOptions.None)
                If fields.Length < 2 Then Continue For

                Dim StringName As String = fields(0).Trim
                If StringName = String.Empty Then Continue For
                If StringName.StartsWith(">>") Then Continue For


                Dim value As String = fields(1).Trim
                Try
                    If StringName.Contains("."c) Then
                        If StringName.EndsWith(".ToolTip") Then ' real tool tip
                            StringName = StringName.Substring(0, StringName.IndexOf(".")) & "ToolTip"
                        ElseIf StringName.EndsWith(".Text") OrElse StringName.EndsWith(".HeaderText") Then
                            StringName = StringName.Substring(0, StringName.IndexOf("."))
                        Else
                            Continue For
                        End If
                        If StringName = "$this" Then
                            StringName = "$FormTitle"
                        End If
                        'If StringName = "ServerUrlTextBoxToolTip" Then Stop
                    ElseIf StringName.StartsWith(CurrentForm.ModuleName) Then
                        StringName = StringName.Substring(CurrentForm.ModuleName.Length)
                        '                   Else
                        '                        Continue For
                    End If
                    Dim ttName As String = StringName & "ToolTip"
                    If value.EndsWith(":") Then value = value.Substring(0, value.Length - 1)

                    If StringName.Length <> 0 AndAlso value.Length <> 0 Then
                        Debug.WriteLine(String.Format("adding field '{0}', default value '{1}', for form {2}", StringName, value, CurrentForm.ModuleName))
                        Dim newFieldRow As CustomStringsDataSet.StringValueRow

                        newFieldRow = CustomStringsDataSet.StringValue.FindByModuleNameStringNameClientKeyLanguageId(CurrentForm.ModuleName, StringName, CurrentForm.ClientKey, CurrentForm.LanguageId)
                        If newFieldRow Is Nothing Then
                            newFieldRow = CustomStringsDataSet.StringValue.NewStringValueRow
                            newFieldRow.ModuleName = CurrentForm.ModuleName
                            newFieldRow.StringName = StringName
                            newFieldRow.DefaultValue = value
                            newFieldRow.ClientKey = CurrentForm.ClientKey
                            newFieldRow.LanguageId = CurrentForm.LanguageId
                            CustomStringsDataSet.StringValue.AddStringValueRow(newFieldRow)
                        Else
                            If ReplaceOnPaste Then
                                newFieldRow.DefaultValue = value
                            Else
                                duplicatesIgnored += 1
                            End If
                        End If

                        If DoToolTips AndAlso Not StringName.EndsWith("ToolTip") Then ' add a pseudo-tooltip
                            StringName &= "ToolTip"
                            newFieldRow = CustomStringsDataSet.StringValue.FindByModuleNameStringNameClientKeyLanguageId(CurrentForm.ModuleName, StringName, CurrentForm.ClientKey, CurrentForm.LanguageId)
                            If newFieldRow Is Nothing Then
                                newFieldRow = CustomStringsDataSet.StringValue.NewStringValueRow
                                newFieldRow.ModuleName = CurrentForm.ModuleName
                                newFieldRow.StringName = ttName
                                newFieldRow.DefaultValue = value
                                newFieldRow.ClientKey = CurrentForm.ClientKey
                                newFieldRow.LanguageId = CurrentForm.LanguageId
                                CustomStringsDataSet.StringValue.AddStringValueRow(newFieldRow)
                            Else
                                duplicatesIgnored += 1
                            End If
                        End If
                    End If
                Catch ex As Exception
                    Debug.WriteLine(ex.Message)
                End Try
            Next
            Try
                If duplicatesIgnored <> 0 Then
                    MsgBox(String.Format("{0} duplicates ignored", duplicatesIgnored))
                End If
                CustomStringsDataSet.EnforceConstraints = True

            Catch ex As Data.ConstraintException
                MsgBox("One or more added rows have errors, please correct immediately")
            End Try
        End If
    End Sub

    Function GetClipboardData() As List(Of String())
        If Not (Clipboard.ContainsText(TextDataFormat.UnicodeText) OrElse Clipboard.ContainsText(TextDataFormat.Text)) Then
            Return Nothing
        End If
        Dim clipText As String = Clipboard.GetText()
        Dim sr As New IO.StringReader(clipText)

        Dim result As New List(Of String())

        Using parser As New TextFieldParser(sr)
            parser.TextFieldType = FieldType.Delimited
            parser.Delimiters = New String() {vbTab}
            parser.HasFieldsEnclosedInQuotes = True
            Dim row As String()
            While Not parser.EndOfData
                Try
                    Debug.WriteLine(parser.PeekChars(20))
                    row = parser.ReadFields()
                    result.Add(row)
                Catch mlex As MalformedLineException
                    Continue While
                    ' Return Nothing
                    ' Throw New FormatException("Unable to parse data", mlex)
                End Try
            End While
        End Using

        Return result
    End Function


End Module
