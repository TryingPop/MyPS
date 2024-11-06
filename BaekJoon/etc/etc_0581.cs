using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 20
이름 : 배성훈
내용 : 칠무해
    문제번호 : 14729번

    정렬 문제다
    우선순위 큐를 써서 풀었다

    매번 넣고 최대값인지 확인하고, 1000만개 까지 들어오므로 시간이 많이 걸린다
    처음에는 일단 넣고 최대값을 빼는 식으로 했는데 2초대가 나왔다

    이후, 힙안의 최대값보다 큰 경우만 넣으니 1.5초대까지 줄이고 이외 다른 걸로 1.3초까지 줄였다
*/

namespace BaekJoon.etc
{
    internal class etc_0581
    {

        static void Main581(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 32);
            StreamWriter sw = new(Console.OpenStandardOutput());

            Solve();

            sr.Close();
            sw.Close();

            void Solve()
            {

                PriorityQueue<float, float> q = new(8, Comparer<float>.Create((x, y) => y.CompareTo(x)));

                int n = int.Parse(sr.ReadLine());

                for (int i = 0; i < n; i++)
                {

                    float cur = float.Parse(sr.ReadLine());
                    
                    if (q.Count < 7)
                    {

                        q.Enqueue(cur, cur);
                        continue;
                    }

                    if (q.Peek() < cur) continue;
                    q.Enqueue(cur, cur);
                    q.Dequeue();
                }

                float[] ret = new float[7];
                for (int i = 6; i >= 0; i--)
                {

                    ret[i] = q.Dequeue();
                }

                for (int i = 0; i < 7; i++)
                {

                    sw.Write($"{ret[i]:0.000}\n");
                }
            }
        }
    }

#if other
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

int N = int.Parse(sr.ReadLine()!);

List<double> grades = new();

for(int i = 0; i < 7; ++i) 
    grades.Add(double.Parse(sr.ReadLine()!));

double maxGrade = grades.Max();
for (int i = 7; i < N; ++i) {
    double newGrade = double.Parse(sr.ReadLine()!);

    if (newGrade < maxGrade) {
        int idx = grades.IndexOf(maxGrade);
        grades[idx] = newGrade;
        maxGrade = grades.Max();
    }
}

grades.Sort();
foreach (var g in grades)
    sw.WriteLine(String.Format("{0:0.000}", g));

sw.Close();
sr.Close();
#elif other2
// cs14729_2 - rby
// 2023-06-18 20:11:12
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace cs14729_2
{
    class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int N = int.Parse(sr.ReadLine());
            double num;
            List<double> list = new List<double>();

            for(int i = 0; i < 7; i++)
            {
                list.Add(double.Parse(sr.ReadLine()));
            }
            list.Sort();

            for(int i = 7; i < N; i++)
            {
                num = double.Parse(sr.ReadLine());

                if (list[6] > num)
                {
                    list[6] = num;
                    list.Sort();
                }
            }

            list.Sort();
            foreach(var item in list)
            {
                sb.AppendFormat("{0:0.000}\n", item);
            }


            sw.Write(sb);
            sw.Close();
            sr.Close();
        }
    }
}

#endif
}
