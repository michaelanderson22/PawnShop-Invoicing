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

        public wndMain()
        {
            InitializeComponent();

            //Hide the invoice panel by default, will show when Add Invoice button or edit invoice button is clicked
            invoicePanel.Visibility = Visibility.Collapsed;
            updateItemComboBox();
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

            /*// Use ShowDialog to open the item table window and get the listUpdated flag.
            // Will uncomment when item table window is implemented.
            bool? result = ItemsWindow.ShowDialog();

            if (result == true) // If the user closes the item table window
            {
                // Retrieve listUpdated flag from the item table window
                bool listUpdated = ItemsWindow.listUpdated;
                if (listUpdated == true)
                {
                    // Get the current item list by calling getItemList.
                    List<clsItem> itemList = mainLogic.getItemList();

                    // Update the itemComboBox, may have to change this implementation later on.
                    itemComboBox.Items.Clear();
                    foreach (clsItem item in itemList)
                    {
                        itemComboBox.Items.Add(item);
                    }

                }
            }*/
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
                int selectedInvoiceNumber = SearchWindow.SelectedInvoice;
            }
        }

        // Update Item Combo Box
        private void updateItemComboBox()
        {
            itemComboBox.Items.Clear();
            List<clsItem> itemList = clsItemsLogic.getItemList();
            itemComboBox.ItemsSource = itemList;
        }



        // Invoice form Methods

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            mainLogic.items.Add((clsItem)itemComboBox.SelectedItem);
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

        private void EditInvoice_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}