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
    public interface IAuthorService
    {
        IQueryable<AuthorModel> Query();
        Result Add(AuthorModel model);
        Result Update(AuthorModel model);
        Result Delete(int id);
        List<AuthorModel> GetList() => Query().ToList();
        AuthorModel GetItem(int id) => Query().SingleOrDefault(g => g.Id == id);
    }
    public class AuthorService : ServiceBase, IAuthorService
    {
        public AuthorService(Db db) : base(db)
        {
        }

        public Result Add(AuthorModel model)
        {
            if (_db.Authors.Any(p => p.Name.ToLower() == model.Name.ToLower().Trim()) && _db.Authors.Any(p => p.Surname.ToLower() == model.Surname.ToLower().Trim()))
                return new ErrorResult("Author with the same name exists!");
            Author entity = new Author()
            {
                
                Name = model.Name.Trim(),
                Surname = model.Surname.Trim(),
            };

            
            _db.Add(entity);

            _db.SaveChanges();
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            Author entity = _db.Authors.Include(r => r.Books).SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return new ErrorResult("Author not found!");
            if (entity.Books is not null && entity.Books.Any())
                return new ErrorResult("Author can't be deleted because it has relational games!");

           
            _db.Remove(entity);

            _db.SaveChanges();
            return new SuccessResult();
        }

        public IQueryable<AuthorModel> Query()
        {
            return _db.Authors.Include(b => b.Books).Select(a => new AuthorModel
            {
                Guid = a.Guid,
                Id = a.Id,
                Name = a.Name,
                Surname = a.Surname,
                
                Books = string.Join("<br />", a.Books.Select(b => b.Name))

            });
        }

        public Result Update(AuthorModel model)
        {
            if (_db.Authors.Any(p => p.Id != model.Id && p.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Author with the same name exists!");
            Author entity = _db.Authors.Find(model.Id);
            if (entity is null)
                return new ErrorResult("Author not found!");
            entity.Name = model.Name.Trim();

            // Way 1:
            //_db.Publishers.Update(entity);
            // Way 2:
            _db.Update(entity);

            _db.SaveChanges();
            return new SuccessResult();
        }
    }
}
