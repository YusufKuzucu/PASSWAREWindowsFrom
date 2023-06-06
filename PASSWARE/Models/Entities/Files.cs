using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASSWARE.Models.Entities
{
    public class Files
    {
        public int Id { get; set; }
        public string ConnectExplanation { get; set; }
        public byte[] ConnectionInfo { get; set; }
        public int ProjectId { get; set; }
    }
}
