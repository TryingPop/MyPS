using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 15
이름 : 배성훈
내용 : 나무 자르기
    문제번호 : 13263번

    dp, 볼록 껍질을 이용한 최적화 문제다
    기울기가 단조 감소 함수로 만들어 푼다
    모양이 마치 볼록껍질같아 컨벡스 헐 트릭이라 불린다고 한다
*/

namespace BaekJoon._58
{
    internal class _58_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr;

            int n;
            int[] a, b;

            (long x, long y, double s)[] l;
            long[] dp;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                dp = new long[n];
                l = new (long x, long y, double s)[n];

                int top = 0;

                for (int i = 1; i < n; i++)
                {

                    (long x, long y, double s) temp = (b[i - 1], dp[i - 1], 0.0);
                    while (top > 0)
                    {

                        temp.s = GetCross(ref l[top - 1], ref temp);
                        if (l[top - 1].s <= temp.s) break;

                        top--;
                    }

                    l[top++] = temp;

                    long x = a[i];

                    int fpos = 0;

                    int left = 0;
                    int right = top - 1;

                    while(left <= right)
                    {

                        int mid = (left + right) >> 1;
                        if (l[mid].s < x) 
                        {

                            fpos = mid;
                            left = mid + 1; 
                        }
                        else right = mid - 1;
                    }

                    dp[i] = l[fpos].x * x + l[fpos].y;
                }

                Console.Write(dp[n - 1]);
            }

            double GetCross(ref (long x, long y, double s) _f, ref (long x, long y, double s) _g)
            {

                return (double)(_g.y - _f.y) / (_f.x - _g.x);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                a = new int[n];
                b = new int[n];

                for (int i = 0; i < n; i++)
                {

                    a[i] = ReadInt();
                }

                for (int i = 0; i < n; i++)
                {

                    b[i] = ReadInt();
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
}
