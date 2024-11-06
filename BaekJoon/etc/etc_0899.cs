using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 22
이름 : 배성훈
내용 : 라면 사기 (Small)
    문제번호 : 18185번

    그리디 문제다
    아이디어는 다음과 같다
    라면을 낱개로 사는거보다 세트로 사는게 좋은건 자명하다
    그렇다면 세트를 어떻게 효율적으로 사는가가 중요하다

    처음에는 그냥 3개 먼저 최대한 사면 되는거 아닌가? 하고 생각했고
    게시판 반례들에서 바로 통과 못했다
    그래서 2개짜리를 먼저 사는게 좋은 경우가 있다라 생각을 하고 확인했다

    실제로 다음과 같은 예제가 있다
        2 4 2 2
    같이 있을 때 이다
    이 경우 3개 짜리를 먼저 사면
        0 2 0 2
    이고 14원을 썼다
    이후 이어진게 없으므로 3원짜리 4개를 사야한다
    그래서 총 26원이 나온다

    반면 앞에 2개짜리를 먼저 사는 경우
        0 2 2 2
    이고 10원을 썼다
    이후 3개짜리 2개를 사면 총 24원이 나온다
    그래서 2개짜리를 먼저 사는게 좋은 경우가 있다

    그래서 2개짜리를 먼저 사는 경우가 좋은지 분석해봤다
    그러니 앞에서 2번째께 3번째보다 많으면 
    2개를 사는게 이득이 아닐까 추론했다
    해당 방법으로 제출하니 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0899
    {

        static void Main899(string[] args)
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

            void GetRet()
            {

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int cnt;
                    if (arr[i + 1] > arr[i + 2])
                    {

                        cnt = Math.Min(arr[i + 1] - arr[i + 2], arr[i]);
                        ret += Buy(i, 2, cnt);
                    }

                    cnt = Math.Min(arr[i], arr[i + 1]);
                    ret += Buy(i, 3, cnt);
                    ret += Buy(i, 1, arr[i]);
                }

                Console.Write(ret);
            }

            int Buy(int _s, int _set = 1, int _cnt = 0)
            {

                for (int i = 0; i < _set; i++)
                {

                    arr[_s + i] -= _cnt;
                }

                return _cnt * (3 + 2 * (_set - 1));
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new int[n + 2];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1  && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }

#if other
using static System.Console;
using System.Collections.Generic;

namespace _18185;
class BuyRamen
{
    static void Main()
    {
        int money = 0;
        Stack<int> beside = new Stack<int>();
        int n = int.Parse(ReadLine());
        string[] ramen = ReadLine().Split();
        for (int i = 0; i < n; i++)
        {
            int now = int.Parse(ramen[i]);
            //0아닌 경우
            if (now != 0)
            {
                //만약 t가 남게되는 경우에는, t는 스택으로 복귀시켜야 한다.
                beside.Push(now);
                if (beside.Count() == 3)
                {
                    bool chance = true;
                    int t = beside.Pop(); int s = beside.Pop(); int f = beside.Pop();

                    //t > s 경우 - 그 뒤 수가 t보다 작은 경우 f t 를 먼저 소거해야만 한다.
                    if (t < s && f < s)
                    {
                        chance = false; 
                        //f가 먼저 사라질지 s ==t 가 먼저 충족될지에 따라 수행 절차가 바뀜.
                        if (f > s - t)
                        {
                            money += 5 * (s - t);
                            f -= s - t; s = t;
                            chance = true;
                        }
                        else
                        {
                            money += 5 * f;
                            s -= f;
                            beside.Push(s); beside.Push(t);
                        }
                    }
                        if (chance)
                        {
                            //t가 제일 작은 경우 / f, s는 같을 수 있음
                            if (t < s && t < f)
                            {
                                s -= t; f -= t;
                                money += t * 7;
                                money += (f > s ? s * 5 + (f - s) * 3 : f * 5 + (s - f) * 3);
                            }
                            //s가 제일 작은 경우 / t, f는 같을 수 잇음, t가 남는 경우 스택 복귀
                            else if (s < t && s < f)
                            {
                                t -= s; f -= s;
                                money += s * 7 + f * 3;
                                beside.Push(t);
                            }
                            //f가 제일 작은 경우 / s, t 스택 복귀
                            else if (f < s && f < t)
                            {
                                s -= f; t -= f;
                                money += f * 7;
                                beside.Push(s); beside.Push(t);
                            }
                            //s t f 같은 경우
                            else if (s == t && s == f)
                                money += 7 * s;
                            //s t 동시에 작은 경우
                            else if (s == t && s < f)
                            {
                                f -= s;
                                money += s * 7;
                                money += f * 3;
                            }
                            // t f 동시에 작은 경우
                            else if (t == f && f < s)
                            {
                                s -= f;
                                money += t * 7;
                                money += s * 3;
                            }
                            //f, s 동시에 작은 경우. 이때는 t는 스택으로 복귀
                            else if (f == s && s < t)
                            {
                                t -= s;
                                money += s * 7;
                                beside.Push(t);
                            }
                        }
                    }

            }
                //0인 경우
            else
            {
                //스택의 길이가 0인 경우는 그냥 지나감.
                //1인 경우는 그냥 1번 규칙으로 더해주고,
                //2인 경우는 2번과1번규칙으로 더해준다.
                if (beside.Count == 1)
                    money += 3 * beside.Pop();
                else if (beside.Count == 2)
                {
                    int f = beside.Pop(); int s = beside.Pop();
                    money += (f > s ? s * 5 + (f - s) * 3 : f * 5 + (s - f) * 3);
                }

            }
            //WriteLine(money);
        }
        //스택에 아직 원소가 남아있는 경우, 빼주어야 한다.
        if (beside.Count == 1)
            money += 3 * beside.Pop();
        else if (beside.Count() == 2)
        {
            int f = beside.Pop(); int s = beside.Pop();
            money += (f > s ? s * 5 + (f - s) * 3 : f * 5 + (s - f) * 3);
        }
        else if (beside.Count() == 3)
        {
            int f = beside.Pop(); int s = beside.Pop(); int t = beside.Pop();
            if (f < s && f < t)
            {
                s -= f; t -= f;
                money += 7 * f;
                money += s < t ? 5 * s + 3 * (t - s) : 5 * t + 3 * (s - t);
            }
            else if (s < f && s < t)
            {
                f -= s; t -= s;
                money += 7 * s;
                money += f < t ? 5 * f + 3 * (t - f) : 5 * t + 3 * (f - t);
            }
            else if (t < s && t < f)
            {
                f -= t; s -= t;
                money += 7 * t;
                money += f < s ? 5 * f + 3 * (s - f) : 5 * s + 3 * (f - s);
            }
        }
        Write(money);
        
    }
}
#elif other2
StreamReader r=new(Console.OpenStandardInput());
int m=int.Parse(r.ReadLine());
var l=Array.ConvertAll(r.ReadLine().Split(),int.Parse);
int usedmoney=0;
void three(int i) {
    int min=l[i]>l[i+1]?l[i+1]:l[i];
    min = min > l[i+2] ? l[i+2] : min;
    l[i]-=min;
    l[i+1]-=min;
    l[i+2]-=min;
    usedmoney+=min*7;
}
void two(int i) {
    int min=l[i]>l[i+1]?l[i+1]:l[i];
    l[i]-=min;
    l[i+1]-=min;
    usedmoney+=min*5;
}
void one(int i) {
    for(;l[i]>0;usedmoney+=3)l[i]--;
}

for(int i=0,end=m-2;i<end;i++) {
    if (l[i+1]>l[i+2]) {
        int temp= l[i+1] - l[i+2], min = l[i] > temp ? temp : l[i];
        l[i] -= min;
        l[i+1] -= min;
        usedmoney += 5 * min;
        three(i);
        one(i);
        continue;
    }
    three(i);
    two(i);
    one(i);
}
two(m-2);
one(m-2);
one(m-1);

Console.Write(usedmoney);
#elif other3
// #include <stdio.h>
inline int max(int a, int b) { return a > b ? a : b; }
inline int min(int a, int b) { return a < b ? a : b; }
int main() {
	int N, re = 0, up = 0, cap = 0, p = 0, pp = 0;
	scanf("%d", &N);
	for (int i = 0, in; i < N; i++) {
		scanf("%d", &in);
		re += in * 2;
		pp = p, p = up, up += max(in - cap, 0);
		cap = min(in, up - pp);
	}
	printf("%d", re + up);
	return 0;
}
#endif
    }
}
