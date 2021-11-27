using System;
using System.Windows.Forms;

namespace EmployesApp
{
    public partial class AddNewCategory : Form
    {
        public AddNewCategory()
        {
            InitializeComponent();

            EnterString.maskTextBox_ErrorMessage(maskedTextBox1, errorProvider1);

            EnterString.maskTextBox_ChangeClickPosition(this, maskedTextBox1);
        }

        CategoryList _categoryList;

        public CategoryList CategoryList
        {
            get { return _categoryList; }

            set { _categoryList = value; }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(maskedTextBox1.Text))
            {
                _categoryList.EmployeInfo.Employe.Category = maskedTextBox1.Text.Trim();
                _categoryList.EmployeInfo.SetCategory();
                _categoryList.Close();

                this.Close();
            }
        }

    }
}
