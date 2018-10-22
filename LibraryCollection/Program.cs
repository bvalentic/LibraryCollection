using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraryCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Library Terminal
             * search a library catalog and reserve books
             * 16 book list, sorted in collection
             * allow user to: display list of books (title, author, checked in/out
             *      search for book by author or title keyword
             *      select a book to check out
             *          if checked out, let them know
             *          if not check it out
             *      return a book
             *          same as checking out book
             *      sort list alphabetically by author or title
             *      add to book list
             */
             
            List<string[]> bookList = new List<string[]> {//I picked a list of most-read books in high school 
                new []{"Watership Down", "Adams, Richard", "out"},                
                new []{"Fahrenheit 451", "Bradbury, Ray", "in"},
                new []{"Tale of Two Cities, A", "Dickens, Charles", "in"},
                new []{"Sound and the Fury, The", "Faulker, William", "in"},
                new []{"Great Gatsby, The", "Fitzgerald, F. Scott", "out"},                
                new []{"Lord of the Flies", "Golding, William", "in"},
                new []{"Outsiders, The", "Hinton, S.E.", "out"},
                new []{"To Kill a Mockingbird", "Lee, Harper", "in" },
                new []{"Crucible, The", "Miller, Arthur", "in"},
                new []{"1984", "Orwell, George", "out"},
                new []{"Little Prince, The", "Saint-Exupery, Antoine de", "in"},
                new []{"Catcher in the Rye, The", "Salinger, J.D.", "out"},
                new []{"Frankenstein", "Shelley, Mary", "out"},
                new []{"Of Mice and Men", "Steinbeck, John", "in"}, 
                new []{"Candide", "Voltaire", "in"},
                new []{"Slaughterhouse-Five", "Vonnegut, Kurt", "out"}
            };            
            
            Console.WriteLine("Hello! Welcome to the Dev.Build(2.0) Library!");
            bool goAhead = true;
            while (goAhead)
            {
                goAhead = Chooser(goAhead,bookList);
            }
        }

        static bool Quitter(string message)
        {//method to check if user wants to quit, returns boolean used as check
            bool correctInput = true; //makes sure user puts in a variation of "yes" or "no"
            bool continuer = true; //eventual returned boolean
            do
            {
                Console.Write("\n" + message);
                string confirm = Console.ReadLine().ToLower();
                if (confirm == "y" || confirm == "yes")
                {
                    Console.WriteLine("Come back soon!");
                    continuer = false;
                    correctInput = true;
                    Console.ReadKey();
                }
                else if (confirm == "n" || confirm == "no")
                {
                    Console.WriteLine("\nOkay!\n");
                    continuer = true;
                    correctInput = true;
                }
                else
                {
                    Console.WriteLine("Sorry, I didn't understand.");
                    correctInput = false;
                }
            } while (!correctInput);
            return continuer;
        }

        static void PrintHelp()
        {//prints list of available choices
            Console.WriteLine($@"
{"add", -15} -   adds a book to the library
{"checkout",-15} -   checks out a book of your choice
{"checkout [book]",-15} -   searches for a book and checks it out
{"help",-15} -   displays this list of available commands
{"list",-15} -   displays list of books and their checked in/out status
{"return",-15} -   returns a book of your choice
{"return [book]",-15} -   searches for a book and returns it
{"search",-15} -   searches list of books by title or author
{"sort",-15} -   sorts list of books by title or author
{"quit",-15} -   ends the program");
        }

        static void PrintList(List<string[]> bookList)
        {//prints list of books (sorted by author by default)
            Console.WriteLine($"\n{"Title",-30} {"Author",-30} {"Checked In/Out Status",-30}");
            Console.WriteLine($"{"-----",-30} {"------",-30} {"---------------------",-30}");            
            for (int i = 0; i < bookList.Count; i++)
            {
                Console.WriteLine($"{bookList[i][0],-30} {bookList[i][1],-30} {bookList[i][2],-30}");
            }
        }
        static void Checkout(string bookString, List<string[]> bookList)
        {//"checks out" a book in the list, flips status to "out"
            if (Regex.IsMatch(bookString,@"\w"))
            {
                Console.WriteLine($"Entry detected: {bookString}");
            }
            else
            {
                Console.Write("Please enter the name of the book you wish to check out: ");
                bookString = Console.ReadLine();
                Console.WriteLine($"Searching for {bookString}. . .");
            }
            
            foreach (string[] bookArray in bookList)
            {
                foreach (string book in bookArray)
                {
                    if (book.ToLower().Contains(bookString))
                    {
                        Console.WriteLine($"{bookArray[0]} by {bookArray[1]}.");
                        if (bookArray[2] == "in")
                        {
                            Console.Write("Would you like to check this book out? (y/n) ");
                            string checkInput = Console.ReadLine();
                            if (checkInput == "yes" || checkInput == "y")
                            {
                                bookArray[2] = "out";
                                Console.WriteLine($"You have checked out {bookArray[0]}. Thank you!");
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Okay! Going back to the main menu. . .");
                                return;
                            }
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, this book is already checked out.");
                            return;
                        }                        
                    }                    
                }
            }
        }

        static void Return(string bookString, List<string[]> bookList)
        {//"returns" a book in the list, flips status to "in"
            if (Regex.IsMatch(bookString, @"\w"))
            {
                Console.WriteLine($"Entry detected: {bookString}");
            }
            else
            {
                Console.Write("Please enter the name of the book you wish to return: ");
                bookString = Console.ReadLine();
                Console.WriteLine($"Searching for {bookString}. . .");
            }

            foreach (string[] bookArray in bookList)
            {
                foreach (string book in bookArray)
                {
                    if (book.ToLower().Contains(bookString))
                    {
                        Console.WriteLine($"{bookArray[0]} by {bookArray[1]}.");
                        if (bookArray[2] == "out")
                        {
                            Console.Write("Would you like to return this book? (y/n) ");
                            string checkInput = Console.ReadLine();
                            if (checkInput == "yes" || checkInput == "y")
                            {
                                bookArray[2] = "in";
                                Console.WriteLine($"You have returned {bookArray[0]}. Thank you!");
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Okay! Going back to the main menu. . .");
                                return;
                            }                            
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, this book is already here.");
                            return;
                        }
                    }
                }
            }
        }


        static void Searcher(List<string[]> bookList)
        {//searches list of books based on title or author
            bool continuer = true;
            while (continuer)
            {
                Console.Write("Search by title or author? ");
                string searchInput = Console.ReadLine().ToLower();
                if (searchInput == "title")
                {
                    SearchBy(bookList, "Enter the title or part of title you wish to search: ", 0);
                    continuer = false;
                }
                else if (searchInput == "author")
                {
                    SearchBy(bookList, "Enter the name of the author (first, last or both) you wish to search: ", 1);
                    continuer = false;
                }
                else
                {
                    Console.WriteLine("I'm sorry, I don't understand.");
                }
            }
        }

        static void SearchBy(List<string[]> bookList, string message, int index)
        {//finds and prints books containing search criteria given in Searcher method
            Console.Write(message);
            string searchInput = Console.ReadLine();
            bool isString = true;
            int bookCount = 0;
            isString = CheckBlank(searchInput);
            while (!isString)
            {
                Console.Write(message);
                searchInput = Console.ReadLine();
                isString = CheckBlank(searchInput);
            }
            Console.WriteLine($"\n{"Title",-30} {"Author",-30} {"Checked In/Out Status",-30}");
            foreach (string[] bookArray in bookList)
            {
                foreach (string book in bookArray)
                {
                    if (bookArray[index] == book)//only searches in specific index (title or author)
                    {
                        if (book.ToLower().Contains(searchInput))
                        {
                            Console.WriteLine($"{bookArray[0],-30} {bookArray[1],-30} {bookArray[2],-30}");
                            bookCount++;
                            break;
                        }
                    }
                }
            }
            if (bookCount > 0)
            {
                Console.WriteLine($"\n{bookCount} book(s) containing \"{searchInput}\".");
            }
            else Console.WriteLine($"\nI'm sorry. No results were found with search criteria \"{searchInput}\".");
        }

        static List<string[]> Sorter(List<string[]> bookList)
        {//sorts list alphabetically either by author or title
            bool sortLoop = true;
            do
            {
                Console.WriteLine("Sort by author or title? ");
                string sortInput = Console.ReadLine();
                if (sortInput.ToLower() == "author")
                {//I used LINQ here, because in the numerous searches for ways to 
                 //sort a list of arrays, the only ways I found were using LINQ  
                 //or using IComparer methods which involved classes and seemed overly complicated                   
                    bookList = bookList.OrderBy(book => book[1]).ToList();
                 //this sorts the arrays by the second index in each array, alphabetical by author last name
                    PrintList(bookList);
                    sortLoop = false;
                }
                else if (sortInput.ToLower() == "title")
                {
                    bookList = bookList.OrderBy(book => book[0]).ToList();
                 //this sorts the list by the first index, alphabetical by book title
                    PrintList(bookList);
                    sortLoop = false;
                }
                else Console.WriteLine("I'm sorry, I don't understand.");
            } while (sortLoop);
            return bookList;
        }

        static List<string[]> AddBook(List<string[]> bookList)
        {//adds a book to the list and marks it "in"
            bool isString = true;
            if (isString)
            {
                Console.Write("Please add the name of the book: ");
                string nameInput = Console.ReadLine();
                isString = CheckBlank(nameInput);
                while (!isString)
                {
                    Console.Write("Please add the name of the book: ");
                    nameInput = Console.ReadLine();
                    isString = CheckBlank(nameInput);
                }                
                Console.WriteLine("Please add the name of the author (in \"last name, first name\" format): ");
                string authorInput = Console.ReadLine();
                isString = CheckBlank(authorInput);
                while (!isString)
                {
                    Console.WriteLine("Please add the name of the author (in \"last name, first name\" format): ");
                    authorInput = Console.ReadLine();
                    isString = CheckBlank(authorInput);
                }
                string[] newBook = new[] { nameInput, authorInput, "in" };
                bookList.Add(newBook);
                bookList = bookList.OrderBy(book => book[1]).ToList();
                //sorts by author so added book isn't automatically at bottom of list

                Console.WriteLine($"{nameInput} has been added to the library. Thank you!");                
            }
            return bookList;
        }

        static bool CheckBlank(string input)
        {//verifies if input contains a non-blank string
            bool isString = true;
            if (Regex.IsMatch(input, @"\S+"))
            {
                isString = true;
            }
            else
            {
                Console.WriteLine("Please make an entry.");
                isString = false;
            }

            
            return isString;
        }

        static bool Chooser(bool goAhead, List<string[]> bookList)
        {
           /* "hub" method
            * show list of choices
            * if input = choice go to method for that choice
            * returns bool used in main to continue/quit
            */
            while (goAhead)
            {
                Console.Write("\nWhat would you like to do? \n(Type \"help\" for a list of options) ");
                string input = Console.ReadLine().ToLower();
                if (input == "quit")
                {
                    goAhead = Quitter("Are you sure you want to quit? ");
                }
                else if (input == "help")
                {
                    PrintHelp();
                }
                else if (input == "list")
                {
                    PrintList(bookList);                    
                }
                else if (input == "add")
                {
                    bookList = AddBook(bookList);
                }
                else if (input.Contains("checkout"))
                {//allows user to input "checkout [book]"
                    input = input.Remove(0,8).Trim(' '); //removes "checkout" and any whitespace from beginning of string        
                    Checkout(input,bookList);
                }
                else if (input.Contains("return"))
                {//allows user to input "return [book]"
                    input = input.Remove(0,7).Trim(' '); //removes "return" and any whitespace from beginning of string        
                    Return(input, bookList);
                }
                else if (input == "search")
                {
                    Searcher(bookList);
                }
                else if (input == "sort")
                {
                    bookList = Sorter(bookList);
                }
                else
                {//if the input is something else, it's wrong
                    Console.WriteLine("I'm sorry, I don't understand.");
                }                
            }
            return goAhead;
        }
    }
}
