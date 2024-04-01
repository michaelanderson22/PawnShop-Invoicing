using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project
{
    class clsItemsSQL
    {
        /// <summary>
        /// This method returns a string for a SQL query to grab all records from the ItemDesc table
        /// </summary>
        /// <returns> A string SQL query</returns>
        public string QueryAllItemDesc()
        {
            return "select ItemCode, ItemDesc, Cost from ItemDesc;";
        }
        /// <summary>
        /// This method returns a string for a SQL query to find a unique invoice number from the ItemDesc table using an item code
        /// </summary>
        /// <param name="itemCode">Integer, corresponds to the item in the itemDesc Table</param>
        /// <returns> A string SQL query</returns>
        public string QueryInvoiceByItemCode(int itemCode)
        {
            return $"select distinct(InvoiceNum) from LineItems where ItemCode = {itemCode};";
        }
        /// <summary>
        /// This method returns a string for a SQL query to update an items description and cost by its itemcode
        /// </summary>
        /// <param name="desc"> String, description of item</param>
        /// <param name="cost"> Integer, Cost of Item</param>
        /// <param name="itemCode"> Int, item code for item in itemDesc table</param>
        /// <returns> A string SQL query</returns>
        public string UpdateItemDescAndCost(string desc, int cost, int itemCode)
        {
            return $"Update ItemDesc Set ItemDesc = {desc}, Cost = {cost} where ItemCode = {itemCode};";

        }
        /// <summary>
        /// This method returns a string for a SQL query to insert a new record into the ItemDesc table
        /// </summary>
        /// <param name="desc"> String, description of item</param>
        /// <param name="cost"> Integer, Cost of Item</param>
        /// <param name="itemCode"> Int, item code for item in itemDesc table</param>
        /// <returns> A string SQL query</returns>
        public string InsertNewItem(string desc, int itemCode, int cost)
        {
            return $"Insert into ItemDesc(ItemCode, ItemDesc, Cost) Values({itemCode}, {desc}, {cost});";
        }
        /// <summary>
        /// This method returns a string for a SQL query to a record from the ItemDesc table
        /// </summary>
        /// <param name="itemCode">Integer, item to be deleted</param>
        /// <returns> A string SQL query</returns>
        public string DeleteFromItemDesc(int itemCode)
        {
            return $"Delete from ItemDesc Where ItemCode = {itemCode};";
        }

    }
}