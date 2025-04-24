using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 24
이름 : 배성훈
내용 : DNA 발견
    문제번호 : 2806번

    dp 문제다.
    그리디일까 고민하다가 힌트를 봤고 dp라서 dp로 접근해 풀었다.
    다른 사람의 풀이를 보니 그리디 방법으로 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1571
    {

        static void Main1571(string[] args)
        {

            int n;
            string str;
            int[][] dp;

            Input();

            GetRet();
            void GetRet()
            {

                dp = new int[2][];
                dp[0] = new int[n + 1]; // 정방향
                dp[1] = new int[n + 1]; // 역방향

                dp[1][1] = 1;
                if (str[n - 1] == 'B')
                    dp[0][1] = 1;
                
                for (int i = n - 2, idx = 2; i >= 0; i--, idx++)
                {

                    if (str[i] == 'A')
                    {

                        // 정방향의 경우 이전 정방향이 적은지 혹은
                        // 뒤집어 버리는게 적은지 확인한다.
                        dp[0][idx] = Math.Min(dp[0][idx - 1], dp[1][idx - 1] + 1);

                        // 역방향은 현재 A이므로 B 상태이다. 그래서 1개 뒤집는 연산
                        // 혹은 아니면 정방향에서 현재항 빼고 남은거 전체를 뒤집는 연산 하는지 택1
                        dp[1][idx] = Math.Min(dp[0][idx - 1] + 1, dp[1][idx - 1] + 1);
                    }
                    else
                    {

                        // 현재 B지만 뒤집어진 연산에선 A이다.
                        // 그래서 뒤집어진 상태는 연산이 필요 X 반면 이전 상태에서 1번 뒤집는게 작다면 뒤집는다.
                        dp[1][idx] = Math.Min(dp[1][idx - 1], dp[0][idx - 1] + 1);

                        // 정방향 상태로 만드는 경우
                        // 현재꺼는 무시하고 남은것들 뒤집어 정방향 만들기
                        // 혹은 이전꺼에서 현재꺼만 바꾸기
                        dp[0][idx] = Math.Min(dp[0][idx - 1] + 1, dp[1][idx - 1] + 1);
                    }
                }

                Console.Write(Math.Min(dp[0][str.Length], dp[1][str.Length]));
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());
                str = sr.ReadLine();
            }
        }
    }

#if other
// #include<iostream>
using namespace std;

char data[1000010];

int main(){
	int n;
	int ans = 0;
	cin >> n;
	scanf("%s", data);
	data[n] = 'A';
	for (int i = 1; i < n; i++){
		if (data[i - 1] != data[i] && data[i] != data[i + 1]){
			if (data[i] == 'A')
				data[i] = 'B';
			else
				data[i] = 'A';
			ans++;
		}
	}
	for (int i = 1; i <= n; i++){
		if (data[i - 1] != data[i])
			ans++;
	}
	cout << ans << endl;
}
#endif
}
