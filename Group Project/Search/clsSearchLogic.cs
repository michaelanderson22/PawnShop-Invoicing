using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.TextFormatting;

namespace Group_Project {
    class clsSearchLogic {
        DataSet ds;
        clsDataAccess dataAccess = new clsDataAccess();
        /// <summary>
        /// the list of invoices to populate the cb with
        /// </summary>
        List<clsInvoice> invoices;
        /// <summary>
        /// bool to see if an id was selected
        /// </summary>
        public static bool id = false;
        /// <summary>
        /// bool to see if a date was selected
        /// </summary>
        public static bool date = false;
        /// <summary>
        /// bool to see if a cost was selected
        /// </summary>
        public static bool cost = false;
        /// <summary>
        /// list of items for invoices
        /// </summary>
        List<clsItem> items;

        /*
        private void InvoiceSelected_Click(object sender, RoutedEventArgs e) {
            // Retrieve selected invoice information from UI elements
            SelectedInvoice = "Selected Invoice Information"; // Replace with actual selected invoice data
            DialogResult = true; // Set DialogResult to true to indicate a valid selection
            Close(); // Close the search window
        /
        SelectedInvoice - this is the casing to use so micheal can call the attribute ( int invoiceID - make it static maybe?)
        } */

        /// <summary>
        /// returns a list of ALL of the invoices
        /// </summary>
        /// <returns></returns>
        public List<clsInvoice> getInvoices() { return queryInvoices(clsSearchSQL.getInvoices()); }

        /// <summary>
        /// get all distict ID#s for the cb
        /// </summary>
        /// <returns></returns>

        public List<string> getNumbers() {
            int iRet = 0;
            List<string> numbers = new List<string>();
            string sSQL = clsSearchSQL.invoicesByNum();
            ds = dataAccess.ExecuteSQLStatement(sSQL, ref iRet);
            string temp;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                temp = ds.Tables[0].Rows[i][0].ToString();
                numbers.Add(temp);
            }
            return numbers;
        }

        /// <summary>
        /// get all distict dates for cb
        /// </summary>
        /// <returns></returns>
        public List<string> getDates() {
            int iRet = 0;
            List<string> dates = new List<string>();
            string sSQL = clsSearchSQL.invoicesByDate();
            ds = dataAccess.ExecuteSQLStatement(sSQL, ref iRet);
            string temp;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                temp = ds.Tables[0].Rows[i][0].ToString();
                dates.Add(temp);
            }
            return dates;
        }

        /// <summary>
        /// get all distinct costs for cb
        /// </summary>
        /// <returns></returns>
        public List<string> getCosts() {
            int iRet = 0;
            List<string> costs = new List<string>();
            string sSQL = clsSearchSQL.invoicesByCost();
            ds = dataAccess.ExecuteSQLStatement(sSQL, ref iRet);
            string temp;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                temp = ds.Tables[0].Rows[i][0].ToString();
                costs.Add(temp);
            }
            return costs;
        }

        /// <summary>
        /// sets a string to query the database by or something like that?
        /// </summary>
        /// <returns></returns>
        public List<clsInvoice> beginSearch() {
            string sSQL = "";
            int theCost = wndSearch.selectedCost;
            string theDate = wndSearch.selectedDate;
            string theNumber = wndSearch.invoiceNumber.ToString();
            if (id && cost && date) {
                sSQL = clsSearchSQL.getInvoice(theNumber, theDate, theCost);
            } else if (id && cost) {
                sSQL = clsSearchSQL.getInvoice(theNumber, theCost);
            } else if (id && date) {
                sSQL = clsSearchSQL.getInvoice(theNumber, theDate);
            } else if (cost && date) {
                sSQL = clsSearchSQL.getInvoice(theCost, theDate);
            } else if (id) {
                sSQL = clsSearchSQL.getInvoice(theNumber);
            } else if (cost) {
                sSQL = clsSearchSQL.getInvoice(theCost);
            } else { //when they selected only the date
                sSQL = clsSearchSQL.getInvoiceWithDate(theDate);
            }
            return queryInvoices(sSQL);
        }

        /// <summary>
        /// takes in an SQL string and returns a list created from the dataset 
        /// </summary>
        /// <param name="sSQL"></param>
        /// <returns></returns>
        public List<clsInvoice> queryInvoices(string sSQL) {
            invoices = new List<clsInvoice>();
            int iRet = 0;
            string tempString;
            int tempInt;
            string dateString;
            string newString;

            ds = dataAccess.ExecuteSQLStatement(sSQL, ref iRet);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                clsInvoice temp = new clsInvoice();
                Int32.TryParse(ds.Tables[0].Rows[i][0].ToString(), out tempInt); //invoice number
                temp.theInvoiceNum = tempInt;
                //dateString = (string)ds.Tables[0].Rows[i][1];
                //DateTime dateTime = DateTime.ParseExact(dateString, "M/d/yyyy", CultureInfo.InvariantCulture);
                //temp.theDate = dateTime;
                temp.theDate = (DateTime)ds.Tables[0].Rows[i][1]; //date
                var smt = ds.Tables[0].Rows[i][1];
                var help = ds.Tables[0].Rows[i][1].GetType();
                Int32.TryParse(ds.Tables[0].Rows[i][2].ToString(), out tempInt); //total cost
                temp.theCost = tempInt;

                temp.theItems = getItems(temp.theInvoiceNum);//items

                newString = "";
                for (int j = 0; j < temp.theItems.Count; j++) {
                    newString += temp.theItems[j].Desc;
                    newString += "\n";
                }
                temp.allTheItems = newString;
                invoices.Add(temp);
            }


            return invoices;
        }

        /// <summary>
        /// gets items for an invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public List<clsItem> getItems(int invoiceNum) {
            items = clsItemsLogic.getItemList(); //list of ALL the items from the database
            int iRet = 0;
            int tempInt = 0;
            string sSQL = clsSearchSQL.itemsAndInvoices();
            List<string> neededItems = new List<string>();
            List<clsItem> theItems = new List<clsItem>();

            DataSet ds = dataAccess.ExecuteSQLStatement(sSQL, ref iRet);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                Int32.TryParse(ds.Tables[0].Rows[i][0].ToString(), out tempInt); //invoice number from ds
                if(tempInt == invoiceNum) {
                    neededItems.Add(ds.Tables[0].Rows[i][2].ToString()); 
                }
            }
            //A B C
            //:D   !!!
            while (neededItems.Count > 0) {
                foreach (clsItem item in items) {
                    if (item.ID == neededItems[0]) {
                        theItems.Add(item);
                        neededItems.Remove(item.ID);
                        break;
                    }
                }
            }

            return theItems;
        }
    }

    class clsInvoice {
        int invoiceNum;
        DateTime date;
        int cost;
        List<clsItem> items;
        string allItems;

        /// <summary>
        /// get and set invoice #
        /// </summary>
        public int theInvoiceNum { get { return invoiceNum; } set { invoiceNum = value; } }

        /// <summary>
        /// get and set the date
        /// </summary>
        public DateTime theDate { get { return date; } set { date = value; } }

        /// <summary>
        /// get and set the cost
        /// </summary>
        public int theCost { get { return cost; } set { cost = value; } }

        /// <summary>
        /// get and set the string of all the item names
        /// </summary>
        public string allTheItems { get { return allItems; } set { allItems = value; } }

        /// <summary>
        /// get and set the list of clsItems
        /// </summary>
        public List<clsItem> theItems { get { return items; } set { items = value; } }

        
    }
}
