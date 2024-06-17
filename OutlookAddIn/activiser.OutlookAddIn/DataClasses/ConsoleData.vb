Public Module ConsoleData
    Private Const MODULENAME As String = "ConsoleData"

    Friend ReadOnly ApplicationGuid As New Guid("35060af8-9b60-45a9-ac93-5dc6711af0cd")

    Friend WithEvents WebService As Library.activiserWebService.activiser

    Friend ConsoleUser As Library.activiserWebService.Utility.UserRow

End Module
