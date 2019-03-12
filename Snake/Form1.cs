using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        Label Head;
        Label apple;
        Label[] Body = new Label[159];
        int x = 0, y = 0;
        int length = 0;
        Point Popka = new Point(1,1);
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            Square.Controls.Clear();

            Head = new Label();
            Head.AutoSize = false;
            Head.BackColor = Color.Green;
            Head.Width = Head.Height = 50;
            Head.Text = "";
            Head.Location = new Point(0, 0);

            apple = new Label();
            apple.AutoSize = false;
            apple.BackColor = Color.Red;
            apple.Height = apple.Width = 50;
            apple.Text = "";
            apple.Location = new Point(rand.Next(16) * 50, rand.Next(10) * 50);
            while(apple.Location == new Point(0,0))
                apple.Location = new Point(rand.Next(16) * 50, rand.Next(10) * 50);

            Square.Controls.Add(Head);
            Square.Controls.Add(apple);
            Snake_step_timer.Enabled = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch(keyData)
            {
                case Keys.Up: x = 0; y = -1; ; break;
                case Keys.Down: x = 0; y = 1; ; break;
                case Keys.Right: x = 1; y = 0; break;
                case Keys.Left: x = -1; y = 0; break;
                default: return base.ProcessCmdKey(ref msg, keyData);
            }
            return false;
        }

        private void Snake_step_Tick(object sender, EventArgs e)
        {
            Snake_step();
            if (ItHead(apple.Location)) eat_apple();
        }

        private void Snake_step()
        {
            if (length != 0)
            {
                Popka = Body[length - 1].Location;
                for (int i = length - 1; i > 0; i--)
                    Body[i].Location = Body[i - 1].Location;
                Body[0].Location = Head.Location;
            }
            else Popka = Head.Location;

            Head.Location = new Point(Head.Location.X + x * 50, Head.Location.Y + y * 50);
            if (Head.Location.X == 800) Head.Location = new Point(0, Head.Location.Y);
            if (Head.Location.Y == 500) Head.Location = new Point(Head.Location.X, 0);
            if (Head.Location.X < 0) Head.Location = new Point(750, Head.Location.Y);
            if (Head.Location.Y < 0) Head.Location = new Point(Head.Location.X, 450);
            
            if(ItBody(Head.Location))
            {
                Snake_step_timer.Enabled = false;
                if (length == 159)
                    MessageBox.Show("You win!!");
                else
                    MessageBox.Show("You fail");
            }
        }

        private void eat_apple()
        {
            while(ItHead(apple.Location)||ItBody(apple.Location))
                apple.Location = new Point(rand.Next(16) * 50, rand.Next(10) * 50);

            Body[length] = new Label();
            Body[length].AutoSize = false;
            Body[length].BackColor = Color.LightGreen;
            Body[length].Text = "";
            Body[length].Width = Body[length].Height = 50;
            Body[length].Location = Popka;
            Square.Controls.Add(Body[length]);
            length++;
        }

        private bool ItHead(Point p)
        {
            if (p == Head.Location) return true;
            return false;
        }

        private bool ItBody(Point p)
        {
            for (int i = 0; i < length; i++)
                if (p == Body[i].Location) return true;
            return false;
        }
    }
}
