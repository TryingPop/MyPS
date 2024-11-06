using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 20
이름 : 배성훈
내용 : Hello World!
    문제번호 : 13140번

    수학, 브루트포스 알고리즘 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0829
    {

        static void Main829(string[] args)
        {

            string LINE = "-------\n";
            string NO = "No Answer";
            StreamReader sr;

            int[] num;
            bool[] use;

            int[] up;
            int[] down;

            int n;
            bool ret;

            Solve();
            void Solve()
            {

                Init();

                ret = DFS(0);

                if (ret)
                {

                    Console.Write($"{ArrToNum(up), 7}\n");
                    Console.Write($"+{ArrToNum(down), 6}\n");
                    Console.Write(LINE);
                    Console.Write($"{n, 7}");
                }
                else Console.Write(NO);
            }

            bool DFS(int _depth)
            {

                if(_depth == 7)
                {

                    NtoUD();
                    int chk = ArrToNum(up) + ArrToNum(down);
                    return chk == n;
                }

                int save = num[_depth];

                for (; num[_depth] < 10; num[_depth]++)
                {

                    if (use[num[_depth]]) continue;
                    use[num[_depth]] = true;
                    if (DFS(_depth + 1)) return true;
                    use[num[_depth]] = false;
                }

                num[_depth] = save;
                return false;
            }

            void NtoUD()
            {

                up[0] = num[0];     // h
                up[1] = num[2];     // e
                up[2] = num[3];     // l
                up[3] = num[3];     // l
                up[4] = num[4];     // o

                down[0] = num[1];   // w
                down[1] = num[4];   // o
                down[2] = num[5];   // r
                down[3] = num[3];   // l
                down[4] = num[6];   // d
            }

            int ArrToNum(int[] _arr)
            {

                int ret = 0;
                for (int i = 0; i < _arr.Length; i++)
                {

                    ret = ret * 10 + _arr[i];
                }

                return ret;
            }

            void Init()
            {

                n = int.Parse(Console.ReadLine());

                // h, w, e, l, o, r, d

                up = new int[5];
                down = new int[5];

                use = new bool[10];
                num = new int[7];

                num[0] = 1;
                num[1] = 1;
            }
        }
    }

#if other
using System;

int[] table = new int[7];
int result = int.Parse(Console.ReadLine());

if (result > 184010 || result < 33986) goto OUT; //종료

if (recur(0, 0)) 
{
    Console.WriteLine($"  {table[(int)ch.h]}{table[(int)ch.e]}{table[(int)ch.l]}{table[(int)ch.l]}{table[(int)ch.o]}\n" +
                      $"+ {table[(int)ch.w]}{table[(int)ch.o]}{table[(int)ch.r]}{table[(int)ch.l]}{table[(int)ch.d]}\n" +
                      "-------\n" +
                      ((result > 99999) ? " " : "  ") + result); return;
}

OUT: Console.WriteLine("No Answer"); return;

bool recur(int len, int check)
{
    if(len == 7)
    {
        int sum1 = table[(int)ch.h] * 10000 + table[(int)ch.e] * 1000 + table[(int)ch.l] * 100 + table[(int)ch.l] * 10 + table[(int)ch.o];
        int sum2 = table[(int)ch.w] * 10000 + table[(int)ch.o] * 1000 + table[(int)ch.r] * 100 + table[(int)ch.l] * 10 + table[(int)ch.d];
        
        if (sum1 + sum2 == result) return true; return false;
    }
    int i = (len != (int)ch.h && len != (int)ch.w) ? 0 : 1;
    for (; i < 10; i++)
    {
        if ((check & 1 << i) > 0) continue; 
        table[len] = i;
        if (recur(len + 1, check | 1 << i)) return true;
    }
    return false;
}

enum ch { h = 0, e, l, o, w, r, d }
#elif other2
// #include <cstdio>

int main()
{
	unsigned int n;
	scanf("%u", &n);
	
	bool flg[10]{};
	for (int o = 0; o < 10; ++o)
	{
		flg[o] = 1;
		for (int d = 0; d < 10; ++d)
		{
			if (flg[d] || (o + d) % 10 != n % 10) continue;
			flg[d] = 1;
			for (int l = 0; l < 10; ++l)
			{
				if (flg[l] || (l*20+o+d) % 100 != n % 100) continue;
				flg[l] = 1;
				for (int r = 0; r < 10; ++r)
				{
					if (flg[r] || (l*120+o+r*100+d) % 1000 != n % 1000) continue;
					flg[r] = 1;
					for (int e = 0; e < 10; ++e)
					{
						if (flg[e] || (e*1000+o*1001+l*120+r*100+d) % 10000 != n % 10000) continue;
						flg[e] = 1;
						for (int h = 1; h < 10; ++h)
						{
							if (flg[h]) continue;
							flg[h] = 1;
							for (int w = 1; w < 10; ++w)
							{
								if (flg[w]) continue;
								flg[w] = 1;
								
								int num1 = h*10000 + e*1000 + l*110 + o;
								int num2 = w*10000 + o*1000 + r*100 + l*10 + d;
								
								if (num1 + num2 == n)
								{
									printf("%7d\n+ %d\n-------\n%7d", num1, num2, n);
									return 0;
								}
								flg[w] = 0;
							}
							flg[h] = 0;
						}
						flg[e] = 0;
					}
					flg[r] = 0;
				}
				flg[l] = 0;
			}
			flg[d] = 0;
		}
		flg[o] = 0;
	}
	
	printf("No Answer");
	
	return 0;
}
#endif
}
