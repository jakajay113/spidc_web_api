Public Class index2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim Spidc_Web_API_Config As New Spidc_Web_API_Config

        Spidc_Web_API_Config.WebApiConfig()

        Dim testdata As String = Spidc_Web_API_Config._mAppSequenceLabel

        Response.Write(testdata)
    End Sub

End Class