using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Group_Project {
	public static class clsItemsLogic {
		#region Attributes
		/// <summary>
		/// clsItemsSQL obj to use for getting SQL strings
		/// </summary>
		private static clsItemsSQL itemSQL;
		/// <summary>
		/// Data access class obj
		/// </summary>
		private static clsDataAccess data;
		/// <summary>
		/// String to hold SQL queries for dataaccess class calls
		/// </summary>
		private static string sql;
		#endregion
		#region DBInfoGetters

		/// <summary>
		/// Public list for app to use when it need a list of all items in the db
		/// </summary>
		/// <returns>List of cls Items</returns>
		public static List<clsItem> getItemList() {
			try {
				string sql;
				data = new clsDataAccess();
				itemSQL = new clsItemsSQL();
				int retVal = 0;
				DataSet ds = new DataSet();
				List<clsItem> itemList = new List<clsItem>();
				sql = itemSQL.QueryAllItemDesc();
				ds = data.ExecuteSQLStatement(sql, ref retVal);
				if (ds != null && ds.Tables.Count > 0) {
					foreach (DataRow row in ds.Tables[0].Rows) {
						clsItem item = new clsItem(row["ItemCode"].ToString(), row["ItemDesc"].ToString(), (decimal)row["Cost"]);
						itemList.Add(item);
					}
				}
				return itemList;
			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		/// <summary>
		/// This method returns just the dataset of all the items in the item def table
		/// </summary>
		/// <returns>DataSet with all DB items</returns>
		public static DataSet getItemDataSet() {
			string sql;
			clsDataAccess data = new clsDataAccess();
			clsItemsSQL itemSQL = new clsItemsSQL();
			try {
				int retVal = 0;
				DataSet ds = new DataSet();

				sql = itemSQL.QueryAllItemDesc();
				ds = data.ExecuteSQLStatement(sql, ref retVal);
				return ds;
			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		#endregion
		#region ItemCheckers

		/// <summary>
		/// Method to check for uniqueness of key code entered for new items etc
		/// </summary>
		/// <param name="code">String of item code to be used as a primary key</param>
		/// <returns>Bool true for unique, false if code is in use already</returns>
		public static bool IsCodeUnique(string code) {
			try {
				bool result = true;
				foreach (clsItem item in getItemList()) {
					if (code == item.ID.ToString()) {
						result = false;
						break;
					}
				}
				return result;
			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		/// <summary>
		/// This method is to check if a code is in an invoice, to know if it is safe to delete
		/// </summary>
		/// <param name="code">String of Item code to check</param>
		/// <returns>Bool true = safe to delete</returns>
		public static bool IsCodeSafeToDelete(string code) {
			try {
				itemSQL = new clsItemsSQL();
				string sql;
				DataSet ds = new DataSet();
				int numOfRows = 0;
				foreach (clsItem item in getItemList()) {
					if (item.ID.ToString() == code) {
						sql = itemSQL.QueryInvoiceByItemCode(item.ID);
						data.ExecuteSQLStatement(sql, ref numOfRows);
						if (numOfRows > 0) {
							return false;
						}
					}
				}
				return true;
			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		#endregion
		#region DBManip

		/// <summary>
		/// This method will add a new item obj to the database.
		/// </summary>
		/// <param name="item">Item obj with values desired for new db entry</param>
		/// <exception cref="Exception">Throws exception</exception>
		public static void AddItem(clsItem item) {
			try {
				sql = itemSQL.InsertNewItem(item.Desc, item.ID, (int)item.Cost);
				data.ExecuteNonQuery(sql);
			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		/// <summary>
		/// This method will edit an existing item with the new parameters conatined an a temp item passed in
		/// </summary>
		/// <param name="item">Temp item obj holding the values to overwrite the item with matching code</param>
		/// <exception cref="Exception">Throws exception</exception>
		public static void EditItem(clsItem item) {
			try {
				sql = itemSQL.UpdateItemDescAndCost(item.Desc, (int)item.Cost, item.ID);
				data.ExecuteNonQuery(sql);

			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		/// <summary>
		/// This method will delete an item whos cade passed to it
		/// </summary>
		/// <param name="itemCode">String of item code to be deleted</param>
		/// <exception cref="Exception">Throws exception</exception>
		public static void DeleteItem(string itemCode) {
			try {
				sql = itemSQL.DeleteFromItemDesc(itemCode);
				data.ExecuteNonQuery(sql);
			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		#endregion
	}
	/// <summary>
	/// This class provides the app acces to a list of objects that represent all items in the DB
	/// </summary>
	public class clsItem {
		/// <summary>
		/// Property to get string of ID
		/// </summary>
		public string ID { get { return sID; } }
		/// <summary>
		/// String tto hold ID of item obj
		/// </summary>
		private string sID;
		/// <summary>
		/// Property to get string of Desc
		/// </summary>
		public string Desc { get { return sDesc; } }
		/// <summary>
		///  string of description of item obj
		/// </summary>
		private string sDesc;
		/// <summary>
		/// Property to get Cost Decimal
		/// </summary>
		public decimal Cost { get { return sCost; } }
		/// <summary>
		/// decimal of item objects cost
		/// </summary>
		private decimal sCost;
		/// <summary>
		/// Contructor for the clsItem Class
		/// </summary>
		/// <param name="id">Primary Id for the item</param>
		/// <param name="desc">String description</param>
		/// <param name="cost">Decimal cost of item obj</param>
		public clsItem(string id, string desc, decimal cost) {
			try {
				sID = id; sDesc = desc; sCost = cost;

			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
	}
}