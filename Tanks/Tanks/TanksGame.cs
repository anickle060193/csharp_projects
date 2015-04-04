using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks
{
    public class TanksGame
    {
        private static TerrainCell AirCell = new TerrainCell( Tanks.TerrainCell.TerrainType.Air );
        private static TerrainCell GrassCell = new TerrainCell( Tanks.TerrainCell.TerrainType.Grass );
        private static TerrainCell DirtCell = new TerrainCell( Tanks.TerrainCell.TerrainType.Dirt );

        private readonly Timer _timer = new Timer();

        private TerrainCell[ , ] _terrain;
        private bool _terrainGenerated;
        private bool _needsUpdate;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int CellSize { get; private set; }

        public float PeakHeight { get; private set; }
        public float Flatness { get; private set; }
        public int Offset { get; private set; }
        public int GrassThickness { get; private set; }

        public TanksGame( int width, int height, int cellSize )
        {
            Width = width;
            Height = height;
            CellSize = cellSize;

            PeakHeight = 80.0f;
            Flatness = 120.0f;
            Offset = Height / 2;

            GrassThickness = 10;

            _timer.Interval = 200;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick( object sender, EventArgs e )
        {
            if( _needsUpdate )
            {
                UpdateTerrain();
            }
        }

        public void InitializeTerrainGeneration( float peakHeight, float flatness, int grassThickness )
        {
            if( _terrainGenerated )
            {
                throw new InvalidOperationException( "Terrain has already been generated and therefore cannot be re-initialized." );
            }
            PeakHeight = peakHeight;
            Flatness = flatness;
            GrassThickness = grassThickness;
        }

        public void RunTerrainGeneration()
        {
            GenerateTerrain( Width, Height );
        }

        private void GenerateTerrain( int width, int height )
        {
            _terrainGenerated = true;
            _terrain = new TerrainCell[ width, height ];

            int[] elevation = GenerateElevationLine( width, PeakHeight, Flatness, Offset );
            for( int x = 0; x < width; x++ )
            {
                int elevationAtX = elevation[ x ];
                for( int y = 0; y < height; y++ )
                {
                    int actualY = height - y;
                    if( actualY > elevationAtX )
                    {
                        _terrain[ x, y ] = AirCell;
                    }
                    else if( actualY > elevationAtX - GrassThickness )
                    {
                        _terrain[ x, y ] = GrassCell;
                    }
                    else
                    {
                        _terrain[ x, y ] = DirtCell;
                    }
                }
            }
            _needsUpdate = true;
        }

        private int[] GenerateElevationLine( int width, float peakHeight, float flatness, int offset )
        {
            double[] rands = new double[] { Utilities.R.NextDouble() + 2, Utilities.R.NextDouble() + 1, Utilities.R.NextDouble() + 3 };

            int[] elevation = new int[ width ];
            for( int x = 0; x < width; x++ )
            {
                double h = 0;
                foreach( double rand in rands )
                {
                    h += peakHeight / rand * Math.Sin( (float)x / flatness * rand + rand );
                }
                h += offset;
                elevation[ x ] = (int)h;
            }
            return elevation;
        }

        public void RemoveTerrain( int x, int y, int radius )
        {
            foreach( Point p in GetPointsInCircle( new Point( x, y ), radius ) )
            {
                if( 0 <= p.X && p.X < Width
                 && 0 <= p.Y && p.Y < Height )
                {
                    _terrain[ p.X, p.Y ] = AirCell;
                }
            }
            _needsUpdate = true;
        }

        public void UpdateTerrain()
        {
            _needsUpdate = false;
            TerrainCell[ , ] newTerrain = (TerrainCell[ , ])_terrain.Clone();
            for( int x = 0; x < Width; x++ )
            {
                for( int y = 0; y < Height - 1; y++ )
                {
                    if( _terrain[ x, y].Terrain != Tanks.TerrainCell.TerrainType.Air
                     && _terrain[ x, y + 1].Terrain == Tanks.TerrainCell.TerrainType.Air )
                    {
                        newTerrain[ x, y + 1 ] = _terrain[ x, y ];
                        newTerrain[ x, y ] = _terrain[ x, y + 1 ];
                        _needsUpdate = true;
                    }
                }
            }
            _terrain = newTerrain;
        }

        public void Paint( Graphics g, SolidBrush[] terrainBrushes )
        {
            if( terrainBrushes.Length != Enum.GetValues( typeof( Tanks.TerrainCell.TerrainType ) ).Length )
            {
                throw new ArgumentException( "The number of brushes must match the number of terrain types." );
            }
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            g.Clear( terrainBrushes[ (int)Tanks.TerrainCell.TerrainType.Air ].Color );
            for( int x = 0; x < Width; x++ )
            {
                for( int y = 0; y < Height; y++ )
                {
                    if( _terrain[ x, y ].Terrain != Tanks.TerrainCell.TerrainType.Air )
                    {
                        g.FillRectangle( terrainBrushes[ (int)_terrain[ x, y ].Terrain ], x * CellSize, y * CellSize, CellSize, CellSize );
                    }
                }
            }
        }

        private static IEnumerable<Point> GetPointsInCircle( Point circleCenter, int radius )
        {
            if( radius <= 0 )
            {
                throw new ArgumentOutOfRangeException( "radius", "Argument must be positive." );
            }

            // Loop bounds for X dimension:
            int i1 = -radius;
            int i2 = radius;

            // Constant square of the radius:
            int radius2 = radius * radius;

            for( int i = i1; i <= i2; i++ )
            {
                // X-coordinate for the points of the i-th circle segment:
                int x = circleCenter.X + i;

                // Local radius of the circle segment (half-length of chord) calulated in 3 steps.
                // Step 1. Offset of the (x, *) from the (circleCenter.x, *):
                int localRadius = circleCenter.X - x;
                // Step 2. Square of it:
                localRadius *= localRadius;
                // Step 3. Local radius of the circle segment:
                localRadius = (int)Math.Sqrt( radius2 - localRadius );

                // Loop bounds for Y dimension:
                int j1 = -localRadius;
                int j2 = localRadius;

                for( int j = j1; j <= j2; j++ )
                {
                    yield return new Point( (int)x, circleCenter.Y + j );
                }
            }
        }
    }
}
