using System.Linq;
using System;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace EmployesApp
{
    public static class OperationRec
    {
        public static void AddNewEmploye(Employe employe)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var departments = db.Departments.ToList();

                if (!departments.Exists(d => d.Name == employe.Department)) // проверяем, имеется ли такой отдел в базе. Если нет - добавляем
                {
                    Department newDepartment = new Department { Name = employe.Department };

                    db.Departments.Add(newDepartment);
                }


                var positions = db.Positions.ToList(); // проверяем, имеется ли такая должность в базе. Если нет - добавляем

                if (!positions.Exists(p => p.Name == employe.Position))
                {
                    Position newPosition = new Position { Name = employe.Position };

                    db.Positions.Add(newPosition);
                }


                var categories = db.Categories.ToList(); // проверяем, имеется ли такая категория в базе. Если нет - добавляем

                if (!categories.Exists(c => c.Name == employe.Category))
                {
                    Category newCategory = new Category { Name = employe.Category };

                    db.Categories.Add(newCategory);
                }

                db.Employes.Add(employe);

                db.SaveChanges();
            }

        }

        public static void DeleteEmploye(int employesID)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var employes = (from emp in db.Employes
                                where emp.Id == employesID
                                select emp).ToList();

                db.Employes.Remove(employes[0]);

                db.SaveChanges();

            }
        }

        public static void ChangeEmploye(Employe employe, int selectedRec)
        {
            DeleteEmploye(selectedRec);

            AddNewEmploye(employe);
        }

        public static List<Employe> FindEmploye(Employe employe)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var count = 0;

                var empValue = new List<string>();

                var strSQL = "SELECT * FROM Employes";

                foreach (PropertyInfo e in employe.GetType().GetProperties())
                {
                    if (e.PropertyType == typeof(string))
                    {
                        string value = Convert.ToString(e.GetValue(employe));

                        if (!string.IsNullOrEmpty(value))
                        {
                            count++;

                            strSQL += count > 1 ? " AND " : " WHERE ";

                            strSQL += e.Name + " LIKE {" + Convert.ToString(count - 1) + "}";

                            empValue.Add("%" + value + "%");
                        }
                    }

                    else if (e.Name == "Salary")
                    {

                        int value = Convert.ToInt32(e.GetValue(employe));

                        if (value > 0)
                        {
                            count++;

                            strSQL += count > 1 ? " AND " : " WHERE ";

                            strSQL += e.Name + " = {" + Convert.ToString(count - 1) + "}";

                            empValue.Add(Convert.ToString(value));
                        }
                    }
                }

                var employesFind = new List<Employe>();

                switch (count)
                {
                    case 1:
                        {
                            employesFind = db.Employes.FromSqlRaw(strSQL, empValue[0]).ToList();
                            break;
                        }

                    case 2:
                        {
                            employesFind = db.Employes.FromSqlRaw(strSQL, empValue[0], empValue[1]).ToList();
                            break;
                        }

                    case 3:
                        {
                            employesFind = db.Employes.FromSqlRaw(strSQL, empValue[0], empValue[1], empValue[2]).ToList();
                            break;
                        }

                    case 4:
                        {
                            employesFind = db.Employes.FromSqlRaw(strSQL, empValue[0], empValue[1], empValue[2], empValue[3]).ToList();
                            break;
                        }

                    case 5:
                        {
                            employesFind = db.Employes.FromSqlRaw(strSQL, empValue[0], empValue[1], empValue[2], empValue[3], empValue[4]).ToList();
                            break;
                        }

                    case 6:
                        {
                            employesFind = db.Employes.FromSqlRaw(strSQL, empValue[0], empValue[1], empValue[2], empValue[3], empValue[4], empValue[5]).ToList();
                            break;
                        }

                    case 7:
                        {
                            employesFind = db.Employes.FromSqlRaw(strSQL, empValue[0], empValue[1], empValue[2], empValue[3], empValue[4], empValue[5], empValue[6]).ToList();
                            break;
                        }

                    case 8:
                        {
                            employesFind = db.Employes.FromSqlRaw(strSQL, empValue[0], empValue[1], empValue[2], empValue[3], empValue[4], empValue[5], empValue[6], empValue[7]).ToList();
                            break;
                        }

                    default:
                        {
                            employesFind = db.Employes.FromSqlRaw(strSQL).ToList();
                            break;
                        }
                }

                return employesFind;
            }
        }
    }
}


