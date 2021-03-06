﻿using Handicap.Application.Interfaces;
using Handicap.Data.Infrastructure;
using Handicap.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Handicap.Data.Repo
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly HandicapContext _context;
        private readonly DbSet<Player> _players;

        public PlayerRepository(HandicapContext context)
        {
            _context = context;
            _players = context.Set<Player>();
        }

        public async Task<Player> AddOrUpdate(Player player)
        {
            if (PlayerExists(player))
            {
                return await Update(player);
            }
            else
            {
                return await Add(player);
            }
        }

        public async Task<IQueryable<Player>> Find(
            Expression<Func<Player, bool>> expression = null,
            params string[] navigationProperties)
        {
            var query = _players
                //.Include(p => p.MatchDayPlayers)
                .AsNoTracking()
                .AsQueryable()
                ;

            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            if (expression != null)
            {
                query = query
                    .Where(expression);
            }

            return await Task.FromResult(query);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var playerDbo = await _players.Where(p => p.Id == id).SingleOrDefaultAsync();

            if (playerDbo != null)
            {
                _players.Remove(playerDbo);
            }
        }
        private async Task<Player> Add(Player player)
        {
            await _players.AddAsync(player);

            return player;
        }

        private async Task<Player> Update(Player player)
        {
            _context.Update(player);

            return await Task.FromResult(player);
        }


        private bool PlayerExists(Player player)
        {
            var query = _players
                .AsNoTracking();

            if (query.Where(p => p.Id == player.Id).FirstOrDefault() != null) { return true; }

            var exists = query.Where(
                p => p.FirstName == player.FirstName
                && p.LastName == player.LastName)
                .FirstOrDefault();

            return exists != null;
        }
    }
}
