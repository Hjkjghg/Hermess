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

        //enum przechowujący typy EventListenerów do których można przypisać EventHandlery dla elementów siatki
        public enum eventType
        {
            Click,
            Checked,

        }

        //konstruktor który pobiera okno które jest używane do rejstracji nazw
        public GridCreator(Window parent) 
        {
            myGrid = new Grid();
            currentWindow = parent;
            previousWindow = parent;
            gridLineShown = false;
        }

        //zmienia okno rodzica na nowe okno
        public void SetNewParentWindow(Window parent)
        {
            previousWindow = currentWindow;
            currentWindow = parent;
            RenameAllElementsToNewParent();
        }

        //przerzuca nazwy wszystkich elementów do aktualnego okna z poprzedniego okna
        private void RenameAllElementsToNewParent()
        {
            foreach(FrameworkElement element in myGrid.Children)
            {
                previousWindow.UnregisterName(element.Name);
                currentWindow.RegisterName(element.Name, element);
            }
        }

        //ustawia wysokość siatki(double)
        public void SetGridHeight(double height)
        {
            myGrid.Height = height;
        }
        //ustawia szerokość siatki(double)
        public void SetGridWidth(double width) 
        {
            myGrid.Width = width;
        }

        //wstawia siatkę do siatki
        public void AppendGridTo(ref Grid grid)
        {
            grid.Children.Add(myGrid);
        }
        //wstawia siatke do StackPanela
        public void AppendGridTo(ref StackPanel stackPanel)
        {
            stackPanel.Children.Add(myGrid);
        }
        //wstawia siatkę do Bordera
        public void AppendGridTo(ref Border border)
        {
            border.Child = myGrid;
        }

        //zwraca siatkę
        public Grid GetGrid()
        {
            return myGrid;
        }

        //ustawia siatkę na podaną siatkę
        public void SetGrid(Grid grid)
        {
            myGrid = grid;
        }

        //zmienia kolor tła siatki na określony kolor w formacie HEX
        public void ChangeGridBackground(string colorHEX)
        {
            Brush brush = new SolidColorBrush(ColorHexRgbConverter.HexToColor(colorHEX));
            myGrid.Background = brush;
        }
        //zmienia kolor tła siatki na określony kolor w formacie Brush
        public void ChangeGridBackground(Brush brush)
        {
            myGrid.Background = brush;
        }

        //zmienia kolor tłą pojedyńczej komórki poprzez wstawienie prostokąta określonego koloru w formacie HEX
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
        //zmienia kolor tłą pojedyńczej komórki poprzez wstawienie prostokąta określonego koloru w formacie Brush
        public void ChangeColorOfGridCell(Brush brush, int rowIndex, int columnIndex, string rectangleName = "")
        {

            System.Windows.Shapes.Rectangle rec = new System.Windows.Shapes.Rectangle()
            {
                Fill = brush,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };
            if (rectangleName != "")
            {
                rec.Name = rectangleName;
                if (currentWindow.FindName(rectangleName) != null)
                {
                    currentWindow.UnregisterName(rectangleName);
                }
                currentWindow.RegisterName(rectangleName, rec);
            }
            Grid.SetRow(rec, rowIndex);
            Grid.SetColumn(rec, columnIndex);
            myGrid.Children.Add(rec);
        }

        //włącza/wyłącza linie separacji komórek siatki
        public void ToggleGridsGridLines()
        {
            gridLineShown = !gridLineShown;
            myGrid.ShowGridLines = gridLineShown;
        }
        
        //wstawia element od siatki w określonej komórce
        public void AppendElementToGrid(FrameworkElement element, int rowIndex = 0, int columnIndex = 0)
        {
            Grid.SetRow(element, rowIndex);
            Grid.SetColumn(element, columnIndex);
            myGrid.Children.Add(element);
        }

        //zamienia element innym elementem o odpowiednej nazwie
        public void ReplaceElementByName(string name, FrameworkElement element)
        {
            FrameworkElement temp = (FrameworkElement)currentWindow.FindName(name);
            Grid.SetRow(element, Grid.GetRow(temp));
            Grid.SetColumn(element, Grid.GetColumn(temp));
            myGrid.Children.Remove(temp);
            currentWindow.UnregisterName(name);
            element.Name = name;
            currentWindow.RegisterName(name, element);
            myGrid.Children.Add(element);
        }

        //dodaje rząd do siatki o okoreślonej wysokości(double)
        public void AddGridRow(GridLength height = new GridLength())
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = height;
            myGrid.RowDefinitions.Add(rowDefinition);
        }
        //dodaje rząd do siatki o okoreślonej wysokości(double)
        public void AddGridRow(double height)
        {
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(height);
            myGrid.RowDefinitions.Add(rowDefinition);
        }

        //zmienia wysokość kolumny siatki(double) o określonym indeksie
        public void ChangeGridRowHeight(int index, GridLength height)
        {
            myGrid.RowDefinitions[index].Height = height;
        }
        //zmienia wysokość kolumny siatki(double) o określonym indeksie
        private void ChangeGridRowHeight(int index, double height)
        {
            myGrid.RowDefinitions[index].Height = new GridLength(height);
        }

        //dodaje kolumnę siatki o określonej szerokości(GridLenght)
        public void AddGridColumn(GridLength width = new GridLength())
        {
            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = width;
            myGrid.ColumnDefinitions.Add(columnDefinition);
        }
        //dodaje kolumnę siatki o określonej szerokości(double)
        public void AddGridColumn(double width)
        {
            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = new GridLength(width);
            myGrid.ColumnDefinitions.Add(columnDefinition);
        }

        //zmienia szerokość kolumny siatki(GridLenght) o okrwślonym indeksie
        public void ChangeGridColumnWidth(int index, GridLength width)
        {
            myGrid.ColumnDefinitions[index].Width = width;
        }
        //zmienia szerokość kolumny siatki(double) o okrwślonym indeksie
        public void ChangeGridColumnWidth(int index, double width)
        {
            myGrid.ColumnDefinitions[index].Width = new GridLength(width);
        }
        
        //dodaje Labela do siatki o określonych parametrach
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

        //dodaje TextBlocka do siatki o określonych parametrach
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

        //dodaje CheckBoxa do siatki o określonych parametrach
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

        //Dodaje event handler podanego typu z enum do elementu siatki o podanym imieniu
        public void AddEventHandlerToElementByName(RoutedEventHandler routedEventHandler, string name, eventType typeOfEvent)
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
    }

    public class GridSettings
    {
        //frontuje lub backuje siatkę
        public static void EnableDisabelGrid(ref Grid siatka, bool enabled)
        {
            siatka.Visibility = enabled ? Visibility.Visible : Visibility.Hidden;
            siatka.IsEnabled = enabled;
            Panel.SetZIndex(siatka, enabled ? 1 : -1);
        }
    }
}
