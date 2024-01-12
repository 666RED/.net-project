<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/MasterPage.Master" AutoEventWireup="true" CodeBehind="ViewBook.aspx.cs" Inherits="Net_project.ViewBook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mb-3">
        <div class="d-flex justify-content-between align-items-center">
            <h1>View Book</h1>
            <p class="text-primary p-0 m-0 align-self-end" onclick="viewHistory()" style="cursor: pointer;">Borrow History</p>
        </div>

        <form class="border-secondary rounded-3 border p-3 mt-3">
            <div class="container">
                <div class="row">
                    <label class="form-label col-6 px-0">Book Title:</label>
                    <label class="form-label col-6 px-0">Author:</label>
                </div>
                <div class="row">
                    <input type="text" class="border border-secondary col-5 rounded-3" readonly runat="server" id="title"/>
                    <div class="col-1"></div>
                    <input type="text" class="border border-secondary col-5 rounded-3" readonly runat="server" id="author"/>
                </div>
                <div class="row mt-3">
                    <label class="form-label col-6 px-0">Pages:</label>
                    <label class="form-label col-6 px-0">ISBN:</label>
                </div>
                <div class="row">
                    <input type="text" class="border border-secondary col-5 rounded-3" readonly runat="server" id="pages"/>
                    <div class="col-1"></div>
                    <input type="text" class="border border-secondary col-5 rounded-3" readonly runat="server" id="isbn"/>
                </div>
                <div class="row mt-3">
                    <label class="form-label col-6 px-0">Publisher:</label>
                    <label class="form-label col-6 px-0">Publish Date:</label>
                </div>
                <div class="row">
                    <input type="text" class="border border-secondary col-5 rounded-3" readonly runat="server" id="publisher"/>
                    <div class="col-1"></div>
                    <input type="text" class="border border-secondary col-5 rounded-3" readonly runat="server" id="publishDate"/>
                </div>
                <div class="row mt-3">
                    <label class="form-label col-6 px-0">Language:</label>
                    <label class="form-label col-6 px-0">Price:</label>
                </div>
                <div class="row">
                    <input type="text" class="border border-secondary col-5 rounded-3" readonly runat="server" id="language"/>
                    <div class="col-1"></div>
                    <input type="text" class="border border-secondary col-5 rounded-3" readonly runat="server" id="price"/>
                </div>
                <div class="row mt-3">
                    <label class="form-label col-6 px-0">Rack Number:</label>
                    <label class="form-label col-6 px-0">Status:</label>
                </div>
                <div class="row">
                    <input type="text" class="border border-secondary col-5 rounded-3" readonly runat="server" id="rackNumber"/>
                    <div class="col-1"></div>
                    <input type="text" class="border border-secondary col-5 rounded-3" readonly runat="server" id="availability"/>
                </div>
                <button class="btn btn-secondary d-block mx-auto mt-5 w-25" onclick="return handleBack()">Back</button>
            </div>
        </form>

        <script>
            const handleBack = () => {
                const urlParams = new URLSearchParams(window.location.search);
                const value = urlParams.get('value');
                const page = urlParams.get('page');
                if (!value) {
                    if (page) {
                        window.location.href = `Default.aspx?page=${page}`;
                        return false;
                    } else {
                        window.location.href = `Default.aspx`;
                        return false;
                    }
                } else {
                    if (page) {
                        window.location.href = `Default.aspx?value=${value}&page=${page}`;
                        return false;
                    } else {
                        window.location.href = `Default.aspx?value=${value}`;
                        return false;
                    }
                }
            }

            const viewHistory = () => {
                const urlParams = new URLSearchParams(window.location.search);
                const id = urlParams.get('id');

                window.location.href = `ViewBorrowHistory.aspx?id=${id}`;
            }
        </script>
    </div>
</asp:Content>
