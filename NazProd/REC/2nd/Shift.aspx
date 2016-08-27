<%@ Page Title="Receiving 2nd Shift" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Shift.aspx.vb" Inherits="REC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div id="sideNav">
 <asp:Panel ID="sideNavPanel" runat="server">
 <h3>Tasks</h3>
    <ul>
        <li><asp:LinkButton ID="TMorSumm" runat="server" Text="Verify Team Members" CausesValidation="False" OnClick="TMorSumm_click" onmouseover="window.status='View Summary or Verify Team Members';return true;" onmouseout ="window.status=''; return true;"></asp:LinkButton></li>
    <li><asp:LinkButton ID="ImportProd" runat="server"  PostBackUrl="~/REC/2nd/ImportTMProd.aspx"  onmouseover="window.status='Import Productivity';return true;" onmouseout ="window.status=''; return true;">Import Productivity</asp:LinkButton> <ajaxToolkit:ConfirmButtonExtender ID="ImportConfirmButton" runat="server" TargetControlID="ImportProd" ConfirmText="Click OK only if you have verified the Team Members that worked on the Import Date.">
         </ajaxToolkit:ConfirmButtonExtender> </li>
     <li><asp:LinkButton ID="EditProd" runat="server" PostBackUrl="~/REC/2nd/Edit.aspx" onmouseover="window.status='Edit Productivity';return true;" onmouseout ="window.status=''; return true;">Edit Productivity</asp:LinkButton> </li>
   
       </ul>
   

    <h3>Reports</h3>
        <ul>   
    <li><asp:LinkButton ID="ViewByTM" runat="server" PostBackUrl="~/REC/2nd/ViewbyTM.aspx" onmouseover="window.status='View By Team Member';return true;" onmouseout ="window.status=''; return true;">View by Team Member</asp:LinkButton> </li>
    <li><asp:LinkButton ID="ViewByFunction" runat="server" PostBackUrl="~/REC/2nd/ViewbyFunct.aspx" onmouseover="window.status='View By Function';return true;" onmouseout ="window.status=''; return true;" >View by Function</asp:LinkButton> </li>
    <li><asp:LinkButton ID="SummaryDateRange" runat="server" PostBackUrl="~/REC/2nd/SummDateRange.aspx" onmouseover="window.status='Summary of Date Range';return true;" onmouseout ="window.status=''; return true;">Summary of Date Range</asp:LinkButton> </li>
    <li><asp:LinkButton ID="SummbyFunct" runat="server" PostBackUrl="~/REC/2nd/SummbyFunct.aspx" onmouseover="window.status='Summary by Function';return true;" onmouseout ="window.status=''; return true;">Summary by Function</asp:LinkButton> </li>
    </ul>
       </asp:Panel>

       <asp:Panel ID="SideNavigation" runat="server" DefaultButton="FindTMButton" CssClass="SearchTM">
                         <h3>
                Search Team Members</h3>
                <ul>
                    <li>
                        <asp:TextBox ID="FindTM" runat="server"></asp:TextBox></li>
                    <li>
                        <asp:LinkButton ID="FindTMButton" runat="server" ToolTip="By Last Name" OnClick="FindTMButton_Click">By Last Name</asp:LinkButton>
                    </li>
                </ul>
                <ul>
                    <li>
                        <asp:TextBox ID="FindTMByID" runat="server"></asp:TextBox>
                    </li>
                    <li>
                        <asp:LinkButton ID="FindTMButtonByID" runat="server" ToolTip="By AS400 ID" OnClick="FindTMButtonByID_Click">By AS400 ID</asp:LinkButton>
                        <asp:LinkButton ID="ClearButton" runat="server" ToolTip="Reset" OnClick="ClearButton_Click">Reset</asp:LinkButton></li>
                </ul>
                <ul>
                    <li>
                        <asp:LinkButton ID="AddButton" runat="server" ToolTip="Add a New Team Member" OnClick="AddButton_Click">Add a New Team Member</asp:LinkButton>
                        </li>
                        </ul>
         
        </asp:Panel>
    </div>

<div id="LeftColumn">
<h2><asp:Label ID="DeptLabel" runat="server" Text="Receiving 2nd Shift"></asp:Label> </h2>
  <h6> <asp:Label ID="TMCountLabel" runat="server" Text="" ></asp:Label>    </h6>
<asp:Panel id="SummPanel" runat="server">
   
      <h6>Previous Day</h6>
    <asp:GridView ID="PreviousDaySumm" runat="server" 
        AutoGenerateColumns="False" DataSourceID="PrevDay" ViewStateMode="Disabled">
       <AlternatingRowStyle BackColor="#f2f2f2" />
        <Columns>
            <asp:BoundField DataField="Description" 
                SortExpression="Description" HeaderText="" >
             </asp:BoundField>
            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
            <asp:BoundField DataField="SumOfAmount" HeaderText="Amount" 
                SortExpression="SumOfAmount" />
                  <asp:BoundField DataField="SumOfTags" HeaderText="Tags" 
                SortExpression="SumOfTags" />
            <asp:BoundField DataField="SumOfFunctHours" HeaderText="Hours"  
                SortExpression="SumOfFunctHours" DataFormatString="{0:F2}" />
           
            <asp:BoundField DataField="ActRate" HeaderText="Actual Rate" 
                SortExpression="ActRate" DataFormatString="{0:F2}" HtmlEncode="False" 
                HtmlEncodeFormatString="False" />
                <asp:BoundField DataField="GoalHours" HeaderText="Goal Hours" SortExpression="GoalHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="percntGoal" HeaderText="% Goal" 
                SortExpression="percntGoal" DataFormatString="{0:P1}" HtmlEncode="False" 
                HtmlEncodeFormatString="False" />
        
        </Columns>
        <EmptyDataTemplate>
           There is no data available.
        </EmptyDataTemplate>
    </asp:GridView>
  
   <h6>Previous Week</h6>
<asp:GridView ID="PreviousWeekSumm" runat="server" 
        AutoGenerateColumns="False" DataSourceID="PrevWeek" ViewStateMode="Disabled">
       <AlternatingRowStyle BackColor="#f2f2f2" />
        <Columns>

            <asp:BoundField DataField="Description" 
                SortExpression="Description" HeaderText="" >
                    
            </asp:BoundField>
            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
            <asp:BoundField DataField="SumOfSumOfAmount" HeaderText="Amount" 
                SortExpression="SumOfSumOfAmount" />
                   <asp:BoundField DataField="SumOfSumOfTags" HeaderText="Tags" 
                SortExpression="SumOfSumOfTags" />
            <asp:BoundField DataField="SumOfSumOfFunctHours" HeaderText="Hours"  
                SortExpression="SumOfSumOfFunctHours" DataFormatString="{0:F2}" />
           
            <asp:BoundField DataField="ActRate" HeaderText="Actual Rate" 
                SortExpression="ActRate" DataFormatString="{0:F2}" />
                <asp:BoundField DataField="GoalHours" HeaderText="Goal Hours" SortExpression="GoalHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="percntGoal" HeaderText="% Goal" 
                SortExpression="percntGoal" DataFormatString="{0:P1}" />

        </Columns>
        <EmptyDataTemplate>
           There is no data available.
        </EmptyDataTemplate>
    </asp:GridView>
   
   <h6>Previous Month</h6>
<asp:GridView ID="PreviousMonthSumm" runat="server" 
        AutoGenerateColumns="False" DataSourceID="PrevMonth" ViewStateMode="Disabled">
       <AlternatingRowStyle BackColor="#f2f2f2" />
        <Columns>
                <asp:BoundField DataField="Description" 
                SortExpression="Description" HeaderText="" >
                     
            </asp:BoundField>
            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
            <asp:BoundField DataField="SumOfSumOfAmount" HeaderText="Amount" 
                SortExpression="SumOfSumOfAmount" />
                 <asp:BoundField DataField="SumOfSumOfTags" HeaderText="Tags" 
                SortExpression="SumOfSumOfTags" />
            <asp:BoundField DataField="SumOfSumOfFunctHours" HeaderText="Hours"  
                SortExpression="SumOfSumOfFunctHours" DataFormatString="{0:F2}" />
           
            <asp:BoundField DataField="ActRate" HeaderText="Actual Rate" 
                SortExpression="ActRate" DataFormatString="{0:F2}" />
                <asp:BoundField DataField="GoalHours" HeaderText="Goal Hours" SortExpression="GoalHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="percntGoal" HeaderText="% Goal" 
                SortExpression="percntGoal" DataFormatString="{0:P1}" />
        
        </Columns>
        <EmptyDataTemplate>
           There is no data available.
        </EmptyDataTemplate>
    </asp:GridView>
   
   </asp:Panel>
           
       
  <asp:GridView ID="ShiftTM" runat="server"  AllowSorting="True" 
           AutoGenerateColumns="False" DataKeyNames="EmployeeID" ViewStateMode="Disabled"
           DataSourceID="TMDataSource"  > 
           
          <AlternatingRowStyle BackColor="#f2f2f2" />
           
           <Columns>
           
           <asp:BoundField DataField="EmployeeID" HeaderText="AS400 ID" ReadOnly="True" 
               SortExpression="EmployeeID" />
           <asp:BoundField DataField="FirstName" HeaderText="First Name" ReadOnly="True"
               SortExpression="FirstName" >
             
               </asp:BoundField>
           <asp:BoundField DataField="LastName" HeaderText="Last Name" ReadOnly="True"
               SortExpression="LastName" >
            
               </asp:BoundField>
           
          
           <asp:CheckBoxField DataField="FarmIn" HeaderText="Farm In" 
                    SortExpression="FarmIn" >
              
                </asp:CheckBoxField>
      <asp:CommandField ShowSelectButton="True" SelectText="Edit" />
           
       </Columns>
           <EmptyDataTemplate>
             There are no Current Team Members for this Department and Shift.
           </EmptyDataTemplate>
       </asp:GridView>
     
    </div>

    <div id="Form" class="SmallForm">
        <asp:DetailsView ID="EditTM" runat="server" DataSourceID="EditTMDataSource" Visible="False"
            DefaultMode="Edit" AutoGenerateRows="False" OnItemCommand="EditTM_ItemCommand"
            OnItemUpdated="EditTM_ItemUpdated" DataKeyNames="EmployeeID">
            <EmptyDataTemplate>
                No Team Member selected
            </EmptyDataTemplate>
            <Fields>
                <asp:TemplateField HeaderText="AS400 ID:">
                    <EditItemTemplate>
                        <asp:TextBox ID="AS400ID" runat="server" Text='<%# Bind("EmployeeID") %>' Enabled="false"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="First Name:">
                    <EditItemTemplate>
                        <asp:TextBox ID="FirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:TextBox><asp:RequiredFieldValidator
                            ID="FirstNameRequiredFieldValidator" runat="server" ErrorMessage="A First Name is required."
                            ControlToValidate="FirstName" Display="Dynamic"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name:">
                    <EditItemTemplate>
                        <asp:TextBox ID="LastName" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox><asp:RequiredFieldValidator
                            ID="LastNameRequiredFieldValidator" runat="server" ErrorMessage="A Last Name is required."
                            ControlToValidate="LastName" Display="Dynamic"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department:">
                    <EditItemTemplate>
                        <asp:DropDownList ID="EditDeptDDl" runat="server" DataSourceID="DeptDataSource" DataTextField="Dept"
                            DataValueField="DepartmentID" SelectedValue='<%# Bind("DepartmentID") %>'>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Shift:">
                    <EditItemTemplate>
                        <asp:DropDownList ID="EditShiftDDL" runat="server" DataSourceID="ShiftDataSource"
                            DataTextField="Shift" DataValueField="ShiftID" SelectedValue='<%# Bind("ShiftID") %>'>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField HeaderText="Farm-In:">
                    <EditItemTemplate>
                      
                       
                        <asp:CheckBox ID="FarmIn" runat="server" Checked='<%# Bind("FarmIn") %>' />
                      
                       
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ShowEditButton="True" />
                <asp:ButtonField CommandName="DeleteThis" Text="Delete" />
            </Fields>
        </asp:DetailsView>
        <asp:DetailsView ID="AddTM" runat="server" AutoGenerateRows="False" DefaultMode="Insert"
            CssClass="Details" Visible="False" OnItemInserting="AddTM_ItemInserting" OnModeChanging="AddTM_ModeChanging"
            DataKeyNames="EmployeeID">
            <Fields>
                <asp:TemplateField HeaderText="AS400 ID:" SortExpression="EmployeeID">
                    <InsertItemTemplate>
                        <asp:TextBox ID="DetEmployeeID" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ID="EmployeeIDValidator" runat="server" ErrorMessage=" An AS400 ID is Required"
                            Display="Dynamic" ControlToValidate="DetEmployeeID" ForeColor="Red"></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="First Name:" SortExpression="FirstName">
                    <InsertItemTemplate>
                        <asp:TextBox ID="DetFirstName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="FirstNameValidator" runat="server" ErrorMessage="A First Name is Required."
                            ForeColor="Red" Display="Dynamic" ControlToValidate="DetFirstName"></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name:" SortExpression="LastName">
                    <InsertItemTemplate>
                        <asp:TextBox ID="DetLastName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="LastNameValidator" runat="server" ErrorMessage="A Last Name is Required"
                            ForeColor="Red" Display="Dynamic" ControlToValidate="DetLastName"></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department:" SortExpression="Dept">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="DeptDownList" runat="server" DataSourceID="DeptDataSource"
                            DataTextField="Dept" DataValueField="DepartmentID" SelectedValue='<%# Bind("DepartmentID") %>'>
                        </asp:DropDownList>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Shift:" SortExpression="Shift">
                    <InsertItemTemplate>
                        <asp:DropDownList ID="ShiftDropDownList" runat="server" DataSourceID="ShiftDataSource"
                            DataTextField="Shift" DataValueField="ShiftID" SelectedValue='<%# Bind("ShiftID") %>'>
                        </asp:DropDownList>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField CommandName="Insert" Text="Insert" CausesValidation="True" />
                <asp:ButtonField CommandName="Cancel" Text="Cancel" />
            </Fields>
        </asp:DetailsView>
    </div>
 
     <asp:SqlDataSource ID="TMDataSource" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
           ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"
          SelectCommand="SELECT TMs.EmployeeID, TMs.ShiftID, TMs.FirstName, TMs.DepartmentID, TMs.LastName, TMs.FarmIn,  Departments.Dept, Shifts.Shift FROM (Shifts INNER JOIN (Departments INNER JOIN TMs ON Departments.DepartmentID = TMs.DepartmentID) ON Shifts.ShiftID = TMs.ShiftID) WHERE (TMs.ShiftID = 2) AND (TMs.DepartmentID = 1) ORDER BY TMs.EmployeeID" 
          UpdateCommand="UPDATE TMs SET DepartmentID=?, ShiftID=?, FarmIn=? WHERE EmployeeID=?" >
                                             
      <UpdateParameters>
      <asp:Parameter Name="EmployeeID"  DbType="String" />
      <asp:Parameter Name="DepartmentID" DbType="Int32" />
       <asp:Parameter Name="ShiftID" DbType="Int32" />
       <asp:Parameter Name="FarmIn" DbType="Boolean" />
      </UpdateParameters>
      
 </asp:SqlDataSource>

          <asp:SqlDataSource ID="DeptDataSource" runat="server" 
              ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
              ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
              SelectCommand="SELECT DepartmentID, Dept FROM Departments ">   </asp:SqlDataSource>

          <asp:SqlDataSource ID="ShiftDataSource" runat="server" 
              ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
              ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
              SelectCommand="SELECT ShiftID,Shift FROM Shifts"></asp:SqlDataSource>             

          <asp:SqlDataSource ID="PrevDay" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT Function, Description, Rate, SumOfAmount, SumOfTags, SumOfFunctHours,  SumOfAmount / SumOfFunctHours AS ActRate,SumOfAmount/Rate AS GoalHours, (SumOfAmount/Rate)/ SumOfFunctHours AS percntGoal FROM SummaryReceiving2ndDay"></asp:SqlDataSource>
         
          <asp:SqlDataSource ID="PrevWeek" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT Function, Rate, Description, SumOfSumOfAmount, SumOfSumOfTags, SumOfSumOfFunctHours, [SumOfSumOfAmount]/[SumOfSumOfFunctHours] AS ActRate,  SumOfSumOfAmount/Rate AS GoalHours, (SumOfSumOfAmount/Rate)/ SumOfSumOfFunctHours AS percntGoal FROM SummOfSummReceiving2ndWeek
"></asp:SqlDataSource>

          <asp:SqlDataSource ID="PrevMonth" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT Function, Description, Rate, SumOfSumOfAmount, SumOfSumOfTags, SumOfSumOfFunctHours, SumOfSumOfAmount / SumOfSumOfFunctHours AS ActRate, SumOfSumOfAmount/Rate AS GoalHours, (SumOfSumOfAmount/Rate)/ SumOfSumOfFunctHours AS percntGoal FROM SummOfSummReceiving2ndMonth">
</asp:SqlDataSource>



    <asp:SqlDataSource ID="FindTMDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT TMs.EmployeeID, TMs.FirstName, TMs.LastName, Departments.Dept, Shifts.Shift, TMs.DepartmentID,TMs.FarmIn, TMs.ShiftID FROM (Departments INNER JOIN (Shifts INNER JOIN TMs ON Shifts.ShiftID = TMs.ShiftID) ON Departments.DepartmentID = TMs.DepartmentID) WHERE (TMs.LastName = ?)">
        <SelectParameters>
            <asp:ControlParameter ControlID="FindTM" Name="?" PropertyName="Text" DbType="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="FindTMDataSourceByID" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT TMs.EmployeeID, TMs.FirstName, TMs.LastName, Departments.Dept, TMs.FarmIn, Shifts.Shift, TMs.DepartmentID, TMs.ShiftID FROM (Departments INNER JOIN (Shifts INNER JOIN TMs ON Shifts.ShiftID = TMs.ShiftID) ON Departments.DepartmentID = TMs.DepartmentID) WHERE (TMs.EmployeeID = ?)">
        <SelectParameters>
            <asp:ControlParameter ControlID="FindTMByID" Name="?" PropertyName="Text" DbType="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="EditTMDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT Departments.Dept, Shifts.Shift, TMs.EmployeeID, TMs.DepartmentID, TMs.ShiftID,  TMs.FirstName, TMs.LastName, TMs.FarmIn FROM ((Departments INNER JOIN TMs ON Departments.DepartmentID = TMs.DepartmentID) INNER JOIN Shifts ON TMs.ShiftID = Shifts.ShiftID) WHERE (TMs.EmployeeID = ?)"
        UpdateCommand="UPDATE TMs SET FirstName =?, LastName =?, DepartmentID =?, ShiftID =?, FarmIn=? WHERE EmployeeID = ?">
        <SelectParameters>
            <asp:ControlParameter ControlID="ShiftTM" Name="?" PropertyName="SelectedValue" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="DepartmentID" Type="Int32" />
            <asp:Parameter Name="ShiftID" Type="Int32" />
                   <asp:Parameter Name="FarmIn" DbType="Boolean" />
            <asp:Parameter Name="EmployeeID" Type="String" />
             
        </UpdateParameters>
         </asp:SqlDataSource>


</asp:Content>

