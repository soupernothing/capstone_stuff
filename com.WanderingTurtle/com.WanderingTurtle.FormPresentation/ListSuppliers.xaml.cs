﻿using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListSuppliers.xaml
    /// </summary>
    public partial class ListSuppliers : IDataGridContextMenu
    {
        private readonly SupplierManager _supplierManager = new SupplierManager();
        private List<Supplier> _suppliers;

        /// <summary>
        /// This will fill the list of suppliers and set this object to the "Instance variable"
        /// Created: by will fritz 2015/02/06
        /// </summary>
        /// <exception cref="ArgumentNullException"><see cref="DataGridContextMenuResult"/> is null. </exception>
        /// <exception cref="ArgumentException"><see cref="DataGridContextMenuResult"/> is not an <see cref="T:System.Enum" />. </exception>
        /// <exception cref="InvalidOperationException">The item to add already has a different logical parent. </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        /// <exception cref="WanderingTurtleException" />
        public ListSuppliers()
        {
            InitializeComponent();
            FillList();

            LvSuppliersList.SetContextMenu();
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/15
        /// Logic for context menus
        /// </summary>
        /// <exception cref="WanderingTurtleException"/>
        public void ContextMenuItemClick(object sender, RoutedEventArgs e)
        {
            DataGridContextMenuResult command;
            var selectedItem = sender.ContextMenuClick<Supplier>(out command);
            switch (command)
            {
                case DataGridContextMenuResult.Add:
                    OpenSupplier();
                    break;

                case DataGridContextMenuResult.View:
                    OpenSupplier(selectedItem, true);
                    break;

                case DataGridContextMenuResult.Edit:
                    OpenSupplier(selectedItem);
                    break;

                case DataGridContextMenuResult.Delete:
                    ArchiveSupplier();
                    break;

                default:
                    throw new WanderingTurtleException(this, "Error processing context menu");
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/18
        /// Opens supplier form depending if it is read only or not
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <param name="readOnly"></param>
        private void OpenSupplier(Supplier selectedItem = null, bool readOnly = false)
        {
            try
            {
                if (selectedItem == null)
                {
                    if (new AddEditSupplier().ShowDialog() == false) return;
                    FillList();
                }
                else
                {
                    if (new AddEditSupplier(selectedItem, readOnly).ShowDialog() == false || readOnly) return;
                    FillList();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/18
        /// archives supplier
        /// </summary>
        private async void ArchiveSupplier()
        {
            try
            {
                Supplier supplierToDelete = (Supplier)LvSuppliersList.SelectedItems[0];
                MessageDialogResult result =
                    await
                        this.ShowMessageDialog("Are you sure you want to delete?", "Confirm Delete",
                            MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    _supplierManager.ArchiveSupplier(supplierToDelete);
                }
                FillList();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "You Must Select A Supplier Before You Can Delete");
            }
        }

        /// <summary>
        /// Fills the list view with the suppliers with a fresh list of suppliers
        /// Created: by Will Fritz 15/2/6
        /// </summary>
        /// <remarks>
        /// edited by will fritz 15/2/19
        /// </remarks>
        /// <exception cref="WanderingTurtleException">Child window errored during initialization.</exception>
        public void FillList()
        {
            try
            {
                LvSuppliersList.ItemsSource = null;
                _suppliers = _supplierManager.RetrieveSupplierList();
                LvSuppliersList.Items.Clear();
                LvSuppliersList.ItemsSource = _suppliers;
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "There was an error accessing the database");
            }
        }

        /// <summary>
        /// opens the AddEditSupplier window
        /// Created: by Pat 15/2/6
        /// </summary>
        /// <remarks>
        /// Edited to make it a singleton pattern
        /// By Will Fritz 15/3/6
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            OpenSupplier();
        }

        /// <summary>
        /// Will get selected supplier and delete it (archive)
        /// Created: by Will Fritz 15/2/6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ArchiveSupplier();
        }

        /// <summary>
        /// Miguel Santana
        /// Created 2015/04/04
        /// opens pending supplier ui on double click
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPendingSuppliers_Click(object sender, RoutedEventArgs e)
        {
            ((TabItem)Parent).Content = new ListPendingSuppliers();
        }

        /// <summary>
        /// Will get selected supplier and fill the add/edit tab with info
        /// Created: by Will Fritz 15/2/6
        /// </summary>
        /// <remarks>
        /// Edited to make it a singleton pattern
        /// By Will Fritz 15/3/6
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenSupplier(LvSuppliersList.SelectedItems[0] as Supplier);
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "You Must Select A Supplier Before You Can Update");
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created 2015/04/04
        /// opens supplier ui on double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvSuppliersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenSupplier(sender.RowClick<Supplier>(), true);
        }

        /// <summary>
        /// Justin Pennington
        /// Created 2015/04/14
        /// searches the text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchSupplier_Click(object sender, RoutedEventArgs e)
        {
            var myList = _supplierManager.searchSupplier(TxtSearchSupplier.Text);
            LvSuppliersList.ItemsSource = myList;
            LvSuppliersList.Items.Refresh();
        }

        /// <summary>
        /// Justin Pennington
        /// Created 2015/04/14
        /// searches the text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchSupplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            BtnSearchSupplier.Content = TxtSearchSupplier.Text.Length == 0 ? "Refresh List" : "Search";
        }
    }
}