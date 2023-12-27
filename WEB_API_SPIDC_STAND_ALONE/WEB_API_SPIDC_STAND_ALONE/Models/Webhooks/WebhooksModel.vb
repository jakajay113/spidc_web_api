Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.IO
Public Class WebhooksModel
    'SPIDC Config
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config
    'Webhooks Data Access Layer
    Private Shared WebhooksDataAccessLayer As New WebhooksDataAccessLayer

#Region "Variable Webhooks Web API"
    Public Shared _mSqlCon As New SqlConnection
    Public Shared _mSqlCmd As SqlCommand
    Public Shared _mDataTable As New DataTable
    Public Shared _mDataAdapter As New SqlDataAdapter
    Public Shared _mDataset As New DataSet
    Public Shared _mStrSql As String
    Public Shared _mStrSql1 As String
    Public Shared _mStrSql2 As String
    Public Shared _mStrSql3 As String
    Public Shared Property _mStatus As String
    Public Shared Property _mMessage As String
    Public Shared Property _mData As Object
    Public Shared Property _mCode As Object

    Public Shared _mJsonObject As JObject
    Public Shared _mControlNo As String
    Public Shared _mAppStatus As String
    Public Shared _mFilaData As Byte()
    Public Shared _mCTCType As String
    Public Shared _mActionType As String

#End Region
#Region "Property Webhooks Web API"
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
#End Region
    'Post Model with parameters
    Public Shared Function _mPostValue(ByVal param As String, ByVal value As Object)
        Try
            'Call THE SPIDC WEB API CONFIG
            Spidc_Web_API_Config.WebApiConfig()
            'Parse the json object in variable
            _mJsonObject = JObject.Parse(value.ToString)
            'Check The  param If Exist
            Select Case param
                Case Spidc_Web_API_Config._mAppPostParamWebhooks
                    'Call The Data Access Layer
                    If WebhooksDataAccessLayer._mPostWebhooksEvent(value) Then
                        _mStatus = WebhooksDataAccessLayer._mStatus
                        _mData = WebhooksDataAccessLayer._mData
                        _mMessage = WebhooksDataAccessLayer._mMessage
                        _mCode = WebhooksDataAccessLayer._mCode
                        Return True
                    Else
                        _mStatus = WebhooksDataAccessLayer._mStatus
                        _mData = WebhooksDataAccessLayer._mData
                        _mMessage = WebhooksDataAccessLayer._mMessage
                        _mCode = WebhooksDataAccessLayer._mCode
                        Return False
                    End If
                Case Else
                    _mStatus = WebhooksDataAccessLayer._mStatus
                    _mData = WebhooksDataAccessLayer._mData
                    _mMessage = WebhooksDataAccessLayer._mMessage
                    _mCode = WebhooksDataAccessLayer._mCode
                    Return False
            End Select
        Catch ex As Exception
            _mStatus = "error"
            _mData = Nothing
            _mMessage = ex.Message
            _mCode = "500"
            Return False
        End Try
    End Function



  



End Class
