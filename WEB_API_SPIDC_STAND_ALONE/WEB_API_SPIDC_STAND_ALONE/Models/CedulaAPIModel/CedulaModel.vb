Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.IO

Public Class CedulaModel
    'SPIDC Config
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config
    'Cedula Data Access Layer
    Private Shared CedulaDataAccessLayer As New CedulaDataAccessLayer
    'Login Checker
    Private Shared cBllRegistered As New cBllRegistered

#Region "Variable CEDULA Web API"
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

    Public Shared _mMiddleNameNotRequired As String
    Public Shared _mGrossNotRequired As String
    Public Shared _mSalariesNotRequired As String
    Public Shared _mIncomeRealPropertyNotRequired As String


    Private Shared _mMiddleName As String 
    Private Shared _mGrossReciept As String
    Private Shared _mSalaries As String
    Private Shared _mIncomeRealProperty As String
    Private Shared _mRealProperty As String

    Private Shared _mDecryptPass As String = Nothing
    Private Shared _mEncryptPass As String = Nothing
    Private Shared _mUserKeySalt As Byte()
    Private Shared _mKeyToken As Object



#End Region
#Region "Property CEDULA Web API"
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

    'Get Model 
    Public Shared Function _mGetValues(ByVal param As String) As Boolean
        Try
            'Check The  Method param is exist and set the connection type
            Select Case Spidc_Web_API_Param_Checker_Config._mCheckParamCedula(param)
                Case Spidc_Web_API_Config._mAppNameCedula
                    CedulaDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TIMS
                Case Spidc_Web_API_Config._mAppNameTOIMS
                    CedulaDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TOIMS
                Case Spidc_Web_API_Config._mAppNameTIMS
                    CedulaDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TIMS
            End Select
            'Check The Param To Build The Query Type 
            Select Case param
                Case Spidc_Web_API_Config._mAppGetParam1Tims
                    CedulaDataAccessLayer._mStrSql = "SELECT * FROM VW_CTC_Online_Application WHERE CTC_Type ='Individual Cedula'"
                Case Spidc_Web_API_Config._mAppGetParam2Tims
                    CedulaDataAccessLayer._mStrSql = "SELECT * FROM VW_CTC_Online_Application WHERE CTC_Type ='Corporation Cedula'"
                Case Spidc_Web_API_Config._mAppGetParam3Toims
                    CedulaDataAccessLayer._mStrSql = "SELECT  Amount FROM CTCSETUPCODE LEFT OUTER JOIN coa ON CTCSETUPCODE.AccountCd = coa.acctno  WHERE CTCSETUPCODE.Form_Use = N'BF0016'"
                Case Spidc_Web_API_Config._mAppGetParam4Toims
                    CedulaDataAccessLayer._mStrSql = "SELECT Amount FROM CTCSETUPCODE LEFT OUTER JOIN coa ON CTCSETUPCODE.AccountCd = coa.acctno  WHERE CTCSETUPCODE.Form_Use = N'BF907'"
                Case Spidc_Web_API_Config._mAppGetParam5Toims
                    CedulaDataAccessLayer._mStrSql = "SELECT top 1 Value23 AS INDIVIDUALMULTIPLIER FROM SETTINGPERMOD WHERE frmname='CTCINDIVIDUAL'"
                Case Spidc_Web_API_Config._mAppGetParam6Toims
                    CedulaDataAccessLayer._mStrSql = "SELECT top 1 Value23 AS CORPORATIONMULTIPLIER FROM SETTINGPERMOD WHERE frmname='CTCCORPORATION'"
                Case Spidc_Web_API_Config._mAppGetParam7Toims
                    CedulaDataAccessLayer._mStrSql = "SELECT MONTH(GETDATE())AS CurrentDate,CtcMonth, CtcDay, CtcInterest, CtcSucceed FROM CtcInterest WHERE CtcMonth = 3"
                Case Spidc_Web_API_Config._mAppGetParam8Tims
                    CedulaDataAccessLayer._mStrSql = "SELECT * FROM VW_CTC_Online_Application"
            End Select

            'Call The Data Access Layer
            If CedulaDataAccessLayer._mDataAccessLayerGetValues() Then
                _mStatus = CedulaDataAccessLayer._mStatus
                _mData = CedulaDataAccessLayer._mData
                _mMessage = CedulaDataAccessLayer._mMessage
                _mCode = CedulaDataAccessLayer._mCode
            Else
                _mStatus = CedulaDataAccessLayer._mStatus
                _mData = CedulaDataAccessLayer._mData
                _mMessage = CedulaDataAccessLayer._mMessage
                _mCode = CedulaDataAccessLayer._mCode
            End If
            Return True
        Catch ex As Exception
            _mStatus = "error"
            _mData = Nothing
            _mMessage = ex.Message
            _mCode = "500"
            Return False
        End Try
    End Function

    'Get Model with parameters
    Public Shared Function _mGetValue(ByVal param As String, ByVal id As String) As Boolean
        Try
            'Check The  Method param is exist and set the connection type
            Select Case Spidc_Web_API_Param_Checker_Config._mCheckParamCedula(param)
                Case Spidc_Web_API_Config._mAppNameCedula
                    CedulaDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TIMS
                Case Spidc_Web_API_Config._mAppNameTOIMS
                    CedulaDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TOIMS
                Case Spidc_Web_API_Config._mAppNameTIMS
                    CedulaDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TIMS
            End Select
            'Check The Param To Build The Query Type 
            Select Case param
                Case Spidc_Web_API_Config._mAppGetParam9Tims
                    CedulaDataAccessLayer._mStrSql = "SELECT * FROM VW_CTC_Online_Application WHERE ControlNo ='" & id & "'"
            End Select
            'Call The Data Access Layer
            If CedulaDataAccessLayer._mDataAccessLayerGetValue() Then
                _mStatus = CedulaDataAccessLayer._mStatus
                _mData = CedulaDataAccessLayer._mData
                _mMessage = CedulaDataAccessLayer._mMessage
                _mCode = CedulaDataAccessLayer._mCode
            Else
                _mStatus = CedulaDataAccessLayer._mStatus
                _mData = CedulaDataAccessLayer._mData
                _mMessage = CedulaDataAccessLayer._mMessage
                _mCode = CedulaDataAccessLayer._mCode
            End If
            Return True
        Catch ex As Exception
            _mStatus = "error"
            _mData = Nothing
            _mMessage = ex.Message
            _mCode = "500"
            Return False
        End Try
    End Function

    'Post Model with parameters
    Public Shared Function _mPostValue(ByVal param As String, ByVal value As Object)
        Try
            'Check The  Method param is exist and set the connection type
            Select Case Spidc_Web_API_Param_Checker_Config._mCheckParamCedula(param)
                Case Spidc_Web_API_Config._mAppNameOAIMS
                    CedulaDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                Case Spidc_Web_API_Config._mAppNameCedula
                    CedulaDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TIMS
                Case Spidc_Web_API_Config._mAppNameTOIMS
                    CedulaDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TOIMS
                Case Spidc_Web_API_Config._mAppNameTIMS
                    CedulaDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TIMS
            End Select
            'Parse the json object in variable
            _mJsonObject = JObject.Parse(value.ToString)
            'Check The Param If For Login Or Not To Change the query type only login is diffrent login is returning a data the other not
            If param = Spidc_Web_API_Config._mAppPostParam Then

             
                If Not cBllRegistered._pFuncVerifyIfAccountIsRegistered(_mJsonObject("Email").ToString()) Then
                    _mStatus = Spidc_Web_API_Config._mApiResponese401Status
                    _mData = Spidc_Web_API_Config._mApiResponese401Data
                    _mMessage = "Unregistered Email Address."
                    _mCode = Spidc_Web_API_Config._mApiResponese401Code
                    Return False
                End If

                If Not cBllRegistered._pFuncVerifyIfAccountIsActivated(_mJsonObject("Email").ToString()) Then
                    _mStatus = Spidc_Web_API_Config._mApiResponese401Status
                    _mData = Spidc_Web_API_Config._mApiResponese401Data
                    _mMessage = "Email is not  yet Activated."
                    _mCode = Spidc_Web_API_Config._mApiResponese401Code
                    Return False
                End If

                If Not cBllRegistered._pFuncGetUserKeySalt(_mJsonObject("Email").ToString(), _mUserKeySalt) Then
                    _mStatus = Spidc_Web_API_Config._mApiResponese401Status
                    _mData = Spidc_Web_API_Config._mApiResponese401Data
                    _mMessage = "Failed to get Key. Please Try again."
                    _mCode = Spidc_Web_API_Config._mApiResponese401Code
                    Return False
                End If

                If Not cBllRegistered._pFuncVerifyUserIDUserKey(_mJsonObject("Email").ToString(), _mJsonObject("Password").ToString(), _mUserKeySalt) Then
                    _mStatus = Spidc_Web_API_Config._mApiResponese401Status
                    _mData = Spidc_Web_API_Config._mApiResponese401Data
                    _mMessage = "Incorrect Password."
                    _mCode = Spidc_Web_API_Config._mApiResponese401Code
                    Return False
                End If
                'Sql Query String
                CedulaDataAccessLayer._mStrSql = "SELECT UserID,KeyToken,UserType,LastName,FirstName,MiddleName,SetupGender,Office,UserLevel FROM Registered WHERE UserID='" & _mJsonObject("Email").ToString() & "'"
                'Call The Data Access Layer
                If CedulaDataAccessLayer._mDataAccessLayerPostValue(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _mJsonObject("Email").ToString()) Then
                    _mStatus = CedulaDataAccessLayer._mStatus
                    _mData = CedulaDataAccessLayer._mData
                    _mMessage = CedulaDataAccessLayer._mMessage
                    _mCode = CedulaDataAccessLayer._mCode
                    Return True
                Else
                    _mStatus = CedulaDataAccessLayer._mStatus
                    _mData = CedulaDataAccessLayer._mData
                    _mMessage = CedulaDataAccessLayer._mMessage
                    _mCode = CedulaDataAccessLayer._mCode
                    Return False
                End If

            Else
                'Generate ControlNo Or TransactionNO
                _mControlNo = _FnAutoGenID("CTC_ControlNo", "WEB-CTC")
                'Status
                _mAppStatus = "Pending"
                'Convert File Base64 To Byte 
                _mFilaData = Convert.FromBase64String(_mJsonObject("FileBase64").ToString())
                Select Case param
                    Case Spidc_Web_API_Config._mAppPostParam1Tims
                        'Type
                        _mCTCType = "Individual Cedula"
                        'Parameters value this can be null 
                        _mMiddleName = _mJsonObject("MiddleName").ToString()
                        _mGrossReciept = _mJsonObject("GrossReciept").ToString()
                        _mSalaries = _mJsonObject("Salaries").ToString()
                        _mIncomeRealProperty = _mJsonObject("IncomeRealProperty").ToString()
                        _mRealProperty = Nothing
                        _mStrSql1 = "INSERT INTO  CTC_Online_Application (Email, ControlNo, CTC_Type, ForYear, FirstName, MiddleName, LastName, BirthPlace, BirthDate, [Address], Gender, CivilStatus, Citizenship, [Weight], Height, TIN, Profession, BasicCommunityTax, GrossReceipts, " & _
                                                          "SalariesOrGross, IncomeFromRealProperty, Total, Interest, AmountToPay, TransactionDate, [Status])" & _
                                                          "VALUES('" & _mJsonObject("Email").ToString() & "', '" & _mControlNo & "', '" & _mCTCType & "', YEAR(GETDATE()), '" & _mJsonObject("FirstName").ToString() & "', @MiddleName, '" & _mJsonObject("LastName").ToString() & "', '" & _mJsonObject("BirthPlace").ToString() & "', '" & _mJsonObject("BirthDate").ToString() & "', '" & _mJsonObject("Address").ToString() & "', '" & _mJsonObject("Gender").ToString() & "', '" & _mJsonObject("CivilStatus").ToString() & "', " & _
                                                          "'" & _mJsonObject("Citizenship").ToString() & "', '" & _mJsonObject("Weight").ToString() & "', '" & _mJsonObject("Height").ToString() & "', '" & _mJsonObject("TIN").ToString() & "', '" & _mJsonObject("Profession").ToString() & "', '" & _mJsonObject("BasicTax").ToString() & "', @GrossReciept, @Salaries, @IncomeRealProperty, '" & _mJsonObject("Total").ToString() & "', " & _
                                                          "'" & _mJsonObject("Interest").ToString() & "', '" & _mJsonObject("AmountToPay").ToString() & "', GETDATE(), '" & _mAppStatus & "');"
                    Case Spidc_Web_API_Config._mAppPostParam2Tims
                        'Type
                        _mCTCType = "Corporation Cedula"
                        'Parameters value this can be null 
                        _mMiddleName = Nothing
                        _mGrossReciept = _mJsonObject("GrossReciept").ToString()
                        _mSalaries = Nothing
                        _mIncomeRealProperty = Nothing
                        _mRealProperty = _mJsonObject("RealProperty").ToString()
                        _mStrSql1 = "INSERT INTO CTC_Online_Application (Email,ControlNo, CTC_Type, ForYear,CompanyName,[Address],KindOfOrganization,BusinessNature,PlaceOfOrganization,DateOfOrganization,BasicCommunityTax,RealProperty,GrossReceipts,Total,Interest,AmountToPay,TransactionDate,[Status]) " & _
                                    " VALUES('" & _mJsonObject("Email").ToString() & "', '" & _mControlNo & "', '" & _mCTCType & "', YEAR(GETDATE()), '" & _mJsonObject("CompanyName").ToString() & "', '" & _mJsonObject("Address").ToString() & "', '" & _mJsonObject("KindofOrganization").ToString() & "', '" & _mJsonObject("BusinessNature").ToString() & "', '" & _mJsonObject("PlaceofOrganization").ToString() & "', '" & _mJsonObject("DateofOrganization").ToString() & "', '" & _mJsonObject("BasicTax").ToString() & "', @RealProperty, @GrossReciept, '" & _mJsonObject("Total").ToString() & "', '" & _mJsonObject("Interest").ToString() & "', '" & _mJsonObject("AmountToPay").ToString() & "', GETDATE(), '" & _mAppStatus & "') "
                End Select
                'For File Attachement
                _mStrSql2 = " INSERT INTO CTC_Online_Application_File_Attachement (Email,ControlNo,FileName, FileSize, FileType, [File]) " & _
                                                  " VALUES('" & _mJsonObject("Email").ToString() & "','" & _mControlNo & "','" & _mJsonObject("FileName").ToString() & "','" & _mJsonObject("FileSize").ToString() & "','" & _mJsonObject("FileType").ToString() & "',@File); "
                'Sql Query String
                CedulaDataAccessLayer._mStrSql = _mStrSql1 & _mStrSql2
                'Call The Data Access ALayer
                If CedulaDataAccessLayer._mDataAccessLayerPostValue(_mCTCType, _mFilaData, _mMiddleName, _mGrossReciept, _mSalaries, _mIncomeRealProperty, _mRealProperty) Then
                    _mStatus = CedulaDataAccessLayer._mStatus
                    _mData = CedulaDataAccessLayer._mData
                    _mMessage = CedulaDataAccessLayer._mMessage
                    _mCode = CedulaDataAccessLayer._mCode
                Else
                    _mStatus = CedulaDataAccessLayer._mStatus
                    _mData = CedulaDataAccessLayer._mData
                    _mMessage = CedulaDataAccessLayer._mMessage
                    _mCode = CedulaDataAccessLayer._mCode
                End If
                Return True
            End If
        Catch ex As Exception
            _mStatus = "error"
            _mData = Nothing
            _mMessage = ex.Message
            _mCode = "500"
            Return False
        End Try
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

    'Put Model with parameters
    Public Shared Function _mPutValue(ByVal param As String, ByVal value As Object, ByVal id As String)
        Try
            'Check The  Method param is exist and set the connection type
            Select Case Spidc_Web_API_Param_Checker_Config._mCheckParamCedula(param)
                Case Spidc_Web_API_Config._mAppNameOAIMS
                    CedulaDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                Case Spidc_Web_API_Config._mAppNameCedula
                    CedulaDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TIMS
                Case Spidc_Web_API_Config._mAppNameTOIMS
                    CedulaDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TOIMS
                Case Spidc_Web_API_Config._mAppNameTIMS
                    CedulaDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TIMS
            End Select
            'Parse the json object in variable
            _mJsonObject = JObject.Parse(value.ToString)
            'Check The Param To Build The Query Type  " & _mJsonObject("Action").ToString() & "
            Select Case param
                Case Spidc_Web_API_Config._mAppPutParam1Tims
                    Select Case _mJsonObject("Action").ToString()
                        Case "SaveChanges"
                            Select Case _mJsonObject("ApplicationType").ToString()
                                Case "Individual Cedula"
                                    'Type
                                    _mCTCType = _mJsonObject("ApplicationType").ToString()
                                    _mActionType = _mJsonObject("Action").ToString()
                                    'Parameters value this can be null 
                                    _mMiddleName = _mJsonObject("MiddleName").ToString()
                                    _mGrossReciept = _mJsonObject("GrossReciept").ToString()
                                    _mSalaries = _mJsonObject("Salaries").ToString()
                                    _mIncomeRealProperty = _mJsonObject("IncomeRealProperty").ToString()
                                    _mRealProperty = Nothing
                                    CedulaDataAccessLayer._mStrSql = "UPDATE CTC_Online_Application SET FirstName = '" & _mJsonObject("FirstName").ToString() & "',LastName = '" & _mJsonObject("LastName").ToString() & "',MiddleName = @MiddleName,BirthPlace = '" & _mJsonObject("BirthPlace").ToString() & "',BirthDate = '" & _mJsonObject("BirthDate").ToString() & "',Address = '" & _mJsonObject("Address").ToString() & "',Gender = '" & _mJsonObject("Gender").ToString() & "',CivilStatus = '" & _mJsonObject("CivilStatus").ToString() & "',Citizenship = '" & _mJsonObject("Citizenship").ToString() & "',[Weight] = '" & _mJsonObject("Weight").ToString() & "',Height = '" & _mJsonObject("Height").ToString() & "',TIN = '" & _mJsonObject("TIN").ToString() & "',Profession = '" & _mJsonObject("Profession").ToString() & "',BasicCommunityTax = '" & _mJsonObject("BasicTax").ToString() & "',GrossReceipts = @GrossReciept,SalariesOrGross = @Salaries,IncomeFromRealProperty = @IncomeRealProperty, Total = '" & _mJsonObject("Total").ToString() & "',Interest = '" & _mJsonObject("Interest").ToString() & "',AmountToPay = '" & _mJsonObject("AmountToPay").ToString() & "' WHERE ControlNo ='" & id & "'"
                                Case Else
                                    'Type
                                    _mCTCType = _mJsonObject("ApplicationType").ToString()
                                    _mActionType = _mJsonObject("Action").ToString()
                                    'Parameters value this can be null 
                                    _mMiddleName = Nothing
                                    _mGrossReciept = _mJsonObject("GrossReciept").ToString()
                                    _mSalaries = Nothing
                                    _mIncomeRealProperty = Nothing
                                    _mRealProperty = _mJsonObject("RealProperty").ToString()
                                    CedulaDataAccessLayer._mStrSql = "UPDATE CTC_Online_Application  SET CompanyName = '" & _mJsonObject("CompanyName").ToString() & "', Address = '" & _mJsonObject("Address").ToString() & "', KindofOrganization = '" & _mJsonObject("KindofOrganization").ToString() & "', BusinessNature = '" & _mJsonObject("BusinessNature").ToString() & "', PlaceofOrganization = '" & _mJsonObject("PlaceofOrganization").ToString() & "', DateofOrganization = '" & _mJsonObject("DateofOrganization").ToString() & "', BasicCommunityTax = '" & _mJsonObject("BasicTax").ToString() & "', RealProperty = @RealProperty, GrossReceipts = @GrossReciept, Total = '" & _mJsonObject("Total").ToString() & "', Interest = '" & _mJsonObject("Interest").ToString() & "', AmountToPay = '" & _mJsonObject("AmountToPay").ToString() & "'  WHERE ControlNo ='" & id & "'"
                            End Select
                        Case "Remarks"
                            'Remarks
                            _mCTCType = _mJsonObject("ApplicationType").ToString()
                            _mActionType = _mJsonObject("Action").ToString()
                            CedulaDataAccessLayer._mStrSql = "UPDATE CTC_Online_Application SET Remarks ='" & _mJsonObject("Remarks").ToString() & "' WHERE ControlNo ='" & id & "'"
                        Case Else
                            'Approval and Rejected
                            _mCTCType = _mJsonObject("ApplicationType").ToString()
                            _mActionType = _mJsonObject("Action").ToString()
                            CedulaDataAccessLayer._mStrSql = "UPDATE CTC_Online_Application SET [Status] ='" & _mJsonObject("Action").ToString() & "' WHERE ControlNo ='" & id & "'"
                    End Select
            End Select
                    'Call The Data Access ALayer
            If CedulaDataAccessLayer._mDataAccessLayerPutValue(_mCTCType, _mActionType, _mMiddleName, _mGrossReciept, _mSalaries, _mIncomeRealProperty, _mRealProperty) Then
                _mStatus = CedulaDataAccessLayer._mStatus
                _mData = CedulaDataAccessLayer._mData
                _mMessage = CedulaDataAccessLayer._mMessage
                _mCode = CedulaDataAccessLayer._mCode
            Else
                _mStatus = CedulaDataAccessLayer._mStatus
                _mData = CedulaDataAccessLayer._mData
                _mMessage = CedulaDataAccessLayer._mMessage
                _mCode = CedulaDataAccessLayer._mCode
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

    'Delete Model with parameters
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




End Class
