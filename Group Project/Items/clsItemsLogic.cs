using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project {
	public static class clsItemsLogic {

		private static clsItemsSQL itemSQL;

		private static clsDataAccess data;


		/// <summary>
		/// Public list for app to use when it need a list of all items in the db
		/// </summary>
		/// <returns>List of cls Items</returns>
		public static List<clsItem> getItemList() {
			string sql;
			data = new clsDataAccess();
			itemSQL = new clsItemsSQL();

			try {

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

				throw;
			}
		}

		public static bool IsCodeUnique(string code) {
			bool result = true;
			foreach (clsItem item in getItemList()) {
				if (code == item.ID.ToString()) {
					result = false;
					break;
				}
			}
			return result;
		}

		public static bool IsCodeSafeToDelete(string code) {
			itemSQL = new clsItemsSQL();
			string sql;
			int numOfRows = 0;
			foreach (clsItem item in getItemList()) {
				sql = itemSQL.QueryInvoiceByItemCode(item.ID);
				data.ExecuteSQLStatement(sql, ref numOfRows);
				if (numOfRows > 0) {
					return false;
				}
			}
			return true;
		}

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

				throw;
			}
		}

		public static void NewItem(clsItem item) {

		}

	}
	public class clsItem {
		public string ID { get { return sID; } }
		private string sID;
		public string Desc { get { return sDesc; } }
		private string sDesc;
		public decimal Cost { get { return sCost; } }
		private decimal sCost;
		public clsItem(string id, string desc, decimal cost) {
			try {
				sID = id; sDesc = desc; sCost = cost;

			}
			catch (Exception ex) {

				throw;
			}
		}
		// this class provide items that can be listed for use my data grids in various windows

	}
}