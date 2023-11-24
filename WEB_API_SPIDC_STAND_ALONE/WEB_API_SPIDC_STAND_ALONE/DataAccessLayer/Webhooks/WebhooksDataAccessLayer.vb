Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System.Net.Mail
Imports System.Net
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

            Dim fromAddress As New MailAddress(Spidc_Web_API_Config._mApiMailFromAddress, Spidc_Web_API_Config._mApiMailFromName) 'Sender Email
            Dim toAddress As New MailAddress("jakajaysitjar13@gmail.com", "Jaka Jay S. Sitjar")
            Dim fromPassword As String = Spidc_Web_API_Config._mApiMailPassword  'Sender Password
            Dim smtpClient As New SmtpClient()
            smtpClient.Host = Spidc_Web_API_Config._mApiMailHost ' Update with your SMTP server
            smtpClient.Port = Spidc_Web_API_Config._mApiMailPort ' Update with the port for your SMTP server
            smtpClient.EnableSsl = True ' Enable SSL for secure connection
            smtpClient.UseDefaultCredentials = False
            smtpClient.Credentials = New NetworkCredential(fromAddress.Address, fromPassword)
            Dim mailMessage As New MailMessage(fromAddress, toAddress)
            mailMessage.Subject = "Webhooks Testing"
            Dim body As String = "<!DOCTYPE html><html lang='en'><head> <meta charset='UTF-8'> <meta name='viewport' content='width=device-width, initial-scale=1.0'> <title>HTML Email</title></head><body bgcolor='#f7f7f7' width='100%' style='margin: 0;'> <table bgcolor='#f7f7f7' cellpadding='0' cellspacing='0' border='0' height='100%' width='100%' style='border-collapse:collapse;'> <tr> <td> <center style='width: 100%;'> <table cellspacing='0' cellpadding='0' border='0' align='center' bgcolor='#ffffff' width='600' class='email-container'> <tr> <td style='padding: 20px; text-align: center; font-family: sans-serif; font-size: 12px; mso-height-rule: exactly; line-height: 20px; color: #555555;'> Email Header Bla bla<br> <br> </td> </tr> </table> <table align='center' width='600' class='email-container'> <tr> <td style='padding: 0px; text-align: center'><img src='https://defendmusic.com/_site/wp-content/uploads/2017/09/Nike-Banner1.jpg' width='100%' alt='alt_text' border='0'></td> </tr> </table> <table cellspacing='0' cellpadding='0' border='0' align='center' bgcolor='#ffffff' width='600' class='email-container'> <tr> <td style='padding: 40px; text-align: center; font-family: sans-serif; font-size: 15px; mso-height-rule: exactly; line-height: 20px; color: #555555;'><h1 style='color: black;'>Hi Your Webhooks Is Dope. </h1> <img src='https://images.squarespace-cdn.com/content/v1/5a863eb564b05f76722c62f2/1655521401241-5XTSCVI4KDEOB8BR6NCF/IMG_6917.jpg?format=1000w' width='50%' height='50%' alt='alt_text' border='0'> <br> <br> </td> </tr> <tr> <td background='file:///Macintosh HD/Users/Finy/Library/Application Support/Adobe/Dreamweaver CC 2015/en_US/Configuration/Temp/Assets/eam8f18613.TMP/images/Image_600x230.png' bgcolor='black' style='text-align: center; background-position: center center !important; background-size: cover !important;'> <div> <table align='center' border='0' cellpadding='0' cellspacing='0' width='100%'> <tr> <td valign='middle' style='text-align: center; padding: 40px; font-family: sans-serif; font-size: 15px; mso-height-rule: exactly; line-height: 20px; color: #ffffff;'> Before Footer </td> </tr> </table> </div> </td> </tr> </table> <table align='center' width='600' class='email-container'> <tr> <td style='padding: 10px 10px;width: 100%;font-size: 12px; font-family: sans-serif; mso-height-rule: exactly; line-height:18px; text-align: center; color: #888888;'> <span class='mobile-link--footer'>Copyright &copy; 2023 spdic. All rights reserved.<br> address </span> <br> <br> <span style='color:#888888;'>Please do not reply to this message. This email was sent from a notification-only email address that cannot accept incoming email</span> </td> </tr> </table> <!-- Email Footer : END --> </center> </td> </tr> </table> </body></html>"
            mailMessage.IsBodyHtml = True ' Indicates that the email body is HTML
            mailMessage.Body = body ' HTML Email

            ' Attach the files (add more attachments as needed)
            'Dim attachmentPaths As String() = {"C:\path\to\attachment1.pdf", "C:\path\to\attachment2.docx"}
            'For Each attachmentPath As String In attachmentPaths
            '    Dim attachment As New Attachment(attachmentPath)
            '    mailMessage.Attachments.Add(attachment)
            'Next

            ' Attach the files (add more attachments as needed)
            Dim attachmentPaths As String() = {HttpContext.Current.Server.MapPath("~/Configuration/LCR-UCD.pdf")}
            For Each attachmentPath As String In attachmentPaths
                Dim attachment As New Attachment(attachmentPath)
                mailMessage.Attachments.Add(attachment)
            Next

            ' Send the email
            smtpClient.Send(mailMessage)

            _mStatus = "success"
            _mData = Nothing
            _mMessage = "Email successfully sent."
            _mCode = "200"
            Return True
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
