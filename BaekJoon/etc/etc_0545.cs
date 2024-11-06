using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 16
이름 : 배성훈
내용 : 생일
    문제 : 5635번

    구현, 문자열, 정렬 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0545
    {

        static void Main545(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());

            int n = int.Parse(sr.ReadLine());

            int[] minY = { 9999, 12, 31 };
            int[] maxY = { 1, 1, 1 };
            int[] calc = { 0, 0, 0 };

            string min = string.Empty;
            string max = string.Empty;
            for (int i = 0; i < n; i++)
            {

                Calc();
            }
            sr.Close();

            Console.Write($"{max}\n{min}");

            void Calc()
            {

                string[] temp = sr.ReadLine().Split();

                calc[0] = int.Parse(temp[3]);
                calc[1] = int.Parse(temp[2]);
                calc[2] = int.Parse(temp[1]);

                if (Comp(minY, calc) > 0)
                {

                    minY[0] = calc[0];
                    minY[1] = calc[1];
                    minY[2] = calc[2];
                    min = temp[0];
                }

                if (Comp(maxY, calc) < 0)
                {

                    maxY[0] = calc[0];
                    maxY[1] = calc[1];
                    maxY[2] = calc[2];
                    max = temp[0];
                }

                return;
            }

            int Comp(int[] _arr1, int[] _arr2)
            {

                for (int i = 0; i < 3; i++)
                {

                    if (_arr1[i] == _arr2[i]) continue;
                    return _arr1[i].CompareTo(_arr2[i]);
                }

                return 0;
            }
        }
    }

#if other
using System;

namespace _5635
{
    class Student
    {
        public string name;
        public int day;
        public int month;
        public int year;

        public int ymd;

        public Student(string name, int day, int month, int year)
        {
            this.name = name;
            this.day = day;
            this.month = month;
            this.year = year;
        }

        public void MakeYmd()
        {
            ymd = year * 10000 + month * 100 + day;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int n = int.Parse(input);

            
            
            Student[] student = new Student[n];
           
            for(int i = 0; i < n;i++)
            {
                
                input = Console.ReadLine();
                string[] arrinput = input.Split();
                string name = arrinput[0];
                int day = int.Parse(arrinput[1]);
                int month = int.Parse(arrinput[2]);
                int year = int.Parse(arrinput[3]);

                student[i] = new Student(name, day, month, year);
                student[i].MakeYmd();

            }
            
            int max = int.MinValue;
            string maxName = " ";
            for(int i = 0; i < n; i++)
            {
                if (max < student[i].ymd)
                {
                    max = student[i].ymd;
                    maxName = student[i].name;
                }
                
            }
            int min = int.MaxValue;
            string minName = " ";
            for (int i = 0; i < n; i++)
            {
                if (min > student[i].ymd)
                {
                    min = student[i].ymd;
                    minName = student[i].name;
                }

            }
            Console.WriteLine(maxName);
            Console.WriteLine(minName);
        }
    }
}

#endif
}
