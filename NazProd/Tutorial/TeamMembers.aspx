<%@ Page Title="How to View, Search and Edit Team Members" Language="VB" MasterPageFile="~/Site.Master"
    AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="sideNav">
        <h3>
            How To...</h3>
        <ul>
            <li>
                <asp:HyperLink ID="EditFunctions" runat="server" NavigateUrl="EditFunctions.aspx"
                    onmouseover="window.status='How to Edit Functions';return true;" onmouseout="window.status=''; return true;">Edit Functions</asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="EditProd" runat="server" NavigateUrl="EditProd.aspx" onmouseover="window.status='How to Edit Productivity';return true;"
                    onmouseout="window.status=''; return true;">Edit Productivity</asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="ImportProd" runat="server" NavigateUrl="ImportProd.aspx" onmouseover="window.status='How to Import Productivity';return true;"
                    onmouseout="window.status=''; return true;">Import Productivity</asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="TeamMembers" runat="server" NavigateUrl="TeamMembers.aspx" onmouseover="window.status='How to View, Edit and Search for Team Members';return true;"
                    onmouseout="window.status=''; return true;">View, Edit and Search for Team Members</asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="VerifyTMs" runat="server" NavigateUrl="VerifyTMs.aspx" onmouseover="window.status='How to Verify Team Members';return true;"
                    onmouseout="window.status=''; return true;">Verify Team Members</asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="ViewProdSumm" runat="server" NavigateUrl="ViewSummaries.aspx"
                    onmouseover="window.status='How to View Productivity Summaries';return true;"
                    onmouseout="window.status=''; return true;">View Productivity Summaries</asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="ViewByTM" runat="server" NavigateUrl="ViewbyTM.aspx" onmouseover="window.status='How to View Productivity by Team Member';return true;"
                    onmouseout="window.status=''; return true;">View Productivity by Team Member</asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="ViewbyFunct" runat="server" NavigateUrl="ViewbyFunct.aspx" onmouseover="window.status='How to View Productivity by Function';return true;"
                    onmouseout="window.status=''; return true;">View Productivity by Function</asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="ViewbySummDateRange" runat="server" NavigateUrl="ViewSummarywithDays.aspx"
                    onmouseover="window.status='How to View Summary by Date Range';return true;"
                    onmouseout="window.status=''; return true;">View Summary by Date Range</asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="ViewSummarybyFunct" runat="server" NavigateUrl="ViewSummaryTotallingDays.aspx"
                    onmouseover="window.status='How to View Summary by Function';return true;" onmouseout="window.status=''; return true;">View Summary by Function</asp:HyperLink></li>
        </ul>
    </div>
    <h2>
        How to View, Search and Edit Team Members</h2>
    <div class="Tutorial" id="LeftColumn">
        <h3>
            View all current Team Members</h3>
        <ol>
            <li>From your any page in the application Click <a href="../TMs.aspx">Team Members</a>
                <br />
                <img alt="Click TMS" src="images/ClickTeamMembers.jpg" />
            </li>
            <li>The List of Team Members will be displayed as well as the number of team members
                of all shifts and departments combined.
                <br />
                <img alt="TM List" src="images/TMList.jpg" />
            </li>
        </ol>
        <h3>
            Search for a current Team Member</h3>
        <ol>
            <li>You can search for a team member by typing their last name in the first field and
                clicking "By Last Name" </li>
            <li>You can search for a team member by typing their AS400 User ID in the second field
                and clicking "By AS400 ID"</li>
        </ol>
        <h3>
            Edit a current Team Member's details</h3>
        <ol>
            <li>Click Edit next to any team members name (either on the main list or from a search's
                result) to edit the details of a team member.
                <br />
                <img alt="Click Edit a TM" src="images/ClickEditaTM.jpg" /></li>
            <li>The Edit a Team Member form is then displayed.
                <br />
                <img alt="Edit TM form" src="images/EditaTMForm.jpg" /></li>
            <li>Modify any of the fields and click <span class="StrongEmphasis">Update</span> or <span class="StrongEmphasis">
                Cancel</span> to return to the Team Member list.</li>
        </ol>
        <h3>
            <a name="Delete"></a>Delete a current Team Member
        </h3>
        <ol>
            <li>Click <span class="StrongEmphasis">Delete</span> on the Edit Team Member form to delete a Team
                Member <em>(only if never included in any productivity data)</em></li>
        </ol>
        <h3>
            Add a current Team Member
        </h3>
        <ol>
            <li>From the Team Member Main page click "Add A Team Member"</li>
            <li>The Add a Team Member form is then displayed.
                <br />
                <img alt="Edit TM form" src="images/AddATM.jpg" /></li>
            <li>Fill in the necessary first three fields and any other information available and
                click <span class="StrongEmphasis">Insert</span> or <span class="StrongEmphasis">Cancel</span> to return
                to the Team Member list.</li>
        </ol>
    </div>
</asp:Content>
