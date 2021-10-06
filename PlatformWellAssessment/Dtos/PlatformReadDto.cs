using PlatformWellAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformWellAssessment.Dtos
{
    public class PlatformReadDto
    {
        public int Id { get; set; }

        public string UniqueName { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Well> Well { get; set; }

    }
}
