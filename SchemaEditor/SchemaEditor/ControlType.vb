Imports System.ComponentModel

Public Enum ControlType
    <Description("Type not defined")> Undefined
    <Description("Free text input box")> TextBox
    <Description("Yes/No check box")> CheckBox
    <Description("Date picker")> [Date]
    <Description("Time picker")> Time
    <Description("Date/Time picker")> DateTime
    <Description("Numeric up/down")> Number
    <Description("Drop-down list")> DropDownList
    <Description("Auto-completing text box")> AutoCompleteList ' TODO
    <Description("Auto-completing text box with lookup button")> LookupButtonList ' TODO
    <Description("Readonly lookup")> ReadonlyLookup ' TODO
    <Description("Image browser")> ImageBrowser ' TODO
    <Description("File browser")> FileBrowser ' TODO
    <Description("Contact browser")> ContactBrowser ' TODO
End Enum

