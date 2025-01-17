﻿using System.Linq;

using Zilon.Core.Schemes;
using Zilon.Core.World;
using Zilon.Core.WorldGeneration;

namespace Zilon.Core.ProgressStoring
{
    public sealed class GlobeRegionStorageData
    {
        public string Id { get; set; }
        public GlobeRegionNodeStorageData[] Nodes { get; set; }

        public static GlobeRegionStorageData Create(GlobeRegion globeRegion, TerrainCell terrainCell)
        {
            var storageData = new GlobeRegionStorageData();

            storageData.Id = terrainCell.Coords.ToString();
            storageData.Nodes = globeRegion.RegionNodes.Select(x => GlobeRegionNodeStorageData.Create(x)).ToArray();

            return storageData;
        }

        public GlobeRegion Restore(ISchemeService schemeService)
        {
            var globeNode = new GlobeRegion(20);

            foreach (var storedNode in Nodes)
            {
                var scheme = schemeService.GetScheme<ILocationScheme>(storedNode.SchemeSid);
                var node = new GlobeRegionNode(storedNode.Coords.X, storedNode.Coords.Y, scheme)
                {
                    IsBorder = storedNode.IsBorder,
                    IsHome = storedNode.IsHome,
                    IsStart = storedNode.IsStart,
                    IsTown = storedNode.IsTown,
                    ObservedState = storedNode.Observed
                };
                globeNode.AddNode(node);
            }

            return globeNode;
        }
    }
}
