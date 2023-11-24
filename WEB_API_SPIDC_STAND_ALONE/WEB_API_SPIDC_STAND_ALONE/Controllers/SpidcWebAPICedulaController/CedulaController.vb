Imports System.IO
Imports Newtonsoft.Json
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Filters
Imports System.Net
Imports System.Net.Http.Headers

Public Class CedulaController
    Inherits ApiController
    'SPIDC Config
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config
    'Model
    Private Shared CedulaModel As New CedulaModel
    'Other
    Private Shared _SpidcAPIResponse As New Spidc_Web_API_Standard_Response
    Private Shared _mResponse As HttpResponseMessage

    ' GET api/Spidc
    <HttpGet>
    Public Function GetValues(ByVal param As String) As IHttpActionResult
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization(param) = "200" Then
            'Call The Model And API Standard Respone
            'Call the model and check 
            If CedulaModel._mGetValues(param) Then 'Success
                'Create an instance of the StandardApiResponse
                _SpidcAPIResponse.status = CedulaModel._mStatus 'Or "error" for unsuccessful responses
                _SpidcAPIResponse.data = CedulaModel._mData ' Data Objectr Array From Model
                _SpidcAPIResponse.message = CedulaModel._mMessage 'A descriptive message about the response,
                _SpidcAPIResponse.code = CedulaModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
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
            _SpidcAPIResponse.status = Spidc_Web_API_Config._mApiRespone400Status
            _SpidcAPIResponse.data = Spidc_Web_API_Config._mApiRespone400Data
            _SpidcAPIResponse.message = Spidc_Web_API_Config._mApiRespone400Message
            _SpidcAPIResponse.code = Spidc_Web_API_Config._mApiRespone400Code
            Return Content(HttpStatusCode.Unauthorized, _SpidcAPIResponse)
        End If
    End Function


    ' GET api/Spidc/5
    <HttpGet>
    Public Function GetValue(ByVal param As String, ByVal id As String) As IHttpActionResult
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization(param) = "200" Then
            'Call The Model And API Standard Respone
            'Call the model and check 
            If CedulaModel._mGetValue(param, id) Then 'Success
                'Create an instance of the StandardApiResponse
                _SpidcAPIResponse.status = CedulaModel._mStatus 'Or "error" for unsuccessful responses
                _SpidcAPIResponse.data = CedulaModel._mData ' Data Objectr Array From Model
                _SpidcAPIResponse.message = CedulaModel._mMessage 'A descriptive message about the response,
                _SpidcAPIResponse.code = CedulaModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
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


    ' POST api/Spidc
    <HttpPost>
    Public Function PostValue(ByVal param As String, <FromBody()> ByVal value As Object) As IHttpActionResult
        'Web API Authorization Validation
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization(param) = "200" Then
            'Call The Model And API Standard Respone
            'Call the model and check 
            If CedulaModel._mPostValue(param, value) Then 'Success
                'Create an instance of the StandardApiResponse
                _SpidcAPIResponse.status = CedulaModel._mStatus 'Or "error" for unsuccessful responses
                _SpidcAPIResponse.data = CedulaModel._mData ' Data Objectr Array From Model
                _SpidcAPIResponse.message = CedulaModel._mMessage 'A descriptive message about the response,
                _SpidcAPIResponse.code = CedulaModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
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


    ' PUT api/Spidc/5
    <HttpPut>
    Public Function PutValue(ByVal param As String, <FromBody()> ByVal value As Object, ByVal id As String) As IHttpActionResult
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization(param) = "200" Then
            'Call The Model And API Standard Respone
            'Call the model and check 
            If CedulaModel._mPutValue(param, value, id) Then 'Success
                'Create an instance of the StandardApiResponse
                _SpidcAPIResponse.status = CedulaModel._mStatus 'Or "error" for unsuccessful responses
                _SpidcAPIResponse.data = CedulaModel._mData ' Data Objectr Array From Model
                _SpidcAPIResponse.message = CedulaModel._mMessage 'A descriptive message about the response,
                _SpidcAPIResponse.code = CedulaModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
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

    ' DELETE api/Spidc/5
    <HttpDelete>
    Public Function DeleteValue(ByVal param As String, ByVal id As String) As IHttpActionResult
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization(param) = "200" Then



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