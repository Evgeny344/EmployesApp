using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EmployesApp
{
    public partial class FormStart : Form
    {
        List<int> employeId;

        int selectedRec; 

        public FormStart()
        {
            InitializeComponent();

            employeId = new List<int>();

            selectedRec = 0;

            recUpdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {

                EmployeInfo newForm = new EmployeInfo(false, true, false);

                newForm.FormStart = this;

                newForm.ShowDialog();
            
        }

        private List<String> GetListForEmploye()
        {
            List<String> employeRec = new List<String>(); // выводим список сотрудников

            using (ApplicationContext db = new ApplicationContext())
            {
                var employes = db.Employes.ToList();

                foreach (Employe e in employes)
                {
                    employeRec.Add(e.FirstName + " " + e.LastName + " " + e.PatronymicName + " " + 
                        e.Department + " " + e.Position + " " + e.Category + " " + e.Salary + " " +
                        e.Status);

                    employeId.Add(e.Id);
                }
            }

            return employeRec;
        }

        private List<String> GetListForEmploye( List <Employe> employeFind)
        {
            List<String> employeRec = new List<String>(); // выводим список сотрудников

            using (ApplicationContext db = new ApplicationContext())
            {
                var employes = employeFind;

                foreach (Employe e in employes)
                {
                    employeRec.Add(e.FirstName + " " + e.LastName + " " + e.PatronymicName + " " +
                        e.Department + " " + e.Position + " " + e.Category + " " + e.Salary + " " +
                        e.Status);

                    employeId.Add(e.Id);
                }
            }

            return employeRec;
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedRec = listBox1.SelectedIndex;
        }

        private void button_edit_Click(object sender, EventArgs e)
        {
            if (employeId.Count > 0)
            {
                EmployeInfo newForm = new EmployeInfo(true, false, false);

                newForm.FormStart = this;

                newForm.InitialStartValue(employeId[selectedRec]);

                newForm.ShowDialog();
            }
        }

        public void recUpdate()
        {
            employeId.Clear();

            listBox1.DataSource = GetListForEmploye();
        }

        public void recUpdate(List<Employe> employesFind)
        {
            employeId.Clear();

            listBox1.DataSource = GetListForEmploye(employesFind);

            selectedRec = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (employeId.Count > 0)
            {
                OperationRec.DeleteEmploye(employeId[selectedRec]);

                recUpdate();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
  
                EmployeInfo newForm = new EmployeInfo(false, false, true);

                newForm.FormStart = this;

                newForm.ShowDialog();
        }
    }
}
