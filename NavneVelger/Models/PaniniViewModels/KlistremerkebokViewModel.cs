﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Panini.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NavneVelger.Models.PaniniViewModels
{
    public class KlistremerkebokViewModel
    {

        public int Id { get; set; }

        public SelectList Eiere { get; set; }
        [Required]
        [Display(Name = "Eier")]
        public int EierId { get; set; }

        [Required]
        [Display(Name = "År")]
        public int Aar { get; set; }
        public SelectList AarList { get; set; }

        [Required]
        [Display(Name = "Bok")]
        public string Navn { get; set; }

        public SelectList BokTyper { get; set; }
        [Required]
        [Display(Name = "Type")]
        public int BokTypeId { get; set; }

        [Display(Name = "Antall merker")]
        public List<Merke> Merker { get; set; }
        [Display(Name = "Merker")]
        public string MerkeString { get; set; }
        public string BytteString {
            get
            {
                if (Merker != null)
                    return String.Join(" - ", Merker.OrderBy(x => x.Nummer).Where(x => !x.klistretInn).Select(x => x.Nummer));

                return "";
            }
        }

        public string Mangler
        {
            get
            {
                string s = "";
                List<string> klistretInn = Merker.OrderBy(x => x.Nummer)
                for (int i = 0; i <= TotaltAntallMerker; i++)
                {

                }


                if (Merker != null)
                    return String.Join(" - ", Merker.OrderBy(x => x.Nummer).Where(x => !x.klistretInn).Select(x => x.Nummer));

                return "";
            }
        }


        [Display(Name = "Totalt antall merker")]
        public int TotaltAntallMerker { get; set; }

        public string MerkerAvTotalt
        {
            get
            {
                return $"{Merker?.Count(x => x.klistretInn)}/{TotaltAntallMerker}";
            }
            set
            {
                MerkerAvTotalt = value;
            }
        }

        public int? BytteMerker
        {
            get
            {
                return Merker?.Count(x => !x.klistretInn);
            }
            set
            {
                BytteMerker = value;
            }
        }

        public List<KlistremerkeBok> Boker { get; set; }

        public string StatusMessage { get; set; }
    }
}
