<%@ Page Title="Full Case Pick 1st Shift" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Shift.aspx.vb" Inherits="FCP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 
<div class="TMList" id="TMList">
<h2><asp:Label ID="DeptLabel" runat="server" Text="Full Case Pick 1st Shift"></asp:Label>
  <asp:Label ID="TMCountLabel" runat="server" Text="" CssClass="TMsNum"></asp:Label>   </h2>

<asp:Panel class="Summ" id="Summ" runat="server">
   <div id="Day" class="Shift">
      <h6>Previous Day</h6>
    <asp:GridView ID="PreviousDaySumm" runat="server" 
        AutoGenerateColumns="False" DataSourceID="FirstPrevDay" 
           ViewStateMode="Disabled">
        <AlternatingRowStyle BackColor="#f2f2f2" />
        <Columns>
            <asp:BoundField DataField="Description" 
                SortExpression="Description" HeaderText="" >
             </asp:BoundField>
            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
            <asp:BoundField DataField="SumOfAmount" HeaderText="Amount" 
                SortExpression="SumOfAmount" />
            <asp:BoundField DataField="SumOfFunctHours" HeaderText="Hours"  
                SortExpression="SumOfFunctHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="SumOfMiscHours" HeaderText="Misc Hours" 
                SortExpression="SumOfMiscHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="ActRate" HeaderText="Actual Rate" 
                SortExpression="ActRate" DataFormatString="{0:F2}" HtmlEncode="False" 
                HtmlEncodeFormatString="False" />
                <asp:BoundField DataField="GoalHours" HeaderText="Goal Hours" SortExpression="GoalHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="percntGoal" HeaderText="% Goal" 
                SortExpression="percntGoal" DataFormatString="{0:P1}" HtmlEncode="False" 
                HtmlEncodeFormatString="False" />
            <asp:BoundField DataField="TotalHours" HeaderText="Total Hours" 
                SortExpression="TotalHours" DataFormatString="{0:F2}" />
        </Columns>
        <EmptyDataTemplate>
           There is no data available.
        </EmptyDataTemplate>
    </asp:GridView>
  </div>
    <div id="Week" class="Shift">
   <h6>Previous Week</h6>
<asp:GridView ID="PreviousWeekSumm" runat="server" 
        AutoGenerateColumns="False" DataSourceID="FirstPrevWeek" 
            ViewStateMode="Disabled">
        <AlternatingRowStyle BackColor="#f2f2f2" />
        <Columns>

            <asp:BoundField DataField="Description" 
                SortExpression="Description" HeaderText="" >
                    
            </asp:BoundField>
            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
            <asp:BoundField DataField="SumOfSumOfAmount" HeaderText="Amount" 
                SortExpression="SumOfSumOfAmount" />
            <asp:BoundField DataField="SumOfSumOfFunctHours" HeaderText="Hours"  
                SortExpression="SumOfSumOfFunctHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="SumOfSumOfMiscHours" HeaderText="Misc Hours" 
                SortExpression="SumOfSumOfMiscHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="ActRate" HeaderText="Actual Rate" 
                SortExpression="ActRate" DataFormatString="{0:F2}" />
                <asp:BoundField DataField="GoalHours" HeaderText="Goal Hours" SortExpression="GoalHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="percntGoal" HeaderText="% Goal" 
                SortExpression="percntGoal" DataFormatString="{0:P1}" />
            <asp:BoundField DataField="TotalHours" HeaderText="Total Hours" 
                SortExpression="TotalHours" DataFormatString="{0:F2}" />
        </Columns>
        <EmptyDataTemplate>
           There is no data available.
        </EmptyDataTemplate>
    </asp:GridView>
    </div>
    <div id="Month" class="Shift">
   <h6>Previous Month</h6>
<asp:GridView ID="PreviousMonthSumm" runat="server" 
        AutoGenerateColumns="False" DataSourceID="FirstPrevMonth" 
            ViewStateMode="Disabled">
        <AlternatingRowStyle BackColor="#f2f2f2" />
        <Columns>
                <asp:BoundField DataField="Description" 
                SortExpression="Description" HeaderText="" >
                     
            </asp:BoundField>
            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
            <asp:BoundField DataField="SumOfSumOfAmount" HeaderText="Amount" 
                SortExpression="SumOfSumOfAmount" />
            <asp:BoundField DataField="SumOfSumOfFunctHours" HeaderText="Hours"  
                SortExpression="SumOfSumOfFunctHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="SumOfSumOfMiscHours" HeaderText="Misc Hours" 
                SortExpression="SumOfSumOfMiscHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="ActRate" HeaderText="Actual Rate" 
                SortExpression="ActRate" DataFormatString="{0:F2}" />
                <asp:BoundField DataField="GoalHours" HeaderText="Goal Hours" SortExpression="GoalHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="percntGoal" HeaderText="% Goal" 
                SortExpression="percntGoal" DataFormatString="{0:P1}" />
            <asp:BoundField DataField="TotalHours" HeaderText="Total Hours" 
                SortExpression="TotalHours" DataFormatString="{0:F2}" />
        </Columns>
        <EmptyDataTemplate>
           There is no data available.
        </EmptyDataTemplate>
    </asp:GridView>
    </div>
   </asp:Panel>
       
<asp:GridView ID="ShiftTM" runat="server" AllowPaging="True" AllowSorting="True" 
           AutoGenerateColumns="False" DataKeyNames="EmployeeID" 
           DataSourceID="TMDataSource" PageSize="20" ViewStateMode="Disabled" > 
           
           <AlternatingRowStyle BackColor="#f2f2f2" />
           
           <Columns>
           
           <asp:BoundField DataField="EmployeeID" HeaderText="AS400 ID" ReadOnly="True" 
               SortExpression="EmployeeID" />
           <asp:BoundField DataField="FirstName" HeaderText="First Name" ReadOnly="True"
               SortExpression="FirstName" >
               <ControlStyle Width="100px" />
               </asp:BoundField>
           <asp:BoundField DataField="LastName" HeaderText="Last Name" ReadOnly="True"
               SortExpression="LastName" >
               <ControlStyle Width="100px" />
               </asp:BoundField>
         
          
           <asp:TemplateField HeaderText="Dept" SortExpression="Dept">
           <ControlStyle Width="100px" />
               <EditItemTemplate>
                  <asp:DropDownList ID="DDLDepartment" runat="server" 
                       DataSourceID="DeptDataSource" DataTextField="Dept" DataValueField="DepartmentID"  SelectedValue='<%# Bind("DepartmentID") %>' AppendDataBoundItems="true">
                   </asp:DropDownList> 
               </EditItemTemplate>
               
               <ItemTemplate>
                   <asp:Label ID="Label1" runat="server" Text='<%# Bind("Dept") %>'></asp:Label>
               </ItemTemplate>

           </asp:TemplateField>

           <asp:TemplateField HeaderText="Shift" SortExpression="Shift"  >
           <ControlStyle Width="60px" />
               <EditItemTemplate>
                  <asp:DropDownList ID="DDLShift" runat="server" DataSourceID="ShiftDataSource" DataTextField="Shift" DataValueField="ShiftID" SelectedValue='<%# Bind("ShiftID") %>' AppendDataBoundItems="true"  >
                    </asp:DropDownList> 

               </EditItemTemplate>
               <ItemTemplate>
                   <asp:Label ID="Label2" runat="server" Text='<%# Bind("Shift") %>' ></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
           <asp:CheckBoxField DataField="FarmIn" HeaderText="Farm In" 
                    SortExpression="FarmIn" >
                <ControlStyle Width="15px" />
                </asp:CheckBoxField>
           <asp:CommandField ShowEditButton="True"  />
           
       </Columns>
           <EmptyDataTemplate>
               There are no Current Team Members for this Department and Shift.
           </EmptyDataTemplate>
       </asp:GridView>
      
    </div>
 <div id="sideNav" class="sideNav">

 <h3>Tasks</h3>
    <ul>
        <li><asp:LinkButton ID="TMorSumm" runat="server" Text="Verify Team Members" CausesValidation="False" OnClick="TMorSumm_click" onmouseover="window.status='View Summary or Verify Team Members';return true;" onmouseout ="window.status=''; return true;"></asp:LinkButton></li>
   <li><asp:LinkButton ID="ImportProd" runat="server" PostBackUrl="~/FCP/1st/ImportTMProd.aspx"  onmouseover="window.status='Import Productivity';return true;" onmouseout ="window.status=''; return true;">Import Productivity</asp:LinkButton><ajaxToolkit:ConfirmButtonExtender ID="ImportConfirmButton" runat="server" TargetControlID="ImportProd" ConfirmText="Click OK only if you have verified the Team Members that worked on the Import Date.">         
         </ajaxToolkit:ConfirmButtonExtender>
     </li>
     <li><asp:LinkButton ID="EditProd" runat="server" PostBackUrl="~/FCP/1st/EditTMProd.aspx" onmouseover="window.status='Edit Productivity';return true;" onmouseout ="window.status=''; return true;">Edit Productivity</asp:LinkButton> </li>
      <li><asp:LinkButton ID="AddProd" runat="server" PostBackUrl="~/FCP/1st/AddTMProd.aspx" onmouseover="window.status='Add Productivity';return true;" onmouseout ="window.status=''; return true;">Add Productivity</asp:LinkButton> </li>
       </ul>
   

    <h3>Reports</h3>
        <ul>   
    <li><asp:LinkButton ID="ViewByTM" runat="server" PostBackUrl="~/FCP/1st/ViewbyTM.aspx" onmouseover="window.status='View By Team Member';return true;" onmouseout ="window.status=''; return true;">View by Team Member</asp:LinkButton> </li>
    <li><asp:LinkButton ID="ViewByFunction" runat="server" PostBackUrl="~/FCP/1st/ViewbyFunct.aspx" onmouseover="window.status='View By Function';return true;" onmouseout ="window.status=''; return true;">View by Function</asp:LinkButton> </li>
    <li><asp:LinkButton ID="SummaryDateRange" runat="server" PostBackUrl="~/FCP/1st/SummDateRange.aspx" onmouseover="window.status='Summary of Date Range';return true;" onmouseout ="window.status=''; return true;">Summary of Date Range</asp:LinkButton> </li>
    <li><asp:LinkButton ID="SummbyFunct" runat="server" PostBackUrl="~/FCP/1st/SummbyFunct.aspx" onmouseover="window.status='Summary by Function';return true;" onmouseout ="window.status=''; return true;">Summary by Function</asp:LinkButton> </li>
    </ul>
       
    </div>


 <asp:SqlDataSource ID="TMDataSource" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
           ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"
                    
           SelectCommand="SELECT TMs.EmployeeID, TMs.ShiftID, TMs.FirstName, TMs.DepartmentID, TMs.LastName, TMs.FarmIn, TMs.EmployeeNum, Departments.Dept, Shifts.Shift FROM (Shifts INNER JOIN (Departments INNER JOIN TMs ON Departments.DepartmentID = TMs.DepartmentID) ON Shifts.ShiftID = TMs.ShiftID) WHERE (TMs.ShiftID = 1) AND (TMs.DepartmentID = 2) ORDER BY TMs.EmployeeID"            
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
                    
          <asp:SqlDataSource ID="FirstPrevDay" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT Function, Description, Rate, SumOfAmount, SumOfFunctHours, SumOfMiscHours, SumOfAmount / SumOfFunctHours AS ActRate,SumOfAmount/Rate AS GoalHours, (SumOfAmount/Rate)/ SumOfFunctHours AS percntGoal, SumOfFunctHours + SumOfMiscHours AS TotalHours FROM SummaryFCP1stDay"></asp:SqlDataSource>
          <asp:SqlDataSource ID="FirstPrevWeek" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT Function, Description, Rate, SumOfSumOfAmount, SumOfSumOfFunctHours, SumOfSumOfMiscHours, [SumOfSumOfAmount]/[SumOfSumOfFunctHours] AS ActRate,SumOfSumOfAmount/Rate AS GoalHours, (SumOfSumOfAmount/Rate)/ SumOfSumOfFunctHours AS percntGoal, ([SumOfSumOfFunctHours]+[SumOfSumOfMiscHours]) AS TotalHours
FROM SummOfSummFCP1stWeek
"></asp:SqlDataSource>
          <asp:SqlDataSource ID="FirstPrevMonth" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT Function, Description, Rate, SumOfSumOfAmount, SumOfSumOfFunctHours, SumOfSumOfMiscHours, SumOfSumOfAmount / SumOfSumOfFunctHours AS ActRate, SumOfSumOfAmount/Rate AS GoalHours, (SumOfSumOfAmount/Rate)/ SumOfSumOfFunctHours AS percntGoal, SumOfSumOfFunctHours + SumOfSumOfMiscHours AS TotalHours FROM SummOfSummFCP1stMonth">
</asp:SqlDataSource>
    
</asp:Content>

