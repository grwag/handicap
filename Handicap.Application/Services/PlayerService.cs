﻿using Handicap.Domain.Models;
using System.Threading.Tasks;
using System;
using Handicap.Application.Interfaces;
using System.Linq;

namespace Handicap.Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<Player> InsertPlayer(Player player)
        {
            await _playerRepository.Insert(player);

            return await _playerRepository.GetById(player.Id);
        }

        public async Task<Player> GetById(Guid id)
        {
            var player = await _playerRepository.GetById(id);

            return player;
        }

        public async Task Delete(Guid id)
        {
            var player = await _playerRepository.GetById(id);

            _playerRepository.Delete(player);
            await _playerRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<Player>> All()
        {
            var result = await _playerRepository.All();

            return result;
        }

        public async Task<Player> Update(Player player)
        {
            await _playerRepository.Update(player);

            var updatedPlayer = await _playerRepository.GetById(player.Id);

            return updatedPlayer;
        }
    }
}
