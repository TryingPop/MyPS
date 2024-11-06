using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 16
이름 : 배성훈
내용 : Guess
    문제번호 : 1248번

    브루트포스, 백트래킹 문제다
    접근 방법이 안떠올라 힌트보고 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0539
    {

        static void Main539(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            char[,] op = new char[n, n];
            int[] ret = new int[n];
            int[] sum = new int[n];

            Solve();

            void Solve()
            {

                Input();


                DFS();

                for (int i = 0; i < n; i++)
                {

                    Console.Write($"{ret[i]} ");
                }
            }

            void Input()
            {

                string str = Console.ReadLine();
                int idx = 0;

                for (int i = 0; i < n; i++)
                {

                    for (int j = i; j < n; j++)
                    {

                        op[i, j] = str[idx];
                        idx++;
                    }
                }
            }

            bool DFS(int _depth = 0)
            {

                if (_depth == n)
                {

                    return true;
                }

                for (int i = -10; i <= 10; i++)
                {

                    ret[_depth] = i;
                    for (int j = _depth; j >= 0; j--)
                    {

                        sum[j] += i;
                    }

                    bool find = false;
                    if (Chk(_depth)) find = DFS(_depth + 1);
                    if (find) return true;

                    for (int j = _depth; j >= 0; j--)
                    {

                        sum[j] -= i;
                    }
                }

                return false;
            }

            bool Chk(int _n)
            {

                for (int j = _n; j >= 0; j--)
                {

                    if (op[j, _n] == '+' && sum[j] > 0) continue;
                    if (op[j, _n] == '-' && sum[j] < 0) continue;
                    if (op[j, _n] == '0' && sum[j] == 0) continue;
                    return false;
                }

                return true;
            }
        }
    }

#if other
/* acmicpc.net 1248 - Guess
 * solved.ac - Gold III
 * Source code by RebeLin
 * Time Limit: 2s (C#: 5s)
 * Memory Limit: 128MB (C#: 272MB)
 */


namespace BJ1248 {
  internal class Program {
    static StreamReader streamReader = new StreamReader (Console.OpenStandardInput());
    static StreamWriter streamWriter = new StreamWriter (Console.OpenStandardOutput());

    static public char signCheck (in int[] guessBoard, int first, int last){
      int ret = 0;
      for (int i = first; i <= last; i++)
        ret += guessBoard[i];
      
      if (ret > 0)
        return '+';
      if (ret < 0)
        return '-';
      return '0';
    }

    static public bool takeGuess (
      ref int[]    guessBoard,
          int      recursionDepth,
          int      maxDepth,
       in char[,] signMatrix
    ){
      for (
        guessBoard[recursionDepth] = -10;
        guessBoard[recursionDepth] <= 10;
        guessBoard[recursionDepth]++
      ){
        bool stopper = false;
        for (int i = recursionDepth; ! stopper && i >= 0; i--)
          stopper = (signCheck(guessBoard, i, recursionDepth) != signMatrix[i, recursionDepth]);
        
        if (
          ! stopper
          && (
            recursionDepth+1 == maxDepth 
            || takeGuess(ref guessBoard, recursionDepth+1, maxDepth, signMatrix)
          )
        )
          return true;
      }

      return false;
    }

    static public void Main (){
      int N = int.Parse(streamReader.ReadLine());

      char[,] signMatrix = new char[N, N];
      for (int i = 0; i < N; i++){
        for (int j = i; j < N; j++)
          signMatrix[i, j] = (char) streamReader.Read();
      }

      int[] guessBoard = new int[N];

      takeGuess(ref guessBoard, 0, N, signMatrix);

      foreach (int guess in guessBoard){
        streamWriter.Write(guess);
        streamWriter.Write(' ');
      }
      streamWriter.Flush();
    }
  }
}
#elif other2
using System;

public class Program
{
	static int n;
	static char[,] arr;
	static int[] par;
	static int[] sar;
	static int chLen;

	static int sum(int i, int j)
	{
		if (i == j) return par[i];
		if (i < 1) return sar[j];
		return sar[j] - sar[i - 1];
	}

	static void addSar(int idx, int num)
    {
		if (idx == 0)
		{
			sar[0] = num;
		}
		else
		{
			sar[idx] = sar[idx - 1] + num;
		}
	}

	static void f(int idx)
    {
		if(idx == n)
        {
			for(int i=0; i<n; i++)
            {
				Console.Write(par[i] + " ");
            }
			Console.WriteLine();
			Environment.Exit(0);
        }

		ref char start = ref arr[idx, idx];

		int row=idx, col=idx;
		if(start == '+')
        {
			for(int i=1; i<11; i++)
            {
				row = idx;
				while (true)
				{
					row = row - 1;
					if (row == -1)
					{
						par[idx] = i;
						addSar(idx, i);
						f(idx + 1);
						break;
					}

					addSar(idx, i);
					ref char next = ref arr[row, col];

					if (next == '+')
					{
						if (sum(row, col) <= 0) break;
					}
					else if (next == '-')
					{
						if (sum(row, col) >= 0) break;
					}
					else
					{
						if (sum(row, col) != 0) break;
					}
				}
            }
			return;
        }

		if (start == '-')
		{
			for (int i = -1; i > -11; i--)
			{
				row = idx;
				while (true)
				{
					row = row - 1;
					if (row == -1)
					{
						par[idx] = i;
						addSar(idx, i);
						f(idx + 1);
						break;
					}

					addSar(idx, i);
					ref char next = ref arr[row, col];

					if (next == '+')
					{
						if (sum(row, col) <= 0) break;
					}
					else if (next == '-')
					{
						if (sum(row, col) >= 0) break;
					}
					else
					{
						if (sum(row, col) != 0) break;
					}
				}
			}
			return;
		}

		par[idx] = 0;
		if (idx == 0)
		{
			sar[0] = 0;
		}
		else
		{
			sar[idx] = sar[idx - 1];
		}
		f(idx + 1);
	}

	static int fac(int n)
	{
		if (n == 1) return 1;
		return fac(n - 1) + n;
	}
	static public void Main()
	{
		n = int.Parse(Console.ReadLine());

		par = new int[n];

		chLen = fac(n);
		arr = new char[n,n];
		sar = new int[n];

		int row = 0;
		int col = 0;
		foreach (var c in Console.ReadLine().Trim().ToCharArray())
		{
			arr[row,col++] = Convert.ToChar(c);
			if (col == n)
            {
				row++;
				col = row;
            }
		}

		f(0);
	}
}
#elif other3
using System;
using System.IO;
using System.Text;

namespace 기초브루트포스재귀
{
    class 구에스
    {
        static int n;
        static int[] cho;
        static char[,] pmz;
        static bool find = false;
        static StringBuilder answer = new StringBuilder();
        
        static void Main()
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            
            n = int.Parse(sr.ReadLine());
            string s = sr.ReadLine();
            
            cho = new int[n];
            pmz = new char[n, n];
            
            int sit = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    pmz[i, j] = s[sit];
                    sit++;
                }
            }
            BT(0);
            sw.WriteLine(answer);
            
            sw.Flush();
            sw.Close();
            sr.Close();
        }
        
        static void BT(int cnt)
        {
            if (cnt == n && !find)
            {
                find = true;
                for (int i = 0; i < n; i++)
                {
                    answer.Append(cho[i].ToString() + " ");
                }
                return;
            }
            else if (find)
            {
                return;
            }
            
            for (int i = -10; i <= 10; i++)
            {
                cho[cnt] = i;
                if (check(cnt))
                {
                    BT(cnt + 1);
                }
            }
        }
        
        static bool check(int cnt)
        {
            for (int i = 0; i <= cnt; i++)
            {
                int sum = 0;
                for (int j = i; j <= cnt; j++)
                {
                    sum += cho[j];
                    if (sum == 0 && pmz[i, j] != '0')
                    {
                        return false;
                    }
                    else if (sum > 0 && pmz[i, j] != '+')
                    {
                        return false;
                    }
                    else if (sum < 0 && pmz[i, j] != '-')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
#elif other4
// #include<stdio.h>
// #include<algorithm>

using namespace std;

int N;
char D[15][15];
int ans[15];

bool dfs(int x)
{
	if( x == N+1 ){
		for(int i = 1; i <= N; i++) printf("%d ", ans[i]);
		printf("\n");
		return false;
	}
	int mn = -10, mx = 10, t = 0;
	for(int i = x; i >= 1; i--){
		int s = 0, e = 0;
		if( D[i][x] == '+' ) s = 1, e = 10;
		if( D[i][x] == '-' ) s = -10, e = -1;
		s -= t, e -= t;
		mn = max(mn, s), mx = min(mx, e);
		t += ans[i-1];
	}
	for(ans[x] = mn; ans[x] <= mx; ans[x]++){
		if(dfs(x+1)) return true;
	}
	return false;
}

int main()
{
	char buf[105], *p = buf;
	scanf("%d%s", &N, buf);
	for(int i = 1; i <= N; i++){
		for(int j = i; j <= N; j++){
			D[i][j] = *(p++);
		}
	}
	dfs(1);
}
#elif other5
// #include <bits/stdc++.h>
// #define endl '\n' // don't use when you cover interactive problem
// #define all(v) (v).begin(), (v).end()
// #define NOELEMENT -1

using namespace std;
typedef long long ll;
typedef pair<int, int> pi;
typedef pair<ll, ll> pl;
typedef tuple<int, int, int> ti;

int N;
const int root = 0;

char order(int a, int b, vector<vector<char> >& v)
{
    if(a > b) swap(a, b);
    return v[a+1][b];
}

void fill_same(int node, int val, vector<int>& prefix, vector<ti>& tree)
{
    prefix[node] = val;
    if(get<1>(tree[node]) != NOELEMENT) fill_same(get<1>(tree[node]), val, prefix, tree);
}

void make_prefix(int node, vector<int>& prefix, vector<ti>& tree)
{
    static int cnt = 0;
    if(get<0>(tree[node]) != NOELEMENT){
        make_prefix(get<0>(tree[node]), prefix, tree);
    }

    if(get<1>(tree[node]) != NOELEMENT) fill_same(get<1>(tree[node]), cnt, prefix, tree);
    prefix[node] = cnt++;
    

    if(get<2>(tree[node]) != NOELEMENT){
        make_prefix(get<2>(tree[node]), prefix, tree);
    }
}

void insert(int node, vector<ti>& tree, vector<vector<char> >& v)
{
    int cur = root;
    while(cur <= N) {        
        char cri = order(cur, node, v);
        switch(cri){
        case '-':
            if(get<0>(tree[cur]) == NOELEMENT){
                get<0>(tree[cur]) = node;
                return;
            }
            else{
                cur = get<0>(tree[cur]);
            }
            break;

        case '0':
            if(get<1>(tree[cur]) == NOELEMENT){
                get<1>(tree[cur]) = node;
                return;
            }
            else{
                cur = get<1>(tree[cur]);
            }
            break;

        case '+':
            if(get<2>(tree[cur]) == NOELEMENT){
                get<2>(tree[cur]) = node;
                return;
            }   
            else{
                cur = get<2>(tree[cur]);
            }   
            break;

        }
    }
}

int main() {
    ios::sync_with_stdio(false);
    cin.tie(0), cout.tie(0);

    cin >> N;
    vector<vector<char> > v(N+1, vector<char>(N+1));
    for(int r = 1; r <= N; r++) for(int c = r; c <= N; c++){
        cin >> v[r][c];
    }

    vector<ti> tree(N+1, {NOELEMENT, NOELEMENT, NOELEMENT});
    for(int node = 1; node <= N; node++){
        insert(node, tree, v);
    }

    vector<int> prefix(N+1, NOELEMENT);
    make_prefix(root, prefix, tree);

    for(int i = 1; i <= N; i++){
        cout << prefix[i] - prefix[i-1] << ' ';
    }
    cout << endl;

    return 0;
}
#endif
}
