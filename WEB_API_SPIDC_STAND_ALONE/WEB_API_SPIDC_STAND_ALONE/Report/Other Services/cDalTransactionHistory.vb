
#Region "Imports"
Imports System.Data.SqlClient
Imports System.Web.HttpContext
#End Region
Public Class cDalTransactionHistory

#Region "Variables Data"
    Private _mSqlCon As SqlConnection
    Private _mQuery As String = Nothing
    Private _mSqlCommand As SqlCommand
    Private _mDataTable As DataTable
    Private _mSqlDataReader As SqlDataReader


#End Region

#Region "Properties Data"
    Public WriteOnly Property _pSqlConnection() As SqlConnection
        Set(value As SqlConnection)
            _mSqlCon = value
        End Set
    End Property
    Public ReadOnly Property _pQuery() As String
        Get
            Return _mQuery
        End Get
    End Property
    Public ReadOnly Property _pSqlCommand() As SqlCommand
        Get
            Return _mSqlCommand
        End Get
    End Property
    Public ReadOnly Property _pDataTable() As DataTable
        Get
            Try
                '----------------------------------
                Dim _nSqlDataAdapter As New SqlDataAdapter(_mSqlCommand)
                _mDataTable = New DataTable
                _nSqlDataAdapter.Fill(_mDataTable)

                Return _mDataTable
                '----------------------------------
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property
    Public ReadOnly Property _pSqlDataReader() As SqlDataReader
        Get
            Try
                '----------------------------------
                _mSqlDataReader = _mSqlCommand.ExecuteReader

                Return _mSqlDataReader
                '----------------------------------
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property
#End Region

#Region "Variables Field"
    Private _mEmail As String
    Private _mTransDate As Date
    Private _mTransNo As String
    Private _mAcctNo As String
    Private _mTransType As String
    Private _mStatus As String
#End Region

#Region "Properties Field"

    Public Property _pEmail As String
        Get
            Return _mEmail
        End Get
        Set(value As String)
            _mEmail = value
        End Set
    End Property

    Public Property _pTransDate As Date
        Get
            Return _mTransDate
        End Get
        Set(value As Date)
            _mTransDate = value
        End Set
    End Property
    Public Property _pTransNo As String
        Get
            Return _mTransNo
        End Get
        Set(value As String)
            _mTransNo = value
        End Set
    End Property
    Public Property _pAcctNo As String
        Get
            Return _mAcctNo
        End Get
        Set(value As String)
            _mAcctNo = value
        End Set
    End Property

    Public Property _pTransType As String
        Get
            Return _mTransType
        End Get
        Set(value As String)
            _mTransType = value
        End Set
    End Property
    Public Property _pStatus As String
        Get
            Return _mStatus
        End Get
        Set(value As String)
            _mStatus = value
        End Set
    End Property


#End Region

#Region "Properties Field Original"

#End Region

#Region "Routine Command"

    Public Sub _pSubSelectHistory(Email)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = _
                " SELECT ID, Email, Module, Type, Description, REPLACE(Particulars,CHAR(13),'')Particulars, Date,REPLACE(status,CHAR(13),'') Status FROM History where Email='" & Email & "' order by [Date] desc"
            _mQuery = _nQuery
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

        Catch ex As Exception

        End Try

    End Sub

    Public Sub _pSubInsertHistory(ByVal [ID] As String, _
                                  ByVal [Email] As String, _
                                  ByVal [Module] As String, _
                                  ByVal [Type] As String, _
                                  ByVal [Description] As String, _
                                  ByVal [Particulars] As String, _
                                  ByVal [Status] As String)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "INSERT INTO [dbo].[History] VALUES" & _
           "('" & [ID] & "','" & [Email] & "','" & [Module] & "','" & [Type] & "','" & _
           [Description] & "','" & [Particulars] & "',getdate(),'" & [Status] & "')"

            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    'Jay Sitjar 12-05-2023
    Public Sub _pSubInsertPayMaya_Transactions(ByVal API_TYPE As String, _
                                 ByVal ACCTNO As String, _
                                 ByVal EMAIL As String, _
                                 ByVal JSON_POST As String, _
                                 ByVal JSON_RESPONSE As String, _
                                 ByVal RRN As String)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "INSERT INTO [dbo].[PAYMAYA_Transactions] VALUES (getdate(),'" & API_TYPE & "','" & ACCTNO & "','" & EMAIL & "','" & JSON_POST & "','" & JSON_RESPONSE & "','" & RRN & "')"
            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
        Catch ex As Exception

        End Try
    End Sub

    'Public Sub _pSubUpdatePayMaya_Transactions(ByVal API_TYPE As String, _
    '                           ByVal ACCTNO As String, _
    '                           ByVal EMAIL As String, _
    '                           ByVal JSON_POST As String, _
    '                           ByVal JSON_RESPONSE As String, _
    '                           ByVal RRN As String)
    '    Try
    '        Dim _nQuery As String = Nothing
    '        _nQuery = "Update PayMaya_Transactions set JSON_RESPONSE = '" & JSON_RESPONSE & "' where " & _
    '            "ACCTNO = '" & ACCTNO & "' and " & _
    '            "EMAIL = '" & EMAIL & "' and " & _
    '            "JSON_POST = '" & JSON_POST & "' and " & _
    '            "RRN = '" & RRN & "'"

    '        Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
    '        _nSqlCommand.ExecuteNonQuery()

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub

   
  


#End Region
End Class
