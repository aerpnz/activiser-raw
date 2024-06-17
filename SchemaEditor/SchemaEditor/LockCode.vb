Imports System.ComponentModel

Public Enum LockCode
    <Description("Not locked")> None = 0
    <Description("Locked (read-only)")> Locked = 1
    <Description("Locked with Parent")> LockWithParent = 2
    <Description("Locked when sync'd")> LockedWhenSynchronised = 3
    <Description("Locked when flagged")> LockedWhenFlagged = 4
    <Description("Hide")> Hidden = 128
End Enum