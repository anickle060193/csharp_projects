using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class RuleSet
    {
        public static readonly RuleSet Normal = new RuleSet( "Normal", new int[] { 2, 3 }, new int[] { 3 } );
        public static readonly RuleSet HighLife = new RuleSet( "HighLife", new int[] { 2, 3 }, new int[] { 3, 6 } );
        public static readonly RuleSet Sierpinski = new RuleSet( "Sierpinski", new int[] { 1, 2 }, new int[] { 1 } );
        public static readonly RuleSet[] RuleSets = new RuleSet[] { Normal, HighLife, Sierpinski };

        public String Name { get; private set; }
        public int[] RequiredToLive { get; private set; }
        public int[] RequiredForBirth { get; private set; }

        public RuleSet( String name, int[] requiredToLive, int[] requiredForBirth )
        {
            Name = name;
            RequiredToLive = requiredToLive;
            RequiredForBirth = requiredForBirth;
        }

        public GameOfLifeBoard.State GetNextState( int neighbors, GameOfLifeBoard.State currentState, bool showTransition )
        {
            if( currentState == GameOfLifeBoard.State.Alive )
            {
                if( !RequiredToLive.Contains( neighbors ) )
                {
                    return showTransition ? GameOfLifeBoard.State.Dying : GameOfLifeBoard.State.Dead;
                }
            }
            else if( currentState == GameOfLifeBoard.State.Dead )
            {
                if( RequiredForBirth.Contains( neighbors ) )
                {
                    return showTransition ? GameOfLifeBoard.State.ComingBack : GameOfLifeBoard.State.Alive;
                }
            }
            return currentState;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
