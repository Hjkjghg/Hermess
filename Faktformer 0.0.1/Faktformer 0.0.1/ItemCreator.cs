using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Faktformer_0._0._1
{
    public class ItemCreator
    {
        public static void SetComboBoxItems(List<string> values, ref ComboBox comboBox)
        {
            comboBox.Items.Clear();
            for(int i = 0; i < values.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = values[i];
                comboBox.Items.Add(item);
            }
        }
        public static void AddComboBoxItems(List<string> values, ref ComboBox comboBox)
        {
            for (int i = 0; i < values.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = values[i];
                comboBox.Items.Add(item);
            }
        }
    }


    
}
