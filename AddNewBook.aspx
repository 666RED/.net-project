<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddNewBook.aspx.cs" Inherits="Net_project.AddNewBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mb-3">
        <h1>Add New Book</h1>
        <form class="border-secondary rounded-3 border p-3 mt-3" onsubmit="return handleAdd()">
            <div class="container">
                <div class="row">
                    <label class="form-label col-6 px-0">Book Title:</label>
                    <label class="form-label col-6 px-0">Author:</label>
                </div>
                <div class="row">
                    <input type="text" class="border border-secondary col-5 rounded-3" id="title" required />
                    <div class="col-1"></div>
                    <input type="text" class="border border-secondary col-5 rounded-3" id="author" required />
                </div>
                <div class="row mt-3">
                    <label class="form-label col-6 px-0">Pages:</label>
                    <label class="form-label col-6 px-0">ISBN:</label>
                </div>
                <div class="row">
                    <input type="number" min="1" class="border border-secondary col-5 rounded-3" id="pages" required />
                    <div class="col-1"></div>
                    <input type="text" class="border border-secondary col-5 rounded-3" id="isbn" required />
                </div>
                <div class="row mt-3">
                    <label class="form-label col-6 px-0">Publisher:</label>
                    <label class="form-label col-6 px-0">Publish Date:</label>
                </div>
                <div class="row">
                    <input type="text" class="border border-secondary col-5 rounded-3" id="publisher" required />
                    <div class="col-1"></div>
                    <input type="date" class="border border-secondary col-5 rounded-3" id="publishDate" required />
                </div>
                <div class="row mt-3">
                    <label class="form-label col-6 px-0">Language:</label>
                    <label class="form-label col-6 px-0">Price:</label>
                </div>
                <div class="row">
                    <select class="border border-secondary col-5 rounded-3" id="language" required>
                        <option value="English">English</option>
                        <option value="Malay">Malay</option>
                        <option value="Chinese">Chinese</option>
                        <option value="Tamil">Tamil</option>
                    </select>
                    <div class="col-1"></div>
                    <input type="number" min="0.01" step="0.01" max="999.99" class="border border-secondary col-5 rounded-3" id="price" required placeholder="RM" />
                </div>
                <div class="row mt-3">
                    <label class="form-label col-6 px-0">Rack Number:</label>
                    <label class="form-label col-6 px-0">Status:</label>
                </div>
                <div class="row">
                    <input type="text" class="border border-secondary col-5 rounded-3" id="rackNumber" required />
                    <div class="col-1"></div>
                    <select class="border border-secondary col-5 rounded-3" id="availability" required>
                        <option value="True">Available</option>
                        <option value="False">Not Available</option>
                    </select>
                </div>
                <button class="btn btn-success d-block mx-auto mt-5 w-25" type="submit">Add</button>
            </div>
        </form>

        <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
        <script>
            const handleAdd = () => {
                const urlParams = new URLSearchParams(window.location.search);
                const id = urlParams.get('id');

                const title = document.getElementById("title").value;
                const author = document.getElementById("author").value;
                const pages = document.getElementById("pages").value;
                const isbn = document.getElementById("isbn").value;
                const publisher = document.getElementById("publisher").value;
                const publishDate = document.getElementById("publishDate").value;
                const language = document.getElementById("language").value;
                const price = document.getElementById("price").value;
                const rackNumber = document.getElementById("rackNumber").value;
                const availability = document.getElementById("availability").value;

                $.ajax({
                    url: 'AddNewBook.aspx/AddBook',
                    type: 'POST',
                    data: JSON.stringify({ id, title, author, pages, isbn, publisher, publishDate, language, price, rackNumber, availability }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        console.log(data);
                        alert("Saved");
                        window.location.href = 'ManageBook.aspx';
                    },
                    error: function (error) {
                        console.error("Error retrieving borrower information:", error);
                    }
                });
                return false;
            }
        </script>
    </div>
</asp:Content>
