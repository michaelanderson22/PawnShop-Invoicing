using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.TextFormatting;

namespace Group_Project {
    class clsSearchLogic {
        DataSet ds;
        clsDataAccess dataAccess = new clsDataAccess();
        List<clsInvoice> invoices;

        
        /*
        private void InvoiceSelected_Click(object sender, RoutedEventArgs e) {
            // Retrieve selected invoice information from UI elements
            SelectedInvoice = "Selected Invoice Information"; // Replace with actual selected invoice data
            DialogResult = true; // Set DialogResult to true to indicate a valid selection
            Close(); // Close the search window
        /
        SelectedInvoice - this is the casing to use so micheal can call the attribute ( int invoiceID - make it static maybe?)
        } */



        public List<clsInvoice> getInvoices(/*variables here?*/) {
            invoices = new List<clsInvoice>();
            int iRet = 0;
            string tempString;
            int tempInt;

            string sSQL = clsSearchSQL.getInvoices();
            ds = dataAccess.ExecuteSQLStatement(sSQL, ref iRet);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                clsInvoice temp = new clsInvoice();
                Int32.TryParse(ds.Tables[0].Rows[i][0].ToString(), out tempInt); //invoice number
                temp.theInvoiceNum = tempInt;
                tempString = ds.Tables[0].Rows[i][1].ToString(); //invoice date
                temp.theDate = tempString;
                Int32.TryParse(ds.Tables[0].Rows[i][2].ToString(), out tempInt); //total cost
                temp.theCost = tempInt;
                //where do i get the list of items?
                invoices.Add(temp);
            }
            return invoices;
        }

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


    
    }

    class clsInvoice {
        int invoiceNum;
        string date;
        int cost;
        List<clsItem> items;

        public int theInvoiceNum { get { return invoiceNum; }  set { invoiceNum = value; } }

        public string theDate { get { return date; } set { date = value; } }

        public int theCost { get { return cost; } set { cost = value; } }

        public List<clsItem> theItems { get { return items; } set { items = value; } }
    }


}
