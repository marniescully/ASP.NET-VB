<%@ Page Title="Split Case Pick 2nd Shift Summary by Date Range" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="SummDateRange.aspx.vb" Inherits="SCP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<p class="floatRight">
    <asp:HyperLink ID="backToShiftLink" runat="server" NavigateUrl="~/SCP/2nd/Shift.aspx">2nd Shift Home</asp:HyperLink>
    </p>
 
    <h2>
        Split Case Pick 2nd Shift Productivity Summary by Date Range
    </h2>

  <div id="SearchReports">
        
                <asp:Label ID="FunctionLabel" runat="server">Function: &nbsp;</asp:Label>
         <asp:DropDownList ID="FunctList" runat="server"   DataTextField="Description" DataValueField="Function"  DataSourceID="FunctListDataSource" AppendDataBoundItems="true"  >
    <asp:ListItem Value="All" Selected="True">All </asp:ListItem>
       </asp:DropDownList>

           <asp:Label ID="LblStartDate" runat="server" >Start Date: &nbsp;</asp:Label><asp:TextBox ID="txtStartDate" runat="server" Class="Dates" AutoPostBack="true"> </asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="The Start Date is a Required field." ControlToValidate="txtStartDate" Display="None" ></asp:RequiredFieldValidator>

        <asp:Label ID="LblEndDate" runat="server" Text="End Date">End Date: &nbsp;</asp:Label><asp:TextBox ID="txtEndDate" runat="server" Class="Dates"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="The End Date is a Required field."  ControlToValidate="txtEndDate" Display="None" ></asp:RequiredFieldValidator>
 <asp:CompareValidator ID="dateCheck" runat="server" ErrorMessage="End Date must be after Start Date" ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Operator="GreaterThanEqual" Type="Date" Display="None"  ></asp:CompareValidator>
           <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" runat="server"> </ajaxToolkit:CalendarExtender>
                      <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" runat="server">  </ajaxToolkit:CalendarExtender>
              
              <asp:LinkButton ID="SearchButton"  runat="server" OnClick="SearchButton_OnClick"   Visible="true" Text="View Report"> </asp:LinkButton>
              <asp:LinkButton ID="ClearSearchButton" runat="server" OnClick="ClearSearchButton_OnClick"  Visible="true" Text="Reset" CausesValidation="false"></asp:LinkButton>

              <asp:ValidationSummary ID="ValidationSummary1"  runat="server" /></div>

 
<div class="AllRepeat"> 


 <asp:GridView ID="TMProd" runat="server" 
        AutoGenerateColumns="False" 
        PageSize="20"  ViewStateMode="Disabled" CssClass="DistFunctTable" >
          <AlternatingRowStyle BackColor="#f2f2f2" />
        <Columns>
                    
            <asp:BoundField DataField="Description" HeaderText="" 
                SortExpression="Description" />
            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" 
                SortExpression="Amount" />
            <asp:BoundField DataField="FunctHours" HeaderText="Hours" 
                SortExpression="FunctHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="ActualRate" HeaderText="Act. Rate" 
                SortExpression="ActualRate" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="GoalHours" HeaderText="Goal Hours" 
                SortExpression="GoalHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="PercentAvg" HeaderText="% of Goal" 
                SortExpression="PercentAvg" DataFormatString="{0:P1}" />
            
        </Columns>
        <EmptyDataTemplate>
            There is no data to display
        </EmptyDataTemplate>
    </asp:GridView>


</div>

 

   

       <asp:SqlDataSource ID="FunctListDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT Function, Description FROM Functions WHERE Functions.Department=8 Order by Description">
     </asp:SqlDataSource>
    </asp:Content>

