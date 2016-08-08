using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GR.Core.Data;
using GR.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace GR.Data.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EfRepository<TEntity> : EfRepository<GRDbContext, TEntity, int> where TEntity : BaseEntity
    {
        public EfRepository(GRDbContext dbContext) : base(dbContext)
        { }
    }

    /// <summary>
    /// 通用仓储基类 
    /// </summary>
    /// <remarks>
    ///  参考了 ABP 的 仓储基类 
    ///  https://github.com/aspnetboilerplate
    /// </remarks>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public class EfRepository<TDbContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TDbContext : DbContext
        where TEntity : BaseEntity
    {
        private readonly TDbContext _dbContext;

        public EfRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        public virtual DbSet<TEntity> Table
        {
            get
            {
                return _dbContext.Set<TEntity>();
            }
        }

        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        public virtual TDbContext DbContext
        {
            get { return _dbContext; }
        }

        public IQueryable<TEntity> GetAll()
        {
            return Table;
        }

        public TEntity FirstOrDefault(TPrimaryKey id)
        {
            return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public List<TEntity> GetAllList()
        {
            return Table.ToList();
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Where(predicate).ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        public TEntity Get(TPrimaryKey id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new ArgumentNullException("There is no such an entity with given primary key. Entity type: " + typeof(TEntity).FullName + ", primary key: " + id);
            }
            return entity;
        }

        public async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsync(id);
            if (entity == null)
            {
                throw new ArgumentNullException("There is no such an entity with given primary key. Entity type: " + typeof(TEntity).FullName + ", primary key: " + id);
            }

            return entity;
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        public int Insert(TEntity entity, bool IsCommit = true)
        {
            Table.Add(entity);
            return IsCommit ? _dbContext.SaveChanges() : -1;
        }

        public Task<int> InsertAsync(TEntity entity, bool IsCommit = true)
        {
            Table.Add(entity);
            return IsCommit ? Task.FromResult(_dbContext.SaveChanges()) : Task.FromResult(-1);
        }

        public int Update(TEntity entity, bool IsCommit = true)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return IsCommit ? _dbContext.SaveChanges() : -1;
        }

        public Task<int> UpdateAsync(TEntity entity, bool IsCommit = true)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return IsCommit ? Task.FromResult(_dbContext.SaveChanges()) : Task.FromResult(-1);
        }


        public int InsertOrUpdate(TEntity entity, bool IsCommit = true)
        {
            return entity.IsTransient() ? Insert(entity, IsCommit) : Update(entity, IsCommit);
        }

        public async Task<int> InsertOrUpdateAsync(TEntity entity, bool IsCommit = true)
        {
            return entity.IsTransient() ? await InsertAsync(entity, IsCommit) : await UpdateAsync(entity, IsCommit);
        }

        public int Delete(TEntity entity, bool IsCommit = true)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
            return IsCommit ? _dbContext.SaveChanges() : -1;
        }

        public int Delete(TPrimaryKey id, bool IsCommit = true)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                return 0;
            }
            return Delete(entity, IsCommit);
        }

        public Task DeleteAsync(TEntity entity, bool IsCommit = true)
        {
            return Task.FromResult(Delete(entity, IsCommit));
        }

        public Task DeleteAsync(TPrimaryKey id, bool IsCommit = true)
        {
            return Task.FromResult(Delete(id, IsCommit));
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate, bool IsCommit = true)
        {
            var entities = GetAll().Where(predicate).ToList();
            entities.ForEach(entity =>
            {
                Delete(entity, IsCommit);
            });
        }


        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool IsCommit = true)
        {
            Delete(predicate, IsCommit);
            return Task.FromResult(1);
        }
        public async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }

        public async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).LongCountAsync();
        }

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        protected void AttachIfNot(TEntity entity)
        {
            //if (!Table.Local.Contains(entity))
            //{
            //    Table.Attach(entity);
            //}
            Table.Attach(entity);
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public TEntity Load(TPrimaryKey id)
        {
            return Get(id);
        }

        public int Count()
        {
            return GetAll().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).Count();
        }

        public long LongCount()
        {
            return GetAll().LongCount();
        }

        public long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).LongCount();
        }

    }
}
