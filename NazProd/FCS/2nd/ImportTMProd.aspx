﻿<%@ Page Title="Import Full Case Stock 2nd Shift Productivity" Language="VB" MasterPageFile="~/Site.master"
    AutoEventWireup="false" CodeFile="ImportTMProd.aspx.vb" Inherits="ImportTM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="sideNav">
        <h3>
            Tasks</h3>
        <ul>
            <li>
                <asp:HyperLink ID="backToShift" runat="server" NavigateUrl="~/FCS/2nd/Shift.aspx"
                    onmouseover="window.status='2nd Shift Home';return true;" onmouseout="window.status=''; return true;">2nd Shift Home</asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="EditProd" runat="server" NavigateUrl="~/FCS/2nd/Edit.aspx" onmouseover="window.status='Edit Productivity';return true;"
                    onmouseout="window.status=''; return true;">Edit Productivity</asp:HyperLink>
            </li>
        </ul>
        <h3>
            Reports</h3>
        <ul>
            <li>
                <asp:HyperLink ID="ViewByTM" runat="server" NavigateUrl="~/FCS/2nd/ViewbyTM.aspx"
                    CausesValidation="False" onmouseover="window.status='View By TM';return true;"
                    onmouseout="window.status=''; return true;">View by Team Member</asp:HyperLink>
            </li>
            <li>
                <asp:HyperLink ID="ViewByFunction" runat="server" NavigateUrl="~/FCS/2nd/ViewbyFunct.aspx"
                    onmouseover="window.status='View By Function';return true;" onmouseout="window.status=''; return true;">View by Function</asp:HyperLink>
            </li>
            <li>
                <asp:HyperLink ID="SummaryDateRange" runat="server" NavigateUrl="~/FCS/2nd/SummDateRange.aspx"
                    onmouseover="window.status='Summary of Date Range';return true;" onmouseout="window.status=''; return true;">Summary of Date Range</asp:HyperLink>
            </li>
            <li>
                <asp:LinkButton ID="SummbyFunct" runat="server" PostBackUrl="~/FCS/2nd/SummbyFunct.aspx"
                    onmouseover="window.status='Summary by Function';return true;" onmouseout="window.status=''; return true;"
                    CausesValidation="false">Summary by Function</asp:LinkButton>
            </li>
        </ul>
    </div>
   <p class="floatRight"><asp:Label ID="LastDateImport" runat="server" Text=""></asp:Label></p>
            
        <h2>
            Import Full Case Stock 2nd Shift Productivity</h2>

    <div id="Search">
        <asp:Label ID="LblDate" runat="server" CssClass="bold">Select Date: &nbsp;</asp:Label><asp:TextBox
            ID="txtDate" runat="server"> </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="The Date is a Required field."
            ControlToValidate="txtDate" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender" TargetControlID="txtDate" runat="server">
        </ajaxToolkit:CalendarExtender>
        <asp:LinkButton ID="ImportButton" runat="server" Visible="true" Text="Import Productivity"> </asp:LinkButton>
        <asp:LinkButton ID="ClearSearchButton" runat="server" OnClick="ClearSearchButton_OnClick"
            Visible="true" Text="Reset" CausesValidation="false"></asp:LinkButton>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" />        <p>
            <asp:Label ID="SuccessLabel" runat="server" Text=""></asp:Label></p>
    </div>
</asp:Content>
