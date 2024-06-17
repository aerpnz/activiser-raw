Public Class StdOleImageConverter
    Inherits System.Windows.Forms.AxHost

    Public Sub New()
        MyBase.New("{63109182-966B-4e3c-A8B2-8BC4A88D221C}")
    End Sub

    Public Shared Function GetIPictureDispFromImage(ByVal objImage As System.Drawing.Image) As stdole.IPictureDisp
        Return CType(AxHost.GetIPictureDispFromPicture(objImage), stdole.IPictureDisp)
    End Function
End Class
