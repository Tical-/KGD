using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGD.Domain.Entity
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public string KGDCode { get; set; }
        public string Deportament { get; set; }
        public string BIN { get; set; }
        public string NPName { get; set; }
    }
}
