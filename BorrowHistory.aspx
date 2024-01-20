<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/MasterPage.Master" AutoEventWireup="true" CodeBehind="BorrowHistory.aspx.cs" Inherits="Net_project.BorrowHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <h1>Borrow History</h1>
    <table class="table table-hover table-bordered border-secondary mt-4">
        <thead>
            <tr>
                <th scope="col" class="col-1 text-center">No</th>
                <th scope="col" class="col-3">Borrower Name</th>
                <th scope="col" class="col-4 text-center">Book Title</th>
                <th scope="col" class="col-2 text-center">Borrow Date</th>
                <th scope="col" class="col-2 text-center">Return Date</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="BorrowTable" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="text-center"><%# Container.ItemIndex + 1%></td>
                        <td><%# Eval("borrowerName") %></td>
                        <td class="text-center"><%# Eval("bookTitle") %></td>
                        <td class="text-center"><%# Convert.ToDateTime(Eval("borrowDate")).ToString("MM-dd-yyyy") %></td>
                        <td class="text-center"><%# Convert.ToDateTime(Eval("returnDate")).ToString("MM-dd-yyyy") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</div>
</asp:Content>
