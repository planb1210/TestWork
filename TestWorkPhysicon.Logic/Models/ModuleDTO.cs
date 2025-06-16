using TestWorkPhysicon.DataBase.Models;

namespace TestWorkPhysicon.Logic.Models
{
    public class ModuleDTO : Module
    {
        public List<ModuleDTO>? Modules { get; set; }
    }
}
