using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 전구 상태 바꾸기
    문제번호 : 30023번

    그리디로 풀었다
    만약 해집합이 존재하는 경우
    앞에서부터 최소한도로 색을 변경해야한다
    그래서 R, G, B각각에 색을 변경하며 진행했다

    그리고 깔맞춤이 가능한 경우 이 중에 최소 값을 결과로 해서 
    제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0183
    {

        static void Main183(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = int.Parse(sr.ReadLine());

            int[] R = new int[n];
            int[] G = new int[n];
            int[] B = new int[n];
            
            for (int i = 0; i < n; i++)
            {

                int c = sr.Read();
                if (c == 'R')
                {

                    R[i] = 1;
                    G[i] = 1;
                    B[i] = 1;
                }
                else if (c == 'G') 
                { 
                    
                    R[i] = 2; 
                    G[i] = 2; 
                    B[i] = 2;
                }
                else
                {

                    R[i] = 3;
                    G[i] = 3;
                    B[i] = 3;
                }
            }

            sr.Close();

            int r = 0;
            int g = 0;
            int b = 0;


            for (int i = 0; i < n - 2; i++)
            {

                if (R[i] != 1)
                {

                    int diff = 4 - R[i];
                    r += diff;
                    R[i] = 1;
                    R[i + 1] = Add(R[i + 1], diff);
                    R[i + 2] = Add(R[i + 2], diff);
                }

                if (G[i] != 2)
                {

                    int diff = G[i] == 1 ? 1 : 2;
                    g += diff;
                    G[i] = 2;
                    G[i + 1] = Add(G[i + 1], diff);
                    G[i + 2] = Add(G[i + 2], diff);
                }

                if (B[i] != 3)
                {

                    int diff = 3 - B[i];
                    b += diff;
                    B[i] = 3;
                    B[i + 1] = Add(B[i + 1], diff);
                    B[i + 2] = Add(B[i + 2], diff);
                }
            }

            bool canR = (R[n - 1] == 1 && R[n - 2] == 1);
            bool canG = (G[n - 1] == 2 && G[n - 2] == 2);
            bool canB = (B[n - 1] == 3 && B[n - 2] == 3);

            int ret = 10_000_000;
            if (canR && ret > r) ret = r;
            if (canG && ret > g) ret = g;
            if (canB && ret > b) ret = b;

            if (ret == 10_000_000) ret = -1;
            Console.WriteLine(ret);
        }

        static int Add(int _a, int _add)
        {

            int ret = _a + _add;
            if (ret > 3) ret -= 3;
            return ret;
        }
    }
}
