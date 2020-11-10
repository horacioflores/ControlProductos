<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="wellcome.aspx.cs" Inherits="ControlProductos.wellcome" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/drilldown.js"></script>
<script src="https://code.highcharts.com/highcharts-more.js"></script>
<script src="https://code.highcharts.com/modules/solid-gauge.js"></script>
<script src="https://code.highcharts.com/modules/exporting.js"></script>
<script src="https://code.highcharts.com/modules/export-data.js"></script>
<script src="https://code.highcharts.com/modules/accessibility.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .ind_card{
            margin-top: 30px !important;
        }

        .m-b-3 {
            margin-bottom: 30px !important;
        }
        .box-rounded {
            border-radius: 5px;
        }
        .card-body {
            padding: 1.25rem;
        }
        .info-box-icon.bg-transparent {
            background-color: transparent;
            height: auto;
            width: auto;
            font-size: 35px;
            line-height: 0;
        }
        .info-box-icon {
            border-top-left-radius: 2px;
            border-top-right-radius: 0;
            border-bottom-right-radius: 0;
            border-bottom-left-radius: 2px;
            display: block;
            float: right;
            height: 65px;
            width: 65px;
            text-align: center;
            font-size: 28px;
            line-height: 65px;
            background: rgba(0,0,0,0.2);
            border-radius: 50%;
        }
        .box-gradient {
            background: #328dff;
            background: -webkit-linear-gradient(135deg, #3d74f1, #9986ee);
            background: linear-gradient(-45deg, #3d74f1, #9986ee);
        }
        .box-gradient-2 {
            background: #7a86ff;
            background: -webkit-linear-gradient(135deg, #7a86ff, #949dff);
            background: linear-gradient(-45deg, #7a86ff, #949dff);
        }
        .box-gradient-3 {
            background: #fab63f;
            background: -webkit-linear-gradient(135deg, #fab63f, #fbc465);
            background: linear-gradient(-45deg, #fab63f, #fbc465);
        }
        .box-gradient-4 {
            background: #fe413b;
            background: -webkit-linear-gradient(135deg, #fe413b, #fc7572);
            background: linear-gradient(-45deg, #fe413b, #fc7572);
        }
    </style>

    <%--<div class="content">
        <div class="row">
            <div class="col-lg-12 ind_card">
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 m-b-3">
                    <div class="card">
                    <div class="card-body box-rounded box-gradient"> <span class="info-box-icon bg-transparent"><i class="glyphicon glyphicon-dashboard text-white"></i></span>
                        <div class="info-box-content">
                        <h6 class="info-box-text text-white">Totoal Missing</h6>
                        <h1 class="text-white"><asp:Label ID="lblTotalMissing" runat="server" Text=""></asp:Label></h1>
                        </div>
                    </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 m-b-3">
                    <div class="card">
                    <div class="card-body box-rounded box-gradient-4"> <span class="info-box-icon bg-transparent"><i class="glyphicon glyphicon-stats text-white"></i></span>
                        <div class="info-box-content">
                        <h6 class="info-box-text text-white">Total Quoted</h6>
                        <h1 class="text-white"><asp:Label ID="lblTotalQuoted" runat="server" Text=""></asp:Label></h1>
                        </div>
                    </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 m-b-3">
                    <div class="card">
                    <div class="card-body box-rounded box-gradient-2"> <span class="info-box-icon bg-transparent"><i class="glyphicon glyphicon-time text-white"></i></span>
                        <div class="info-box-content">
                        <h6 class="info-box-text text-white">Days Passed</h6>
                        <h1 class="text-white"><asp:Label ID="lbldaysPass" runat="server" Text=""></asp:Label></h1>
                        </div>
                    </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 m-b-3">
                    <div class="card">
                    <div class="card-body box-rounded box-gradient-3"> <span class="info-box-icon bg-transparent"><i class="glyphicon glyphicon-calendar text-white"></i></span>
                        <div class="info-box-content">
                        <h6 class="info-box-text text-white">Remaining Days</h6>
                        <h1 class="text-white"><asp:Label ID="lblRemaingdays" runat="server" Text=""></asp:Label></h1>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-12">
                <div class="col-lg-6" id="betterCost"></div>
                <div class="col-lg-6" id="bestDelivery"></div>
            </div>
        </div>
    </div>--%>
</asp:Content>
