
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Display { get; set; }
        public DateTime DatePost { get; set; }
       
        
    }
}
