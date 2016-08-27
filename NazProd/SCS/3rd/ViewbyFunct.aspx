<%@ Page Title="Split Case Stock 3rd Shift Productivity by Function" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="ViewbyFunct.aspx.vb" Inherits="SCS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <p class="floatRight">
    <asp:HyperLink ID="backToShiftLink" runat="server" NavigateUrl="~/SCS/3rd/Shift.aspx">3rd Shift Home</asp:HyperLink>
    </p>
 
    <h2>
        Split Case Stock 3rd Shift Productivity by Function
    </h2>

  <div id="SearchReports">
        
        
         <asp:Label ID="FunctionLabel" runat="server">Function: &nbsp;</asp:Label>
         <asp:DropDownList ID="FunctList" runat="server"   DataTextField="Description" DataValueField="Function"  DataSourceID="FunctListDataSource" AppendDataBoundItems="true"  >
    <asp:ListItem Value="All" Selected="True">All </asp:ListItem>
       </asp:DropDownList>

           <asp:Label ID="LblStartDate" runat="server" >Start Date: &nbsp;</asp:Label><asp:TextBox ID="txtStartDate" runat="server" Class="Dates" AutoPostBack="true"> </asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="The Start Date is a Required field." ControlToValidate="txtStartDate" Display="None" ></asp:RequiredFieldValidator>

        <asp:Label ID="LblEndDate" runat="server" Text="End Date">End Date: &nbsp;</asp:Label><asp:TextBox ID="txtEndDate" runat="server" Class="Dates"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="The End Date is a Required field." ControlToValidate="txtEndDate" Display="None" ></asp:RequiredFieldValidator>
 <asp:CompareValidator ID="dateCheck" runat="server" ErrorMessage="End Date must be after Start Date" ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Operator="GreaterThanEqual" Type="Date" Display="None"  ></asp:CompareValidator>
           <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" runat="server"> </ajaxToolkit:CalendarExtender>
                      <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" runat="server">  </ajaxToolkit:CalendarExtender>
     
             <asp:LinkButton ID="SearchButton"  runat="server" OnClick="SearchButton_OnClick"  Visible="true" Text="View Report"> </asp:LinkButton>
              <asp:LinkButton ID="ClearSearchButton" runat="server" OnClick="ClearSearchButton_OnClick"  Visible="true" Text="Reset" CausesValidation="false" ></asp:LinkButton>

              <asp:ValidationSummary ID="ValidationSummary1"  runat="server" /></div>

 
<div class="AllRepeat">

<asp:Repeater ID="DateRepeat" runat="server"  Visible="false"  OnItemDataBound="DateRepeat_ItemDataBound" EnableViewState="False"  >

<ItemTemplate>
<table id="DateLabelTable" class="PageBreak">
                    <tr>
                        <td class="left">
<h3 class="negMarg" >
<asp:Label ID="DteLabel" runat="server"  CssClass="Red" Text= '<%# Databinder.Eval(Container.DataItem,"DTE","{0:MM/dd/yy}" ) %>' /></h3>
<asp:HiddenField  ID="HiddenDateField" runat="server" Value= '<%# Databinder.Eval(Container.DataItem,"DTE", "{0:MM/dd/yy}") %>' />
</td>
                    </tr>
                    <tr>
                        <td>
<asp:Repeater ID="DistFunct" runat="server" OnItemDataBound="DistFunct_ItemDataBound" Visible="false" ViewStateMode="Disabled" >

<HeaderTemplate>

<table id="DistFunctTable" ></HeaderTemplate>
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
            <asp:BoundField DataField="FullName" HeaderText="" 
                SortExpression="FullName" />
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
       
    </asp:GridView>
   <p>&nbsp;</p></td></tr>
</ItemTemplate>
<FooterTemplate></table></FooterTemplate>
</asp:Repeater>
</td>
                    </tr>
            </table>
</ItemTemplate>

</asp:Repeater> </div>

<asp:HiddenField ID="OuterDateField" runat="server" visible="false" />
<asp:TextBox ID="DateSelected" runat="server" Text="" Visible="false"></asp:TextBox>

<asp:Label ID="NoData" runat="server" Text="No Data to Display" Visible="false" CssClass="Red" ></asp:Label>

<asp:SqlDataSource ID="FunctListDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT Function, Description FROM Functions WHERE Functions.Department=9 Order by Description">
     </asp:SqlDataSource>


     <asp:SqlDataSource ID="DatesDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT DISTINCT DTE FROM PROD WHERE DTE Between ? And ? AND Shift=3 and Department =9">

        <SelectParameters>
                <asp:ControlParameter ControlID="txtStartDate"  Name="?" 
                PropertyName="Text" ConvertEmptyStringToNull="False" Type="DateTime" />
            <asp:ControlParameter ControlID="txtEndDate"  Name="?" 
                PropertyName="Text" Type="DateTime" />
             
        </SelectParameters>
     </asp:SqlDataSource>

 
     


        <asp:SqlDataSource ID="ProductFunctofDateOne" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand=" SELECT DISTINCT Functions.Description, Functions.Function, Functions.Rate FROM Functions INNER JOIN Prod ON (Functions.Department = Prod.Department) AND (Functions.Function = Prod.Function)
WHERE (Prod.DTE BETWEEN ? AND ?) AND (Prod.Function=?) AND (Prod.Department = 9) AND (Prod.Shift = 3)">
 
 <SelectParameters>
                <asp:ControlParameter ControlID="txtStartDate" DbType="Date" Name="?" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="txtEndDate" DbType="Date" Name="?" 
                PropertyName="Text" />
              <asp:ControlParameter ControlID="FunctList" Name="?" PropertyName="SelectedValue" DbType="String" />
        </SelectParameters>
        </asp:SqlDataSource>
    </asp:Content>

