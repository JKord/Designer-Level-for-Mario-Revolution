using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;

namespace Designer_Level
{
    public class ItemsTable : INotifyPropertyChanged
    {
        #region Fields

        private string _name = "";
        private ImageSource _image;    
    
        #endregion

        public ImageSource image
        {
            get { return _image; }
            set
            {
                if (value != _image)
                {
                    _image = value;
                    OnNotifyPropertyChanged("image");
                }
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnNotifyPropertyChanged("Name");
                }
            }
        }
        #region Notify Property Changed Members

        private void OnNotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
     }
    public class Table
    {
        #region Fields

        public ObservableCollection<ItemsTable> []items;

        public ImageSource[] bonus;
        public ImageSource[] gum;
        public ImageSource[] elements;

        #endregion

        public Table()
        {
            items = new ObservableCollection<ItemsTable>[3];
            items[0] = new ObservableCollection<ItemsTable>();
            items[1] = new ObservableCollection<ItemsTable>();
            items[2] = new ObservableCollection<ItemsTable>();

            bonus = new ImageSource[7];
            gum = new ImageSource[6];
            elements = new ImageSource[5];
        }
        ImageSource ISC(String name)
        {
            return (ImageSource)new ImageSourceConverter().ConvertFrom(name);
        }
        public void Load()
        {
            String str = "Game Elements\\";
            String strB = "Bonus\\";
            String strG = "Gum\\";

            bonus[0] = ISC(str + strB + "apple.png");
            bonus[1] = ISC(str + strB + "beetle.png");
            bonus[2] = ISC(str + strB + "hand.png");
            bonus[3] = ISC(str + strB + "heart.png");
            bonus[4] = ISC(str + strB + "men.png");
            bonus[5] = ISC(str + strB + "ruby.png");
            bonus[6] = ISC(str + strB + "box1.png");

            gum[0] = ISC(str + strG + "menper.png");
            gum[1] = ISC(str + strG + "robot.png");
            gum[2] = ISC(str + strG + "ghost.png");
            gum[3] = ISC(str + strG + "hand.png");
            gum[4] = ISC(str + strG + "monkey.png");
            gum[5] = ISC(str + strG + "rock.png");

            elements[0] = ISC(str + "block1.png");
            elements[1] = ISC(str + "block2.png");
            elements[2] = ISC(str + "bamboo.png");
            elements[3] = ISC(str + "bambootopSliceHorizontal.png");           
            elements[4] = ISC(str + "hause1.png");
        }
        public String[] nameElements = { "block1", "block2", "bamboo", "bambootopSliceHorizontal", "hause1" };
        public String[] nameBonus = { "apple", "beetle", "hand", "heart", "men", "ruby", "box1" };
        public String[] nameGums = { "menper", "robot", "ghost", "handG", "monkey", "rock", };
        public void addItems()
        {      
            Load();
            int i = 0;
            foreach (ImageSource im in elements)
            {                
                ItemsTable it = new ItemsTable();
                it.Name = nameElements[i];
                it.image = im;
                items[0].Add(it);
                i++;
            }
            i = 0;
            foreach(ImageSource im in bonus)
            {
                ItemsTable it = new ItemsTable();
                it.Name = nameBonus[i];
                it.image = im;
                items[1].Add(it);
                i++;
            }
            i = 0;
            foreach (ImageSource im in gum)
            {
                ItemsTable it = new ItemsTable();
                it.Name = nameGums[i];
                it.image = im;
                items[2].Add(it);
                i++;
            }            
        }
    }
}
