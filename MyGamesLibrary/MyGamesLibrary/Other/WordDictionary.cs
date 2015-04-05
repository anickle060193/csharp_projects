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
        private const int MinimumWordLength = 3;
        private const int MaximumWordLength = 6;

        private static readonly String[] WordLists = new String[]
        {
            Properties.Resources.AllWords,
            Properties.Resources.TextTwistWords,
            Properties.Resources.UnabrigedDictionary,
            Properties.Resources.UnixWords,
        };

        private static readonly TrieTree _words = new TrieTree( false );
        private static readonly TrieTree _maxLengthWords = new TrieTree( false );

        public static int Count { get { return _words.Count; } }

        static WordDictionary()
        {
            foreach( String wordList in WordLists )
            {
                foreach( String word in wordList.Split( new String[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries ) )
                {
                    string trimmed = word.Trim();
                    if( MinimumWordLength <= trimmed.Length && trimmed.Length <= MaximumWordLength )
                    {
                        _words.AddWord( trimmed.ToUpper() );
                    }
                    if( trimmed.Length == MaximumWordLength )
                    {
                        _maxLengthWords.AddWord( trimmed );
                    }
                }
            }
        }

        private static bool IsWord( String word )
        {
            return _words.Contains( word.Trim().ToUpper() );
        }

        public static String GetRandomMaxLengthWord()
        {
            StringBuilder sb = new StringBuilder();
            char character;
            TrieTree child = _maxLengthWords;
            do
            {
                if( !child.GetRandomChild( out character, out child ) )
                {
                    break;
                }
                else
                {
                    sb.Append( character );
                }
            }
            while( sb.Length < MaximumWordLength );
            return sb.ToString();
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
