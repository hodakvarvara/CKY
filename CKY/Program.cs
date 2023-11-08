using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKY
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Output = false; // Переменная для хранения результата проверки слова по грамматике
            List<ProductionRule> LeftSide = new List<ProductionRule>
            {
                // Добавление продукций в грамматику
                new ProductionRule("S", "AB"),
                new ProductionRule("A", "CD"),
                new ProductionRule("A", "FA"),
                new ProductionRule("A", "f"),
                new ProductionRule("A", "t"),
                new ProductionRule("A", "a"),
                new ProductionRule("B", "EA"),
                new ProductionRule("B", "f"),
                new ProductionRule("B", "l"),
                new ProductionRule("C", "n"),
                new ProductionRule("D", "f"),
                new ProductionRule("D", "t"),
                new ProductionRule("D", "a"),
                new ProductionRule("E", "f"),
                new ProductionRule("E", "l"),
                new ProductionRule("F", "l")
            }; // Список продукций грамматики

            string Word = "tflna"; // Слово, которое нужно проверить
            List<ProductionRule>[,] CKYMat = new List<ProductionRule>[Word.Length, Word.Length]; // Матрица для хранения результатов

            // Инициализация матрицы результатов
            for (int a = 0; a < Word.Length; a++)
            {
                for (int b = 0; b < Word.Length; b++)
                {
                    CKYMat[a, b] = new List<ProductionRule>();
                }
            }

            // Заполнение первой строки матрицы
            for (int h = 0; h < Word.Length; h++)
            {
                foreach (ProductionRule prod in LeftSide)
                {
                    if (Word[h].ToString() == prod.rightHandSide)
                    {
                        CKYMat[h, h].Add(prod);
                    }
                }
            }

            // Окончание заполнения

            int i1 = 0; // Первый столбец
            int i = 0; // Индекс строки матрицы
            int j = 1; // Индекс строки матрицы
            int k = 0; // Переменная k
            int k1 = 0; // Переменная k + 1
            List<string> RightSide = new List<string>(); // Список продукций для поиска в грамматике
            List<ProductionRule> Swaplist1 = new List<ProductionRule>(); // Список для хранения продукций позиции матрицы
            List<ProductionRule> Swaplist2 = new List<ProductionRule>(); // Список для хранения продукций позиции матрицы
            int column = Word.Length - 1; // Количество столбцов для обработки

            for (int je = 1; je < Word.Length; je++)
            { // Цикл по строкам для поиска продукций
                j = je;
                i = i1;
                for (int v = 0; v < column; v++)
                { // Цикл по столбцам для текущей строки
                    for (int k2 = i; k2 < j; k2++)
                    { // Цикл для поиска k и взятия генераторов в позициях [i, k] и [k, j]
                        k = k2;
                        k1 = k2 + 1;
                        Swaplist1 = CKYMat[i, k];
                        Swaplist2 = CKYMat[k1, j];
                        if (Swaplist1.Count == 0 || Swaplist2.Count == 0)
                            break;
                        else
                        {
                            foreach (ProductionRule p1 in Swaplist1)
                            {
                                foreach (ProductionRule p2 in Swaplist2)
                                    RightSide.Add(p1.leftHandSide + p2.leftHandSide);
                            }
                        }
                    }
                    // Проверка, порождает ли какая-либо продукция из грамматики результаты, найденные путем объединения k
                    foreach (string str in RightSide)
                    {
                        foreach (ProductionRule prod in LeftSide)
                        {
                            if (str == prod.rightHandSide)
                            {
                                CKYMat[i, j].Add(prod); // Если продукция существует в грамматике, добавляется правило, порождающее ее, в эту позицию матрицы
                            }
                        }
                    }
                    i++;
                    j++;
                }
                RightSide.Clear(); // Очистка списка найденных продукций
                column--; // Уменьшение количества столбцов для анализа (это создает "лесенку" в матрице)
            }

            ////вывод матрицы 
            //Console.WriteLine("вывод матрицы");
            //for (int index1 = 0; index1 < CKYMat.GetLength(0); index1++)
            //{
            //    for (int index2 = 0; index2 < CKYMat.GetLength(1); index2++)
            //    {
            //        string str = "";
            //        foreach (var item in CKYMat[index1, index2])
            //        {
            //            if (!str.Contains(item.leftHandSide))
            //            {
            //                str += item.leftHandSide;
            //            }

            //        }
            //        if (str == "")
            //        {
            //            str = "none";
            //        }
            //        Console.Write(str + "\t");
            //    }
            //    Console.WriteLine();
            //}
            // Проверка наличия генератора S в верхнем углу матрицы для определения, можно ли породить слово
            foreach (ProductionRule prod in LeftSide)
            {
                if (prod.leftHandSide == "S" && CKYMat[0, Word.Length - 1].Contains(prod))
                {
                    Output = true;
                }
            }
            Console.WriteLine(Output); // Вывод true, если слово генерируется грамматикой
            Console.Read(); // Поддержание открытой консоли
        }
    }
}
