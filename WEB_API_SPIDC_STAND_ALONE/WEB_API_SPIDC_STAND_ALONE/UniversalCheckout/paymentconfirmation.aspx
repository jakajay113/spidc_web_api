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

         /* Notification Section */
      
           .main-body  { 
                    height: 100vh;
                    display: flex;
                    font-size: 14px;
                    text-align: center;
                    justify-content: center;
                    align-items: center;
                    font-family: 'Khand', sans-serif;   
                }        

                .wrapperCheck {
                    width: 500px;
                    height: 590px;
                    overflow: hidden;
                    border-radius: 12px;
                    border: thin solid #ddd;           
                }

                .topHalfCheck {
                    width: 100%;
                    color: white;
                    overflow: hidden;
                    min-height: 250px;
                    position: relative;
                    padding: 40px 0;
                    background: rgb(0,0,0);
                    background: -webkit-linear-gradient(45deg, #019871, #a0ebcf);
                }

                .topHalfCheck p {
                    margin-bottom: 30px;
                }
                svg {
                    fill: white;
                }
                .topHalfCheck h1 {
                    font-size: 2.25rem;
                    display: block;
                    font-weight: 500;
                    letter-spacing: 0.15rem;
                    text-shadow: 0 2px rgba(128, 128, 128, 0.6);
                }
        
                /* Original Author of Bubbles Animation -- https://codepen.io/Lewitje/pen/BNNJjo */
                .bg-bubbles-Check   .bg-bubbles-Cross{
                    position: absolute;
                    top: 0;
                    left: 0;
                    width: 100%;
                    height: 100%;            
                    z-index: 1;
                }

                .square{
                    position: absolute;
                    list-style: none;
                    display: block;
                    width: 40px;
                    height: 40px;
                    background-color: rgba(255, 255, 255, 0.15);/* fade(green, 75%);*/
                    bottom: -160px;

                    -webkit-animation: square 20s infinite;
                    animation:         square 20s infinite;

                    -webkit-transition-timing-function: linear;
                    transition-timing-function: linear;
                }
                .square:nth-child(1){
                    left: 10%;
                }		
                .square:nth-child(2){
                    left: 20%;

                    width: 80px;
                    height: 80px;

                    animation-delay: 2s;
                    animation-duration: 17s;
                }		
                .square:nth-child(3){
                    left: 25%;
                    animation-delay: 4s;
                }		
                .square:nth-child(4){
                    left: 40%;
                    width: 60px;
                    height: 60px;

                    animation-duration: 22s;

                  background-color: rgba(white, 0.3); /* fade(white, 25%); */
                }		
                .square:nth-child(5){
                    left: 70%;
                }		
                .square:nth-child(6){
                    left: 80%;
                    width: 120px;
                    height: 120px;

                    animation-delay: 3s;
                  background-color: rgba(white, 0.2); /* fade(white, 20%); */
                }		
                .square:nth-child(7){
                    left: 32%;
                    width: 160px;
                    height: 160px;

                    animation-delay: 7s;
                }		
                .square:nth-child(8){
                    left: 55%;
                    width: 20px;
                    height: 20px;

                    animation-delay: 15s;
                    animation-duration: 40s;
                }		
                .square:nth-child(9){
                    left: 25%;
                    width: 10px;
                    height: 10px;

                    animation-delay: 2s;
                    animation-duration: 40s;
                    background-color: rgba(white, 0.3); /*fade(white, 30%);*/
                }		
                .square:nth-child(10){
                    left: 90%;
                    width: 160px;
                    height: 160px;
                    animation-delay: 11s;
                }

                @-webkit-keyframes square {
                    0%   { transform: translateY(0); }
                    100% { transform: translateY(-500px) rotate(600deg); }
                }
                @keyframes square {
                    0%   { transform: translateY(0); }
                    100% { transform: translateY(-500px) rotate(600deg); }
                }

                .bottomHalfCheck {
                    align-items: center;
                    padding: 22px;
                }
                .bottomHalfCheck p {
                    font-weight: 500;
                    font-size: 1.05rem;
                    margin-bottom: 20px;
                }

                .btnCheck {
                    border: none;
                    color: white;
                    cursor: pointer;
                    border-radius: 12px;            
                    padding: 10px 18px;            
                    background-color: #019871;
                    text-shadow: 0 1px rgba(128, 128, 128, 0.75);
                    margin-top: 12px;
                }
                .btnCheck:hover {
                    background-color: #85ddbf;
                }

                .wrapperAlertCross {
                    width: 500px;
                    height: 400px;
                    overflow: hidden;
                    border-radius: 12px;
                    border: thin solid #ddd;           
                }

                .topHalfCross {
                    width: 100%;
                    color: white;
                    overflow: hidden;
                    min-height: 250px;
                    position: relative;
                    padding: 40px 0;
                    background: rgb(0,0,0);
                    background: -webkit-linear-gradient(#ff0000, #ff8080);
                }

                .topHalfCross p {
                    margin-bottom: 30px;
                }
                .topHalfCross h1 {
                    font-size: 2.25rem;
                    display: block;
                    font-weight: 500;
                    letter-spacing: 0.15rem;
                    text-shadow: 0 2px rgba(128, 128, 128, 0.6);
                }
        
                .bottomHalfCross {
                    align-items: center;
                    padding: 35px;
                }
                .bottomHalfCross p {
                    font-weight: 500;
                    font-size: 1.05rem;
                    margin-bottom: 10px;
                }

                .btnCross {
                    border: none;
                    color: white;
                    cursor: pointer;
                    border-radius: 12px;            
                    padding: 10px 18px;            
                    background-color: #ff4d4d;
                    text-shadow: 0 1px rgba(128, 128, 128, 0.75);
                }
                .btnCross:hover {
                    background-color: #ffb3b3;
                }


                .btn-sucess {
                    border: none;
                    color: white;
                    cursor: pointer;
                    border-radius: 12px;            
                    padding: 10px 18px;            
                    background-color: #019871;
                    text-shadow: 0 1px rgba(128, 128, 128, 0.75);
                }
                .btn-sucess:hover { 
                    background-color: #85ddbf;
                }

                .btn-failed {
                    border: none;
                    color: white;
                    cursor: pointer;
                    border-radius: 12px;            
                    padding: 10px 18px;            
                    background-color: rgb(236, 49, 74);
                    text-shadow: 0 1px rgba(128, 128, 128, 0.75);
                }
                .btn-failed:hover {
                    background-color: #dd8585;
                }


                /* list Item */

                .transaction li{
                    font-family: sans-serif;
                    font-size: 18px;
                    list-style: none;
                    text-align: left;
                    margin-bottom: 6px;
                }

                /* Button */

                .bottomHalf {
                    align-items: center;
                    padding: 35px;
                }
                .bottomHalf p {
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    height: 100vh; /* Adjust the height as needed */
                }


    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div>

       <div class="main-body">
            <div class="wrapper">

                <!-- Success -->
                <div class="wrapperCheck">
                    <div class="wrapperCheck">
                        <div class="contentAlert">
                            <div class="topHalfCheck">
                                <p>
                                    <svg viewBox="0 0 512 512" width="100" title="check-circle">
                                    <path d="M504 256c0 136.967-111.033 248-248 248S8 392.967 8 256 119.033 8 256 8s248 111.033 248 248zM227.314 387.314l184-184c6.248-6.248 6.248-16.379 0-22.627l-22.627-22.627c-6.248-6.249-16.379-6.249-22.628 0L216 308.118l-70.059-70.059c-6.248-6.248-16.379-6.248-22.628 0l-22.627 22.627c-6.248 6.248-6.248 16.379 0 22.627l104 104c6.249 6.249 16.379 6.249 22.628.001z" />
                                    </svg>
                                </p>
                                <h1 id="notificationHeaderSuccess">""</h1>
                                <ul class="bg-bubbles-Check">
                                    <li class="square"></li>
                                    <li class="square"></li>
                                    <li class="square"></li>
                                    <li class="square"></li>
                                    <li class="square"></li>
                                    <li class="square"></li>
                                    <li class="square"></li>
                                    <li class="square"></li>
                                    <li class="square"></li>
                                    <li class="square"></li>
                                </ul>
                            </div>
                            <div class="bottomHalfCheck">
                                <ul class="transaction">
                                    <li class="list-items">Transaction Type: <span id="_transactionType"></span> </li>
                                    <li class="list-items">Control No: <span id="_controlNo"></span> </li>
                                    <li class="list-items">Email: <span id="_email"></span> </li>
                                    <li class="list-items">Billing: <span id="_billing"></span> </li>
                                    <li class="list-items">Total Amount: <span id="_totalAmount"></span> </li>
                                    <li class="list-items">Billing Date: <span id="_billingDate"></span> </li>
                                </ul>
                                <button type="button" class="btnCheck"  id="_btnSuccessBackToHome">Back to home</button>
                            </div>
                        </div>        
                    </div>
                </div>

                <!-- Failed -->
                <div class="wrapperAlertCross">
                    <div class="wrapperAlertCross"> 
                        <div class="contentAlert">
                            <div class="topHalfCross">
                            <p>
                                <svg viewBox="0 0 512 512" width="100" title="x-circle">
                                    <path d="M302.35 256l103.77-103.77c6.24-6.24 6.24-16.38 0-22.62l-22.62-22.62c-6.24-6.24-16.38-6.24-22.62 0L256 210.35 152.23 106.58c-6.24-6.24-16.38-6.24-22.62 0l-22.62 22.62c-6.24 6.24-6.24 16.38 0 22.62L209.65 256 105.88 359.77c-6.24 6.24-6.24 16.38 0 22.62l22.62 22.62c6.24 6.24 16.38 6.24 22.62 0L256 301.65l103.77 103.77c6.24 6.24 16.38 6.24 22.62 0l22.62-22.62c6.24-6.24 6.24-16.38 0-22.62L302.35 256z"/>
                                </svg>
                            </p>
                            <h1 id="notificationHeaderFailed">""</h1>
                            <ul class="bg-bubbles-Cross">
                                <li class="square"></li>
                                <li class="square"></li>
                                <li class="square"></li>
                                <li class="square"></li>
                                <li class="square"></li>
                                <li class="square"></li>
                                <li class="square"></li>
                                <li class="square"></li>
                                <li class="square"></li>
                                <li class="square"></li>
                            </ul>
                            </div>
                            <div class="bottomHalfCross">
                                <p id="_payment-failed-decription">""</p>
                                <button type="button" class="btnCross" id="_btnFailedBackToHome">Back to home</button>
                            </div>
                        </div>         
                    </div>
                </div>

                <!-- Buttons -->
           <%--<div class="bottomHalf">
                    <button type="button" class="btn-sucess" id="successButton">Success</button> 
                    <button type="button" class="btn-failed" id="failedButton">Failed</button> 
                </div>--%>

            </div>  
            </div>   




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
       <rsweb:ReportViewer ID="Report_EOR" runat="server" AsynRendering="true" SizeToReportContent = "true" KeepSessionAlive="false"></rsweb:ReportViewer>
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
        document.getElementsByClassName('wrapperCheck')[0].style.display = 'none'
        document.getElementsByClassName('wrapperAlertCross')[0].style.display = 'none'
        var url = window.location.href;
        //localStorage.getItem('payloadToProcess')
        //var payload = '{"payload":{"dataInformation":[{"TransactionRef":"UCP2024010850032","TrefNo":"t1233454","AppName":"PINNACLE","Email":"mataverdekenneth@gmail.com","Fname":"Yest","MiddleName":"Hina","LastName":"Nagi","Suffix":null,"AccountNo":"2023012319-S","BillingAmount":"1.00","TotalAmount":"1.00","BiilingDate":"1900-01-01T00:00:00","OtherFee":"0.00","RawAmount":"1.00","SpidcFee":"0.00","Token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJtYXRhdmVyZGVrZW5uZXRoQGdtYWlsLmNvbSIsImFwcE5hbWUiOiJQSU5OQUNMRSIsImFzc2Vzc21lbnRObyI6IjIwMjMtMTItMTIxMjg5IiwiaWF0IjoiMS84LzIwMjQgMTozNzowOSBQTSIsIm5iZiI6MTcwNDcyMTAyOSwiZXhwIjoxNzA0NzIxNjI5LCJpc3MiOiJcIlNQSURDIFVOSVZFUlNBTCBDSEVDS09VVFwiIiwiYXVkIjoiaHR0cHM6Ly9nb29nbGUuY29tIn0.wH8yY-amEJJQ67IMwpcMGDviS6QvATl8gzNtgn5Q9Sc","CheckOutStatus":"Pending","UrlOrigin":"https://google.com","UrlSuccess":"https://google.com","CheckOutDate":"2024-01-08T21:37:09.69","Address":"Blk 8 Lotus St. Cainta Rizal","AssessmentNo":"2023-11-88888","transDesc":"Tuition Fee"}],"dataCode":[{"accountNo":"2023012319-S","SysTran_ProviderCode":"4-02-02-010-01","systrans_codeDesc":"Medical Fee","systems_codeAmt":"1.00","SysTran_MainCode":"4-02-02-010-01","SysTran_AncestorCode":"4-02-02-010-01","SysTran_SubAccCode":"4-02-02-010-01-001","assessmentNo":"2023-11-88888"}],"paymentGateway":"GCASH"},"urlProcess":"processing.aspx"}';
        //console.log(payload);
        var payload = localStorage.getItem('payloadToProcess');
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
                //console.log(reponse.d);
                if (reponse.d[0] == "success") {
                    document.getElementsByClassName('wrapperCheck')[0].style.display = 'block'
                    document.getElementById('notificationHeaderSuccess').textContent = "Payment successfuly"
                    document.getElementById('_transactionType').textContent = reponse.d[1];
                    document.getElementById('_controlNo').textContent = reponse.d[2];
                    document.getElementById('_email').textContent = reponse.d[3];
                    document.getElementById('_billing').textContent = reponse.d[4];
                    document.getElementById('_totalAmount').textContent = reponse.d[5];
                    document.getElementById('_billingDate').textContent = reponse.d[6];
                    document.getElementById('_btnSuccessBackToHome').addEventListener('click', function () {
                        window.location.href = reponse.d[7];
                    });
                } else {
                    document.getElementsByClassName('wrapperAlertCross')[0].style.display = 'block'
                    document.getElementById('notificationHeaderFailed').textContent = "Payment Failed"
                    document.getElementById('_payment-failed-decription').textContent = "Something went wrong try again later!"
                    document.getElementById('_btnFailedBackToHome').addEventListener('click', function () {
                        window.location.href = reponse.d[7];
                    });
                }


            },
            error: function (xhr, status, errorThrown) {
                console.log(xhr.responseText);
                console.log(status);
                console.log(errorThrown);
            }
        });

    </script>
</body>
</html>
