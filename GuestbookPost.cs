using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Console;

namespace Posts
{
    public class GuestBookPost
    {
        // Hämtar in hur ett inlägg ska se ut och sparar ned som en lista
        static List<Post> posts = new List<Post>();

        // Filen där inläggen sparas
        static string savePostSrc = @"GuestbookPosts.json";

        // Laddar in inlägg
        public static List<Post> LoadPosts()
        {
            // Metoden gör inget om ingen json-fil finns nedsparad än
            if (!File.Exists(savePostSrc))
            {
                return new List<Post>();
            }

            // Läser in texten inom json-filen
            string jsonString = File.ReadAllText(savePostSrc);

            // Provar deserialiserar texten i json-filen till en Post-lista, annars en tom lista
            posts = JsonSerializer.Deserialize<List<Post>>(jsonString) ?? [];
            return posts;
        }

        // Raderar ett inlägg
        public static void DeletePostData(int index)
        {
            posts.RemoveAt(index); // Tar bort inlägg i listan genom indexet som angetts
            SavePost(posts); // Sparar ned den nya listan
        }

        // Sparar ett inlägg till json-fil
        public static void SavePost(List<Post> post)
        {
            string jsonString = JsonSerializer.Serialize(post);
            File.WriteAllText(savePostSrc, jsonString);
        }

        // Visar alla sparade inlägg i gränssnittet
        public static void ShowPosts()
        {
            // Hämtar in alla lagrade inlägg
            var posts = LoadPosts();

            // Skriver ut varje inlägg
            for (int i = 0; i < posts.Count; i++)
            {
                WriteLine($"[{i}] {posts[i].Author} - {posts[i].Content}");
            }
        }
    }
}
