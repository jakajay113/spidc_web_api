

#Region "Imports"

Imports System.Data.SqlClient
Imports System.Data.SqlTypes

#End Region

Public Class cReturnDataType
    Implements IDisposable

    Shared Function _pIIF(Of T)(ByVal _nExpression As Boolean, ByVal _nTruePart As T, ByVal _nFalsePart As T) As T
        Try
            '----------------------------------
            If _nExpression Then
                Return _nTruePart
            Else
                Return _nFalsePart
            End If
            '----------------------------------
        Catch ex As Exception
            _pIIF = _nFalsePart
        End Try
    End Function
    Public Function _pIsNothingString(ByVal _nString As String) As Boolean
        '----------------------------------
        Try
            _nString = _pIIF(_nString = "" Or _nString = Nothing, Nothing, _nString)
            If _nString Is Nothing Then
                Return True
                Exit Function
            End If

            Return False
            '----------------------------------
        Catch ex As Exception
            Return False
        End Try
    End Function
    Shared Function _pYieldString(ByVal _nObject As Object) As String
        Try
            '----------------------------------
            If String.IsNullOrEmpty(_nObject) Then
                Return Nothing
            End If

            If String.IsNullOrWhiteSpace(_nObject) Then
                Return Nothing
            End If

            If String.IsNullOrWhiteSpace(_nObject) Then
                Return Nothing
            End If

            Return _nObject.ToString
            '----------------------------------
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Shared Function _pYieldInteger(ByVal _nObject As Object) As Integer
        Try
            '----------------------------------
            If String.IsNullOrWhiteSpace(_nObject) Then
                Return Nothing
            End If

            Return _nObject
            '----------------------------------
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Shared Function _pYieldDecimal(ByVal _nObject As Object) As Decimal
        Try
            '----------------------------------
            If String.IsNullOrWhiteSpace(_nObject) Then
                Return Nothing
            End If

            Return _nObject
            '----------------------------------
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Shared Function _pYieldByteArray(ByVal _nObject As Object) As Byte()
        Try
            '----------------------------------
            If String.IsNullOrWhiteSpace(_nObject) Then
                Return Nothing
            End If

            Return _nObject
            '----------------------------------
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Shared Function _pYieldBoolean(ByVal _nObject As Object) As Boolean
        Try
            '----------------------------------
            If _nObject.[GetType]() = System.DBNull.Value.[GetType]() Then
                Return False
            End If

            'If IsDBNull(_nObject) Then
            '    Return False
            'End If

            Return _nObject
            '----------------------------------
        Catch ex As Exception
            Return False
        End Try
    End Function

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
