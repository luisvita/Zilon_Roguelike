﻿using Zilon.Core.Common;
using Zilon.Core.Schemes;

namespace Zilon.Core.Persons
{
    /// <summary>
    /// Тактическое действие актёров под управлением игрока.
    /// </summary>
    public class TacticalAct : ITacticalAct
    {
        public TacticalAct(TacticalActScheme scheme)
        {
            Scheme = scheme;
            Stats = scheme.Stats;
        }

        public TacticalActStatsSubScheme Stats { get; }

        public TacticalActScheme Scheme { get; }
    }
}
