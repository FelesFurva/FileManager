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

namespace FileManager
{
    public partial class Form1 : Form
    {
        List<string> ListFiles = new List<string>();
        public Form1()
        {
            InitializeComponent();
            
        }
        
        public static string fileName;
        public string fileText;
        string path;
        string selectedPath;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        string GetFilePath()
        {
            fileName = textBox1.Text;
            path = selectedPath + "/" + fileName + ".txt";
            return path;
        }


        void CreateFile(string newFilePath)
        {
            if (!File.Exists(newFilePath))
            {
                using (StreamWriter sw = File.CreateText(newFilePath))
                MessageBox.Show("File created sucessfully!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetFilePath();
            CreateFile(path);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        string GetTextforFile()
        {
            fileText = textBox3.Text;
            return fileText;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetFilePath();
            GetTextforFile();
            WriteToFile(path, fileText);
        }
        void WriteToFile(string path, string fileText)
        {
            if (File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(fileText);
                    MessageBox.Show("Text saved sucessfully!");
                }
            }
            else
            {
                MessageBox.Show("No such file");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetFilePath();
            ReadFile(path);
        }

        void ReadFile(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string text = sr.ReadLine();
                    richTextBox1.Text = text;
                }
            }
            else
            {
                MessageBox.Show("No such file");
            }
        }

        void CopyFile(string path)
        {
            if(File.Exists(path))
            {
                
                string copyName = fileName + "copy.txt";

                string[] files = System.IO.Directory.GetFiles(selectedPath, fileName+"*");

                int count = files.Count();
                if (count > 1)
                {
                    copyName = fileName + $"copy{count}.txt";
                }
                string nameWithPath = selectedPath + "/" + copyName;
                File.Copy(path, nameWithPath);
                MessageBox.Show("File copied successfully!");
            }
            else
            {
                MessageBox.Show("No such file");
            }
        }

        void RenameFile(string path)
        {
            if(File.Exists(path))
            {
                string newName = selectedPath +"/"+ textBox3.Text + ".txt";
                File.Move(path, newName);
                MessageBox.Show($"File {fileName} renamed {textBox3.Text}");
            }
            else
            {
                MessageBox.Show("No such file");
            }

        }

        void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                MessageBox.Show("File deleted successfully");
            }
            else
            {
                MessageBox.Show("No such file");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetFilePath();
            DeleteFile(path);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GetFilePath();
            CopyFile(path);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GetFilePath();
            RenameFile(path);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ListFiles.Clear();
            listView1.Items.Clear();
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description="Select your path"})
            {
                if(fbd.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = fbd.SelectedPath;
                    selectedPath = textBox2.Text;
                    foreach (string item in Directory.GetFiles(fbd.SelectedPath))
                    {https://mdps.rigasmezi.lv/?fbclid=IwAR3efpcIMTmtNu20qBQgHAZxuSXU81hKFafYJRwzXrs2MAbD_owx4n-afyM
                        imageList1.Images.Add(System.Drawing.Icon.ExtractAssociatedIcon(item));
                        FileInfo fileInfo = new FileInfo(item);
                        ListFiles.Add(fileInfo.FullName);
                        listView1.Items.Add(fileInfo.Name, imageList1.Images.Count - 1);
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
