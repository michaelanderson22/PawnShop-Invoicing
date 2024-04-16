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

namespace Group_Project
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        clsItemsSQL clsItemsSQL;
        clsDataAccess data;
		string sql;
        string desc;
        int itemCode;
        int cost;
		public wndItems()
        {
            InitializeComponent();
			clsItemsSQL = new clsItemsSQL();
            data = new clsDataAccess();
            DataSet ds = new DataSet();
            sql = desc = "";
            itemCode = cost = 0;
            ItemGrid.ItemsSource = new DataView(clsItemsLogic.getItemDataSet().Tables[0]);
		}

		private void SubmitButton_Click(object sender, RoutedEventArgs e) {
            cost = Int32.Parse(costTextBox.Text);
            desc = descTextBox.Text;

			// This is throwing errors
            itemCode = Int32.Parse(codeTextBox.Text);
			// End of error town ps I know why, im just tired pick up here tomorrow.

            if (AddRdoBtn.IsChecked == true) {
                if (codeTextBox.Text != "" && costTextBox.Text != "" && descTextBox.Text != "" ) {
					sql = clsItemsSQL.InsertNewItem(desc, itemCode, cost);
                    data.ExecuteNonQuery(sql);
                } else {
                    return;
                }
            }
            else if (EditRdoBtn.IsChecked == true) {
				if (codeTextBox.Text != "" && costTextBox.Text != "" && descTextBox.Text != "") {
					sql = clsItemsSQL.UpdateItemDescAndCost(desc, itemCode, cost);
					data.ExecuteNonQuery(sql);
				}
				else {
					return;
				}
			} else if (DeleteRdoBtn.IsChecked == true) {
				if (codeTextBox.Text != "" && costTextBox.Text != "" && descTextBox.Text != "") {
					sql = clsItemsSQL.DeleteFromItemDesc(itemCode);
					data.ExecuteNonQuery(sql);
				}
				else {
					return;
				}
			} else {
                return;
            }
        }

		private void ItemGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			
			if (ItemGrid.SelectedItem != null) {
				dynamic selectedRow = ItemGrid.SelectedItem;
				descTextBox.Text = selectedRow["ItemDesc"].ToString();
				codeTextBox.Text = selectedRow["ItemCode"].ToString();
				costTextBox.Text = selectedRow["Cost"].ToString();
			}
		}
	}
}