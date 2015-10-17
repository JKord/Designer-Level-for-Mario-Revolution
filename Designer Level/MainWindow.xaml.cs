using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Win32;

using Levels;


namespace Designer_Level
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    struct Position
    {
        public int x,y;
    }

    public partial class MainWindow : Window
    {
        #region Fields

        Table table;
        Level level;

        Position pozMause;

        InputBox inputBox;
        HelpBox helpBox;

        System.Drawing.Rectangle rectElem = new System.Drawing.Rectangle(0, 0, 0, 0);

        #endregion

        public MainWindow()
        {
           // this.AllowsTransparency = true;
           // this.WindowStyle = new System.Windows.WindowStyle();

            InitializeComponent();

            table = new Table();
            level = new Level(canvas1);
        }        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {           
            table.addItems();
            listView1.DataContext = table.items[0];
            listView2.DataContext = table.items[1];
            listView3.DataContext = table.items[2];

            level.blockTexture = table.elements;
            level.bonusTexture = table.bonus;
            level.gumTexture = table.gum;
        }

        private void Move_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void MouseMove(object sender, MouseEventArgs e)
        {
            pozMause.x = (int)e.GetPosition(canvas1).X;
            pozMause.y = (int)e.GetPosition(canvas1).Y;

        }   
       
        public static int ScrollX = 0;
        public static System.Drawing.Rectangle GetScreenRect(System.Drawing.Rectangle rect)
        {
            System.Drawing.Rectangle screenRect = rect;
            screenRect.Offset(-ScrollX, 0);
            return screenRect;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Save.IsEnabled) 
            {
                MessageBox.Show("Спочатку створіть або відкрийте новий рівень!", "!", 
                                 MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return ;
            }
            try
            {
                if (scrollBar1.Maximum != level.lenghtLevel || inputBox.lenghtLevel != level.lenghtLevel)
                    scrollBar1.Maximum = level.lenghtLevel = inputBox.lenghtLevel;
            }
            catch (System.Exception ex) { }
            try
            {               
                if (tabControl1.SelectedIndex == 0)
                {
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(200 + ScrollX, 200, 40, 40);
                    if (listView1.SelectedIndex == 2)
                    { rect.Width = 41; rect.Height = 184; }
                    if (listView1.SelectedIndex == 3)
                    { rect.Width = 41; rect.Height = 92; }
                    if (listView1.SelectedIndex == 4)
                    { rect.Width = 280; rect.Height = 220; }
                    GObject block = new GObject(level.blockTexture[listView1.SelectedIndex], rect);
                    block.type = table.nameElements[listView1.SelectedIndex];
                    level.blocks.Add(block);
                }
                if (tabControl1.SelectedIndex == 1)
                {
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(300 + ScrollX, 200, 20, 20);
                    GObject bonus = new GObject(level.bonusTexture[listView2.SelectedIndex], rect);
                    bonus.type = table.nameBonus[listView2.SelectedIndex];
                    level.bonus.Add(bonus);
                }
                if (tabControl1.SelectedIndex == 2)
                {
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(400 + ScrollX, 200, 70, 70);
                    GObject gum = new GObject(level.gumTexture[listView3.SelectedIndex], rect);
                    gum.type = table.nameGums[listView3.SelectedIndex];
                    level.gums.Add(gum);
                }
                if (!level.timerLevel.IsEnabled) level.timerLevel.Start();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Спочатку виберіть елемент!",")", MessageBoxButton.OK, MessageBoxImage.Exclamation); 
            }
            //level.Draw();                     
        }            
        
        private void Add_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Save.IsEnabled)
            {
                MessageBox.Show("Спочатку створіть або відкрийте новий рівень!", "!",
                                 MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            try
            {
                if (scrollBar1.Maximum != level.lenghtLevel || inputBox.lenghtLevel != level.lenghtLevel)
                    scrollBar1.Maximum = level.lenghtLevel = inputBox.lenghtLevel;
            }
            catch (System.Exception ex) { }
            try
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(pozMause.x + ScrollX, pozMause.y, 40, 40);
                    if (listView1.SelectedIndex == 2)
                    { rect.Width = 41; rect.Height = 184; }
                    if (listView1.SelectedIndex == 3)
                    { rect.Width = 41; rect.Height = 92; }
                    if (listView1.SelectedIndex == 4)
                    { rect.Width = 280; rect.Height = 220; }
                    GObject block = new GObject(level.blockTexture[listView1.SelectedIndex], rect);
                    block.type = table.nameElements[listView1.SelectedIndex];                    
                    if (!block.Bounds.IntersectsWith(rectElem))
                        level.blocks.Add(block);
                    rectElem = block.Bounds;
                    rectElem.Width = rectElem.Width - rectElem.Width / 10;
                    rectElem.Height = rectElem.Height - rectElem.Height / 10;                   
                }
                if (tabControl1.SelectedIndex == 1)
                {
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(pozMause.x + ScrollX, pozMause.y, 20, 20);
                    GObject bonus = new GObject(level.bonusTexture[listView2.SelectedIndex], rect);
                    bonus.type = table.nameBonus[listView2.SelectedIndex];                    
                    if (!bonus.Bounds.IntersectsWith(rectElem))
                        level.bonus.Add(bonus);
                    rectElem = bonus.Bounds;
                    rectElem.Width = rectElem.Width - rectElem.Width / 10;
                    rectElem.Height = rectElem.Height - rectElem.Height / 10;
                }
                if (tabControl1.SelectedIndex == 2)
                {
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(pozMause.x + ScrollX, pozMause.y, 70, 70);
                    GObject gum = new GObject(level.gumTexture[listView3.SelectedIndex], rect);
                    gum.type = table.nameGums[listView3.SelectedIndex];                  
                    if (!gum.Bounds.IntersectsWith(rectElem))
                        level.gums.Add(gum);
                    rectElem = gum.Bounds;
                    rectElem.Width = rectElem.Width - rectElem.Width / 10;
                    rectElem.Height = rectElem.Height - rectElem.Height / 10;
                }
                if (!level.timerLevel.IsEnabled) level.timerLevel.Start();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Спочатку виберіть елемент!", ")", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }       
        }        

        #region Menu Item

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Save.IsEnabled = true;
            inputBox = new InputBox();
            inputBox.Show();
            level.blocks = new List<GObject>();
            level.gums = new List<GObject>();
            level.bonus = new List<GObject>();
            canvas1.Children.Clear();       
    
            scrollBar1.Maximum = level.lenghtLevel;
            scrollBar1.Value = 0;            
        }  
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Level*(*.lv)|*.lv|Всі файли (*.*)|*.*";
            Save.IsEnabled = true;
            if (fileDialog.ShowDialog() == true)
            {                
                level.fileName = fileDialog.FileName;

                level.blocks = new List<GObject>();
                level.gums = new List<GObject>();
                level.bonus = new List<GObject>();
                canvas1.Children.Clear();  

                scrollBar1.Value = 0;
                level.CreateLevel();               
                scrollBar1.Maximum = level.lenghtLevel;
                if (!level.timerLevel.IsEnabled) level.timerLevel.Start();               
            }
          /*  TextBlock txt1 = new TextBlock();
            txt1.FontSize = 14;
            txt1.Text = "" + scrollBar1.Maximum;
            Canvas.SetTop(txt1, 200);
            Canvas.SetLeft(txt1, 10);
            canvas1.Children.Add(txt1);*/
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Level*(*.lv)|*.lv|Всі файли (*.*)|*.*";
            if (fileDialog.ShowDialog() == true)
            {
                level.SaveLevel(fileDialog.FileName);
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

        private void help_Click(object sender, RoutedEventArgs e)
        {
            helpBox =new HelpBox();
            helpBox.Show();
        }        

        #endregion

        private void scrollBar1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScrollX = (int) scrollBar1.Value;
            //canvas1.Children.Clear();
           // level.Draw();            
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }    
        private void Designer_Level_Closed(object sender, EventArgs e)
        {
            level.timerLevel.Stop();
            try
            {
                if (inputBox.IsVisible) inputBox.Close();              
            }
            catch (System.Exception ex) { }

            try
            {
                if (helpBox.IsVisible) helpBox.Close();                
            }
            catch (System.Exception ex) { }    
        }                    
        
    }
}
