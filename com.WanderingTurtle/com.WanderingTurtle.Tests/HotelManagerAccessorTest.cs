﻿using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Tests
{
    /// <summary>
    /// Updated: Tony Noel, 2015/04/22- Updated three methods that were failing- HotelAccessorGet, Update, and Archive
    /// All Tests passing as of the date above.
    /// </summary>
    [TestClass]
    public class HotelManagerAccessorTest
    {
        [TestInitialize]
        public void initialize()
        {
            HotelGuestAccessor.HotelGuestAdd(new HotelGuest("Fake", "Person", "1111 Fake St.", "", new CityState("52641", "Mt. Pleasant", "IA"), "5556667777", "fake@gmail.com", "000", "6663", true));
        }

        [TestMethod]
        public void HotelAccessorAdd()
        {
            int changed = HotelGuestAccessor.HotelGuestAdd(new HotelGuest("Fake", "Person", "1111 Fake St.", "", new CityState("52641", "Mt. Pleasant", "IA"), "5556667777", "fake@gmail.com", "234234234", "3456", true));
            Assert.AreEqual(2, changed);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void HotelAccessorAddFail()
        {
            HotelGuestAccessor.HotelGuestAdd(new HotelGuest("Fake", "Person", "1111 Fake St.", "", new CityState("52641", "Mt. Pleasant", "IA"), "5556667777", "fake@gmail.com", "000", "5678", true));
        }

        [TestMethod]
        public void HotelAccessorGet()
        {
            //calls generic Test accessor to grab the guestID
            int guestID = TestCleanupAccessor.GetHotelGuest();
            //Calls to the accessor method
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet(guestID);
            //Asserts that the firstname of the in the list corresponds to the dummy record first name
            Assert.AreEqual("Fake", guest[guest.Count - 1].FirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void HotelAccessorGetFail()
        {
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet(-1);
        }

        [TestMethod]
        public void HotelAccessorUpdate()
        {
            //gets the guestID from the Test accessor
            int guestID = TestCleanupAccessor.GetHotelGuest();
            //passes guestID to the HotelGuestAccessor method to grab the guest.
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet(guestID);
            int hotelGuest = guest.Count - 1;
            guest.Add(new HotelGuest((int)guest[hotelGuest].HotelGuestID, guest[hotelGuest].FirstName, guest[hotelGuest].LastName, guest[hotelGuest].Address1, guest[hotelGuest].Address2, guest[hotelGuest].CityState, guest[hotelGuest].PhoneNumber, guest[hotelGuest].EmailAddress, guest[hotelGuest].Room, guest[hotelGuest].GuestPIN, false));
            int changed = HotelGuestAccessor.HotelGuestUpdate(guest[guest.Count - 2], guest[hotelGuest]);
            Assert.AreEqual(1, changed);
        }

        [TestMethod]
        public void HotelAccessorArchive()
        {
            //gets the guestID from the Test accessor
            int guestID = TestCleanupAccessor.GetHotelGuest();
            //passes guestID to the HotelGuestAccessor method to grab the guest.
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet(guestID);
            //Calls to archive guest through the accessor
            int changed = HotelGuestAccessor.HotelGuestArchive(guest[guest.Count - 1], false);
            //asserts that a record was affected
            Assert.AreEqual(1, changed);
        }

        [TestCleanup]
        public void cleanup()
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string commandText = @"DELETE FROM Invoice WHERE HotelGuestID >= 10";
            string commandText3 = @"DELETE FROM [dbo].[HotelGuest] WHERE FirstName = 'Fake'";

            var cmd = new SqlCommand(commandText, conn);
            //var cmd2 = new SqlCommand(commandText2, conn);
            var cmd3 = new SqlCommand(commandText3, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                Console.Write("Fail!");
            }
            finally { conn.Close(); }
        }
    }
}