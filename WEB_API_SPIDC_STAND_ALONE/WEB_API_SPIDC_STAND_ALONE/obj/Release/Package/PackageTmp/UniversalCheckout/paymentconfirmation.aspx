<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="paymentconfirmation.aspx.vb" Inherits="WEB_API_SPIDC_STAND_ALONE.paymentconfirmation" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="icon" type="image/x-icon" href="https://cdn-icons-png.flaticon.com/512/726/726559.png"/>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
    <link rel="preconnect" href="https://fonts.gstatic.com"/>
    <link href="https://fonts.googleapis.com/css2?family=Lato:wght@400;700&amp;display=swap" rel="stylesheet"/>
    <link href="https://cdn.jsdelivr.net/npm/@splidejs/splide@4.1.4/dist/css/splide.min.css" rel="stylesheet"/>
    <title>Payment Confirmation</title>
    <style>
        /*Font Style*/
        body {
            font-family: 'Lato', sans-serif;
        }
        /*Payment Method Radio*/
        .paymenthod-method-radio:checked {
            background-color: #212529 !important;
            border-color: #212529 !important;
        }

        .payment-method, .payment-method-icon, .checkout-back {
            cursor: pointer;
        }


        .outline-circle {
            width: 50px; /* Adjust the width and height as needed */
            height: 50px;
            border-radius: 50%; /* Make it circular */
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .loader-container {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        .loader {
            width: 70px;
            height: 70px;
            position: relative;
        }

            .loader:before {
                content: "";
                width: 70px;
                height: 70px;
                border-radius: 50%;
                border: 6px solid #212529;
                position: absolute;
                top: 0;
                left: 0;
                animation: pulse 1s ease-in-out infinite;
            }

            .loader:after {
                content: "";
                width: 70px;
                height: 70px;
                border-radius: 50%;
                border: 6px solid transparent;
                border-top-color: #212529;
                position: absolute;
                top: 0;
                left: 0;
                animation: spin 2s linear infinite;
            }

        .loader-text {
            font-size: 20px;
            margin-top: 20px;
            color: #212529;
            font-family: Arial, sans-serif;
            text-align: center;
            text-transform: uppercase;
        }

        @keyframes pulse {
            0% {
                transform: scale(0.6);
                opacity: 1;
            }

            50% {
                transform: scale(1.2);
                opacity: 0;
            }

            100% {
                transform: scale(0.6);
                opacity: 1;
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        .content {
            display: none;
        }

        .loaded .loader-container {
            display: none;
        }

        .loaded .content {
            display: block;
        }


        /* Mobile Devices (Portrait Orientation) */
        @media (max-width: 767px) and (min-width: 350px) {
            /* Your CSS rules for mobile devices go here */
            .text-success {
                font-size: 11px;
            }

            .text-dark {
                font-size: 20px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div>
         <!--Loader-->
            <div class="modal bg-light" id="apploader" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" style="display: none;" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-body">
                        <div class="container row d-flex justify-content-center align-items-center">
                            <!-- <span class="loader text-dark fs-2"></span> -->
                            <div class="loader-container">
                                <div class="loader"></div>
                                <div class="loader-text">Please wait while processing...</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--End Loader-->

     <%--<input type="hidden" id="_payload" runat="server"/>--%>
        <%--<rsweb:ReportViewer ID="Report_EOR" runat="server" AsynRendering="true" SizeToReportContent = "true" KeepSessionAlive="false"></rsweb:ReportViewer>--%>
    </div>
    </form>

     <script src="https://code.jquery.com/jquery-3.7.1.min.js" integrity="sha256-/JqT3SQfawRcv/BIHPThkBvs0OEvtFFmqPF/lYI/Cxo=" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <script>

        function preventBack() {
            window.history.forward();
        }
        setTimeout("preventBack()", 0);
        window.onunload = function () {
            null;
        };
        var loader = new bootstrap.Modal(document.getElementById('apploader'));
        //loader.show();
        //console.log(localStorage.getItem('payloadToProcess'));
        //const payloadString = localStorage.getItem('payloadToProcess');
        //const payloadObj = JSON.parse(payloadString)
        <%--document.getElementById('<%=_payload.ClientID%>').value = payloadString;--%>
        //console.log(payloadObj.payload.paymentGateway);
        //setTimeout(function () {
        //    __doPostBack('PaymentConfirmation')
        //}, 1000);
        var url = window.location.href;
        var payload = '{"payload":{"dataInformation":[{"TransactionRef":"UCP2023121158784","TrefNo":null,"AppName":"CEDULAAPP","Email":"spidcenduser@yopmail.com","Fname":"jay","MiddleName":"jay","LastName":"jay","Suffix":null,"AccountNo":"2023-11-00002","BillingAmount":"13.42","TotalAmount":"13.42","BiilingDate":"2023-12-11T11:48:33.907","OtherFee":"0.00","RawAmount":"13.42","SpidcFee":"0.00","Token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzcGlkY2VuZHVzZXJAeW9wbWFpbC5jb20iLCJhcHBOYW1lIjoiQ0VEVUxBQVBQIiwiYWNjb3VudE5vIjoiMjAyMy0xMS0wMDAwMiIsImlhdCI6IjEyLzExLzIwMjMgMzo0ODozMyBBTSIsIm5iZiI6MTcwMjI2NjUxMywiZXhwIjoxNzAyMjY3MTEzLCJpc3MiOiJcIlNQSURDIFVOSVZFUlNBTCBDSEVDS09VVFwiIiwiYXVkIjoiaHR0cHM6Ly9vbmxpbmUuc3BpZGMuY29tLnBoL2NhbG9vY2FuL1dlYlBvcnRhbC9DZWR1bGFBcHAvVGF4cGF5ZXIvaW5kZXguaHRtbCJ9.XZiOl9WqJdvxk9IMycEX0QdCGZ6lRU46-TuFXyPwCZk","CheckOutStatus":"Pending","UrlOrigin":"https://online.spidc.com.ph/caloocan/WebPortal/CedulaApp/Taxpayer/index.html","UrlSuccess":"https://online.spidc.com.ph/caloocan/WebPortal/CedulaApp/Taxpayer/index.html","CheckOutDate":"2023-12-11T11:48:33.907","Address":"jay","AssessmentNo":"2023-11-00002","transDesc":"Individual Cedula"}],"dataCode":[{"accountNo":"2023-11-00002","SysTran_ProviderCode":"SF-002","systrans_codeDesc":"diploma fee","systems_codeAmt":"500.00","SysTran_MainCode":"4-02-02-010-01","SysTran_AncestorCode":"4-02-02-010-01","SysTran_SubAccCode":"4-02-02-010-01-002"},{"accountNo":"2023-11-00002","SysTran_ProviderCode":"SF-002","systrans_codeDesc":"diploma fee","systems_codeAmt":"500.00","SysTran_MainCode":"4-02-02-010-01","SysTran_AncestorCode":"4-02-02-010-01","SysTran_SubAccCode":"4-02-02-010-01-002"},{"accountNo":"WEB-CTC-2023-11-00010","SysTran_ProviderCode":"","systrans_codeDesc":"","systems_codeAmt":"110.00","SysTran_MainCode":"4-01-01-050-51","SysTran_AncestorCode":"4-01-01-050-51","SysTran_SubAccCode":"4-01-01-050-51-001"},{"accountNo":"WEB-CTC-2023-11-00010","SysTran_ProviderCode":"","systrans_codeDesc":"","systems_codeAmt":"24.20","SysTran_MainCode":"4-01-05-010-01","SysTran_AncestorCode":"4-01-05-010-01","SysTran_SubAccCode":"4-01-05-010-01-001"}],"paymentGateway":"GCASH"},"urlProcess":"processing.aspx"}'; 
        $.ajax({
            url: "paymentconfirmation.aspx/PaymentConfirmation",
            type: "post",
            data: JSON.stringify({ url: url, payload: payload }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function (xhr) {
                loader.show();
            },
            success: function (reponse) {
                loader.hide();
                console.log(reponse.d);
            },
            error: function (xhr, status, errorThrown) {
                console.log(xhr.responseText);
            }
        });

    </script>
</body>
</html>
