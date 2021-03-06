﻿using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace com.WanderingTurtle.DataAccess
{
    public class ItemListingAccessor
    {
        /// <summary>
        /// Tyler Collins
        /// Created: 2015/02/10
        /// Retrieves ItemListing data from the Database using a Stored Procedure.
        /// Creates an ItemListing object.
        /// </summary>
        /// <remarks>
        /// Tyler Collins
        /// Updated:  2015/02/26
        /// Now up to date with most recent ItemListing object class
        /// </remarks>
        /// <param name="itemListID">Requires the ItemListID to SELECT the correct ItemListing record.</param>
        /// <returns>ItemListing object</returns>
        public static ItemListing GetItemListing(string itemListID)
        {
            ItemListing itemListingToRetrieve = new ItemListing();

            SqlConnection conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spSelectItemListing";
            SqlCommand cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemListID", itemListID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    itemListingToRetrieve.StartDate = reader.GetDateTime(0);
                    itemListingToRetrieve.EndDate = reader.GetDateTime(1);
                    itemListingToRetrieve.ItemListID = reader.GetInt32(2);
                    itemListingToRetrieve.EventID = reader.GetInt32(3);
                    itemListingToRetrieve.Price = reader.GetDecimal(4);

                    //Are we using QuanityOffered and ProductSize since these are Event Items? O.o
                    itemListingToRetrieve.SupplierID = reader.GetInt32(5);
                    itemListingToRetrieve.MaxNumGuests = reader.GetInt32(7);
                    itemListingToRetrieve.MinNumGuests = reader.GetInt32(8);
                    itemListingToRetrieve.CurrentNumGuests = reader.GetInt32(6);
                }
                else
                {
                    var pokeball = new ApplicationException("Requested ID did not match any records.");
                    throw pokeball;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return itemListingToRetrieve;
        }

        /// <summary>
        /// Tyler Collins
        /// Created: 2015/02/10
        /// Retrieves all ItemListing data from the Database using a Stored Procedure.
        /// Creates an ItemListing object from retrieved data.
        /// Adds ItemListing object to List of ItemListing objects.
        /// </summary>
        /// <remarks>
        /// Tyler Collins
        /// Updated:  2015/02/26
        /// Now up to date with most recent ItemListing object class
        /// </remarks>
        /// <returns>List of Active ItemListing objects</returns>
        public static List<ItemListing> GetItemListingList()
        {
            List<ItemListing> itemListingList = new List<ItemListing>();

            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spSelectAllItemListings";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ItemListing currentItemListing = new ItemListing();

                        currentItemListing.StartDate = reader.GetDateTime(0);
                        currentItemListing.EndDate = reader.GetDateTime(1);
                        currentItemListing.ItemListID = reader.GetInt32(2);
                        currentItemListing.EventID = reader.GetInt32(3);
                        currentItemListing.Price = reader.GetDecimal(4);
                        currentItemListing.SupplierID = reader.GetInt32(5);
                        currentItemListing.CurrentNumGuests = reader.GetInt32(6);
                        currentItemListing.MaxNumGuests = reader.GetInt32(7);
                        currentItemListing.MinNumGuests = reader.GetInt32(8);
                        currentItemListing.EventName = reader.GetString(9);
                        currentItemListing.SupplierName = reader.GetString(10);

                        if (currentItemListing.EndDate > DateTime.Now)
                        {
                            itemListingList.Add(currentItemListing);
                        }
                    }
                }
                else
                {
                    var pokeball = new ApplicationException("Data Not Found!");
                    throw pokeball;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return itemListingList;
        }


        /// <summary>
        /// Tyler Collins
        /// Created: 2015/02/10
        /// Retrieves all ItemListing data from the Database using a Stored Procedure.
        /// Creates an ItemListing object from retrieved data.
        /// Adds ItemListing object to List of ItemListing objects.
        /// </summary>
        /// <remarks>
        /// Tyler Collins
        /// Updated:  2015/02/26
        /// Now up to date with most recent ItemListing object class
        /// </remarks>
        /// <returns>List of ItemListing objects</returns>
        public static List<ItemListing> GetAllItemListingList()
        {
            List<ItemListing> itemListingList = new List<ItemListing>();

            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spSelectAllItemListings";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ItemListing currentItemListing = new ItemListing();

                        currentItemListing.StartDate = reader.GetDateTime(0);
                        currentItemListing.EndDate = reader.GetDateTime(1);
                        currentItemListing.ItemListID = reader.GetInt32(2);
                        currentItemListing.EventID = reader.GetInt32(3);
                        currentItemListing.Price = reader.GetDecimal(4);
                        currentItemListing.SupplierID = reader.GetInt32(5);
                        currentItemListing.CurrentNumGuests = reader.GetInt32(6);
                        currentItemListing.MaxNumGuests = reader.GetInt32(7);
                        currentItemListing.MinNumGuests = reader.GetInt32(8);
                        currentItemListing.EventName = reader.GetString(9);
                        currentItemListing.SupplierName = reader.GetString(10);

                        itemListingList.Add(currentItemListing);
                        
                    }
                }
                else
                {
                    var pokeball = new ApplicationException("Data Not Found!");
                    throw pokeball;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return itemListingList;
        }

        /// <summary>
        /// Tyler Collins
        /// Created: 2015/02/11
        /// INSERTs an ItemListing into the Database using a Stored Procedure.
        /// </summary>
        /// <remarks>
        /// Tyler Collins
        /// Updated:  2015/02/26
        /// Now up to date with most recent ItemListing object class
        /// </remarks>
        /// <param name="itemListingToAdd">Requires an ItemListing object to INSERT</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int AddItemListing(ItemListing itemListingToAdd)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spInsertItemListing";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StartDate", itemListingToAdd.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", itemListingToAdd.EndDate);
            cmd.Parameters.AddWithValue("@EventItemID", itemListingToAdd.EventID);
            cmd.Parameters.AddWithValue("@Price", itemListingToAdd.Price);
            cmd.Parameters.AddWithValue("@MaxNumberOfGuests", itemListingToAdd.MaxNumGuests);
            cmd.Parameters.AddWithValue("@MinNumberOfGuests", itemListingToAdd.MinNumGuests);
            cmd.Parameters.AddWithValue("@CurrentNumberOfGuests", itemListingToAdd.CurrentNumGuests);
            cmd.Parameters.AddWithValue("@SupplierID", itemListingToAdd.SupplierID);

            int rowsAffected;
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        /// <summary>
        /// Tyler Collins
        /// Created: 2015/02/11
        /// UPDATEs an ItemListing record in the Database with new data using a Stored Procedure.
        /// </summary>
        /// <remarks>
        /// Tyler Collins
        /// Updated:  2015/02/26
        /// Now up to date with most recent ItemListing object class
        /// </remarks>
        /// <param name="newItemListing">Requires the ItemListing object containing the new information</param>
        /// <param name="oldItemListing">Requires the ItemListing object to replace that matches the record in the Database</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int UpdateItemListing(ItemListing newItemListing, ItemListing oldItemListing)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spUpdateItemListing";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //newItemListing
            cmd.Parameters.AddWithValue("@StartDate", newItemListing.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", newItemListing.EndDate);
            cmd.Parameters.AddWithValue("@EventItemID", newItemListing.EventID);
            cmd.Parameters.AddWithValue("@Price", newItemListing.Price);
            cmd.Parameters.AddWithValue("@MaxNumberOfGuests", newItemListing.MaxNumGuests);
            cmd.Parameters.AddWithValue("@CurrentNumberOfGuests", newItemListing.CurrentNumGuests);
            cmd.Parameters.AddWithValue("@SupplierID", newItemListing.SupplierID);

            //oldItemListing
            cmd.Parameters.AddWithValue("@ItemListID", oldItemListing.ItemListID);
            cmd.Parameters.AddWithValue("@originalStartDate", oldItemListing.StartDate);
            cmd.Parameters.AddWithValue("@originalEndDate", oldItemListing.EndDate);
            cmd.Parameters.AddWithValue("@originalEventItemID", oldItemListing.EventID);
            cmd.Parameters.AddWithValue("@originalPrice", oldItemListing.Price);
            cmd.Parameters.AddWithValue("@originalMaxNumberOfGuests", oldItemListing.MaxNumGuests);
            cmd.Parameters.AddWithValue("@originalCurrentNumberOfGuests", oldItemListing.CurrentNumGuests);
            cmd.Parameters.AddWithValue("@originalSupplierID", oldItemListing.SupplierID);

            int rowsAffected;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Tyler Collins
        /// Created: 2015/02/11
        /// DELETEs (Sets Boolean Active field to false) an ItemListing record in the Database using a Stored Procedure.
        /// </summary>
        /// <remarks>
        /// Tyler Collins
        /// Updated:  2015/02/26
        /// Now up to date with most recent ItemListing object class
        /// </remarks>
        /// <param name="itemListingToDelete">Requires the ItemListing object which matches the record to be DELETED in the Database.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int DeleteItemListing(ItemListing itemListingToDelete)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string storedProcedure = "spDeleteItemListing";
            var cmd = new SqlCommand(storedProcedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemListID", itemListingToDelete.ItemListID);
            cmd.Parameters.AddWithValue("@StartDate", itemListingToDelete.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", itemListingToDelete.EndDate);
            cmd.Parameters.AddWithValue("@EventItemID", itemListingToDelete.EventID);
            cmd.Parameters.AddWithValue("@Price", itemListingToDelete.Price);
            cmd.Parameters.AddWithValue("@MaxNumGuests", itemListingToDelete.MaxNumGuests);
            cmd.Parameters.AddWithValue("@CurrentNumGuests", itemListingToDelete.CurrentNumGuests);
            cmd.Parameters.AddWithValue("@SupplierID", itemListingToDelete.SupplierID);

            int rowsAffected;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Bryan Hurst 
        /// Created:  4/23/2015
        /// Method for deletion of test records created with the unit tests
        /// </summary>
        /// <param name="TestListing">The ItemListing added during testing -- to be deleted</param>
        /// <returns>Number of rows affected</returns>
        public static int DeleteItemListingTestItem(ItemListing TestListing)
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            var cmdText = "spDeleteTestItemListing";
            var cmd = new SqlCommand(cmdText, conn);
            var rowsAffected = 0;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndDate", TestListing.EndDate);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new ApplicationException("Concurrency Violation");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;  // needs to be rows affected
        }
    }
}