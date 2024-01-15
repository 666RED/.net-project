<%@ Page Title="" Language="C#" MasterPageFile="~/Master Page/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditBorrower.aspx.cs" Inherits="Net_project.EditBorrower" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mb-3">
        <h1>Edit Borrower</h1>
        <div class="border rounded-3 border-secondary p-3 mt-3">
            <div class="container">
                <div class="row mt-3">
                    <label class="form-label col-12 p-0">Borrower Name:</label>
                    <input type="text" class="form-control border border-secondary col-12" id="name" required/>
                </div>
                <div class="row mt-3">
                    <label class="col-4 form-label p-0">Age:</label>
                    <label class="col-4 form-label p-0">Gender:</label>
                    <label class="col-4 form-label p-0">Fine Status:</label>
                </div>
                <div class="row">
                    <input type="number" min="12" max="99" class="col-3 border border-secondary read-input" id="age" required />
                    <div class="col-1"></div>
                    <select class="col-3 form-contorl border border-secondary read-dropdown" id="gender" required>
                        <option value="male">Male</option>
                        <option value="female">Female</option>
                    </select>
                    <div class="col-1"></div>
                    <select class="col-3 form-contorl border border-secondary read-dropdown" id="fineStatus" required>
                        <option value="False">Clear</option>
                        <option value="True">Fined</option>
                    </select>
                </div>
                <div class="row mt-3">
                    <label class="form-label p-0">Email Address:</label>
                    <input type="email" class="form-control border border-secondary read-input" id="email" required />
                </div>
                <div class="row mt-3">
                    <label class="form-label p-0">Phone Number:</label>
                    <input type="text" class="form-control border border-secondary read-input" id="phoneNumber" required pattern="[0-9]{9,11}"/>
                </div>
                <div class="row mt-3">
                    <label class="form-label p-0">Home Address:</label>
                    <textarea class="form-control border border-secondary read-input" rows="3" style="resize: none;" id="address" required></textarea>
                </div>
                <button class="btn btn-success d-block mx-auto mt-5 w-25" type="submit" onclick="return handleSubmit()">Save</button>
            </div>
        </div>

    </div>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script>
        const retrieveBorrowerInfo = () => {
            const urlParams = new URLSearchParams(window.location.search);
            const id = urlParams.get('id');

            const borrowerName = document.getElementById('name');
            const borrowerAge = document.getElementById('age');
            const borrowerGender = document.getElementById('gender');
            const borrowerFineStatus = document.getElementById('fineStatus');
            const borrowerEmailAddress = document.getElementById('email');
            const borrowerPhoneNumber = document.getElementById('phoneNumber');
            const borrowerAddress = document.getElementById('address');

            $.ajax({
                url: 'EditBorrower.aspx/RetrieveBorrowerInfo',
                type: 'POST',
                data: JSON.stringify({ borrowerIdString: id }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    const parsedData = JSON.parse(data.d);
                    borrowerName.value = parsedData.name;
                    borrowerAge.value = parsedData.age;
                    borrowerGender.value = parsedData.gender;
                    borrowerFineStatus.value = parsedData.fineStatus
                    borrowerEmailAddress.value = parsedData.emailAddress;
                    borrowerPhoneNumber.value = parsedData.phoneNumber;
                    borrowerAddress.value = parsedData.address;
                },
                error: function (error) {
                    console.error("Error retrieving borrower information:", error);
                }
            });
        }

        retrieveBorrowerInfo();

        const handleSubmit = () => {
            const urlParams = new URLSearchParams(window.location.search);
            const id = urlParams.get('id');
            
            const borrowerName = document.getElementById('name').value;
            const borrowerAge = document.getElementById('age').value;
            const borrowerGender = document.getElementById('gender').value;
            const borrowerFineStatus = document.getElementById('fineStatus').value;
            const borrowerEmailAddress = document.getElementById('email').value;
            const borrowerPhoneNumber = document.getElementById('phoneNumber').value;
            const borrowerAddress = document.getElementById('address').value;


            $.ajax({
                url: 'EditBorrower.aspx/UpdateBorrowerInfo',
                type: 'POST',
                data: JSON.stringify({id: id, name: borrowerName, age: borrowerAge, gender: borrowerGender, fineStatus: borrowerFineStatus, emailAddress: borrowerEmailAddress, phoneNumber: borrowerPhoneNumber, address: borrowerAddress}),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    alert('Saved');
                    window.location.href = 'ManageBorrower.aspx';
                },
                error: function (error) {
                    console.error("Error retrieving borrower information:", error);
                }
            });
            return false;
        }
    </script>
</asp:Content>
