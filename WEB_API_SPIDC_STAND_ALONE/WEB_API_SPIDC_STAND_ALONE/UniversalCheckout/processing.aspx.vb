Public Class processing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

        Else
            Dim action = Request("__EVENTTARGET")
            Dim val = Request("__EVENTARGUMENT")

            If action = "Processing" Then
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('GCASH');", True)
                Dim test As String = _payloadToProcess.Value
            Else
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('OTHER PAYMENT METHOD');", True)
            End If

        End If



        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert(localStorage.getItem('payloadToProcess'));", True)
        '' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "document.getElementById('<%= _payloadToProcess.ClientID%>').value='new data'", True)
        'Dim test As String = _payloadToProcess.Value
        'Dim test2 As String = "data to"


    End Sub

End Class