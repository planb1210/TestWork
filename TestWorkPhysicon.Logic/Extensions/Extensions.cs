using TestWorkPhysicon.DataBase.Models;
using TestWorkPhysicon.Logic.Models;

namespace TestWorkPhysicon.Logic.Extensions
{
    public static class Extensions
    {
        public static CourseDTO ToCoursesDTO(this Course course, List<ModuleDTO> modules)
        {
            return new CourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Status = course.Status,
                ExternalId = course.ExternalId,
                Hash = course.Hash,
                Subject = course.Subject,
                Grade = course.Grade,
                Genre = course.Genre,
                Modules = modules
            };
        }

        public static List<ModuleDTO> ToModulesDTO(this IEnumerable<Module> modules)
        {
            return modules.Select(module => new ModuleDTO
            {
                Id = module.Id,
                Title = module.Title,
                ContentType = module.ContentType,
                CourseId = module.CourseId,
                ExternalId = module.ExternalId,
                Href = module.Href,
                Num = module.Num,
                Order = module.Order,
                ParentId = module.ParentId,
                Modules = new List<ModuleDTO>()
            }).ToList();
        }

        public static ModuleDTO ToModuleDTO(this Module module)
        {
            return new ModuleDTO {
                Id = module.Id,
                Title = module.Title,
                ContentType = module.ContentType,
                CourseId = module.CourseId,
                ExternalId = module.ExternalId,
                Href = module.Href,
                Num = module.Num,
                Order = module.Order,
                ParentId = module.ParentId,
                Modules = new List<ModuleDTO>()                
            };
        }
    }
}
