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

        public List<clsItem> items = new List<clsItem>();


        /*public List<clsItem> getItemList()
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

        }*/

        public void addItemToList(clsItem item)
        {
            items.Add(item);
        }

        public void addInvoice(DateTime invoiceDate, decimal totalCost)
        {
            int retVal = 0;
            int rowsAffected = 0;
            int invoiceNum;
            DataSet ds = new DataSet();


            // Insert Invoice
            sql = clsMainSQL.insertInvoice(invoiceDate, totalCost);
            rowsAffected = db.ExecuteNonQuery(sql);

            // Get Invoice Number of added Invoice
            sql = clsMainSQL.getMostRecentInvoiceNumber();
            ds = db.ExecuteSQLStatement(sql, ref retVal);
            if (ds != null && ds.Tables.Count > 0)
            {
                invoiceNum = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                // Insert Line Item
                for (int i = 0; i < items.Count; i++)
                {
                    int lineItemNum = i + 1;

                    sql = clsMainSQL.insertLineItem(invoiceNum, lineItemNum, items[i].itemCode);
                    rowsAffected = db.ExecuteNonQuery(sql);
                }
            }

            
        }
    }

    



}
