using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 27
이름 : 배성훈
내용 : 스터디 시간 정하기 1
    문제번호 : 23295번

    누적합, 슬라이딩 윈도우, 스위핑 문제다
    누적합과 슬라이딩 윈도우 아이디어를 이용해 풀었다

    누적합 구함과 동시에 결과를 찾는 코드를 합쳐,
    코드를 압축할 수 있지만 가독성이 떨어질거 같아
    아래와 같이 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0631
    {

        static void Main631(string[] args)
        {

            StreamReader sr;
            int[] time;
            int t;

            Solve();

            void Solve()
            {

                Input();
                int calc = 0;
                for (int i = 0; i < time.Length; i++)
                {

                    int cur = time[i];
                    calc += cur;
                    time[i] = calc;
                }

                calc = 0;
                for (int i = 0; i < t; i++)
                {

                    calc += time[i];
                }

                int max = calc;
                int ret = t;
                for (int i = t; i < time.Length; i++)
                {

                    calc += time[i];
                    calc -= time[i - t];
                    if(max < calc)
                    {

                        max = calc;
                        ret = i + 1;
                    }
                }

                Console.WriteLine($"{ret - t} {ret}");
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
                int n = ReadInt();

                t = ReadInt();
                time = new int[100_001];

                for (int i = 0; i < n; i++)
                {

                    int len = ReadInt();

                    for (int j = 0; j < len; j++)
                    {

                        int s = ReadInt();
                        int e = ReadInt();
                        time[s]++;
                        time[e]--;
                    }
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
// # include <bits/stdc++.h>
using namespace std;

template <class T>
bool chmin(T& _old, T _new) { return _old > _new && (_old = _new, true); }
template <class T>
bool chmax(T& _old, T _new) { return _old < _new && (_old = _new, true); }

int main() {
	cin.tie(nullptr)->sync_with_stdio(false);
// # ifdef palilo
	freopen("in", "r", stdin);
	freopen("out", "w", stdout);
// #endif
    constexpr int MX = 1e5 + 1;
    int n, t;
    cin >> n >> t;
	array<int, MX> pref { };
	for (int k, s, e; n--;) {
		for (cin >> k; k--;) {
			cin >> s >> e;
			++pref[s], --pref[e];
		}
	}
	partial_sum(pref.begin(), pref.end(), pref.begin());
auto mx = accumulate(pref.begin(), pref.begin() + t, 0);
auto cur = mx, st = 0;
for (int i = 0; i + t < MX; ++i)
{
    cur += pref[i + t] - pref[i];
    if (chmax(mx, cur))
    {
        st = i + 1;
    }
}
cout << st << ' ' << st + t;
}
#elif other2
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.StringTokenizer;

public class Main {
	static int len=100000;
	static int n,t;
	static int[] time = new int[len+2];
	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringTokenizer st = new StringTokenizer(br.readLine());
		n = Integer.parseInt(st.nextToken());
		t = Integer.parseInt(st.nextToken());
		for(int i=0; i<n; i++) {
			int k = Integer.parseInt(br.readLine());
			for(int j=0; j<k; j++) {
				st = new StringTokenizer(br.readLine());
				int s = Integer.parseInt(st.nextToken());
				int e = Integer.parseInt(st.nextToken());
				time[s]++; time[e]--;
			}
		}
//		System.out.println(Arrays.toString(time));
		for(int i=1; i<len+2; i++)
			time[i]+=time[i-1];
//		System.out.println(Arrays.toString(time));
		for(int i=1; i<len+2; i++)
			time[i]+=time[i-1];
//		System.out.println(Arrays.toString(time));
		
		int max=time[t-1]; int anstime=-1;
		for(int i=0; i<len+1-t; i++) {
			int tmp = time[t+i]-time[i];
			if(max<tmp) {
				max=tmp; anstime=i;
			}
		}
		System.out.println(anstime+1+" "+(anstime+t+1));
	}
}
#endif
}
