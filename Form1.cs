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

namespace MyNotePad
{
    public partial class Form1 : Form
    {
        string path;
        bool modified;
        Form exitDlg = new Form();
        public Form1()
        {
            InitializeComponent();
            modified = false;


            exitDlg.Size = new Size(400, 300);

            Label exitWarning = new Label();
            exitWarning.Size= new Size(200, 20);
            exitWarning.Text = "Та өөрчлөлтөө хадгалах уу?";

            Button yes_btn = new Button();
            Button no_btn = new Button();
            Button cancel_btn = new Button();

            yes_btn.Name = "yes_btn";

            yes_btn.Text = "Тийм";
            no_btn.Text = "Үгүй";
            cancel_btn.Text = "Болих";
            exitWarning.Location = new Point(20, 20);

            yes_btn.Location = new Point(20, 60);

            no_btn.Location = new Point(120, 60);

            cancel_btn.Location = new Point(220, 60);
            yes_btn.Click += new EventHandler(saveToolStripMenuItem_Click);
            no_btn.Click += new EventHandler(exit);
            cancel_btn.Click += new EventHandler(cancel);
            exitDlg.Controls.Add(yes_btn);
            exitDlg.Controls.Add(no_btn);
            exitDlg.Controls.Add(cancel_btn);
            exitDlg.Controls.Add(exitWarning);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "my file (*.myf)|*.myf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog1.FileName;
                var fileStream = openFileDialog1.OpenFile();

                using(StreamReader reader = new StreamReader(fileStream))
                {

                    textBox1.Text = reader.ReadToEnd();
                    path = filePath;
                    
                }
            }

           

            
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream myStream;
            saveFileDialog1.Filter = "my file (*.myf)|*.myf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (File.Exists(path))
            {
                File.Delete(path);
                myStream = File.Create(path);
                byte[] info = new UTF8Encoding(true).GetBytes(textBox1.Text);
                myStream.Write(info, 0, info.Length);
            }
            else
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    myStream = File.Create(saveFileDialog1.FileName);
                    byte[] info = new UTF8Encoding(true).GetBytes(textBox1.Text);
                    myStream.Write(info, 0, info.Length);



                    myStream.Close();

                }
            }
            modified = false;
            if (sender is Button)
            {
                Button btn = (Button)sender;
                if (btn.Name.Equals("yes_btn")) Application.Exit();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (modified)
            {
                
                exitDlg.ShowDialog();

            }
            else Application.Exit(); 
        }

        private void textModified(object sender, EventArgs e)
        {
            modified = true;
        }
        private void exit(object sender, EventArgs e) {
            Application.Exit();
        }
        private void cancel(object sender, EventArgs e)
        {
            exitDlg.Close();
        }
    }
}
