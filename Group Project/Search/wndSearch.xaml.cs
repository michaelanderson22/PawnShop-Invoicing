using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Group_Project {
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window {
        /// <summary>
        /// instance of the searchLogic class to aid us
        /// </summary>
        clsSearchLogic searchLogic = new clsSearchLogic();
        /// <summary>
        /// integer value of the selected invoice ID (if any) - used to communicate with the main window
        /// </summary>
        int SelectedInvoice; //this is how to communicate to main window 
        /// <summary>
        /// static variable for the selected cost
        /// </summary>
        public static int selectedCost;
        /// <summary>
        /// static variable for selected invoiceNumber to search by
        /// </summary>
        public static int invoiceNumber;
        /// <summary>
        /// static variable for selected date
        /// </summary>
        public static string selectedDate;
        public wndSearch() {
            InitializeComponent();
            populateThings();
        }

        /// <summary>
        /// populate all 3 comboboxes and the datagrid defaultly
        /// </summary>
        private void populateThings() {
            dgDataGrid.ItemsSource = searchLogic.getInvoices();
            cbNumber.ItemsSource = searchLogic.getNumbers();
            cbDate.ItemsSource = searchLogic.getDates();
            cbCosts.ItemsSource = searchLogic.getCosts();
        }

        //property for invoiceNum
        // ^^^^^ What is that for?? ^^^^^

        /// <summary>
        /// when the clear button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e) {
            dgDataGrid.ItemsSource = searchLogic.getInvoices();
            cbNumber.SelectedIndex = -1; //is it -1 or 0
            cbDate.SelectedIndex = -1;
            cbCosts.SelectedIndex = -1;
            clsSearchLogic.date = false; clsSearchLogic.id = false; clsSearchLogic.cost = false;
            selectedDate = ""; selectedCost = -1; invoiceNumber = -1;
            populateThings();
        }

        /// <summary>
        /// when user decides to select an invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void btnSelect_Click(object sender, RoutedEventArgs e) {
            if (dgDataGrid.SelectedItem != null) {
                int value;
                Int32.TryParse(dgDataGrid.SelectedItem.ToString(), out value);
                SelectedInvoice = value; //need to parse?
            } else { SelectedInvoice = -1; } //set SelectedInvoice to -1 if nothing is selected
            this.Close();
        }

        /// <summary>
        /// don't remember what this is for, but i think it's probably useless
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectionChanged(object sender, SelectionChangedEventArgs e) {
            dgDataGrid.ItemsSource = searchLogic.getInvoices();
        }

        // ^^^^^^^^^ can all of this be consolidated into a single function? ^^^^^^^^^
        // no because there needs to be something to change the bools
        // buuuuut we can write a seperate method
        // call selectionChanged (the general method) after bools are set in the specified ones?
        // actually that might too much work-

        /// <summary>
        /// the costs selection is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCosts_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cbCosts.SelectedItem != null) {
                clsSearchLogic.cost = true;
                Int32.TryParse(cbCosts.SelectedItem.ToString(), out selectedCost); //might need to overload toString
                dgDataGrid.ItemsSource = searchLogic.beginSearch();
            } else {  clsSearchLogic.cost = false; }
        }

        /// <summary>
        /// the ID number selection is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbNumber_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cbNumber.SelectedItem != null) { 
                clsSearchLogic.id = true;
                Int32.TryParse(cbNumber.SelectedItem.ToString(), out invoiceNumber); //overload ToString?
                dgDataGrid.ItemsSource = searchLogic.beginSearch();
            } else { clsSearchLogic.id = false; }
            
        }

        /// <summary>
        /// the date selection is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDate_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cbDate.SelectedItem != null) { 
                clsSearchLogic.date = true;
                selectedDate = cbDate.SelectedItem.ToString(); //overload?
                dgDataGrid.ItemsSource = searchLogic.beginSearch();
            } else { clsSearchLogic.date = false; }
            Int32.TryParse(cbCosts.SelectedItem.ToString(), out int value); //might need to overload toString
            //dgDataGrid.ItemsSource = searchLogic.getInvoicesWcost(value);
        }

    }
}
