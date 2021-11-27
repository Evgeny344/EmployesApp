using System;
using System.Windows.Forms;

namespace EmployesApp
{
    public partial class AddNewDepartment : Form
    {

        DepartmentList _departmentList;

        public DepartmentList DepartmentList
        {
            get { return _departmentList; }

            set { _departmentList = value; }
        }

        public AddNewDepartment()
        {
            InitializeComponent();

            EnterString.maskTextBox_ErrorMessage(maskedTextBox1, errorProvider1);

            EnterString.maskTextBox_ChangeClickPosition(this, maskedTextBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(maskedTextBox1.Text))
            {
                _departmentList.EmployeInfo.Employe.Department = maskedTextBox1.Text.Trim();
                _departmentList.EmployeInfo.SetDepartment();
                _departmentList.Close();

                this.Close();
            }
        }
    }
}
