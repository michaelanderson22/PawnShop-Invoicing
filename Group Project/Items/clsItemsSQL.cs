using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project {
	public class clsItemsSQL {
		/// <summary>
		/// This method returns a string for a SQL query to grab all records from the ItemDesc table
		/// </summary>
		/// <returns> A string SQL query</returns>
		public string QueryAllItemDesc() {
			try {
				return "select ItemCode, ItemDesc, Cost from ItemDesc;";
			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
								   MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		/// <summary>
		/// This method returns a string for a SQL query to find a unique invoice number from the ItemDesc table using an item code
		/// </summary>
		/// <param name="itemCode">Integer, corresponds to the item in the itemDesc Table</param>
		/// <returns> A string SQL query</returns>
		public string QueryInvoiceByItemCode(string itemCode) {
			try {
				return $"select distinct(InvoiceNum) from LineItems where ItemCode = '{itemCode}';";
			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
								   MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		/// <summary>
		/// This method returns a string for a SQL query to update an items description and cost by its itemcode
		/// </summary>
		/// <param name="desc"> String, description of item</param>
		/// <param name="cost"> Integer, Cost of Item</param>
		/// <param name="itemCode"> Int, item code for item in itemDesc table</param>
		/// <returns> A string SQL query</returns>
		public string UpdateItemDescAndCost(string desc, int cost, string itemCode) {
			try {
				return $"Update ItemDesc Set ItemDesc = '{desc}', Cost = {cost} where ItemCode = '{itemCode}';";

			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
								   MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}

		}
		/// <summary>
		/// This method returns a string for a SQL query to insert a new record into the ItemDesc table
		/// </summary>
		/// <param name="desc"> String, description of item</param>
		/// <param name="cost"> Integer, Cost of Item</param>
		/// <param name="itemCode"> Int, item code for item in itemDesc table</param>
		/// <returns> A string SQL query</returns>
		public string InsertNewItem(string desc, string itemCode, int cost) {
			try {
				return $"Insert into ItemDesc(ItemCode, ItemDesc, Cost) Values('{itemCode}', '{desc}', {cost});";

			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
								   MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
		/// <summary>
		/// This method returns a string for a SQL query to a record from the ItemDesc table
		/// </summary>
		/// <param name="itemCode">Integer, item to be deleted</param>
		/// <returns> A string SQL query</returns>
		public string DeleteFromItemDesc(string itemCode) {
			try {
				return $"Delete from ItemDesc Where ItemCode = '{itemCode}';";
			}
			catch (Exception ex) {
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
								   MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}

	}
}