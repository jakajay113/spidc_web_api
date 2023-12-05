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

      <%--  var testdata = '{ "payload": { "dataInformation": [{ "TransactionRef": "UCP2023112954170", "TrefNo": null, "AppName": "CEDULAAPP", "Email": "spidcenduser@yopmail.com", "Fname": "jay", "MiddleName": "jay", "LastName": "jay", "Suffix": null, "AccountNo": "2023-11-00002", "BillingAmount": "13.42", "TotalAmount": "13.42", "BiilingDate": "2023-11-29T20:33:25.61", "OtherFee": "0.00", "RawAmount": "13.42", "SpidcFee": "0.00", "Token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzcGlkY2VuZHVzZXJAeW9wbWFpbC5jb20iLCJhcHBOYW1lIjoiQ0VEVUxBQVBQIiwiYWNjb3VudE5vIjoiMjAyMy0xMS0wMDAwMiIsImlhdCI6IjExLzI5LzIwMjMgMTI6MzM6MjUgUE0iLCJuYmYiOjE3MDEyNjEyMDUsImV4cCI6MTcwMTI2MTgwNSwiaXNzIjoiXCJTUElEQyBVTklWRVJTQUwgQ0hFQ0tPVVRcIiIsImF1ZCI6Imh0dHBzOi8vb25saW5lLnNwaWRjLmNvbS5waC9jYWxvb2Nhbi9XZWJQb3J0YWwvQ2VkdWxhQXBwL1RheHBheWVyL2luZGV4Lmh0bWwifQ.oF1eEztnHAE6OFLwkQl82R2eOZDJinHhM5RwdCC3Dyo", "CheckOutStatus": "Pending", "UrlOrigin": "https://online.spidc.com.ph/caloocan/WebPortal/CedulaApp/Taxpayer/index.html", "UrlSuccess": "https://online.spidc.com.ph/caloocan/WebPortal/CedulaApp/Taxpayer/index.html", "CheckOutDate": "2023-11-29T20:33:25.61", "Address": "jay", "AssessmentNo": "2023-11-00002", "transDesc": "Individual Cedula" }], "dataCode": [{ "accountNo": "2023-11-00002", "SysTran_ProviderCode": "SF-002", "systrans_codeDesc": "diploma fee", "systems_codeAmt": "500.00", "SysTran_MainCode": "4-02-02-010-01", "SysTran_AncestorCode": "4-02-02-010-01", "SysTran_SubAccCode": "4-02-02-010-01-002" }, { "accountNo": "2023-11-00002", "SysTran_ProviderCode": "SF-002", "systrans_codeDesc": "diploma fee", "systems_codeAmt": "500.00", "SysTran_MainCode": "4-02-02-010-01", "SysTran_AncestorCode": "4-02-02-010-01", "SysTran_SubAccCode": "4-02-02-010-01-002" }], "paymentGateway": "PAYMAYA" }, "urlProcess": "processing.aspx" }';
        document.getElementById('<%=_payloadToProcess.ClientID%>').value = testdata;
        setTimeout(function () {
        __doPostBack('Processing', "PAYMAYA")
        }, 1000);--%>


       function preventBack() {
            window.history.forward();
        }
        setTimeout("preventBack()", 0);
        window.onunload = function () {
            null;
        };
        const payloadString = localStorage.getItem('payloadToProcess');
        const payloadObj = JSON.parse(payloadString)
        document.getElementById('<%=_payloadToProcess.ClientID%>').value = payloadString;
        setTimeout(function () {
        __doPostBack('Processing', payloadObj.payload.paymentGateway)
        }, 1000);
    </script>
</html>
