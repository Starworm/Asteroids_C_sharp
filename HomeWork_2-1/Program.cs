using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Мамалиев С.Ф.
/// <summary>
/// Построить три класса (базовый и 2 потомка), описывающих некоторых работников с почасовой оплатой(один из  потомков)  и фиксированной оплатой(второй потомок).
/// а) Описать в базовом классе абстрактный метод для расчёта среднемесячной заработной платы. Для «повременщиков» формула для расчета такова: 
/// «среднемесячная заработная плата = 20.8  * 8 * почасовую ставку»,для работников с фиксированной  оплатой«среднемесячная  
/// заработная плата = фиксированной месячной оплате».
/// б) Создать на базе абстрактного класса массив сотрудников и заполнить его.
/// в) ** Реализовать интерфейсы для возможности сортировки массива используя Array.Sort().
/// г) *** Реализовать возможность вывода данных с использованием foreach.
/// </summary>

namespace HomeWork_2_1
{

    // Реализация интерфейса для сравнения
    class EmployeeSort : IComparer
    {
        // используем метод сравнения CompareTo: s1.CompareTo(s2)
        public int Compare(object x, object y)
        {
            Employee em1 = (Employee)x;
            Employee em2 = (Employee)y;

            return em1.GetName.CompareTo(em2.GetName);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {                                 
            // Создание списка сотрудников
            List<Employee> employees = new List<Employee>();
            employees.Add(new FullTimeEmployee("Иван", "Full Time", 200));
            employees.Add(new HourEmployee("Дмитрий", "Part Time", 3.5));
            employees.Add(new FullTimeEmployee("Алексей", "Full Time", 150));

            // Расчет зарплаты сотрудника
            foreach (Employee emp in employees)
            {
                Console.WriteLine(emp.name+" получает " + emp.Salary() + " денег в месяц.");
                //Console.WriteLine(emp.name.CompareTo(s2));
            }
            Console.WriteLine();
            
            // Преобразуем в массив для использования метода Array.Sort()
            Employee[] new_emp_arr = employees.ToArray();

            // Сортировка сотрудников по именам
            Array.Sort(new_emp_arr, new EmployeeSort());

            // Вывод данных сотрудников
            foreach (Employee item in new_emp_arr)
            {
                Console.WriteLine(item.name+" работает "+item.workTime + " и получает " + item.Salary()+ " денег в месяц");
            }
            Console.ReadLine();
        }
    }
}
