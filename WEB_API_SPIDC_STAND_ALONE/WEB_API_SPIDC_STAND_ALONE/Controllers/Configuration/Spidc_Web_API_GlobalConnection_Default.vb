

#Region "Import"

Imports System.Data.SqlClient

#End Region

Public Class Spidc_Web_API_GlobalConnection_Default
    Implements IDisposable

#Region "Variables Data"
    Private _mCxn As SqlConnection = Nothing
    Private _mQry As String = Nothing
    Private _mCmd As SqlCommand = Nothing
    Private _mDt As DataTable = Nothing
    Private _mDr As SqlDataReader = Nothing
#End Region

#Region "Properties Data"

    Public Property _pCxn() As SqlConnection
        Get
            Return _mCxn
        End Get
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
                'Dim _nDa As New SqlDataAdapter(_mCmd)
                '_nDa.Fill(_mDt)
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

    Private Const _mTableName As String = "GlobalConnectionsDefault"

    Private _mSetupGlobalConnectionsDatabases As String = Nothing
    Private _mDBDataSource As String = Nothing
    Private _mDBInitialCatalog As String = Nothing
    Private _mDBUserID As String = Nothing
    Private _mDBUserKey As String = Nothing

    Private _mOrigSetupGlobalConnectionsDatabases As String = Nothing

#End Region

#Region "Property Field"

    Public Property _pSetupGlobalConnectionsDatabases As String
        Get
            Return _mSetupGlobalConnectionsDatabases
        End Get
        Set(value As String)
            _mSetupGlobalConnectionsDatabases = value
        End Set
    End Property
    Public Property _pDBDataSource As String
        Get
            Return _mDBDataSource
        End Get
        Set(value As String)
            _mDBDataSource = value
        End Set
    End Property
    Public Property _pDBInitialCatalog As String
        Get
            Return _mDBInitialCatalog
        End Get
        Set(value As String)
            _mDBInitialCatalog = value
        End Set
    End Property
    Public Property _pDBUserID As String
        Get
            Return _mDBUserID
        End Get
        Set(value As String)
            _mDBUserID = value
        End Set
    End Property
    Public Property _pDBUserKey As String
        Get
            Return _mDBUserKey
        End Get
        Set(value As String)
            _mDBUserKey = value
        End Set
    End Property

#End Region

#Region "Propertiy Field Original"

    Public Property _pOrigSetupGlobalConnectionsDatabases() As String
        Get
            Return _mOrigSetupGlobalConnectionsDatabases
        End Get
        Set(value As String)
            _mOrigSetupGlobalConnectionsDatabases = value
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
                    " * " & _
                "FROM [" & _mTableName & "] "

            '----------------------------------    
            _nWhere = "WHERE [SetupGlobalConnectionsDatabases] IN (SELECT [SetupGlobalConnectionsDatabases] FROM [" & _mTableName & "]) "

            If Not _mSetupGlobalConnectionsDatabases Is Nothing Then _
                _nWhere += "AND [SetupGlobalConnectionsDatabases] = '" & _mSetupGlobalConnectionsDatabases & "' "

            '----------------------------------
            _mQry = _nQuery & _nWhere

            _mCmd = New SqlCommand(_mQry, _mCxn)

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Sub _pSubInsert()
        Try
            '----------------------------------
            'Perform Checking of Primary Keys before Inserting.
            If String.IsNullOrEmpty(_mSetupGlobalConnectionsDatabases) Then
                Exit Sub
            End If

            '----------------------------------
            Dim _nQuery As String = Nothing

            '----------------------------------
            _nQuery = _
             "INSERT INTO " & _
            "[" & _mTableName & "] " & _
             "(" & _
                IIf(_mSetupGlobalConnectionsDatabases Is Nothing, "", " [SetupGlobalConnectionsDatabases]") & _
                IIf(_mDBDataSource Is Nothing, "", ", [DBDataSource]") & _
                IIf(_mDBInitialCatalog Is Nothing, "", ", [DBInitialCatalog]") & _
                IIf(_mDBUserID Is Nothing, "", ", [DBUserID]") & _
                IIf(_mDBUserKey Is Nothing, "", ", [DBUserKey]") & _
             ") " & _
             "VALUES " & _
             "(" & _
                IIf(_mSetupGlobalConnectionsDatabases Is Nothing, "", "'" & _mSetupGlobalConnectionsDatabases & "'") & _
                IIf(_mDBDataSource Is Nothing, "", ", '" & _mDBDataSource & "'") & _
                IIf(_mDBInitialCatalog Is Nothing, "", ", '" & _mDBInitialCatalog & "'") & _
                IIf(_mDBUserID Is Nothing, "", ", '" & _mDBUserID & "'") & _
                IIf(_mDBUserKey Is Nothing, "", ", '" & _mDBUserKey & "'") & _
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
            'Perform Checking of Primary Keys before Updating.
            If String.IsNullOrEmpty(_mOrigSetupGlobalConnectionsDatabases) Then
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
                    IIf(_mSetupGlobalConnectionsDatabases Is Nothing, "", " [SetupGlobalConnectionsDatabases] = '" & _mSetupGlobalConnectionsDatabases & "'") & _
                    IIf(_mDBDataSource Is Nothing, "", ", [DBDataSource] = '" & _mDBDataSource & "'") & _
                    IIf(_mDBInitialCatalog Is Nothing, "", ", [DBInitialCatalog] = '" & _mDBInitialCatalog & "'") & _
                    IIf(_mDBUserID Is Nothing, "", ", [DBUserID] = '" & _mDBUserID & "'") & _
                    IIf(_mDBUserKey Is Nothing, "", ", [DBUserKey] = '" & _mDBUserKey & "'") & _
                " "

            '----------------------------------
            _nWhere = "WHERE [SetupGlobalConnectionsDatabases] = '" & _mOrigSetupGlobalConnectionsDatabases & "' "

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
            If String.IsNullOrEmpty(_mSetupGlobalConnectionsDatabases) Then
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
            _nWhere = "WHERE [SetupGlobalConnectionsDatabases] = '" & _mSetupGlobalConnectionsDatabases & "' "

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
            'This method/ routine willl delete all records.
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

#End Region

#Region "Routine"

    Public Sub _pSubRecordSelectSpecific()
        Try
            '----------------------------------
            'Check if Primary key is not empty/ null/ whitespace.
            If String.IsNullOrEmpty(_mSetupGlobalConnectionsDatabases) Then

                Exit Sub
            End If

            '----------------------------------
            _pSubSelect()

            Using _nDr As SqlDataReader = _mCmd.ExecuteReader


                'Declare Indexes for Fast Retrieval of Data.
                Dim _iSetupGlobalConnectionsDatabases As Integer = _nDr.GetOrdinal("SetupGlobalConnectionsDatabases")
                Dim _iDBDataSource As Integer = _nDr.GetOrdinal("DBDataSource")
                Dim _iDBInitialCatalog As Integer = _nDr.GetOrdinal("DBInitialCatalog")
                Dim _iDBUserID As Integer = _nDr.GetOrdinal("DBUserID")
                Dim _iDBUserKey As Integer = _nDr.GetOrdinal("DBUserKey")

                If _nDr.HasRows Then
                    _nDr.Read()

                    _mSetupGlobalConnectionsDatabases = _nDr(_iSetupGlobalConnectionsDatabases).ToString
                    _mDBDataSource = _nDr(_iDBDataSource).ToString
                    _mDBInitialCatalog = _nDr(_iDBInitialCatalog).ToString
                    _mDBUserID = _nDr(_iDBUserID).ToString
                    _mDBUserKey = _nDr(_iDBUserKey).ToString

                End If

            End Using

            '----------------------------------
        Catch ex As Exception

        End Try
    End Sub

    Public Function _pFuncRecordExists() As Boolean
        Try
            '----------------------------------
            'Check if Primary key is not empty/ null/ whitespace.
            If String.IsNullOrEmpty(_mSetupGlobalConnectionsDatabases) Then

                Return Nothing
            End If

            '----------------------------------
            _pSubSelect()

            Dim _nDr As SqlDataReader = _pDr

            If _nDr Is Nothing Or Not _nDr.HasRows Then
                _nDr.Close()
                Return False

            End If

            If _nDr.HasRows Then
                _nDr.Close()
                Return True

            End If

            Return Nothing
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
