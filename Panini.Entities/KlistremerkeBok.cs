using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Panini.Entities
{
    public class KlistremerkeBok
    {
        public KlistremerkeBok()
        {
            Merker = new List<Merke>();
        }
        public int Id { get; set; }

        public int Aar { get; set; }

        [Required]
        [StringLength(255)]
        public string Navn { get; set; }

        [Required]
        public BokType Type { get; set; }
        [Required]
        public Eier Eier { get; set; }
        public List<Merke> Merker { get; set; }
    }
}
