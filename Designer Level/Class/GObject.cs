using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Designer_Level
{    
    class GObject
    {
        #region Fields

        ImageBrush Sprite { get; set; }
        public System.Drawing.Rectangle rect;       
        public string type;
        public bool Visible { get; set; } 

        System.Windows.Shapes.Rectangle img = new System.Windows.Shapes.Rectangle();

        #endregion

        public GObject(ImageSource sprite)
        {
            Sprite = new ImageBrush();
            Sprite.ImageSource = sprite;
            rect = new System.Drawing.Rectangle(0, 0, 1, 1); 
            Visible = true;                   
        }
        public GObject(ImageSource sprite,System.Drawing.Rectangle _rect)
        {
            Sprite = new ImageBrush();
            Sprite.ImageSource = sprite;
            rect = _rect;  
            Visible = true;                    
        }       
        
        public System.Drawing.Rectangle Bounds 
        {
            get
            {
                return rect;
            }
        }      
        public void Draw(Canvas canvas)
        {
            System.Drawing.Rectangle tmpRect = MainWindow.GetScreenRect(rect);
            if (Visible && tmpRect.X > -tmpRect.Height && tmpRect.X + tmpRect.Height < 850)
            {                   
                img.Fill = Sprite;
                img.Width = tmpRect.Width;
                img.Height = tmpRect.Height;
                Canvas.SetLeft(img, tmpRect.X);
                Canvas.SetTop(img, tmpRect.Y);                              
                canvas.Children.Add(img);     
            }               
        }       

    }
}
