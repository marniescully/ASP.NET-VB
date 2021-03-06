﻿<%@ Page Title="Full Case Stock 1st Shift Productivity Summary by Function" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="SummbyFunct.aspx.vb" Inherits="FCS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <h2 class="ReportTitle">Full Case Stock 1st Shift Productivity Summary by Function </h2>
 <div id="Link" class="Link"><asp:HyperLink ID="backToShift" runat="server" NavigateUrl="~/FCS/1st/Shift.aspx">1st Shift Home</asp:HyperLink></div>
       
       <div id="Search" class="Search">
      
        <asp:Label ID="FunctionLabel" runat="server">Function: &nbsp;</asp:Label>
        
         <asp:DropDownList ID="FunctList" runat="server" DataTextField="Description" DataValueField="Function"  DataSourceID="FunctListDataSource" AppendDataBoundItems="true"  >
    <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
       </asp:DropDownList>

           <asp:Label ID="LblStartDate" runat="server" >Start Date: &nbsp;</asp:Label><asp:TextBox ID="txtStartDate" runat="server" Class="Dates" AutoPostBack="true"> </asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="The Start Date is a Required field." ControlToValidate="txtStartDate" Display="None"></asp:RequiredFieldValidator>

        <asp:Label ID="LblEndDate" runat="server" Text="End Date">End Date: &nbsp;</asp:Label><asp:TextBox ID="txtEndDate" runat="server" Class="Dates"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="The End Date is a Required field." ControlToValidate="txtEndDate" Display="None" ></asp:RequiredFieldValidator>
         <asp:CompareValidator ID="dateCheck" runat="server" ErrorMessage="End Date must be after Start Date" ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Operator="GreaterThanEqual" Type="Date" Display="None"  ></asp:CompareValidator>
           <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" runat="server"> </ajaxToolkit:CalendarExtender>
                      <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" runat="server">  </ajaxToolkit:CalendarExtender>
              
              <asp:LinkButton ID="SearchButton"  runat="server" OnClick="SearchButton_OnClick"   Visible="true" Text="View Report"> </asp:LinkButton>
              <asp:LinkButton ID="ClearSearchButton" runat="server" OnClick="ClearSearchButton_OnClick"  Visible="true" Text="Reset" CausesValidation="false"></asp:LinkButton>

               <asp:ValidationSummary ID="ValidationSummary"  runat="server" />
         </div>    

<asp:Label ID="NoData" runat="server" Text="No Data to Display" Visible="false" CssClass="Red" ></asp:Label>

<div id="AllRepeat" class="AllRepeat">


<asp:Repeater ID="DistFunct" runat="server" OnItemDataBound="DistFunct_ItemDataBound" Visible="false" ViewStateMode="Disabled" >

<HeaderTemplate>

<table id="DistFunctTable"></HeaderTemplate>
<ItemTemplate>

<tr>
<td class="left">
<asp:Label ID="FunctLabel" runat="server" Text= '<%# Container.DataItem("Description")%>' /><asp:HiddenField ID="HiddenFunctionField" runat="Server" Value= '<%# Container.DataItem("Function") %>' />
</td>

<td class="right">
<asp:Label runat="server" ID="GoalRateLabel" >Goal Rate:</asp:Label>
<asp:Label ID="RateLabel" runat="server"  Text='<%# Container.DataItem("Rate")%>' /> 
</td>


</tr>
<tr><td colspan="2">
 <asp:GridView ID="TMProd" runat="server" 
        AutoGenerateColumns="False" PageSize="20" OnRowDataBound="TMProd_RowDataBound" ShowFooter="True" DataKeyNames="DTE,Function" ViewStateMode="Disabled" Cssclass="DistFunctTable"  >
         <AlternatingRowStyle BackColor="#F2F2F2"  />
        <Columns>
            <asp:BoundField DataField="DTE" HeaderText="" 
                SortExpression="DTE" DataFormatString="{0:MM/dd/yy}" />
            <asp:BoundField DataField="SumOfAmount" HeaderText="Amount" 
                SortExpression="SumOfAmount" />
            <asp:BoundField DataField="SumOfFunctHours" HeaderText="Hours" 
                SortExpression="SumOfFunctHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="ActualRate" HeaderText="Act. Rate" 
                SortExpression="ActualRate" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="GoalHours" HeaderText="Goal Hours" 
                SortExpression="GoalHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="PercentAvg" HeaderText="% of Goal" 
                SortExpression="PercentAvg" DataFormatString="{0:P1}" />
            <asp:BoundField DataField="SumOfMiscHours" HeaderText="Misc. Hours" 
                SortExpression="SumOfMiscHours" DataFormatString="{0:F2}" />
            <asp:BoundField DataField="TotalHours" HeaderText="Total Hours" 
                SortExpression="TotalHours" DataFormatString="{0:F2}" />
        </Columns>
         <EmptyDataTemplate>
            There is no data to display
        </EmptyDataTemplate>
    </asp:GridView>
   <p>&nbsp;</p> </td></tr>
</ItemTemplate>
<FooterTemplate></table></FooterTemplate>
</asp:Repeater>
</div>


    

       <asp:SqlDataSource ID="FunctListDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT Function, Description FROM Functions WHERE (Functions.Department)=3 Order by Description">
     </asp:SqlDataSource>
     
      
        <asp:SqlDataSource ID="ProductFunctofDate" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand=" SELECT DISTINCT Functions.Description, Functions.Function, Functions.Rate FROM Functions INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function) WHERE (Prod.DTE BETWEEN ? AND ?) AND (Prod.Shift = 1) AND (Prod.Department = 3)">
 
 <SelectParameters>
                <asp:ControlParameter ControlID="txtStartDate" DbType="Date" Name="?" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="txtEndDate" DbType="Date" Name="?" 
                PropertyName="Text" />
             
        </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="ProductFunctofDateOne" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand=" SELECT DISTINCT Functions.Description, Functions.Function, Functions.Rate FROM Functions INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function) WHERE (Prod.DTE BETWEEN ? AND ?) AND (Prod.Function=?) AND (Prod.Shift = 1) AND (Prod.Department = 3)">
 
 <SelectParameters>
                <asp:ControlParameter ControlID="txtStartDate" DbType="Date" Name="?" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="txtEndDate" DbType="Date" Name="?" 
                PropertyName="Text" />

             <asp:ControlParameter ControlID="FunctList" Name="?" PropertyName="SelectedValue" DbType="String" />
        </SelectParameters>
        </asp:SqlDataSource>
        
        

    </asp:Content>

