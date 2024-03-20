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

        }

        private void btnSelect_Click(object sender, RoutedEventArgs e) {
            //set the invoice ID local variable
        }
    }
}
