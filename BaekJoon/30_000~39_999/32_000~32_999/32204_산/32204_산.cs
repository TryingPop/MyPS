using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 13
이름 : 배성훈
내용 : 산
    문제번호 : 32204번

    dp 문제다.
    오른쪽 끝으로 하는 산을 모두 찾는다.
    
    다른 빠르게 푼 사람의 풀이를 보니 이를 dp 저장없이 조건문으로 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1540
    {

        static void Main1540(string[] args)
        {

            int n;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int[] inc = new int[n], dec = new int[n];
                
                inc[0] = 1;
                for (int cur = 1; cur < n; cur++)
                {

                    int prev = cur - 1;
                    if (arr[prev] <= arr[cur]) inc[cur] = inc[prev] + 1;
                    else inc[cur] = 1;

                    if (arr[cur] <= arr[prev]) dec[cur] = dec[prev] + 1;
                }

                long ret = 0;
                for (int i = 0; i < n; i++)
                {

                    // 오른쪽 끝으로 하는 산의 경우 찾기
                    if (0 < dec[i])
                    {

                        // 작은게 존재한다면 산이 되는 가장 긴 길이를 찾는다.
                        int prev = i - dec[i];
                        ret += dec[i] + inc[prev];
                    }
                    // 작은게 없으므로 증가하는 갯수가 가장긴 산의 길이다.
                    else ret += inc[i];
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

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
// #pragma GCC optimize("Ofast")
// #include <bits/stdc++.h>
using namespace std;

int main(){
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);
    cout.tie(NULL);
    
    int N;
    cin >> N;
    vector<int> A(N);
    long long ans = 0, uc = 0, dc = 0, sc = 0;
    
    for (int i = 0; i < N; i++){
        cin >> A[i];
        if (i){
            if (A[i - 1] < A[i]){
                if (sc == uc) dc = uc;
                uc++;
                dc++;
                sc = 1;
            }
            else if (A[i - 1] > A[i]){
                uc = 1;
                dc++;
                sc = 1;
            }
            else{
                uc++;
                dc++;
                sc++;
            }
            ans += dc;
        }
        else{
            ans++;
            uc++;
            dc++;
        }
    }
    
    cout << ans << '\n';
    
    return 0;
}

#endif
}
