using System.Collections.Generic;

namespace Panini.Entities
{
    public class BokType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<KlistremerkeBok> Boker { get; set; }

    }
}
