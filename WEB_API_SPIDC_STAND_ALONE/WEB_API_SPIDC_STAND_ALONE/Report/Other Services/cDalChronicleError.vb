

#Region "Import"

Imports System.Data.SqlClient
'Imports VS2015.Classes

#End Region

Public Class cDalChronicleError
    Implements IDisposable

#Region "Variable Data"

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

#Region "Variable Field"

    Private Const _mTableName As String = "ChronicleError"

    Private _mIDNo As String = Nothing
    Private _mExDateTime As String = Nothing
    Private _mExSource As String = Nothing
    Private _mExMessage As String = Nothing
    Private _mExType As String = Nothing
    Private _mExStackTrace As String = Nothing

    Private _mOrigIDNo As String = Nothing

#End Region

#Region "Property Field"

    Public Property _pIDNo As String
        Get
            Return _mIDNo
        End Get
        Set(value As String)
            _mIDNo = value
        End Set
    End Property
    Public Property _pExDateTime As String
        Get
            Return _mExDateTime
        End Get
        Set(value As String)
            _mExDateTime = value
        End Set
    End Property
    Public Property _pExSource As String
        Get
            Return _mExSource
        End Get
        Set(value As String)
            _mExSource = value
        End Set
    End Property
    Public Property _pExMessage As String
        Get
            Return _mExMessage
        End Get
        Set(value As String)
            _mExMessage = value
        End Set
    End Property
    Public Property _pExType As String
        Get
            Return _mExType
        End Get
        Set(value As String)
            _mExType = value
        End Set
    End Property
    Public Property _pExStackTrace As String
        Get
            Return _mExStackTrace
        End Get
        Set(value As String)
            _mExStackTrace = value
        End Set
    End Property
#End Region

#Region "Propertiy Field Original"

    Public WriteOnly Property _pOrigIDNoe() As String
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
            Dim _nOrder As String = Nothing
            '----------------------------------          
            _nQuery = _
                "SELECT " & _
                    " * " & _
                "FROM " & _
                    "[" & _mTableName & "] "

            '----------------------------------    
            _nWhere = "WHERE [IDNo] IN (SELECT [IDNo] FROM [" & _mTableName & "]) "

            If _mIDNo IsNot Nothing Then _nWhere += "AND [IDNo] = '" & _mIDNo & "' "

            '----------------------------------
            _nOrder = "ORDER BY [IDNo]"
            '----------------------------------
            _mQry = _nQuery & _nWhere & _nOrder

            _mCmd = New SqlCommand(_mQry, _mCxn)

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubInsert()
        Try
            '----------------------------------
            'Check if Primary Key(s) is not empty/ null/ whitespace.
            If String.IsNullOrEmpty(_mIDNo) Then
                Exit Sub
            End If

            '----------------------------------
            Dim _nQuery As String = Nothing

            '----------------------------------
            _nQuery =
             "INSERT INTO " &
            "[" & _mTableName & "] " &
             "(" &
                 IIf(_mIDNo IsNot Nothing, " [IDNo]", "") &
                 IIf(_mExDateTime IsNot Nothing, ", [ExDateTime]", "") &
                 IIf(_mExSource IsNot Nothing, ", [ExSource]", "") &
                 IIf(_mExMessage IsNot Nothing, ", [ExMessage]", "") &
                 IIf(_mExType IsNot Nothing, ", [ExType]", "") &
                 IIf(_mExStackTrace IsNot Nothing, ", [ExStackTrace]", "") &
             ") " &
             "VALUES " &
             "(" &
                 IIf(_mIDNo IsNot Nothing, "'" & _mIDNo & "'", "") &
                 IIf(_mExDateTime IsNot Nothing, ", '" & _mExDateTime & "'", "") &
                 IIf(_mExSource IsNot Nothing, ", '" & _mExSource & "'", "") &
                 IIf(_mExMessage IsNot Nothing, ", '" & _mExMessage & "'", "") &
                 IIf(_mExType IsNot Nothing, ", '" & _mExType & "'", "") &
                 IIf(_mExStackTrace IsNot Nothing, ", '" & _mExStackTrace & "'", "") &
             ") "

            '----------------------------------
            _mQry = _nQuery

            _mCmd = New SqlCommand(_mQry, _mCxn)

            _mCmd.ExecuteNonQuery()

            _mCmd.Dispose()

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubUpdate()

        Try
            '----------------------------------
            'Check if Primary Key(s) is not empty/ null/ whitespace.
            If String.IsNullOrEmpty(_mOrigIDNo) Then
                'Prevent Bulk Update.
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
                    IIf(_mIDNo Is Nothing, "", " [IDNo] = '" & _mIDNo & "'") & _
                    IIf(_mExDateTime Is Nothing, "", ", [ExDateTime] = '" & _mExDateTime & "'") & _
                    IIf(_mExSource Is Nothing, "", ", [ExSource] = '" & _mExSource & "'") & _
                    IIf(_mExMessage Is Nothing, "", ", [ExMessage] = '" & _mExMessage & "'") & _
                    IIf(_mExType Is Nothing, "", ", [ExType] = '" & _mExType & "'") & _
                    IIf(_mExStackTrace Is Nothing, "", ", [ExStackTrace] = '" & _mExStackTrace & "'") & _
                " "

            '----------------------------------
            _nWhere = "WHERE [IDNo] = '" & _mOrigIDNo & "' "

            '----------------------------------
            _mQry = _nQuery & _nWhere

            _mCmd = New SqlCommand(_mQry, _mCxn)

            _mCmd.ExecuteNonQuery()

            _mCmd.Dispose()

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubDelete()
        Try
            '----------------------------------
            'Check if Primary Key(s) is not empty/ null/ whitespace.
            If String.IsNullOrEmpty(_mIDNo) Then
                'Prevent Bulk Delete.
                Exit Sub
            End If

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            '----------------------------------
            _nQuery = _
                "DELETE FROM " & _
                "[" & _mTableName & "] "

            '----------------------------------
            _nWhere = "WHERE [IDNo] = '" & _mIDNo & "' "

            '----------------------------------
            _mQry = _nQuery & _nWhere

            _mCmd = New SqlCommand(_mQry, _mCxn)

            _mCmd.ExecuteNonQuery()

            _mCmd.Dispose()

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubClear()
        Try
            '----------------------------------
            'Warning: 
            'This method willl delete all records.
            'This cannot be undone.

            Dim _nQuery As String = Nothing
            _nQuery = _
               "DELETE FROM " & _
               "[" & _mTableName & "] "

            _mQry = _nQuery

            _mCmd = New SqlCommand(_mQry, _mCxn)

            _mCmd.ExecuteNonQuery()

            _mCmd.Dispose()

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubGetSeq()
        Try
            '----------------------------------
            'TODO: Perform Checking of Primary Keys to be inserted here if is nothing.

            '----------------------------------
            Dim _nQuery As String = Nothing
            Dim _nWhere As String = Nothing

            '----------------------------------  

            _nQuery = _
            "SELECT (CONVERT(varchar(6), GETDATE(), 12) + " & _
            "REPLACE(CONVERT(varchar, GETDATE(), 114), ':','')) AS DateSeq "

            _mQry = _nQuery

            '----------------------------------
            _mCmd = New SqlCommand(_mQry, _mCxn)

            _mCmd.ExecuteNonQuery()
            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Routine"

    Public Sub _pSubRecordSelectSpecific()
        Try
            '----------------------------------
            'Check if Primary Key(s) is not empty/ null/ whitespace.
            If String.IsNullOrEmpty(_mIDNo) Then
                Exit Sub
            End If

            '----------------------------------
            _pSubSelect()

            Using _nDr As SqlDataReader = _pDr
                '----------------------------------
                'Declare Indexes to Retrieve Data Faster.
                'Use Exact Column Name Case for Efficiency.
                Dim _iIDNo As Integer = _nDr.GetOrdinal("IDNo")
                Dim _iExDateTime As Integer = _nDr.GetOrdinal("ExDateTime")
                Dim _iExSource As Integer = _nDr.GetOrdinal("ExSource")
                Dim _iExMessage As Integer = _nDr.GetOrdinal("ExMessage")
                Dim _iExType As Integer = _nDr.GetOrdinal("ExType")
                Dim _iExStackTrace As Integer = _nDr.GetOrdinal("ExStackTrace")

                '----------------------------------
                'Read Data.
                If _nDr.HasRows Then
                    _nDr.Read()

                    _mIDNo = _nDr(_iIDNo).ToString
                    _mExDateTime = _nDr(_iExDateTime).ToString
                    _mExSource = _nDr(_iExSource).ToString
                    _mExMessage = _nDr(_iExMessage).ToString
                    _mExType = _nDr(_iExType).ToString
                    _mExStackTrace = _nDr(_iExStackTrace).ToString

                End If

            End Using 'Disposes Object Automatically.

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

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
