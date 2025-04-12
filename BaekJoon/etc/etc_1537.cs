using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 12
이름 : 배성훈
내용 : LRH 식물
    문제번호 : 2934번

    세그먼트 트리 문제다.
    중복되는 좌표에 꽃이 피는 것을 세면 안된다.
    양끝 세로 선이 있는 곳은 꽃이 안자란다고 한다.
    그래서 양끝을 제외한 줄기의 가로 선이 중요하다.
    해당 가로선에 갯수를 입력한다.

    그리고 해당 좌표는 중복이 안되고 좌표로 탐색하기에 해당 좌표의 값은 사용안되게 빼준다.
    이렇게 쿼리를 진행해가면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1537
    {

        static void Main1537(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int b = 1 << 17;

            int n = ReadInt();
            int[] seg = new int[b << 1];

            for (int i = 0; i < n; i++)
            {

                int l = ReadInt();
                int r = ReadInt();

                int ret = GetVal(l) + GetVal(r);
                Update(l + 1, r - 1);
                sw.Write($"{ret}\n");
            }

            void Update(int _l, int _r)
            {

                int l = b | _l;
                int r = b | _r;

                while (l < r)
                {

                    if ((l & 1) == 1) seg[l++]++;
                    if ((r & 1) == 0) seg[r--]++;
                    l >>= 1;
                    r >>= 1;
                }

                if (l == r) seg[l]++;
            }

            int GetVal(int _chk)
            {

                int ret = 0;
                int idx = b | _chk;

                while (idx > 0)
                {

                    ret += seg[idx];
                    idx >>= 1;
                }

                seg[b | _chk] -= ret;
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
                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
