using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace EmployesApp
{
    public partial class CategoryList : Form
    {
        string selectedCategory;

        const string addNewCategory = "Добавить новую";

        EmployeInfo _employeInfo;

        public EmployeInfo EmployeInfo
        {
            get { return _employeInfo; }

            set { _employeInfo = value; }
        }

        Employe _employe;

        public Employe Employe
        {
            get { return _employe; }

            set { _employe = value; }
        }

        public CategoryList()
        {
            InitializeComponent();

            listBox1.DataSource = GetListForCategory();

        }

        private List<String> GetListForCategory()
        {
            List<String> strCategories = new List<String>();

            using (ApplicationContext db = new ApplicationContext())
            {
                List<Category> categories = db.Categories.ToList();

                foreach (Category cat in categories)
                {
                    strCategories.Add(cat.Name);
                }
            }

            strCategories.Add(addNewCategory);

            return strCategories;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (selectedCategory == addNewCategory)
            {
                AddNewCategory newForm = new AddNewCategory();

                newForm.CategoryList = this;

                newForm.ShowDialog();
            }

            else
            {
                _employe.Category = selectedCategory;

                EmployeInfo.SetCategory();

                this.Close();
            }
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            selectedCategory = (string)listBox1.SelectedItem;
        }
    }
}
