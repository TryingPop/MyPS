using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 구간과 쿼리
    문제번호 : 16965번

    간선 이동이 가능한지 판별은 100개 이하의 쿼리이므로 플로이드 워셜로 판별했다
    다른 사람의 풀이를 보니 다익스트라(BFS를 빙자한), DFS 등의 풀이가 있었다
*/

namespace BaekJoon.etc
{
    internal class etc_0193
    {

        static void Main193(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int test = ReadInt(sr);
            List<(int min, int max)> interval = new(test);

            int[,] fw = new int[test, test];
            bool needChkRoot = false;
            while (test-- > 0)
            {

                int op = ReadInt(sr);

                if (op == 1) 
                { 
                    
                    interval.Add((ReadInt(sr), ReadInt(sr)));
                    int cur = interval.Count - 1;
                    for (int i = 0; i < interval.Count - 1; i++)
                    {

                        // 길 연결
                        if (ChkIntersection(interval[i], interval[cur])) fw[i, cur] = 1;
                        if (ChkIntersection(interval[cur], interval[i])) fw[cur, i] = 1;
                    }

                    needChkRoot = true;
                }
                else
                {

                    int f = ReadInt(sr) - 1;
                    int b = ReadInt(sr) - 1;

                    if (needChkRoot)
                    {

                        // 플로이드 워셜로 길 판별
                        needChkRoot = false;
                        int len = interval.Count;
                        for (int mid = 0; mid < len; mid++)
                        {

                            for (int start = 0; start < len; start++)
                            {

                                if (fw[start, mid] == 0) continue;

                                for (int end = 0; end < len; end++)
                                {

                                    if (fw[mid, end] == 0) continue;
                                    fw[start, end] = 1;
                                }
                            }
                        }
                    }

                    sw.WriteLine(fw[f, b]);
                }
            }

            sr.Close();
            sw.Close();
        }

        static bool ChkIntersection((int min, int max) _interval1, (int min, int max) _interval2)
        {

            if (_interval2.min < _interval1.min && _interval1.min < _interval2.max) return true;
            if (_interval2.min < _interval1.max && _interval1.max < _interval2.max) return true;

            return false;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool plus = true;

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
}
