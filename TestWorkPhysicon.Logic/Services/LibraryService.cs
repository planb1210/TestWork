using TestWorkPhysicon.DataBase.Models;
using TestWorkPhysicon.DataBase.Repositories;
using TestWorkPhysicon.Logic.Extensions;
using TestWorkPhysicon.Logic.Models;

namespace TestWorkPhysicon.Logic.Services
{
    public interface ILibraryService
    {
        Task<IEnumerable<CourseDTO>> GetModules(string subject, string grade, string genre, CancellationToken token = default);
    }
    public class LibraryService : ILibraryService
    {
        public readonly IModulesRepository _modulesRepository;
        public readonly ICoursesRepository _coursesRepository;
        public LibraryService(IModulesRepository modulesRepository, ICoursesRepository coursesRepository)
        {
            _modulesRepository = modulesRepository;
            _coursesRepository = coursesRepository;
        }

        public async Task<IEnumerable<CourseDTO>> GetModules(string subject, string grade, string genre, CancellationToken token = default)
        {
            var courses = await _coursesRepository.GetCourses(subject, grade, genre, token: token);
            if (courses is null || courses.Count() == 0)
                return new List<CourseDTO>();

            var courseIds = courses.Select(x => x.Id);
            var modules = await _modulesRepository.GetModules(courseIds, token: token);

            return GetThree(courses, modules);
        }

        private IEnumerable<CourseDTO> GetThree(IEnumerable<Course> courses, IEnumerable<Module> modules)
        {
            var coursesResult = new List<CourseDTO>();
            foreach (var course in courses)
            {
                var findModulesTop = modules.Where(x => x.CourseId == course.Id && x.ParentId == null).ToModulesDTO();

                if (findModulesTop.Count > 0)
                {
                    foreach (var module in findModulesTop)
                    {
                        var findModules = FindModules(module, modules);
                        if (findModules.Count > 0)
                        {
                            module.Modules.AddRange(findModules);
                        }
                    }

                    coursesResult.Add(course.ToCoursesDTO(findModulesTop));
                }
            }
            return coursesResult.OrderBy(c => c.Title);
        }


        private List<ModuleDTO> FindModules(ModuleDTO moduleDTO, IEnumerable<Module> modules)
        {
            var modulesDTO = modules.Where(x => x.ParentId == moduleDTO.Id).ToModulesDTO();

            if (modulesDTO.Count() == 0)
                return new List<ModuleDTO>();

            foreach (var module in modulesDTO)
            {
                var findModules = FindModules(module, modules);
                if (findModules.Count > 0)
                    module.Modules.AddRange(findModules);
            }

            return modulesDTO.OrderBy(c => c.Num).ToList();
        }
    }
}
