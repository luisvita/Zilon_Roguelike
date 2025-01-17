﻿using System.Linq;

using Zilon.Core.Props;
using Zilon.Core.Tactics;
using Zilon.Core.Tactics.Behaviour;
using Zilon.Core.Tactics.Spatial;

namespace Zilon.Bot.Players.Logics
{
    public sealed class LootLogicState : LogicStateBase
    {
        public IPropContainer _propContainer;

        public MoveTask _moveTask;

        private readonly IPropContainerManager _propContainerManager;
        private readonly ISectorMap _map;

        public LootLogicState(IPropContainerManager propContainerManager, ISectorManager sectorManager)
        {
            _propContainerManager = propContainerManager;
            _map = sectorManager.CurrentSector.Map;
        }

        public IPropContainer FindContainer(IActor actor)
        {
            var foundContainers = LootHelper.FindAvailableContainers(_propContainerManager.Items,
                actor.Node,
                _map);

            var orderedContainers = foundContainers.OrderBy(x => _map.DistanceBetween(actor.Node, x.Node));
            var nearbyContainer = orderedContainers.FirstOrDefault();

            return nearbyContainer;
        }

        public override IActorTask GetTask(IActor actor, ILogicStrategyData strategyData)
        {
            _propContainer = FindContainer(actor);

            if (_propContainer == null || !_propContainer.Content.CalcActualItems().Any())
            {
                Complete = true;
                return null;
            }

            var distance = _map.DistanceBetween(actor.Node, _propContainer.Node);
            if (distance <= 1)
            {
                return TakeAllFromContainerTask(actor, _propContainer);
            }
            else
            {
                var storedMoveTask = _moveTask;
                var moveTask = MoveToContainerTask(actor, _propContainer.Node, storedMoveTask);
                _moveTask = moveTask;
                return moveTask;
            }
        }

        private MoveTask MoveToContainerTask(IActor actor, IMapNode containerMapNode, MoveTask storedMoveTask)
        {
            var moveTask = storedMoveTask;
            if (storedMoveTask == null)
            {
                moveTask = new MoveTask(actor, containerMapNode, _map);
            }

            if (moveTask.IsComplete || !moveTask.CanExecute())
            {
                Complete = true;
                return null;
            }

            return moveTask;
        }

        private static IActorTask TakeAllFromContainerTask(IActor actor, IPropContainer container)
        {
            var inventoryTransfer = new PropTransfer(actor.Person.Inventory,
                                container.Content.CalcActualItems(),
                                new IProp[0]);

            var containerTransfer = new PropTransfer(container.Content,
                new IProp[0],
                container.Content.CalcActualItems());

            return new TransferPropsTask(actor, new[] { inventoryTransfer, containerTransfer });
        }

        protected override void ResetData()
        {
            _propContainer = null;
            _moveTask = null;
        }
    }
}
