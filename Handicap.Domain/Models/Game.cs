﻿using System;

namespace Handicap.Domain.Models
{
    public class Game : BaseEntity {
        private readonly Player _playerOne;
        private readonly Player _playerTwo;
        private readonly Random _rnd;

        public GameType Type { get; }
        public int PlayerOneRequiredPoints { get; set; }
        public int PlayerOnePoints { get; set; }
        public int PlayerTwoRequiredPoints { get; set; }
        public int PlayerTwoPoints { get; set; }
        public DateTimeOffset Date { get; set; }
        public int Table { get; set; }
    }
}