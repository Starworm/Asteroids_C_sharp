using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2_1
{
    // Базовый абстрактный класс работников и наследование интерфейса IComparable
    abstract class Employee
    {
        public string name;      // Имя
        public string workTime;  // PartTime или FullTime

        public Employee(string name, string workTime)
        {
            this.name = name;
            this.workTime = workTime;
        }

        // Свойство вывода имени рабочего
        public string GetName => name;


        // абстрактный класс расчета ЗП
        abstract public double Salary();                
    }

    // Дочерний класс повременных сотрудников
    class HourEmployee : Employee
    {
        double hourSalary; // Почасовая ставка        

        public HourEmployee(string name, string workTime, double hourSalary) : base(name, workTime)
        {
            this.hourSalary = hourSalary;
        }

        // Переопределение метода расчета ЗП
        public override double Salary()
        {
            return 20.8 * 8 * hourSalary;
        }
    }

    // Дочерний класс постоянных сотрудников
    class FullTimeEmployee : Employee
    {
        double monthSalary;

        public FullTimeEmployee(string name, string workTime, double monthSalary) : base(name, workTime)
        {
            this.monthSalary = monthSalary;
        }

        // Переопределение метода расчета ЗП
        public override double Salary()
        {
            return monthSalary;
        }
    }
}
