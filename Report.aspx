<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/MasterPage.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Net_project.Report" %>
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
                            <!-- Display information about the most popular book -->
                            <asp:Label class="card-text" ID="MostPopularBookTitle" runat="server""/>
                            <br />
                            <asp:Label class="card-text" ID="MostPopularBookAuthor" runat="server" />
                            <!-- Add more details as needed -->
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Late Borrower</h5>
                            <!-- Display information about the late borrower -->
                            <asp:Label class="card-text" ID="LateBorrowerName" runat="server" />
                            <br />
                            <asp:Label class="card-text" ID="LateDays" runat="server" />
                            <!-- Add more details as needed -->
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Least Popular Book</h5>
                            <!-- Display information about the least popular book -->
                            <asp:Label class="card-text" ID="LeastPopularBookTitle" runat="server" />
                            <br />
                            <asp:Label class="card-text" ID="LeastPopularBookAuthor" runat="server" />
                            <!-- Add more details as needed -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <div class="row mt-5">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Borrowed Books Over Time</h5>

                    <!-- Time Switch Buttons -->
                    <div class="btn-group" role="group" aria-label="Time Switch">
                        <button type="button" class="btn btn-secondary" onclick="switchTime('week')">Week</button>
                        <button type="button" class="btn btn-secondary" onclick="switchTime('month')">Month</button>
                        <button type="button" class="btn btn-secondary" onclick="switchTime('year')">Year</button>
                    </div>

                    <!-- Graph Canvas -->
                    <canvas id="borrowedBooksChart" width="800" height="400"></canvas>
                </div>
            </div>
        </div>
    </div>
    <script>
    // Dummy data for demonstration (replace with actual data)
    var weeklyData = [10, 20, 15, 25, 30, 18, 22];
    var monthlyData = [40, 35, 50, 45, 55, 48, 60];
    var yearlyData = [200, 250, 300, 280, 320, 300, 350];

    // Initial time
    var currentTime = 'week';

    // Function to switch time and update graph
    function switchTime(time) {
        currentTime = time;
        updateGraph();
    }

    // Function to update the graph based on the selected time
    function updateGraph() {
        var ctx = document.getElementById('borrowedBooksChart').getContext('2d');
        var chartData;

        switch (currentTime) {
            case 'week':
                chartData = weeklyData;
                break;
            case 'month':
                chartData = monthlyData;
                break;
            case 'year':
                chartData = yearlyData;
                break;
            default:
                chartData = [];
        }

        var borrowedBooksChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ['Week 1', 'Week 2', 'Week 3', 'Week 4', 'Week 5', 'Week 6', 'Week 7'],
                datasets: [{
                    label: 'Borrowed Books',
                    data: chartData,
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 2,
                    fill: false
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    x: {
                        type: 'category',
                        labels: ['Week 1', 'Week 2', 'Week 3', 'Week 4', 'Week 5', 'Week 6', 'Week 7']
                    },
                    y: {
                        beginAtZero: true
                    };
                }
            }
        });
    }

    // Initial graph setup
    updateGraph()
    </script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</asp:Content>
