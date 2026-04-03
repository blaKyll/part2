using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modification
{
    //исходный модуль для расчета ЗП
    public class SalaryCalculator
    {
        //метод расчета ЗП 
        public double CalculateBaseSalary(double hours,double rate)
        {
            return hours * rate;//возращаем произведение часов на ставку
        }
    //метод для расчета ЗП с учетом налога 13%
    public double CalculateNetSalary(double hours,double rate)
        {
            double gross=CalculateBaseSalary(hours,rate);//получаем и записываем переменную зарплату до вычета налога
            double tax = gross * 0.13;//вычисляем и записываем в переменную tax налог 13 %
            return gross - tax;//возращаем чистую зарплату после вычета налога
        }
    }
    public class ModificatedSalaryCalculator : SalaryCalculator//создаем новый класс и наследуем от SalaryCalculator
    {
        //переопределяем метод расчета зарплаты с учетом новых правил
        public new double CalculateNetSalary(double hours,double rate,double bonus = 0)//добавляем параметр премия
        {
            double gross=CalculateBaseSalary(hours,rate);
            gross += bonus;//добавляем премию до вычета налога
            //добавляем систему поэтапного налога
            double tax = 0;
            if (gross <= 25000)
                tax = gross * 0.10;//низкий налог для маленькой ЗП
            else
                tax = 25000 * 0.10 + (gross - 25000) * 0.20;//10 процентов налога с первых 25 тысяч + 25 процентов с 100000
            return gross - tax;//возращаем обновленную чистую зарплату 
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SalaryCalculator oldCalc = new SalaryCalculator();//создаем экземпляр класса
            double oldNet = oldCalc.CalculateNetSalary(160, 250);//считаем зп до модификации 160 часов * 250 рублей
            Console.WriteLine($"Старая версия:{oldNet}");//отображение результатов в консоль
            //тестирование модифициранного модуля 
            ModificatedSalaryCalculator newCalc = new ModificatedSalaryCalculator();//создаем экземпляр модифицированного класса
            double newNet = newCalc.CalculateNetSalary(160, 250, 3000);//считаем ЗП с учетом премии
            Console.WriteLine($"Новая версия:{newNet}");//выводим результаты модифицированного расчета
            //демонстрация обратной совместимости(без премии)
            double noBonus = newCalc.CalculateNetSalary(160, 250);
            Console.WriteLine($"Без премии:{noBonus}");
        }
    }
}
