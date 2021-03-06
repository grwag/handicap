﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Handicap.Api.Extensions;
using Handicap.Api.Paging;
using Handicap.Application.Exceptions;
using Handicap.Application.Interfaces;
using Handicap.Application.Services;
using Handicap.Domain.Models;
using Handicap.Dto.Request;
using Handicap.Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Handicap.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize("read_write")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GamesController> _logger;
        private readonly IMapper _mapper;

        public GamesController(
            IGameService gameService,
            ILogger<GamesController> logger,
            IMapper mapper)
        {
            _gameService = gameService;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery]string orderBy = "Date",
            [FromQuery]bool desc = false,
            [FromQuery]int pageSize = 10,
            [FromQuery]int page = 0)
        {
            var tenantId = this.GetTenantId();

            var query = await _gameService.Find(g => g.TenantId == tenantId,
                nameof(Game.PlayerOne),
                nameof(Game.PlayerTwo));

            query = desc ?
                query.OrderByDescending(orderBy)
                : query.OrderBy(orderBy);

            //var responseQuery = query.ProjectTo<GameResponse>(_mapper.ConfigurationProvider);

            var response = HandicapResponse<GameResponse, Game>.Create(query, null, page, pageSize, _mapper);

            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetGameById")]
        public async Task<IActionResult> GetById([FromRoute]string id)
        {
            var tenantId = this.GetTenantId();

            var query = await _gameService.Find(g => g.TenantId == tenantId,
                nameof(Game.PlayerOne),
                nameof(Game.PlayerTwo));

            var game = query.FirstOrDefault(g => g.Id == id);

            if(game == null)
            {
                throw new EntityNotFoundException($"Game with id {id} not found.");
            }

            var gameResponse = _mapper.Map<GameResponse>(query.FirstOrDefault());

            return Ok(gameResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GameRequest gameRequest)
        {
            var tenantId = this.GetTenantId();

            var game = await _gameService.CreateGame(
                tenantId,
                gameRequest.PlayerOneId,
                gameRequest.PlayerTwoId,
                gameRequest.MatchDayId);

            game = await _gameService.Add(game);
            var gameResponse = _mapper.Map<GameResponse>(game);

            return CreatedAtAction(
                nameof(GetById),
                new { id = game.Id },
                gameResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]string id, [FromBody]GameUpdateDto gameUpdateDto)
        {
            var gameUpdate = _mapper.Map<GameUpdate>(gameUpdateDto);

            var game = await _gameService.Update(gameUpdate);
            var gameResponse = _mapper.Map<GameResponse>(game);

            return Ok(gameResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete ([FromRoute]string id)
        {
            var tenantId = this.GetTenantId();

            await _gameService.Delete(id, tenantId);
            return NoContent();
        }
    }
}