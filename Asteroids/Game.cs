using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Asteroids
{
    delegate void GameMessage();

    static class Game
    {
        public delegate void GameMessage(string path, string text);

        static Bullet bullet; // Пуля
        static Asteroid[] asteroids; // Массив астероидов
        static FirstAid[] firstAids; // Массив аптечек
        static FirstAid firstAid; // Аптечка
        //static double delayTime; // Время минимальной задержки появления аптечки
        //static double realDelay; // Время реальной задержки появления
        static Ship ship;
        static Timer timer = new Timer();
        static string path = @"h:\С_sharp_projects\C_sharp_lev2\Asteroids_C_sharp\Asteroids\bin\log.txt";

        static BaseObject[] stars; // Массив звезд
        // Двойная буферизация - вывод в буфер, а затем на форму
        static BufferedGraphicsContext context;
        static public BufferedGraphics buffer;

        static Random rnd1 = new Random();
        static Random rnd2 = new Random();

       
        // Задний фон
        static Image img = Asteroids.Properties.Resources.StarField;
        
        // Автоматические свойства Ширина и высота игрового поля - свойства с полями внутри
        static public int Width { get; set; }
        static public int Height { get; set; }

        static Game()
        {
        }

        static public void Init(Form form)
        {                        
            // Графическое устройство для вывода графики
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics(); // Создаем объект поверхности рисования и связываем его с формой
            Width = form.Width;
            Height = form.Height;

            // обработка исключения "Размер экрана больше 1000 пикселей или меньше 0"
            try
            {
                /* Связываем буфер в памяти с графическим объектом
                 * для того, чтобы рисовать в буфере */
                buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
                if (Width > 1000 || Height > 1000 || Width <= 0 || Height <= 0)
                    throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Размеры экрана должны быть от 0 до 1000 пикселей");
            }
            Load();
            
            // Таймер для вызова Draw и Update            
            timer.Interval = 100;            
            timer.Tick += Timer_Tick;
            form.KeyDown += Form_KeyDown;
            
            timer.Start();

            // Таймер для вызова генерации аптечек
            //timer.Interval = 5000;
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                bullet = new Bullet(new Point(ship.X + 63, ship.Y + 20), new Point(4, 0), new Size(4, 1));
            if (e.KeyCode == Keys.Up)
                ship.Up();
            if (e.KeyCode == Keys.Down)
                ship.Down();
            if (e.KeyCode == Keys.Left)
                ship.Left();
            if (e.KeyCode == Keys.Right)
                ship.Right();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        static public void Draw()
        {
            // Проверяем вывод графики
            buffer.Graphics.DrawImage(img, 0, 0);
            buffer.Graphics.DrawString(ship.Power.ToString(), SystemFonts.DefaultFont, Brushes.AliceBlue, 0, 0);

            foreach (BaseObject obj in stars)
                obj.Draw();
            foreach (BaseObject obj in asteroids)
                obj.Draw();
            //foreach (BaseObject obj in firstAids)
            //    obj.Draw();
            firstAid.Draw();

            bullet?.Draw();
            ship.Draw();
            buffer.Render();
        }

        static public void Update()
        {
            foreach (BaseObject obj in stars)
                obj.Update();
            foreach (Asteroid obj in asteroids)
            {
                obj.Update();
                
                // При столкновении...
                if (obj.Collision(bullet))
                {
                    Console.WriteLine("Crash!");
                    string text = "Астероид сбит";
                    PrintF(path, text); // Запись в файл
                    obj.Regenerate();   // ... и астероида
                }
                if (obj.Collision(ship))
                {
                    Console.WriteLine("Crash!!!!");
                    string text = "Астероид попал в корабль"; // Запись в файл
                    PrintF(path, text);
                    ship.Power--;
                    obj.Regenerate();    // ... и астероида
                }
                bullet?.Update();
            }


            firstAid.Update();

            // При столкновении...
            if (firstAid.Collision(ship))
            {
                Console.WriteLine("Аптечка взята");
                string text = "Аптечка взята"; // Запись в файл
                PrintF(path, text);
                ship.Power += firstAid.Heal; // Увеличение здоровья                     
            }
                    //bullet?.Update();
        }

        static public void Load()
        {

            // Генерация звезд
            stars = new BaseObject[15];
            for (int i = 0; i < stars.Length; i++)
            {
                int r1 = rnd2.Next(5, 50);
                stars[i] = new Star(
                        new Point(800, Game.rnd2.Next(0, Game.Height)),
                        new Point(-r1/5, r1/5),
                        new Size(r1, r1)
                        );
            }
            ship = new Ship(new Point(0, 200), new Point(5, 0), new Size(10, 10));
            //bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            Ship.MessageDie += Ship_MessageDie;

            // Генерация астероидов
            asteroids = new Asteroid[40];
            for (int i = 0; i < asteroids.Length; i++)
            {
                int r = rnd1.Next(5, 50);
                asteroids[i] = new Asteroid(
                        new Point(400, Game.rnd1.Next(0, Game.Height)),
                        new Point(-r, r),
                        new Size(r, r)
                        );
            }
            //bullet = new Bullet(new Point(0, 400), new Point(5, 0), new Size(5, 2));

            // Генерация аптечки
            int r2 = rnd1.Next(5, 50);
            firstAid = new FirstAid(
                        new Point(400, Game.rnd1.Next(0, Game.Height)),
                        new Point(-r2, r2),
                        new Size(r2, r2)
                        );
        }

        private static void Ship_MessageDie()
        {
            timer.Stop();
        }

        // Метод, записывающий данные в файл
        public static void PrintF(string path, string text)
        {
            // Путь сохранения            
            try
            {                
                using (StreamWriter sw = File.AppendText(path))
                {                                        
                    sw.WriteLine(text); // Записываем строку в файл                    
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
