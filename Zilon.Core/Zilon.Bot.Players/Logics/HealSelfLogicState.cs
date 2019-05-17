﻿using System.Collections.Generic;
using System.Linq;

using Zilon.Core.Components;
using Zilon.Core.Persons;
using Zilon.Core.Props;
using Zilon.Core.Schemes;
using Zilon.Core.Tactics;
using Zilon.Core.Tactics.Behaviour;

namespace Zilon.Bot.Players.Logics
{
    public class HealSelfLogicState : ILogicState
    {
        public bool Complete { get; private set; }

        public ILogicStateData CreateData(IActor actor)
        {
            return new EmptyLogicTriggerData();
        }

        public IActorTask GetTask(IActor actor, ILogicStateData data)
        {
            var hpStat = actor.Person.Survival.Stats.SingleOrDefault(x => x.Type == SurvivalStatType.Health);
            var hpStatCoeff = (float)hpStat.Value / (hpStat.Range.Max - hpStat.Range.Min);
            var isLowHp = hpStatCoeff <= 0.5f;
            if (!isLowHp)
            {
                Complete = true;
                return null;
            }

            var props = actor.Person.Inventory.CalcActualItems();
            var resources = props.OfType<Resource>();
            var foundHealResources = FindHealResources(resources);

            var orderedHealResources = foundHealResources.OrderByDescending(x => x.Rule.Level);
            var bestHealResource = foundHealResources.FirstOrDefault();

            if (bestHealResource == null)
            {
                Complete = true;
                return null;
            }

            return new UsePropTask(actor, bestHealResource.Resource);
        }

        private static IEnumerable<HealSelection> FindHealResources(IEnumerable<Resource> resources)
        {
            foreach (var resource in resources)
            {
                var rule = resource.Scheme.Use.CommonRules
                    .SingleOrDefault(x => x.Type == ConsumeCommonRuleType.Health && x.Direction == PersonRuleDirection.Positive);

                if (rule != null)
                {
                    yield return new HealSelection
                    {
                        Resource = resource,
                        Rule = rule
                    };
                }
            }
        }

        private class HealSelection
        {
            public Resource Resource;
            public ConsumeCommonRule Rule;
        }
    }
}