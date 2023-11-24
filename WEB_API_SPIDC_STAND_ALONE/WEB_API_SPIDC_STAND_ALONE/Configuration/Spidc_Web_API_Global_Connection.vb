#Region "Imports"
Imports System.Data.SqlClient
'Imports VS2014.CL.CR.cEventLog
Imports System.Reflection.MethodBase

#End Region

Public Class Spidc_Web_API_Global_Connection

#Region "Variable"

    Private Shared _mSqlCxn_Templates As New SqlConnection

    '----------------------------------
    'Web Database.
    Private Shared _mStrCxn_CR As String
    Private Shared _mSqlCxn_CR As New SqlConnection

    'Added 20190620 By louie
    Private Shared _mStrCR_ServerName As String
    Private Shared _mStrCR_ID As String
    Private Shared _mStrCR_Pass As String
    Private Shared _mStrCR_DBName As String

    'Windows Form Database.112	
    Private Shared _mStrCxn_LGU As String
    Private Shared _mSqlCxn_LGU As New SqlConnection

    '----------------------------------
    'Web Application Database.
    Private Shared _mStrCxn_OAIMS As String
    Private Shared _mSqlCxn_OAIMS As New SqlConnection

    '----------------------------------
    'Windows Form Database.
    Private Shared _mStrCxn_TOIMS As String
    Private Shared _mSqlCxn_TOIMS As New SqlConnection

    '----------------------------------
    'Windows Form Database.
    Private Shared _mStrCxn_RPTAS As String
    Private Shared _mSqlCxn_RPTAS As New SqlConnection

    '----------------------------------
    'Web Application Database.
    Private Shared _mStrCxn_RPTIMS As String
    Private Shared _mSqlCxn_RPTIMS As New SqlConnection

    'LCR Onpremise Database.
    Private Shared _mStrCxn_LCR As String
    Private Shared _mSqlCxn_LCR As New SqlConnection

    'LCR  'Web Application Database.
    Private Shared _mStrCxn_LCRIMS As String
    Private Shared _mSqlCxn_LCRIMS As New SqlConnection

    'GAAMS  'Web Application Database.
    Private Shared _mStrCxn_GAAMS As String
    Private Shared _mSqlCxn_GAAMS As New SqlConnection


    'GAAMS  'Web Application Database.
    Private Shared _mStrCxn_TIMS As String
    Private Shared _mSqlCxn_TIMS As New SqlConnection


    'FAMS  'Web Application Database.
    Private Shared _mStrCxn_FAMS As String
    Private Shared _mSqlCxn_FAMS As New SqlConnection


    'Spidc Web Api Database.
    Private Shared _mStrCxn_SPIDC_Web_API As String
    Private Shared _mSqlCxn_SPIDC_Web_API As New SqlConnection

#End Region

#Region "Property CR"
    Public Shared ReadOnly Property _pStrCxn_CR() As String
        Get
            _mSubGetConnectionString_CR()
            'TODO: Encrypt Connection String 
            Return _mStrCxn_CR
        End Get
    End Property

    Public Shared ReadOnly Property _pStrCxn_LGU() As String
        Get
            Return _mSubGetConnectionString("LGU")
        End Get
    End Property
    Public Shared ReadOnly Property _pSqlCxn_LGU() As SqlConnection
        Get
            Try
                '----------------------------------
                If _mSqlCxn_LGU.State = ConnectionState.Closed Then
                    _mSqlCxn_LGU.ConnectionString = _pStrCxn_LGU
                    _mSqlCxn_LGU.Open()
                End If

                Return _mSqlCxn_LGU
                '----------------------------------
            Catch ex As Exception
                '_pSubEventLog(ex, 2)
                Return Nothing
            End Try
        End Get
    End Property

    Public Shared ReadOnly Property _pSqlCxn_CR() As SqlConnection
        Get
            Try
                '----------------------------------
                'TODO: Try detecting if connection is broken, closed, fetching..etc..

                If _mSqlCxn_CR.State = ConnectionState.Closed Then
                    _mSqlCxn_CR = New SqlConnection
                    _mSqlCxn_CR.ConnectionString = _pStrCxn_CR
                    _mSqlCxn_CR.Open()
                Else
                    _mSqlCxn_CR.Close()
                    _mSqlCxn_CR = New SqlConnection
                    _mSqlCxn_CR.ConnectionString = _pStrCxn_CR
                    _mSqlCxn_CR.Open()

                End If

                Return _mSqlCxn_CR
                '----------------------------------
            Catch ex As Exception
                '_pSubEventLog(ex, 2)
                Return Nothing
            End Try
        End Get
    End Property

    ' Added 20190620 By louie
    '========================================================================
    Public Shared ReadOnly Property _pStrCR_ServerName() As String
        Get
            Return _mStrCR_ServerName
        End Get
    End Property

    Public Shared ReadOnly Property _pStrCR_ID() As String
        Get
            Return _mStrCR_ID
        End Get
    End Property

    Public Shared ReadOnly Property _pStrCR_Pass() As String
        Get
            Return _mStrCR_Pass
        End Get
    End Property

    Public Shared ReadOnly Property _pStrCR_DBName() As String
        Get
            Return _mStrCR_DBName
        End Get
    End Property
    '========================================================================

#End Region

#Region "Property OAIMS"

    Public Shared ReadOnly Property _pStrCxn_OAIMS() As String
        Get
            Return _mSubGetConnectionString("OAIMS")
        End Get
    End Property
    Public Shared ReadOnly Property _pSqlCxn_OAIMS() As SqlConnection
        Get
            Try
                '----------------------------------
                'If _mSqlCxn_OAIMS.State <> ConnectionState.Closed And _mSqlCxn_OAIMS.State.HasFlag(ConnectionState.Open) Then
                '    Return _mSqlCxn_OAIMS
                'Else
                '    _mSqlCxn_OAIMS.ConnectionString = _pStrCxn_OAIMS
                '    _mSqlCxn_OAIMS.Open()
                'End If


                If _mSqlCxn_OAIMS.State = ConnectionState.Closed Then
                    _mSqlCxn_OAIMS.ConnectionString = _pStrCxn_OAIMS
                    _mSqlCxn_OAIMS.Open()
                End If

                Return _mSqlCxn_OAIMS
                '----------------------------------
            Catch ex As Exception
                '_pSubEventLog(ex, 2)
                Return Nothing
            End Try
        End Get
    End Property

#End Region

#Region "Property TOIMS"

    Public Shared ReadOnly Property _pStrCxn_TOIMS() As String
        Get
            Return _mSubGetConnectionString("TOIMS")
        End Get
    End Property
    Public Shared ReadOnly Property _pSqlCxn_TOIMS() As SqlConnection
        Get
            Try
                '----------------------------------

                If _mSqlCxn_TOIMS.State = ConnectionState.Closed Then
                    _mSqlCxn_TOIMS.ConnectionString = _pStrCxn_TOIMS
                    _mSqlCxn_TOIMS.Open()
                End If

                Return _mSqlCxn_TOIMS
                '----------------------------------
            Catch ex As Exception
                '_pSubEventLog(ex, 2)
                Return Nothing
            End Try
        End Get
    End Property

#End Region

#Region "Property RPTAS"

    Public Shared ReadOnly Property _pStrCxn_RPTAS() As String
        Get
            Return _mSubGetConnectionString("RPTAS")
        End Get
    End Property
    Public Shared ReadOnly Property _pSqlCxn_RPTAS() As SqlConnection
        Get
            Try
                '----------------------------------
                If _mSqlCxn_RPTAS.State = ConnectionState.Closed Then
                    _mSqlCxn_RPTAS.ConnectionString = _pStrCxn_RPTAS
                    _mSqlCxn_RPTAS.Open()
                End If

                Return _mSqlCxn_RPTAS
                '----------------------------------
            Catch ex As Exception
                '_pSubEventLog(ex, 2)
                Return Nothing
            End Try
        End Get
    End Property

#End Region

#Region "Property LCR"

    Public Shared ReadOnly Property _pStrCxn_LCR() As String
        Get
            Return _mSubGetConnectionString("LCR")
        End Get
    End Property
    Public Shared ReadOnly Property _pSqlCxn_LCR() As SqlConnection
        Get
            Try
                '----------------------------------
                If _mSqlCxn_LCR.State = ConnectionState.Closed Then
                    _mSqlCxn_LCR.ConnectionString = _pStrCxn_LCR
                    _mSqlCxn_LCR.Open()
                End If

                Return _mSqlCxn_LCR
                '----------------------------------
            Catch ex As Exception
                '_pSubEventLog(ex, 2)
                Return Nothing
            End Try
        End Get
    End Property

#End Region

#Region "Property LCRIMS"

    Public Shared ReadOnly Property _pStrCxn_LCRIMS() As String
        Get
            Return _mSubGetConnectionString("LCRIMS")
        End Get
    End Property
    Public Shared ReadOnly Property _pSqlCxn_LCRIMS() As SqlConnection
        Get
            Try
                '----------------------------------
                If _mSqlCxn_LCRIMS.State = ConnectionState.Closed Then
                    _mSqlCxn_LCRIMS.ConnectionString = _pStrCxn_LCRIMS
                    _mSqlCxn_LCRIMS.Open()
                End If

                Return _mSqlCxn_LCRIMS
                '----------------------------------
            Catch ex As Exception
                '_pSubEventLog(ex, 2)
                Return Nothing
            End Try
        End Get
    End Property

#End Region


#Region "Property FAMS"

    Public Shared ReadOnly Property _pStrCxn_FAMS() As String
        Get
            Return _mSubGetConnectionString("LCR")
        End Get
    End Property
    Public Shared ReadOnly Property _pSqlCxn_FAMS() As SqlConnection
        Get
            Try
                '----------------------------------
                If _mSqlCxn_FAMS.State = ConnectionState.Closed Then
                    _mSqlCxn_FAMS.ConnectionString = _pStrCxn_FAMS
                    _mSqlCxn_FAMS.Open()
                End If

                Return _mSqlCxn_FAMS
                '----------------------------------
            Catch ex As Exception
                '_pSubEventLog(ex, 2)
                Return Nothing
            End Try
        End Get
    End Property

#End Region

#Region "Property GAAMS"

    Public Shared ReadOnly Property _pStrCxn_GAAMS() As String
        Get
            Return _mSubGetConnectionString("GAAMS")
        End Get
    End Property
    Public Shared ReadOnly Property _pSqlCxn_GAAMS() As SqlConnection
        Get
            Try
                '----------------------------------
                If _mSqlCxn_GAAMS.State = ConnectionState.Closed Then
                    _mSqlCxn_GAAMS.ConnectionString = _pStrCxn_GAAMS
                    _mSqlCxn_GAAMS.Open()
                End If

                Return _mSqlCxn_GAAMS
                '----------------------------------
            Catch ex As Exception
                '_pSubEventLog(ex, 2)
                Return Nothing
            End Try
        End Get
    End Property

#End Region

#Region "Property TIMS"

    Public Shared ReadOnly Property _pStrCxn_TIMS() As String
        Get
            Return _mSubGetConnectionString("TIMS")
        End Get
    End Property
    Public Shared ReadOnly Property _pSqlCxn_TIMS() As SqlConnection
        Get
            Try
                '----------------------------------
                If _mSqlCxn_TIMS.State = ConnectionState.Closed Then
                    _mSqlCxn_TIMS.ConnectionString = _pStrCxn_TIMS
                    _mSqlCxn_TIMS.Open()
                End If

                Return _mSqlCxn_TIMS
                '----------------------------------
            Catch ex As Exception
                '_pSubEventLog(ex, 2)
                Return Nothing
            End Try
        End Get
    End Property

#End Region



#Region "Property SPIC Web API"

    Public Shared ReadOnly Property _pStrCxn_SPIDC_Web_API() As String
        Get
            Return _mSubGetConnectionString("Web_API")
        End Get
    End Property
    Public Shared ReadOnly Property _pSqlCxn_SPIDC_Web_API() As SqlConnection
        Get
            Try
                '----------------------------------
                If _mSqlCxn_SPIDC_Web_API.State = ConnectionState.Closed Then
                    _mSqlCxn_SPIDC_Web_API.ConnectionString = _pStrCxn_SPIDC_Web_API
                    _mSqlCxn_SPIDC_Web_API.Open()
                End If

                Return _mSqlCxn_SPIDC_Web_API
                '----------------------------------
            Catch ex As Exception
                '_pSubEventLog(ex, 2)
                Return Nothing
            End Try
        End Get
    End Property

#End Region


#Region "Routine"
    Private Shared Sub _mSubGetConnectionString_CR()
        Try
            '----------------------------------
            'Hard-coded Connection String for "CR" database.
            'Hard-coded Machine Names.
            Dim _nStringConnection As String = Nothing
            Dim _nClass As New cHardwareInformation
            Dim _nMachineName As String = Nothing
            Dim _nMachineIP As String = Nothing

            _nMachineName = _nClass._pMachineName.ToUpper
            _nMachineIP = _nClass._pMachineIP

            'Server Names should be in Upper Casing.

            Select Case _nMachineName
                Case "HAVANA"
                    Dim curr_url As String = HttpContext.Current.Request.Url.AbsoluteUri
                    If curr_url.ToUpper.Contains("SPIDC_WEB_API") = True Then
                        _nStringConnection = _FnGetCRCon("SOS_SPIDC_WEB_API")
                    End If
                Case "WEBAPP"
                    '_nStringConnection = _FnGetCRCon("SOS_LCR_CAVITE_LIVE")
                    _nStringConnection = _FnGetCRCon("SOS_SPIDC_WEB_API_LIVE")
                Case Else
                    '_nStringConnection = _FnGetCRCon("SOS_LCR") 'localhost
                    _nStringConnection = _FnGetCRCon("SOS_SPIDC_WEB_API") 'localhost


            End Select

            _mStrCxn_CR = _nStringConnection
            '----------------------------------
        Catch ex As Exception
            '_pSubEventLog(ex, 2)
            _mStrCxn_CR = Nothing
        End Try
    End Sub

    Private Shared Function _mSubGetConnectionString(_nCode As String)
        Try
            '----------------------------------

            Dim _nConnectionString As String = Nothing

            Dim _nClass As New Spidc_Web_API_GlobalConnection_Default

            _nClass._pCxn = _pSqlCxn_CR
            _nClass._pSetupGlobalConnectionsDatabases = _nCode
            _nClass._pSubRecordSelectSpecific()

            _nConnectionString =
                "Data Source=" & _nClass._pDBDataSource & _
                ";Initial Catalog=" & _nClass._pDBInitialCatalog & _
                ";User ID=" & _nClass._pDBUserID & _
                ";Password=" & _nClass._pDBUserKey & _
                ";MultipleActiveResultSets=True"
            Return _nConnectionString
            '----------------------------------
        Catch ex As Exception
            ''_pSubEventLog(ex, 2)
            Return Nothing
        End Try
    End Function

    Private Shared Function _FnGetCRCon(ByVal xServerName As String) As String
        Try
            _FnGetCRCon = Nothing

            Select Case xServerName
                Case "SOS_SPIDC_WEB_API"
                    _mStrCR_ServerName = "HAVANA\MSSQL2019"
                    _mStrCR_ID = "sa"
                    _mStrCR_Pass = "P@ssw0rd"
                    _mStrCR_DBName = "SOS_CR_CALOOCAN_20230928"
                Case "SOS_SPIDC_WEB_API_LIVE"
                    _mStrCR_ServerName = "WEBAPP\MSSQLSERVER2019"
                    _mStrCR_ID = "spidcweb"
                    _mStrCR_Pass = "#P@ssw0rd"
                    _mStrCR_DBName = "SOS_CR_CALOOCAN_20230928"
            End Select


            _FnGetCRCon = "Data Source=" & _mStrCR_ServerName & _
                            ";Initial Catalog=" & _mStrCR_DBName & _
                            ";User ID=" & _mStrCR_ID & _
                            ";Password=" & _mStrCR_Pass & _
                            ";MultipleActiveResultSets=True"
            Return _FnGetCRCon
        Catch ex As Exception
            _FnGetCRCon = Nothing
        End Try

    End Function

#End Region

End Class
