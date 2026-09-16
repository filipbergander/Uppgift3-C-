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
            LoadPosts(); // Hämtar in sparade inlägg

            // Håller koll på om programmet ska vara igång eller inte
            bool programOn = true;
            while (programOn)
            {
                Clear(); // Återställer fönstret och tar bort tidigare text
                LoadMenu(); // Visar menyn
                LoadPosts(); // Hämtar in sparade inlägg

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
                        programOn = false; // Hoppar ut ur programmet -> stänger av loopen
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
            // Metoden gör inget om ingen json-fil finns nedsparad än
            if (!File.Exists(savePostSrc))
            {
                return;
            }
            // Läser in texten inom json-filen
            string jsonString = File.ReadAllText(savePostSrc);

            // Provar deserialiserar texten i json-filen till en newpost-lista, annars en tom lista
            GuestBookPosts = JsonSerializer.Deserialize<List<NewPost>>(jsonString) ?? [];

            // Skriver ut varje inlägg
            for (int i = 0; i < GuestBookPosts.Count; i++)
            {
                WriteLine($"[{i}] {GuestBookPosts[i].Author} - {GuestBookPosts[i].Content}");
            }
        }

        // Tar bort ett sparat inlägg
        static void RemovePost()
        {
            // Om inga sparade inlägg finns
            if (GuestBookPosts.Count == 0)
            {
                WriteLine("Inga inlägg finns lagrade!");
                Write("\nTryck valfri tangent för att gå tillbaka till menyn...");
                ReadKey(true);
                return;
            }
            // Annars, går vidare och skriver ut inläggen
            Write("Vilket inlägg vill du radera?\n");
            for (int i = 0; i < GuestBookPosts.Count; i++)
            {
                WriteLine($"[{i}] {GuestBookPosts[i].Author} - {GuestBookPosts[i].Content}");
            }
            Write("\nAnge siffra för inlägget som du vill radera och tryck enter: ");
            string? indexInput = ReadLine();
            // Validerar input och tar bort ett inlägg om input är korrekt
            if (string.IsNullOrEmpty(indexInput))
            {
                WriteLine("Ingen siffra angavs..");
            }
            if (
                int.TryParse(indexInput, out int index)
                && index >= 0
                && index < GuestBookPosts.Count
            )
            {
                // Sparar ned den nya listan med inlägg, efter radering
                GuestBookPosts.RemoveAt(index);
                string jsonString = JsonSerializer.Serialize(GuestBookPosts);
                File.WriteAllText(savePostSrc, jsonString);
                WriteLine($"Inlägget raderades!");
                Thread.Sleep(1000);
                Clear();
            }
            // Felhantering
            else
            {
                WriteLine("Ogilitgt knappval...");
                WriteLine($"Du behöver ange en siffra mellan 0 och {GuestBookPosts.Count - 1}.");
                Write("\nTryck valfri tangent för att gå tillbaka till menyn...");
                ReadKey(true);
                Clear();
            }
        }
    }
}
