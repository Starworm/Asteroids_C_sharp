using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Asteroid : BaseObject
    {
        Image img = Asteroids.Properties.Resources.meteor2_21;

        public int Power { get; set; }
        public Random rand_height = new Random();

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }

        public override void Draw()
        {
            //Game.buffer.Graphics.FillEllipse(Brushes.Goldenrod, pos.X, pos.Y, size.Width, size.Height);
            // Готовое изображение
            Game.buffer.Graphics.DrawImage(img, pos);
        }

        public override void Update()
        {
            pos.X += dir.X;

            if (pos.X < 0)
                pos.X = Game.Width+size.Width;
        }

        // Регенерация астероида в конце экрана на случайной высоте
        public void Regenerate()
        {
            pos.X = Game.Width;
            pos.Y = rand_height.Next(0, Game.Height);
        }

    }
}
