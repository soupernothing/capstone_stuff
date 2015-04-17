﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListTheListings.xaml
    /// </summary>
    public partial class ListTheListings : UserControl
    {
        private GridViewColumnHeader _sortColumn;

        //Class level variables needed for sorting method
        private ListSortDirection _sortDirection;

        private List<ItemListing> myListingList;
        private ProductManager prodMan = new ProductManager();

        public ListTheListings()
        {
            InitializeComponent();
            refreshData();
        }

        private void btnAddListing_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window AddItemListings = new AddEditListing();
                //Commented out by Justin Penningtonon 3/10/2015 4:02 AM causes errors due to ShowDialog only being able to be used on hidden
                //AddItemListings.Show();
                if (AddItemListings.ShowDialog() == false)
                {
                    refreshData();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        private async void btnArchiveListing_click(object sender, RoutedEventArgs e)
        {
            ItemListing ListingToDelete = (ItemListing)lvListing.SelectedItem;
            if (ListingToDelete == null)
            {
                throw new WanderingTurtleException(this, "Please select a row to delete.");
            }
            try
            {
                MessageDialogResult result = await DialogBox.ShowMessageDialog(this, "Are you sure you want to delete this?", "Confirm Delete", MessageDialogStyle.AffirmativeAndNegative);
                switch (result)
                {
                    case MessageDialogResult.Affirmative:
                        var numRows = prodMan.ArchiveItemListing(ListingToDelete);
                        if (numRows == listResult.Success)
                        {
                            await DialogBox.ShowMessageDialog(this, "Listing successfully deleted.");
                        }
                        refreshData();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        private void btnEditListing_click(object sender, RoutedEventArgs e)
        {
            EditListing(lvListing.SelectedItem as ItemListing);
        }

        private void EditListing(ItemListing ListingEdit, bool ReadOnly = false)
        {
            if (ListingEdit == null)
            {
                throw new WanderingTurtleException(this, "Please select a row to edit");
            }

            try
            {
                Window EditListings = new AddEditListing(ListingEdit, ReadOnly);

                //Commented out by Justin Penningtonon 3/10/2015 4:02 AM causes errors due to ShowDialog only being able to be used on hidden
                //AddItemListings.Show();
                if (EditListings.ShowDialog() == false)
                {
                    refreshData();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        private void lvListing_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditListing(DataGridHelper.DataGridRow_Click<ItemListing>(sender, e), true);
        }

        /// <summary>
        /// This method will sort the listview column in both asending and desending order
        /// Created by Will Fritz 15/2/27
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvListingsListHeaderClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = e.OriginalSource as GridViewColumnHeader;
            if (column == null)
            {
                return;
            }

            if (_sortColumn == column)
            {
                // Toggle sorting direction
                _sortDirection = _sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            }
            else
            {
                _sortColumn = column;
                _sortDirection = ListSortDirection.Ascending;
            }

            string header = string.Empty;

            // if binding is used and property name doesn't match header content
            Binding b = _sortColumn.Column.DisplayMemberBinding as Binding;

            if (b != null)
            {
                header = b.Path.Path;
            }

            try
            {
                ICollectionView resultDataView = CollectionViewSource.GetDefaultView(lvListing.ItemsSource);
                resultDataView.SortDescriptions.Clear();
                resultDataView.SortDescriptions.Add(new SortDescription(header, _sortDirection));
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "There must be data in the list before you can sort it");
            }
        }

        private void refreshData()
        {
            try
            {
                myListingList = prodMan.RetrieveItemListingList();
                foreach (ItemListing item in myListingList)
                {
                    item.Seats = (item.MaxNumGuests - item.CurrentNumGuests);
                }
                lvListing.ItemsSource = myListingList;
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "No database able to be accessed for Listings");
            }
        }

        private void txtSearchListing_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtSearchListing.Text.Length == 0)
            {
                btnSearchListing.Content = "Refresh List";
            }
            else
            {
                btnSearchListing.Content = "Search";
            }
        }

        private void btnSearchListing_Click(object sender, RoutedEventArgs e)
        {
            var myList = prodMan.SearchItemLists(txtSearchListing.Text);
            foreach (ItemListing item in myList)
            {
                item.Seats = (item.MaxNumGuests - item.CurrentNumGuests);
            }
            lvListing.ItemsSource = myList;
            lvListing.Items.Refresh();
        }
    }
}