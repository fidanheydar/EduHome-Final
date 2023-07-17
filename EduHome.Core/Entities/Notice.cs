using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class Notice:BaseModel
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
