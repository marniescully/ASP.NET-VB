<%@ Page Title="How to View and Edit Department Functions" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false"  %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<div id="sideNav">

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

<h2>How to View and Edit Department Functions</h2>
<div id="LeftColumn" class="Tutorial">
    <h3>View the Productivity Functions Tracked</h3>
    <ol>
       <li >From your any page in the application move your mouse over the name of your department and then Click
        <span class="StrongEmphasis">Edit Functions</span>  <br />
            <img alt="Click Edit Functions" src="images/ClickEditFunct.jpg" />
        </li>
        <li >The List of productivity functions will be displayed. <br />
            <img  alt="Function List" src="images/FunctionList.jpg" />
        </li>
     </ol>
     
     <h3>Modifiy the description or rate of the Productivity Functions Tracked</h3>

    <ol>
         <li>Click Edit to modify. <br />
         <img  alt="Click Edit Functions" src="images/ModifyFunct.jpg" />        
        </li>

        <li >The Edit Function form is displayed. <br />
            <img  alt="Edit Function Form" src="images/EditFunctForm.jpg" />
        </li>
        <li>Change the rate of the Function and click <span class="StrongEmphasis">Update</span> or <span class="StrongEmphasis">Cancel</span> to return to the list of functions.</li>
     </ol>

   

   <h3>Delete functions if needed</h3>
    <ol>
         <li>Click <span class="StrongEmphasis">Delete</span>  <em>(only if never included in any productivity data)</em> to return to the list of functions.</li>
        
     </ol>
  

   <h3>Add additional functions </h3>
    <ol>
         <li>To Add a new function to the list click <span class="StrongEmphasis">Add</span>
         <img alt="Add Function" src="images/AddFunct.jpg" /> </li>
        <li>The Add Function form will be displayed. <img alt="Add Function Form" src="images/AddFunctForm.jpg"/> </li>
        <li>Fill in the Function abbreviation, the rate and the description and click <span class="StrongEmphasis">Insert </span>or click <span class="StrongEmphasis">Cancel</span> to return to the list of functions </li>
     </ol>
     </div>
</asp:Content>