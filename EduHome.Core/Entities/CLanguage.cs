﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class CLanguage:BaseModel
    {
        [Required]
        public string Name { get; set; }
        public List<Course>? Courses { get; set; }
    }

}
