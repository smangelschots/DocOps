using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocOps.Models
{
    public class ApplicationConfiguration
    {
        public string Id { get; internal set; } = Guid.NewGuid().ToString();
        public string Path { get; set; } = @"c:\temp\DocOps\";
        public required string SelectedProject { get; set; }

        public IList<ProjectConfiguration> ProjectConfigurations { get; set; } = new List<ProjectConfiguration>();




    }
}
