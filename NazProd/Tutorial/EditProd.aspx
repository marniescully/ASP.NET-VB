<%@ Page Title="How to Edit Team Member Productivity" Language="VB" MasterPageFile="~/Site.Master"
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
        How to Edit Team Member Productivity</h2>
    <div id="LeftColumn" class="Tutorial">
        <p>
            After Team Member productivity has been imported, you then must edit the hours worked
            and any other productivity data for that day.</p>
        <h3>
            For Each day that you Edit Productivity:</h3>
        <ol>
            <li>From your department's shift page click <span class="StrongEmphasis">Edit Productivity</span>
                <br />
                <img alt="Click Edit Link" src="images/ClickEditProd.jpg" />
            </li>
            <li>Your department's shift's Edit page will be displayed<br />
                <img alt="Edit Page" src="images/EditProdPage.jpg" />
            </li>
            <li>Choose the date that you'd like to Edit by clicking in the date field and selecting
                a date from the calendar that appears.<br />
                <img alt="Choose Date" src="images/ChooseDateEdit.jpg" />
            </li>
            <li>If productivity data exists for that date, department and shift, a form with the
                first record will be displayed. After each record, click
                 <span class="StrongEmphasis">Accept and Continue</span> to save the
                information. If you click Next without clicking 
                <span class="StrongEmphasis">Accept and Continue</span> your edits will not be saved.<br />
                <img alt="Edit Form" src="images/EditForm.jpg" />
                
            </li>
         
            <li>If there is no data for that day a message will be displayed<br />
                <img alt="No Data Message" src="images/NoData.jpg" />
            </li>
            <li>When finished editing click <span class="StrongEmphasis">Reset</span> to choose
                another day to edit or click any of the links on the right to <em>View Reports</em>
                or <span class="StrongEmphasis">Add</span> non-AS400 productivity.<br />
            </li>
        </ol>
    </div>
</asp:Content>
