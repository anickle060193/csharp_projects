using AdamNickle.Settings;
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
        private const int SizeWidth = 1200;
        private const int SizeHeight = 500;

        private static readonly String ExplosionsKey = "explosions";

        private readonly SettingsForm _settingsForm = new SettingsForm();
        private readonly List<Explosion> _explosions = new List<Explosion>();
        private bool _mouseDown;

        public override string GameName { get { return "Chain Reaction"; } }

        public ChainReactionForm()
        {
            InitializeComponent();

            this.ClientSize = new Size( SizeWidth, SizeHeight );

            _settingsForm.AddIntegerSetting( ExplosionsKey, "Number of explosions:", Explosions, delegate( int explosions )
            {
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
            _explosions.ForEach( ex => ex.Update( this.ClientSize.Width, this.ClientSize.Height ) );

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
                        while( newExplosionCount > _explosions.Count )
                        {
                            _explosions.Add( CreateRandomExplosion() );
                        }
                    }
                    else if( newExplosionCount < _explosions.Count )
                    {
                        while( newExplosionCount < _explosions.Count )
                        {
                            _explosions.RemoveAt( 0 );
                        }
                    }
                    Invalidate();
                }
            }
        }
    }

    class Explosion
    {
        private const int MillisecondsPerSecond = 1000;
        private const int MinimumInitialRadius = 10;
        private const int MaximumInitialRadius = 20;
        private const int MinimumMaxRadius = 40;
        private const int MaximumMaxRadius = 50;
        private const float MaximumExplosionRate = 0.20f;
        private const float MinimumExplosionRate = 0.10f;
        private const int UpdateDelay = 20;
        private const int DisappearDelay = 2 * MillisecondsPerSecond;
        private const int TransitionDelay = 20;
        private const int TransitionStep = 10;
        private const int InitialAlphaValue = 200;
        private const float MaxVelocity = 0.1f;
        private const float MinVelocity = -MaxVelocity;

        public enum State { Primed, Exploding, FinishedExploding, Leaving, Gone }

        private DateTime _lastUpdate;
        private int _alpha;
        private float _xVelocity;
        private float _yVelocity;

        public Color Color { get; private set; }
        public float Radius { get; private set; }
        public float MaxRadius { get; private set; }
        public float ExplosionRate { get; private set; }
        public PointF Location { get; private set; }
        public State CurrentState { get; private set; }

        public Explosion( int x, int y )
        {
            _lastUpdate = DateTime.Now;
            CurrentState = State.Primed;
            _alpha = InitialAlphaValue;
            Location = new Point( x, y );
            Radius = (float)Utilities.NextDouble( MinimumInitialRadius, MaximumInitialRadius );
            MaxRadius = (float)Utilities.NextDouble( MinimumMaxRadius, MaximumMaxRadius );
            ExplosionRate = (float)Utilities.NextDouble( MinimumExplosionRate, MaximumExplosionRate );
            Color = Utilities.RandomColor();
            _xVelocity = (float)Utilities.NextDouble( MinVelocity, MaxVelocity );
            _yVelocity = (float)Utilities.NextDouble( MinVelocity, MaxVelocity );
        }

        public static Explosion CreateClickExplosion( int x, int y )
        {
            return new Explosion( x, y );
        }

        public void Update( float width, float height )
        {
            DateTime now = DateTime.Now;
            int millisecondsElapsed = (int)( now - _lastUpdate ).TotalMilliseconds;
            if( CurrentState == State.Primed )
            {
                if( millisecondsElapsed >= UpdateDelay )
                {
                    float deltaX = _xVelocity * millisecondsElapsed;
                    float deltaY = _yVelocity * millisecondsElapsed;
                    float x = Location.X + deltaX;
                    float y = Location.Y + deltaY;
                    if( x - Radius < 0 )
                    {
                        _xVelocity = -_xVelocity;
                        x = Radius;
                    }
                    else if( x + Radius >= width )
                    {
                        _xVelocity = -_xVelocity;

                        x = width - Radius - 1;
                    }
                    if( y - Radius < 0 )
                    {
                        _yVelocity = -_yVelocity;
                        y = Radius;
                    }
                    else if( y + Radius >= height )
                    {
                        _yVelocity = -_yVelocity;
                        y = height - Radius - 1;
                    }
                    Location = new PointF( x, y );
                    _lastUpdate = now;
                }
            }
            else if( CurrentState == State.Exploding )
            {
                if( millisecondsElapsed >= UpdateDelay )
                {
                    Radius += ExplosionRate * millisecondsElapsed;
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
                    _lastUpdate = now;
                }
            }
            else if( CurrentState == State.Leaving )
            {
                if( millisecondsElapsed >= UpdateDelay )
                {
                    Radius -= ExplosionRate * millisecondsElapsed;
                    _alpha = (int)( Radius / MaxRadius * 255 );
                    if( Radius < 0 )
                    {
                        Radius = 0;
                        CurrentState = State.Gone;
                    }
                    _lastUpdate = now;
                }
            }
        }

        private static bool ExplosionsIntersect( Explosion ex1, Explosion ex2 )
        {
            float xSqr = ex1.Location.X - ex2.Location.X;
            xSqr *= xSqr;
            float ySqr = ex1.Location.Y - ex2.Location.Y;
            ySqr *= ySqr;
            float distSqr = xSqr + ySqr;
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
