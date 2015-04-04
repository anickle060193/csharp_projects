using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesLibrary.Other
{
    public class TrieTree
    {
        bool _hasEmptyString = false;
        private Dictionary<char, TrieTree> _words = new Dictionary<char, TrieTree>();

        public bool Contains( String word )
        {
            if( word == "" )
            {
                return _hasEmptyString;
            }
            else if( word[ 0 ] < 'A' || 'Z' < word[ 0 ] )
            {
                throw new ArgumentException( "Invalid word character: " + word[ 0 ] );
            }
            else if( _words.ContainsKey( word[ 0 ] ) )
            {
                return _words[ word[ 0 ] ].Contains( word.Substring( 1 ) );
            }
            else
            {
                return false;
            }
        }

        public void AddWord( String word )
        {
            if( word == "" )
            {
                _hasEmptyString = true;
            }
            else if( word[ 0 ] < 'A' || 'Z' < word[ 0 ] )
            {
                throw new ArgumentException( "Invalid word character: " + word[ 0 ] );
            }
            else
            {
                if( !_words.ContainsKey( word[0] ) )
                {
                    _words.Add( word[ 0 ], new TrieTree() );
                }
                _words[ word[ 0 ] ].AddWord( word.Substring( 1 ) );
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
                throw new ArgumentException( "Invalid word character: " + word[ 0 ] );
            }
            else
            {
                if( _words.ContainsKey( word[0]))
                {
                    return _words[ word[ 0 ] ].RemoveWord( word.Substring( 1 ) );
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
            else if( !_words.ContainsKey( prefix[ 0 ] ) )
            {
                return null;
            }
            else
            {
                return _words[ prefix[ 0 ] ].GetCompletions( prefix.Substring( 1 ) );
            }
        }

        public void GetAll( StringBuilder prefix, List<String> words )
        {
            if( _hasEmptyString )
            {
                words.Add( prefix.ToString() );
            }
            foreach( char c in _words.Keys )
            {
                prefix.Append( c );
                _words[ c ].GetAll( prefix, words );
                prefix.Length--;
            }
        }
    }
}
