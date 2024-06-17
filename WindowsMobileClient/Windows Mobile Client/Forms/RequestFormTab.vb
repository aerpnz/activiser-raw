Public Enum RequestFormTab
    ' Important: must match tab order!
    Main = 0            ' = TabControl.TabPages.IndexOf(RequestForm.DescriptionTab)
    LongDescription = 1 ' = TabControl.TabPages.IndexOf(RequestForm.LongDescriptionTab)
    Jobs = 2            ' = TabControl.TabPages.IndexOf(RequestForm.JobTab)
    History = 3         ' = TabControl.TabPages.IndexOf(RequestForm.HistoryTab)
End Enum
