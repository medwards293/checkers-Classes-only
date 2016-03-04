using System.IO;
using System.Collections;
using System;
using System.Windows.Forms;
namespace Checkers
{
    partial class Leaderboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        string player1Name;
        string player2Name;

        public void setPlayer1Name(string player1)
        {
            player1Name = player1;
        }

        public void setPlayer2Name(string player2)
        {
            player2Name = player2;
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.Player = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Wins = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Losses = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Player,
            this.Wins,
            this.Losses});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(22, 37);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(234, 186);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // Player
            // 
            this.Player.Text = "Player";
            this.Player.Width = 129;
            // 
            // Wins
            // 
            this.Wins.Text = "Wins";
            this.Wins.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Wins.Width = 50;
            // 
            // Losses
            // 
            this.Losses.Text = "Losses";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(85, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Leaderboard";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Name",
            "Wins",
            "Losses"});
            this.comboBox1.Location = new System.Drawing.Point(72, 229);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Text = "Sort by...";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Leaderboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Name = "Leaderboard";
            this.Text = "Leaderboard";
            this.Load += new System.EventHandler(this.Leaderboard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public void showList()
        {
            try
            {
                using (StreamReader file = new StreamReader("scores.txt"))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] row = { line, line = file.ReadLine(), line = file.ReadLine() };
                        ListViewItem listViewItem = new ListViewItem(row);
                        listView1.Items.Add(listViewItem);
                    }
                }
            }
            catch (Exception e) {; listView1.Items.Add("NotFound"); };
        }
        class ListViewItemComparer : IComparer
        {
            private int col;
            private SortOrder order;
            public ListViewItemComparer()
            {
                col = 0;
                order = SortOrder.Descending;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
                this.order = SortOrder.Descending;
            }
            public int Compare(object x, object y)
            {
                int returnVal = -1;
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                        ((ListViewItem)y).SubItems[col].Text);
                // Determine whether the sort order is descending.
                if ((order == SortOrder.Descending)&&col!=0)
                    // Invert the value returned by String.Compare.
                    returnVal *= -1;
                return returnVal;
            }
        }
        public void sortByNames()
        {
            listView1.ListViewItemSorter = new ListViewItemComparer(0);
            listView1.Sort();
        }
        public void sortByWins()
        {
            listView1.ListViewItemSorter = new ListViewItemComparer(1);
            listView1.Sort();
        }
        public void sortByLosses()
        {
           listView1.ListViewItemSorter = new ListViewItemComparer(2);
            listView1.Sort();
        }
        public void updateScores(int p)
        {
            string winner;
            string loser;
            if (p == 1)
            {
                winner = player1Name;
                loser = player2Name;
            }
            else
            {
                winner = player2Name;
                loser = player1Name;
            }


            if (winner != "computer")
            {
                //check if winner has a record
                int pos = findRecord(winner);
                //if not a new record is added else wins are incremented
                if (pos == -1)
                {
                    addNewRecord(winner, 1, 0);
                }
                else
                    increment(pos, "wins");
            }

            if (loser != "computer")
            {
                //if so do the same for loser
                int pos = findRecord(loser);
                if (pos == -1)
                {
                    addNewRecord(loser, 0, 1);
                }
                else
                    increment(pos, "losses");
            }

        }
        //checks if player has a record and returns position in file else returns -1
        int findRecord(string p)
        {
            string line;
            bool found=false;
            int l=0;
            using (StreamReader reader = new StreamReader("scores.txt"))
            {
                while ((line = reader.ReadLine()) != null )
                {
                    if (line == p)
                    {
                        found = true;
                        break;
                    }
                    l++;
                }
            }
            Console.WriteLine(l);
            if (found) return l; else return -1;

        }
        void addNewRecord(string n,int w,int l)
        {
            using (StreamWriter sw = File.AppendText("scores.txt"))
            {
                sw.WriteLine(n);
                sw.WriteLine(w.ToString());
                sw.WriteLine(l.ToString());
            }
        }
        //increments wins or losses at position p
        void increment(int p, string winsOrLosses)
        {
            if (winsOrLosses == "wins")
                p++;
            else p += 2;

            string[] lines = File.ReadAllLines("scores.txt");
            int score = Convert.ToInt32(lines[p]);
            score++;
            lines[p] = score.ToString();
            File.WriteAllLines("scores.txt", lines);
        }
        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader Player;
        private System.Windows.Forms.ColumnHeader Wins;
        private System.Windows.Forms.ColumnHeader Losses;
        private System.Windows.Forms.ComboBox comboBox1;
    }

}