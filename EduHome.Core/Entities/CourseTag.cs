﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public  class CourseTag : BaseModel
    {
        public int TagIdd { get; set; }
        public Tag? Tag { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
