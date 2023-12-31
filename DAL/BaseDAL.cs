﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Microsoft.EntityFrameworkCore;
using Model;
using Models;

namespace DAL
{
    public class BaseDAL<T> : IBaseDAL<T> where T : BaseEntity
    {
        DemoManageSysDbContext _dbContext;

        public BaseDAL(
            DemoManageSysDbContext dbContext
            )
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 获取范式返回的表
        /// </summary>
        /// <returns></returns>
        public DbSet<T> GetEntities()
        {
            return _dbContext.Set<T>();
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool CreateEntity(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges() > 0;//看描述
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">根据ID</param>
        /// <returns></returns>
        public bool DeleteEntityById(string id)
        {
            T entity = _dbContext.Set<T>().FirstOrDefault(it => it.Id == id);//根据ID查出符合条件的第一条数据
            //判断是否存在实体
            if (entity == null)
            {
                return false;
            }
            else
            {
                _dbContext.Set<T>().Remove(entity);
                return _dbContext.SaveChanges() > 0;//看描述
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">根据实体</param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            else
            {
                _dbContext.Set<T>().Remove(entity);
                return _dbContext.SaveChanges() > 0;//看描述
            }
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool UpdateEntity(T entity)
        {
            _dbContext.Set<T>().Update(entity);

            return _dbContext.SaveChanges() > 0;//看描述
        }

        /// <summary>
        /// 批量修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool UpdateListEntity(List<T> entity)
        {
            _dbContext.Set<T>().UpdateRange(entity);

            return _dbContext.SaveChanges() > 0;//看描述
        }
    }
}
