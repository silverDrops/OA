<%@ Control Language="C#" AutoEventWireup="true" CodeFile="combox.ascx.cs" Inherits="combox" %>
<style type="text/css">
.cls{
 POSITION:absolute; 
}
</style>
<div class="cls" style="WIDTH: 85px;z-index:100" align="center">
   <asp:TextBox ID="TextBox1" runat="server" Width="79px" Height="20px" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
</div>
<asp:DropDownList ID="DropDownList1" runat ="server"  Width="100px" 
    Height="24px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
    AutoPostBack="True"></asp:DropDownList>
