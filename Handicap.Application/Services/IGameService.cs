﻿using Handicap.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Handicap.Application.Services
{
    public interface IGameService
    {
        Task<Game> CreateGame(string PlayerOneId, string PlayerTwoId, string TenantId, string MatchDayId);
        Task Delete(string id, string tenantId);
        Task<Game> Add(Game game);
        Task<Game> Update(GameUpdate gameUpdate);
        Task<Game> CreateNewGameForMatchDay(string matchDayId, string tenantId);
        Task<IQueryable<Game>> Find(
            Expression<Func<Game, bool>> expression,
            params string[] navigationProperties);
        Task<Game> GetById(string Id);
        Task<IQueryable<Game>> GetGamesForPlayer(string playerId);
    }
}
