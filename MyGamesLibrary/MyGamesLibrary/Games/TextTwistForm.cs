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

        private TextBox[] _letterBoxes;
        private TextBox[] _wordBoxes;
        private char[] _letters = new char[ Letters ];
        private char[] _word = new char[ Letters ];
        private bool[] _used = new bool[ Letters ];
        private int _enteredLetters;
        private TrieTree _possibleWords;

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
            uxWordsList.Focus();
        }

        private void InitializeBoard()
        {
            GenerateRandomCharacters();
        }

        private void GenerateRandomCharacters()
        {
            for( int i = 0; i < _letters.Length; i++ )
            {
                _letters[ i ] = (char)Utilities.R.Next( (int)'A', (int)'Z' + 1 );
                _letterBoxes[ i ].Text = _letters[ i ].ToString();
            }
            _possibleWords = WordDictionary.GetPossibleWords( _letters );
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

        private void TextTwist_KeyPress( object sender, KeyPressEventArgs e )
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
                        if( CheckWord( sb.ToString() ) )
                        {
                            MessageBox.Show( "That's a word" );
                            while( _enteredLetters > 0 )
                            {
                                BackspaceWord();
                            }
                        }
                        else
                        {
                            MessageBox.Show( "Not a word" );
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
            return _possibleWords != null && _possibleWords.Contains( word );
        }

        private void UpdateGUI()
        {
            for( int i = 0; i < Letters; i++ )
            {
                _letterBoxes[ i ].Text = _letters[ i ].ToString();
                _wordBoxes[ i ].Text = _word[ i ].ToString();
            }
        }
    }
}
