Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.Threading.Tasks
Imports System.Threading
Imports RestSharp
Imports System.Web.Script.Serialization
Imports System.Net

Public Class UniversalCheckoutDataAccessLayer

    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config

#Region "Variable Data Access Layer Universal Checkout Web API"
    Public Shared _mSqlCon As New SqlConnection
    Public Shared _mSqlCmd As SqlCommand
    Public Shared _mDataTable As New DataTable
    Public Shared _mDataAdapter As New SqlDataAdapter
    Public Shared _mDataAdapter1 As New SqlDataAdapter
    Public Shared _mDataset As New DataSet
    Public Shared _mDataset1 As New DataSet
    Public Shared _mStrSql As String
    Public Shared _mStrSql1 As String
    Public Shared _mStrSql2 As String
    Public Shared _mStrSql3 As String
    Public Shared Property _mStatus As String
    Public Shared Property _mData As Object
    Public Shared Property _mMessage As String
    Public Shared Property _mCode As Object

    Private Shared _mJsonObject As JObject
    Private Shared _mJson As String
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
    Public Shared _msystran_MainCode As String
    Public Shared _msystran_codeDesc As String
    Public Shared _msystran_codeamt As String
    Public Shared _msystran_AncestorCode As String
    Public Shared _msysTran_SubAccCode As String
    Public Shared _maccountCodeDataArray As JArray
    Public Shared _mDataArray As JArray

    Public Shared _mBillingAmount As String
    Public Shared _mTotalAmmount As String
    Public Shared _mselectedGateway As String

    Private Shared _mAppStatus As String
    Private Shared _mFilaData As Byte()
    Private Shared _mCTCType As String
    Private Shared _mActionType As String
    Private Shared _mDataResponse As String

    Private Shared _mUniversalCheckoutURL As String
    Private Shared _mUniversalCheckouTFinalURL As String

    Private Shared _mSuccessPaymentConfirmationURL As String


#End Region
#Region "Property Data Access Layer Universal Checkout Web  Web API"
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


    '-----------------------------------------------------------------------------------GET  Data Access Layer-----------------------------------------------------------------------------------------------------
    Public Shared Function _mGetUniversalCheckoutPaymentMetod() As Boolean
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
    End Function


    '-----------------------------------------------------------------------------------GET with Parameters Data Access Layer-----------------------------------------------------------------------------------------------------
    Public Shared Function _mGetUniversalCheckout() As Boolean
        Try
            'Main UniversalCheckout Table
            _mDataset = New DataSet
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            _mDataAdapter = New SqlDataAdapter(_mSqlCmd)
            _mDataAdapter.Fill(_mDataset)
            If _mDataset.Tables(0).Rows.Count > 0 Then
                'Extention UniversalCheckout Table
                _mDataset1 = New DataSet
                _mStrSql1 = "SELECT * FROM UniversalCheckout_Extn"
                _mSqlCmd = New SqlCommand(_mStrSql1, _mSqlCon)
                _mDataAdapter1 = New SqlDataAdapter(_mSqlCmd)
                _mDataAdapter1.Fill(_mDataset1)
                ' Create a List to hold the tables as objects
                Dim tableList As New List(Of Object)
                If _mDataset1.Tables(0).Rows.Count > 0 Then
                    ' Convert tables to JSON strings
                    Dim mainTable = JsonConvert.SerializeObject(_mDataset.Tables(0))
                    Dim extentionTable = JsonConvert.SerializeObject(_mDataset1.Tables(0))
                    ' Build the final JSON response
                    _mJson = "{""dataInformation"": " & mainTable & ",""dataCode"": " & extentionTable & "}"
                    ' If you want to parse the JSON into an object, you can use a library like Newtonsoft.Json (Json.NET):
                    Dim _mJsonResponse = JsonConvert.DeserializeObject(Of JObject)(_mJson)
                    _mStatus = "success"
                    _mData = _mJsonResponse
                    _mMessage = "Data retrieved successfully"
                    _mCode = "200"
                End If
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
    End Function


    '-----------------------------------------------------------------------------------POST with Parameters Data Access Layer-----------------------------------------------------------------------------------------------------
    Public Shared Function _mPostUniversalCheckout(ByVal appName As String, ByVal value As Object, ByVal jwttoken As String, ByVal hash1 As String, ByVal hash2 As String, ByVal hash3 As String, ByVal transactionReferenceNo As String) As Boolean
        Try
            'Call THE SPIDC WEB API CONFIG
            Spidc_Web_API_Config.WebApiConfig()

            'Parse the json object in variable
            _mJsonObject = JObject.Parse(value.ToString)
            'For Generating JWT TOKEN 
            _mEmail = _mJsonObject("universalCheckout")("payorInfo")("email").ToString()
            _mAppName = _mJsonObject("universalCheckout")("systemInformation")("appName").ToString()
            _mAccountNo = _mJsonObject("universalCheckout")("payorInfo")("accountNo").ToString()
            _mUrlOrigin = _mJsonObject("universalCheckout")("systemInformation")("urlOrigin").ToString()
            'Payor Information
            _mFname = _mJsonObject("universalCheckout")("payorInfo")("firstName").ToString()
            _mMname = _mJsonObject("universalCheckout")("payorInfo")("lastName").ToString()
            _mLname = _mJsonObject("universalCheckout")("payorInfo")("middleName").ToString()
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
            'Execute Main UniversalCheckout Table
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            With _mSqlCmd.Parameters
                .AddWithValue("@Suffix", IIf(String.IsNullOrEmpty(_mSuffix), DBNull.Value, _mSuffix))
            End With
            _mSqlCmd.ExecuteNonQuery()
            'Execute Extention UniversalCheckout Table
            'Account Codes Data
            _maccountCodeDataArray = _mJsonObject("universalCheckout")("accountCodeData")
            For Each item As JObject In _maccountCodeDataArray
                _mStrSql = "INSERT INTO UniversalCheckout_Extn (accountNo, SysTran_ProviderCode, systrans_codeDesc, systems_codeAmt, SysTran_MainCode, SysTran_AncestorCode, SysTran_SubAccCode,assessmentNo)" & _
                           "VALUES('" & _mAccountNo & "','" & item("systran_providerCode").ToString() & "' ,'" & item("systrans_codedesc").ToString() & "', '" & item("systems_codeamt").ToString() & "', '" & item("systran_MainCode").ToString() & "','" & item("systran_AncestorCode").ToString() & "','" & item("sysTran_SubAccCode").ToString() & "','" & _massessmentNo & "')"
                _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
                _mSqlCmd.ExecuteNonQuery()
            Next
            'Build a url 
            _mUniversalCheckoutURL = Spidc_Web_API_Config._mUniversalCheckoutURL
            _mUniversalCheckouTFinalURL = _mUniversalCheckoutURL & "?scv1=" & hash1 & "&vc1=" & hash2 & "&gg=" & hash3 & "&xyz=" & jwttoken
            _mJson = "{""transactionReferenceNo"": """ & transactionReferenceNo & """,""checkoutURL"": """ & _mUniversalCheckouTFinalURL & """}"
            ' If you want to parse the JSON into an object, you can use a library like Newtonsoft.Json (Json.NET):
            Dim _mJsonResponse = JsonConvert.DeserializeObject(Of JObject)(_mJson)

            _mStatus = "success"
            _mData = _mJsonResponse
            _mMessage = "Checkout successfully posted."
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


    '-----------------------------------------------------------------------------------POST Proceed To Payment Gateway-----------------------------------------------------------------------------------------------------
    Public Shared Function _mPostUniversalCheckoutProceedToPaymentGateway(ByVal value As Object) As Boolean
        Try
            'Parse the json object in variable
            _mJsonObject = JObject.Parse(value.ToString)
            'Gateway Selectec
            _mselectedGateway = _mJsonObject("paymentGateway").ToString()

            'Get The APP USE IT FOR OTC SELECTED GATEWAY
            _mDataArray = _mJsonObject("dataInformation")
            For Each item As JObject In _mDataArray
                _mAppName = item("AppName").ToString()
                _mControlNo = item("AccountNo").ToString()
                _mEmail = item("Email").ToString()
                _mBillingAmount = item("BillingAmount").ToString()
                _mTotalAmmount = item("TotalAmount").ToString()
                _mbillingDate = item("BiilingDate").ToString()
            Next
            Select Case _mselectedGateway
                Case "OTC" 'Checkout If OTC IS FOR CEDULA OR FOR OTHER ONLY CEDULA HAVE OTC FOR NOW
                    Select Case _mAppName
                        Case "CEDULAAPP" 'SET THE OTC TO 1 FOR CEDULA OVER THE COUNTER
                            _mSqlCmd = New SqlCommand("UPDATE CTC_Online_Application SET OTC = 1 WHERE ControlNo='" & _mControlNo & "'", Spidc_Web_API_Global_Connection._pSqlCxn_TIMS)
                            _mSqlCmd.ExecuteNonQuery()
                            'Builf a respone json format
                            'build Payment Confirmation URL THIS FOR OTC 
                            _mSuccessPaymentConfirmationURL = "paymentconfirmation.aspx"
                            _mJson = "{""transactionType"": """ & _mselectedGateway & """,""controlNo"": """ & _mControlNo & """,""email"": """ & _mEmail & """,""billingAmount"": """ & _mBillingAmount & """,""totalAmount"": """ & _mTotalAmmount & """,""billingDate"": """ & _mbillingDate & """,""successURL"": """ & _mSuccessPaymentConfirmationURL & """}"
                            ' If you want to parse the JSON into an object, you can use a library like Newtonsoft.Json (Json.NET):
                            Dim _mJsonResponse = JsonConvert.DeserializeObject(Of JObject)(_mJson)
                            _mStatus = "success"
                            _mData = _mJsonResponse
                            _mMessage = "Over the counter payment"
                            _mCode = "200"
                            Return True
                            _mSqlCmd.Dispose()
                            _mSqlCon.Close()
                        Case Else 'Other APP
                            _mStatus = "error"
                            _mData = Nothing
                            _mMessage = "Over the counter is not available!"
                            _mCode = "500"
                            Return True
                    End Select

                Case Else

                    ' Create a new JObject to include the data property
                    Dim jsonData As New JObject()
                    jsonData.Add("payload", _mJsonObject)
                    jsonData.Add("urlProcess", "processing.aspx")
                    ' If you need to convert it to a JSON string
                    Dim jsonString As String = jsonData.ToString()
                    ' If you need to convert it to a JObject (optional, as jsonData is already a JObject)
                    Dim _mJsonResponse As JObject = JsonConvert.DeserializeObject(Of JObject)(jsonString)
                    _mStatus = "success"
                    _mData = _mJsonResponse
                    _mMessage = "Checkout successfully posted."
                    _mCode = "200"
                    Return True
            End Select

        Catch ex As Exception
            _mStatus = "error"
            _mData = Nothing
            _mMessage = ex.Message
            _mCode = "500"
            Return False
        End Try
    End Function




    '-----------------------------------------------------------------------------------GET CHECK with Parameters Data Access Layer-----------------------------------------------------------------------------------------------------
    Public Shared Function _mCheckAssessmentNoAlreadyExist(ByVal key As String) As Boolean
        Try
            _mStrSql1 = "SELECT * FROM UniversalCheckout WHERE AssessmentNo ='" & key & "'"
            _mDataset = New DataSet
            _mSqlCmd = New SqlCommand(_mStrSql1, _mSqlCon)
            _mDataAdapter = New SqlDataAdapter(_mSqlCmd)
            _mDataAdapter.Fill(_mDataset)
            If _mDataset.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        _mSqlCmd.Dispose()
        _mSqlCon.Close()
    End Function
    '-----------------------------------------------------------------------------------DELETE  with Parameters Data Access Layer-----------------------------------------------------------------------------------------------------
    Public Shared Function _mCheckAssessmentNoAlreadyExistAndDelete(ByVal key As String) As Boolean
        Try
            _mStrSql1 = "DELETE  FROM UniversalCheckout WHERE AssessmentNo='" & key & "'"
            _mSqlCmd = New SqlCommand(_mStrSql1, _mSqlCon)
            _mSqlCmd.ExecuteNonQuery()
            _mStatus = "success"
            _mData = Nothing
            _mMessage = "Checkout successfully deleted."
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

End Class
