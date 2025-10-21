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
        public GridCreator() 
        {
            myGrid = new Grid();
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

        public void AddGridRow(GridLength gridLength = new GridLength())
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = gridLength;
            myGrid.RowDefinitions.Add(rowDefinition);
        }
        public void AddGridRow(double height = 0)
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = (height != 0) ? new GridLength(height) : new GridLength();
            myGrid.RowDefinitions.Add(rowDefinition);
        }
        public void ChangeGridRowHeight(int index, GridLength gridLength)
        {
            myGrid.RowDefinitions[index].Height = gridLength;
        }
        private void ChangeGridRowHeight(int index, double height)
        {
            myGrid.RowDefinitions[index].Height = new GridLength(height);
        }
        public void AddGridColumn(GridLength gridLength = new GridLength())
        {
            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = gridLength;
            myGrid.ColumnDefinitions.Add(columnDefinition);
        }
        public void AddGridColumn(double width = 0)
        {
            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = (width != 0) ? new GridLength(width) : new GridLength();
            myGrid.ColumnDefinitions.Add(columnDefinition);
        }
        public void ChangeGridColumnWidth(int index, GridLength gridLength)
        {
            myGrid.ColumnDefinitions[index].Width = gridLength;
        }
        public void ChangeGridColumnWidth(int index, double width)
        {
            myGrid.ColumnDefinitions[index].Width = new GridLength(width);
        }

        public void AddLabelToGrid(Window window, string name, string content, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, VerticalAlignment verticalAlignment = VerticalAlignment.Top, int row = 0, int column = 0, double width = 0, double height = 0)
        {
            Label label = new Label();
            label.Name = name;
            window.RegisterName(name, label);
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
        public void AddTextBlockToGrid(Window window, string name, string text, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, VerticalAlignment verticalAlignment = VerticalAlignment.Top, int row = 0, int column = 0, double width = 0, double height = 0)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Name = name;
            window.RegisterName(name, textBlock);
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
        public void AddEventListenerToElementByName(RoutedEventHandler routedEventHandler, string name)
        {
          
        }
    }
}
