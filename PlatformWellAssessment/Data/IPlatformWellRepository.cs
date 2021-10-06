using PlatformWellAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformWellAssessment.Data
{
    public interface IPlatformWellRepository
    {
        bool SaveChanges();

        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);
        void CreatePlatform(Platform platform);
        void UpdatePlatform(Platform platform);
        void DeletePlatform(Platform platform);
    }
}
