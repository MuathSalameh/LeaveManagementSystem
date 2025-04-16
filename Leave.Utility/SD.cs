using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Utility
{
    public class SD
    {
        public const string Role_Admin = "Admin";
        public const string Role_Supervisor = "Supervisor";
        public const string Role_Employee = "Employee";

        public const int Request_Status_Pending = 1;
        public const int Request_Status_Approved = 2;
        public const int Request_Status_Declined = 3;
        public const int Request_Status_Canceled =4;
    }
}
