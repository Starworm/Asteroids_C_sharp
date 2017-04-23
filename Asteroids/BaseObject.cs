using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{    
    // Класс базового объекта
    abstract class BaseObject: ICollision
    {
        protected Point pos;
        protected Point dir;
        protected Size size;

        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        public Rectangle rect => new Rectangle(pos,size);

        public bool Collision(ICollision obj)
        {
            if (obj == null) return false;
            if (obj.rect.IntersectsWith(this.rect))
                return true;
            else
                return false;
        }

        // Отображение базового объекта
        public abstract void Draw();

        // Обновление положения объектов
        public abstract void Update();
    }
}
