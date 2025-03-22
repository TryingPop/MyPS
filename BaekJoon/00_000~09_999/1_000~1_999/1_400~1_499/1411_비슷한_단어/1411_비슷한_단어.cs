using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 20
이름 : 배성훈
내용 : 비슷한 단어
    문제번호 : 1411번

    문자열, 브루트포스 문제다.
    해당 문제에서 A와 B가 비슷한 단어란 말은
    A에서 첫번째 문자와 같은 인덱스 그룹을 i와 B에서 첫 번째 문자와 같은 인덱스 그룹 j라 할 때,
    i != j이면 비슷한 단어가 될 수 없다.

    이렇게 모든 자리에서 같은 그룹을 찾고, B와 같은지 확인하면 된다.
    다만 구현에서 비교하는 부분을 엄밀히 안해 2번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1426
    {

        static void Main1426(string[] args)
        {

            int n;
            string[] strs;
            bool[] visit;
            int[] chkIdx, curIdx;

            Input();

            GetRet();

            void GetRet()
            {

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (visit[i]) continue;
                    visit[i] = true;
                    FillCnt(i, chkIdx);
                    int cnt = 0;
                    for (int j = i + 1; j < n; j++)
                    {

                        if (visit[j]) continue;
                        if (ChkStr(j))
                        {

                            visit[j] = true;
                            cnt++;
                        }
                    }

                    ret += cnt * (cnt + 1) / 2;
                }

                Console.Write(ret);

                bool ChkStr(int _idx)
                {

                    FillCnt(_idx, curIdx);

                    for (int i = 0; i < strs[_idx].Length; i++)
                    {

                        if (curIdx[i] == chkIdx[i]) continue;
                        return false;
                    }

                    return true;
                }

                void FillCnt(int _idx, int[] _arr)
                {

                    Array.Fill(_arr, -1);
                    for (int i = 0; i < strs[_idx].Length; i++)
                    {

                        if (_arr[i] != -1) continue;
                        _arr[i] = i;
                        for (int j = i + 1; j < strs[_idx].Length; j++)
                        {

                            if (strs[_idx][i] == strs[_idx][j]) _arr[j] = i;
                        }
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());

                strs = new string[n];
                for (int i = 0; i < n; i++)
                {

                    strs[i] = sr.ReadLine().Trim();
                }

                chkIdx = new int[strs[0].Length];
                curIdx = new int[strs[0].Length];
                visit = new bool[n];
            }
        }
    }
}
