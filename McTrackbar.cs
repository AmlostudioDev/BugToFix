using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;

namespace MCPU.Components
{
    public class McTrackbar : Control
    {
        //----Settings----//
        //--Design--//
        int black_borders_range = 3;
        int Line1_range = 3;
        int Line2_range = 5;
        int Line2_range2 = 3;
        //---------//     
        //--Variables--//
        public int Valuetoshow;
        public int ValueToShow { get { return Valuetoshow; } set { Valuetoshow = value; Invalidate(); } }
        private PointF BarPosition;
        private PointF Barposition 
        { 
            get { return BarPosition; } 
            set 
            {
                if (value.X + BarSize + 1 > Width || value.X < 1) { }
                else { BarPosition = value; Invalidate(); }
            } 
        }   
        private bool MouseInside = false;
        private bool Mousedown = false;
        private Point LastMousePosition;
        public List<KeyValuePair<int, string>> Data = new List<KeyValuePair<int, string>>();
        //-----------//
        public float ValuePercent { get { return (Value - ValueMin) / (ValueMax - ValueMin) * 100; } }
        int BarSize { get { return 20 + (black_borders_range * 2); } set { BarSize = value; } }
        public float Value { get; set; } //????
        public float ValueMin { get; set; } //30
        public float ValueMax { get; set; } //90
        private float BarPercentToValue { get { return Convert.ToInt32(ValueMin + ((ValueMax - ValueMin) * (BarPercent / 100))); } }
        public float BarPercent { get { return (BarPosition.X + BarSize / 2) / Width * 100; } }  
        //----------------//
        public McTrackbar(Size Size,int ValueMin, int ValueMax, int Value, List<KeyValuePair<int,string>> Data)
        {
            this.ForeColor = Color.White;
            this.DoubleBuffered = true;

            this.ValueMin = ValueMin;
            this.ValueMax = ValueMax;
            this.Value = Value;
            this.Data = Data;
            this.Size = Size;
            

            float BarPosX = Width / (100 / ValuePercent) - (BarSize / 2);
            Barposition = new PointF(BarPosX, 0);
        }
       
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (MouseInside)
            {
                MouseInside = false;
                Invalidate();
            }
        }
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (MouseInside)
            {
                LastMousePosition = this.PointToClient(Cursor.Position);
                Mousedown = true;
            }
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            Mousedown = false;
        }
        private void CheckMouseInsideBar()
        {
            var relativePoint = this.PointToClient(Cursor.Position);
            if (relativePoint.X >= BarPosition.X && relativePoint.X <= BarPosition.X + BarSize &&
                relativePoint.Y >= BarPosition.Y && relativePoint.Y <= BarPosition.Y + Height)
            {
                MouseInside = true;
                Invalidate();
            }
            else
            {
                if (MouseInside)
                {
                    MouseInside = false;
                    Mousedown = false;
                    Invalidate();
                }
            }
        }
        private void MoveBarToLeft()
        {
            var newMousePosition = this.PointToClient(Cursor.Position);
            if (newMousePosition.X < LastMousePosition.X)
            {
                float deplacement = LastMousePosition.X - newMousePosition.X;
                LastMousePosition = this.PointToClient(Cursor.Position);

                if (BarPosition.X - deplacement /*- BarSize / 2*/ >= 1)
                {
                    Value = BarPercentToValue;
                    Barposition = new PointF(BarPosition.X - deplacement, 0);
                } 
            }
        }
        private void MoveBarToRight()
        {
            var newMousePosition = this.PointToClient(Cursor.Position);
            if (newMousePosition.X > LastMousePosition.X)
            {
                float deplacement = newMousePosition.X - LastMousePosition.X;
                LastMousePosition = this.PointToClient(Cursor.Position);

                if (BarPosition.X + deplacement + BarSize / 2 <= Width)
                {
                    Value = BarPercentToValue;
                    Barposition = new PointF(BarPosition.X + deplacement, 0);
                }
                
            }
        }
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
            CheckMouseInsideBar();
            if(Mousedown)
            {
                MoveBarToLeft();
                MoveBarToRight();
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            RectangleF black_borders = new RectangleF {
                Location = new Point(1, 1),
                Size = new Size(Width - black_borders_range - 1, Height - black_borders_range)
            };

            RectangleF Bar = new RectangleF
            {
                Location = new PointF(BarPosition.X, BarPosition.Y),
                Size = new Size(BarSize, Height)
            };
            RectangleF Bar_black_borders = new RectangleF
            {
                Location = new PointF(BarPosition.X, BarPosition.Y),
                Size = new SizeF(Bar.Width - 1, Bar.Height - 1)
            };
            RectangleF Line1 = new RectangleF
            {
                Location = new PointF(Bar.Location.X + black_borders_range, Bar.Location.Y + black_borders_range),
                Size = new SizeF(Bar.Width - black_borders_range, Line1_range)
            };
            RectangleF Line1_2 = new RectangleF
            {
                Location = new PointF(Bar.Location.X + black_borders_range, Bar.Location.Y + black_borders_range + Line1_range),
                Size = new SizeF(Line1_range, Bar.Height - Line1_range)
            };

            RectangleF Line2 = new RectangleF
            {
                Location = new PointF(Bar.Location.X + black_borders_range, Bar.Location.Y + Bar.Height - black_borders_range - Line2_range),
                Size = new SizeF(Bar.Width - black_borders_range, Line2_range)
            };
            RectangleF Line2_2 = new RectangleF
            {
                Location = new PointF(Bar.Location.X + Bar.Width - black_borders_range - Line2_range2, Bar.Location.Y + black_borders_range - 1),
                Size = new SizeF(Line2_range, Bar.Height - Line2_range2)
            };
            base.OnPaint(e);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100 * 255 / 100, 43, 43, 43)), 0, 0, Width, Height);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100 * 255 / 100, 111, 111, 111)), Bar);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60 * 255 / 100, 160, 160, 160)), Line1);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60 * 255 / 100, 160, 160, 160)), Line1_2);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(85 * 255 / 100, 86, 86, 86)), Line2);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(85 * 255 / 100, 86, 86, 86)), Line2_2);

            if (MouseInside)
            {
                //Draw MouseEnter
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60 * 255 / 100, 124, 134, 190)), Bar);
            }

            e.Graphics.DrawRectangles(new Pen(new SolidBrush(Color.Black), black_borders_range), new RectangleF[] { Bar_black_borders });
            e.Graphics.DrawRectangles(new Pen(new SolidBrush(Color.Black), black_borders_range), new RectangleF[] { black_borders });

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            //Search for Text to draw
            bool textwasdraw=false;
            string DrawText = "";
            if (Data != null)
            {
                for (int x = 0; x < Data.Count(); x++)
                {
                    if (Value == Data[x].Key)
                    {
                        DrawText = "Fov " + Data[x].Value;
                        textwasdraw = true;
                        break;
                    }
                }
            }
            if(!textwasdraw)
            {
                DrawText = "Fov " + Value.ToString();      
            }
            if(ValueToShow == 1)
            {
                DrawText = "Fov " + BarPercent.ToString() + " %";
            }
            TextRenderer.DrawText(e.Graphics, DrawText, Font, new Point(Width, Height / 2), ForeColor, flags);
        }
    }
}
