﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PagesGuest/SiteGuest.Master" AutoEventWireup="true" CodeBehind="ViewBookings.aspx.cs" Inherits="com.WanderingTurtle.Web.PagesGuest.ViewBookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Created: by Arik Chadima 2015/4/24
        Allows for guests to view and print the bookings their signed up for.
        Modified by Arik Chadima 2015/4/30
        changed gridview to a repeater to allow for easier styling and item manipulation. --%>
    <div>
        <div id="loginDiv" runat="server">
            <asp:Label ID="lblError" Text="" runat="server" Visible="false" ForeColor="Red" /><br />
            <asp:Label ID="lblLogin" Text="Enter your pin:" runat="server" />
            <asp:TextBox ID="txtLogin" runat="server" MaxLength="6" TextMode="Password" /><br />
            <asp:Button ID="btnLogin" runat="server" Text="View bookings" OnClick="btnLogin_Click" UseSubmitBehavior="True" />
        </div>
        <div id="guestDetailsDiv" runat="server" visible="false">
            <br />
            <h3>Viewing booked events for: </h3>
            <br />
            <asp:Label ID="GuestFullName" runat="server" CssClass="boldBigger" /><br />
            <asp:Label ID="Address1" runat="server" CssClass="boldBigger" /><br />
            <asp:Label ID="Address2" runat="server" CssClass="boldBigger" /><br />
            <asp:Label ID="CityStateZip" runat="server" CssClass="boldBigger" /><br />
            <asp:Label ID="EmailAddress" runat="server" CssClass="boldBigger" /><br />
            <asp:Label ID="PhoneNumber" runat="server" CssClass="boldBigger" /><br />

            <table id="bookingTable">
                <thead>
                    <tr>
                        <td>Event Start Date
                        </td>
                        <td>Event Name
                        </td>
                        <td>Number of Tickets
                        </td>
                        <td>Price per Ticket
                        </td>
                        <td>Total
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repBookings" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# String.Format("{0:ddd, MMM d}, {0:t}", DataBinder.Eval(Container.DataItem,"StartDate")) %></td>
                                <td><%# DataBinder.Eval(Container.DataItem,"EventItemName") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "Quantity") %></td>
                                <td><%# String.Format("{0:c}",DataBinder.Eval(Container.DataItem,"TicketPrice")) %></td>
                                <td><%# String.Format("{0:c}",DataBinder.Eval(Container.DataItem,"TotalCharge")) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Total price:</td>
                        <td>
                            <asp:Label ID="TotalPrice" runat="server" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div id="otherMessage" title="Message" style="display: none;">
        <div class="errorMsg">
            <asp:Label ID="lblOtherMessage" runat="server" Text="Error" ForeColor="#000000"></asp:Label>
            <br />
        </div>
    </div>
</asp:Content>
