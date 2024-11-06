using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 21
이름 : 배성훈
내용 : 케빈 베이컨의 6단계 법칙
    문제번호 : 1389번

    플로이드 워셜로 풀었다
    다익스트라로 처음에 접근하려고 했는데,
    최대 경로 수가 5_000개이고 최대 노드는 100개이다
    다익스트라로 접근하면 5_000 * 5_000 * 100이고,
    플로이드 워셜로 접근하면 100 * 100 * 100 이므로
    플로이드 워셜로 접근했다

    그리고 안쓰는 0번인덱스 비어있어 총합을 저장하는 용도로 썼다
*/

namespace BaekJoon.etc
{
    internal class etc_0073
    {

        static void Main73(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int[,] fw = new int[n + 1, n + 1];
            int MAX = 101;
            for (int i = 1; i <= n; i++)
            {

                for (int j = 1; j <= n; j++)
                {

                    if (i != j) fw[i, j] = MAX;
                }
            }

            int len = ReadInt(sr);
            for (int i = 0; i < len; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                fw[f, b] = 1;
                fw[b, f] = 1;
            }

            sr.Close();

            for (int mid = 1; mid <= n; mid++)
            {

                for (int start = 1; start <= n; start++)
                {

                    if (fw[start, mid] == MAX) continue;
                    for (int end = 1; end <= n; end++)
                    {

                        if (fw[mid, end] == MAX) continue;

                        int calc1 = fw[start, mid] + fw[mid, end];
                        int calc2 = fw[start, end];
                        if (calc1 < calc2) fw[start, end] = calc1;
                    }
                }
            }

            int min = 20_000;
            for (int i = 1;  i <= n; i++)
            {

                for (int j = 1; j <= n; j++)
                {

                    fw[i, 0] += fw[i, j];
                }

                if (min > fw[i, 0]) min = fw[i, 0];
            }

            int ret = 0;
            for (int i = 1; i <= n; i++)
            {

                if (fw[i, 0] != min) continue;
                ret = i;
                break;
            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0, c;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
