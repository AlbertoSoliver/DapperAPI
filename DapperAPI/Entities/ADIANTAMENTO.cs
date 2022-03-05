using System;
using System.ComponentModel.DataAnnotations;

namespace DapperAPI.Entities
{
    public class ADIANTAMENTO
    {
        [Key]
        public int ID { get; set; }
        public int CONTRATOPROFITSHAREID { get; set; }
        public DateTime DATAADIANT { get; set; }
        public int PERIODOREF { get; set; }
        public Decimal VALORADIANT { get; set; }
    }
}
