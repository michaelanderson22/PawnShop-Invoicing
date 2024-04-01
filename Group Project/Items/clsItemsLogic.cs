using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project
{
    class clsItemsLogic
    {

        //!!! <<<<****>>>>> This list will be public and static for any window that needs full list of items to use
        //public List<clsItem> getItemList()
        //{
        //    int retVal = 0;

        //    DataSet ds = new DataSet();
        //    List<clsItem> itemList = new List<clsItem>();

        //    sql = clsMainSQL.getItemDesc();
        //    ds = db.ExecuteSQLStatement(sql, ref retVal);

        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            clsItem item = new clsItem();
        //            item.itemCode = row["ItemCode"].ToString();
        //            item.itemDesc = row["ItemDesc"].ToString();
        //            item.itemCost = decimal.Parse(row["Cost"].ToString());

        //            itemList.Add(item);
        //        }
        //    }
        //    return itemList;

        //}
    }

    public class clsItem
    {
        // this class provide items that can be listed for use my data grids in various windows
        public string itemCode { get; set; }
        public string itemDesc { get; set; }
        public decimal itemCost { get; set; }

    }
}