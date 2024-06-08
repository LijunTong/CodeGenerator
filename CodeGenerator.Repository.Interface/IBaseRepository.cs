using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Repository.Interface
{
    public interface IBaseRepository<T> : ISimpleClient<T> where T : class, new()
    {
    }
}
