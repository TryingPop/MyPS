using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 20
이름 : 배성훈
내용 : 도로의 개수
    문제번호 : 1577번

    dp, 집합과 맵 문제다.
    정렬 순서를 잘못해 계속해서 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1831
    {

        static void Main1831(string[] args)
        {

#if first
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int row = ReadInt();
            int col = ReadInt();

            long[][] dp = new long[2][];
            dp[0] = new long[col + 1];
            dp[1] = new long[col + 1];

            int k = ReadInt();
            (int r, int c, bool w)[] obs = new (int r, int c, bool w)[k];

            for (int i = 0; i < k; i++)
            {

                (int r, int c) p1 = (ReadInt(), ReadInt());
                (int r, int c) p2 = (ReadInt(), ReadInt());

                if (p2.r < p1.r || p2.c < p1.c)
                {

                    // Swap
                    (int r, int c) tmp = p1;
                    p1 = p2;
                    p2 = tmp;
                }

                if (p1.r == p2.r)
                    // - 형태
                    obs[i] = (p1.r, p1.c, true);
                else
                    // | 형태
                    obs[i] = (p1.r, p1.c, false);
            }

            Array.Sort(obs, (x, y) => {

                int ret = x.r.CompareTo(y.r);
                if (ret == 0) ret = x.w == y.w ? 0 : (x.w ? -1 : 1);
                if (ret == 0) ret = x.c.CompareTo(y.c);
                return ret;
            });

            int oidx = 0;
            dp[0][0] = 1;
            for (int r = 0; r < row; r++)
            {

                // Right 이동
                for (int c = 0; c < col; c++)
                {

                    if (k == oidx || !obs[oidx].w || obs[oidx].r != r || obs[oidx].c != c)
                        dp[0][c + 1] += dp[0][c];
                    else if (oidx < k) oidx = AddIdx(oidx);
                }

                // Up 이동
                for (int c = 0; c <= col; c++)
                {

                    if (k == oidx || obs[oidx].w || obs[oidx].r != r || obs[oidx].c != c)
                        dp[1][c] += dp[0][c];
                    else if (oidx < k) oidx = AddIdx(oidx);
                }

                // Swap
                for (int c = 0; c <= col; c++)
                {

                    dp[0][c] = dp[1][c];
                    dp[1][c] = 0;
                }
            }

            for (int c = 0; c < col; c++)
            {

                if (k == oidx || !obs[oidx].w || obs[oidx].r != row || obs[oidx].c != c)
                    dp[0][c + 1] += dp[0][c];
                else if (oidx < k) oidx = AddIdx(oidx);
            }

            Console.Write(dp[0][col]);

            int AddIdx(int _idx)
            {

                _idx++;
                while (_idx < k && obs[_idx - 1] == obs[_idx])
                {

                    _idx++;
                }

                _idx = k < _idx ? k : _idx;
                return _idx;
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

#else

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int row = ReadInt();
            int col = ReadInt();

            long[] dp = new long[col + 1];

            int k = ReadInt();
            (int r, int c, bool w)[] obs = new (int r, int c, bool w)[k];

            for (int i = 0; i < k; i++)
            {

                (int r, int c) p1 = (ReadInt(), ReadInt());
                (int r, int c) p2 = (ReadInt(), ReadInt());

                if (p2.r < p1.r || p2.c < p1.c)
                {

                    // Swap
                    (int r, int c) tmp = p1;
                    p1 = p2;
                    p2 = tmp;
                }

                if (p1.r == p2.r)
                    // - 형태
                    obs[i] = (p1.r, p1.c, true);
                else
                    // | 형태
                    obs[i] = (p1.r, p1.c, false);
            }

            Array.Sort(obs, (x, y) => {

                int ret = x.r.CompareTo(y.r);
                if (ret == 0) ret = x.w == y.w ? 0 : (x.w ? -1 : 1);
                if (ret == 0) ret = x.c.CompareTo(y.c);
                return ret;
            });

            int oidx = 0;
            dp[0] = 1;
            for (int r = 0; r < row; r++)
            {

                // Right 이동
                for (int c = 0; c < col; c++)
                {

                    if (k == oidx || !obs[oidx].w || obs[oidx].r != r || obs[oidx].c != c)
                        dp[c + 1] += dp[c];
                    else if (oidx < k) oidx = AddIdx(oidx);
                }

                // Up 이동
                for (int c = 0; c <= col; c++)
                {

                    if (k == oidx || obs[oidx].w || obs[oidx].r != r || obs[oidx].c != c) continue;
                    else
                    {

                        dp[c] = 0;
                        if (oidx < k) oidx = AddIdx(oidx);
                    }
                }
            }

            for (int c = 0; c < col; c++)
            {

                if (k == oidx || !obs[oidx].w || obs[oidx].r != row || obs[oidx].c != c)
                    dp[c + 1] += dp[c];
                else if (oidx < k) oidx = AddIdx(oidx);
            }

            Console.Write(dp[col]);

            int AddIdx(int _idx)
            {

                _idx++;
                while (_idx < k && obs[_idx - 1] == obs[_idx])
                {

                    _idx++;
                }

                _idx = k < _idx ? k : _idx;
                return _idx;
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
#endif
        }
    }
}
