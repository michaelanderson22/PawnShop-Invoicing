using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Media.Media3D;

namespace Group_Project
{
    class clsMainLogic
    {
        /// <summary>
        /// clsDataAccess object. Handles all database calls.
        /// </summary>
        clsDataAccess db = new clsDataAccess();

        /// <summary>
        /// Holds the sql statement to be executed by clsDataAccess.
        /// </summary>
        public string sql = "";


        public List<clsItem> getItemList()
        {
            int retVal = 0;

            DataSet ds = new DataSet();
            List<clsItem> itemList = new List<clsItem>();

            sql = clsMainSQL.getItemDesc();
            ds = db.ExecuteSQLStatement(sql, ref retVal);

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    clsItem item = new clsItem();
                    item.itemCode = row["ItemCode"].ToString();
                    item.itemDesc = row["ItemDesc"].ToString();
                    item.itemCost = decimal.Parse(row["Cost"].ToString());

                    itemList.Add(item);
                }
            }
            return itemList;

        }
    }

    
}
