using static System.Console;

int m, n;
m = InputPositive("Введите кол-во строк m > 0    : ");
n = InputPositive("Введите кол-во столбцов n > 0 : ");
    // работаем с матрицей
Matrix a = new Matrix(m, n);
a.Print("Исходная матрица:");
a.Divide();
a.Print("Разделить элементы в каждом столбце на номер этого столбца:");
a.Clear();
a.Print("Почистили:");
    // теперь в коллекциях
Lists l = new(m, n);
l.Print("Исходная коллекция:");
l.Stats("Статистика коллекции:");
l.SumOfPositiveElementsInRows("Сумма положительных элементов в строках:");
l.SwapMinInColsWith1();
l.Print("Смена min элемента в столбце с первым:");

static int InputPositive(string msg) // by Буцких Вячеслав
{
    int x = 0;
    bool cycle = true;
    while (cycle)
    {
        try
        {
            Write(msg);
            x = Convert.ToInt32(ReadLine());
            if (x > 0)
            {
                cycle = false;
            }
            else
            {
                throw new Exception("Введите число > 0 ! ");
            }
        }
        catch (Exception e)
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine(e.Message);
            ResetColor();
        }
    }
    return x;
}
class Lists : Matrix
{
    // строки 
    public List<List<int>> ?Rows { get; set; }
    // колонки
    public List<List<int>> ?Columns { get; set; }
    // вся матрица в коллекции
    public List<int> ?L { get; set; }

    public Lists(int m, int n) : base(m, n)
    {
        Matrix2Rows();
        Matrix2Columns();
        Matrix2List();
    }
    void Matrix2Rows()
    {
        Rows = new(m);
        for (int i = 0; i < m; i++)
        {
            List<int> TempR = new(n);
            for (int j = 0; j < n; j++)
            {
                TempR.Add(a[i, j]); 
            }
            Rows.Add(TempR);
        }
    }
    void Matrix2Columns()
    {
        Columns = new(n);
        for (int j = 0; j < n; j++)
        {
            List<int> TempC = new(m);
            for (int i = 0; i < m; i++)
            {
                TempC.Add(a[i, j]);
            }
            Columns.Add(TempC);
        }
    }
    void Matrix2List()
    {
        L = new(m*n);
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                L.Add(a[i, j]);
            }
        }
    }
    void Columns2Rows()
    {
        if (Rows == null || Columns == null)
        {
            WriteLine("Коллекция пуста");
            return;
        }
        for (int i = 0; i < m; i++)
        {
            for(int j = 0; j < n; j++)
            {
                Rows[i][j] = Columns[j][i]; 
            }
        }
    }
    public void Stats(string Header)
    {
        if (L == null)
        {
            WriteLine("Коллекция пуста");
            return;
        }
        WriteLine(Header);
        WriteLine("Сумма    = " + L.Sum());
        WriteLine("Среднее  = " + L.Average());
        WriteLine("Минимум  = " + L.Min());
        WriteLine("Максимум = " + L.Max());
        WriteLine();
    }
    public void SumOfPositiveElementsInRows(string Header)
    {
        if (Rows == null)
        {
            WriteLine("Коллекция пуста");
            return;
        }
        WriteLine(Header);
        for (int i = 0; i < m; i++)
        {
            WriteLine(String.Format("{0,3}", i) + ") " +
                      Rows[i].FindAll(x => x > 0).Sum());
        }
        WriteLine();
    }
    public void SwapMinInColsWith1()
    {
        int min, minIndex, temp;
        for (int i = 0; i < n; i++)
        {
            min = Columns[i].Min(x => Math.Abs(x)); // определяем min в колонке
            // если min отрицательный (не находится), меняем знак
            if (Columns[i].IndexOf(min) == -1) {
                min = -min;
                minIndex = Columns[i].IndexOf(min); // ищем его индекс
            }
            else
            {
                minIndex = Columns[i].IndexOf(min); // ищем его индекс
            }
            minIndex = Columns[i].IndexOf(min); // ищем его индекс
            // унив Swap здесь не работает - руками меняем
            temp = Columns[i][0];
            Columns[i][0] = Columns[i][minIndex];
            Columns[i][minIndex] = temp;    
        }
        // поскольку изменили Columns, конвертим в Rows для корректного Print
        Columns2Rows();
    }
    public override void Print(string Header)
    {
        if (Rows == null)
        {
            WriteLine("Коллекция пуста");
            return;
        }
        WriteLine(Header);
        Write("   ");
        for (int j = 0; j < n; j++)
        {
            Write(String.Format("{0,5}", j + ")"));
        }
        WriteLine();
        for (int i = 0; i < m; i++)
        {
            Write(String.Format("{0,3}", i + ")"));
            for (int j = 0; j < n; j++)
            {
                Write(String.Format("{0,5}", Rows[i][j]));
            }
            WriteLine();
        }
        WriteLine();
    }
}

class Matrix
{
    public int m { get; set; }
    public int n { get; set; }
    public int[,] a;
    readonly int max = 50;

    public Matrix(int m, int n)
    {
        this.m = m;
        this.n = n;
        Generate(m, n);
    }
    public void Generate(int m, int n)
    {
        a = new int[m, n]; // создаем пустой массив
        Random r = new();
        //заполняем массив данными
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                a[i, j] = r.Next(-max, max);
            }
        }
    }
    public void Clear()
    {
        a = new int[m, n]; // создаем пустой массив
    }
    public void Divide()
    {
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                a[i, j] /= j + 1; // иначе Divide by Zero
            }
        }
    }
    public virtual void Print(string Header)
    {
        if (a == null)
        {
            WriteLine("Матрица пуста");
            return;
        }
        WriteLine(Header);
        Write("   ");
        for (int j = 0; j < n; j++)
        {
            Write(String.Format("{0,5}", j + ")"));
        }
        WriteLine();
        for (int i = 0; i < m; i++)
        {
            Write(String.Format("{0,3}", i +")"));
            for (int j = 0; j < n; j++)
            {
                Write(String.Format("{0,5}", a[i, j]));
            }
            WriteLine();
        }
        WriteLine();
    }
}