Imports System.IO
Imports Newtonsoft.Json
Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Filters
Imports System.Net
Imports System.Web.Http.Results


Public Class SpidcController
    Inherits ApiController

    'List Of Model
    Private Shared SpidcModel As New SpidcModel
    Private Shared SpidcAPIResponse As New Spidc_Web_API_Standard_Response
    Private Shared _mResponse As HttpResponseMessage

    ' GET api/Spidc
    <HttpGet>
    Public Function GetValues(ByVal param As String) As IHttpActionResult

        'Web API Authorization Validation
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then
            'Call The Model And API Standard Respone
            'Set the connection of  model
            SpidcModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_SPIDC_Web_API
            'Call the model and check 
            If SpidcModel._mGetValues(param) Then 'Success
                'Create an instance of the StandardApiResponse
                SpidcAPIResponse.status = SpidcModel._mStatus 'Or "error" for unsuccessful responses
                SpidcAPIResponse.data = SpidcModel._mData ' Data Objectr Array From Model
                SpidcAPIResponse.message = SpidcModel._mMessage 'A descriptive message about the response,
                SpidcAPIResponse.code = SpidcModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(SpidcAPIResponse)
            Else 'Error
                SpidcAPIResponse.status = SpidcModel._mStatus
                SpidcAPIResponse.data = SpidcModel._mData
                SpidcAPIResponse.message = SpidcModel._mMessage
                SpidcAPIResponse.code = SpidcModel._mCode
                Return Content(HttpStatusCode.InternalServerError, SpidcAPIResponse)
            End If

            'HTTP/1.1 401 Unauthorized
        ElseIf Spidc_Web_API_Authorization_Config._pAuthorization() = "401" Then
            SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
            SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
            SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
            SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
            Return Content(HttpStatusCode.Unauthorized, SpidcAPIResponse)
        Else
            'HTTP/1.1 400 Bad Request
            SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
            SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
            SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
            SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
            Return Content(HttpStatusCode.Unauthorized, SpidcAPIResponse)
        End If

    End Function


    ' GET api/Spidc/5
    <HttpGet>
    Public Function GetValue(ByVal param As String, ByVal id As String) As IHttpActionResult
        'Web API Authorization Validation
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then

            'Call The Model And API Standard Respone
            'Set the connection of  model
            SpidcModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_SPIDC_Web_API
            'Call the model and check 
            If SpidcModel._mGetValue(param, id) Then 'Success
                'Create an instance of the StandardApiResponse
                SpidcAPIResponse.status = SpidcModel._mStatus 'Or "error" for unsuccessful responses
                SpidcAPIResponse.data = SpidcModel._mData ' Data Objectr Array From Model
                SpidcAPIResponse.message = SpidcModel._mMessage 'A descriptive message about the response,
                SpidcAPIResponse.code = SpidcModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(SpidcAPIResponse)
            Else 'Error
                SpidcAPIResponse.status = SpidcModel._mStatus
                SpidcAPIResponse.data = SpidcModel._mData
                SpidcAPIResponse.message = SpidcModel._mMessage
                SpidcAPIResponse.code = SpidcModel._mCode
                Return Content(HttpStatusCode.InternalServerError, SpidcAPIResponse)
            End If
            'HTTP/1.1 401 Unauthorized
        ElseIf Spidc_Web_API_Authorization_Config._pAuthorization() = "401" Then
            SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
            SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
            SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
            SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
            Return Content(HttpStatusCode.Unauthorized, SpidcAPIResponse)
        Else
            'HTTP/1.1 400 Bad Request
            SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
            SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
            SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
            SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
            Return Content(HttpStatusCode.Unauthorized, SpidcAPIResponse)
        End If
    End Function


    ' POST api/Spidc
    <HttpPost>
    Public Function PostValue(ByVal param As String, <FromBody()> ByVal value As Object) As IHttpActionResult
        'Web API Authorization Validation
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then
            'Call The Model And API Standard Respone
            'Set the connection of  model
            SpidcModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_SPIDC_Web_API
            'Call the model and check 
            If SpidcModel._mPostValue(param, value) Then 'Success
                'Create an instance of the StandardApiResponse
                SpidcAPIResponse.status = SpidcModel._mStatus 'Or "error" for unsuccessful responses
                SpidcAPIResponse.data = SpidcModel._mData ' Data Objectr Array From Model
                SpidcAPIResponse.message = SpidcModel._mMessage 'A descriptive message about the response,
                SpidcAPIResponse.code = SpidcModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(SpidcAPIResponse)
            Else 'Error
                SpidcAPIResponse.status = SpidcModel._mStatus
                SpidcAPIResponse.data = SpidcModel._mData
                SpidcAPIResponse.message = SpidcModel._mMessage
                SpidcAPIResponse.code = SpidcModel._mCode
                Return Content(HttpStatusCode.InternalServerError, SpidcAPIResponse)
            End If
            'HTTP/1.1 401 Unauthorized
        ElseIf Spidc_Web_API_Authorization_Config._pAuthorization() = "401" Then
            SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
            SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
            SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
            SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
            Return Content(HttpStatusCode.Unauthorized, SpidcAPIResponse)
        Else
            'HTTP/1.1 400 Bad Request
            SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
            SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
            SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
            SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
            Return Content(HttpStatusCode.Unauthorized, SpidcAPIResponse)
        End If
    End Function


    ' PUT api/Spidc/5
    <HttpPut>
    Public Function PutValue(ByVal param As String, <FromBody()> ByVal value As Object, ByVal id As String) As IHttpActionResult
        'Web API Authorization Validation
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then
            'Call The Model And API Standard Respone
            'Set the connection of  model
            SpidcModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_SPIDC_Web_API
            'Call the model and check 
            If SpidcModel._mPutValue(param, value, id) Then 'Success
                'Create an instance of the StandardApiResponse
                SpidcAPIResponse.status = SpidcModel._mStatus 'Or "error" for unsuccessful responses
                SpidcAPIResponse.data = SpidcModel._mData ' Data Objectr Array From Model
                SpidcAPIResponse.message = SpidcModel._mMessage 'A descriptive message about the response,
                SpidcAPIResponse.code = SpidcModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(SpidcAPIResponse)
            Else 'Error
                SpidcAPIResponse.status = SpidcModel._mStatus
                SpidcAPIResponse.data = SpidcModel._mData
                SpidcAPIResponse.message = SpidcModel._mMessage
                SpidcAPIResponse.code = SpidcModel._mCode
                Return Content(HttpStatusCode.InternalServerError, SpidcAPIResponse)
            End If

            'HTTP/1.1 401 Unauthorized
        ElseIf Spidc_Web_API_Authorization_Config._pAuthorization() = "401" Then
            SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
            SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
            SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
            SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
            Return Content(HttpStatusCode.Unauthorized, SpidcAPIResponse)
        Else
            'HTTP/1.1 400 Bad Request
            SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
            SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
            SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
            SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
            Return Content(HttpStatusCode.Unauthorized, SpidcAPIResponse)
        End If
    End Function

    ' DELETE api/Spidc/5
    <HttpDelete>
    Public Function DeleteValue(ByVal param As String, ByVal id As String) As IHttpActionResult
        'Web API Authorization Validation
        'HTTP/1.1 200 OK
        If Spidc_Web_API_Authorization_Config._pAuthorization() = "200" Then

            'Call The Model And API Standard Respone
            'Set the connection of  model
            SpidcModel._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_SPIDC_Web_API
            'Call the model and check 
            If SpidcModel._mDeleteValue(param, id) Then 'Success
                'Create an instance of the StandardApiResponse
                SpidcAPIResponse.status = SpidcModel._mStatus 'Or "error" for unsuccessful responses
                SpidcAPIResponse.data = SpidcModel._mData ' Data Objectr Array From Model
                SpidcAPIResponse.message = SpidcModel._mMessage 'A descriptive message about the response,
                SpidcAPIResponse.code = SpidcModel._mCode ' HTTP status code 200/401/400/ Ok/Unauthorized/Bad Request
                Return Ok(SpidcAPIResponse)
            Else 'Error
                SpidcAPIResponse.status = SpidcModel._mStatus
                SpidcAPIResponse.data = SpidcModel._mData
                SpidcAPIResponse.message = SpidcModel._mMessage
                SpidcAPIResponse.code = SpidcModel._mCode
                Return Content(HttpStatusCode.InternalServerError, SpidcAPIResponse)
            End If


            'HTTP/1.1 401 Unauthorized
        ElseIf Spidc_Web_API_Authorization_Config._pAuthorization() = "401" Then
            SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
            SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
            SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
            SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
            Return Content(HttpStatusCode.Unauthorized, SpidcAPIResponse)
        Else
            'HTTP/1.1 400 Bad Request
            SpidcAPIResponse.status = Spidc_Web_API_Config._mApiResponese401Status
            SpidcAPIResponse.data = Spidc_Web_API_Config._mApiResponese401Data
            SpidcAPIResponse.message = Spidc_Web_API_Config._mApiResponese401Message
            SpidcAPIResponse.code = Spidc_Web_API_Config._mApiResponese401Code
            Return Content(HttpStatusCode.Unauthorized, SpidcAPIResponse)
        End If
    End Function



End Class