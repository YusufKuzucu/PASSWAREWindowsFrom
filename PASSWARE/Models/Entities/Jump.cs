using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASSWARE.Models.Entities
{
    public class Jump
    {
        public int Id { get; set; }
        public string JumpServerIP { get; set; }
        public string JumpServerUserName { get; set; }
        public string JumpServerPassword { get; set; }
        public int ProjectId { get; set; }
    }
}
