<Flags()> _
    Public Enum SchemaClients
    None = 0
    MobileClientPublic = 1
    MobileClientPrivate = 2
    ConsolePublic = 4
    ConsolePrivate = 8
    WindowsClientPublic = 16
    WindowsClientPrivate = 32
    OutlookPublic = 64
    OutlookPrivate = 128

    Mobile = MobileClientPublic Or MobileClientPrivate
    Console = ConsolePublic Or ConsolePrivate
    WindowsClient = WindowsClientPublic Or WindowsClientPrivate
    Outlook = OutlookPublic Or OutlookPrivate

    ConsoleOrOutlook = Console Or Outlook

    ConsoleOrMobilePublic = ConsolePublic Or MobileClientPublic
    ConsoleOrMobilePrivate = ConsolePrivate Or MobileClientPrivate

    DesktopPublic = ConsolePublic Or OutlookPublic Or WindowsClientPublic
    DesktopPrivate = ConsolePrivate Or OutlookPrivate Or WindowsClientPrivate
    Desktop = DesktopPublic Or DesktopPrivate

    ClientPublic = MobileClientPublic Or WindowsClientPublic
    ClientPrivate = MobileClientPrivate Or WindowsClientPrivate
    Client = ClientPublic Or ClientPrivate

    All = &HFF
End Enum