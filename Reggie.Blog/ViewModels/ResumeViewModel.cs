using System.Collections.Generic;
using Reggie.Blog.Models;

namespace Reggie.Blog.ViewModels
{
    public class ResumeViewModel
    {
        public List<Skill> Skills { get; set; }

        public List<JobExperience> JobExperiences { get; set; }

        public List<Sample> Samples { get; set; }

        public List<ContentFlag> ContentFlags { get; set; }

        public string Motto { get; set; }

        public string PersonalProfile { get; set; }
    }
}