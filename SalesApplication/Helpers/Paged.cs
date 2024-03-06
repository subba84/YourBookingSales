using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApplication 
{
    public class Paged
    {
        public string Sort { get; set; }
        public string Filter { get; set; }
        public string Search { get; set; }
        public string AlternateSearch { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int Page { get; set; }             // current page 
        public int Size { get; set; }             // page size
        public int OriginalSize { get; set; }     // original page size
        public int Pages { get; set; }            // total pages
        public bool HasPrev { get; set; }
        public bool HasNext { get; set; }
        public bool Reset { get; set; }
        public int TotalRows { get; set; }
        public int NowShowing { get; set; }
        public bool ThroughSearchOrFilter { get; set; }
        //public List<KeyValueStringPair> SortList { get; set; }
        //public List<KeyValueStringPair> FilterList { get; set; }
        //public List<KeyValueStringPair> SizeList { get; set; }
        //public List<KeyValueStringPair> ToggleColumnList { get; set; }

        //Newly Added below lines for extra Filter
        public string FilterForType { get; set; }
        //public List<KeyValueStringPair> FilterListForType { get; set; }
        //public List<KeyValueStringPair> FilterListForLocation { get; set; }
        //public string FilterForRequestMode { get; set; }
        //public List<KeyValueStringPair> FilterListForRequestMode { get; set; }
        public string FilterForLocation { get; set; }

        public string FilterForDivision { get; set; }
        public string QueryFilter { get; set; }
        public string QuerySelector { get; set; }
        public string QueryFilterForRequestMode { get; set; }
        public string QuerySelectorForRequestMode { get; set; }
        public string FilterForRequestStatus { get; set; }
        //public List<KeyValueStringPair> FilterListForRequestStatus { get; set; }
        public string QueryFilterForRequestStatus { get; set; }
        public string QuerySelectorForRequestStatus { get; set; }
        public string FilterForResult { get; set; }
        public string TypeId { get; set; }
        public string Type { get; set; }

        public Paged()
        {
            Page = 1;
            Size = 10;
            //SizeList = new List<KeyValueStringPair>();
            //SizeList.Add(new KeyValueStringPair() { Key = "10", Value = "10" });
            //SizeList.Add(new KeyValueStringPair() { Key = "20", Value = "20" });
            //SizeList.Add(new KeyValueStringPair() { Key = "50", Value = "50" });
            //SizeList.Add(new KeyValueStringPair() { Key = "100", Value = "100" });

        }
        public string CurrentRoleDepartments { get; set; }
        public string CurrentRoleDivisions { get; set; }
        public string ActiveInActive { get; set; }
        public string FilterForDocVersion { get; set; }
        public int? FilterForDocVersionID { get; set; }


    }
}