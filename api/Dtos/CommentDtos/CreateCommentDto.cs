using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.CommentDtos
{
    public class CreateCommentDto
    {
        public string Title { get; set; }=string.Empty;
        public string Content { get; set; }=string.Empty;

    }
}