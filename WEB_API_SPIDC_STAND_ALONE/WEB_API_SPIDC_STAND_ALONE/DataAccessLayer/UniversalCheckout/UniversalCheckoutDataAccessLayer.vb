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
    Public Shared _msystran_AncestorCode As String
    Public Shared _msysTran_SubAccCode As String

    Private Shared _mAppStatus As String
    Private Shared _mFilaData As Byte()
    Private Shared _mCTCType As String
    Private Shared _mActionType As String
    Private Shared _mDataResponse As String

    Private Shared _mUniversalCheckoutURL As String
    Private Shared _mUniversalCheckouTFinalURL As String

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


    '-----------------------------------------------------------------------------------POST with Parameters Data Access Layer-----------------------------------------------------------------------------------------------------
    Public Shared Function _mPostUniversalCheckout(ByVal value As Object, ByVal jwttoken As String, ByVal hash1 As String, ByVal hash2 As String, ByVal hash3 As String, ByVal transactionReferenceNo As String) As Boolean
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
            'Account Codes Data
            _msystran_providerCode = _mJsonObject("universalCheckout")("accountCodeData")("systran_providerCode").ToString()
            _msystran_MainCode = _mJsonObject("universalCheckout")("accountCodeData")("systran_MainCode").ToString()
            _msystran_AncestorCode = _mJsonObject("universalCheckout")("accountCodeData")("systran_AncestorCode").ToString()
            _msysTran_SubAccCode = _mJsonObject("universalCheckout")("accountCodeData")("sysTran_SubAccCode").ToString()

            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            With _mSqlCmd.Parameters
                .AddWithValue("@Suffix", IIf(String.IsNullOrEmpty(_mSuffix), DBNull.Value, _mSuffix))
            End With
            _mSqlCmd.ExecuteNonQuery()

            'Build a url 
            _mUniversalCheckoutURL = Spidc_Web_API_Config._mUniversalCheckoutURL
            _mUniversalCheckouTFinalURL = _mUniversalCheckoutURL & "?a=" & hash1 & "&b=" & hash2 & "&c=" & hash3 & "&d=" & jwttoken

            _mJson = "{""transactonReferenceNo"": """ & transactionReferenceNo & """,""checkoutURL"": """ & _mUniversalCheckouTFinalURL & """}"
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
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New Script.Serialization.JavaScriptSerializer()
            Dim _sqlDateNow As DateTime
            Dim _sqlDateNow10 As DateTime

            Dim objReq As New GCashModel.Gcash_OrderCreate
            objReq.request = New GCashModel.Request()
            objReq.request.head = New GCashModel.Head()
            objReq.request.body = New GCashModel.Body()
            objReq.request.body.order = New GCashModel.Order()
            objReq.request.body.order.buyer = New GCashModel.Buyer()
            objReq.request.body.order.seller = New GCashModel.Seller()
            objReq.request.body.order.orderAmount = New GCashModel.OrderAmount()
            objReq.request.body.envInfo = New GCashModel.EnvInfo()
            'Head
            objReq.request.head.version = "2.0"
            objReq.request.head._function = "gcash.acquiring.order.create"
            objReq.request.head.clientId = "ClientId"
            objReq.request.head.clientSecret = "ClientSecret"
            objReq.request.head.reqTime = _sqlDateNow.ToString("yyyy-MM-dd'T'HH:mm:ssK")
            objReq.request.head.reqMsgId = "ReqMsgID"
            'Body>Order>Buyer
            objReq.request.body.order.buyer.userId = ""
            objReq.request.body.order.buyer.externalUserId = "1001"
            objReq.request.body.order.buyer.externalUserType = "1001"
            'Body>Order>Seller
            objReq.request.body.order.seller.userId = ""
            objReq.request.body.order.seller.externalUserId = "TESTSELLER"
            objReq.request.body.order.seller.externalUserType = "TESTSELLER"
            'Body>Order>orderTitle
            objReq.request.body.order.orderTitle = "PaymentDesc" & " - " & "ACCTNO"
            'Body>Order>orderAmount
            objReq.request.body.order.orderAmount.currency = "PHP" 'Amount
            objReq.request.body.order.orderAmount.value = (CStr("100.00" * 100)).Replace(".00", "")
            'Body>Order>
            objReq.request.body.order.merchantTransId = "SPIDCRefNo"
            objReq.request.body.order.createdTime = _sqlDateNow.ToString("yyyy-MM-dd'T'HH:mm:ssK")
            objReq.request.body.order.expirytime = _sqlDateNow10.ToString("yyyy-MM-dd'T'HH:mm:ssK")
            'Body>
            objReq.request.body.merchantId = "MerchantID"
            objReq.request.body.subMerchantId = ""
            objReq.request.body.subMerchantName = "PaymentDesc"
            objReq.request.body.productCode = "ProductCode"
            'Body>envInfo           
            objReq.request.body.envInfo.orderTerminalType = "WEB"
            objReq.request.body.envInfo.terminalType = "WEB"
            objReq.signature = "signature string"

            Dim client = New RestClient("https://api.saas.mynt.xyz/")
            client.Timeout = -1
            Dim request = New RestRequest("gcash/acquiring/order/create.htm", Method.POST)
            Dim body = serializer.Serialize(objReq)

            'Dim strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery
            'Dim strUrl As String

            '' Dim callbackurl As String = strUrl & "PaymentConfirmation.aspx"
            ''callbackurl = "http://ptsv2.com/t/zewy6-1646711060/post"

            'Dim API_callback As String
            'If HttpContext.Current.Request.Url.AbsoluteUri.ToUpper.Contains("TEST") Then
            '    strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/TEST/")
            'ElseIf HttpContext.Current.Request.Url.AbsoluteUri.ToUpper.Contains("ONLINE.SPIDC.COM.PH/CAINTA") Then
            '    strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/Cainta/")
            'ElseIf HttpContext.Current.Request.Url.AbsoluteUri.ToUpper.Contains("ONLINE.SPIDC.COM.PH/CALOOCAN") Then
            '    strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/Caloocan/")
            'Else
            '    strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/")
            'End If
            'API_callback = strUrl & "API_Payment/api/GCASH_Notify"
            ' ''  API_callback = "https://ptsv2.com/t/mxn1t-1650360758/post"
            'Dim PostBack_callback As String = strUrl & "PaymentConfirmation.aspx?referenceCode=" & "SPIDCRefNo" & "&SelectedBank=GCASH"

            Dim notifUrls As String = Nothing
            notifUrls += "[{""type"":""PAY_RETURN"",""url"":""" & "https://online.spidc.com.ph/spidc_web_api/" & "&S=S""},"
            notifUrls += "{""type"":""CANCEL_RETURN"",""url"":""" & "https://online.spidc.com.ph/spidc_web_api/" & "&S=F""},"
            notifUrls += "{""type"":""NOTIFICATION"",""url"":""" & "https://online.spidc.com.ph/spidc_web_api/" & """}]}}"
            body = body.Replace("null}}", notifUrls)
            body = body.Replace("_function", "function")

            Dim StringToSign As String = Nothing
            StringToSign = body.Remove(0, 11) ' Remove "request":
            StringToSign = StringToSign.Replace(",""signature"":""signature string""}", "")
            Dim signedString = GCashModel.Do_Sign(StringToSign)
            body = body.Replace("signature string", signedString)
            request.AddParameter("application/json", body, ParameterType.RequestBody)

            Dim response1 As IRestResponse = client.Execute(request)
            If response1.StatusCode = HttpStatusCode.OK Then
                ' Optionally, you can read the response content if needed
                Dim jsonResponse As String = response1.Content
                MsgBox(jsonResponse)
            Else
                Dim jsonResponse As String = response1.Content
                MsgBox(jsonResponse)
            End If



            '--Insert REQUEST to table GCASH_TRANSACTIONS 
            '_function = objReq.request.head._function
            '_transactionId = ""
            '_merchantTransId = objReq.request.body.order.merchantTransId
            '_acquirementStatus = ""
            '_signature = signedString


            '_nClass._pSqlConnection = cGlobalConnections._pSqlCxn_OAIMS
            '_nClass.GCASH_InsertLog(_function, _transactionId, _merchantTransId, body, "Request from SPIDC", _acquirementStatus, _signature)

            'WriteLogs(_function, "REQUEST", body)
            'WriteLogs(_function, "RESPONSE", response1.Content)


            'Dim Response_OriginalString
            'Dim index As Integer = response1.Content.LastIndexOf(","c)
            'Response_OriginalString = response1.Content.Remove(index)
            'Response_OriginalString = Response_OriginalString.Remove(0, 12) ' Remove "response":
            ''   Response.Write(Response_OriginalString)
            'Dim res As Object = New JavaScriptSerializer().Deserialize(Of Object)(response1.Content)
            ''  Response.Write(res("signature"))
            'body = response1.Content


            MsgBox(response1.Content)


        Catch ex As Exception
            _mStatus = "error"
            _mData = Nothing
            _mMessage = ex.Message
            _mCode = "500"
            Return False
        End Try
    End Function





    '-----------------------------------------------------------------------------------GET CHECK with Parameters Data Access Layer-----------------------------------------------------------------------------------------------------
    Public Shared Function _mCheckAccountNoAlreadyExist(ByVal key As String) As Boolean
        Try
            _mStrSql1 = "SELECT * FROM UniversalCheckout WHERE AccountNo ='" & key & "'"
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
    Public Shared Function _mCheckAccountNoAlreadyExistAndDelete(ByVal key As String) As Boolean
        Try
            _mStrSql1 = "DELETE  FROM UniversalCheckout WHERE AccountNo='" & key & "'"
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
