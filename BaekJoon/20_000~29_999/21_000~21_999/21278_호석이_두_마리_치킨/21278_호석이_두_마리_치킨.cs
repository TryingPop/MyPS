using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 16
이름 : 배성훈
내용 : 호석이 두 마리 치킨
    문제번호 : 21278번

    풀이가 안떠올라 알고리즘 분류에 적힌 메모를 봐서 풀었다
    플로이드 워셜? 이 있길래 먼저 플로이드 워셜 알고리즘을 실행했고,
    
    다음으로 정해진 값으로 최단 경로 찾는 아이디어가 있는가 고민했지만 따로 안떠올랐고
    브루트포스라는 알고리즘을 보고 찾는걸 그냥 실행해보자는 취지로 했다

    시간은 88ms가 나왔다
*/

namespace BaekJoon.etc
{
    internal class etc_0042
    {

        static void Main42(string[] args)
        {

            // 가질 수 있는 최단 경로보다 큰 값이다
            // 거리가 1이기에 1 * 100 < 10_000
            int MAX = 10_000;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int[][] fw = new int[n + 1][];

            for (int i = 1; i <= n; i++)
            {

                fw[i] = new int[n + 1];
                for (int j = 1; j <= n; j++)
                {

                    if (i != j) fw[i][j] = MAX;
                }
            }

            int l = ReadInt(sr);

            for (int i = 0; i < l; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                // 모든 거리가 1이다!
                fw[f][b] = 1;
                fw[b][f] = 1;
            }
            sr.Close();

            // 플로이드 워셜 알고리즘
            // n <= 100이기에 썼다!
            for (int mid = 1; mid <= n; mid++)
            {

                for (int start = 1; start <= n; start++)
                {

                    if (fw[start][mid] == MAX) continue;
                    for (int end = 1; end <= n; end++)
                    {

                        if (fw[mid][end] == MAX) continue;
                        int chk = fw[start][mid] + fw[mid][end];
                        if (chk < fw[start][end]) fw[start][end] = chk;
                    }
                }
            }

            // 이젠 브루트 포스로 모든 경우 비교한다
            // 많아야 50만 연산
            int ret = MAX * n;
            int pos1 = -1, pos2 = -1;

            for (int i = 1; i < n; i++) 
            { 
            
            
                for (int j = i + 1; j <= n; j++)
                {

                    int chk = 0;
                    for (int k = 1; k <= n; k++)
                    {

                        chk += fw[i][k] < fw[j][k] ? fw[i][k] : fw[j][k];
                    }

                    if (chk < ret)
                    {

                        ret = chk;
                        pos1 = i;
                        pos2 = j;
                    }
                }
            }

            // 출력 형식에 맞춰 출력
            // 왕복 비용이므로 *2배
            Console.WriteLine($"{pos1} {pos2} {ret * 2}");
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
