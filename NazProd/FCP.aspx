<%@ Page Title="Full Case Pick" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<div id="sideNav">

 <h3>Shifts</h3>
    <ul>
        <li><asp:HyperLink ID="FirstShift" runat="server" NavigateUrl="~/FCP/1st/Shift.aspx">1st Shift</asp:HyperLink></li>
    <li><asp:HyperLink ID="SecondShift" runat="server" NavigateUrl="~/FCP/2nd/Shift.aspx">2nd Shift</asp:HyperLink></li>
     <li><asp:HyperLink ID="EditFunctions" runat="server" NavigateUrl="~/FCP/EditFunct.aspx">Edit Functions</asp:HyperLink></li>
       </ul>
   <h3>Reports</h3>
    <ul>
        <li><asp:LinkButton ID="SummaryDateRange" runat="server" PostBackUrl="~/FCP/SummDateRange.aspx" onmouseover="window.status='Summary of Date Range';return true;" onmouseout ="window.status=''; return true;">Summary of Date Range</asp:LinkButton> </li>
         <li><asp:LinkButton ID="SummbyFunct" runat="server" PostBackUrl="~/FCP/SummbyFunct.aspx" onmouseover="window.status='Summary by Function';return true;" onmouseout ="window.status=''; return true;">Summary by Function</asp:LinkButton> </li>
       </ul>
       
    </div>

<div id="LeftColumn">

<h2>Full Case Pick</h2>
 
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
            <asp:BoundField DataField="SumOfFunctHours" HeaderText="Hours" 
                SortExpression="SumOfFunctHours" DataFormatString="{0:F2}" />
           
            <asp:BoundField DataField="ActRate" HeaderText="Actual Rate" 
                SortExpression="ActRate" DataFormatString="{0:F2}" HtmlEncode="False" 
                HtmlEncodeFormatString="False" />
                 <asp:BoundField DataField="GoalHours" HeaderText="Goal Hours" 
                SortExpression="GoalHours" DataFormatString="{0:F2}" />
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
            <asp:BoundField DataField="SumOfSumOfFunctHours" HeaderText="Hours" 
                SortExpression="SumOfSumOfFunctHours" DataFormatString="{0:F2}" />
            
            <asp:BoundField DataField="ActRate" HeaderText="Actual Rate" 
                SortExpression="ActRate" DataFormatString="{0:F2}" />
                 <asp:BoundField DataField="GoalHours" HeaderText="Goal Hours" 
                SortExpression="GoalHours" DataFormatString="{0:F2}" />
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
            <asp:BoundField DataField="SumOfSumOfFunctHours" HeaderText="Hours" 
                SortExpression="SumOfSumOfFunctHours" DataFormatString="{0:F2}" />
           
            <asp:BoundField DataField="ActRate" HeaderText="Actual Rate" 
                SortExpression="ActRate" DataFormatString="{0:F2}" />
                 <asp:BoundField DataField="GoalHours" HeaderText="Goal Hours" 
                SortExpression="GoalHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="percntGoal" HeaderText="% Goal" 
                SortExpression="percntGoal" DataFormatString="{0:P1}" />
          
        </Columns>
        <EmptyDataTemplate>
            There is no data available.
        </EmptyDataTemplate>
    </asp:GridView>

  

</div>




<asp:SqlDataSource ID="PrevDay" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT Function, Description, Rate, SumOfAmount, SumOfFunctHours,  SumOfAmount / SumOfFunctHours AS ActRate, SumOfAmount/Rate AS GoalHours, (SumOfAmount/Rate)/ SumOfFunctHours AS percntGoal FROM SummaryFCPDay">
</asp:SqlDataSource>

 <asp:SqlDataSource ID="PrevWeek" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT Function,  Description, Rate, SumOfSumOfAmount, SumOfSumOfFunctHours,  SumOfSumOfAmount / SumOfSumOfFunctHours AS ActRate,  SumOfSumOfAmount/Rate AS GoalHours, (SumOfSumOfAmount/Rate)/ SumOfSumOfFunctHours AS percntGoal FROM SummOfSummFCPWeek">
</asp:SqlDataSource> 
         
<asp:SqlDataSource ID="PrevMonth" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT Function, Description, Rate, SumOfSumOfAmount, SumOfSumOfFunctHours, SumOfSumOfAmount / SumOfSumOfFunctHours AS ActRate,  SumOfSumOfAmount/Rate AS GoalHours, (SumOfSumOfAmount/Rate)/ SumOfSumOfFunctHours AS percntGoal FROM SummOfSummFCPMonth">
</asp:SqlDataSource>



</asp:Content>

