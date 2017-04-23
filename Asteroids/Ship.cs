using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    delegate void Message();    

    class Ship: BaseObject
    {
        
        public static event Message MessageDie;

        // Изображение корабля
        Image img = Asteroids.Properties.Resources.spaceship_1;

        public int Power { get; set; }

        public Ship(Point pos, Point dir, Size size): base(pos,dir,size)
        {
            Power = 100;
        }

        public override void Draw()
        {
            // Готовое изображение
            Game.buffer.Graphics.DrawImage(img, pos);
            //Game.buffer.Graphics.FillEllipse(Brushes.Azure, new RectangleF(pos, size));
        }

        public override void Update()
        {
            if (Power <= 0)
                MessageDie?.Invoke();
        }

        public void Up()
        {
            pos.Y -= 4;
        }
        public void Down()
        {
            pos.Y += 4;
        }
        public void Left()
        {
            pos.X -= 4;
        }
        public void Right()
        {
            pos.X += 4;
        }

        public int X => pos.X;
        public int Y => pos.Y;
    }
}
