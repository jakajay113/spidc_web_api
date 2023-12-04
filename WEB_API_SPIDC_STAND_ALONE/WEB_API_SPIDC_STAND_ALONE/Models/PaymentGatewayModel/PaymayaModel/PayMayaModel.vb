Imports WEB_API_SPIDC_STAND_ALONE.GCashModel
Imports Org.BouncyCastle.Asn1.Cms





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

Public Class PayMayaModel
    Public Property totalAmount As TotalAmount
    Public Property buyer As Buyer
    Public Property items As Item
    Public Property redirectUrl As RedirectUrl
    Public Property requestReferenceNumber As String
    Public Property metadata As MetaData
    Public Property reason As String

End Class
