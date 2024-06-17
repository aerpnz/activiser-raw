
Public Enum ControlPosition As Byte ' implemented as first char of char(2) in database - Control/Label
    Undefined = 0
    FullWidth = Asc("F"c)
    Left = Asc("L"c)
    Right = Asc("R"c)
End Enum
