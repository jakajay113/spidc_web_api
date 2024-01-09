

#Region "Imports"

Imports System.Data.SqlClient
Imports IMC.cReturnDataType

#End Region

Public Class cBllAutoIDNo
    Implements IDisposable

#Region "Variables Data"

    Private _mQry As String
    Private _mCmd As SqlCommand
    Private _mDt As DataTable
    Private _mDr As SqlDataReader
    Private _mCxn As SqlConnection

#End Region

#Region "Properties Data"
    Public WriteOnly Property _pCxn As SqlConnection
        Set(value As SqlConnection)
            _mCxn = value
        End Set
    End Property
#End Region

#Region "Variables"

    Private _mCatalog As String 'the Database location of the AutoID Table.
    Private _mServerName As String 'the current Sql Server Instance.

    Private _mTableName As String 'the table to perform an auto-increment
    Private _mColumnName As String 'the column to auto-increment.

    Private _mAutoIDCode As String 'the Auto ID Code. 
    Private _mAutoIDType As String 'the Type of increment.

    Private _mFilterCondition As String 'the SQL Query condition to filter IDs if generating a Sub ID Number.

    Private _mSattellite As String
    Private _mSattelliteLength As Integer

    Private _mYear As String
    Private _mYearLength As Integer
    Private _mMonth As String
    Private _mMonthLength As Integer
    Private _mDay As String
    Private _mDayLength As Integer
    Private _mHour As String
    Private _mHourLength As Integer

    Private _mSequenceLength As Integer
    Private _mSequenceFormat As String

    Private _mIDNoLength As Integer 'the Length of the ID Number.
    Private _mLastIDNo As String
    Private _mLastSequence As Long 'Long DataType is also System.int64 DataType.
    Private _mNewSequence As String

    Private _mIsMaximumSequence As Boolean

    Private _mNewIDNo As String 'the New ID Number.

#End Region

#Region "Constant Variables"

    'No Sattellite
    Private _mStringArray101() As String = {"101.03", "101.04", "101.05", "101.06", "101.07", "101.08", "101.09", "101.10"}

    'With Sattellite
    Private _mStringArray102() As String = {"102.03", "102.04", "102.05", "102.06", "102.07", "102.08", "102.09", "102.10"}

    'No Sattellite
    Private _mStringArray201() As String = {"201.03", "201.04", "201.05", "201.06", "201.07", "201.08", "201.09", "201.10"}
    Private _mStringArray202() As String = {"202.03", "202.04", "202.05", "202.06", "202.07", "202.08", "202.09", "202.10"}
    Private _mStringArray203() As String = {"203.03", "203.04", "203.05", "203.06", "203.07", "203.08", "203.09", "203.10"}
    Private _mStringArray204() As String = {"204.03", "204.04", "204.05", "204.06", "204.07", "204.08", "204.09", "204.10"}

    'With Sattellite
    Private _mStringArray301() As String = {"301.03", "301.04", "301.05", "301.06", "301.07", "301.08", "301.09", "301.10"}
    Private _mStringArray302() As String = {"302.03", "302.04", "302.05", "302.06", "302.07", "302.08", "302.09", "302.10"}
    Private _mStringArray303() As String = {"303.03", "303.04", "303.05", "303.06", "303.07", "303.08", "303.09", "303.10"}
    Private _mStringArray304() As String = {"304.03", "304.04", "304.05", "304.06", "304.07", "304.08", "304.09", "304.10"}

#End Region

#Region "Properties"
    Public WriteOnly Property _pAutoIDCode() As String
        Set(ByVal value As String)
            _mAutoIDCode = value
        End Set
    End Property
    Public WriteOnly Property _pTableName() As String
        Set(ByVal value As String)
            _mTableName = value
        End Set
    End Property
    Public WriteOnly Property _pColumnName() As String
        Set(ByVal value As String)
            _mColumnName = value
        End Set
    End Property
    Public WriteOnly Property _pFilterCondition() As String
        Set(ByVal value As String)
            _mFilterCondition = value
        End Set
    End Property
    Public ReadOnly Property _pNewIDNo As String
        Get
            Return _mNewIDNo
        End Get
    End Property
    Public ReadOnly Property _pIsMaximumSequence As Boolean
        Get
            Return _mIsMaximumSequence
        End Get
    End Property
#End Region

#Region "Properties Test Mode"
    Public ReadOnly Property _pServerName() As String
        Get
            Return _mServerName
        End Get
    End Property
    Public ReadOnly Property _pAutoIDType() As String
        Get
            Return _mAutoIDType
        End Get
    End Property

    Public ReadOnly Property _pSattellite() As String
        Get
            Return _mSattellite
        End Get
    End Property
    Public ReadOnly Property _pSattelliteLength() As Integer
        Get
            Return _mSattelliteLength
        End Get
    End Property
    Public ReadOnly Property _pYear() As String
        Get
            Return _mYear
        End Get
    End Property
    Public ReadOnly Property _pYearLength() As Integer
        Get
            Return _mYearLength
        End Get
    End Property
    Public ReadOnly Property _pMonth() As String
        Get
            Return _mMonth
        End Get
    End Property
    Public ReadOnly Property _pMonthLength() As Integer
        Get
            Return _mMonthLength
        End Get
    End Property
    Public ReadOnly Property _pDay() As String
        Get
            Return _mDay
        End Get
    End Property
    Public ReadOnly Property _pDayLength() As Integer
        Get
            Return _mDayLength
        End Get
    End Property
    Public ReadOnly Property _pHour() As String
        Get
            Return _mHour
        End Get
    End Property
    Public ReadOnly Property _pHourLength() As Integer
        Get
            Return _mHourLength
        End Get
    End Property
    Public ReadOnly Property _pSequenceLength() As Integer
        Get
            Return _mSequenceLength
        End Get
    End Property
    Public ReadOnly Property _pSequenceFormat() As String
        Get
            Return _mSequenceFormat
        End Get
    End Property
    Public ReadOnly Property _pIDNoLength() As Integer
        Get
            Return _mIDNoLength
        End Get
    End Property
    Public ReadOnly Property _pLastIDNo() As String
        Get
            Return _mLastIDNo
        End Get
    End Property
    Public ReadOnly Property _pLastSequence() As Long
        Get
            Return _mLastSequence
        End Get
    End Property
    Public ReadOnly Property _pNewSequence() As String
        Get
            Return _mNewSequence
        End Get
    End Property
#End Region

#Region "Routine"

    Public Sub _pSubGenerateID()
        Try
            '----------------------------------
            'Get SQL Instance Name for Sattellite.
            'Gets the value of _mServerName.
            _mSubGetSQLServerInstance()

            '----------------------------------
            'Get the IDNO Type.
            'Gets the value of _mIDNoType.
            _mSubGetAutoIDType()

            '----------------------------------
            'Get the Sattellite Prefix.
            'Sattellite is based on the Server Name.
            'Gets the value of _mSattellite.
            _mSubGetSatellite()
            _mSattelliteLength = _mSattellite.Length

            '----------------------------------
            'Get the 4 Digit Year.
            _mYear = DateTime.Now.Year.ToString("0000")
            _mYearLength = _mYear.Length

            '----------------------------------
            'Get the 2 Digit Month.
            _mMonth = DateTime.Now.Month.ToString("00")
            _mMonthLength = _mMonth.Length

            '----------------------------------
            'Get the 2 Digit Day.
            _mDay = DateTime.Now.Day.ToString("00")
            _mDayLength = _mDay.Length

            '----------------------------------
            'Get the 2 Digit Hour.
            _mHour = DateTime.Now.Hour.ToString("00")
            _mHourLength = _mHour.Length

            '----------------------------------
            'Get the Sequence Format.
            'Gets the value of _mSequenceLength and _mSequenceFormat.
            _mSubGetSequenceFormat()

            '----------------------------------
            'Get the ID Number Length.
            'Gets the value of _mIDNoLength.
            _mSubGetIDNoLength()

            '----------------------------------
            'Concatenate Query for the Last ID Number.
            'Gets the value of _mQuery
            _mSubInitLastIDNumberQuery()

            '----------------------------------
            'Get the Last ID Number.
            'Gets the value of _mLastIDNo
            _mSubGetLastIDNumber()

            '----------------------------------
            'Get the Last and New Sequence from the Last ID Number.
            'Gets the value of _mLastSequence and _mNewSequence
            _mSubGetLastSequence()

            '----------------------------------
            'Generate the New ID Number.
            'Gets the value of _mNewIDNo
            _mSubGenerateNewIDNo()

            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub

    Private Sub _mSubGetSQLServerInstance()
        Try
            '----------------------------------
            'Gets the SQL Server Instance this Application is connected to.
            Dim _nServerName As String = Nothing

            '----------------------------------
            Dim _nQuery As String = Nothing

            _nQuery = "SELECT [ServerName] = @@SERVERNAME "

            _mCmd = New SqlCommand(_nQuery, _mCxn)
            _mDr = _mCmd.ExecuteReader

            Using _mDr

                Dim _iServerName As Integer = _mDr.GetOrdinal("ServerName")

                If _mDr.HasRows Then
                    _mDr.Read()

                    _nServerName = _mDr(_iServerName).ToString

                End If

            End Using

            '----------------------------------
            _mServerName = _nServerName

            '----------------------------------
        Catch ex As Exception

        End Try

    End Sub

    Private Sub _mSubGetAutoIDType()
        Try
            '----------------------------------
            'Get the IDNO Type.
            Dim _nAutoIDType As String = Nothing

            '----------------------------------
            Dim _nQuery As String = Nothing

            _nQuery = _
                "SELECT " & _
                "[AutoIDType] " & _
                "FROM [AutoID] " & _
                "WHERE [Code] = '" & _mAutoIDCode & "' "

            _mCmd = New SqlCommand(_nQuery, _mCxn)
            _mDr = _mCmd.ExecuteReader

            Using _mDr

                Dim _iAutoIDType As Integer = _mDr.GetOrdinal("AutoIDType")

                If _mDr.HasRows Then
                    _mDr.Read()
                    _nAutoIDType = _mDr(_iAutoIDType).ToString
                End If

            End Using

            '----------------------------------
            _mAutoIDType = _nAutoIDType

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _mSubGetSatellite()
        Try
            '----------------------------------
            'Get the Sattellite Prefix.
            Dim _nSatellite As String = Nothing

            '----------------------------------
            Dim _nQuery As String = Nothing

            _nQuery = _
               "SELECT " & _
                   "* " & _
               "FROM " & _
                   "[AutoIDSatellites] " & _
               "WHERE [ServerName] = '" & _mServerName & "' "

            _mCmd = New SqlCommand(_nQuery, _mCxn)
            _mDr = _mCmd.ExecuteReader

            Using _mDr

                Dim _iCode As Integer = _mDr.GetOrdinal("Code")

                If _mDr.HasRows Then
                    _mDr.Read()
                    _nSatellite = _mDr(_iCode).ToString
                End If

            End Using

            '----------------------------------
            _mSattellite = _nSatellite

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _mSubGetSequenceFormat()
        Try
            '----------------------------------
            'IDNo Type are Hard-coded in table "SetupAutoIDType"...
            'End-user should not be allowed to edit the Codes...

            _mSequenceLength = CType(Right(_mAutoIDType, 2), Integer)

            For _nCounter As Integer = 1 To _mSequenceLength
                _mSequenceFormat += "0"
            Next

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _mSubGetIDNoLength()
        Try
            '----------------------------------
            'Get the ID Number Length.

            Dim _nStringArray() As String = Nothing

            '---------------------------------- 
            'With Sequence
            _nStringArray = Nothing
            _nStringArray =
                _mStringArray101.Union(
                _mStringArray102.Union(
                _mStringArray201.Union(
                _mStringArray202.Union(
                _mStringArray203.Union(
                _mStringArray204.Union(
                _mStringArray301.Union(
                _mStringArray302.Union(
                _mStringArray303.Union(
                _mStringArray304
                ))))))))).ToArray
            If _nStringArray.Contains(_mAutoIDType) Then
                _mIDNoLength += _mSequenceLength
            End If

            '---------------------------------- 
            'With Sattellite
            _nStringArray = Nothing
            _nStringArray =
               _mStringArray102.Union(
               _mStringArray301.Union(
               _mStringArray302.Union(
               _mStringArray303.Union(
               _mStringArray304
               )))).ToArray
            If _nStringArray.Contains(_mAutoIDType) Then
                _mIDNoLength += _mSattelliteLength
            End If

            '---------------------------------- 
            'With Year
            _nStringArray = Nothing
            _nStringArray =
                _mStringArray201.Union(
                _mStringArray202.Union(
                _mStringArray203.Union(
                _mStringArray204.Union(
                _mStringArray301.Union(
                _mStringArray302.Union(
                _mStringArray303.Union(
                _mStringArray304
                ))))))).ToArray
            If _nStringArray.Contains(_mAutoIDType) Then
                _mIDNoLength += _mYearLength
            End If

            '----------------------------------
            'With Month
            _nStringArray = Nothing
            _nStringArray =
                _mStringArray202.Union(
                _mStringArray203.Union(
                _mStringArray204.Union(
                _mStringArray302.Union(
                _mStringArray303.Union(
                _mStringArray304
                ))))).ToArray
            If _nStringArray.Contains(_mAutoIDType) Then
                _mIDNoLength += _mMonthLength
            End If

            '---------------------------------- 
            'With Day 
            _nStringArray = Nothing
            _nStringArray =
                _mStringArray203.Union(
                _mStringArray204.Union(
                _mStringArray303.Union(
                _mStringArray304
                ))).ToArray
            If _nStringArray.Contains(_mAutoIDType) Then
                _mIDNoLength += _mDayLength
            End If

            '---------------------------------- 
            'With Hour
            _nStringArray = Nothing
            _nStringArray =
               _mStringArray204.Union(
               _mStringArray304
               ).ToArray
            If _nStringArray.Contains(_mAutoIDType) Then
                _mIDNoLength += _mHourLength
            End If


            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _mSubInitLastIDNumberQuery()
        Try
            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhereAndSattellite As String = Nothing
            Dim _nWhereAndYear As String = Nothing
            Dim _nWhereAndMonth As String = Nothing
            Dim _nWhereAndDay As String = Nothing
            Dim _nWhereAndHour As String = Nothing


            Dim _nStringArray() As String = Nothing

            '---------------------------------- ----------------------------------
            '1234567890
            _nStringArray = Nothing
            _nStringArray = _mStringArray101
            If _nStringArray.Contains(_mAutoIDType) Then
                _nWhereAndSattellite = Nothing
                _nWhereAndYear = Nothing
                _nWhereAndMonth = Nothing
                _nWhereAndDay = Nothing
                _nWhereAndHour = Nothing
            End If

            '----------------------------------
            'Contains Sattellite (SA).
            'Gets the value of _nWhereAndSattellite'
            _nStringArray = Nothing
            _nStringArray =
                _mStringArray102.Union(
                _mStringArray301.Union(
                _mStringArray302.Union(
                _mStringArray303.Union(
                _mStringArray304
                )))).ToArray

            If _nStringArray.Contains(_mAutoIDType) Then
                _nWhereAndSattellite = "AND SUBSTRING([" & _mColumnName & "], " &
                    "1, " & _mSattelliteLength & ") = '" & _mSattellite & "' "
            End If

            '---------------------------------- ----------------------------------
            'Contains Year(YYYY).
            'Gets the value of _nWhereAndYear
            _nStringArray = Nothing
            _nStringArray =
                _mStringArray201.Union(
                _mStringArray202.Union(
                _mStringArray203.Union(
                _mStringArray204
                ))).ToArray()

            If _nStringArray.Contains(_mAutoIDType) Then
                _nWhereAndYear = "AND SUBSTRING([" & _mColumnName & "], " &
                    "1, " & _mYearLength & ") = '" & _mYear & "' "
            End If

            '----------------------------------
            'Contains Sattellite(SA) + Year(YYYY).
            'Gets the value of _nWhereAndYear.
            _nStringArray = Nothing
            _nStringArray =
                _mStringArray301.Union(
                _mStringArray302.Union(
                _mStringArray303.Union(
                _mStringArray304
                ))).ToArray()

            If _nStringArray.Contains(_mAutoIDType) Then
                _nWhereAndYear = "AND SUBSTRING([" & _mColumnName & "], " &
                    _mSattelliteLength + 1 & ", " & _mYearLength & ") = '" & _mYear & "' "
            End If

            '---------------------------------- ----------------------------------
            'Contains Year(YYYY) + Month(MM).
            'Gets the value of _nWhereAndMonth.
            _nStringArray = Nothing
            _nStringArray =
                 _mStringArray202.Union(
                 _mStringArray203.Union(
                 _mStringArray204
                 )).ToArray()

            If _nStringArray.Contains(_mAutoIDType) Then
                _nWhereAndMonth = "AND SUBSTRING([" & _mColumnName & "], " &
                    _mYearLength + 1 & ", " & _mMonthLength & ") = '" & _mMonth & "' "
            End If

            '----------------------------------
            'Cotains Sattellite(SA) + Year(YYYY) + Month(MM).
            'Gets the value of _nWhereAndMonth.
            _nStringArray = Nothing
            _nStringArray =
                 _mStringArray302.Union(
                 _mStringArray303.Union(
                 _mStringArray304
                 )).ToArray()

            If _nStringArray.Contains(_mAutoIDType) Then
                _nWhereAndMonth = "AND SUBSTRING([" & _mColumnName & "], " &
                    _mSattelliteLength + _mYearLength + 1 & ", " & _mMonthLength & ") = '" & _mMonth & "' "
            End If

            '---------------------------------- ----------------------------------
            'Cotains Year(YYYY) + Month(MM) + Day(DD).
            'Gets the value of _nWhereAndDay.
            _nStringArray = Nothing
            _nStringArray =
                 _mStringArray203.Union(
                 _mStringArray204
                 ).ToArray()

            If _nStringArray.Contains(_mAutoIDType) Then
                _nWhereAndDay = "AND SUBSTRING([" & _mColumnName & "], " &
                    _mYearLength + _mMonthLength + 1 & ", " & _mDayLength & ") = '" & _mDay & "' "
            End If

            '----------------------------------
            'Cotains Sattellite(SA) + Year(YYYY) + Month(MM) + Day(DD).
            'Gets the value of _nWhereAndDay.
            _nStringArray = Nothing
            _nStringArray =
                 _mStringArray303.Union(
                 _mStringArray304
                 ).ToArray()

            If _nStringArray.Contains(_mAutoIDType) Then
                _nWhereAndDay = "AND SUBSTRING([" & _mColumnName & "], " &
                    _mSattelliteLength + _mYearLength + _mMonthLength + 1 & ", " & _mDayLength & ") = '" & _mDay & "' "
            End If

            '---------------------------------- ----------------------------------
            'Cotains Year(YYYY) + Month(MM) + Day(DD) + Hour(HH).
            'Gets the value of _nWhereAndHour.
            _nStringArray = Nothing
            _nStringArray = _mStringArray204

            If _nStringArray.Contains(_mAutoIDType) Then
                _nWhereAndHour = "AND SUBSTRING([" & _mColumnName & "], " &
                    _mYearLength + _mMonthLength + _mDayLength + 1 & ", " &
                    _mHourLength & ") = '" & _mHour & "' "
            End If

            '----------------------------------
            'Cotains Sattellite(SA) + Year(YYYY) + Month(MM) + Day(DD) + Hour(HH).
            'Gets the value of _nWhereAndHour.
            _nStringArray = Nothing
            _nStringArray = _mStringArray304

            If _nStringArray.Contains(_mAutoIDType) Then
                _nWhereAndHour = "AND SUBSTRING([" & _mColumnName & "], " &
                    _mSattelliteLength + _mYearLength + _mMonthLength + _mDayLength + 1 & ", " &
                    _mHourLength & ") = '" & _mHour & "' "
            End If

            '---------------------------------- ----------------------------------

            '----------------------------------
            _nQuery = _
                "SELECT " &
                    "[" & _mColumnName & "] = MAX([" & _mColumnName & "]) " &
                "FROM [" & _mTableName & "] " &
                "WHERE " &
                    "LEN([" & _mColumnName & "]) = " & _mIDNoLength & " " &
                    _nWhereAndSattellite &
                    _nWhereAndYear &
                    _nWhereAndMonth &
                    _nWhereAndDay &
                    _nWhereAndHour &
                    _mFilterCondition

            '----------------------------------
            _mQry = Nothing
            _mQry = _nQuery

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _mSubGetLastIDNumber()
        Try
            '----------------------------------
            'Get Last IDNo.
            Dim _nLastIDNo As String = Nothing

            '----------------------------------
            _mCmd = New SqlCommand(_mQry, _mCxn)
            _mDr = _mCmd.ExecuteReader

            Using _mDr

                Dim _iLastIDNo As Integer = _mDr.GetOrdinal(_mColumnName)

                If _mDr.HasRows Then
                    _mDr.Read()
                    _nLastIDNo = _mDr(_iLastIDNo).ToString

                Else
                    _nLastIDNo = 0

                End If

            End Using

            '----------------------------------
            _mLastIDNo = _nLastIDNo

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _mSubGetLastSequence()
        Try
            '----------------------------------
            If String.IsNullOrEmpty(_mLastIDNo) Then
                _mLastSequence = 0
            Else
                _mLastSequence = CType(Right(_mLastIDNo, _mSequenceLength), Long)
            End If

            '----------------------------------
            'Check if Last Sequence is at Maximum Value.

            Dim _nMaximumSequence As String = Nothing
            For _nConter As Integer = 1 To _mSequenceLength
                _nMaximumSequence += "9"
            Next

            If _mLastSequence = CType(_nMaximumSequence, Long) Then
                _mIsMaximumSequence = True
                Exit Sub
            End If

            '----------------------------------
            _mNewSequence = Format(_mLastSequence + 1, _mSequenceFormat)

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _mSubGenerateNewIDNo()
        Try
            '----------------------------------
            'Set New IDNo to nothing if Maximum Sequence has been reached.
            If _mIsMaximumSequence Then
                _mNewIDNo = Nothing
                Exit Sub
            End If

            '----------------------------------
            _mNewIDNo = Nothing
            Dim _nStringArray() As String = Nothing

            '---------------------------------- 
            'With Sattellite
            _nStringArray = Nothing
            _nStringArray =
               _mStringArray102.Union(
               _mStringArray301.Union(
               _mStringArray302.Union(
               _mStringArray303.Union(
               _mStringArray304
               )))).ToArray
            If _nStringArray.Contains(_mAutoIDType) Then
                _mNewIDNo += _mSattellite
            End If

            '---------------------------------- 
            'With Year
            _nStringArray = Nothing
            _nStringArray =
                _mStringArray201.Union(
                _mStringArray202.Union(
                _mStringArray203.Union(
                _mStringArray204.Union(
                _mStringArray301.Union(
                _mStringArray302.Union(
                _mStringArray303.Union(
                _mStringArray304
                ))))))).ToArray
            If _nStringArray.Contains(_mAutoIDType) Then
                _mNewIDNo += _mYear
            End If

            '----------------------------------
            'With Month
            _nStringArray = Nothing
            _nStringArray =
                _mStringArray202.Union(
                _mStringArray203.Union(
                _mStringArray204.Union(
                _mStringArray302.Union(
                _mStringArray303.Union(
                _mStringArray304
                ))))).ToArray
            If _nStringArray.Contains(_mAutoIDType) Then
                _mNewIDNo += _mMonth
            End If

            '---------------------------------- 
            'With Day 
            _nStringArray = Nothing
            _nStringArray =
                _mStringArray203.Union(
                _mStringArray204.Union(
                _mStringArray303.Union(
                _mStringArray304
                ))).ToArray
            If _nStringArray.Contains(_mAutoIDType) Then
                _mNewIDNo += _mDay
            End If

            '---------------------------------- 
            'With Hour
            _nStringArray = Nothing
            _nStringArray =
               _mStringArray204.Union(
               _mStringArray304
               ).ToArray
            If _nStringArray.Contains(_mAutoIDType) Then
                _mNewIDNo += _mHour
            End If

            '---------------------------------- 
            'With Sequence
            _nStringArray = Nothing
            _nStringArray =
                _mStringArray101.Union(
                _mStringArray102.Union(
                _mStringArray201.Union(
                _mStringArray202.Union(
                _mStringArray203.Union(
                _mStringArray204.Union(
                _mStringArray301.Union(
                _mStringArray302.Union(
                _mStringArray303.Union(
                _mStringArray304
                ))))))))).ToArray
            If _nStringArray.Contains(_mAutoIDType) Then
                _mNewIDNo += _mNewSequence
            End If

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Routine Function"
    ''' <summary>
    ''' Generates a New ID Number.
    ''' </summary>
    ''' <param name="_nCxn">The Connection.</param>
    ''' <param name="_nAutoIDCode">The Hard-coded "Code" in Table "AutoID".</param>
    ''' <param name="_nTableName">The Table Name.</param>
    ''' <param name="_nColumnName">The Column Name.</param>
    ''' <returns>New ID Number</returns>
    Shared Function _pFnGetNewIDNo(
        _nCxn As SqlConnection,
        _nAutoIDCode As String,
        _nTableName As String,
        _nColumnName As String
        ) As String
        Try
            '----------------------------------
            'Instantiate Class.
            Dim _nBll As New cBllAutoIDNo
            _nBll._pCxn = _nCxn

            _nBll._pAutoIDCode = _nAutoIDCode

            _nBll._pTableName = _nTableName

            _nBll._pColumnName = _nColumnName

            'Execute the Public Sub Routine to Generate ID.
            _nBll._pSubGenerateID()

            'Final Result.
            If _nBll._pIsMaximumSequence Then
                'TODO: do something when maximum sequence is reached.
                Return Nothing
            Else
                'Return the New IDNo
                Return _nBll._pNewIDNo
            End If

            '---------- ---------- ---------- ----------
            'Dispose Class.
            _nBll.Dispose()

            '----------------------------------
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
