using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs
{
    public class CreateStockRequestDto
    {     
        [Required]
        [MaxLength (10,ErrorMessage="Symbol be can't be more then 10 chars")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength (10,ErrorMessage="Company name be can't be more then 10 chars")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range (1,10000000)]
        public decimal Purchase { get; set; }  
        [Required]
        [Range (0.001,100)]  
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength (10,ErrorMessage="Industry be can't be more then 10 chars")]
        public string Industry { get; set; } = string.Empty;
        [Range (1,50000000)]  
        public long MarketCap { get; set; }
    }
}