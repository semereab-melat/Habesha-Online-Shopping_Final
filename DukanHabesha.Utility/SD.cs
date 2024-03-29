﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukanHabeshaUtility
{
    public static class SD //SD means Static Details
    {
        public const string Role_User_Indi = "Individual";
        public const string Role_User_Comp = "Company";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";

       
        //constants related with order status -- processing 
        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        //constants related with payment status -- processing

        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
       /* public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";*/
        public const string PaymentStatusRejected = "Rejected";

        
        public const string SessionCart = "SessionShoppingCart";
    }
}
