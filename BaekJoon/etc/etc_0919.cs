using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 28
이름 : 배성훈
내용 : 앵무새
    문제번호 : 14713번

    큐, 구현, 문자열 문제다
    큐를 이용해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0919
    {

        static void Main919(string[] args)
        {

            string YES = "Possible";
            string NO = "Impossible";

            StreamReader sr;

            int n;
            Queue<string>[] q;
            string[] find;

            Solve();
            void Solve()
            {

                Input();

                bool ret = true;
                for (int i = 0; i < find.Length; i++)
                {

                    bool flag = false;
                    for (int j = 0; j < n; j++)
                    {

                        if (q[j].Count == 0 || find[i] != q[j].Peek()) continue;
                        flag = true;
                        q[j].Dequeue();
                    }

                    if (flag) continue;

                    ret = false;
                    break;
                }

                if (ret)
                {

                    for (int i = 0; i < n; i++)
                    {

                        if (q[i].Count == 0) continue;
                        ret = false;
                        break;
                    }
                }

                Console.Write(ret ? YES : NO);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());

                q = new Queue<string>[n];
                for (int i = 0; i < n; i++)
                {

                    string[] temp = sr.ReadLine().Split();
                    q[i] = new(temp.Length);
                    for (int j = 0; j < temp.Length; j++)
                    {

                        q[i].Enqueue(temp[j]);
                    }
                }

                find = sr.ReadLine().Split();
                sr.Close();
            }
        }
    }
}
