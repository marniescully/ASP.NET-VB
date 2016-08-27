<%@ Page Title="Edit Shipping 2nd Shift Productivity" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Edit.aspx.vb" Inherits="SHIP_2nd_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<div id="sideNav" >

 <h3>Tasks</h3>
    <ul>
        <li><asp:HyperLink ID="backToShift" runat="server" NavigateUrl="~/SHIP/2nd/Shift.aspx" onmouseover="window.status='2nd Shift Home';return true;" onmouseout ="window.status=''; return true;">2nd Shift Home</asp:HyperLink></li>
    <li><asp:LinkButton ID="ImportProd" runat="server" PostBackUrl="~/SHIP/2nd/ImportTMProd.aspx"  onmouseover="window.status='Import Productivity';return true;" onmouseout ="window.status=''; return true;" CausesValidation="false">Import Productivity</asp:LinkButton><ajaxToolkit:ConfirmButtonExtender ID="ImportConfirmButton" runat="server" TargetControlID="ImportProd" ConfirmText="Click OK only if you have verified the Team Members that worked on the Import Date.">         
         </ajaxToolkit:ConfirmButtonExtender>
     </li>
    
      
       </ul>
   

    <h3>Reports</h3>
        <ul>   
    <li><asp:HyperLink ID="ViewByTM" runat="server" NavigateUrl="~/SHIP/2nd/ViewbyTM.aspx" onmouseover="window.status='View By Team Member';return true;" onmouseout ="window.status=''; return true;">View by Team Member</asp:HyperLink> </li>
    <li><asp:HyperLink ID="ViewByFunction" runat="server" NavigateUrl="~/SHIP/2nd/ViewbyFunct.aspx" onmouseover="window.status='View By Function';return true;" onmouseout ="window.status=''; return true;">View by Function</asp:HyperLink> </li>
    <li><asp:HyperLink ID="SummaryDateRange" runat="server" NavigateUrl="~/SHIP/2nd/SummDateRange.aspx" onmouseover="window.status='Summary of Date Range';return true;" onmouseout ="window.status=''; return true;">Summary of Date Range</asp:HyperLink> </li>
     <li><asp:LinkButton ID="SummbyFunct" runat="server" PostBackUrl="~/SHIP/2nd/SummbyFunct.aspx" onmouseover="window.status='Summary by Function';return true;" onmouseout ="window.status=''; return true;" CausesValidation="false">Summary by Function</asp:LinkButton> 
         
           
         
            </li>
    </ul>
       
    </div>

   <div class="Form" id="Form">
    <div id="HeadDate">
       <h2><asp:Label ID="EditLabel" runat="server" Text="Edit Shipping 2nd Shift Productivity"></asp:Label></h2>
      <asp:TextBox ID="DTE" runat="server"  AutoPostBack="True"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="DTE_CalendarExtender" runat="server" 
                            TargetControlID="DTE"  >
                        </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="DteValidator" runat="server" ErrorMessage="Date is a required field" ControlToValidate="DTE" Display="Dynamic"></asp:RequiredFieldValidator>  </div>
                        

                         

        <asp:FormView ID="FVTM" runat="server" 
            DataSourceID="TMListDS" 
            EmptyDataText="There are no Team Members for this Dept. and Shift" 
         AllowPaging="True" PagerSettings-Mode="NextPrevious" PagerSettings-Position="Bottom" PagerSettings-Visible="True"  PagerStyle-BackColor="White" PagerStyle-ForeColor="White">
 
                    <ItemTemplate>
                     
                      
                        <h2> <asp:Label ID="EmployeeID" runat="server" CssClass="left" 
                             Text='<%# Bind("FullName") %>' >
                        </asp:Label> </h2>
                        
                       <asp:HiddenField  ID="EmpID" runat="server" Value='<%# Bind("EmployeeID") %>' />
                     
                        <asp:GridView ID="GVFunctProd" runat="server" DataSourceID="GVFunctDS" AutoGenerateColumns="False"   OnRowDataBound="GVFunctProd_RowDatabound" DataKeyNames="Function">
                         <Columns>
                        
                             <asp:BoundField DataField="Description" 
                SortExpression="Description" HeaderText="" >
             </asp:BoundField> 

                             <asp:TemplateField HeaderText="Hours" >
                                <ItemTemplate>
                                     <asp:TextBox ID="Hours" runat="server"  ></asp:TextBox>
                                       <asp:CompareValidator ID="HoursValidator" runat="server" ErrorMessage="Hours must be greater than zero" ControlToValidate="Hours" Operator="NotEqual" ValueToCompare="0" Display="none"></asp:CompareValidator>
                              
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Amount" >
                                 <ItemTemplate>
                                     <asp:TextBox ID="Amount" runat="server" ></asp:TextBox>
                                       <asp:CompareValidator ID="AmountValidator" runat="server" ErrorMessage="Amount must be greater than zero" ControlToValidate="Amount" Operator="NotEqual" ValueToCompare="0" Display="None"></asp:CompareValidator>
                                       <asp:CompareValidator ID="AmountValidator2" runat="server" ErrorMessage="Amount must whole number" ControlToValidate="Amount" Operator="GreaterThan" ValueToCompare="0.1" Display="None"></asp:CompareValidator>
                                   
                                 </ItemTemplate>
                             </asp:TemplateField>

                            <asp:TemplateField >
                                 <ItemTemplate>
                                     <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
                                     
                                 </ItemTemplate>
                             </asp:TemplateField>
                             
                             
                             <asp:BoundField DataField="Function" HeaderText=""> </asp:BoundField> 
                              
                                 
             </Columns>

 </asp:GridView>
                      <br />
                       
                                       
                         <div class="EditButtons">
                          
                          
                          
                             <asp:Button ID="Reset" runat="server" CausesValidation="False" 
                            CommandArgument="Reset" CommandName="Reset" Text="Reset" OnCommand="resetClick" />
                                                  <asp:Button ID="Back" runat="server" CausesValidation="False" 
                            CommandArgument="Back" CommandName="Back" Text="Back" OnCommand="backClick"   />
                                                   <asp:Button ID="Next" runat="server" CausesValidation="False" 
                            CommandArgument="Next" CommandName="Next" Text="Next" OnCommand="nextClick"   />
                            <asp:Button ID="SubmitButton" runat="server" CausesValidation="True" 
                            CommandArgument="Submit" CommandName="Submit" Text="Accept and Continue" OnCommand="submitClick"   />
                             
                            
                            </div>
                  
                   <p><asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" /></p>


                    </ItemTemplate>
                  
        </asp:FormView>
      
        


    </div>





    <asp:SqlDataSource ID="TMListDS" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT TMs.EmployeeID, (FirstName + ' ' + LastName) AS FullName
FROM TMs WHERE (((TMs.DepartmentID)=4) AND ((TMs.ShiftID)=2)) ORDER BY TMS.LastName"></asp:SqlDataSource>



  <asp:SqlDataSource ID="GVFunctDS" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT DISTINCT Function, Description FROM Functions WHERE (Department = 4) ORDER BY Description"></asp:SqlDataSource>






</asp:Content>

