using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post
{
    // Skapar klass som definierar ett inlägg med skribent och innehåll
    public class NewPost
    {
        public required string Author {get; set;}
        public required string Content {get; set;}
    }
}