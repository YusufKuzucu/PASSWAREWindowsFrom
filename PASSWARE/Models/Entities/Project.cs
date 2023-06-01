using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASSWARE.Models.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectServerIP { get; set; }
        public string ProjectServerUserName { get; set; }
        public string ProjectServerPassword { get; set; }
        public int CompanyId { get; set; }
    }
}
