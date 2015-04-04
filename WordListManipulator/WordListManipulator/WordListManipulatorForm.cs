using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordListManipulator
{
    public partial class WordListManipulatorForm : Form
    {
        enum CapitalizationOption { LowerCase, UpperCase }

        public WordListManipulatorForm()
        {
            InitializeComponent();
        }

        private void uxBrowse_Click( object sender, EventArgs e )
        {
            if( uxOpenFileDialog.ShowDialog() == DialogResult.OK )
            {
                uxFilename.Text = uxOpenFileDialog.FileName;
            }
        }

        private void uxProcess_Click( object sender, EventArgs e )
        {
            String filename = uxFilename.Text;
            if( File.Exists( filename ) )
            {
                if( uxSaveFileDialog.ShowDialog() == DialogResult.OK )
                {
                    WriteWordList( filename, uxSaveFileDialog.FileName );
                }
            }
            else
            {
                MessageBox.Show( "File does not exist:\n" + filename );
            }
        }

        private void WriteWordList( string inputFilename, string outputFilename )
        {
            int minWord = (int)uxMinimumLength.Value;
            int maxWord = (int)uxMaximumLength.Value;
            if( minWord > maxWord )
            {
                MessageBox.Show( "Minimum word length must be less than maximum word length" );
                return;
            }
            CapitalizationOption capitalizationOption;
            if( uxAllUpperCase.Checked )
            {
                capitalizationOption = CapitalizationOption.UpperCase;
            }
            else
            {
                capitalizationOption = CapitalizationOption.LowerCase;
            }
            using( StreamReader input = new StreamReader( inputFilename ) )
            {
                using( StreamWriter output = new StreamWriter( outputFilename ) )
                {
                    while( !input.EndOfStream )
                    {
                        string word = input.ReadLine().Trim();
                        if( word.Length < minWord || maxWord < word.Length )
                        {
                            continue;
                        }
                        if( word.Any( c => !Char.IsLetter( c ) ) )
                        {
                            continue;
                        }
                        if( capitalizationOption == CapitalizationOption.LowerCase )
                        {
                            word = word.ToLower();
                        }
                        else if( capitalizationOption == CapitalizationOption.UpperCase )
                        {
                            word = word.ToUpper();
                        }
                        output.WriteLine( word );
                    }
                }
            }
            MessageBox.Show( "Dictionary has been written." );
        }
    }
}
