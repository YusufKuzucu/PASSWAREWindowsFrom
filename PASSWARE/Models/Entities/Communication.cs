using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASSWARE.Models.Entities
{
    public class Communication
    {
        public int Id { get; set; }
        public string InternalNumber { get; set; }
        public string InternalEmail { get; set; }
        public string ExternalNumber { get; set; }
        public string ExternalEmail { get; set; }
        public int ProjectId { get; set; }
    }
}
