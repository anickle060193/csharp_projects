using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesLibrary.Other
{
    public static class WordDictionary
    {
        private static readonly TrieTree _words = new TrieTree();
        static WordDictionary()
        {
            foreach( String word in Properties.Resources.TextTwistWords.Split( new String[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries ) )
            {
                _words.AddWord( word.Trim().ToUpper() );
            }
        }

        private static bool IsWord( String word )
        {
            return _words.Contains( word.Trim().ToUpper() );
        }

        public static TrieTree GetPossibleWords( char[] letters )
        {
            bool[] used = new bool[ letters.Length ];
            TrieTree words = new TrieTree();
            for( int i = 0; i< letters.Length; i++ )
            {
                GetWords( i, letters, new StringBuilder(), new bool[ letters.Length ], _words, words );
            }
            return words;
        }

        private static void GetWords( int index, char[] letters, StringBuilder sb, bool[] used, TrieTree completions, TrieTree words )
        {
            char current = letters[ index ];
            TrieTree currentCompletions = completions.GetCompletions( current.ToString() );
            if( currentCompletions == null )
            {
                return;
            }
            used[ index ] = true;
            sb.Append( current );
            if( currentCompletions.Contains( "" ) )
            {
                words.AddWord( sb.ToString() );
            }
            for( int i = 0; i < letters.Length; i++ )
            {
                if( !used[ i ] )
                {
                    GetWords( i, letters, sb, used, currentCompletions, words );
                }
            }
            sb.Length--;
            used[ index ] = false;
        }
    }
}
