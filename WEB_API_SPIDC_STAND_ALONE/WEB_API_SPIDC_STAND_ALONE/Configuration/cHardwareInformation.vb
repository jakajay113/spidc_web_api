#Region "Imports"

Imports System.Web
Imports System.Web.HttpServerUtility
Imports System.Net

#End Region

Public Class cHardwareInformation
    Implements IDisposable

#Region "Variable"

    Private _mMachineName As String
    Private _mMachineIP As String

#End Region

#Region "Property"
    Public ReadOnly Property _pMachineName() As String
        Get
            _mMachineName = System.Environment.MachineName
            Return _mMachineName
        End Get
    End Property

    Public ReadOnly Property _pMachineIP() As String
        Get
            _mMachineIP = Dns.GetHostByName(Environment.MachineName).AddressList(0).ToString()
            Return _mMachineIP
        End Get
    End Property



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
