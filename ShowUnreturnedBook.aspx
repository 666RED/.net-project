<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/MasterPage.Master" AutoEventWireup="true" CodeBehind="ShowUnreturnedBook.aspx.cs" Inherits="Net_project.ShowUnreturnedBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>Unreturned Books</h1>
        <table class="table table-hover table-bordered border-secondary mt-4">
            <thead>
                <tr>
                    <th scope="col" class="col-1 text-center">No</th>
                    <th scope="col" class="col-5">Book Title</th>
                    <th scope="col" class="col-2 text-center">Borrow Date</th>
                    <th scope="col" class="col-2 text-center">Return Date</th>
                    <th scope="col" class="col-2 text-center">Operation</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="BookTable" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="text-center"><%# Container.ItemIndex + 1%></td>
                            <td><%# Eval("bookTitle") %></td>
                            <td class="text-center"><%# Eval("borrowDate", "{0:M-d-yyyy}") %></td>
                            <td class="text-center"><%# Eval("returnDate", "{0:M-d-yyyy}") %></td>
                            <td class="d-flex align-items-center justify-content-center">
                                <a class="btn btn-warning op-btn" href="ReturnBook.aspx?id=<%# Eval("bookId") %>">Return</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>
