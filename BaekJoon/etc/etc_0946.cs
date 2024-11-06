using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 5
이름 : 배성훈
내용 : 맛집 가이드
    문제번호 : 26126번

    그리디 문제다
    ... 번호와 순위를 반대로 해서 엄청나게 틀렸다;

    아이디어는 다음과 같다
    문제 조건에서 X의 등급이 Y의 등급보다 높다면
    두 평론가의 순위표에서 X의 순위가 Y보다 높아야한다

    이는 n = 8이고 x 의 등수가 각각 동규의 등수가 3, 원이의 등수가 5면
    동규의 등수보다 낮고 동시에 원이의 등수 보다 낮아야한다

    그래서 y의 등수가 각각 동규는 4, 원이가 7이면 x보다 뒤에 있다
    반면 y의 등수가 동규의 등수는 7이지만 원이의 등수가 4면 해당안된다

    예제 1을 보면
        8 3
        1 2 3 4 5 6 7 8
        2 1 4 3 6 5 8 7

    위 조건에의해 7, 8은 서로 같은 등급에 있어야한다
    그리고 5, 6도 같은 등급에 있어야한다
    마찬가지로 1, 2와 3, 4도 같은 등급에 있어야한다
    
    그래서 같은 그룹에 있어야 하는 애들을 먼저 묶는다
    이후에 그리디 알고리즘으로 k개씩 그룹을 묶으면 최대가 된다
*/
namespace BaekJoon.etc
{
    internal class etc_0946
    {

        static void Main946(string[] args)
        {

            StreamReader sr;

            int n, k;
            int[] rank;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                bool[] chk = new bool[n + 1];

                int cnt1 = 0;
                int cnt2 = 0;

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (chk[rank[i * 2]]) 
                    { 
                        
                        cnt1--;
                        cnt2++;
                    }
                    else
                    {

                        cnt1++;
                    }

                    chk[rank[i * 2]] = !chk[rank[i * 2]];

                    if (chk[rank[i * 2 + 1]])
                    {

                        cnt1--;
                        cnt2++;
                    }
                    else
                    {

                        cnt1++;
                    }

                    chk[rank[i * 2 + 1]] = !chk[rank[i * 2 + 1]];

                    if (cnt1 == 0 && k <= cnt2)
                    {

                        cnt2 = 0;
                        ret++;
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                rank = new int[n * 2];
                for (int i = 0; i < n; i++)
                {

                    rank[i * 2] = ReadInt();
                }

                for (int i = 0; i < n; i++)
                {

                    rank[i * 2 + 1] = ReadInt();
                }

                sr.Close();
            }

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

#if other
// #include <bits/stdc++.h>

using namespace std;

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	int N{}, K{};
	cin >> N >> K;
	vector<int> A(2 * N);
	for (int i = 0; i < N; ++i) {
		cin >> A[2 * i];
	}
	for (int i = 0; i < N; ++i) {
		cin >> A[2 * i + 1];
	}
	int cnt1{}, cnt2{}, ans{};
	deque<bool> B(N + 1);
	for (int i = 0; i < N; ++i) {
		for (int j = 0; j < 2; ++j) {
			if (B[A[2 * i + j]]) {
				--cnt1; ++cnt2;
			} else {
				++cnt1;
			}
			B[A[2 * i + j]] ^= true;
		}
		if (cnt1 == 0 && cnt2 >= K) {
			++ans;
			cnt2 = 0;
		}
	}
	cout << ans << "\n";
	return 0;
}
#elif other2
// #include <bits/stdc++.h>
using namespace std; typedef long long ll;
void OJize(){cin.tie(NULL);ios_base::sync_with_stdio(false);}
// #define sz(X) (int)((X).size())
// #define entire(X) X.begin(),X.end()

const int IINF = 0x3f3f3f3f;
const ll LINF = 0x3f3f3f3f3f3f3f3f;
template <class T1, class T2>ostream&operator<<(ostream &os,pair<T1,T2>const&x){os<<'('<<x.first<<", "<<x.second<<')';return os;}
template <class Ch, class Tr, class Container>basic_ostream<Ch,Tr>&operator<<(basic_ostream<Ch,Tr>&os,Container const&x){os<<"[ ";for(auto&y:x)os<<y<<" ";return os<<"]";}

int main(){OJize();
	int n, k; cin>>n>>k;
	vector<int> A(n), B(n); int x;
	for(int i=0; i<n; i++) cin>>x, A[x-1] = i+1;
	for(int i=0; i<n; i++) cin>>x, B[x-1] = i+1;
	vector<int> AtoB(n+1);
	for(int i=0; i<n; i++) AtoB[A[i]] = B[i];
	
	int succ = 0, cur = 0;
	int target = n, lo = n+1, hi = -1;
	for(int i=n; i>0; i--){
		lo = min(lo, AtoB[i]), hi = max(hi, AtoB[i]);
		cur++;
		if(cur >= k && target == hi && cur == hi-lo+1){
			succ++; cur = 0;
			target = lo-1, lo = n+1, hi = -1;
		}
	}
	cout << succ;
}

#endif
}
