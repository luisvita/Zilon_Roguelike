﻿using System;

using Zilon.Core.Persons;

namespace Zilon.Core.Tactics
{
    /// <summary>
    /// Аргументы события, которое выстреливает, когда актёр отрабатывает оборону.
    /// </summary>
    public sealed class DefenceEventArgs : EventArgs
    {
        public DefenceEventArgs(PersonDefenceItem prefferedDefenceItem, int successToHitRoll, int factToHitRoll)
        {
            PrefferedDefenceItem = prefferedDefenceItem ?? throw new ArgumentNullException(nameof(prefferedDefenceItem));
            SuccessToHitRoll = successToHitRoll;
            FactToHitRoll = factToHitRoll;
        }

        /// <summary>
        /// Оборона, которая была использована.
        /// </summary>
        public PersonDefenceItem PrefferedDefenceItem { get; }

        /// <summary>
        /// Бросок, который был необходим для того, чтобы пробить оборону.
        /// </summary>
        public int SuccessToHitRoll { get; }

        /// <summary>
        /// Фактический бросок, который был выполнен для пробития обороны.
        /// </summary>
        public int FactToHitRoll { get; }
    }
}
