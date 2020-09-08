using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Koinonia.Domain.Interface
{
    /// <summary>
    /// Defines a generic repository pattern for all domain entities
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public interface IRepository<TObject> where TObject:class
    {
        /// <summary>
        /// Adds a new instance of an entity to the db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Entity Added</returns>
        TObject AddNew(TObject entity);

        /// <summary>
        /// An Asynchronous method to add a new instance of an object to the db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>new entity added</returns>
        Task<TObject> AddNewAsync(TObject entity);

        /// <summary>
        /// Return a list of all occurance in the db
        /// </summary>
        /// <returns></returns>
        IEnumerable<TObject> GetAll();

        /// <summary>
        /// Fimds a singe entity in the db
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        TObject Get(Guid Id);

        /// <summary>
        /// An Asynchronous method to find a single entity
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<TObject> GetAsync(Guid Id);

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TObject Update(TObject entity);

        /// <summary>
        /// Delete an entity from the db
        /// </summary>
        /// <param name="Id"></param>
        void Delete(Guid Id);

        /// <summary>
        /// Save changes made to db
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();

        /// <summary>
        /// Asynchronous method to save changes made to the db
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChangesAsync();

        TObject GetKoinoniaUser(string userId);
    }
}
