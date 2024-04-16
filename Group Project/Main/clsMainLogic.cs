using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Media.Media3D;
using System.Collections.ObjectModel;

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

        public ObservableCollection<clsItem> addedItems = new ObservableCollection<clsItem>();


        public void addItemToList(clsItem item)
        {
            try
            {
                addedItems.Add(item);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding item to list");
            }
        }

        public decimal getTotalCost()
        {
            try
            {
                decimal totalCost = 0;
                foreach (clsItem item in addedItems)
                {
                    totalCost += item.Cost;
                }
                return totalCost;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting total cost");
            }
            
        }

        public void addInvoice(DateTime invoiceDate)
        {
            try
            {
                int retVal = 0;
                int rowsAffected = 0;
                int invoiceNum;
                DataSet ds = new DataSet();

                decimal totalCost = getTotalCost();


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
                    for (int i = 0; i < addedItems.Count; i++)
                    {
                        int lineItemNum = i + 1;

                        sql = clsMainSQL.insertLineItem(invoiceNum, lineItemNum, addedItems[i].ID);
                        rowsAffected = db.ExecuteNonQuery(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding invoice. Please fill out all required fields.");

            }
            
        }
    }

    



}
