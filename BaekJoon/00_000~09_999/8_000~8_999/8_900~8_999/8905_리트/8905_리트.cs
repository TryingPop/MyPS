using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 21
이름 : 배성훈
내용 : 리트
    문제번호 : 8905번

    브루트포스, 백트래킹 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1721
    {

        static void Main1721(string[] args)
        {

            string Y = "1\n";
            string N = "0\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            string a, b;
            int k;

            int[] change = new int[255];
            int[] len = new int[255];
            int t = int.Parse(sr.ReadLine());

            while (t-- > 0)
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                if (DFS()) sw.Write(Y);
                else sw.Write(N);

                bool DFS(int _aIdx = 0, int _bIdx = 0)
                {

                    if (_aIdx == a.Length && _bIdx == b.Length) return true;
                    else if (_aIdx == a.Length || _bIdx == b.Length) return false;

                    int cur = a[_aIdx];

                    if (len[cur] == 0)
                    {

                        for (int i = 1; i <= k; i++)
                        {

                            int chk = Read(i);
                            if (chk == -1) break;
                            change[cur] = chk;
                            len[cur] = i;

                            if (DFS(_aIdx + 1, _bIdx + i)) return true;
                        }

                        len[cur] = 0;
                    }
                    else
                    {

                        int chk = Read(len[cur]);
                        if (change[cur] == chk && DFS(_aIdx + 1, _bIdx + len[cur])) return true;
                    }

                    return false;

                    int Read(int _len)
                    {

                        int e = _bIdx + _len;
                        if (e > b.Length) return -1;

                        int ret = 0;
                        for (int i = _bIdx; i < e; i++)
                        {

                            ret = ret * 256 + b[i];
                        }

                        return ret;
                    }
                }
            }

            void Input()
            {

                k = int.Parse(sr.ReadLine());
                a = sr.ReadLine();
                b = sr.ReadLine();

                Array.Fill(change, 0);
                Array.Fill(len, 0);
            }
        }
    }
}
