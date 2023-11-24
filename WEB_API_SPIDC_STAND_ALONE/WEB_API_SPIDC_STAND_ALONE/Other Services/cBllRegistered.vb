#Region "Imports"

Imports System.Data.SqlClient
Imports System.Reflection.MethodBase
Imports System.Web.UI.WebControls
'Imports IMC.cEventLog
'Imports IMC.cReturnDataType
#End Region


Public Class cBllRegistered


#Region "Variable Data"

    Private Shared _mCxn As SqlConnection = Nothing

#End Region

#Region "Property Data"

    Public WriteOnly Property _pCxn() As SqlConnection
        Set(value As SqlConnection)
            _mCxn = value
        End Set
    End Property

#End Region


#Region "Routine"

    Public Shared Function _pFuncVerifyIfAccountIsRegistered( _
        ByVal _nUserID As String, Optional ByVal xLiteral As String = Nothing _
    ) As Boolean

        'Commented temporarily by Tomi 10/14/2019
        '_pLiteral.Text = xLiteral
        'Dim _nSw As Stopwatch = Stopwatch.StartNew
        '_pSubEventLog("Verify If Account Is Registered" & ":Begin")

        Dim _nReturnValue As Boolean = Nothing
        Try
            '----------------------------------
            Dim _nPrmUserIDExists As Boolean = False
            Dim _nPrmUserType As String = Nothing

            '----------------------------------
            Dim _nDal As New cDalRegistered

            _nDal._pCxn = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS '_mCxn 

            _nDal._pUserID = _nUserID
            _nDal._pSubSelect()

            Using _nSqlDataReader As SqlDataReader = _nDal._pDr
                Dim _iUserType As Integer = _nSqlDataReader.GetOrdinal("UserType")

                If _nSqlDataReader.HasRows Then
                    Do While _nSqlDataReader.Read
                        _nPrmUserType = cReturnDataType._pYieldString(_nSqlDataReader(_iUserType))

                    Loop
                    _nPrmUserIDExists = True
                    'cSessionUser._pUsertype = _nPrmUserType
                End If

            End Using

            _nDal = Nothing
            '----------------------------------
            _nReturnValue = _nPrmUserIDExists
            Return _nReturnValue



        Catch ex As Exception
            '_pSubEventLog(ex, 2)
            ''cEventLog._pSubLogError(ex)
            Return False
        End Try
        'Commented temporarily by Tomi 10/14/2019
        '_pSubEventLog("Verify If Account Is Registered" & ":End")
        '_pSubEventLog(_nSw.Elapsed, 2)


    End Function

    Public Shared Function _pFuncVerifyIfAccountIsActivated( _
        ByVal _nUserID As String, Optional ByVal xLiteral As String = "" _
    ) As Boolean
        'Commented temporarily by Tomi 10/14/2019
        'Dim _nSw As Stopwatch = Stopwatch.StartNew
        '_pSubEventLog("Verify If Account Is Activated" & ":Start")

        Dim _nReturnValue As Boolean = Nothing
        Try
            '----------------------------------
            Dim _nPrmIsActivated As Boolean

            '----------------------------------
            Dim _nQuery As String = Nothing
            _nQuery = _
                "SELECT " & _
                "* " & _
                "FROM " & _
                "[uvw.VS2014.WA.OAIMS.Registerred.Data.Read] " & _
                "WHERE " & _
                "[UserID] = @UserID"

            '----------------------------------
            Using _nCommand As New SqlCommand

                _nCommand.Connection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                _nCommand.CommandText = _nQuery
                _nCommand.CommandType = CommandType.Text

                _nCommand.Parameters.AddWithValue("@UserID", _nUserID)

                Using _nSqlDataReader As SqlDataReader = _nCommand.ExecuteReader

                    Dim _iIsActivated As Integer = _nSqlDataReader.GetOrdinal("IsActivated")

                    If _nSqlDataReader.HasRows Then
                        Do While _nSqlDataReader.Read
                            _nPrmIsActivated = cReturnDataType._pYieldString(_nSqlDataReader(_iIsActivated))
                        Loop

                    End If

                    _nSqlDataReader.Close()
                End Using '_nSqlDataReader

            End Using '_nCommand

            '----------------------------------
            _nReturnValue = _nPrmIsActivated
            'Commented temporarily by Tomi 10/14/2019
            '_pSubEventLog("Verify If Account Is Activated" & ":End")
            '_pSubEventLog(_nSw.Elapsed, 2)
            Return _nReturnValue

            '----------------------------------
        Catch ex As Exception
            '_pSubEventLog(ex, 2)
            ' 'cEventLog._pSubLogError(ex)
            Return False
        End Try
    End Function

    Public Shared Function _pFuncInsertNewAccount( _
        ByVal _nUserID As String _
        , ByVal _nFirstName As String _
        , ByVal _nMiddleName As String _
        , ByVal _nLastName As String _
        , ByVal _nBirthDate As String _
        , ByVal _nSetupGender As String _
        , Optional ByVal xLiteral As String = ""
    ) As Boolean

        'Commented temporarily by Tomi 10/14/2019
        '_pLiteral.Text = xLiteral
        'Dim _nSw As Stopwatch = Stopwatch.StartNew
        '_pSubEventLog("Insert New Account" & ":Begin")

        Dim _nReturnValue As Boolean = Nothing

        Try
            '----------------------------------
            Dim _nQuery As String = Nothing
            _nQuery = _
                "INSERT INTO " & _
                "[uvw.VS2014.WA.OAIMS.Registerred.Data.Write] " & _
                "(UserId, FirstName, LastName, BirthDate, SetupGender) " & _
                "VALUES " & _
                "(@UserId, @FirstName, @LastName, @BirthDate, @SetupGender) "

            '----------------------------------
            Using _nCommand As New SqlCommand

                _nCommand.Connection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                _nCommand.CommandText = _nQuery
                _nCommand.CommandType = CommandType.Text

                _nCommand.Parameters.AddWithValue("@UserID", _nUserID)
                _nCommand.Parameters.AddWithValue("@FirstName", _nFirstName)
                '  _nCommand.Parameters.AddWithValue("@MiddleName", _nMiddleName)
                _nCommand.Parameters.AddWithValue("@LastName", _nLastName)
                _nCommand.Parameters.AddWithValue("@BirthDate", _nBirthDate)
                _nCommand.Parameters.AddWithValue("@SetupGender", _nSetupGender)

                _nReturnValue = _nCommand.ExecuteNonQuery()

            End Using '_nCommand

            '----------------------------------
            Return _nReturnValue

            '----------------------------------
        Catch ex As Exception
            '_pSubEventLog(ex, 2)
            'cEventLog._pSubLogError(ex)
            Return False
        End Try
        'Commented temporarily by Tomi 10/14/2019
        '_pSubEventLog("Insert New Account" & ":End")
        '_pSubEventLog(_nSw.Elapsed, 2)
    End Function

    Public Shared Function _pFuncGetUserInfo(ByVal _nUserID As String, ByRef _nUsertype As String) As Boolean

        Try

            '----------------------------------
            Dim _nQuery As String = Nothing
            _nQuery = _
                "SELECT * FROM REGISTERED WHERE [USERID] = @UserID"
            '"SELECT " & _
            '"* " & _
            '"FROM " & _
            '"[uvw.VS2014.WA.OAIMS.Registerred.Data.Read] " & _

            '"WHERE " & _
            '"[UserID] = @UserID"

            '----------------------------------
            '----------------------------------
            Using _nCommand As New SqlCommand

                _nCommand.Connection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                _nCommand.CommandText = _nQuery
                _nCommand.CommandType = CommandType.Text

                _nCommand.Parameters.AddWithValue("@UserID", _nUserID)

                Using _nSqlDataReader As SqlDataReader = _nCommand.ExecuteReader

                    Dim _iUserType As Integer = _nSqlDataReader.GetOrdinal("USERTYPE")

                    If _nSqlDataReader.HasRows Then
                        Do While _nSqlDataReader.Read
                            _nUsertype = cReturnDataType._pYieldString(_nSqlDataReader(_iUserType))
                        Loop
                        Return True
                    End If

                End Using '_nSqlDataReader

            End Using '_nCommand

        Catch ex As Exception
            'cEventLog._pSubLogError(ex)
            Return False
        End Try

    End Function

    Public Shared Function _pFuncValidateEmail(ByVal _nUserID As String, _nKeyToken As String) As Boolean

        Try
            Dim _nReturnValue As Boolean = Nothing
            '----------------------------------
            Dim _nQuery As String = Nothing
            _nQuery = _
                "SELECT * FROM REGISTERED WHERE [USERID] = @UserID and [KEYTOKEN] = @KeyToken"

            '----------------------------------
            Using _nCommand As New SqlCommand

                _nCommand.Connection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                _nCommand.CommandText = _nQuery
                _nCommand.CommandType = CommandType.Text

                _nCommand.Parameters.AddWithValue("@UserID", _nUserID)
                _nCommand.Parameters.AddWithValue("@KeyToken", _nKeyToken)
                Using _nSqlDataReader As SqlDataReader = _nCommand.ExecuteReader

                    Dim _iUserType As Integer = _nSqlDataReader.GetOrdinal("USERTYPE")

                    If _nSqlDataReader.HasRows Then
                        Return True
                    End If

                End Using '_nSqlDataReader

            End Using '_nCommand
            Return _nReturnValue
        Catch ex As Exception
            'cEventLog._pSubLogError(ex)
            Return False
        End Try

    End Function

    Public Shared Function _pFuncGetKeyToken( _
        ByVal _nUserID As String _
        , ByRef _nKeyToken As String _
    ) As Boolean

        Try
            '----------------------------------
            Dim _nPrmKeyToken As String = Nothing

            '----------------------------------
            Dim _nQuery As String = Nothing
            _nQuery = _
               "SELECT " & _
               "* " & _
               "FROM " & _
               "[uvw.VS2014.WA.OAIMS.Registerred.Data.Read] " & _
               "WHERE " & _
               "[UserID] = @UserID"

            '----------------------------------
            Using _nCommand As New SqlCommand

                _nCommand.Connection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                _nCommand.CommandText = _nQuery
                _nCommand.CommandType = CommandType.Text

                _nCommand.Parameters.AddWithValue("@UserID", _nUserID)

                Using _nSqlDataReader As SqlDataReader = _nCommand.ExecuteReader

                    Dim _iKeyToken As Integer = _nSqlDataReader.GetOrdinal("KeyToken")

                    If _nSqlDataReader.HasRows Then
                        Do While _nSqlDataReader.Read
                            _nPrmKeyToken = cReturnDataType._pYieldString(_nSqlDataReader(_iKeyToken))
                        Loop

                    End If

                End Using '_nSqlDataReader

            End Using '_nCommand

            '----------------------------------
            If Not String.IsNullOrEmpty(_nPrmKeyToken.Trim()) Then
                _nKeyToken = _nPrmKeyToken
                Return True
            Else
                Return False
            End If

            '----------------------------------
        Catch ex As Exception
            'cEventLog._pSubLogError(ex)
            Return False
        End Try
    End Function

    Public Shared Function _pFuncActivateAccount( _
    ByVal _nUserID As String _
    ) As Boolean

        Dim _nReturnValue As Boolean = Nothing
        Try
            '----------------------------------
            Dim _nPrmIsActivated As Boolean = True

            '----------------------------------
            Dim _nQuery As String = Nothing
            _nQuery = _
                "UPDATE " & _
                "[uvw.VS2014.WA.OAIMS.Registerred.Data.Write] " & _
                "SET " & _
                "[IsActivated] = @IsActivated " & _
                "WHERE " & _
                "[UserID] = @UserId "

            '----------------------------------
            Using _nCommand As New SqlCommand

                _nCommand.Connection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                _nCommand.CommandText = _nQuery
                _nCommand.CommandType = CommandType.Text

                _nCommand.Parameters.AddWithValue("@UserID", _nUserID)
                _nCommand.Parameters.AddWithValue("@IsActivated", _nPrmIsActivated)

                _nReturnValue = _nCommand.ExecuteNonQuery()

            End Using '_nCommand

            '----------------------------------
            Return _nReturnValue

            '----------------------------------
        Catch ex As Exception
            'cEventLog._pSubLogError(ex)
            Return False
        End Try
    End Function

    Public Shared Sub _pSubGenerateKeyTokenSignOut( _
    ByVal _nUserID As String _
    )

        Try
            '----------------------------------
            Dim _nNewKeyToken As String = Guid.NewGuid.ToString

            '----------------------------------
            Dim _nQuery As String = Nothing
            _nQuery = _
                "UPDATE " & _
                "[uvw.VS2014.WA.OAIMS.Registerred.Data.Write] " & _
                "SET " & _
                "[KeyToken] = @KeyToken " & _
                "WHERE " & _
                "[UserID] = @UserId "

            '----------------------------------
            Using _nCommand As New SqlCommand

                _nCommand.Connection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                _nCommand.CommandText = _nQuery
                _nCommand.CommandType = CommandType.Text

                _nCommand.Parameters.AddWithValue("@UserID", _nUserID)
                _nCommand.Parameters.AddWithValue("@KeyToken", _nNewKeyToken)

            End Using '_nCommand

            '----------------------------------
        Catch ex As Exception
            'cEventLog._pSubLogError(ex)
        End Try
    End Sub

    Public Shared Function _pFuncGetUserKeySalt( _
    ByVal _nUserID As String _
    , ByRef _nUserKeySalt As Byte() _
    , Optional ByVal xLiteral As String = "" _
    ) As Boolean

        Dim _nReturnValue As Boolean = Nothing
        'Commented temporarily by Tomi 10/14/2019
        '_pLiteral.Text = xLiteral
        'Dim _nSw As Stopwatch = Stopwatch.StartNew
        '_pSubEventLog("Get User Key Salt" & ":Begin")
        Try
            '----------------------------------
            Dim _nPrmUserKeySalt As Byte() = Nothing

            '----------------------------------
            Dim _nQuery As String = Nothing
            _nQuery = _
                "SELECT " & _
                "* " & _
                "FROM " & _
                "[uvw.VS2014.WA.OAIMS.Registerred.Data.Read] " & _
                "WHERE " & _
                "[UserID] = @UserID"

            '----------------------------------
            Using _nCommand As New SqlCommand

                _nCommand.Connection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                _nCommand.CommandText = _nQuery
                _nCommand.CommandType = CommandType.Text

                _nCommand.Parameters.AddWithValue("@UserID", _nUserID)

                Using _nSqlDataReader As SqlDataReader = _nCommand.ExecuteReader

                    '----------------------------------
                    'Indexes
                    Dim _iUserKeySalt As Integer = _nSqlDataReader.GetOrdinal("UserKeySalt")
                    '----------------------------------

                    If _nSqlDataReader.HasRows Then
                        Do While _nSqlDataReader.Read
                            _nPrmUserKeySalt = cReturnDataType._pYieldByteArray(_nSqlDataReader(_iUserKeySalt))
                        Loop

                    End If

                End Using '_nSqlDataReader

            End Using '_nCommand

            '----------------------------------
            If Not _nPrmUserKeySalt Is Nothing Then
                _nUserKeySalt = _nPrmUserKeySalt
                _nReturnValue = True
            Else
                _nReturnValue = False
            End If

            '----------------------------------
            Return _nReturnValue

            '----------------------------------
        Catch ex As Exception
            'cEventLog._pSubLogError(ex)
            '_pSubEventLog(ex, 2)
            Return False
        End Try
        'Commented temporarily by Tomi 10/14/2019
        '_pSubEventLog("Get User Key Salt" & ":End")
        '_pSubEventLog(_nSw.Elapsed, 2)
    End Function

    Public Shared Function _pFuncVerifyUserIDUserKey( _
    ByVal _nUserID As String _
    , ByVal _nUserKey As String _
    , ByVal _nUserKeySalt As Byte() _
    ) As Boolean

        Dim _nReturnValue As Boolean = Nothing
        Try
            '----------------------------------
            Dim _nEncryptedUserKey As String = Nothing
            Dim _nPrmUserKey As String = Nothing
            Dim _nPrmUserType As String = Nothing

            '----------------------------------
            'Encrypt UserKey.
            Dim _nEncryption As New cEncryption
            _nEncryptedUserKey = _nEncryption.Encrypt(_nUserKey, _nUserKeySalt)

            '----------------------------------
            Dim _nQuery As String = Nothing
            _nQuery = _
                "SELECT " & _
                "* " & _
                "FROM " & _
                "[uvw.VS2014.WA.OAIMS.Registerred.Data.Read] " & _
                "WHERE " & _
                "[UserID] = @UserID"

            '----------------------------------
            Using _nCommand As New SqlCommand

                _nCommand.Connection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                _nCommand.CommandText = _nQuery
                _nCommand.CommandType = CommandType.Text

                _nCommand.Parameters.AddWithValue("@UserID", _nUserID)

                Using _nSqlDataReader As SqlDataReader = _nCommand.ExecuteReader

                    '----------------------------------
                    'Indexes
                    Dim _iUserKey As Integer = _nSqlDataReader.GetOrdinal("UserKey")

                    '----------------------------------

                    If _nSqlDataReader.HasRows Then
                        Do While _nSqlDataReader.Read
                            _nPrmUserKey = cReturnDataType._pYieldString(_nSqlDataReader(_iUserKey))

                        Loop

                    End If

                    _nSqlDataReader.Close()
                End Using '_nSqlDataReader

            End Using '_nCommand

            '----------------------------------
            If _nEncryptedUserKey = _nPrmUserKey Then

                _nReturnValue = True
            End If

            '----------------------------------
            Return _nReturnValue

            '----------------------------------
        Catch ex As Exception
            'cEventLog._pSubLogError(ex)
            Return False
        End Try
    End Function

    Public Shared Function _pFuncUpdateUserKey( _
    ByVal _nUserID As String _
    , ByVal _nUserKey As String _
    , ByVal _nOffice As String _
    , ByVal _nUserKeySalt As Byte() _
    , Optional ByVal xLiteral As String = "" _
    ) As Boolean

        Dim _nReturnValue As Boolean = Nothing
        'Commented temporarily by Tomi 10/14/2019
        '_pLiteral.Text = xLiteral
        'Dim _nSw As Stopwatch = Stopwatch.StartNew
        '_pSubEventLog("Update User Key" & ":Begin")

        Try
            '----------------------------------
            Dim _nEncryptedUserKey As String = Nothing

            '----------------------------------
            'Encrypt UserKey.
            Dim _nEncryption As New cEncryption
            _nEncryptedUserKey = _nEncryption.Encrypt(_nUserKey, _nUserKeySalt)

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing
            '_nQuery = _
            '    "UPDATE " & _
            '    "[uvw.VS2014.WA.OAIMS.Registerred.Data.Write] " & _
            '    "SET " & _
            '    "[UserKey] = @UserKey " & _
            '    "WHERE " & _
            '    "[UserID] = @UserId "
            '_nQuery = _
            '    "UPDATE " & _
            '    "[uvw.VS2014.WA.OAIMS.Registerred.Data.Write] " & _
            '    "SET " & _
            '    "[UserKey] = @UserKey " & _
            '    IIf(String.IsNullOrEmpty(_nOffice), "", ", [UserType] = @_nOffice ")

            '_nWhere = "WHERE " & _
            '    "[UserID] = @UserId "

            If _nOffice = Nothing Then
                _nQuery = _
             "UPDATE " & _
             "Registered " & _
             "SET " & _
             "[UserKey] = '" & _nEncryptedUserKey & "' " & _
              " WHERE [UserID] = '" & _nUserID & "' "
            Else
                _nQuery = _
             "UPDATE " & _
             "Registered " & _
             "SET " & _
             "[UserKey] = '" & _nEncryptedUserKey & "', " & _
             "[UserType] = '" & _nOffice & "'" & _
              " WHERE [UserID] = '" & _nUserID & "' "
            End If



            '",[UserType] = '" & _nOffice & "' " & _
            '----------------------------------
            Using _nCommand As New SqlCommand

                _nCommand.Connection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                _nCommand.CommandText = _nQuery
                _nCommand.CommandType = CommandType.Text

                '_nCommand.Parameters.AddWithValue("@UserID", _nUserID)
                '_nCommand.Parameters.AddWithValue("@UserKey", _nEncryptedUserKey)
                '_nCommand.Parameters.AddWithValue("@_nOffice", _nOffice)
                _nReturnValue = _nCommand.ExecuteNonQuery()

            End Using '_nCommand

            '----------------------------------
            Return _nReturnValue

            '----------------------------------
        Catch ex As Exception
            'cEventLog._pSubLogError(ex)
            '_pSubEventLog(ex, 2)
            Return _nReturnValue
        End Try
        'Commented temporarily by Tomi 10/14/2019
        '_pSubEventLog("_pFuncVerifyLGURegistry" & ":End")
        '_pSubEventLog(_nSw.Elapsed, 2)

    End Function

    Public Shared Function _pFuncResetUserKey( _
    ByVal _nUserID As String _
    ) As Boolean

        Dim _nReturnValue As Boolean = Nothing
        Try
            '----------------------------------
            Dim _nEncryptedUserKey As String = Nothing

            '----------------------------------
            Dim _nQuery As String = Nothing
            _nQuery = _
                "UPDATE " & _
                "[uvw.VS2014.WA.OAIMS.Registerred.Data.Write] " & _
                "SET " & _
                "[UserKey] = @UserKey " & _
                ", [UserKeySalt] = Crypt_Gen_Random(32) " & _
                "WHERE " & _
                "[UserID] = @UserId "

            '----------------------------------
            Using _nCommand As New SqlCommand

                _nCommand.Connection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                _nCommand.CommandText = _nQuery
                _nCommand.CommandType = CommandType.Text

                _nCommand.Parameters.AddWithValue("@UserID", _nUserID)
                _nCommand.Parameters.AddWithValue("@UserKey", "")

                _nReturnValue = _nCommand.ExecuteNonQuery()

            End Using '_nCommand

            '----------------------------------
            Return _nReturnValue

            '----------------------------------
        Catch ex As Exception
            'cEventLog._pSubLogError(ex)
            Return _nReturnValue
        End Try
    End Function

    Public Shared Function _pFuncVerifyIfValidKeyToken( _
    ByVal _nUserID As String _
    , ByVal _nKeyToken As String _
    ) As Boolean

        Dim _nReturnValue As Boolean = Nothing
        Try
            '----------------------------------
            Dim _nPrmKeyToken As String = Nothing

            '----------------------------------
            Dim _nQuery As String = Nothing
            _nQuery = _
                "SELECT " & _
                "* " & _
                "FROM " & _
                "[uvw.VS2014.WA.OAIMS.Registerred.Data.Read] " & _
                "WHERE " & _
                "[UserID] = @UserID"
            '----------------------------------
            Using _nCommand As New SqlCommand

                _nCommand.Connection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                _nCommand.CommandText = _nQuery
                _nCommand.CommandType = CommandType.Text

                _nCommand.Parameters.AddWithValue("@UserID", _nUserID)

                Using _nSqlDataReader As SqlDataReader = _nCommand.ExecuteReader

                    '----------------------------------
                    'Indexes
                    Dim _iKeyToken As Integer = _nSqlDataReader.GetOrdinal("KeyToken")
                    '----------------------------------

                    If _nSqlDataReader.HasRows Then
                        Do While _nSqlDataReader.Read
                            _nPrmKeyToken = cReturnDataType._pYieldString(_nSqlDataReader(_iKeyToken))
                        Loop

                    End If

                    _nSqlDataReader.Close()
                End Using '_nSqlDataReader

            End Using '_nCommand

            '----------------------------------
            If _nKeyToken = _nPrmKeyToken Then
                'NOTE: 
                'TODO: Encrypt Parameters being passed.
                _nReturnValue = True

            End If
            '----------------------------------

            Return _nReturnValue
            '----------------------------------
        Catch ex As Exception
            'cEventLog._pSubLogError(ex)
            Return False
        End Try
    End Function

    Public Shared Function _pFuncGenerateKeyToken( _
    ByVal _nUserID As String _
    ) As Boolean

        Dim _nReturnValue As Boolean = Nothing
        Try
            '----------------------------------
            Dim _nNewKeyToken As String = Guid.NewGuid.ToString

            '----------------------------------
            Dim _nQuery As String = Nothing
            _nQuery = _
                "UPDATE " & _
                "[uvw.VS2014.WA.OAIMS.Registerred.Data.Write] " & _
                "SET " & _
                "[KeyToken] = @KeyToken " & _
                "WHERE " & _
                "[UserID] = @UserId "

            '----------------------------------
            Using _nCommand As New SqlCommand

                _nCommand.Connection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
                _nCommand.CommandText = _nQuery
                _nCommand.CommandType = CommandType.Text

                _nCommand.Parameters.AddWithValue("@UserID", _nUserID)
                _nCommand.Parameters.AddWithValue("@KeyToken", _nNewKeyToken)

                _nReturnValue = _nCommand.ExecuteNonQuery()

            End Using '_nCommand

            '----------------------------------
            Return _nReturnValue

            '----------------------------------
        Catch ex As Exception
            'cEventLog._pSubLogError(ex)
            Return False
        End Try
    End Function

    'Public Shared Function _pFuncCheckifLoginNameExist(ByVal _nLoginName As String) As Boolean

    '    Try
    '        ----------------------------------
    '        Dim _nDal As New cCheckAuthentication
    '        _nDal._pSqlConnection = Spidc_Web_API_Global_Connection._pSqlCxn_BPLTAS
    '        _nDal._pLoginname = _nLoginName
    '        _nDal._pSubSelect()

    '        Dim _nDataTable As New DataTable
    '        _nDataTable = _nDal._pDataTable

    '        If _nDataTable.Rows.Count = 0 Then
    '            Return False
    '        Else

    '            Return True
    '        End If

    '        _nDal = Nothing

    '        ----------------------------------
    '    Catch ex As Exception
    '        cEventLog._pSubLogError(ex)
    '        Return False
    '    End Try
    'End Function '@ Added 20180727 

    'Public Shared Function _pFuncVerifyPasskey(ByVal _mLoginName As String, ByVal _mPassKey As String) As Boolean '@ Added 20180727 
    '    _pFuncVerifyPasskey = False
    '    Try
    '        Dim _nPassKey As String = Nothing

    '        '----------------------------------
    '        Dim _nDal As New cCheckAuthentication
    '        _nDal._pSqlConnection = Spidc_Web_API_Global_Connection._pSqlCxn_BPLTAS
    '        _nDal._pLoginname = _mLoginName
    '        _nDal._pSubSelect()

    '        Dim _nDataTable As New DataTable
    '        _nDataTable = _nDal._pDataTable

    '        If _nDataTable.Rows.Count <> 0 Then
    '            _nPassKey = _nDataTable.Rows("0").Item("webpassword").ToString()

    '            If _nPassKey = _mPassKey Then
    '                Return True
    '            Else
    '                Return False
    '            End If

    '        End If

    '        _nDal = Nothing

    '        '----------------------------------
    '    Catch ex As Exception
    '        'cEventLog._pSubLogError(ex)
    '        Return False
    '    End Try
    'End Function

    'Public Shared Function _pFuncVerifyWebEmail(ByVal _mLoginName As String, ByVal _mWebEmail As String, Optional ByVal xLiteral As String = "") As Boolean '@ Added 20180727 
    '    _pFuncVerifyWebEmail = False
    '    'MsgBox("BEGIN")

    '    '_pLiteral.Text = xLiteral
    '    'Dim _nSw As Stopwatch = Stopwatch.StartNew
    '    '_pSubEventLog("Verify WebEmail" & ":Begin")

    '    Try
    '        Dim _nWebEmail As String = Nothing

    '        '----------------------------------
    '        Dim _nDal As New cCheckAuthentication
    '        _nDal._pSqlConnection = cGlobalConnections._pSqlCxn_BPLTAS
    '        _nDal._pLoginname = _mLoginName

    '        _nDal._pSubSelect()

    '        Dim _nDataTable As New DataTable
    '        _nDataTable = _nDal._pDataTable

    '        If _nDataTable.Rows.Count <> 0 Then
    '            _nWebEmail = _nDataTable.Rows("0").Item("WebEmailAdd").ToString()
    '            '  MsgBox(_nWebEmail & _mWebEmail)
    '            If _nWebEmail = _mWebEmail Then
    '                '      MsgBox("True")
    '                Return True

    '            Else
    '                '    MsgBox("FASLE")
    '                Return False

    '            End If

    '        End If

    '        _nDal = Nothing

    '        '----------------------------------
    '    Catch ex As Exception
    '        'cEventLog._pSubLogError(ex)
    '        ' _pSubEventLog(ex, 2)
    '        '    MsgBox("ERROR")
    '        Return False
    '    End Try

    '    '_pSubEventLog("Verify WebEmail" & ":End")
    '    '_pSubEventLog(_nSw.Elapsed, 2)

    'End Function

    'Public Shared Function _pFuncVerifyOffice(ByVal _mLoginName As String, ByVal _mOffice As String, Optional ByVal xLiteral As String = "") As Boolean '@ Added 20180727 
    '    _pFuncVerifyOffice = False
    '    'Commented temporarily by Tomi 10/14/2019
    '    '_pLiteral.Text = xLiteral
    '    'Dim _nSw As Stopwatch = Stopwatch.StartNew
    '    '_pSubEventLog("Verify Office" & ":Begin")

    '    Try
    '        Dim _nOffice As String = Nothing

    '        '----------------------------------
    '        Dim _nDal As New cCheckAuthentication
    '        _nDal._pSqlConnection = cGlobalConnections._pSqlCxn_BPLTAS
    '        _nDal._pLoginname = _mLoginName
    '        _nDal._pSubSelect()

    '        Dim _nDataTable As New DataTable
    '        _nDataTable = _nDal._pDataTable

    '        If _nDataTable.Rows.Count <> 0 Then
    '            _nOffice = _nDataTable.Rows("0").Item("weboffice").ToString()

    '            If _nOffice = "license" And _mOffice = "BPLO" Then
    '                Return True
    '            ElseIf _nOffice = "treasurer" And _mOffice = "Treasury" Then
    '                Return True
    '            Else
    '                Return False
    '            End If

    '        End If

    '        _nDal = Nothing

    '        '----------------------------------
    '    Catch ex As Exception
    '        'cEventLog._pSubLogError(ex)
    '        '   _pSubEventLog(ex, 2)
    '        Return False
    '    End Try
    '    'Commented temporarily by Tomi 10/14/2019
    '    '_pSubEventLog("Verify Office" & ":End")
    '    '_pSubEventLog(_nSw.Elapsed, 2)

    'End Function

    'Public Shared Function _pFuncVerifyIfWebAuthorized(ByVal _mLoginName As String, Optional ByVal xLiteral As String = "") As Boolean '@ Added 20180727 
    '    _pFuncVerifyIfWebAuthorized = False
    '    'Commented temporarily by Tomi 10/14/2019
    '    '_pLiteral.Text = xLiteral
    '    'Dim _nSw As Stopwatch = Stopwatch.StartNew
    '    '_pSubEventLog("Verify If Web Authorized" & ":Begin")

    '    Try
    '        Dim _nWebUser As String = Nothing

    '        '----------------------------------
    '        Dim _nDal As New cCheckAuthentication
    '        _nDal._pSqlConnection = cGlobalConnections._pSqlCxn_BPLTAS
    '        _nDal._pLoginname = _mLoginName
    '        _nDal._pSubSelect()

    '        Dim _nDataTable As New DataTable
    '        _nDataTable = _nDal._pDataTable

    '        If _nDataTable.Rows.Count <> 0 Then
    '            _nWebUser = _nDataTable.Rows("0").Item("WebUser").ToString()

    '            If _nWebUser <> "0" Then
    '                Return True
    '            Else
    '                Return False
    '            End If

    '        End If

    '        _nDal = Nothing

    '        '----------------------------------
    '    Catch ex As Exception
    '        'cEventLog._pSubLogError(ex)
    '        '  _pSubEventLog(ex, 2)
    '        Return False
    '    End Try
    '    'Commented temporarily by Tomi 10/14/2019
    '    '_pSubEventLog("Verify If Web Authorized" & ":End")
    '    '_pSubEventLog(_nSw.Elapsed, 2)
    'End Function

    'Public Shared Function _pFuncVerifyIfActiveLocal(ByVal _mLoginName As String, Optional ByVal xLiteral As String = "") As Boolean '@ Added 20180727 

    '    'Commented temporarily by Tomi 10/14/2019
    '    '_pLiteral.Text = xLiteral
    '    'Dim _nSw As Stopwatch = Stopwatch.StartNew
    '    '_pSubEventLog("Verify If Active Local" & ":Begin")

    '    _pFuncVerifyIfActiveLocal = False
    '    Try
    '        Dim _mStatus As String = Nothing

    '        '----------------------------------
    '        Dim _nDal As New cCheckAuthentication
    '        _nDal._pSqlConnection = cGlobalConnections._pSqlCxn_BPLTAS
    '        _nDal._pLoginname = _mLoginName
    '        _nDal._pSubSelect()

    '        Dim _nDataTable As New DataTable
    '        _nDataTable = _nDal._pDataTable

    '        If _nDataTable.Rows.Count <> 0 Then
    '            _mStatus = _nDataTable.Rows("0").Item("Status").ToString()

    '            If _mStatus = "0" Then
    '                Return True
    '            Else
    '                Return False
    '            End If

    '        End If

    '        _nDal = Nothing

    '        '----------------------------------
    '    Catch ex As Exception
    '        'cEventLog._pSubLogError(ex)
    '        ' _pSubEventLog(ex, 2)
    '        Return False
    '    End Try
    '    'Commented temporarily by Tomi 10/14/2019
    '    '_pSubEventLog("Verify If Active Local" & ":End")
    '    '_pSubEventLog(_nSw.Elapsed, 2)

    'End Function

    'Public Shared Function _pFuncVerifyLGURegistry(ByVal _nLoginName As String, ByVal _mPassKey As String, ByVal _mWebEmail As String, ByVal _mOffice As String, ByRef _mResult As String, Optional ByVal xLiteral As String = "") As Boolean
    '    Commented temporarily by Tomi 10/14/2019
    '    _pLiteral.Text = xLiteral
    '    Dim _nSw As Stopwatch = Stopwatch.StartNew
    '    _pSubEventLog("Verify LGU Registry" & ":Begin")


    '    Try

    '        Dim _nPassKey As String = Nothing
    '        Dim _nWebEmail As String = Nothing
    '        Dim _nOffice As String = Nothing
    '        Dim _nWebUser As String = Nothing
    '        Dim _mStatus As String = Nothing
    '        ----------------------------------
    '        Dim _nDal As New cCheckAuthentication
    '        _nDal._pSqlConnection = cGlobalConnections._pSqlCxn_BPLTAS
    '        _nDal._pLoginname = _nLoginName
    '        _nDal._pSubSelect()

    '        Dim _nDataTable As New DataTable
    '        _nDataTable = _nDal._pDataTable

    '        If _nDataTable.Rows.Count = 0 Then
    '            Return False
    '        Else

    '            _nPassKey = _nDataTable.Rows("0").Item("webpassword").ToString()
    '            _nWebEmail = _nDataTable.Rows("0").Item("WebEmailAdd").ToString()
    '            _nOffice = _nDataTable.Rows("0").Item("weboffice").ToString()
    '            _nWebUser = _nDataTable.Rows("0").Item("WebUser").ToString()
    '            _mStatus = _nDataTable.Rows("0").Item("Status").ToString()

    '            Select Case False
    '                Case _nPassKey = _mPassKey
    '                    _mResult = "Invalid Passkey."
    '                    Return False
    '                Case _nWebEmail = _mWebEmail
    '                    _mResult = "Email address did not match with local email."
    '                    Return False
    '                Case (_nOffice = "license" And _mOffice = "BPLO" Or UCase(_mOffice) = "REGULATORY") Or (_nOffice = "treasurer" And _mOffice = "Treasury")
    '                    _mResult = "Invalid User Type."
    '                    Return False
    '                Case _nWebUser = "-1" Or _nWebUser = "1"
    '                    _mResult = "Unauthorized Web User."
    '                    Return False
    '                Case _mStatus = "0"
    '                    _mResult = "Inactive Local User."
    '                    Return False
    '                Case Else
    '                    Return True
    '            End Select

    '        End If

    '        _nDal = Nothing

    '        ----------------------------------
    '    Catch ex As Exception
    '        cEventLog._pSubLogError(ex)
    '        _pSubEventLog(ex, 2)
    '        _mResult = ex.Message
    '        Return False

    '    End Try
    '    Commented temporarily by Tomi 10/14/2019
    '    _pSubEventLog("Verify LGU Registry" & ":End")
    '    _pSubEventLog(_nSw.Elapsed, 2)
    'End Function '@ Added 20180727 

#End Region




End Class
