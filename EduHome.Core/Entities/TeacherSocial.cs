﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class TeacherSocial:BaseModel
    {
        [Required]
        public string ? Link { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ? TeacherId { get; set; }
        [Required]
        public Teacher ? Teacher { get; set; }
    }
}
