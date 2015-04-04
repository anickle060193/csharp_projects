using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    public struct TerrainCell
    {
        public enum TerrainType { Air, Dirt, Grass }

        public readonly TerrainType Terrain;

        public TerrainCell( TerrainType type )
        {
            Terrain = type;
        }
    }
}
