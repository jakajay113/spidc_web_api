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

    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config
    Public Shared GcashDomain As String
    Public Shared GcashFunctionOrderCreate As String
    Public Shared GcashFunctionOrderQuery As String
    Public Shared GcashFunctionOrderCancel As String
    Public Shared GcashFunctionOrderRefund As String
    Public Shared MerchantName As String
    Public Shared PublicKey_XML As String
    Public Shared PrivateKey_XML As String
    Public Shared PublicKey_PEM As String
    Public Shared PrivateKey_PEM As String
    Public Shared ClientId As String
    Public Shared ClientSecret As String
    Public Shared MerchantID As String
    Public Shared ProductCode As String
    Public Shared GCASH_PublicKey_PEM As String
    Public Shared ReqMsgID As String
    Public Shared GCASH_PubKey As String

    'Public Shared GcashDomain As String = Spidc_Web_API_Config._mAppGCASH_DOMAIN
    'Public Shared GcashFunctionOrderCreate As String = Spidc_Web_API_Config._mAppGCASH_FUNCTION_CREATE_ORDER
    'Public Shared GcashFunctionOrderQuery As String = Spidc_Web_API_Config._mAppGCASH_FUNCTION_QUERY_ORDER
    'Public Shared GcashFunctionOrderCancel As String = Spidc_Web_API_Config._mAppGCASH_FUNCTION_CANCEL_ORDER
    'Public Shared GcashFunctionOrderRefund As String = Spidc_Web_API_Config._mAppGCASH_FUNCTION_REFUND_ORDER
    'Public Shared MerchantName As String = Spidc_Web_API_Config._mAppGCASH_MERCHANT_NAME
    'Public Shared PublicKey_XML As String = Spidc_Web_API_Config._mAppGCASH_PUBLIC_KEY_XML
    'Public Shared PrivateKey_XML As String = Spidc_Web_API_Config._mAppGCASH_PRIVATE_KEY_XML
    'Public Shared PublicKey_PEM As String = Spidc_Web_API_Config._mAppGCASH_PUBLIC_KEY_PEM
    'Public Shared PrivateKey_PEM As String = Spidc_Web_API_Config._mAppGCASH_PRIVATE_KEY_PEM
    'Public Shared ClientId As String = Spidc_Web_API_Config._mAppGCASH_CLIENT_ID
    'Public Shared ClientSecret As String = Spidc_Web_API_Config._mAppGCASH_CLIENT_SECRET
    'Public Shared MerchantID As String = Spidc_Web_API_Config._mAppGCASH_MERCHANT_ID
    'Public Shared ProductCode As String = Spidc_Web_API_Config._mAppGCASH_PRODUCT_CODE
    'Public Shared GCASH_PublicKey_PEM As String
    'Public Shared ReqMsgID As String
    'Public Shared GCASH_PubKey As String = Spidc_Web_API_Config._mAppGCASH_PUBLIC_KEY


    Public Shared Sub GCashConfig()
        'Call The Web Config
        Spidc_Web_API_Config.WebApiConfig()

        GcashDomain = Spidc_Web_API_Config._mAppGCASH_DOMAIN
        GcashFunctionOrderCreate = Spidc_Web_API_Config._mAppGCASH_FUNCTION_CREATE_ORDER
        GcashFunctionOrderQuery = Spidc_Web_API_Config._mAppGCASH_FUNCTION_QUERY_ORDER
        GcashFunctionOrderCancel = Spidc_Web_API_Config._mAppGCASH_FUNCTION_CANCEL_ORDER
        GcashFunctionOrderRefund = Spidc_Web_API_Config._mAppGCASH_FUNCTION_REFUND_ORDER

        MerchantName = Spidc_Web_API_Config._mAppGCASH_MERCHANT_NAME
        PublicKey_XML = Spidc_Web_API_Config._mAppGCASH_PUBLIC_KEY_XML
        PrivateKey_XML = Spidc_Web_API_Config._mAppGCASH_PRIVATE_KEY_XML
        PublicKey_PEM = Spidc_Web_API_Config._mAppGCASH_PUBLIC_KEY_PEM
        PrivateKey_PEM = Spidc_Web_API_Config._mAppGCASH_PRIVATE_KEY_PEM
        ClientId = Spidc_Web_API_Config._mAppGCASH_CLIENT_ID
        ClientSecret = Spidc_Web_API_Config._mAppGCASH_CLIENT_SECRET
        MerchantID = Spidc_Web_API_Config._mAppGCASH_MERCHANT_ID
        ProductCode = Spidc_Web_API_Config._mAppGCASH_PRODUCT_CODE
        GCASH_PubKey = Spidc_Web_API_Config._mAppGCASH_PUBLIC_KEY
    End Sub





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
            Return False
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
        'Try
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
        'Catch ex As Exception
        'End Try
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
