using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Библиотека
{
    public partial class Start_page : Form
    {
        public Start_page()
        {
            InitializeComponent();
        }

        private void SerializeXML(Books books)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Books));

            using (FileStream fs = new FileStream("Books.xml", FileMode.Create))
            {
                xml.Serialize(fs, books);
            }
        }
        private static Books DeserializeXML()
        {
            XmlSerializer xml = new XmlSerializer(typeof(Books));

            using (FileStream fs = new FileStream("Books.xml", FileMode.OpenOrCreate))
            {
                return (Books)xml.Deserialize(fs);
            }
        }

        BindingList<Book> data = new BindingList<Book>();
        Books books = DeserializeXML();
        int button = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Book book in books.BookList)
                data.Add(book);
            dataGridView1.DataSource = data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
            if (button != 0)
            {
                string s = textBox1.Text;
                int i = 0;
                BindingList<Book> data1 = new BindingList<Book>();
                if (button == 1)
                {
                    foreach(Book book in books.BookList)
                    {
                        if (book.Name.Contains(s))
                        {
                            data1.Add(book);
                            i = 1;
                        }
                    }
                    if (i == 1)
                        dataGridView2.DataSource = data1;
                }
                if (button == 2)
                {
                    foreach (Book book in books.BookList)
                    {
                        if (book.Author.Contains(s))
                        {
                            data1.Add(book);
                            i = 1;
                        }
                    }
                    if (i == 1)
                        dataGridView2.DataSource = data1;
                }
                if (button == 3)
                {
                    foreach (Book book in books.BookList)
                    {
                        if (book.Year_of_release.Contains(s))
                        {
                            data1.Add(book);
                            i = 1;
                        }
                    }
                    if (i == 1)
                        dataGridView2.DataSource = data1;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                Book book1 = new Book(textBox4.Text, textBox2.Text, textBox3.Text);
                books.BookList.Add(book1);
                data.Add(book1);
                dataGridView1.DataSource = data;
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                SerializeXML(books);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                button = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                button = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                button = 3;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                books.BookList.RemoveAt(row.Index);
                dataGridView1.Rows.Remove(row);
            }
            SerializeXML(books);
        }
    }
}
