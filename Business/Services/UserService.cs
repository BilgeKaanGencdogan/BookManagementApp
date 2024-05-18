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
	public interface IUserService
	{
		IQueryable<UserModel> Query();
		Result Add(UserModel model);
		Result Update(UserModel model);
		Result Delete(int id);
		List<UserModel> GetList() => Query().ToList();
		UserModel GetItem(int id) => Query().SingleOrDefault(q => q.Id == id);
	}
	public class UserService : ServiceBase, IUserService
	{
		public UserService(Db db) : base(db)
		{
		}

		public Result Add(UserModel model)
		{
			
			if (_db.Users.Any(u => u.UserName.ToUpper() == model.UserName.ToUpper().Trim()))
				return new ErrorResult("Active user with the same user name exists!");

			User entity = new User()
			{
				
				Password = model.Password.Trim(),

				// Way 1: assign 0 if model's RoleId is null
				//RoleId = model.RoleId ?? 0,
				// Way 2: since model's RoleId is required (can't be null), assign its value
				RoleId = model.RoleId.Value,

				UserName = model.UserName.Trim()

				// TODO: Games
			};

			_db.Users.Add(entity);
			_db.SaveChanges();

			model.Id = entity.Id;

			return new SuccessResult("User added successfully.");
		}

		public Result Delete(int id)
		{
			User entity = _db.Users.SingleOrDefault(u => u.Id == id);
			if (entity is null)
				return new ErrorResult("User not found!");

			_db.Users.Remove(entity);
			_db.SaveChanges();

			return new SuccessResult("User deleted successfully.");
		}

		public IQueryable<UserModel> Query()
		{
			return _db.Users.Include(u => u.Role)
				.OrderByDescending(u => u.RoleId).ThenBy(u => u.UserName)
				.Select(u => new UserModel()
				{
					// entity properties
					Guid = u.Guid,
					Id = u.Id,
					Password = u.Password,
					RoleId = u.RoleId,
					UserName = u.UserName,

					// extra properties
					RoleOutput = new RoleModel()
					{
						Guid = u.Role.Guid,
						Id = u.Role.Id,
						Name = u.Role.Name
					}
				});
		}

		public Result Update(UserModel model)
		{
			if (_db.Users.Any(u => u.Id != model.Id && u.UserName.ToUpper() == model.UserName.ToUpper().Trim()))
				return new ErrorResult("User with the same user name exists!");

			User entity = _db.Users.SingleOrDefault(u => u.Id == model.Id);
			if (entity is null)
				return new ErrorResult("User not found!");

			entity.Password = model.Password.Trim();
			entity.RoleId = model.RoleId.Value;
			entity.UserName = model.UserName.Trim();

			

			_db.Users.Update(entity);
			_db.SaveChanges();

			return new SuccessResult("User updated successfully.");
		}
	}
}
