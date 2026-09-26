using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Posts;
using static System.Console;

namespace Posts
{
    public class ValidatePost
    {
        private readonly GuestBookPost guestBook;

        public ValidatePost(GuestBookPost guestBook)
        {
            this.guestBook = guestBook;
        }

        // Skapar ett nytt inlägg, efter validering
        public void ValidateCreatePost()
        {
            //var posts = guestBook.posts; // Hämtar in listan av inlägg
            // Skriver ut textmeddelande när man ska till att skapa nytt inlägg
            void printHeader()
            {
                WriteLine("Nytt inlägg i gästboken");
                WriteLine("Skriv ESC för att avbryta\n");
            }
            printHeader();
            string? author;
            while (true) // Validerar ägare av inlägget
            {
                Write("Ange ägare: ");
                author = (ReadLine() ?? "").Trim();
                // Återgår till menyn om man skriver esc
                if (author.ToLower() == "esc")
                {
                    Clear();
                    ForegroundColor = ConsoleColor.DarkYellow;
                    WriteLine("\n----- Avbröt, återgår till menyn -----");
                    ResetColor();
                    WriteLine();
                    Thread.Sleep(1000);
                    Clear();
                    return;
                }

                // Validerar så att ägaren av inlägget inte är tom
                if (string.IsNullOrEmpty(author))
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    WriteLine("Ägarens namn kan inte lämnas tomt...");
                    ResetColor();
                    WriteLine();
                    Thread.Sleep(1500);
                    continue;
                }
                // Validerar längden på namnet av ägaren
                if (author.Length < 3 | author.Length > 15)
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    WriteLine("Ägarens namn behöver vara mellan tre till femton tecken!");
                    ResetColor();
                    WriteLine();
                    Thread.Sleep(1500);
                    continue;
                }
                break;
            }
            string? content;
            while (true) // Validerar texten till inlägget
            {
                Write("Skriv inlägg: ");
                content = ReadLine();
                // Återgår till menyn om man skriver esc
                if (content == "esc")
                {
                    Clear();
                    ForegroundColor = ConsoleColor.DarkYellow;
                    WriteLine("\n----- Avbröt, återgår till menyn -----");
                    ResetColor();
                    WriteLine();
                    Thread.Sleep(1000);
                    Clear();
                    return;
                }

                // Validerar så att inlägget inte är tomt
                if (string.IsNullOrEmpty(content))
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    WriteLine("Ett inlägg kan inte vara tomt...");
                    ResetColor();
                    WriteLine();
                    Thread.Sleep(1500);
                    continue;
                }

                // Validerar längden på inlägget
                if (content.Length > 250)
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    WriteLine(
                        $"Inlägget är för långt, max 250 tecken! Inläggets längd nu: {content.Length} tecken..."
                    );
                    ResetColor();
                    WriteLine();
                    Thread.Sleep(1500);
                    continue;
                }
                break;
            }
            // Sparar det nya inlägget och lägger till i listan och json-filen
            Post post = new Post { Author = author, Content = content };
            guestBook.AddPost(post);
            ForegroundColor = ConsoleColor.DarkGreen;
            WriteLine("Nytt inlägg skapades!");
            ResetColor();
            WriteLine();
            Thread.Sleep(1000);
        }

        // Tar bort ett inlägg från programmet och ur "databasen" -> json-filen

        public void ValidateDeletePost()
        {
            // Hämtar in alla inlägg
            var posts = guestBook.Posts;
            Clear();
            // Om inga sparade inlägg finns
            if (posts.Count == 0)
            {
                WriteLine("Inga inlägg finns lagrade!");
                Write("\nTryck valfri tangent för att gå tillbaka till menyn...");
                ReadKey(true);
                return;
            }
            // Skriver ut text i konsollen när man klickat för att radera ett inlägg
            void printHeader()
            {
                WriteLine("Radera inlägg från gästboken");
                WriteLine("Skriv ESC för att avbryta\n");
                Write("Vilket inlägg vill du radera?\n");
                guestBook.ShowPosts();
            }
            printHeader();
            // Validerar input, samt stänger av programmet när man skriver esc
            while (true)
            {
                Write("\nAnge siffra för inlägget och tryck enter: ");
                string? indexInput = ReadLine()?.Trim().ToLower();

                if (indexInput == "esc")
                {
                    Clear();
                    ForegroundColor = ConsoleColor.DarkYellow;
                    WriteLine("\n----- Avbröt, återgår till menyn -----");
                    ResetColor();
                    WriteLine();
                    Thread.Sleep(1000);
                    Clear();
                    return;
                }
                // Validerar tom input
                if (string.IsNullOrEmpty(indexInput))
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    WriteLine("Ingen siffra skickades med...");
                    ResetColor();
                    WriteLine();
                    Thread.Sleep(1500);
                    Clear();
                    printHeader();
                    continue;
                }
                // Om en korrekt siffra angetts
                if (!int.TryParse(indexInput, out int index) || index < 0 || index >= posts.Count)
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    WriteLine("Ogilitgt knappval...");
                    WriteLine(
                        posts.Count == 1
                            ? "Du behöver ange siffran 0 för att ta bort inlägget."
                            : $"Du behöver ange en siffra mellan 0 och {posts.Count - 1}."
                    );
                    ResetColor();
                    WriteLine();
                    Thread.Sleep(1500);
                    Clear();
                    printHeader();
                    continue;
                }
                // Vid lyckad radering -> Byter färg på konsollens text till mörkgrön
                ForegroundColor = ConsoleColor.DarkGreen;
                guestBook.DeletePostData(index); // Skickar med siffran som angetts
                WriteLine($"Inlägget raderades!");
                ResetColor();
                WriteLine();
                Thread.Sleep(1000); // Hoppar tillbala till menyn efter 1 sek
                Clear();
                return;
            }
        }
    }
}
