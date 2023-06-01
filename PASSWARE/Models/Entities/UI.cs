using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASSWARE.Models.Entities
{
    public class UI
    {
        public int Id { get; set; }
        public string UIServerIP { get; set; }
        public string UIServerUserName { get; set; }
        public string UIServerPassword { get; set; }
        public int ProjectId { get; set; }
    }
}
