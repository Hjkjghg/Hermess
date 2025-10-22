using Microsoft.Windows.Themes;
using System;
using System.CodeDom;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;


namespace Faktformer_0._0._1
{
    internal class GridCreator
    {
        private Grid myGrid;
        private Window currentWindow;
        private Window previousWindow;
        private bool gridLineShown;

        public enum eventType
        {
            Click,
            Checked,

        }

        public GridCreator(Window parent) 
        {
            myGrid = new Grid();
            currentWindow = parent;
            previousWindow = parent;
            gridLineShown = false;
        }

        public void SetNewParentWindow(Window parent)
        {
            previousWindow = currentWindow;
            currentWindow = parent;
            RenameAllElementsToNewParent();
        }

        private void RenameAllElementsToNewParent()
        {
            foreach(FrameworkElement element in myGrid.Children)
            {
                previousWindow.UnregisterName(element.Name);
                currentWindow.RegisterName(element.Name, element);
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
        public void AppendGridTo(ref Border border)
        {
            border.Child = myGrid;
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
        public void ChangeColorOfGridCell(string colorHEX, int rowIndex, int columnIndex, string rectangleName = "")
        {

            System.Windows.Shapes.Rectangle rec = new System.Windows.Shapes.Rectangle()
            {
                Fill = new SolidColorBrush(ColorHexRgbConverter.HexToColor(colorHEX)),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };
            if (rectangleName != "")
            {
                rec.Name = rectangleName;
                if(currentWindow.FindName(rectangleName) != null)
                {
                    currentWindow.UnregisterName(rectangleName);
                }
                currentWindow.RegisterName(rectangleName, rec);
            }
            Grid.SetRow(rec, rowIndex);
            Grid.SetColumn(rec, columnIndex);
            myGrid.Children.Add(rec);
        }

        public void ToggleGridsGridLines()
        {
            gridLineShown = !gridLineShown;
            myGrid.ShowGridLines = gridLineShown;
        }
        
        //dodaj window
        public void AppendElementToGrid(FrameworkElement element, int rowIndex = 0, int columnIndex = 0)
        {
            Grid.SetRow(element, rowIndex);
            Grid.SetColumn(element, columnIndex);
            myGrid.Children.Add(element);
        }
        //dodaj window
        public void ReplaceElementByName(string name, FrameworkElement element)
        {
            FrameworkElement temp = (FrameworkElement)currentWindow.FindName(name);
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
        public void AddGridRow(double height)
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(height);
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
        public void AddGridColumn(double width)
        {
            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(width);
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

        public void AddLabelToGrid(string name, string content, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, VerticalAlignment verticalAlignment = VerticalAlignment.Top, int row = 0, int column = 0, Thickness margin = new Thickness(), double width = 0, double height = 0)
        {
            Label label = new Label();
            label.Name = name;
            label.Margin = margin;
            if(currentWindow.FindName(name) != null)
            {
                currentWindow.UnregisterName(name);
            }
            currentWindow.RegisterName(name, label);

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
        public void AddTextBlockToGrid(string name, string text, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, VerticalAlignment verticalAlignment = VerticalAlignment.Top, int row = 0, int column = 0, Thickness margin = new Thickness(), double width = 0, double height = 0)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Name = name;
            textBlock.Margin = margin; 
            if (currentWindow.FindName(name) != null)
            {
                currentWindow.UnregisterName(name);
            }
            currentWindow.RegisterName(name, textBlock);
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
            myGrid.Children.Add(textBlock);
        }
        public void AddCheckBoxToGrid(string name, string content, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, VerticalAlignment verticalAlignment = VerticalAlignment.Top, int row = 0, int column = 0, Thickness margin = new Thickness(), double width = 0, double height = 0)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Name = name;
            checkBox.Margin = margin;
            if (currentWindow.FindName(name) != null)
            {
                currentWindow.UnregisterName(name);
            }
            currentWindow.RegisterName(name, checkBox);
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
        public void AddEventListenerToElementByName(RoutedEventHandler routedEventHandler, string name, eventType typeOfEvent)
        {
            Object element = currentWindow.FindName(name);
            switch (element)
            {
                case (CheckBox):
                    CheckBox castedCheckBox = (CheckBox)element;
                    switch (typeOfEvent)
                    {
                        case (eventType.Click):
                            castedCheckBox.Click += routedEventHandler;
                            break;
                        case (eventType.Checked):
                            castedCheckBox.Checked += routedEventHandler;
                            break;
                    }                   
                    break;
                case (RadioButton):
                    RadioButton castedRadioButton = (RadioButton)element;
                    switch (typeOfEvent)
                    {
                        case (eventType.Click):
                            castedRadioButton.Click += routedEventHandler;
                            break;
                        case (eventType.Checked):
                            castedRadioButton.Checked += routedEventHandler;
                            break;
                    }                   
                    break;
                case (Button):
                    Button castedButton = (Button)element;
                    switch (typeOfEvent)
                    {
                        case (eventType.Click):
                            castedButton.Click += routedEventHandler;
                            break;
                    }
                    break;
            }
            
        }

        private void Element_KeyDown(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class GridSettings
    {
        public static void EnableDisabelGrid(ref Grid siatka, bool enabled)
        {
            siatka.Visibility = enabled ? Visibility.Visible : Visibility.Hidden;
            siatka.IsEnabled = enabled;
            Panel.SetZIndex(siatka, enabled ? 1 : -1);
        }
    }
}
