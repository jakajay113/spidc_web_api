

#Region "Import"

Imports System.Data.SqlClient
#End Region

Public Class cBllChronicleError
    Implements IDisposable

    'TODO
    'centralize common tasks...
    'create functions for data searches...
    'create functions for checking if record exists...

#Region "Variable Data"

    Private _mCxn As SqlConnection = Nothing

#End Region

#Region "Property Data"

    Public WriteOnly Property _pCxn() As SqlConnection
        Set(value As SqlConnection)
            _mCxn = value
        End Set
    End Property

#End Region

#Region "Routine"
    Shared Function _pFnLogError(_nEx As Exception) As String
        Try
            '---------- ---------- ---------- ----------
            'Get New IDNo.
            Dim _nIDNo As String =
                cBllAutoIDNo._pFnGetNewIDNo(
                   Spidc_Web_API_Global_Connection._pSqlCxn_CR, "ChronicleError1", "ChronicleError", "IDNo")

            '---------- ---------- ---------- ----------
            'Insert Exception in table "ChronicleError".
            If _nIDNo IsNot Nothing Then
                'DAL Class.
                Dim _nDal As New cDalChronicleError
                _nDal._pCxn = Spidc_Web_API_Global_Connection._pSqlCxn_CR

                _nDal._pIDNo = _nIDNo
                _nDal._pExDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                _nDal._pExSource = _nEx.Source.ToString
                _nDal._pExMessage = _nEx.Message.ToString
                _nDal._pExType = _nEx.GetType.ToString
                _nDal._pExStackTrace = _nEx.StackTrace.ToString

                _nDal._pSubInsert()

                'Dispose Object.
                _nDal.Dispose()
            End If

            Return _nIDNo
            '---------- ---------- ---------- ----------
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
