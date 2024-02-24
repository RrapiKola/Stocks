using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.CommentDtos
{
    public class UpdateCommentDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MinLength(5, ErrorMessage = "Title must be at least five characters")]
        [MaxLength(280,ErrorMessage ="Title maximal length is 280 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required")]
        [MinLength(10, ErrorMessage = "Content must be at least ten characters")]
        [MaxLength(1050,ErrorMessage ="Content maximal length is 1050 characters")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "StockId is required")]
        public int? StockId { get; set; }
    }
}