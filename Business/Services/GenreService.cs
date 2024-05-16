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
    public interface IGenreService
    {
        IQueryable<GenreModel> Query();
        Result Add(GenreModel model);
        Result Update(GenreModel model);
        Result Delete(int id);
        List<GenreModel> GetList() => Query().ToList();
        GenreModel GetItem(int id) => Query().SingleOrDefault(g => g.Id == id);
    }
    public class GenreService : ServiceBase, IGenreService
    {
        public GenreService(Db db) : base(db)
        {
        }

        public Result Add(GenreModel model)
        {
			if (_db.Genres.Any(p => p.Name.ToLower() == model.Name.ToLower().Trim()))
				return new ErrorResult("Genre with the same name exists!");
			Genre entity = new Genre()
			{
			    
				Name = model.Name.Trim(),

                 BookGenres = model.GenreInput?.Select(genreInput => new BookGenre()
                 {
                     GenreId = genreInput
                 }).ToList()
            };

			
			_db.Add(entity);

			_db.SaveChanges();
			return new SuccessResult();

		}

        public Result Delete(int id)
        {
			Genre entity = _db.Genres.Include(r => r.BookGenres).SingleOrDefault(g => g.Id == id);
			if (entity is null)
				return new ErrorResult("Genre not found!");
			if (entity.BookGenres is not null && entity.BookGenres.Any())
				return new ErrorResult("Genre can't be deleted because it has relational BookGenres!");


            _db.BookGenres.RemoveRange(entity.BookGenres);
            _db.Remove(entity);
            

            _db.SaveChanges();
			return new SuccessResult();
		}

        public IQueryable<GenreModel> Query()
        {
			return _db.Genres.Include(g =>g.BookGenres).Select(p => new GenreModel()
			{
				// entity properties
				Id = p.Id,
				Name = p.Name,

                // extra properties
                GenreModels = p.BookGenres.Select(ug => new GenreModel()
                {
                    Name = ug.Genre.Name,
                }).ToList(),

                GenreInput = p.BookGenres.Select(ug => ug.Id).ToList()
            });
		}


        public Result Update(GenreModel model)
        {
			if (_db.Genres.Any(p => p.Id != model.Id && p.Name.ToLower() == model.Name.ToLower().Trim()))
				return new ErrorResult("Genre with the same name exists!");
			Genre entity = _db.Genres.Find(model.Id);
			if (entity is null)
				return new ErrorResult("Genre not found!");
			entity.Name = model.Name.Trim();
            entity.BookGenres = model.GenreInput?.Select(userInput => new BookGenre()
            {
                GenreId = userInput
            }).ToList();

      
            _db.Update(entity);

			_db.SaveChanges();
			return new SuccessResult();
		}
    }
}
