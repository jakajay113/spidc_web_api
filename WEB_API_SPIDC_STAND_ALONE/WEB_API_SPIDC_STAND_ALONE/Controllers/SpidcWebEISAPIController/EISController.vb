Imports System.IO
Imports Newtonsoft.Json
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Filters
Imports System.Net

Public Class EISController
    Inherits ApiController
    'SPIDC Config
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config
    'SPIDC Config
    Private Shared Spidc_Web_API_Param_Checker_Config As New Spidc_Web_API_Param_Checker_Config
    'Model
    Private Shared EISModel As New EISModel
    'Other
    Private Shared _SpidcAPIResponse As New Spidc_Web_API_Standard_Response
    Private Shared _mResponse As HttpResponseMessage


    ' GET api/eis
    <HttpGet>
    Public Function GetValues(ByVal param As String) As IHttpActionResult
        'Web API Authorization Validation
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then
            'Check The  Method param is exist and set the connection type
            Select Case Spidc_Web_API_Param_Checker_Config._mCheckParamEis(param)
                Case Spidc_Web_API_Config._mAppNameFAMS
                    EISModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_FAMS
                Case Spidc_Web_API_Config._mAppNameGAAMS
                    EISModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_GAAMS
            End Select
            'Call The Model And API Standard Respone
            'Call the model and check 
            If EISModel._mGetValues(param) Then 'Success
                'Create an instance of the StandardApiResponse
                _SpidcAPIResponse.status = EISModel._mStatus 'Or "error" for unsuccessful responses
                _SpidcAPIResponse.data = EISModel._mData ' Data Objectr Array From Model
                _SpidcAPIResponse.message = EISModel._mMessage 'A descriptive message about the response,
                _SpidcAPIResponse.code = EISModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(_SpidcAPIResponse)
            Else 'Error
                _SpidcAPIResponse.status = EISModel._mStatus
                _SpidcAPIResponse.data = EISModel._mData
                _SpidcAPIResponse.message = EISModel._mMessage
                _SpidcAPIResponse.code = EISModel._mCode
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

    ' GET api/eis/5
    <HttpGet>
    Public Function GetValue(ByVal param As String, ByVal id As String) As IHttpActionResult
        'Web API Authorization Validation
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then
          
            'Check The  Method param is exist and set the connection type
            Select Case Spidc_Web_API_Param_Checker_Config._mCheckParamEis(param)
                Case Spidc_Web_API_Config._mAppNameGAAMS
                    EISModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_GAAMS
                Case Spidc_Web_API_Config._mAppNameFAMS
                    EISModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_FAMS
            End Select
            'Call The Model And API Standard Respone 
            'Call the model and check 
            If EISModel._mGetValue(param, id) Then 'Success
                'Create an instance of the StandardApiResponse
                _SpidcAPIResponse.status = EISModel._mStatus 'Or "error" for unsuccessful responses
                _SpidcAPIResponse.data = EISModel._mData ' Data Objectr Array From Model
                _SpidcAPIResponse.message = EISModel._mMessage 'A descriptive message about the response,
                _SpidcAPIResponse.code = EISModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(_SpidcAPIResponse)
            Else 'Error
                _SpidcAPIResponse.status = EISModel._mStatus
                _SpidcAPIResponse.data = EISModel._mData
                _SpidcAPIResponse.message = EISModel._mMessage
                _SpidcAPIResponse.code = EISModel._mCode
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

    ' POST api/eis
    <HttpPost>
    Public Function PostValue(ByVal param As String, <FromBody()> ByVal value As String()) As IHttpActionResult
        'Web API Authorization Validation
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then
           
            'Check The  Method param is exist and set the connection type
            Select Case Spidc_Web_API_Param_Checker_Config._mCheckParamEis(param)
                Case Spidc_Web_API_Config._mAppNameGAAMS
                    EISModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_GAAMS
                Case Spidc_Web_API_Config._mAppNameFAMS
                    EISModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_FAMS
            End Select
            'Call The Model And API Standard Respone 
            'Call the model and check 
            EISModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_SPIDC_Web_API
            'Call the model and check 
            If EISModel._mPostValue(param, value) Then 'Success
                'Create an instance of the StandardApiResponse
                _SpidcAPIResponse.status = EISModel._mStatus 'Or "error" for unsuccessful responses
                _SpidcAPIResponse.data = EISModel._mData ' Data Objectr Array From Model
                _SpidcAPIResponse.message = EISModel._mMessage 'A descriptive message about the response,
                _SpidcAPIResponse.code = EISModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(_SpidcAPIResponse)
            Else 'Error
                _SpidcAPIResponse.status = EISModel._mStatus
                _SpidcAPIResponse.data = EISModel._mData
                _SpidcAPIResponse.message = EISModel._mMessage
                _SpidcAPIResponse.code = EISModel._mCode
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

    ' PUT api/eis/5
    <HttpPut>
    Public Function PutValue(ByVal param As String, <FromBody()> ByVal value As String(), ByVal id As String) As IHttpActionResult
        'Web API Authorization Validation
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then
           
            'Check The  Method param is exist and set the connection type
            Select Case Spidc_Web_API_Param_Checker_Config._mCheckParamEis(param)
                Case Spidc_Web_API_Config._mAppNameGAAMS
                    EISModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_GAAMS
                Case Spidc_Web_API_Config._mAppNameFAMS
                    EISModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_FAMS
            End Select
            'Call The Model And API Standard Respone 
            'Call the model and check 
            EISModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_SPIDC_Web_API
            'Call the model and check 
            If EISModel._mPutValue(param, value, id) Then 'Success
                'Create an instance of the StandardApiResponse
                _SpidcAPIResponse.status = EISModel._mStatus 'Or "error" for unsuccessful responses
                _SpidcAPIResponse.data = EISModel._mData ' Data Objectr Array From Model
                _SpidcAPIResponse.message = EISModel._mMessage 'A descriptive message about the response,
                _SpidcAPIResponse.code = EISModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(_SpidcAPIResponse)
            Else 'Error
                _SpidcAPIResponse.status = EISModel._mStatus
                _SpidcAPIResponse.data = EISModel._mData
                _SpidcAPIResponse.message = EISModel._mMessage
                _SpidcAPIResponse.code = EISModel._mCode
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

    ' DELETE api/eis/5
    <HttpDelete>
    Public Function DeleteValue(ByVal param As String, ByVal id As String) As IHttpActionResult
        'Web API Authorization Validation
        'Call The Config File 
        Spidc_Web_API_Config.WebApiConfig()
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then
            'Call The Config File 
            Spidc_Web_API_Config.WebApiConfig()
            'Check The  Method param is exist and set the connection type
            Select Case Spidc_Web_API_Param_Checker_Config._mCheckParamEis(param)
                Case Spidc_Web_API_Config._mAppNameGAAMS
                    EISModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_GAAMS
                Case Spidc_Web_API_Config._mAppNameFAMS
                    EISModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_FAMS
            End Select
            'Call The Model And API Standard Respone 
            'Call the model and check 
            EISModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_SPIDC_Web_API
            'Call the model and check 
            If EISModel._mDeleteValue(param, id) Then 'Success
                'Create an instance of the StandardApiResponse
                _SpidcAPIResponse.status = EISModel._mStatus 'Or "error" for unsuccessful responses
                _SpidcAPIResponse.data = EISModel._mData ' Data Objectr Array From Model
                _SpidcAPIResponse.message = EISModel._mMessage 'A descriptive message about the response,
                _SpidcAPIResponse.code = EISModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(_SpidcAPIResponse)
            Else 'Error
                _SpidcAPIResponse.status = EISModel._mStatus
                _SpidcAPIResponse.data = EISModel._mData
                _SpidcAPIResponse.message = EISModel._mMessage
                _SpidcAPIResponse.code = EISModel._mCode
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