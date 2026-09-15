/*
* Uppgift 3, Miun 2026
* Filnamn: Program.cs
* Författare: Filip Bergander
* Datum: 2026-09-15
* Kurs: Programmering i C#
*
* Beskrivning:
* En konsollapplikation med en gästbok där användare kan
* skriva ett inlägg, ta bort ett inlägg samt visa alla inlägg.
*
*/

using System.Collections.Generic;
using System.Text.Json;
using Post;
using static System.Console;

namespace Guestbook
{
    class Program
    {
        // Hämtar in hur ett inlägg ska se ut och sparar ned till en lista
        static List<NewPost> GuestBookPosts = new List<NewPost>();
        static string savePostSrc = "GuestbookPosts.json";

        static void Main(string[] args)
        {
            LoadPosts();
            // Visar menyn
            LoadMenu();
            // Läser in vilken tangent som klickades
            ConsoleKeyInfo keyClicked = ReadKey(true);
            // Utför olika metoder i programmet när olika knappar klickas, 1/2/X
            switch (keyClicked.Key)
            {
                case ConsoleKey.D1: // Skapar ett nytt inlägg när man klickar på 1
                case ConsoleKey.NumPad1:
                    Clear();
                    CreatePost();
                    break;
                case ConsoleKey.D2: // Går till metoden för att ta bort en post vid klick på 2
                case ConsoleKey.NumPad2:
                    Clear();
                    RemovePost();
                    break;
                case ConsoleKey.X: // Stänger ned programmet när man klickar på tangenten X
                    WriteLine("\nProgrammet börjar stängas ned...");
                    Thread.Sleep(1000);
                    WriteLine("3...");
                    Thread.Sleep(700);
                    WriteLine("2...");
                    Thread.Sleep(700);
                    WriteLine("1...");
                    Thread.Sleep(700);
                    WriteLine("Programmet avslutas!");
                    Thread.Sleep(900);
                    Clear();
                    break;
                default: // Om man råkar klicka en annan knapp
                    Clear();
                    WriteLine("Okänt knappval...");
                    WriteLine("Alternativen 1, 2 eller X finns i gästboken.");
                    Write("\nTryck valfri tangent för att gå tillbaka till menyn...");
                    ReadKey(true);
                    break;
            }
        }

        // Laddar menyn för gästbokens framsida
        static void LoadMenu()
        {
            WriteLine("<- F I L I P S  G Ä S T B O K ->\n\n");
            WriteLine("1. Skriv i gästboken");
            WriteLine("2. Ta bort inlägg\n");
            WriteLine("X. Avsluta\n");
        }

        // Skapar ett nytt inlägg i gästboken
        static void CreatePost()
        {
            WriteLine("Nytt inlägg.");
            Write("Ange ägare: ");
            string? author = ReadLine();
            // Validerar så att ägaren av inlägget inte är tom
            if (string.IsNullOrEmpty(author))
            {
                WriteLine("Ägarens namn kan inte vara tomt...");
                Write("\nTryck valfri tangent för att gå tillbaka till menyn...");
                ReadKey(true);
                Clear();
                return;
            }

            // Validerar längden på namnet av ägaren
            if (author.Length < 3 | author.Length > 15)
            {
                WriteLine("\nÄgarens namn behöver vara mellan tre till femton tecken!");
                Write("Tryck valfri tangent för att gå tillbaka till menyn...");
                ReadKey(true);
                return;
            }
            Write("Skriv inlägg: ");
            string? content = ReadLine();

            // Validerar så att inlägget inte är tomt
            if (string.IsNullOrEmpty(content))
            {
                WriteLine("Ett inlägg kan inte vara tomt...");
                Write("\nTryck valfri tangent för att gå tillbaka till menyn...");
                ReadKey(true);
                Clear();
                return;
            }

            // Validerar längden på inlägget
            if (content.Length > 250)
            {
                WriteLine("----------------------------------------");
                WriteLine(
                    $"Inlägget är för långt, max 250 tecken! Inläggets längd nu: {content.Length} tecken..."
                );
                Write("\nTryck valfri tangent för att gå tillbaka till menyn...");
                ReadKey(true);
                return;
            }

            // Hämtar in vad som angetts för skribent och inlägget
            NewPost newPost = new NewPost { Author = author, Content = content };
            //WriteLine($"Skribenten: {newPost.Author} och innehållet: {newPost.Content}");
            // Sparar ned inlägget till listan
            GuestBookPosts.Add(newPost);
            string jsonString = JsonSerializer.Serialize(GuestBookPosts);
            File.WriteAllText(savePostSrc, jsonString);

            // Utskrift efter att man skapat ett inlägg
            WriteLine($"\nEtt nytt inlägg har skapats av: {author}");
            Thread.Sleep(1300);
            WriteLine($"Programmet återgår till menyn...");
            Thread.Sleep(1300);
            Clear();
        }

        // Hämtar sparade inlägg
        static void LoadPosts()
        {
            if (File.Exists(savePostSrc))
            {
                string jsonString = File.ReadAllText(savePostSrc);
                List<NewPost> savedPosts =
                    JsonSerializer.Deserialize<List<NewPost>>(jsonString) ?? new List<NewPost>();
                for (int i = 0; i < savedPosts.Count; i++)
                {
                    WriteLine($"[{i}] {savedPosts[i].Author} - {savedPosts[i].Content}");
                }
            }
        }

        // Tar bort ett sparat inlägg
        static void RemovePost() { }
    }
}
