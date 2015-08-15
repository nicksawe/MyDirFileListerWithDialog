using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyDirFileListerWithDialog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string Path ;
        public bool IsSelected ;
        public List<string> Files ;
        public int Filecount;
        public DateTime Start ;
        public DateTime End ;
        public string PathToWriteTo ;

        private void btnSource_Click(object sender, EventArgs e)
        {
            GetValue();
            lblSource.Text = Path;
            GetFileData(out Files, out Filecount, out Start, out End);
        }

        private void GetValue()
        {
            IsSelected = false;
            folderBrowserDialog1.SelectedPath = "";
            folderBrowserDialog1.ShowDialog();
            Path = folderBrowserDialog1.SelectedPath;

            if (Path != string.Empty)
            {
                IsSelected = true;
               // MessageBox.Show(path);
            }
        }

        private void btnDestination_Click(object sender, EventArgs e)
        {
            GetValue();
            lblDestination.Text = Path;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is Label)
                {
                    control.Text = "";
                }
            }
        }

        private void GetFileData(out List<string> Files, out int Filecount, out DateTime Start, out DateTime End)
        {
            Start = DateTime.Now;

            var temp = Directory.EnumerateFiles(Path, "*.*", SearchOption.AllDirectories).ToList();
            temp.Sort();
            Files = temp;

            Filecount = Files.Count();

            End = DateTime.Now;
        }

        public void WriteToFile()
        {
            
            using (var x = File.CreateText(Path + @"\" + txtFilename.Text + ".txt"))
            {
                foreach (var t in Files)
                {
                    x.Write(t + Environment.NewLine);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtFilename.Text!="")
            {
                WriteToFile();
                lblResult.Text = Filecount + @" files written to " + Path + @"\" + txtFilename.Text + @".txt";
            }
            else
            {
                lblResult.Text = @"Enter filename to save to";
            }

            
        }
    }
}


