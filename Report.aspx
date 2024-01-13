<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/MasterPage.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Net_project.Report" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Most Popular Book</h5>
                            <asp:Label class="card-text" ID="MostPopularBookTitle" runat="server" />
                            <br />
                            <asp:Label class="card-text" ID="MostPopularBookAuthor" runat="server" />
                            <br />
                            <asp:Label class="card-text" ID="MostQuantity" runat="server" />
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Late Returns Count</h5>
                            <asp:Label class="card-text" ID="CurrentlyLateforReturn" runat="server" />
                            <br />
                            <asp:Label class="card-text" ID="TotalLateReturns" runat="server" />
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Least Popular Book</h5>
                            <asp:Label class="card-text" ID="LeastPopularBookTitle" runat="server" />
                            <br />
                            <asp:Label class="card-text" ID="LeastPopularBookAuthor" runat="server" />
                            <br />
                            <asp:Label class="card-text" ID="LeastQuantity" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row mt-5">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Borrowed Books Over Time</h5>
                        <h5 class="card-title">&nbsp;</h5>
                        <div class="d-flex justify-content-center">

                            <asp:Chart ID="Chart1" runat="server" DataSourceID="SqlDataSource1" Height="422px" Width="1433px" ViewStateMode="Enabled">
                                <Series>
                                    <asp:Series Name="Series1" XValueMember="DateInMonth" YValueMembers="quantity" IsXValueIndexed="True"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisY Title="Borrowed Books Quantity">
                                        </AxisY>
                                        <AxisX Interval="1" IsLabelAutoFit="False" Title="Days">
                                            <LabelStyle Angle="-90" />
                                            <MajorGrid Enabled="False" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TestDatabaseConnectionString %>" SelectCommand="WITH DateRange AS (
    SELECT
        DATEADD(DAY, number - 1, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)) AS DateInMonth
    FROM master.dbo.spt_values
    WHERE type = 'P' AND number &lt;= DAY(EOMONTH(GETDATE()))
)
SELECT
    dr.DateInMonth,
    ISNULL(COUNT(bb.borrowDate), 0) AS quantity
FROM DateRange dr
LEFT JOIN Borrower_Book bb ON CONVERT(DATE, bb.borrowDate) = dr.DateInMonth
GROUP BY dr.DateInMonth
ORDER BY dr.DateInMonth;
"
                                ProviderName="<%$ ConnectionStrings:TestDatabaseConnectionString.ProviderName %>"></asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</asp:Content>
