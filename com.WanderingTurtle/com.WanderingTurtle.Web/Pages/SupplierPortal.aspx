﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierPortal.aspx.cs" Inherits="com.WanderingTurtle.Web.Pages.SupplierPortal" %>

<%@ Import Namespace="com.WanderingTurtle.Web" %>
<%@ Import Namespace="com.WanderingTurtle.Common" %>
<%@ Import Namespace="com.WanderingTurtle.BusinessLogic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="mainpage">
        <div id="leftcontainer" runat="server">
            <div id="supplierdetails">
                <h2><%= _currentSupplier.CompanyName %></h2>



            </div>
            <div id="quickdetails">
                <h3>Quick Event Details</h3>
                You have: <%= currentListingCount %> Events Scheduled
            <br />
                with a total of <%=currentGuestsCount %> guests signed up.<br />
            </div>

        </div>
        <div id="rightcontainer">
            <div id="actions" runat="server">
                <h2>Upcoming Events</h2>
                <asp:ListView ID="lvLists" ItemType="com.WanderingTurtle.Common.ItemListing" SelectMethod="GetItemLists" DataKeyNames="ItemListID" EnableViewState="False" runat="server">
                    <ItemTemplate>

                        <tr>
                            <td><%# Item.EventName.Truncate(25) %></td>
                            <td><%# Item.StartDate.ToString("MM/dd/yy hh:mmt") %></td>
                            <td><%# Item.EndDate.ToString("MM/dd/yy hh:mmt") %></td>
                            <td><%# Item.CurrentNumGuests %></td>
                            <td>
                                <asp:Button ID="btnDetails" runat="server" CommandArgument="<%#Item.ItemListID %>" Text="View Details" OnClick="btnDetails_Click" UseSubmitBehavior="False" />
                            </td>
                        </tr>

                    </ItemTemplate>

                    <LayoutTemplate>
                        <table id="tblmain" class="sortable">
                            <thead>
                                <tr id="tr1" runat="server">
                                    <th id="td8" class="eventheader" runat="server">Event Name</th>
                                    <th id="td2" class="eventheader" runat="server">Start Time</th>
                                    <th id="td3" class="eventheader" runat="server">End Time</th>
                                    <th id="td5" class="eventheader" runat="server"># Guests</th>
                                    <th style="display: none;"></th>
                                </tr>

                            </thead>
                            <tr id="ItemPlaceholder" runat="server">
                            </tr>

                        </table>
                    </LayoutTemplate>
                </asp:ListView>

            </div>

            <div id="eventsDetails" runat="server" style="display: none;">
                <asp:ListView ID="lvDetails" ItemType="com.WanderingTurtle.Common.BookingNumbers" DataKeyNames="Room" EnableViewState="False" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Item.FirstName +" " + Item.LastName%></td>
                            <td><%# Item.Quantity %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="tbl1" class="sortable">
                            <thead>
                                <tr id="tr1" runat="server">
                                    <th id="td8" class="eventheader" runat="server">Guest Name</th>
                                    <th id="td2" class="eventheader" runat="server">Number of Tickets</th>
                                </tr>

                            </thead>
                            <tr id="ItemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:Button ID="btnGoBack" runat="server" Text="Go Back" OnClick="btnGoBack_Click" UseSubmitBehavior="False" />
            </div>
        </div>


    </div>
</asp:Content>