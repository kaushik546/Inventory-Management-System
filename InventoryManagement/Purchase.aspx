<%@ Page Title="Purchase" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Purchase.aspx.cs" Inherits="Store" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <style type="text/css">
    .auto-style41 {
        text-align: center;
    }
    .auto-style42 {
        width: 100%;
    }
    .auto-style43 {
            text-align: right;
            width: 608px;
        }
    .auto-style44 {
        text-align: left;
    }
        .auto-style46 {
        text-align: right;
        width: 571px;
    }
    .auto-style49 {
        text-align: center;
        width: 11px;
    }
    .auto-style50 {
        text-align: center;
        width: 73px;
    }
        .auto-style51 {
            text-align: center;
        }
        .auto-style52 {
            color: #003399;
        }
           .auto-style53 {
               margin-left: 0px;
           }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="auto-style41">
    <h3 class="auto-style52">Purchase</h3>
        <asp:HiddenField ID="hfPurchaseId" runat="server" />
    <table class="auto-style42">
        <tr>
            <td class="auto-style43" colspan="2">Product Name:</td>
            <td class="auto-style44" colspan="2">
                <asp:DropDownList ID="DropDownProduct" runat="server" Height="26px" Width="128px" AppendDataBoundItems="True"  OnSelectedIndexChanged="DropDownProduct_SelectedIndexChanged">
                    
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style43" colspan="2">Supplier Name :</td>
            <td class="auto-style44" colspan="2">
                <asp:DropDownList ID="DropDownSupplier" runat="server" Height="26px" Width="128px" AppendDataBoundItems="True" OnSelectedIndexChanged="DropDownSupplier_SelectedIndexChanged">
                    
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style43" colspan="2">Quantity (Packet):</td>
            <td class="auto-style44" colspan="2">
                <asp:TextBox ID="txtQuantity" runat="server" Width="128px" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style43" colspan="2">Others :</td>
            <td class="auto-style44" colspan="2">
                <asp:TextBox ID="txtOthers" runat="server" TextMode="MultiLine" MaxLength="99" Width="128px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style46">
                <asp:Button ID="btnsave" runat="server" Text="Save" Font-Bold="True" OnClick="btnsave_Click" Width="55px" />
            </td>
            <td class="auto-style49">
                <asp:Button ID="btndelete" runat="server" Text="Delete" Width="56px" Font-Bold="True" OnClick="btndelete_Click" CssClass="auto-style53" />
            </td>
            <td class="auto-style50">
                <asp:Button ID="btnclear" runat="server" Text="Clear" Font-Bold="True" OnClick="btnclear_Click" Width="56px" />
            </td>
            <td class="auto-style44">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblsuccessmassage" runat="server" Text="" ForeColor="Green"></asp:Label>
                <asp:Label ID="lblerrormessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
        <h4>
            <br />
            <span class="auto-style52">Purchase List</span><br />
        <br />
        </h4>
        <div class="auto-style51">
        <asp:GridView ID="purchaseGrid" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center" OnSelectedIndexChanged="purchaseGrid_SelectedIndexChanged">
            <columns>
                <asp:BoundField Datafield="PurchaseId" HeaderText="Purchase Id" />
                <asp:BoundField Datafield="ProductName" HeaderText="Product Name" />
                <asp:BoundField Datafield="CompanyName" HeaderText="Supplier Name" />
                <asp:BoundField Datafield="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="Others" HeaderText="Others" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval("PurchaseId") %>' OnClick="lnk_onClick">Select</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </columns>
        </asp:GridView>
            <br />
        </div>
</div>
</asp:Content>

