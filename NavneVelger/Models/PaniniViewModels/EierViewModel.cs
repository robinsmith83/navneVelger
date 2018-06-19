using Panini.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NavneVelger.Models.PaniniViewModels
{
    public class EierViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Navn")]
        public string Navn { get; set; }

        public int Id { get; set; }

        public List<Eier> Eiere { get; set; }

        public string StatusMessage { get; set; }
    }
}
