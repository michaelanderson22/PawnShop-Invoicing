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
        clsSearchLogic searchLogic = new clsSearchLogic();
        int SelectedInvoice; //this is how to communicate to main window 
        public wndSearch() {
            InitializeComponent();
            populateThings();
        }

        private void populateThings() {
            dgDataGrid.ItemsSource = searchLogic.getInvoices();
            cbNumber.ItemsSource = searchLogic.getNumbers();
            cbDate.ItemsSource = searchLogic.getDates();
            cbCosts.ItemsSource = searchLogic.getCosts();
        }

        //property for invoiceNum

        private void btnClear_Click(object sender, RoutedEventArgs e) {
            dgDataGrid.ItemsSource = searchLogic.getInvoices();
            cbNumber.SelectedIndex = -1; //is it -1 or 0
            cbDate.SelectedIndex = -1;
            cbCosts.SelectedIndex = -1;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e) {
            //set the invoice ID local variable
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e) {
            dgDataGrid.ItemsSource = searchLogic.getInvoices(variables);
        }

        private void cbCosts_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Int32.TryParse(cbCosts.SelectedItem.ToString(), out int value); //might need to overload toString
            dgDataGrid.ItemsSource = searchLogic.getInvoicesWcost(value);
        }
    }
}
