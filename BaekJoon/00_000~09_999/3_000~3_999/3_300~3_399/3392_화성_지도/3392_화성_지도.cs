/*
날짜 : 2024. 11. 18
이름 : 배성훈
내용 : 화성 지도
    문제번호 : 3392번

    세그먼트 트리, 스위핑 문제다
    값의 범위가 3만으로 작아 작은 값으로 표현할 필요는 없다.
    느리게 갱신되는 세그먼트 트리를 이용하면 쉽게 해결되지만,
    이전에 느리게 갱신되는 세그먼트 트리없이 푼 기억이 있어
    세그먼트 트리만을 이용해 접근하려고 했다.

    그러니 2개의 데이터가 필요했다.
    하나는 해당 길이에 값이 있다와 길이를 담는 변수다.
    이렇게 세그먼트트리를 설정하니, 이상없이 풀린다.
*/

using System.Runtime.CompilerServices;

namespace BaekJoon.etc
{
    internal class etc_1122
    {

        static void Main1122(string[] args)
        {

            int n;
            (int x, int sY, int eY, bool add)[] line;
            (int val, int cnt)[] seg;

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void GetRet()
            {

                int bX = 0;
                int ret = 0;
                for (int i = 0; i < line.Length; i++)
                {

                    if (bX != line[i].x)
                    {

                        ret += (line[i].x - bX) * seg[0].val;
                    }

                    Update(0, 30_001, line[i].sY, line[i].eY - 1, line[i].add ? 1 : -1, 0);
                    bX = line[i].x;
                }

                Console.Write(ret);
            }

            void SetArr()
            {

                Array.Sort(line, (x, y) => x.x.CompareTo(y.x));
                seg = new (int val, int cnt)[1 << 16];
            }

            void Update(int _s, int _e, int _chkS, int _chkE, int _cnt, int _idx)
            {

                if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx].cnt += _cnt;
                    SetSegVal(_s, _e, _idx);
                    return;
                }
                else if (_e < _chkS || _chkE < _s) return;

                int mid = (_s + _e) >> 1;
                Update(_s, mid, _chkS, _chkE, _cnt, _idx * 2 + 1);
                Update(mid + 1, _e, _chkS, _chkE, _cnt, _idx * 2 + 2);

                SetSegVal(_s, _e, _idx);
            }

            void SetSegVal(int _s, int _e, int _idx)
            {

                if (_s == _e) seg[_idx].val = seg[_idx].cnt > 0 ? 1 : 0;
                else seg[_idx].val = seg[_idx].cnt > 0 ?
                        _e - _s + 1 : seg[_idx * 2 + 1].val + seg[_idx * 2 + 2].val;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                line = new (int x, int sY, int eY, bool add)[n << 1];

                for (int i = 0; i < n; i++)
                {

                    int sX = ReadInt();
                    int sY = ReadInt();
                    int eX = ReadInt();
                    int eY = ReadInt();

                    line[i * 2] = (sX, sY, eY, true);
                    line[i * 2 + 1] = (eX, sY, eY, false);
                }

                sr.Dispose();
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
}
