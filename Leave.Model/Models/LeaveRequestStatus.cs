using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Model.Models
{
    public class LeaveRequestStatus
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}
