<%@ Page Title="How to Import Productivity" Language="VB" MasterPageFile="~/Site.Master"
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
        How to Import Productivity</h2>
    <div class="Tutorial" id="LeftColumn">
        <h3>
            For Each day that you Import Productivity:</h3>
        <ol>
            <li >From your department's shift page click <span class="StrongEmphasis">
                Import Productivity</span>
                <br />
                <img  alt="Click Import Link" src="images/clickImport.jpg" />
            </li>
            <li >Your department's shift's Import page will be displayed<br />
                <img  alt="Import Page" src="images/ImportPage.jpg" />
            </li>
            <li >Choose the date that you'd like to import by clicking
                in the date field and selecting a date from the calendar that appears.<br />
                <img  alt="Choose Date" src="images/ChooseDate.jpg" />
            </li>
            <li >If the Import is successful a message will be displayed<br />
                <img  alt="Successful Import Message" src="images/successfulImport.jpg" />
            </li>
            <li >If there is no data for that day a message will be displayed<br />
                <img  alt="No Data Message" src="images/NoData.jpg" />
            </li>
            <li >If productivity has been imported already for that day
                a message will be displayed<br />
                <img  alt="Already Imported message" src="images/AlreadyImport.jpg" />
            </li>
            <li >If productivity can't be imported for that day because
                it is the current day a message will be displayed<br />
                <img  alt="Must wait message" src="images/MustWait.jpg" />
            </li>
            <li >You can choose to <span class="StrongEmphasis">Import</span> another
                day, or if finished importing, <span class="StrongEmphasis">Add</span> or <span class="StrongEmphasis">Edit
                </span>productivity by clicking a link on the Tasks list on the right. </li>
        </ol>
    </div>
</asp:Content>
