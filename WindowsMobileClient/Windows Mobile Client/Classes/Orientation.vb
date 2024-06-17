Imports Microsoft.WindowsCE.Forms

Module Orientation
    Private Const MODULENAME As String = "Orientation"
    Public Sub SetOrientation(ByVal angle As ScreenOrientation)
        SystemSettings.ScreenOrientation = angle
    End Sub

    Public Sub NextOrientation()
        Const METHODNAME As String = "NextOrientation"
        Try
            Select Case SystemSettings.ScreenOrientation
                Case ScreenOrientation.Angle0
                    SetOrientation(ScreenOrientation.Angle90)
                Case ScreenOrientation.Angle90
                    SetOrientation(ScreenOrientation.Angle180)
                Case ScreenOrientation.Angle180
                    SetOrientation(ScreenOrientation.Angle270)
                Case ScreenOrientation.Angle270
                    SetOrientation(ScreenOrientation.Angle0)
            End Select
        Catch ex As Exception
            SetOrientation(ScreenOrientation.Angle0)
            Debug.WriteLine(String.Format(WithoutCulture, "{0}:{1}, {2}", MODULENAME, METHODNAME, ex.Message))
            LogError(MODULENAME, METHODNAME, ex, True, RES_OrientationChangeFailure)
        End Try
    End Sub
End Module
