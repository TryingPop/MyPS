using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 12
이름 : 배성훈
내용 : 과일 탕후루
    문제번호 : 30804번

    브루트포스, 두 포인터, 구현 문제다
    과일의 종류가 9개 이기에
    2개를 선택하는 방법은 9 x 8 / 2 = 36개가 존재한다
    일치하면 길이를 재는 식으로 O(N)에 최대 길이를 찾을 수 있다
    그래서 N의 최대값이 20만이고 20만 x 36 = 720만이라 해당 방법으로 풀었다

    만약 과일의 종류가 많아진다면 쓸 수 없는 방법이다
    이 경우 슬라이딩 윈도우로 해결해야한다
*/

namespace BaekJoon.etc
{
    internal class etc_1058
    {

        static void Main1058(string[] args)
        {

            StreamReader sr;
            int n;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

#if first
            void GetRet()
            {

                int ret = 0;
                for (int i = 1; i < 10; i++)
                {

                    for (int j = i + 1; j < 10; j++)
                    {

                        ret = Math.Max(Cnt(i, j), ret);
                    }
                }

                Console.Write(ret);
            }

            int Cnt(int _n1, int _n2)
            {

                int ret = 0;
                int len = 0;
                for (int i = 0; i < n; i++)
                {

                    if (arr[i] == _n1 || arr[i] == _n2)
                    {

                        len++;
                        ret = Math.Max(ret, len);
                    }
                    else len = 0;
                }

                return ret;
            }
#else

            void GetRet()
            {

                int[] cnt = new int[10];
                int l = 0, ret = 0, num = 0, cur = 0;
                for (int r = 0; r < n; r++)
                {

                    cur++;
                    if (cnt[arr[r]]++ == 0) num++;

                    while (num > 2)
                    {

                        if (--cnt[arr[l++]] == 0) num--;
                        cur--;
                    }

                    ret = Math.Max(ret, cur);
                }

                Console.Write(ret);
            }
#endif

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
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
var reader = new Reader();
var n = reader.NextInt();
var fruits = new int[n];
for (int i = 0; i < n; i++)
    fruits[i] = reader.NextInt();


int front = 0, back = 0;
var tang = new int[10];
int kCnt = 0;
int max = 0;
for (int i = 0; i < n; i++)
{
    int f = fruits[back++];
    
    if (tang[f] == 0)
        kCnt++;
    tang[f]++;

    while (kCnt > 2)
    {
        int ff = fruits[front++];
        tang[ff]--;
        if (tang[ff] == 0)
            kCnt--;
    }

    max = Math.Max(max, back - front);
}

Console.WriteLine(max);

class Reader
{

    StreamReader R;
    public Reader()=> R = new(new BufferedStream(Console.OpenStandardInput()));
    public int NextInt()
    {
    
        var(v,n,r)=(0,false,false);
        while(true)
        {
            int c=R.Read();
            if((r,c)==(false,'-'))
            {
                (n,r)=(true,true);
                continue;
            }
            
            if('0'<=c&&c<='9')
            {
                
                (v,r)=(v*10+(c-'0'),true);
                continue;
            }
            
            if(r==true) break;
        }
        
        return n?-v:v;
    }
}
#elif other2
// #include <algorithm>
// #include <cstdio>

using namespace std;

int get_int() {
	int i = 0;
	char c;
	do
		c = getchar_unlocked();
	while (not isdigit(c));
	do {
		i = (i << 3) + (i << 1) + (c & 15);
		c = getchar_unlocked();
	} while (isdigit(c));
	return i;
}

int N;
int type[2];
int idx[2];

int main() {
	N = get_int();
	int answer = 0;
	int start = 0;
	for (int i = 1; i <= N; ++i) {
		int s = get_int();
		if (type[1] == s) {
			swap(type[0], type[1]);
			idx[1] = idx[0];
		}
		else if (type[0] != s) {
			start = idx[1];
			type[1] = type[0];
			idx[1] = idx[0];
			type[0] = s;
		}
		idx[0] = i;
		answer = max(answer, i-start);
	}
	printf("%d", answer);
}
#endif
}
