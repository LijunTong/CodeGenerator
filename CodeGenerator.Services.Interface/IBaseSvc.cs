using CodeGenerator.Services.Interface.ResultModel;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Services.Interface
{
    public interface IBaseSvc<T>
    {
        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>实体对象</returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// 获取所有实体列表
        /// </summary>
        /// <returns>实体列表</returns>
        Task<List<T>> GetListAsync();

        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>实体列表</returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 获取符合条件的单个实体
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>单个实体</returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 获取符合条件的第一条记录
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>第一条记录</returns>
        Task<T> GetFirstAsync(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 分页获取实体列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="pageModel">分页模型</param>
        /// <returns>分页结果</returns>
        Task<PageResultModel<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel pageModel);

        /// <summary>
        /// 分页获取实体列表
        /// </summary>
        /// <param name="conditionalModels">条件模型列表</param>
        /// <param name="pageModel">分页模型</param>
        /// <returns>分页结果</returns>
        Task<PageResultModel<T>> GetPageListAsync(List<IConditionalModel> conditionalModels, PageModel pageModel);

        /// <summary>
        /// 分页获取实体列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="pageModel">分页模型</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="orderByType">排序类型</param>
        /// <returns>分页结果</returns>
        Task<PageResultModel<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel pageModel, Expression<Func<T, object>> orderByExpression, OrderByType orderByType);

        /// <summary>
        /// 将查询结果转换为列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>实体列表</returns>
        Task<List<T>> QueryableToListAsync(Expression<Func<T, bool>> whereExpression);

        // 插入
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        Task InsertAsync(T entity);

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        Task InsertRangeAsync(List<T> entities);

        /// <summary>
        /// 插入实体并返回自增ID
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>自增ID</returns>
        Task<object> InsertReturnIdentityAsync(T entity);

        /// <summary>
        /// 插入实体并返回雪花ID
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>雪花ID</returns>
        Task<object> InsertReturnSnowflakeIdAsync(T entity);

        // 删除
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        Task DeleteAsync(List<T> entities);

        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id">实体ID</param>
        Task DeleteByIdAsync(object id);

        /// <summary>
        /// 根据ID数组删除实体
        /// </summary>
        /// <param name="ids">实体ID数组</param>
        Task DeleteByIdsAsync(object[] ids);

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        Task DeleteAsync(Expression<Func<T, bool>> whereExpression);

        // 更新
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        Task UpdateRangeAsync(List<T> entities);

        /// <summary>
        /// 根据条件更新实体
        /// </summary>
        /// <param name="columns">更新的列</param>
        /// <param name="whereExpression">条件表达式</param>
        Task UpdateAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 更新指定列且过滤字段
        /// </summary>
        /// <param name="columns">更新的列</param>
        /// <param name="whereExpression">条件表达式</param>
        Task UpdateSetColumnsTrueAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 根据条件更新指定列
        /// </summary>
        /// <param name="columns">更新的列</param>
        /// <param name="whereExpression">条件表达式</param>
        Task UpdateColumnsAsync(Expression<Func<T, bool>> columns, Expression<Func<T, bool>> whereExpression);
    }
}
