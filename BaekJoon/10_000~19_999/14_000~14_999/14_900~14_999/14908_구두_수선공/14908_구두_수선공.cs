using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 13
이름 : 배성훈
내용 : 구두 수선공
    문제번호 : 14908번

    그리디, 정렬 문제다
    아이디어는 다음과 같다
    그리디(exchange argument)로 2개씩 비교해서 최소가 되는 규칙을 찾아도 된다
    (교환법칙, 결합법칙, 그리고 수학적 귀납법으로 증명가능하다)

    그래서
        s t
        2 3
        3 2
    를 비교하니

    t / s 가 큰 것을 먼저 실행하는게 좋음을 알 수 있다
    실수는 오차가 날 수 있어 x.s * y.t, y.x * x.t로 비교했다
    범위가 1_000, 10_000이므로 두 수를 곱해도 int 범위라 곱으로 정렬했다
*/

namespace BaekJoon.etc
{
    internal class etc_1059
    {

        static void Main1059(string[] args)
        {

            int n;
            (int s, int t, int idx)[] order; 

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                Array.Sort(order, (x, y) =>
                {

                    int chk1 = x.s * y.t;
                    int chk2 = x.t * y.s;

                    if (chk1 != chk2) return chk1.CompareTo(chk2);
                    return x.idx.CompareTo(y.idx);
                });

                for (int i = 0; i < n; i++)
                {

                    sw.Write($"{order[i].idx} ");
                }

                sw.Close();
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                order = new (int s, int t, int idx)[n];
                for (int i = 0; i < n; i++)
                {

                    order[i] = (ReadInt(), ReadInt(), i + 1);
                }

                sr.Close();

                int ReadInt()
                {
                    int c, ret = 0;
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }
        }
    }

#if other
// #include <cstdio>
// #include <algorithm>
using namespace std;

struct Job{
	int t, s, n;
	bool operator <(const Job& O)const{
		if(t*O.s != s*O.t) return t*O.s < s*O.t;
		return n < O.n;
	}
};

int main(){
	int N;
	Job J[1000];
	scanf("%d", &N);
	for(int i=0; i<N; i++){
		scanf("%d %d", &J[i].t, &J[i].s);
		J[i].n = i+1;
	}
	sort(J, J+N);
	for(int i=0; i<N; i++)
		printf("%d ", J[i].n);
}
#endif
}
