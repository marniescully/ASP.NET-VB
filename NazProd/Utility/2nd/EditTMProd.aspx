<%@ Page Title="Edit Full Case Pick Productivity" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="EditTMProd.aspx.vb" Inherits="EditTM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

   
   <div id="sideNav" class="sideNav">

 <h3>Tasks</h3>
    <ul>
        <li><asp:HyperLink ID="backToShift" runat="server" NavigateUrl="~/FCP/1st/Shift.aspx" onmouseover="window.status='1st Shift Home';return true;" onmouseout ="window.status=''; return true;">1st Shift Home</asp:HyperLink></li>
   <li><asp:LinkButton ID="ImportProd" runat="server" PostBackUrl="~/FCP/1st/ImportTMProd.aspx"  onmouseover="window.status='Import Productivity';return true;" onmouseout ="window.status=''; return true;" CausesValidation="false">Import Productivity</asp:LinkButton><ajaxToolkit:ConfirmButtonExtender ID="ImportConfirmButton" runat="server" TargetControlID="ImportProd" ConfirmText="Click OK only if you have verified the Team Members that worked on the Import Date.">         
         </ajaxToolkit:ConfirmButtonExtender>
     </li>
          <li><asp:HyperLink ID="AddProd" runat="server" NavigateUrl="~/FCP/1st/AddTMProd.aspx" onmouseover="window.status='Add Productivity';return true;" onmouseout ="window.status=''; return true;">Add Productivity</asp:HyperLink> </li>
       </ul>
   

    <h3>Reports</h3>
        <ul>   
    <li><asp:HyperLink ID="ViewByTM" runat="server" NavigateUrl="~/FCP/1st/ViewbyTM.aspx" onmouseover="window.status='View By Team Member';return true;" onmouseout ="window.status=''; return true;">View by Team Member</asp:HyperLink> </li>
    <li><asp:HyperLink ID="ViewByFunction" runat="server" NavigateUrl="~/FCP/1st/ViewbyFunct.aspx" onmouseover="window.status='View By Function';return true;" onmouseout ="window.status=''; return true;">View by Function</asp:HyperLink> </li>
    <li><asp:HyperLink ID="SummaryDateRange" runat="server" NavigateUrl="~/FCP/1st/SummDateRange.aspx" onmouseover="window.status='Summary of Date Range';return true;" onmouseout ="window.status=''; return true;">Summary of Date Range</asp:HyperLink> </li>
     <li><asp:LinkButton ID="SummbyFunct" runat="server" PostBackUrl="~/FCP/1st/SummbyFunct.aspx" onmouseover="window.status='Summary by Function';return true;" onmouseout ="window.status=''; return true;" CausesValidation="false">Summary by Function</asp:LinkButton> </li>
    </ul>
       
    </div>

<h2>Edit Full Case Pick 1st Shift Productivity</h2>

       <div class="Search">
 <asp:Label ID="LblDate" runat="server" >Select Date &nbsp;</asp:Label><asp:TextBox ID="txtDate" runat="server" Class="Dates"> </asp:TextBox>
           <asp:RequiredFieldValidator ID="DateRequiredFieldValidator" runat="server" ErrorMessage="The Date is a Required field." ControlToValidate="txtDate" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                   
           <ajaxToolkit:CalendarExtender ID="DateCalendarExtender" TargetControlID="txtDate" runat="server"> </ajaxToolkit:CalendarExtender>
                                         
                      <asp:LinkButton ID="SearchButton"  runat="server" OnClick="SearchButton_OnClick" Visible="true" Text="Edit Productivity"> </asp:LinkButton>
              <asp:LinkButton ID="ClearSearchButton" runat="server" OnClick="ClearSearchButton_OnClick" Visible="true" Text="Reset" CausesValidation="false" ></asp:LinkButton>
</div>

<div class="TMList" id="TMList">
 <asp:DetailsView ID="EditTMProd" runat="server"   
        AutoGenerateRows="False" DataSourceID="EditTMProdDataSource" 
        AllowPaging="True" DefaultMode="Edit"  DataKeyNames="DTE,EmployeeID,Function" 
             CssClass="DetailsEdit" >
        <EmptyDataTemplate>
            No data available.
        </EmptyDataTemplate>
        <Fields>
            <asp:BoundField DataField="DTE" HeaderText="Date" SortExpression="DTE" 
                ReadOnly="true" DataFormatString="{0:d}" >
           
            </asp:BoundField>
            <asp:BoundField DataField="EmployeeID" HeaderText="AS400 ID:" 
                SortExpression="Step" ReadOnly="true" >
            
            </asp:BoundField>
            <asp:BoundField DataField="FirstName" HeaderText="First Name:" 
                SortExpression="FirstName" ReadOnly="true" >
            
            </asp:BoundField>
            <asp:BoundField DataField="LastName" HeaderText="Last Name:" 
                SortExpression="LastName" ReadOnly="true" >
           
            </asp:BoundField>
            <asp:BoundField DataField="Description" HeaderText="Function:" 
                SortExpression="Description" ReadOnly="True" >
           
            </asp:BoundField>

           <asp:TemplateField HeaderText="Amount:" SortExpression="Amount">
                <EditItemTemplate>
                    <asp:TextBox ID="Amount" runat="server" Text='<%# Bind("Amount") %>' CssClass="DropDownList"></asp:TextBox>

               <asp:RequiredFieldValidator ID="AmountValidator" runat="server" ErrorMessage="Amount is a required field" ControlToValidate="Amount" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="AmountCompareValidator" runat="server" ErrorMessage="Amount must be a positive number. Type a zero before a decimal." ControlToValidate="Amount" Operator="GreaterThanEqual" ValueToCompare="0.01" Display="Dynamic"></asp:CompareValidator> 
                
                <asp:CompareValidator ID="AmountTypeValidator" runat="server" ErrorMessage=" Amount Must be a Number" ControlToValidate="Amount" Operator="DataTypeCheck" Type="Double" ></asp:CompareValidator>
                
                </EditItemTemplate>
               
               
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Function Hours:" SortExpression="FunctHours">
                <EditItemTemplate>
                    <asp:TextBox ID="FunctHours" runat="server" Text='<%# Bind("FunctHours") %>' CssClass="DropDownList"></asp:TextBox>

               <asp:RequiredFieldValidator ID="FunctHoursValidator" runat="server" ErrorMessage="Function Hours is a required field" ControlToValidate="FunctHours" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="FunctHoursAmountValidator" runat="server" ErrorMessage="Function Hours must be a positive number. Type a zero before a decimal." ControlToValidate="FunctHours" Operator="GreaterThanEqual" ValueToCompare="0.01" Display="Dynamic"></asp:CompareValidator> 
                
                <asp:CompareValidator ID="FunctHoursTypeValidator" runat="server" ErrorMessage="Must be a Number" ControlToValidate="FunctHours" Operator="DataTypeCheck" Type="Double" ></asp:CompareValidator>
                
                </EditItemTemplate>
               
               
            </asp:TemplateField>
            <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Misc. Hours:" 
                SortExpression="MiscHours">
                <EditItemTemplate>
                    <asp:TextBox ID="MiscHours" runat="server" Text='<%# Bind("MiscHours") %>' CssClass="DropDownList"></asp:TextBox><asp:RequiredFieldValidator ID="MiscHoursValidator" runat="server" ErrorMessage="Misc. Hours cannot be blank." ControlToValidate="MiscHours" Display="Dynamic">
                    </asp:RequiredFieldValidator>  <asp:CompareValidator ID="MiscHoursAmountValidator" runat="server" ErrorMessage="Misc Hours must be zero or a positive number. Type a zero before a decimal." ControlToValidate="MiscHours" Operator="GreaterThanEqual" ValueToCompare="0" Display="Dynamic"></asp:CompareValidator>
                    <asp:CompareValidator ID="MiscHoursTypeValidator" runat="server" ErrorMessage="Must be a Number" ControlToValidate="MiscHours" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </EditItemTemplate>
               
            </asp:TemplateField>
          
            
               <asp:CommandField ButtonType="Link" ShowEditButton="True" ShowCancelButton="False" ControlStyle-CssClass="Command">
            </asp:CommandField>
            

            <asp:TemplateField><ItemTemplate>
          <asp:LinkButton ID="DeleteButton" CausesValidation="false" CommandName="Delete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this item?');" >Delete </asp:LinkButton>
          </ItemTemplate></asp:TemplateField>
            
        </Fields>

        <PagerSettings FirstPageText="First" LastPageText="Last" 
            Mode="NextPrevious" NextPageText="Next" PreviousPageText="Prev"  />



    </asp:DetailsView></div>

     <asp:SqlDataSource ID="EditTMProdDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT TMs.EmployeeID, TMs.FirstName, TMs.LastName, Prod.DTE,Functions.Description, Prod.Amount, Prod.Function, Prod.FunctHours, Prod.MiscHours,  Prod.Shift
FROM TMs INNER JOIN (Functions INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function)) ON TMs.EmployeeID = Prod.EmployeeID
WHERE (((Prod.DTE)=[?]) AND ((Prod.Shift)=1) AND ((Prod.Department)=2))
ORDER BY TMS.LastName"

UpdateCommand="UPDATE PROD SET PROD.Amount=?, PROD.FunctHours = ?, PROD.MiscHours = ? WHERE PROD.DTE = ? AND PROD.EmployeeID = ? AND PROD.Function = ? AND PROD.Department=2"

DeleteCommand="DELETE FROM Prod WHERE (DTE = ?) AND (EmployeeID = ?) AND (Function = ?)">

<SelectParameters>
                <asp:ControlParameter ControlID="txtDate" DbType="Date" Name="?" 
                PropertyName="Text" />          
              
        </SelectParameters>
 <UpdateParameters>
        <asp:Parameter Name="Amount"  DbType="Decimal" />
        <asp:Parameter Name="FunctHours"  DbType="Decimal" />
        <asp:Parameter Name="MiscHours" DbType="Decimal" />
      
        <asp:Parameter Name="DTE"  DbType="DateTime" />
        <asp:Parameter Name="EmployeeID"  DbType="String" />
        <asp:Parameter Name="Function"  DbType="String" />
</UpdateParameters>

<DeleteParameters>

<asp:Parameter Name="DTE"  DbType="DateTime" />
        <asp:Parameter Name="EmployeeID"  DbType="String" />
        <asp:Parameter Name="Function"  DbType="String" />

</DeleteParameters>

</asp:SqlDataSource>

</asp:Content>

