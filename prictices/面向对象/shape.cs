using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceToObj
{
    class Shape
    {
        protected int width;
        protected int height;
        
        public void setWidth(int w)
        {
            width = w;
        }
        public void setHeight(int h)
        {
            height = h;
        }
    }
    class Rectangle: Shape
    {
        public int getArea()
        {
            return width * height;
        }
    }
}
