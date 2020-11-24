using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore

{
    interface IEvent
    {
        List<IBook> ActivateEvent(List<IBook> books);
    }

    class FreeEBook : IEvent
    {
        public List<IBook> ActivateEvent(List<IBook> books)
        {
            int numberPaperBook = 2;
            var authorsBook = new Dictionary<string, int>();
            var eBookAuthor = new Dictionary<string, List<IBook>>();

            var paperBook = books.Where(x => x.Type == BookType.paperBook);
            var ebooks = books.Where(x => x.Type == BookType.eBook);
            var result = new List<IBook>();

            foreach (var paper in paperBook)
            {
                if (!authorsBook.ContainsKey(paper.Author))
                    authorsBook[paper.Author] = 1;
                else
                    authorsBook[paper.Author] += 1;
            }

            foreach (var ebook in ebooks)
            {
                if (authorsBook.ContainsKey(ebook.Author) && (authorsBook[ebook.Author] > numberPaperBook))
                    if (!eBookAuthor.ContainsKey(ebook.Author))
                    {
                        eBookAuthor[ebook.Author] = new List<IBook>
                        {
                            ebook
                        };
                    }
                    else
                        eBookAuthor[ebook.Author].Add(ebook);
            }

            foreach (var author in eBookAuthor.Keys)
                result.Add(eBookAuthor[author].OrderBy(a => a.Price).First());

            return result;
        }
    }

    class FreeEBookWithAudiobook : IEvent
    {
        public List<IBook> ActivateEvent(List<IBook> books)
        {
            List<IBook> result = new List<IBook>();
            var audioBook = books.Where(x => x.Type == BookType.audioBook);
            var ebooks = books.Where(x => x.Type == BookType.eBook);
            foreach (var audio in audioBook)
            {
                var coincidence = ebooks.Where(x => x.Title == audio.Title && x.Author == audio.Author);
                foreach (var c in coincidence)
                    result.Add(c);
            }
            return result;
        }
    }
}
