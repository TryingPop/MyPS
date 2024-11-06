using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 18
이름 : 배성훈
내용 : 소수 쌍
    문제번호 : 1017번
    
    에라토스테네스의 체 이론, 소수 판정, 정수론, 수학, 이분 매칭 문제다
    이분 매칭에서도 못 찾은 경우 -1을 출력 안해 1번 틀렸다

    아이디어는 단순하다
    먼저 소수 판정을 중복해서 사용하기에, 
    가질 수 있는 범위(2000 이하)의 소수들을 모두 찾아 배열에 저장한다

    그리고 중복되는 자연수가 없으므로 소수를 만들려면 
    홀수 + 짝수가 되어야하므로 홀수와 짝수로 나누었고
    홀수와 짝수의 수가 다르면 바로 탈출해 결론을 냈다

    맨 처음 숫자와 매칭되는 것을 찾아야하므로 
    arr1에 맨 처음 숫자가 담기게 했다

    그리고 앞의 숫자와 다른 그룹의 숫자와 매칭시키고
    다른 숫자들을 이분매칭으로 하나씩 연결시켜줬다 
    연결된 갯수가 홀수의 갯수가 될 때, 결과로 인정하고 cnt에 저장
    범위가 1000이므로 그냥 1000 범위의 bool 배열에 저장하고
    마지막에 순차적으로 출력했다
*/

namespace BaekJoon.etc
{
    internal class etc_0564
    {

        static void Main564(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));

            int n;
            int[] arr;

            bool[] notPrime;
            bool[] num;
            int[] match;
            bool[] visit;

            int[] arr1;
            int[] arr2;

            Solve();

            sr.Close();
            sw.Close();

            void Solve()
            {

                Input();
                if (Init()) return;
                GetPrime();

                for (int i = 0; i < n; i++)
                {

                    if (notPrime[arr1[0] + arr2[i]]) continue;
                    Array.Fill(match, -1);
                    match[i] = 0;
                    int ret = 1;
                    
                    for (int j = 1; j < n; j++)
                    {

                        Array.Fill(visit, false);
                        visit[i] = true;
                        if (DFS(j)) ret++;
                    }

                    if (ret == n) num[arr2[i]] = true;
                }

                bool empty = true;
                for (int i = 1; i <= 1_000; i++)
                {

                    if (num[i]) 
                    {

                        empty = false;
                        sw.Write($"{i} "); 
                    }
                }

                if (empty) sw.Write(-1);
            }

            bool Init()
            {
                
                int cnt1 = 0;
                int cnt2 = 0;

                for (int i = 0; i < n; i++)
                {

                    int cur = arr[i];
                    if ((cur & 1) == 0) cnt1++;
                    else cnt2++;
                }
                n /= 2;
                visit = new bool[n];
                match = new int[n];

                arr1 = new int[n];
                arr2 = new int[n];

                num = new bool[1_001];

                if (cnt1 != cnt2)
                {

                    sw.Write(-1);
                    return true;
                }

                cnt1 = 0;
                cnt2 = 0;

                int chk = arr[0] & 1;
                for (int i = 0; i < 2 * n; i++)
                {

                    int cur = arr[i];
                    if ((cur & 1) == chk) arr1[cnt1++] = cur;
                    else arr2[cnt2++] = cur;
                }

                return false;
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < n; i++)
                {

                    int chk = arr1[_n] + arr2[i];
                    if (visit[i] || notPrime[chk]) continue;
                    visit[i] = true;

                    if (match[i] == -1 || DFS(match[i]))
                    {

                        match[i] = _n;
                        return true;
                    }
                }

                return false;
            }

            void GetPrime()
            {

                notPrime = new bool[2_001];

                for (int i = 2; i <= 2_000; i++)
                {

                    if (notPrime[i]) continue;
                    for (int j = i * 2; j <= 2_000; j += i)
                    {

                        if (notPrime[j]) continue;
                        notPrime[j] = true;
                    }
                }
            }

            void Input()
            {

                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    arr[i] = cur;
                }
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
int[] f() => Console.ReadLine().Split().Select(int.Parse).ToArray();
var (N, l) = (f()[0], f());
var primes = new HashSet<int>();
foreach (var i in Enumerable.Range(2, 2000))
{
    if (!primes.Any(x => i % x == 0))
    {
        primes.Add(i);
    }
}
var x = Enumerable.Range(0, N);
var g = x.Select(u => x.Where(v => primes.Contains(l[u] + l[v])).ToArray()).ToArray();
var answer = new List<int>();
foreach (var v in g[0])
{
    var original = g[0];
    g[0] = new int[] { v };
    var yMate = new Dictionary<int, int>();
    if (x.Count(u => p(u, new bool[N])) == N)
    {
        answer.Add(l[v]);
    }
    bool p(int u, bool[] c)
    {
        c[u] = true;
        foreach (var v in g[u])
        {
            if (!yMate.TryGetValue(v, out var t) || !c[t] && p(t, c))
            {
                yMate[v] = u;
                return true;
            }
        }
        return false;
    }
    g[0] = original;
}
answer.Sort();
Console.WriteLine($"{(answer.Any() ? string.Join(" ", answer) : -1)}");

#elif other2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackJun_Pool2 {
    class Num1017 {
        
        int n;
        string[] input;

        HashSet<int> primes;
        List<int>[] nodes;
        List<int> odd;
        List<int> even;
        List<int> ans;

        int[] seiz;
        bool[] is_processed;

        public void solve() {
            
            n            = int.Parse(Console.ReadLine());
            input        = Console.ReadLine().Split();

            odd          = new List<int>();
            even         = new List<int>();
            ans          = new List<int>();
            primes       = new HashSet<int>();

            seiz         = new int[1001];      //점유 index 저장(중복되지 않는 1000까지의 수)
            is_processed = new bool[1001];

            for(int i = 0, tmp; i<n; i++) {
                tmp = int.Parse(input[i]);

                if(tmp%2 == 0) {
                    even.Add(tmp);
                } else {
                    odd.Add(tmp);
                }
            }

            if(odd.Count != even.Count) {
                Console.WriteLine("-1");    //홀수, 짝수 그룹 개수가 같아야 한다
                return;
            }

            getEratos(primes, 2000);             //소수

            if(int.Parse(input[0])%2 == 1) {
                nodes        = new List<int>[odd.Count+1];      //홀수(index=1부터 시작이라 size+1)
                make_node(odd, even);
            } else {
                nodes        = new List<int>[even.Count+1];
                make_node(even, odd);
            }

            
            foreach(int next in nodes[1]) {
                int count=1;
                seiz = new int[1001];
                seiz[next] = 1;

                for(int i = 2; i<nodes.Length; i++) {
                    
                    is_processed = new bool[1001];
                    is_processed[next] = true;
                    if(dfs(i)==true) {
                        count++;
                    } else {
                        break;  
                    }
                }

                if(count == nodes.Length-1) {
                    ans.Add(next);
                }
            }

            if(ans.Count == 0) {
                ans.Add(-1);
            } else {
                ans.Sort();
            }

            Console.WriteLine("{0}", string.Join(" ", ans));
        }
        

        void getEratos(HashSet<int> primelist, int fin) {
            if(fin <= 1)
                return;

            bool[] overlep = new bool[fin+2];

            for(int i = 2; i<=fin; i++) {
                if(overlep[i] == false) {

                    primelist.Add(i);

                    for(int j = i; j<=fin; j+=i) {
                        overlep[j] = true;
                    }
                }
            }
        }

        void make_node(List<int> vertex, List<int> seizing) {
            int next = 1;
            foreach(int v in vertex) {
                nodes[next] = new List<int>();
                foreach(int s in seizing) {
                    if(primes.Contains(v+s) == true) {
                        nodes[next].Add(s);
                    }
                }
                next++;
            }
        }

        bool dfs(int x) {                               ///-- 매칭에 성공한 경우 true, 실패한 경우 false
            foreach(int t in nodes[x]) {
                if(is_processed[t] == true) {
                    continue;                           //이미 처리된 것은 넘긴다
                }

                is_processed[t] = true;

                //비어있거나 점유 노드에 더 들어갈 공간이 있는 경우
                if(seiz[t] == 0 || dfs(seiz[t])==true) {
                    seiz[t] = x;
                    return true;
                }
            }
            return false;
        }

        static void Main(string[] args) {
            new Num1017().solve();
        }
    }
}

#endif
}
