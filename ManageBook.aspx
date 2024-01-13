﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/MasterPage.Master" AutoEventWireup="true" CodeBehind="ManageBook.aspx.cs" Inherits="Net_project.ManageBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mb-3">
        <h1>Manage Book</h1>
        <div class="d-flex justify-content-between mt-4">
            <div class="d-flex align-items-center justify-content-between">
                <div class="d-flex align-items-center">
                    <div class="d-flex align-items-center border border-secondary rounded-2 p-0 pe-2 search-container col-8" onclick="focusInput()" runat="server">
                        <input type="text" id="search" class="form-control search-input w-100 px-2" maxlength="70" autocomplete="off" placeholder="Book title" onkeypress="return handlePress(event)">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-search" viewBox="0 0 16 16">
                            <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001q.044.06.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1 1 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0" />
                        </svg>
                    </div>
                    <form class="col-4 m-0" onsubmit="return handleSearch()">
                        <button id="btnSearch" class="btn btn-primary ms-3">Search</button>
                    </form>
                </div>
            </div>
            <a class="btn btn-success" href="AddNewBook.aspx">Add New Book</a>
        </div>
        <table class="table table-hover table-bordered border-secondary mt-4">
            <thead>
                <tr>
                    <th scope="col" class="col-1 text-center">No</th>
                    <th scope="col" class="col-6">Book title</th>
                    <th scope="col" class="col-2 text-center">Rack number</th>
                    <th scope="col" class="col-3 text-center">Operation</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="BookTable" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="text-center"><%# CalculateItemIndex(Container.ItemIndex + 1)%></td>
                            <td><%# Eval("bookTitle") %></td>
                            <td class="text-center"><%# Eval("bookRackNumber") %></td>
                            <td class="d-flex align-items-center justify-content-around">
                                <button class="btn btn-primary op-btn" onclick='<%# "viewBook(" + Eval("BookID") + ")" %>'>View</button>
                                <button class="btn btn-success op-btn" onclick='<%# "editBook(" + Eval("BookID") + ")" %>'>Edit</button>
                                <button class="btn btn-danger op-btn" onclick='<%# "removeBook(" + Eval("BookID") + ", \"" + Eval("BookTitle") + "\")" %>'>Remove</button>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <div runat="server" id="PageContainer" class="d-flex align-items-center justify-content-center border border-secondary py-1">
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script>
        const focusInput = () => {
            const searchInput = document.getElementById("search");
            searchInput.focus();
        }

        function handlePageLabelClick(pageNumber) {
            window.location.href = `ManageBook.aspx?page=${pageNumber}`;
        }

        function handleSearchPageLabelClick(pageNumber, searchString) {
            window.location.href = `ManageBook.aspx?page=${pageNumber}&value=${encodeURIComponent(searchString)}`;
        }

        const handlePress = (e) => {
            if (e.key === "Enter") {
                return handleSearch();
            }
        }

        const handleSearch = () => {
            const inputValue = document.getElementById("search").value;
            if (inputValue == "") {
                window.location.href = "ManageBook.aspx";
                return false;
            }
            window.location.href = `ManageBook.aspx?value=${inputValue}`;
            return false;
        }

        const viewBook = (id) => {
            const urlParams = new URLSearchParams(window.location.search);
            const valueFromUrl = urlParams.get('value');
            const pageFromUrl = urlParams.get('page');
            if (!valueFromUrl) {
                if (pageFromUrl) {
                    window.location.href = `ViewBook.aspx?id=${id}&page=${pageFromUrl}&dest=ManageBook.aspx`;
                } else {
                    window.location.href = `ViewBook.aspx?id=${id}&dest=ManageBook.aspx`;
                }
            } else {
                if (pageFromUrl) {
                    window.location.href = `ViewBook.aspx?id=${id}&value=${valueFromUrl}&page=${pageFromUrl}&dest=ManageBook.aspx`;
                } else {
                    window.location.href = `ViewBook.aspx?id=${id}&value=${valueFromUrl}&dest=ManageBook.aspx`;
                }
            }
        }

        const editBook = (id) => {
            window.location.href = `EditBook.aspx?id=${id}`;
        }

        const removeBook = (id, title) => {
            const result = confirm(`Remove ${title}?`);
            if (result) {
                $.ajax({
                    url: 'ManageBook.aspx/RemoveBook',
                    type: 'POST',
                    data: JSON.stringify({ bookIdString: id }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        console.log(data);
                        alert("Removed");
                        window.location.href = "ManageBook.aspx";
                    },
                    error: function (error) {
                        console.error("Error retrieving borrower information:", error);
                    }
                });
            }
        }
    </script>
</asp:Content>

