using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Faktformer_0._0._1
{
    //Klasa przechowująca i worząca 2 wymiatowe tabele w postaci list [rząd][kolumna] jako Tables List<Rows>, Rows List<object>  
    public class Tables
    {
        public List<Rows> table;
        public int tableWidth;
        public int tableHeight;

        //konstruktor
        public Tables()
        {
            table = new List<Rows>();
            tableHeight = 0;
            tableWidth = 0;
        }

        //zwraca wybrany rząd tabeli jako List<object>
        public List<object>? ReturnRow(int index)
        {
            if (table.Count >= index)
            {
                return table[index].row;
            }
            throw new IndexOutOfRangeException(index.ToString());
        }

        //zwraca wybrany rząd tabeli jako object[]
        public object[] ReturnRowAsArray(int index)
        {
            if (table.Count >= index)
            {
                return table[index].row.ToArray();
            }
            throw new IndexOutOfRangeException(index.ToString());
        }

        //zwraca wybraną kolumnę jako List<object>
        public List<object> ReturnColumn(int index)
        {
            List<object> columnOut = new List<object>();
            foreach(Rows row in table)
            {
                if (row.row[index] == null)
                {
                    throw new IndexOutOfRangeException(index.ToString());
                }
                columnOut.Add(row.row[index]);
            }
            return columnOut;
        }

        //zwraca wybraną kolumnę jako object[]
        public object[] ReturnColumnAsArray(int index)
        {
            object[] columnOut = new object[table.Count];
            for(int i = 0; i < table.Count; i++)
            {
                if (table[i].row[index] == null)
                {
                    throw new IndexOutOfRangeException(index.ToString());
                }
                columnOut[i] = table[i].row[index];
            }
            return columnOut;
        }

        //zwraca długość tabeli zależnie od podanego wymiaru
        public int ReturnLenght(int dimention)
        {
            return (dimention == 0) ? tableHeight : tableWidth;
        }

        //dodaje rząd do tabeli pobierając z List<object>
        public void AddRow(List<object> row)
        {
            Rows rows = new Rows(row);
            table.Add(rows);
            tableWidth = (row.Count > tableWidth) ? row.Count : tableWidth;
            tableHeight++;
        }
        //dodaje rząd do tabeli pobierając z object[]
        public void AddRow(object[] row)
        {
            Rows rows = new Rows(row);
            tableWidth = (rows.row.Count > tableWidth) ? rows.row.Count : tableWidth;
            tableHeight++;
            table.Add(rows);
        }
        //dodaje rząd do tabeli pobierając z Rows
        public void AddRow(Rows row)
        {
            table.Add(row);
            tableWidth = (row.row.Count > tableWidth) ? row.row.Count : tableWidth;
            tableHeight++;
        }

        //zastępuje wybrany rząd pobierając z List<object>
        public void ReplaceRow(List<object> row, int rowNumber)
        {
            Rows rows = new Rows(row);
            if (table[rowNumber] != null)
            {
                table[rowNumber] = rows;
            }
        }
        //zastępuje wybrany rząd pobierając z object[]
        public void ReplaceRow(object[] row, int rowNumber)
        {
            Rows rows = new Rows(row);
            if (table[rowNumber] != null)
            {
                table[rowNumber] = rows;
            }
        }
        //zastępuje wybrany rząd pobierając z Rows
        public void ReplaceRow(Rows row, int rowNumber)
        {
            if (table[rowNumber] != null)
            {
                table[rowNumber] = row;
            }
        }

        //usuwa wybrany rząd
        public void RemoveRow(int row)
        {
            if (table[row] != null)
            {
                table.RemoveAt(row);
                tableHeight--;
            }
        }

        //zwraca całą tablę jako object[,]
        public object[,] ReturnArray()
        {
            object[,] array = new object[tableHeight, tableWidth];
            int i = 0;
            int f = 0;
            foreach (Rows list in table)
            {
                foreach (object item in list.row)
                {
                    array[i, f] = item;
                    f++;
                }
                i++;
            }
            return array;
        }
        //zwraca całą tabę jako  List<list<dtring>>
        public List<List<object>> ReturnList()
        {
            List<List<object>> list = new List<List<object>>();
            foreach(Rows rows in table)
            {
                list.Add(rows.row);
            }
            return list;
        }
        //zwraca całą tabele jako Tables
        public List<Rows> ReturnRows()
        {
            return table;
        }

        //dodaje element object do wybranego rzęu
        public void AddElementToRow(object element, int row)
        {
            if (table[row] != null)
            {
                table[row].row.Add(element);
                tableWidth = (table[row].row.Count > tableWidth) ? table[row].row.Count : tableWidth;
            }
        }

        //podmienia element na wybranej pozycji rzędu i kolumny
        public void ReplaceElementAt(object element, int row, int column)
        {
            if (table[row].row[column] != null)
            {
                table[row].row[column] = element;
            }
        }

        //stawia element na wybranym miejscu w tabeli, może wymusić
        public void PlaceElementAt(object element, int row, int column, bool force = false)
        {
            if (force)
            {
                while (table[row] == null)
                {
                    table.Add(new Rows());
                }
                while (table[row].row[column] == null)
                {
                    table[row].Add("");
                }
                table[row].row[column] = element;
            }
            else
            {
                if (table[row].row[column] != null)
                {
                    table[row].row[column] = element;
                }
            }
        }

        //usuwa element na wybranym iejscu w tabeli
        public void RemoveElementAt(int row, int column)
        {
            if (table[row].row[column] != null)
            {
                table[row].row.RemoveAt(column);
            }
        }

        //ustawia tabelę na dane pobrane z List<List<object>>
        public void SetContentTo(List<List<object>> content)
        {
            foreach(List<object> list in content)
            {
                Rows rows = new Rows(list);
                table.Add(rows);
            }
            tableHeight = content.Count;
            tableWidth = content[0].Count;
        }
        //ustawia tabelę na dane pobrane z List<Rows>
        public void SetContentTo(List<Rows> content)
        {
            table = content;
            tableHeight = content.Count;
            tableWidth = content[0].row.Count;
        }
        //ustawia tabelę na dane pobrane z object[,]
        public void SetContentTo(object[,] content)
        {
            table.Clear();
            tableHeight = content.GetLength(0);
            tableWidth = content.GetLength(1);
            for (int i = 0; i < content.GetLength(0); i++)
            {
                table.Add(new Rows());
                for (int j = 0; j < content.GetLength(1); j++)
                {
                    table[i].row.Add(content[i, j]);
                }
            }
        }

        //zwraca element na wybranej pozycji, jeśli nie istnieje to null
        public object? ReturnItemAt(int row, int column)
        {
            if (table[row].row[column] != null)
            {
                return table[row].row[column];
            }
            return null;
        }
        
        //czyści tabele
        public void ClearTable()
        {
            table.Clear();
            tableHeight = 0;
            tableWidth = 0;
            //this[4]
        }
        
        public Rows this[int i]
        {
            get => table[i];
        }
    }

    //klasa zawierająca i tworząca rzędy
    public class Rows
    {
        public List<object> row;
        public int lenght;

        //konstruktor który przyjmóje List<object> i ustawia rząd na niego
        public Rows(List<object> inputRow)
        {
            row = inputRow;
            lenght = inputRow.Count;
        }
        //konstruktor który przyjmóje object[] i ustawia rząd na niego
        public Rows(object[] inputRow)
        {
            row = new List<object>();
            for (int i = 0; i < inputRow.Length; i++)
            {
                row.Add(inputRow[i]);
            }
            lenght = inputRow.Length;
        }
        //konstruktor który nie przyjmóje argumentów
        public Rows()
        {
            row = new List<object>();
            lenght = 0;
        }

        //dodaje element do rzędu
        public void Add(object input)
        {
            row.Add(input);
        }

        //usuwa ostatni element rzzędu
        public void RemoveLast()
        {
            row.RemoveAt(row.Count-1);
        }

        //usuwa wybrany element rzędu
        public void Remove(int index)
        {
            row.RemoveAt(index);
        }

        public object this[int i]
        {
            get { return row[i]; }
        }

    }

    //klasa przechowująa funkcje konvertujące
    public class ListCaster
    {
        //konwertuje List<List<object>> na List<List<string>>
        public static List<List<string>> CastString2D(List<List<object>> objectList)
        {
            List<List<string>> listOut = new List<List<string>>();
            foreach(List<object> list in objectList)
            {
                List<string> temp = list.ConvertAll(x => Convert.ToString(x));
                listOut.Add(temp);
            }
            return listOut;
        }
        //konwertuje List<object> na List<string>
        public static List<string> CastString(List<object> objectList)
        {
            List<string> listOut = new List<string>();
            listOut = objectList.ConvertAll(x => Convert.ToString(x));
            return listOut;
        }

        //konwertuje List<List<object>> na List<List<int>>
        public static List<List<int>> CastInt2D(List<List<object>> objectList)
        {
            List<List<int>> listOut = new List<List<int>>();
            foreach (List<object> list in objectList)
            {
                List<int> temp = list.ConvertAll(x => Convert.ToInt32(x));
                listOut.Add(temp);
            }
            return listOut;
        }
        //konwertuje List<object> na List<int>
        public static List<int> CastInt(List<object> objectList)
        {
            List<int> listOut = new List<int>();
            listOut = objectList.ConvertAll(x => Convert.ToInt32(x));
            return listOut;
        }

        //konwertuje List<List<object>> na List<List<double>>
        public static List<List<double>> CastDouble2D(List<List<object>> objectList)
        {
            List<List<double>> listOut = new List<List<double>>();
            foreach (List<object> list in objectList)
            {
                List<double> temp = list.ConvertAll(x => Convert.ToDouble(x));
                listOut.Add(temp);
            }
            return listOut;
        }
        //konwertuje List<object> na List<double>
        public static List<double> CastDouble(List<object> objectList)
        {
            List<double> listOut = new List<double>();
            listOut = objectList.ConvertAll(x => Convert.ToDouble(x));
            return listOut;
        }

        //konwertuje List<List<object>> na List<List<bool>>
        public static List<List<bool>> CastBool2D(List<List<object>> objectList)
        {
            List<List<bool>> listOut = new List<List<bool>>();
            foreach (List<object> list in objectList)
            {
                List<bool> temp = list.ConvertAll(x => Convert.ToBoolean(x));
                listOut.Add(temp);
            }
            return listOut;
        }
        //konwertuje List<object> na List<bool>
        public static List<bool> CastBool(List<object> objectList)
        {
            List<bool> listOut = new List<bool>();
            listOut = objectList.ConvertAll(x => Convert.ToBoolean(x));
            return listOut;
        }
        
        //public static Rows ConvertRowsToString(Rows rows)
        //{
        //    List<string> temp = rows.row.ConvertAll(x => Convert.ToString(x));
        //    Rows listOut = new Rows(temp.ToArray());
        //    return listOut;
        //}

    }
}
