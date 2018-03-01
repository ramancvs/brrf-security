using BRRF.IdentityServer.Repository.DbContexts;
using BRRF.IdentityServer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BRRF.IdentityServer.Repository
{
    public class UserRepository 
    {
        private ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //#region Create New Template Parameters
        /// <summary>
        /// Create New Template Parameter
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Create(User entity)
        {
            int res;
            try
            {
                _dbContext.User.Add(
                    new User
                    {
                        Name = entity.Name,
                        Description = entity.Description,
                        UserName = entity.UserName,
                        Password = entity.Password,
                        CreatedOn = DateTime.Now

                    });
                res = _dbContext.SaveChanges();
                return res == 1 ? true : false;
            }

            catch (Exception)
            {

                return false;
            }
        }
        //#endregion

        //#region Delete Template Parameters
        /// <summary>
        /// Delete the record from Template Parameter Table based On ID
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(User entity)
        {
            var itemToRemove = _dbContext.User.SingleOrDefaultAsync(x => x.Id == entity.Id).Result; //returns a single item.
            int res = 0;
            if (itemToRemove != null)
            {
                _dbContext.User.Remove(itemToRemove);
                res = _dbContext.SaveChanges();
            }
            return res == 1 ? true : false;
        }
        //#endregion

        //#region Get All Template Parameters
        /// <summary>
        /// Get all Records from Template Parameter Table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAll()
        {
            var user = _dbContext.User
                            .Include(userrole=> userrole.UserRole)
                            .ThenInclude(a => a.Role).ToList();
            return user;
        }

        public IEnumerable<User> GetAll(Expression<Func<User, bool>> whereExpression)
        {
            return _dbContext.User
                            .Include(userbrand => userbrand.UserBrand)
                            .ThenInclude(brand => brand.Brand)
                            .Include(userrole => userrole.UserRole)
                            .ThenInclude(a => a.Role).Where(whereExpression);
        }

        //#endregion

        //#region Update Template Parameters Info
        /// <summary>
        /// Update Existing record in Template Parameter Table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(User entity)
        {
            int res = 0;
            var result = _dbContext.User.SingleOrDefaultAsync(b => b.Id == entity.Id).Result;
            if (result != null)
            {
                result.Name = entity.Name;
                result.Description = entity.Description;
                result.UserName = entity.UserName;
                result.Password = entity.Password;
                result.ModifiedOn = DateTime.Now;
                res = _dbContext.SaveChanges();

            }
            return res == 1 ? true : false;
        }
        //#endregion
    }
}
