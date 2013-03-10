using System;
using System.Collections.Generic;
using System.Linq;
using democracy.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace democracy.DB
{
    public class Repository<T>
    {
        protected readonly string _collectionName;

        public Repository(string collection)
        {
            _collectionName = collection;
        }

        protected MongoCollection<T> Collection
        {
            get
            {
                return Database.Instance.GetCollection<T>(_collectionName);
            }
        }

        public IEnumerable<T> All()
        {
            return Collection.FindAll();
        }

        public void Save(T item)
        {
            Collection.Save(item);
        }
    }
}
