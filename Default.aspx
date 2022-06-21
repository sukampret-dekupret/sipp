<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>Login SIPP</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--===============================================================================================-->

    <link rel="icon" type="image/png" href="x_images/icons/kemlu.ico" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="x_vendor/bootstrap/css/bootstrap.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="x_fonts/font-awesome-4.7.0/css/font-awesome.min.css" />
    <!--==============================================================================================-->
    <link rel="stylesheet" type="text/css" href="x_fonts/Linearicons-Free-v1.0.0/icon-font.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="x_vendor/animate/animate.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="x_vendor/css-hamburgers/hamburgers.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="x_vendor/animsition/css/animsition.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="x_vendor/select2/select2.min.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="x_vendor/daterangepicker/daterangepicker.css" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="x_css/util.css" />
    <link rel="stylesheet" type="text/css" href="x_css/main.css" />
    <!--===============================================================================================-->
</head>
<body style="background-color: #666666;">
    <div class="limiter">
        <div class="container-login100">
            <div class="wrap-login100">
                <form class="login100-form validate-form" runat="server">
                    <span class="login100-form-title p-b-43">L O G I N
                    </span>


                    <div class="wrap-input100 validate-input" data-validate="masukan username yang benar ">
                        <input class="input100" type="text" name="email" runat="server" id="txtUsername"/>
                        <span class="focus-input100"></span>
                        <span class="label-input100">Username</span>
                    </div>


                    <div class="wrap-input100 validate-input" data-validate="Password is required">
                        <input class="input100" type="password" name="pass" runat="server" id="txtPassword"/>
                        <span class="focus-input100"></span>
                        <span class="label-input100">Password</span>
                    </div>



                    <div class="container-login100-form-btn">
                        <asp:Button ID="btnSubmit" runat="server" Text="Login" CssClass="login100-form-btn" OnClick="Button1_Click" />
<%--                        <button class="login100-form-btn">
                            Login
                        </button>--%>
                    </div>
                </form>

                <div class="login100-more" style="background-image: url('x_images/loginsipp2.png');">
                </div>
            </div>
        </div>
    </div>



    <!--===============================================================================================-->
    <%--<script src="x_vendor/jquery/jquery-3.2.1.min.js"></script>--%>
    <script src="x_vendor/jquery/jquery-3.6.0.min.js"></script>

    <!--===============================================================================================-->
    <script src="x_vendor/animsition/js/animsition.min.js"></script>
    <!--===============================================================================================-->
    <script src="x_vendor/bootstrap/js/popper.js"></script>
    <script src="x_vendor/bootstrap/js/bootstrap.min.js"></script>
    <!--===============================================================================================-->
    <script src="x_vendor/select2/select2.min.js"></script>
    <!--===============================================================================================-->
    <script src="x_vendor/daterangepicker/moment.min.js"></script>
    <script src="x_vendor/daterangepicker/daterangepicker.js"></script>
    <!--===============================================================================================-->
    <script src="x_vendor/countdowntime/countdowntime.js"></script>
    <!--===============================================================================================-->
    <script src="x_js/main.js"></script>
</body>
</html>
