Imports System.Net
Imports System.Web.Http
Imports System.Net.Http


Public Class WebhooksController
    Inherits ApiController
    'SPIDC Config
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config
    'Model
    Private Shared webhooksModel As New WebhooksModel
    'Other
    Private Shared _SpidcAPIResponse As New Spidc_Web_API_Standard_Response
    Private Shared _mResponse As HttpResponseMessage


    ' POST api/webhooks
    <HttpPost>
    Public Function ReceiveWebhook(ByVal param As String, <FromBody> data As Object) As IHttpActionResult
        'Web API Authorization Validation
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization(param) = "200" Then
            'Call The Model And API Standard Respone
            'Call the model and check 
            If webhooksModel._mPostValue(param, data) Then 'Success
                'Create an instance of the StandardApiResponse
                _SpidcAPIResponse.status = webhooksModel._mStatus 'Or "error" for unsuccessful responses
                _SpidcAPIResponse.data = webhooksModel._mData ' Data Objectr Array From Model
                _SpidcAPIResponse.message = webhooksModel._mMessage 'A descriptive message about the response,
                _SpidcAPIResponse.code = webhooksModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(_SpidcAPIResponse)
            Else 'Error
                _SpidcAPIResponse.status = CedulaModel._mStatus
                _SpidcAPIResponse.data = CedulaModel._mData
                _SpidcAPIResponse.message = CedulaModel._mMessage
                _SpidcAPIResponse.code = CedulaModel._mCode
                Return Content(HttpStatusCode.InternalServerError, _SpidcAPIResponse)
            End If

            'HTTP/1.1 401 Unauthorized
        ElseIf Spidc_Web_API_Authorization_Config._pAuthorization() = "401" Then
            _SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
            _SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
            _SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
            _SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
            Return Content(HttpStatusCode.Unauthorized, _SpidcAPIResponse)
        Else
            'HTTP/1.1 400 Bad Request
            _SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
            _SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
            _SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
            _SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
            Return Content(HttpStatusCode.Unauthorized, _SpidcAPIResponse)
        End If
    End Function

End Class