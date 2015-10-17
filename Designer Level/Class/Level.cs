using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Controls;
using Designer_Level;
using System.Windows.Threading;
using System.Windows.Input;

namespace Levels
{
    class Level
    {
        #region Fields

        public int currentLevel = 1;
        public int maxLevel = 3;
        public int lenghtLevel = 100;
        public String fileName;      
        Canvas canvas = new Canvas();

        public DispatcherTimer timerLevel;

        public ImageSource[] blockTexture;
        public ImageSource[] bonusTexture;
        public ImageSource[] gumTexture;
  
        public List<GObject> blocks;
        public List<GObject> bonus;
        public List<GObject> gums;

        #endregion

        public Level(Canvas _canvas)
        {
            blocks = new List<GObject>();
            bonus = new List<GObject>();
            gums = new List<GObject>();
            canvas = _canvas;

            timerLevel = new DispatcherTimer(DispatcherPriority.Loaded);          
            timerLevel.Interval = TimeSpan.FromMilliseconds(1);
            timerLevel.Tick += new EventHandler(delegate(object s, EventArgs a)
            {
                Update(); Draw();
            });          
        }
        public Level(Canvas _convas,int _currentLevel, int _maxLevel)
        {
            currentLevel = _currentLevel;
            maxLevel = _maxLevel;

            blocks = new List<GObject>();
            bonus = new List<GObject>();
            gums = new List<GObject>();
            canvas = _convas;
        }
       
        public void Load()
        {            
        }
        public void CreateLevel()
        {          
            string[] s = File.ReadAllLines(fileName);
            //File.Encrypt(st + currentLevel + ".lv");
            //File.Decrypt(st + currentLevel + ".lv");      
     
            lenghtLevel = s[0].Length * 40;
            int x = 0;
            int y = 0;
            foreach (string str in s)
            {
                foreach (char c in str)
                {
                    Rectangle rect = new Rectangle(x, y, 40, 40);
                    switch (c)
                    {
                        #region Block
                        case 'X':
                            {
                                GObject block = new GObject(blockTexture[0], rect);
                                block.type = "block1";
                                blocks.Add(block);
                            } break;
                        case 'Y':
                            {
                                GObject block = new GObject(blockTexture[1], rect);
                                block.type = "block2";
                                blocks.Add(block);
                            } break;
                        case 'B':
                            {
                                GObject block = new GObject(blockTexture[2], new Rectangle(x, y, 41, 184));
                                block.type = "bamboo";
                                blocks.Add(block);
                            } break;
                        case 'S':
                            {
                                GObject block = new GObject(blockTexture[3], new Rectangle(x, y, 41, 92));
                                block.type = "bambootopSliceHorizontal";
                                blocks.Add(block);
                            } break;
                        case 'H':
                            {
                                GObject block = new GObject(blockTexture[4], new Rectangle(x, y, 280, 220));
                                block.type = "hause1";
                                blocks.Add(block);
                            } break;
                        #endregion
                        #region Bonus
                        case 'a':
                            {
                                GObject bon = new GObject(bonusTexture[0], new Rectangle(x, y,
                                                                 rect.Width / 2, rect.Height / 2));
                                bon.type = "apple";
                                bonus.Add(bon);
                            } break;
                        case 'b':
                            {
                                GObject bon = new GObject(bonusTexture[1], rect);
                                bon.type = "beetle";
                                bonus.Add(bon);
                            } break;
                        case 'h':
                            {
                                GObject bon = new GObject(bonusTexture[2], new Rectangle(x, y,
                                                                 rect.Width, rect.Height / 2));
                                bon.type = "hand";
                                bonus.Add(bon);
                            } break;
                        case 'c':
                            {
                                GObject bon = new GObject(bonusTexture[3], new Rectangle(x, y,
                                                                 rect.Width / 2, rect.Height / 2));
                                bon.type = "heart";
                                bonus.Add(bon);
                            } break;
                        case 'm':
                            {
                                GObject bon = new GObject(bonusTexture[4], new Rectangle(x, y,
                                                                 rect.Width / 2, rect.Height));
                                bon.type = "men";
                                bonus.Add(bon);
                            } break;
                        case 'r':
                            {
                                GObject bon = new GObject(bonusTexture[5], new Rectangle(x, y,
                                                                 rect.Width / 2, rect.Height / 2));
                                bon.type = "ruby";
                                bonus.Add(bon);
                            } break;
                        case 'A':
                            {
                                GObject bon = new GObject(bonusTexture[6], new Rectangle(x, y,
                                                                 rect.Width / 2, rect.Height / 2));
                                bon.type = "box1";
                                bonus.Add(bon);
                            } break;
                        #endregion
                        #region Gum

                        case 'M':
                            {
                                GObject gum = new GObject(gumTexture[0], new Rectangle(x, y, 70, 70));
                                gum.type = "menper";
                                gums.Add(gum);
                            } break;
                        case 'R':
                            {
                                GObject gum = new GObject(gumTexture[1], new Rectangle(x, y, 70, 70));
                                gum.type = "robot";
                                gums.Add(gum);
                            } break;
                        case 'G':
                            {
                                GObject gum = new GObject(gumTexture[4], new Rectangle(x, y, 70, 70));
                                gum.type = "monkey";
                                gums.Add(gum);
                            } break;
                        case 'C':
                            {
                                GObject gum = new GObject(gumTexture[3], new Rectangle(x, y, 70, 70));
                                gum.type = "handG";
                                gums.Add(gum);

                            } break;
                        case 'K':
                            {
                                GObject gum = new GObject(gumTexture[5], new Rectangle(x, y, 70, 70));
                                gum.type = "rock";
                                gums.Add(gum);

                            } break;
                        case 'g':
                            {
                                GObject gum = new GObject(gumTexture[2], new Rectangle(x, y, 70, 70));
                                gum.type = "ghost";
                                gums.Add(gum);
                            } break;
                        #endregion
                    }

                    x += 40;
                }
                x = 0;
                y += 40;
            }
         }

        char translationInABC(GObject gobj)
        {
            switch (gobj.type)
            {
                case "block1": return 'X';
                case "block2": return 'Y';
                case "bamboo": return 'B';
                case "bambootopSliceHorizontal": return 'S';
                case "hause1": return 'H';
                case "apple": return 'a';
                case "beetle": return 'b';
                case "hand": return 'h';
                case "heart": return 'c';
                case "men": return 'm';
                case "ruby": return 'r';
                case "box1": return 'A';
                case "menper": return 'M';
                case "robot": return 'R';
                case "monkey": return 'G';
                case "handG": return 'C';
                case "rock": return 'K';
                case "ghost": return 'g';
                default : return' ';
            }

        }
       
        public void SaveLevel(String fileName)
        {
            
            //File.Encrypt(st + currentLevel + ".lv");
            //File.Decrypt(st + currentLevel + ".lv");   
            char[,] s = new char[1000, lenghtLevel / 40];
            try
            {
                /*for (int i = 0; i < 25; i++)
                    for (int j = 0; j < lenghtLevel / 40; j++)
                        s[j, i] = '0';*/
                foreach (GObject block in blocks)
                {
                    if (block.Visible) s[block.Bounds.X / 40, block.Bounds.Y / 40] = translationInABC(block);
                }
                foreach (GObject bon in bonus)
                {
                    if(bon.Visible) s[bon.Bounds.X / 40, bon.Bounds.Y / 40] = translationInABC(bon);
                }
                foreach (GObject gum in gums)
                {
                   if(gum.Visible) s[gum.Bounds.X / 40, gum.Bounds.Y / 40] = translationInABC(gum);
                }

                FileStream f = new FileStream(fileName, FileMode.Create);
                for (int i = 0; i < 25; i++)
                {
                    for (int j = 0; j < lenghtLevel / 40; j++)
                    {
                        f.WriteByte((Byte)s[j, i]);
                    }
                    f.WriteByte(Convert.ToByte('\n'));                    
                }
                f.Dispose(); 
            }
            catch (System.Exception ex) 
            {
                MessageBox.Show("Помилка в 'Save Level'","Error",MessageBoxButton.OK,MessageBoxImage.Error); 
            }
                      
       }   
        
        public void Update()
        {        
        }     
       
        public void Draw()
        {
            canvas.Children.Clear();    
            Rectangle rect = new Rectangle(0,0,17,17);
            Rectangle tmpobj;
            
            foreach (GObject block in blocks)
            {
                if (block.Visible)
                {
                    rect.X = (int)Mouse.GetPosition(canvas).X;
                    rect.Y = (int)Mouse.GetPosition(canvas).Y;
                    tmpobj = MainWindow.GetScreenRect(block.Bounds);
                    if (tmpobj.IntersectsWith(rect) && (Keyboard.GetKeyStates(Key.LeftShift) & KeyStates.Down) > 0)
                        block.rect = new Rectangle(rect.X + MainWindow.ScrollX - block.rect.Width / 2,
                                      rect.Y - block.rect.Height / 2, block.rect.Width, block.rect.Height);
                    if (tmpobj.IntersectsWith(rect) && (Keyboard.GetKeyStates(Key.Delete) & KeyStates.Down) > 0)
                        block.Visible = false;
                    block.Draw(canvas);
                }
            }
            foreach (GObject bon in bonus)
            {
                if (bon.Visible)
                {
                    rect.X = (int)Mouse.GetPosition(canvas).X;
                    rect.Y = (int)Mouse.GetPosition(canvas).Y;
                    tmpobj = MainWindow.GetScreenRect(bon.Bounds);
                    if (tmpobj.IntersectsWith(rect) && (Keyboard.GetKeyStates(Key.LeftShift) & KeyStates.Down) > 0)
                        bon.rect = new Rectangle(rect.X + MainWindow.ScrollX - bon.rect.Width / 2,
                                    rect.Y - bon.rect.Height / 2, bon.rect.Width, bon.rect.Height);
                    if (tmpobj.IntersectsWith(rect) && (Keyboard.GetKeyStates(Key.Delete) & KeyStates.Down) > 0)
                        bon.Visible = false;
                    bon.Draw(canvas);
                }
            }
            foreach (GObject gum in gums)
            {
                if (gum.Visible)
                {
                    rect.X = (int)Mouse.GetPosition(canvas).X;
                    rect.Y = (int)Mouse.GetPosition(canvas).Y;
                    tmpobj = MainWindow.GetScreenRect(gum.Bounds);
                    tmpobj.Width += 10;
                    tmpobj.Height += 10;
                    if (tmpobj.IntersectsWith(rect) && (Keyboard.GetKeyStates(Key.LeftShift) & KeyStates.Down) > 0)
                        gum.rect = new Rectangle(rect.X + MainWindow.ScrollX - gum.rect.Width / 2,
                                    rect.Y - gum.rect.Height / 2, gum.rect.Width, gum.rect.Height);
                    if (tmpobj.IntersectsWith(rect) && (Keyboard.GetKeyStates(Key.Delete) & KeyStates.Down) > 0)
                        gum.Visible = false;
                    gum.Draw(canvas);
                }
            }            
        }       
    }
}
