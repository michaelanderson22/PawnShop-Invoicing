using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project {
	public static class clsItemsLogic {

		/// <summary>
		/// Public list for app to use when it need a list of all items in the db
		/// </summary>
		/// <returns>List of cls Items</returns>
		public static List<clsItem> getItemList() {
			string sql;
			clsDataAccess data = new clsDataAccess();
			clsItemsSQL itemSQL = new clsItemsSQL();
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

		public static void NewItem(clsItem item) {
				
		}

	}
    public class clsItem
    {
        // Declare variables like this, as properties, to display them in datagrids
        public string sID { get; set; }
        public string sDesc { get; set; }
        public decimal sCost { get; set; }

        public clsItem(string id, string desc, decimal cost)
        {
            try
            {
                sID = id;
                sDesc = desc;
                sCost = cost;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Override the ToString method so that item names can be displayed in a combo box
        public override string ToString()
        {
            return sDesc;
        }

        // This class provides items that can be listed for use by data grids in various windows
    }


}