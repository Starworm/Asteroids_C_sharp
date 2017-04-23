using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Bullet: BaseObject
    {
        public Random rand_height = new Random();

        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawRectangle(Pens.OrangeRed, pos.X, pos.Y, size.Width, size.Height);
        }

        public override void Update()
        {
            pos.X += 3;
            //if (pos.X > Game.Width)
            //    pos.X = 0;
        }

        // Регенерация пули в начале экрана на случайной высоте
        public void Regenerate()
        {
            pos.X = 0;            
            pos.Y = rand_height.Next(0, Game.Height);
        }
    }
}
