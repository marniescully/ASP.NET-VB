<%@ Page Title="Add Full Case Pick Productivity" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="AddTMProd.aspx.vb" Inherits="AddTM" %>

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
     <li><asp:HyperLink ID="EditProd" runat="server" NavigateUrl="~/FCP/1st/EditTMProd.aspx" onmouseover="window.status='Edit Productivity';return true;" onmouseout ="window.status=''; return true;">Edit Productivity</asp:HyperLink> </li>
      
       </ul>
   

    <h3>Reports</h3>
        <ul>   
    <li><asp:HyperLink ID="ViewByTM" runat="server" NavigateUrl="~/FCP/1st/ViewbyTM.aspx" onmouseover="window.status='View By Team Member';return true;" onmouseout ="window.status=''; return true;">View by Team Member</asp:HyperLink> </li>
    <li><asp:HyperLink ID="ViewByFunction" runat="server" NavigateUrl="~/FCP/1st/ViewbyFunct.aspx" onmouseover="window.status='View By Function';return true;" onmouseout ="window.status=''; return true;">View by Function</asp:HyperLink> </li>
    <li><asp:HyperLink ID="SummaryDateRange" runat="server" NavigateUrl="~/FCP/1st/SummDateRange.aspx" onmouseover="window.status='Summary of Date Range';return true;" onmouseout ="window.status=''; return true;">Summary of Date Range</asp:HyperLink> </li>
     <li><asp:LinkButton ID="SummbyFunct" runat="server" PostBackUrl="~/FCP/1st/SummbyFunct.aspx" onmouseover="window.status='Summary by Function';return true;" onmouseout ="window.status=''; return true;" CausesValidation="false">Summary by Function</asp:LinkButton> </li>
    </ul>
       
    </div>

<h2><asp:Label ID="AddLabel" runat="server" Text="Add Full Case Pick 1st Shift Productivity"></asp:Label></h2>
  
    

<div class="TMList" id="TMList">

 <asp:DetailsView ID="AddTMProd" runat="server" AutoGenerateRows="False"  CssClass="DetailsAdd" DefaultMode="Insert"  DataKeyNames="DTE,EmployeeID,Function" OnItemcommand="AddTMProd_ItemCommand" OnModeChanging="AddTMProd_ModeChanging" ViewStateMode="Disabled" >
            <Fields>
                           
                <asp:TemplateField HeaderText="Date:">
                    
                    <InsertItemTemplate>
                        <asp:TextBox ID="DTE" runat="server"  CssClass="DetailsAddDropDownList" ></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="DTE_CalendarExtender" runat="server" 
                            TargetControlID="DTE">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="DteValidator" runat="server" ErrorMessage="Date is a required field" ControlToValidate="DTE" Display="Dynamic"></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Team Member:">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="EmployeeID" runat="server" CssClass="DetailsAddDropDownList" 
                            DataSourceID="TMListDataSource" DataTextField="FullName" 
                            DataValueField="EmployeeID">
                        </asp:DropDownList> <asp:RequiredFieldValidator ID="EmployeeIDValidator" runat="server" ErrorMessage="EmployeeID is a required field" ControlToValidate="EmployeeID" Display="Dynamic"></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                    
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Function:" SortExpression="Function">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="Function" runat="server" CssClass="DetailsAddDropDownList" 
                            DataSourceID="FunctionListDataSource" DataTextField="Description" 
                            DataValueField="Function">
                        </asp:DropDownList><asp:RequiredFieldValidator ID="FunctionValidator" runat="server" ErrorMessage="Function is a required field" ControlToValidate="Function" Display="Dynamic"></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount:" SortExpression="Amount" 
                    ConvertEmptyStringToNull="False">
                   
                    <InsertItemTemplate>
                        <asp:TextBox ID="Amount" runat="server"  CssClass="DetailsAddDropDownList" ViewStateMode="Disabled"></asp:TextBox>
                        <asp:CompareValidator ID="AmountValidator" runat="server" ErrorMessage="Amount must be greater than zero" ControlToValidate="Amount" Operator="GreaterThan" ValueToCompare="0" Display="Dynamic"></asp:CompareValidator><asp:RequiredFieldValidator ID="AmountReqValidator" runat="server" ErrorMessage="Amount is a required field" ControlToValidate="Amount" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="AmountTypeValidator" runat="server" ErrorMessage="Must be a Whole Number" ControlToValidate="Amount" Operator="DataTypeCheck" Type="Integer" ></asp:CompareValidator>
                       
                    </InsertItemTemplate>
                   
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Function Hours:" SortExpression="FunctHours">
                    
                    <InsertItemTemplate>
                        <asp:TextBox ID="FunctHours" runat="server"  CssClass="DetailsAddDropDownList" ></asp:TextBox>
                       <asp:RequiredFieldValidator ID="FunctHoursValidator" runat="server" ErrorMessage="Function Hours is a required field" ControlToValidate="FunctHours" Display="Dynamic"></asp:RequiredFieldValidator>
                                              
                        <asp:CompareValidator ID="FunctHoursAmountValidator" runat="server" ErrorMessage="Must be a positive number. Type zero before a decimal." ControlToValidate="FunctHours" Operator="GreaterThan" ValueToCompare="0" Display="Dynamic"></asp:CompareValidator> 

                        <asp:CompareValidator ID="FunctHoursTypeValidator" runat="server" ErrorMessage="Must be a Number" ControlToValidate="FunctHours" Operator="DataTypeCheck" Type="Double" ></asp:CompareValidator>
                   
                    </InsertItemTemplate>
                   
                </asp:TemplateField>
                <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Misc. Hours:" 
                    SortExpression="MiscHours">
                    
                    <InsertItemTemplate>
                        <asp:TextBox ID="MiscHours" runat="server"  CssClass="DetailsAddDropDownList"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="MiscHoursValidator" runat="server" ErrorMessage="Cannot be blank. Must contain at least a zero" ControlToValidate="MiscHours" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                      <asp:CompareValidator ID="MiscHoursAmountValidator" runat="server" ErrorMessage="Must be zero or a positive number. Type zero before a decimal." ControlToValidate="MiscHours" Operator="GreaterThanEqual" ValueToCompare="0" Display="Dynamic"></asp:CompareValidator>
                      <asp:CompareValidator ID="MiscHoursTypeValidator" runat="server" ErrorMessage="Must be a Number" ControlToValidate="MiscHours" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                    </InsertItemTemplate>
                   
                </asp:TemplateField>

               
           <asp:ButtonField Text="Add" CommandName="Add" CausesValidation="true" />
             <asp:ButtonField Text="Reset" CommandName="Reset" CausesValidation="false" />
              <asp:ButtonField Text="Cancel" CommandName="Cancel" CausesValidation="false" />
        </Fields>

        

    </asp:DetailsView>
   
    </div>
    

    
   

    <asp:SqlDataSource ID="FunctionListDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT [Function], [Description] FROM [Functions] WHERE Department=2 ORDER BY Description"></asp:SqlDataSource>

    <asp:SqlDataSource ID="TMListDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT EmployeeID, (FirstName + ' ' + LastName) AS FullName FROM TMs WHERE (DepartmentID = 2) AND (ShiftID = 1) ORDER BY TMs.LastName"></asp:SqlDataSource>
       
        
</asp:Content>



