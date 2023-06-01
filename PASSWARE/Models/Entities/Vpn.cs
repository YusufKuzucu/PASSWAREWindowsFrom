using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASSWARE.Models.Entities
{
    public class Vpn
    {
        public int Id { get; set; }
        public string VpnProgramName { get; set; }
        public string VpnConnectionAddress { get; set; }
        public string VpnPassword { get; set; }
        public int ProjectId { get; set; }
    }
}
