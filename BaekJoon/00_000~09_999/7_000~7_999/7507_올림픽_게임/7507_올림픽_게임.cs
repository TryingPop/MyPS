using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 6
이름 : 배성훈
내용 : 올림픽 게임
    문제번호 : 7507번

    그리디, 정렬 문제다.
    출력 형식을 잘못해 계속해서 틀렸다;
    (:을 출력하지 않았다..)

    아이디어는 단순하다.
    최대한 배정하는 방법은 끝 시간으로 정렬한 뒤
    순서대로 들을 수 있는 것을 계속해서 들으면 최대가 보장된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1318
    {

        static void Main1318(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            List<(int d, int s, int e)> arr;

            Solve();
            void Solve()
            {

                int n = ReadInt();
                arr = new(50_000);

                for (int i = 1; i <= n; i++)
                {

                    Input();
                    int ret = GetRet();

                    sw.Write($"Scenario #{i}:\n");
                    sw.Write(ret);
                    sw.Write('\n');
                    sw.Write('\n');
                }
            }

            int GetRet()
            {

                arr.Sort((x, y) => 
                {

                    int ret = x.d.CompareTo(y.d);
                    if (ret == 0) ret = x.e.CompareTo(y.e);
                    return ret;
                });

                int curD = -1;
                int curT = 0;
                int ret = 0;

                for (int i = 0; i < arr.Count; i++)
                {

                    if (curD < arr[i].d 
                        || (curD == arr[i].d && curT <= arr[i].s))
                    {

                        curT = arr[i].e;
                        curD = arr[i].d;
                        ret++;
                    }
                }

                arr.Clear();

                return ret;
            }

            void Input()
            {

                int MUL = 10_000;
                int n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    int d = ReadInt();
                    int s = ReadInt();
                    int e = ReadInt();

                    arr.Add((d, s, e));
                }
            }

            int ReadInt()
            {

                int ret = 0;
                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;

                    ret = c - '0';
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
