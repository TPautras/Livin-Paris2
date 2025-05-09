﻿using System;
using System.Collections.Generic;

namespace SqlConnector
{
    public interface IDataAccess<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}