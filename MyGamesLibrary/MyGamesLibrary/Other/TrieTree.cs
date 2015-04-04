using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesLibrary.Other
{
    public class TrieTree
    {
        private const int AlphabetLength = 26;

        bool _hasEmptyString = false;
        private Dictionary<char, TrieTree> _characterMap = new Dictionary<char, TrieTree>();
        private int[][] _lengthByLetter = new int[ AlphabetLength ][];

        public bool ErrorOnInvalidWord { get; set; }

        public TrieTree( bool errorOnInvalidWord )
        {
            ErrorOnInvalidWord = errorOnInvalidWord;
        }

        public TrieTree() : this( true ) { }

        public bool Contains( String word )
        {
            if( word == "" )
            {
                return _hasEmptyString;
            }
            else if( word[ 0 ] < 'A' || 'Z' < word[ 0 ] )
            {
                return false;
            }
            else if( _characterMap.ContainsKey( word[ 0 ] ) )
            {
                return _characterMap[ word[ 0 ] ].Contains( word.Substring( 1 ) );
            }
            else
            {
                return false;
            }
        }

        public bool AddWord( String word )
        {
            if( word == "" )
            {
                if( !_hasEmptyString )
                {
                    _hasEmptyString = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if( word[ 0 ] < 'A' || 'Z' < word[ 0 ] )
            {
                if( ErrorOnInvalidWord )
                {
                    throw new ArgumentException( "Invalid word character: " + word[ 0 ] );
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if( !_characterMap.ContainsKey( word[0] ) )
                {
                    _characterMap.Add( word[ 0 ], new TrieTree( ErrorOnInvalidWord ) );
                }
                return _characterMap[ word[ 0 ] ].AddWord( word.Substring( 1 ) );
            }
        }

        public bool RemoveWord( String word )
        {
            if( word == "" )
            {
                if( _hasEmptyString )
                {
                    _hasEmptyString = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if( word[ 0 ] < 'A' || 'Z' < word[ 0 ] )
            {
                if( ErrorOnInvalidWord )
                {
                    throw new ArgumentException( "Invalid word character: " + word[ 0 ] );
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if( _characterMap.ContainsKey( word[0]))
                {
                    return _characterMap[ word[ 0 ] ].RemoveWord( word.Substring( 1 ) );
                }
                else
                {
                    return false;
                }
            }
        }

        public TrieTree GetCompletions( String prefix )
        {
            if( prefix == "" )
            {
                return this;
            }
            else if( prefix[ 0 ] < 'A' || 'Z' < prefix[ 0 ] )
            {
                if( ErrorOnInvalidWord )
                {
                    throw new ArgumentException( "Invalid prefix character: " + prefix[ 0 ] );
                }
                else
                {
                    return null;
                }
            }
            else if( !_characterMap.ContainsKey( prefix[ 0 ] ) )
            {
                return null;
            }
            else
            {
                return _characterMap[ prefix[ 0 ] ].GetCompletions( prefix.Substring( 1 ) );
            }
        }

        public void GetAll( StringBuilder prefix, IList words )
        {
            if( _hasEmptyString )
            {
                words.Add( prefix.ToString() );
            }
            foreach( char c in _characterMap.Keys )
            {
                prefix.Append( c );
                _characterMap[ c ].GetAll( prefix, words );
                prefix.Length--;
            }
        }

        public bool GetRandomChild( out char character, out TrieTree child )
        {
            if( _characterMap.Count == 0 )
            {
                character = (char)0;
                child = null;
                return false;
            }
            else
            {
                character = _characterMap.Keys.ElementAt( Utilities.R.Next( _characterMap.Count ) );
                child = _characterMap[ character ];
                return true;
            }
        }
    }
}
