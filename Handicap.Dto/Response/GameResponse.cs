﻿using System;

namespace Handicap.Dto.Response
{
    public class GameResponse : BaseResponse
    {
        public PlayerResponse PlayerOne { get; set; }
        public PlayerResponse PlayerTwo { get; set; }
        public string MatchDayId { get; set; }
        public int Type { get; set; }
        public int PlayerOneRequiredPoints { get; set; }
        public int PlayerOnePoints { get; set; }
        public int PlayerTwoRequiredPoints { get; set; }
        public int PlayerTwoPoints { get; set; }
        public DateTimeOffset Date { get; set; }
        public bool IsFinished { get; set; }
    }
}
