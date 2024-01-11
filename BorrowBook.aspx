<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/MasterPage.Master" AutoEventWireup="true" CodeBehind="BorrowBook.aspx.cs" Inherits="Net_project.BorrowBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mb-3">
        <h1>Borrow Book</h1>
        <form class="border-secondary rounded-3 border p-3 mt-3" onsubmit="return handleSubmit()">
            <div class="container">
                <div class="container">
                    <div class="row mt-3">
                        <label class="form-label col-12 p-0">Book Title:</label>
                        <input type="text" readonly class="form-control border border-secondary col-12" id="title" runat="server"/>
                    </div>
                    <div class="d-flex flex-row align-items-center mt-3">
                        <input type="checkbox" onchange="handleOnChange()" id="existed"/>
                        <label class="ms-2">Existing user</label>
                    </div>
                    <div class="row mt-3">
                        <label class="form-label col-12 p-0">Borrower Name:</label>
                        <select id="name-dropdown" style="display: none;" class="col-12 form-control border border-secondary" onchange="getBorrowerInfo()">
                            <option value="">Please select borrower</option>
                        </select>
                        <input type="text" id="name-input" class="col-12 form-control border border-secondary" required name="name"/>
                    </div>
                    <div class="row mt-3">
                        <label class="col-4 form-label p-0">Age:</label>
                        <label class="col-4 form-label p-0">Gender:</label>
                        <label class="col-4 form-label p-0">Fine Status:</label>
                    </div>
                    <div class="row">
                        <input type="number" min="12" max="99" class="col-3 border border-secondary read-input" required id="age-input"/>
                        <div class="col-1"></div>
                        <select class="col-3 form-contorl border border-secondary read-dropdown" required id="gender-dropdown">
                            <option value="male">Male</option>
                            <option value="female">Female</option>
                        </select>
                        <div class="col-1"></div>
                        <select class="col-3 form-contorl border border-secondary read-dropdown" required id="fine-status-dropdown">
                            <option value="False">Clear</option>
                            <option value="True">Fined</option>
                        </select>
                    </div>
                    <div class="row mt-3">
                        <label class="form-label p-0">Email Address:</label>
                        <input type="email" class="form-control border border-secondary read-input" required id="email-input"/>
                    </div>
                    <div class="row mt-3">
                        <label class="form-label p-0">Phone Number:</label>
                        <input type="text" class="form-control border border-secondary read-input" required id="phone-number-input"/>
                    </div>
                    <div class="row mt-3">
                        <label class="form-label p-0">Home Address:</label>
                        <textarea class="form-control border border-secondary read-input" rows="3" required style="resize: none;" id="address-input"></textarea>
                    </div>
                    <div class="row mt-3">
                        <label class="col-6 form-label p-0">Borrow Date:</label>
                        <label class="col-6 form-label p-0">Return Date:</label>
                    </div>
                    <div class="row">
                        <input type="date" class="col-3 border border-secondary" value="<%= DateTime.Now.ToString("yyyy-MM-dd") %>" required id="borrow-date-input"/>
                        <div class="col-3"></div>
                        <input type="date" class="col-3 border border-secondary" min="<%= DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") %>" required id="return-date-input"/>
                    </div>
                    <button class="btn btn-success d-block mx-auto mt-5 w-25" runat="server" type="submit">Submit</button>
                <div/>
            </div>
            </div>
        </form>

        
        <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
        <script>
            const handleOnChange = () => {
                const existed = document.getElementById("existed").checked;
                const dropdown = document.getElementById("name-dropdown");
                const nameInput = document.getElementById("name-input");

                const inputElements = document.querySelectorAll('.read-input');
                const dropdownElements = document.querySelectorAll('.read-dropdown');

                if (existed) {
                    dropdown.style['display'] = '';
                    nameInput.style['display'] = 'none';
                    dropdown.setAttribute('required', 'required');
                    nameInput.removeAttribute('required');
                    const checkBox = document.getElementById("existed");

                    inputElements.forEach(input => {
                        input.setAttribute('readonly', 'readonly');
                    });

                    dropdownElements.forEach(dropdown => {
                        dropdown.setAttribute('disabled', 'disabled');
                    });

                    $.ajax({
                        url: 'BorrowBook.aspx/RetrieveBorrowerInformation',
                        type: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (data) {
                            const parsedData = JSON.parse(data.d);
                            for (let i = 0; i < parsedData.length; i++) {
                                const borrowerName = parsedData[i].borrowerName; 
                                const borrowerId = parsedData[i].borrowerId;
                                $('#name-dropdown').append('<option value="' + borrowerId + '">' + borrowerName + '</option>');
                            }
                        },
                        error: function (error) {
                            console.error("Error retrieving borrower information:", error);
                        }
                    });
                } else {
                    const genderDropDown = document.getElementById("gender-dropdown");
                    const fineStatusDropDown = document.getElementById("fine-status-dropdown");

                    dropdown.style['display'] = 'none';
                    nameInput.style['display'] = '';
                    dropdown.removeAttribute('required')
                    nameInput.setAttribute('required', 'required');

                    inputElements.forEach(input => {
                        input.removeAttribute('readonly');
                        input.value = "";
                    });

                    dropdownElements.forEach(dropdown => {
                        dropdown.removeAttribute('disabled');
                    });

                    genderDropDown.value = "male";
                    fineStatusDropDown.value = "False";

                    $('#name-dropdown').empty();
                    $('#name-dropdown').append('<option value="">Please select borrower</option>');
                }
            }

            function getBorrowerInfo() {
                const borrowerId = document.getElementById('name-dropdown').value;
                $.ajax({
                    url: 'BorrowBook.aspx/RetrieveBorrowerInformationByName',
                    type: 'POST',
                    data: JSON.stringify({ borrowerId: borrowerId }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        const borrowerInfo = JSON.parse(data.d);
                        $('#age-input').val(borrowerInfo.age);
                        $('#gender-dropdown').val(borrowerInfo.gender);
                        $('#fine-status-dropdown').val(borrowerInfo.fineStatus);
                        $('#email-input').val(borrowerInfo.emailAddress);
                        $('#phone-number-input').val(borrowerInfo.phoneNumber);
                        $('#address-input').val(borrowerInfo.address);
                    },
                    error: function (error) {
                        console.error("Error retrieving borrower information:", error);
                    }
                });
            }

            const handleSubmit = () => {
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
