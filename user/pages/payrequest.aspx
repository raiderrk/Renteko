﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="payrequest.aspx.cs" Inherits="payrequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="btnpay" runat="server" Text="Pay Now" />
            <button id="rzp-button1">Pay with Razorpay</button>
        <script src="https://checkout.razorpay.com/v1/checkout.js"></script>
        <script>
            var orderId = "<%=orderId%>"
            var options = {
                "name": "DJ Tiesto",
                "description": "Tron Legacy",
                "order_id": orderId,
                "image": "https://s29.postimg.org/r6dj1g85z/daft_punk.jpg",
                "prefill": {
                    "name": "Daft Punk",
                    "email": "customer@merchant.com",
                    "contact": "+919999999999",
                },
                "notes": {
                    "address": "Hello World",
                    "merchant_order_id": "12312321",
                },
                "theme": {
                    "color": "#F37254"
                }
            }
            // Boolean whether to show image inside a white frame. (default: true)
            options.theme.image_padding = false;
            options.handler = function (response) {
                document.getElementById('razorpay_payment_id').value = response.razorpay_payment_id;
                document.getElementById('razorpay_order_id').value = orderId;
                document.getElementById('razorpay_signature').value = response.razorpay_signature;
                document.razorpayForm.submit();
            };
            options.modal = {
                ondismiss: function () {
                    console.log("This code runs when the popup is closed");
                },
                // Boolean indicating whether pressing escape key 
                // should close the checkout form. (default: true)
                escape: true,
                // Boolean indicating whether clicking translucent blank
                // space outside checkout form should close the form. (default: false)
                backdropclose: false
            };
            var rzp = new Razorpay(options);
            document.getElementById('rzp-button1').onclick = function (e) {
                rzp.open();
                e.preventDefault();
            }
        </script>

        </div>
    </form>
</body>
</html>
