using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 7
이름 : 배성훈
내용 : 추출하는 폴도 바리스타입니다
    문제번호 : 15648번

    세그먼트 트리 문제다
    앞 부분에 가능한 최대 길이만 모아놓고 풀면 되겠네 하고 접근했다
    막상 구현하려니, 세그먼트 트리 한 개로는 힘듦을 깨닫고
    나눠서 구현했다
*/

namespace BaekJoon._57
{
    internal class _57_02
    {

        static void Main2(string[] args)
        {

            int MAX = 500_000;
            StreamReader sr;
            int n, k, d;
            int[] rangeSeg, modSeg;

            Solve();

            void Solve()
            {

                Init();

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    int mod = cur % k;
                    int chk1 = Query(rangeSeg, 0, MAX, cur - d, cur + d);
                    int chk2 = Query(modSeg, 0, k, mod, mod);

                    int chk = Math.Max(chk1 + 1, chk2 + 1);
                    SetVal(rangeSeg, 0, MAX, cur, chk);
                    SetVal(modSeg, 0, k, mod, chk);

                    if (ret < chk) ret = chk;
                }
                Console.Write(ret);
                sr.Close();
            }

            int Query(int[] _seg, int _s, int _e, int _chkS, int _chkE)
            {

                if (_chkS < _s) _chkS = _s;
                if (_e < _chkE) _chkE = _e;

                return GetVal(_seg, _s, _e, _chkS, _chkE);
            }

            int GetVal(int[] _seg, int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                if (_chkE < _s || _e < _chkS) return 0;
                else if (_chkS <= _s && _e <= _chkE) return _seg[_idx];

                int mid = (_s + _e) >> 1;

                int ret = Math.Max(GetVal(_seg, _s, mid, _chkS, _chkE, _idx * 2 + 1),
                    GetVal(_seg, mid + 1, _e, _chkS, _chkE, _idx * 2 + 2));

                return ret;
            }

            void SetVal(int[] _seg, int _s, int _e, int _chk, int _val, int _idx = 0)
            {

                if (_s == _e)
                {

                    _seg[_idx] = _val;
                    return;
                }

                int mid = (_s + _e) >> 1;
                if (mid < _chk) SetVal(_seg, mid + 1, _e, _chk, _val, _idx * 2 + 2);
                else SetVal(_seg, _s, mid, _chk, _val, _idx * 2 + 1);

                _seg[_idx] = Math.Max(_seg[_idx * 2 + 1], _seg[_idx * 2 + 2]);
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();
                d = ReadInt();


                int log1 = 1 + (int)Math.Ceiling(Math.Log2(MAX));
                int log2 = 1 + (int)Math.Ceiling(Math.Log2(k + 1));

                rangeSeg = new int[1 << log1];
                modSeg = new int[1 << log2];
            }

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

#if other
using System;

public class Program
{
    static void Main()
    {
        int[] nkd = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        int n = nkd[0], k = nkd[1], d = nkd[2];
        int[] array = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        int[] dp = new int[k];
        int answer = 0;
        for (int i = 0; i < n; i++)
        {
            int mod = array[i] % k;
            int cur = Math.Max(dp[mod], Query(Math.Max(1, array[i] - d), Math.Min(500000, array[i] + d))) + 1;
            dp[mod] = Math.Max(dp[mod], cur);
            Update(array[i], cur);
            answer = Math.Max(answer, cur);
        }
        Console.Write(answer);
    }
    static int[] tree = new int[1 << 20];
    static void Update(int idx, int value)
    {
        Update(1, 500000, 1, idx, value);
    }
    static void Update(int start, int end, int node, int idx, int value)
    {
        if (start > idx || end < idx)
            return;
        if (start == end)
        {
            tree[node] = Math.Max(tree[node], value);
            return;
        }
        int mid = (start + end) / 2;
        Update(start, mid, node * 2, idx, value);
        Update(mid + 1, end, node * 2 + 1, idx, value);
        tree[node] = Math.Max(tree[node * 2], tree[node * 2 + 1]);
    }
    static int Query(int left, int right)
    {
        return Query(1, 500000, 1, left, right);
    }
    static int Query(int start, int end, int node, int left, int right)
    {
        if (start > right || end < left)
            return int.MinValue;
        if (start >= left && end <= right)
            return tree[node];
        int mid = (start + end) / 2;
        return Math.Max(Query(start, mid, node * 2, left, right), Query(mid + 1, end, node * 2 + 1, left, right));
    }
}
#endif
}
