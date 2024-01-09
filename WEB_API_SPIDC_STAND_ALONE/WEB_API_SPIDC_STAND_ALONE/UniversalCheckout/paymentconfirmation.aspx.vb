
Imports System.Net
Imports System.Web.Services
Imports System.Net.Http
Imports RestSharp
Imports System.IdentityModel.Tokens.Jwt
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq
Imports Microsoft.Reporting.WebForms
'Imports SPIDC.Resources
Imports System.Reflection
Imports WEB_API_SPIDC_STAND_ALONE.My.Resources

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
    Private Shared _msgID As Integer

    Private Shared _mJson As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        _msgID = _ucpseqrandom.Next(10000, 100000)
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
                    Case "CANCEL"
                        'PaymentConfirmationStatus = _mprocessPaymentConfirmation(paymentgateway, transactionid, payload)
                End Select

            Case "PAYMAYA"
                'Check paymentgateway Status
                Select Case status
                    Case "SUCCESS"
                        PaymentConfirmationStatus = _mprocessPaymentConfirmation(paymentgateway, transactionid, payload)
                    Case "FAILED"
                        'PaymentConfirmationStatus = _mprocessPaymentConfirmation(paymentgateway, transactionid, payload)
                    Case "CANCEL"
                        'PaymentConfirmationStatus = _mprocessPaymentConfirmation(paymentgateway, transactionid, payload)

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



    Public Shared Function _mprocessPaymentConfirmation(paymentgateway As String, transactionid As String, payload As String) As String()
        _mprocessPaymentConfirmation = Nothing
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
                'Add This To Display Response of api call
                ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
            Case "LBP2"
                'Add This To Display Response of api call
                ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)

        End Select

        Return _mprocessPaymentConfirmation
    End Function

    'GCASH PAYMENT INQUIRY
    Public Shared Function _mGCASHPaymentInquiry(paymentgateway As String, transactionid As String, _acquirementId As String, _shortTransId As String, payload As String) As String()
        _mGCASHPaymentInquiry = Nothing
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

            Dim api_SPIDCFee = _payloadJson("payload")("dataInformation")(0)("SpidcFee")
            'Dim api_TotalAmt_Paid = _payloadJson("payload")("dataInformation")(0)("TotalAmount")
            Dim api_BillingDate = _payloadJson("payload")("dataInformation")(0)("BiilingDate")
            'for online payment reference
            Dim api_email As String = _payloadJson("payload")("dataInformation")(0)("Email")

            Dim api_checkoutstatus As String = _payloadJson("payload")("dataInformation")(0)("CheckOutStatus")

            Dim api_OriginLink = _payloadJson("payload")("dataInformation")(0)("UrlOrigin")

            Dim api_SuccessLink = _payloadJson("payload")("dataInformation")(0)("UrlSuccess")
            'Get Web Config
            Spidc_Web_API_Config.WebApiConfig()

            Dim api_otherFee = Spidc_Web_API_Config._mAppGCASH_GATEWAY_FEE '_payloadJson("payload")("dataInformation")(0)("OtherFee")
            Dim api_TotalAmt_Paid As Integer = CInt(Double.Parse(api_Total) + Integer.Parse(api_otherFee))


            'check and create dfrom base on APPNAME
            Dim DFrom As String = Nothing
            If api_AppName = Spidc_Web_API_Config._mAppLinkSystem1 Then

                If api_TransDesc = "Individual Cedula" Then

                    DFrom = "CCIWEB"

                ElseIf api_TransDesc = "Corporation Cedula" Then

                    DFrom = "CCCWEB"

                End If


            Else

                DFrom = api_AppName

            End If





            Dim interest As String = Nothing
            'check if cedulaApp has interest and get the 2nd data in array as interest
            If api_AppName = Spidc_Web_API_Config._mAppLinkSystem1 Then
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

            'Check If POSTING IS SUCCESS
            If _cPaymentPosting._Insert_GenOR_Posting(api_accno, api_lname, api_fname, api_mname, api_address, api_PaymentRef, api_AssessmentNo, api_AppName, api_TransDesc, api_Total, api_otherFee, api_SPIDCFee, interest, api_TotalAmt_Paid, _paymentGateway, _paymentGatewayRefNo, api_BillingDate, DFrom, transactionid, api_email, api_checkoutstatus, _security, payload) Then
                'Call Webhook To Send EOR Email
                If sendEOR(api_AppName, "Electronic Official Receipt", EorPostingModel.eORno, api_Total, api_email, api_OriginLink, api_PaymentRef, api_TransDesc, api_accno) Then
                    '    'Check If System Is Not SPIDC  FOR SENDING NOTIFICATION OF PAYMENT
                    Select Case api_AppName
                        Case Spidc_Web_API_Config._mAppLinkSystem2
                            'Post Notification To Other App WebHook To Get Payment Status APP NAME / NOTIFICATION URL/ Gateway Payment Ref/PAYMENT REF / STATUS 1 FOR SUCCESS
                            _mWebhooksNotifications(api_AppName, api_SuccessLink, transactionid, api_PaymentRef, "1", EorPostingModel.eORno, _paymentGateway)
                        Case Else

                    End Select

                    _mGCASHPaymentInquiry = {"success", api_TransDesc, api_accno, api_email, api_TotalAmt_Paid, api_TotalAmt_Paid, _sqlDateNow.ToString("yyyy-MM-dd'T'HH:mm:ssK"), api_OriginLink}
                Else
                    _mGCASHPaymentInquiry = {"error", api_TransDesc, api_accno, api_email, api_TotalAmt_Paid, api_TotalAmt_Paid, api_BillingDate, api_OriginLink}
                End If

            Else

                _mGCASHPaymentInquiry = {"error", api_OriginLink}
            End If
            Return _mGCASHPaymentInquiry
        End If


    End Function

    'PAYMAYA PAYMENT INQUIRY
    Public Shared Function _mPayMayaPaymentInquiry(paymentgateway As String, transactionid As String, payload As String) As String()
        _mPayMayaPaymentInquiry = Nothing

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

        Dim _mpayload As Object = New JavaScriptSerializer().Deserialize(Of Object)(payload)
        Dim _Email As String = _mpayload("payload")("dataInformation")(0)("Email")
        Dim _ACCTNO As String = _mpayload("payload")("dataInformation")(0)("AccountNo")
        'SAVE RESPONE FROM PAYMAYA
        PayMayaModel.insert_PaymayaTransactions("Payment Response", _ACCTNO, _Email, "", jsonResponse, PAYMENT_RRN)

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

            Dim api_OriginLink = _payloadJson("payload")("dataInformation")(0)("UrlOrigin")

            Dim api_SuccessLink = _payloadJson("payload")("dataInformation")(0)("UrlSuccess")
            'Get Web Config
            Spidc_Web_API_Config.WebApiConfig()

            'check and create dfrom base on APPNAME
            Dim DFrom As String = Nothing
            If api_AppName = Spidc_Web_API_Config._mAppLinkSystem1 Then

                If api_TransDesc = "Individual Cedula" Then

                    DFrom = "CCIWEB"

                ElseIf api_TransDesc = "Corporation Cedula" Then

                    DFrom = "CCCWEB"

                End If


            Else

                DFrom = api_AppName

            End If


            Dim interest As String = Nothing
            'check if cedulaApp has interest and get the 2nd data in array as interest
            If api_AppName = Spidc_Web_API_Config._mAppLinkSystem1 Then
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

            'Check If POSTING IS SUCCESS
            If _cPaymentPosting._Insert_GenOR_Posting(api_accno, api_lname, api_fname, api_mname, api_address, api_PaymentRef, api_AssessmentNo, api_AppName, api_TransDesc, api_Total, api_otherFee, api_SPIDCFee, interest, api_TotalAmt_Paid, _paymentGateway, _paymentGatewayRefNo, api_BillingDate, DFrom, transactionid, api_email, api_checkoutstatus, _security, payload) Then
                'Call Webhook To Send EOR Email
                'Call Webhook To Send EOR Email
                If sendEOR(api_AppName, "Electronic Official Receipt", EorPostingModel.eORno, api_Total, api_email, api_OriginLink, api_PaymentRef, api_TransDesc, api_accno) Then
                    '    'Check If System Is Not SPIDC  FOR SENDING NOTIFICATION OF PAYMENT
                    Select Case api_AppName
                        Case Spidc_Web_API_Config._mAppLinkSystem2
                            'Post Notification To Other App WebHook To Get Payment Status APP NAME / NOTIFICATION URL/ Gateway Payment Ref/PAYMENT REF / STATUS 1 FOR SUCCESS
                            _mWebhooksNotifications(api_AppName, api_SuccessLink, transactionid, api_PaymentRef, "1", EorPostingModel.eORno, _paymentGateway)
                        Case Else

                    End Select

                    _mPayMayaPaymentInquiry = {"success", api_TransDesc, api_accno, api_email, api_TotalAmt_Paid, api_TotalAmt_Paid, _sqlDateNow.ToString("yyyy-MM-dd'T'HH:mm:ssK"), api_OriginLink}
                Else
                    _mPayMayaPaymentInquiry = {"error", api_TransDesc, api_accno, api_email, api_TotalAmt_Paid, api_TotalAmt_Paid, _sqlDateNow.ToString("yyyy-MM-dd'T'HH:mm:ssK"), api_OriginLink}
                End If
            Else
                _mPayMayaPaymentInquiry = {"error", api_OriginLink}
            End If
        End If




        Return _mPayMayaPaymentInquiry
    End Function


    'WEBHOOK SEND EMAIL EOR 
    'Public Shared Function _mWebhooks(appName As String, notificationSubject As String, eorNo As String, totalPaid As String, email As String, urlOrigin As String, transactionRef As String, transactionDesc As String, accountNo As String) As Boolean
    '    'Get Web Config
    '    Spidc_Web_API_Config.WebApiConfig()
    '    ' Replace with your API endpoint WEB HOOK URL
    '    Dim webhookURL As String = Spidc_Web_API_Config._mApiEndPointWebhooks
    '    ' Replace "your-api-key" with your actual API key
    '    Dim apiKey As String = Spidc_Web_API_Config._mApiKey
    '    ' Create a RestClient
    '    Dim client As New RestClient(webhookURL)
    '    ' Create a request with the desired HTTP method (POST in this case)
    '    Dim request As New RestRequest(Method.POST)
    '    ' Set the request content type (application/json in this example)
    '    request.AddHeader("Content-Type", "application/json")
    '    ' Add API key to the request headers
    '    request.AddHeader("Authorization", apiKey)
    '    ' Add any parameters or request body as needed
    '    ' Create a JObject to represent the JSON structure
    '    Dim jsonObject As New JObject From {
    '          {"event", "send_email_eor"},
    '          {"subject", notificationSubject},
    '          {"type", "webhook"},
    '          {"data", New JObject From {
    '              {"appName", appName},
    '              {"oRno", eorNo},
    '              {"accontNo", accountNo},
    '              {"transactionRef", transactionRef},
    '              {"transactionType", transactionDesc},
    '              {"email", email},
    '              {"totalPaid", totalPaid},
    '              {"urlOrigin", urlOrigin}
    '          }}
    '      }
    '    ' Convert JObject to a JSON string
    '    Dim jsonPayload As String = jsonObject.ToString()
    '    request.AddParameter("application/json", jsonPayload, ParameterType.RequestBody)
    '    ' Execute the request
    '    Dim response As IRestResponse = client.Execute(request)
    '    Dim _jsonResponse As Object = New JavaScriptSerializer().Deserialize(Of Object)(response.Content)

    '    'Check If Status is Success
    '    If _jsonResponse("status") = "success" Then
    '        Return True
    '    Else
    '        Return False
    '    End If

    'End Function


    Public Shared Function sendEOR(appName As String, notificationSubject As String, eorNo As String, totalPaid As String, email As String, urlOrigin As String, transactionRef As String, transactionDesc As String, accountNo As String) As Boolean
        Dim reportViewer As New ReportViewer()
        reportViewer.LocalReport.DataSources.Clear()
        'generate datatable for RDLC
        Dim EorPostingModel As New EorPostingModel
        Dim _nDataTable0 As New DataTable
        _nDataTable0 = EorPostingModel.Print_Template

        Dim _nDataTable1 As New DataTable
        _nDataTable1 = EorPostingModel.Print_Report(eorNo)
        Dim _nDataTable2 As New DataTable
        _nDataTable2 = EorPostingModel.Print_TOP(eorNo)
        reportViewer.ProcessingMode = ProcessingMode.Local

        Dim fullPath As String = System.Web.HttpContext.Current.Server.MapPath("../Report/eOR_Universal.rdlc")
        reportViewer.LocalReport.ReportPath = fullPath
        'Pass the datatable into the rdlc
        Dim _nReportDataSource0 As New ReportDataSource
        _nReportDataSource0.Name = "DataSet0"
        _nReportDataSource0.Value = _nDataTable0
        reportViewer.LocalReport.DataSources.Add(_nReportDataSource0)

        Dim _nReportDataSource1 As New ReportDataSource
        _nReportDataSource1.Name = "DataSet1"
        _nReportDataSource1.Value = _nDataTable1
        reportViewer.LocalReport.DataSources.Add(_nReportDataSource1)

        Dim _nReportDataSource2 As New ReportDataSource
        _nReportDataSource2.Name = "DataSet2"
        _nReportDataSource2.Value = _nDataTable2
        reportViewer.LocalReport.DataSources.Add(_nReportDataSource2)

        'convert amount money into letters
        Dim strAmount As String = Nothing
        strAmount = EorPostingModel.AmountInWords(totalPaid)

        'set the converted money in words into parameter on RDLC
        Dim paramList As New List(Of ReportParameter)
        paramList.Add(New ReportParameter("AmountInWords", strAmount))
        reportViewer.LocalReport.SetParameters(paramList)


        Dim bytes As Byte() = reportViewer.LocalReport.Render("PDF")



        reportViewer.LocalReport.Refresh()

        'Sending the generated report into pdf to email

        Dim sent As Boolean = False
        Dim err As String = Nothing
        Dim body As String

        body = "Dear Valued Tax Payer, <br> " & _
               "This confirms your bill payment transaction with the following details: <br> " & _
               "Transaction Number: " & transactionRef & "<br>" & _
               "Transaction Type: " & transactionDesc & "<br>" & _
               "Account No. : " & accountNo & "<br>" & _
               "Amount Paid : " & totalPaid & "<br> <br>" & _
               "Your Electronic Official Receipt is attached in this e-mail."


        'Send  The Email  check if  successfully send
        If EorPostingModel.Send_eOR(email, notificationSubject, body, bytes, sent, urlOrigin, err) Then
            Return True
        Else
            Return False
        End If




    End Function






    'WEBHOOK NOTIFICATIONS
    Public Shared Sub _mWebhooksNotifications(appName As String, notificationURL As String, gatewayTrasactionRef As String, transactionRef As String, status As String, orno As String, paymentType As String)
        'Get Web Config
        Spidc_Web_API_Config.WebApiConfig()
        ' Replace with your API endpoint WEB HOOK URL
        Dim webhookURL As String = notificationURL
        'Replace "your-api-key" with your actual API key
        Dim apiHeader As String = Nothing
        Dim apiKey As String = Nothing
        Select Case appName
            Case Spidc_Web_API_Config._mAppLinkSystem2 'PINANCLE
                apiHeader = "API_KEY"
                apiKey = "Pinn@cleP@ss123"
            Case Spidc_Web_API_Config._mAppLinkSystem3 'QPAX
                apiHeader = ""
                apiKey = ""
            Case Spidc_Web_API_Config._mAppLinkSystem4 'LCR
                apiHeader = ""
                apiKey = ""
            Case Spidc_Web_API_Config._mAppLinkSystem5 'DOHS
                apiHeader = ""
                apiKey = ""
            Case Spidc_Web_API_Config._mAppLinkSystem6 'EOBO
                apiHeader = ""
                apiKey = ""
        End Select
        ' Create a RestClient
        Dim client As New RestClient(webhookURL)
        ' Create a request with the desired HTTP method (POST in this case)
        Dim request As New RestRequest(Method.POST)
        ' Set the request content type (application/json in this example)
        request.AddHeader("Content-Type", "application/json")
        ' Add API key to the request headers
        request.AddHeader(apiHeader, apiKey)
        ' Add any parameters or request body as needed
        ' Create a JObject to represent the JSON structure
        _mJson = "{""transactionReferenceNo"": """ & transactionRef & """,""transactionNo"": """ & gatewayTrasactionRef & """,""status"": """ & status & """,""orno"": """ & orno & """,""paymentType"": """ & paymentType & """}"
        request.AddParameter("application/json", _mJson, ParameterType.RequestBody)
        ' Execute the request
        Dim response As IRestResponse = client.Execute(request)
    End Sub
End Class