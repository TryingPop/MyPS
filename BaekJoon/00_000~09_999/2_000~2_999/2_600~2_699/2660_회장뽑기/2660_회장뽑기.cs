using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 26
이름 : 배성훈
내용 : 회장뽑기
    문제번호 : 2660번

    플로이드 워셜로 회원 점수를 세어주면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0097
    {

        static void Main97(string[] args)
        {

            // len 의 최대값이 50이므로 넉넉하게 100으로 설정
            int MAX = 100;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int len = ReadInt(sr);

            int[,] fw = new int[len + 1, len + 1];

            for (int i = 1; i <= len; i++)
            {

                for (int j = 1; j <= len; j++)
                {

                    if (i != j) fw[i, j] = MAX;
                }
            }

            while(true)
            {

                int f = ReadInt(sr);
                if (f == -1) break;

                int b = ReadInt(sr);

                fw[f, b] = 1;
                fw[b, f] = 1;
            }

            sr.Close();

            for (int mid = 1; mid <= len; mid++)
            {

                for (int start = 1; start <= len; start++)
                {

                    if (fw[start, mid] == MAX) continue;

                    for (int end = 1; end <= len; end++)
                    {

                        if (fw[mid, end] == MAX) continue;

                        int calc = fw[start, mid] + fw[mid, end];
                        if (fw[start, end] > calc) fw[start, end] = calc;
                    }
                }
            }

            int min = MAX;
            int cnt = 0;
            for (int i = 1; i <= len; i++)
            {

                for (int j = 1; j <= len; j++)
                {

                    // 문제 조건에서 fw의 값 중 MAX는 없다
                    fw[i, 0] = fw[i, 0] < fw[i, j] ? fw[i, j] : fw[i, 0];
                }

                if (min > fw[i, 0])
                {

                    min = fw[i, 0];
                    cnt = 1;
                }
                else if (min == fw[i, 0]) cnt++;
            }

            StreamWriter sw = new(Console.OpenStandardOutput());

            sw.Write($"{min} {cnt}\n");

            for (int i = 1; i <= len; i++)
            {

                if (fw[i, 0] == min)
                {

                    sw.Write(i);
                    sw.Write(' ');
                }
            }

            sw.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            bool plus = true;

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
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
}
