using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 19
이름 : 배성훈
내용 : 근성아 일하자
    문제번호 : 32358번

    시뮬레이션, 두 포인터 문제다.
    쿼리가 20만개까지 주어진다.
    그리고 쓰레기를 수거하면 해당 쓰레기가 사라지기에
    매 2번 쿼리마다 정렬해도 시간초과가 나지 않는다.
    즉 시뮬레이션 방법이 유효하다.
*/

namespace BaekJoon.etc
{
    internal class etc_1633
    {

        static void Main1633(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int q = ReadInt();
            int len = 0;
            int[] arr = new int[q];
            long ret = 0;
            int cur = 0;
            while (q-- > 0)
            {

                int op = ReadInt();

                if (op == 1)
                {

                    int next = ReadInt();
                    arr[len++] = next;
                }
                else
                {

                    if (len == 0) continue;
                    Array.Sort(arr, 0, len);
                    int curIdx = 0;
                    int minDis = Math.Abs(arr[0] - cur);

                    for (int i = 1; i < len; i++)
                    {

                        int chk = Math.Abs(arr[i] - cur);
                        if (chk < minDis)
                        {

                            minDis = chk;
                            curIdx = i;
                        }
                    }

                    ret += minDis;
                    cur = arr[curIdx];

                    int lIdx = curIdx - 1, rIdx = curIdx + 1;


                    while (lIdx != -1 || rIdx != len)
                    {

                        if (lIdx == -1) MoveRight();
                        else if (rIdx == len) MoveLeft();
                        else
                        {

                            int lDis = GetDis(curIdx, lIdx);
                            int rDis = GetDis(curIdx, rIdx);

                            if (lDis <= rDis)
                                MoveLeft();
                            else
                                MoveRight();
                        }
                    }

                    cur = arr[curIdx];
                    len = 0;

                    int GetDis(int _fIdx, int _tIdx)
                        => Math.Abs(arr[_fIdx] - arr[_tIdx]);

                    void MoveLeft()
                    {

                        ret += GetDis(curIdx, lIdx);
                        curIdx = lIdx;
                        lIdx--;
                    }

                    void MoveRight()
                    {

                        ret += GetDis(curIdx, rIdx);
                        curIdx = rIdx;
                        rIdx++;
                    }
                }
            }

            Console.Write(ret);

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
