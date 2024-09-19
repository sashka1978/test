using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Comment")]
    public class Comment
    {
       public int Id { get; set; }
       public int? StockId { get; set; }
       //navigation 
       public Stock? Stock { get; set; }
       public string Title { get; set; } = string.Empty;
       public string Content { get; set; } = string.Empty;
       public DateTime CreatedOn { get; set; } = DateTime.Now;
       public string AppUserId {get; set;} 
       public AppUser AppUser {get; set;}
    }
}