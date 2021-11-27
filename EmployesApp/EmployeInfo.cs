using System;
using System.Windows.Forms;
using System.Linq;

namespace EmployesApp
{
    public partial class EmployeInfo : Form
    {
        int employeID;

        readonly bool _formChangeEmploye;

        readonly bool _formAddEmploye;

        readonly bool _formFindEmploye;

        Employe _employe;

        public Employe Employe
        {
            get { return _employe; }

            set { _employe = value; }
        }

        FormStart _formStart;

        public FormStart FormStart
        {
            get { return _formStart; }

            set { _formStart = value; }
        }

        const string status = "При исполнении";

        public void SetDepartment ()
        {
            linkLabel1.Text = _employe.Department;
        }

        public void SetPosition ()
        {
            linkLabel2.Text = _employe.Position;
        }

        public void SetCategory()
        {
            linkLabel3.Text = _employe.Category;
        }
        public EmployeInfo(bool formChangeEmploye, bool formAddEmploye, bool formFindEmploye)
        {
            InitializeComponent();

            _employe = new Employe();

            _formChangeEmploye = formChangeEmploye;

            _formAddEmploye = formAddEmploye;

            _formFindEmploye = formFindEmploye;

            EnterString.maskTextBox_ChangeClickPosition(this, maskedTextBox1);

            EnterString.maskTextBox_ChangeClickPosition(this, maskedTextBox2);

            EnterString.maskTextBox_ChangeClickPosition(this, maskedTextBox3);

            EnterString.maskTextBox_ChangeClickPosition(this, maskedTextBox4);

            if (_formAddEmploye)
            {
                maskedTextBox4.Text = "0";
            }

            if (_formChangeEmploye)
            {
                checkBox1.Visible = true;
            }

            if (_formFindEmploye)
            {
                label8.Visible = true;

                maskedTextBox5.Visible = true;

                EnterString.maskTextBox_ChangeClickPosition(this, maskedTextBox5);
            }

        }

        public void InitialStartValue(int selectedEmployeID)
        {
            employeID = selectedEmployeID;

            using (ApplicationContext db = new ApplicationContext()) // получаем выделенный пользователем сотрудника
            {
                var employes = (from emp in db.Employes
                                where emp.Id == selectedEmployeID
                                select emp).ToList();

                var employe = employes[0];

                maskedTextBox1.Text = employe.FirstName;

                maskedTextBox2.Text = employe.LastName;

                maskedTextBox3.Text = employe.PatronymicName;

                maskedTextBox4.Text = Convert.ToString(employe.Salary);

                linkLabel1.Text = employe.Department;

                linkLabel2.Text = employe.Position;

                linkLabel3.Text = employe.Category;

                _employe.Department = employe.Department;

                _employe.Position = employe.Position;

                _employe.Category = employe.Category;

                checkBox1.Checked = employe.Status == "Уволен" ? true : false;

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DepartmentList newForm = new DepartmentList();

            newForm.EmployeInfo = this;

            newForm.Employe = _employe;

            newForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
          _employe.FirstName = maskedTextBox1.Text.Trim();

          _employe.LastName = maskedTextBox2.Text.Trim();

          _employe.PatronymicName = maskedTextBox3.Text.Trim();

                
            if (!_formFindEmploye) // если форма не представляет поиск, то заполняем данные автоматически
            {
                if (String.IsNullOrEmpty(maskedTextBox4.Text))
                {
                    maskedTextBox4.Text = "0";
                }

                _employe.Salary = Convert.ToInt32(maskedTextBox4.Text.Trim());

                _employe.Status = DefineStatus();

                if (!EnterString.IsAnyNullOrEmpty(_employe)) // проверяем, не осталось ли пустых строк
                {
                    if (_formAddEmploye)
                    {
                        OperationRec.AddNewEmploye(_employe);
                    }

                    else if (_formChangeEmploye)
                    {
                        OperationRec.ChangeEmploye(_employe, employeID);
                    }

                    _formStart.recUpdate();

                    this.Close();
                }

                else
                {
                    errorProvider1.SetError(button1, "Заполните все строки");
                }
            }

            else // если форма представляет поиск сотрудника 

            {

                _employe.Salary = String.IsNullOrEmpty(maskedTextBox4.Text) ? 0 : Convert.ToInt32(maskedTextBox4.Text.Trim());

                _employe.Status = maskedTextBox5.Text.Trim();

                _formStart.recUpdate(OperationRec.FindEmploye(_employe));

                this.Close();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PositionList newForm = new PositionList();

            newForm.EmployeInfo = this;

            newForm.Employe = _employe;

            newForm.ShowDialog();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CategoryList newForm = new CategoryList();

            newForm.EmployeInfo = this;

            newForm.Employe = _employe;

            newForm.ShowDialog();

        }

        string DefineStatus ()
        {
            if (_formChangeEmploye)
            {
                return checkBox1.Checked ? "Уволен" : status;  
            }

            return status;
        }

    }
}
