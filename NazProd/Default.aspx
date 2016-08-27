<%@ Page Title="Walgreen Nazareth RFC Productivity" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false"  %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  
   <div class="Lists">
     <h3>For Each Day With Team Member Productivity:</h3>
    <ul>
        <li><a href="Tutorial/VerifyTMs.aspx">Verify the team members</a> that worked in that department and that shift for the 
            day you want to import and edit as necessary</li>
        <li><a href="Tutorial/ImportProd.aspx">Import the day&#39;s productivity</a> (<em>Always after the correct team members are verified</em>)</li>
        <li><a href="Tutorial/EditProd.aspx">Edit the productivity data </a> by adding the hours worked</li>
        
     </ul>
         </div>

   <div class="Lists">
  
   <h3>View Productivity Reports on each department and shift&#39;s pages :</h3>
                  <ul>
                   <li><a href="Tutorial/ViewSummaries.aspx">Shift Summaries</a> </li>
                    <li><a href="Tutorial/ViewbyTM.aspx">By Team Member</a> </li>
                    <li><a href="Tutorial/ViewbyFunct.aspx">By a specific function or all functions</a> </li>
                    <li><a href="Tutorial/ViewSummarywithDays.aspx">Summary of each function's totals for a date range</a></li>
                    <li><a href="Tutorial/ViewSummaryTotallingDays.aspx">Summary of each function total for each day </a></li>
                </ul>
                </div>

   
        <div class="Lists">
    <h3>Edit Function Pages for each Department:</h3>
     <ul>
                    <li><a href="Tutorial/EditFunctions.aspx#View">View the Productivity Functions tracked</a></li>
                    <li><a href="Tutorial/EditFunctions.aspx#Modify">Modify </a>the description or rate of Productivity Functions tracked</li>
                    <li><a href="Tutorial/EditFunctions.aspx#Add">Add </a>additional functions to be tracked</li>
                    <li><a href="Tutorial/EditFunctions.aspx#Delete">Delete</a> functions if needed <em>(only if never included in any productivity data)</em> </li>
                </ul></div>
            
         <div class="Lists">
        
   <h3>Team Members Page:</h3>
     <ul >
                    <li><a href="Tutorial/TeamMembers.aspx#View">View all current Team Members</a> for all Departments</li>
                    <li><a href="Tutorial/TeamMembers.aspx#Search">Search for a Team Member</a> by 
                        last name or AS400 ID</li>
                    <li><a href="Tutorial/TeamMembers.aspx#Edit">Edit</a> a Team Member Details</li>
                    <li><a href="Tutorial/TeamMembers.aspx#Delete">Delete</a> a Team Member if needed <em>(only if never included in any productivity data)</em> </li>
                    <li><a href="Tutorial/TeamMembers.aspx#Add">Add</a> a Team Member</li>
                </ul>
                </div>
              
</asp:Content>