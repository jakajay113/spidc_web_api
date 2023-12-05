Imports WEB_API_SPIDC_STAND_ALONE.GCashModel
Imports Org.BouncyCastle.Asn1.Cms



Public Class PayMayaModel
    Public Property totalAmount As TotalAmount
    Public Property buyer As Buyer
    Public Property items As Item
    Public Property redirectUrl As RedirectUrl
    Public Property requestReferenceNumber As String
    Public Property metadata As Metadata
    Public Property reason As String


    Private Shared API_Type As String
    Private Shared ACCTNO As String
    Private Shared PayorEmail As String
    Private Shared jsonPost As String
    Private Shared jsonResponse As String
    Private Shared SPIDCRefNo As String


    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config
    Public Shared PayMayaDomain As String
    Public Shared PayMayaCheckout As String
    Public Shared MerchantCode As String
    Public Shared TestURL As String
    Public Shared TestURL_Return As String
    Public Shared ProdURL As String
    Public Shared ProdURL_Return As String
    Public Shared Username As String
    Public Shared Password As String
    Public Shared PrivateKey As String
    Public Shared SecretKey As String
    Public Shared PKPASS As String


    Public Shared Sub PayMayaConfig()
        'Call The Web Config
        Spidc_Web_API_Config.WebApiConfig()
        PayMayaDomain = Spidc_Web_API_Config._mAppPAYMAYA_DOMAIN
        PayMayaCheckout = Spidc_Web_API_Config._mAppPAYMAYA_CHECKOUT
        MerchantCode = Spidc_Web_API_Config._mAppPAYMAYA_MERCHANT_CODE
        TestURL = Spidc_Web_API_Config._mAppPAYMAYA_TEST_URL
        TestURL_Return = Spidc_Web_API_Config._mAppPAYMAYA_TEST_URL_RETURN
        ProdURL = Spidc_Web_API_Config._mAppPAYMAYA_PROD_URL
        ProdURL_Return = Spidc_Web_API_Config._mAppPAYMAYA_PROD_URL_RETURN
        Username = Spidc_Web_API_Config._mAppPAYMAYA_USERNAME
        Password = Spidc_Web_API_Config._mAppPAYMAYA_PASSWORD
        PrivateKey = Spidc_Web_API_Config._mAppPAYMAYA_PRIVATEKEY
        SecretKey = Spidc_Web_API_Config._mAppPAYMAYA_SECRETKEY
        PKPASS = ""
    End Sub



    Public Shared Sub insert_PaymayaTransactions(ByVal API_Type As String, ByVal ACCTNO As String, ByVal PayorEmail As String, ByVal jsonPost As String, ByVal jsonResponse As String, ByVal SPIDCRefNo As String)
        Dim _nClass3 As New cDalTransactionHistory
        _nClass3._pSqlConnection = Spidc_Web_API_Global_Connection._pSqlCxn_OAIMS
        _nClass3._pSubInsertPayMaya_Transactions(API_Type, ACCTNO, PayorEmail, jsonPost, jsonResponse, SPIDCRefNo)
    End Sub



    Public Shared Function Base64Encode(ByVal plainText As String) As String
        Dim plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText)
        Return System.Convert.ToBase64String(plainTextBytes)
    End Function

End Class
Public Class Details
    Public Property discount As Double
    Public Property serviceCharge As Double
    Public Property shippingFee As Double
    Public Property tax As Double
    Public Property subtotal As Double
End Class

Public Class TotalAmount
    Public Property value As Double
    Public Property currency As String
    Public Property details As Details
End Class

Public Class Contact
    Public Property phone As String
    Public Property email As String
End Class

Public Class ShippingAddress
    Public Property firstName As String
    Public Property middleName As String
    Public Property lastName As String
    Public Property phone As String
    Public Property email As String
    Public Property line1 As String
    Public Property line2 As String
    Public Property city As String
    Public Property state As String
    Public Property zipCode As String
    Public Property countryCode As String
    Public Property shippingType As String
End Class

Public Class BillingAddress
    Public Property line1 As String
    Public Property line2 As String
    Public Property city As String
    Public Property state As String
    Public Property zipCode As String
    Public Property countryCode As String
End Class

Public Class Buyer
    Public Property firstName As String
    Public Property middleName As String
    Public Property lastName As String
    Public Property birthday As String
    Public Property customerSince As String
    Public Property sex As String
    Public Property contact As Contact
    Public Property shippingAddress As ShippingAddress
    Public Property billingAddress As BillingAddress
End Class

Public Class Amount
    Public Property value As Double
    Public Property details As Details
End Class

Public Class ItemTotalAmount
    Public Property value As Double
    Public Property details As Details
End Class

Public Class Item
    Public Property name As String
    Public Property quantity As Integer
    Public Property code As String
    Public Property description As String
    Public Property amount As Amount
    Public Property totalAmount As ItemTotalAmount
End Class
Public Class RedirectUrl
    Public Property success As String
    Public Property failure As String
    Public Property cancel As String
End Class

Public Class Metadata
End Class




