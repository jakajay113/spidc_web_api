Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Net.Http
Public Class EISModel

    'SPIDC Config
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config

#Region "Variable EIS Web API"
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
#End Region
#Region "Property EIS Web API"
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

#Region "Routine EIS Web API"
    '--------------------------------------------------------------------------------------GET METHOD PARAMETERS--------------------------------------------------------------------------------------------------
    'GET  
    Public Shared Function _mGetValues(ByVal param As String) As Boolean
        Try
            Spidc_Web_API_Config.WebApiConfig()
            'Chect The Param To Find Out What Type Of Query to Build
            Select Case param
                Case Spidc_Web_API_Config._mAppGetParam1Eis
                    _mStrSql = "SELECT DeptCode,Description,ExpenseType, SUM(isnull(TotalCR,0)) as TotalCR , " & _
                                " SUM(isnull(TotalDR,0)) as TotalDR FROM (SELECT ROA.DeptCode, Departments.Description, ROA.ExpenseType,ROA.OblNo,ISNULL(SUM(ROAExtn.Credit), 0) AS TotalCR,  " & _
                                " (SELECT ISNULL(SUM(VouchExtn.Debit), 0) AS ss FROM Voucher LEFT OUTER JOIN  VouchExtn ON Voucher.RefNo = VouchExtn.RefNo    " & _
                                " WHERE Voucher.DeptCode = ROA.DeptCode   AND Voucher.OblNo = ROA.OblNo and Voucher.OblDate = ROA.OblDate) AS TotalDR  " & _
                                " FROM ROAExtn " & _
                                " LEFT OUTER JOIN ROA ON ROAExtn.Grp = ROA.Grp AND ROAExtn.OblNo = ROA.OblNo  " & _
                                " LEFT OUTER JOIN Departments ON ROA.DeptCode = Departments.DeptCode  " & _
                                " WHERE (ROA.OblDate BETWEEN '1/1/2023' AND '9/4/2023') AND (ROA.Grp = 'ROA') AND (Departments.ForYear = '2023') " & _
                                " GROUP BY ROA.DeptCode, ROA.ExpenseType, Departments.Description, ROA.OblNo , ROA.OblDate) jhe GROUP BY DeptCode, ExpenseType, Description " & _
                                " ORDER BY DeptCode "
                Case Spidc_Web_API_Config._mAppGetParam2Eis
                    '_mStrSql = "SELECT * FROM Employee ORDER BY EmployeeID ASC"
            End Select

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
        'Try
        '    _mStrSql = "SELECT * FROM Employee WHERE EmployeeID='" & id & "'"
        '    _mDataset = New DataSet
        '    _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
        '    _mDataAdapter = New SqlDataAdapter(_mSqlCmd)
        '    _mDataAdapter.Fill(_mDataset)
        '    If _mDataset.Tables(0).Rows.Count > 0 Then
        '        _mStatus = "success"
        '        _mData = _mDataset.Tables(0)
        '        _mMessage = "Data retrieved successfully"
        '        _mCode = "200"
        '    Else
        '        _mStatus = "Success"
        '        _mData = Nothing
        '        _mMessage = "No data in rows"
        '        _mCode = "200"
        '    End If
        '    Return True
        'Catch ex As Exception
        '    _mStatus = "error"
        '    _mData = Nothing
        '    _mMessage = ex.Message
        '    _mCode = "500"
        '    Return False
        'End Try
        '_mSqlCmd.Dispose()
        '_mSqlCon.Close()
        Return False
    End Function
    '-----------------------------------------------------------------------------------POST METHOD PARAMETERS-----------------------------------------------------------------------------------------------------
    'POST  with Parameters
    Public Shared Function _mPostValue(ByVal param As String, ByVal value As String())
        'Try
        '    _mStrSql = "INSERT INTO  Employee (EmployeeID, Fname, Mname, Lname, Email, Phone, Position, [Status], DateAdded)" & _
        '               "VALUES('" & value(0).ToString() & "', '" & value(1).ToString() & "', '" & value(2).ToString() & "', '" & value(3).ToString() & "', '" & value(4).ToString() & "', '" & value(5).ToString() & "', '" & value(6).ToString() & "', '" & value(7).ToString() & "', GETDATE())"
        '    _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
        '    _mSqlCmd.ExecuteNonQuery()
        '    _mStatus = "success"
        '    _mData = Nothing
        '    _mMessage = "Data saved successfully"
        '    _mCode = "200"
        '    Return True
        'Catch ex As Exception
        '    _mStatus = "error"
        '    _mData = Nothing
        '    _mMessage = ex.Message
        '    _mCode = "500"
        '    Return False
        'End Try
        '_mSqlCmd.Dispose()
        '_mSqlCon.Close()
        Return False
    End Function
    '------------------------------------------------------------------------------------PUT METHOD PARAMETERS----------------------------------------------------------------------------------------------------
    'PUT  with Parameters
    Public Shared Function _mPutValue(ByVal param As String, ByVal value As String(), ByVal id As String)
        'Try
        '    _mStrSql = "UPDATE Employee SET " & _
        '               "Fname = '" & value(0).ToString() & "'," & _
        '               "Mname = '" & value(1).ToString() & "'," & _
        '               "Lname = '" & value(2).ToString() & "'," & _
        '               "Email = '" & value(3).ToString() & "'," & _
        '               "Phone = '" & value(4).ToString() & "', " & _
        '               "Position = '" & value(5).ToString() & "', " & _
        '               "[Status] ='" & value(6).ToString() & "'" & _
        '               "WHERE EmployeeID  = '" & id & "'"
        '    _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
        '    _mSqlCmd.ExecuteNonQuery()
        '    _mStatus = "success"
        '    _mData = Nothing
        '    _mMessage = "Data updated successfully"
        '    _mCode = "200"
        '    Return True
        'Catch ex As Exception
        '    _mStatus = "error"
        '    _mData = Nothing
        '    _mMessage = ex.Message
        '    _mCode = "500"
        '    Return False
        'End Try
        '_mSqlCmd.Dispose()
        '_mSqlCon.Close()
        Return False
    End Function
    '------------------------------------------------------------------------------------DELETE METHOD PARAMETERS----------------------------------------------------------------------------------------------------
    'DELETE with Parameters
    Public Shared Function _mDeleteValue(ByVal param As String, ByVal id As String)
        'Try
        '    _mStrSql = "DELETE FROM Employee WHERE EmployeeID = '" & id & "'"
        '    _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
        '    _mSqlCmd.ExecuteNonQuery()
        '    _mStatus = "success"
        '    _mData = Nothing
        '    _mMessage = "Data deleted successfully"
        '    _mCode = "200"
        '    Return True
        'Catch ex As Exception
        '    _mStatus = "error"
        '    _mData = Nothing
        '    _mMessage = ex.Message
        '    _mCode = "500"
        '    Return False
        'End Try
        '_mSqlCmd.Dispose()
        '_mSqlCon.Close()
        Return False
    End Function

#End Region

End Class
