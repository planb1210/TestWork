using System;

namespace TestWorkPhysicon.DataBase.Models
{
    public class Module
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }

        public string Href { get; set; }

        public int? ParentId { get; set; }

        public string ExternalId { get; set; }

        public string ContentType { get; set; }

        public string Num { get; set; }
    }
}
