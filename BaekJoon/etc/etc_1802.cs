using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 5
이름 : 배성훈
내용 : 스무고개
    문제번호 : 3882번

    dp, 비트마스킹 문제다.

    먼저 수는 0 또는 1로 표현되고 11자리를 넘지 않으니 int로 저장한 뒤
    비트 연산자로 각 수를 읽으면 된다고 판단했다.
    이후 각 원소에 대해서 최소한으로 구분 짓는 개수를 찾고 이중 가장 큰 값을 찾아 제출하니 틀렸다.
        
    이유는 다음과 같은 반례였다.

        3 4
        000
        001
        010
        100

    해당 경우 각 수를 구분하는 경우는 1개이다.
    그런데 전체를 구분지을러면 3번의 탐색이 필요하다!

    이외에도 예제에 반례가 있었다.
    예제 확인없이 코드 작성하고 제출했다.

    이후 고민을해도 답은 나오지 않았다.
    dp의 힌트를 보고 각비트를 선택한 경우로 기록하면 되지 않을까 생각했다.

    예를들어 0번 비트와 3번 비트가 선택된 것을 비트로 나타내면 1001로 표현 가능하다.
    그리고 각 선택 상태에 & 연산자를 통해 그룹을 나눌 수 있다.
    해당 그룹은 선택 상태에 대해 고유하다!
    이는 상태의 결과가 같다면 진행 순서에 상관없이 나뉘는 그룹이 같음을 확인했다.
    그리고 그룹은 각 과정에서 DFS를 하면 이전 경우의 그룹에서 2개의 그룹씩 나뉘게 됨을 확인했다.

    여기까지 인식한 상황에서 구현하려니
    상황인식이 제대로 안되었다.

    그래서 chat gpt에게 자문을 구해 예시 코드를 받았다.
    chat gpt 코드는 예제를 이상없이 통과했다.
    
    gpt의 코드는 1이하인 경우 해당 원소를 바로 빼버려서 상태별 집합이 달라질 수 있다.
    그래서 더 많은 탐색을 한다.

    그리고 key는 문자열이라 메모리 역시 엄청 사용하고,
    매번 리스트를 생성해서 가비지랑 메모리 사용량이 엄청 많아 보였다.

    로직이 안떠오르는 이유를 상태 파악이 제대로 안되서라고 파악했고,
    chat gpt 가 로직으로 구현해준 코드를 기반으로 어떻게 돌아가는지 
    디버깅해서 로직을 명확히 한 뒤 코드로 구현했다.

    상태에 따라 나뉘는 그룹이 유니크 하게 되니,
    진입 순서는 영향을 미치지 않겠네 하고 단 방향으로만 탐색을 했다.

    반례를 들어 보면

        4 4
        0000
        0010
        0100
        1100

    을 보면 왼쪽부터 0번 비트라 하자.
    0번 -> 1번 -> 2번 -> 3번 처럼 단방향으로만 탐색하면
    해당 경우는 0번 비트, 1번, 2번 비트 순으로 확인하며 3번에 모두 구분이 가능하다.

    반면 1번을 먼저 탐색하면 0000, 0010과 0100, 1100으로 구분된다.
    그리고 0000, 0010은 2번 비트로 구분지으면 해당 구간은 2회에 다 찾아지고,
    0번 비트로 0100, 1100을 구분 지으면 해당 구간 역시 2회에 다 찾아진다.
    그래서 전체 2회에 모든 구간을 구분 지을 수 있다.
    이 반례는 chat gpt에게 예제좀 만들어 달라고 하니 나왔다.

    그래서 해당 부분을 수정하니 이상없이 통과했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1802
    {
#if WRONG
        static void Main(string[] args)
        {

            // 로직이 잘못됨 - 3882
            // 000
            // 001
            // 010
            // 100
            // 정답은 2임;
            int MAX_N = 128, MAX_M = 11;
            int MAX = (1 << (MAX_M + 1)) - 1;
            int n, m;
            int[] arr = new int[MAX_N], calc = new int[MAX_N];

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            while (Input())
            {

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    SetCalc(i);

                    ret = Math.Max(ret, DFS());
                }

                sw.Write($"{ret}\n");
            }

            int DFS(int _dep = 0, int _state = 0, int _sel = 0)
            {

                if (_dep == m) return Cnt() == 1 ? _sel : m;
                else return Math.Min(DFS(_dep + 1, _state, _sel), DFS(_dep + 1, _state | (1 << _dep), _sel + 1));

                int Cnt()
                {

                    int ret = 0;
                    for (int i = 0; i < n; i++)
                    {

                        int chk = calc[i] & _state;
                        if (chk == _state)
                        {

                            if (++ret == 2) return 2;
                        }
                    }

                    return ret;
                }
            }

            void SetCalc(int _idx)
            {

                int change = MAX ^ arr[_idx];
                for (int i = 0; i < n; i++)
                {

                    calc[i] = change ^ arr[i];
                }
            }

            bool Input()
            {

                m = ReadInt();
                n = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadVal(m);
                }

                return m != 0;
            }

            int ReadVal(int _m)
            {

                int ret = 0;

                int c;
                for (int i = 0; i < _m; i++)
                {

                    c = sr.Read() - '0';

                    ret |= c << i;
                }

                while ((c = sr.Read()) != '\n') ;

                return ret;
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

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
#elif TRUE

        static void Main()
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m;
            int[] arr;
            int[][][] group;
            Dictionary<int, int>[] dp;

            Init();

            while (Input())
            {

                GetRet();
            }

            void GetRet()
            {

                int ret = DFS(0, 0, group[0][0]);
                sw.Write($"{ret}\n");

                for (int i = 0; i < dp.Length; i++)
                {

                    dp[i].Clear();
                }
            }

            int DFS(int _state, int _gIdx, int[] _group)
            {

                if (_group[0] <= 1) dp[_state][_gIdx] = 0;

                if (dp[_state].ContainsKey(_gIdx)) return dp[_state][_gIdx];
                int ret = m;

                for (int i = 0; i < m; i++)
                {

                    // 해당 번호 선택
                    if ((_state & (1 << i)) != 0) continue;

                    int[][] nextGroup = group[i + 1];
                    nextGroup[0][0] = 0;
                    nextGroup[1][0] = 0;

                    for (int j = 1; j <= _group[0]; j++)
                    {

                        int cur = arr[_group[j]];
                        int k = (cur & (1 << i)) == 0 ? 0 : 1;
                        int idx = ++nextGroup[k][0];
                        nextGroup[k][idx] = _group[j];
                    }

                    int nextState = _state | (1 << i);
                    int chk1 = DFS(nextState, _gIdx | (1 << i), nextGroup[1]);
                    int chk2 = DFS(nextState, _gIdx, nextGroup[0]);

                    ret = Math.Min(ret, Math.Max(chk1, chk2) + 1);
                }

                return dp[_state][_gIdx] = ret;
            }

            bool Input()
            {

                m = ReadInt();
                n = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadVal();
                }

                group[0][0][0] = n;
                return n != 0;
            }

            void Init()
            {

                int MAX_M = 11;
                int MAX_N = 128;
                arr = new int[MAX_N + 1];

                group = new int[MAX_M + 2][][];
                for (int i = 0; i < group.Length; i++)
                {

                    // Y, N
                    group[i] = new int[2][];
                    for (int j = 0; j < 2; j++)
                    {

                        group[i][j] = new int[MAX_N + 1];
                    }
                }

                for (int i = 1; i <= MAX_N; i++)
                {

                    group[0][0][i] = i;
                }

                // dp[state][groupIdx] = 최소 횟수
                dp = new Dictionary<int, int>[1 << MAX_M];
                for (int i = 0; i < dp.Length; i++)
                {

                    dp[i] = new(MAX_N);
                }
            }

            int ReadVal()
            {

                int ret = 0;

                for (int i = 0; i < m; i++)
                {

                    ret |= (sr.Read() - '0') << i;
                }
                while (sr.Read() != '\n') ;
                return ret;
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

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
#else 
        static void Main1802()
        {
            while (true)
            {
                string? line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) break;

                var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int m = int.Parse(tokens[0]);
                int n = int.Parse(tokens[1]);

                if (m == 0 && n == 0)
                    break;

                var objects = new List<int>();

                for (int i = 0; i < n; i++)
                {
                    string s = Console.ReadLine()!;
                    int val = 0;
                    for (int j = 0; j < m; j++)
                    {
                        if (s[j] == '1')
                            val |= (1 << j); // 비트 역순 저장 (0번째 특징은 LSB)
                    }
                    objects.Add(val);
                }

                var features = new List<int>();
                for (int i = 0; i < m; i++) features.Add(i);

                var cache = new Dictionary<string, int>();
                int result = Dfs(objects, features, m, cache);

                Console.WriteLine(result);
            }
        }

        static int Dfs(List<int> objs, List<int> features, int m, Dictionary<string, int> memo)
        {
            if (objs.Count <= 1) return 0;

            string key = $"{string.Join(",", objs)}|{string.Join(",", features)}";
            if (memo.ContainsKey(key))
                return memo[key];

            int minDepth = int.MaxValue;

            foreach (int f in features)
            {
                var yes = new List<int>();
                var no = new List<int>();

                foreach (var obj in objs)
                {
                    if (((obj >> f) & 1) == 1) yes.Add(obj);
                    else no.Add(obj);
                }

                if (yes.Count == 0 || no.Count == 0)
                    continue;

                var nextFeatures = new List<int>(features);
                nextFeatures.Remove(f);

                int depthLeft = Dfs(yes, nextFeatures, m, memo);
                int depthRight = Dfs(no, nextFeatures, m, memo);
                int currentDepth = Math.Max(depthLeft, depthRight) + 1;

                minDepth = Math.Min(minDepth, currentDepth);
            }

            memo[key] = minDepth == int.MaxValue ? 0 : minDepth;
            return memo[key];
        }
#endif
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
typedef long long lint;
typedef pair<int, int> pi;
const int MAXN = 100005;

int n, m, p[12], msk[200];
int dp[180000];
int cnt[180000];

int main(){
	p[0] = 1;
	for(int i=1; i<=11; i++) p[i] = p[i-1] * 3;
	while(true){
		cin >> n >> m;
		if(n + m == 0) break;
		memset(cnt, 0, sizeof(cnt));
		for(int i=0; i<m; i++){
			string s;
			cin >> s;
			msk[i] = 0;
			for(int j=0; j<s.size(); j++){
				if(s[j] & 1) msk[i] += p[j];
			}
			cnt[msk[i]]++;
		}
		for(int x=0; x<p[n]; x++){
			for(int i=0; i<n; i++){
				if((x / p[i]) % 3 == 2){
					cnt[x] = cnt[x - p[i]] + cnt[x - 2 * p[i]];
					break;
				}
			}
			if(cnt[x] <= 1) dp[x] = 0;
			else{
				dp[x] = 1e9;
				for(int i=0; i<n; i++){
					if((x / p[i]) % 3 == 2){
						dp[x] = min(dp[x], 1 + max(dp[x - p[i]], dp[x - 2 * p[i]]));
					}
				}
			}
		}
		printf("%d\n", dp[p[n] - 1]);
	}
}

#endif
}
