﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Handicap.Domain.Models;

namespace Handicap.Application.Interfaces {
    public interface IPlayerRepository
    {
        Task<IQueryable<Player>> All(params string[] navigationProperties);

        Task Insert(Player player);
        void Delete(Guid id);
        Task<Player> GetById(Guid id);
        Task Update(Player player);
        Task SaveChangesAsync();
    }
}