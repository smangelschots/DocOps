using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocOps.Models
{

    public enum OutputFormat
    {
        HTML,
        //PDF,
        //DOCX
    }


    public class ProjectConfiguration
    {
        public string Id { get; internal set; } = Guid.NewGuid().ToString();

        public required string Name { get; set; }

        public required string Path { get; set; } 

        public OutputFormat OutputFormat { get; set; } = OutputFormat.HTML;

    }
}
