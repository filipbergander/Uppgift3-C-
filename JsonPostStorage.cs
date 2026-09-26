using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Posts
{
    // Implementerar interfacet för att gästboken ska kunna spara och ladda inlägg till json-fil
    public class JsonPostStorage : IPostStorage
    {
        // Filen där inläggen sparas
        private readonly string postSrc;

        // Tar emot och ger filnamnet där inläggen ska sparas
        public JsonPostStorage(string postSrc)
        {
            this.postSrc = postSrc;
        }

        // Laddar in alla sparade inlägg
        public List<Post> LoadPosts()
        {
            // Metoden returnerar en tom lista om ingen json-fil finns nedsparad än
            if (!File.Exists(postSrc))
            {
                return new List<Post>();
            }

            // Läser in texten inom json-filen
            string jsonString = File.ReadAllText(postSrc);

            // Provar deserialiserar texten i json-filen till en Post-lista, annars en tom lista
            List<Post> posts =
                JsonSerializer.Deserialize<List<Post>>(jsonString) ?? new List<Post>();
            return posts;
        }

        // Sparar inläggen till json-filen
        public void SavePost(List<Post> post)
        {
            string jsonString = JsonSerializer.Serialize(post);
            File.WriteAllText(postSrc, jsonString);
        }
    }
}
