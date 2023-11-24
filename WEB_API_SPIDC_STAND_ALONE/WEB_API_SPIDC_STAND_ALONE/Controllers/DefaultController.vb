Imports System.Net
Imports System.Web.Http
Imports System.Net.Http

Public Class DefaultController
    Inherits ApiController
    'SPIDC Config
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config
    'SPIDC Config
    Private Shared Spidc_Web_API_Param_Checker_Config As New Spidc_Web_API_Param_Checker_Config
    'Model

    'Other
    Private Shared _SpidcAPIResponse As New Spidc_Web_API_Standard_Response
    Private Shared _mResponse As HttpResponseMessage


    ' GET api/Spidc
    <HttpGet>
    Public Function GetValues(ByVal param As String) As IHttpActionResult
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then


            Return Ok()
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


    ' GET api/Spidc/5
    <HttpGet>
    Public Function GetValue(ByVal param As String, ByVal id As String) As IHttpActionResult
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then


            Return Ok()
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
    Public Function PostValue(ByVal param As String, <FromBody()> ByVal value As String()) As IHttpActionResult
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then


            Return Ok()
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
    Public Function PutValue(ByVal param As String, <FromBody()> ByVal value As String(), ByVal id As String) As IHttpActionResult
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then


            Return Ok()
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
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then


            Return Ok()
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