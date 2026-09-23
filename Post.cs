using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Posts
{
    // Skapar klass som definierar ett inlägg med skribent och innehåll
    public class Post
    {
        public required string Author {get; set;}
        public required string Content {get; set;}
    }
}