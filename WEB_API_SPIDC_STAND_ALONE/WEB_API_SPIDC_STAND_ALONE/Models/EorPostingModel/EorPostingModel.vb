Imports System.Data.SqlClient
Imports System.Web.HttpContext
Imports System.Security.Cryptography
Imports System.Web.Script.Serialization

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Mail
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports Microsoft.Reporting.WebForms
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json


Imports System.Drawing.Imaging
Imports System.Drawing
Imports QRCoder
Imports System.Threading.Tasks
Imports System.Net.Mime



Public Class EorPostingModel






#Region "Variables Data"


    Private Shared EorPostingDataAccessLayer As New EorPostingDataAccessLayer

    '------------------------------------------


    Private _mSqlCon As SqlConnection
    Private _mQuery As String = Nothing
    Private _mSqlCommand As SqlCommand
    Private _mDataTable As DataTable
    Private _mSqlDataReader As SqlDataReader
    Private Shared _nQuery As String = Nothing
    '--------------------------------------------


    Public Shared srs As String = Nothing
    Public Shared form As String = Nothing
    Public Shared BookNo As String = Nothing
    Public Shared user As String = Nothing

    '---------------------------------------------

    Public Shared TIN As String = Nothing
    Public Shared Weight As String = Nothing
    Public Shared Height As String = Nothing
    Public Shared Gender As String = Nothing
    Public Shared Birth_Place As String = Nothing
    Public Shared BirthDate As String = Nothing
    Public Shared Civil_Status As String = Nothing
    Public Shared Citizenship As String = Nothing
    Public Shared BrgyCode As String = Nothing
    Public Shared BrgyDesc As String = Nothing

    '-----------------------------------------------


    Public Shared or_no As String = Nothing
    Public Shared agency As String = Nothing
    Public Shared eORno As String = Nothing
    Public Shared count As String = Nothing

    '------------------------------------------------

    Public Shared currentDate As DateTime = DateTime.Now
    Public Shared formattedDate_Setup As String = currentDate.ToString("yyyyMMdd")
    Public Shared timenow As String = Nothing

    '------------------------------------------------
    Public Shared QR_FILE As Byte()
    Public Shared QR_STRING As String = Nothing

    '--------------------------------------------------


    Public Shared SystemCode As String = Nothing
    Public Shared Ex_String As String = Nothing
    Public Shared Error_EventType As String = Nothing
    Public Shared WebhookName As String = Nothing
    Public Shared ErrorType As String = Nothing
    Public Shared ErrorStatusCode As String = Nothing


    '-------------------------------------------------

    Public Shared API_APP_NAME As String = Nothing

    '-----------------------------------------------
    Public Shared _mSenderEmailAddress As String
    Public Shared _mSenderEmailPassword As String
    Public Shared _mHost As String
    Public Shared _mPort As String
    Public Shared _mCC As String
    Public Shared _mBCC As String
    Public Shared _mSSL As String



#End Region

#Region "Properties Data"
    Public WriteOnly Property _pSqlConnection() As SqlConnection
        Set(value As SqlConnection)
            _mSqlCon = value
        End Set
    End Property
    Public ReadOnly Property _pQuery() As String
        Get
            Return _mQuery
        End Get
    End Property
    Public ReadOnly Property _pSqlCommand() As SqlCommand
        Get
            Return _mSqlCommand
        End Get
    End Property
    Public ReadOnly Property _pDataTable() As DataTable
        Get
            Try
                '----------------------------------
                Dim _nSqlDataAdapter As New SqlDataAdapter(_mSqlCommand)
                _mDataTable = New DataTable
                _nSqlDataAdapter.Fill(_mDataTable)

                Return _mDataTable
                '----------------------------------
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property
    Public ReadOnly Property _pSqlDataReader() As SqlDataReader
        Get
            Try
                '----------------------------------
                _mSqlDataReader = _mSqlCommand.ExecuteReader

                Return _mSqlDataReader
                '----------------------------------
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property
#End Region






    Public Function _Insert_GenOR_Posting(ByVal [AccNo] As String, _
                            ByVal [Lname] As String, _
                            ByVal [Fname] As String, _
                            ByVal [Mname] As String, _
                            ByVal [Address] As String, _
                            ByVal [PaymentRefNo] As String, _
                            ByVal [AssessmentNo] As String, _
                            ByVal [APPName] As String, _
                            ByVal [Description] As String, _
                            ByVal [Total] As String, _
                            ByVal [OtherFee] As String, _
                            ByVal [SPIDC_Fee] As String, _
                            ByVal [Interest] As String, _
                            ByVal [TotalAmt_Paid] As String, _
                            ByVal [Gateway] As String, _
                            ByVal [GatewayRef] As String, _
                            ByVal [Gateway_conf] As String, _
                            ByVal DFrom As String, _
                            ByVal transactionId As String, _
                            ByVal Email As String, _
                            ByVal Checkoutstatus As String, _
                            ByVal Security As String, _
                            ByVal [_JsonOBj] As Object)



        API_APP_NAME = APPName

        'get Data from TOIMS
        If get_Other_TOIMS_data(form, srs, BookNo, user) Then
        Else
            Return False
        End If

        'GENERATE NEW ORNO on TOIMS
        If get_new_ORNO(or_no) Then
        Else
            Return False
        End If

        'GET AGENCY ON TIMS
        If get_Agency(agency) Then
        Else
            Return False
        End If

        If APPName = "CEDULAAPP" Then


            If get_Other_TOIMS_data2(AccNo, TIN, Weight, Height, Gender, Birth_Place, BirthDate, Civil_Status, Citizenship) Then
            Else
                Return False
            End If

            'GET CITIZENSHIPCODE
            If get_CitizenshipCode(Citizenship) Then
            Else
                Return False
            End If

            If get_BrgyDesc(BrgyCode) Then
            Else
                Return False
            End If

        Else

            TIN = ""
            Height = ""
            Weight = ""
            Gender = ""
            Birth_Place = ""
            BirthDate = ""
            Civil_Status = ""
            Citizenship = ""
            BrgyCode = ""
            BrgyDesc = ""

        End If



        'GET EOR TIME
        If get_timeEOR(timenow) Then
        Else
            Return False
        End If

        'GENERATE NEW EORNO
        If get_EORNO(srs, or_no) Then
        Else
            Return False
        End If


        Dim _QSelect_GEN_OR As String = Nothing
        Dim _QSelect_GEN_OR2 As String = Nothing

        _QSelect_GEN_OR = " Select (SELECT CONVERT(VARCHAR, GETDATE(), 112))Setup, " &
                    " ('" & BookNo & "')BookNo, " &
                    " ('" & or_no & "')OR_No, " &
                    " (CONVERT(VARCHAR, GETDATE(), 120))OR_Date, " &
                    " (Concat('" & [Lname] & "', ',','" & [Fname] & "'))Payor, " &
                    " ('" & Address & "')Address, " &
                    " ('COL')PayCode, " &
                    " ('" & form & "')Form_Use, " &
                    " ('" & user & "')[User], " &
                    " ('" & srs & "')SRS, " &
                    " ('1')isPrintOR, " &
                    " (Year(GETDATE()))Year, " &
                    " ('" & agency & "')Place_Issued, " &
                    " ('" & Lname & "')LastName, " &
                    " ('" & Fname & "')FirstName, " &
                    " ('" & Mname & "')MI, " &
                    " ('" & Citizenship & "')Citizenship, " &
                    " ('" & BirthDate & "')Birth_Date, " &
                    " ('" & Birth_Place & "')Birth_Place, " &
                    " ('" & Height & "')Height, " &
                    " ('" & Weight & "')Weight, " &
                    " ('" & Gender & "')Male, " &
                    " ('" & Civil_Status & "')Civil_Status, " &
                    " ('" & TIN & "')TIN, " &
                    " ('" & PaymentRefNo & "')PaymentRefNo, " &
                    " ('1')isWeb, " &
                    " ('" & AssessmentNo & "')WebAssessNo, " &
                    " ('" & eORno & "')EORNumber, " &
                    " ('" & DFrom & "')DFrom, " &
                    " ('" & Total & "')Total, " &
                    " ('" & Interest & "')Interest, " &
                    " ('" & TotalAmt_Paid & "')Total_Amt_Paid, " &
                    " ('Community Tax Individual')TypeTrans, " &
                    " ('1')IsPrintORPermanent, " &
                    " ('" & AccNo & "')TIMSControlNo, " &
                    " ('" & Gateway & "')Gateway, " &
                    " ('" & GatewayRef & "')GatewayRefno, " &
                    " ('" & timenow & "')OR_Time "



        Dim jsonObject As JObject = JObject.Parse(_JsonOBj)
        Dim dataCodeArray As JArray = jsonObject("payload")("dataCode")


        ' Process dataCode API data FOR GEN_EXTN
        For Each dataCode As JObject In dataCodeArray
            Dim Main_Code As String = dataCode("SysTran_MainCode").ToString()
            Dim Ancestor As String = dataCode("SysTran_AncestorCode").ToString()
            Dim AcctNo As String = dataCode("SysTran_SubAccCode").ToString()
            Dim System_Amount As String = dataCode("systems_codeAmt").ToString()

            _QSelect_GEN_OR2 += "  Select (SELECT CONVERT(VARCHAR, GETDATE(), 112))Setup, " &
            " ('" & or_no & "')OR_No, " &
            " ('" & Main_Code & "')Main_Code, " &
            " ('" & Ancestor & "')Ancestor, " &
            " ('" & AcctNo & "')AcctNo, " &
            " ('" & System_Amount & "')Amount, " &
            " ('" & DFrom & "')DFrom, " &
            " ('" & form & "')SubFormUse, " &
            " (Year(GETDATE()))foryear, " &
            " ('" & srs & "')SRS  "

            If dataCode IsNot dataCodeArray.Last Then
                ' Add UNION ALL except for the last iteration
                _QSelect_GEN_OR2 += " UNION "
            End If

        Next
        '-----------------------------------
        'GEN OR POSTING

        Try
            Dim _InSertQuery As String = Nothing
            _InSertQuery = "INSERT INTO Gen_or (Setup,BookNo,Or_No,OR_Date,Payor,Address,PayCode,Form_Use,[User],SRS,isPrintOR,Year,Place_Issued,LastName,FirstName,MI,Citizenship,Birth_Date,Birth_Place,Height,Weight,Male,Civil_Status,TIN,PaymentRefNo,isWeb,WebAssessNo,EORNumber,DFrom,Total,Interest,Total_amt_Paid,TypeTrans,isPrintORPermanent,TIMSControlNo,Gateway,GatewayRefno,OR_Time) " & _QSelect_GEN_OR

            Dim _InSertQuery2 As String = Nothing
            _InSertQuery2 = "  INSERT INTO Gen_Extn " & _
           "(Setup,OR_No,Main_Code,Ancestor,AcctNo,Amount,DFrom,SubFormUse,ForYear,SRS)" & _QSelect_GEN_OR2

            'GENERATE [INSERT INTO SELECT] QUERY FOR GEN_OR AND GEN_EXTN
            Dim _nQueryBuild As String = _InSertQuery + _InSertQuery2


            EorPostingDataAccessLayer._mStrSql = _nQueryBuild
            EorPostingDataAccessLayer._mSqlCon = _mSqlCon

            'INSERT DATA TO GEN_OR AND GEN_EXTN
            If EorPostingDataAccessLayer.Execute_Insert_GEN_OR_AND_EXTN() Then
            Else
                Return False
            End If


            '----------------------------------
            'UPDATE OR_USED COUNT


            Dim UpdateQry As String = Nothing
            get_EORCount(count)

            UpdateQry = "Update Receipts set OR_Used =  '" & count & "' WHERE Form='EOR'  "

            EorPostingDataAccessLayer._mStrSql = UpdateQry
            EorPostingDataAccessLayer._mSqlCon = _mSqlCon

            'UPDATE OR_USED COUNT ON GEN_OR
            If EorPostingDataAccessLayer.Execute_Update_EOR_COUNT() Then
            Else
                Return False
            End If


            '----------------------------------------------

            Dim _QSelect_EOR As String = Nothing
            Dim _QSelect_EOR2 As String = Nothing


            _QSelect_EOR = " Select (Concat('" & Lname & "',',','" & Fname & "',' ', '" & Mname & "' ))PayorName, " &
                        " ('" & BrgyDesc & "')Barangay, " &
                        " (null)TaxType, " &
                        " ('" & [AccNo] & "')TDNBIN, " &
                        " (null)PIN, " &
                        " (null)PeriodCovered, " &
                        " ('" & AssessmentNo & "')AssessNo, " &
                        " ('" & eORno & "')eORno, " &
                        " (FORMAT(GETDATE(), 'MMMM dd, yyyy'))DateTime, " &
                        " ('" & transactionId & "')OnlineID, " &
                        " ('" & Gateway & "')Gateway_Selected, " &
                        " ('" & GatewayRef & "')Gateway_RefNo, " &
                        " ('" & Gateway_conf & "')Gateway_ConfDate, " &
                        " ('" & Total & "')Bill_Amount, " &
                        " ('" & [OtherFee] & "')Gateway_Fee, " &
                        " ('" & [SPIDC_Fee] & "')SPIDC_Fee, " &
                        " ('" & TotalAmt_Paid & "')GrandTotal, " &
                        " (null)QR_File, " &
                        " (null)QR_String, " &
                        " (null)PrevORno, " &
                        " (null)PrevAmountPaid, " &
                        " ('1')[Sent], " &
                        " (GETDATE())Sent_Date "

            ' Process dataCode from API FOR EOR_EXTN
            For Each dataCode As JObject In dataCodeArray
                Dim AcctNo As String = dataCode("SysTran_SubAccCode").ToString()
                Dim System_Amount As String = dataCode("systems_codeAmt").ToString()

                _QSelect_EOR2 += " Select ('" & eORno & "')eORNO, " &
                                " ('" & or_no & "')ORno, " &
                                " (null)TDNBIN, " &
                                " ('" & AcctNo & "')AccountCode, " &
                                " ('" & AssessmentNo & "')AssessmentNo, " &
                                " ('SPIDC API Payment')NatureOfCollection, " &
                                " ('" & System_Amount & "')Amount "

                If dataCode IsNot dataCodeArray.Last Then
                    ' Add UNION ALL except for the last iteration
                    _QSelect_EOR2 += " UNION "
                End If
            Next


            Dim _InsertQuery_EOR As String = Nothing
            Dim _InsertQuery_EOR2 As String = Nothing


            '--------------------------------
            'EOR POSTING

            _InsertQuery_EOR = "INSERT INTO [eOR] (PayorName,Barangay,TaxType,TDNBIN,PIN,PeriodCovered,AssessNo,eORno,DateTime,OnlineID,Gateway_Selected,Gateway_RefNo,Gateway_ConfDate,Bill_Amount,Gateway_Fee,SPIDC_Fee,GrandTotal,QR_File,QR_String,PrevORno,PrevAmountPaid,Sent,Sent_Date) " & _QSelect_EOR

            _InsertQuery_EOR2 = "INSERT INTO [eOR_Extn] (eORNO,ORno,TDNBIN,AccountCode,AssessmentNo,NatureOfCollection,Amount) " & _QSelect_EOR2

            Dim _nQuery_EOR_Build As String = _InsertQuery_EOR + _InsertQuery_EOR2


            EorPostingDataAccessLayer._mStrSql = _nQuery_EOR_Build
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS

            'INSERT both EOR and EOR_EXTN
            If EorPostingDataAccessLayer.Execute_EOR_AND_EXTN() Then
            Else
                Return False
            End If


            'Update GENERATED QR STRING AND FILE
            If Update_eOR_QR_Data(eORno) Then
            Else
                Return False
            End If

            'Insert in Online Payment Refference
            Dim OnlPaymentRefQry As String = "INSERT INTO OnlinePaymentRefno(TXNREFNO, EMAILADDR, PAYMENTCHANNEL, ACCTNO, strDATE, StatusID, Status, GateWayRefNo, Security, Type, TransDate, TRXDATE, rawAmount, totAmount, feeAmount, DateVerified, VerifiedBy, Verifying, Via, PostStatus, PostedDate, FeeAmountSPIDC, minORNO, maxORNO) VALUES ('" & transactionId & "', '" & Email & "','" & Gateway & "','" & AccNo & "',FORMAT(GETDATE(), 'yyMMdd'),'SUCCESS','Successful','" & GatewayRef & "', '" & Security & "', '" & DFrom & "','" & Gateway_conf & "', '" & Gateway_conf & "','" & Total & "', '" & TotalAmt_Paid & "', '" & SPIDC_Fee & "', null , null, null, '" & Gateway & "', '1', FORMAT(GETDATE(), 'yyyy-M-dd'), '" & SPIDC_Fee & "', '" & eORno & "', '" & eORno & "')"
            EorPostingDataAccessLayer._mStrSql = OnlPaymentRefQry
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS

            'INSERT OnlinePaymentRef
            If EorPostingDataAccessLayer.Execute_Insert_OnlinePaymentRef() Then
            Else
                Return False
            End If






            If APPName = "CEDULAAPP" Then
                Dim UpdateCTCPostStatusQry As String = "update [CTC_Online_Application] set PostedStatus = 'Paid' where ControlNo = '" & AccNo & "'  "
                EorPostingDataAccessLayer._mStrSql = UpdateCTCPostStatusQry
                EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TIMS
                'INSERT OnlinePaymentRef
                If EorPostingDataAccessLayer.Execute_Update_CTC_Online_App_PostStatus() Then
                Else
                    Return False
                End If
            End If


            Return True

            '----------------------------------


        Catch ex As Exception

            Ex_String = ex.ToString
            ERRORLOGS(APPName, "EORPOSTING", Ex_String)
            Return False

        End Try


    End Function


#Region "TOIMS FUNCTION"
    Public Function get_Other_TOIMS_data(ByRef Form_use As String, ByRef Srs As String, ByRef BookNo As String, ByRef User As String) As String
        Try

            _nQuery = "select srs,Book_No,Form,[user] from Receipts where Form='eor' "

            EorPostingDataAccessLayer._mStrSql = _nQuery
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TOIMS

            If EorPostingDataAccessLayer.Execute_other_TOIMS_Data() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function


    Public Function get_new_ORNO(ByRef OR_No As String) As String
        Try
            _nQuery = Nothing
            _nQuery = _
           "select top 1 (select top 1 dbo.fn_FormatNumberWithZeros(len(OR_No),OR_No+1) from gen_or " & _
           "where OR_No between '000000000001' and '000000001000' and Form_Use='eor' and BookNo ='EOR001' " & _
           " and SRS='CAL' order by OR_No desc) as OR_No "


            EorPostingDataAccessLayer._mStrSql = _nQuery
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TOIMS

            If EorPostingDataAccessLayer.Execute_get_new_ORNO() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return OR_No
        End Try

    End Function


    Public Function get_Agency(ByRef Agency As String) As String
        Try
            Dim _nQuery As String = Nothing

            _nQuery = _
           "select Agency from SYSTEMSETTINGS"

            EorPostingDataAccessLayer._mStrSql = _nQuery
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TOIMS

            If EorPostingDataAccessLayer.Execute_get_Agency() Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Return False
        End Try

    End Function


    Public Function get_Other_TOIMS_data2(ByVal AccNo As String, ByRef TIN As String, ByRef Height As String, ByRef Weight As String, ByRef Gender As String, ByRef Birth_Place As String, ByRef Birth_Date As String, ByRef Civil_Status As String, ByRef Citizenship As String) As String
        Try
            Dim _nQuery As String = Nothing
            _nQuery = _
           "SELECT * FROM [CTC_Online_Application] where ControlNo = '" & AccNo & "'  "

            EorPostingDataAccessLayer._mStrSql = _nQuery
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TIMS

            If EorPostingDataAccessLayer.Execute_get_other_TOIMS_Data2() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function get_BrgyDesc(ByRef BrgyCode As String) As String
        Try
            Dim _nQuery As String = Nothing

            _nQuery = _
           " SELECT * FROM MuniCitySubBrgy where MuniCityBrgyCode = '" & BrgyCode & "' "

            EorPostingDataAccessLayer._mStrSql = _nQuery
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TOIMS

            If EorPostingDataAccessLayer.Execute_get_BrgyDesc() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function



    Public Function get_CitizenshipCode(ByRef Citizenship As String) As String
        Try
            Dim _nQuery As String = Nothing

            _nQuery = _
           " Select Code2 from tabextn where seq+code1 =  '02003' and Description  = '" & Citizenship & "' "

            EorPostingDataAccessLayer._mStrSql = _nQuery
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TOIMS

            If EorPostingDataAccessLayer.Execute_get_CitizenshipCode() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function


    Public Function get_EORCount(ByRef CountNo As String) As String
        Try
            Dim _nQuery As String = Nothing

            _nQuery = _
           " SELECT count(*) as [Count] FROM Gen_Or WHERE OR_No between '000000000001' and '000000001000' and Form_Use='EOR' and SRS ='CAL' and BookNo ='EOR001'"


            EorPostingDataAccessLayer._mStrSql = _nQuery
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TOIMS

            If EorPostingDataAccessLayer.Execute_get_EORNOCOUNT() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function
#End Region

#Region "EORPOSTING"

    Public Function get_timeEOR(ByRef Time As String) As String
        Try
            Dim _nQuery As String = Nothing

            _nQuery = _
           " SELECT FORMAT(GETDATE(), 'hh:mm:ss tt') AS CurrentServerTime "


            EorPostingDataAccessLayer._mStrSql = _nQuery
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TOIMS

            If EorPostingDataAccessLayer.Execute_get_timeEOR() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function


    Public Function get_EORNO(ByRef srs As String, ByRef OR_No As String) As String
        Dim EORNO As String = Nothing
        Try
            Dim _nQuery As String = Nothing

            _nQuery = _
           "select Concat(  '" & srs & "',Format(getdate(), 'yyyyMM-'), '" & OR_No & "' ) AS EOR_NO"

            EorPostingDataAccessLayer._mStrSql = _nQuery
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_TOIMS

            If EorPostingDataAccessLayer.Execute_get_EORNO() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function




    Public Shared Function Update_eOR_QR_Data(ByVal eORno As String, Optional ByRef err As String = Nothing)
        Try
            'QR_STRING = GET_QRstring(eORno)
            GET_QRstring(eORno)
            'GenerateQRcode(QR_STRING)
            QR_FILE = GenerateQRcode(QR_STRING)

            Dim _nQuery As String = Nothing

            _nQuery = "UPDATE eOR set QR_File = @QR_File,QR_STRING='" & QR_STRING & "' where eORno = '" & eORno & "'"

            EorPostingDataAccessLayer._mStrSql = _nQuery
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS

            If EorPostingDataAccessLayer.Execute_Update_EOR_QR_DATA(QR_FILE) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function



    Public Shared Function GenerateQRcode(ByVal QR_String As String, Optional err As String = Nothing) As Byte()
        Dim result As Byte()
        Try
            Dim qrGenerator As New QRCodeGenerator
            Dim qrCode = qrGenerator.CreateQrCode(QR_String, QRCodeGenerator.ECCLevel.Q)
            Dim imgBarCode As System.Web.UI.WebControls.Image = New System.Web.UI.WebControls.Image()
            Dim QR_CODE As New QRCode(qrCode)
            imgBarCode.Height = 350
            imgBarCode.Width = 350

            Using bm As Bitmap = QR_CODE.GetGraphic(6)
                Using ms As MemoryStream = New MemoryStream()
                    bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                    result = ms.ToArray()
                End Using
            End Using
        Catch ex As Exception
            err = ex.Message
        End Try
        Return result
    End Function




    Public Shared Function GET_QRstring(ByVal ORNO As String) As String
        Try
            Dim _nQuery As String = _
           " select eorno,TaxType,TDNBIN,GrandTotal,gateway_selected,[DateTime] from eor" &
           " where eORNO ='" & ORNO & "'"

            EorPostingDataAccessLayer._mStrSql = _nQuery
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS

            If EorPostingDataAccessLayer.Execute_get_QR_STRING() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function



#End Region

#Region "FOR ERROR"

    Public Shared Function ERRORLOGS(ByVal APPName As String, ByVal FUNCTION_NAME As String, ByVal ERROR_DESC As String)

        Try

            Dim replace_ex_string As String = Nothing
            replace_ex_string = ERROR_DESC.Replace("'", "`")

            Dim _zLogBook = "INSERT INTO [dbo].[WebhooksLogs] VALUES" & _
                            "('" & [APPName] & "','EorPostingModel',null,'" & _
                            FUNCTION_NAME & "',getdate(),'200','" & replace_ex_string & "')"


            'Dim _nSqlCommand_Error As New SqlCommand(_zLogBook, Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS)
            '_nSqlCommand_Error.ExecuteNonQuery()
            EorPostingDataAccessLayer._mStrSql = _zLogBook
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS

            'INSERT both EOR and EOR_EXTN
            If EorPostingDataAccessLayer.Execute_ERROR_LOGS() Then

            End If

            Return False

        Catch ex As Exception
            Return False
        End Try

    End Function





#End Region

#Region "ReportData"
    Public Function Print_Template() As DataTable
        _mDataTable = New DataTable
        Try
            Dim _Query As String = "SELECT *,(select LGU_LOGO from [" & Spidc_Web_API_Global_Connection._pSqlCxn_CR.Database & "].dbo.[LGU_Profile]) LGU_LOGO FROM EOR_SETUP"
            'Dim _nSqlDataAdapter As New SqlDataAdapter(_Query, Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS)
            '_nSqlDataAdapter.Fill(_mDataTable)


            EorPostingDataAccessLayer._mStrSql = _Query
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS

            'INSERT OnlinePaymentRef
            If EorPostingDataAccessLayer.Execute_Print_Template(_mDataTable) Then

            End If



        Catch ex As Exception
        End Try
        Return _mDataTable

    End Function


    Public Function Print_Report(ByVal eORNO As String) As DataTable
        _mDataTable = New DataTable
        Try
            Dim _Query As String = "SELECT * FROM EOR WHERE eORNO='" & eORNO & "'"

            'Dim _nSqlDataAdapter As New SqlDataAdapter(_Query, Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS)
            '_nSqlDataAdapter.Fill(_mDataTable)

            EorPostingDataAccessLayer._mStrSql = _Query
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS

            If EorPostingDataAccessLayer.Execute_Print_Report(_mDataTable) Then

            End If

        Catch ex As Exception
        End Try
        Return _mDataTable
    End Function

    Public Function Print_TOP(eORNO) As DataTable
        _mDataTable = New DataTable
        Dim _SQLcon As New SqlConnection
        Try
            Dim _Query As String
            _Query = "SELECT * FROM EOR_EXTN WHERE eORNO='" & eORNO & "'"
            'Dim _nSqlDataAdapter As New SqlDataAdapter(_Query, Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS)
            '_nSqlDataAdapter.Fill(_mDataTable)

            EorPostingDataAccessLayer._mStrSql = _Query
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS

            If EorPostingDataAccessLayer.Execute_Print_TOP(_mDataTable) Then

            End If

        Catch ex As Exception
        End Try
        Return _mDataTable
    End Function


    Public Shared Function NumberToWords(ByVal number As Integer) As String
        Dim ones() As String = {"", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"}
        Dim teens() As String = {"ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"}
        Dim tens() As String = {"", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"}

        If number < 10 Then
            Return ones(number)
        ElseIf number < 20 Then
            Return teens(number - 10)
        ElseIf number < 100 Then
            Return tens(number \ 10) & " " & ones(number Mod 10)
        ElseIf number < 1000 Then
            Return ones(number \ 100) & " hundred " & NumberToWords(number Mod 100)
        ElseIf number < 1000000 Then
            Return NumberToWords(number \ 1000) & " thousand " & NumberToWords(number Mod 1000)
        Else
            Return ""
        End If
    End Function

    Public Shared Function AmountInWords(ByVal amount As Decimal) As String
        Dim cents As Integer = Math.Round((amount - Math.Truncate(amount)) * 100)
        Dim dollars As Integer = Math.Truncate(amount)
        Dim centavo As String = ""
        Dim dollarText As String = ""

        ' Get centavo text
        If cents > 0 Then
            centavo = " and " & NumberToWords(cents) & " centavos"
        Else
            centavo = " and zero centavos"
        End If

        ' Get dollar text
        If dollars > 0 Then
            dollarText = NumberToWords(dollars)
            If dollars = 1 Then
                dollarText &= " pesos"
            Else
                dollarText &= " pesos"
            End If
        Else
            '  dollarText = "zero dollars"
        End If

        Return dollarText & centavo
    End Function



#End Region

#Region "Sending Email EOR"

    Public Shared Function _pSubGetEmailMaster()
        Try
            '----------------------------------      
            Dim _nQuery As String
            _nQuery = _
                "select EmailAddress,Password,Port,Host,isnull(EmailCC,'0')EmailCC,isnull(EmailBCC,'0')EmailBCC,SSL from [SetupWebEmailMaster]"

            EorPostingDataAccessLayer._mStrSql = _nQuery
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_CR

            If EorPostingDataAccessLayer.Execute_pSubGetEmailMaster() Then
                Return True
            Else
                Return False
            End If

            '----------------------------------
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Shared Function Send_eOR(ByVal ReceiverEmail As String, _
                       ByVal Subject As String, _
                       ByVal Body As String, _
                       ByVal eOR As Byte(), _
                       ByRef Sent As Boolean,
                       ByRef btnLink As String,
                       Optional ByRef err As String = Nothing)
        Try
            Dim _nClass2 As New cHardwareInformation
            Dim _nMachineName As String = _nClass2._pMachineName.ToUpper
            Dim curr_url As String = HttpContext.Current.Request.Url.AbsoluteUri
            Dim clr As String = "#ccc"
            Dim Footer As String = Nothing

            Dim Logo_Data As Byte()
            Dim Logo_Name As String
            Dim Logo_Ext As String

            Dim FullUrl As String = HttpContext.Current.Request.Url.AbsoluteUri
            Dim PagePath As String = HttpContext.Current.Request.Url.AbsolutePath
            Dim PageName As String = System.IO.Path.GetFileName(PagePath)
            Try
                If FullUrl.IndexOf("?") > 0 Then
                    FullUrl = FullUrl.Substring(0, FullUrl.IndexOf("?"))
                End If
            Catch ex As Exception

            End Try

            Dim loginURL As String = FullUrl.Replace(PageName, "Register.aspx?email=" & ReceiverEmail)


            EorPostingModel.get_Header_DATA(Logo_Data, Logo_Name, Logo_Ext)

            Dim Logo_IMG As System.Web.UI.WebControls.Image = New System.Web.UI.WebControls.Image()
            Logo_IMG.ImageUrl = "data:" & Logo_Ext & ";base64," & Convert.ToBase64String(Logo_Data)

            Dim LOGOBytes As Byte() = Convert.FromBase64String(Replace(Logo_IMG.ImageUrl, "data:" & Logo_Ext & ";base64,", ""))
            Dim LOGOBitmap As System.IO.MemoryStream = New System.IO.MemoryStream(LOGOBytes)
            Dim LOGOResource As LinkedResource = New LinkedResource(LOGOBitmap, MediaTypeNames.Image.Jpeg)



            Body = "<style>.panel1{color:black; width:60%;}" & _
                                     "@media screen and (max-width: 360px) {.panel1 {width: width:100%;}}" & _
            "</style>" & _
                    "<center style='font-size:x-large;'> " & _
                    "  <div style='border:2px solid white;background-color:#EAEAEA;font-family:calibri;padding:20px';>" & _
                    "  <div class='panel1'>" & _
                    "  <div style='font-size:large;padding:5px;border:2px solid white;color:white;background-color:" & clr & "'>" & _
              " <img   style='object-fit: contain;width:100%;' src='cid:" & LOGOResource.ContentId & "'/>" & _
"  <p><strong>" & _
                    Subject & _
                    "  </strong> </p>" & _
"  <div style='text-align:left;padding:10px;background-color:white;color:black;'>" & _
Body & _
"<a href='" & btnLink & "'" & " target='_blank' style='text-decoration:none;display: block;width: 200px;height: 25px;background: #5373DC;padding: 10px;text-align: center;border-radius: 5px;color: white;font-weight: bold;line-height: 25px;font-size:small;'> Visit Web Service Portal </a><br/><br/>" & _
Footer & _
"  </div></br></br>*** This is an electronic message please do not reply ***<div></div></div></center>"

            'Dim _nclass As New EorPostingModel
            '_nclass._pSqlConnection = Spidc_Web_API_Global_Connection._pSqlCxn_CR
            EorPostingModel._pSubGetEmailMaster()

            Using mm As New MailMessage(_mSenderEmailAddress, ReceiverEmail)
                mm.Subject = Subject
                mm.Body = Body

                If String.IsNullOrEmpty(_mBCC) = False Then
                    Dim BlindCarbonCopy As MailAddress = New MailAddress(_mBCC)
                    mm.Bcc.Add(BlindCarbonCopy)
                End If

                If String.IsNullOrEmpty(_mCC) = False Then
                    Dim CarbonCopy As MailAddress = New MailAddress(_mCC)
                    mm.CC.Add(CarbonCopy)
                End If

                If LCase(ReceiverEmail).Contains("@gmail.com") = True Then
                    Dim alternativeView As AlternateView = AlternateView.CreateAlternateViewFromString(Body, Nothing, MediaTypeNames.Text.Html)
                    alternativeView.LinkedResources.Add(LOGOResource)
                    mm.AlternateViews.Add(alternativeView)
                End If



                mm.IsBodyHtml = True
                Dim smtp As New SmtpClient()
                smtp.Host = _mHost

                If _mSSL = 1 Then
                    smtp.EnableSsl = True
                Else
                    smtp.EnableSsl = False
                End If

                Dim NetworkCred As New NetworkCredential(_mSenderEmailAddress, _mSenderEmailPassword)
                smtp.UseDefaultCredentials = False
                smtp.Credentials = NetworkCred
                smtp.Port = _mPort

                Dim attachment As New Attachment(New System.IO.MemoryStream(eOR), "Electronic Official Receipt.pdf")

                ' Add the PDF attachment to the email.
                mm.Attachments.Add(attachment)

                smtp.Send(mm)
                Sent = True

                If Sent = True Then
                    Return True
                Else
                    Return False
                End If


            End Using
        Catch ex As Exception
            Sent = False
            err = ex.Message
            Return False
        End Try
    End Function


    Public Shared Function get_Header_DATA(ByRef HEADER_TEMPLATE As Byte(), ByRef HEADER_TEMPLATE_Name As String, ByRef HEADER_TEMPLATE_Ext As String)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = " SELECT HEADER_TEMPLATE,HEADER_TEMPLATE_Name,HEADER_TEMPLATE_Ext from [LGU_Profile]"

            EorPostingDataAccessLayer._mStrSql = _nQuery
            EorPostingDataAccessLayer._mSqlCon = Spidc_Web_API_Global_Connection._pSqlCxn_CR

            If EorPostingDataAccessLayer.Execute_get_Header_DATA(HEADER_TEMPLATE, HEADER_TEMPLATE_Name, HEADER_TEMPLATE_Ext) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function



#End Region





End Class

