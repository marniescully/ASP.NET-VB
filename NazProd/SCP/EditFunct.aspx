<%@ Page Title="Split Case Pick Functions" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="EditFunct.aspx.vb" Inherits="SCP_EditSCPFunct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

 <asp:Panel ID="SideNavigation" runat="server" >

   <div id="sideNav">

 <h3>Shifts</h3>
    <ul>
    <li><asp:HyperLink ID="FirstShift" runat="server" NavigateUrl="~/SCP/1st/Shift.aspx">1st Shift</asp:HyperLink></li>
    <li><asp:HyperLink ID="SecondShift" runat="server" NavigateUrl="~/SCP/2nd/Shift.aspx">2nd Shift</asp:HyperLink></li>

    </ul>
   
    </div>

   </asp:Panel>
 
    <div id="LeftColumn"> 
<h2><asp:Label ID="FunctionLabel" runat="server" Text="Split Case Pick Functions"></asp:Label></h2>

    <asp:GridView ID="SCPFunct" runat="server" AllowSorting="True" 
        AutoGenerateColumns="False" DataKeyNames="Function" DataSourceID="SCPFunctions">
            <AlternatingRowStyle BackColor="#f2f2f2"  />
        <Columns>
            <asp:TemplateField HeaderText="" SortExpression="Function">
                
                 <ItemTemplate>
                    <asp:Label ID="FunctLabel2" runat="server" Text='<%# Bind("Function") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Rate" SortExpression="Rate">
                
                             <ItemTemplate>
                    <asp:Label ID="Ratelabel" runat="server" Text='<%# Bind("Rate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="Description">
              
               
                <ItemTemplate>
                    <asp:Label ID="DescriptLabel" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

           <asp:CommandField ShowSelectButton="True" SelectText="Edit" /> 
            <asp:ButtonField Text="Add" CommandName="Add" />
           
            
        </Columns>

 
      
    </asp:GridView></div>
<div id="Form" class="SmallForm">
<asp:DetailsView ID="EditFunct" runat="server" AutoGenerateRows="False" 
            DataSourceID="EditFunctDataSource"  Visible="False" DefaultMode="Edit" OnItemcommand="EditFunct_ItemCommand" OnItemUpdated="EditFunct_ItemUpdated" DataKeyNames="Function">
           
           <EmptyDataTemplate>
            No Function selected
        </EmptyDataTemplate>
            <Fields>
                <asp:BoundField DataField="Function" HeaderText="" ReadOnly="True" 
                    SortExpression="Function"/>
                <asp:TemplateField HeaderText="Rate:" SortExpression="Rate">
                    <EditItemTemplate>
                        <asp:TextBox ID="EditRate" runat="server" Text='<%# Bind("Rate") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="The rate is a required field"
                            ControlToValidate="EditRate" Display="None"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="The rate must be a number."
                            Display="None" ControlToValidate="EditRate" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                        <asp:CompareValidator ID="RateValidator" runat="server" ErrorMessage="Amount must be greater than zero"
                            ControlToValidate="EditRate" Operator="GreaterThan" ValueToCompare="0.1" Display="None"></asp:CompareValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="EditDescription" runat="server" Text='<%# Bind("Description") %>' Columns="20" ReadOnly="True"></asp:TextBox>
                    </EditItemTemplate>
                    
                </asp:TemplateField>
                   <asp:ButtonField CommandName="Update" Text="Update" CausesValidation="True" />
                     <asp:ButtonField CommandName="CancelThis" Text="Cancel" />
                      <asp:ButtonField CommandName="DeleteThis" Text="Delete" />
            </Fields>
        </asp:DetailsView>
        
        

    <asp:DetailsView ID="AddFunction" runat="server" DefaultMode="Insert" AutoGenerateRows="False" DataKeyNames="Function" Visible="false" OnItemInserting="AddFunction_ItemInserting" OnModeChanging="AddFunction_ModeChanging">
                <Fields>
                    <asp:TemplateField HeaderText="Function:" SortExpression="Function">
                      <InsertItemTemplate>
                      <asp:TextBox ID="DetFunction" runat="server" Text='<%# Bind("Function") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="The function is a Required field." ControlToValidate="DetFunction" Display="None">
                       </asp:RequiredFieldValidator> 
                      </InsertItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Rate:" SortExpression="Rate">
                    <InsertItemTemplate>
                        <asp:TextBox ID="DetRate" runat="server" Text='<%# Bind("Rate") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="The rate is a required field"
                            ControlToValidate="DetRate" Display="None"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="The rate must be a number."
                            Display="None" ControlToValidate="DetRate" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                        <asp:CompareValidator ID="AddRateValidator" runat="server" ErrorMessage="Amount must be greater than zero"
                            ControlToValidate="DetRate" Operator="GreaterThan" ValueToCompare="0.1" Display="None"></asp:CompareValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description:" SortExpression="Description">
                       
                        <InsertItemTemplate>
                            <asp:TextBox ID="DetDescrip" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        
                    </asp:TemplateField>
                    <asp:ButtonField CommandName="Insert" Text="Insert" CausesValidation="True" />
                     
                    <asp:ButtonField CommandName="Cancel" Text="Cancel" />
                    
                 </Fields>
            </asp:DetailsView>

    
   <p><asp:ValidationSummary ID="ValidationSummary1" runat="server" /></p></div>
   
   
 <asp:SqlDataSource ID="SCPFunctions" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT [Function], [Rate], [Description] FROM [Functions] WHERE Functions.Department = 8 ORDER BY [Function]"  >
          
                
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="EditFunctDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="SELECT [Function], [Rate], [Description] FROM [Functions] WHERE ([Function] = ?) AND Functions.Department = 8" UpdateCommand="UPDATE [Functions] SET [Rate] = ?, [Description] = ? WHERE [Function] = ? AND Functions.Department = 8">
          
            <SelectParameters>
                <asp:ControlParameter ControlID="SCPFunct" Name="?" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
           
            <UpdateParameters>
            <asp:Parameter Name="Rate" DbType="Decimal" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="Function" Type="String" />
        </UpdateParameters>
         
        </asp:SqlDataSource>
       
</asp:Content>

