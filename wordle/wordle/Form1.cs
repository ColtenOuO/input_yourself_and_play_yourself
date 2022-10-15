using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wordle
{
    public partial class Form1 : Form
    {
        Button[] arr = new Button[26];
        string answer = "";

        int wrong = 0,now_time = 0;

        public Form1()
        {
            InitializeComponent();
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;

            now_time = 0;
            timer1.Interval = 1000;
            timer1.Enabled = false;
        }
        public void init()
        {
            textBox1.Text = "";


            wrong = now_time = 0;
            label5.Text = "猜錯次數: " + wrong + " 次";
            timer1.Enabled = false;

            for (int i=0;i<13;i++)
            {
                arr[i] = new Button();
                arr[i].SetBounds(40 * (i + 1), 30, 40, 40);
                arr[i].Enabled = false;
                Controls.Add(arr[i]);
                string s = "";
                char u2 = (char)('A' + i);
                s += u2;
                arr[i].Text = s;

                int idx = i;

              //  arr[i].Click += (sender, ex) => arr_Click(arr[idx]);
            }
            for(int i=13;i<26;i++)
            {
                arr[i] = new Button();
                arr[i].SetBounds(40 * (i - 13 + 1),70, 40, 40);
                Controls.Add(arr[i]);
                arr[i].Enabled = false;
                string s = "";
                char u2 = (char)('A' + i);
                s += u2;
                arr[i].Text = s;

                int idx = i;

              //  arr[i].Click += (sender, ex) => arr_Click(arr[idx]);
            }

            string u = "";
            for (int i = 0; i < answer.Length; i++) u += "_ ";
            label6.Text = u;

            button1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            textBox1.Visible = false;
            button1.Enabled = false;

            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;

            label4.Text = "時間: 0";

            return;
        }
        public void reset()
        {
            label4.Text = "時間: 0";
            textBox1.Text = "";
            timer1.Enabled = false;

            textBox1.Enabled = true;
            button1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            textBox1.Visible = true;

            button1.Enabled = true;
            textBox1.Enabled = true;

            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;

            for (int i = 0; i < 26; i++) arr[i].Visible = false;

            return;
        }

        public void Form1_KeyDown(object sender, KeyEventArgs e)        {
            if (timer1.Enabled == false) return;

            char u = '@';
            switch (e.KeyCode)
            {
                case Keys.A:
                    u = 'A';
                    break;
                case Keys.B:
                    u = 'B';
                    break;
                case Keys.C:
                    u = 'C';
                    break;
                case Keys.D:
                    u = 'D';
                    break;
                case Keys.E:
                    u = 'E';
                    break;
                case Keys.F:
                    u = 'F';
                    break;
                case Keys.G:
                    u = 'G';
                    break;
                case Keys.H:
                    u = 'H';
                    break;
                case Keys.I:
                    u = 'I';
                    break;
                case Keys.J:
                    u = 'J';
                    break;
                case Keys.K:
                    u = 'K';
                    break;
                case Keys.L:
                    u = 'L';
                    break;
                case Keys.M:
                    u = 'M';
                    break;
                case Keys.N:
                    u = 'N';
                    break;
                case Keys.O:
                    u = 'O';
                    break;
                case Keys.P:
                    u = 'P';
                    break;
                case Keys.Q:
                    u = 'Q';
                    break;
                case Keys.R:
                    u = 'R';
                    break;
                case Keys.S:
                    u = 'S';
                    break;
                case Keys.T:
                    u = 'T';
                    break;
                case Keys.U:
                    u = 'U';
                    break;
                case Keys.V:
                    u = 'V';
                    break;
                case Keys.W:
                    u = 'W';
                    break;
                case Keys.X:
                    u = 'X';
                    break;
                case Keys.Y:
                    u = 'Y';
                    break;
                case Keys.Z:
                    u = 'Z';
                    break;
                defalult:
                    break;
            }

            textBox1.Text = "";

            solve(u);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            answer = "";
            for(int i=0;i<textBox1.Text.Length;i++)
            {
                if( textBox1.Text[i] >= 'a' && textBox1.Text[i] <= 'z' )
                {
                    char u = (char)(textBox1.Text[i] - ('a' - 'A'));
                    answer += u;
                }
                else
                {
                    answer += textBox1.Text[i];
                }
            }
            init();

            timer1.Enabled = true;
            for (int i = 0; i < 26; i++) arr[i].Visible = true;
        }
        private void solve(char u)
        {
            char s = u;

            if (s >= 'a' && s <= 'z') s = (char)(s - ('a' - 'A'));

            bool have = false;
            string s2 = "";
            for(int i=0;i<answer.Length;i++)
            {
                if (answer[i] == s)
                {
                    have = true;
                    s2 += s;
                    arr[s-'A'].BackColor = Color.LightGreen;
                }
                else s2 += label6.Text[i*2];

                s2 += " ";
            }

            label6.Text = s2;

            if( have == false )
            {
                arr[s - 'A'].Visible = false;
                wrong++;
            }

            label5.Text = "猜錯次數: " + wrong + " 次";

            if( wrong >= 6 )
            {
                MessageBox.Show("你輸了");
                timer1.Enabled = false;
                reset();
            }
            else
            {
                bool win = true;
                for(int i=0;i<label6.Text.Length;i++)
                {
                    if (label6.Text[i] == '_') win = false;
                }

                if( win == true )
                {
                    MessageBox.Show("花費時間:" + now_time + "\n猜錯 " + wrong + "次", "You win!");
                    timer1.Enabled = false;
                    reset();
                }
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) // 247,274
        {
            char c = e.KeyChar;
            bool ok = false;

            if (c >= 'a' && c <= 'z') ok = true;
            if (c >= 'A' && c <= 'Z') ok = true;

            if (ok == true)
            {
                e.Handled = false;
                if (timer1.Enabled == true)
                {
                    MessageBox.Show("你贏了", "You win!");
                    char u = c;
                    solve(c);
                }
            }
            else
            {
                e.Handled = true;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            int num = ++now_time;
            label4.Text = "時間: " + num.ToString();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

    }
}
