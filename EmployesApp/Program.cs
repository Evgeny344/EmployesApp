using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace EmployesApp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (ApplicationContext db = new ApplicationContext()) // добавляем первого работника по умолчанию
            {
                if (!db.Employes.Any(e => e.Id != null))
                {
                    Department department = new Department { Name = "Разработка ПО" };

                    Position position = new Position { Name = "Разработчик" };

                    Category category = new Category { Name = "Младший" };
                    
                    Employe employe = new Employe
                    {

                        FirstName = "Евгений",
                        LastName = "Беленков",
                        PatronymicName = "Сергеевич",

                        Department = department.Name,
                        Position = position.Name,
                        Category = category.Name,
                        Salary = 35000,
                        Status = "При исполнении"

                    };

                    OperationRec.AddNewEmploye(employe);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormStart());
        }

    }
}
