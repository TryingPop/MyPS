using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 8
이름 : 배성훈
내용 : 방어선
    문제번호 : 3429번

    세그먼트 트리, dp 문제다.
    아이디어는 다음과 같다.
    연속된 가장 긴 길이를 찾아야 한다.
    O(N)에 현재 지점을 시작으로 하는 가장 긴 길이 s[i]와 
    현재 지점을 끝으로 하는 가장 긴 길이 e[i]를 모두 찾는다.

    그러면 정답은 Max(s[i] + e[j])가 될것이다.
    여기서 i를 독립변수라 하면 j는 j < i이고 arr[j] < arr[i]인 모든 j이다.
    일반적으로 모든 j를 찾으면 N^2의 시간이 걸린다.

    그런데 j < i의 최댓값을 저장하는 세그먼트 트리를 만들면 log n 시간에 찾을 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1812
    {

        static void Main1812(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int b = 1 << 18;
            int[] seg = new int[b << 1];

            int[] arr = new int[200_003];
            int[] s = new int[200_003];
            int[] e = new int[200_003];
            int[] compact = new int[200_003];
            int n;

            Comparer<int> comp = Comparer<int>.Create((x, y) =>
            {

                int ret = arr[x].CompareTo(arr[y]);
                if (ret == 0) ret = y.CompareTo(x);
                return ret;
            });

            int t = ReadInt();
            int ret;

            while (t-- > 0)
            {

                Input();

                Compact();

                SetInit();

                GetRet();
            }

            void GetRet()
            {

                Array.Fill(seg, 0);

                ret = 1;
                for (int i = 1; i <= n; i++)
                {

                    ret = Math.Max(ret, e[i]);
                    ret = Math.Max(ret, s[i] + GetVal(arr[i]));

                    Update(arr[i], e[i]);
                }

                sw.Write($"{ret}\n");
            }

            void Update(int _idx, int _val)
            {

                int idx = b | _idx;
                seg[idx] = Math.Max(seg[idx], _val);

                while (idx > 1)
                {

                    int p = idx >> 1;
                    seg[p] = Math.Max(seg[idx ^ 1], seg[idx]);
                    idx = p;
                }
            }

            int GetVal(int _idx)
            {

                int l = b | 1;
                int r = b | _idx;

                int ret = 0;
                while (l <= r)
                {

                    if ((l & 1) == 1) ret = Math.Max(ret, seg[l++]);
                    if ((r & 1) == 0) ret = Math.Max(ret, seg[r--]);

                    l >>= 1;
                    r >>= 1;
                }

                if (l == r) ret = Math.Max(ret, seg[l]);
                return ret;
            }

            void SetInit()
            {

                for (int i = 1; i <= n; i++)
                {

                    if (arr[i] > arr[i - 1]) e[i] = e[i - 1] + 1;
                    else e[i] = 1;
                }

                s[n + 1] = 0;
                for (int i = n; i > 0; i--)
                {

                    if (arr[i] < arr[i + 1]) s[i] = s[i + 1] + 1;
                    else s[i] = 1;
                }
            }

            void Compact()
            {

                Array.Sort(compact, 1, n, comp);

                for (int i = 1; i <= n; i++)
                {

                    int idx = compact[i];
                    arr[idx] = i;
                }
            }

            void Input()
            {

                n = ReadInt();
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                    compact[i] = i;
                }
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
