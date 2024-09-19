using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Comments
{
    public class CreateCommentsRequestDto
    {
       [Required]
       [MinLength(5,ErrorMessage="Title must be atleast 5 chars")]
       [MaxLength (280,ErrorMessage="Title must be can't be more then 280 chars")]
       public string Title { get; set; } = string.Empty;
       [Required]
       [MinLength(5,ErrorMessage="Content must be atleast 5 chars")]
       [MaxLength (280,ErrorMessage="Content must be can't be more then 280 chars")]

       public string Content { get; set; } = string.Empty;
    }
}