using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public  class Person:BaseModel 
    {
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        [NotMapped]
        public IFormFile? FormFile { get; set; }

    }
}
