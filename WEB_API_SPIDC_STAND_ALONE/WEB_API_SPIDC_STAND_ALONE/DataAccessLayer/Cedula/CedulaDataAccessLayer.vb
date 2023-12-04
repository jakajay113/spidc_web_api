Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.IO

Public Class CedulaDataAccessLayer
#Region "Variable Data Access Layer CEDULA Web API"
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config
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
    Public Shared Property _mDataObjectDatatable As Object
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
#Region "Property Data Access Layer CEDULA Web API"
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

    '--------------------------------------------------------------------------------------GET Data Access Layer------------------------------------------------------------------------------------------------
    'GET Data Access Layer
    Public Shared Function _mDataAccessLayerGetValues() As Boolean
        Try
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
                _mStatus = "success"
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
        Return False
    End Function
    '----------------------------------------------------------------------------------GET with Parameters Data Access Layer-------------------------------------------------------------------------------------------------
    'GET with Parameters Data Access Layer
    Public Shared Function _mDataAccessLayerGetValue(Optional ByRef param As String = Nothing) As Boolean
        Try
            'Call The Web Config
            Spidc_Web_API_Config.WebApiConfig()
            'Check Param if If Get Account Codes else Do Other
            Select Case param
                Case Spidc_Web_API_Config._mAppGetParam10Toims
                    _mDataset = New DataSet
                    _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
                    _mDataAdapter = New SqlDataAdapter(_mSqlCmd)
                    _mDataAdapter.Fill(_mDataset)
                    If _mDataset.Tables(0).Rows.Count > 0 Then
                        _mStatus = "success"
                        _mData = _mDataset.Tables(0).Rows(0)("Code2")
                        _mMessage = "Data retrieved successfully"
                        _mCode = "200"
                    Else
                        _mStatus = "Success"
                        _mData = Nothing
                        _mMessage = "No data in rows"
                        _mCode = "200"
                    End If

                Case Else
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
            End Select
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
        Return False
    End Function

    '-----------------------------------------------------------------------------------POST with Parameters Data Access Layer-----------------------------------------------------------------------------------------------------
    'POST  with Parameters Data Access Layer
    Public Shared Function _mDataAccessLayerPostValue(Optional ByVal _mCTCType As String = Nothing,
                                                      Optional _mFileData As Byte() = Nothing,
                                                      Optional ByVal _mMiddleName As String = Nothing,
                                                      Optional ByVal _mGrossReciept As String = Nothing,
                                                      Optional ByVal _mSalaries As String = Nothing,
                                                      Optional ByVal _mIncomeRealProperty As String = Nothing,
                                                      Optional ByVal _mRealProperty As String = Nothing,
                                                      Optional ByVal _mEmail As String = Nothing
                                                      )
        Try

            If Not String.IsNullOrEmpty(_mEmail) Then 'Login
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
            Else 'Other
                Select Case _mCTCType
                    Case "Individual Cedula"
                        _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
                        With _mSqlCmd.Parameters
                            .AddWithValue("@MiddleName", IIf(String.IsNullOrEmpty(_mMiddleName), DBNull.Value, _mMiddleName))
                            .AddWithValue("@GrossReciept", IIf(String.IsNullOrEmpty(_mGrossReciept), DBNull.Value, _mGrossReciept))
                            .AddWithValue("@Salaries", IIf(String.IsNullOrEmpty(_mSalaries), DBNull.Value, _mSalaries))
                            .AddWithValue("@IncomeRealProperty", IIf(String.IsNullOrEmpty(_mIncomeRealProperty), DBNull.Value, _mIncomeRealProperty))
                            .AddWithValue("@File", _mFileData)
                        End With
                        _mSqlCmd.ExecuteNonQuery()
                        _mDataResponse = Nothing
                    Case "Corporation Cedula"
                        _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
                        With _mSqlCmd.Parameters
                            .AddWithValue("@RealProperty", IIf(String.IsNullOrEmpty(_mRealProperty), DBNull.Value, _mRealProperty))
                            .AddWithValue("@GrossReciept", IIf(String.IsNullOrEmpty(_mGrossReciept), DBNull.Value, _mGrossReciept))
                            .AddWithValue("@File", _mFileData)
                        End With
                        _mSqlCmd.ExecuteNonQuery()
                        _mDataResponse = Nothing
                End Select
                _mStatus = "success"
                _mData = _mDataResponse
                _mMessage = "Data successfully saved."
                _mCode = "200"
                Return True
            End If
        Catch ex As Exception
            _mStatus = "error"
            _mData = Nothing
            _mMessage = ex.Message
            _mCode = "500"
            Return False
        End Try
        _mSqlCmd.Dispose()
        _mSqlCon.Close()
        Return False
    End Function
    '------------------------------------Generate Control No ---------------------------------------------------
    Public Shared Function _FnAutoGenID(ByVal _nModuleID As String, Optional _nRefCode As String = Nothing) As String
        _FnAutoGenID = Nothing
        Try
            Dim _nClass As New cDalAutoGenID
            _nClass._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TIMS
            Dim _nResult = _nClass._FnAutoGenID(_nModuleID, _nRefCode)
            Return _nResult
        Catch ex As Exception
        End Try
    End Function
    '------------------------------------End Generate Control No ---------------------------------------------------

    '------------------------------------------------------------------------------------PUT with Parameters Data Access Layer---------------------------------------------------------------------------------------------------
    'PUT  with Parameters Data Access Layer
    Public Shared Function _mDataAccessLayerPutValue(Optional ByVal _mCTCType As String = Nothing,
                                                     Optional ByVal _mActionType As String = Nothing,
                                                     Optional ByVal _mMiddleName As String = Nothing,
                                                     Optional ByVal _mGrossReciept As String = Nothing,
                                                     Optional ByVal _mSalaries As String = Nothing,
                                                     Optional ByVal _mIncomeRealProperty As String = Nothing,
                                                     Optional ByVal _mRealProperty As String = Nothing)
        Try
            'Check Action Type If SaveChanges Or Approved Or Rejected
            If _mActionType = "Approved" Or _mActionType = "Rejected" Or _mActionType = "Remarks" Then
                _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
                _mSqlCmd.ExecuteNonQuery()
            Else
                Select Case _mCTCType
                    Case "Individual Cedula"
                        _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
                        With _mSqlCmd.Parameters
                            .AddWithValue("@MiddleName", IIf(String.IsNullOrEmpty(_mMiddleName), DBNull.Value, _mMiddleName))
                            .AddWithValue("@GrossReciept", IIf(String.IsNullOrEmpty(_mGrossReciept), DBNull.Value, _mGrossReciept))
                            .AddWithValue("@Salaries", IIf(String.IsNullOrEmpty(_mSalaries), DBNull.Value, _mSalaries))
                            .AddWithValue("@IncomeRealProperty", IIf(String.IsNullOrEmpty(_mIncomeRealProperty), DBNull.Value, _mIncomeRealProperty))
                        End With
                        _mSqlCmd.ExecuteNonQuery()
                    Case "Corporation Cedula"
                        _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
                        With _mSqlCmd.Parameters
                            .AddWithValue("@RealProperty", IIf(String.IsNullOrEmpty(_mRealProperty), DBNull.Value, _mRealProperty))
                            .AddWithValue("@GrossReciept", IIf(String.IsNullOrEmpty(_mGrossReciept), DBNull.Value, _mGrossReciept))
                        End With
                        _mSqlCmd.ExecuteNonQuery()
                End Select
            End If
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
        Return False
    End Function

    '------------------------------------------------------------------------------------DELETE with Parameters  Data Access Layer----------------------------------------------------------------------------------------------------
    'DELETE with Parameters Data Access Layer
    Public Shared Function _mDataAccessLayerDeleteValue(ByVal param As String, ByVal id As String)
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




End Class
