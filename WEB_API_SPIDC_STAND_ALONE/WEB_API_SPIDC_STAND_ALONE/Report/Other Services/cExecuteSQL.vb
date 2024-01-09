Imports System.Data.SqlClient


''Standard Class for CRUD Operation


Public Class cExecuteSQL
#Region "Variable Object"
    Private _mSqlCon As New SqlConnection
    Private _mSqlCmd As SqlCommand
    Private _mDataTable As New DataTable

#End Region

#Region "Property Object"
    Public ReadOnly Property _pDataTable() As DataTable
        Get

            Try
                Dim _nSqlDataAdapter As New SqlDataAdapter(_mSqlCmd)
                _mDataTable = New DataTable
                _nSqlDataAdapter.Fill(_mDataTable)

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

#Region "Routine"

    Public Sub _pExec(ByVal _StrSQL As String, ByRef _nSuccessful As Boolean, Optional ByRef _nErrMsg As String = Nothing)
        Try
            Dim _nOutput As String = ""

            _mSqlCmd = New SqlCommand(_StrSQL, _mSqlCon)

            'Execute and Read the content
            _mSqlCmd.CommandTimeout = 0
            Using _nSqlDr As SqlDataReader = _mSqlCmd.ExecuteReader

                If _nSqlDr.HasRows Then
                    'Getting Record using reader
                    Do While _nSqlDr.Read

                    Loop
                End If

            End Using

            _mSqlCmd.Dispose()

            _nSuccessful = True
            _nErrMsg = Nothing
        Catch ex As Exception
            _nSuccessful = False
            _nErrMsg = ex.Message
            MsgBox(_nErrMsg, vbOKOnly + MsgBoxStyle.Critical, "Warning")
        End Try
    End Sub

#End Region


End Class
