using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 16
이름 : 배성훈
내용 : 폭탄 제조
    문제번호 : 2977번

    이분 탐색, 매개 변수 탐색 문제다.
    아이디어는 다음과 같다.
    우선 해집합을 보면 n개 살 수 있으면 n - 1개는 자명하게 살 수 있다.
    반면 m개를 살 수 없으면 m + 1개는 살 수 없다.

    그래서 해집합은 살 수 있으면 1, 살 수 없는 경우 0이라하면 정렬 집합이 된다.
    그래서 m개를 살 수 있는지 매개변수 탐색 방법이 유효하다.

    수의 범위가 크다면 GCD를 구해서 그리디로 찾아야 한다.
    문제의 수의 범위는 작아 살 수 있는 모든 경우를 조사할 수 있다.
    그래서 큰거 묶음으로 조사했다.

    작은걸로 조사해도 충분히 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1118
    {

        static void Main1118(string[] args)
        {

            int n, m;
            int[][] arr;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Console.Write(BinarySearch());
            }

            int BinarySearch()
            {

                int l = 0, r = 200_000;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;
                    if (ChkPossible(mid)) l = mid + 1;
                    else r = mid - 1;
                }

                return l - 1;

                // 해당 금액 가능한지 확인
                bool ChkPossible(int _cnt)
                {

                    int money = m;

                    for (int i = 0; i < n; i++)
                    {

                        // 큰 묶음 가능한 갯수 찾기
                        int needCnt = arr[i][0] * _cnt - arr[i][1];
                        if (needCnt <= 0) continue;
                        int e = needCnt / arr[i][4];
                        if (needCnt % arr[i][4] > 0) e++;

                        // 초기값은 큰걸로만 다 샀을 때이다.
                        int curMoney = e * arr[i][5];
                        // 큰거 bCnt만큼 묶음으로 사고, 작은걸로 나머지 채운다.
                        for (int bCnt = 0; 0 < needCnt; bCnt++, needCnt -= arr[i][4])
                        {

                            int sCnt = needCnt / arr[i][2];
                            if (needCnt % arr[i][2] > 0) sCnt++;
                            int spendMoney = bCnt * arr[i][5] + arr[i][3] * sCnt;
                            curMoney = Math.Min(curMoney, spendMoney);
                        }

                        money -= curMoney;
                        if (money < 0) return false;
                    }

                    return true;
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                arr = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = new int[6];
                    for (int j = 0; j < 6; j++)
                    {

                        arr[i][j] = ReadInt();
                    }
                }

                sr.Close();
                int ReadInt()
                {

                    int c, ret = 0;
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }
        }
    }
}
