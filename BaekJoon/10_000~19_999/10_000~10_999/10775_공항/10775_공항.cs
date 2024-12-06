using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 6
이름 : 배성훈
내용 : 공항
    문제번호 : 10775번

    그리디, 분리 집합 문제다.
    공항에 도킹할 때 가능한 가장 큰 번호로 도킹하는게 최대한 이어붙일 수 있다.
    그래서 가장 큰 번호로 도킹한 것을 알리기 위해 유니온 파인드 알고리즘을 사용한다.
    해당 번호가 도킹되었다면 바로 앞 번호를 가리키게 해서 해당 번호로 가능함을 알린다.
*/

namespace BaekJoon.etc
{
    internal class etc_1161
    {

        static void Main1161(string[] args)
        {

            StreamReader sr;
            int g, p;
            int[] group;
            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                p = ReadInt();

                int[] stk = new int[g + 1];
                int ret = 0;

                for (int i = 0; i < p; i++)
                {

                    int num = ReadInt();

                    int f = Find(num);
                    if (f == 0) break;
                    ret++;
                    int b = Find(f - 1);

                    group[f] = b;
                }

                Console.Write(ret);
                sr.Close();

                int Find(int _chk)
                {

                    int len = 0;
                    while (group[_chk] != _chk)
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
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                g = ReadInt();
                group = new int[g + 1];
                for (int i = 1; i <= g; i++)
                {

                    group[i] = i;
                }
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
// #include <cstdio>
// #include <sys/stat.h>
// #include <sys/mman.h>

int par[100001];
int Find(int x) { return x == par[x] ? x : par[x] = Find(par[x]); }
void Union(int a, int b) { a = Find(a), b = Find(b), a > b ? par[a] = b : par[b] = a; }

int main() {
    char* p = (char*)mmap(0, 700000, PROT_READ, MAP_SHARED, 0, 0);
	auto readInt = [&]() {
		int ret = 0;
        for (char c = *p++; c & 16; ret = 10 * ret + (c & 15), c = *p++);
		return ret;
	};
    
	int n = readInt(), m = readInt(), cnt = 0;
    for (int i = 1; i <= n; i++) par[i] = i;
    for (int t; m-- && (t = Find(readInt())); Union(t, t - 1), cnt++);
    printf("%d\n", cnt);
}
#endif
}
