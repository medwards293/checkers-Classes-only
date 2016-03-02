using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public partial class NewGame : Form
    {
        public NewGame()
        {
            InitializeComponent();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        // AI radio button
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Hide();
        }

        // Player 2 radio button
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Show();
        }

        // Player 1 name
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        // player 2 name
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1.getBoardName().Show();
            Form1.getLeaderboardName().setPlayer1Name(textBox1.ToString());
            Form1.getLeaderboardName().setPlayer2Name(textBox2.ToString());
        }
    }
}
