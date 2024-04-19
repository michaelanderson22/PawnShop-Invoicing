using System.Collections.ObjectModel;
using System.Text;
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
        /// <summary>
        /// Main logic object.
        /// </summary>
        clsMainLogic mainLogic = new clsMainLogic();

        /// <summary>
        /// Store list of invoices
        /// </summary>
        private List<clsInvoice> invoicesList = new List<clsInvoice> ();

        /// <summary>
        /// Store list of invoices as an observable collection
        /// </summary>
        private ObservableCollection<clsInvoice> invoices;

        /// <summary>
        /// instatiate clsSearch object
        /// </summary>
        private clsSearchLogic searchLogic = new clsSearchLogic();

        public wndMain()
        {
            InitializeComponent();

            //Hide the invoice panel by default, will show when Add Invoice button or edit invoice button is clicked
            invoicePanel.Visibility = Visibility.Collapsed;
            updateItemComboBox();

            // Get list of invoices
            updateInvoicesList();

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

            // Subscribe to the Closed event of ItemsWindow
            ItemsWindow.Closed += ItemsWindow_Closed;

            // Use ShowDialog to open the item table window
            bool? result = ItemsWindow.ShowDialog();

            // Unsubscribe from the Closed event to prevent memory leaks
            ItemsWindow.Closed -= ItemsWindow_Closed;
        }

        private void ItemsWindow_Closed(object sender, EventArgs e)
        {
            // Get the current item list by calling getItemList.
            List<clsItem> itemList = clsItemsLogic.getItemList();

            // Update the itemComboBox
            ObservableCollection<clsItem> observableItemList = new ObservableCollection<clsItem>(itemList);
            itemComboBox.ItemsSource = observableItemList;
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
            bool? result = SearchWindow.ShowDialog();

            if (result == true) // If the user closes the search window with a valid selection
            {
                // Retrieve selected invoice information from the search window


                /*int selectedInvoiceNumber = SearchWindow.SelectedInvoice;*/
            }
        }

        /// <summary>
        /// Updates the invoices list with an observable collection so that the data grid updates properly.
        /// </summary>
        private void updateInvoicesList()
        {
            invoicesList = searchLogic.getInvoices();
            invoices = new ObservableCollection<clsInvoice>(invoicesList);
            invoiceDataGrid.ItemsSource = invoices;
        }

        // Update Item Combo Box
        /// <summary>
        /// Updates the item combo box
        /// </summary>
        private void updateItemComboBox()
        {
            try
            {
                itemComboBox.Items.Clear();
                List<clsItem> itemList = clsItemsLogic.getItemList();
                ObservableCollection<clsItem> observableItemList = new ObservableCollection<clsItem>(itemList);
                itemComboBox.ItemsSource = observableItemList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        // Invoice form Methods
        /// <summary>
        /// Adds an item to the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (itemComboBox.SelectedItem != null)
                {
                    mainLogic.addedItems.Add((clsItem)itemComboBox.SelectedItem);
                    ObservableCollection<clsItem> observableItemList = new ObservableCollection<clsItem>(mainLogic.addedItems);
                    itemDataGrid.ItemsSource = observableItemList;

                    totalCostTextBlock.Text = "Total Cost: $" + mainLogic.getTotalCost().ToString();
                }
                else
                {
                    MessageBox.Show("Please select an item to add to the invoice");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Updates the cost of the selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (itemComboBox.SelectedItem != null)
                {
                    itemCostTextBox.Text = "$" + ((clsItem)itemComboBox.SelectedItem).Cost.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Deletes an item from the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (itemDataGrid.SelectedItem != null)
                {
                    mainLogic.addedItems.Remove((clsItem)itemDataGrid.SelectedItem);
                    ObservableCollection<clsItem> observableItemList = new ObservableCollection<clsItem>(mainLogic.addedItems);
                    itemDataGrid.ItemsSource = observableItemList;
                }
                else
                {
                    MessageBox.Show("Please select an item to delete from the invoice");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Saves the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datePicker.SelectedDate.HasValue)
                {
                    // Add invoice
                    mainLogic.addInvoice(datePicker.SelectedDate.Value);

                    // Update invoice list
                    updateInvoicesList();

                    MessageBox.Show("Invoice Saved");

                    invoicePanel.Visibility = Visibility.Collapsed;

                    clearInvoiceForm();
                }
                else
                {
                    MessageBox.Show("Please select an invoice date");
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Clears the invoice form
        /// </summary>
        private void clearInvoiceForm()
        {
            itemCostTextBox.Text = "";
            totalCostTextBlock.Text = "Total Cost: $0.00";
            datePicker.SelectedDate = null;
            itemComboBox.SelectedItem = null;
            itemDataGrid.ItemsSource = null;

            mainLogic.addedItems.Clear();
        }



        // Invoice List Methods

        private void InvoiceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /// <summary>
        /// runs when the Add Invoice button is clicked, shows the invoice form to be filled out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {
            invoicePanel.Visibility = Visibility.Visible;
            invoiceNumberLabel.Text = "Invoice Number: TBD";

        }

        /// <summary>
        /// Opens the selected invoice to be edited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Clear invoice form
                clearInvoiceForm();

                clsInvoice selectedInvoice = invoiceDataGrid.SelectedItem as clsInvoice;

                if (selectedInvoice != null)
                {
                    // Add items to list
                    foreach (clsItem item in selectedInvoice.theItems)
                    {
                        mainLogic.addedItems.Add(item);
                    }

                    invoicePanel.Visibility = Visibility.Visible;
                    invoiceNumberLabel.Text = "Invoice Number: " + selectedInvoice.theInvoiceNum.ToString();

                    datePicker.SelectedDate = selectedInvoice.theDate;

                    itemDataGrid.ItemsSource = mainLogic.addedItems;
                    totalCostTextBlock.Text = "Total Cost: $" + selectedInvoice.theCost.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

    }
}