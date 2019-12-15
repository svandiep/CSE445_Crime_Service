<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crime.aspx.cs" Inherits="TryIt_Crime_Service.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        http://webstrar10.fulton.asu.edu/page4/Service1.svc<p>
            <strong>Crime Service Try It page</strong></p>
        <p>
            Service takes a City, and State as a String ex.(Tempe, Arizona) and returns an int 0-50 based on the level of crime, 50 being highest.</p>
        <p style="width: 43px; height: 30px">
            City:
            <asp:TextBox ID="CityBox" runat="server"></asp:TextBox>
        </p>
        <p style="width: 51px; height: 30px">
            State:<asp:TextBox ID="StateBox" runat="server"></asp:TextBox>
        </p>
        <p style="width: 69px; height: 30px">
            <asp:Button ID="CityButton" runat="server" OnClick="Button1_Click" Text="Invoke" />
        </p>
        <asp:Label ID="Label1" runat="server" Text="Crime Level: "></asp:Label>
        <asp:Label ID="CityLabel" runat="server"></asp:Label>
        <p>
            Service takes a Zip Code and returns an int 0-50 based on the level of reported crime, 50 being highest.</p>
        <p style="width: 86px">
            Zip Code:<asp:TextBox ID="ZipBox" runat="server" OnTextChanged="Page_Load"></asp:TextBox>
        </p>
        <asp:Button ID="ZipButton" runat="server" OnClick="ZipButton_Click" Text="Invoke" />
        <br />
        <br />
        Crime Level: <asp:Label ID="ZipLabel" runat="server"></asp:Label>
        <br />
        <br />
        Go to home directory
        <asp:Button ID="HomeButton" runat="server" OnClick="HomeButton_Click" Text="Home" />
    </form>
</body>
</html>
