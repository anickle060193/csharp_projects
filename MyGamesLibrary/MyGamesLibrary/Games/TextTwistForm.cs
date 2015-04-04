using MyGamesLibrary.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGamesLibrary.Games
{
    public partial class TextTwistForm : GameForm
    {
        private const int Letters = 6;

        private static readonly int[] LetterValues = new int[]
        {
            /* A */ 1,
            /* B */ 3,
            /* C */ 3,
            /* D */ 2,
            /* E */ 1,
            /* F */ 4,
            /* G */ 2,
            /* H */ 4,
            /* I */ 1,
            /* J */ 8,
            /* K */ 5,
            /* L */ 1,
            /* M */ 3,
            /* N */ 1,
            /* O */ 1,
            /* P */ 3,
            /* Q */ 10,
            /* R */ 1,
            /* S */ 1,
            /* T */ 1,
            /* U */ 1,
            /* V */ 4,
            /* W */ 4,
            /* Y */ 4,
            /* X */ 8,
            /* Z */ 10
        };

        private TextBox[] _letterBoxes;
        private TextBox[] _wordBoxes;
        private char[] _letters = new char[ Letters ];
        private char[] _word = new char[ Letters ];
        private bool[] _used = new bool[ Letters ];
        private int _enteredLetters;
        private TrieTree _possibleWords;
        private int _score;
        private TrieTree _foundWords = new TrieTree();

        public override string GameName { get { return "Text Twist"; } }

        public TextTwistForm()
        {
            InitializeComponent();
            _letterBoxes = new TextBox[]
            {
                uxLetter1, uxLetter2, uxLetter3, uxLetter4, uxLetter5, uxLetter6
            };
            _wordBoxes = new TextBox[]
            {
                uxWordLetter1, uxWordLetter2, uxWordLetter3, uxWordLetter4, uxWordLetter5, uxWordLetter6
            };
        }

        protected override void OnGameStarted( EventArgs e )
        {
            InitializeBoard();
            uxWordList.Focus();
        }

        private void InitializeBoard()
        {
            foreach( TextBox textbox in _letterBoxes )
            {
                textbox.Text = "";
            }
            foreach( TextBox textbox in _wordBoxes )
            {
                textbox.Text = "";
            }
        }

        private void GenerateRandomCharacters()
        {
            String word = WordDictionary.GetRandomMaxLengthWord();
            for( int i = 0; i < word.Length && i < _letters.Length; i++ )
            {
                _letters[ i ] = word[ i ];
            }
            Utilities.Randomize( _letters );
            _possibleWords = WordDictionary.GetPossibleWords( _letters );

            UpdateGUI();
        }

        private bool LetterEntered( char c )
        {
            int index = -1;
            for( int i = 0; i < _letters.Length; i++ )
            {
                if( !_used[ i ] && _letters[ i ] != ' ' && _letters[ i ] == c )
                {
                    index = i;
                    break;
                }
            }
            if( index != -1 )
            {
                _word[ _enteredLetters ] = _letters[ index ];
                _letters[ index ] = ' ';
                _enteredLetters++;
                _used[ index ] = true;
                return true;
            }
            return false;
        }
        
        private void TextTwistForm_KeyPress( object sender, KeyPressEventArgs e )
        {
            char enteredLetter = e.KeyChar.ToString().ToUpper()[0];
            if( !LetterEntered( enteredLetter ) )
            {
                switch( e.KeyChar )
                {
                    case (char)Keys.Back:
                        BackspaceWord();
                        break;

                    case (char)Keys.Enter:
                        StringBuilder sb = new StringBuilder();
                        for( int i = 0; i < _enteredLetters; i++ )
                        {
                            sb.Append( _word[ i ] );
                        }
                        String word = sb.ToString();
                        if( CheckWord( word ) )
                        {
                            uxWordList.Items.Add( word );
                            _foundWords.AddWord( word );
                            foreach( char c in word )
                            {
                                _score += LetterValues[ (int)( c - 'A' ) ];
                            }
                            MessageBox.Show( "That's a word" );
                        }
                        else
                        {
                            MessageBox.Show( "Not a word" );
                        }
                        while( _enteredLetters > 0 )
                        {
                            BackspaceWord();
                        }
                    break;
                }
            }
            UpdateGUI();
        }

        private void BackspaceWord()
        {
            if( _enteredLetters > 0 )
            {
                _enteredLetters--;
                for( int i = 0; i < Letters; i++ )
                {
                    if( _used[ i ] )
                    {
                        _used[ i ] = false;
                        _letters[ i ] = _word[ _enteredLetters ];
                        _word[ _enteredLetters ] = ' ';
                        break;
                    }
                }
            }
        }

        private bool CheckWord( String word )
        {
            return _possibleWords != null && !_foundWords.Contains( word ) && _possibleWords.Contains( word );
        }

        private void UpdateGUI()
        {
            for( int i = 0; i < Letters; i++ )
            {
                _letterBoxes[ i ].Text = _letters[ i ].ToString();
                _wordBoxes[ i ].Text = _word[ i ].ToString();
            }
            uxScore.Text = _score.ToString();
        }

        private void uxNewGame_Click( object sender, EventArgs e )
        {
            GenerateRandomCharacters();
        }
    }
}
