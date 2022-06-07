using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Библиотека
{
    public partial class Start_page : Form
    {
        public Start_page()
        {
            InitializeComponent();
        }

        private void Start_page_Load(object sender, EventArgs e)
        {
            IConverting converting = new XmlConverting();
            try
            {
                Holder.library = converting.Deserialize();
            }
            catch (Exception)
            {
                MessageBox.Show(
                "Некорректный файл Books.xml",
                "Предупреждение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                Environment.Exit(2);
            }
            foreach (Book book in Holder.library.BookList)
                Holder.data.Add(book);
            dgvStart.DataSource = Holder.data;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IConverting converting = new XmlConverting();
            if (tbAuthor.Text != "" && tbYear.Text != "" && tbName.Text != "")
            {
                Book book1 = new Book(tbName.Text, tbAuthor.Text, tbYear.Text);
                Holder.library.BookList.Add(book1);
                Holder.data.Add(book1);
                dgvStart.DataSource = Holder.data;
                tbAuthor.Text = "";
                tbYear.Text = "";
                tbName.Text = "";
                converting.Serialize(Holder.library);
            }
        }

        int button = 1;
        private void btn_search_Click(object sender, EventArgs e)
        {
            dgvSearch.ClearSelection();
            string s = tbSearch.Text;
            int i = 0;
            BindingList<Book> data1 = new BindingList<Book>();
            if (button == 1)
            {
                foreach (Book book in Holder.library.BookList)
                {
                    if (book.Name.Contains(s))
                    {
                        data1.Add(book);
                        i = 1;
                    }
                }
                if (i == 1)
                    dgvSearch.DataSource = data1;
            }
            if (button == 2)
            {
                foreach (Book book in Holder.library.BookList)
                {
                    if (book.Author.Contains(s))
                    {
                        data1.Add(book);
                        i = 1;
                    }
                }
                if (i == 1)
                    dgvSearch.DataSource = data1;
            }
            if (button == 3)
            {
                foreach (Book book in Holder.library.BookList)
                {
                    if (book.Year_of_release.Contains(s))
                    {
                        data1.Add(book);
                        i = 1;
                    }
                }
                if (i == 1)
                    dgvSearch.DataSource = data1;
            }
            if (i == 0)
                dgvSearch.Rows.Clear();

        }

        private void rbName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbName.Checked)
                button = 1;
        }

        private void rbAuthor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAuthor.Checked)
                button = 2;
        }

        private void rbYear_CheckedChanged(object sender, EventArgs e)
        {
            if (rbYear.Checked)
                button = 3;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            IConverting converting = new XmlConverting();
            foreach (DataGridViewRow row in dgvStart.SelectedRows)
            {
                Holder.library.BookList.RemoveAt(row.Index);
                dgvStart.Rows.Remove(row);
            }
            converting.Serialize(Holder.library);
        }

    }
}
