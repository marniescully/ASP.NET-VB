<%@ Page Title="Utility Productivity by Team Member" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="ViewbyTM.aspx.vb" Inherits="UTILITY" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<h2>Utility Productivity by Team Member </h2> 
 <div id="Link" class="Link">
 <asp:HyperLink ID="backToShift" runat="server" NavigateUrl="~/Utility.aspx">Utility Home</asp:HyperLink>
 </div>

 
   <div id="Search" class="Search">
    
        <asp:Label ID="TmListLabel" runat="server">TM:</asp:Label>
        <asp:DropDownList ID="TMList" runat="server"  DataTextField="FullName" DataValueField="EmployeeID"  DataSourceID="TMListDataSource" AppendDataBoundItems="true"  >
         <asp:ListItem Value="All" Selected="True">All </asp:ListItem>
           </asp:DropDownList>

          

           <asp:Label ID="FunctionLabel" runat="server">Function:</asp:Label>
         <asp:DropDownList ID="FunctList" runat="server"  DataTextField="Description" DataValueField="Function"  DataSourceID="FunctListDataSource" AppendDataBoundItems="true">
    <asp:ListItem Value="All" Selected="True">All </asp:ListItem>
       </asp:DropDownList>

           <asp:Label ID="LblStartDate" runat="server">Start Date: &nbsp;</asp:Label>
           <asp:TextBox ID="txtStartDate" runat="server" Class="Dates" AutoPostBack="true"> </asp:TextBox>
           <asp:RequiredFieldValidator ID="StDateRequiredFieldValidator" runat="server" ErrorMessage="The Start Date is a Required field." ControlToValidate="txtStartDate" Display="None" ></asp:RequiredFieldValidator>

        <asp:Label ID="LblEndDate" runat="server" Text="End Date">End Date: &nbsp;</asp:Label><asp:TextBox ID="txtEndDate" runat="server" Class="Dates"></asp:TextBox>
        <asp:RequiredFieldValidator ID="EndDateRequiredFieldValidator" runat="server" ErrorMessage="The End Date is a Required field."  ControlToValidate="txtEndDate" Display="None" ></asp:RequiredFieldValidator>
         <asp:CompareValidator ID="dateCheck" runat="server" ErrorMessage="End Date must be after Start Date" ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Operator="GreaterThanEqual" Type="Date" Display="None"  ></asp:CompareValidator>
           <ajaxToolkit:CalendarExtender ID="StDateCalendarExtender" TargetControlID="txtStartDate" runat="server"> </ajaxToolkit:CalendarExtender>
                      <ajaxToolkit:CalendarExtender ID="EndDateCalendarExtender" TargetControlID="txtEndDate" runat="server">  </ajaxToolkit:CalendarExtender>
              
              <asp:LinkButton ID="SearchButton"  runat="server" OnClick="SearchButton_OnClick"   Visible="true" Text="View Report"> </asp:LinkButton>
              <asp:LinkButton ID="ClearSearchButton" runat="server" OnClick="ClearSearchButton_OnClick"  Visible="true" Text="Reset" CausesValidation="false"></asp:LinkButton>
              
             
         </div>    

  
 
<div class="TMReports"> 

<asp:Repeater ID="DistTM" runat="server"  DataSourceID="TMProductofDate"
OnItemDataBound="DistTM_ItemDataBound" >

<ItemTemplate>

<h3><asp:Label ID="EmployeeIDLabel" runat="server" Text= '<%# Container.DataItem("FullName")%>' /></h3>
<asp:HiddenField ID="HiddenTMField" runat="Server" Value= '<%# Container.DataItem("EmployeeID") %>' />

 <asp:GridView ID="TMProd" runat="server" 
        AutoGenerateColumns="False" PageSize="20" OnRowDataBound="TMProd_RowDataBound" ShowFooter="True" DataKeyNames="DTE,Function" ViewStateMode="Disabled" CssClass="PageBreak"  >
       <AlternatingRowStyle BackColor="#f2f2f2" />
        <Columns>
            <asp:BoundField DataField="DTE" HeaderText="" SortExpression="DTE" DataFormatString="{0:MM/dd/yy}" />
            <asp:BoundField DataField="Description" HeaderText="" 
                SortExpression="Description" />
            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
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
        <EmptyDataTemplate>
            There is no data to display
        </EmptyDataTemplate>
    </asp:GridView>
   
</ItemTemplate>

</asp:Repeater>



 
    <asp:Label ID="NoData" runat="server" Text="No Data to Display" Visible="false" CssClass="Red"></asp:Label>  

    </div>


     <asp:SqlDataSource ID="TMListDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT TMs.EmployeeID, [FirstName]+' '+[LastName] AS FullName
FROM TMs WHERE (((TMs.ShiftID)=3))
ORDER BY [FirstName]+' '+[LastName];
">
     </asp:SqlDataSource>

       <asp:SqlDataSource ID="FunctListDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT DISTINCT Functions.Function, Functions.Description
FROM Functions ORDER BY Functions.Description">
     </asp:SqlDataSource>

     <asp:SqlDataSource ID="TMProductofDate" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT DISTINCT PROD.EmployeeID, TMs.[FirstName]+' '+[LastName] AS FullName, TMs.ShiftID
FROM TMs INNER JOIN PROD ON TMs.EmployeeID = PROD.EmployeeID
WHERE (((TMs.ShiftID)=3) AND ((PROD.DTE) Between [Date1] And [Date2]))">
 
 
 <SelectParameters>
                <asp:ControlParameter ControlID="txtStartDate" DbType="Date" Name="?" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="txtEndDate" DbType="Date" Name="?" 
                PropertyName="Text" />
             
        </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="OneTMProductofDateAllFunct" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
         SelectCommand=" SELECT DISTINCT PROD.EmployeeID, TMs.[FirstName]+' '+[LastName] AS FullName, TMs.ShiftID
FROM TMs INNER JOIN PROD ON TMs.EmployeeID = PROD.EmployeeID
WHERE (((TMs.ShiftID)=3) AND ((PROD.DTE) Between [Date1] And [Date2]) AND (PROD.EmployeeID=[EmpID]))">
 
 
 <SelectParameters>
 
                <asp:ControlParameter ControlID="txtStartDate" DbType="Date" Name="?" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="txtEndDate" DbType="Date" Name="?" 
                PropertyName="Text" />
                <asp:ControlParameter ControlID="TMList" Name="?" PropertyName="SelectedValue" DbType="String" />
           
        </SelectParameters>
        </asp:SqlDataSource>

          <asp:SqlDataSource ID="OneTMProductofDateOneFunct" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand=" SELECT DISTINCT PROD.EmployeeID, TMs.[FirstName]+' '+[LastName] AS FullName,  TMs.ShiftID
FROM TMs INNER JOIN PROD ON TMs.EmployeeID = PROD.EmployeeID
WHERE (((TMs.ShiftID)=3) AND ((PROD.DTE) Between [Date1] And [Date2]) AND (PROD.EmployeeID=[EmpID])  AND ((PROD.Function)=[Funct]))">
 
 
 <SelectParameters>
 
                <asp:ControlParameter ControlID="txtStartDate" DbType="Date" Name="?" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="txtEndDate" DbType="Date" Name="?" 
                PropertyName="Text" />
                <asp:ControlParameter ControlID="TMList" Name="?" PropertyName="SelectedValue" DbType="String" />
             <asp:ControlParameter ControlID="FunctList" Name="?" PropertyName="SelectedValue" DbType="String" />
        </SelectParameters>
        </asp:SqlDataSource>


        <asp:SqlDataSource ID="TMProductofDateOneFunct" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand=" SELECT DISTINCT PROD.EmployeeID, TMs.[FirstName]+' '+[LastName] AS FullName, TMs.ShiftID
FROM TMs INNER JOIN PROD ON TMs.EmployeeID = PROD.EmployeeID
WHERE (((TMs.ShiftID)=3) AND ((PROD.DTE) Between [Date1] And [Date2]) AND ((PROD.Function)=[Funct]))">
 
 
 <SelectParameters>
                <asp:ControlParameter ControlID="txtStartDate" DbType="Date" Name="?" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="txtEndDate" DbType="Date" Name="?" 
                PropertyName="Text" />
             <asp:ControlParameter ControlID="FunctList" Name="?" PropertyName="SelectedValue" DbType="String" />
        </SelectParameters>
        </asp:SqlDataSource>

    </asp:Content>

