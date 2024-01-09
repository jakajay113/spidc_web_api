Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System.Net.Mail
Imports System.Net
Imports Microsoft.Reporting.WebForms

Public Class WebhooksDataAccessLayer
    'SPIDC Config
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config

#Region "Variable Data Access Layer Webhooks Web API"
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
    Private Shared _mControlNo As String
    Private Shared _mAppStatus As String
    Private Shared _mFilaData As Byte()
    Private Shared _mCTCType As String
    Private Shared _mActionType As String
    Private Shared _mDataResponse As String

    'Webhook Payload
    Private Shared _mWebhookEvent As String
    Private Shared _mWebhookSubject As String
    Private Shared _mWebhookType As String
    Private Shared _mEORNo As String
    Private Shared _mAccountNo As String
    Private Shared _mTotalAmount As String
    Private Shared _mEmail As String
    Private Shared _mTransactionRef As String
    Private Shared _mTransactionType As String
    Private Shared _mAppName As String
    Private Shared _mUrlOrigin As String


#End Region
#Region "Property Data Access Layer Webhooks Web API"
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

    '-----------------------------------------------------------------------------------POST with Parameters Data Access Layer-----------------------------------------------------------------------------------------------------
    Public Shared Function _mPostWebhooksEvent(ByVal value As Object) As Boolean
        Try
            'Call THE SPIDC WEB API CONFIG
            Spidc_Web_API_Config.WebApiConfig()
            'Parse the json object in variable
            _mJsonObject = JObject.Parse(value.ToString)

            'Payload Webhook Event
            _mWebhookEvent = _mJsonObject("event")
            _mWebhookSubject = _mJsonObject("subject")
            _mWebhookType = _mJsonObject("type")

            'Check Webhook Event
            Select Case _mWebhookEvent
                Case "send_email_eor" 'Send Email EOR
                    'Payload Webhook 
                    _mAppName = _mJsonObject("data")("appName")
                    _mEORNo = _mJsonObject("data")("oRno")
                    _mAccountNo = _mJsonObject("data")("accontNo")
                    _mTotalAmount = _mJsonObject("data")("totalPaid")
                    _mTransactionRef = _mJsonObject("data")("totalPaid")
                    _mTransactionType = _mJsonObject("data")("transactionType")
                    _mEmail = _mJsonObject("data")("email")
                    _mUrlOrigin = _mJsonObject("data")("urlOrigin")

                    Dim reportViewer As New ReportViewer()
                    reportViewer.LocalReport.DataSources.Clear()
                    'generate datatable for RDLC
                    Dim EorPostingModel As New EorPostingModel
                    Dim _nDataTable0 As New DataTable
                    _nDataTable0 = EorPostingModel.Print_Template

                    Dim _nDataTable1 As New DataTable
                    _nDataTable1 = EorPostingModel.Print_Report(_mEORNo)
                    Dim _nDataTable2 As New DataTable
                    _nDataTable2 = EorPostingModel.Print_TOP(_mEORNo)
                    reportViewer.ProcessingMode = ProcessingMode.Local

                    Dim fullPath As String = System.Web.HttpContext.Current.Server.MapPath("../../Report/eOR_Universal.rdlc")
                    Dim replace_fullpath = fullPath.Replace("\api", "")
                    reportViewer.LocalReport.ReportPath = replace_fullpath
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
                    strAmount = EorPostingModel.AmountInWords(_mTotalAmount)

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
                           "Transaction Number: " & _mTransactionRef & "<br>" & _
                           "Transaction Type: " & _mTransactionType & "<br>" & _
                           "Account No. : " & _mAccountNo & "<br>" & _
                           "Amount Paid : " & _mTotalAmount & "<br> <br>" & _
                           "Your Electronic Official Receipt is attached in this e-mail."


                    'Send  The Email  check if  successfully send
                    If EorPostingModel.Send_eOR(_mEmail, _mWebhookSubject, body, bytes, sent, _mUrlOrigin, err) Then
                        _mStatus = "success"
                        _mData = Nothing
                        _mMessage = "Email successfully sent."
                        _mCode = "200"
                        Return True
                    Else
                        _mStatus = "error"
                        _mData = Nothing
                        _mMessage = "Error sending email"
                        _mCode = "500"
                        Return False
                    End If



                Case "send_email" 'Send Email 




                Case Else 'Recieve Payment Gateway Notification



            End Select

       
        Catch ex As Exception
            _mStatus = "error"
            _mData = Nothing
            _mMessage = ex.Message
            _mCode = "500"
            Return False
        End Try
        Return False
    End Function




End Class
