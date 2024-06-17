''' <summary>
''' Custom form &amp; field lock conditions.
''' </summary>
''' <remarks>Implemented as a tinyint in the database structure</remarks>
<Flags()> _
Public Enum LockCodes As Int32 ' implemented as tinyint in database
    None = 0
    Locked = 1
    LockWithParent = 2
    LockedWhenSynchronised = 4
    LockedWhenFlagged = 8
    Hidden = 128
End Enum
