using System;
using System.Collections.Generic;

namespace Библиотека
{

    [Serializable]
    public class Books
    {
        public Books() { }
        public List<Book> BookList { get; set; } = new List<Book>();
    }

    [Serializable]
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Year_of_release { get; set; }

        public Book() { }
        public Book(string name, string author, string year_of_release)
        {
            this.Name = name;
            this.Author = author;
            this.Year_of_release = year_of_release;
        }
    }
}
