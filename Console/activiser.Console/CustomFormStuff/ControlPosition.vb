
<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32")> Public Enum ControlPosition As Byte ' implemented as char(1) in database
    Undefined = 0
    FullWidth = Asc("F"c)
    Left = Asc("L"c)
    Right = Asc("R"c)
End Enum
