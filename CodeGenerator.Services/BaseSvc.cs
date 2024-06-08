using CodeGenerator.Repository.Interface;
using CodeGenerator.Services.Interface;
using CodeGenerator.Services.Interface.ResultModel;
using NLog;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeGenerator.Services
{
    public class BaseSvc<T> : IBaseSvc<T> where T : class, new()
    {
        protected readonly IBaseRepository<T> _repository;
        protected static readonly Logger _logger = LogManager.GetCurrentClassLogger();


        public BaseSvc(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<T>> GetListAsync()
        {
            return await _repository.GetListAsync();
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await _repository.GetListAsync(whereExpression);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await _repository.GetSingleAsync(whereExpression);
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await _repository.GetFirstAsync(whereExpression);
        }

        public async Task<PageResultModel<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel pageModel)
        {
            PageResultModel<T> result = new PageResultModel<T>();
            result.List = await _repository.GetPageListAsync(whereExpression, pageModel);
            result.Total = pageModel.TotalCount;
            return result;
        }

        public async Task<PageResultModel<T>> GetPageListAsync(List<IConditionalModel> conditionalModels, PageModel pageModel)
        {
            PageResultModel<T> result = new PageResultModel<T>();
            result.List = await _repository.GetPageListAsync(conditionalModels, pageModel);
            result.Total = pageModel.TotalCount;
            return result;
        }

        public async Task<PageResultModel<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel pageModel, Expression<Func<T, object>> orderByExpression, OrderByType orderByType)
        {
            PageResultModel<T> result = new PageResultModel<T>();
            result.List = await _repository.GetPageListAsync(whereExpression, pageModel, orderByExpression, orderByType);
            result.Total = pageModel.TotalCount;
            return result;
        }

        public async Task<List<T>> QueryableToListAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await _repository.AsQueryable().Where(whereExpression).ToListAsync();
        }

        // 插入
        public async Task InsertAsync(T entity)
        {
            await _repository.InsertAsync(entity);
        }

        public async Task InsertRangeAsync(List<T> entities)
        {
            await _repository.InsertRangeAsync(entities);
        }

        public async Task<object> InsertReturnIdentityAsync(T entity)
        {
            return await _repository.InsertReturnIdentityAsync(entity);
        }

        public async Task<object> InsertReturnSnowflakeIdAsync(T entity)
        {
            return await _repository.InsertReturnSnowflakeIdAsync(entity);
        }

        // 删除
        public async Task DeleteAsync(T entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task DeleteAsync(List<T> entities)
        {
            await _repository.DeleteAsync(entities);
        }

        public async Task DeleteByIdAsync(object id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        public async Task DeleteByIdsAsync(object[] ids)
        {
            await _repository.DeleteByIdsAsync(ids);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> whereExpression)
        {
            await _repository.DeleteAsync(whereExpression);
        }

        // 更新
        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task UpdateRangeAsync(List<T> entities)
        {
            await _repository.UpdateRangeAsync(entities);
        }

        public async Task UpdateAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression)
        {
            await _repository.UpdateAsync(columns, whereExpression);
        }

        public async Task UpdateSetColumnsTrueAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression)
        {
            await _repository.UpdateSetColumnsTrueAsync(columns, whereExpression);
        }

        public async Task UpdateColumnsAsync(Expression<Func<T, bool>> columns, Expression<Func<T, bool>> whereExpression)
        {
            await _repository.AsUpdateable().SetColumns(columns).Where(whereExpression).ExecuteCommandAsync();
        }
    }
}
