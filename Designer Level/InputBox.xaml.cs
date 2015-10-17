using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Designer_Level
{
    /// <summary>
    /// Логика взаимодействия для InputBox.xaml
    /// </summary>
    public partial class InputBox : Window
    {
        public InputBox()
        {
            InitializeComponent();
        } 
      
        public int lenghtLevel;

        public void button1_Click(object sender, RoutedEventArgs e)
        {
            try { lenghtLevel = Convert.ToInt32(textBox1.Text); }
            catch (System.Exception ex)
            {
                MessageBox.Show("Некоректно введені дані!", ")", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }            
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
