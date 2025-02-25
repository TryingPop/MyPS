using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 30
이름 : 배성훈
내용 : Hangman Game
    문제번호 : 3128번

    브루트포스, dp 문제다.
    그리디로 해결했다.

    그리디로 시작과 끝을 정한 뒤 한 방향으로 진행하는게 최소가 보장됨을 알 수 있다.
    그래서 가능한 시작과 끝 모든 경우에 대해 최소인 경우를 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1227
    {

        static void Main1227(string[] args)
        {

            int[] alphabets;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string input = sr.ReadLine();

                bool[] use = new bool[26];
                int len = 0;
                for (int i = 0; i < input.Length; i++)
                {

                    int idx = input[i] - 'A';
                    if (input[i] == ' ' || use[idx]) continue;
                    len++;
                    use[idx] = true;
                }

                alphabets = new int[len];
                len = 0;
                for (int i = 0; i < 26; i++)
                {

                    if (!use[i]) continue;
                    alphabets[len++] = i;
                }
            }

            void GetRet()
            {

                int min = 52;
                int start = -1;
                bool right = true;
                for (int s = 0; s < alphabets.Length; s++)
                {

                    int e = s == 0 ? alphabets.Length - 1 : s - 1;
                    
                    int chk = alphabets[e] - alphabets[s];
                    if (alphabets[e] < alphabets[s]) chk += 26;
                    int cur = chk + MinDis(alphabets[s]);
                    if (cur < min)
                    {

                        min = cur;
                        start = s;
                        right = true;
                    }

                    cur = chk + MinDis(alphabets[e]);
                    if (cur < min)
                    {

                        min = cur;
                        start = e;
                        right = false;
                    }
                }

                int MinDis(int _to)
                {

                    return _to < 26 - _to ? _to : 26 - _to;
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sw.Write($"{min + alphabets.Length}\n");

                if (right)
                {

                    for (int i = 0; i < alphabets.Length; i++)
                    {

                        int idx = (start + i) % alphabets.Length;
                        sw.Write((char)(alphabets[idx] + 'A'));
                    }
                }
                else
                {

                    for (int i = 0; i < alphabets.Length; i++)
                    {

                        int idx = start - i;
                        if (idx < 0) idx += alphabets.Length;

                        sw.Write((char)(alphabets[idx] + 'A'));
                    }
                }

            }
        }
    }

#if other
// #include<stdio.h>
// #include<algorithm>
using namespace std;
int n;
char a[999];
char b[999];
int bl;
int main()
{
	int i, j;
	gets(a);
	for(n=0;a[n];n++);
	for(i=0;i<n;i++)
		if(a[i]<'A') a[i] = 'a';
	sort(a,a+n);
	for(;a[n-1]=='a';n--);
	n=unique(a,a+n)-a;
	int res;
	for(i=0;i<n;i++)
		a[i]-='A';
	if(n==1)
	{
		res = a[0];
		if(res>26-a[0])res = 26-a[0];
		b[0] = a[0];
	}
	else
	{
		res = a[n-1];
		for(j=0;j<n;j++)b[j] = a[j];
		if(res > 26 - a[0])
		{
		res = 26-a[0];
		for(j=0;j<n;j++)b[j] = a[n-j-1];
		}
		for(i=0;i<n-1;i++)
		{
			if(res > a[i]*2 + (26 - a[i+1]))
			{
				res =a[i]*2 + (26 - a[i+1]);
				bl=0;
				for(j=0;j<=i;j++) b[bl++] = a[j];
				for(j=n-1;j>i;j--) b[bl++] = a[j];
			}
			if(res > a[i] + 2*(26 - a[i+1]))
			{
				res = a[i] + 2*(26 - a[i+1]);
				bl=0;
				for(j=n-1;j>i;j--) b[bl++] = a[j];
				for(j=0;j<=i;j++) b[bl++] = a[j];
			}
		}
	}
	printf("%d\n",res + n);
	for(i=0;i<n;i++)
		printf("%c",b[i]+'A');

}
#elif other2
// #include <cstdio>
// #include <climits>
// #include <cstring>
// #include <map>
// #include <set>
// #include <vector>
// #include <string>
// #include <algorithm>

using namespace std;

char dat[202];
bool table[26];

int main(){
  fgets(dat, 202, stdin);
  for(int i = 0; dat[i] != '\0'; i++) {
    if (dat[i] >= 'A' && dat[i] <= 'Z') {
      table[dat[i]-'A'] = true;
    }
  }
  int total = 0;
  for(int i = 0; i < 26; i++) if(table[i]) total++;

  int ans1 = INT_MAX;
  string ans2;

  for(int first = -25; first <= 25; first++) {
    for(int second = -25; second <= 25; second++) {
      bool table2[26]={false,};
      int pos = 0;
      int cnt = 0;
      int len = 0;
      string simul;
      while(pos != first) {
        int normalized = (pos%26+26)%26;
        if (!table2[normalized] && table[normalized]) {
          table2[normalized] = true;
          simul.push_back(normalized + 'A');
          cnt++;
        }
        len++;
        if (pos > first) pos--;
        else pos++;
      }
      while(pos != second){
        int normalized = (pos%26+26)%26;
        if (!table2[normalized] && table[normalized]) {
          table2[normalized] = true;
          simul.push_back(normalized + 'A');
          cnt++;
        }
        len++;
        if (pos > second) pos--;
        else pos++;
      }
      {
        int normalized = (pos%26+26)%26;
        if (!table2[normalized] && table[normalized]) {
          table2[normalized] = true;
          simul.push_back(normalized + 'A');
          cnt++;
        }
      }

      if (total != cnt) continue;

      if (ans1 > len+cnt) {
        ans1 = len+cnt;
        ans2 = simul;
      }
    }
  }
  printf("%d\n%s\n", ans1, ans2.c_str());
}
#endif
}
