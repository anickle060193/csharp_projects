using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGamesLibrary.Games
{
    public partial class ChainReactionForm : GameForm
    {
        private const int Explosions = 100;
        private const int MaximumExplosions = 1000;
        private const int SizeWidth = 1800;
        private const int SizeHeight = 800;

        private static readonly String ExplosionsKey = "explosions";

        private readonly SettingsForm _settingsForm = new SettingsForm();
        private readonly List<Explosion> _explosions = new List<Explosion>();
        private bool _mouseDown;

        public override string GameName { get { return "Chain Reaction"; } }

        public ChainReactionForm()
        {
            InitializeComponent();

            this.ClientSize = new Size( SizeWidth, SizeHeight );

            _settingsForm.AddIntegerSetting( ExplosionsKey, "Number of explosions:", Explosions, delegate( object value )
            {
                int explosions = (int)value;
                return 0 <= explosions && explosions <= MaximumExplosions;
            } );
        }

        protected override void OnGameStarted( EventArgs e )
        {
            _explosions.Clear();
            for( int i = 0; i < Explosions; i++ )
            {
                _explosions.Add( CreateRandomExplosion() );
            }

            uxUpdateTimer.Start();
        }

        private Explosion CreateRandomExplosion()
        {
            int x = Utilities.R.Next( this.ClientSize.Width );
            int y = Utilities.R.Next( this.ClientSize.Height );
            return new Explosion( x, y );
        }

        private void uxUpdateTimer_Tick( object sender, EventArgs e )
        {
            _explosions.ForEach( ex => ex.Update() );

            for( int i = 0; i < _explosions.Count; i++)
            {
                for( int j = 0; j < _explosions.Count; j++ )
                {
                    _explosions[ i ].PropogateExplosion( _explosions[ j ] );
                }
            }

            _explosions.RemoveAll( ex => ex.CurrentState == Explosion.State.Gone );
            Invalidate();
        }

        private void ChainReactionForm_MouseClick( object sender, MouseEventArgs e )
        {
            Explosion ex = Explosion.CreateClickExplosion( e.X, e.Y );
            _explosions.Add( ex );
            ex.Explode();
            Invalidate();
        }

        private void ChainReactionForm_Paint( object sender, PaintEventArgs e )
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for( int i = 0; i < _explosions.Count; i++ )
            {
                _explosions[ i ].Paint( e.Graphics );
            }
        }

        private void ChainReactionForm_MouseDown( object sender, MouseEventArgs e )
        {
            _mouseDown = true;
        }

        private void ChainReactionForm_MouseMove( object sender, MouseEventArgs e )
        {
            if( _mouseDown )
            {
                ChainReactionForm_MouseClick( sender, e );
            }
        }

        private void ChainReactionForm_MouseUp( object sender, MouseEventArgs e )
        {
            _mouseDown = false;
        }

        private void ChainReactionForm_KeyPress( object sender, KeyPressEventArgs e )
        {
            if( e.KeyChar == ' ' )
            {
                if( _settingsForm.ShowDialog() == DialogResult.OK )
                {
                    int newExplosionCount = _settingsForm.GetIntegerSetting( ExplosionsKey );
                    if( newExplosionCount > _explosions.Count )
                    {
                        while( newExplosionCount > _explosions.Count)
                        {
                            _explosions.RemoveAt( 0 );
                        }
                    }
                    else
                    {
                        while( newExplosionCount < _explosions.Count)
                        {
                            _explosions.Add( CreateRandomExplosion() );
                        }
                    }
                }
            }
        }
    }

    class Explosion
    {
        private const int MillisecondsPerSecond = 1000;
        private const int MinimumInitialRadius = 20;
        private const int MaximumInitialRadius = 50;
        private const int MinimumMaxRadius = 100;
        private const int MaximumMaxRadius = 300;
        private const float MaximumExplosionRate = 20;
        private const float MinimumExplosionRate = 10;
        private const int UpdateDelay = 20;
        private const int DisappearDelay = 2 * MillisecondsPerSecond;
        private const int TransitionDelay = 20;
        private const int TransitionStep = 10;

        public enum State { Primed, Exploding, FinishedExploding, Leaving, Gone }

        private DateTime _lastUpdate;
        private int _alpha;

        public Color Color { get; private set; }
        public float Radius { get; private set; }
        public float MaxRadius { get; private set; }
        public float ExplosionRate { get; private set; }
        public Point Location { get; private set; }
        public State CurrentState { get; private set; }

        public Explosion( int x, int y )
        {
            CurrentState = State.Primed;
            _alpha = 255;
            Location = new Point( x, y );
            Radius = (float)Utilities.NextDouble( MinimumInitialRadius, MaximumInitialRadius );
            MaxRadius = (float)Utilities.NextDouble( MinimumMaxRadius, MaximumMaxRadius );
            ExplosionRate = (float)Utilities.NextDouble( MinimumExplosionRate, MaximumExplosionRate );
            Color = Utilities.RandomColor();
        }

        public static Explosion CreateClickExplosion( int x, int y )
        {
            Explosion ex = new Explosion( x, y );
            ex.Radius = 20;
            ex.MaxRadius = 100;
            ex.ExplosionRate = 15;
            return ex;
        }

        public void Update()
        {
            DateTime now = DateTime.Now;
            int millisecondsElapsed = (int)( now - _lastUpdate ).TotalMilliseconds;
            if( CurrentState == State.Exploding )
            {
                if( millisecondsElapsed >= UpdateDelay )
                {
                    Radius += ExplosionRate * ( millisecondsElapsed / 100.0f );
                    if( Radius >= MaxRadius )
                    {
                        Radius = MaxRadius;
                        CurrentState = State.FinishedExploding;
                    }
                    _lastUpdate = now;
                }
            }
            else if( CurrentState == State.FinishedExploding )
            {
                if( millisecondsElapsed >= DisappearDelay )
                {
                    CurrentState = State.Leaving;
                }
            }
            else if( CurrentState == State.Leaving )
            {
                if( millisecondsElapsed >= TransitionDelay )
                {
                    _alpha -= TransitionStep;
                    if( _alpha <= 0 )
                    {
                        CurrentState = State.Gone;
                    }
                }
            }
        }

        private static bool ExplosionsIntersect( Explosion ex1, Explosion ex2 )
        {
            int xSqr = ex1.Location.X - ex2.Location.X;
            xSqr *= xSqr;
            int ySqr = ex1.Location.Y - ex2.Location.Y;
            ySqr *= ySqr;
            int distSqr = xSqr + ySqr;
            float radiusSqr = ex1.Radius + ex2.Radius;
            radiusSqr *= radiusSqr;
            return distSqr <= radiusSqr;
        }

        public void PropogateExplosion( Explosion other )
        {
            if( this != other
             && ( this.CurrentState == State.Exploding
               || this.CurrentState == State.FinishedExploding )
             && other.CurrentState == State.Primed
             && ExplosionsIntersect( this, other ) )
            {
                other.Explode();
            }
        }

        public bool Explode()
        {
            if( CurrentState == State.Primed )
            {
                CurrentState = State.Exploding;
                _lastUpdate = DateTime.Now;
                return true;
            }
            return false;
        }

        public bool Intersects( Point v )
        {
            return Utilities.Distance( v, Location ) <= Radius;
        }

        public void Paint( Graphics g )
        {
            if( CurrentState != State.Gone )
            {
                using( SolidBrush brush = new SolidBrush( Color.FromArgb( _alpha, Color ) ) )
                {
                    g.FillEllipse( brush, Location.X - Radius, Location.Y - Radius, Radius * 2, Radius * 2 );
                }
            }
        }
    }
}
