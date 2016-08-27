<%@ Page Title="Utility" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false"
    CodeFile="Utility.aspx.vb" Inherits="Utility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<div id="sideNav" >
        <h3>
           Utility </h3>
        
        <h3>
            Reports</h3>
        <ul>
            <li>
                <asp:LinkButton ID="ViewByTM" runat="server" PostBackUrl="~/Utility/ViewbyTM.aspx">View By Team Member</asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="ViewByFunction" runat="server" PostBackUrl="~/Utility/ViewbyFunct.aspx">View By Function</asp:LinkButton></li>
        </ul>
    </div>
    <div id="LeftColumn">
        <h2>
            <asp:Label ID="DeptLabel" runat="server" Text="Utility"></asp:Label></h2>
            <h6><asp:Label ID="TMCountLabel" runat="server" Text=""></asp:Label></h6>
        
        <asp:GridView ID="ShiftTM" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" DataKeyNames="EmployeeID" DataSourceID="ShiftTMDataSource"
            PageSize="20">
            <AlternatingRowStyle BackColor="#f2f2f2" />
            <Columns>
                <asp:BoundField DataField="EmployeeID" HeaderText="AS400 ID" ReadOnly="True" SortExpression="EmployeeID" />
                <asp:BoundField DataField="FirstName" HeaderText="First Name" ReadOnly="True" SortExpression="FirstName">
                    <ControlStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="LastName" HeaderText="Last Name" ReadOnly="True" SortExpression="LastName">
                    <ControlStyle Width="100px" />
                </asp:BoundField>
                
                <asp:TemplateField HeaderText="Dept" SortExpression="Dept">
                    <ControlStyle Width="100px" />
                    <EditItemTemplate>
                        <asp:DropDownList ID="DDLDepartment" runat="server" DataSourceID="DeptDataSource"
                            DataTextField="Dept" DataValueField="DepartmentID" SelectedValue='<%# Bind("DepartmentID") %>'
                            AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Dept") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Shift" SortExpression="Shift">
                    <ControlStyle Width="60px" />
                    <EditItemTemplate>
                        <asp:DropDownList ID="DDLShift" runat="server" DataSourceID="ShiftDataSource" DataTextField="Shift"
                            DataValueField="ShiftID" SelectedValue='<%# Bind("ShiftID") %>' AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Shift") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CheckBoxField DataField="FarmIn" HeaderText="Farm In" SortExpression="FarmIn">
                    <ControlStyle Width="15px" />
                </asp:CheckBoxField>
                <asp:CommandField ShowEditButton="True" />
            </Columns>
            <EmptyDataTemplate>
                There are no current Team Members for that Department and Shift.
            </EmptyDataTemplate>
        </asp:GridView>
    </div>

    
    <asp:SqlDataSource ID="ShiftTMDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT TMs.EmployeeID, TMs.ShiftID, TMs.FirstName, TMs.DepartmentID, TMs.LastName, TMs.FarmIn, TMs.EmployeeNum, Departments.Dept, Shifts.Shift
FROM Shifts INNER JOIN (Departments INNER JOIN TMs ON Departments.DepartmentID = TMs.DepartmentID) ON Shifts.ShiftID = TMs.ShiftID
WHERE (((TMs.DepartmentID)=5))
ORDER BY TMs.EmployeeID"
        
        UpdateCommand="UPDATE TMs SET DepartmentID=?, ShiftID=?, FarmIn=? WHERE EmployeeID=?">
        <UpdateParameters>
            <asp:Parameter Name="EmployeeID" DbType="String" />
            <asp:Parameter Name="DepartmentID" DbType="Int32" />
            <asp:Parameter Name="ShiftID" DbType="Int32" />
            <asp:Parameter Name="FarmIn" DbType="Boolean" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="DeptDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT DepartmentID, Dept FROM Departments ">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="ShiftDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT ShiftID,Shift FROM Shifts">
    </asp:SqlDataSource>
</asp:Content>
