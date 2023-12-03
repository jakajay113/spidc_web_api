Imports Newtonsoft.Json.Linq
Imports System.Net
Imports RestSharp
Imports System.IdentityModel.Tokens.Jwt
Imports System.Web.Script.Serialization

Public Class processing
    Inherits System.Web.UI.Page
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config

    Private Shared _mJsonObject As JObject
    Private Shared _mJson As JArray
    Private Shared _mPayload As String

    Private Shared _mEmail As String
    Private Shared _mFname As String
    Private Shared _mMname As String
    Private Shared _mLname As String
    Private Shared _mSuffix As String
    Private Shared _mAccountNo As String
    Private Shared _mBillingAmount As String
    Private Shared _mTotalAmount As String
    Private Shared _mPaymentDescription As String

    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New Script.Serialization.JavaScriptSerializer()
    Dim _sqlDateNow As DateTime
    Dim _sqlDateNow10 As DateTime

    Private Shared _msecqLabel As String
    Private Shared _ucpseqrandom As New Random()
    Private Shared _msgID As Integer = _ucpseqrandom.Next(10000, 100000)

    Private Shared url As Uri
    Private Shared scheme As String
    Private Shared host As String
    Private Shared port As String
    Private Shared path As String
    Private Shared replacePath As String
    Private Shared buildURL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Call The Web API Config
        Spidc_Web_API_Config.WebApiConfig()
        _msecqLabel = Spidc_Web_API_Config._mAppSequenceLabel

        If Not IsPostBack Then
            'Do Nothing
        Else
            Dim action = Request("__EVENTTARGET")
            Dim val = Request("__EVENTARGUMENT")

            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)

            If action = "Processing" Then
                'Payload
                _mPayload = _payloadToProcess.Value
                'Processing 
                _mProcessingToPaymentGateway(val, _mPayload)
            Else
                'Do Nothing
            End If



        End If

    End Sub

    'Processing 
    Private Sub _mProcessingToPaymentGateway(ByRef gatewaySelected As String, ByRef payload As String)

        Select Case gatewaySelected
            Case "GCASH"
                _mGCASH(payload)
            Case "PAYMAYA"
                _mPAYMAYA(payload)
            Case "LB1"
                _mLB1(payload)
            Case "LB2"
                _mLB2(payload)
        End Select

    End Sub

    '--------------------------------------------------------------------------GCASH PAYMENT METHOD ---------------------------------------------------------------------------------
    Private Sub _mGCASH(ByRef payload As String, Optional err As String = Nothing)

        'Parse the json object in variable
        _mJsonObject = JObject.Parse(payload.ToString)

        Dim _function As String
        Dim _transactionId As String
        Dim _merchantTransId As String
        Dim _acquirementStatus As String
        Dim _signature As String
        Dim Amount As Double = _mJsonObject.SelectToken("payload.dataInformation[0].BillingAmount").ToString()
        Dim ACCTNO As String = _mJsonObject.SelectToken("payload.dataInformation[0].AccountNo").ToString()
        Dim PaymentDesc As String = _mJsonObject.SelectToken("payload.dataInformation[0].transDesc").ToString() & " " & "Payment"
        Dim _nClass As New cDalPayment
        _nClass._pSqlConnection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
        _nClass.GetsqlDateNow(_sqlDateNow, _sqlDateNow10)
        'Call Gcash Model
        Dim objReq As New GCashModel.Gcash_OrderCreate
        'Call Gcash Config
        GCashModel.GCashConfig()

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
        objReq.request.head.clientId = GCashModel.ClientId
        objReq.request.head.clientSecret = GCashModel.ClientSecret
        objReq.request.head.reqTime = _sqlDateNow.ToString("yyyy-MM-dd'T'HH:mm:ssK")
        objReq.request.head.reqMsgId = _msecqLabel & _msgID.ToString().PadLeft(20, "0"c)
        'Body>Order>Buyer
        objReq.request.body.order.buyer.userId = ""
        objReq.request.body.order.buyer.externalUserId = "1001"
        objReq.request.body.order.buyer.externalUserType = "1001"
        'Body>Order>Seller
        objReq.request.body.order.seller.userId = ""
        objReq.request.body.order.seller.externalUserId = "TESTSELLER"
        objReq.request.body.order.seller.externalUserType = "TESTSELLER"
        'Body>Order>orderTitle
        objReq.request.body.order.orderTitle = PaymentDesc & " - " & ACCTNO
        'Body>Order>orderAmount
        objReq.request.body.order.orderAmount.currency = "PHP"
        objReq.request.body.order.orderAmount.value = (CStr(Amount * 100)).Replace(".00", "")
        'Body>Order> _msecqLabel & _msgID.ToString().PadLeft(10, "0"c)
        objReq.request.body.order.merchantTransId = _msecqLabel & _msgID.ToString().PadLeft(10, "0"c)
        objReq.request.body.order.createdTime = _sqlDateNow.ToString("yyyy-MM-dd'T'HH:mm:ssK")
        objReq.request.body.order.expirytime = _sqlDateNow10.ToString("yyyy-MM-dd'T'HH:mm:ssK")
        'Body>
        objReq.request.body.merchantId = GCashModel.MerchantID
        objReq.request.body.subMerchantId = ""
        objReq.request.body.subMerchantName = GCashModel.MerchantName
        objReq.request.body.productCode = GCashModel.ProductCode
        'Body>envInfo           
        objReq.request.body.envInfo.orderTerminalType = "WEB"
        objReq.request.body.envInfo.terminalType = "WEB"
        objReq.signature = "signature string"
        Dim client = New RestClient(GCashModel.GcashDomain)
        client.Timeout = -1
        Dim request = New RestRequest(GCashModel.GcashFunctionOrderCreate, Method.POST)
        Dim body = serializer.Serialize(objReq)
        'Dim strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery
        'Dim strUrl As String
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
        'Dim PostBack_callback As String = strUrl & "PaymentConfirmation.aspx?referenceCode=" & SPIDCRefNo & "&SelectedBank=GCASH"


        url = HttpContext.Current.Request.Url
        scheme = url.Scheme ' e.g., "https"
        host = url.Host ' e.g., "www.example.com"
        port = url.Port ' e.g., 8080
        path = url.AbsolutePath ' e.g., "/path/to/resource"
        If path = "/UniversalCheckout/processing.aspx" Then
            replacePath = path.Replace("processing.aspx", "")
        Else
            replacePath = path.Replace("processing.aspx", "")
        End If
        If port Then
            buildURL = scheme & "://" & host & ":" & port & replacePath
        Else
            buildURL = scheme & "://" & host & replacePath
        End If





        Dim notifUrls As String = Nothing
        notifUrls += "[{""type"":""PAY_RETURN"",""url"":""" & buildURL & "&S=S""},"
        notifUrls += "{""type"":""CANCEL_RETURN"",""url"":""" & buildURL & "&S=F""},"
        notifUrls += "{""type"":""NOTIFICATION"",""url"":""" & buildURL & """}]}}"
        body = body.Replace("null}}", notifUrls)
        body = body.Replace("_function", "function")
        Dim StringToSign As String = Nothing
        StringToSign = body.Remove(0, 11) ' Remove "request":
        StringToSign = StringToSign.Replace(",""signature"":""signature string""}", "")
        Dim signedString = GCashModel.Do_Sign(StringToSign)
        body = body.Replace("signature string", signedString)
        request.AddParameter("application/json", body, ParameterType.RequestBody)
        Dim response1 As IRestResponse = client.Execute(request)

        '--Insert REQUEST to table GCASH_TRANSACTIONS 
        _function = objReq.request.head._function
        _transactionId = ""
        _merchantTransId = objReq.request.body.order.merchantTransId
        _acquirementStatus = ""
        _signature = signedString
        _nClass._pSqlConnection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
        _nClass.GCASH_InsertLog(_function, _transactionId, _merchantTransId, body, "Request from SPIDC", _acquirementStatus, _signature)
        '--End Insert REQUEST to table GCASH_TRANSACTIONS 

        'I SKIP THE RESPONSE VERIFY SIGNATURE IT DOESNT MATTER JUST Deserialize THE RESPONSE
        'SAVE RESPONSE FROM GCASH
        Dim Response_JSON As String = response1.Content
        Dim _gcashResponse As Object = New JavaScriptSerializer().Deserialize(Of Object)(response1.Content)
        _function = _gcashResponse("response")("head")("function")
        _merchantTransId = _gcashResponse("response")("body")("merchantTransId")
        Dim _acquirementId As String = _gcashResponse("response")("body")("acquirementId")
        _signature = _gcashResponse("signature")

        _nClass._pSqlConnection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
        _nClass.GCASH_InsertLog(_function, "_transactionId", _merchantTransId, Response_JSON, "Response from GCASH", _acquirementId, _signature)
        'End SAVE RESPONSE FROM GCASH

        'Redirect To Checkout Of Payment Gateway
        Response.Redirect(_gcashResponse("response")("body")("checkoutUrl"))


    End Sub
    '--------------------------------------------------------------------------END GCASH PAYMENT METHOD ---------------------------------------------------------------------------------





    Private Sub _mPAYMAYA(ByRef payload As String)
        'Parse the json object in variable
        _mJsonObject = JObject.Parse(payload.ToString)

    End Sub

    Private Sub _mLB1(ByRef payload As String)
        'Parse the json object in variable
        _mJsonObject = JObject.Parse(payload.ToString)

    End Sub
    Private Sub _mLB2(ByRef payload As String)
        'Parse the json object in variable
        _mJsonObject = JObject.Parse(payload.ToString)

    End Sub














End Class