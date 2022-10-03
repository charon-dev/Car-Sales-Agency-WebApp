using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSalesAgency.Utility
{
    public static class SD
    {
        //lowest role(coming/browsing in the website)
        public const string Role_User_Indi = "Individual";
        public const string Role_Admin = "Admin";

        //initial status when the order is created
        public const string StatusPending = "Pending";
        //If it's a cutomer the the payement is approved, we change the status to approved
        public const string StatusApproved = "Approved";
        //Will be updated by admin when they are processing the order
        public const string StatusInProcess = "Processing";
        //After the process is done the order will be shipped(Final status)
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        //Initial
        public const string PayementStatusPending = "Pending";
        //Payement is done it will be approved
        public const string PayementStatusApproved = "Approved";
        public const string PayementStatusRejected = "Rejected";

        //Key name of session
        public const string SessionCart = "SessionShoppingCart";
    }
}
