using System;
using System.ComponentModel.DataAnnotations;

namespace lsport.Models
{
    public class Giphy
    {
        [Key]
        public int ID { get; set; }
        public string URL { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}