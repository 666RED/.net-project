<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReturnBook.aspx.cs" Inherits="Net_project.ReturnBook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mb-3">
    <h1>Return Book</h1>
    <form class="border-secondary rounded-3 border p-3 mt-3" onsubmit="return handleReturn()">
        <div class="container">
            <div class="container">
                <div class="row mt-3">
                    <label class="form-label col-12 p-0">Book Title:</label>
                    <input type="text" readonly class="form-control border border-secondary col-12" id="title" runat="server" />
                </div>
                <div class="row mt-3">
                    <label class="form-label col-12 p-0">Borrower Name:</label>
                    <input type="text" id="name" class="col-12 form-control border border-secondary" readonly/>
                </div>
                <div class="row mt-3">
                    <label class="col-6 form-label p-0">Borrow Date:</label>
                    <label class="col-6 form-label p-0">Return Date:</label>
                </div>
                <div class="row">
                    <input type="date" class="col-3 border border-secondary" id="borrowDate" readonly/>
                    <div class="col-3"></div>
                    <input type="date" class="col-3 border border-secondary" id="returnDate" readonly/>
                </div>
                <button class="btn btn-primary d-block mx-auto mt-5 w-25" runat="server" type="submit">Return</button>
            <div/>
        </div>
        </div>
    </form>

    
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script>

        const handleReturn = () => {
            const urlParams = new URLSearchParams(window.location.search);
            const bookId = urlParams.get('id');
            if (document.getElementById("existed").checked) {
                const borrowerId = document.getElementById('name-dropdown').value;
                const borrowDate = document.getElementById('borrow-date-input').value;
                const returnDate = document.getElementById('return-date-input').value;

                $.ajax({
                    url: 'BorrowBook.aspx/BorrowBookAndUpdateAvailability',
                    type: 'POST',
                    data: JSON.stringify({ borrowerId, bookId, borrowDate, returnDate }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        alert("Saved");
                        window.location.href = "Default.aspx";
                    },
                    error: function (error) {
                        console.error("Error borrowing book:", error);
                    }
                });
            } else {
                const name = document.getElementById("name-input").value;
                const age = document.getElementById("age-input").value;
                const gender = document.getElementById("gender-dropdown").value;
                const fineStatus = document.getElementById("fine-status-dropdown").value;
                const email = document.getElementById("email-input").value;
                const phoneNumber = document.getElementById("phone-number-input").value;
                const address = document.getElementById("address-input").value;
                const borrowDate = document.getElementById("borrow-date-input").value;
                const returnDate = document.getElementById("return-date-input").value;

                $.ajax({
                    url: 'BorrowBook.aspx/AddBorrowerAndUpdateAvailability',
                    type: 'POST',
                    data: JSON.stringify({ name, bookId, age, gender, fineStatus, email, phoneNumber, address, borrowDate, returnDate }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        alert("Saved");
                        window.location.href = "Default.aspx";
                    },
                    error: function (error) {
                        console.error("Error borrowing book:", error);
                    }
                });

            }
            return false;
        }
    </script>
</div>
</asp:Content>
