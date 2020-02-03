using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public interface ICommonService<T> where T:class
    {
        void Add(T entity);
        T Create(T entity);
        IList<T> GetAll();
    }
}
