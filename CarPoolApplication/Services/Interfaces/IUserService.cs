using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public interface IUserService<T> where T:class
    {
        void Add(T entity);
        T Create(T entity);
        List<T> GetAll();
    }
}
