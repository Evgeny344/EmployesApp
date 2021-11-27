using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace EmployesApp
{
    public partial class DepartmentList : Form
    {
        string selectedDepartment;

        const string addNewDepartment = "Добавить новый";

        Employe _employe;

        public Employe Employe
        {
            get { return _employe; }

            set { _employe = value; }
        }

        EmployeInfo _employeInfo;
  
        public EmployeInfo EmployeInfo
        {
            get  { return _employeInfo; }

            set { _employeInfo = value; }
        }

        public DepartmentList()
        {
            InitializeComponent();

            listBox1.DataSource = GetListForDepartment();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedDepartment = (string)listBox1.SelectedItem;         
        }

        private List<String> GetListForDepartment()
        {
            var strDepartments = new List<String>();

            using (ApplicationContext db = new ApplicationContext())
            {
               List<Department> departments = db.Departments.ToList();

                foreach (Department dep in departments)
                {
                    strDepartments.Add(dep.Name);
                }
            }

            strDepartments.Add(addNewDepartment);

            return strDepartments;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedDepartment == addNewDepartment)
            {
                AddNewDepartment newForm = new AddNewDepartment();

                newForm.DepartmentList = this;

                newForm.ShowDialog();
            }

            else
            {
                _employe.Department = selectedDepartment;

                _employeInfo.SetDepartment();

                this.Close();
            }
        }
    }
}
