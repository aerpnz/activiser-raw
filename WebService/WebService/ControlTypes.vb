
' <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32")> _
Public Enum ControlTypes ' As Byte ' implemented as tinyint in database
    Undefined = 0
    TextBox = 1
    CheckBox = 2
    [Date] = 3
    Time = 4
    DateTime = 5
    Number = 6
    List = 7
End Enum