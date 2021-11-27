using System;
using System.Windows.Forms;

namespace EmployesApp
{
    public partial class AddNewPosition : Form
    {
        PositionList _positionList;

        public PositionList PositionList
        {
            get { return _positionList; }

            set { _positionList = value; }
        }
        public AddNewPosition()
        {
            InitializeComponent();

            EnterString.maskTextBox_ErrorMessage(maskedTextBox1, errorProvider1);

            EnterString.maskTextBox_ChangeClickPosition(this, maskedTextBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(maskedTextBox1.Text))
            {
                _positionList.EmployeInfo.Employe.Position= maskedTextBox1.Text.Trim();
                _positionList.EmployeInfo.SetPosition();
                _positionList.Close();

                this.Close();
            }
        }
    }
}
