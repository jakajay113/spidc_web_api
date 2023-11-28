Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.OpenSsl
Imports Org.BouncyCastle.Security
Imports System.Security.Cryptography
Imports System.IO
Imports Org.BouncyCastle.Crypto.Parameters

Public Class GCashModel
    Public Class Head
        Public Property reqTime As String
        Public Property reqMsgId As String
        Public Property clientSecret As String
        Public Property clientId As String
        Public Property _function As String
        Public Property version As String
    End Class

    Public Class EnvInfo
        Public Property terminalType As String
        Public Property orderTerminalType As String
    End Class

    Public Class OrderAmount
        Public Property currency As String
        Public Property value As String
    End Class

    Public Class Buyer
        Public Property externalUserId As String
        Public Property externalUserType As String
        Public Property userId As String
    End Class

    Public Class Seller
        Public Property externalUserId As String
        Public Property externalUserType As String
        Public Property userId As String
    End Class

    Public Class Order
        Public Property orderTitle As String
        Public Property merchantTransId As String
        Public Property createdTime As String
        Public Property orderAmount As OrderAmount
        Public Property expirytime As String
        Public Property buyer As Buyer
        Public Property seller As Seller
    End Class

    Public Class NotificationUrl
        Public Property type As String
        Public Property url As String
    End Class

    Public Class Body
        Public Property envInfo As EnvInfo
        Public Property order As Order
        Public Property productCode As String
        Public Property merchantId As String
        Public Property subMerchantId As String
        Public Property subMerchantName As String
        Public Property notificationUrls As NotificationUrl()
    End Class

    Public Class Request
        Public Property head As Head
        Public Property body As Body
    End Class

    Public Class Gcash_OrderCreate
        Public Property request As Request
        Public Property signature As String
    End Class


    Public Shared PublicKey_XML As String = "<RSAKeyValue><Modulus>yl2KagdfJ2AS2L6tE19mC4rkbWsM8RzoVuM7G9a2EFCXlIyr0bQ1i2zFywZuDLp8jMLS/vkKfuSE42iki1mIP/UJTtVeQRId2/dKDNH8dv9tQUYx33ZdXm9JRuByVOeW3rqLAL/dv0eg7mIkStzaTsxHOOrsDWhVg/EC9EBxx1pNJbUp/U0Nn21vMQMQH33IZPdRWp3HMfRYqtMmAol3mjF/lzFqUxpedTCHIbmXiirdSQqze7VXxYUqqwspvnf5ISC14XbfG7/b3taOyECvXCNIUxS650HCpB9TZg3oZLtiyp4u005++dOog5CuG4U65IQcQotKJBNyes0L0zrNCQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>"
    Public Shared PrivateKey_XML As String = "<RSAKeyValue><Modulus>yl2KagdfJ2AS2L6tE19mC4rkbWsM8RzoVuM7G9a2EFCXlIyr0bQ1i2zFywZuDLp8jMLS/vkKfuSE42iki1mIP/UJTtVeQRId2/dKDNH8dv9tQUYx33ZdXm9JRuByVOeW3rqLAL/dv0eg7mIkStzaTsxHOOrsDWhVg/EC9EBxx1pNJbUp/U0Nn21vMQMQH33IZPdRWp3HMfRYqtMmAol3mjF/lzFqUxpedTCHIbmXiirdSQqze7VXxYUqqwspvnf5ISC14XbfG7/b3taOyECvXCNIUxS650HCpB9TZg3oZLtiyp4u005++dOog5CuG4U65IQcQotKJBNyes0L0zrNCQ==</Modulus><Exponent>AQAB</Exponent><P>+kIsotWEndjB1fkyQ/PUjpb4W+52xmUE5I3KKJjhpLzj1ShM5aSKBehg+6VZCRk0TkbNnZ4IpRLzWUM68i0P3WMym2rEOdSn4N8cIqPTZ27LVVa4Qsx4LXuGAfDKQ1c0ccbemj7hs7A88fdsBPC1btHEhn0gNAx+1sNqjT3Jm98=</P><Q>zwIUQlTN7bz3EBJytwKlbjEe2vRyTZbZM3opfp8tt1R0XyagKRKgzbt/wQ5KRPVfp8uhu6f/hNlOguXnC/aTVJE4lOjEjcJO8vaoaJY6gEeM5LPHXpiM0KbHaODf6ZfnILYYHQK5IqAsFbq3ypIm+Gj+CG1a56i8ShxDnGwbtBc=</Q><DP>orW3s79DVCWEEYzOA6RaxMKfg5YNn3w6rCcME00jGCq/ru1e4cgS/ThTJgohU2sRuPsL4LjJQhqKIbU2dBKcSAKg+q92GWuMIwasklEVuCAvD7MsZjHuyROQ014tA4+FR5xXSs4rjNq/JUsK7kNak0zLi+16rJybMyMm+eH8XN0=</DP><DQ>OE/0eube7iFNhE3AbxCFOCicoCuHPJwgkeRVjWB36ztKPOghAYtTuyOaaUYd5gxp7Rdz87yLwfPra6hm5dY51fN7VObNMVL+bxGyVmEnoOXV6hEN1ynghJIaBOnHf6AW+8sNXDb1bfnshCy6+pwvhbvp/xjgdcxtDCwMKG0wTBk=</DQ><InverseQ>9PsydJga7rk05pIfYZ0tJdjWhpZkbtEhU25yXGFaoK/IxFH3QT2JNg5AGDMP2PbdrMe1x8l51ZBJ4Oheesx9kmy55o4bb+bpfJOiiww7RP3ofvMmbJhyQnpnd83FWGIL/GrdQNm3SH30UPNQ8QlvflzBkduU0dK5A9/duGgvVWw=</InverseQ><D>pjKsuufS/kOpNtliy8ZNyK0JbdO9jUhwiOuWYAa3AC8wTpA5jMC0OQ3cZCGjwFBoejKHl2BxfkWv3hTTDp85h013l4clIcv/OeieqGjNL+4XjMRZsW1EwYokZFFxlMme3s3V24e4VqA9S4rOw60enhinZdeKhnhpwzLteixzRYUsyrzyyVNz+QSMnmPmfQjg8LYdBrMhfApKi3mCaEhg1MOhUHjTw/23xLADUOZ880O8INz1+Z+BSC4/C/K0HTUjX1S1mxsvtZ5lOw3QXbjWvxNFZ9cMXCoMOZJrsXM3QiM+sIgZa1Bh9MHd0iHXrwj4zTJpIyom+GIwo3V5yPitAQ==</D></RSAKeyValue>"
    Public Shared PublicKey_PEM As String = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAyl2KagdfJ2AS2L6tE19mC4rkbWsM8RzoVuM7G9a2EFCXlIyr0bQ1i2zFywZuDLp8jMLS/vkKfuSE42iki1mIP/UJTtVeQRId2/dKDNH8dv9tQUYx33ZdXm9JRuByVOeW3rqLAL/dv0eg7mIkStzaTsxHOOrsDWhVg/EC9EBxx1pNJbUp/U0Nn21vMQMQH33IZPdRWp3HMfRYqtMmAol3mjF/lzFqUxpedTCHIbmXiirdSQqze7VXxYUqqwspvnf5ISC14XbfG7/b3taOyECvXCNIUxS650HCpB9TZg3oZLtiyp4u005++dOog5CuG4U65IQcQotKJBNyes0L0zrNCQIDAQAB"
    Public Shared PrivateKey_PEM As String = "MIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQDKXYpqB18nYBLYvq0TX2YLiuRtawzxHOhW4zsb1rYQUJeUjKvRtDWLbMXLBm4MunyMwtL++Qp+5ITjaKSLWYg/9QlO1V5BEh3b90oM0fx2/21BRjHfdl1eb0lG4HJU55beuosAv92/R6DuYiRK3NpOzEc46uwNaFWD8QL0QHHHWk0ltSn9TQ2fbW8xAxAffchk91Fanccx9Fiq0yYCiXeaMX+XMWpTGl51MIchuZeKKt1JCrN7tVfFhSqrCym+d/khILXhdt8bv9ve1o7IQK9cI0hTFLrnQcKkH1NmDehku2LKni7TTn7506iDkK4bhTrkhBxCi0okE3J6zQvTOs0JAgMBAAECggEBAKYyrLrn0v5DqTbZYsvGTcitCW3TvY1IcIjrlmAGtwAvME6QOYzAtDkN3GQho8BQaHoyh5dgcX5Fr94U0w6fOYdNd5eHJSHL/znonqhozS/uF4zEWbFtRMGKJGRRcZTJnt7N1duHuFagPUuKzsOtHp4Yp2XXioZ4acMy7Xosc0WFLMq88slTc/kEjJ5j5n0I4PC2HQazIXwKSot5gmhIYNTDoVB408P9t8SwA1DmfPNDvCDc9fmfgUguPwvytB01I19UtZsbL7WeZTsN0F241r8TRWfXDFwqDDmSa7FzN0IjPrCIGWtQYfTB3dIh168I+M0yaSMqJvhiMKN1ecj4rQECgYEA+kIsotWEndjB1fkyQ/PUjpb4W+52xmUE5I3KKJjhpLzj1ShM5aSKBehg+6VZCRk0TkbNnZ4IpRLzWUM68i0P3WMym2rEOdSn4N8cIqPTZ27LVVa4Qsx4LXuGAfDKQ1c0ccbemj7hs7A88fdsBPC1btHEhn0gNAx+1sNqjT3Jm98CgYEAzwIUQlTN7bz3EBJytwKlbjEe2vRyTZbZM3opfp8tt1R0XyagKRKgzbt/wQ5KRPVfp8uhu6f/hNlOguXnC/aTVJE4lOjEjcJO8vaoaJY6gEeM5LPHXpiM0KbHaODf6ZfnILYYHQK5IqAsFbq3ypIm+Gj+CG1a56i8ShxDnGwbtBcCgYEAorW3s79DVCWEEYzOA6RaxMKfg5YNn3w6rCcME00jGCq/ru1e4cgS/ThTJgohU2sRuPsL4LjJQhqKIbU2dBKcSAKg+q92GWuMIwasklEVuCAvD7MsZjHuyROQ014tA4+FR5xXSs4rjNq/JUsK7kNak0zLi+16rJybMyMm+eH8XN0CgYA4T/R65t7uIU2ETcBvEIU4KJygK4c8nCCR5FWNYHfrO0o86CEBi1O7I5ppRh3mDGntF3PzvIvB8+trqGbl1jnV83tU5s0xUv5vEbJWYSeg5dXqEQ3XKeCEkhoE6cd/oBb7yw1cNvVt+eyELLr6nC+Fu+n/GOB1zG0MLAwobTBMGQKBgQD0+zJ0mBruuTTmkh9hnS0l2NaGlmRu0SFTbnJcYVqgr8jEUfdBPYk2DkAYMw/Y9t2sx7XHyXnVkEng6F56zH2SbLnmjhtv5ul8k6KLDDtE/eh+8yZsmHJCemd3zcVYYgv8at1A2bdIffRQ81DxCW9+XMGR25TR0rkD3924aC9VbA=="
    Public Shared ClientId As String
    Public Shared ClientSecret As String
    Public Shared MerchantID As String
    Public Shared ProductCode As String
    Public Shared GCASH_PublicKey_PEM As String
    Public Shared ReqMsgID As String
    Public Shared GCASH_PubKey As String = "-----BEGIN PUBLIC KEY-----" & vbCrLf & "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsWcpXRPXOH3m0sytTeuPoFMCSWwg+ZJVc+K2krCJqRjWsiCtaGx6jUgRvL+eTJVIAvjwxpH3ftmnUjORCaD12Oam7L37c5tst5PHHyDC+ChPsdjLf8I6tl6bNUzdN08Cse+q09CIoaRo4KwO5FQupdqZjIXILogr14dMPgELCcxnqOgZ4bKqgVT2QH97f4Mx0uw+bWadOtMnmMkRtR1WEVULn7SYG7DMHTEEAsFhrX9fVDvoYBvBM5AIH+0nD2ZYOqR63VfgUxaiGk5d3BztK5UNfA+WjeclkKPVI7ED1NPdiojlUXrUchwMGYI+GPTHM8UkqAWtcDxobHrhnfMmoQIDAQAB" & vbCrLf & "-----END PUBLIC KEY-----"
    Public Shared Function Do_Sign(ByVal StringToSign As String) As String
        Try
            Dim originalData As Byte() = Encoding.UTF8.GetBytes(StringToSign)
            Dim signedData As Byte()
            Dim rsa As RSACryptoServiceProvider = New RSACryptoServiceProvider(2048)
            Dim XML_PrivateKey As String = PrivateKey_XML
            rsa.FromXmlString(XML_PrivateKey)
            Dim key As RSAParameters = rsa.ExportParameters(True)
            signedData = HashAndSignBytes(originalData, key)
            Return Convert.ToBase64String(signedData)
        Catch ex As Exception

        End Try
    End Function
    Public Shared Function HashAndSignBytes(ByVal DataToSign As Byte(), ByVal Key As RSAParameters) As Byte()
        Try
            Dim RSAalg As RSACryptoServiceProvider = New RSACryptoServiceProvider(2048)
            RSAalg.ImportParameters(Key)
            Return RSAalg.SignData(DataToSign, SHA256.Create())
        Catch e As CryptographicException
            Console.WriteLine(e.Message)
            Return Nothing
        End Try
    End Function
    Public Shared Function VerifySignature(ByVal OriginalString As String, ByVal SignedString As String) As Boolean
        Try
            Dim initialProvider As RSACryptoServiceProvider = New RSACryptoServiceProvider(2048)
            Dim privateKey As String = ExportPrivateKey(initialProvider)
            Dim publicKey As String = ExportPublicKey(initialProvider)

            Dim importedProvider As RSACryptoServiceProvider = ImportPublicKey(GCASH_PubKey)
            publicKey = ExportPublicKey(importedProvider)

            Dim pubKey As RSAParameters = importedProvider.ExportParameters(False)

            Dim encoder = New UTF8Encoding()
            Dim bytesToVerify As Byte() = encoder.GetBytes(OriginalString)
            Dim signedBytes As Byte() = Convert.FromBase64String(SignedString)

            Return importedProvider.VerifyData(bytesToVerify, SHA256.Create(), signedBytes)


        Catch ex As Exception

        End Try
    End Function
    Public Shared Function ImportPublicKey(ByVal pem As String) As RSACryptoServiceProvider
        Dim pr As PemReader = New PemReader(New StringReader(pem))
        Dim publicKey As AsymmetricKeyParameter = CType(pr.ReadObject(), AsymmetricKeyParameter)
        Dim rsaParams As RSAParameters = DotNetUtilities.ToRSAParameters(CType(publicKey, RsaKeyParameters))
        Dim csp As RSACryptoServiceProvider = New RSACryptoServiceProvider()
        csp.ImportParameters(rsaParams)
        Return csp
    End Function
    Public Shared Function ExportPublicKey(ByVal csp As RSACryptoServiceProvider) As String
        Dim outputStream As StringWriter = New StringWriter()
        Dim parameters = csp.ExportParameters(False)

        Using stream = New MemoryStream()
            Dim writer = New BinaryWriter(stream)
            writer.Write(CByte(&H30))

            Using innerStream = New MemoryStream()
                Dim innerWriter = New BinaryWriter(innerStream)
                innerWriter.Write(CByte(&H30))
                EncodeLength(innerWriter, 13)
                innerWriter.Write(CByte(&H6))
                Dim rsaEncryptionOid = New Byte() {&H2A, &H86, &H48, &H86, &HF7, &HD, &H1, &H1, &H1}
                EncodeLength(innerWriter, rsaEncryptionOid.Length)
                innerWriter.Write(rsaEncryptionOid)
                innerWriter.Write(CByte(&H5))
                EncodeLength(innerWriter, 0)
                innerWriter.Write(CByte(&H3))

                Using bitStringStream = New MemoryStream()
                    Dim bitStringWriter = New BinaryWriter(bitStringStream)
                    bitStringWriter.Write(CByte(&H0))
                    bitStringWriter.Write(CByte(&H30))

                    Using paramsStream = New MemoryStream()
                        Dim paramsWriter = New BinaryWriter(paramsStream)
                        EncodeIntegerBigEndian(paramsWriter, parameters.Modulus)
                        EncodeIntegerBigEndian(paramsWriter, parameters.Exponent)
                        Dim paramsLength = CInt(paramsStream.Length)
                        EncodeLength(bitStringWriter, paramsLength)
                        bitStringWriter.Write(paramsStream.GetBuffer(), 0, paramsLength)
                    End Using

                    Dim bitStringLength = CInt(bitStringStream.Length)
                    EncodeLength(innerWriter, bitStringLength)
                    innerWriter.Write(bitStringStream.GetBuffer(), 0, bitStringLength)
                End Using

                Dim length = CInt(innerStream.Length)
                EncodeLength(writer, length)
                writer.Write(innerStream.GetBuffer(), 0, length)
            End Using

            Dim base64 = Convert.ToBase64String(stream.GetBuffer(), 0, CInt(stream.Length)).ToCharArray()
            outputStream.Write("-----BEGIN PUBLIC KEY-----" & vbLf)

            For i = 0 To base64.Length - 1 Step 64
                outputStream.Write(base64, i, Math.Min(64, base64.Length - i))
                outputStream.Write(vbLf)
            Next

            outputStream.Write("-----END PUBLIC KEY-----")
        End Using

        Return outputStream.ToString()
    End Function
    Public Shared Function ExportPrivateKey(ByVal csp As RSACryptoServiceProvider) As String
        Dim outputStream As StringWriter = New StringWriter()
        If csp.PublicOnly Then Throw New ArgumentException("CSP does not contain a private key", "csp")
        Dim parameters = csp.ExportParameters(True)

        Using stream = New MemoryStream()
            Dim writer = New BinaryWriter(stream)
            writer.Write(CByte(&H30))

            Using innerStream = New MemoryStream()
                Dim innerWriter = New BinaryWriter(innerStream)
                EncodeIntegerBigEndian(innerWriter, New Byte() {&H0})
                EncodeIntegerBigEndian(innerWriter, parameters.Modulus)
                EncodeIntegerBigEndian(innerWriter, parameters.Exponent)
                EncodeIntegerBigEndian(innerWriter, parameters.D)
                EncodeIntegerBigEndian(innerWriter, parameters.P)
                EncodeIntegerBigEndian(innerWriter, parameters.Q)
                EncodeIntegerBigEndian(innerWriter, parameters.DP)
                EncodeIntegerBigEndian(innerWriter, parameters.DQ)
                EncodeIntegerBigEndian(innerWriter, parameters.InverseQ)
                Dim length = CInt(innerStream.Length)
                EncodeLength(writer, length)
                writer.Write(innerStream.GetBuffer(), 0, length)
            End Using

            Dim base64 = Convert.ToBase64String(stream.GetBuffer(), 0, CInt(stream.Length)).ToCharArray()
            outputStream.Write("-----BEGIN RSA PRIVATE KEY-----" & vbLf)

            For i = 0 To base64.Length - 1 Step 64
                outputStream.Write(base64, i, Math.Min(64, base64.Length - i))
                outputStream.Write(vbLf)
            Next

            outputStream.Write("-----END RSA PRIVATE KEY-----")
        End Using

        Return outputStream.ToString()
    End Function
    Private Shared Sub EncodeIntegerBigEndian(ByVal stream As BinaryWriter, ByVal value As Byte(), Optional ByVal forceUnsigned As Boolean = True)
        stream.Write(CByte(&H2))
        Dim prefixZeros = 0

        For i = 0 To value.Length - 1
            If value(i) <> 0 Then Exit For
            prefixZeros += 1
        Next

        If value.Length - prefixZeros = 0 Then
            EncodeLength(stream, 1)
            stream.Write(CByte(0))
        Else

            If forceUnsigned AndAlso value(prefixZeros) > &H7F Then
                EncodeLength(stream, value.Length - prefixZeros + 1)
                stream.Write(CByte(0))
            Else
                EncodeLength(stream, value.Length - prefixZeros)
            End If

            For i = prefixZeros To value.Length - 1
                stream.Write(value(i))
            Next
        End If
    End Sub
    Private Shared Sub EncodeLength(ByVal stream As BinaryWriter, ByVal length As Integer)
        If length < 0 Then Throw New ArgumentOutOfRangeException("length", "Length must be non-negative")

        If length < &H80 Then
            stream.Write(CByte(length))
        Else
            Dim temp = length
            Dim bytesRequired = 0

            While temp > 0
                temp >>= 8
                bytesRequired += 1
            End While

            stream.Write(CByte((bytesRequired Or &H80)))

            For i = bytesRequired - 1 To 0
                stream.Write(CByte((length >> (8 * i) & &HFF)))
            Next
        End If
    End Sub

End Class
