using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadderDrawing
{
    public class DrawArea
    {
        public int x;
        public int y;
        public int width;
        public int height;

        int lastX = 0, lastY = 0;
        int remX = 0, remY = 0;

        public List<DrawArea> Areas = new List<DrawArea>();

        public DrawArea(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
            remX = width;
            remY = height;
            lastX = x;
            lastY = y;
        }

        public DrawArea SplitX(int xpercentage, int ypercentage)
        {
            int newwidth = (this.width * xpercentage) / 100;
            int newheight = (this.height * ypercentage) / 100;

            if (ypercentage == 0)
                newheight = remY;
            else
                remY -= newwidth;

            if (xpercentage == 0)
                newwidth = remX;
            else
                remX -= newwidth;

            DrawArea area = new DrawArea(lastX, lastY, newwidth, newheight);
            Areas.Add(area);
            lastX += newwidth;

            return area;
        }

        public DrawArea SplitY(int xpercentage, int ypercentage)
        {
            int newwidth = (this.width * xpercentage) / 100;
            int newheight = (this.height * ypercentage) / 100;

            if (ypercentage == 0)
                newheight = remY;
            else
                remY -= newheight;

            if (xpercentage == 0)
                newwidth = remX;
            else
                remY -= newheight;

            DrawArea area = new DrawArea(lastX, lastY, newwidth, newheight);
            Areas.Add(area);
            lastY += newheight;

            return area;
        }

        public void Fill(Brush brush, Graphics graphics)
        {
            Rectangle rectangle = new Rectangle(this.x, this.y, this.width, this.height);
            graphics.FillRectangle(brush, rectangle);
        }

        public void Border(Pen pen, Graphics graphics)
        {
            Rectangle rectangle = new Rectangle(this.x, this.y, this.width, this.height);
            graphics.DrawRectangle(pen, rectangle);
        }

        public void DrawString(int align, string str, Brush brush, Font font, Graphics graphics)
        {
            int xAxis = 0;
            if (align == 0)
                xAxis = x + 2;
            else if (align == 1)
            {
                SizeF sz = graphics.MeasureString(str, font, 0, StringFormat.GenericTypographic);
                xAxis = x + (int)((width / 2) - (sz.Width / 2));
            }

            graphics.DrawString(str, font, brush, new PointF(xAxis, this.y + ((this.height / 2) - (font.Size))), StringFormat.GenericTypographic);
        }

        public void DrawConnector(DrawArea area, Pen pen, Graphics graphics)
        {
            if (area != null)
            {
                int startx = 0;
                int starty = 0;

                int endx = 0;
                int endy = 0;

                if (area.x > this.x)
                {
                    startx = area.x;
                    starty = area.y + (area.height / 2);

                    endx = this.x + area.width;
                    endy = this.y + (this.height / 2);
                }
                else
                {
                    startx = this.x;
                    starty = this.y + (this.height / 2);

                    endx = area.x;
                    endy = area.y + (area.height / 2);
                }

                graphics.DrawLine(pen, startx, starty, endx, endy);
            }
        }

    }
}
