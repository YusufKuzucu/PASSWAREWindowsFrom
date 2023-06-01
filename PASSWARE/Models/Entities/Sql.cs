using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASSWARE.Models.Entities
{
    public class Sql
    {
        public int Id { get; set; }
        public string SqlServerIp { get; set; }
        public string SqlServerUserName { get; set; }
        public string SqlsServerPassword { get; set; }
        public int ProjectId { get; set; }
    }
}
