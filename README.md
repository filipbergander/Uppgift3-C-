# Uppgift 3 - Programmering i C#.NET

Jag skapar en konsollapp där användare kan skriva, radera och se inlägg i en gästbok.   
För att starta applikationen anges kommandoraden: dotnet run   

#### För att skriva ett inlägg krävs att man anger:
**Author: Ägare**   
**Content: Innehåll**

I applikationen navigerar man med tangentbordet -> tangenterna 1, 2 och X

<img width="592" height="297" alt="Skärmbild Filips gästbok" src="https://github.com/user-attachments/assets/874bbca5-6836-4f91-8d38-62a342aee81c" />   

## Projektstruktur
**Program.cs**: Programmet startar upp här i metoden Main och utgår från denna fil.   
**ValidatePost.cs**: All validering för att skapa och radera ett inlägg.    
**Post.cs**: Definierar ett schema för ett inlägg med Author och Content via get, set.    
**GuestbookPost.cs**: Metoder för att spara inlägg, visa inlägg, radera och hämta inlägg från "databasen", GuestbookPosts.json.   
**GuestbookPosts.json**: Inlägg sparas ned i denna fil.

*Filip Bergander HT2026, Mittuniversitetet*
