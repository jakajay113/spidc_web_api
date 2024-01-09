Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Net.Http
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq

Public Class SpidcModel
#Region "Variable Web API"
    Public Shared _mSqlCon As New SqlConnection
    Public Shared _mSqlCmd As SqlCommand
    Public Shared _mDataTable As New DataTable
    Public Shared _mDataAdapter As New SqlDataAdapter
    Public Shared _mDataset As New DataSet
    Public Shared _mStrSql As String
    Public Shared Property _mStatus As String
    Public Shared Property _mMessage As String
    Public Shared Property _mData As Object
    Public Shared Property _mCode As Object

    Private Shared _mJsonObject As JObject
    Private Shared _mEmployeeID As String
    Private Shared _mFname As String
    Private Shared _mMname As String
    Private Shared _mLname As String
    Private Shared _mEmail As String
    Private Shared _mPhone As String
    Private Shared _mPosition As String
    Private Shared _mSStatus As String


#End Region

#Region "Property Web API "
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


#Region "Routine Web API"
    '--------------------------------------------------------------------------------------GET METHOD PARAMETERS--------------------------------------------------------------------------------------------------
    'GET  
    Public Shared Function _mGetValues(ByVal param As String) As Boolean
        Try
            _mStrSql = "SELECT * FROM Employee ORDER BY EmployeeID ASC"
            _mDataset = New DataSet
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            _mDataAdapter = New SqlDataAdapter(_mSqlCmd)
            _mDataAdapter.Fill(_mDataset)
            If _mDataset.Tables(0).Rows.Count > 0 Then
                _mStatus = "success"
                _mData = _mDataset.Tables(0)
                _mMessage = "Data retrieved successfully"
                _mCode = "200"
            Else
                _mStatus = "Success"
                _mData = Nothing
                _mMessage = "No data in rows"
                _mCode = "200"
            End If
            Return True
        Catch ex As Exception
            _mStatus = "error"
            _mData = Nothing
            _mMessage = ex.Message
            _mCode = "500"
            Return False
        End Try
        _mSqlCmd.Dispose()
        _mSqlCon.Close()
    End Function

    '----------------------------------------------------------------------------------GET METHOD WITH PARAMETERS-------------------------------------------------------------------------------------------------
    'GET with Parameters
    Public Shared Function _mGetValue(ByVal param As String, ByVal id As String) As Boolean
        Try
            _mStrSql = "SELECT * FROM Employee WHERE EmployeeID='" & id & "'"
            _mDataset = New DataSet
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            _mDataAdapter = New SqlDataAdapter(_mSqlCmd)
            _mDataAdapter.Fill(_mDataset)
            If _mDataset.Tables(0).Rows.Count > 0 Then
                _mStatus = "success"
                _mData = _mDataset.Tables(0)
                _mMessage = "Data retrieved successfully"
                _mCode = "200"
            Else
                _mStatus = "Success"
                _mData = Nothing
                _mMessage = "No data in rows"
                _mCode = "200"
            End If
            Return True
        Catch ex As Exception
            _mStatus = "error"
            _mData = Nothing
            _mMessage = ex.Message
            _mCode = "500"
            Return False
        End Try
        _mSqlCmd.Dispose()
        _mSqlCon.Close()
    End Function
    '-----------------------------------------------------------------------------------POST METHOD PARAMETERS-----------------------------------------------------------------------------------------------------
    'POST  with Parameters
    Public Shared Function _mPostValue(ByVal param As String, ByVal value As Object)
        Try
            'Parse the json object in variable
            _mJsonObject = JObject.Parse(value.ToString)
            _mEmployeeID = _mJsonObject("EmployeeID").ToString()
            _mFname = _mJsonObject("Fname").ToString()
            _mMname = _mJsonObject("Mname").ToString()
            _mLname = _mJsonObject("Lname").ToString()
            _mEmail = _mJsonObject("Email").ToString()
            _mPhone = _mJsonObject("Phone").ToString()
            _mPosition = _mJsonObject("Position").ToString()
            _mSStatus = _mJsonObject("Status").ToString()

            _mStrSql = "INSERT INTO  Employee (EmployeeID, Fname, Mname, Lname, Email, Phone, Position, [Status], DateAdded)" & _
                       "VALUES('" & _mEmployeeID & "', '" & _mFname & "', '" & _mMname & "', '" & _mLname & "', '" & _mEmail & "', '" & _mPhone & "', '" & _mPosition & "', '" & _mSStatus & "', GETDATE())"
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            _mSqlCmd.ExecuteNonQuery()
            _mStatus = "success"
            _mData = Nothing
            _mMessage = "Data saved successfully"
            _mCode = "200"
            Return True
        Catch ex As Exception
            _mStatus = "error"
            _mData = Nothing
            _mMessage = ex.Message
            _mCode = "500"
            Return False
        End Try
        _mSqlCmd.Dispose()
        _mSqlCon.Close()
    End Function
    '------------------------------------------------------------------------------------PUT METHOD PARAMETERS----------------------------------------------------------------------------------------------------
    'PUT  with Parameters
    Public Shared Function _mPutValue(ByVal param As String, ByVal value As Object, ByVal id As String)
        Try
            'Parse the json object in variable
            _mJsonObject = JObject.Parse(value.ToString)
            _mFname = _mJsonObject("Fname").ToString()
            _mMname = _mJsonObject("Mname").ToString()
            _mLname = _mJsonObject("Lname").ToString()
            _mEmail = _mJsonObject("Email").ToString()
            _mPhone = _mJsonObject("Phone").ToString()
            _mPosition = _mJsonObject("Position").ToString()
            _mSStatus = _mJsonObject("Status").ToString()

            _mStrSql = "UPDATE Employee SET " & _
                       "Fname = '" & _mFname & "'," & _
                       "Mname = '" & _mMname & "'," & _
                       "Lname = '" & _mLname & "'," & _
                       "Email = '" & _mEmail & "'," & _
                       "Phone = '" & _mPhone & "', " & _
                       "Position = '" & _mPosition & "', " & _
                       "[Status] ='" & _mSStatus & "'" & _
                       "WHERE EmployeeID  = '" & id & "'"
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            _mSqlCmd.ExecuteNonQuery()
            _mStatus = "success"
            _mData = Nothing
            _mMessage = "Data updated successfully"
            _mCode = "200"
            Return True
        Catch ex As Exception
            _mStatus = "error"
            _mData = Nothing
            _mMessage = ex.Message
            _mCode = "500"
            Return False
        End Try
        _mSqlCmd.Dispose()
        _mSqlCon.Close()
    End Function
    '------------------------------------------------------------------------------------DELETE METHOD PARAMETERS----------------------------------------------------------------------------------------------------
    'DELETE with Parameters
    Public Shared Function _mDeleteValue(ByVal param As String, ByVal id As String)
        Try
            _mStrSql = "DELETE FROM Employee WHERE EmployeeID = '" & id & "'"
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            _mSqlCmd.ExecuteNonQuery()
            _mStatus = "success"
            _mData = Nothing
            _mMessage = "Data deleted successfully"
            _mCode = "200"
            Return True
        Catch ex As Exception
            _mStatus = "error"
            _mData = Nothing
            _mMessage = ex.Message
            _mCode = "500"
            Return False
        End Try
        _mSqlCmd.Dispose()
        _mSqlCon.Close()
    End Function
#End Region


End Class
