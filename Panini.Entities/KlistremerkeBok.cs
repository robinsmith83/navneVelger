using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [StringLength(255)]
        public string Navn { get; set; }

        public BokType Type { get; set; }
        public Eier Eier { get; set; }
        public List<Merke> Merker { get; set; }
        public int TotaltAntallMerker { get; set; }
        public string MerkerAvTotalt
        {
            get
            {
                return $"{Merker.Count(x => x.klistretInn)}/{TotaltAntallMerker}";
            }
        }
        public int BytteMerker
        {
            get
            {
                return Merker.Count(x => !x.klistretInn);
            }
        }

    }
}
