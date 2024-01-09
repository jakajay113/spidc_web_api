

#Region "Imports"

Imports System.Data.SqlClient
'Imports VB.NET.Methods
Imports System.Web.HttpContext
Imports System.Security.Cryptography


#End Region

Public Class cDalPayment

#Region "Variables Data"
    Private _mSqlCon As SqlConnection
    Private _mQuery As String = Nothing
    Private _mSqlCommand As SqlCommand
    Private _mDataTable As DataTable
    Private _mSqlDataReader As SqlDataReader
    Public acquirementId As String
    Public merchantId As String
    Public Shared _DataSet As DataSet
    Public Shared _WebAssessNo As String
    Public Shared _TXNREFNO As String

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

#Region "Variables Field"
    '---------For Entry---------------------
    Private _mReferenceNumber As String
    Private _mReferenceDate As Date
    Private _mBPAccountNumber As String
    Private _mForyear As String 'added by abby 20180223
    Private _mTypeofPayment As String
    Private _mBankName As String
    Private _mBankCode As String
    Private _mCardNumber As String
    Private _mAccountNumber As String
    Private _mAccountName As String
    Private _mExpiryMonth As String
    Private _mExpiryYear As String
    Private _mCardCVV As String
    Private _mBankAddress As String
    Private _mTOPamount As String
    Private _mFormatDate As String
    Private _mDragonRefNo As String
    Private _mPickupDelivery As String

    Private _mPaymentStatus As String

    Private _mctr As String

    '--------------------------------OAIMS ONline Payment Parameters
    Private _mTXNREFNO As String
    Private _mEMAILADDR As String
    Private _mPaymentChannel As String
    Private _mACCTNO As String
    Private _mSTatus As String
    Private _mGateWyRefNo As String
    Private _mSecurity As String
    Private _mType As String
    Private _mtrxdate As String
    Private _mrawAmount As String
    Private _mtotAmount As String
    Private _mfeeAmount As String
    Private _mGWfeeAmount As String
    Private _mAssessmentNo As String



    '---------For Printing------------------
    Private _mStatcode As String
    Private _mOwner As String
    Private _mOwndesc As String
    Private _mCommercialName As String
    Private _mCommercialAddress As String
    '---------For Printing------------------

    '---------For Payment List------------------
    Private _mRemarks As String
    Private _mVerify As String
    '---------For Payment List------------------

    '---------For Update is posted------------------
    Private _misPosted As String
    '---------For Update is posted------------------

    '---------For Courier------------------
    Private _mCourierCode As String
    Private _mCourierName As String
    Private _mCourierLocation As String
    Private _mCourierContact As String
    '---------For Courier------------------

    '---------For Filter------------------
    Private _mFilter As String

    '---------For LBP------------------
    Public Shared LBP_MerchantCode As String
    Public Shared LBP_MerchantRefNo As String
    Public Shared LBP_Particulars As String
    Public Shared LBP_Amount As String
    Public Shared LBP_PayorName As String
    Public Shared LBP_PayorEmail As String
    Public Shared LBP_ReturnURLOK As String
    Public Shared LBP_ReturnURLError As String
    Public Shared LBP_Hash As String
    Public Shared LBP_ACCTNO As String
    Public Shared LBP_TransType As String
    Public Shared LBP_OnlineID As String



    '-------------------------------------

    '---------For DBP------------------

    Public Shared DBP_terminalID As String
    Public Shared DBP_referenceCode As String
    Public Shared DBP_amount As String
    Public Shared DBP_securityToken As String
    Public Shared DBP_returnURL As String
    Public Shared DBP_serviceType As String

    '-------------------------------------

    '---------For Gateway Credentials------------------
    Public Shared gw_Status As Boolean
    Public Shared gw_MerchantCode As String
    Public Shared gw_TestURL As String
    Public Shared gw_TestURL_Return As String
    Public Shared gw_ProdURL As String
    Public Shared gw_ProdURL_Return As String
    Public Shared gw_UserName As String
    Public Shared gw_Password As String
    Public Shared gw_SecretKey As String
    Public Shared gw_apiKey As String
    Public Shared gw_SecretApiKey As String
    Public Shared gw_PublicApiKey As String
    Public Shared gw_DateNow As DateTime
    Public Shared gw_LIVE As Boolean

    Private _mDate_Esta As String
    Private _mEmail As String

    Public Shared RPT_Single_TDN As String
    Public Shared Date_Expire As String

#End Region

#Region "Properties Field"
    '-------- LBP ---------


    '-----------------
    Public Property _pCtr As String
        Get
            Return _mctr
        End Get
        Set(value As String)
            _mctr = value
        End Set
    End Property


    Public Property _pReferenceNumber As String
        Get
            Return _mReferenceNumber
        End Get
        Set(value As String)
            _mReferenceNumber = value
        End Set
    End Property
    Public Property _pReferenceDate As Date
        Get
            Return _mReferenceDate
        End Get
        Set(value As Date)
            _mReferenceDate = value
        End Set
    End Property
    Public Property _pBPAccountNumber As String
        Get
            Return _mBPAccountNumber
        End Get
        Set(value As String)
            _mBPAccountNumber = value
        End Set
    End Property
    Public Property _pForyear As String
        Get
            Return _mForyear
        End Get
        Set(value As String)
            _mForyear = value
        End Set
    End Property

    Public Property _pTypeofPayment As String
        Get
            Return _mTypeofPayment
        End Get
        Set(value As String)
            _mTypeofPayment = value
        End Set
    End Property
    Public Property _pBankName As String
        Get
            Return _mBankName
        End Get
        Set(value As String)
            _mBankName = value
        End Set
    End Property
    Public Property _pBankCode As String
        Get
            Return _mBankCode
        End Get
        Set(value As String)
            _mBankCode = value
        End Set
    End Property
    Public Property _pCardNumber As String
        Get
            Return _mCardNumber
        End Get
        Set(value As String)
            _mCardNumber = value
        End Set
    End Property
    Public Property _pAccountNumber As String
        Get
            Return _mAccountNumber
        End Get
        Set(value As String)
            _mAccountNumber = value
        End Set
    End Property
    Public Property _pAccountName As String
        Get
            Return _mAccountName
        End Get
        Set(value As String)
            _mAccountName = value
        End Set
    End Property
    Public Property _pExpiryMonth As String
        Get
            Return _mExpiryMonth
        End Get
        Set(value As String)
            _mExpiryMonth = value
        End Set
    End Property
    Public Property _pExpiryYear As String
        Get
            Return _mExpiryYear
        End Get
        Set(value As String)
            _mExpiryYear = value
        End Set
    End Property
    Public Property _pCardCVV As String
        Get
            Return _mCardCVV
        End Get
        Set(value As String)
            _mCardCVV = value
        End Set
    End Property
    Public Property _pBankAddress As String
        Get
            Return _mBankAddress
        End Get
        Set(value As String)
            _mBankAddress = value
        End Set
    End Property
    Public Property _pTOPamount As String
        Get
            Return _mTOPamount
        End Get
        Set(value As String)
            _mTOPamount = value
        End Set
    End Property
    Public Property _pFormatDate As String
        Get
            Return _mFormatDate
        End Get
        Set(value As String)
            _mFormatDate = value
        End Set
    End Property
    Public Property _pStatcode As String
        Get
            Return _mStatcode
        End Get
        Set(value As String)
            _mStatcode = value
        End Set
    End Property
    Public Property _pOwner As String
        Get
            Return _mOwner
        End Get
        Set(value As String)
            _mOwner = value
        End Set
    End Property
    Public Property _pOwndesc As String
        Get
            Return _mOwndesc
        End Get
        Set(value As String)
            _mOwndesc = value
        End Set
    End Property
    Public Property _pCommercialName As String
        Get
            Return _mCommercialName
        End Get
        Set(value As String)
            _mCommercialName = value
        End Set
    End Property
    Public Property _pCommercialAddress As String
        Get
            Return _mCommercialAddress
        End Get
        Set(value As String)
            _mCommercialAddress = value
        End Set
    End Property

    Public Property _pRemarks As String
        Get
            Return _mRemarks
        End Get
        Set(value As String)
            _mRemarks = value
        End Set
    End Property
    Public Property _pVerify As String
        Get
            Return _mVerify
        End Get
        Set(value As String)
            _mVerify = value
        End Set
    End Property
    Public Property _pisPosted As String
        Get
            Return _misPosted
        End Get
        Set(value As String)
            _misPosted = value
        End Set
    End Property
    Public Property _pDragonRefNo As String
        Get
            Return _mDragonRefNo
        End Get
        Set(value As String)
            _mDragonRefNo = value
        End Set
    End Property
    Public Property _pPickupDelivery As String
        Get
            Return _mPickupDelivery
        End Get
        Set(value As String)
            _mPickupDelivery = value
        End Set
    End Property

    Public Property _pPaymentStatus As String
        Get
            Return _mPaymentStatus
        End Get
        Set(value As String)
            _mPaymentStatus = value
        End Set
    End Property
    Public Property _pCourierCode As String
        Get
            Return _mCourierCode
        End Get
        Set(value As String)
            _mCourierCode = value
        End Set
    End Property
    Public Property _pCourierName As String
        Get
            Return _mCourierName
        End Get
        Set(value As String)
            _mCourierName = value
        End Set
    End Property
    Public Property _pCourierLocation As String
        Get
            Return _mCourierLocation
        End Get
        Set(value As String)
            _mCourierLocation = value
        End Set
    End Property
    Public Property _pCourierContact As String
        Get
            Return _mCourierContact
        End Get
        Set(value As String)
            _mCourierContact = value
        End Set
    End Property
    Public Property _pFilter As String
        Get
            Return _mFilter
        End Get
        Set(value As String)
            _mFilter = value
        End Set
    End Property

    Public Property _pEmail() As String
        Get
            Return _mEmail
        End Get
        Set(ByVal value As String)
            _mEmail = value
        End Set
    End Property


    '------------------- Oaims Online Payment Parameters
    Public Property _pTXNREFNO() As String
        Get
            Return _mTXNREFNO
        End Get
        Set(ByVal value As String)
            _mTXNREFNO = value
        End Set
    End Property

    Public Property _pEMAILADDR() As String
        Get
            Return _mEMAILADDR
        End Get
        Set(ByVal value As String)
            _mEMAILADDR = value
        End Set
    End Property

    Public Property _pPaymentChannel() As String
        Get
            Return _mPaymentChannel
        End Get
        Set(ByVal value As String)
            _mPaymentChannel = value
        End Set
    End Property

    Public Property _pACCTNO() As String
        Get
            Return _mACCTNO
        End Get
        Set(ByVal value As String)
            _mACCTNO = value
        End Set
    End Property
    Public Property _pSTatus() As String
        Get
            Return _mSTatus
        End Get
        Set(ByVal value As String)
            _mSTatus = value
        End Set
    End Property

    Public Property _pGateWyRefNo() As String
        Get
            Return _mGateWyRefNo
        End Get
        Set(ByVal value As String)
            _mGateWyRefNo = value
        End Set
    End Property
    Public Property _pSecurity() As String
        Get
            Return _mSecurity
        End Get
        Set(ByVal value As String)
            _mSecurity = value
        End Set
    End Property
    Public Property _pType() As String
        Get
            Return _mType
        End Get
        Set(ByVal value As String)
            _mType = value
        End Set
    End Property
    Public Property _ptrxdate() As String
        Get
            Return _mtrxdate
        End Get
        Set(ByVal value As String)
            _mtrxdate = value
        End Set
    End Property

    Public Property _prawAmount() As String
        Get
            Return _mrawAmount
        End Get
        Set(ByVal value As String)
            _mrawAmount = value
        End Set
    End Property
    Public Property _pfeeAmount() As String
        Get
            Return _mfeeAmount
        End Get
        Set(ByVal value As String)
            _mfeeAmount = value
        End Set
    End Property

    Public Property _pGWfeeAmount() As String
        Get
            Return _mGWfeeAmount
        End Get
        Set(ByVal value As String)
            _mGWfeeAmount = value
        End Set
    End Property

    Public Property _ptotAmount() As String
        Get
            Return _mtotAmount
        End Get
        Set(ByVal value As String)
            _mtotAmount = value
        End Set
    End Property

    Public Property _pAssessmentNo() As String
        Get
            Return _mAssessmentNo
        End Get
        Set(ByVal value As String)
            _mAssessmentNo = value
        End Set
    End Property

    Public Property _pDate_Esta() As Date
        Get
            Return _mDate_Esta
        End Get
        Set(ByVal value As Date)
            _mDate_Esta = value
        End Set
    End Property
#End Region

#Region "Properties Field Original"

#End Region

#Region "Routine Command"
    'Public Sub _pSubGetRPTpaymentDetails()
    '    Try
    '        Dim _nQuery As String = Nothing
    '        _nQuery = " " & _
    '      "  select top 1 AssessFromBilling.AssessNo,Payer,sum(TotalAmount)TotalAmount                                             " & _
    '      "  from  AssessFromBilling                                                                                         " & _
    '      "          left join Assess_FrmNewBilling_A on AssessFromBilling.AssessNo = Assess_FrmNewBilling_A.AssessNo        " & _
    '      "          where isnull(Trans_Condition,'') = ''                                                                   " & _
    '      "  and isdate(AssessDate)=1                                                                                        " & _
    '      "  and convert(nvarchar(10),convert(datetime,ValidDate),101)>= convert(nvarchar(10),getdate(),101)                 " & _
    '      "  and AssessFromBilling.assessno in (select distinct assessno                                                     " & _
    '      "  from Assess_FrmNewBilling_dtls                                                                                  " & _
    '      "  where isnull(isfrmbackEnd,0)=0)                                                                                 " & _
    '      "  group by AssessFromBilling.AssessNo,Payer,AssessFromBilling.AssessDate                                          " & _
    '      "  order by convert(datetime,AssessDate) desc,AssessFromBilling.assessno desc                                      "


    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
    '        With _mSqlCommand.Parameters
    '        End With

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader

    '            '----------------------------------
    '            'Indexes
    '            With _nSqlDataReader
    '                Dim _iAssessNo As Integer = .GetOrdinal("AssessNo")
    '                Dim _iPayer As Integer = .GetOrdinal("Payer")
    '                Dim _iTotalAmount As Integer = .GetOrdinal("TotalAmount")
    '                Dim _ictr As Integer = 0

    '                '----------------------------------
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes

    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            cSessionLoader._pAssessmentNo = ._pReturnString(_nSqlDataReader(_iAssessNo))
    '                            cSessionLoader._pPayor = ._pReturnString(_nSqlDataReader(_iPayer))
    '                            cSessionLoader._pRPTtotal = ._pReturnString(_nSqlDataReader(_iTotalAmount))

    '                        Loop
    '                    End If

    '                End With
    '            End With

    '            _nSqlDataReader.Close()
    '        End Using
    '    Catch ex As Exception

    '    End Try



    'End Sub

    Public Sub _pSubSelect()
        Try
            '----------------------------------
            'TODO: Perform Checking of Primary Keys to be inserted here if is nothing.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing


            '----------------------------------    
            _nQuery = _
               "SELECT " & _
               "DISTCODE, BRGYCODE,	BRGYDESC, GRPCODE " & _
               "FROM [BRGYCODE] " & _
                " "

            '----------------------------------    
            If Not String.IsNullOrWhiteSpace(_mAccountNumber) Then

                _nWhere = "WHERE [DISTCODE] = @_mDistNo"
            Else
                _nWhere = Nothing
            End If

            '----------------------------------
            _mQuery = _nQuery & _nWhere


            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters
                If Not String.IsNullOrWhiteSpace(_mAccountNumber) Then .AddWithValue("@_mDistNo", _mAccountNumber)
            End With

            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub

    Public Sub _pSubSelectLBPLOGS()

    End Sub
    '    Public Sub _pSubSelectTransactions()
    '        Try
    '            Dim _nQuery As String = Nothing
    '            _nQuery = " " & _
    '" (SELECT [Security],TransDate as date2,(SELECT CONVERT(VARCHAR(20), TransDate, 100)) as 'Date', TXNREFNO as 'Transaction Number', Type as 'Transaction Type',status as Status,EMAILADDR as Email,[PAYMENTCHANNEL],[GateWayRefNo],[rawAmount],[feeAmount],[totAmount],[Via],[ACCTNO],null as Owner, null as AppDate,null as slot,null as AppID,TXNREFNO,null as Office from OnlinePaymentRefNo where Status <> 'Pending' and	EMAILADDR='" & cSessionUser._pUserID & "')  " & _
    '" UNION " & _
    '" (SELECT [Security],TransDate as date2,(SELECT CONVERT(VARCHAR(20), TransDate, 100)) as 'Date', TXNREFNO as 'Transaction Number', Type as 'Transaction Type',status as Status,EMAILADDR as Email,[PAYMENTCHANNEL],[GateWayRefNo],[rawAmount],[feeAmount],[totAmount],[Via],[ACCTNO],null as Owner, null as AppDate,null as slot,null as AppID,TXNREFNO,null as Office from OnlinePaymentRefNo where Status = 'Pending' and PAYMENTCHANNEL='OTC' and	EMAILADDR='" & cSessionUser._pUserID & "')  " & _
    '" UNION " & _
    '" (SELECT [Security],TransDate as date2,(SELECT CONVERT(VARCHAR(20), TransDate, 100)) as 'Date', TXNREFNO as 'Transaction Number', Type as 'Transaction Type',status as Status,EMAILADDR as Email,[PAYMENTCHANNEL],[GateWayRefNo],[rawAmount],[feeAmount],[totAmount],[Via],[ACCTNO],null as Owner, null as AppDate,null as slot,null as AppID,TXNREFNO,null as Office from OnlinePaymentRefNo where Status = 'Pending' and PAYMENTCHANNEL='LBP' and	EMAILADDR='" & cSessionUser._pUserID & "')      " & _
    '" UNION " & _
    '" (SELECT null,TransDate as date2,(SELECT CONVERT(VARCHAR(20),TransDate, 100)) as 'Date',	AppID as 'Transaction Number',TransType as 'Transaction Type', status as Status,	email as Email,	null,	null,	null,	null,	null,	null,	ACCTID, owner as Owner,  AppDate,slot,AppID,null,Office FROM Appointment where email='" & cSessionUser._pUserID & "')                                                                                                               " & _
    '" UNION " & _
    '" (SELECT null,ReqDate as date2,(SELECT CONVERT(VARCHAR(20), [ReqDate], 100)) as 'Date',	'N/A' as 'Transaction Number','Business Enrollment' as 'Transaction Type', status as Status,	[EMAIL2] as Email,	null,	null,	null,	null,	null,	null,	ACCTNO, [OWNER] as Owner, null,null,null,null,null FROM [" & cGlobalConnections._pSqlCxn_BPLTIMS.DataSource & "].[" & cGlobalConnections._pSqlCxn_BPLTIMS.Database & "].[dbo].[BUSDETAIL] where email2='" & cSessionUser._pUserID & "')                                                                         " & _
    '" UNION " & _
    '" (SELECT null,ReqDate as date2,(SELECT CONVERT(VARCHAR(20), [ReqDate], 100)) as 'Date',	'N/A' as 'Transaction Number','Real Property Enrollment' as 'Transaction Type',	isnull(status,'Pending') as Status,	[EMAIL2] as Email,	null,	null,	null,	null,	null,	null,	TDN, [OWNERNAME] as Owner, null,null,null,null,null	FROM [" & cGlobalConnections._pSqlCxn_RPTIMS.DataSource & "].[" & cGlobalConnections._pSqlCxn_RPTIMS.Database & "].[dbo].[RPTDETAIL] where email2='" & cSessionUser._pUserID & "')                                                                   " & _
    '" UNION " & _
    '" (SELECT null,App_Date as date2,(SELECT CONVERT(VARCHAR(20), App_Date, 100)) as 'Date',	'N/A' as 'Transaction Number','New Business Application' as 'Transaction Type',[REMARKS] as Status,	[EMAILADDR],	null,	null,	null,	null,	null,	null,	[ACCTNO], [FIRST_NAME] + ' ' + [MIDDLE_NAME] + ' ' + [LAST_NAME] as Owner, null,null,null,null,null FROM [" & cGlobalConnections._pSqlCxn_BPLTIMS.DataSource & "].[" & cGlobalConnections._pSqlCxn_BPLTIMS.Database & "].[dbo].[BUSMAST] where [EMAILADDR]='" & cSessionUser._pUserID & "')                     " & _
    '" UNION " & _
    '" (SELECT null,TransDate as date2,(SELECT CONVERT(VARCHAR(20), TransDate, 100)) as 'Date',	'N/A' as 'Transaction Number','Declaration of New Property' as 'Transaction Type', [REMARKS] as Status,	userID,	null,	null,	null,	null,	null,	null,	[PropId], [OwnerName] as Owner, null,null,null,null,null FROM [" & cGlobalConnections._pSqlCxn_RPTIMS.DataSource & "].[" & cGlobalConnections._pSqlCxn_RPTIMS.Database & "].[dbo].[RPTASMastNew] where userID='" & cSessionUser._pUserID & "' AND Remarks <> NULL)                                               " & _
    '"  order by date2 desc,[Transaction Number] desc                                                                                                                                                                                                                                                                                                                                                                    "



    '            _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
    '            With _mSqlCommand.Parameters
    '            End With
    '        Catch ex As Exception

    '        End Try
    '    End Sub

    Public Sub _pSubSelectHistory()
        Try
            Dim _nQuery As String = Nothing
            _nQuery = ""
            _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            With _mSqlCommand.Parameters
            End With
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubSelectSample()
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "SELECT UserType,Department FROM [Department]"
            _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            With _mSqlCommand.Parameters
            End With
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubSelectPaymentHistory(EMAILADDR)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = " SELECT FORMAT(totAmount, 'N2') as TotalAmount,* from [OnlinePaymentRefNo] where EMAILADDR =  '" & EMAILADDR & "' and totamount IS NOT NULL and gatewayrefno is not null"
            _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            With _mSqlCommand.Parameters
            End With
        Catch ex As Exception

        End Try
    End Sub
    Public Sub _pSubSelectBPAccount()
        Try
            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            '----------------------------------          
            _nQuery = _
                  "select * from BUSMAST  "


            '----------------------------------    
            '_nWhere = "WHERE pd.emp_no IN (SELECT [emp_no] FROM [leave_ledger]) " & _
            '          "and dep.BMMS_DeptCode <>''  "


            If Not String.IsNullOrWhiteSpace(_mEmail) Then
                _nWhere += "where [EMAILADDR] = @_mEmail "
            End If

            If Not String.IsNullOrWhiteSpace(_mRemarks) Then
                _nWhere += " and [Remarks] = @_mRemarks and isnull(isPosted,0) = 0 "
            End If

            '----------------------------------
            _mQuery = _nQuery & _nWhere

            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters

                If Not String.IsNullOrWhiteSpace(_mEmail) Then .AddWithValue("@_mEmail", _mEmail)
                If Not String.IsNullOrWhiteSpace(_mRemarks) Then .AddWithValue("@_mRemarks", _mRemarks)

            End With

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub


    Public Sub _pSubUpdatePaymentRemarks(TXNREFNO As String, security As String, gatewayrefno As String)
        Try

            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            _nQuery = "update OnlinePAymentRefNo set" _
                                 & " Status='Reviewed For O.R./Permit Release'" _
                                 & ",GateWayRefNo='" & gatewayrefno & "'" _
                                 & ",Security='" & security & "'" _
                                 & " where TXNREFNO='" & TXNREFNO & "' and Status='Paid/For Treasury Verification'"

            _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            _mSqlCommand.ExecuteNonQuery()

        Catch ex As Exception

        End Try
    End Sub
    Public Sub _pSubSelectIdNo()

        Try
            '----------------------------------
            'TODO: Perform Checking of Primary Keys to be inserted here if is nothing.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            '----------------------------------  
            '   @ Modified 20170103


            _nQuery = " SELECT * from [OnlinePaymentRefNo] where strDate =  (SELECT CONVERT(VARCHAR(6),GETDATE(),12))"

            '_nQuery = " SELECT TOP 1 CONVERT(VARCHAR(6),GETDATE(),12) AS SvrDate,max(TXNREFNO) TXNREFNO   from [OnlinePaymentRefNo] "
            '_nWhere = "where Left(ReferenceNumber, 6) =  (SELECT CONVERT(VARCHAR(6),GETDATE(),12)) "

            '----------------------------------
            _mQuery = _nQuery & _nWhere

            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters

            End With

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubCheckIfTransactionExists(ByVal ACCTNO As String, ByVal TYPE As String, ByVal totAmount As String, ByRef Found As Boolean)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = " SELECT * from [OnlinePaymentRefNo] where " & _
                      " ACCTNO =  '" & ACCTNO & "' and " & _
                      " TYPE =  '" & TYPE & "' and " & _
                      " totAmount =  '" & totAmount & "' and " & _
                      " STATUSID =  'SUCCESS'"

            _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            With _mSqlCommand.Parameters
            End With

            Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
                With _nSqlDataReader
                    If _nSqlDataReader.HasRows Then
                        Found = True
                    Else
                        Found = False
                    End If
                End With
                _nSqlDataReader.Close()
            End Using


        Catch ex As Exception

        End Try
    End Sub
    Public Sub _pSubSelectPaidAccount(txnrefno)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = " SELECT * from [OnlinePaymentRefNo] where TXNREFNO =  '" & txnrefno & "'"
            _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            With _mSqlCommand.Parameters
            End With
        Catch ex As Exception

        End Try
    End Sub

    'Public Sub _pSubGetPaidAccountNo(txnrefno)
    '    Try
    '        '----------------------------------       
    '        _pSubSelectPaidAccount(txnrefno)

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader

    '            '----------------------------------
    '            'Indexes
    '            With _nSqlDataReader
    '                Dim _iTXNREFNO As Integer = .GetOrdinal("TXNREFNO")
    '                Dim _iACCTNO As Integer = .GetOrdinal("ACCTNO")
    '                Dim _ictr As Integer = 0

    '                '----------------------------------
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes

    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            _mBPAccountNumber = ._pReturnString(_nSqlDataReader(_iACCTNO))
    '                        Loop
    '                    End If

    '                End With
    '            End With

    '            _nSqlDataReader.Close()
    '        End Using

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Public Sub GetCredentials_PayMaya(ByRef PK As String, ByRef SK As String)
    '    Try
    '        '----------------------------------                 
    '        Dim _nQuery As String = Nothing
    '        _nQuery = " SELECT * from [OnlinePaymentSetup] where Code =  'PAYMAYA'"
    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
    '        With _mSqlCommand.Parameters
    '        End With

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
    '            With _nSqlDataReader
    '                Dim _iPK As Integer = .GetOrdinal("PublicApiKey")
    '                Dim _iSK As Integer = .GetOrdinal("SecretApiKey")
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes
    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            PK = ._pReturnString(_nSqlDataReader(_iPK))
    '                            SK = ._pReturnString(_nSqlDataReader(_iSK))
    '                        Loop
    '                    End If

    '                End With
    '            End With

    '            _nSqlDataReader.Close()
    '        End Using

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Public Function Get_DateExpire_BP(ByVal BIN As String) As String
    '    Try
    '        '----------------------------------                 
    '        Dim _nQuery As String = Nothing
    '        Dim DE As String = Nothing
    '        _nQuery = " SELECT top 1 ISNULL(DATE_EXPIRE, convert(varchar, getdate(), 101))DATE_EXPIRE from [BILLINGTEMP] where ACCTNO =  '" & BIN & "'  and IsRegBill='1'"
    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
    '        With _mSqlCommand.Parameters
    '        End With

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
    '            With _nSqlDataReader
    '                Dim _iDE As Integer = .GetOrdinal("DATE_EXPIRE")

    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes
    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            DE = ._pReturnString(_nSqlDataReader(_iDE))
    '                        Loop
    '                    End If
    '                End With
    '            End With
    '            _nSqlDataReader.Close()
    '        End Using

    '        Return DE
    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Function

    'Public Function Get_DateExpire_RP(ByVal AssessNo As String) As String
    '    Try
    '        '----------------------------------                 
    '        Dim _nQuery As String = Nothing
    '        Dim DE As String = Nothing
    '        _nQuery = " SELECT top 1 ISNULL(convert(varchar,[ValidDate],101), getdate())[ValidDate] from [AssessFromBilling] where [AssessNo] =  '" & AssessNo & "'  and [IsfrNewBill]='1'"
    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
    '        With _mSqlCommand.Parameters
    '        End With

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
    '            With _nSqlDataReader
    '                Dim _iDE As Integer = .GetOrdinal("ValidDate")

    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes
    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            DE = ._pReturnString(_nSqlDataReader(_iDE))
    '                        Loop
    '                    End If
    '                End With
    '            End With
    '            _nSqlDataReader.Close()
    '        End Using

    '        Return DE
    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Function

    'Public Sub _pSubGetACCTNO(ByVal txnrefno As String, ByRef ACCTNO As String)
    '    Try
    '        '----------------------------------       
    '        Dim _nQuery As String = Nothing
    '        _nQuery = " SELECT * from [OnlinePaymentRefNo] where TXNREFNO =  '" & txnrefno & "' and emailaddr='" & cSessionUser._pUserID & "'"
    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
    '            With _nSqlDataReader
    '                Dim _iTXNREFNO As Integer = .GetOrdinal("TXNREFNO")
    '                Dim _iACCTNO As Integer = .GetOrdinal("ACCTNO")
    '                Dim _ictr As Integer = 0

    '                '----------------------------------
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes

    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            ACCTNO = ._pReturnString(_nSqlDataReader(_iACCTNO))
    '                        Loop
    '                    End If

    '                End With
    '            End With

    '            _nSqlDataReader.Close()
    '        End Using

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Public Sub _pSubGetEpsErrorCode(ByVal ErrorCode As String, ByRef Description As String)
    '    Try
    '        '----------------------------------       
    '        Dim _nQuery As String = Nothing
    '        _nQuery = " SELECT * from [LBP_EpsErrorCode] where ErrorCode =  '" & ErrorCode & "'"
    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
    '            With _nSqlDataReader
    '                Dim _iDescription As Integer = .GetOrdinal("Description")
    '                '----------------------------------
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes

    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            Description = ._pReturnString(_nSqlDataReader(_iDescription))
    '                            Description = Description.Replace(vbCr, "")
    '                        Loop
    '                    Else
    '                        Description = Nothing
    '                    End If

    '                End With
    '            End With

    '            _nSqlDataReader.Close()
    '        End Using

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Public Sub _pSubGetDate(ByRef getdate As String)
    '    Try
    '        '----------------------------------       
    '        Dim _nQuery As String = Nothing
    '        _nQuery = "select getdate() as 'getdate'"
    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
    '            With _nSqlDataReader
    '                Dim _igetdate As Integer = .GetOrdinal("getdate")
    '                '----------------------------------
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes

    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            getdate = ._pReturnString(_nSqlDataReader(_igetdate))
    '                        Loop
    '                    End If

    '                End With
    '            End With

    '            _nSqlDataReader.Close()
    '        End Using

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub



    'Public Sub _pSubGetPayorInfo(ByVal TXNREFNO As String, ByRef EmailADDR As String, ByRef PAYMENTCHANNEL As String, ByRef PayorName As String, ByRef ACCTNO As String, Optional ByRef Type As String = Nothing)
    '    Try
    '        '----------------------------------       
    '        Dim _nQuery As String = Nothing
    '        _nQuery = "SELECT Type,EMAILADDR,ACCTNO,PAYMENTCHANNEL,Registered.FirstName + ' ' + Registered.MiddleName + ' ' + Registered.LastName as 'PayorName' FROM OnlinePaymentRefno INNER JOIN Registered ON OnlinePaymentRefno.EMAILADDR = Registered.UserID where OnlinePaymentRefno.TXNREFNO=  '" & TXNREFNO & "'"
    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
    '            With _nSqlDataReader
    '                Dim _iEMAILADDR As Integer = .GetOrdinal("EMAILADDR")
    '                Dim _iPAYMENTCHANNEL As Integer = .GetOrdinal("PAYMENTCHANNEL")
    '                Dim _iPayorName As Integer = .GetOrdinal("PayorName")
    '                Dim _iACCTNO As Integer = .GetOrdinal("ACCTNO")
    '                Dim _iType As Integer = .GetOrdinal("Type")
    '                '----------------------------------
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes

    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            EmailADDR = ._pReturnString(_nSqlDataReader(_iEMAILADDR))
    '                            PAYMENTCHANNEL = ._pReturnString(_nSqlDataReader(_iPAYMENTCHANNEL))
    '                            PayorName = ._pReturnString(_nSqlDataReader(_iPayorName))
    '                            ACCTNO = ._pReturnString(_nSqlDataReader(_iACCTNO))
    '                            Type = ._pReturnString(_nSqlDataReader(_iType))
    '                        Loop
    '                    End If

    '                End With
    '            End With

    '            _nSqlDataReader.Close()
    '        End Using

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Public Sub _pSubGetEmail(ByVal txnrefno As String, ByRef Email As String)
    '    Try
    '        '----------------------------------       
    '        Dim _nQuery As String = Nothing
    '        _nQuery = " SELECT * from [OnlinePaymentRefNo] where TXNREFNO =  '" & txnrefno & "'"
    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
    '            With _nSqlDataReader
    '                Dim _iTXNREFNO As Integer = .GetOrdinal("TXNREFNO")
    '                Dim _iEMAILADDR As Integer = .GetOrdinal("EMAILADDR")
    '                Dim _ictr As Integer = 0

    '                '----------------------------------
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes

    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            Email = ._pReturnString(_nSqlDataReader(_iEMAILADDR))
    '                        Loop
    '                    End If

    '                End With
    '            End With

    '            _nSqlDataReader.Close()
    '        End Using

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Public Function _pSubGetGatewayStatus(ByVal GatewayCode As String) As Boolean
    '    Try
    '        '----------------------------------       
    '        Dim Status As Boolean
    '        Dim _nQuery As String = Nothing
    '        _nQuery = " SELECT * from [OnlinePaymentSetup] where CODE =  '" & GatewayCode & "'"
    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
    '            With _nSqlDataReader
    '                Dim _iStatus As Integer = .GetOrdinal("Status")
    '                '----------------------------------
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes
    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            Status = ._pReturnString(_nSqlDataReader(_iStatus))
    '                        Loop
    '                    Else
    '                        Status = 0
    '                    End If
    '                End With
    '            End With
    '            '  _nSqlDataReader.Close()
    '        End Using
    '        Return Status
    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Function
    Public Function GetHashMD5(theInput As String) As String
        Using hasher As MD5 = MD5.Create()
            Dim dbytes As Byte() = hasher.ComputeHash(System.Text.Encoding.UTF8.GetBytes(theInput))
            Dim sBuilder As New StringBuilder()
            For n As Integer = 0 To dbytes.Length - 1
                sBuilder.Append(dbytes(n).ToString("X2"))
            Next n
            Return sBuilder.ToString()
        End Using
    End Function
    Public Function GenerateSHA256String(ByVal inputString) As String
        Dim sha256 As SHA256 = SHA256Managed.Create()
        Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(inputString)
        Dim hash As Byte() = sha256.ComputeHash(bytes)
        Dim stringBuilder As New StringBuilder()

        For i As Integer = 0 To hash.Length - 1
            stringBuilder.Append(hash(i).ToString("X2"))
        Next

        Return stringBuilder.ToString()
    End Function

    'Public Function _pSubGetGatewayCredentials(ByVal GatewayCode As String)
    '    Try
    '        '----------------------------------       
    '        gw_Status = Nothing
    '        gw_MerchantCode = Nothing
    '        gw_TestURL = Nothing
    '        gw_TestURL = Nothing
    '        gw_ProdURL = Nothing
    '        gw_ProdURL = Nothing
    '        gw_UserName = Nothing
    '        gw_Password = Nothing
    '        gw_SecretKey = Nothing
    '        gw_apiKey = Nothing
    '        gw_SecretApiKey = Nothing
    '        gw_PublicApiKey = Nothing
    '        gw_DateNow = Nothing
    '        Dim Status As Boolean
    '        Dim _nQuery As String = Nothing
    '        _nQuery = " SELECT *, GETDATE() as 'DateNow' from [OnlinePaymentSetup] where CODE =  '" & GatewayCode & "'"
    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
    '            With _nSqlDataReader
    '                Dim _iStatus As Integer = .GetOrdinal("Status")
    '                Dim _iMerchantCode As Integer = .GetOrdinal("MerchantCode")
    '                Dim _iTestURL As Integer = .GetOrdinal("TestURL")
    '                Dim _iTestURL_Return As Integer = .GetOrdinal("TestURL_Return")
    '                Dim _iProdURL As Integer = .GetOrdinal("ProdURL")
    '                Dim _iProdURL_Return As Integer = .GetOrdinal("ProdURL_Return")
    '                Dim _iUsername As Integer = .GetOrdinal("Username")
    '                Dim _iPassword As Integer = .GetOrdinal("Password")
    '                Dim _iSecretKey As Integer = .GetOrdinal("SecretKey")
    '                Dim _iapiKey As Integer = .GetOrdinal("apiKey")
    '                Dim _iSecretApiKey As Integer = .GetOrdinal("SecretApiKey")
    '                Dim _iPublicApiKey As Integer = .GetOrdinal("PublicApiKey")
    '                Dim _iDateNow As Integer = .GetOrdinal("DateNow")
    '                Dim _iLIVE As Integer = .GetOrdinal("LIVE")

    '                '----------------------------------
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes
    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            cDalPayment.gw_Status = ._pReturnString(_nSqlDataReader(_iStatus))
    '                            cDalPayment.gw_MerchantCode = ._pReturnString(_nSqlDataReader(_iMerchantCode))
    '                            cDalPayment.gw_TestURL = ._pReturnString(_nSqlDataReader(_iTestURL))
    '                            cDalPayment.gw_TestURL_Return = ._pReturnString(_nSqlDataReader(_iTestURL_Return))
    '                            cDalPayment.gw_ProdURL = ._pReturnString(_nSqlDataReader(_iProdURL))
    '                            cDalPayment.gw_ProdURL_Return = ._pReturnString(_nSqlDataReader(_iProdURL_Return))
    '                            cDalPayment.gw_UserName = ._pReturnString(_nSqlDataReader(_iUsername))
    '                            cDalPayment.gw_Password = ._pReturnString(_nSqlDataReader(_iPassword))
    '                            cDalPayment.gw_SecretKey = ._pReturnString(_nSqlDataReader(_iSecretKey))
    '                            cDalPayment.gw_apiKey = ._pReturnString(_nSqlDataReader(_iapiKey))
    '                            cDalPayment.gw_SecretApiKey = ._pReturnString(_nSqlDataReader(_iSecretApiKey))
    '                            cDalPayment.gw_PublicApiKey = ._pReturnString(_nSqlDataReader(_iPublicApiKey))
    '                            cDalPayment.gw_DateNow = ._pReturnString(_nSqlDataReader(_iDateNow))
    '                            cDalPayment.gw_LIVE = ._pReturnInteger(_nSqlDataReader(_iLIVE))
    '                        Loop
    '                    Else
    '                        Status = 0
    '                    End If
    '                End With
    '            End With
    '            _nSqlDataReader.Close()
    '        End Using

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Function


    Public Sub _pSubSelectPaidAccountReview(acctno, status)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = " SELECT * from [OnlinePaymentRefNo] where ACCTNO =  '" & acctno & "' and Status Like '%Success%'"
            _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            With _mSqlCommand.Parameters
            End With
        Catch ex As Exception

        End Try
    End Sub

    'Public Sub _pSubGetPaidAccountReviewDetails(acctno, status)
    '    Try
    '        '----------------------------------       
    '        _pSubSelectPaidAccountReview(acctno, status)

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader

    '            '----------------------------------
    '            'Indexes
    '            With _nSqlDataReader

    '                Dim _iEMAILADDR As String = .GetOrdinal("EMAILADDR")
    '                Dim _iPAYMENTCHANNEL As String = .GetOrdinal("PAYMENTCHANNEL")
    '                Dim _iSTATUS As String = .GetOrdinal("Status")
    '                Dim _IGATEWAYREFNO As String = .GetOrdinal("GateWayRefNo")
    '                Dim _iSecurity As String = .GetOrdinal("Security")
    '                Dim _iTrxdate As String = .GetOrdinal("TRXDATE")
    '                Dim _iTXNREFNO As Integer = .GetOrdinal("TXNREFNO")


    '                Dim _iACCTNO As Integer = .GetOrdinal("ACCTNO")

    '                Dim _ictr As Integer = 0

    '                '----------------------------------
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes

    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            _mEMAILADDR = ._pReturnString(_nSqlDataReader(_iEMAILADDR))
    '                            _mPaymentChannel = ._pReturnString(_nSqlDataReader(_iPAYMENTCHANNEL))
    '                            _mSTatus = ._pReturnString(_nSqlDataReader(_iSTATUS))
    '                            _mGateWyRefNo = ._pReturnString(_nSqlDataReader(_IGATEWAYREFNO))
    '                            _mSecurity = ._pReturnString(_nSqlDataReader(_iSecurity))
    '                            _mtrxdate = ._pReturnString(_nSqlDataReader(_iTrxdate))
    '                            _mACCTNO = ._pReturnString(_nSqlDataReader(_iACCTNO))
    '                            _mTXNREFNO = ._pReturnString(_nSqlDataReader(_iTXNREFNO))
    '                        Loop
    '                    End If

    '                End With
    '            End With

    '            _nSqlDataReader.Close()
    '        End Using

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Public Sub _pSubSelectPaymentExist()
        Try
            '----------------------------------
            'TODO: Perform Checking of Primary Keys to be inserted here if is nothing.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing


            '----------------------------------    
            _nQuery = _
               "select ReferenceNumber,ReferenceDate,BPAccountNumber,TypeofPayment,BankName BankCode, " & _
               "(select BANKDESC from BANKCODE where BANKCODE=PaymentPosting.BankName)BankName, " & _
               "BankAddress,AccountNumber,CardNumber,AccountName,ExpiryMonth,ExpiryYear,CVV,TotalAmount " & _
               "from PaymentPosting " & _
                " "

            '----------------------------------    
            If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then

                _nWhere = "WHERE [BPAccountNumber] = @_mBPAccountNumber"
            Else
                _nWhere = Nothing
            End If

            '----------------------------------
            _mQuery = _nQuery & _nWhere


            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters
                If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)
            End With

            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub
    Public Sub _pSubSelectParameter1()
        Try
            '----------------------------------
            'TODO: Perform Checking of Primary Keys to be inserted here if is nothing.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing


            '----------------------------------    
            '_nQuery = _
            '   "select PaymentPosting.ReferenceNumber,PaymentPosting.ReferenceDate,ACCTNO,isnull(FIRST_NAME,'') + ' ' + ISNULL(MIDDLE_NAME,'')  + ' '  + isnull(LAST_NAME,'')owner,STATCODE, (select OWNDESC from OWNCODE where OWNCODE=busmast.OWNCODE)OWNDESC,COMMNAME,COMMADDR  from BUSMAST " & _
            '   "Inner Join PaymentPosting on  BUSMAST.ACCTNO=PaymentPosting.BPAccountNumber " & _
            '   " " & _
            '    " "

            _nQuery = _
               "select ACCTNO,isnull(FIRST_NAME,'') + ' ' + ISNULL(MIDDLE_NAME,'')  + ' '  + isnull(LAST_NAME,'')owner,STATCODE, (select OWNDESC from OWNCODE where OWNCODE=busmast.OWNCODE)OWNDESC,COMMNAME,COMMADDR  from BUSMAST " & _
               " " & _
               " " & _
                " "

            '----------------------------------    
            If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then

                _nWhere = "WHERE [acctno] = @_mBPAccountNumber"
            Else
                _nWhere = Nothing
            End If

            '----------------------------------
            _mQuery = _nQuery & _nWhere


            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters
                If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)
            End With

            Dim _nDR As SqlDataReader = _mSqlDataReader

            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub
    Public Sub _pSubSelectParameter1_2()
        Try
            '----------------------------------
            'TODO: Perform Checking of Primary Keys to be inserted here if is nothing.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing


            '----------------------------------    
            '_nQuery = _
            '   "select PaymentPosting.ReferenceNumber,PaymentPosting.ReferenceDate,ACCTNO,isnull(FIRST_NAME,'') + ' ' + ISNULL(MIDDLE_NAME,'')  + ' '  + isnull(LAST_NAME,'')owner,STATCODE, (select OWNDESC from OWNCODE where OWNCODE=busmast.OWNCODE)OWNDESC,COMMNAME,COMMADDR  from BUSMAST " & _
            '   "Inner Join PaymentPosting on  BUSMAST.ACCTNO=PaymentPosting.BPAccountNumber " & _
            '   " " & _
            '    " "

            _nQuery = _
               "select ACCTNO,isnull(FIRST_NAME,'') + ' ' + ISNULL(MIDDLE_NAME,'')  + ' '  + isnull(LAST_NAME,'')owner,STATCODE, (select OWNDESC from OWNCODE where OWNCODE=busmast.OWNCODE)OWNDESC,COMMNAME,COMMADDR  from BUSMAST " & _
               " " & _
               " " & _
                " "

            '----------------------------------    
            If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then

                _nWhere = "WHERE [acctno] = @_mBPAccountNumber"
            Else
                _nWhere = Nothing
            End If

            '----------------------------------
            _mQuery = _nQuery & _nWhere


            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters
                If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)
            End With

            Dim _nDR As SqlDataReader = _mSqlDataReader

            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub
    'Public Sub _pSubSelectParameter2()
    '    Try
    '        '----------------------------------
    '        'TODO: Perform Checking of Primary Keys to be inserted here if is nothing.

    '        '----------------------------------
    '        Dim _nQuery As String = Nothing
    '        Dim _nWhere As String = Nothing

    '        'BPLTAS LIVE
    '        Dim _nClBP As New cDalGlobalConnectionsDefault
    '        _nClBP._pCxn = cGlobalConnections._pSqlCxn_CR
    '        _nClBP._pSetupGlobalConnectionsDatabases = "BPLTAS"
    '        _nClBP._pSubRecordSelectSpecific()

    '        Dim _nLiveServerName As String = _nClBP._pDBDataSource
    '        Dim _nLiveDatabaseName As String = _nClBP._pDBInitialCatalog


    '        '----------------------------------    
    '        _nQuery = _
    '                        "select *,(Select OWNDESC from [" & _nLiveServerName & "].[" & _nLiveDatabaseName & "].dbo.[OWNCODE] " & _
    '        "WHERE  OWNCODE COLLATE DATABASE_DEFAULT = (select OWNCODE from vw_C_BUSMAST where acctno =  @_mBPAccountNumber)) as OWNDESC1 from vw_C_BUSMAST "
    '        '"select *,(Select OWNDESC from [" & _nLiveServerName & "].[" & _nLiveDatabaseName & "].dbo.[OWNCODE] " & _
    '        ' "WHERE  OWNCODE COLLATE DATABASE_DEFAULT = (select OWNCODE from vw_C_BUSMAST WHERE [ACCTNO]  = @_mBPAccountNumber)) as OWNDESC1) from vw_C_BUSMAST "





    '        '----------------------------------    
    '        If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then

    '            _nWhere = " WHERE [ACCTNO] = @_mBPAccountNumber"
    '        Else
    '            _nWhere = Nothing
    '        End If

    '        '----------------------------------
    '        _mQuery = _nQuery & _nWhere


    '        '----------------------------------
    '        _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

    '        With _mSqlCommand.Parameters
    '            If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)
    '        End With

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try

    'End Sub
    Public Sub _pSubSelectCourierDetails()
        Try
            '----------------------------------
            'TODO: Perform Checking of Primary Keys to be inserted here if is nothing.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing


            '----------------------------------    
            _nQuery = _
               "select * from Courier " & _
               " " & _
                " "

            '----------------------------------    
            If Not String.IsNullOrWhiteSpace(_mCourierCode) Then

                _nWhere = "WHERE [Code] = @_mCourierCode"
            Else
                _nWhere = Nothing
            End If

            '----------------------------------
            _mQuery = _nQuery & _nWhere


            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters
                If Not String.IsNullOrWhiteSpace(_mCourierCode) Then .AddWithValue("@_mCourierCode", _mCourierCode)
            End With

            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub
    Public Sub _pSubSelectPaymentDetails()
        Try
            '----------------------------------
            'TODO: Perform Checking of Primary Keys to be inserted here if is nothing.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing


            '----------------------------------    
            _nQuery = _
               "select * from PaymentPosting " & _
               " " & _
                " "

            '----------------------------------    
            If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then

                _nWhere = "WHERE [BPAccountNumber] = @_mBPAccountNumber"
            Else
                _nWhere = Nothing
                Exit Sub
            End If

            '----------------------------------
            _mQuery = _nQuery & _nWhere


            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters
                If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)
            End With

            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub
    'Public Sub _pSubSelectBillingReport()
    '    Try
    '        '----------------------------------
    '        'TODO: Perform Checking of Primary Keys to be inserted here if is nothing.

    '        '----------------------------------
    '        Dim _nQuery As String = Nothing
    '        Dim _nWhere As String = Nothing


    '        '----------------------------------    
    '        _nQuery = _
    '           "SELECT * FROM BILLINGTEMP " & _
    '            " "

    '        '----------------------------------    
    '        If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then

    '            _nWhere = "where Acctno= @_mBPAccountNumber "
    '        Else
    '            _nWhere = Nothing
    '        End If

    '        '----------------------------------
    '        _mQuery = _nQuery & _nWhere


    '        '----------------------------------
    '        _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

    '        With _mSqlCommand.Parameters
    '            If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)
    '        End With

    '        '----------------------------------
    '    Catch ex As Exception
    '        cDalLogEvent._pSubLogError(ex)
    '    End Try

    'End Sub
    Public Sub _pSubSelectPaymentListReport()
        Try
            '----------------------------------
            'TODO: Perform Checking of Primary Keys to be inserted here if is nothing.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing


            '----------------------------------    
            _nQuery = _
               "select acctno,ReferenceNumber,Fullname from vw_PaymentPosting  " & _
                " "

            '----------------------------------    

            _nWhere = "where remarks ='Reviewed For O.R./Permit Release'"



            '----------------------------------
            _mQuery = _nQuery & _nWhere


            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            'With _mSqlCommand.Parameters
            '    If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)
            'End With

            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub
    Public Sub _pSubSelect_Payment_List()
        Try
            '----------------------------------
            'TODO: Perform Checking of Primary Keys to be inserted here if is nothing.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing


            '----------------------------------    
            _nQuery = _
              "Select ReferenceNumber, " & _
              "ReferenceDate,BPAccountNumber,COMMNAME,TypeofPayment,BankName BankCode, " & _
              "(select bankdesc from BANKCODE where bankcode = PaymentPosting.bankname)BankDesc, " & _
              "BankAddress,AccountNumber,CardNumber,AccountName,TotalAmount,busmast.EMAILADDR Email,PickupDelivery " & _
              "FROM PaymentPosting " & _
              "Inner Join  busmast on busmast.acctno = PaymentPosting.BPAccountNumber "
            '----------------------------------    
            If Not String.IsNullOrWhiteSpace(_mRemarks) Then

                _nWhere = "WHERE busmast.remarks = @_mRemarks"
            Else
                _nWhere = Nothing
            End If

            If Not String.IsNullOrWhiteSpace(_mFilter) Then
                If _mFilter = "All" Then
                    '_nWhere = _nWhere
                ElseIf _mFilter = "S" Then
                    _nWhere = _nWhere & " and PaymentStatus = 'S'"
                ElseIf _mFilter = "P" Then
                    _nWhere = _nWhere & " and PaymentStatus = 'P'"
                End If
            End If

            '----------------------------------
            _mQuery = _nQuery & _nWhere & " order by ReferenceNumber  "


            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters
                If Not String.IsNullOrWhiteSpace(_mRemarks) Then .AddWithValue("@_mRemarks", _mRemarks)
            End With

            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub
    Public Sub _pSubSelect_Payment_List_Search()
        Try
            '----------------------------------
            'TODO: Perform Checking of Primary Keys to be inserted here if is nothing.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing


            '----------------------------------    
            _nQuery = _
              "Select ReferenceNumber, " & _
              "ReferenceDate,BPAccountNumber,TypeofPayment,COMMNAME,BankName BankCode, " & _
              "(select bankdesc from BANKCODE where bankcode = PaymentPosting.bankname)BankDesc, " & _
              "BankAddress,AccountNumber,CardNumber,AccountName,TotalAmount,busmast.EMAILADDR Email,PickupDelivery " & _
              "FROM PaymentPosting " & _
              "Inner Join  busmast on busmast.acctno = PaymentPosting.BPAccountNumber "
            '----------------------------------    
            If Not String.IsNullOrWhiteSpace(_mRemarks) Then

                _nWhere = "WHERE busmast.remarks = @_mRemarks "
            Else
                _nWhere = Nothing
            End If

            If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then

                _nWhere = _nWhere & "and  BPAccountNumber like   '%" & _mBPAccountNumber & "%' "
            Else

            End If

            If Not String.IsNullOrWhiteSpace(_mFilter) Then
                If _mFilter = "All" Then
                    '_nWhere = _nWhere
                ElseIf _mFilter = "S" Then
                    _nWhere = _nWhere & " and PaymentStatus = 'S'"
                ElseIf _mFilter = "P" Then
                    _nWhere = _nWhere & " and PaymentStatus = 'P'"
                End If
            End If

            '----------------------------------
            _mQuery = _nQuery & _nWhere & " order by ReferenceNumber  "


            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters
                If Not String.IsNullOrWhiteSpace(_mRemarks) Then .AddWithValue("@_mRemarks", _mRemarks)
                'If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)
            End With

            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub

    Public Sub _pSubInsertPaymentPosting()
        Try
            Dim _nQuery As String = Nothing ' For For Requirements module 20161114


            _nQuery = _
             "INSERT INTO " & _
              "[PaymentPosting] " & _
              "(" & _
              IIf(String.IsNullOrWhiteSpace(_mReferenceNumber), "", " [ReferenceNumber]") & _
              IIf(String.IsNullOrWhiteSpace(_mReferenceDate), "", ", [ReferenceDate]") & _
              IIf(String.IsNullOrWhiteSpace(_mBPAccountNumber), "", ", [BPAccountNumber]") & _
              IIf(String.IsNullOrWhiteSpace(_mTypeofPayment), "", ", [TypeofPayment]") & _
              IIf(String.IsNullOrWhiteSpace(_mBankName), "", ", [BankName]") & _
              IIf(String.IsNullOrWhiteSpace(_mBankAddress), "", ", [BankAddress]") & _
              IIf(String.IsNullOrWhiteSpace(_mAccountNumber), "", ", [AccountNumber]") & _
              IIf(String.IsNullOrWhiteSpace(_mCardNumber), "", ", [CardNumber]") & _
              IIf(String.IsNullOrWhiteSpace(_mAccountName), "", ", [AccountName]") & _
              IIf(String.IsNullOrWhiteSpace(_mExpiryMonth), "", ", [ExpiryMonth]") & _
              IIf(String.IsNullOrWhiteSpace(_mExpiryYear), "", ", [ExpiryYear]") & _
              IIf(String.IsNullOrWhiteSpace(_mCardCVV), "", ", [CVV]") & _
              IIf(String.IsNullOrWhiteSpace(_mTOPamount), "", ", [TotalAmount]") & _
              IIf(String.IsNullOrWhiteSpace(_mDragonRefNo), "", ", [DragonRefNo]") & _
              IIf(String.IsNullOrWhiteSpace(_mPickupDelivery), "", ", [PickupDelivery]") & _
              IIf(String.IsNullOrWhiteSpace(_mPaymentChannel), "", ", [PaymentChannel]") & _
              IIf(String.IsNullOrWhiteSpace(_mPaymentStatus), "", ", [PaymentStatus]") & _
              ") " & _
              "VALUES " & _
              "(" & _
              IIf(String.IsNullOrWhiteSpace(_mReferenceNumber), "", " @_mReferenceNumber") & _
              IIf(String.IsNullOrWhiteSpace(_mReferenceDate), "", ", @_mReferenceDate") & _
              IIf(String.IsNullOrWhiteSpace(_mBPAccountNumber), "", ", @_mBPAccountNumber") & _
              IIf(String.IsNullOrWhiteSpace(_mTypeofPayment), "", ", @_mTypeofPayment") & _
              IIf(String.IsNullOrWhiteSpace(_mBankName), "", ", @_mBankName") & _
              IIf(String.IsNullOrWhiteSpace(_mBankAddress), "", ", @_mBankAddress") & _
              IIf(String.IsNullOrWhiteSpace(_mAccountNumber), "", ", @_mAccountNumber") & _
              IIf(String.IsNullOrWhiteSpace(_mCardNumber), "", ", @_mCardNumber") & _
              IIf(String.IsNullOrWhiteSpace(_mAccountName), "", ", @_mAccountName") & _
              IIf(String.IsNullOrWhiteSpace(_mExpiryMonth), "", ", @_mExpiryMonth") & _
              IIf(String.IsNullOrWhiteSpace(_mExpiryYear), "", ", @_mExpiryYear") & _
              IIf(String.IsNullOrWhiteSpace(_mCardCVV), "", ", @_mCardCVV") & _
              IIf(String.IsNullOrWhiteSpace(_mTOPamount), "", ", @_mTOPamount") & _
              IIf(String.IsNullOrWhiteSpace(_mDragonRefNo), "", ", @_mDragonRefNo") & _
              IIf(String.IsNullOrWhiteSpace(_mPickupDelivery), "", ", @_mPickupDelivery") & _
              IIf(String.IsNullOrWhiteSpace(_mPaymentChannel), "", ", @_mPaymentChannel") & _
              IIf(String.IsNullOrWhiteSpace(_mPaymentStatus), "", ", @_mPaymentStatus") & _
              ") "

            _mQuery = _nQuery

            _mQuery = Replace(_mQuery, "(,", "(") ' INSERT INTO BUSMAST_WEB

            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters
                If Not String.IsNullOrWhiteSpace(_mReferenceNumber) Then .AddWithValue("@_mReferenceNumber", _mReferenceNumber)
                If Not String.IsNullOrWhiteSpace(_mReferenceDate) Then .AddWithValue("@_mReferenceDate", _mReferenceDate)
                If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)
                If Not String.IsNullOrWhiteSpace(_mTypeofPayment) Then .AddWithValue("@_mTypeofPayment", _mTypeofPayment)
                If Not String.IsNullOrWhiteSpace(_mBankName) Then .AddWithValue("@_mBankName", _mBankName)
                If Not String.IsNullOrWhiteSpace(_mBankAddress) Then .AddWithValue("@_mBankAddress", _mBankAddress)
                If Not String.IsNullOrWhiteSpace(_mAccountNumber) Then .AddWithValue("@_mAccountNumber", _mAccountNumber)
                If Not String.IsNullOrWhiteSpace(_mCardNumber) Then .AddWithValue("@_mCardNumber", _mCardNumber)
                If Not String.IsNullOrWhiteSpace(_mAccountName) Then .AddWithValue("@_mAccountName", _mAccountName)
                If Not String.IsNullOrWhiteSpace(_mExpiryMonth) Then .AddWithValue("@_mExpiryMonth", _mExpiryMonth)
                If Not String.IsNullOrWhiteSpace(_mExpiryYear) Then .AddWithValue("@_mExpiryYear", _mExpiryYear)
                If Not String.IsNullOrWhiteSpace(_mCardCVV) Then .AddWithValue("@_mCardCVV", _mCardCVV)
                If Not String.IsNullOrWhiteSpace(_mTOPamount) Then .AddWithValue("@_mTOPamount", _mTOPamount)
                If Not String.IsNullOrWhiteSpace(_mDragonRefNo) Then .AddWithValue("@_mDragonRefNo", _mDragonRefNo)
                If Not String.IsNullOrWhiteSpace(_mPickupDelivery) Then .AddWithValue("@_mPickupDelivery", _mPickupDelivery)
                If Not String.IsNullOrWhiteSpace(_mPaymentChannel) Then .AddWithValue("@_mPaymentChannel", _mPaymentChannel)
                If Not String.IsNullOrWhiteSpace(_mPaymentStatus) Then .AddWithValue("@_mPaymentStatus", _mPaymentStatus)
            End With

            _mSqlCommand.ExecuteNonQuery()
        Catch ex As Exception

        End Try

    End Sub
    Public Sub _pSubUpdate()
        Try
            '----------------------------------
            'TODO: Check Primary Keys before updating.
            'Check Conditions before updating.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            '----------------------------------
            _nQuery = _
                "UPDATE " & _
                "[PaymentPosting] " & _
                "SET " & _
                    IIf(String.IsNullOrWhiteSpace(_mTypeofPayment), "", " [TypeofPayment] = @_mTypeofPayment") & _
                    IIf(String.IsNullOrWhiteSpace(_mReferenceDate), "", ", [ReferenceDate] = @_mReferenceDate") & _
                    IIf(String.IsNullOrWhiteSpace(_mBankName), "", ", [BankName] = @_mBankName") & _
                    IIf(String.IsNullOrWhiteSpace(_mBankAddress), "", ", [BankAddress] = @_mBankAddress") & _
                    IIf(String.IsNullOrWhiteSpace(_mAccountNumber), "", ", [AccountNumber] = @_mAccountNumber") & _
                    IIf(String.IsNullOrWhiteSpace(_mCardNumber), "", ", [CardNumber] = @_mCardNumber") & _
                    IIf(String.IsNullOrWhiteSpace(_mAccountName), "", ", [AccountName] = @_mAccountName") & _
                    IIf(String.IsNullOrWhiteSpace(_mTOPamount), "", ", [TotalAmount] = @_mTOPamount") & _
                " "

            '----------------------------------
            If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then

                _nWhere = "WHERE [BPAccountNumber] = @_mBPAccountNumber "

            Else
                _nWhere = Nothing
            End If

            '----------------------------------
            'Prevent Bulk Update.
            If _nWhere = Nothing Then
                Exit Sub
            End If

            '----------------------------------
            _mQuery = _nQuery & _nWhere

            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters

                If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)

                If Not String.IsNullOrWhiteSpace(_mReferenceDate) Then .AddWithValue("@_mReferenceDate", _mReferenceDate)
                If Not String.IsNullOrWhiteSpace(_mTypeofPayment) Then .AddWithValue("@_mTypeofPayment", _mTypeofPayment)
                If Not String.IsNullOrWhiteSpace(_mBankName) Then .AddWithValue("@_mBankName", _mBankName)
                If Not String.IsNullOrWhiteSpace(_mBankAddress) Then .AddWithValue("@_mBankAddress", _mBankAddress)
                If Not String.IsNullOrWhiteSpace(_mAccountNumber) Then .AddWithValue("@_mAccountNumber", _mAccountNumber)
                If Not String.IsNullOrWhiteSpace(_mCardNumber) Then .AddWithValue("@_mCardNumber", _mCardNumber)
                If Not String.IsNullOrWhiteSpace(_mAccountName) Then .AddWithValue("@_mAccountName", _mAccountName)
                If Not String.IsNullOrWhiteSpace(_mTOPamount) Then .AddWithValue("@_mTOPamount", _mTOPamount)

            End With

            _mSqlCommand.ExecuteNonQuery()

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub
    Public Sub _pSubUpdateBillingTemp()
        Try
            '----------------------------------
            'TODO: Check Primary Keys before updating.
            'Check Conditions before updating.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            '----------------------------------
            _nQuery = _
                "UPDATE " & _
                "[BILLINGTEMP] " & _
                "SET " & _
                    IIf(String.IsNullOrWhiteSpace(_mReferenceNumber), "", " [xORNO] = @_mReferenceNumber") & _
                    IIf(String.IsNullOrWhiteSpace(_mRemarks), "", ", [Remarks] = @_mRemarks") & _
                    IIf(String.IsNullOrWhiteSpace(_misPosted), "", ", [isPosted] = @_misPosted") & _
                " "

            '----------------------------------
            If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then

                _nWhere = "WHERE [Acctno] = @_mBPAccountNumber "

            Else
                _nWhere = Nothing
            End If

            '----------------------------------
            'Prevent Bulk Update.
            If _nWhere = Nothing Then
                Exit Sub
            End If

            '----------------------------------
            _mQuery = _nQuery & _nWhere

            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters

                If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)
                If Not String.IsNullOrWhiteSpace(_mReferenceNumber) Then .AddWithValue("@_mReferenceNumber", _mReferenceNumber)
                If Not String.IsNullOrWhiteSpace(_mRemarks) Then .AddWithValue("@_mRemarks", _mRemarks)
                If Not String.IsNullOrWhiteSpace(_misPosted) Then .AddWithValue("@_misPosted", _misPosted)

            End With

            _mSqlCommand.ExecuteNonQuery()

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub
    Public Sub _pSubUpdateBillingRemarks()
        Try
            '----------------------------------
            'TODO: Check Primary Keys before updating.
            'Check Conditions before updating.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            '----------------------------------
            _nQuery = _
                "UPDATE " & _
                "[BUSMAST] " & _
                "SET " & _
                    IIf(String.IsNullOrWhiteSpace(_mRemarks), "", " [Remarks] = @_mRemarks") & _
                " "

            '----------------------------------
            If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then

                _nWhere = "WHERE [Acctno] = @_mBPAccountNumber "

            Else
                _nWhere = Nothing
            End If

            '----------------------------------
            'Prevent Bulk Update.
            If _nWhere = Nothing Then
                Exit Sub
            End If

            '----------------------------------
            _mQuery = _nQuery & _nWhere

            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters

                If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)
                If Not String.IsNullOrWhiteSpace(_mRemarks) Then .AddWithValue("@_mRemarks", _mRemarks)

            End With

            _mSqlCommand.ExecuteNonQuery()

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub
    Public Sub _pSubUpdateBillingisPosted(isPosted, REMARKS, ACCTNO)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "update [BUSMAST] set " _
                                    & " isPosted = '" & isPosted & "', " _
                                    & " REMARKS = '" & REMARKS & "' " _
                                    & " where ACCTNO='" & ACCTNO & "'"

            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub
    'Public Sub _pSubUpdateBillingisPosted() -------------orignal
    '    Try
    '        '----------------------------------
    '        'TODO: Check Primary Keys before updating.
    '        'Check Conditions before updating.

    '        '----------------------------------
    '        Dim _nQuery As String = Nothing
    '        Dim _nWhere As String = Nothing

    '        '----------------------------------
    '        _nQuery = _
    '            "UPDATE " & _
    '            "[BUSMAST] " & _
    '            "SET " & _
    '                IIf(String.IsNullOrWhiteSpace(_misPosted), "", " [isPosted] = @_misPosted, [REMARKS] = @_mRemarks") & _
    '            " "

    '        '----------------------------------
    '        If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then

    '            _nWhere = "WHERE [Acctno] = @_mBPAccountNumber "

    '        Else
    '            _nWhere = Nothing
    '        End If

    '        '----------------------------------
    '        'Prevent Bulk Update.
    '        If _nWhere = Nothing Then
    '            Exit Sub
    '        End If

    '        '----------------------------------
    '        _mQuery = _nQuery & _nWhere

    '        '----------------------------------
    '        _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

    '        With _mSqlCommand.Parameters

    '            If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)
    '            If Not String.IsNullOrWhiteSpace(_mRemarks) Then .AddWithValue("@_mRemarks", _mRemarks)
    '            If Not String.IsNullOrWhiteSpace(_misPosted) Then .AddWithValue("@_misPosted", _misPosted)

    '        End With

    '        _mSqlCommand.ExecuteNonQuery()

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Public Sub _pSubUpdateBillingisPosted_RPT(isPosted, REMARKS, ACCTNO)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "update [BUSMAST] set " _
                                    & " isPosted = '" & isPosted & "', " _
                                    & " REMARKS = '" & REMARKS & "' " _
                                    & " where ACCTNO='" & ACCTNO & "'"

            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub
    Public Sub _pSubUpdateBuslineNewSwitch()
        Try
            'added by abby 20180223
            '----------------------------------
            'TODO: Check Primary Keys before updating.
            'Check Conditions before updating.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            '----------------------------------
            _nQuery = _
                "UPDATE " & _
                "[BUSLINE] " & _
                "SET " & _
                    IIf(String.IsNullOrWhiteSpace(_misPosted), "", " [newsw] = @_mNewSwitch") & _
                " "

            '----------------------------------
            If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then

                _nWhere = "WHERE [Acctno] = @_mBPAccountNumber and isnull(newsw,0) = 0 "

            Else
                _nWhere = Nothing
            End If

            '----------------------------------
            'Prevent Bulk Update.
            If _nWhere = Nothing Then
                Exit Sub
            End If

            '----------------------------------
            _mQuery = _nQuery & _nWhere

            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters

                If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)
                If Not String.IsNullOrWhiteSpace(_misPosted) Then .AddWithValue("@_mNewSwitch", _misPosted)


            End With

            _mSqlCommand.ExecuteNonQuery()

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubUpdatePaymentVerifyBy()
        Try
            '----------------------------------
            'TODO: Check Primary Keys before updating.
            'Check Conditions before updating.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            '----------------------------------
            _nQuery = _
                "UPDATE " & _
                "[PaymentPosting] " & _
                "SET " & _
                    IIf(String.IsNullOrWhiteSpace(_mVerify), "", " [Verified_By] = @_mVerify") & _
                " "

            '----------------------------------
            If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then

                _nWhere = "WHERE [BPAccountNumber] = @_mBPAccountNumber "

            Else
                _nWhere = Nothing
            End If

            '----------------------------------
            'Prevent Bulk Update.
            If _nWhere = Nothing Then
                Exit Sub
            End If

            '----------------------------------
            _mQuery = _nQuery & _nWhere

            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters

                If Not String.IsNullOrWhiteSpace(_mBPAccountNumber) Then .AddWithValue("@_mBPAccountNumber", _mBPAccountNumber)
                If Not String.IsNullOrWhiteSpace(_mVerify) Then .AddWithValue("@_mVerify", _mVerify)

            End With

            _mSqlCommand.ExecuteNonQuery()

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    'Public Sub _pSubGetAutoReferenceNumberID()
    '    Try
    '        '----------------------------------

    '        _pSubSelectIdNo()
    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader

    '            '----------------------------------
    '            'Indexes
    '            With _nSqlDataReader
    '                Dim _iSvrDate As Integer = .GetOrdinal("SvrDate")
    '                Dim _iReferenceNumber As Integer = .GetOrdinal("ReferenceNumber")


    '                '----------------------------------
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes

    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            _mFormatDate = ._pReturnString(_nSqlDataReader(_iSvrDate))
    '                            _mReferenceNumber = ._pReturnString(_nSqlDataReader(_iReferenceNumber))

    '                            If _mReferenceNumber = Nothing Then
    '                                '  _mFormatDate = _mFormatDate
    '                                _mReferenceNumber = _mFormatDate & "-" & "0001"
    '                            Else
    '                                _mReferenceNumber = _mFormatDate & "-" & Format(Right(_mReferenceNumber, 4) + 1, "0000")
    '                            End If

    '                        Loop
    '                    Else

    '                    End If

    '                End With
    '            End With

    '            _nSqlDataReader.Close()
    '        End Using

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Public Sub _pSubGenerateDateFormat()
        Try
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            _nQuery = "SELECT CONVERT(VARCHAR(6),GETDATE(),12) as strDate"
            _mQuery = _nQuery

            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            With _mSqlCommand.Parameters

            End With
        Catch ex As Exception

        End Try
    End Sub
    'Public Sub _pSubGetAutoPaymentRefNo()
    '    Try
    '        '----------------------------------       
    '        _pSubSelectIdNo()

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader

    '            '----------------------------------
    '            'Indexes
    '            With _nSqlDataReader
    '                Dim _iTXNREFNO As Integer = .GetOrdinal("TXNREFNO")
    '                Dim _istrDate As Integer = .GetOrdinal("strDate")
    '                Dim _ictr As Integer = 1

    '                '----------------------------------
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes

    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            _ictr = _ictr + 1
    '                            '  _mReferenceNumber = ._pReturnString(_nSqlDataReader(_istrDate))
    '                        Loop
    '                        '_mReferenceNumber = _mReferenceNumber & "-" & _ictr.ToString().PadLeft(5, "0"c)
    '                        _mReferenceNumber = DateTime.Now.ToString("yyMMdd") & "-" & _ictr.ToString().PadLeft(5, "0"c)
    '                    Else
    '                        _mReferenceNumber = DateTime.Now.ToString("yyMMdd") & "-" & "00001"

    '                    End If

    '                End With
    '            End With

    '            _nSqlDataReader.Close()
    '        End Using

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub


    'Public Sub _pSubUpdate_forIssuance(ByVal ACCTNO As String)
    '    Try
    '        Dim _nQuery As String = Nothing
    '        _nQuery = "update [BUSMAST] set " _
    '                                & " IsForPayment = 0, " _
    '                                & " isPosted = 1 " _
    '                                & " where ACCTNO='" & ACCTNO & "'"
    '        Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
    '        _nSqlCommand.ExecuteNonQuery()
    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Sub _pSubSelect_PaymentDetails(ByVal TXNREFNO As String, ByRef ACCTNO As String, ByRef StatusID As String, ByRef PaymentFor As String, ByRef TaxpayerEmail As String)
    '    Try
    '        Dim _nQuery As String = Nothing
    '        _nQuery = " select TXNREFNO,ACCTNO,StatusID,TYPE,emailaddr from onlinepaymentrefno where TXNREFNO='" & TXNREFNO & "'"
    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
    '            With _nSqlDataReader
    '                Dim _iTXNREFNO As Integer = .GetOrdinal("TXNREFNO")
    '                Dim _iACCTNO As Integer = .GetOrdinal("ACCTNO")
    '                Dim _iStatusID As Integer = .GetOrdinal("StatusID")
    '                Dim _iPaymentFor As Integer = .GetOrdinal("Type")
    '                Dim _iEmail As Integer = .GetOrdinal("EmailADDR")
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes
    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            TXNREFNO = ._pReturnString(_nSqlDataReader(_iTXNREFNO))
    '                            ACCTNO = ._pReturnString(_nSqlDataReader(_iACCTNO))
    '                            StatusID = ._pReturnString(_nSqlDataReader(_iStatusID))
    '                            PaymentFor = ._pReturnString(_nSqlDataReader(_iPaymentFor))
    '                            TaxpayerEmail = ._pReturnString(_nSqlDataReader(_iEmail))
    '                        Loop
    '                    End If
    '                End With
    '            End With
    '            _nSqlDataReader.Close()
    '        End Using
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Public Sub _pSubUpdate_PaymentDetails(ByVal TXNREFNO As String, ByVal id As String)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "update [OnlinePaymentRefno] set " _
                                    & " Status = 'Successful Payment', " _
                                    & " GateWayRefNo = '" & id & "', " _
                                    & " StatusID='SUCCESS'," _
                                    & " DateVerified= getdate()," _
                                    & " Via='PayMaya'" _
                                    & " where TXNREFNO='" & TXNREFNO & "'"
            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubInsertPaymentRefNo(TXNREFNO, EMAILADDR, PAYMENTCHANNEL, ACCTNO, PaymentFor)
        Try
            Dim _nQuery As String = Nothing

            _nQuery = "INSERT INTO [OnlinePaymentRefno]([TXNREFNO], [EMAILADDR], [PAYMENTCHANNEL], [ACCTNO], [strDATE],[Status],[Type], [TransDate], [StatusID]) VALUES('" & TXNREFNO & "','" & EMAILADDR & "','" & PAYMENTCHANNEL & "','" & ACCTNO & "',CONVERT(VARCHAR(6),GETDATE(),12),'Pending','" & PaymentFor & "','" & Format(Date.Now, "MM-dd-yyyy hh:mm:ss") & "','PENDING')"
            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubInsertPaymentPaymaya(ByVal _TXNREFNO As String, ByVal _EMAILADDR As String, ByVal _PAYMENTCHANNEL As String, ByVal _ACCTNO As String, ByVal _strDATE As String, ByVal _STATUSID As String, ByVal _STATUS As String, ByVal _GATEWAYREFNO As String, ByVal _SECURITY As String, ByVal _TYPE As String, ByVal _TRANSDATE As String, ByVal _TRXDATE As String, ByVal _RAWAMOUNT As String, ByVal _TOTAMOUNT As String, ByVal _FEEAMOUNT As String, ByVal _DATEVERIFIED As String, ByVal _VERIFIEDBY As String, ByVal _VERIFYING As String, ByVal _VIA As String, ByVal _POSTEDDATE As String, ByVal _POSTSTATUS As String
)
        Try
            Dim _nQuery As String = Nothing

            _nQuery = "INSERT INTO [OnlinePaymentRefno] values(" & _
                 "'" & _TXNREFNO & "'," & _
                 "'" & _EMAILADDR & "'," & _
                 "'" & _PAYMENTCHANNEL & "'," & _
                 "'" & _ACCTNO & "'," & _
                 "(SELECT CONVERT(VARCHAR(6),GETDATE(),12))," & _
                 "'" & _STATUSID & "'," & _
                 "'" & _STATUS & "'," & _
                 "NULL," & _
                 "NULL," & _
                 "'" & _TYPE & "'," & _
                 "(SELECT GETDATE())," & _
                 "(SELECT GETDATE())," & _
                 "'" & _RAWAMOUNT & "'," & _
                 "'" & _TOTAMOUNT & "'," & _
                 "'" & _FEEAMOUNT & "'," & _
                 "NULL," & _
                 "NULL," & _
                 "NULL," & _
                 "'" & _VIA & "'," & _
                 "NULL," & _
                 "NULL" & _
                ")"
            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubInsertPaymentLBP(TXNREFNO, EMAILADDR, PAYMENTCHANNEL, ACCTNO, TYPE, RAWAMOUNT, HASH)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "INSERT INTO [OnlinePaymentRefno]  ([TXNREFNO],[EMAILADDR],[PAYMENTCHANNEL],[ACCTNO],[strDATE],[Status],[Type], [TransDate], [StatusID],[rawAmount],[Security])   VALUES   (   '" & TXNREFNO & "',   '" & EMAILADDR & "',   '" & PAYMENTCHANNEL & "',   '" & ACCTNO & "',   CONVERT(VARCHAR(6),GETDATE(),12),   'Pending',   '" & TYPE & "',   '" & Format(Date.Now, "MM-dd-yyyy hh:mm:ss") & "',   'PENDING',   '" & RAWAMOUNT & "',   '" & HASH & "'   )"
            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubInsertLBPLogs(ByVal MerchantCode As String,
                                  ByVal MerchantRefNo As String,
                                  ByVal Particulars As String,
                                  ByVal Amount As String,
                                  ByVal PayorName As String,
                                  ByVal PayorEmail As String,
                                  ByVal Status As String,
                                  ByVal EppRefNo As String,
                                  ByVal PaymentOption As String,
                                  ByVal DateStamp As String,
                                  ByVal StatusDesc As String,
                                  Optional ByVal PostedString As String = Nothing)
        Try
            Dim _nQuery As String = Nothing
            If String.IsNullOrEmpty(PostedString) Then
                _nQuery = "INSERT INTO [LBP_Logs]  " & _
                    "([MerchantCode],[MerchantRefNo],[Particulars],[Amount],[PayorName],[PayorEmail],[Status], [EppRefNo], [PaymentOption],[DateStamp],[StatusDesc])" & _
                    " VALUES " & _
                    "('" & MerchantCode & "',   '" & MerchantRefNo & "',   '" & Particulars & "',   '" & Amount & "',   '" & PayorName & "',   '" & PayorEmail & "',   '" & Status & "',   '" & EppRefNo & "',   '" & PaymentOption & "',  '" & DateStamp & "' ,'" & StatusDesc & "')"

            Else
                _nQuery = "INSERT INTO [LBP_Logs]  " & _
                    "([MerchantCode],[MerchantRefNo],[Particulars],[Amount],[PayorName],[PayorEmail],[Status], [EppRefNo], [PaymentOption],[DateStamp],[StatusDesc],[PostedString])" & _
                    " VALUES " & _
                    "('" & MerchantCode & "',   '" & MerchantRefNo & "',   '" & Particulars & "',   '" & Amount & "',   '" & PayorName & "',   '" & PayorEmail & "',   '" & Status & "',   '" & EppRefNo & "',   '" & PaymentOption & "', getdate() ,'" & StatusDesc & "','" & PostedString & "')"

            End If

            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubUpdateOnlinePaymentInfo(ByVal TXNREFNO As String, ByVal status As String, ByVal GateWayRefNo As String, ByVal Security As String, ByVal trxdate As String, ByVal amount As String, ByVal fee As String, ByVal total As String, ByVal statusID As String, ByVal PaymentOption As String, Optional ByRef err As String = Nothing)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "update [OnlinePaymentRefno] set " _
                                    & " Status = '" & status & "', " _
                                    & " GateWayRefNo = '" & GateWayRefNo & "', " _
                                    & " Security='" & Security & "'," _
                                    & " TRXDATE='" & trxdate & "'," _
                                    & " TransDate = getdate()," _
                                    & " StatusID='" & statusID & "'," _
                                    & " DateVerified= getdate()," _
                                    & " Via='" & PaymentOption & "'," _
                                    & " PostStatus='1'" _
                                    & " where TXNREFNO='" & TXNREFNO & "'"

            '& " rawAmount='" & amount & "'," _
            '& " feeAmount='" & fee & "'," _
            '& " totAmount='" & total & "'," _
            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
        Catch ex As Exception
            err = ";_pSubUpdateOnlinePaymentInfo:" & ex.Message
        End Try
    End Sub

    Public Sub _pSubUpdateOnlinePaymentInfo2(PaymentChannel, TXNREFNO, Status, GateWayRefNo, Security, statusID)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "update [OnlinePaymentRefno] set " _
                                    & " PaymentChannel = '" & PaymentChannel & "', " _
                                    & " Status = '" & Status & "', " _
                                    & " GateWayRefNo = '" & GateWayRefNo & "', " _
                                    & " Security='" & Security & "'," _
                                    & " StatusID='" & statusID & "'" _
                                    & " where TXNREFNO='" & TXNREFNO & "'"
            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub
    Sub GetsqlDateNow(ByRef _sqlDateNow As DateTime, ByRef _sqlDateNow10 As DateTime)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "  select getdate() as '_sqlDateNow',DATEADD(mi,+10,GETDATE()) as '_sqlDateNow10'"
            _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
                With _nSqlDataReader
                    Dim _isqlDateNow As Integer = .GetOrdinal("_sqlDateNow")
                    Dim _isqlDateNow10 As Integer = .GetOrdinal("_sqlDateNow10")
                    If _nSqlDataReader.HasRows Then
                        Do While _nSqlDataReader.Read
                            _sqlDateNow = _nSqlDataReader(_isqlDateNow).ToString
                            _sqlDateNow10 = _nSqlDataReader(_isqlDateNow10).ToString()
                        Loop
                    End If
                End With
                _nSqlDataReader.Close()
            End Using
        Catch ex As Exception

        End Try
    End Sub

    'Sub Cancel_Expired(ByRef _sqlDateNow As DateTime, ByRef _sqlDateNow10 As DateTime)
    '    Try
    '        Dim _nQuery As String = Nothing
    '        _nQuery = "  select getdate() as '_sqlDateNow',DATEADD(mi,+10,GETDATE()) as '_sqlDateNow10'"
    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
    '            With _nSqlDataReader
    '                Dim _isqlDateNow As Integer = .GetOrdinal("_sqlDateNow")
    '                Dim _isqlDateNow10 As Integer = .GetOrdinal("_sqlDateNow10")
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes
    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            _sqlDateNow = ._pReturnString(_nSqlDataReader(_isqlDateNow))
    '                            _sqlDateNow10 = ._pReturnString(_nSqlDataReader(_isqlDateNow10))
    '                        Loop
    '                    End If
    '                End With
    '            End With
    '            _nSqlDataReader.Close()
    '        End Using
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Public Sub LBP_EPS_InsertLog(ByVal _Request As String,
                               ByVal _Response As String,
                               ByVal _TRXREFNO As String,
                               ByVal _ACCTNO As String,
                               ByVal _TRANSTYPE As String)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "Insert into LBP_EpsLogs  values(getdate()" & _
             ", '" & _Request & "' " & _
             ", '" & _Response & "' " & _
             ", '" & _TRXREFNO & "' " & _
             ", '" & _ACCTNO & "' " & _
             ", '" & _TRANSTYPE & "' " & _
            ")"

            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            '----------------------------------
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
            Exit Sub
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GCASH_InsertLog(ByVal _function As String,
                                ByVal _transacstionID As String,
                                ByVal _merchantTransId As String,
                                ByVal _JSON As String,
                                ByVal _request As String,
                                ByVal _acquirementStatus As String,
                                ByVal _signature As String)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "Insert into Gcash_Transactions  values(" & _
                        "'" & _function & "'" & _
                        ",'" & _transacstionID & "'" & _
                        ",'" & _merchantTransId & "'" & _
                        ",'" & _JSON & "'" & _
                        ",'" & _request & "'" & _
                        ",'" & _signature & "'" & _
                        ",'" & _acquirementStatus & "'" & _
                        ",getdate())"

            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            '----------------------------------
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
            Exit Sub
        Catch ex As Exception

        End Try
    End Sub


    'Public Sub GCASH_CheckExpired(ByRef merchantTransId As String,
    '                                   ByRef transactionId As String,
    '                                   ByRef acquirementId As String)
    '    Try
    '        Dim _nQuery As String = Nothing
    '        _nQuery = " select * from [Gcash_Transactions]"
    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
    '            With _nSqlDataReader
    '                Dim _imerchantTransId As Integer = .GetOrdinal("merchantTransId")
    '                Dim _itransactionId As Integer = .GetOrdinal("transactionId")
    '                Dim _iacquirementId As Integer = .GetOrdinal("acquirementStatus")
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes
    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            merchantTransId = ._pReturnString(_nSqlDataReader(_imerchantTransId))
    '                            transactionId = ._pReturnString(_nSqlDataReader(_itransactionId))
    '                            acquirementId = ._pReturnString(_nSqlDataReader(_iacquirementId))

    '                        Loop
    '                    End If
    '                End With
    '            End With
    '            _nSqlDataReader.Close()
    '        End Using
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Sub GCASH_GetOrderQueryParams(ByRef merchantTransId As String,
    '                                     ByRef transactionId As String,
    '                                     ByRef acquirementId As String)
    '    Try
    '        Dim _nQuery As String = Nothing
    '        _nQuery = " select * from [Gcash_Transactions]"
    '        _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader
    '            With _nSqlDataReader
    '                Dim _imerchantTransId As Integer = .GetOrdinal("merchantTransId")
    '                Dim _itransactionId As Integer = .GetOrdinal("transactionId")
    '                Dim _iacquirementId As Integer = .GetOrdinal("acquirementStatus")
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes
    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read
    '                            merchantTransId = ._pReturnString(_nSqlDataReader(_imerchantTransId))
    '                            transactionId = ._pReturnString(_nSqlDataReader(_itransactionId))
    '                            acquirementId = ._pReturnString(_nSqlDataReader(_iacquirementId))

    '                        Loop
    '                    End If
    '                End With
    '            End With
    '            _nSqlDataReader.Close()
    '        End Using
    '    Catch ex As Exception

    '    End Try
    'End Sub


    Public Sub _pSubUpdateOnlinePaymentInfoCancelled(TXNREFNO, Status, GateWayRefNo, Security, trxdate, amount, fee, total, statusID)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "update [OnlinePaymentRefno] set " _
                                    & " Status = '" & Status & "', " _
                                    & " GateWayRefNo = '" & GateWayRefNo & "', " _
                                    & " TransDate= getdate()," _
                                    & " StatusID='" & statusID & "'" _
                                    & " where TXNREFNO='" & TXNREFNO & "'"
            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub



    Public Sub _pSubUpdateOTCPaymentInfo(TXNREFNO, Status, GateWayRefNo, Security, trxdate, amount, fee, total, statusID)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "update [OnlinePaymentRefno] set " _
                                    & " Status = '" & Status & "', " _
                                    & " GateWayRefNo = '" & GateWayRefNo & "', " _
                                    & " TRXDATE='" & trxdate & "'," _
                                    & " rawAmount='" & amount & "'," _
                                    & " feeAmount='" & fee & "'," _
                                    & " totAmount='" & total & "'," _
                                    & " TransDate='" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "'," _
                                    & " StatusID='" & statusID & "'," _
                                    & " DateVerified=''," _
                                    & " VerifiedBy=''," _
                                    & " Via='Over-The-Counter'," _
                                     & " PostStatus='1'" _
                                    & " where TXNREFNO='" & TXNREFNO & "'"
            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubUpdateCTC(_nTrxDate, _nStatusID, ACCTNO, referenceNo)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "update [CTC_Online] set " _
                                    & " TransDate = '" & _nTrxDate & "', " _
                                    & " Status = '" & _nStatusID & "', " _
                                    & " RefNo = '" & referenceNo & "' " _
                                    & " where ControlNo='" & ACCTNO & "'"

            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub

    Public Sub _pSubUpdateCTC_OTC(_nTrxDate, _nStatusID, ACCTNO, referenceNo)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "update [CTC_Online] set " _
                                    & " TransDate =  (select getdate()), " _
                                    & " Status = '" & _nStatusID & "', " _
                                    & " RefNo = '" & referenceNo & "', " _
                                    & " OTC = 1 " _
                                    & " where ControlNo='" & ACCTNO & "'"

            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub


    'Public Sub _pSubGetPaymentExist()
    '    Try
    '        '----------------------------------

    '        _pSubSelectPaymentExist()

    '        Using _nSqlDataReader As SqlDataReader = _mSqlCommand.ExecuteReader

    '            '----------------------------------
    '            'Indexes
    '            With _nSqlDataReader

    '                Dim _iReferenceNumber As Integer = .GetOrdinal("ReferenceNumber")
    '                Dim _iReferenceDate As Integer = .GetOrdinal("ReferenceDate")
    '                Dim _iTypeofPayment As Integer = .GetOrdinal("TypeofPayment")
    '                Dim _iBankName As Integer = .GetOrdinal("BankName")
    '                Dim _iBankCode As Integer = .GetOrdinal("BankCode")
    '                Dim _iBankAddress As Integer = .GetOrdinal("BankAddress")
    '                Dim _iAccountNumber As Integer = .GetOrdinal("AccountNumber")
    '                Dim _iCardNumber As Integer = .GetOrdinal("CardNumber")
    '                Dim _iAccountName As Integer = .GetOrdinal("AccountName")
    '                Dim _iAmount As Integer = .GetOrdinal("TotalAmount")


    '                '----------------------------------
    '                Dim _nClassReturnTypes As New ClassReturnTypes
    '                With _nClassReturnTypes

    '                    If _nSqlDataReader.HasRows Then
    '                        Do While _nSqlDataReader.Read

    '                            _mReferenceNumber = ._pReturnString(_nSqlDataReader(_iReferenceNumber))
    '                            _mReferenceDate = ._pReturnString(_nSqlDataReader(_iReferenceDate))
    '                            _mTypeofPayment = ._pReturnString(_nSqlDataReader(_iTypeofPayment))
    '                            _mBankName = ._pReturnString(_nSqlDataReader(_iBankName))
    '                            _mBankCode = ._pReturnString(_nSqlDataReader(_iBankCode))
    '                            _mBankAddress = ._pReturnString(_nSqlDataReader(_iBankAddress))
    '                            _mAccountNumber = ._pReturnString(_nSqlDataReader(_iAccountNumber))
    '                            _mCardNumber = ._pReturnString(_nSqlDataReader(_iCardNumber))
    '                            _mAccountName = ._pReturnString(_nSqlDataReader(_iAccountName))


    '                        Loop
    '                    Else
    '                        _mBPAccountNumber = Nothing
    '                    End If

    '                End With
    '            End With

    '            _nSqlDataReader.Close()
    '        End Using

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Sub _pSubGetParameter1()
    '    Try
    '        '----------------------------------

    '        _pSubSelectParameter1()

    '        'Using _nSqlDataReader As SqlDataReader = _mSqlDataReader
    '        Dim _nSQLDR As SqlDataReader = _mSqlCommand.ExecuteReader

    '        '----------------------------------
    '        'Indexes
    '        With _nSQLDR

    '            'Dim _iReferenceNumber As Integer = .GetOrdinal("ReferenceNumber")
    '            'Dim _iReferenceDate As Integer = .GetOrdinal("ReferenceDate")
    '            Dim _iStatcode As Integer = .GetOrdinal("STATCODE")
    '            Dim _iOwner As Integer = .GetOrdinal("owner")
    '            Dim _iOwndesc As Integer = .GetOrdinal("OWNDESC")
    '            Dim _iCommercialName As Integer = .GetOrdinal("COMMNAME")
    '            Dim _iCommercialAddress As Integer = .GetOrdinal("COMMADDR")


    '            '----------------------------------
    '            Dim _nClassReturnTypes As New ClassReturnTypes
    '            With _nClassReturnTypes

    '                If _nSQLDR.HasRows Then
    '                    Do While _nSQLDR.Read

    '                        '_mReferenceNumber = ._pReturnString(_nSQLDR(_iReferenceNumber))
    '                        '_mReferenceDate = ._pReturnString(_nSQLDR(_iReferenceDate))
    '                        _mStatcode = ._pReturnString(_nSQLDR(_iStatcode))
    '                        _mOwner = ._pReturnString(_nSQLDR(_iOwner))
    '                        _mOwndesc = ._pReturnString(_nSQLDR(_iOwndesc))
    '                        _mCommercialName = ._pReturnString(_nSQLDR(_iCommercialName))
    '                        _mCommercialAddress = ._pReturnString(_nSQLDR(_iCommercialAddress))

    '                    Loop
    '                Else

    '                    _mStatcode = Nothing
    '                    _mOwner = Nothing
    '                    _mOwndesc = Nothing
    '                    _mCommercialName = Nothing
    '                    _mCommercialAddress = Nothing

    '                End If

    '            End With
    '        End With

    '        _nSQLDR.Close()
    '        'End Using

    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Sub _pSubGetParameter2()
    '    Try
    '        '----------------------------------

    '        _pSubSelectParameter2()

    '        Dim _mDataReader As SqlDataReader = _mSqlCommand.ExecuteReader

    '        'If _mDataTable.Rows.Count > 0 Then
    '        '    _mReferenceNumber = _mDataTable.Rows(0).Item("PBN").ToString
    '        '    _mReferenceDate = _mDataTable.Rows(0).Item("APP_DATE").ToString
    '        '    _mStatcode = _mDataTable.Rows(0).Item("STATCODE").ToString
    '        '    _mOwner = _mDataTable.Rows(0).Item("FullName").ToString
    '        '    _mOwndesc = _mDataTable.Rows(0).Item("OWNDESC1").ToString
    '        '    _mCommercialName = _mDataTable.Rows(0).Item("COMMNAME").ToString
    '        '    _mCommercialAddress = _mDataTable.Rows(0).Item("COMMADDR").ToString
    '        'Else
    '        '    _mStatcode = Nothing
    '        '    _mOwner = Nothing
    '        '    _mOwndesc = Nothing
    '        '    _mCommercialName = Nothing
    '        '    _mCommercialAddress = Nothing
    '        'End If

    '        '----------------------------------
    '        'Indexes
    '        With _mDataReader

    '            Dim _iReferenceNumber As Integer = .GetOrdinal("PBN")
    '            Dim _iReferenceDate As Integer = .GetOrdinal("APP_DATE")
    '            Dim _iStatcode As Integer = .GetOrdinal("STATCODE")
    '            Dim _iOwner As Integer = .GetOrdinal("FullName")
    '            Dim _iOwndesc As Integer = .GetOrdinal("OWNDESC1")
    '            Dim _iCommercialName As Integer = .GetOrdinal("COMMNAME")
    '            Dim _iCommercialAddress As Integer = .GetOrdinal("COMMADDR")
    '            Dim _iDate_Esta As Integer = .GetOrdinal("Date_Esta")

    '            '----------------------------------
    '            Dim _nClassReturnTypes As New ClassReturnTypes
    '            With _nClassReturnTypes

    '                If _mDataReader.HasRows Then
    '                    Do While _mDataReader.Read

    '                        _mReferenceNumber = ._pReturnString(_mDataReader(_iReferenceNumber))
    '                        _mReferenceDate = ._pReturnString(_mDataReader(_iReferenceDate))
    '                        _mStatcode = ._pReturnString(_mDataReader(_iStatcode))
    '                        _mOwner = ._pReturnString(_mDataReader(_iOwner))
    '                        _mOwndesc = ._pReturnString(_mDataReader(_iOwndesc))
    '                        _mCommercialName = ._pReturnString(_mDataReader(_iCommercialName))
    '                        _mCommercialAddress = ._pReturnString(_mDataReader(_iCommercialAddress))
    '                        _mDate_Esta = ._pReturnString(_mDataReader(_iDate_Esta))

    '                    Loop
    '                Else

    '                    _mStatcode = Nothing
    '                    _mOwner = Nothing
    '                    _mOwndesc = Nothing
    '                    _mCommercialName = Nothing
    '                    _mCommercialAddress = Nothing
    '                    _mDate_Esta = Nothing
    '                End If

    '            End With
    '        End With

    '        _mDataReader.Close()


    '        '----------------------------------
    '    Catch ex As Exception
    '        cDalLogEvent._pSubLogError(ex)
    '    End Try
    'End Sub
    'Public Sub _pSubGetCourierDetails()
    '    Try
    '        '----------------------------------

    '        _pSubSelectCourierDetails()

    '        Dim _mDataReader As SqlDataReader = _mSqlCommand.ExecuteReader


    '        '----------------------------------
    '        'Indexes
    '        With _mDataReader

    '            Dim _iCourierName As Integer = .GetOrdinal("Name")
    '            Dim _iCourierLocation As Integer = .GetOrdinal("Location")
    '            Dim _iCourierContact As Integer = .GetOrdinal("Contactno")



    '            '----------------------------------
    '            Dim _nClassReturnTypes As New ClassReturnTypes
    '            With _nClassReturnTypes

    '                If _mDataReader.HasRows Then
    '                    Do While _mDataReader.Read

    '                        _mCourierName = ._pReturnString(_mDataReader(_iCourierName))
    '                        _mCourierLocation = ._pReturnString(_mDataReader(_iCourierLocation))
    '                        _mCourierContact = ._pReturnString(_mDataReader(_iCourierContact))


    '                    Loop
    '                Else

    '                    _mStatcode = Nothing
    '                    _mOwner = Nothing
    '                    _mOwndesc = Nothing
    '                    _mCommercialName = Nothing
    '                    _mCommercialAddress = Nothing

    '                End If

    '            End With
    '        End With

    '        _mDataReader.Close()


    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Public Sub _pSubCheckBPAccountNumber()
    '    Try
    '        '----------------------------------

    '        _pSubSelectPaymentDetails()

    '        Dim _mDataReader As SqlDataReader = _mSqlCommand.ExecuteReader


    '        '----------------------------------
    '        'Indexes
    '        With _mDataReader

    '            Dim _iBPAccountNumber As Integer = .GetOrdinal("BPAccountNumber")




    '            '----------------------------------
    '            Dim _nClassReturnTypes As New ClassReturnTypes
    '            With _nClassReturnTypes

    '                If _mDataReader.HasRows Then
    '                    Do While _mDataReader.Read

    '                        _mBPAccountNumber = ._pReturnString(_mDataReader(_iBPAccountNumber))


    '                    Loop
    '                Else

    '                    _mBPAccountNumber = Nothing

    '                End If

    '            End With
    '        End With

    '        _mDataReader.Close()


    '        '----------------------------------
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Sub _pSubSelectPenaltySetup()
        Try
            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            '----------------------------------
            _nQuery = _
                "Select BT_INTEREST, BT_PENALTY, MF_INTEREST, MF_PENALTY, GF_INTEREST, GF_PENALTY, SF_INTEREST, SF_PENALTY, FF_INTEREST, FF_PENALTY from Setup"

            _mQuery = _nQuery

            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            _mSqlCommand.ExecuteNonQuery()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubSelectBillingTempNew_Caloocan(ByVal ACCTNO As String) ' @Added 20220610
        Try
            Dim _nQuery As String = Nothing
            _nQuery = _
                    "  select Acctno,Foryear,Bus_Code,Desc1,Taxbase,Pres,Prev,AssessTotal,Taxcode,Qtryr1,Amt_Pd as Taxdue,Amt_PenPd as Pendue,Totdue,Discount from ( " & _
                    " select Acctno, Foryear, Bus_Code, Desc1, Taxbase, Pres, Prev, AssessTotal, Taxcode, Qtryr1, sum(convert(money,Amt_Pd)) as Amt_Pd, sum(convert(money,Amt_PenPd)) as Amt_PenPd, Sum(convert(money,Totdue)) as Totdue, sum(convert(money,isnull(Discount,0))) as Discount from (select Acctno, Foryear, Bus_Code, case when xxx is null then Desc1 else XXX end as Desc1, Taxbase, Pres, Prev, AssessTotal, Taxcode, Qtryr1, Amt_Pd, Amt_PenPd, Totdue, Discount from ( select Acctno, Foryear, '' as Bus_Code, upper(MainAcctDesc) as Desc1, '' as Taxbase, Pres, Prev, AssessTotal, Taxcode, isnull(Qtryr1,'') as Qtryr1, sum(convert(money,Amt_Pd)) as Amt_Pd, sum(convert(money,Amt_PenPd)) as Amt_PenPd, Sum(convert(money,Totdue)) as Totdue, sum(convert(money,isnull(Discount,0))) as Discount, subacctcode , (Select taxdesc from [TEXAS\MSSQL2014].[BPLTAS_CALOOCAN_WEB_2021].dbo.acctlink where subacctcode =  BILLINGTEMP.SubAcctCode and taxcode = 'BT') as XXX " & _
                    " from BILLINGTEMP where acctno = '" & ACCTNO & "' and IsRegBill = '1' and substring(desc1, 1, 9) <> 'TaxCredit' and substring(desc1, 1, 9) <> 'Discount' and isnull(I_YEAR1,'') = '' and not (isnull(REMARKS,'') = 'DELETE') and taxcode = 'bt  '  group by  Acctno, Foryear, Pres, Prev, AssessTotal, Taxcode, isnull(Qtryr1,''), MainAcctDesc, subacctcode) as y) as x group by  Acctno, Foryear, Bus_Code, desc1, taxbase, Pres, Prev, AssessTotal, Taxcode, Qtryr1 " & _
                    " Union All " & _
                    " select Acctno, Foryear, Bus_Code, Desc1, Taxbase, Pres, Prev, AssessTotal, Taxcode, Qtryr1, sum(convert(money,Amt_Pd)) as Amt_Pd, sum(convert(money,Amt_PenPd)) as Amt_PenPd, Sum(convert(money,Totdue)) as Totdue, sum(convert(money,isnull(Discount,0))) as Discount from (select Acctno, Foryear, Bus_Code, 'DISCOUNT ON ' + case when xxx is null then Desc1 else XXX end as Desc1, Taxbase, Pres, Prev, AssessTotal, Taxcode, Qtryr1, Amt_Pd, Amt_PenPd, Totdue, Discount from ( select Acctno, Foryear, '' as Bus_Code, upper(MainAcctDesc) as Desc1, '' as Taxbase, '' Pres, '' Prev,  'A02' AssessTotal, Taxcode, isnull(Qtryr1,'') as Qtryr1, sum(convert(money,Amt_Pd)) as Amt_Pd, sum(convert(money,Amt_PenPd)) as Amt_PenPd, Sum(convert(money,Totdue)) as Totdue, sum(convert(money,isnull(Discount,0))) as Discount, subacctcode , (Select taxdesc from  [TEXAS\MSSQL2014].[BPLTAS_CALOOCAN_WEB_2021].dbo.acctlink where subacctcode =  BILLINGTEMP.SubAcctCode and taxcode = 'BT') as XXX " & _
                    " from BILLINGTEMP where acctno = '" & ACCTNO & "' and IsRegBill = '1' and substring(desc1, 1, 9) = 'Discount' and isnull(I_YEAR1,'') = '' and not (isnull(REMARKS,'') = 'DELETE') and taxcode = 'bt  '  group by  Acctno, Foryear, Pres, Prev, AssessTotal, Taxcode, isnull(Qtryr1,''), MainAcctDesc, subacctcode) as y) as x group by  Acctno, Foryear, Bus_Code, desc1, taxbase, Pres, Prev, AssessTotal, Taxcode, Qtryr1 " & _
                    " Union All " & _
                    " select Acctno, Foryear, Bus_Code, Desc1, Taxbase, Pres, Prev, AssessTotal, Taxcode, Qtryr1, sum(convert(money,Amt_Pd)) as Amt_Pd, sum(convert(money,Amt_PenPd)) as Amt_PenPd, Sum(convert(money,Totdue)) as Totdue, sum(convert(money,isnull(Discount,0))) as Discount from (select Acctno, Foryear, Bus_Code, 'TAX CREDIT ON ' + case when xxx is null then Desc1 else XXX end as Desc1, Taxbase, Pres, Prev, AssessTotal, Taxcode, Qtryr1, Amt_Pd, Amt_PenPd, Totdue, Discount from ( select Acctno, Foryear, '' as Bus_Code, upper(MainAcctDesc) as Desc1, '' as Taxbase, '' Pres, '' Prev, 'A03' AssessTotal, Taxcode,isnull(Qtryr1,'') as Qtryr1, sum(convert(money,Amt_Pd)) as Amt_Pd, sum(convert(money,Amt_PenPd)) as Amt_PenPd, Sum(convert(money,Totdue)) as Totdue, sum(convert(money,isnull(Discount,0))) as Discount, subacctcode , (Select taxdesc from  [TEXAS\MSSQL2014].[BPLTAS_CALOOCAN_WEB_2021].dbo.acctlink where subacctcode =  BILLINGTEMP.SubAcctCode and taxcode = 'BT') as XXX " & _
                    "  from BILLINGTEMP where acctno = '" & ACCTNO & "' and IsRegBill = '1' and substring(desc1, 1, 9) = 'TaxCredit' and isnull(I_YEAR1,'') = '' and not (isnull(REMARKS,'') = 'DELETE') and taxcode = 'bt  '  group by  Acctno, Foryear, Pres, Prev, AssessTotal, Taxcode, isnull(Qtryr1,''), MainAcctDesc, subacctcode) as y) as x group by  Acctno, Foryear, Bus_Code, desc1, taxbase, Pres, Prev, AssessTotal, Taxcode, Qtryr1 " & _
                    "  Union All " & _
                    "  select Acctno, Foryear, '' as Bus_Code, 'MAYORS PERMIT FEES' as Desc1, Taxbase, Pres, Prev, AssessTotal, Taxcode, isnull(Qtryr1,'') as Qtryr1, sum(convert(money,Amt_Pd)) as Amt_Pd, sum(convert(money,Amt_PenPd)) as Amt_PenPd, Sum(convert(money,Totdue)) as Totdue, sum(convert(money,isnull(Discount,0))) as Discount " & _
                    "  from BILLINGTEMP where acctno = '" & ACCTNO & "' and IsRegBill = '1' and substring(desc1, 1, 9) <> 'TaxCredit' and isnull(I_YEAR1,'') = '' and not (isnull(REMARKS,'') = 'DELETE') and taxcode = 'mf  '  group by  Acctno, Foryear, Taxbase, Pres, Prev, AssessTotal, Taxcode, isnull(Qtryr1,'') " & _
                    " union all " & _
                    " select Acctno, Foryear, '' as Bus_Code, 'SANITARY FEES' as Desc1, Taxbase, Pres, Prev, AssessTotal, Taxcode, isnull(Qtryr1,'') as Qtryr1, sum(convert(money,Amt_Pd)) as Amt_Pd, sum(convert(money,Amt_PenPd)) as Amt_PenPd,Sum(convert(money,Totdue)) as Totdue, sum(convert(money,isnull(Discount,0))) as Discount " & _
                    " from BILLINGTEMP where acctno = '" & ACCTNO & "' and IsRegBill = '1' and substring(desc1, 1, 9) <> 'TaxCredit' and isnull(I_YEAR1,'') = '' and not (isnull(REMARKS,'') = 'DELETE') and taxcode = 'sf  '  group by  Acctno, Foryear, Taxbase, Pres, Prev, AssessTotal, Taxcode, isnull(Qtryr1,'') " & _
                    " union all " & _
                    " select Acctno, Foryear, '' as Bus_Code, 'REFUSE FEE' as Desc1, Taxbase, Pres, Prev, AssessTotal, Taxcode, isnull(Qtryr1,'') as Qtryr1, sum(convert(money,Amt_Pd)) as Amt_Pd, sum(convert(money,Amt_PenPd)) as Amt_PenPd,Sum(convert(money,Totdue)) as Totdue, sum(convert(money,isnull(Discount,0))) as Discount " & _
                    " from BILLINGTEMP where acctno = '" & ACCTNO & "' and IsRegBill = '1' and substring(desc1, 1, 9) <> 'TaxCredit' and isnull(I_YEAR1,'') = '' and not (isnull(REMARKS,'') = 'DELETE') and taxcode = 'gf  '  group by  Acctno, Foryear, Taxbase, Pres, Prev, AssessTotal, Taxcode, isnull(Qtryr1,'')  union all select Acctno, Foryear, '' as Bus_Code, 'FIRE SAFETY INSP. FEE' as Desc1, Taxbase, Pres, Prev, AssessTotal, Taxcode, isnull(Qtryr1,'') as Qtryr1, sum(convert(money,Amt_Pd)) as Amt_Pd, sum(convert(money,Amt_PenPd)) as Amt_PenPd,Sum(convert(money,Totdue)) as Totdue, sum(convert(money,isnull(Discount,0))) as Discount " & _
                    " from BILLINGTEMP where acctno = '" & ACCTNO & "' and IsRegBill = '1' and substring(desc1, 1, 9) <> 'TaxCredit' and isnull(I_YEAR1,'') = '' and not (isnull(REMARKS,'') = 'DELETE') and taxcode = 'ff  '  group by  Acctno, Foryear, Taxbase, Pres, Prev, AssessTotal, Taxcode, isnull(Qtryr1,'') " & _
                    " union all " & _
                    " select Acctno, Foryear, '' as Bus_Code, Desc1, Taxbase, Pres, Prev, AssessTotal, Taxcode, isnull(Qtryr1,'') as Qtryr1, sum(convert(money,Amt_Pd)) as Amt_Pd, sum(convert(money,Amt_PenPd)) as Amt_PenPd, Sum(convert(money,Totdue)) as Totdue, sum(convert(money,isnull(Discount,0))) as Discount " & _
                    " from BILLINGTEMP where acctno = '" & ACCTNO & "' and IsRegBill = '1' and substring(desc1, 1, 9) <> 'TaxCredit' and isnull(I_YEAR1,'') = '' and not (isnull(REMARKS,'') = 'DELETE') and not taxcode in ('bt  ','mf  ','sf  ','gf  ','ff  ') group by  Acctno, Foryear, Desc1, Taxbase, Pres, Prev, AssessTotal, Taxcode, isnull(Qtryr1,'') " & _
                    " ) as xxx order by Foryear desc, AssessTotal, Bus_Code,  Taxbase, isnull(Qtryr1,'x') "

            _mQuery = _nQuery

            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)

            _mSqlCommand.ExecuteNonQuery()

        Catch ex As Exception

        End Try
    End Sub

    Public Function isCheckSumExists(ByVal CheckSum As String, ByVal trxRefNo As String) As Boolean
        Dim hasrow As Boolean = False
        Try

            Dim _nQuery As String =
                "  Select * from LBP_EPSLOGS where Request like '%" & CheckSum & "%' and TRXREFNO='" & trxRefNo & "'"
            _mQuery = _nQuery
            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)
            Dim _nSqlDr As SqlDataReader = _mSqlCommand.ExecuteReader
            Using _nSqlDr
                If _nSqlDr.HasRows Then
                    hasrow = True
                Else
                    hasrow = False
                End If
            End Using
            Return hasrow
        Catch ex As Exception
            Return hasrow
        End Try
    End Function

    Public Function isEPPExists(ByVal MerchantRefNo As String, Optional ByVal PARTICULARS As String = Nothing, Optional ByRef qry As String = Nothing) As Boolean
        Dim hasrow As Boolean = False
        Try
            Dim _nQuery As String
            If String.IsNullOrEmpty(PARTICULARS) Then
                _nQuery = "  Select * from LBP_LOGS where MerchantRefNo ='" & MerchantRefNo & "'"
            Else
                _nQuery = "  Select * from LBP_LOGS where MerchantRefNo ='" & MerchantRefNo & "' and PARTICULARS='" & PARTICULARS & "'"
            End If

            qry = _nQuery
            _mQuery = _nQuery
            '----------------------------------
            _mSqlCommand = New SqlCommand(_mQuery, _mSqlCon)
            Dim _nSqlDr As SqlDataReader = _mSqlCommand.ExecuteReader
            Using _nSqlDr
                If _nSqlDr.HasRows Then
                    hasrow = True
                Else
                    hasrow = False
                End If
            End Using
            Return hasrow
        Catch ex As Exception
            Return hasrow
        End Try
    End Function

    Public Function Get_PaymentOptions() As DataSet
        Try

            _DataSet = New DataSet
            Dim _Query As String = "select * from OnlinePaymentSetup where status=1"
            _mSqlCommand = New SqlCommand(_Query, _mSqlCon)
            Dim _SqlDataAdapter As New SqlDataAdapter(_mSqlCommand)
            _SqlDataAdapter.Fill(_DataSet)
            Return _DataSet
        Catch ex As Exception

        End Try
    End Function

    'Public Sub Get_GatewayFee(ByVal Code As String, ByRef GateWayFee As String, ByRef SpidcFee As String)
    '    Try
    '        Dim _Query As String = _
    '            " IF COL_LENGTH('ONLINEPAYMENTSETUP', 'GATEWAYFEE') IS NULL AND COL_LENGTH('ONLINEPAYMENTSETUP', 'SPIDCFEE') IS NULL " &
    '            " BEGIN " &
    '            " ALTER TABLE ONLINEPAYMENTSETUP ADD  GATEWAYFEE NVARCHAR(MAX),  SPIDCFEE NVARCHAR(MAX) " &
    '            " ALTER TABLE " & cGlobalConnections._pSqlCxn_OAIMS.Database & ".dbo.[ONLINEPAYMENTREFNO] ADD  FeeAmountSPIDC Money " &
    '            " End " &
    '            " Else " &
    '            " Declare @ColName as nVarchar(max)" &
    '            " select @ColName= COALESCE( @ColName + ' ISNULL(' +c.name + ',''0'') ','', '') +" &
    '            " c.name + ', ' from sysobjects o JOIN syscolumns c ON o.id = c.id" &
    '            " WHERE o.xtype = 'U' AND o.name ='OnlinePaymentSetup'" &
    '            " SEt @ColName=  'Select ' + SUBSTRING(@ColName,0,LEN(@ColName)) + ' FROM [OnlinePaymentSetup] where Code =''" & Code & "'''" &
    '            " EXEC(@ColName)"


    '        Dim _SqlCommand = New SqlCommand(_Query, _mSqlCon)
    '        _mSqlDataReader = _SqlCommand.ExecuteReader
    '        If _mSqlDataReader.HasRows Then
    '            Do Until _mSqlDataReader.Read = False
    '                GateWayFee = _mSqlDataReader("GateWayFee")
    '                SpidcFee = _mSqlDataReader("SpidcFee")
    '            Loop
    '        Else
    '            GateWayFee = 0
    '            SpidcFee = 0
    '        End If
    '        _mSqlDataReader.Close()
    '        _SqlCommand.Dispose()

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Public Function Generate_SPIDCREFNO(ByVal Type As String) As String
        Dim SPIDCREFNO As String = Nothing
        Try


            Dim isTest As String = Nothing
            Dim LGU_Unique As String = Nothing
            If HttpContext.Current.Request.Url.AbsoluteUri.ToUpper.Contains("TEST") = True Then
                isTest = "T"
            ElseIf System.Environment.MachineName.ToUpper = "WEBSVRCALOOCAN" And HttpContext.Current.Request.Url.AbsoluteUri.ToUpper.Contains("TEST") = True Then
                isTest = "CALTEST-"
            ElseIf System.Environment.MachineName.ToUpper = "HAVANA" Then
                isTest = "H"
            ElseIf System.Environment.MachineName.ToUpper = "MADRID" Then
                isTest = "M"
            Else
                Dim _nClass2 As New cHardwareInformation
                Dim _nMachineName As String = Nothing
                _nMachineName = _nClass2._pMachineName.ToUpper
                If _nMachineName = "MANDAUEWEBSVR" Then
                    LGU_Unique = "-M"
                ElseIf _nMachineName = "WEBSVRCALOOCAN" Then
                    isTest = "CAL-"
                End If
            End If

            Dim _nQuery As String = " " &
                    "  select concat('" & isTest & "','" & Type & "','" & LGU_Unique & "',year(getdate()),'-'," &
                    "  (" &
                    "   SELECT REPLACE(STR((" &
                    "  SELECT CASE" &
                    "   WHEN ISNULL(" &
                    "  (SELECT  TOP 1 RIGHT(TXNREFNO,5)SEQ FROM OnlinePaymentRefno" &
                    "  WHERE YEAR(TransDate) = YEAR(GETDATE()) and [Type] = '" & Type & "'" &
                    "  ORDER BY RIGHT(TXNREFNO,5) DESC),0)=0" &
                    "  	THEN  0" &
                    "  ELSE (" &
                    "  SELECT  TOP 1 RIGHT(TXNREFNO,5)SEQ FROM OnlinePaymentRefno" &
                    "  WHERE YEAR(TransDate) = YEAR(GETDATE()) and [Type] = '" & Type & "'" &
                    "  ORDER BY RIGHT(TXNREFNO,5) DESC)" &
                    "   END NEWSEQ)+1,5),' ','0') AS SEQ" &
                    "   ))SPIDCREFNO"


            Dim _nSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            _mSqlDataReader = _nSqlCommand.ExecuteReader
            If _mSqlDataReader.HasRows Then
                Dim ctr As Integer = 0
                Do Until _mSqlDataReader.Read = False
                    SPIDCREFNO = _mSqlDataReader("SPIDCREFNO")
                Loop
            Else
                SPIDCREFNO = Nothing
            End If
            _mSqlDataReader.Close()
            _nSqlCommand.Dispose()
        Catch ex As Exception
            SPIDCREFNO = Nothing
        End Try

        Return SPIDCREFNO
    End Function


    'Sub Insert_Transaction(ByVal SPIDCREFNO As String, ByVal ACCTNO As String, ByVal TYPE As String, ByVal RAWAMOUNT As String, ByVal TOTAMOUNT As String, ByVal FEEAMOUNT As String, ByVal FeeAmountSPIDC As String, Optional ByRef err As String = Nothing)
    '    Try
    '        Dim _nQuery As String = Nothing
    '        _nQuery = "INSERT INTO ONLINEPAYMENTREFNO" &
    '            "(TXNREFNO, EMAILADDR, PAYMENTCHANNEL, ACCTNO, strDATE, StatusID, " &
    '            " Status, GateWayRefNo, Security, Type, TransDate, TRXDATE, rawAmount, " &
    '            " totAmount, feeAmount, DateVerified, VerifiedBy, Verifying, Via, " &
    '            " PostedDate, PostStatus, FeeAmountSPIDC) VALUES(" &
    '                  "'" & SPIDCREFNO & "','" & cSessionUser._pUserID & "'," &
    '                  "'','" & ACCTNO & "'," &
    '                  "(format(getdate(),'yyMMdd')),NULL," &
    '                  "NULL,NULL," &
    '                  "NULL,'" & TYPE & "'," &
    '                  "(GETDATE()),(GETDATE())," &
    '                  "'" & RAWAMOUNT & "','" & TOTAMOUNT & "'," &
    '                  "'" & FEEAMOUNT & "',NULL," &
    '                  "NULL,NULL," &
    '                   "NULL,NULL," &
    '                   "'1'," &
    '                   FeeAmountSPIDC &
    '                  ")"
    '        Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
    '        _nSqlCommand.ExecuteNonQuery()

    '    Catch ex As Exception
    '        err = ex.Message
    '    End Try
    'End Sub

    Sub Update_Transaction(ByVal Set_Col As String, ByVal Set_Value As String, ByVal Where_Col As String, ByVal Where_Value As String, Optional ByRef qry As String = Nothing, Optional ByRef err As String = Nothing)
        Try
            Dim _nQuery As String = Nothing
            _nQuery = "Update OnlinePaymentRefNo set [" & Set_Col & "] = '" & Set_Value & "' where [" & Where_Col & "] = '" & Where_Value & "'"
            qry = _nQuery
            Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
            _nSqlCommand.ExecuteNonQuery()
        Catch ex As Exception
            err = ex.Message
        End Try
    End Sub

    'Sub Insert_History(ByVal SPIDCREFNO As String,
    '                   ByVal TYPE As String,
    '                   ByVal Gateway_Selected As String,
    '                   ByVal acctno As String,
    '                   ByVal RAWAMOUNT As String,
    '                   ByVal TOTAMOUNT As String,
    '                   ByVal FEEAMOUNT As String,
    '                   ByVal Status As String,
    '                   Optional ByVal GateWayRefNo As String = Nothing,
    '                   Optional ByVal PayorEmail As String = Nothing)
    '    Try
    '        Dim _nQuery As String = Nothing
    '        Dim numdesc As String = Nothing
    '        Dim gwrn As String = Nothing
    '        If String.IsNullOrEmpty(GateWayRefNo) Then
    '            gwrn = Nothing
    '        Else
    '            gwrn = "GATEWAY REFNO=" & GateWayRefNo & ";"
    '        End If


    '        Select Case TYPE
    '            Case "RPT"
    '                numdesc = "TDN/AssessNo"
    '            Case "BP"
    '                numdesc = "BIN"
    '            Case "OBO"
    '                numdesc = "APPNO"
    '            Case "CTC"
    '                numdesc = "CtrlNo"
    '            Case "LCR"
    '                numdesc = "RegNo"
    '        End Select

    '        PayorEmail = IIf(String.IsNullOrEmpty(PayorEmail), cSessionUser._pUserID, PayorEmail)
    '        Dim _RAWAMOUNT As Double = String.Format("{0:0.##}", RAWAMOUNT)
    '        Dim _FEEAMOUNT As Double = String.Format("{0:0.##}", FEEAMOUNT)
    '        Dim _TOTAMOUNT As Double = String.Format("{0:0.##}", TOTAMOUNT)



    '        _nQuery = "INSERT INTO HISTORY VALUES(" &
    '                  "'" & SPIDCREFNO & "','" & PayorEmail & "'," &
    '                  "'" & TYPE & "','PAYMENT'," &
    '                  "'" & Gateway_Selected & "','Description=" & TYPE & " PAYMENT;" & numdesc & "=" & acctno & ";FeeAmount=" & String.Format("{0:n}", _FEEAMOUNT) & ";TotalAmount=" & String.Format("{0:n}", _TOTAMOUNT) & ";Payment Option=" & Gateway_Selected & ";Status=" & Status & ";" & gwrn & "'," &
    '                  "GETDATE(),'" & Status & "'" &
    '                  ")"
    '        Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
    '        _nSqlCommand.ExecuteNonQuery()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Sub Insert_History2(ByVal Status As String, Optional ByVal GateWayRefNo As String = Nothing)
    '    '  PayNow2.SpidcRefNo, PayNow2.TransactionType, PayNow2.Gateway_Selected, PayNow2.ACCTNO, PayNow2.RawAmount, PayNow2.OtherFee, PayNow2.TotalAmount, PayNow2.SpidcFEE, "PENDING"
    '    Try
    '        Dim _nQuery As String = Nothing
    '        Dim numdesc As String = Nothing
    '        Dim gwrn As String = Nothing
    '        If String.IsNullOrEmpty(GateWayRefNo) Then
    '            gwrn = Nothing
    '        Else
    '            gwrn = "GATEWAY REFNO=" & GateWayRefNo & ";"
    '        End If

    '        Select Case PayNow2.TransactionType
    '            Case "RPT"
    '                numdesc = "TDN/AssessNo"
    '            Case "BP"
    '                numdesc = "BIN"
    '            Case "OBO"
    '                numdesc = "APPNO"
    '            Case "CTC"
    '                numdesc = "CtrlNo"
    '            Case "LCR"
    '                numdesc = "RegNo"
    '        End Select

    '        Dim _RAWAMOUNT As Double = String.Format("{0:0.##}", PayNow2.RawAmount)
    '        Dim _FEEAMOUNT As Double = String.Format("{0:0.##}", PayNow2.OtherFee)
    '        Dim _TOTAMOUNT As Double = String.Format("{0:0.##}", PayNow2.TotalAmount)

    '        _nQuery = "INSERT INTO HISTORY VALUES(" &
    '                  "'" & PayNow2.SpidcRefNo & "','" & PayNow2.Email & "'," &
    '                  "'" & PayNow2.TransactionType & "','PAYMENT'," &
    '                  "'" & PayNow2.Gateway_Selected & "','Description=" & PayNow2.TransactionType & " PAYMENT;" & numdesc & "=" & PayNow2.ACCTNO & ";BillingAmount=" & String.Format("{0:n}", _RAWAMOUNT) & ";FeeAmount=" & String.Format("{0:n}", _FEEAMOUNT) & ";TotalAmount=" & String.Format("{0:n}", _TOTAMOUNT) & ";Payment Option=" & PayNow2.Gateway_Selected & ";Status=" & Status & ";" & gwrn & "'," &
    '                  "GETDATE(),'" & Status & "'" &
    '                  ")"
    '        Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
    '        _nSqlCommand.ExecuteNonQuery()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Sub Update_History(ByVal SPIDCREFNO As String, ByVal TYPE As String, ByVal Gateway_Selected As String, ByVal acctno As String, ByVal TOTAMOUNT As String, ByVal FEEAMOUNT As String, ByVal Status As String, Optional ByVal GateWayRefNo As String = Nothing, Optional ByRef err As String = Nothing)
    '    Try
    '        Dim _nQuery As String = Nothing
    '        Dim numdesc As String = Nothing
    '        Dim gwrn As String = Nothing

    '        If String.IsNullOrEmpty(GateWayRefNo) Then
    '            gwrn = Nothing
    '        Else
    '            gwrn = "GATEWAY REFNO=" & GateWayRefNo & ";"
    '        End If

    '        Select Case TYPE
    '            Case "RPT"
    '                numdesc = "TDN/AssessNo"
    '            Case "BP"
    '                numdesc = "BIN"
    '            Case "OBO"
    '                numdesc = "APPNO"
    '            Case "CTC"
    '                numdesc = "CtrlNo"
    '            Case "LCR"
    '                numdesc = "RegNo"
    '        End Select

    '        _nQuery = "UPDATE HISTORY SET " &
    '                  "[ID]='" & SPIDCREFNO & "'," &
    '                  "EMAIL='" & cSessionUser._pUserID & "'," &
    '                  "MODULE='" & TYPE & "'," &
    '                  "[TYPE]='PAYMENT'," &
    '                  "[DESCRIPTION]='" & Gateway_Selected & "'," &
    '                  "PARTICULARS='Description=" & TYPE & " PAYMENT;" & numdesc & "=" & acctno & ";FeeAmount=" & FEEAMOUNT & ";TotalAmount=" & TOTAMOUNT & ";Payment Option=" & Gateway_Selected & ";Status=" & Status & ";" & gwrn & "'," &
    '                  "[DATE]=GETDATE()," &
    '                  "[STATUS]='" & Status & "' " &
    '                  " WHERE ID='" & SPIDCREFNO & "'"
    '        Dim _nSqlCommand As New SqlCommand(_nQuery, _mSqlCon)
    '        _nSqlCommand.ExecuteNonQuery()
    '    Catch ex As Exception
    '        err = ";Update_History:" & ex.Message
    '    End Try
    'End Sub

    Public Function Get_PaymentDetails(ByVal ParamName As String, ByVal whereParam As String, ByVal whereValue As String) As String
        Dim Details As String = Nothing
        Try
            Dim _nQuery As String = " Select [" & ParamName & "] from OnlinePaymentRefNo where [" & whereParam & "]  = '" & whereValue & "'"
            Dim _nSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            _mSqlDataReader = _nSqlCommand.ExecuteReader
            If _mSqlDataReader.HasRows Then
                Dim ctr As Integer = 0
                Do Until _mSqlDataReader.Read = False
                    Details = _mSqlDataReader(ParamName)
                Loop
            Else
                Details = Nothing
            End If
            _mSqlDataReader.Close()
            _nSqlCommand.Dispose()
        Catch ex As Exception
            Details = Nothing
        End Try

        Return Details
    End Function

    Public Function Get_Details(ByVal TableName As String, ByVal ParamName As String, ByVal whereParam As String, ByVal whereValue As String) As String
        Dim Details As String = Nothing
        Try
            Dim _nQuery As String = " Select [" & ParamName & "] from [" & TableName & "] where [" & whereParam & "]  = '" & whereValue & "'"
            Dim _nSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            _mSqlDataReader = _nSqlCommand.ExecuteReader
            If _mSqlDataReader.HasRows Then
                Dim ctr As Integer = 0
                Do Until _mSqlDataReader.Read = False
                    Details = _mSqlDataReader(ParamName)
                Loop
            Else
                Details = Nothing
            End If
            _mSqlDataReader.Close()
            _nSqlCommand.Dispose()
        Catch ex As Exception
            Details = Nothing
        End Try

        Return Details
    End Function

    Public Function Get_EPS_ERRCODES(ByVal CODE As String) As String
        Dim ErrDesc As String = Nothing
        Try
            Dim _nQuery As String = " Select * from LBP_EpsErrorCode where ErrorCode = '" & CODE & "'"
            Dim _nSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            _mSqlDataReader = _nSqlCommand.ExecuteReader
            If _mSqlDataReader.HasRows Then
                Dim ctr As Integer = 0
                Do Until _mSqlDataReader.Read = False
                    ErrDesc = _mSqlDataReader("Description")
                Loop
            Else
                ErrDesc = Nothing
            End If
            _mSqlDataReader.Close()
            _nSqlCommand.Dispose()
        Catch ex As Exception
            ErrDesc = Nothing
        End Try

        Return ErrDesc
    End Function

    Public Function get_OriginURL(ByVal SPIDCREFNO As String) As String
        Dim URL As String = Nothing
        Dim type As String = String.Concat(SPIDCREFNO.Where(AddressOf Char.IsLetter))
        If type.Contains("RPT") Or type.Contains("BP") Or type.Contains("CTC") Then
            URL = "WebPortal/Account.aspx"
        ElseIf type = "OBO" Then
            URL = "Engineering/Webportal/ApplicationPermit.aspx"
        ElseIf type = "LCR" Then
            URL = "LCR/WebPortal/ApplicationCertificate.aspx"
        End If
        Return URL
    End Function

    Public Function RPT_HasPayment(ByVal CODE As String) As String
        Dim ErrDesc As String = Nothing
        Try
            Dim _nQuery As String = " Select * from LBP_EpsErrorCode where ErrorCode = '" & CODE & "'"
            Dim _nSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            _mSqlDataReader = _nSqlCommand.ExecuteReader
            If _mSqlDataReader.HasRows Then
                Dim ctr As Integer = 0
                Do Until _mSqlDataReader.Read = False
                    ErrDesc = _mSqlDataReader("Description")
                Loop
            Else
                ErrDesc = Nothing
            End If
            _mSqlDataReader.Close()
            _nSqlCommand.Dispose()
        Catch ex As Exception
            ErrDesc = Nothing
        End Try

        Return ErrDesc
    End Function

    'Function hasPayment_RPT(ByVal tdn As String, ByVal qtr As Integer, ByVal yr As Integer) As Boolean
    '    Dim hasRow As Boolean = False
    '    Try
    '        Dim _nQuery As String =
    '            " select oaims.TXNREFNO,rpt.TDN,rpt.AssessNo,StatusID,RPT.SetQtr,RPT.SetYear,oaims.totAmount from [OnlinePaymentRefno] OAIMS " &
    '            " inner join [" & cGlobalConnections._pSqlCxn_RPTIMS.Database & "].[dbo].[Assess_FrmNewBilling_A] RPT on OAIMS.ACCTNO = rpt.AssessNo" &
    '            " where RPT.tdn='" & tdn & "' and rpt.SetQtr >= '" & qtr & "' and rpt.SetYear='" & yr & "'  and oaims.StatusID='SUCCESS' "
    '        Dim _nSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
    '        _mSqlDataReader = _nSqlCommand.ExecuteReader
    '        If _mSqlDataReader.HasRows Then
    '            hasRow = True
    '        Else
    '            hasRow = False
    '        End If
    '        _mSqlDataReader.Close()
    '        _nSqlCommand.Dispose()
    '    Catch ex As Exception
    '        hasRow = False
    '    End Try
    '    Return hasRow
    'End Function

    Public Function Get_Date(Optional ByVal _Format As String = Nothing) As String
        Dim dt As String = Nothing
        Try
            Dim _nQuery As String
            If String.IsNullOrEmpty(_Format) Then
                _nQuery = " Select getdate()dt"
            Else
                _nQuery = " Select format(getdate(),'" & _Format & "')dt"
            End If
            Dim _nSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            _mSqlDataReader = _nSqlCommand.ExecuteReader
            If _mSqlDataReader.HasRows Then
                Do Until _mSqlDataReader.Read = False
                    dt = _mSqlDataReader("dt")
                Loop
            End If
            _mSqlDataReader.Close()
            _nSqlCommand.Dispose()
        Catch ex As Exception

        End Try
        Return dt
    End Function

    Public Function Get_ServerDate() As Date
        Dim dt As String = Nothing
        Try
            Dim _nQuery As String
            _nQuery = "SELECT CONVERT(CHAR, GETDATE(), 101)dt"

            Dim _nSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            _mSqlDataReader = _nSqlCommand.ExecuteReader
            If _mSqlDataReader.HasRows Then
                Do Until _mSqlDataReader.Read = False
                    dt = _mSqlDataReader("dt")
                Loop
            End If
            _mSqlDataReader.Close()
            _nSqlCommand.Dispose()
        Catch ex As Exception

        End Try
        Return dt
    End Function

    Public Function Get_GateWayUsed(ByVal RefNo As String) As String
        Dim GateWay As String = Nothing
        Try
            Dim _nQuery As String = " Select PaymentChannel from OnlinePaymentRefNo where TXNREFNO = '" & RefNo & "'"
            Dim _nSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            _mSqlDataReader = _nSqlCommand.ExecuteReader
            If _mSqlDataReader.HasRows Then
                Do Until _mSqlDataReader.Read = False
                    GateWay = _mSqlDataReader("PaymentChannel")
                Loop
            End If
            _mSqlDataReader.Close()
            _nSqlCommand.Dispose()
        Catch ex As Exception
        End Try
        Return GateWay
    End Function


    'Public Function PaymentAttemptFound(ByRef TDN As String, ByRef WEBASSESSNO As String, ByRef TXNREFNO As String, ByRef Amount As String) As Boolean
    '    Dim result As Boolean = False
    '    Try
    '        Dim _xQuery As String = _
    '        " select top 1 AA.tdn,OPR.ACCTNO,OPR.TXNREFNO,OPR.rawAmount from OnlinePaymentRefno OPR" &
    '        " inner join [" & cGlobalConnections._pSqlCxn_RPTIMS.Database & "].dbo.Assess_FrmNewBilling_A AA on OPR.ACCTNO=AA.AssessNo" &
    '        " where  aa.tdn=" & cSessionLoader._pTDN & "" &
    '        " and year(aa.DateBilled) = year(getdate())" &
    '        " and month(aa.DateBilled) = MONTH(getdate())" &
    '        " and (opr.statusID <> 'FAILED' or opr.statusID <> 'SUCCESS')  " &
    '        " order by TransDate desc"
    '        _mSqlCommand = New SqlCommand(_xQuery, _mSqlCon)
    '        _mSqlDataReader = _mSqlCommand.ExecuteReader
    '        If _mSqlDataReader.HasRows Then
    '            result = True
    '            Do Until _mSqlDataReader.Read = False
    '                TDN = _mSqlDataReader("TDN")
    '                WEBASSESSNO = _mSqlDataReader("ACCTNO")
    '                TXNREFNO = _mSqlDataReader("TXNREFNO")
    '                Amount = _mSqlDataReader("rawAmount")
    '            Loop
    '        End If
    '        _mSqlDataReader.Close()
    '        _mSqlCommand.Dispose()
    '    Catch ex As Exception
    '        result = False
    '    End Try
    '    Return result
    'End Function



    Public Sub Get_GcashLog(ByVal SPIDCREFNO As String, ByRef acquirementId As String, ByRef shortTransId As String)
        Try
            Dim _nQuery As String = _
               " select distinct transactionId ,merchantTransId,acquirementStatus from Gcash_Transactions" &
               " where transactionID <> '' and transactionId <> '_transactionId'" &
               " and acquirementStatus <> 'CLOSED'" &
               " and acquirementStatus <> 'SUCCESS'" &
               " and merchanttransid='" & SPIDCREFNO & "'"
            Dim _nSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            _mSqlDataReader = _nSqlCommand.ExecuteReader
            If _mSqlDataReader.HasRows Then
                Do Until _mSqlDataReader.Read = False
                    acquirementId = _mSqlDataReader("acquirementStatus")
                    shortTransId = _mSqlDataReader("transactionId")
                Loop
            End If
            _mSqlDataReader.Close()
            _nSqlCommand.Dispose()
        Catch ex As Exception
        End Try

    End Sub


    Public Sub _pSubVoidPreviousTransaction(ByVal WebAssessNo As String, ByVal TXNREFNO As String, Optional ByRef err As String = Nothing)
        Try

            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            _nQuery = "update OnlinePAymentRefNo set" _
                                 & " ACCTNO='" & WebAssessNo & "x'" _
                                 & " where TXNREFNO='" & TXNREFNO & "' and ACCTNO  = '" & WebAssessNo & "'"

            _mSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
            _mSqlCommand.ExecuteNonQuery()

        Catch ex As Exception
            err = ex.Message
        End Try
    End Sub


    'Public Sub Get_PayNowDetails(ByVal SPIDCREFNO As String, ByVal Email As String)
    '    Try
    '        Dim _nQuery As String = _



    '           " select distinct transactionId ,merchantTransId,acquirementStatus from Gcash_Transactions" &
    '           " where transactionID <> '' and transactionId <> '_transactionId'" &
    '           " and acquirementStatus <> 'CLOSED'" &
    '           " and acquirementStatus <> 'SUCCESS'" &
    '           " and merchanttransid='" & SPIDCREFNO & "'"
    '        Dim _nSqlCommand = New SqlCommand(_nQuery, _mSqlCon)
    '        _mSqlDataReader = _nSqlCommand.ExecuteReader
    '        If _mSqlDataReader.HasRows Then
    '            Do Until _mSqlDataReader.Read = False
    '                acquirementId = _mSqlDataReader("acquirementStatus")
    '                shortTransId = _mSqlDataReader("transactionId")
    '            Loop
    '        End If
    '        _mSqlDataReader.Close()
    '        _nSqlCommand.Dispose()
    '    Catch ex As Exception
    '    End Try

    'End Sub


#End Region

End Class
