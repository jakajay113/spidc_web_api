Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.Threading.Tasks
Imports System.Threading
Imports RestSharp
Imports System.Web.Script.Serialization
Imports System.Net

Imports System.Web.Services
Imports System.Web.HttpContext


Public Class EorPostingDataAccessLayer





#Region "Variable Data Access Layer Universal Checkout Web API"
    Public Shared _mSqlCon As New SqlConnection
    Public Shared _mSqlCmd As SqlCommand
    Public Shared _mDataTable As New DataTable
    Public Shared _mDataAdapter As New SqlDataAdapter
    Public Shared _mDataAdapter1 As New SqlDataAdapter
    Public Shared _mDataset As New DataSet
    Public Shared _mDataset1 As New DataSet

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



    '--------------------------------------


    Private Const _sscPrefix As String = "EorPostingDataAccessLayer."
    Private Const __mStrSql As String = _sscPrefix & "__mStrSql"

    Shared Property _mStrSql() As String
        Get
            Return Current.Session(__mStrSql)
        End Get
        Set(ByVal value As String)
            Current.Session(__mStrSql) = value
        End Set
    End Property








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
    Public Shared Property _pSqlCon() As SqlConnection
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






    Public Shared Function Execute_other_TOIMS_Data() As Boolean

        Try

            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            Using _nSqlDataReader As SqlDataReader = _mSqlCmd.ExecuteReader

                Try
                    While _nSqlDataReader.Read()
                        If _nSqlDataReader.HasRows Then
                            EorPostingModel.form = _nSqlDataReader("Form").ToString
                            EorPostingModel.srs = _nSqlDataReader("srs").ToString
                            EorPostingModel.BookNo = _nSqlDataReader("Book_No").ToString
                            EorPostingModel.user = _nSqlDataReader("User").ToString
                        End If
                    End While
                Finally
                    _nSqlDataReader.Close()
                End Try

            End Using

            Return True

        Catch ex As Exception

            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "get_Other_TOIMS_data", ex.ToString())
            Return False

        End Try


        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function



    Public Shared Function Execute_get_new_ORNO() As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            Using _nSqlDataReader As SqlDataReader = _mSqlCmd.ExecuteReader
                Try
                    While _nSqlDataReader.Read()
                        If _nSqlDataReader.HasRows Then
                            EorPostingModel.OR_No = _nSqlDataReader("Or_No").ToString
                        End If
                    End While
                Finally
                    _nSqlDataReader.Close()
                End Try
            End Using

            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "get_new_ORNO", ex.ToString())
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function



    Public Shared Function Execute_get_Agency() As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            Using _nSqlDataReader As SqlDataReader = _mSqlCmd.ExecuteReader
                Try
                    While _nSqlDataReader.Read()
                        If _nSqlDataReader.HasRows Then
                            EorPostingModel.agency = _nSqlDataReader("Agency").ToString
                        End If
                    End While
                Finally
                    _nSqlDataReader.Close()
                End Try
            End Using

            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "get_Agency", ex.ToString())
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function



    Public Shared Function Execute_get_other_TOIMS_Data2() As Boolean

        Try

            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            Using _nSqlDataReader As SqlDataReader = _mSqlCmd.ExecuteReader

                Try
                    While _nSqlDataReader.Read()
                        If _nSqlDataReader.HasRows Then
                            EorPostingModel.TIN = _nSqlDataReader("TIN").ToString
                            EorPostingModel.Height = _nSqlDataReader("Height").ToString
                            EorPostingModel.Weight = _nSqlDataReader("Weight").ToString
                            EorPostingModel.Gender = _nSqlDataReader("Gender").ToString
                            EorPostingModel.Birth_Place = _nSqlDataReader("BirthPlace").ToString
                            EorPostingModel.BirthDate = _nSqlDataReader("BirthDate").ToString
                            EorPostingModel.Civil_Status = _nSqlDataReader("CivilStatus").ToString
                            EorPostingModel.Citizenship = _nSqlDataReader("Citizenship").ToString
                            EorPostingModel.BrgyCode = _nSqlDataReader("Barangay").ToString
                        End If
                    End While
                Finally
                    _nSqlDataReader.Close()
                End Try

            End Using


            '-------------------------------------------------------------------------


            If EorPostingModel.Civil_Status = "Single" Then
                EorPostingModel.Civil_Status = "0"
            ElseIf EorPostingModel.Civil_Status = "Married" Then
                EorPostingModel.Civil_Status = "1"
            ElseIf EorPostingModel.Civil_Status = "Widow/Widower/Legally Separted" Then
                EorPostingModel.Civil_Status = "2"
            ElseIf EorPostingModel.Civil_Status = "Divorced" Then
                EorPostingModel.Civil_Status = "3"
            End If


            If EorPostingModel.Gender = "M" Or EorPostingModel.Gender = "Male" Then
                EorPostingModel.Gender = "1"
            ElseIf EorPostingModel.Gender = "F" Or EorPostingModel.Gender = "Female" Then
                EorPostingModel.Gender = "0"
            End If


            If String.IsNullOrEmpty(EorPostingModel.TIN) Then

            Else

                Dim stringWithoutHyphens As String = EorPostingModel.TIN.Replace("-", "")
                ' Check if the string length is at least 12 characters before attempting to remove "000"
                If stringWithoutHyphens.Length >= 12 Then
                    ' Remove "000" from the 10th to 12th positions
                    EorPostingModel.TIN = stringWithoutHyphens.Remove(9, 3)
                Else
                    EorPostingModel.TIN = stringWithoutHyphens
                End If

            End If


            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "get_Other_TOIMS_data2", ex.ToString())
            Return False
        End Try


        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function



    Public Shared Function Execute_get_CitizenshipCode() As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            Using _nSqlDataReader As SqlDataReader = _mSqlCmd.ExecuteReader
                Try
                    While _nSqlDataReader.Read()
                        If _nSqlDataReader.HasRows Then
                            EorPostingModel.Citizenship = _nSqlDataReader("Code2").ToString
                        End If
                    End While
                Finally
                    _nSqlDataReader.Close()
                End Try
            End Using

            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "get_CitizenshipCode", ex.ToString())
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function


    Public Shared Function Execute_get_BrgyDesc() As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            Using _nSqlDataReader As SqlDataReader = _mSqlCmd.ExecuteReader
                Try
                    While _nSqlDataReader.Read()
                        If _nSqlDataReader.HasRows Then
                            EorPostingModel.BrgyDesc = _nSqlDataReader("MuniCityBrgyName").ToString
                        End If
                    End While
                Finally
                    _nSqlDataReader.Close()
                End Try
            End Using

            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "get_BrgyDesc", ex.ToString())
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function

    Public Shared Function Execute_get_timeEOR() As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            Using _nSqlDataReader As SqlDataReader = _mSqlCmd.ExecuteReader
                Try
                    While _nSqlDataReader.Read()
                        If _nSqlDataReader.HasRows Then
                            EorPostingModel.timenow = _nSqlDataReader("CurrentServerTime").ToString
                        End If
                    End While
                Finally
                    _nSqlDataReader.Close()
                End Try
            End Using

            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "get_timeEOR", ex.ToString())
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function





    Public Shared Function Execute_get_EORNO() As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            Using _nSqlDataReader As SqlDataReader = _mSqlCmd.ExecuteReader
                Try
                    While _nSqlDataReader.Read()
                        If _nSqlDataReader.HasRows Then
                            EorPostingModel.eORno = _nSqlDataReader("EOR_NO").ToString
                        End If
                    End While
                Finally
                    _nSqlDataReader.Close()
                End Try
            End Using

            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "get_EORNO", ex.ToString())
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function


    Public Shared Function Execute_get_EORNOCOUNT() As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            Using _nSqlDataReader As SqlDataReader = _mSqlCmd.ExecuteReader
                Try
                    While _nSqlDataReader.Read()
                        If _nSqlDataReader.HasRows Then
                            EorPostingModel.count = _nSqlDataReader("Count").ToString
                        End If
                    End While
                Finally
                    _nSqlDataReader.Close()
                End Try
            End Using

            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "get_EORCount", ex.ToString())
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function




    Public Shared Function Execute_Insert_GEN_OR_AND_EXTN() As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            _mSqlCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "Insert_GEN_OR_AND_EXTN", ex.ToString())
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function



    Public Shared Function Execute_EOR_AND_EXTN() As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            _mSqlCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "Insert_EOR_AND_EXTN", ex.ToString())
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function


    Public Shared Function Execute_Update_EOR_COUNT() As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            _mSqlCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "Update_EOR_COUNT", ex.ToString())
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function






    Public Shared Function Execute_Update_EOR_QR_DATA(ByVal qr_file As Byte()) As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)

            With _mSqlCmd.Parameters
                .AddWithValue("@QR_File", qr_file)
            End With

            _mSqlCmd.ExecuteNonQuery()


            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "Update_eOR_QR_Data", ex.ToString())
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function


    Public Shared Function Execute_get_QR_STRING() As String

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            Dim _nSqlDr As SqlDataReader = _mSqlCmd.ExecuteReader
            Using _nSqlDr
                If _nSqlDr.HasRows Then
                    Do While _nSqlDr.Read
                        EorPostingModel.QR_STRING = _nSqlDr("eorno").ToString & vbNewLine _
                               & _nSqlDr("GrandTotal").ToString & vbNewLine _
                               & _nSqlDr("gateway_selected").ToString & vbNewLine _
                               & _nSqlDr("DateTime").ToString
                    Loop
                End If
            End Using

            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "GET_QRstring", ex.ToString())
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function






    Public Shared Function Execute_ERROR_LOGS() As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            _mSqlCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function


    Public Shared Function Execute_Insert_OnlinePaymentRef() As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            _mSqlCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "Insert_OnlinePaymentRef", ex.ToString())
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function




    Public Shared Function Execute_pSubGetEmailMaster()

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            Using _nSqlDataReader As SqlDataReader = _mSqlCmd.ExecuteReader
                Try
                    Do Until _nSqlDataReader.Read = False
                        EorPostingModel._mSenderEmailAddress = _nSqlDataReader("EmailAddress")
                        EorPostingModel._mSenderEmailPassword = _nSqlDataReader("Password")
                        EorPostingModel._mPort = _nSqlDataReader("port")
                        EorPostingModel._mHost = _nSqlDataReader("host")
                        EorPostingModel._mCC = IIf(_nSqlDataReader("EmailCC") = "0", Nothing, _nSqlDataReader("EmailCC"))
                        EorPostingModel._mBCC = IIf(_nSqlDataReader("EmailBCC") = "0", Nothing, _nSqlDataReader("EmailBCC"))
                        EorPostingModel._mSSL = _nSqlDataReader("SSL")
                    Loop
                Finally
                    _nSqlDataReader.Close()
                End Try

            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function





    Public Shared Function Execute_get_Header_DATA(ByRef HEADER_TEMPLATE As Byte(), ByRef HEADER_TEMPLATE_Name As String, ByRef HEADER_TEMPLATE_Ext As String) As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            Using _nSqlDataReader As SqlDataReader = _mSqlCmd.ExecuteReader
                Try
                    Do Until _nSqlDataReader.Read = False
                        HEADER_TEMPLATE = _nSqlDataReader("HEADER_TEMPLATE")
                        HEADER_TEMPLATE_Name = _nSqlDataReader("HEADER_TEMPLATE_Name")
                        HEADER_TEMPLATE_Ext = _nSqlDataReader("HEADER_TEMPLATE_Ext")
                    Loop
                Finally
                    _nSqlDataReader.Close()
                End Try

            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function


    '----------------------------------------------------------------------
    'generate report datatable


    Public Shared Function Execute_Print_Template(ByRef _passTable As DataTable) As Boolean

        Try

            Dim _nSqlDataAdapter As New SqlDataAdapter(_mStrSql, _mSqlCon)
            _nSqlDataAdapter.Fill(_passTable)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function



    Public Shared Function Execute_Print_Report(ByRef _passTable As DataTable) As Boolean

        Try

            Dim _nSqlDataAdapter As New SqlDataAdapter(_mStrSql, _mSqlCon)
            _nSqlDataAdapter.Fill(_passTable)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function




    Public Shared Function Execute_Print_TOP(ByRef _passTable As DataTable) As Boolean

        Try

            Dim _nSqlDataAdapter As New SqlDataAdapter(_mStrSql, _mSqlCon)
            _nSqlDataAdapter.Fill(_passTable)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function



    Public Shared Function Execute_Update_CTC_Online_App_PostStatus() As Boolean

        Try
            _mSqlCmd = New SqlCommand(_mStrSql, _mSqlCon)
            _mSqlCmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            EorPostingModel.ERRORLOGS(EorPostingModel.API_APP_NAME, "Update_CTC_Online_App_PostStatus", ex.ToString())
            Return False
        End Try

        _mSqlCmd.Dispose()
        _mSqlCon.Close()

    End Function



End Class
