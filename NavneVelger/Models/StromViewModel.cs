using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NavneVelger.Models
{
    public class StromViewModel
    {
        public string Navn { get; set; }
        public string Adresse { get; set; }
        public string Postnummer { get; set; }
        public string Poststed { get; set; }
        public string Land { get; set; }
        public double StromprisTotal { get; set; }
        public double StromprisEnergi { get; set; }
        public double StromprisAvgift { get; set; }
        public DateTime StromprisGjelderFra { get; set; }

        public int Id { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty("value")]
        public double Value { get; set; }

        public string StatusMessage { get; set; }
        public List<Consumption> Consumption { get; set; }
        public List<Consumption> ConsumptionHourly { get; set; }
    }

    public class Consumption
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        [DataType(DataType.Currency)]
        public double Cost { get; set; }
        public double Price { get; set; }
        public double ConsumptionInPeriod { get; set; }
        public string FormattedDate
        {
            get
            {
                return From.ToString("MMM yy");
            }                
        }

        public string FormattedDateTime
        {
            get
            {
                return From.ToString("g");
            }
        }

    }
}
