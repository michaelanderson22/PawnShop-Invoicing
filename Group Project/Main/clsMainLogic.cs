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

        /// <summary>
        /// Stores the current invoice number for editing.
        /// </summary>
        public int currentInvoiceNum;

        private clsSearchLogic searchLogic = new clsSearchLogic();


        /// <summary>
        /// Adds an item to the list to be added to the invoice
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Returns an invoice by its invoice number.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public clsInvoice getInvoiceByInvoiceNum(int invoiceNum)
        {
            List<clsInvoice> invoiceList = new List<clsInvoice>();

            // Populate invoice list
            invoiceList = searchLogic.getInvoices();
            try
            {
                // Search invoice list for invoice number, return invoice
                foreach (clsInvoice invoice in invoiceList)
                {
                    if (invoice.theInvoiceNum == invoiceNum)
                    {
                        return invoice;
                    }
                }
                // No invoice found
                return null;
            }
            catch
            {
                throw new Exception("Error getting invoice by invoice number");
            }
            
        }

        /// <summary>
        /// Returns the total cost of the invoice
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Adds an invoice to the database
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Edits an invoice
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <exception cref="Exception"></exception>
        public void editInvoice(DateTime invoiceDate)
        {
            try
            {
                int retVal = 0;
                int rowsAffected = 0;
                int invoiceNum = this.currentInvoiceNum;
                DataSet ds = new DataSet();


                decimal totalCost = getTotalCost();

                // Update Invoice Table
                sql = clsMainSQL.updateInvoice(invoiceDate, invoiceNum, totalCost);
                rowsAffected = db.ExecuteNonQuery(sql);


                // Delete Line  Items from Invoice, before adding new ones.
                sql = clsMainSQL.deleteLineItems(invoiceNum);
                rowsAffected = db.ExecuteNonQuery(sql);


                // Insert Line Item
                for (int i = 0; i < addedItems.Count; i++)
                {
                    int lineItemNum = i + 1;

                    sql = clsMainSQL.insertLineItem(invoiceNum, lineItemNum, addedItems[i].ID);
                    rowsAffected = db.ExecuteNonQuery(sql);
                }
            }
            catch
            {
                throw new Exception("Error editing invoice");
            }
        }

        
        /// <summary>
        /// Deletes an invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <exception cref="Exception"></exception>
        public void deleteInvoice(int invoiceNum)
        {
            try
            {
                int retVal = 0;
                int rowsAffected = 0;
                DataSet ds = new DataSet();

                // Delete Line Items Entries
                sql = clsMainSQL.deleteLineItems(invoiceNum);
                rowsAffected = db.ExecuteNonQuery(sql);

                // Delete Invoice Table Entry
                sql = clsMainSQL.deleteInvoice(invoiceNum);
                rowsAffected = db.ExecuteNonQuery(sql);

                
            }
            catch
            {
                throw new Exception("Error deleting invoice");
            }
        }
    }

    



}
