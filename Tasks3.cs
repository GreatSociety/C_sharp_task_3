using System;

namespace TestSpace
{
    class Program
    {
        static void Main()
        {
            int[] array = new int[] { 2, 55, 8, -999, 2, 7, 6, 1, 0, 5, 5, 0, 3, 5, 4, 8, 260, 703 };

            Array0fInt instanceArray = new Array0fInt(array);

            // 1.   Посчитать среднее арифметическое всех элементов массива array
            Console.WriteLine(String.Format("{0:f2}", instanceArray.Average()));

            // 2.   Найти сумму всех четных положительных элементов массива array

            // Если мы идем только по четным элементам массива
            Console.WriteLine(instanceArray.EvenSumPlus('E'));
            // Если мы идем по всем элементам массива
            Console.WriteLine(instanceArray.EvenSumPlus('V'));

            //Console.WriteLine(instanceArray.EvenSumPlus(10000000)); 


            // 3.   Найти результат деления суммы положительных элементов на сумму отрицательных. Результат должен быть дробным числом (не целым).
            Console.WriteLine(instanceArray.DivideOfSum());

            // 4.   Отсортировать элементы массива array по возрастанию

            // Реализовал так, ибо предположил, что array должен сохранять первоначальный порядок (как бы это бессмысленно не звучало). 
            // Ну и с поведением ссылочных и занчимымых типов в C# теперь все яснее. Плюс вспомнил сортировки. 

            Console.WriteLine("Sorted: " + string.Join(",", instanceArray.ImmutableSort()));
            Console.WriteLine("Not sorted: " + string.Join(",", instanceArray.array));

            // 5.   Преобразовать массив array в строку, состояющую из элементов массива, идущих в таком же порядке как и в массиве.
            //      Между элементами массива вставить разделитель (пробел или ,)

            string arrayString = string.Join(",", array);

            // 6.   Зеркально отразить строку, полученную в #5

            Console.WriteLine($"Reverse string: {ReverseWord(arrayString)}");

            // 7.   Удалить из полученной строки знаки минуса там, где они не имеют математического смысла (после числа и перед разделителем)

            Console.WriteLine($"Reverse string without nonsence minus: {DelMinus(ReverseWord(arrayString))}");

            // 8.   Преобразовать строку, полученную в #7 обратно в массив целочисленных значений и записать его в переменную array

            array = IntConverter(DelMinus(ReverseWord(arrayString)));

            Console.WriteLine("Int Array: " + string.Join(",", array));

            // 9.   Создать массив byte[] и заполнить его такими же значениями как и массив array, если число слишком велико для типа byte - заменить его на значение по умолчанию для типа byte

            byte[] byteArray = Byter(array);

            Console.WriteLine("byte Array: " + string.Join(",", byteArray));

            Console.ReadKey();
        }

        static string ReverseWord(string word)
        {
            char[] array = word.ToCharArray();

            string wordRev = "";

            for (int i = array.Length - 1; i >= 0; i--)
            {
                wordRev += array[i];
            }

            return wordRev;
        }

        static string DelMinus(string word)
        {
            char[] array = word.ToCharArray();

            string result = "";

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '-' && array[i + 1] == ',')
                {
                    continue;
                }

                result += array[i];
            }

            return result;
        }

        static int[] IntConverter(string source)
        {

            string[] toConvert = source.Split(',');

            int[] array = new int[toConvert.Length];

            for (int i = 0; i < toConvert.Length; i++)
            {
                array[i] = int.Parse(toConvert[i]);
            }

            return array;
        }

        static byte[] Byter(int[] source)
        {
            byte[] array = new byte[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] <= byte.MaxValue)
                {
                    array[i] = (byte)source[i];
                }
                else
                {
                    array[i] = default;
                }

            }

            return array;
        }

    }

    class Array0fInt
    {
        public int[] array;
        private int[] arraySort;

        public Array0fInt(int[] array)
        {
            this.array = array;
            this.arraySort = new int[array.Length];
        }

        // #1
        public double Average()
        {
            int average = 0;

            foreach (var i in this.array)
            {
                average += i;
            }

            return (double)average / this.array.Length;
        }

        // #2
        public int EvenSumPlus(object mode_)
        {
            char mode;

            try
            {
                mode = Convert.ToChar(mode_);
            }
            catch (System.OverflowException)
            {
                return -1;
            }

            int sum = 0;

            if (mode == 'E')
            {
                for (var i = 0; i < this.array.Length; i++, i++)
                {
                    if (this.array[i] >= 0)
                    {
                        sum += this.array[i];
                    }
                }
            }
            else if (mode == 'V')
            {
                foreach (var i in this.array)
                {
                    if (i >= 0 && i % 2 == 0)
                    {
                        sum += i;
                    }
                }

            }
            else
            {
                Console.WriteLine("This method have a two mode:\n E - if we need to sum even elements in array\n V - if we need to sum even value of each elements in array");
            }

            return sum;
        }

        // #3
        public double DivideOfSum()
        {
            int sumplus = 0;
            int summinus = 0;

            foreach (var i in this.array)
            {
                if (i > 0)
                {
                    sumplus += i;
                }
                else
                {
                    summinus += i;
                }

            }

            return (double)sumplus / summinus;
        }

        // #4
        public int[] ImmutableSort()
        {
            Array.Copy(this.array, this.arraySort, this.array.Length);
            HoareSort(this.arraySort, 0, this.arraySort.Length - 1);
            return this.arraySort;
        }

        private void HoareSort(int[] array, int left, int right)
        {
            int leftIndex = left;
            int rightIndex = right;
            int temp;
            int prop = array[(left + right) / 2];

            while (leftIndex <= rightIndex)
            {
                for (; array[leftIndex] < prop; leftIndex++) ;

                for (; array[rightIndex] > prop; rightIndex--) ;

                if (leftIndex <= rightIndex)
                {

                    temp = array[leftIndex];
                    array[leftIndex++] = array[rightIndex];
                    array[rightIndex--] = temp;
                }
            }
            if (left < rightIndex)
            {
                HoareSort(array, left, rightIndex);
            }
            if (leftIndex < right)
            {
                HoareSort(array, leftIndex, right);
            }

        }

    }

}