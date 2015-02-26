﻿CREATE PROCEDURE spSelectInvoiceBookings
(@hotelGuestID int)
AS
	SELECT Distinct BookingID, Booking.GuestID, Booking.ItemListID, Booking.Quantity, ItemListing.Price,ItemListing.StartDate, EventItem.EventItemName
	FROM Invoice, Booking, HotelGuest, EventItem, ItemListing
	WHERE Booking.GuestID = @hotelGuestID
	AND Invoice.HotelGuestID = Booking.GuestID
	AND Booking.ItemListID = ItemListing.ItemListID
	AND ItemListing.EventItemID = EventItem.EventItemID

