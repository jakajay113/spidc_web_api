Imports System.Net
Imports System.Web.Http
Imports System.Net.Http


Public Class UniversalCheckoutController
    Inherits ApiController
    'SPIDC Config
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config
    'Model
    Private Shared UniversalCheckoutModel As New UniversalCheckoutModel
    'Other
    Private Shared _SpidcAPIResponse As New Spidc_Web_API_Standard_Response
    Private Shared _mResponse As HttpResponseMessage

    'Get api/universal checkout
    <HttpGet>
    Public Function GetUniversalCheckoutPaymentMethod(ByVal param As String) As IHttpActionResult
        'Web API Authorization Validation
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization(param) = "200" Then
            'Call The Model And API Standard Respone
            'Call the model and check 
            If UniversalCheckoutModel._mGetValues(param) Then 'Success
                'Create an instance of the StandardApiResponse
                _SpidcAPIResponse.status = UniversalCheckoutModel._mStatus 'Or "error" for unsuccessful responses
                _SpidcAPIResponse.data = UniversalCheckoutModel._mData ' Data Objectr Array From Model
                _SpidcAPIResponse.message = UniversalCheckoutModel._mMessage 'A descriptive message about the response,
                _SpidcAPIResponse.code = UniversalCheckoutModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(_SpidcAPIResponse)
            Else 'Error
                _SpidcAPIResponse.status = UniversalCheckoutModel._mStatus
                _SpidcAPIResponse.data = UniversalCheckoutModel._mData
                _SpidcAPIResponse.message = UniversalCheckoutModel._mMessage
                _SpidcAPIResponse.code = UniversalCheckoutModel._mCode
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

    'Get api/universal checkout
    <HttpGet>
    Public Function GetUniversalCheckout(ByVal param As String, ByVal id As String) As IHttpActionResult
        'Web API Authorization Validation
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization(param) = "200" Then
            'Call The Model And API Standard Respone
            'Call the model and check 
            If UniversalCheckoutModel._mGetValue(param, id) Then 'Success
                'Create an instance of the StandardApiResponse
                _SpidcAPIResponse.status = UniversalCheckoutModel._mStatus 'Or "error" for unsuccessful responses
                _SpidcAPIResponse.data = UniversalCheckoutModel._mData ' Data Objectr Array From Model
                _SpidcAPIResponse.message = UniversalCheckoutModel._mMessage 'A descriptive message about the response,
                _SpidcAPIResponse.code = UniversalCheckoutModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(_SpidcAPIResponse)
            Else 'Error
                _SpidcAPIResponse.status = UniversalCheckoutModel._mStatus
                _SpidcAPIResponse.data = UniversalCheckoutModel._mData
                _SpidcAPIResponse.message = UniversalCheckoutModel._mMessage
                _SpidcAPIResponse.code = UniversalCheckoutModel._mCode
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

    ' POST api/universal checkout
    <HttpPost>
    Public Function PostUniversalCheckout(ByVal param As String, <FromBody> data As Object) As IHttpActionResult
        'Web API Authorization Validation
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization(param) = "200" Then
            'Call The Model And API Standard Respone
            'Call the model and check 
            If UniversalCheckoutModel._mPostValue(param, data) Then 'Success
                'Create an instance of the StandardApiResponse
                _SpidcAPIResponse.status = UniversalCheckoutModel._mStatus 'Or "error" for unsuccessful responses
                _SpidcAPIResponse.data = UniversalCheckoutModel._mData ' Data Objectr Array From Model
                _SpidcAPIResponse.message = UniversalCheckoutModel._mMessage 'A descriptive message about the response,
                _SpidcAPIResponse.code = UniversalCheckoutModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(_SpidcAPIResponse)
            Else 'Error
                _SpidcAPIResponse.status = UniversalCheckoutModel._mStatus
                _SpidcAPIResponse.data = UniversalCheckoutModel._mData
                _SpidcAPIResponse.message = UniversalCheckoutModel._mMessage
                _SpidcAPIResponse.code = UniversalCheckoutModel._mCode
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

    'Dele api/universal checkout
    <HttpDelete>
    Public Function DeleteUniversalCheckout(ByVal param As String, ByVal id As String) As IHttpActionResult
        'Web API Authorization Validation
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization(param) = "200" Then
            'Call The Model And API Standard Respone
            'Call the model and check 
            If UniversalCheckoutModel._mDeleteValues(param, id) Then 'Success
                'Create an instance of the StandardApiResponse
                _SpidcAPIResponse.status = UniversalCheckoutModel._mStatus 'Or "error" for unsuccessful responses
                _SpidcAPIResponse.data = UniversalCheckoutModel._mData ' Data Objectr Array From Model
                _SpidcAPIResponse.message = UniversalCheckoutModel._mMessage 'A descriptive message about the response,
                _SpidcAPIResponse.code = UniversalCheckoutModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(_SpidcAPIResponse)
            Else 'Error
                _SpidcAPIResponse.status = UniversalCheckoutModel._mStatus
                _SpidcAPIResponse.data = UniversalCheckoutModel._mData
                _SpidcAPIResponse.message = UniversalCheckoutModel._mMessage
                _SpidcAPIResponse.code = UniversalCheckoutModel._mCode
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