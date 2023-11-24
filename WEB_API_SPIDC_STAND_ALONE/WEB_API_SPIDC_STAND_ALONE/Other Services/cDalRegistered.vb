

#Region "Imports"

Imports System.Data.SqlClient

#End Region

Public Class cDalRegistered

#Region "Variables Data"

    Private _mCxn As SqlConnection = Nothing
    Private _mQry As String = Nothing
    Private _mCmd As SqlCommand = Nothing
    Private _mDt As DataTable = Nothing
    Private _mDr As SqlDataReader = Nothing

#End Region

#Region "Property Data"

    Public WriteOnly Property _pCxn() As SqlConnection
        Set(value As SqlConnection)
            _mCxn = value
        End Set
    End Property

    Public ReadOnly Property _pQry() As String
        Get
            Return _mQry
        End Get
    End Property

    Public ReadOnly Property _pCmd() As SqlCommand
        Get
            Return _mCmd
        End Get
    End Property

    Public ReadOnly Property _pDt() As DataTable
        Get
            Try
                '----------------------------------
                _mDt = New DataTable
                _mDt.Load(_mCmd.ExecuteReader)
                _mCmd.Dispose()
                Return _mDt
                '----------------------------------
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    Public ReadOnly Property _pDr() As SqlDataReader
        Get
            Try
                '----------------------------------
                _mDr = _mCmd.ExecuteReader
                _mCmd.Dispose()
                Return _mDr
                '----------------------------------
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

#End Region

#Region "Variables Field"

    Private Const _mTableName As String = "Registered"

    Private _mIDNo As String  'Has Default Binding Property
    Private _mUserID As String

    Private _mDateRegistered As String 'Has Default Binding Property
    Private _mUserKey As String
    Private _mUserKeySalt As Byte() = Nothing

    Private _mLastName As String
    Private _mFirstName As String
    Private _mMiddleName As String
    Private _mExtnName As String

    Private _mBirthDate As String
    Private _mBirthPlace As String

    Private _mSetupGender As String
    Private _mSetupGenderDetail As String

    Private _mSetupCivilStatus As String
    Private _mSetupCivilStatusDetail As String

    Private _mSetupCitizenship As String
    Private _mSetupCitizenshipDetail As String

    Private _mIsActivated As Boolean = Nothing
    Private _mKeyToken As String

    Private _mOrigIDNo As String
    Private _mUserType As String
#End Region

#Region "Properties Field"
    Public Property _pIDNo() As String
        Get
            Return _mIDNo
        End Get
        Set(value As String)
            _mIDNo = value
        End Set
    End Property
    Public Property _pUserID() As String
        Get
            Return _mUserID
        End Get
        Set(value As String)
            _mUserID = value
        End Set
    End Property

    Public Property _pDateRegistered() As String
        Get
            Return _mDateRegistered
        End Get
        Set(value As String)
            _mDateRegistered = value
        End Set
    End Property
    Public Property _pUserKey() As String
        Get
            Return _mUserKey
        End Get
        Set(value As String)
            _mUserKey = value
        End Set
    End Property
    Public Property _pUserKeySalt() As Byte()
        Get
            Return _mUserKeySalt
        End Get
        Set(value As Byte())
            _mUserKeySalt = value
        End Set
    End Property

    Public Property _pLastName() As String
        Get
            Return _mLastName
        End Get
        Set(value As String)
            _mLastName = value
        End Set
    End Property
    Public Property _pFirstName() As String
        Get
            Return _mFirstName
        End Get
        Set(value As String)
            _mFirstName = value
        End Set
    End Property
    Public Property _pMiddleName() As String
        Get
            Return _mMiddleName
        End Get
        Set(value As String)
            _mMiddleName = value
        End Set
    End Property
    Public Property _pExtnName() As String
        Get
            Return _mExtnName
        End Get
        Set(value As String)
            _mExtnName = value
        End Set
    End Property

    Public Property _pBirthDate() As String
        Get
            Return _mBirthDate
        End Get
        Set(value As String)
            _mBirthDate = value
        End Set
    End Property
    Public Property _pBirthPlace() As String
        Get
            Return _mBirthPlace
        End Get
        Set(value As String)
            _mBirthPlace = value
        End Set
    End Property

    Public Property _pSetupGender() As String
        Get
            Return _mSetupGender
        End Get
        Set(value As String)
            _mSetupGender = value
        End Set
    End Property
    Public ReadOnly Property _pSetupGenderDetail() As String
        Get
            Return _mSetupGenderDetail
        End Get
    End Property

    Public Property _pSetupCivilStatus() As String
        Get
            Return _mSetupCivilStatus
        End Get
        Set(value As String)
            _mSetupCivilStatus = value
        End Set
    End Property
    Public ReadOnly Property _pSetupCivilStatusDetail() As String
        Get
            Return _mSetupCivilStatusDetail
        End Get
    End Property

    Public Property _pSetupCitizenship() As String
        Get
            Return _mSetupCitizenship
        End Get
        Set(value As String)
            _mSetupCitizenship = value
        End Set
    End Property
    Public ReadOnly Property _pSetupCitizenshipDetail() As String
        Get
            Return _mSetupCitizenshipDetail
        End Get
    End Property

    Public Property _pIsActivated() As Boolean
        Get
            Return _mIsActivated
        End Get
        Set(value As Boolean)
            _mIsActivated = value
        End Set
    End Property
    Public Property _pKeyToken() As String
        Get
            Return _mKeyToken
        End Get
        Set(value As String)
            _mKeyToken = value
        End Set
    End Property
    Public Property _pUserType() As String
        Get
            Return _mUserType
        End Get
        Set(value As String)
            _mUserType = value
        End Set
    End Property

#End Region

#Region "Propertiy Field Original"

    Public WriteOnly Property _pOrigIDNo() As String
        Set(value As String)
            _mOrigIDNo = value
        End Set
    End Property

#End Region

#Region "Routine Command"

    Public Sub _pSubSelect()
        Try
            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            '----------------------------------
            _nQuery = _
                "SELECT " & _
                " [R].[IDNo] " & _
                ", [R].[UserID] " & _
                ", CONVERT(VARCHAR, [R].[DateRegistered], 111) AS [DateRegistered] " & _
                ", [R].[UserKey] " & _
                ", [R].[UserKeySalt] " & _
                ", [R].[LastName] " & _
                ", [R].[FirstName] " & _
                ", [R].[MiddleName] " & _
                ", [R].[ExtnName] " & _
                ", CONVERT(VARCHAR, [R].[BirthDate], 111) AS [BirthDate] " & _
                ", [R].[BirthPlace] " & _
                ", [R].[SetupGender] " & _
                ", [SG].[Detail] AS [SetupGenderDetail] " & _
                ", [R].[SetupCivilStatus] " & _
                ", [SCS].[Detail] AS [SetupCivilStatusDetail] " & _
                ", [R].[SetupCitizenship] " & _
                ", [SC].[Detail] AS [SetupCitizenshipDetail] " & _
                ", [R].[IsActivated] " & _
                ", [R].[KeyToken] " & _
                ", [R].[UserType] " & _
                "FROM " & _
                "[" & _mTableName & "] [R] " & _
                    "LEFT JOIN [dbo].[SetupGender] [SG] " & _
                        "ON [R].[SetupGender] = [SG].[Code] " & _
                    "LEFT JOIN [dbo].[SetupCivilStatus] [SCS] " & _
                        "ON [R].[SetupCivilStatus] = [SCS].[Code]  " & _
                    "LEFT JOIN [dbo].[SetupCitizenship] [SC] " & _
                        "ON [R].[SetupCitizenship] = [SC].[Code] " & _
                " "

            '----------------------------------    
            _nWhere = "WHERE [IDNo] IN (SELECT [IDNo] FROM [" & _mTableName & "]) "

            If Not String.IsNullOrEmpty(_mIDNo) _
                Or Not String.IsNullOrEmpty(_mUserID) Then

                _nWhere += "AND [R].[IDNo] = @_mIDNo OR [R].[UserID] = @_mUserID "

            End If

            '----------------------------------
            _mQry = _nQuery & _nWhere

            '----------------------------------
            _mCmd = New SqlCommand(_mQry, _mCxn)

            With _mCmd.Parameters

                .AddWithValue("@_mIDNo", IIf(String.IsNullOrEmpty(_mIDNo), "", _mIDNo))
                .AddWithValue("@_mUserID", IIf(String.IsNullOrEmpty(_mUserID), "", _mUserID))

            End With

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubInsert()
        Try
            '----------------------------------
            'TODO:
            'Create sub procedure to generate UserKeySalt.

            '----------------------------------
            'Check if Primary Key(s) is not empty/ null/ whitespace.
            If String.IsNullOrEmpty(_mIDNo) And String.IsNullOrEmpty(_mUserID) Then
                Exit Sub
            End If

            '----------------------------------
            Dim _nQuery As String = Nothing

            '----------------------------------
            _nQuery = _
             "INSERT INTO " & _
             "[" & _mTableName & "] " & _
             "(" & _
                IIf(String.IsNullOrEmpty(_mIDNo), "", "[IDNo]") & _
                IIf(String.IsNullOrEmpty(_mUserID), "", ", [UserID]") & _
                    IIf(String.IsNullOrEmpty(_mDateRegistered), "", ", [DateRegistered]") & _
                    IIf(String.IsNullOrEmpty(_mUserKey), "", ", [UserKey]") & _
                    IIf((_mUserKeySalt) Is Nothing, "", ", [UserKeySalt]") & _
                IIf(String.IsNullOrEmpty(_mLastName), "", ", [LastName]") & _
                IIf(String.IsNullOrEmpty(_mFirstName), "", ", [FirstName]") & _
                IIf(String.IsNullOrEmpty(_mMiddleName), "", ", [MiddleName]") & _
                IIf(String.IsNullOrEmpty(_mExtnName), "", ", [ExtnName]") & _
                    IIf(String.IsNullOrEmpty(_mBirthDate), "", ", [BirthDate]") & _
                    IIf(String.IsNullOrEmpty(_mBirthPlace), "", ", [BirthPlace]") & _
                IIf(String.IsNullOrEmpty(_mSetupGender), "", ", [SetupGender]") & _
                IIf(String.IsNullOrEmpty(_mSetupCivilStatus), "", ", [SetupCivilStatus]") & _
                IIf(String.IsNullOrEmpty(_mSetupCitizenship), "", ", [SetupCitizenship]") & _
                    IIf((_mIsActivated) = Nothing, "", ", [IsActivated]") & _
                    IIf(String.IsNullOrEmpty(_mKeyToken), "", ", [KeyToken]") & _
             ") " & _
             "VALUES " & _
             "(" & _
                IIf(String.IsNullOrEmpty(_mIDNo), "", "@_mIDNo") & _
                IIf(String.IsNullOrEmpty(_mUserID), "", ", @_mUserID") & _
                    IIf(String.IsNullOrEmpty(_mDateRegistered), "", ", @_mDateRegistered") & _
                    IIf(String.IsNullOrEmpty(_mUserKey), "", ", @_mUserKey") & _
                    IIf((_mUserKeySalt) Is Nothing, "", ", @_mUserKeySalt") & _
                IIf(String.IsNullOrEmpty(_mLastName), "", ", @_mLastName") & _
                IIf(String.IsNullOrEmpty(_mFirstName), "", ", @_mFirstName") & _
                IIf(String.IsNullOrEmpty(_mMiddleName), "", ", @_mMiddleName") & _
                IIf(String.IsNullOrEmpty(_mExtnName), "", ", @_mExtnName") & _
                    IIf(String.IsNullOrEmpty(_mBirthDate), "", ", @_mBirthDate") & _
                    IIf(String.IsNullOrEmpty(_mBirthPlace), "", ", @_mBirthPlace") & _
                IIf(String.IsNullOrEmpty(_mSetupGender), "", ", @_mSetupGender") & _
                IIf(String.IsNullOrEmpty(_mSetupCivilStatus), "", ", @_mSetupCivilStatus") & _
                IIf(String.IsNullOrEmpty(_mSetupCitizenship), "", ", @_mSetupCitizenship") & _
                    IIf((_mIsActivated) = Nothing, "", ", @_mIsActivated") & _
                    IIf(String.IsNullOrEmpty(_mKeyToken), "", ", @_mKeyToken") & _
             ") "

            '----------------------------------
            _mQry = _nQuery

            '----------------------------------
            _mCmd = New SqlCommand(_mQry, _mCxn)

            With _mCmd.Parameters

                If Not String.IsNullOrEmpty(_mIDNo) Then .AddWithValue("@_mIDNo", _mIDNo)
                If Not String.IsNullOrEmpty(_mUserID) Then .AddWithValue("@_mUserID", _mUserID)

                If Not String.IsNullOrEmpty(_mDateRegistered) Then .AddWithValue("@_mDateRegistered", _mDateRegistered)
                If Not String.IsNullOrEmpty(_mUserKey) Then .AddWithValue("@_mUserKey", _mUserKey)
                If Not (_mUserKeySalt) Is Nothing Then .AddWithValue("@_mUserKeySalt", _mUserKeySalt)

                If Not String.IsNullOrEmpty(_mLastName) Then .AddWithValue("@_mLastName", _mLastName)
                If Not String.IsNullOrEmpty(_mFirstName) Then .AddWithValue("@_mFirstName", _mFirstName)
                If Not String.IsNullOrEmpty(_mMiddleName) Then .AddWithValue("@_mMiddleName", _mMiddleName)
                If Not String.IsNullOrEmpty(_mExtnName) Then .AddWithValue("@_mExtnName", _mExtnName)

                If Not String.IsNullOrEmpty(_mBirthDate) Then .AddWithValue("@_mBirthDate", _mBirthDate)
                If Not String.IsNullOrEmpty(_mBirthPlace) Then .AddWithValue("@_mBirthPlace", _mBirthPlace)

                If Not String.IsNullOrEmpty(_mSetupGender) Then .AddWithValue("@_mSetupGender", _mSetupGender)
                If Not String.IsNullOrEmpty(_mSetupCivilStatus) Then .AddWithValue("@_mSetupCivilStatus", _mSetupCivilStatus)
                If Not String.IsNullOrEmpty(_mSetupCitizenship) Then .AddWithValue("@_mSetupCitizenship", _mSetupCitizenship)

                If Not (_mIsActivated) = Nothing Then .AddWithValue("@_mIsActivated", _mIsActivated)
                If Not String.IsNullOrEmpty(_mKeyToken) Then .AddWithValue("@_mKeyToken", _mKeyToken)

            End With

            _mCmd.ExecuteNonQuery()

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubUpdate()
        Try
            '----------------------------------
            'Check if Primary Key(s) is not empty/ null/ whitespace.
            If String.IsNullOrEmpty(_mOrigIDNo) Then
                Exit Sub
            End If
            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            '----------------------------------
            _nQuery = _
             "UPDATE " & _
             "[" & _mTableName & "] " & _
             "SET " & _
                IIf(String.IsNullOrEmpty(_mDateRegistered), "", " [DateRegistered] = @_mDateRegistered") & _
                IIf(String.IsNullOrEmpty(_mUserKey), "", ", [UserKey] = @_mUserKey") & _
                    IIf(String.IsNullOrEmpty(_mLastName), "", ", [LastName] = @_mLastName") & _
                    IIf(String.IsNullOrEmpty(_mFirstName), "", ", [FirstName] = @_mFirstName") & _
                    IIf(String.IsNullOrEmpty(_mMiddleName), "", ", [MiddleName] = @_mMiddleName") & _
                    IIf(String.IsNullOrEmpty(_mExtnName), "", ", [ExtnName] = @_mExtnName") & _
                IIf(String.IsNullOrEmpty(_mBirthDate), "", ", [BirthDate] = @_mBirthDate") & _
                IIf(String.IsNullOrEmpty(_mBirthPlace), "", ", [BirthPlace] = @_mBirthPlace") & _
                    IIf(String.IsNullOrEmpty(_mSetupGender), "", ", [SetupGender] = @_mSetupGender") & _
                    IIf(String.IsNullOrEmpty(_mSetupCivilStatus), "", ", [SetupCivilStatus] = @_mSetupCivilStatus") & _
                    IIf(String.IsNullOrEmpty(_mSetupCitizenship), "", ", [SetupCitizenship] = @_mSetupCitizenship") & _
                IIf((_mIsActivated) = Nothing, "", ", [IsActivated] = @IsActivated") & _
                IIf(String.IsNullOrEmpty(_mKeyToken), "", ", [KeyToken] = @KeyToken") & _
                " "

            '----------------------------------
            If Not String.IsNullOrEmpty(_mOrigIDNo) Then

                _nWhere = "WHERE [IDNo] = @_mOrigIDNo "

            Else
                _nWhere = Nothing
            End If

            '----------------------------------
            'Prevent Bulk Update.
            If _nWhere = Nothing Then
                Exit Sub
            End If

            '----------------------------------
            _mQry = _nQuery & _nWhere

            '----------------------------------
            _mCmd = New SqlCommand(_mQry, _mCxn)

            With _mCmd.Parameters

                'Original Primary Key(s).
                If Not String.IsNullOrEmpty(_mIDNo) Then .AddWithValue("@_mOrigIDNo", _mOrigIDNo)

                'New Values.
                If Not String.IsNullOrEmpty(_mIDNo) Then .AddWithValue("@_mIDNo", _mIDNo)
                If Not String.IsNullOrEmpty(_mUserID) Then .AddWithValue("@_mUserID", _mUserID)

                If Not String.IsNullOrEmpty(_mDateRegistered) Then .AddWithValue("@_mDateRegistered", _mDateRegistered)
                If Not String.IsNullOrEmpty(_mUserKey) Then .AddWithValue("@_mUserKey", _mUserKey)

                If Not String.IsNullOrEmpty(_mLastName) Then .AddWithValue("@_mLastName", _mLastName)
                If Not String.IsNullOrEmpty(_mFirstName) Then .AddWithValue("@_mFirstName", _mFirstName)
                If Not String.IsNullOrEmpty(_mMiddleName) Then .AddWithValue("@_mMiddleName", _mMiddleName)
                If Not String.IsNullOrEmpty(_mExtnName) Then .AddWithValue("@_mExtnName", _mExtnName)

                If Not String.IsNullOrEmpty(_mBirthDate) Then .AddWithValue("@_mBirthDate", _mBirthDate)
                If Not String.IsNullOrEmpty(_mBirthPlace) Then .AddWithValue("@_mBirthPlace", _mBirthPlace)

                If Not String.IsNullOrEmpty(_mSetupGender) Then .AddWithValue("@_mSetupGender", _mSetupGender)
                If Not String.IsNullOrEmpty(_mSetupCivilStatus) Then .AddWithValue("@_mSetupCivilStatus", _mSetupCivilStatus)
                If Not String.IsNullOrEmpty(_mSetupCitizenship) Then .AddWithValue("@_mSetupCitizenship", _mSetupCitizenship)

                If Not (_mIsActivated) = Nothing Then .AddWithValue("@_mIsActivated", _mIsActivated)
                If Not String.IsNullOrEmpty(_mKeyToken) Then .AddWithValue("@_mKeyToken", _mKeyToken)

            End With

            _mCmd.ExecuteNonQuery()

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubDelete()
        Try
            '----------------------------------
            'Check if Primary Key(s) is not empty/ null/ whitespace.
            If String.IsNullOrEmpty(_mIDNo) Then
                'Prevent Bulk Deletion.
                Exit Sub
            End If

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            '----------------------------------
            _nQuery = _
                "DELETE FROM " & _
                "[" & _mTableName & "] " & _
                " "

            '----------------------------------
            _nWhere = "WHERE [IDNo] = @_mIDNo OR [UserID] = @_mUserID "

            '----------------------------------
            _mQry = _nQuery & _nWhere

            '----------------------------------
            _mCmd = New SqlCommand(_mQry, _mCxn)

            With _mCmd.Parameters

                If Not String.IsNullOrEmpty(_mIDNo) Then .AddWithValue("@_mIDNo", _mIDNo)

            End With

            _mCmd.ExecuteNonQuery()

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Routines"

    Public Sub _pSubSelectSpecificRecord()
        Try
            '----------------------------------
            'Check if Primary Key(s) is not empty/ null/ whitespace.
            If String.IsNullOrEmpty(_mIDNo) And String.IsNullOrEmpty(_mUserID) Then
                Exit Sub
            End If

            '----------------------------------
            _pSubSelect()

            Using _nDr As SqlDataReader = _pDr

                '----------------------------------
                'Declare Indexes for Fast Retrieval of Data.

                Dim _iIDNo As Integer = _nDr.GetOrdinal("IDNo")
                Dim _iUserID As Integer = _nDr.GetOrdinal("UserID")
                Dim _iKeyToken As Integer = _nDr.GetOrdinal("KeyToken")

                Dim _iFirstName As Integer = _nDr.GetOrdinal("FirstName")
                Dim _iLastName As Integer = _nDr.GetOrdinal("LastName")
                Dim _iMiddleName As Integer = _nDr.GetOrdinal("LastName")
                Dim _iExtnName As Integer = _nDr.GetOrdinal("ExtnName")

                Dim _iBirthDate As Integer = _nDr.GetOrdinal("BirthDate")
                Dim _iBirthPlace As Integer = _nDr.GetOrdinal("BirthPlace")

                Dim _iGender As Integer = _nDr.GetOrdinal("SetupGender")
                Dim _iGenderDetail As Integer = _nDr.GetOrdinal("SetupGenderDetail")

                Dim _iCivilStatus As Integer = _nDr.GetOrdinal("SetupCivilStatus")
                Dim _iCivilStatusDetail As Integer = _nDr.GetOrdinal("SetupCivilStatusDetail")

                Dim _iCitizenship As Integer = _nDr.GetOrdinal("SetupCitizenship")
                Dim _iCitizenshipDetail As Integer = _nDr.GetOrdinal("SetupCitizenshipDetail")

                Dim _iIsActivated As Integer = _nDr.GetOrdinal("IsActivated")

                Dim _iUserType As Integer = _nDr.GetOrdinal("UserType")
                '----------------------------------

                If _nDr.HasRows Then
                    _nDr.Read()

                    _mIDNo = cReturnDataType._pYieldString(_nDr(_iIDNo))
                    _mUserID = cReturnDataType._pYieldString(_nDr(_iUserID))
                    _mKeyToken = cReturnDataType._pYieldString(_nDr(_iKeyToken))

                    _mFirstName = cReturnDataType._pYieldString(_nDr(_iFirstName))
                    _mLastName = cReturnDataType._pYieldString(_nDr(_iLastName))
                    _mMiddleName = cReturnDataType._pYieldString(_nDr(_iMiddleName))
                    _mExtnName = cReturnDataType._pYieldString(_nDr(_iExtnName))

                    _mBirthDate = cReturnDataType._pYieldString(_nDr(_iBirthDate))
                    _mBirthPlace = cReturnDataType._pYieldString(_nDr(_iBirthPlace))

                    _mSetupGender = cReturnDataType._pYieldString(_nDr(_iGender))
                    _mSetupGenderDetail = cReturnDataType._pYieldString(_nDr(_iGenderDetail))

                    _mSetupCivilStatus = cReturnDataType._pYieldString(_nDr(_iCivilStatus))
                    _mSetupCivilStatusDetail = cReturnDataType._pYieldString(_nDr(_iCivilStatusDetail))

                    _mSetupCitizenship = cReturnDataType._pYieldString(_nDr(_iCitizenship))
                    _mSetupCitizenshipDetail = cReturnDataType._pYieldString(_nDr(_iCitizenshipDetail))

                    _mIsActivated = CType(_nDr(_iIsActivated), Boolean)
                    _mUserType = cReturnDataType._pYieldString(_nDr(_iUserType))

                End If

            End Using

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

#End Region

    '#Region "SYSCONTROL"
    '    Public Sub _pSelectLocalLogin()
    '        Try
    '            Dim _nQuery As String = Nothing
    '            Dim _nWhere As String = Nothing

    '            _nQuery = "Select LoginName," & _
    '            "webpassword, weboffice, [STATUS], " & _
    '            "WebEmailAdd, WebUser " & _
    '            "from(SYSCTRL) " & _
    '            "where loginname = '" & _mLoginname & "' "
    '            '----------------------------------    

    '            '----------------------------------
    '            _mQry = _nQuery & _nWhere

    '            '----------------------------------
    '            _mCmd = New SqlCommand(_mQry, _mCxn)
    '        Catch ex As Exception

    '        End Try
    '    End Sub
    '#End Region

End Class
