using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 28
이름 : 배성훈
내용 : 정상 회담 2
    문제번호 : 1670번

    dp, 수학, 조합론 문제다
    dp로 해결했다
    아이디어는 다음과 같다
    한명을 고정시키고 악수하는 것을 볼 때,
    왼쪽과 오른쪽 모두가 짝수가 되어야 완벽하게 악수를 한다
    그리고 왼쪽과 오른쪽 경우의 수를 dp로 누적하면서 알아냈다

    이렇게 찾을 시 시간은 N^2이다
    -> N = 5_000이므로 N^2은 충분히 시도해볼만하다 생각해서
    이렇게 풀어 제출했다 그러니 124ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0648
    {

        static void Main648(string[] args)
        {

            long MOD = 987654321;
            int n = int.Parse(Console.ReadLine()) / 2;
            int len = Math.Max(3, n + 1);
            long[] dp = new long[len];

            dp[1] = 1;
            for (int i = 2; i <= n; i++)
            {

                dp[i] = (2 * dp[i - 1]) % MOD;
                for (int j = 1; j < i - 1; j++)
                {

                    dp[i] = (dp[i] + ((dp[j] * dp[i - 1 - j]) % MOD)) % MOD;
                }
            }

            Console.WriteLine(dp[n]);
        }
    }

#if other
long n = long.Parse(Console.ReadLine());
long?[] memo = new long?[n+1];
memo[0] = 1;
long handshake(long count) {
    if (memo[count] is long already_calc) return already_calc;
    long sum = 0;
    for(long x=2;x<=count;x+=2) {
        sum += handshake(x-2) * handshake(count-x);
        sum %= 987654321;
    }
    memo[count] = sum;
    return sum;
}
Console.Write(handshake(n));
#elif other2
// #include <iostream>
// #include <vector>
// #define SZ 10001
using namespace std;
typedef long long ll;

const ll MOD = 987654321;
vector<int> prime;
vector<int> cofactor(1250);
int seive[SZ];

void findPrime(){
    seive[0] = seive[1] = 1;
    for(int i=2; i<SZ; i++){
        if(seive[i]) continue;
        prime.push_back(i);
        for(int j=i*2; j<SZ; j+=i){
            seive[j] = 1;
        }
    }
}

void factorize(int n, int md){
    int i = 0;
    while(prime[i] <= n){
        if(n % prime[i]) i++;
        else{
            n /= prime[i];
            cofactor[i] += md;
        }
    }
}

int main(){
    int n;
    cin >> n;
    n /= 2;
    findPrime();
    for(int k=n+2; k<=n*2; k++){
        factorize(k, 1);
    }
    for(int k=2; k<=n; k++){
        factorize(k, -1);
    }
    ll ans = 1;
    for(int i=0; i<prime.size(); i++){
        for(int j=0; j<cofactor[i]; j++){
            ans = (ans * (ll)prime[i]) % MOD;
        }
    }
    cout << ans << endl;
}
#endif
}
