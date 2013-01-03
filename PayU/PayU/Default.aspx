﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PayUOperation.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="https://secure.payu.com.tr/openpayu/v2/client/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="https://secure.payu.com.tr/openpayu/v2/client/json2.js" type="text/javascript"></script>
    <script src="https://secure.payu.com.tr/openpayu/v2/client/openpayu-2.0.js" type="text/javascript"></script>
    <script src="https://secure.payu.com.tr/openpayu/v2/client/plugin-payment-2.0.js" type="text/javascript"></script>
    <script src="https://secure.payu.com.tr/openpayu/v2/client/plugin-installment-2.0.js" type="text/javascript"></script>
    <!--  Style class for preloader -->
    <link rel="stylesheet" type="text/css" href="https://secure.payu.com.tr/openpayu/v2/client/openpayu-builder-2.0.css"/>
    <style type="text/css">
        .card
        {
            width: 200px;
            height: 20px;
            border: 1px solid #CCC;
            margin: 1px;
            padding: 1px;
            font-size: 16px;
        }
        .style1
        {
            height: 30px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" method="post">
    <div>
        <h1>
            OpenPayU JS+API Implementaion</h1>
        <h2>
            Test cards:</h2>
        <p>
            card no: 4022774022774026<br />
            exp: 12/2012<br />
            cvv: 000</p>
        <p>
            card no: 4508034508034509<br />
            exp: 12/2012<br />
            cvv: 000</p>
        <p>
            card no: 4355084355084358 (for 3dSecure)<br />
            exp: 12/2012<br />
            cvv: 000</p>
        <p>
            Card program: <span id="card-program"/>
        </p>
        <p>
            Error message: <span id="error" style="color:Red" />
        </p>
    </div>
    <div>
        <table>
            <%--<tr>
                <td>
                    Product name
                </td>
                <td>
                    <input runat="server" type="text" style="width: 400px" id="description" />
                </td>
            </tr>
            <tr>
                <td>
                    Amount (TRY)
                </td>
                <td>
                    <input  type="text" id="amount" />
                </td>
            </tr>--%>
            <tr>
                <td>
                    First Name
                </td>
                <td>
                    <input type="text" id="first_name" />
                    
                </td>
            </tr>
            <tr>
                <td>
                    Last Name
                </td>
                <td>
                    <input type="text" id="last_name" />
                </td>
            </tr>
            <tr>
                <td>
                    Email
                </td>
                <td>
                    <input type="text" id="email" />
                </td>
            </tr>
            <tr>
                <td>
                    Phone
                </td>
                <td>
                    <input type="text" id="phone" />
                </td>
            </tr>
            <tr>
                <td>
                    Name
                </td>
                <td>
                <div  id="payu-card-cardholder-placeholder" class="card"  ></div>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Card number
                </td>
                <td class="style1">
                <div   id="payu-card-number-placeholder" class="card" ></div>
                </td>
            </tr>
            <tr>
                <td>
                    Card cvv
                </td>
                <td>
                <div  id="payu-card-cvv-placeholder" class="card" ></div>
                </td>
            </tr>
            <tr>
                <td>
                    Expiration month
                </td>
                <td>
                    <input type="text"  class="card" id="payu-card-expm" />
                </td>
            </tr>
            <tr>
                <td>
                    Expiration year
                </td>
                <td>
                    <input type="text" name="ExpirationYear" class="card" id="payu-card-expy"  />
                </td>
            </tr>
            <tr>
                <td>
                    Installment no
                </td>
                <td>
                    <input type="text" name="InstallmentNo" class="card" id="payu-card-installment" />
                </td>
            </tr>
            <tr>
                <td>
                    <input runat="server" type="submit" id="payu"  />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">


        $(function () {

            //**********************************************************
            //installment setup 
            //**********************************************************

            //used to control some stuff when card program is change
            OpenPayU.Installment.onCardChange(function (data) { //optional
                //data.program - Axess, Bonus, Maximum, Advantage, CardFinans, World
                $('#card-program').html(JSON.stringify(data.program));
            });

            //**********************************************************
            //payment setup
            //**********************************************************
            OpenPayU.Payment.setup({ id_account: "OPU_TEST", orderCreateRequestUrl: "ocr.aspx" });


            $('#payu').click(function () {



                //add preloader
                OpenPayU.Builder.addPreloader('Please wait ... ');

                //**********************************************************
                //begin payment 
                //**********************************************************
                OpenPayU.Payment.create({
                    //merchant can send to his server side script other additional data from page. (OPTIONAL)
                    orderCreateRequestData: {
                        Email: $('#email').val(),
                        FirstName: $('#first_name').val(),
                        LastName: $('#last_name').val(),
                        Amount: $('#amount').val(),
                        Description: $('#description').val(),
                        Phone: $('#phone').val(),
                        Currency: 'TRY'
                    }
                }, function (response) {
                    //update buyer experience
                    if (response.OrderCreateResponse.Status != 'undefined' && response.OrderCreateResponse.Status.StatusCode == 'OPENPAYU_SUCCESS') 
                    {
                        alert('Thank for payment.' + '\n' + JSON.stringify(response.OrderCreateResponse.Status.StatusCode));
                    }
                     else 
                    {
                          alert('An error ocured.' + '\n' + JSON.stringify(response.OrderCreateResponse.Status.StatusCode));
                          $('#error').html(response.status + '\n' + JSON.stringify(response.OrderCreateResponse.Status.StatusCode));
                                
                    }
                    

                    //remove preloader
                    OpenPayU.Builder.removePreloader();
                    return false;
                });
                return false;
            });
        } ());

    </script>
</body>
</html>

