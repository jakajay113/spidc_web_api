<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="processing.aspx.vb" Inherits="WEB_API_SPIDC_STAND_ALONE.processing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div>
      <input type="hidden" id="_payloadToProcess" runat="server"/>
    </div>
    </form>
</body>
    <script>
        //value='{"payload":{"dataInformation":[{"TransactionRef":"UCP2023113080916","TrefNo":null,"AppName":"CEDULAAPP","Email":"spidcenduser@yopmail.com","Fname":"jay","MiddleName":"jay","LastName":"jay","Suffix":null,"AccountNo":"2023-11-00002","BillingAmount":"13.42","TotalAmount":"13.42","BiilingDate":"2023-11-30T12:57:55.307","OtherFee":"0.00","RawAmount":"13.42","SpidcFee":"0.00","Token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzcGlkY2VuZHVzZXJAeW9wbWFpbC5jb20iLCJhcHBOYW1lIjoiQ0VEVUxBQVBQIiwiYWNjb3VudE5vIjoiMjAyMy0xMS0wMDAwMiIsImlhdCI6IjExLzMwLzIwMjMgNDo1Nzo1NSBBTSIsIm5iZiI6MTcwMTMyMDI3NSwiZXhwIjoxNzAxMzIwODc1LCJpc3MiOiJcIlNQSURDIFVOSVZFUlNBTCBDSEVDS09VVFwiIiwiYXVkIjoiaHR0cHM6Ly9vbmxpbmUuc3BpZGMuY29tLnBoL2NhbG9vY2FuL1dlYlBvcnRhbC9DZWR1bGFBcHAvVGF4cGF5ZXIvaW5kZXguaHRtbCJ9.iGZc8aZUsT8fk22m0vxBQXb-CRjjM44DeOS1zISzKWk","CheckOutStatus":"Pending","UrlOrigin":"https://online.spidc.com.ph/caloocan/WebPortal/CedulaApp/Taxpayer/index.html","UrlSuccess":"https://online.spidc.com.ph/caloocan/WebPortal/CedulaApp/Taxpayer/index.html","CheckOutDate":"2023-11-30T12:57:55.307","Address":"jay","AssessmentNo":"2023-11-00002","transDesc":"Individual Cedula"}],"dataCode":[{"accountNo":"2023-11-00002","SysTran_ProviderCode":"SF-002","systrans_codeDesc":"diploma fee","systems_codeAmt":"500.00","SysTran_MainCode":"4-02-02-010-01","SysTran_AncestorCode":"4-02-02-010-01","SysTran_SubAccCode":"4-02-02-010-01-002"},{"accountNo":"2023-11-00002","SysTran_ProviderCode":"SF-002","systrans_codeDesc":"diploma fee","systems_codeAmt":"500.00","SysTran_MainCode":"4-02-02-010-01","SysTran_AncestorCode":"4-02-02-010-01","SysTran_SubAccCode":"4-02-02-010-01-002"}],"paymentGateway":"GCASH"},"urlProcess":"processing.aspx"}'
        //localStorage.getItem('payloadToProcess')
        document.getElementById('<%= _payloadToProcess.ClientID%>').value = "data test 123";
        //setTimeout(function () {
        //__doPostBack('Processing', '')
        //}, 1000);

    </script>
</html>
