<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style43 {
            width: 100%;
        }
        .auto-style44 {
            text-align: center;
            width:100%;
        }
     
        
    .auto-style46 {
        text-align: right;
        width: 50%;
    }
        .secclmn {
            width: 50%;

        }
        .auto-style44 {
            color: blue;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="auto-style43">
        <tr>
            <td class="auto-style44" colspan="2">
                <h3><strong>Login</strong></h3>
            </td>
        </tr>
        <tr>
            <td class="auto-style46">Username:&nbsp;</td>
            <td class="secclmn">
                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style46">Password:&nbsp;</td>
            <td class="secclmn">
                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style46">
                <asp:Button ID="btnLogin" runat="server" Font-Bold="True" ForeColor="#006600" Text="Login" Font-Size="Medium" OnClick="btnLogin_Click" />
            </td>
            <td class="secclmn">
                <asp:Button ID="btnRefresh" runat="server" Font-Bold="True" ForeColor="#006600" Text="Refresh" Font-Size="Medium" OnClick="btnRefresh_Click" />&nbsp; &nbsp;
                <asp:Label ID="lblerror" runat="server" ForeColor="#CC0000"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

