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
        const payloadString = localStorage.getItem('payloadToProcess');
        const payloadObj = JSON.parse(payloadString)
        document.getElementById('<%= _payloadToProcess.ClientID%>').value = payloadString;
        setTimeout(function () {
        __doPostBack('Processing', payloadObj.payload.paymentGateway)
        }, 1000);
    </script>
</html>
