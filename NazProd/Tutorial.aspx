<%@ Page Title="The Nazareth RFC Productivity Application Tutorial!" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false"  %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<p class="HomePageHeading">The Nazareth RFC Productivity Application Tutorial</p>


    <h3 style="margin-bottom: 5px;">For Each Day With Team Member Productivity:</h3>
    <ul class="HomePageList">
        <li><a href="Tutorial/VerifyTMs.aspx">Verify the team members</a> that worked in that department and that shift for the 
            day you want to import and edit as necessary</li>
        <li>After the correct team members are listed, <a href="Tutorial/ImportProd.aspx">Import the day&#39;s productivity</a></li>
        <li><a href="Tutorial/EditProd.aspx">Edit the productivity data </a> by adding the hours worked</li>
        <li><a href="Tutorial/AddProd.aspx">Add any additional non-AS400 productivity</a></li>
     </ul>
     

    
   <h3 style="margin-bottom: 5px;">View Reports Once Productivity has been added :</h3>
   
               <ul class="HomePageList">
                   <li><a href="Tutorial/ViewSummaries.aspx">View Productivity Summaries</a> for the previous day, week and month on each 
                       department and shift&#39;s pages.</li>
                    <li><a href="Tutorial/ViewbyTM.aspx">View Productivity for a Team Member</a> for a specific day or range of dates</li>
                    <li><a href="Tutorial/ViewbyFunct.aspx">View Productivity for a shift for all Team members</a> for a specific function or all functions</li>
                    <li><a href="Tutorial/ViewSummarywithDays.aspx">View a Productivity summary for a specific day or Range of Dates with each function's totals</a></li>
                    <li><a href="Tutorial/ViewSummaryTotallingDays.aspx">View a Productivity summary for a specific day or Range of Dates with each day's totals for each function</a></li>
                </ul>
              
   

    
    <h3 style="margin-bottom: 5px;">Edit Function Pages for each Department:</h3>
     <ul class="HomePageList">
                    <li><a href="Tutorial/EditFunctions.aspx#View">View the Productivity Functions tracked</a></li>
                    <li><a href="Tutorial/EditFunctions.aspx#Modify">Modify </a>the description or rate of Productivity Functions tracked</li>
                    <li><a href="Tutorial/EditFunctions.aspx#Add">Add </a>additional functions to be tracked</li>
                    <li><a href="Tutorial/EditFunctions.aspx#Delete">Delete</a> functions if needed <em>(only if never included in any productivity data)</em> </li>
                </ul>

   <h3 style="margin-bottom: 5px;">Team Members Page:</h3>
     <ul class="HomePageList">
                    <li><a href="Tutorial/TeamMembers.aspx#View">View all current Team Members</a> for all Departments</li>
                    <li><a href="Tutorial/TeamMembers.aspx#Search">Search for a Team Member</a> by 
                        last name or AS400 ID</li>
                    <li><a href="Tutorial/TeamMembers.aspx#Edit">Edit</a> a Team Member Details</li>
                    <li><a href="Tutorial/TeamMembers.aspx#Delete">Delete</a> a Team Member if needed <em>(only if never included in any productivity data)</em> </li>
                    <li><a href="Tutorial/TeamMembers.aspx#Add">Add</a> a Team Member</li>
                </ul>
</asp:Content>