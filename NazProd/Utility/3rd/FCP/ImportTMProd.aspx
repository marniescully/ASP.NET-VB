<%@ Page Title="Import Full Case Pick 2nd Shift Productivity" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="ImportTMProd.aspx.vb" Inherits="ImportTM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">



<div id="sideNav" class="sideNav">

 <h3>
     Tasks</h3>
    <ul>
        <li><asp:HyperLink ID="backToShift" runat="server" NavigateUrl="~/FCP/2nd/Shift.aspx" onmouseover="window.status='2nd Shift Home';return true;" onmouseout ="window.status=''; return true;">2nd Shift Home</asp:HyperLink></li>
        <li><asp:HyperLink ID="EditProd" runat="server" NavigateUrl="~/FCP/2nd/EditTMProd.aspx" onmouseover="window.status='Edit Productivity';return true;" onmouseout ="window.status=''; return true;">Edit Productivity</asp:HyperLink> </li>
      <li><asp:HyperLink ID="AddProd" runat="server" NavigateUrl="~/FCP/2nd/AddTMProd.aspx" onmouseover="window.status='Add Productivity';return true;" onmouseout ="window.status=''; return true;">Add Productivity</asp:HyperLink> </li>
       </ul>
   

    <h3>Reports</h3>
        <ul>   
    <li><asp:HyperLink ID="ViewByTM" runat="server" NavigateUrl="~/FCP/2nd/ViewbyTM.aspx" onmouseover="window.status='View By Team Member';return true;" onmouseout ="window.status=''; return true;">View by Team Member</asp:HyperLink> </li>
    <li><asp:HyperLink ID="ViewByFunction" runat="server" NavigateUrl="~/FCP/2nd/ViewbyFunct.aspx" onmouseover="window.status='View By Function';return true;" onmouseout ="window.status=''; return true;">View by Function</asp:HyperLink> </li>
    <li><asp:HyperLink ID="SummaryDateRange" runat="server" NavigateUrl="~/FCP/2nd/SummDateRange.aspx" onmouseover="window.status='Summary of Date Range';return true;" onmouseout ="window.status=''; return true;">Summary of Date Range</asp:HyperLink> </li>
     <li><asp:LinkButton ID="SummbyFunct" runat="server" PostBackUrl="~/FCP/2nd/SummbyFunct.aspx" onmouseover="window.status='Summary by Function';return true;" onmouseout ="window.status=''; return true;"  CausesValidation="false">Summary by Function</asp:LinkButton> 
         
            </li>
    </ul>
       
    </div>



  <h2>Import Full Case Pick 2nd Shift Productivity</h2>
  <asp:Label ID="LastDateImport" runat="server" Text="" ></asp:Label>
       <div class="Search">
 <asp:Label ID="LblDate" runat="server" >Select Date &nbsp;</asp:Label><asp:TextBox ID="txtDate" runat="server" CssClass="Dates"> </asp:TextBox>

           <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="The Date is a Required field." ControlToValidate="txtDate" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>

           <ajaxToolkit:CalendarExtender ID="CalendarExtender" TargetControlID="txtDate" 
               runat="server"> </ajaxToolkit:CalendarExtender>
                     
                      <asp:LinkButton ID="ImportButton"  runat="server"  Visible="true" Text="Import Productivity"> </asp:LinkButton>
              <asp:LinkButton ID="ClearSearchButton" runat="server" OnClick="ClearSearchButton_OnClick" Visible="true" Text="Reset" CausesValidation="false" ></asp:LinkButton>
               
  <asp:ValidationSummary ID="ValidationSummary"  runat="server" />
</div>



    <asp:Label ID="SuccessLabel" runat="server" Text="" CssClass="Red"></asp:Label>
    
   
    
  
    </asp:Content>

