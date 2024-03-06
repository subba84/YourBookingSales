using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesApplication
{
    //public class Enum
    //{

    //}

    public static class MasterTypeIds
    {
        public const int Role = 1;
        public const int Department = 2;
        public const int Branch = 5;
        public const int BranchRating = 6;
    }
    public static class RoleIds
    {
        public const int Admin = 1;
        public const int Approver = 2;
        public const int Sales = 3;
        public const int Observer = 4;
        public const int Finance = 5;
        public const int CompanyAdmin = 6;
    }

    public static class SaleStatus
    {
        public const int Save = 1;
        public const int Submit = 2;
        public const int Approve = 3;
        public const int Review = 4;
        public const int Reject = 5;
        public const int SaveRenewal = 7;
        public const int SubmitRenewal = 8;
        public const int ApproveRenewal = 9;
        public const int RejectRenewal = 10;
    }
    public static class SaleStatusName
    {
        public const string Save = "Save";
        public const string Submit = "Submit";
        public const string Approve = "Approve";
        public const string Review = "Review";
        public const string Reject = "Reject";
        public const string RenewalReject = "RenewalReject";
        public const string RenewalSubmit = "RenewalSubmit";
        public const string RenewalApprove = "RenewalApprove";
    }
    public static class SMSPrice
    {
        public const string SMSCharges = "100";

    }

    public class AppModules
    {
        public const int HajandUmrahPackage = 1;
        public const int SearchHajandUmrahPackage = 2;
        public const int CancelHajUmrahPackage = 3;
        public const int HajUmrahTermsandConditions = 4;
        public const int HajUmrahReports = 5;

        public const int BusBooking = 6;
        public const int SearchBusBooking = 7;
        public const int CancelBusBooking = 8;
        public const int BusBookingTermsandConditions = 9;
        public const int BusBookingReports = 10;

        public const int CarHire = 11;
        public const int SearchCarHire = 12;
        public const int CancelCarHire = 13;
        public const int CarHireTermsandConditions = 14;
        public const int CarHireReports = 15;

        public const int BusHire = 16;
        public const int SearchBusHire = 17;
        public const int CancelBusHire = 18;
        public const int BusHireTermsandConditions = 19;
        public const int BusHireReports = 20;

        public const int BookPackage = 21;
        public const int SearchPackage = 22;
        public const int CancelPackage = 23;
        public const int PackageTermsandConditions = 24;
        public const int PackageReports = 25;

        public const int GroupCreation = 26;

        public const int UmrahPackage = 27;
        public const int SearchUmrahPackage = 28;
        public const int CancelUmrahPackage = 29;
        public const int UmrahPackageReports = 30;
        public const int UmrahCancellationPolicy = 31;

        public const int Invoices = 32;
        public const int ManageUsers = 33;
        public const int ManageBranches = 34;
        public const int ManageUserModules = 35;
    }
}