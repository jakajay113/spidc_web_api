

#Region "Imports"

Imports System.IO
Imports System.Security.Cryptography

#End Region

Public Class cEncryption
    Implements IDisposable

    'excerpts from http://weblogs.asp.net/dwahlin/archive/2009/05/21/encrypting-data-in-net-applications.aspx ...
    'Get Encryption Key here https://www.grc.com/passwords.htm ....
    Private _mKey As String = "C1B32EBED50A5F1CA4AF0A6B405A5F5BC5179B98644522618D499056A528D80E" 'Be careful storing this in code unless it’s secured and not distributed
    'Private _nSalt As Byte() = New Byte() {&H45, &HF1, &H61, &H6E, &H20, &H0, &H65, &H64, &H76, &H65, &H64, &H3, &H76}
    'Shared _Pwd As String = "2B4B99CE8A2EBA33AD2188FA5DE9A9764EDEC4A155E95653BA3516DF0AD6C448" 'Be careful storing this in code unless it’s secured and not distributed
    'Shared _Salt As Byte() = New Byte() {&H45, &HF1, &H61, &H6E, &H20, &H0, &H65, &H64, &H76, &H65, &H64, &H3, &H76}
    'How to Use...
    ' dim creditCardNumber  as string =  Encryptor2.Encrypt(cust.CreditCardNumber)
    'Just always compare encrypted data.... no decryption needed....

    Public Function Encrypt(ByVal _nClearText As String, ByVal _nSalt As Byte()) As String

        'Dim _nMethod As String = Reflection.MethodBase.GetCurrentMethod.Name
        '_gLog._WriteEvent(_gAssemblyInfo._pProduct, _nMethod, "Begin")
        '----------------------------------
        Encrypt = Nothing
        Try
            Dim clearBytes As Byte() = System.Text.Encoding.Unicode.GetBytes(_nClearText)
            Dim pdb As New Rfc2898DeriveBytes(_mKey, _nSalt)
            Dim encryptedData As Byte() = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16))
            Return Convert.ToBase64String(encryptedData)
            '----------------------------------
        Catch ex As Exception
            '_gLog._WriteEvent(_gAssemblyInfo._pProduct, _nMethod, , ex)
        Finally
            '_gLog._WriteEvent(_gAssemblyInfo._pProduct, _nMethod, "End")
        End Try
    End Function

    Private Function Encrypt(ByVal clearData As Byte(), ByVal Key As Byte(), ByVal IV As Byte()) As Byte()

        'Dim _nMethod As String = Reflection.MethodBase.GetCurrentMethod.Name
        '_gLog._WriteEvent(_gAssemblyInfo._pProduct, _nMethod, "Begin")
        '----------------------------------
        Encrypt = Nothing
        Dim ms As New MemoryStream()
        Dim cs As CryptoStream = Nothing
        Try
            Dim alg As Rijndael = Rijndael.Create()
            alg.Key = Key
            alg.IV = IV
            cs = New CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write)
            cs.Write(clearData, 0, clearData.Length)
            cs.FlushFinalBlock()
            Return ms.ToArray()
            '----------------------------------
        Catch ex As Exception
            '_gLog._WriteEvent(_gAssemblyInfo._pProduct, _nMethod, , ex)
        Finally
            cs.Close()
            '_gLog._WriteEvent(_gAssemblyInfo._pProduct, _nMethod, "End")
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

