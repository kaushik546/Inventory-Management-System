<%@ Page Title="Sales" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Sales.aspx.cs" Inherits="Sales" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style type="text/css">
    .auto-style41 {
        text-align: center;
    }
    .auto-style42 {
        width: 100%;
    }
    .auto-style44 {
        text-align: left;
    }
        .auto-style51 {
            text-align: center;
        }
        .auto-style52 {
            color: #003399;
            font-weight: normal;
        }
           .auto-style54 {
            text-align: left;
            text-decoration: none;
        }
        .auto-style55 {
            color: #003399;
            text-align: center;
        }
        .auto-style56 {
            width: 100%;
            height: 140px;
        }
        .auto-style59 {
            width: 65%;
        }
         .auto-style77 {
            width: 5%;
        }
           .auto-style79 {
            width: 5%;
        }
          .auto-style78 {
            height: 30px;
        }
        
        .auto-style60 {
            width: 173px;
            text-align: center;
        }
        .auto-style65 {
            width: 10%;
            height: 30px;
        }
        .auto-style68 {
            width: 173px;
            color: #0033CC;
            height: 26px;
            text-align: right;
        }
        .auto-style69 {
            width: 28px;
            height: 30px;
        }
        .auto-style71 {
            color: #0033CC;
            text-align: center;
        }
        .auto-style72 {
            width: 65%;
            height: 26px;
            text-align: center;
            color: #003399;
        }
        .auto-style73 {
            width: 15%;
            color: #0033CC;
            text-align: right;
        }
        .auto-style74 {
            color: #0033CC;
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
   <div class="auto-style54">
    <h3 class="auto-style55">Sales</h3>
       <div class="auto-style44">
           
           <table class="auto-style56">
               <tr>
                   <td class="auto-style78">&nbsp;</td>
                   <td class="auto-style65">
                       &nbsp;</td>
                   <td class="auto-style65">
                       &nbsp;</td>
                   <td class="auto-style72" colspan="2"><strong>Search Result</strong></td>
                   <td class="auto-style79">
                       &nbsp;</td>
               </tr>
               <tr>
                   <td class="auto-style74">
                       Search Product Name:</td>
                   <td class="auto-style71">
                <asp:TextBox ID="TextBox2" Width="128px" runat="server" OnTextChanged="TextBox2_TextChanged" AutoPostBack="True"></asp:TextBox>
                       <ajaxToolkit:AutoCompleteExtender ID="TextBox2_AutoCompleteExtender" runat="server" MinimumPrefixLength="1" ServiceMethod="GetSearch" TargetControlID="TextBox2">
                       </ajaxToolkit:AutoCompleteExtender>
                   </td>
                   <td class="auto-style77" rowspan="7">&nbsp;
                        
                   </td>
                   <td class="auto-style59" rowspan="7" style="border:thin solid #003300">
            <asp:GridView ID="searchGrid" runat="server" AutoGenerateColumns="False" AutoPostBack="true" HorizontalAlign="Center" OnSelectedIndexChanged="searchGrid_SelectedIndexChanged">
                <columns>
                    <asp:BoundField Datafield="ProductName" HeaderText="Product Name" />
                    <asp:BoundField Datafield="CompanyName" HeaderText="Company Name" />
                    <asp:BoundField Datafield="total" HeaderText="Available Quantity" />
                    <asp:TemplateField>
                      
                    </asp:TemplateField>
                   
                    <asp:ButtonField CommandName="Select" Text="select" />
                   
                </columns>
            </asp:GridView>
                   </td>
                   <td class="auto-style79" rowspan="7">
                       &nbsp;</td>
               </tr>
               <tr>
                   <td class="auto-style73">Company Name :</td>
                   <td class="auto-style65">
                <asp:TextBox ID="TextBox3" Width="128px" runat="server" AutoPostBack="True"></asp:TextBox>
                   </td>
               </tr>
               <tr>
                   <td class="auto-style68">Available Quantity (Packet)</td>
                   <td class="auto-style69">
                <asp:TextBox ID="txtQuantity1" runat="server" Width="128px" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                   </td>
               </tr>
               <tr>
                   <td class="auto-style73">Sell Quantity (Packet) :</td>
                   <td class="auto-style65">
                <asp:TextBox ID="txtQuantity2" runat="server" Width="128px" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                   </td>
               </tr>
               <tr>
                   <td class="auto-style51" colspan="2">
                <asp:Button ID="btnsell" runat="server" Text="Sell" Font-Bold="True" Width="55px" OnClick="btnsell_Click1" />
                <asp:Button ID="btnclear" runat="server" Text="Clear" Font-Bold="True" OnClick="btnclear_Click" Width="56px" />
                   </td>
               </tr>
               <tr>
                   <td class="auto-style60" colspan="2" rowspan="2">
                <asp:Label ID="lblsuccessmassage" runat="server" Text="" ForeColor="Green"></asp:Label>
                       <br />
                <asp:Label ID="lblerrormessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                   </td>
                  
               </tr>
              
           </table>
       </div>
    <table class="auto-style42">
        <tr>
            <td class="auto-style44">
                &nbsp;</td>
        </tr>
    </table>
        <h4 class="auto-style51">
            <br />
            <span class="auto-style52"><strong>Sales List</strong></span><br />
        </h4>
       <p class="auto-style51">
            
        <asp:GridView ID="SalesGrid" runat="server" AutoGenerateColumns="False" AutoPostBack="true" HorizontalAlign="Center" OnSelectedIndexChanged="searchGrid_SelectedIndexChanged">
                <columns>
                    <asp:BoundField Datafield="SalesProductName" HeaderText="Product Name" />
                    <asp:BoundField Datafield="SalesCompanyName" HeaderText="Company Name" />
                    <asp:BoundField Datafield="SalesQuantity" HeaderText="Quantity (Packet)" />
                                       
                   <%-- <asp:ButtonField CommandName="Select" Text="select" />--%>
                   
                </columns>
            </asp:GridView>
           </p>
        <div class="auto-style51">
            <br />
        </div>
</div>
</asp:Content>

