﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Handicap.Dto.Request
{
    public class AddRemovePlayerToMatchDayRequest
    {
        public IEnumerable<string> PlayerIds { get; set; }
    }
}
