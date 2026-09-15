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

using Post;
using static System.Console;

namespace Guestbook
{
    class Program
    {
        // Hämtar in hur ett inlägg ska se ut och sparar ned till en lista
        static List<NewPost> GuestBookPosts = new List<NewPost>();

        static void Main(string[] args)
        {
            // Visar menyn
            LoadMenu();
        }

        // Laddar menyn för gästbokens framsida
        static void LoadMenu()
        {
            WriteLine("<- F I L I P S  G Ä S T B O K ->\n\n");
            WriteLine("1. Skriv i gästboken");
            WriteLine("2. Ta bort inlägg\n");
            WriteLine("X. Avsluta\n");
        }
    }
}
