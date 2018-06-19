using Panini.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NavneVelger.Models.PaniniViewModels
{
    public class MerkerViewModel
    {
        public int Id
        {
            get
            {
                return Merke.Id;
            }

            set
            {
                if (Merke != null)
                    Merke.Id = value;
                else
                    Merke = new Merke
                    {
                        Id = value,
                        klistretInn = false
                    };
            }
        }

        public Merke Merke { get; set; }

        public List<Merke> Merker { get; set; }

        public string StatusMessage { get; set; }
    }
}
