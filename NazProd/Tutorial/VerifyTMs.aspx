<%@ Page Title="How to Verify the Correct Team Members " Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false"  %>

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
    <h2>How to Verify the Correct Team Members</h2>

<div id="#LeftColumn" class="Tutorial">
<p>Before you import productivity, it is <span class="StrongEmphasis">crucial</span> that you verify the
 <span class="StrongEmphasis">exact</span>
 list of team members that worked in the department on the specific shift on that day. 
 Only the team members listed will have productivity imported. 
 If a team member is a farm-in you still need to list them as part of the department and shift,
  but just mark them as a farm-in.</p>
     
      <h3>View all Team Members currently listed:</h3>
    <ol >
        <li>View your department's shift page by clicking link at top<br />
            <img alt="Click Shift Link" src="images/clickShiftLink.jpg" />
        </li>
        <li >Click <span class="StrongEmphasis">View Team Members</span> Under the Tasks List<br />
        <img alt="Click View TMS" src="images/clickViewTMs.jpg" />
        </li>
        <li>The list of team members currently in the department and shift will be displayed.
        <br /><img alt="List TMS" src="images/ListTMs.jpg" /></li>
      
         <li >Click <span class="StrongEmphasis"> Edit </span> next to any Team Member
          name to change their department information. <br />Click <span class="StrongEmphasis">Update </span> 
          when complete or <span class="StrongEmphasis">Cancel</span> to return to previous view.
        <br /><img  alt="Edit TMS" src="images/EditTMShift.jpg"/>
        </li>
      <li >If Team Member is not listed that needs productivity data imported, click the 
          <a href="../TMs.aspx">Team Members</a> link at the top to add the team member
           to your department and shift. <br /><img  alt="Click TMS" src="images/ClickTeamMembers.jpg"/><br />
        </li>
     </ol>
     </div>
</asp:Content>