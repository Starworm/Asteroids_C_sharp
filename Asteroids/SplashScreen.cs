using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Asteroids
{
    static class SplashScreen
    {
        // Двойная буферизация - вывод в буфер, а затем на форму
        static BufferedGraphicsContext context;
        static public BufferedGraphics buffer;
        // Задний фон
        static Image img = Asteroids.Properties.Resources.PANORAMA;//Image.FromFile("images\\Panorama.JPG");
        static BaseObject[] objs;
        // Автоматические свойства Ширина и высота игрового поля - свойства с полями внутри
        static public int Width { get; set; }
        static public int Height { get; set; }

        static SplashScreen()
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
            /* Связываем буфер в памяти с графическим объектом
            * для того, чтобы рисовать в буфере */
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Button btn_New = new Button();
            Button btn_Records = new Button();
            Button btn_Exit = new Button();

            btn_New.Text = "New Game";

            btn_Records.Text = "Records";
            btn_Records.Location = new System.Drawing.Point(75, 0);

            btn_Exit.Text = "Exit Game";
            btn_Exit.Location = new System.Drawing.Point(150, 0);

            form.Controls.Add(btn_New);
            form.Controls.Add(btn_Records);
            form.Controls.Add(btn_Exit);


            Load();
            // Таймер для вызова Draw и Update
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Start();
            timer.Tick += Timer_Tick;
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
            foreach (BaseObject obj in objs)
                obj.Draw();
            buffer.Graphics.DrawString("Stanislav Mamaliev", SystemFonts.DefaultFont, Brushes.PaleGoldenrod, 700, 10);
            buffer.Render();
        }

        static public void Update()
        {
            foreach (BaseObject obj in objs)
            {
                obj.Update();
            }
        }

        static public void Load()
        {
            Random rnd1 = new Random();
            Random rnd2 = new Random();
            objs = new Asteroid[20];
            //objs = new BaseObject[30];
            //for (int i = 0; i < objs.Length / 2; i++)
            //    objs[i] = new BaseObject(
            //        new Point(600, i * 20),
            //        new Point(15 - i, 15 - i),
            //        new Size(rnd1.Next(5, 20), rnd2.Next(5, 20))
            //        );

            for (int i = 0; i < objs.Length; i++)
                objs[i] = new Asteroid(
                    new Point(300, i + 20),
                    new Point(15 + i, 15 - i),
                    new Size(rnd1.Next(5, 20), rnd2.Next(5, 20))
                    );
        }
    }
}
