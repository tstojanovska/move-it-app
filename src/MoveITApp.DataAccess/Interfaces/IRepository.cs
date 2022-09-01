

namespace MoveITApp.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Get all records
        /// </summary>
        Task<List<T>> GetAllAsync();
        /// <summary>
        /// Get a record by id
        /// </summary>
        /// <param name="id">the id of the record we are searching for</param>
        Task<T> GetByIdAsync(int id);
        /// <summary>
        /// Add a new record
        /// </summary>
        /// <param name="entity">The data for the new record</param>
        Task AddAsync(T entity);
        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="entity">The data needed for the update including the id</param>
        Task UpdateAsync(T entity);
        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="entity">The record that needs to be deleted</param>
        Task DeleteAsync(T entity);
    }
}
