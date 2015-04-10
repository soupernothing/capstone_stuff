﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.DataAccess;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.Tests
{
    /// <summary>
    /// Summary description for ItemListingAccessorTest
    /// </summary>
    [TestClass]
    public class ItemListingAccessorTest
    {
            int itemListID = 104;
            int eventID = 102;
            DateTime startDate = new DateTime(2015, 3, 28, 8, 30, 0);
            DateTime endDate = new DateTime(2015, 3, 28, 10, 30, 0);
            decimal price = 10.0000M;
            int quantityOffered = 5;
            string productSize = "10";
            int maxNumGuests = 11;
            int minNumGuests = 0;
            int currentNumGuests = 10;
            public int supplierID = 102;

        [TestMethod]
        public void AddItemListing_ValidItemListing()
        {
            int expected = 1;
            ItemListing testItemListing = new ItemListing(itemListID, eventID, startDate, endDate, price, quantityOffered, productSize, maxNumGuests, minNumGuests, currentNumGuests);

            int actual = ItemListingAccessor.AddItemListing(testItemListing);

            Assert.AreEqual(expected, actual);
        }

        /*[TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void AddItemListing_InvalidItemListing()
        {
            ItemListing testItemListing = new ItemListing(itemListID, eventID, startDate, endDate, price, quantityOffered, productSize, maxNumGuests, minNumGuests, currentNumGuests);

            int actual = ItemListingAccessor.AddItemListing(testItemListing);
        }*/

        [TestMethod]
        public void UpdateItemListing_ValidItemListing()
        {
            int expected = 1;

            int newEventID = 10;
            int newItemListID = 0;
            DateTime newStartDate = DateTime.Now;
            DateTime newEndDate = DateTime.Now;
            decimal newPrice = 222;
            int newQuantityOffered = 67;
            string newProductSize = "12";
            int newMaxNumGuests = 15;
            int newMinNumGuests = 1;
            int newCurrentNumGuests = 8;
            ItemListing testItemListing = new ItemListing(itemListID, eventID, startDate, endDate, price, quantityOffered, productSize, maxNumGuests, minNumGuests, currentNumGuests);
            ItemListing newTestItemListing = new ItemListing(newItemListID, newEventID, newStartDate, newEndDate, newPrice, newQuantityOffered, newProductSize, newMaxNumGuests, newMinNumGuests, newCurrentNumGuests);
            testItemListing.SupplierID = 102;
            newTestItemListing.SupplierID = 102;

            int actual = ItemListingAccessor.UpdateItemListing(newTestItemListing, testItemListing);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void UpdateItemListing_InvalidItemListing()
        {
            int oldItemListID = 999;

            int newItemListID = 10;
            int newEventID = 10;
            DateTime newStartDate = DateTime.Now;
            DateTime newEndDate = DateTime.Now;
            decimal newPrice = 222;
            int newQuantityOffered = 67;
            string newProductSize = "12";
            int newMaxNumGuests = 15;
            int newMinNumGuests = 1;
            int newCurrentNumGuests = 8;
            ItemListing testItemListing = new ItemListing(oldItemListID, eventID, startDate, endDate, price, quantityOffered, productSize, maxNumGuests, minNumGuests, currentNumGuests);
            ItemListing newTestItemListing = new ItemListing(newItemListID, newEventID, newStartDate, newEndDate, newPrice, newQuantityOffered, newProductSize, newMaxNumGuests, newMinNumGuests, newCurrentNumGuests);

            int actual = ItemListingAccessor.UpdateItemListing(testItemListing, newTestItemListing);

            if(actual == 0)
            {
                 
                throw new ApplicationException("Concurrency Error");
            }
        }

        [TestMethod]
        public void DeleteItemListing_ValidItemListing()
        {
            int expected = 1;
            ItemListing testItemListing = new ItemListing(itemListID, eventID, startDate, endDate, price, quantityOffered, productSize, maxNumGuests, minNumGuests, currentNumGuests);
            testItemListing.SupplierID = 102;

            int actual = ItemListingAccessor.DeleteItemListing(testItemListing);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void DeleteItemListing_InvalidItemListing()
        {
            ItemListing testItemListing = new ItemListing(itemListID, eventID, startDate, endDate, price, quantityOffered, productSize, maxNumGuests, minNumGuests, currentNumGuests);

            int actual = ItemListingAccessor.DeleteItemListing(testItemListing);

            if (actual == 0)
            {

                throw new ApplicationException("Concurrency Error");
            }
        }
    }
}
