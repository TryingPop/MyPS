using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 14
이름 : 배성훈
내용 : 코딩은 예쁘게
    문제번호 : 2879번

    그리디 알고리즘
    입력에 문제있어 한 번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1113
    {

        static void Main1113(string[] args)
        {

            int n;
            int[] arr;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                int prev = 0;
                int type = 0;
                for (int i = 0; i < n; i++)
                {

                    int curType = GetType(i);

                    if (curType == type)
                    {

                        int add = 0;
                        if (type > 0 && arr[i] > prev) add = arr[i] - prev;
                        else if (type < 0 && arr[i] < prev) add = prev - arr[i];
                        ret += add;
                    }
                    else
                    {

                        if (curType < 0) ret -= arr[i];
                        else ret += arr[i];
                        type = curType;
                    }

                    prev = arr[i];
                }

                Console.Write(ret);

                int GetType(int _idx)
                {

                    if (arr[_idx] > 0) return 1;
                    else if (arr[_idx] < 0) return -1;
                    else return 0;
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n + 1];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                for (int i = 0; i < n; i++)
                {

                    int dst = ReadInt();
                    arr[i] = arr[i] - dst;
                }

                sr.Close();

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
    }

#if other
// #include<cstdio>
int d[1000];
int main()
{
	int n, x, a = 0;
	scanf("%d", &n);
	for (int i = 0; i < n; i++)	scanf("%d", d + i);
	for (int i = 0; i < n; i++)
	{
		scanf("%d", &x);
		d[i] -= x;
	}
	for (int i = 0; i < n; i++)
	{
		if (!d[i]) continue;
		if (d[i] < 0)
		{
			int s = d[i], j = i + 1;
			for (; j < n; j++)
			{
				if (d[j] >= 0) break;
				if (d[j] > s) s = d[j];
			}
			for (int k = i; k < j; k++) d[k] -= s;
			a -= s;
		}
		else
		{
			int s = d[i], j = i + 1;
			for (; j < n; j++)
			{
				if (d[j] <= 0) break;
				if (d[j] < s) s = d[j];
			}
			for (int k = i; k < j; k++) d[k] -= s;
			a += s;
		}
		if (d[i]) i--;
	}
	printf("%d\n", a);
}
#endif
}
