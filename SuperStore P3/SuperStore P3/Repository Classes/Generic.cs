﻿using EcoPower_Logistics.Data;
using EcoPower_Logistics.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EcoPower_Logistics.Repository_Classes
{
    public class Generic<T> : IGeneric<T> where T : class
    {
        protected readonly SuperStoreContext _context;

        public Generic(SuperStoreContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        //Made virtual to alllow overrides in special cases 
        public virtual async Task<T> FindById(Guid? id)
        {
            if (id == null) return null;

            return await _context.Set<T>().FindAsync(id);
        }

        //Made virtual to alllow overrides in special cases 
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void Remove(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException excp)
            {
                throw excp;
            }
        }

        public void Edit(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException excp)
            {
                throw excp;
            }
        }
    }
}

