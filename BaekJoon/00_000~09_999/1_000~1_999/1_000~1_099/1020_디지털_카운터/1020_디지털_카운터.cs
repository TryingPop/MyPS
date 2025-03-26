using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 25
이름 : 배성훈
내용 : 디지털 카운터
    문제번호 : 1020번

    dp문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1459
    {

        static void Main1459(string[] args)
        {

            // 1020 - 디지털 카운터
            long cur;
            long[] tPow;
            int[] line = { 6, 2, 5, 5, 4, 5, 6, 3, 7, 5 };
            string input;
            int[][][] dp;


            Input();

            SetDp();

            GetRet();

            void GetRet()
            {

                long val = 0;
                int cnt = 0;

                for (int i = 0; i < input.Length; i++)
                {

                    cnt += line[input[i] - '0'];
                }

                if (DFS(input, 0, cnt, false) != 10)
                {

                    GetVal(input, 0, cnt, false);

                    long ret = val - cur;

                    Console.Write(ret);
                    return;
                }
                else
                {

                    char[] temp = new char[input.Length];
                    Array.Fill(temp, '0');
                    string find = new(temp);

                    for (int i = 0; i < dp.Length; i++)
                    {

                        for (int j = 0; j < dp[i].Length; j++)
                        {

                            Array.Fill(dp[i][j], -1);
                        }
                    }

                    DFS(find, 0, cnt, false);

                    GetVal(find, 0, cnt, false);

                    long ret = val - cur + tPow[input.Length];

                    Console.Write(ret);
                }

                int DFS(string _str, int _idx, int _cnt, bool _bigger)
                {

                    // 갯수 초과했으니 더 안센다.
                    if (_cnt < 0) return 10;
                    
                    // 남은게 0이고, 큰게 보장되면 0
                    if (_idx == _str.Length) return _bigger && (_cnt == 0) ? 0 : 10;

                    ref int ret = ref dp[_idx][_cnt][_bigger ? 1 : 0];
                    if (ret != -1) return ret;

                    ret = 10;
                    for (int i = _bigger ? 0 : _str[_idx] - '0'; i <= 9; i++)
                    {

                        // 커졌는지 확인
                        int next = DFS(_str, _idx + 1, _cnt - line[i], _bigger || (i > _str[_idx] - '0'));
                        // 해당 경우 찾았으면 탈출 - 백트래킹
                        if (next != 10) 
                        {

                            ret = i;
                            break;
                        }
                    }

                    return ret;
                }

                void GetVal(string _str, int _dep, int _cnt, bool _bigger)
                {

                    if (_dep == _str.Length) return;
                    ref int cur = ref dp[_dep][_cnt][_bigger ? 1 : 0];
                    val = val * 10 + cur;

                    GetVal(_str, _dep + 1, _cnt - line[cur], _bigger || (cur > _str[_dep] - '0'));
                }
            }

            void SetDp()
            {

                // dp[i][j][k] = val
                // 뒤에서 0 ~ i번 단위까지 택했을 때, j 카운트 의 합
                // k는 기존 수보다 큰지 확인
                // val 은 해당 자리값이 담긴다.
                dp = new int[input.Length + 1][][];
                for (int i = 0; i <= input.Length; i++)
                {

                    dp[i] = new int[7 * input.Length + 1][];
                    for (int j = 0; j < dp[i].Length; j++)
                    {

                        dp[i][j] = new int[2];
                        Array.Fill(dp[i][j], -1);
                    }
                }
            }

            void Input()
            {

                input = Console.ReadLine();
                cur = long.Parse(input);
                tPow = new long[input.Length + 1];
                tPow[0] = 1;
                for (int i = 1; i < tPow.Length; i++)
                {

                    tPow[i] = tPow[i - 1] * 10;
                }
            }
        }
    }

#if other
// #include<bits/stdc++.h>
using namespace std;
typedef long long ll;
typedef pair<int,int> pii;
typedef pair<ll,ll> pll;
// #define xx first
// #define yy second

string s;
const int lineCnt[10] = {6, 2, 5, 5, 4, 5, 6, 3, 7, 5};
int dp[16][110][2];

int solve(int pos, int left, bool bigger){
	if(left < 0) return 10;
	if(pos == s.size()) return (bigger && !left ? 0 : 10);
	int &ret = dp[pos][left][bigger];
	if(ret != -1) return ret;

	ret = 10;
	for(int i=(bigger ? 0 : s[pos]-'0'); i<=9; ++i){
		int next = solve(pos+1, left - lineCnt[i], bigger || (i > s[pos]-'0'));
		if(next != 10){
			ret = i;
			break;
		}
	}
	return ret;
}

//dp 배열을 역추적하며 실제 수를 알아낸다.
ll goRet;
ll go(int pos, int left, bool bigger){
	if(pos == s.size()) return 0;
	int &ret = dp[pos][left][bigger]; 
	goRet = goRet * 10 + ret;
	go(pos+1, left - lineCnt[ret], bigger || (ret > s[pos]-'0'));
}

ll tenPow(int a){
	return (a ? 10 * tenPow(a-1) : 1);
}

int main(){
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	string input;
	cin >> input;
	
	int sum = 0;
	for(char &c : input)
		sum += lineCnt[c - '0'];

	//순환 한 후의 답을 구한다.
	memset(dp, -1, sizeof(dp));
	s = string(input.size(), '0');
	solve(0, sum, 0);
	goRet = 0;
	go(0, sum, 0);
	ll ans = goRet - stoll(input) + tenPow(s.size());

	//순환하기 전의 답을 구한다.
	memset(dp, -1, sizeof(dp));
	s = input;
	if(solve(0, sum, 0) != 10){
		goRet = 0;
		go(0, sum, 0);
		ans = goRet - stoll(input);
	}

	cout << ans;
}
#elif other2
namespace DigitalCounter2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str = Console.ReadLine();

            int[] arr = new int[str.Length];
            for (int i = 0; i < str.Length; i++) //reverse
            {
                arr[str.Length - 1 - i] = str[i] - '0';
            }

            int[] cnts = { 4, 0, 3, 3, 2, 3, 4, 1, 5, 3 }; //{ 6, 2, 5, 5, 4, 5, 6, 3, 7, 6 } -2; 0~5

            int digit = 0;
            int[] range = { 0, 0 };

            long value = 0;

            while (digit < str.Length)
            {
                for (int i = arr[digit] + 1; i < 10; i++)
                {
                    int diff = cnts[arr[digit]] - cnts[i];//원래 3개였는데 2개되면 1개 필요
                    if (range[0] <= diff && diff <= range[1])
                    {
                        //증가시 일치 범위내의 적절한 값이 있다면 해당 값 선택
                        value = i - arr[digit]; //i > arr[digit], value > 0
                        range[0] -= diff;
                        range[1] -= diff;
                        break;
                    }
                }

                if (value > 0) break;

                //범위 변경
                range[0] -= cnts[arr[digit]];
                range[1] -= cnts[arr[digit]] - 5;

                digit++;
            }

            if (value == 0) value = 1L;
            digit--;

            while (digit >= 0)
            {
                //범위 변경
                range[0] += cnts[arr[digit]];
                range[1] += cnts[arr[digit]] - 5;
                value *= 10;
                for (int i = 0; i < 10; i++)//1부터 들어갈 수 있는 숫자인지 확인한다. 0, 1, 2, 4, 6, 7, 8 만 유의미하다
                {
                    int diff = cnts[arr[digit]] - cnts[i];

                    if (range[0] <= diff && range[1] >= diff)
                    {
                        value += i - arr[digit];
                        range[0] -= diff;
                        range[1] -= diff;
                        break;
                    }
                }
                digit--;
            }

            Console.WriteLine(value); 
        }
    }
}
#endif
}
