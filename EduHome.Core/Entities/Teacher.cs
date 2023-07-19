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
    public class Teacher : BaseModel
    {
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? Mail { get; set; }
        [Required]
        public string? About { get; set; }
        [Required]
        public int? ExperienceYear { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Faculty { get; set; }
        [Required]
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? FormFile { get; set; }
        [Required]
        public int? PositionId { get; set; }
        [Required]
        public TeacherPosition?  TeacherPosition { get; set; }
        public int? DegreeId { get; set; }
        public TeacherDegree ? TeacherDegree { get; set; }
        public List<TeacherSkill> ?TeacherSkills { get; set; }
        public List <TeacherSocial> ?TeacherSocials { get; set; }
        public List<TeacherHobby>? TeacherHobbies { get; set; }
        [NotMapped]
        public List<int>? HobbyIds { get; set; }

    }

}
