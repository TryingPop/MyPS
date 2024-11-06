using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 7
이름 : 배성훈
내용 : N과 M 7
    문제번호 : 15656번

    백트래킹 문제다
    입력값이 전부 달라 중복 순열이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0472
    {

        static void Main472(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());
            int n = ReadInt();
            int r = ReadInt();

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            sr.Close();
            Array.Sort(arr);

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 4096 * r);
            int[] ret = new int[r];
            DFS(0);

            sw.Close();

            void DFS(int _depth)
            {

                if (_depth == r)
                {

                    for (int i = 0; i < r; i++)
                    {

                        sw.Write($"{ret[i]} ");
                    }
                    sw.Write('\n');
                    return;
                }

                for (int i = 0; i < n; i++)
                {

                    ret[_depth] = arr[i];
                    DFS(_depth + 1);
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// cs15656 - rby
// 2023-03-09 22:04:22
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace cs15656
{
    class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static int N;
        static int M;
        static List<int> input = new List<int>();

        static void Main(string[] args)
        {
            int[] line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            M = line[1];
            input = sr.ReadLine().Split(' ').Select(int.Parse).ToList().Distinct().ToList();
            N = input.Count;
            input.Sort();

            Recursion(M);
            sw.Write(sb);

            sw.Close();
            sr.Close();
        }

        static List<int> list = new List<int>();
        static void Recursion(int count)
        {
            if(count == 0)
            {
                foreach (var item in list)
                    sb.Append(item).Append(' ');
                sb.AppendLine();
                return;
            }

            foreach(var item in input)
            {
                list.Add(item);
                Recursion(count - 1);
                list.RemoveAt(list.Count - 1);
            }
        }
    }
}

#endif
}
