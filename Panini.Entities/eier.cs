using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Panini.Entities
{
    public class Eier
    {
        public Eier()
        {
            Boker = new List<KlistremerkeBok>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Navn { get; set; }
        public List<KlistremerkeBok> Boker { get; set; }
        public string UserId { get; set; }

    }
}
