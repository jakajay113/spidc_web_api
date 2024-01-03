Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System.Security.Cryptography
Imports System.Threading.Tasks

Public Class UniversalCheckoutModel
    'SPIDC Config
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config
    'Universal Checkout Data Access Layer
    Private Shared UniversalCheckoutDataAccessLayer As New UniversalCheckoutDataAccessLayer
    'JWT Token
    Private Shared Spidc_Web_API_JWTToken As New Spidc_Web_API_JWTToken
#Region "Variable Universal Checkout Web API"
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
    'For JWT TOken
    Public Shared _mAppName As String
    Public Shared _mAccountNo As String
    Public Shared _mEmail As String
    Public Shared _mUrlOrigin As String
    Public Shared _mControlNo As String
    'Payor Information
    Public Shared _mFname As String
    Public Shared _mMname As String
    Public Shared _mLname As String
    Public Shared _mSuffix As String
    Public Shared _mAddress As String
    'Notification URL
    Public Shared _mNotificationSuccessURL As String
    'Universal Checkout Other Fee
    Public Shared _mOtherFee As String
    Public Shared _mSpidcFee As String
    'Billing Data 
    Public Shared _mtransrefNo As String
    Public Shared _massessmentNo As String
    Public Shared _mbillingDate As String
    Public Shared _msysTranDesc As String
    Public Shared _msysTranAmt As String
    Public Shared _msysTran_TotalAmt As String
    'Account Codes Data
    Public Shared _msystran_providerCode As String
    Public Shared _msystran_codeDesc As String
    Public Shared _msystran_codeamt As String
    Public Shared _msystran_MainCode As String
    Public Shared _msystran_AncestorCode As String
    Public Shared _msysTran_SubAccCode As String
    Public Shared _maccountCodeDataArray As JArray

    Public Shared _mUCPTransacionNo As String
    Public Shared _mParts As String()
    Public Shared _ucpseqrandom As New Random()

    Public Shared _mAppStatus As String
    Public Shared _mFilaData As Byte()
    Public Shared _mCTCType As String
    Public Shared _mActionType As String
    Public Shared _mUniversalCheckoutStatus As String
    Public Shared _mJWTTOKEN As String

    Public Shared _hash1String As String = "zxcvbnmljgfdsasqwertyuiop"
    Public Shared _hash2String As String = "nbvcbcvbdfgdfgdfsgsdfgsdfg"
    Public Shared _hash3String As String = "tyreterwtertertertertfsdfd"
    Public Shared startIndex As Integer = 5
    Public Shared length As Integer = 7
    Public Shared hash1 As String
    Public Shared hash2 As String
    Public Shared hash3 As String

#End Region
#Region "Property Universal Checkout Web API"
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
    Public Shared Function _mGetValues(ByVal param As String)
        Try
            'Call THE SPIDC WEB API CONFIG
            Spidc_Web_API_Config.WebApiConfig()
            'Check The  Method param is exist and set the connection type Payment Method
            Select Case Spidc_Web_API_Param_Checker_Config._mCheckParamUniversalCheckout(param)
                Case Spidc_Web_API_Config._mAppNameUniversalCheckout
                    UniversalCheckoutDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_CR
            End Select
            'Query Build
            UniversalCheckoutDataAccessLayer._mStrSql = "SELECT * FROM OnlinePaymentSetup WHERE [Status]=1 ORDER BY Code DESC"
            'Call The Data Access Layer 
            If UniversalCheckoutDataAccessLayer._mGetUniversalCheckoutPaymentMetod() Then
                _mStatus = UniversalCheckoutDataAccessLayer._mStatus
                _mData = UniversalCheckoutDataAccessLayer._mData
                _mMessage = UniversalCheckoutDataAccessLayer._mMessage
                _mCode = UniversalCheckoutDataAccessLayer._mCode
            Else
                _mStatus = UniversalCheckoutDataAccessLayer._mStatus
                _mData = UniversalCheckoutDataAccessLayer._mData
                _mMessage = UniversalCheckoutDataAccessLayer._mMessage
                _mCode = UniversalCheckoutDataAccessLayer._mCode
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
    Public Shared Function _mGetValue(ByVal param As String, ByVal id As String)
        Try
            'Call THE SPIDC WEB API CONFIG
            Spidc_Web_API_Config.WebApiConfig()
            'Check The  Method param is exist and set the connection type
            Select Case Spidc_Web_API_Param_Checker_Config._mCheckParamUniversalCheckout(param)
                Case Spidc_Web_API_Config._mAppNameUniversalCheckout
                    UniversalCheckoutDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
            End Select
            'Query Build
            UniversalCheckoutDataAccessLayer._mStrSql = "SELECT * FROM UniversalCheckout WHERE AccountNo='" & id & "'"
            'Call The Data Access Layer 
            If UniversalCheckoutDataAccessLayer._mGetUniversalCheckout() Then
                _mStatus = UniversalCheckoutDataAccessLayer._mStatus
                _mData = UniversalCheckoutDataAccessLayer._mData
                _mMessage = UniversalCheckoutDataAccessLayer._mMessage
                _mCode = UniversalCheckoutDataAccessLayer._mCode
            Else
                _mStatus = UniversalCheckoutDataAccessLayer._mStatus
                _mData = UniversalCheckoutDataAccessLayer._mData
                _mMessage = UniversalCheckoutDataAccessLayer._mMessage
                _mCode = UniversalCheckoutDataAccessLayer._mCode
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
            'Call THE SPIDC WEB API CONFIG
            Spidc_Web_API_Config.WebApiConfig()
            'CHECK IF THE PARAM IS FOR PROCEED TO PAYMENT GATEWAY OT FOR POSTING TO UNIVERSAL CHECKOUT
            Select Case param
                Case Spidc_Web_API_Config._mAppPostParamUniversalCheckOut
                    'Check The  Method param is exist and set the connection type
                    Select Case Spidc_Web_API_Param_Checker_Config._mCheckParamUniversalCheckout(param)
                        Case Spidc_Web_API_Config._mAppNameUniversalCheckout
                            UniversalCheckoutDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                    End Select
                    'Parse the json object in variable
                    _mJsonObject = JObject.Parse(value.ToString)
                    'For Generating JWT TOKEN 
                    _mEmail = _mJsonObject("universalCheckout")("payorInfo")("email").ToString()
                    _mAppName = _mJsonObject("universalCheckout")("systemInformation")("appName").ToString()
                    _mAccountNo = _mJsonObject("universalCheckout")("payorInfo")("accountNo").ToString()
                    _mUrlOrigin = _mJsonObject("universalCheckout")("systemInformation")("urlOrigin").ToString()
                    'Payor Information
                    _mFname = _mJsonObject("universalCheckout")("payorInfo")("firstName").ToString()
                    _mMname = _mJsonObject("universalCheckout")("payorInfo")("middleName").ToString()
                    _mLname = _mJsonObject("universalCheckout")("payorInfo")("lastName").ToString()
                    _mSuffix = _mJsonObject("universalCheckout")("payorInfo")("suffix").ToString()
                    _mAddress = _mJsonObject("universalCheckout")("payorInfo")("address").ToString()
                    'Universal Checkout Other Fee
                    _mOtherFee = "0.00"
                    _mSpidcFee = "0.00"
                    'Billing Data 
                    _mtransrefNo = _mJsonObject("universalCheckout")("billingData")("transrefNo").ToString()
                    _massessmentNo = _mJsonObject("universalCheckout")("billingData")("assessmentNo").ToString()
                    _mbillingDate = _mJsonObject("universalCheckout")("billingData")("billingDate").ToString()
                    _msysTranDesc = _mJsonObject("universalCheckout")("billingData")("sysTranDesc").ToString()
                    _msysTranAmt = _mJsonObject("universalCheckout")("billingData")("sysTranAmt").ToString()
                    _msysTran_TotalAmt = _mJsonObject("universalCheckout")("billingData")("sysTran_TotalAmt").ToString()
                    'Account Codes Data
                    ' Iterate through all elements in the "accountCodeData" array
                    _maccountCodeDataArray = _mJsonObject("universalCheckout")("accountCodeData")
                    For Each item As JObject In _maccountCodeDataArray
                        _msystran_providerCode = item("systran_providerCode").ToString()
                        _msystran_codeDesc = item("systrans_codedesc").ToString()
                        _msystran_codeamt = item("systems_codeamt").ToString()
                        _msystran_MainCode = item("systran_MainCode").ToString()
                        _msystran_AncestorCode = item("systran_AncestorCode").ToString()
                        _msysTran_SubAccCode = item("sysTran_SubAccCode").ToString()
                    Next


                    'Notification URL
                    _mNotificationSuccessURL = _mJsonObject("universalCheckout")("notificationUrl")("url").ToString()


                    'Default Checkout Status
                    _mUniversalCheckoutStatus = "Pending"
                    'Generate JWT TOKEN with para username or email and app name
                    _mJWTTOKEN = Spidc_Web_API_JWTToken.GenerateJwtToken(_mEmail, _mAppName, _mAccountNo, _mUrlOrigin)

                    'Generate ControlNo Or TransactionNO
                    '_mUCPTransacionNo = _FnAutoGenID("UCP_TransactionNo", "UCP")
                    _mParts = _FnAutoGenID("UCP_TransactionNo", "UCP").Split("-"c)
                    _mUCPTransacionNo = _mParts(0) & _ucpseqrandom.Next(10000, 100000)

                    'Check The Param To Build The Query Type 
                    Select Case param
                        Case Spidc_Web_API_Config._mAppPostParamUniversalCheckOut

                            Select Case _mAppName
                                Case "CEDULAAPP"
                                    'Main Table
                                    _mStrSql1 = "INSERT INTO UniversalCheckout (TransactionRef,AppName, Email, Fname, MiddleName, LastName,Suffix, AccountNo, BillingAmount, TotalAmount, BiilingDate, OtherFee, RawAmount, SpidcFee,  Token, CheckOutStatus, UrlOrigin, UrlSuccess, CheckOutDate, [Address], AssessmentNo,transDesc) " & _
                                                "VALUES('" & _mUCPTransacionNo & "','" & _mAppName & "', '" & _mEmail & "', '" & _mFname & "', '" & _mMname & "', '" & _mLname & "', @Suffix, '" & _mAccountNo & "', '" & _msysTranAmt & "', '" & _msysTran_TotalAmt & "', GETDATE(), '" & _mOtherFee & "', '" & _msysTranAmt & "', '" & _mSpidcFee & "', '" & _mJWTTOKEN & "', '" & _mUniversalCheckoutStatus & "', '" & _mUrlOrigin & "', '" & _mNotificationSuccessURL & "', GETDATE(), '" & _mAddress & "', '" & _massessmentNo & "','" & _msysTranDesc & "');"

                                Case "HSIMS"
                                    _mStrSql1 = ""

                                Case "PINNACLE"

                                    _mStrSql1 = "INSERT INTO UniversalCheckout (TransactionRef,TrefNo,AppName, Email, Fname, MiddleName, LastName,Suffix, AccountNo, BillingAmount, TotalAmount, BiilingDate, OtherFee, RawAmount, SpidcFee,  Token, CheckOutStatus, UrlOrigin, UrlSuccess, CheckOutDate, [Address], AssessmentNo,transDesc) " & _
                                                 "VALUES('" & _mUCPTransacionNo & "','" & _mtransrefNo & "','" & _mAppName & "', '" & _mEmail & "', '" & _mFname & "', '" & _mMname & "', '" & _mLname & "', @Suffix, '" & _mAccountNo & "', '" & _msysTranAmt & "', '" & _msysTran_TotalAmt & "','" & _mbillingDate & "', '" & _mOtherFee & "', '" & _msysTranAmt & "', '" & _mSpidcFee & "', '" & _mJWTTOKEN & "', '" & _mUniversalCheckoutStatus & "', '" & _mUrlOrigin & "', '" & _mNotificationSuccessURL & "', GETDATE(), '" & _mAddress & "', '" & _massessmentNo & "','" & _msysTranDesc & "');"

                                Case "QPAX"
                                    _mStrSql1 = ""


                            End Select
                            UniversalCheckoutDataAccessLayer._mStrSql = _mStrSql1
                    End Select

                    'Make A Hash Parameters
                    hash1 = ComputeSubstringHash(_hash1String, startIndex, length)
                    hash2 = ComputeSubstringHash(_hash2String, startIndex, length)
                    hash3 = ComputeSubstringHash(_hash3String, startIndex, length)

                    ''Check iF AccountNo Already Exist
                    If UniversalCheckoutDataAccessLayer._mCheckAccountNoAlreadyExist(_mAccountNo) Then
                        'Check if exist delete the existing
                        If UniversalCheckoutDataAccessLayer._mCheckAccountNoAlreadyExistAndDelete(_mAccountNo) Then
                            'Call The Data Access Layer And Insert Again
                            If UniversalCheckoutDataAccessLayer._mPostUniversalCheckout(_mAppName, value, _mJWTTOKEN, hash1, hash2, hash3, _mUCPTransacionNo) Then
                                _mStatus = UniversalCheckoutDataAccessLayer._mStatus
                                _mData = UniversalCheckoutDataAccessLayer._mData
                                _mMessage = UniversalCheckoutDataAccessLayer._mMessage
                                _mCode = UniversalCheckoutDataAccessLayer._mCode
                            Else
                                _mStatus = UniversalCheckoutDataAccessLayer._mStatus
                                _mData = UniversalCheckoutDataAccessLayer._mData
                                _mMessage = UniversalCheckoutDataAccessLayer._mMessage
                                _mCode = UniversalCheckoutDataAccessLayer._mCode
                            End If
                        End If
                    Else
                        'Call The Data Access Layer And Insert Fresh
                        If UniversalCheckoutDataAccessLayer._mPostUniversalCheckout(_mAppName, value, _mJWTTOKEN, hash1, hash2, hash3, _mUCPTransacionNo) Then
                            _mStatus = UniversalCheckoutDataAccessLayer._mStatus
                            _mData = UniversalCheckoutDataAccessLayer._mData
                            _mMessage = UniversalCheckoutDataAccessLayer._mMessage
                            _mCode = UniversalCheckoutDataAccessLayer._mCode
                        Else
                            _mStatus = UniversalCheckoutDataAccessLayer._mStatus
                            _mData = UniversalCheckoutDataAccessLayer._mData
                            _mMessage = UniversalCheckoutDataAccessLayer._mMessage
                            _mCode = UniversalCheckoutDataAccessLayer._mCode
                        End If
                    End If
                    'Proceed To Payment Gateway
                Case Spidc_Web_API_Config._mAppPostParamUniversalCheckOutProceed
                    'Call The Data Access Layer And Insert Fresh
                    If UniversalCheckoutDataAccessLayer._mPostUniversalCheckoutProceedToPaymentGateway(value) Then
                        _mStatus = UniversalCheckoutDataAccessLayer._mStatus
                        _mData = UniversalCheckoutDataAccessLayer._mData
                        _mMessage = UniversalCheckoutDataAccessLayer._mMessage
                        _mCode = UniversalCheckoutDataAccessLayer._mCode
                    Else
                        _mStatus = UniversalCheckoutDataAccessLayer._mStatus
                        _mData = UniversalCheckoutDataAccessLayer._mData
                        _mMessage = UniversalCheckoutDataAccessLayer._mMessage
                        _mCode = UniversalCheckoutDataAccessLayer._mCode
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
    End Function

    '------------------------------------Generate Transaction No ---------------------------------------------------
    Public Shared Function _FnAutoGenID(ByVal _nModuleID As String, Optional _nRefCode As String = Nothing) As String
        _FnAutoGenID = Nothing
        Try
            Dim _nClass As New cDalAutoGenID
            _nClass._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
            Dim _nResult = _nClass._FnAutoGenID(_nModuleID, _nRefCode)
            Return _nResult
        Catch ex As Exception
        End Try
    End Function
    '------------------------------------End Transaction Control No ---------------------------------------------------
    '------------------------------------Generate Transaction No Ramdom ---------------------------------------------------

    '------------------------------------End TGenerate Transaction No Ramdon ---------------------------------------------------



    'Hash Function For URL
    Public Shared Function ComputeSubstringHash(inputString As String, startIndex As Integer, length As Integer) As String
        ' Convert the input string to bytes
        Dim inputBytes As Byte() = Encoding.UTF8.GetBytes(inputString)

        ' Create a new SHA-256 hash provider
        Using sha256 As New SHA256Managed()
            ' Compute the hash of the substring
            Dim substringBytes As Byte() = New Byte(length - 1) {}
            Array.Copy(inputBytes, startIndex, substringBytes, 0, length)
            Dim hashBytes As Byte() = sha256.ComputeHash(substringBytes)

            ' Convert the hash bytes to a hexadecimal string
            Dim hashStringBuilder As New StringBuilder()
            For Each hashByte As Byte In hashBytes
                hashStringBuilder.Append(hashByte.ToString("x2"))
            Next
            Return hashStringBuilder.ToString()
        End Using
    End Function


    'Delete Model 
    Public Shared Function _mDeleteValues(ByVal param As String, ByVal id As String)
        Try
            'Call THE SPIDC WEB API CONFIG
            Spidc_Web_API_Config.WebApiConfig()
            'Check The  Method param is exist and set the connection type
            Select Case Spidc_Web_API_Param_Checker_Config._mCheckParamUniversalCheckout(param)
                Case Spidc_Web_API_Config._mAppNameUniversalCheckout
                    UniversalCheckoutDataAccessLayer._pSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
            End Select
            'Query Build But The Query for delete universal checkout is in data access layer 
            'Call The Data Access Layer 
            If UniversalCheckoutDataAccessLayer._mCheckAccountNoAlreadyExistAndDelete(id) Then
                _mStatus = UniversalCheckoutDataAccessLayer._mStatus
                _mData = UniversalCheckoutDataAccessLayer._mData
                _mMessage = UniversalCheckoutDataAccessLayer._mMessage
                _mCode = UniversalCheckoutDataAccessLayer._mCode
            Else
                _mStatus = UniversalCheckoutDataAccessLayer._mStatus
                _mData = UniversalCheckoutDataAccessLayer._mData
                _mMessage = UniversalCheckoutDataAccessLayer._mMessage
                _mCode = UniversalCheckoutDataAccessLayer._mCode
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




End Class
