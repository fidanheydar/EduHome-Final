using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class Slider: BaseModel
    {
    
        [StringLength(100)]
        public string? Title { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? FormFile { get; set; }
    }
}
