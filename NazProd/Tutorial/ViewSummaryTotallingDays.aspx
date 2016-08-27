<%@ Page Title="How to View Productivity Summary by Function" Language="VB" MasterPageFile="~/Site.Master"
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
        How to View Productivity Summary by Function</h2>
    <div class="Tutorial" id="LeftColumn">
        <p>
            This Report breaks the totals of each function down for each day and then totals
            the amounts for each function for the dates selected. More detail than the Summary
            by Date Range Report.</p>
        <ol>
            <li >From your department's shift page click <span class="StrongEmphasis">
                Summary by Function</span> from the Reports list
                <br />
                <img alt="Click Summary of Date Range" src="images/ClickSummFunct.jpg" />
            </li>
            <li >Choose a function to view or keep the selected "All" to
                view all functions<br />
                <img  alt="Choose a function" src="images/SelectFunctFunct.jpg" />
            </li>
            <li >Choose the start date that you'd like to view by clicking
                in the date field and selecting a date from the calendar that appears.<br />
                <img  alt="Choose Start Date" src="images/ChooseSummFunctDatestart.jpg" />
            </li>
            <li >The end date is automatically the same date as the start
                date for convenience, but you can choose an end date to report for a range of dates
                by clicking in the date field and selecting a date from the calendar that appears.<br />
                <img  alt="Choose End Date" src="images/ChooseSummFunctDateEnd.jpg" />
            </li>
            <li>Once all choices have been made click <span class="StrongEmphasis">
                View Report</span> or <span class="StrongEmphasis">Reset</span> to clear the choices and select
                again.<br />
            </li>
            <li>The report based on your choices is displayed.
                <br />
                <img alt="Summ by Date Range Report" src="images/SummDateFunctionReport.jpg" />
            </li>
            <li>Click <span class="StrongEmphasis">Reset</span> to clear the choices
                and create a report again or return to your shift page to view other reports<br />
            </li>
        </ol>
    </div>
</asp:Content>
