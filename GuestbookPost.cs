using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Console;
using Microsoft.Data.Sqlite;

namespace Posts
{
    public class GuestBookPost
    {
        // Hämtar in hur ett inlägg ska se ut och sparar ned som en lista
        public List<Post> Posts; // Listan ändras när metoderna nedan körs

        // Sparar en variabel av interfacet
        public readonly IPostStorage postSrc;

        // Konstruktor
        public GuestBookPost(IPostStorage postSrc)
        {
            this.postSrc = postSrc;
            Posts = postSrc.LoadPosts(); // Hämtar in listan av sparade inlägg
        }

        // Lägger till nya inlägg i listan och sparar till "json-filen" eller den fil som ska användas
        public void AddPost(Post post)
        {
            Posts.Add(post); // Lägger till inlägget i listan
            postSrc.SavePost(Posts); // Sparar ned den nya listan
        }

        // Raderar ett inlägg
        public void DeletePostData(int index)
        {
            Posts.RemoveAt(index); // Tar bort inlägg i listan genom indexet som angetts
            postSrc.SavePost(Posts); // Sparar ned den nya listan
        }

        // Visar alla sparade inlägg i gränssnittet
        public void ShowPosts()
        {
            // Skriver ut varje inlägg
            for (int i = 0; i < Posts.Count; i++)
            {
                WriteLine($"[{i}] {Posts[i].Author} - {Posts[i].Content}");
            }
        }
    }
}
