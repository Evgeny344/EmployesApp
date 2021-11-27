using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace EmployesApp
{
   public static class EnterString
    {
        
       public static void maskTextBox_ErrorMessage(MaskedTextBox maskTextBox, ErrorProvider errorProvider) // показываем оповещение, если строка остаётся пустой
        {
            maskTextBox.Validating += maskedTextBox_Validating;

            void maskedTextBox_Validating(object sender, CancelEventArgs e)
            {
                if (String.IsNullOrEmpty(maskTextBox.Text))
                {
                    errorProvider.SetError(maskTextBox, "Заполните строку");
                }

                else
                {
                    errorProvider.Clear();
                }
            }
        }

        

        public static void maskTextBox_ChangeClickPosition (Form form, MaskedTextBox maskTextBox) // переводим курсор мыши в начало строки для удобства
        {
            maskTextBox.Click += maskedTextBoxEnter;

            void maskedTextBoxEnter(object sender, EventArgs e)
            {
                form.BeginInvoke((MethodInvoker)delegate ()
                {
                    maskTextBox.Select(0, 0);

                    if (maskTextBox.Text == "0")
                        maskTextBox.Clear();
                });
            }
        }

        public static bool IsAnyNullOrEmpty(Employe employe) // проверяем, все ли поля информации о сотруднике заполнены
        {
            foreach (PropertyInfo e in employe.GetType().GetProperties())
            {
                if (e.PropertyType == typeof(string))
                {
                    string value = Convert.ToString(e.GetValue(employe));

                    if (string.IsNullOrEmpty(value))
                    {
                        return true;
                    }
                }

                else if (e.Name=="Salary")
                {
                    int value = Convert.ToInt32(e.GetValue(employe));
                    
                    if (value==0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
