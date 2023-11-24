Imports System.Net
Imports System.Web.Http
Imports System.Net.Http
Imports System.Threading.Tasks

Public Class SpidcProxyController
    Inherits ApiController
    'SPIDC Config
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config
    'SPIDC Web API Proxy Config
    Private Shared Spidc_Web_API_Proxy_Config As New Spidc_Web_API_Proxy_Config
    'SPIDC Web API Authorization Config
    Private Shared Spidc_Web_API_Authorization_Config As New Spidc_Web_API_Authorization_Config
    'Other
    Private Shared _SpidcAPIResponse As New Spidc_Web_API_Standard_Response
    Private Shared requestUri As Uri
    Private Shared _mapikey As String
    Private Shared _mtoken As String

    ' GET api/spidcproxy/param
    <HttpGet>
    Public Function GetValues(ByVal param As String) As IHttpActionResult
        'SPIDC Config
        Spidc_Web_API_Config.WebApiConfig()
        'Check If Param is for Get Universal Checkout Payment METHOD Form Get Universal Checkout Payment METHOD I Used JWT TOKEN 
          Select param
            Case Spidc_Web_API_Config._mAppGetParamUniversalCheckOutPaymentMethod
                'Call SPIDC Authorization To Check The Request have Authorization Token Proced the request if have and if not end the request
                _mtoken = Spidc_Web_API_Authorization_Config.AuthorizeBearerToken(Spidc_Web_API_Config._mApiHeaderToken, HttpContext.Current.Request)
                If Not String.IsNullOrEmpty(_mtoken) Then
                    'Parameters of Proxy The Request (httpMethod,  apiEndPoint,  requestBody, apikey, token, param)
                    Return ResponseMessage(Spidc_Web_API_Proxy_Config.ProxyTheRequest(HttpMethod.Get, Spidc_Web_API_Authorization_Config.AuthorizeAndBuildUrlApiEndPoint(Me.Request.RequestUri), Nothing, Nothing, _mtoken, param))
                Else
                    'Header or token is missing
                    _SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
                    _SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
                    _SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
                    _SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
                    Return Content(HttpStatusCode.Unauthorized, _SpidcAPIResponse)
                End If
            Case Else
                'This setup I used Authorization Bearer Token You can change it for API KEY It depends on you
                'Call SPIDC Authorization To Check The Request have Authorization Token Proced the request if have and if not end the request
                _mtoken = Spidc_Web_API_Authorization_Config.AuthorizeBearerToken(Spidc_Web_API_Config._mApiHeaderToken, HttpContext.Current.Request)
                If Not String.IsNullOrEmpty(_mtoken) Then
                    ' 'Parameters of Proxy The Request (httpMethod,  apiEndPoint,  requestBody, apikey, token, param)
                    Return ResponseMessage(Spidc_Web_API_Proxy_Config.ProxyTheRequest(HttpMethod.Get, Spidc_Web_API_Authorization_Config.AuthorizeAndBuildUrlApiEndPoint(Me.Request.RequestUri), Nothing, Nothing, _mtoken, param))
                Else
                    'Header or token is missing
                    _SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
                    _SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
                    _SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
                    _SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
                    Return Content(HttpStatusCode.Unauthorized, _SpidcAPIResponse)
                End If
        End Select
    End Function

    ' GET api/spidcproxy/param/5
    <HttpGet>
    Public Function GetValue(ByVal param As String, ByVal id As String) As IHttpActionResult
        'SPIDC Config
        Spidc_Web_API_Config.WebApiConfig()
        'Check If Param is for Get Universal Checkout Form Get Universal Checkout I Used JWT TOKEN 
        Select Case param
            Case Spidc_Web_API_Config._mAppGetParamUniversalCheckOut
                'Call SPIDC Authorization To Check The Request have Authorization Token Proced the request if have and if not end the request
                _mtoken = Spidc_Web_API_Authorization_Config.AuthorizeBearerToken(Spidc_Web_API_Config._mApiHeaderToken, HttpContext.Current.Request)
                If Not String.IsNullOrEmpty(_mtoken) Then
                    'Parameters of Proxy The Request (httpMethod,  apiEndPoint,  requestBody, apikey, token, param)
                    Return ResponseMessage(Spidc_Web_API_Proxy_Config.ProxyTheRequest(HttpMethod.Get, Spidc_Web_API_Authorization_Config.AuthorizeAndBuildUrlApiEndPoint(Me.Request.RequestUri), Nothing, Nothing, _mtoken, param))
                Else
                    'Header or token is missing
                    _SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
                    _SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
                    _SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
                    _SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
                    Return Content(HttpStatusCode.Unauthorized, _SpidcAPIResponse)
                End If
            Case Else
                'This setup I used Authorization Bearer Token You can change it for API KEY It depends on you
                'Call SPIDC Authorization To Check The Request have Authorization Token Proced the request if have and if not end the request
                _mtoken = Spidc_Web_API_Authorization_Config.AuthorizeBearerToken(Spidc_Web_API_Config._mApiHeaderToken, HttpContext.Current.Request)
                If Not String.IsNullOrEmpty(_mtoken) Then
                    'Parameters of Proxy The Request (httpMethod,  apiEndPoint,  requestBody, apikey, token, param)
                    Return ResponseMessage(Spidc_Web_API_Proxy_Config.ProxyTheRequest(HttpMethod.Get, Spidc_Web_API_Authorization_Config.AuthorizeAndBuildUrlApiEndPoint(Me.Request.RequestUri), Nothing, Nothing, _mtoken, param))
                Else
                    'Header or token is missing
                    _SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
                    _SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
                    _SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
                    _SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
                    Return Content(HttpStatusCode.Unauthorized, _SpidcAPIResponse)
                End If

        End Select
    End Function

    ' POST api/spidcproxy/param
    <HttpPost>
    Public Function PostValue(ByVal param As String, <FromBody()> ByVal value As Object) As IHttpActionResult
        'SPIDC Config
        Spidc_Web_API_Config.WebApiConfig()
        'Check if param is for login because login is a post method
        Select Case param 'Login/Send Email/Universal Checkout/Webhooks Validation
            Case Spidc_Web_API_Config._mAppPostParam, Spidc_Web_API_Config._mAppPostParamEmailApp, Spidc_Web_API_Config._mAppPostParamUniversalCheckOut, Spidc_Web_API_Config._mAppPostParamWebhooks
                'Call SPIDC Authorization To Check The Request have Authorization Api key Proced the request if have and if not end the request
                _mapikey = Spidc_Web_API_Authorization_Config.AuthorizeAPIKey(Spidc_Web_API_Config._mApiHeader, HttpContext.Current.Request)
                If Not String.IsNullOrEmpty(_mapikey) Then
                    'Parameters of Proxy The Request (httpMethod,  apiEndPoint,  requestBody, apikey, token, param)
                    Return ResponseMessage(Spidc_Web_API_Proxy_Config.ProxyTheRequest(HttpMethod.Post, Spidc_Web_API_Authorization_Config.AuthorizeAndBuildUrlApiEndPoint(Me.Request.RequestUri), value, _mapikey, Nothing, param))
                Else
                    'Header or token is missing
                    _SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
                    _SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
                    _SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
                    _SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
                    Return Content(HttpStatusCode.Unauthorized, _SpidcAPIResponse)
                End If
            Case Else 'Other Validation
                'This setup I used Authorization Bearer Token You can change it for API KEY It depends on you
                'Call SPIDC Authorization To Check The Request have Authorization Token Proced the request if have and if not end the request
                _mtoken = Spidc_Web_API_Authorization_Config.AuthorizeBearerToken(Spidc_Web_API_Config._mApiHeaderToken, HttpContext.Current.Request)
                If Not String.IsNullOrEmpty(_mtoken) Then
                    'Parameters of Proxy The Request (httpMethod,  apiEndPoint,  requestBody, apikey, token, param)
                    Return ResponseMessage(Spidc_Web_API_Proxy_Config.ProxyTheRequest(HttpMethod.Post, Spidc_Web_API_Authorization_Config.AuthorizeAndBuildUrlApiEndPoint(Me.Request.RequestUri), value, Nothing, _mtoken, param))
                Else
                    'Header or token is missing
                    _SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
                    _SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
                    _SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
                    _SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
                    Return Content(HttpStatusCode.Unauthorized, _SpidcAPIResponse)
                End If
        End Select
    End Function

    ' PUT api/spidcproxy/param/5
    <HttpPut>
    Public Function PutValue(ByVal param As String, ByVal id As String, <FromBody()> ByVal value As Object) As IHttpActionResult
        'SPIDC Config
        Spidc_Web_API_Config.WebApiConfig()
        'This setup I used Authorization Bearer Token You can change it for API KEY It depends on you
        'Call SPIDC Authorization To Check The Request have Authorization Token Proced the request if have and if not end the request
        _mtoken = Spidc_Web_API_Authorization_Config.AuthorizeBearerToken(Spidc_Web_API_Config._mApiHeaderToken, HttpContext.Current.Request)
        If Not String.IsNullOrEmpty(_mtoken) Then
            'Parameters of Proxy The Request (httpMethod,  apiEndPoint,  requestBody, apikey, token, param)
            Return ResponseMessage(Spidc_Web_API_Proxy_Config.ProxyTheRequest(HttpMethod.Put, Spidc_Web_API_Authorization_Config.AuthorizeAndBuildUrlApiEndPoint(Me.Request.RequestUri), value, Nothing, _mtoken, param))
        Else
            'Header or token is missing
            _SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
            _SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
            _SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
            _SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
            Return Content(HttpStatusCode.Unauthorized, _SpidcAPIResponse)
        End If
    End Function

    ' DELETE api/spidcproxy/param/5
    <HttpDelete>
    Public Function DeleteValue(ByVal param As String, ByVal id As String) As IHttpActionResult
        'SPIDC Config
        Spidc_Web_API_Config.WebApiConfig()
        'Check If Param is for Get Universal Checkout Payment METHOD Form Get Universal Checkout Payment METHOD I Used JWT TOKEN 
         Select param
            Case Spidc_Web_API_Config._mAppDeleteParamUniversalCheckOut
                'Call SPIDC Authorization To Check The Request have Authorization Token Proced the request if have and if not end the request
                _mtoken = Spidc_Web_API_Authorization_Config.AuthorizeBearerToken(Spidc_Web_API_Config._mApiHeaderToken, HttpContext.Current.Request)
                If Not String.IsNullOrEmpty(_mtoken) Then
                    'Parameters of Proxy The Request (httpMethod,  apiEndPoint,  requestBody, apikey, token, param)
                    Return ResponseMessage(Spidc_Web_API_Proxy_Config.ProxyTheRequest(HttpMethod.Delete, Spidc_Web_API_Authorization_Config.AuthorizeAndBuildUrlApiEndPoint(Me.Request.RequestUri), Nothing, Nothing, _mtoken, param))
                Else
                    'Header or token is missing
                    _SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
                    _SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
                    _SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
                    _SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
                    Return Content(HttpStatusCode.Unauthorized, _SpidcAPIResponse)
                End If
            Case Else
                'This setup I used Authorization Bearer Token You can change it for API KEY It depends on you
                'Call SPIDC Authorization To Check The Request have Authorization Token Proced the request if have and if not end the request
                _mtoken = Spidc_Web_API_Authorization_Config.AuthorizeBearerToken(Spidc_Web_API_Config._mApiHeaderToken, HttpContext.Current.Request)
                If Not String.IsNullOrEmpty(_mtoken) Then
                    'Parameters of Proxy The Request (httpMethod,  apiEndPoint,  requestBody, apikey, token, param)
                    Return ResponseMessage(Spidc_Web_API_Proxy_Config.ProxyTheRequest(HttpMethod.Delete, Spidc_Web_API_Authorization_Config.AuthorizeAndBuildUrlApiEndPoint(Me.Request.RequestUri), Nothing, Nothing, _mtoken, param))
                Else
                    'Header or token is missing
                    _SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
                    _SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
                    _SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
                    _SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
                    Return Content(HttpStatusCode.Unauthorized, _SpidcAPIResponse)
                End If

        End Select
    End Function
End Class