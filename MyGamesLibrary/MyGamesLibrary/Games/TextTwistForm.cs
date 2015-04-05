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
        enum WordValidity { Valid, Invalid, None }

        private const int Letters = 6;
        private const int MillisecondsPerSecond = 1000;
        private const int SecondsPerMinutes = 60;
        private const int GameLengthMilliseconds = 1 * SecondsPerMinutes * MillisecondsPerSecond;
        private const int ClearAfterAttemptLengthMilliseconds = 2 * MillisecondsPerSecond;
        private const char ValidWordCharacter = '\u2713';
        private const char InvalidWordCharacter = '\u2717';

        private static readonly Color ValidWordColor = Color.Green;
        private static readonly Color InvalidWordColor = Color.Red;
        private static readonly SolidBrush[] WordValidityBrushes = new SolidBrush[]
        {
            new SolidBrush( Color.FromArgb( 100, Color.Green ) ),
            new SolidBrush( Color.FromArgb( 100, Color.Red   ) ),
            new SolidBrush( Color.FromArgb( 100, Color.White ) )
        };
        private static readonly char[] WordValidityCharacters = new char[] { ValidWordCharacter, InvalidWordCharacter, ' ' };
        private static readonly Font WordValidityFont = new Font( FontFamily.GenericSansSerif, 72 );
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
        private char[] _letters;
        private char[] _word;
        private bool[] _used;
        private int _enteredLetters;
        private TrieTree _possibleWords;
        private int _score;
        private int _maxScore;
        private TrieTree _foundWords;
        private DateTime _timeLimit;
        private bool _gameOver;
        private WordValidity _wordValidity;
        private DateTime _clearWordValidityTime;

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
        }

        private void InitializeBoard()
        {
            _letters = new char[ Letters ];
            _word = new char[ Letters ];
            _used = new bool[ Letters ];

            _wordValidity = WordValidity.None;
            _enteredLetters = 0;
            _gameOver = true;
            _score = 0;
            _maxScore = 0;

            uxTimeRemaining.Text = "00:00.00";
            uxScore.Text = "0";

            uxWordList.Items.Clear();
            _possibleWords = null;
            _foundWords = new TrieTree( false );

            UpdateGUI();
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
            List<String> words = new List<string>();
            _possibleWords.GetAll( new StringBuilder(), words );
            foreach( String w in words )
            {
                _maxScore += GetWordScore( w );
            }
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
            if( _gameOver )
            {
                if( e.KeyChar == (char)Keys.Space )
                {
                    NewGame();
                }
            }
            else
            {
                char enteredLetter = e.KeyChar.ToString().ToUpper()[ 0 ];
                _wordValidity = WordValidity.None;
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
                                _score += GetWordScore( word );
                                while( _enteredLetters > 0 )
                                {
                                    BackspaceWord();
                                }
                                _wordValidity = WordValidity.Valid;
                            }
                            else
                            {
                                _wordValidity = WordValidity.Invalid;
                            }
                            _clearWordValidityTime = DateTime.Now.AddMilliseconds( ClearAfterAttemptLengthMilliseconds );
                            break;
                    }
                }
                UpdateGUI();
            }
        }

        private int GetWordScore( String word )
        {
            int score = 1;
            foreach( char c in word )
            {
                score *= LetterValues[ (int)( c - 'A' ) ] * 2;
            }
            return score;
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
            uxScore.Text = _score.ToString() + " / " + _maxScore.ToString();
            if( _possibleWords != null && _foundWords != null )
            {
                uxWordsRemaining.Text = ( _possibleWords.Count - _foundWords.Count ).ToString();
            }
            else
            {
                uxWordsRemaining.Text = "0";
            }
            uxReponseDisplay.Invalidate();
        }

        private void uxNewGame_Click( object sender, EventArgs e )
        {
            NewGame();
        }

        private void NewGame()
        {
            InitializeBoard();
            _gameOver = false;
            GenerateRandomCharacters();
            _timeLimit = DateTime.Now.AddMilliseconds( GameLengthMilliseconds );
            uxTimer.Start();
        }

        private void uxTimer_Tick( object sender, EventArgs e )
        {
            if( !_gameOver )
            {
                TimeSpan timeRemaining = _timeLimit - DateTime.Now;
                if( timeRemaining < TimeSpan.Zero )
                {
                    timeRemaining = TimeSpan.Zero;
                    _gameOver = true;
                }
                if( _wordValidity != WordValidity.None )
                {
                    if( _clearWordValidityTime < DateTime.Now )
                    {
                        _wordValidity = WordValidity.None;
                        UpdateGUI();
                    }
                }
                uxTimeRemaining.Text = timeRemaining.ToString( @"mm\:ss\.ff" );
                if( _gameOver )
                {
                    MessageBox.Show( "Game over!" );
                    uxTimer.Stop();
                }
            }
        }

        protected override void OnGameEnded( EventArgs e )
        {
            uxTimer.Stop();
        }

        private void uxReponseDisplay_Paint( object sender, PaintEventArgs e )
        {
            if( _wordValidity != WordValidity.None )
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                string s = WordValidityCharacters[ (int)_wordValidity ].ToString();
                SizeF textSize = e.Graphics.MeasureString( s, WordValidityFont );
                SizeF clientSize = uxReponseDisplay.ClientSize;
                PointF center = Utilities.CenterText( clientSize, textSize );
                e.Graphics.DrawString( s, WordValidityFont, WordValidityBrushes[ (int)_wordValidity ], center );
            }
        }
    }
}
