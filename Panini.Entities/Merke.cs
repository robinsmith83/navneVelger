using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Panini.Entities
{
    public class Merke
    {
        public int Id { get; set; }

        [Required]
        public int Nummer { get; set; }

        [Required]
        public KlistremerkeBok Bok { get; set; }
        public int BokId { get; set; }

        [Required]
        public bool klistretInn { get; set; }
    }
}
