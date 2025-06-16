using TestWorkPhysicon.DataBase.Models;

namespace TestWorkPhysicon.Logic.Models
{
    public class CourseDTO : Course
    {
        public List<ModuleDTO>? Modules { get; set; }
    }
}
