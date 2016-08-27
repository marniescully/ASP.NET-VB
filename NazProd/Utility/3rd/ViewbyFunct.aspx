<%@ Page Title="Utility Productivity by Function" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="ViewbyFunct.aspx.vb" Inherits="UTILITY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  <h2>Utility Productivity by Function </h2>
  <div id="Link" class="Link"><asp:HyperLink ID="backToShift" runat="server" NavigateUrl="~/Utility.aspx">Utility Home</asp:HyperLink></div>

 

       <div id="Search" class="Search">
        
         <asp:Label ID="FunctionLabel" runat="server">Function</asp:Label>
         <asp:DropDownList ID="FunctList" runat="server"   DataTextField="Description" DataValueField="Function"  DataSourceID="FunctListDataSource" AppendDataBoundItems="true"  >
    <asp:ListItem Value="All" Selected="True">All </asp:ListItem>
       </asp:DropDownList>

           <asp:Label ID="LblStartDate" runat="server" >Start Date &nbsp;</asp:Label><asp:TextBox ID="txtStartDate" runat="server" Class="Dates" AutoPostBack="true"> </asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="The Start Date is a Required field." ControlToValidate="txtStartDate" Display="None" ></asp:RequiredFieldValidator>

        <asp:Label ID="LblEndDate" runat="server" Text="End Date">End Date &nbsp;</asp:Label><asp:TextBox ID="txtEndDate" runat="server" Class="Dates"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="The End Date is a Required field." ControlToValidate="txtEndDate" Display="None" ></asp:RequiredFieldValidator>
 <asp:CompareValidator ID="dateCheck" runat="server" ErrorMessage="End Date must be after Start Date" ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Operator="GreaterThanEqual" Type="Date" Display="None"  ></asp:CompareValidator>
           <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" runat="server"> </ajaxToolkit:CalendarExtender>
                      <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" runat="server">  </ajaxToolkit:CalendarExtender>
     
             <asp:LinkButton ID="SearchButton"  runat="server" OnClick="SearchButton_OnClick"  Visible="true" Text="View Report"> </asp:LinkButton>
              <asp:LinkButton ID="ClearSearchButton" runat="server" OnClick="ClearSearchButton_OnClick"  Visible="true" Text="Reset" CausesValidation="false" ></asp:LinkButton>

              <asp:ValidationSummary ID="ValidationSummary1"  runat="server" /></div>

 
<div id="AllRepeat" class="AllRepeat">

<asp:Repeater ID="DateRepeat" runat="server"  Visible="false"  OnItemDataBound="DateRepeat_ItemDataBound" EnableViewState="False"  >

<ItemTemplate>

<h3>
<asp:Label ID="DteLabel" runat="server"  Text= '<%# Databinder.Eval(Container.DataItem,"DTE","{0:MM/dd/yy}" ) %>' /></h3>
<asp:HiddenField  ID="HiddenDateField" runat="server" Value= '<%# Databinder.Eval(Container.DataItem,"DTE", "{0:MM/dd/yy}") %>' />

<asp:Repeater ID="DistFunct" runat="server" OnItemDataBound="DistFunct_ItemDataBound" Visible="false" ViewStateMode="Disabled" >

<HeaderTemplate>

<table id="DistFunctTable" class="AllRepeat" ></HeaderTemplate>
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
        AutoGenerateColumns="False" PageSize="20" OnRowDataBound="TMProd_RowDataBound" ShowFooter="True" DataKeyNames="DTE,EmployeeID,Function" ViewStateMode="Disabled" Cssclass="DistFunctTable"  >
         <AlternatingRowStyle BackColor="#F2F2F2"  />
        <Columns>
            <asp:BoundField DataField="EmployeeID" HeaderText="AS400 ID" 
                SortExpression="EmployeeID" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" 
                SortExpression="Amount" />
            <asp:BoundField DataField="FunctHours" HeaderText="Hours" 
                SortExpression="FunctHours" DataFormatString="{0:F1}" />
            <asp:BoundField DataField="ActualRate" HeaderText="Act. Rate" 
                SortExpression="ActualRate" DataFormatString="{0:F1}" />
            <asp:BoundField DataField="GoalHours" HeaderText="Goal Hours" 
                SortExpression="GoalHours" DataFormatString="{0:F1}" />
            <asp:BoundField DataField="PercentAvg" HeaderText="% of Goal" 
                SortExpression="PercentAvg" DataFormatString="{0:P1}" />
            <asp:BoundField DataField="MiscHours" HeaderText="Misc. Hours" 
                SortExpression="MiscHours" DataFormatString="{0:F1}" />
            <asp:BoundField DataField="TotalHours" HeaderText="Total Hours" 
                SortExpression="TotalHours" DataFormatString="{0:F1}" />
        </Columns>
       
    </asp:GridView>
   <p>&nbsp;</p> </td></tr>
</ItemTemplate>
<FooterTemplate></table></FooterTemplate>
</asp:Repeater>

</ItemTemplate>
<FooterTemplate></FooterTemplate>
</asp:Repeater> </div>

<asp:HiddenField ID="OuterDateField" runat="server" visible="false" />
<asp:TextBox ID="DateSelected" runat="server" Text="" Visible="false"></asp:TextBox>

<asp:Label ID="NoData" runat="server" Text="No Data to Display" Visible="false" CssClass="Red" ></asp:Label>

<asp:SqlDataSource ID="FunctListDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT FCPFunctions.Function, FCPFunctions.Description
FROM FCPFunctions 
UNION
SELECT  FCSFunctions.Function, FCSFunctions.Description
FROM FCSFunctions
UNION
SELECT RecFunctions.Function,RecFunctions.Description
FROM  RecFunctions
UNION SELECT ShipFunctions.Function, ShipFunctions.Description
FROM  ShipFunctions
 Order by Description">
     </asp:SqlDataSource>


     <asp:SqlDataSource ID="DatesDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT DISTINCT DTE FROM Shipping WHERE DTE Between ? And ? AND Shift=1">

        <SelectParameters>
                <asp:ControlParameter ControlID="txtStartDate"  Name="?" 
                PropertyName="Text" ConvertEmptyStringToNull="False" Type="DateTime" />
            <asp:ControlParameter ControlID="txtEndDate"  Name="?" 
                PropertyName="Text" Type="DateTime" />
             
        </SelectParameters>
     </asp:SqlDataSource>

 
     <asp:SqlDataSource ID="ProductFunctofDate" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand=" SELECT DISTINCT FCSFunctions.Description, FCSFunctions.Function, FCSFunctions.Rate FROM FCSFunctions INNER JOIN FCS ON SHIPFunctions.Function = Shipping.Function
WHERE (Shipping.DTE BETWEEN ? AND ?) AND (Shipping.Shift = 1)">
 
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
        SelectCommand=" SELECT DISTINCT SHIPFunctions.Description, SHIPFunctions.Function, SHIPFunctions.Rate FROM SHIPFunctions INNER JOIN Shipping ON SHIPFunctions.Function = Shipping.Function
WHERE (Shipping.DTE BETWEEN ? AND ?) AND (Shipping.Function=?) AND (Shipping.Shift = 1)">
 
 <SelectParameters>
                <asp:ControlParameter ControlID="txtStartDate" DbType="Date" Name="?" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="txtEndDate" DbType="Date" Name="?" 
                PropertyName="Text" />
              <asp:ControlParameter ControlID="FunctList" Name="?" PropertyName="SelectedValue" DbType="String" />
        </SelectParameters>
        </asp:SqlDataSource>
    </asp:Content>

