<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/MasterPage.Master" AutoEventWireup="true" CodeBehind="ViewBorrower.aspx.cs" Inherits="Net_project.ViewBorrower1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mb-3">
        <div class="d-flex justify-content-between align-items-center">
            <h1>View Borrower</h1>
            <p class="text-primary p-0 m-0 align-self-end" onclick="viewHistory()" style="cursor: pointer;">Borrow History</p>
        </div>

        <div class="border rounded-3 border-secondary p-3 mt-3">
            <div class="container">
                <div class="row mt-3">
                    <label class="form-label col-12 p-0">Borrower Name:</label>
                    <input type="text" readonly class="form-control border border-secondary col-12" id="name" runat="server" />
                </div>
                <div class="row mt-3">
                    <label class="col-4 form-label p-0">Age:</label>
                    <label class="col-4 form-label p-0">Gender:</label>
                    <label class="col-4 form-label p-0">Fine Status:</label>
                </div>
                <div class="row">
                    <input type="number" min="12" max="99" class="col-3 border border-secondary read-input" id="age" readonly runat="server" />
                    <div class="col-1"></div>
                    <select class="col-3 form-contorl border border-secondary read-dropdown" id="gender" disabled runat="server">
                        <option value="male">Male</option>
                        <option value="female">Female</option>
                    </select>
                    <div class="col-1"></div>
                    <select class="col-3 form-contorl border border-secondary read-dropdown" id="fineStatus" disabled runat="server">
                        <option value="False">Clear</option>
                        <option value="True">Fined</option>
                    </select>
                </div>
                <div class="row mt-3">
                    <label class="form-label p-0">Email Address:</label>
                    <input type="email" class="form-control border border-secondary read-input" id="email" readonly runat="server" />
                </div>
                <div class="row mt-3">
                    <label class="form-label p-0">Phone Number:</label>
                    <input type="text" class="form-control border border-secondary read-input" id="phoneNumber" readonly runat="server" />
                </div>
                <div class="row mt-3">
                    <label class="form-label p-0">Home Address:</label>
                    <textarea class="form-control border border-secondary read-input" rows="3" style="resize: none;" id="address" readonly runat="server"></textarea>
                </div>
                <button class="btn btn-secondary d-block mx-auto mt-5 w-25" runat="server" type="submit" onclick="return handleBack()">Back</button>
                <div />
            </div>
        </div>

        <script>
            const handleBack = () => {
                const urlParams = new URLSearchParams(window.location.search);
                const value = urlParams.get('value');
                const page = urlParams.get('page');

                if (!value) {
                    if (page) {
                        window.location.href = `ManageBorrower.aspx?page=${page}`;
                        return false;
                    } else {
                        window.location.href = `ManageBorrower.aspx`;
                        return false;
                    }
                } else {
                    if (page) {
                        window.location.href = `ManageBorrower.aspx?value=${value}&page=${page}`;
                        return false;
                    } else {
                        window.location.href = `ManageBorrower.aspx?value=${value}`;
                        return false;
                    }
                }
                return false;
            }

            const viewHistory = () => {

            }
        </script>
    </div>
</asp:Content>
