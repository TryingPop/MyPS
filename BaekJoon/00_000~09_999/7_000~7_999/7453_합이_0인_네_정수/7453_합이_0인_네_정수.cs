using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 28
이름 : 배성훈
내용 : 합이 0인 네 정수
    문제번호 : 7453번

    중간에서 만나기 문제다
    딕셔너리 자료구조를 이용해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0122
    {

        static void Main122(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            int[] A = new int[len];
            int[] B = new int[len];
            int[] C = new int[len];
            int[] D = new int[len];

            for (int i = 0; i < len; i++)
            {

                A[i] = ReadInt(sr);
                B[i] = ReadInt(sr);
                C[i] = ReadInt(sr);
                D[i] = ReadInt(sr);
            }

            sr.Close();

            Dictionary<int, int> dic = new Dictionary<int, int>(len * len);

            // A, B 경우 계산
            for (int i = 0; i < len; i++)
            {

                for (int j = 0; j < len; j++)
                {

                    int calc = A[i] + B[j];

                    if (dic.ContainsKey(calc)) dic[calc]++;
                    else dic[calc] = 1;
                }
            }

            // C, D 경우 계산
            long ret = 0;
            for (int i = 0; i < len; i++)
            {

                for (int j = 0; j < len; j++)
                {

                    int calc = C[i] + D[j];

                    if (dic.ContainsKey(-calc)) ret += dic[-calc];
                }
            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            bool plus = true;
            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                else if (c == '-')
                {

                    plus = false;
                    continue;
                }

                ret = ret * 10 + c - '0';
            }

            return plus ? ret : -ret;
        }
    }

#if other
using System;
using System.IO;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static int n;
        static long count;
        static int[] A, B, C, D, AB, CD;

        static void Main(string[] args)
        {
            int i, j;
            StringBuilder sb = new StringBuilder();
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            sw.AutoFlush = true;
            n = Convert.ToInt32(sr.ReadLine());
            A = new int[n];
            B = new int[n];
            C = new int[n];
            D = new int[n];
            AB = new int[n * n];
            CD = new int[n * n];
            count = 0;
            for (i = 0; i < n; i++)
            {
                string[] tmp = sr.ReadLine().Split(' ');
                A[i] = Convert.ToInt32(tmp[0]);
                B[i] = Convert.ToInt32(tmp[1]);
                C[i] = Convert.ToInt32(tmp[2]);
                D[i] = Convert.ToInt32(tmp[3]);
            }

            int idx = 0;
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    AB[idx] = A[i] + B[j];
                    CD[idx] = C[i] + D[j];
                    idx++;
                }
            }

            Array.Sort(AB);
            Array.Sort(CD);
            //Console.WriteLine(long.MaxValue);
            //Console.WriteLine(ulong.MaxValue);

            //for (int j = 0; j < AB.Length; j++)
            //{
            //    Console.WriteLine(AB[j] + ", " + CD[j]);
            //    idx++;
            //}

            //for (j = 0; j < 4000; j++)
            //{
            //    Console.WriteLine("0 0 0 0");
            //}

            i = 0;
            j = CD.Length - 1;

            while (i <= AB.Length - 1 && j >= 0)
            {
                if(AB[i] + CD[j] == 0)
                {
                    int cntAB, cntCD;
                    cntAB = cntCD = 1;

                    while (i < AB.Length - 1) // move i
                    {
                        i++;
                        if (AB[i] == AB[i - 1])
                            cntAB++;
                        else
                            break;
                    }

                    while (j > 0) // move j
                    {
                        j--;
                        if (CD[j] == CD[j + 1])
                            cntCD++;
                        else
                            break;
                    }

                    count += ((long)cntAB * (long)cntCD);

                    if (i == AB.Length - 1 && j == 0)
                        break;
                }
                else if (AB[i] + CD[j] < 0 && i <= AB.Length - 1) // i++
                {
                    i++;
                }
                else if (AB[i] + CD[j] > 0 && j >= 0) // j--
                {
                    j--;
                }
            }
            
            sw.WriteLine(count.ToString());
        }
        
    }
}
#elif other2
using System;
using System.Collections.Generic;

namespace sky.BruteForce
{
    public class Ps7453
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[,] mat = new int[n, 4];
            for (int i = 0; i < n; i++)
            {
                var arr = Array.ConvertAll<string, int>(Console.ReadLine().Split(), t => int.Parse(t));
                for (int j = 0; j < 4; j++)
                {
                    mat[i, j] = arr[j];
                }
            }

            //미리 합쳐둠
            List<int> ab = new List<int>();
            List<int> cd = new List<int>();

            int idx = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    ab.Add(mat[i, 0] + mat[j, 1]);
                    cd.Add(mat[i, 2] + mat[j, 3]);

                    idx++;
                }
            }

            ab.Sort();
            cd.Sort();

            int left = 0;
            int right = idx - 1;
            long result = 0;
            while (left < idx && right >= 0)
            {
                if (ab[left] + cd[right] == 0)
                {
                    int tmp = left;
                    long abCount = 0;
                    long cdCount = 0;

                    while (ab[left] + cd[right] == 0)
                    {
                        abCount++;
                        left++;

                        if (left >= idx)
                            break;
                    }

                    while (ab[tmp] + cd[right] == 0)
                    {
                        cdCount++;
                        right--;

                        if (right < 0)
                            break;
                    }

                    result += (abCount * cdCount);
                }
                else if (ab[left] + cd[right] < 0)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }

            Console.Write(result);
        }
    }
}
#endif
}
