using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 11
이름 : 배성훈
내용 : Super 12
    문제번호 : 2148번

    구현, 정렬 문제다
    영어로 된 문제라 해석하는데 많은 어려움을 겪었다
    구글 번역기, 파파고 번역기를 쓰면 8점 미만으로 진 팀도 보너스를 받는다는데,
    여기서는 상대와 나의 점수차가 8점 미만을 의미한다;
    예제를 보면 8점차 이내로 진 팀의 점수에 추가된 것을 확인할 수 있다

    그리고 스코어 보드를 입력받을 때
    Split하고 그냥 1개씩 읽으면 Format 에러 받는다
    공백으로 의심되는데, 
    Debug.Assert하면 채점 프로그램에서는 찾아내지 못한다
    총 12번을 틀리고 맞췄다..
*/

namespace BaekJoon.etc
{
    internal class etc_0960
    {

        static void Main960(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            Dictionary<string, int> sTi;
            Dictionary<int, string> iTs;
            int len;

            (int ws, int ls, int wt, int lt, int s)[] arr;
            int[] play, sort;
            int chk;

            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                while (Input())
                {

                    if (chk == len)
                    {

                        Output();
                        chk = 0;
                    }
                }

                sr.Close();
                sw.Close();
            }

            void Output()
            {

                if (play[0] != 1) sw.Write('\n');
                sw.Write($"Round {play[0]}\n");
                Array.Sort(sort, MyComp);

                for (int i = 0; i < len; i++)
                {

                    int idx = sort[i];
                    sw.Write($"{iTs[idx],-21}{arr[idx].s,2}{arr[idx].ws,4}{arr[idx].ls,4}{arr[idx].wt,3}{arr[idx].lt,3}\n");
                }
            }

            int MyComp(int _idx1, int _idx2)
            {

                var t1 = arr[_idx1];
                var t2 = arr[_idx2];

                if (t1.s != t2.s) return t2.s.CompareTo(t1.s);
                else if (t1.ws - t1.ls != t2.ws - t2.ls) return (t2.ws - t2.ls).CompareTo(t1.ws - t1.ls);
                else if (t1.wt != t2.wt) return t2.wt.CompareTo(t1.wt);
                return Alphabetic(_idx1, _idx2);
            }

            int Alphabetic(int _idx1, int _idx2)
            {

                string str1 = iTs[_idx1];
                string str2 = iTs[_idx2];

                int l = str1.Length < str2.Length ? str1.Length : str2.Length;

                for (int i = 0; i < l; i++)
                {

                    int f, b;
                    if (str1[i] < 'a') f = str1[i] - 'A';
                    else f = str1[i] - 'a';

                    if (str2[i] < 'a') b = str2[i] - 'A';
                    else b = str2[i] - 'a';

                    if (f != b) return f.CompareTo(b);
                }

                return str1.Length.CompareTo(str2.Length);
            }

            bool Input()
            {

                string[] temp = sr.ReadLine().Trim().Split();
                if (temp[0] == "#") return false;

                int i = 0;
                int idx1, idx2;

                while (temp[i] == "") { i++; }
                idx1 = sTi[temp[i++]];

                while (temp[i] == "") { i++; }
                idx2 = sTi[temp[i++]];

                while (temp[i] == "") { i++; }
                int ws = int.Parse(temp[i++]);

                while (temp[i] == "") { i++; }
                int ls = int.Parse(temp[i++]);

                while (temp[i] == "") { i++; }
                int wt = int.Parse(temp[i++]);

                while (temp[i] == "") { i++; }
                int lt = int.Parse(temp[i]);

                arr[idx1].ws += ws;
                arr[idx1].ls += ls;
                arr[idx1].wt += wt;
                arr[idx1].lt += lt;
                arr[idx1].s += GetScore(ws, ls, wt);

                arr[idx2].ws += ls;
                arr[idx2].ls += ws;
                arr[idx2].wt += lt;
                arr[idx2].lt += wt;
                arr[idx2].s += GetScore(ls, ws, lt);

                play[idx1]++;
                play[idx2]++;

                chk += 2;

                return true;
            }

            int GetScore(int _ws, int _ls, int _t)
            {

                int ret;

                if (_ws == _ls) ret = 2;
                else if (_ws > _ls) ret = 4;
                else if (_ws + 8 > _ls) ret = 1;
                else ret = 0;

                if (_t >= 4) ret++;

                return ret;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sTi = new(20);
                iTs = new(20);
                len = 0;

                while (true)
                {

                    string temp = sr.ReadLine().Trim();
                    if (temp == "#") break;

                    iTs[len] = temp;
                    sTi[temp] = len++;
                }

                arr = new (int ws, int ls, int wt, int lt, int s)[len];
                play = new int[len];
                sort = new int[len];
                for (int i = 0; i < len; i++)
                {

                    sort[i] = i;
                }

                chk = 0;
            }
        }
    }
#if other
// #include <stdio.h>  
// #include <algorithm>  
// #include <assert.h>
// #include <bitset>
// #include <cmath>  
// #include <complex>  
// #include <deque>  
// #include <functional>  
// #include <iostream>  
// #include <limits.h>  
// #include <map>  
// #include <math.h>  
// #include <queue>  
// #include <set>  
// #include <stdlib.h>  
// #include <string.h>  
// #include <string>  
// #include <time.h>  
// #include <vector>  
// #pragma warning(disable:4996)  
// #pragma comment(linker, "/STACK:336777216")  
using namespace std;
// #define mp make_pair  
// #define Fi first  
// #define Se second  
// #define pb(x) push_back(x)  
// #define szz(x) ((int)(x).size())  
// #define rep(i, n) for(int i=0;i<n;i++)  
// #define all(x) (x).begin(), (x).end()  
// #define ldb ldouble  
typedef tuple<int, int, int> t3;
typedef long long ll;
typedef unsigned long long ull;
typedef double db;
typedef long double ldb;
typedef pair <int, int> pii;
typedef pair <ll, ll> pll;
typedef pair <ll, int> pli;
typedef pair <db, db> pdd;
int IT_MAX = 1 << 19;
const ll MOD = 1000003;
const int INF = 0x3f3f3f3f;
const ll LL_INF = 0x3f3f3f3f3f3f3f3f;
const db PI = acos(-1);
const db ERR = 1e-10;

class mydata {
public:
	int games;
	int points;
	int curscore;
	int revscore;
	int curtries;
	int revtries;
	mydata() {
		games = points = curscore = revscore = curtries = revtries = 0;
	}
};

char in[1050];

char in1[1050];
char in2[1050];
int main() {
	
	map <string, mydata> Mx;
	while (1) {
		fgets(in, 1050, stdin);
		int N = strlen(in);
		if (in[N - 1] == '\n') in[--N] = 0;

		if (in[0] == '#') break;
		Mx[string(in)] = mydata();
	}
	
	int rc = 0;
	while(1) {
		fgets(in, 1050, stdin);
		if (in[0] == '#') break;

		int t1, t2, t3, t4;
		sscanf(in, "%s %s %d %d %d %d", in1, in2, &t1, &t2, &t3, &t4);

		string s1 = string(in1);
		string s2 = string(in2);
		Mx[s1].games++;
		Mx[s2].games++;

		if (t1 > t2) {
			Mx[s1].points += 4;
			if (t1 - t2 < 8) Mx[s2].points++;
		}
		else if (t1 < t2) {
			Mx[s2].points += 4;
			if (t2 - t1 < 8) Mx[s1].points++;
		}
		else {
			Mx[s1].points++;
			Mx[s2].points++;
		}
		if (t3 >= 4) Mx[s1].points++;
		if (t4 >= 4) Mx[s2].points++;
		
		Mx[s1].curscore += t1;
		Mx[s2].curscore += t2;
		Mx[s1].revscore += t2;
		Mx[s2].revscore += t1;
		Mx[s1].curtries += t3;
		Mx[s2].curtries += t4;
		Mx[s1].revtries += t4;
		Mx[s2].revtries += t3;

		bool pchk = true;
		t1 = -1;
		for (auto it : Mx) {
			if (t1 == -1) t1 = it.second.games;
			if (t1 != it.second.games) pchk = false;
		}
		if (!pchk) continue;

		vector <pair<string, mydata>> Vl;
		for (auto it : Mx) Vl.emplace_back(it.first, it.second);
		sort(all(Vl), [](pair<string, mydata> a, pair<string, mydata> b) {
			if (a.second.points != b.second.points) return a.second.points > b.second.points;
			
			int da = a.second.curscore - a.second.revscore;
			int db = b.second.curscore - b.second.revscore;
			if (da != db) return da > db;
			
			if (a.second.curtries != b.second.curtries) return a.second.curtries > b.second.curtries;
			return a.first < b.first;
		});
		
		rc++;
		printf("Round %d\n", rc);
		for (auto it : Vl) {
			printf("%-21s", it.first.c_str());
			printf("%2d%4d%4d%3d%3d\n", it.second.points, it.second.curscore, it.second.revscore, it.second.curtries, it.second.revtries);
		}
		printf("\n");
	}
	return 0;
}
#endif
}
