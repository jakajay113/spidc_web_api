Imports System.Net
Imports System.Web.Services
Imports System.Net.Http
Imports RestSharp
Imports System.IdentityModel.Tokens.Jwt
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq

Public Class paymentconfirmation
    Inherits System.Web.UI.Page
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config

    Private Shared paymentgateway As String
    Private Shared transactionid As String
    Private Shared status As String
    Private Shared _sqlDateNow As DateTime
    Private Shared _sqlDateNow10 As DateTime
    Private Shared _msecqLabel As String
    Private Shared _ucpseqrandom As New Random()
    Private Shared _msgID As Integer = _ucpseqrandom.Next(10000, 100000)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub


    <WebMethod()> _
    Public Shared Function PaymentConfirmation(url As String, payload As String)
        Dim PaymentConfirmationStatus = Nothing

        'Call The Web API Config
        Spidc_Web_API_Config.WebApiConfig()
        _msecqLabel = Spidc_Web_API_Config._mAppSequenceLabel

        ' Replace \u0026 with &
        Dim correctedUrl As String = url.Replace("\u0026", "&")
        Dim uri As New Uri(correctedUrl)
        ' Get different parts of the URL
        Dim scheme As String = uri.Scheme
        Dim host As String = uri.Host
        Dim path As String = uri.AbsolutePath
        Dim query As String = uri.Query
        ' Parse parameters from the query string
        Dim parameters As System.Collections.Specialized.NameValueCollection = System.Web.HttpUtility.ParseQueryString(uri.Query)
        'Parameters
        paymentgateway = parameters("paymentgateway")
        transactionid = parameters("transactionID")
        status = parameters("status")
        'Check paymentgateway Type
        Select Case paymentgateway
            Case "GCASH"
                'Check paymentgateway Status
                Select Case status
                    Case "SUCCESS"
                        PaymentConfirmationStatus = _mprocessPaymentConfirmation(paymentgateway, transactionid, payload)
                    Case Else


                End Select


            Case "PAYMAYA"
                'Check paymentgateway Status
                Select Case status
                    Case "SUCCESS"
                        PaymentConfirmationStatus = _mprocessPaymentConfirmation(paymentgateway, transactionid, payload)
                    Case Else

                End Select


            Case "LBP1"
                'Check paymentgateway Status
                Select Case status
                    Case "SUCCESS"

                        PaymentConfirmationStatus = _mprocessPaymentConfirmation(paymentgateway, transactionid, payload)
                    Case Else

                End Select


            Case "LBP2"
                'Check paymentgateway Status
                Select Case status
                    Case "SUCCESS"
                        PaymentConfirmationStatus = _mprocessPaymentConfirmation(paymentgateway, transactionid, payload)

                    Case Else

                End Select


            Case "OTC"
                PaymentConfirmationStatus = _mprocessPaymentConfirmation(paymentgateway, transactionid, payload)


        End Select

        Return PaymentConfirmationStatus

    End Function



    Public Shared Function _mprocessPaymentConfirmation(paymentgateway As String, transactionid As String, payload As String) As Boolean
        _mprocessPaymentConfirmation = False
        Select Case paymentgateway
            Case "GCASH"
                'GCASH PAYMENT INQUIRY
                'Add This To Display Response of api call
                ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
                Dim _nClass As New cDalPayment
                _nClass._pSqlConnection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                Dim acquirementId As String = Nothing
                Dim shortTransId As String = Nothing
                _nClass.Get_GcashLog(transactionid, acquirementId, shortTransId)
                'Static Data
                '_mGCASHPaymentInquiry(paymentgateway, "CTC220624-00004", "20220624121212800110170676525926310", "628164264", payload)
                _mprocessPaymentConfirmation = _mGCASHPaymentInquiry(paymentgateway, transactionid, acquirementId, shortTransId, payload)
            Case "PAYMAYA"
                'PAYMAYA PAYMENT INQUIRY
                'Add This To Display Response of api call
                ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
                Dim _nClass As New cDalPayment
                _nClass._pSqlConnection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                'Dim acquirementId As String = Nothing
                'Dim shortTransId As String = Nothing
                _mprocessPaymentConfirmation = _mPayMayaPaymentInquiry(paymentgateway, transactionid, payload)

            Case "LBP1"

            Case "LBP2"

        End Select



    End Function


    'GCASH PAYMENT INQUIRY
    Public Shared Function _mGCASHPaymentInquiry(paymentgateway As String, transactionid As String, _acquirementId As String, _shortTransId As String, payload As String) As Boolean
        _mGCASHPaymentInquiry = False
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New Script.Serialization.JavaScriptSerializer()
        Dim _sqlDateNow As DateTime
        Dim _sqlDateNow10 As DateTime
        Dim _nClass As New cDalPayment
        _nClass._pSqlConnection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
        _nClass.GetsqlDateNow(_sqlDateNow, _sqlDateNow10)

        Dim objReq As New GCashOrderQueryModel.GCashOrderQuery
        objReq.request = New GCashOrderQueryModel.Request()
        objReq.request.head = New GCashOrderQueryModel.Head()
        objReq.request.body = New GCashOrderQueryModel.Body()

        'Call Gcash Config
        GCashModel.GCashConfig()
        'Head
        objReq.request.head.version = "2.0"
        objReq.request.head._function = "gcash.acquiring.order.query"
        objReq.request.head.clientId = GCashModel.ClientId
        objReq.request.head.clientSecret = GCashModel.ClientSecret
        objReq.request.head.reqTime = _sqlDateNow.ToString("yyyy-MM-dd'T'HH:mm:ssK")
        objReq.request.head.reqMsgId = GCashOrderQueryModel.Gen_MD5()
        objReq.request.body.acquirementId = _acquirementId
        objReq.request.body.merchantTransId = transactionid '_merchantTransId
        objReq.request.body.shortTransId = _shortTransId
        objReq.signature = "signature string"
        Dim client = New RestClient(GCashModel.GcashDomain)
        client.Timeout = -1
        Dim request = New RestRequest(GCashModel.GcashFunctionOrderQuery, Method.POST)
        Dim body = serializer.Serialize(objReq)
        body = body.Replace("_function", "function")
        Dim StringToSign As String = Nothing
        StringToSign = body.Remove(0, 11) ' Remove "request":
        StringToSign = StringToSign.Replace(",""signature"":""signature string""}", "")
        Dim signedString As String = GCashModel.Do_Sign(StringToSign)
        body = body.Replace("signature string", signedString)
        request.AddParameter("application/json", body, ParameterType.RequestBody)

        Dim _function As String = objReq.request.head._function
        Dim _transactionId As String = _shortTransId
        Dim _signature As String = signedString

        Dim response1 As IRestResponse = client.Execute(request)
        Dim _gcashResponse As Object = New JavaScriptSerializer().Deserialize(Of Object)(response1.Content)

        Dim _paymentGateway = paymentgateway
        Dim _acquirementStatus = _gcashResponse("response")("body")("statusDetail")("acquirementStatus")
        Dim _security = _gcashResponse("response")("body")("acquirementId")
        Dim _paymentGatewayRefNo = _gcashResponse("response")("body")("transactionId")

        'SAVE REQUEST FROM SPIDC
        _nClass._pSqlConnection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
        _nClass.GCASH_InsertLog(_function, _transactionId, transactionid, body, "Request from SPIDC", _acquirementId, _signature)

        'SAVE RESPONSE FROM GCASH
        _nClass._pSqlConnection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
        _nClass.GCASH_InsertLog(_function, _transactionId, transactionid, body, "Response from GCASH", _acquirementStatus, _signature)


        'Check If Status Is Payment Status Is Success
        If _acquirementStatus = "SUCCESS" Then
            Dim _payloadJson As Object = New JavaScriptSerializer().Deserialize(Of Object)(payload)
            Dim api_accno = _payloadJson("payload")("dataInformation")(0)("AccountNo")
            Dim api_lname = _payloadJson("payload")("dataInformation")(0)("LastName")
            Dim api_fname = _payloadJson("payload")("dataInformation")(0)("Fname")
            Dim api_mname = _payloadJson("payload")("dataInformation")(0)("MiddleName")
            Dim api_address = _payloadJson("payload")("dataInformation")(0)("Address")
            Dim api_PaymentRef = _payloadJson("payload")("dataInformation")(0)("TransactionRef")
            Dim api_AssessmentNo = _payloadJson("payload")("dataInformation")(0)("AssessmentNo")
            Dim api_AppName = _payloadJson("payload")("dataInformation")(0)("AppName")
            Dim api_TransDesc = _payloadJson("payload")("dataInformation")(0)("transDesc")
            Dim api_Total = _payloadJson("payload")("dataInformation")(0)("RawAmount")
            Dim api_otherFee = _payloadJson("payload")("dataInformation")(0)("OtherFee")
            Dim api_SPIDCFee = _payloadJson("payload")("dataInformation")(0)("SpidcFee")
            Dim api_TotalAmt_Paid = _payloadJson("payload")("dataInformation")(0)("TotalAmount")
            Dim api_BillingDate = _payloadJson("payload")("dataInformation")(0)("BiilingDate")
            'for online payment reference
            Dim api_email As String = _payloadJson("payload")("dataInformation")(0)("Email")

            Dim api_checkoutstatus As String = _payloadJson("payload")("dataInformation")(0)("CheckOutStatus")

            'check and create dfrom base on APPNAME
            Dim DFrom As String = Nothing
            If api_AppName = "CEDULAAPP" Then

                If api_TransDesc = "Individual Cedula" Then

                    DFrom = "CCIWEB"

                ElseIf api_TransDesc = "Corporation Cedula" Then

                    DFrom = "CCCWEB"

                End If

            End If


            Dim interest As String = Nothing
            'check if cedulaApp has interest and get the 2nd data in array as interest
            If api_AppName = "CEDULAAPP" Then
                If _payloadJson("payload")("dataCode")(1)("systems_codeAmt") Then
                    interest = _payloadJson("payload")("dataCode")(1)("systems_codeAmt")
                Else
                    interest = "0.00"
                End If
            Else
                interest = "0.00"
            End If

            'CALL POST OF GEN OR AND EOR
            Dim _cPaymentPosting As New EorPostingModel
            _cPaymentPosting._pSqlConnection = Spidc_Web_API_Global_Connection._pSqlCxn_TOIMS
            _cPaymentPosting._Insert_GenOR_Posting(api_accno, api_lname, api_fname, api_mname, api_address, api_PaymentRef, api_AssessmentNo, api_AppName, api_TransDesc, api_Total, api_otherFee, api_SPIDCFee, interest, api_TotalAmt_Paid, _paymentGateway, _paymentGatewayRefNo, api_BillingDate, DFrom, _transactionId, api_email, api_checkoutstatus, _security, payload)


            'MsgBox("Payment Is Success")
            _mGCASHPaymentInquiry = True
        End If
   

    End Function




    'PAYMAYA PAYMENT INQUIRY
    Public Shared Function _mPayMayaPaymentInquiry(paymentgateway As String, transactionid As String, payload As String) As Boolean
        _mPayMayaPaymentInquiry = False

        Dim _nClass As New cDalPayment
        Dim PayMayaModel As New PayMayaModel
        'Call Gcash Config
        PayMayaModel.PayMayaConfig()

        Dim PK As String = PayMayaModel.PrivateKey
        Dim SK As String = PayMayaModel.SecretKey
        Dim PKPASS As String = ""
        Dim SKPASS As String = ""
        Dim rrn = transactionid

        Dim client = New RestClient(PayMayaModel.PayMayaDomain & "/payments/v1/payment-rrns/" & rrn)
        client.Timeout = -1

        Dim request = New RestRequest(Method.[GET])
        request.AddHeader("Authorization", "Basic " & PayMayaModel.Base64Encode(SK & ":" & SKPASS))

        Dim response1 As IRestResponse = client.Execute(request)
        Dim jsonResponse As String = response1.Content
        Dim _paymayaResponse As Object = New JavaScriptSerializer().Deserialize(Of Object)(response1.Content)

        Dim valueType As Type = _paymayaResponse.[GetType]()
        Dim PAYMENT_STATUS As String = Nothing
        Dim PAYMENT_RRN As String = Nothing
        Dim PAYMENT_ID As String = Nothing
        Dim _paymentGatewayRefNo As String = Nothing
        Dim _paymentGateway As String = paymentgateway
        Dim _transactionId As String = transactionid
        Dim _security As String = _paymayaResponse(_paymayaResponse.length - 1)("fundSource")("id")
        PAYMENT_STATUS = _paymayaResponse(_paymayaResponse.length - 1)("status")
        PAYMENT_RRN = _paymayaResponse(_paymayaResponse.length - 1)("requestReferenceNumber")
        PAYMENT_ID = _paymayaResponse(_paymayaResponse.length - 1)("id")
        _paymentGatewayRefNo = PAYMENT_ID

        'Check If Status Is Payment Status Is Success
        If PAYMENT_STATUS = "PAYMENT_SUCCESS" Then
            Dim _payloadJson As Object = New JavaScriptSerializer().Deserialize(Of Object)(payload)
            Dim api_accno = _payloadJson("payload")("dataInformation")(0)("AccountNo")
            Dim api_lname = _payloadJson("payload")("dataInformation")(0)("LastName")
            Dim api_fname = _payloadJson("payload")("dataInformation")(0)("Fname")
            Dim api_mname = _payloadJson("payload")("dataInformation")(0)("MiddleName")
            Dim api_address = _payloadJson("payload")("dataInformation")(0)("Address")
            Dim api_PaymentRef = _payloadJson("payload")("dataInformation")(0)("TransactionRef")
            Dim api_AssessmentNo = _payloadJson("payload")("dataInformation")(0)("AssessmentNo")
            Dim api_AppName = _payloadJson("payload")("dataInformation")(0)("AppName")
            Dim api_TransDesc = _payloadJson("payload")("dataInformation")(0)("transDesc")
            Dim api_Total = _payloadJson("payload")("dataInformation")(0)("RawAmount")
            Dim api_otherFee = _payloadJson("payload")("dataInformation")(0)("OtherFee")
            Dim api_SPIDCFee = _payloadJson("payload")("dataInformation")(0)("SpidcFee")
            Dim api_TotalAmt_Paid = _payloadJson("payload")("dataInformation")(0)("TotalAmount")
            Dim api_BillingDate = _payloadJson("payload")("dataInformation")(0)("BiilingDate")
            'for online payment reference
            Dim api_email As String = _payloadJson("payload")("dataInformation")(0)("Email")

            Dim api_checkoutstatus As String = _payloadJson("payload")("dataInformation")(0)("CheckOutStatus")

            'check and create dfrom base on APPNAME
            Dim DFrom As String = Nothing
            If api_AppName = "CEDULAAPP" Then
                If api_TransDesc = "Individual Cedula" Then
                    DFrom = "CCIWEB"
                ElseIf api_TransDesc = "Corporation Cedula" Then
                    DFrom = "CCCWEB"
                End If
            End If


            Dim interest As String = Nothing
            'check if cedulaApp has interest and get the 2nd data in array as interest
            If api_AppName = "CEDULAAPP" Then
                If _payloadJson("payload")("dataCode")(1)("systems_codeAmt") Then
                    interest = _payloadJson("payload")("dataCode")(1)("systems_codeAmt")
                Else
                    interest = "0.00"
                End If
            Else
                interest = "0.00"
            End If


            'CALL POST OF GEN OR AND EOR
            Dim _cPaymentPosting As New EorPostingModel
            _cPaymentPosting._pSqlConnection = Spidc_Web_API_Global_Connection._pSqlCxn_TOIMS
            _cPaymentPosting._Insert_GenOR_Posting(api_accno, api_lname, api_fname, api_mname, api_address, api_PaymentRef, api_AssessmentNo, api_AppName, api_TransDesc, api_Total, api_otherFee, api_SPIDCFee, interest, api_TotalAmt_Paid, _paymentGateway, _paymentGatewayRefNo, api_BillingDate, DFrom, _transactionId, api_email, api_checkoutstatus, _security, payload)

            'MsgBox("Payment Is Success")
            _mPayMayaPaymentInquiry = True

        End If


    End Function








End Class