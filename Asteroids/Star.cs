using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Star : BaseObject
    {
        public Image img { get; set; }
        public Random r = new Random();

        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
                
        // Метод выбора звезды
        public void ChooseStar()
        {
            img = Asteroids.Properties.Resources.star1;
        }

        public override void Draw()
        {
            // Звезды
            ChooseStar();
            Game.buffer.Graphics.DrawImage(img, pos);
        }

        public override void Update()
        {

            pos.X += dir.X;

            if (pos.X < 0)
                pos.X = Game.Width + size.Width;

            //pos.X -= dir.X;
            //if (pos.X < 0)
            //    pos.X = Game.Width+size.Width;
            //else if (pos.X > Game.Width)
            //    pos.X = size.Width;
        }
    }
}
