﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesApplication 
{
    public class TicketInfo
    {
        public int Id { get; set; }
        public string EmailId { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string SeatNumbers { get; set; }
        public Nullable<System.DateTime> DepartureOn { get; set; }
        public Nullable<System.DateTime> ArrivalOn { get; set; }
        public Nullable<bool> IsSMSSent { get; set; }
        public string TicketNumber { get; set; }
        public string TicketFare { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ChangedBy { get; set; }
        public string ChangedByName { get; set; }
        public Nullable<System.DateTime> ChangedOn { get; set; }
        public string DeletedBy { get; set; }
        public string DeletedByName { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }

        public string BoardingPoint { get; set; }
        public string DepartureTime { get; set; }
        public string OperatorContactNumber { get; set; }
        public string OperatorName { get; set; }
        public string SalesEntryMobileNumber { get; set; }
        public string JourneyFrom { get; set; }
        public string JourneyTo { get; set; }
        public string BusNumber { get; set; }
        public string GST { get; set; }
        public string ServiceCharge { get; set; }
        public string TotalFare { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
    }
}