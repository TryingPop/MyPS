using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 29
이름 : 배성훈
내용 : 편광판
    문제번호 : 28475번

        1  2  3  4
        5  6  7  8
        /, |, \, -
    바로 다음께 현재와 2 차이 나는 모양이 없어야한다!

    구간 확인을 반복해서 해야하므로 세그먼트 트리를 쓰는 문제다
    세그먼트 트리를 다음과 같이 설정했다
        현재 구간 이동 가능 여부이다

    그래서 and 연산을 base로 뒀고, and연산에서 항등원 역할을 하는 true를 줬다
    이동 가능여부는 모양이 수직이 아니면 된다
    모양이 수직이라는 말은 현재 상태에서 2, 6 차이가 나면 수직이다

    위 모양에서 4, 6은 수직이고 7, 1도 수직이다

    그리고 모양 변경 시에 앞뒤로 확인했다
*/

namespace BaekJoon.etc
{
    internal class etc_0133
    {

        static void Main133(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int len = ReadInt(sr);
            int test = ReadInt(sr);
            bool[] seg;
            {

                int log = (int)Math.Ceiling(Math.Log2(len)) + 1;
                seg = new bool[1 << log];
            }

            int[] states = new int[len + 1];
            for (int i = 1; i <= len; i++)
            {

                states[i] = ReadInt(sr) % 4;
            }

            for (int i = 1; i < len; i++)
            {

                bool add = ChkPass(states[i + 1], states[i]);
                Update(seg, 1, len - 1, i, add);
            }

            while(test-- > 0)
            {

                int order = ReadInt(sr);

                if (order == 1)
                {

                    int idx = ReadInt(sr);
                    int state = ReadInt(sr) % 4;

                    states[idx] = state;
                    
                    if (idx > 1)
                    {

                        bool add = ChkPass(states[idx - 1], states[idx]);
                        Update(seg, 1, len - 1, idx - 1, add);
                    }

                    if (idx < len)
                    {

                        bool add = ChkPass(states[idx], states[idx + 1]);
                        Update(seg, 1, len - 1, idx, add);
                    }

                    continue;
                }

                int start = ReadInt(sr);
                int end = ReadInt(sr) - 1;

                bool ret = GetVal(seg, 1, len - 1, start, end);

                sw.Write(ret ? "1\n" : "0\n");
            }
            sw.Close();
            sr.Close();
        }

        static bool ChkPass(int _state1, int _state2)
        {

            int chk = _state1 - _state2;
            chk = chk < 0 ? -chk : chk;

            return chk != 2;
        }

        static void Update(bool[] _seg, int _start, int _end, int _changeIdx, bool _changeValue, int _idx = 1)
        {

            if (_start == _end)
            {

                _seg[_idx - 1] = _changeValue;
                return;
            }

            int mid = (_start + _end) / 2;
            if (_changeIdx > mid) Update(_seg, mid + 1, _end, _changeIdx, _changeValue, _idx * 2 + 1);
            else Update(_seg, _start, mid, _changeIdx, _changeValue, _idx * 2);

            _seg[_idx - 1] = _seg[_idx * 2 - 1] && _seg[_idx * 2];
        }

        static bool GetVal(bool[] _seg, int _start, int _end, int _chkStart, int _chkEnd, int _idx = 1)
        {

            if (_chkEnd < _chkStart || _chkEnd < _start || _end < _chkStart) return true;
            else if (_chkStart <= _start && _end <= _chkEnd)
            {

                return _seg[_idx - 1];
            }

            int mid = (_start + _end) / 2;
            bool ret = GetVal(_seg, _start, mid, _chkStart, _chkEnd, _idx * 2);
            ret = ret && GetVal(_seg, mid + 1, _end, _chkStart, _chkEnd, _idx * 2 + 1);

            return ret;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
// #include<bits/stdc++.h>

using namespace std;

const int MAX_N = 2e5 + 5;
const int treeSize = 1 << 18;

int A[MAX_N], n, m;
bool tree[treeSize << 1];

bool check(int a, int b)
{
	return abs(A[a] - A[b]) % 4 != 2;
}

bool query(int l, int r)
{
	bool ret = true;

	for (l += treeSize, r += treeSize; l <= r; l >>= 1, r >>= 1)
	{
		if (l & 1) ret &= tree[l++];
		if (~r & 1) ret &= tree[r--];
	}

	return ret;
}

void update(int x)
{
	int idx = treeSize + x;
	tree[idx] = check(x, x + 1);

	while (idx >>= 1)
	{
		tree[idx] = tree[idx << 1] && tree[idx << 1 | 1];
	}
}


int main()
{
	ios_base::sync_with_stdio(0), cin.tie(0);

	cin >> n >> m;
	for (int i = 0; i < n; ++i)
	{
		cin >> A[i];
	}

	memset(tree, 1, sizeof(tree));
	for (int i = 0; i < n - 1; ++i)
	{
		tree[treeSize + i] = check(i, i + 1);
	}

	for (int i = treeSize - 1; i > 0; --i)
	{
		tree[i] = tree[i << 1] && tree[i << 1 | 1];
	}
	

	int q, a, b;
	for (int i = 0; i < m; ++i)
	{
		cin >> q;
		if (q == 1)
		{
			cin >> a >> b;
			a--;
			A[a] = b;

			if (a == 0)
			{
				update(a);
			}
			else
			{
				update(a - 1);
				update(a);
			}

		}
		else if (q == 2)
		{
			cin >> a >> b;
			a--; b--;

			cout << query(a, b - 1) << '\n';
		}
	}
	return 0;
}
#endif
}
