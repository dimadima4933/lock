using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace @lock
{
    public partial class Form1 : Form
    {

        int[] a = new int[10];
        Thread t1 = null, t2 = null, t3 = null, t4 = null;
        

        public Form1()
        {
            InitializeComponent();
        }

        void print()
        {
            listBox1.Items.Clear();
            foreach(int c in a){
                listBox1.Items.Add(c.ToString());
            }
        }

        void thread1()
        {
            for (int i = 0; ; i++)
            {
                textBox1.Text = "Start " + i.ToString();
                for (int k = 0; k < 10; k++)
                {
                    a[k] = 33;
                    Thread.Sleep(200);
                }
                print();
                textBox1.Text = "Done " + i.ToString();
                Thread.Sleep(1000);
            }
        }

        void thread2()
        {
            for (int i = 0; ; i++)
            {
                textBox2.Text = "Start " + i.ToString();
                for (int k = 0; k < 10; k++)
                {
                    a[k] = 555;
                    Thread.Sleep(300);
                }
                print();
                textBox2.Text = "Done " + i.ToString();
                Thread.Sleep(700);
            }
        }

        void thread3()
        {
            for (int i = 0; ; i++)
            {
                textBox1.Text = "Start " + i.ToString();

                // Попытаться занять критическую секцию
                lock(this)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        a[k] = 33;
                        Thread.Sleep(200);
                    }
                    textBox1.Text = " Done" + i.ToString();
                    print();
                }
                Thread.Sleep(1000);
            }
        }

        void thread4()
        {
            for (int i = 0; ; i++)
            {
                textBox2.Text = "Start " + i.ToString();

                // Попытаться занять критическую секцию
                Monitor.Enter(this);
                try
                {
                    for (int k = 0; k < 10; k++)
                    {
                        a[k] = 555;
                        Thread.Sleep(300);
                    }
                    textBox2.Text = "Done " + i.ToString();
                    print();
                }
                finally
                {
                    // Освободить критическую секцию
                    Monitor.Exit(this);
                }
                Thread.Sleep(700);
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            ThreadStart p1 = new ThreadStart(thread1);
            t1 = new Thread(p1);
            t1.IsBackground = true;
            t1.Start();
            
            p1 = new ThreadStart(thread2);
            t2 = new Thread(p1);
            t2.IsBackground = true;
            t2.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (t1 != null && t1.IsAlive) t1.Abort();
            if (t2 != null && t2.IsAlive) t2.Abort();
            if (t3 != null && t3.IsAlive) t3.Abort();
            if (t4 != null && t4.IsAlive) t4.Abort();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ThreadStart p1 = new ThreadStart(thread3);
            t1 = new Thread(p1);
            t1.IsBackground = true;
            t1.Start();

            p1 = new ThreadStart(thread4);
            t2 = new Thread(p1);
            t2.IsBackground = true;
            t2.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}