using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Мамалиев С.Ф.
/// <summary>
/// а) Добавить в игру “Астероиды” ведение журнала в консоль
/// б)* и в файл.
/// 2. Добавьте аптечки, которые добавляют энергии.
/// 3. Добавить подсчет очков за сбитые астероиды.
/// 4.*Добавьте в пример Lesson3 обобщенный делегат
/// </summary>


// Создаем шаблон приложения, где подключаем модули
namespace Asteroids
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();

            form.Width = 800;
            form.Height = 600;
            // Запуск Game
            Game.Init(form);

            // Запуск SplashScreen
            //SplashScreen.Init(form);
            form.Show();
            //SplashScreen.Draw();
            Game.Draw();
            Application.Run(form);
        }
    }
}
