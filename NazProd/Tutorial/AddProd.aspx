<%@ Page Title="How to Add Team Member Productivity" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false"  %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<div id="sideNav" class="sideNav">

 <h3>How To...</h3>
    <ul>
        <li><asp:HyperLink ID="EditFunctions" runat="server" NavigateUrl="EditFunctions.aspx" onmouseover="window.status='How to Edit Functions';return true;" onmouseout ="window.status=''; return true;">Edit Functions</asp:HyperLink></li>
        
        <li><asp:HyperLink ID="EditProd" runat="server" NavigateUrl="EditProd.aspx" onmouseover="window.status='How to Edit Productivity';return true;" onmouseout ="window.status=''; return true;">Edit Productivity</asp:HyperLink></li>

         <li><asp:HyperLink ID="ImportProd" runat="server" NavigateUrl="ImportProd.aspx" onmouseover="window.status='How to Import Productivity';return true;" onmouseout ="window.status=''; return true;">Import Productivity</asp:HyperLink></li>
          <li><asp:HyperLink ID="TeamMembers" runat="server" NavigateUrl="TeamMembers.aspx" onmouseover="window.status='How to View, Edit and Search for Team Members';return true;" onmouseout ="window.status=''; return true;">View, Edit and Search for Team Members</asp:HyperLink></li>

            <li><asp:HyperLink ID="VerifyTMs" runat="server" NavigateUrl="VerifyTMs.aspx" onmouseover="window.status='How to Verify Team Members';return true;" onmouseout ="window.status=''; return true;">Verify Team Members</asp:HyperLink></li>

             <li><asp:HyperLink ID="ViewProdSumm" runat="server" NavigateUrl="ViewSummaries.aspx" onmouseover="window.status='How to View Productivity Summaries';return true;" onmouseout ="window.status=''; return true;">View Productivity Summaries</asp:HyperLink></li>

 <li><asp:HyperLink ID="ViewByTM" runat="server" NavigateUrl="ViewbyTM.aspx" onmouseover="window.status='How to View Productivity by Team Member';return true;" onmouseout ="window.status=''; return true;">View Productivity by Team Member</asp:HyperLink></li>
 <li><asp:HyperLink ID="ViewbyFunct" runat="server" NavigateUrl="ViewbyFunct.aspx" onmouseover="window.status='How to View Productivity by Function';return true;" onmouseout ="window.status=''; return true;">View Productivity by Function</asp:HyperLink></li>
 <li><asp:HyperLink ID="ViewbySummDateRange" runat="server" NavigateUrl="ViewSummarywithDays.aspx" onmouseover="window.status='How to View Summary by Date Range';return true;" onmouseout ="window.status=''; return true;">View Summary by Date Range</asp:HyperLink></li>

  <li><asp:HyperLink ID="ViewSummarybyFunct" runat="server" NavigateUrl="ViewSummaryTotallingDays.aspx" onmouseover="window.status='How to View Summary by Function';return true;" onmouseout ="window.status=''; return true;">View Summary by Function</asp:HyperLink></li>

       </ul>
    </div>

<p class="HomePageHeading">How to Add Team Member Productivity</p>

<div class="TMList" id="TMList">
    <p><em>After</em> Team Member productivity has been imported, you can <em>then</em> add additional non-AS400 productivity data for that day if necessary. <em>You must complete the import first</em>. </p>
   
    <h3 style="margin-bottom: 5px;">For Each day that you Add non-AS400 Productivity:</h3>
   <ol>
        <li style="margin-top:10px;">From your department's shift page click <span class="Red">Add Productivity</span> <br />
            <img style="margin:10px auto;" alt="Click Add Link" src="images/ClickAdd.jpg" />
        </li>
        <li style="margin-top:10px;">Your department's shift's Add page will be displayed<br />
            <img style="margin:10px auto;" alt="Add Page" src="images/AddForm.jpg" />
        </li>
       <li style="margin-top:10px;">Choose the date that you'd like to Add by clicking in the date field and selecting a date from the calendar that appears.<br />
            <img style="margin:10px auto;" alt="Choose Add Date" src="images/ChooseAddDate.jpg" />
        </li>
         <li style="margin-top:10px;">Choose the team member who's productivity you are adding<br />
            <img style="margin:10px auto;" alt="Edit Form" src="images/AddTMList.jpg" />

        </li>
        <li style="margin-top:10px;">Choose the function for the productivity you are adding<br />
            <img style="margin:10px auto;" alt="Edit Form" src="images/AddFunctList.jpg" />

        </li>

        <li style="margin-top:10px;">Fill the rest of the form fields as necessary and choose <span class="Red"> Add </span>to add the data, <span class="Red"> Reset </span> to clear the form or <span class="Red"> Cancel </span> to return to the shift's page. Repeat as needed for each function or Team Member.<br />
            

        </li>
      
       
        
     </ol>
    
  </div>
</asp:Content>