using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace EmployesApp
{
    public partial class PositionList : Form
    {
        string selectedPosition;

        const string addNewPosition = "Добавить новую";

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

        public PositionList()
        {
            InitializeComponent();

            listBox1.DataSource = GetListForPosition();
        }

        private List<String> GetListForPosition()
        {
            List<String> strPositions = new List<String>();

            using (ApplicationContext db = new ApplicationContext())
            {
                List<Position> positions = db.Positions.ToList();

                foreach (Position pos in positions)
                {
                    strPositions.Add(pos.Name);
                }
            }

            strPositions.Add(addNewPosition);

            return strPositions;
        }

        private void PositionList_Load(object sender, EventArgs e)
        {
            selectedPosition = (string)listBox1.SelectedItem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedPosition == addNewPosition)
            {
                AddNewPosition newForm = new AddNewPosition();

                newForm.PositionList = this;

                newForm.ShowDialog();
            }

            else
            {
                _employe.Position = selectedPosition;

                _employeInfo.SetPosition();

                this.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPosition = (string)listBox1.SelectedItem;
        }
    }
}
