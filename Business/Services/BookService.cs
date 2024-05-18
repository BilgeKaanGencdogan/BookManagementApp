using Business.Model;
using Business.Services.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IBookService
    {
        IQueryable<BookModel> Query();
        Result Add(BookModel model);
        Result Update(BookModel model);
        Result Delete(int id);
        List<BookModel> GetList() => Query().ToList();
        BookModel GetItem(int id) => Query().SingleOrDefault(g => g.Id == id);
    }
    public class BookService : ServiceBase, IBookService
    {
        public BookService(Db db) : base(db)
        {
        }

        public Result Add(BookModel model)
        {
            if (_db.Books.Any(g => g.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Book with the same name exists!");
            Book entity = new Book()
            {

                Name = model.Name.Trim(),
                PublishDate = model.PublishDate,
                Price = model.Price,
                AuthorId = model.AuthorId,
                BookType = model.BookType,
                IsTopSeller = model.IsTopSeller,
                Isbn = model.Isbn,
                NumberOfPages = model.NumberOfPages,


				BookGenres = model.GenreInput?.Select(genreId => new BookGenre { GenreId = genreId }).ToList()


			};


            _db.Books.Add(entity);
            _db.SaveChanges();

            model.Id = entity.Id;

            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            Book entity = _db.Books.Include(g => g.BookGenres).SingleOrDefault(g => g.Id == id);
            if (entity is null)
                return new ErrorResult("Book not found!");

            _db.BookGenres.RemoveRange(entity.BookGenres);

            _db.Books.Remove(entity);
            _db.SaveChanges();

            return new SuccessResult("Book deleted successfully.");
        }

        public IQueryable<BookModel> Query()
        {
            return _db.Books.Include(b => b.Author).Include(b => b.BookGenres).ThenInclude(ub => ub.Genre)
        .OrderByDescending(g => g.PublishDate).ThenByDescending(b => b.NumberOfPages).ThenBy(b => b.Name)
        .Select(b => new BookModel()
        {
            // entity properties
            Guid = b.Guid,
            Id = b.Id,
            Name = b.Name,
            PublishDate = b.PublishDate,
            AuthorId = b.AuthorId,
            Isbn = b.Isbn,
            IsTopSeller = b.IsTopSeller,
            NumberOfPages = b.NumberOfPages,
            BookType = b.BookType,
            Price = b.Price,
            // extra properties
            PriceOutput = (b.Price ?? 0).ToString("C2"),
            PublishDateOutput = b.PublishDate.HasValue ? b.PublishDate.Value.ToString("MM/dd/yyyy") : string.Empty,
            AuthorOutput = b.Author.Name,

            GenreModels = b.BookGenres.Select(ug => new GenreModel()
            {
               Name = ug.Genre.Name,
            }).ToList(),

            GenreInput = b.BookGenres.Select(ug => ug.Id).ToList()

        });

        }

        public Result Update(BookModel model)
        {
            if (_db.Books.Any(g => g.Id != model.Id && g.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Book with the same name exists!");

            Book entity = _db.Books.Include(g => g.BookGenres).SingleOrDefault(g => g.Id == model.Id);
            if (entity is null)
                return new ErrorResult("Book not found!");

            _db.BookGenres.RemoveRange(entity.BookGenres);

            entity.Name = model.Name.Trim();
            entity.PublishDate = model.PublishDate;
            entity.Price = model.Price;
            entity.AuthorId = model.AuthorId;
            entity.BookType = model.BookType;
            entity.IsTopSeller = model.IsTopSeller;
            entity.Isbn = model.Isbn;
            entity.NumberOfPages = model.NumberOfPages;

			entity.BookGenres = model.GenreInput?.Select(genreId => new BookGenre { GenreId = genreId }).ToList();

			_db.Books.Update(entity);
            _db.SaveChanges();

            return new SuccessResult();
        }
    }
}
