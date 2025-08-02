using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 1
이름 : 배성훈
내용 : 모든 곳을 안전하게
    문제번호 : 27165번

    ... 문제를 잘못 해석해서 삽질을 엄청했다
    잘못 해석한 부분은 다음과 같다
    cnt로 주어진 값이 i번째 말의 위치인줄 알았다;
    현실은 i번째 위치에 말이 몇 개 있는지 알려주는 값이다;

    그래서 자리에 따른 말의 개수연산을 반복해서 하므로 자꾸 틀렸다;
    로직, 버퍼 사이즈가 작아 입력이 끊기는 가를 의심했다
    둘 다 이상없는데도 틀려서, 다시 문제를 읽어보니 잘못된걸 찾았다;

    주된 아이디어는 다음과 같다
    돌이 1개 놓인 곳의 개수로 상황을 나누어 계산하면 쉽게 풀린다
*/

namespace BaekJoon.etc
{
    internal class etc_0140
    {

        static void Main140(string[] args)
        {

#if Wrong

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 4096 * 1024);

            int n = int.Parse(sr.ReadLine().Trim());
            int[] cnt = new int[n + 1];
            {

                int[] temp = sr.ReadLine().Trim().Split(' ').Select(int.Parse).ToArray();
                for (int i = 0; i <= n; i++)
                {

                    cnt[temp[i]]++;
                }
            }

            int remain = int.Parse(sr.ReadLine().Trim());
            sr.Close();

            bool impossible = false;
            int first = -1;
            int second = -1;

            for (int i = 0; i <= n; i++)
            {

                if (cnt[i] == 1)
                {

                    if (first == -1) first = i;
                    else if (second == -1) second = i;
                    else
                    {

                        impossible = true;
                        break;
                    }
                } 
            }

            int f = -1;
            int b = -1;
            if (!impossible)
            {

                if (second != -1)
                {

                    if (first + remain == second)
                    {

                        f = first;
                        b = second;
                    }
                    else impossible = true;
                }
                else if (first != -1)
                {

                    bool find = false;
                    if (first - remain >= 0) 
                    { 
                        
                        impossible = cnt[first - remain] == 2 || cnt[first - remain] == 0;
                        if (!impossible)
                        {

                            find = true;
                            f = first - remain;
                            b = first;
                        }
                    }

                    if (!find && first + remain <= n)
                    {

                        impossible = cnt[first + remain] == 0;
                        if (!impossible)
                        {

                            f = first;
                            b = first + remain;
                        }
                    }
                }
                else
                {

                    bool allZero = true;
                    impossible = true;
                    for (int i = 0; i <= n - remain; i++)
                    {

                        if (cnt[i] == 0) continue;
                        allZero = false;

                        if (cnt[i] == 2) continue;
                        if (cnt[i + remain] == 0) continue;
                        impossible = false;
                        f = i;
                        b = i + remain;
                        break;
                    }

                    if (allZero) 
                    { 
                        
                        impossible = false;

                    }
                }
            }

            if (impossible) Console.WriteLine("NO");
            else
            {

                Console.WriteLine("YES");
                Console.Write($"{f} {b}");
            }
#endif

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int[] cnt = new int[n + 1];

            for (int i = 0; i <=n; i++)
            {

                cnt[i] = ReadInt(sr);
            }

            int remain = ReadInt(sr);
            sr.Close();

            int first = -1;
            int second = -1;
            bool impossible = false;
            for (int i = 0; i <= n; i++)
            {

                if (cnt[i] == 1)
                {

                    if (first == -1) first = i;
                    else if (second == -1) second = i;
                    else 
                    { 
                        
                        impossible = true;
                        break;
                    }
                }
            }
            int f = -1, b = -1;

            if (!impossible)
            {

                if (second != -1)
                {

                    if (first + remain == second)
                    {

                        f = first;
                        b = second;
                    }
                    else impossible = true;
                }
                else if (first != -1)
                {

                    impossible = true;
                    bool find = false;
                    if (first - remain >= 0)
                    {

                        impossible = cnt[first - remain] == 2 || cnt[first - remain] == 0;
                        if (!impossible)
                        {

                            find = true;
                            f = first - remain;
                            b = first;
                        }
                    }

                    if (!find && first + remain <= n)
                    {

                        impossible = cnt[first + remain] == 0;
                        if (!impossible)
                        {

                            f = first;
                            b = first + remain;
                        }
                    }
                }
                else
                {

                    impossible = true;
                    for (int i = 0; i <= n - remain; i++)
                    {

                        if (cnt[i] == 0 || cnt[i] == 2) continue;

                        if (cnt[i + remain] == 0) continue;

                        impossible = false;
                        f = i;
                        b = i + remain;
                    }
                }
            }

            if (impossible) Console.Write("NO");
            else 
            { 
                
                Console.Write("YES\n");
                if (f != -1) Console.WriteLine($"{f} {b}");
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
