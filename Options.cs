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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(Enum.GetNames(typeof(MyColors)));
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            comboBox2.Items.AddRange(Enum.GetNames(typeof(MyColors)));
            comboBox2.SelectedIndexChanged += new EventHandler(comboBox2_SelectedIndexChanged);
            comboBox3.Items.AddRange(Enum.GetNames(typeof(MyColors)));
            comboBox3.SelectedIndexChanged += new EventHandler(comboBox3_SelectedIndexChanged);
            comboBox4.Items.AddRange(Enum.GetNames(typeof(MyColors)));
            comboBox4.SelectedIndexChanged += new EventHandler(comboBox4_SelectedIndexChanged);
        }

        enum MyColors
        {
            red,
            orange,
            yellow,
            green,
            blue,
            purple,
            black,
            white
        }

        string newBackColor = "red", newForeColor = "black";
        string newPlayer1Checker = "red", newPlayer2Checker = "green";

        //Depending on what is selected in the drop down menu(case), it will display that color
        //in the picturebox and save the string to the newBackColor variable that will be used to save
        //the color option for the Background of the board in the Board class
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "":
                    {
                        this.pictureBox2.BackColor = Color.Red;
                        newBackColor = null;
                        break;
                    }
                case "red":
                    {
                        this.pictureBox1.BackColor = Color.Red;
                        newBackColor = "red";
                        break;
                    }
                case "orange":
                    {
                        this.pictureBox1.BackColor = Color.Orange;
                        newBackColor = "orange";
                        break;
                    }
                case "yellow":
                    {
                        this.pictureBox1.BackColor = Color.Yellow;
                        newBackColor = "yellow";
                        break;
                    }
                case "green":
                    {
                        this.pictureBox1.BackColor = Color.Green;
                        newBackColor = "green";
                        break;
                    }
                case "blue":
                    {
                        this.pictureBox1.BackColor = Color.Blue;
                        newBackColor = "blue";
                        break;
                    }
                case "purple":
                    {
                        this.pictureBox1.BackColor = Color.Purple;
                        newBackColor = "purple";
                        break;
                    }
                case "black":
                    {
                        this.pictureBox1.BackColor = Color.Black;
                        newBackColor = "black";
                        break;
                    }
                case "white":
                    {
                        this.pictureBox1.BackColor = Color.White;
                        newBackColor = "white";
                        break;
                    }
            }
            //check if background and foreground are different colors, otherwise display error message
            if (String.Compare(newBackColor, newForeColor) == 0)
            {
                MessageBox.Show("The Background and Foreground cannot be the same color.");
            }
        }
        public string getBackC()
        {
            return newBackColor;
        }
        //Depending on what is selected in the drop down menu(case), it will display that color
        //in the picturebox and save the string to the newForeColor variable that will be used to save
        //the color option for the Foreground of the board in the Board class
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedItem.ToString())
            {
                case "":
                    {
                        this.pictureBox2.BackColor = Color.Black;
                        newForeColor = null;
                        break;
                    }
                case "red":
                    {
                        this.pictureBox2.BackColor = Color.Red;
                        newForeColor = "red";
                        break;
                    }
                case "orange":
                    {
                        this.pictureBox2.BackColor = Color.Orange;
                        newForeColor = "orange";
                        break;
                    }
                case "yellow":
                    {
                        this.pictureBox2.BackColor = Color.Yellow;
                        newForeColor = "yellow";
                        break;
                    }
                case "green":
                    {
                        this.pictureBox2.BackColor = Color.Green;
                        newForeColor = "green";
                        break;
                    }
                case "blue":
                    {
                        this.pictureBox2.BackColor = Color.Blue;
                        newForeColor = "blue";
                        break;
                    }
                case "purple":
                    {
                        this.pictureBox2.BackColor = Color.Purple;
                        newForeColor = "purple";
                        break;
                    }
                case "black":
                    {
                        this.pictureBox2.BackColor = Color.Black;
                        newForeColor = "black";
                        break;
                    }
                case "white":
                    {
                        this.pictureBox2.BackColor = Color.White;
                        newForeColor = "white";
                        break;
                    }
            }
            //check if the background and foreground will be different colors, or display error message
            if (String.Compare(newBackColor, newForeColor) == 0)
            {
                MessageBox.Show("The Background and Foreground cannot not be the same color.");
            }
        }

        //When user clicks Apply button, their setting will be saved and the Options form will close.
        private void button1_Click(object sender, EventArgs e)
        {
            //If they dont select a color for either fore or back ground, close form
            if (newBackColor == null && newForeColor == null)
            {
                this.Hide();
            }
            //if they dont choose a background color, change foreground and close form
            else if (newBackColor == null)
            {
                Form1.getBoardName().setForeColor(newForeColor);
                this.Hide();
            }
            //if they dont choose a foreground color, change background and close form
            else if (newForeColor == null)
            {
                Form1.getBoardName().setBackColor(newBackColor);
                this.Hide();
            }
            //change both background and foreground colors and close form
            else
            {
                Form1.getBoardName().setBackColor(newBackColor);
                Form1.getBoardName().setForeColor(newForeColor);
                this.Hide();
            }
        }
        //Player 1 piece color
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedItem.ToString())
            {
                case "":
                    {
                        this.pictureBox3.Image = Properties.Resources.checkerRed;
                        break;
                    }
                case "red":
                    {
                        Form1.getBoardName().setPlayer1Checker(Properties.Resources.checkerRed, Properties.Resources.checkerRedKing);
                        this.pictureBox3.Image = Properties.Resources.checkerRed;
                        newPlayer1Checker = "red";
                        break;
                    }
                case "orange":
                    {
                        Form1.getBoardName().setPlayer1Checker(Properties.Resources.checkerOrange, Properties.Resources.checkerOrangeKing);
                        this.pictureBox3.Image = Properties.Resources.checkerOrange;
                        newPlayer1Checker = "orange";
                        break;
                    }
                case "yellow":
                    {
                        Form1.getBoardName().setPlayer1Checker(Properties.Resources.checkerYellow, Properties.Resources.checkerYellowKing);
                        this.pictureBox3.Image = Properties.Resources.checkerYellow;
                        newPlayer1Checker = "yellow";
                        break;
                    }
                case "green":
                    {
                        Form1.getBoardName().setPlayer1Checker(Properties.Resources.checkerGreen, Properties.Resources.checkerGreenKing);
                        this.pictureBox3.Image = Properties.Resources.checkerGreen;
                        newPlayer1Checker = "green";
                        break;
                    }
                case "blue":
                    {
                        Form1.getBoardName().setPlayer1Checker(Properties.Resources.checkerBlue, Properties.Resources.checkerBlueKing);
                        this.pictureBox3.Image = Properties.Resources.checkerBlue;
                        newPlayer1Checker = "blue";
                        break;
                    }
                case "purple":
                    {
                        Form1.getBoardName().setPlayer1Checker(Properties.Resources.checkerPurple, Properties.Resources.checkerPurpleKing);
                        this.pictureBox3.Image = Properties.Resources.checkerPurple;
                        newPlayer1Checker = "purple";
                        break;
                    }
                case "black":
                    {
                        Form1.getBoardName().setPlayer1Checker(Properties.Resources.checkerBlack, Properties.Resources.checkerBlackKing);
                        this.pictureBox3.Image = Properties.Resources.checkerBlack;
                        newPlayer1Checker = "black";
                        break;
                    }
                case "white":
                    {
                        Form1.getBoardName().setPlayer1Checker(Properties.Resources.checkerWhite, Properties.Resources.checkerWhiteKing);
                        this.pictureBox3.Image = Properties.Resources.checkerWhite;
                        newPlayer1Checker = "white";
                        break;
                    }

            }
            //check that the pieces are not the same color as the squares they will sit on
            if (String.Compare(newForeColor, newPlayer1Checker) == 0)
            {
                MessageBox.Show("The Foreground and the checker pieces cannot not be the same color.");
            }
            //check that the pieces are different colors
            //not working yet
            if (String.Compare(newPlayer1Checker, newPlayer2Checker) == 0)
            {
                MessageBox.Show("Each player's checker pieces cannot not be the same color.");
            }
        }
        //player 2 piece color
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedItem.ToString())
            {
                case "":
                    {
                        this.pictureBox4.Image = Properties.Resources.checkerGreen;
                        break;
                    }
                case "red":
                    {
                        Form1.getBoardName().setPlayer2Checker(Properties.Resources.checkerRed, Properties.Resources.checkerRedKing);
                        this.pictureBox4.Image = Properties.Resources.checkerRed;
                        newPlayer2Checker = "red";
                        break;
                    }
                case "orange":
                    {
                        Form1.getBoardName().setPlayer2Checker(Properties.Resources.checkerOrange, Properties.Resources.checkerOrangeKing);
                        this.pictureBox4.Image = Properties.Resources.checkerOrange;
                        newPlayer2Checker = "orange";
                        break;
                    }
                case "yellow":
                    {
                        Form1.getBoardName().setPlayer2Checker(Properties.Resources.checkerYellow, Properties.Resources.checkerYellowKing);
                        this.pictureBox4.Image = Properties.Resources.checkerYellow;
                        newPlayer2Checker = "yellow";
                        break;
                    }
                case "green":
                    {
                        Form1.getBoardName().setPlayer2Checker(Properties.Resources.checkerGreen, Properties.Resources.checkerGreenKing);
                        this.pictureBox4.Image = Properties.Resources.checkerGreen;
                        newPlayer2Checker = "green";
                        break;
                    }
                case "blue":
                    {
                        Form1.getBoardName().setPlayer2Checker(Properties.Resources.checkerBlue, Properties.Resources.checkerBlueKing);
                        this.pictureBox4.Image = Properties.Resources.checkerBlue;
                        newPlayer2Checker = "blue";
                        break;
                    }
                case "purple":
                    {
                        Form1.getBoardName().setPlayer2Checker(Properties.Resources.checkerPurple, Properties.Resources.checkerPurpleKing);
                        this.pictureBox4.Image = Properties.Resources.checkerPurple;
                        newPlayer2Checker = "purple";
                        break;
                    }
                case "black":
                    {
                        Form1.getBoardName().setPlayer2Checker(Properties.Resources.checkerBlack, Properties.Resources.checkerBlackKing);
                        this.pictureBox4.Image = Properties.Resources.checkerBlack;
                        newPlayer2Checker = "black";
                        break;
                    }
                case "white":
                    {
                        Form1.getBoardName().setPlayer2Checker(Properties.Resources.checkerWhite, Properties.Resources.checkerWhiteKing);
                        this.pictureBox4.Image = Properties.Resources.checkerWhite;
                        newPlayer2Checker = "white";
                        break;
                    }
            }
            //check that the pieces are not the same color as the squares they will sit on.
            if (String.Compare(newForeColor, newPlayer2Checker) == 0)
            {
                MessageBox.Show("The Foreground and the checker pieces cannot not be the same color.");
            }
            //check that the pieces are not the same color
            //not working
            if (String.Compare(newPlayer1Checker, newPlayer2Checker) == 0)
            {
                MessageBox.Show("Each player's checker pieces cannot not be the same color.");
            }
        }
        //highlighting option on or off
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Form1.getBoardName().setHighlighting();
        }
        //Don't actually need this, it was generated and if i delete the function I'll get errors during compile time
        private void Options_Load_1(object sender, EventArgs e)
        {

        }
    }
}
