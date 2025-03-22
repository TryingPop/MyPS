using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 4
이름 : 배성훈
내용 : 배
    문제번호 : 1092번

    그리디, 정렬 문제다.
    유니온 파인드를 응용해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1343
    {

        static void Main1343(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n, m;
            int[] arr1, arr2;
            int[] group, stk, init;
            int len = 0;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(arr1);
                Array.Sort(arr2);

                if (arr1[n - 1] < arr2[m])
                {

                    Console.Write(-1);
                    return;
                }


                SetArr();

                int chk = m;
                int ret = 0;
                while (chk > 0)
                {

                    ret++;
                    for (int i = 0; i < n; i++)
                    {

                        int cur = Find(init[i]);

                        if (cur == 0) continue;
                        chk--;

                        Union(cur - 1, cur);
                    }
                }

                Console.Write(ret);
            }

            void Union(int _f, int _t)
            {

                _f = Find(_f);
                _t = Find(_t);
                int min = _f < _t ? _f : _t;
                int max = _f < _t ? _t : _f;

                group[max] = min;
            }

            void SetArr()
            {

                group = new int[m + 1];
                stk = new int[m + 1];
                for (int i = 1; i <= m; i++)
                {

                    group[i] = i;
                }

                init = new int[n];
                int s = 1;
                for (int i = 0; i < n; i++)
                {

                    for (; s <= m; s++)
                    {

                        if (arr1[i] < arr2[s]) break;
                    }

                    init[i] = s - 1;
                }
            }

            int Find(int _chk)
            {

                len = 0;
                while (_chk != group[_chk])
                {

                    stk[len++] = _chk;
                    _chk = group[_chk];
                }

                while (len-- > 0)
                {

                    group[stk[len]] = _chk;
                }

                return _chk;
            }

            void Input()
            {

                n = ReadInt();
                arr1 = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr1[i] = ReadInt();
                }

                m = ReadInt();
                arr2 = new int[m + 1];
                for (int i = 1;i <= m; i++)
                {

                    arr2[i] = ReadInt();
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;
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

#if other
// #include <stdio.h>
// #include <stdlib.h>

int comp(const void* _pa, const void* _pb)
{
	return *(const int*)_pb - *(const int*)_pa;
}

int main() {
	int n, m, l, min, a, i, j, max;
	int c[50];
	int b[10000];
	int t[50] = {0,};

	scanf("%d", &n);
	for(i=0; i<n; i++)
		scanf("%d", &c[i]);

	scanf("%d", &m);
	for(i=0; i<m; i++)
		scanf("%d", &b[i]);

	qsort(c, n, sizeof(int), comp);
	qsort(b, m, sizeof(int), comp);

	if (c[0] < b[0])
		l = -1;
	else
	{
		l = 1;
		for(i=0; i<m; i++)
		{
			a = 0;
			while(!a)
			{
				for(j=0; j<n; j++)
				{
					if(b[i] > c[j])
						continue;

					if(t[j] < l)
					{
						t[j]++;
						a = 1;
						break;
					}
				}

				if (j==n)
					l++;
			}
		}
	}

	printf("%d\n", l);

	return 0;
}

#endif
}
