<%@ Page Title="View Productivity Summaries" Language="VB" MasterPageFile="~/Site.Master"
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
        View Productivity Summaries</h2>
    <div class="Tutorial" id="LeftColumn">
        <p>
            Each Department's Page contains Productivity Summaries for the entire department,
            all shifts combined for the previous day of work, week and month
        </p>
        <img alt="Department Summary" src="images/DeptSumm.jpg" />
        <p>
            Each Shift's Page contains Productivity Summaries for the shift for the previous
            day of work, week and month
        </p>
        <img alt="Shift Summary" src="images/ShiftSumm.jpg" />
    </div>
</asp:Content>
