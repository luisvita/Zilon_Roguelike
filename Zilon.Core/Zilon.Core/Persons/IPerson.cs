﻿using Zilon.Core.Props;

namespace Zilon.Core.Persons
{
    /// <summary>
    /// Интерфейс персонажа.
    /// </summary>
    /// <remarks>
    /// Персонаж - это описание игрового объекта за пределами тактических боёв.
    /// </remarks>
    public interface IPerson
    {
        int Id { get; set; }

        /// <summary>
        /// Носитель экипировки.
        /// </summary>
        IEquipmentCarrier EquipmentCarrier { get; }

        /// <summary>
        /// Носитель тактических действий.
        /// </summary>
        ITacticalActCarrier TacticalActCarrier { get; }

        /// <summary>
        /// Данные о развитие персонажа.
        /// </summary>
        IEvolutionData EvolutionData { get; }

        /// <summary>
        /// Характеристики, используемые персонажем в бою.
        /// </summary>
        ICombatStats CombatStats { get; }

        /// <summary>
        /// Инвентарь персонажа.
        /// </summary>
        /// <remarks>
        /// Для монстров равен null.
        /// </remarks>
        IPropStore Inventory { get; }

        /// <summary>
        /// Данные по выживанию персонажа.
        /// </summary>
        ISurvivalData Survival { get; }

        EffectCollection Effects { get; }
    }
}