using System;
using System.Collections.Generic;

namespace LibraryManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>();
            List<Author> authors = new List<Author>();
            List<Borrower> borrowers = new List<Borrower>();
            List<BorrowedBook> borrowedBooks = new List<BorrowedBook>();

            while (true)
            {
                DisplayMenu();

                int choice;
                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        AddBook(books, authors);
                        break;


                    case 2:
                        EditBook(books, authors, "update");
                        break;
                    case 3:
                        EditBook(books, authors, "delete");
                        break;
                    case 4:
                        ListBooks(books);
                        break;
                    case 5:
                        AddAuthor(authors);
                        break;
                    case 6:
                        int authorIndex = SearchAuthorByName(authors);
                        if (authorIndex != -1)
                        {
                            UpdateAuthor(authors, authorIndex);
                        }
                        else
                        {
                            Console.WriteLine("Author not found.");
                        }
                        break;


                    case 7:
                        int authorIndexToDelete = SearchAuthorByName(authors);
                        DeleteAuthor(authors, authorIndexToDelete);
                        break;


                    case 8:
                        ListAuthors(authors);
                        break;
                    case 9:
                        AddBorrower(borrowers);
                        break;
                    case 10:
                        int borrowerIndexToUpdate = SearchBorrowerByName(borrowers);
                        UpdateBorrower(borrowers, borrowerIndexToUpdate);
                        break;

                    case 11:
                        int borrowerIndexToDelete = SearchBorrowerByName(borrowers);
                        DeleteBorrower(borrowers, borrowerIndexToDelete);
                        break;

                    case 12:
                        ListBorrowers(borrowers);
                        break;
                    case 13:
                        RegisterBookToBorrower(books, borrowers, borrowedBooks);
                        break;

                    case 14:
                        DisplayBorrowedBooks(borrowedBooks);
                        break;

                    case 15:
                        SearchBooks(books);
                        break;
                    case 16:
                        FilterBooksByStatus(books);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("Welcome to the Library Management System!\n");
            Console.WriteLine("1. Add a book");
            Console.WriteLine("2. Update a book");
            Console.WriteLine("3. Delete a book");
            Console.WriteLine("4. List all books");
            Console.WriteLine("5. Add an author");
            Console.WriteLine("6. Update an author");
            Console.WriteLine("7. Delete an author");
            Console.WriteLine("8. List all authors");
            Console.WriteLine("9. Add a borrower");
            Console.WriteLine("10. Update a borrower");
            Console.WriteLine("11. Delete a borrower");
            Console.WriteLine("12. List all borrowers");
            Console.WriteLine("13. Register a book to a borrower");
            Console.WriteLine("14. Display borrowed books.");
            Console.WriteLine("15. Search books");
            Console.WriteLine("16. Filter books by status");
            Console.WriteLine("\nEnter the number corresponding to the action you'd like to perform:");
        }

        static void AddBook(List<Book> books, List<Author> authors)
        {
            Console.Write("Enter the title of the book: ");
            string title = Console.ReadLine();

            Console.Write("Enter the index of the author(or -1 to add a new author) ");
            int authorIndex;
            int.TryParse(Console.ReadLine(), out authorIndex);

            Console.Write("Enter the publication year: ");
            int publicationYear;
            int.TryParse(Console.ReadLine(), out publicationYear);

            bool isAvailable = true;
            if (authorIndex >= -1 && authorIndex < authors.Count)
            {
                if (authorIndex == -1)
                {
                    // Add a new author
                    Author newAuthor = AddAuthor(authors);
                    Book newBook = new Book
                    {
                        Title = title,
                        Author = newAuthor,
                        PublicationYear = publicationYear,
                        IsAvailable = isAvailable
                    };

                    books.Add(newBook);
                    Console.WriteLine("Book added successfully!");
                }
                else
                {
                    Author author = authors[authorIndex];
                    Book newBook = new Book
                    {
                        Title = title,
                        Author = author,
                        PublicationYear = publicationYear,
                        IsAvailable = isAvailable
                    };

                    books.Add(newBook);
                    Console.WriteLine("Book added successfully!");
                }
            }
            else
            {
                Console.WriteLine("Invalid author index.");
            }

           
        }

        static void ListBooks(List<Book> books)
        {
            Console.WriteLine("List of all books:\n");
            foreach (var book in books)
            {
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Author: {book.Author.FirstName} {book.Author.LastName}");
                Console.WriteLine($"Publication Year: {book.PublicationYear}");
                Console.WriteLine($"Status: {(book.IsAvailable ? "Available" : "Borrowed")}");
                Console.WriteLine();
            }
        }

        static void UpdateBook(List<Book> books, int bookIndex, List<Author> authors)
        {
            Book book = books[bookIndex];

            Console.Write("Enter the new title of the book: ");
            string newTitle = Console.ReadLine();

            Console.Write("Enter the new index of the author (or -1 to keep the current author): ");
            int authorIndex;
            int.TryParse(Console.ReadLine(), out authorIndex);

            if (authorIndex >= -1 && authorIndex < authors.Count)
            {
                if (authorIndex == -1)
                {
                    // Keep the current author
                    // Update the book properties
                    book.Title = newTitle;
                    Console.WriteLine("Book updated successfully!");
                }
                else
                {
                    // Update the book with the new author
                    Author newAuthor = authors[authorIndex];
                    book.Title = newTitle;
                    book.Author = newAuthor;
                    Console.WriteLine("Book updated successfully!");
                }
            }
            else
            {
                Console.WriteLine("Invalid author index.");
            }
        }


        static void DeleteBook(List<Book> books, int bookIndex)
        {
            books.RemoveAt(bookIndex);
            Console.WriteLine("Book deleted successfully!");
        }


        static Author AddAuthor(List<Author> authors)
        {
            Console.Write("Enter the first name of the author: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter the last name of the author: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter the date of birth (yyyy-MM-dd) of the author: ");
            DateTime dateOfBirth;
            DateTime.TryParse(Console.ReadLine(), out dateOfBirth);

            Author newAuthor = new Author
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };

            authors.Add(newAuthor);
            Console.WriteLine("Author added successfully!");

            return newAuthor;
        }

        static void ListAuthors(List<Author> authors)
        {
            Console.WriteLine("List of all authors:\n");
            foreach (var author in authors)
            {
                Console.WriteLine($"Author: {author.FirstName} {author.LastName}");
                Console.WriteLine($"Date of Birth: {author.DateOfBirth.ToString("yyyy-MM-dd")}");
                Console.WriteLine();
            }
        }

        static void UpdateAuthor(List<Author> authors, int authorIndex)
        {
            // Get the author to update
            Author author = authors[authorIndex];

            Console.Write("Enter the new first name of the author: ");
            string newFirstName = Console.ReadLine();

            Console.Write("Enter the new last name of the author: ");
            string newLastName = Console.ReadLine();

            Console.Write("Enter the new date of birth (yyyy-MM-dd) of the author: ");
            DateTime newDateOfBirth;
            DateTime.TryParse(Console.ReadLine(), out newDateOfBirth);

            // Update the author properties
            author.FirstName = newFirstName;
            author.LastName = newLastName;
            author.DateOfBirth = newDateOfBirth;

            Console.WriteLine("Author updated successfully!");
        }


        static void DeleteAuthor(List<Author> authors, int authorIndex)
        {
            if (authorIndex != -1 && authorIndex < authors.Count)
            {
                Author author = authors[authorIndex];
                authors.RemoveAt(authorIndex);
                Console.WriteLine("Author deleted successfully!");
            }
            else
            {
                Console.WriteLine("Author not found.");
            }
        }


        static void AddBorrower(List<Borrower> borrowers)
        {
            Console.Write("Enter the first name of the borrower: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter the last name of the borrower: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter the email of the borrower: ");
            string email = Console.ReadLine();

            Console.Write("Enter the phone number of the borrower: ");
            string phoneNumber = Console.ReadLine();

            Borrower newBorrower = new Borrower
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };

            borrowers.Add(newBorrower);
            Console.WriteLine("Borrower added successfully!");
        }

        static void ListBorrowers(List<Borrower> borrowers)
        {
            Console.WriteLine("List of all borrowers:\n");
            foreach (var borrower in borrowers)
            {
                Console.WriteLine($"Borrower: {borrower.FirstName} {borrower.LastName}");
                Console.WriteLine($"Email: {borrower.Email}");
                Console.WriteLine($"Phone Number: {borrower.PhoneNumber}");
                Console.WriteLine();
            }
        }

        static void UpdateBorrower(List<Borrower> borrowers, int borrowerIndex)
        {
            if (borrowerIndex != -1 && borrowerIndex < borrowers.Count)
            {
                Borrower borrower = borrowers[borrowerIndex];

                // Update borrower properties
                Console.Write("Enter the new first name of the borrower: ");
                borrower.FirstName = Console.ReadLine();

                Console.Write("Enter the new last name of the borrower: ");
                borrower.LastName = Console.ReadLine();

                Console.Write("Enter the new email of the borrower: ");
                borrower.Email = Console.ReadLine();

                Console.Write("Enter the new phone number of the borrower: ");
                borrower.PhoneNumber = Console.ReadLine();

                Console.WriteLine("Borrower updated successfully!");
            }
            else
            {
                Console.WriteLine("Borrower not found.");
            }
        }


        static void DeleteBorrower(List<Borrower> borrowers, int borrowerIndex)
        {
            if (borrowerIndex != -1 && borrowerIndex < borrowers.Count)
            {
                borrowers.RemoveAt(borrowerIndex);
                Console.WriteLine("Borrower deleted successfully!");
            }
            else
            {
                Console.WriteLine("Borrower not found.");
            }
        }


        static void RegisterBookToBorrower(List<Book> books, List<Borrower> borrowers, List<BorrowedBook> borrowedBooks)
        {
            int bookIndex = SearchBookByTitle(books);
            int borrowerIndex = SearchBorrowerByName(borrowers);

            if (bookIndex != -1 && borrowerIndex != -1)
            {
                Book book = books[bookIndex];
                Borrower borrower = borrowers[borrowerIndex];

                Console.Write("Enter the due date (yyyy-MM-dd) for the book: ");
                DateTime dueDate;
                DateTime.TryParse(Console.ReadLine(), out dueDate);

                BorrowedBook borrowedBook = new BorrowedBook
                {
                    Book = book,
                    Borrower = borrower,
                    BorrowDate = DateTime.Now,
                    DueDate = dueDate
                };

                borrowedBooks.Add(borrowedBook);
                book.IsAvailable = false;

                Console.WriteLine("Book registered to borrower successfully!");
            }
            else
            {
                Console.WriteLine("Book or borrower not found.");
            }
        }


       
        static void SearchBooks(List<Book> books)
        {
            Console.Write("Enter a keyword to search for: ");
            string keyword = Console.ReadLine();

            Console.WriteLine($"Search results for '{keyword}':\n");
            foreach (var book in books)
            {
                if (book.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    book.Author.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    book.Author.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Title: {book.Title}");
                    Console.WriteLine($"Author: {book.Author.FirstName} {book.Author.LastName}");
                    Console.WriteLine($"Publication Year: {book.PublicationYear}");
                    Console.WriteLine($"Status: {(book.IsAvailable ? "Available" : "Borrowed")}");
                    Console.WriteLine();
                }
            }
        }

        static void FilterBooksByStatus(List<Book> books)
        {
            Console.WriteLine("Filter books by status:");
            Console.WriteLine("1. Available");
            Console.WriteLine("2. Borrowed");
            Console.Write("Enter the number corresponding to the status: ");

            int statusChoice;
            int.TryParse(Console.ReadLine(), out statusChoice);

            bool isAvailable = statusChoice == 1;

            Console.WriteLine($"Books {(isAvailable ? "Available" : "Borrowed")}:\n");

            foreach (var book in books)
            {
                if (book.IsAvailable == isAvailable)
                {
                    Console.WriteLine($"Title: {book.Title}");
                    Console.WriteLine($"Author: {book.Author.FirstName} {book.Author.LastName}");
                    Console.WriteLine($"Publication Year: {book.PublicationYear}");
                    Console.WriteLine();
                }
            }
        }


        static int SearchBookByTitle(List<Book> books)
        {
            Console.Write("Enter the title of the book to search for: ");
            string searchTitle = Console.ReadLine();

            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Title.Equals(searchTitle, StringComparison.OrdinalIgnoreCase))
                {
                    return i; // Return the index of the found book
                }
            }

            return -1; // Book not found
        }


        static void EditBook(List<Book> books, List<Author> authors, string action)
        {
            if (action != "update" && action != "delete")
            {
                Console.WriteLine("Invalid action.");
                return;
            }

            int bookIndex = SearchBookByTitle(books);
            if (bookIndex != -1)
            {
                if (action == "update")
                {
                    UpdateBook(books, bookIndex, authors);
                }
                else if (action == "delete")
                {
                    DeleteBook(books, bookIndex);
                }
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

       


        static int SearchAuthorByName(List<Author> authors)
        {
            Console.Write("Enter the first name of the author to search for: ");
            string searchFirstName = Console.ReadLine();

            Console.Write("Enter the last name of the author to search for: ");
            string searchLastName = Console.ReadLine();

            for (int i = 0; i < authors.Count; i++)
            {
                if (authors[i].FirstName.Equals(searchFirstName, StringComparison.OrdinalIgnoreCase) &&
                    authors[i].LastName.Equals(searchLastName, StringComparison.OrdinalIgnoreCase))
                {
                    return i; // Return the index of the found author
                }
            }

            return -1; // Author not found
        }


        static int SearchBorrowerByName(List<Borrower> borrowers)
        {
            Console.Write("Enter the first name of the borrower to search for: ");
            string searchFirstName = Console.ReadLine();

            Console.Write("Enter the last name of the borrower to search for: ");
            string searchLastName = Console.ReadLine();

            for (int i = 0; i < borrowers.Count; i++)
            {
                if (borrowers[i].FirstName.Equals(searchFirstName, StringComparison.OrdinalIgnoreCase) &&
                    borrowers[i].LastName.Equals(searchLastName, StringComparison.OrdinalIgnoreCase))
                {
                    return i; // Return the index of the found borrower
                }
            }

            return -1; // Borrower not found
        }

        static void DisplayBorrowedBooks(List<BorrowedBook> borrowedBooks)
        {
            Console.WriteLine("List of Borrowed Books:\n");

            foreach (BorrowedBook borrowedBook in borrowedBooks)
            {
                Console.WriteLine($"Title: {borrowedBook.Book.Title}");
                Console.WriteLine($"Author: {borrowedBook.Book.Author.FirstName} {borrowedBook.Book.Author.LastName}");
                Console.WriteLine($"Borrower: {borrowedBook.Borrower.FirstName} {borrowedBook.Borrower.LastName}");
                Console.WriteLine($"Borrow Date: {borrowedBook.BorrowDate.ToString("yyyy-MM-dd")}");
                Console.WriteLine($"Due Date: {borrowedBook.DueDate.ToString("yyyy-MM-dd")}");
                Console.WriteLine();
            }
        }





    }

    class Book
    {
        public string Title { get; set; }
        public Author Author { get; set; }
        public int PublicationYear { get; set; }
        public bool IsAvailable { get; set; }
    }

    class Author
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    class Borrower
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    class BorrowedBook
    {
        public Book Book { get; set; }
        public Borrower Borrower { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
