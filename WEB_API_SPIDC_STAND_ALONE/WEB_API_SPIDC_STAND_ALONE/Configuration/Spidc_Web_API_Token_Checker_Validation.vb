Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq

Public Class Spidc_Web_API_Token_Checker_Validation
    'SPIDC Global Connections
    Dim Spidc_Web_API_Global_Connection As New Spidc_Web_API_Global_Connection

#Region "Variable WEB API Token Checker"
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
    Public Shared Property _mData As Object
    Public Shared Property _mMessage As String
    Public Shared Property _mCode As Object

    Private Shared _mJsonObject As JObject
    Private Shared _mControlNo As String
    Private Shared _mAppStatus As String
    Private Shared _mFilaData As Byte()
    Private Shared _mCTCType As String
    Private Shared _mActionType As String
    Private Shared _mDataResponse As String

    Private Shared _mMiddleNameNotRequired As String
    Private Shared _mGrossNotRequired As String
    Private Shared _mSalariesNotRequired As String
    Private Shared _mIncomeRealPropertyNotRequired As String
#End Region
#Region "Property WEB API Token Checker"
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
    Public Shared Function _checkAuthorization(ByVal token As String) As String
        _mStrSql = "SELECT KeyToken FROM Registered WHERE KeyToken='" & token & "'"
        _mDataset = New DataSet
        _mSqlCmd = New SqlCommand(_mStrSql, Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS)
        _mDataAdapter = New SqlDataAdapter(_mSqlCmd)
        _mDataAdapter.Fill(_mDataset)
        If _mDataset.Tables(0).Rows.Count > 0 Then
            Return _mDataset.Tables(0).Rows(0)("KeyToken").ToString()
        Else
            Return "No Token Exist"
        End If
    End Function

End Class
