#Region "Imports"

Imports System.Data.SqlClient

#End Region
Public Class cDalAutoGenID

#Region "Variable Object"
    Private _mSqlCon As New SqlConnection
    Private _mSqlCmd As SqlCommand
    Private _mDataTable As New DataTable
#End Region

#Region "Property Object"
    Public ReadOnly Property _pDataTable() As DataTable
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

#Region "Variables"

    Private _mGenID As String

#End Region

#Region "Property Field"

    Public Property _pGenID() As String
        Get
            Return _mGenID
        End Get
        Set(value As String)
            _mGenID = value
        End Set
    End Property

#End Region

#Region "Routines"

    Public Function _FnAutoGenID(ByVal _nModuleID As String, Optional _nReferenceCode As String = Nothing) As String
        _FnAutoGenID = Nothing
        Try
            Dim _nErrMsg As String = ""
            'Dim _nOutput As String = ""
            Dim _nStrSql As String
            Dim _nSelectCond As String = ""
            'Initialize String SQL
            '_nSelectCond = Replace(_nCondition, "'", "''")
            _nStrSql = "declare @myCode varchar(50) " & _
                            "EXEC [sp_getCode] " & _
                            "@moduleID = N'" & _nModuleID & "' " & _
                            ",@ReferenceCode = N'" & _nReferenceCode & "' " & _
                            ",@output = @myCode output " & _
                            "select @myCode as Result"

            _mSqlCmd = New SqlCommand(_nStrSql, _mSqlCon)
            'set paramater Success
            '_mSqlCmd.Parameters.Add("@Successful", SqlDbType.Bit)
            '_mSqlCmd.Parameters("@Successful").Direction = ParameterDirection.Output

            'set paramater Error
            _mSqlCmd.Parameters.Add("@output", SqlDbType.NVarChar, 2000)
            _mSqlCmd.Parameters("@output").Direction = ParameterDirection.Output

            'Execute and Read the content
            Dim _nSqlDataAdapter As New SqlDataAdapter(_nStrSql, _mSqlCon) '_gDBCon
            _mDataTable.Clear()
            _nSqlDataAdapter.Fill(_mDataTable)

            Dim _nSqlDr As SqlDataReader = _mSqlCmd.ExecuteReader
            Using _nSqlDr
                If _nSqlDr.HasRows Then
                    'Getting Record using reader
                    Do While _nSqlDr.Read
                        _mGenID = _nSqlDr.Item("Result").ToString
                        Return _nSqlDr.Item("Result").ToString
                    Loop
                End If
            End Using

            'Get values of parameter output
            '_nSuccessful = _mSqlCmd.Parameters("@Successful").Value
            '_nErrMsg = _mSqlCmd.Parameters("@ErrMsg").Value
            _mSqlCmd.Dispose()

        Catch ex As Exception
            _FnAutoGenID = Nothing
            '_nSuccessful = False
        End Try
        Return _FnAutoGenID
    End Function




#End Region


End Class

