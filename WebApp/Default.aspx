<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp.Default" %>

<!DOCTYPE html>
<html lang="pt-br" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>XML Reader</title>
    <script src="https://unpkg.com/vue@3/dist/vue.global.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.7.0.min.js" integrity="sha256-2Pmvv0kuTBOenSvLm6bvfBSSHrUJ+3A7x6P5Ebd07/g=" crossorigin="anonymous"></script>
    <link href="https://cdn.datatables.net/1.13.5/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" rel="stylesheet" />
    <style>
        body {
            overflow-x: hidden;
        }

        .fa {
            display: flex;
            justify-content: center;
            align-items: center;
            font-size: 22px;
            padding-top: 10px;
        }

        .modal {
            position: fixed;
            width: 35%;
            height: 100%;
            left: 36%;
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 9999;
        }

        .dataTables_filter input {
            margin-right: 90px; /* Defina o valor de margem esquerda desejado */
            position: relative;
        }

        .dataTables_wrapper .dataTables_filter input {
            border-radius: 5px;
        }

        .overlay {
            position: absolute;
            right: 0;
            float: right;
            z-index: 1;
        }


        .modal-content {
            background-color: #0D6efd;
            padding: 30px;
            border-radius: 5px;
        }

        .modal-close {
            position: absolute;
            font-size: 35px;
            top: 10px;
            right: 10px;
            cursor: pointer;
            color: aliceblue;
        }

        .btn-outline-success:hover {
            background-color: white;
            color: #fff;
        }

        .table-container {
            width: 40%;
            padding-left: 10%;
        }

        .dataTables_wrapper {
            position: relative;
        }

        .ms-2 {
            width: 50px !important;
            display: flex;
        }

    </style>
</head>

<body>
    <div id="app"></div>
    <script src="https://unpkg.com/vuex@4.1.0/dist/vuex.global.js"></script>
    <script src="https://cdn.datatables.net/1.13.5/js/jquery.dataTables.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/axios/dist/axios.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.7.0.js"></script>
    <script src="https://cdn.datatables.net/1.13.5/js/jquery.dataTables.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.11.5/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/plug-ins/1.11.3/i18n/Portuguese.json"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous"></script>
    <script src="./js/App.js" type="module"></script>

</body>
</html>
