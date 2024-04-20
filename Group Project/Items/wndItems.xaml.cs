using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class wndItems : Window {
		#region Attributes

		/// <summary>
		/// clsItemsSQL obj to use for SQL strings
		/// </summary>
		clsItemsSQL clsItemsSQL;
		/// <summary>
		/// clsDataAccess obj to execute SQL strings
		/// </summary>
		clsDataAccess data;
		/// <summary>
		/// string var to store SQL strings
		/// </summary>
		string sql;
		/// <summary>
		/// String to hold Desc for new items, or to update existing ones
		/// </summary>
		string desc;
		/// <summary>
		/// String to hold itemCode for new items, or to update existing ones
		/// </summary>
		string itemCode;
		/// <summary>
		/// int to hold cost
		/// </summary>
		int cost;
		/// <summary>
		/// Enum for tracking state of radio buttons
		/// </summary>
		public enum RadioButtons {
			None,
			Add,
			Edit,
			Delete
		}
		RadioButtons eRadioButtons;
		#endregion
		#region Constructor

		/// <summary>
		/// Constructor, runs when window is initialized
		/// </summary>
		public wndItems() {
			try {
				InitializeComponent();
				clsItemsSQL = new clsItemsSQL();
				data = new clsDataAccess();
				DataSet ds = new DataSet();
				sql = desc = itemCode = "";
				cost = 0;
				WarningLabel.Content = "";
				eRadioButtons = RadioButtons.None;
				RefreshItemsGrid();
			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
				   MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		#endregion

		#region Grid

		/// <summary>
		/// This method is called when the ItemGrid needs to have its DataSet Refreshed
		/// </summary>
		private void RefreshItemsGrid() {
			try {
				ItemGrid.ItemsSource = new DataView(clsItemsLogic.getItemDataSet().Tables[0]);
			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
								   MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		#endregion
		#region ButtonsAndClicks

		/// <summary>
		/// Method runs when Submit button is clicked.
		/// </summary>
		/// <param name="sender">Submit button obj</param>
		/// <param name="e">click event</param>
		/// <exception cref="Exception">throws exception</exception>
		private void SubmitButton_Click(object sender, RoutedEventArgs e) {
			try {
				bool haveCost = Int32.TryParse(costTextBox.Text, out cost);
				desc = descTextBox.Text;
				itemCode = codeTextBox.Text;
				if (Add.IsChecked == true) {
					if (clsItemsLogic.IsCodeUnique(itemCode) && itemCode != "" && haveCost && desc != "") {
						clsItem temp = new clsItem(itemCode, desc, cost);
						clsItemsLogic.AddItem(temp);
					}
					else {
						WarningLabel.Content = "Check Item to Add, must have unique Code and all values filled.";
						codeTextBox.Focus();
						return;
					}
				}
				else if (Edit.IsChecked == true) {
					if (!clsItemsLogic.IsCodeUnique(itemCode)) {
						if (itemCode != "" && haveCost && desc != "") {
							clsItem temp = new clsItem(itemCode, desc, cost);
							clsItemsLogic.EditItem(temp);
						}
						else {
							WarningLabel.Content = "Please fill all fields.";
						}
					}
					else { 
						WarningLabel.Content = "Please enter valid item for edit.";
					}
				}
				else if (Delete.IsChecked == true && !clsItemsLogic.IsCodeUnique(itemCode)) {
					if (itemCode != "") {
						if (clsItemsLogic.IsCodeSafeToDelete(itemCode)) {
							clsItemsLogic.DeleteItem(itemCode);
						}
						else {
							WarningLabel.Content = "Can Not Delete Item, Exists in an invoice!";
							return;
						}
					}
					else {
						WarningLabel.Content = "Please enter item to delete.";
					}
				}
				else {
					return;
				}
				RefreshItemsGrid();
				codeTextBox.Text = costTextBox.Text = descTextBox.Text = "";
				SubmitButton.IsEnabled = false;
				Delete.IsChecked = Edit.IsChecked = Add.IsChecked = false;
			}
			catch (Exception ex) {
				HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
			}
		}
		/// <summary>
		/// This method is called when an item is selected (click) in the ItemGrid
		/// </summary>
		/// <param name="sender">Item clicked on</param>
		/// <param name="e">click event</param>
		private void ItemGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			try {
				if (ItemGrid.SelectedItem != null) {
					dynamic selectedRow = ItemGrid.SelectedItem;
					descTextBox.Text = selectedRow["ItemDesc"].ToString();
					codeTextBox.Text = selectedRow["ItemCode"].ToString();
					costTextBox.Text = selectedRow["Cost"].ToString();
				}
			}
			catch (Exception ex) {
				HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
			}
		}
		#endregion
		#region inputs

		/// <summary>
		/// This meethod runs when the CodeTextBox has a keydown event
		/// </summary>
		/// <param name="sender">The codetextbox</param>
		/// <param name="e">Keypress event</param>
		/// <exception cref="Exception">Throws exception in try-catch</exception>
		private void CodeTextBoxKeyPress(object sender, KeyEventArgs e) {
			try {
				CheckInput(false, e);
			}
			catch (Exception ex) {
				HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
			}
		}
		/// <summary>
		/// This meethod runs when the CostTextBox has a keydown event
		/// </summary>
		/// <param name="sender">The costtextbox</param>
		/// <param name="e">Keypress event</param>
		/// <exception cref="Exception">Throws exception in try-catch</exception>
		private void CostTextKeyPress(object sender, KeyEventArgs e) {
			try {
				CheckInput(true, e);
			}
			catch (Exception ex) {
				HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
			}
		}
		/// <summary>
		/// This method runs when the descTextBox has a keydown event
		/// </summary>
		/// <param name="sender">The desctextbox</param>
		/// <param name="e">Keypress event</param>
		/// <exception cref="Exception">Throws exception in try-catch</exception>
		private void DescTextBoxKeyPress(object sender, KeyEventArgs e) {
			try {
				CheckInput(false, e);
			}
			catch (Exception ex) {
				HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
			}
		}
		/// <summary>
		/// This method is passed the event from a keypress and a bool for if it is checking for an int or not, handles unwanted key events
		/// </summary>
		/// <param name="inputIsInt">Bool for if int is desired input</param>
		/// <param name="e"></param>
		private void CheckInput(bool inputIsInt, KeyEventArgs e) {
			try {
				if (inputIsInt) {
					//0 - 9 nums and numpad
					if ((e.Key >= Key.D0 && e.Key <= Key.D9) ||
						(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)) {
					}
					// allow deletion
					else if (e.Key == Key.Delete) {
					}
					// if enter, submit
					else if (e.Key == Key.Enter) {
					}
					// disallow any unwanted keypress
					else {
						e.Handled = true;
					}
				}
				else {
					// alpha chars
					if (e.Key >= Key.A && e.Key <= Key.Z) {
					}
					//allow deletion
					else if (e.Key == Key.Delete) {
					}
					// disallow unwanted keypress events
					else {
						e.Handled = true;
					}
				}
				ToggleSubmitEnabled(eRadioButtons);
			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
				   MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		#endregion
		#region ControlEnabling

		/// <summary>
		/// This method will check for radio buttons and text fields content, enabling Submit button if appropriate
		/// </summary>
		/// <param name="selectedRdoBtn">Enum for tracking selected radio button</param>
		/// <exception cref="Exception">Throws exception if error</exception>
		public void ToggleSubmitEnabled(Enum selectedRdoBtn) {
			try {
				if (eRadioButtons.Equals(RadioButtons.Add)) {
					if (codeTextBox.Text != "" && costTextBox.Text != "" && descTextBox.Text != "") {
						SubmitButton.IsEnabled = true;
					}
				}
				else if (eRadioButtons.Equals(RadioButtons.Edit)) {
					if (codeTextBox.Text != "" && costTextBox.Text != "" && descTextBox.Text != "") {
						SubmitButton.IsEnabled = true;
					}
				}
				else if (eRadioButtons.Equals(RadioButtons.Delete)) {
					if (codeTextBox.Text != "") {
						SubmitButton.IsEnabled = true;
					}
				}
				else {
					SubmitButton.IsEnabled = false;
					return;
				}
			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
				   MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		/// <summary>
		/// When radio button is checked, this method clears teh WarningLabel Label in the items window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RdoBtn_Checked(object sender, RoutedEventArgs e) {
			try {
				RadioButton rdo = sender as RadioButton;
				WarningLabel.Content = "";
				switch (rdo.Name) {
					case "Add":
						eRadioButtons = RadioButtons.Add;
						break;
					case "Edit":
						eRadioButtons = RadioButtons.Edit;
						break;
					case "Delete":
						eRadioButtons = RadioButtons.Delete;
						break;
				}
				ToggleSubmitEnabled(eRadioButtons);
			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
				   MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		#endregion
		#region ErrorHandling
		/// <summary>
		/// This method will handle all top level Errors
		/// </summary>
		/// <param name="sClass"></param>
		/// <param name="sMethod"></param>
		/// <param name="sMessage"></param>
		private void HandleError(string sClass, string sMethod, string sMessage) {
			try {
				MessageBox.Show(sClass + "." + sMethod + "." + sMessage);
			}
			catch (Exception ex) {
				System.IO.File.AppendAllText(".//Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
			}
		}
		#endregion
	}
}