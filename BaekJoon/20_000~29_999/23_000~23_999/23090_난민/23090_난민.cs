using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 31
이름 : 배성훈
내용 : 난민
    문제번호 : 23090번

    우선순위 큐 문제다.
    y의 값은 중앙값 중 작은쪽이 된다.
    우선순위 큐로 관리해 중앙값을 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1660
    {

        static void Main1660(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            long ret = 0;
            
            PriorityQueue<int, int> min = new((n >> 1) + 1), max = new((n >> 1) + 1);
            long minSum = 0L, maxSum = 0L;

            for (int i = 0; i < n; i++)
            {

                int x = Math.Abs(ReadInt());
                int y = ReadInt();
                ret += x;

                Add(y);

                long ret1 = min.Peek();
                long ret2 = GetDisY(ret1);

                sw.Write($"{ret1} {ret2}\n");
            }

            long GetDisY(long _mid)
                => min.Count * _mid + minSum
                    + maxSum - max.Count * _mid + ret;

            void Add(int _val)
            {

                if (min.Count == max.Count) MinAdd(_val);
                else MaxAdd(_val);

                Sort();

                void Sort()
                {

                    if (max.Count == 0) return;

                    while (max.Peek() < min.Peek())
                    {

                        int minPop = MinPop();
                        int maxPop = MaxPop();

                        MinAdd(maxPop);
                        MaxAdd(minPop);
                    }
                }
            }

            void MaxAdd(int _val)
            {

                max.Enqueue(_val, _val);
                maxSum += _val;
            }

            int MaxPop()
            {

                int ret = max.Dequeue();
                maxSum -= ret;
                return ret;
            }

            void MinAdd(int _val)
            {

                min.Enqueue(_val, -_val);
                minSum -= _val;
            }

            int MinPop()
            {

                int ret = min.Dequeue();
                minSum += ret;
                return ret;
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
