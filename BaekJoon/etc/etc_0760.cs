using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 19
이름 : 배성훈
내용 : 좋다
    문제번호 : 1253번

    정렬, 투 포인터 문제다
    다른 두 수로 해당 수를 표현할 수 있는지 확인해야한다
    그래서 해당 수를 포함해도 안되고, 선택한 수도 서로 달라야한다
    해당 수를 포함해도 되는줄 알아 1번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_0760
    {
        static void Main760(string[] args)
        {

            StreamReader sr;

            int n;
            int[] arr;

            Solve();

            void Solve()
            {

                Input();
                Array.Sort(arr);

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int find = arr[i];
                    int l = 0;
                    int r = n - 1;
                    while(l < r)
                    {

                        int cur = arr[l] + arr[r];
                        if (cur < find)
                        {

                            l++;
                            if (l == i) l++;
                        }
                        else if (cur > find)
                        {

                            r--;
                            if (r == i) r--;
                        }
                        else
                        {

                            ret++;
                            break;
                        }
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool plus = c != '-';

                int ret = plus ? c - '0' : 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
}
