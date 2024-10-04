# Devblog & Portfolio Project (Razor version)
### By Mastervoliumpl

## Description:
This project creates the backend for a devblog and portfolio website. SOLID was taken into consideration while creating the project.

## Main features:
- Create, display, update, and delete Blogposts and Portfoilo posts.

## Known issues:
- Data does not save after application is closed.

## Technologies used:
- C# 12 
- .NET 8
- Razor Pages
- xUnit Test
- Moq
  
## Changelogs:
- 1.0.1
Readded unit tests from last repo

- 1.0.0
The basis functionality works, finishing polishes and tweaks. 

- 0.9.0
Styling was added for all of the elements, changed to dark mode 

- 0.8.0
Added the functionality to the pages. Utilizing mostly exisiting features.

- 0.7.0
Added new pages, finished Index and made mock posts

- 0.6.0
Razor Pages project initialized

- 0.5.0 (Before Razor)
Changelog was not activley utilized throughout the project. But most recent version added unit tests for the project, as well as small improvements to the main code.

## TO DO:
- Move Null checks to Repository for better error/exception handling/encapsulation.
	- Improve error handling.
- Implement Image handling for Blogposts and Portfolio posts.
- Implement comments for Blogposts.
	- Implement Users (authentication and authorization.)
- Logging
- 
- Implement Razor pages for frontend.
  1. Landing page
    - Hvor man kan login på en login page (punkt 6.)
    - Hvor man kan gå til BlogPost
    - Hvor man kan gå til Portfolio

  2. Page to display all posts
  3. Page to display Portfolio items
  4. Add CRUD
    - Page to create posts
    - Page to edit posts
    - Page to delete posts
    - "Read" of CRUD is specified above in 2 & 3.

  5. Search through Blogposts
  If I have time enough:
  6. User accounts
  7. Special Author permissions/login
  8. Only author will be able to create posts
  9. Comments for Blogposts 
- Implement SQL Database for data storage.

## License:
MIT License

## Contact:
- Discord: Mastervoliumpl
