using Lightning.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lightning.EntityFramework.Repository
{
    public interface IRepository
    {

    }
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : Entity<TPrimaryKey>
    {
        /// <summary>
        /// 获取此存储库中所有实体的计数
        /// </summary>
        /// <returns>实体数量</returns>
        int Count();
        /// <summary>
        /// 根据给定的条件获取此存储库中所有实体的计数
        /// </summary>
        /// <param name="predicate">过滤计数的方法</param>
        /// <returns>实体数量</returns>
        int Count(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据给定的条件获取此存储库中所有实体的计数
        /// </summary>
        /// <param name="predicate">过滤计数的方法</param>
        /// <returns>实体数量</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 获取此存储库中所有实体的计数
        /// </summary>
        /// <returns>实体数量</returns>
        Task<int> CountAsync();
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        void Delete(TEntity entity);
        /// <summary>
        /// 按主键删除实体
        /// </summary>
        /// <param name="id">实体的主键</param>
        void Delete(TPrimaryKey id);

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        void Delete(Expression<Func<TEntity, bool>> Expression);

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        /// <param name="autoSave">是否自动保存</param>
        void Delete(Expression<Func<TEntity, bool>> expression, bool autoSave = true);

        //
        // 摘要:
        //     Deletes an entity by primary key.
        //
        // 参数:
        //   id:
        //     Primary key of the entity
        Task DeleteAsync(TPrimaryKey id);
        /// <summary>
        /// 按功能删除许多实体。 请注意：所有实体都适合给定的谓词
        /// 被检索并删除。 如果存在这可能会导致严重的性能问题
        /// 包含给定谓词的实体太多了
        /// </summary>
        /// <param name="predicate">过滤实体的条件</param>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除实体</param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// 获取具有给定给定谓词的实体，如果未找到则获取null
        /// </summary>
        /// <param name="predicate">谓词来过滤实体</param>
        /// <returns>实体或null</returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取具有给定主键的实体，如果未找到则获取null
        /// </summary>
        /// <param name="id">要获取的实体的主键</param>
        /// <returns>实体或null/returns>
        TEntity FirstOrDefault(TPrimaryKey id);

        /// <summary>
        /// 获取具有给定给定谓词的实体，如果未找到则获取null
        /// </summary>
        /// <param name="predicate">过滤实体的条件</param>
        /// <returns>实体</returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取具有给定主键的实体，如果未找到则获取null
        /// </summary>
        /// <param name="id">获取的实体的主键</param>
        /// <returns>实体</returns>
        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);
        /// <summary>
        /// 获取具有给定主键的实体
        /// </summary>
        /// <param name="id">获取的实体的主键</param>
        /// <returns>实体</returns>
        TEntity Get(TPrimaryKey id);

        /// <summary>
        /// 用于获取用于从整个表中检索实体的IQueryable。
        /// </summary>
        /// <returns>IQueryable用于从数据库中选择实体</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 用于获取用于从整个表中检索实体的IQueryable一个或多个
        /// </summary>
        /// <param name="propertySelectors">包含表达式的列表</param>
        /// <returns>IQueryable用于从数据库中选择实体</returns>
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

        /// <summary>
        /// 用于根据给定的谓词获取所有实体
        /// </summary>
        /// <param name="predicate">过滤实体的条件</param>
        /// <returns>实体</returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 用于获取所有实体
        /// </summary>
        /// <returns>实体</returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// 用于根据给定的谓词获取所有实体
        /// </summary>
        /// <param name="predicate">过滤实体的条件</param>
        /// <returns>实体</returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 用于获取所有实体
        /// </summary>
        /// <returns>实体</returns>
        Task<List<TEntity>> GetAllListAsync();

        /// <summary>
        /// 获取具有给定主键的实体
        /// </summary>
        /// <param name="id">要获取的实体的主键</param>
        /// <returns>实体</returns>
        Task<TEntity> GetAsync(TPrimaryKey id);

        /// <summary>
        /// 插入一个新实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Insert(TEntity entity);

        /// <summary>
        /// 插入一个新实体并获取它的ID。 可能需要保存当前单位
        /// 能够检索id
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体Id</returns>
        TPrimaryKey InsertAndGetId(TEntity entity);

        /// <summary>
        /// 插入一个新实体并获取它的ID。 可能需要保存当前单位能够检索id
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体得ID</returns>
        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

        /// <summary>
        /// 插入一个新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>插入的实体</returns>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// 根据Id的值插入或更新给定实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体</returns>
        void InsertOrUpdate(TEntity entity);

        /// <summary>
        /// 根据Id的值插入或更新给定实体.也返回的Id
        /// 实体.可能需要保存当前工作单元才能检索id。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体ID</returns>
        TPrimaryKey InsertOrUpdateAndGetId(TEntity entity);

        /// <summary>
        /// 根据Id的值插入或更新给定实体.也返回的Id
        /// 实体.可能需要保存当前工作单元才能检索id。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回实体得ID</returns>
        Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity);

        /// <summary>
        /// 根据Id的值插入或更新给定实体。
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体</returns>
        Task InsertOrUpdateAsync(TEntity entity);





        /// <summary>
        /// 根据给定谓词获取此存储库中所有实体的计数（使用此方法）
                /// 如果预期的返回值比System.Int32.MaxValue强，则重载。
        /// </summary>
        /// <param name="predicate">过滤计数的方法</param>
        /// <returns>实体得数量</returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据给定谓词获取此存储库中所有实体的计数（使用此方法）
                /// 如果预期的返回值比System.Int32.MaxValue强，则重载。
        /// </summary>
        /// <returns>实体得数量</returns>
        long LongCount();

        /// <summary>
        /// 根据给定谓词获取此存储库中所有实体的计数（使用此方法）
                /// 如果预期的返回值比System.Int32.MaxValue强，则重载。
        /// </summary>
        /// <returns>实体得数量</returns>
        Task<long> LongCountAsync();

        /// <summary>
        /// 根据给定谓词获取此存储库中所有实体的计数（使用此方法）
                /// 如果预期的返回值比System.Int32.MaxValue强，则重载。
        /// </summary>
        /// <param name="predicate">过滤计数的方法</param>
        /// <returns>实体得数量</returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取具有给定谓词的一个实体。 如果没有实体或者引发异常
        /// 多个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体</returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取具有给定谓词的一个实体。 如果没有实体或者引发异常
        /// 多个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体</returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 更新现有实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        void Update(TEntity entity);

        /// <summary>
        /// 更新现有实体
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="updateAction">可用于更改实体值的操作</param>
        /// <returns>更新的实体</returns>
        void Update(TPrimaryKey id, Action<TEntity> updateAction);

        /// <summary>
        /// 更新现有实体
        /// </summary>
        /// <param name="id">实体的ID</param>
        /// <param name="updateAction">可用于更改实体值的操作</param>
        /// <returns>更新了实体</returns>
        Task UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction);

        /// <summary>
        /// 更新现有实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>更新的实体</returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面条目</param>
        /// <param name="rowCount">数据总数</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        IQueryable<TEntity> LoadPageList(int startPage, int pageSize, out int rowCount, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> order);


        void Save();
    }

    /// <summary>
    /// 默认Guid主键类型仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : Entity
    {

    }
}
