﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Group_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        public wndMain()
        {
            InitializeComponent();
        }

        // Menu Item Methods

        /// <summary>
        /// Opens the item table window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateItemTable_Click(object sender, RoutedEventArgs e)
        {
            wndItems ItemsWindow = new wndItems();
            ItemsWindow.Show();

            // Use ShowDialog to open the item table window and get the listUpdated flag.
            // Will uncomment when item table window is implemented.
            bool? result = ItemsWindow.ShowDialog();

            if (result == true) // If the user closes the item table window
            {
                // Retrieve listUpdated flag from the item table window
                bool listUpdated = ItemsWindow.listUpdated;
                if (listUpdated == true)
                {
                    // Update the itemComboBox, may have to change this implementation later on.
                    itemComboBox.Items.Clear();
                    foreach (string item in itemList)
                    {
                        itemComboBox.Items.Add(item);
                    }

                }
            }
        }

        /// <summary>
        /// Opens the search window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            wndSearch SearchWindow = new wndSearch();

            // Use ShowDialog to open the search window and get the search result
            // Will uncomment when search window SelectedInvoice is implemented.
            /*bool? result = SearchWindow.ShowDialog();

            if (result == true) // If the user closes the search window with a valid selection
            {
                // Retrieve selected invoice information from the search window
                int selectedInvoiceNumber = SearchWindow.SelectedInvoice;
            }*/
        }



        // Invoice form Methods

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ItemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveInvoice_Click(object sender, RoutedEventArgs e)
        {

        }



        // Invoice List Methods

        private void InvoiceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditInvoice_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}