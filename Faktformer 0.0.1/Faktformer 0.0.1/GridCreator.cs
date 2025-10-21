using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Faktformer_0._0._1
{
    internal class GridCreator
    {
        private Grid myGrid;
        private Window parent;

        public GridCreator(ref Window window) 
        {
            myGrid = new Grid();
            parent = window;
        }

        public void ChangeParentWindow(ref Window window)
        {
            Window previosParent = parent;
            parent = window;
        }
        private void ReparentAllElements()
        {
            foreach (FrameworkElement element in myGrid.Children)
            {
                myGrid.UnregisterName(element.Name);
                myGrid.RegisterName(element.Name, element);
            }
        }

        public void SetGridHeight(double height)
        {
            myGrid.Height = height;
        }
        public void SetGridWidth(double width) 
        {
            myGrid.Width = width;
        }

        public void AppendGridTo(ref Grid grid)
        {
            grid.Children.Add(myGrid);
        }
        public void AppendGridTo(ref StackPanel stackPanel)
        {
            stackPanel.Children.Add(myGrid);
        }
        public Grid GetGrid()
        {
            return myGrid;
        }
        public void SetGrid(Grid grid)
        {
            myGrid = grid;
        }

        public void ChangeGridBackground(string colorHEX)
        {
            Brush brush = new SolidColorBrush(ColorHexRgbConverter.HexToColor(colorHEX));
            myGrid.Background = brush;
        }
        public void ChangeGridBackground(Brush brush)
        {
            myGrid.Background = brush;
        }

        
        //dodaj window
        public void AppendElementToGrid(FrameworkElement element, int row = 0, int column = 0)
        {
            Grid.SetRow(element, row);
            Grid.SetColumn(element, column);
            myGrid.Children.Add(element);
        }
        //dodaj window
        public void ReplaceElementByName(string name, FrameworkElement element)
        {
            FrameworkElement temp = (FrameworkElement)myGrid.FindName(name);
            Grid.SetRow(element, Grid.GetRow(temp));
            Grid.SetColumn(element, Grid.GetColumn(temp));
            myGrid.Children.Remove(temp);
            element.Name = name;
            myGrid.Children.Add(element);
        }

        public void AddGridRow(GridLength height = new GridLength())
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = height;
            myGrid.RowDefinitions.Add(rowDefinition);
        }
        public void AddGridRow(double height = 0)
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = (height != 0) ? new GridLength(height) : new GridLength();
            myGrid.RowDefinitions.Add(rowDefinition);
        }
        public void ChangeGridRowHeight(int index, GridLength height)
        {
            myGrid.RowDefinitions[index].Height = height;
        }
        private void ChangeGridRowHeight(int index, double height)
        {
            myGrid.RowDefinitions[index].Height = new GridLength(height);
        }
        public void AddGridColumn(GridLength width = new GridLength())
        {
            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = width;
            myGrid.ColumnDefinitions.Add(columnDefinition);
        }
        public void AddGridColumn(double width = 0)
        {
            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = (width != 0) ? new GridLength(width) : new GridLength();
            myGrid.ColumnDefinitions.Add(columnDefinition);
        }
        public void ChangeGridColumnWidth(int index, GridLength width)
        {
            myGrid.ColumnDefinitions[index].Width = width;
        }
        public void ChangeGridColumnWidth(int index, double width)
        {
            myGrid.ColumnDefinitions[index].Width = new GridLength(width);
        }

        public void AddLabelToGrid(string name, string content, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, VerticalAlignment verticalAlignment = VerticalAlignment.Top, int row = 0, int column = 0, double width = 0, double height = 0)
        {
            Label label = new Label();
            label.Name = name;
            if(myGrid.FindName(name) == null)
            {
                myGrid.UnregisterName(name);
            }
            myGrid.RegisterName(name, label);

            Grid.SetRow(label, row);
            Grid.SetColumn(label, column);
            label.Content = content;
            if(width != 0)
            {
                label.Width = width;
            }
            if (height != 0)
            {
                label.Height = height;
            }
            label.HorizontalAlignment = horizontalAlignment;
            label.VerticalAlignment = verticalAlignment;
            myGrid.Children.Add(label);
        }
        public void AddTextBlockToGrid(string name, string text, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, VerticalAlignment verticalAlignment = VerticalAlignment.Top, int row = 0, int column = 0, double width = 0, double height = 0)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Name = name;
            if (myGrid.FindName(name) == null)
            {
                myGrid.UnregisterName(name);
            }
            myGrid.RegisterName(name, textBlock);
            Grid.SetRow(textBlock, row);
            Grid.SetColumn(textBlock, column);
            textBlock.Text = text;
            if (width != 0)
            {
                textBlock.Width = width;
            }
            if(height != 0)
            {
                textBlock.Height = height;
            }
            textBlock.HorizontalAlignment = horizontalAlignment;
            textBlock.VerticalAlignment = verticalAlignment;
        }
        public void AddCheckBoxToGrid(string name, string content, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, VerticalAlignment verticalAlignment = VerticalAlignment.Top, int row = 0, int column = 0, double width = 0, double height = 0)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Name = name;
            if (myGrid.FindName(name) == null)
            {
                myGrid.UnregisterName(name);
            }
            myGrid.RegisterName(name, checkBox);
            Grid.SetRow(checkBox, row);
            Grid.SetColumn(checkBox, column);
            checkBox.Content = content;
            if (width != 0)
            {
                checkBox.Width = width;
            }
            if(height != 0)
            {
                checkBox.Height = height;
            }
            checkBox.HorizontalAlignment = horizontalAlignment;
            checkBox.VerticalAlignment = verticalAlignment;
            myGrid.Children.Add(checkBox);
        }
        public void AddEventListenerToElementByName(RoutedEventHandler routedEventHandler, string name)
        {
            
        }
    }
}
