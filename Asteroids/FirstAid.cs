using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class FirstAid: BaseObject
    {
        Image img = Asteroids.Properties.Resources.firstaid_1;

        public int Heal { get; set; }
        public Random rand_height = new Random();

        public FirstAid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Heal = 1;
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
                pos.X = Game.Width + size.Width;
        }
    }
}
