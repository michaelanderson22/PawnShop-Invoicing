using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Group_Project
{
    class clsMainSQL
    {
        /// <summary>
        /// Returns the SQL query to update the total cost of an invoice
        /// </summary>
        /// <param name="newCost"></param>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public static string updateInvoiceCost(int newCost, int invoiceNum)
        {
            return "UPDATE Invoices SET TotalCost = " + newCost +" WHERE InvoiceNum = " + invoiceNum;
        }

        /// <summary>
        /// Returns the SQL query to insert a line item.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public static string insertLineItem(int invoiceNum, int lineItemNum, string itemCode)
        {
            return "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) Values(" + invoiceNum + ", " + lineItemNum + ", '" + itemCode + "')";
        }
        /// <summary>
        /// Returns the SQL query to insert an invoice.
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public static string insertInvoice(DateTime invoiceDate, Decimal totalCost)
        {
            return "INSERT INTO Invoices(InvoiceDate, TotalCost) Values(#" + invoiceDate + "#, " + totalCost + ")";
        }

        /// <summary>
        /// returns the SQL query to get an invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public static string getInvoice(int invoiceNum)
        {
            return "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = " + invoiceNum;
        }
        /// <summary>
        /// returns the SQL query to get the ItemDesc table columns.
        /// </summary>
        /// <returns></returns>
        public static string getItemDesc()
        {
            return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
        }

        /// <summary>
        /// Returns the SQL query to get the Item code, description, and cost of the items for any given invoice.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public static string getLineItems(int invoiceNum)
        {
            return "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = " + invoiceNum;
        }

        /// <summary>
        /// Returns the SQL query to delete an invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public static string deleteInvoice(int invoiceNum)
        {
            return "DELETE FROM Invoices WHERE InvoiceNum = " + invoiceNum;
        }

        public static string getMostRecentInvoiceNumber()
        {
            return "SELECT TOP 1 InvoiceNum FROM Invoices ORDER BY InvoiceNum DESC";
        }

    }
}
