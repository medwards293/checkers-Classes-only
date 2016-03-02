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

        string newBackColor, newForeColor;
        //Depending on what is selected in the drop down menu(case), it will display that color
        //in the picturebox and save the string to the newBackColor variable that will be used to save
        //the color option for the Background of the board in the Board class
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "":
                    {
                        this.pictureBox2.BackColor = Color.Transparent;
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
                        this.pictureBox2.BackColor = Color.Transparent;
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
        }
        //When user clicks Apply button, their setting will be saved and the Options form will close.
        private void button1_Click(object sender, EventArgs e)
        {
            //If they dont select a color for either fore or back ground, close form
            if (newBackColor == null && newForeColor == null)
            {
                this.Close();
            }
            //if they dont choose a background color, change foreground and close form
            else if (newBackColor == null)
            {
                Form1.getBoardName().setForeColor(newForeColor);
                this.Close();
            }
            //if they dont choose a foreground color, change background and close form
            else if (newForeColor == null)
            {
                Form1.getBoardName().setBackColor(newBackColor);
                this.Close();
            }
            //change both background and foreground colors and close form
            else
            {
                Form1.getBoardName().setBackColor(newBackColor);
                Form1.getBoardName().setForeColor(newForeColor);
                this.Close();
            }
        }
        //Player 1 piece color
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //player 2 piece color
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //highlighting option on or off
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        
    }
}
