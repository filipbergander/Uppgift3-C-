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
using Posts;
using static System.Console;

namespace Guestbook
{
    class Program
    {
        private static GuestBookPost guestBook = null!;

        static void Main(string[] args)
        {
            // Lagrar instans av JsonPostStorage genom interfacet IPostStorage för att spara som jsondata
            IPostStorage postStorage = new JsonPostStorage("guestbookposts.json");

            // Lagrar instans av GuestBookPost genom poststorage
            guestBook = new GuestBookPost(postStorage);

            // Lagrar instans av ValidatePost genom guestbook
            ValidatePost validatePost = new ValidatePost(guestBook);

            // Håller koll på om programmet ska vara igång eller inte
            bool programOn = true;
            while (programOn)
            {
                Clear(); // Återställer "konsoll-fönstret" och tar bort tidigare text
                CursorVisible = false;
                LoadMenu(); // Visar menyn

                // Läser in vilken tangent som klickades
                ConsoleKeyInfo keyClicked = ReadKey(true);

                // Utför olika metoder i programmet när olika tangenter trycks (1/2/X)
                switch (keyClicked.Key)
                {
                    case ConsoleKey.D1: // Går till menyn för att skapa ett inlägg -> 1
                    case ConsoleKey.NumPad1:
                        Clear();
                        CursorVisible = true;
                        validatePost.ValidateCreatePost();
                        break;
                    case ConsoleKey.D2: // Går till menyn för att ta bort ett inlägg -> 2
                    case ConsoleKey.NumPad2:
                        Clear();
                        CursorVisible = true;
                        validatePost.ValidateDeletePost();
                        break;
                    case ConsoleKey.X: // Stänger ned programmet -> X
                        QuitProgram();
                        programOn = false; // Hoppar ut ur programmet -> stänger av loopen
                        break;
                    default: // Om man råkar trycka på en annan tangent
                        Clear();
                        CursorVisible = true;
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
            Clear();
            WriteLine("<- F I L I P S  G Ä S T B O K ->\n\n");
            WriteLine("1. Skriv i gästboken");
            WriteLine("2. Ta bort inlägg\n");
            WriteLine("X. Avsluta\n");

            guestBook.ShowPosts(); // Hämtar in sparade inlägg när programmet loopas om
        }

        // Stänger ned programmet
        static void QuitProgram()
        {
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
            Environment.Exit(0);
        }
    }
}
