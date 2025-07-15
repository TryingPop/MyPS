using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 14
이름 : 배성훈
내용 : 중앙값 구하기
    문제번호 : 2696번

    우선순위 큐 문제다.
    우선순위 큐 2개를 이용해 중앙값을 구했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1766
    {

        static void Main1766(string[] args)
        {

            int HALF = 5_000;
            int LINE = 10;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            Comparer<int> comp = Comparer<int>.Create((x, y) => y.CompareTo(x));
            PriorityQueue<int, int> min= new(HALF, comp), max = new(HALF);

            int t = ReadInt();
            int[] ret = new int[HALF];

            while (t-- > 0)
            {

                int n = ReadInt();
                int len = 0;

                for (int i = 0; i < n; i++)
                {

                    int add = ReadInt();
                    Add(add);

                    if ((i & 1) == 0) 
                    { 
                        
                        AddRet();
                    }
                }

                Output();

                Clear();

                void Output()
                {

                    // 출력형태에 맞춘 출력 함수
                    sw.Write($"{len}\n");

                    for (int i = 0; i < len; i++)
                    {

                        sw.Write($"{ret[i]}");
                        // 문제 조건에서 10개씩 끊어서 출력하라기에 조건에 맞춰 띄어쓰기인지
                        // 줄바꿈인지 구분
                        if (i % 10 == 9 || i == n - 1) sw.Write('\n');
                        else sw.Write(' ');
                    }
                }

                void AddRet()
                {

                    // 중앙값 저장
                    ret[len++] = GetMiddle();
                }

                int GetMiddle()
                    // 중앙값은 max에 담기게 했다.
                    => max.Peek();

                void Add(int _val)
                {

                    // 중앙값이 max의 맨 위에 가게 설정
                    // 먼저 min에 넣는다.
                    min.Enqueue(_val, _val);

                    if (min.Count > max.Count)
                    {

                        // max Peek에 중앙값을 넣기에
                        // max의 개수가 min 보다 크거나 같아야 한다.
                        int val = min.Dequeue();
                        max.Enqueue(val, val);
                    }

                    // while (min.Count > 0 && max.Count > 0 && min.Peek() > max.Peek())
                    if (min.Count == max.Count && min.Peek() > max.Peek())
                    {

                        // 이후 값 옮겨야 하는지 확인
                        int minToMax = min.Dequeue();
                        int maxToMin = max.Dequeue();

                        max.Enqueue(minToMax, minToMax);
                        min.Enqueue(maxToMin, maxToMin);
                    }
                }

                void Clear()
                {

                    min.Clear();
                    max.Clear();

                    sw.Flush();
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;

                }
            }

        }
    }
}
