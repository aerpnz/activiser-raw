
Public Enum LabelPosition As Byte ' implemented as char(1) in database
    Undefined = 0
    Left = Asc("L")
    Right = Asc("R")
    Top = Asc("T")
    None = Asc("N")
End Enum
