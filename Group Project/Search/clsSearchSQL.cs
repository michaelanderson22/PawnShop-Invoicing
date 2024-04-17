using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Group_Project {
    class clsSearchSQL {
        /// <summary>
        /// get all invoices
        /// </summary>
        /// <returns></returns>
        public static string getInvoices() {
            return "SELECT* FROM Invoices";
        }

        /// <summary>
        /// get invoices with an Id Number
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public static string getInvoice(string invoiceNum) {
            return $"SELECT* FROM Invoices WHERE InvoiceNum = {invoiceNum}";
        }

        /// <summary>
        /// get invoices with an ID and a date
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string getInvoice(string invoiceNum, string date) {
            return $"SELECT* FROM Invoices WHERE InvoiceNum = {invoiceNum} AND InvoiceDate = #{date}#"; //correct format for date?
        }

        /// <summary>
        /// get invoices with ID, date, and total cost
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="date"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static string getInvoice(string invoiceNum, string date, int cost) {
            return $"SELECT * FROM Invoices WHERE InvoiceNum = {invoiceNum} AND InvoiceDate = #{date}# AND TotalCost = {cost}";
        }

        /// <summary>
        /// get invoice with total cost
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static string getInvoice(int cost) {
            return $"SELECT * FROM Invoices WHERE TotalCost = {cost}";
        }

        /// <summary>
        /// get invoice with cost and date
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string getInvoice(int cost, string date) {
            return $"SELECT* FROM Invoices WHERE TotalCost = {cost} and InvoiceDate = #{date}#";
        }

        /// <summary>
        /// get invoice with date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string getInvoiceWithDate(string date) {
            return $"SELECT * FROM Invoices WHERE InvoiceDate = #{date}#";
        }

        public static string getInvoice(string invoiceNum, int cost) {
            return $"SELECT * FROM Invoices WHERE InvoiceNum = {invoiceNum} AND TotalCost = {cost}"; //had to write this myself idk if it's right
        }

        /// <summary>
        /// get distict invoices in ascending order by invoice number
        /// </summary>
        /// <returns></returns>
        public static string invoicesByNum() {
            return "SELECT DISTINCT(InvoiceNum) From Invoices order by InvoiceNum";
        }

        /// <summary>
        /// get distinct invoices in ascending order by date
        /// </summary>
        /// <returns></returns>
        public static string invoicesByDate() {
            return "SELECT DISTINCT(InvoiceDate) From Invoices order by InvoiceDate";
        }

        /// <summary>
        /// get distinct invoices in ascending order by cost
        /// </summary>
        /// <returns></returns>
        public static string invoicesByCost() {
            return "SELECT DISTINCT(TotalCost) From Invoices order by TotalCost";
        }

        /// <summary>
        /// get the connections between what items go to what invoices
        /// </summary>
        /// <returns></returns>
        public static string itemsAndInvoices() {
            return "SELECT * FROM LineItems";
        }


    }
}
