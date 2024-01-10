<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Net_project.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login for Library Book Management System</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
</head>
<body>
   <section class="vh-100">
  <div class="container-fluid">
    <div class="row">
      <div class="col-sm-6 text-black">
        <div class="px-5 ms-xl-4 text-center">
          <i class="fas fa-crow fa-2x me-3 pt-5 mt-xl-4" style="color: #709085;"></i>
          <span class="h1 fw-bold mb-0">Library Book Management System</span>
        </div>
        <div class="d-flex align-items-center h-custom-2 px-5 ms-xl-4 mt-5 pt-5 pt-xl-0 mt-xl-n5 border border-secondary p-2 mb-2 border-opacity-55 bg-secondary bg-opacity-10 rounded">
          <form style="width: 23rem;" runat="server">  
            <h3 class="fw-normal mb-3 pb-3" style="letter-spacing: 1px;">Log in</h3>
            <div class="form-outline mb-4">
              <label class="form-label" for="form2Example18">Librarian ID</label>
              <input type="text" id="LibrarianID" runat="server" class="form-control form-control-lg" />
            </div>

            <div class="form-outline mb-4">
              <label class="form-label" for="form2Example28">Password</label>
              <input type="password" id="password" runat="server" class="form-control form-control-lg" />
            </div>

            <div class="pt-1 mb-4">
                <asp:Button class="btn btn-info btn-lg btn-block" ID="submit" runat="server" OnClick="UserLogin" text="Login"/>
            </div>

            <p class="small mb-5 pb-lg-2"><a class="text-muted" href="#!">Forgot password?</a></p>
            <p>Don't have an account? <a href="Register.aspx" class="link-info">Register here</a></p>

          </form>
        </div>
      </div>
      <div class="col-sm-6 px-0 d-none d-sm-block">
        <img src="https://www.uthm.edu.my/templates/yootheme/cache/c3/libbbangunan-c37a9bf5.webp"
        alt="Login image" id="pic" class="w-30 vh-100" style="object-fit: cover; object-position: left; position:fixed;">
    </div>
    </div>
  </div>
</section>
</body>
</html>
