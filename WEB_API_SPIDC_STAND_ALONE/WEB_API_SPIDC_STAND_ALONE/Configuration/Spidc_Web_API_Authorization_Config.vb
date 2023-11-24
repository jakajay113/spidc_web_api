Imports System.IO
Imports DotEnv
Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports System.Threading.Tasks
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Filters
Imports System.Net
Imports System
Imports System.Collections.Generic

Public Class Spidc_Web_API_Authorization_Config

#Region "Variable Web API Authorization"

    Public Shared _mSqlCon As New SqlConnection
    Public Shared _mSqlCmd As SqlCommand
    Public Shared _mDataTable As New DataTable
    Public Shared _mStrSql As String
    Public Shared _mDataAdapter As New SqlDataAdapter
    Public Shared _mDataset As New DataSet
    Public Shared _mconfig As String
    Public Shared _mjson As Object
    Public Shared _mAPIHeader As String
    Public Shared _mAPITokenHeader As String
    Public Shared _mAPIKey As String
    Public Shared _mAPIToken As String
    Public Shared _mAPIResponse As HttpResponseMessage
    Public Shared _mAPIHeaderCheck As String

    Public Shared _mUri As Uri
    Public Shared _mUrl As String
    Public Shared scheme As String
    Public Shared host As String
    Public Shared port As String
    Public Shared path As String
    Public Shared searchUrlPath As String()
    Public Shared urlFound As Boolean = False
    Public Shared searchUrlFound As String
    Public Shared replaceProxyApiEndPoint As String
    Public Shared buildApiEndPoint As String


#End Region
#Region "Property Web API Authorization"
    Public Shared ReadOnly Property _pDataAdapter() As SqlDataAdapter
        Get
            Try
                Return _mDataAdapter
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property
    Public Shared ReadOnly Property _pDataset() As DataSet
        Get
            Try
                Return _mDataset
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property
    Public Shared ReadOnly Property _pDataTable() As DataTable
        Get
            Try
                Return _mDataTable
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property
    Public Property _pSqlCon() As SqlConnection
        Get
            Try
                Return _mSqlCon
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
        Set(value As SqlConnection)
            _mSqlCon = value
        End Set
    End Property
    Public Shared Property _pconfig As String
        Get
            Return _mconfig
        End Get
        Set(value As String)
            _mconfig = value
        End Set
    End Property
    Public Shared Property _pjson As Object
        Get
            Return _mjson
        End Get
        Set(value As Object)
            _mjson = value
        End Set
    End Property
    Public Shared Property _pAPIHeader As String
        Get
            Return _mAPIHeader
        End Get
        Set(value As String)
            _mAPIHeader = value
        End Set
    End Property
    Public Shared Property _pAPIKey As String
        Get
            Return _mAPIKey
        End Get
        Set(value As String)
            _mAPIKey = value
        End Set
    End Property

#End Region

#Region "Routine Web API Authorization"
    'Authorization
    Public Shared Function _pAuthorization(Optional ByVal param As String = Nothing)
        Try
            'Config
            Spidc_Web_API_Config.WebApiConfig()
            'Check if param is for login add api header key 
            Select Case param 'Login/Send Email/Webhooks Authorization
                Case Spidc_Web_API_Config._mAppPostParam, Spidc_Web_API_Config._mAppPostParamEmailApp, Spidc_Web_API_Config._mAppPostParamUniversalCheckOut, Spidc_Web_API_Config._mAppPostParamWebhooks
                    _mAPIHeaderCheck = Spidc_Web_API_Config._mApiHeader ' API Header Name
                Case Else 'Other Authorization
                    _mAPIHeaderCheck = Spidc_Web_API_Config._mApiHeaderToken ' API Token Header Name
            End Select
            ' Check if the custom  api header is present in the request
            If HttpContext.Current.Request.Headers.AllKeys.Contains(_mAPIHeaderCheck) Then
                Select Case param 'Login/Send Email/Webhooks Authorization
                    Case Spidc_Web_API_Config._mAppPostParam, Spidc_Web_API_Config._mAppPostParamEmailApp, Spidc_Web_API_Config._mAppPostParamUniversalCheckOut, Spidc_Web_API_Config._mAppPostParamWebhooks
                        _mAPIKey = HttpContext.Current.Request.Headers(_mAPIHeaderCheck)
                        If IsValidApiKey(_mAPIKey, Nothing, param) Then
                            'API key is valid; allow the request to proceed 
                            'Response 200
                            _mAPIResponse = New HttpResponseMessage(HttpStatusCode.OK)
                            _mAPIResponse.Content = _mAPIResponse.Content
                            Return _mAPIResponse.StatusCode
                        Else
                            ' API key is not valid; return an unauthorized response HttpStatusCode.Unauthorized
                            'Response 401
                            _mAPIResponse = New HttpResponseMessage(HttpStatusCode.Unauthorized)
                            _mAPIResponse.Content = _mAPIResponse.Content
                            Return _mAPIResponse.StatusCode
                        End If
                    Case Else 'Other Authorization
                        _mAPIToken = HttpContext.Current.Request.Headers(_mAPIHeaderCheck)
                        If IsValidApiKey(Nothing, _mAPIToken, param) Then
                            ' API key is valid; allow the request to proceed 
                            'Response 200
                            _mAPIResponse = New HttpResponseMessage(HttpStatusCode.OK)
                            _mAPIResponse.Content = _mAPIResponse.Content
                            Return _mAPIResponse.StatusCode
                        Else
                            ' API key is not valid; return an unauthorized response HttpStatusCode.Unauthorized
                            'Response 401
                            _mAPIResponse = New HttpResponseMessage(HttpStatusCode.Unauthorized)
                            _mAPIResponse.Content = _mAPIResponse.Content
                            Return _mAPIResponse.StatusCode
                        End If
                End Select


            Else
                ' Custom header is not present in the request; return a bad request response
                'Response 400
                _mAPIResponse = New HttpResponseMessage(HttpStatusCode.BadRequest)
                _mAPIResponse.Content = _mAPIResponse.Content
                Return _mAPIResponse.StatusCode
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function
    'Check If API Key Is Valid In Config And The Token Is Exist And JWT TOKEN For get universal checkout i used JWT TOKEN
    Public Shared Function IsValidApiKey(Optional ByVal apiKey As String = Nothing, Optional ByVal apiToken As String = Nothing, Optional ByVal param As String = Nothing) As Boolean
        'Call THE SPIDC WEB API CONFIG
        Spidc_Web_API_Config.WebApiConfig()
        'Check if param if for login
        Select Case param 'Login/Send Email/Webhooks Validation
            Case Spidc_Web_API_Config._mAppPostParam, Spidc_Web_API_Config._mAppPostParamEmailApp, Spidc_Web_API_Config._mAppPostParamUniversalCheckOut, Spidc_Web_API_Config._mAppPostParamWebhooks
                _mAPIKey = Spidc_Web_API_Config._mApiKey
                If _mAPIKey = apiKey Then
                    Return True
                Else
                    Return False
                End If
            Case Spidc_Web_API_Config._mAppGetParamUniversalCheckOut, Spidc_Web_API_Config._mAppGetParamUniversalCheckOutPaymentMethod, Spidc_Web_API_Config._mAppDeleteParamUniversalCheckOut 'JWT TOKEN Validation
                'Check the JWT TOKEN IF exist or  Expired or not
                If Spidc_Web_API_JWTToken.ValidateJwt(apiToken) Then
                    Return True
                Else
                    Return False
                End If
            Case Else 'Other Validation
                'Call THE SPIDC WEB API TOKEN CHECKER VALIDATION 
                _mAPIToken = Spidc_Web_API_Token_Checker_Validation._checkAuthorization(apiToken)
                If _mAPIToken = apiToken Then
                    Return True
                Else
                    Return False
                End If
        End Select
    End Function
    'Authorize And Build API URL End Point
    Public Shared Function AuthorizeAndBuildUrlApiEndPoint(url As Uri) As String
        'Config
        Spidc_Web_API_Config.WebApiConfig()
        '' Access individual parts of the URL
        scheme = url.Scheme ' e.g., "https"
        host = url.Host ' e.g., "www.example.com"
        port = url.Port ' e.g., 8080
        path = url.AbsolutePath ' e.g., "/path/to/resource"
        'Filter the URL To Build The Request and Figure Out What Type Of Request What System Is It The Default Is Spidc 
        'List Available System and Path In Config File
        searchUrlPath = {Spidc_Web_API_Config._mApiUrlParamOAIMS,
                         Spidc_Web_API_Config._mApiUrlParamFAMS,
                         Spidc_Web_API_Config._mApiUrlParamEIS,
                         Spidc_Web_API_Config._mApiUrlParamTOIMS,
                         Spidc_Web_API_Config._mApiUrlParamTIMS,
                         Spidc_Web_API_Config._mApiUrlParamCedula,
                         Spidc_Web_API_Config._mApiUrlParamEmailApp,
                         Spidc_Web_API_Config._mApiUrlParamUniversalCheckout,
                         Spidc_Web_API_Config._mApiUrlParamWebhooks
                        }
        For Each searchUrl As String In searchUrlPath
            If path.Contains(searchUrl) Then
                urlFound = True
                searchUrlFound = searchUrl
                Exit For
            Else
                urlFound = False
            End If
        Next
        'Check If Found The Url Path If Not The Default End Point Will Run Which is SPIDC API ENDPOINT
        If urlFound Then
            replaceProxyApiEndPoint = path.Replace(Spidc_Web_API_Config._mApiProxyName, searchUrlFound)
        Else
            replaceProxyApiEndPoint = path.Replace(Spidc_Web_API_Config._mApiProxyName, Spidc_Web_API_Config._mApiName)
        End If
        'Check If Port Is Available Live Or Local
        If port Then
            buildApiEndPoint = scheme & "://" & host & ":" & port & replaceProxyApiEndPoint
        Else
            buildApiEndPoint = scheme & "://" & host & replaceProxyApiEndPoint
        End If
        Return buildApiEndPoint
    End Function

    'Authorize Header Check If Have Authorize Header and Token 
    Public Shared Function AuthorizeBearerToken(ByVal headerName As String, request As HttpRequest) As String
        ' Check if the header is present in the request
        If request.Headers.AllKeys.Contains(headerName) Then
            ' Get the header value
            Dim headerValue As String = request.Headers(headerName)
            ' Check if the header value starts with "Bearer " (or any other prefix you expect for tokens)
            If headerValue IsNot Nothing AndAlso headerValue.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) Then
                ' Extract the token part (remove the "Bearer " prefix)
                Return headerValue.Substring(7) ' "Bearer ".Length = 7
            End If
        End If
        ' If no valid token is found, return an empty string or handle it as needed
        Return String.Empty
    End Function

    'Authorize Header Check If Have Authorize Header and API 
    Public Function AuthorizeAPIKey(ByVal headerName As String, request As HttpRequest) As String
        ' Check if the header is present in the request
        If request.Headers.AllKeys.Contains(headerName) Then
            ' Get the header value
            Dim headerValue As String = request.Headers(headerName)
            ' Check if the header value starts with "Bearer " (or any other prefix you expect for tokens)
            If headerValue IsNot Nothing Then
                ' Extract the API key part (remove the "Bearer " prefix)
                Return headerValue ' "Bearer ".Length = 7
            End If
        End If
        ' If no valid API key is found, return an empty string or handle it as needed
        Return String.Empty
    End Function

#End Region


End Class



