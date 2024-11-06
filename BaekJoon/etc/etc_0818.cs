using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 17
이름 : 배성훈
내용 : 더 흔한 타일 색칠 문제
    문제번호 : 28298번

    구현, 그리디, 브루트포스 알고리즘 문제다
    처음에 k * k 타일로 전체를 나눈 뒤 
    나누어진 타일들을 하나씩 비교하면서 가장 적게 바꾸는 걸 삼았다가 한 번 틀렸다

    반례는 다음과 같다
        4 4 2    
        BAAB
        AAAA
        AAAA
        BAAB
    이경우 각 모서리에 B 4개만 A로 바꾸면 된다

    그래서 그냥 그리디하게 각 자리에 가장 많이 쓰인걸 기록하고 해당 경우로 태갷ㅆ다
*/

namespace BaekJoon.etc
{
    internal class etc_0818
    {

        static void Main818(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n, m, k;
            int[][] board;

            int row, col;
            int[] cnt;

            int ret1;
            char[][] ret2;

            Solve();
            void Solve()
            {

                Input();

                GetRet();

                Output();
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sw.Write($"{ret1}\n");

                for (int r = 0; r < row; r++)
                {

                    for (int i = 0; i < k; i++)
                    {

                        for (int c = 0; c < col; c++)
                        {

                            for (int j = 0; j < k; j++)
                            {

                                sw.Write(ret2[i][j]);
                            }
                        }

                        sw.Write('\n');
                    }

                    sw.Flush();
                }

                sw.Close();
            }

            void GetRet()
            {

                ret1 = n * m;
                ret2 = new char[k][];

                for (int i = 0; i < k; i++)
                {

                    ret2[i] = new char[k];
                }

                row = n / k;
                col = m / k;

                cnt = new int[26];
                
                for (int i = 0; i < k; i++)
                {


                    for (int j = 0; j < k; j++)
                    {

                        for (int r = 0; r < row; r++)
                        {

                            for (int c = 0; c < col; c++)
                            {

                                int idx = board[r * k + i][c * k + j];
                                cnt[idx]++;
                            }
                        }

                        int max = -1;
                        for (int idx = 0; idx < 26; idx++)
                        {

                            int cur = cnt[idx];
                            cnt[idx] = 0;

                            if (max < cur)
                            {

                                ret2[i][j] = (char)(idx + 'A');
                                max = cur;
                            }
                        }

                        ret1 -= max;
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                k = ReadInt();

                board = new int[n][];

                for (int i = 0; i < n; i++)
                {

                    board[i] = new int[m];
                    for (int j = 0; j < m; j++)
                    {

                        board[i][j] = sr.Read() - 'A';
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// cs28298 - rby
// 2024-04-21 17:41:41
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cs28298
{
    internal class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int[] line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            int N, M, K;
            (N, M, K) = (line[0], line[1], line[2]);

            char[,] map = new char[N, M];
            string tl;
            for(int i = 0; i < N; i++)
            {
                tl = sr.ReadLine();
                for(int j = 0; j < M; j++)
                {
                    map[i, j] = tl[j];
                }
            }

            int n = N / K;
            int m = M / K;
            char[,] sub = new char[K, K];
            int count = 0;
            List<int> list = new List<int>();

            for (int s = 0; s < K; s++)
            {
                for (int t = 0; t < K; t++)
                {
                    list.Clear();
                    for (int i = 0; i < 26; i++)
                        list.Add(0);

                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            list[map[s + i * K, t + j * K] - 'A']++;
                        }
                    }

                    int max = list.Max();
                    count += n * m - max;
                    int idx = list.IndexOf(max);
                    sub[s, t] = (char)(idx + 'A');

                }
            }
            sb.AppendLine(count.ToString());
            for (int i = 0; i < n; i++)
            {
                for (int s = 0; s < K; s++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        for(int t = 0;t< K; t++)
                        {
                            sb.Append(sub[s, t]);
                        }
                    }
                    sb.AppendLine();
                }
            }
            sw.Write(sb);
            sw.Close();
            sr.Close();
        }
    }
}

#endif
}
