using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MarkdownEditor
{
    public partial class Form1 : Form
    {
        private string editorHtml;

        public Form1()
        {
            InitializeComponent();

            StreamReader reader = new StreamReader("editor.html");
            editorHtml = reader.ReadToEnd();
            webBrowser1.DocumentText = editorHtml;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            updateWebBrowser();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            updateWebBrowser();
        }

        private void updateWebBrowser()
        {
            string newHtml = editorHtml.Replace("CONTENT",
                                                 textBox1.Text.Replace("\n", "<br>"));
            newHtml = newHtml.Replace("CSS", textBox2.Text);

            webBrowser1.DocumentText = newHtml;
        }

        private void dumpHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine(webBrowser1.DocumentText);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                StreamWriter writer;

                writer = new StreamWriter(dialog.SelectedPath + "\\markdown.md");
                foreach (string line in textBox1.Lines)
                    writer.WriteLine(line);
                writer.Close();

                writer = new StreamWriter(dialog.SelectedPath + "\\style.css");
                foreach (string line in textBox2.Lines)
                    writer.WriteLine(line);
                writer.Close();
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                StreamReader reader;

                reader = new StreamReader(dialog.SelectedPath + "\\markdown.md");
                textBox1.Text = reader.ReadToEnd();
                reader.Close();

                reader = new StreamReader(dialog.SelectedPath + "\\style.css");
                textBox2.Text = reader.ReadToEnd();
                reader.Close();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Github Markdown Editor (v0.1)\n" + 
                            "Built by Cameron Edwards\n" +
                            "Suggested by Dave",
                            "About");
        }
    }
}
