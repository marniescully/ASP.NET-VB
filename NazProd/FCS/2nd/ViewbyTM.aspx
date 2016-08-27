<%@ Page Title="Full Case Stock 2nd Shift Productivity by Team Member" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="ViewbyTM.aspx.vb" Inherits="FCS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

 <p class="floatRight">
    <asp:HyperLink ID="backToShiftLink" runat="server" NavigateUrl="~/FCS/2nd/Shift.aspx">2nd Shift Home</asp:HyperLink>
    </p>
 
    <h2>
        Full Case Stock 2nd Shift Productivity by Team Member
    </h2>

  <div id="SearchReports">
     
        <asp:Label ID="TmListLabel" runat="server">TM:</asp:Label>
       <asp:DropDownList ID="TMList" runat="server"  DataTextField="FullName" DataValueField="EmployeeID"  DataSourceID="TMListDataSource" AppendDataBoundItems="true"  >
         <asp:ListItem Value="All" Selected="True">All </asp:ListItem>
           </asp:DropDownList>

            <asp:DropDownList ID="TMListbyName" runat="server" DataTextField="FullName" DataValueField="EmployeeID"   Visible="false"  AppendDataBoundItems="false">
       
           </asp:DropDownList>

           <asp:Label ID="FunctionLabel" runat="server">Function: &nbsp;</asp:Label>
        <asp:DropDownList ID="FunctList" runat="server"   DataTextField="Description" DataValueField="Function"  DataSourceID="FunctListDataSource" AppendDataBoundItems="true">
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
              
                 <asp:ValidationSummary ID="ValidationSummary"  runat="server" />
      <asp:Label ID="TMNotFound" runat="server" Text="" CssClass="bold"></asp:Label>  </div>

<div class="AllRepeat">
        <asp:Repeater ID="DistTM" runat="server" DataSourceID="TMProductofDate" OnItemDataBound="DistTM_ItemDataBound">
            <ItemTemplate><asp:Label ID="NoDataDistTM" runat="server" Text='No Data to Display' Visible="false" CssClass="Red floatLeft"/>
                <table id="TMLabelTable"  class="PageBreak">
                    <tr>
                        <td class="left">
                         
                         <h3 class="Red negMarg">
                                <asp:Label ID="EmployeeIDLabel" runat="server" Text='<%# Container.DataItem("FullName")%>' /></h3>
                            <asp:HiddenField ID="HiddenTMField" runat="Server" Value='<%# Container.DataItem("EmployeeID") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Repeater ID="DistFunct" runat="server" OnItemDataBound="DistFunct_ItemDataBound"
                                Visible="false" ViewStateMode="Disabled">
                                <HeaderTemplate>
                                    <table id="DistFunctTable"  class="AvoidPageBreak">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="left">
                                            <asp:Label ID="FunctLabel" runat="server" Text='<%# Container.DataItem("Description")%>' />
                                            <asp:HiddenField ID="HiddenFunctionField" runat="Server" Value='<%# Container.DataItem("Function") %>' />
                                        </td>
                                        <td class="right">
                                            <asp:Label runat="server" ID="GoalRateLabel">Goal Rate:</asp:Label>
                                            <asp:Label ID="RateLabel" runat="server" Text='<%# Container.DataItem("Rate")%>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="TMProd" runat="server" AutoGenerateColumns="False" OnRowDataBound="TMProd_RowDataBound"
                                                ShowFooter="True" DataKeyNames="DTE,Function" ViewStateMode="Disabled" CssClass="DistFunctTable">
                                               <AlternatingRowStyle BackColor="#f2f2f2" />
                                                <Columns>
                                                    <asp:BoundField DataField="DTE" HeaderText="" SortExpression="DTE" DataFormatString="{0:MM/dd/yy}" />
                                                   
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                                                    <asp:BoundField DataField="FunctHours" HeaderText="Hours" SortExpression="FunctHours"
                                                        DataFormatString="{0:F2}" />
                                                    <asp:BoundField DataField="ActualRate" HeaderText="Act. Rate" SortExpression="ActualRate"
                                                        DataFormatString="{0:F2}" />
                                                    <asp:BoundField DataField="GoalHours" HeaderText="Goal Hours" SortExpression="GoalHours"
                                                        DataFormatString="{0:F2}" />
                                                    <asp:BoundField DataField="PercentAvg" HeaderText="% of Goal" SortExpression="PercentAvg"
                                                        DataFormatString="{0:P1}" />
                                                   
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    There is no data to display
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                           
                                        <p>&nbsp;</p></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table></FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>


      
        <asp:HiddenField ID="OuterTMField" runat="server" Visible="false" />
        <asp:TextBox ID="TMSelected" runat="server" Text="" Visible="false"></asp:TextBox>
      
    </div>

   <asp:Label ID="NoData" runat="server" Text="No Data to Display" Visible="false" CssClass="Red"></asp:Label>
  
 
       
     <asp:SqlDataSource ID="TMListDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT EmployeeID,  (FirstName + ' ' + LastName) AS FullName, ShiftID, DepartmentID FROM TMs WHERE ((ShiftID = 2) AND (DepartmentID = 3)) ORDER BY EmployeeID">
     </asp:SqlDataSource>
       <asp:SqlDataSource ID="FunctListDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT Function, Description FROM Functions WHERE Functions.Department =3 Order by Description">
     </asp:SqlDataSource>
      <asp:SqlDataSource ID="TMProductofDate" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand=" SELECT DISTINCT PROD.EmployeeID, ([TMS].[FirstName] +' '+[TMS].[LastName]) AS FullName FROM TMs INNER JOIN PROD ON TMs.EmployeeID = PROD.EmployeeID
WHERE (((PROD.DTE) Between ? And ?) AND ((TMs.DepartmentID)=3) AND ((TMs.ShiftID)=2))">
 
 
 <SelectParameters>
                <asp:ControlParameter ControlID="txtStartDate" DbType="Date" Name="?" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="txtEndDate" DbType="Date" Name="?" 
                PropertyName="Text" />
             
        </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="TMProductofDateOneFunct" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand=" SELECT DISTINCT PROD.EmployeeID, ([TMS].[FirstName] +' '+[TMS].[LastName]) AS FullName FROM TMs INNER JOIN PROD ON TMs.EmployeeID = PROD.EmployeeID
WHERE (((PROD.DTE) Between ? And ?) AND ((TMs.DepartmentID)=3) AND ((TMs.ShiftID)=2)) AND(PROD.Function =? )">
 
 
 <SelectParameters>
                <asp:ControlParameter ControlID="txtStartDate" DbType="Date" Name="?" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="txtEndDate" DbType="Date" Name="?" 
                PropertyName="Text" />
             <asp:ControlParameter ControlID="FunctList" Name="?" PropertyName="SelectedValue" DbType="String" />
        </SelectParameters>
        </asp:SqlDataSource>

         <asp:SqlDataSource ID="TMListName" runat="server" 
              ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
              ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
              SelectCommand="SELECT EmployeeID, (FirstName + ' ' + LastName) AS FullName FROM TMs WHERE (EmployeeID = ?)">
        <SelectParameters>
            <asp:ControlParameter ControlID="TMList" Name="?" 
                PropertyName="SelectedValue" />
        </SelectParameters>
          </asp:SqlDataSource>



          <asp:SqlDataSource ID="TMListByLastName" runat="server" 
              ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
              ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
              SelectCommand="SELECT EmployeeID,   (FirstName + ' ' + LastName) AS FullName FROM TMs WHERE (EmployeeID = ?)">
              <SelectParameters>
                  <asp:ControlParameter ControlID="TMListbyName" Name="?" 
                      PropertyName="SelectedValue" />
              </SelectParameters>
          </asp:SqlDataSource>

</asp:Content>

