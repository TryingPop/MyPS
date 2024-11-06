using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 17
이름 : 배성훈
내용 : 피아의 아틀리에 ~신비한 대회의 연금술사~
    문제번호 : 15898번

    구현, 브루프토스, 시뮬레이션 문제다
    처음에 재료 3개 조건을 놓쳐서
    10! * 2^40인데 어떻게 풀지? 생각했고,
    힌트를 보니 브루트포스가 있었다
    이후 3개 조건을 보고 다시 계산해보니 브루트포스가 적합해 보였다

    재료 중 3개 선택 처음에 경우는 
    적절히 회전하면 자신 이외에 겹치는 경우가 3개가 있다

    시작지점 4군대로 옮긴 경우만 확인하면 된다
    두 번째 부터는 16경우 확인해야한다
    그리고 맵 전체 확인하는 25
    10P3 * 4 * 16^2 * 25 = 1850만 연산이다
    
    이렇게 되므로 모든 경우 찾아서 풀었다
    그리고 순열이나 방향 정하는 것은 n중 for문이 아닌 DFS로 구현했다

    처음은 시작 번호를 16경우 확인해 2336ms가 걸렸고
    두 번째는 중복 경우 줄이니 688ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0973
    {

        static void Main973(string[] args)
        {

            StreamReader sr;
            (int v, int c)[][][] ele;
            (int v, int c)[][] board;
            int n;

            bool[] use;
            int[] order, pos;

            int[] posR, posC;
            int ret;
            Solve();
            void Solve()
            {

                Input();

                DFS_ORDER();

                Console.Write(ret);
            }

            void DFS_ORDER(int _depth = 0, int _s = 0)
            {

                if (_depth == 3)
                {

                    DFS_POS();
                    return;
                }

                for (int i = 0; i < n; i++)
                {

                    if (use[i]) continue;
                    use[i] = true;
                    order[_depth] = i;

                    DFS_ORDER(_depth + 1, i + 1);

                    use[i] = false;
                }
            }

            void DFS_POS(int _depth = 0)
            {

                if (_depth == 3)
                {

                    for (int i = 0; i < 3; i++)
                    {

                        Fill(order[i], pos[i]);
                    }

                    GetScore();
                    return;
                }

                if (_depth > 0)
                {

                    for (int i = 0; i < 16; i++)
                    {

                        pos[_depth] = i;
                        DFS_POS(_depth + 1);
                    }
                }
                else
                {

                    for (int i = 0; i < 4; i++)
                    {

                        pos[_depth] = i;
                        DFS_POS(_depth + 1);
                    }
                }
            }

            void GetScore()
            {

                int red = 0, blue = 0, green = 0, yellow = 0;
                
                for (int r = 0; r < 5; r++)
                {

                    for (int c = 0; c < 5; c++)
                    {

                        switch (board[r][c].c)
                        {

                            case 1:
                                red += board[r][c].v;
                                break;

                            case 2:
                                blue += board[r][c].v;
                                break;

                            case 3:
                                green += board[r][c].v;
                                break;

                            case 4:
                                yellow += board[r][c].v;
                                break;

                            default:
                                break;
                        }

                        board[r][c] = (0, 0);
                    }
                }

                int chk = 7 * red + 5 * blue + 3 * green + 2 * yellow;
                ret = Math.Max(chk, ret);
            }

            void Fill(int _idx, int _type)
            {

                int rot = _type / 4;
                int pos = _type % 4;

                switch (rot)
                {

                    case 0:
                        for (int r = 0; r < 4; r++)
                        {

                            for (int c = 0; c < 4; c++)
                            {

                                AddVal(_idx, r, c, r, c);
                            }
                        }

                        return;

                    case 1:
                        for (int r = 0; r < 4; r++)
                        {

                            for (int c = 0; c < 4; c++)
                            {

                                AddVal(_idx, c, 3 - r, r, c);
                            }
                        }

                        return;

                    case 2:
                        for (int r = 0; r < 4; r++)
                        {

                            for (int c = 0; c < 4; c++)
                            {

                                AddVal(_idx, 3 - r, 3 - c, r, c);
                            }
                        }

                        return;

                    case 3:
                        for (int r = 0; r < 4; r++)
                        {

                            for (int c = 0; c < 4; c++)
                            {

                                AddVal(_idx, 3 - c, r, r, c);
                            }
                        }

                        return;
                }

                void AddVal(int _idx, int _r1, int _c1, int _r2, int _c2)
                {

                    int val = board[_r1 + posR[pos]][_c1 + posC[pos]].v;
                    val += ele[_idx][_r2][_c2].v;
                    if (val < 0) val = 0;
                    else if (val > 9) val = 9;

                    board[_r1 + posR[pos]][_c1 + posC[pos]].v = val;

                    if (ele[_idx][_r2][_c2].c == 0) return;
                    board[_r1 + posR[pos]][_c1 + posC[pos]].c = ele[_idx][_r2][_c2].c;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                ele = new (int v, int c)[n][][];
                use = new bool[n];
                for (int i = 0; i < n; i++)
                {

                    ele[i] = new (int v, int c)[4][];
                    for (int r = 0; r < 4; r++)
                    {

                        ele[i][r] = new (int v, int c)[4];
                        for (int c = 0; c < 4; c++)
                        {

                            ele[i][r][c].v = ReadInt();
                        }
                    }

                    for (int r = 0; r < 4; r++)
                    {

                        for (int c = 0; c < 4; c++)
                        {

                            ele[i][r][c].c = ReadColor();
                        }
                    }
                }

                board = new (int v, int c)[5][];
                for (int i = 0; i < 5; i++)
                {

                    board[i] = new (int v, int c)[5];
                }

                order = new int[3];
                pos = new int[3];

                posR = new int[4] { 0, 0, 1, 1 };
                posC = new int[4] { 0, 1, 0, 1 };

                ret = 0;
                sr.Close();
            }

            int ReadColor()
            {

                int c = sr.Read();
                if (sr.Read() == '\r') sr.Read();

                switch(c)
                {

                    case 'R':
                        return 1;

                    case 'B':
                        return 2;

                    case 'G':
                        return 3;

                    case 'Y':
                        return 4;

                    default:
                        return 0;
                }
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool positive = c != '-';
                int ret = positive ? c - '0' : 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return positive ? ret : -ret;
            }
        }
    }

#if other
int n = IO.NextInt();
var ing = new Cell[n][][,];

for (int i = 0; i < n; i++)
{
    ing[i] = new Cell[4][,];
    ing[i][0] = new Cell[4, 4];

    for (int r = 0; r < 4; r++)
        for (int c = 0; c < 4; c++)
            ing[i][0][r, c].Value = IO.NextInt();

    for (int r = 0; r < 4; r++)
        for (int c = 0; c < 4; c++)
            ing[i][0][r, c].Element = IO.NextChar();

    for (int rot = 1; rot < 4; rot++)
    {
        ing[i][rot] = new Cell[4, 4];

        for (int r = 0; r < 4; r++)
            for (int c = 0; c < 4; c++)
                ing[i][rot][3 - c, r] = ing[i][rot - 1][r, c];
    }
}

var pivots = new (int r, int c)[4] { (0, 0), (0, 1), (1, 0), (1, 1) };
var used = new (int, int, int)[3];
var caul = new Cell[5, 5];

int max = 0;
for (int i = 0; i < n; i++)
    for (int j = 0; j < n; j++)
        for (int k = 0; k < n; k++)
        {
            if (i == j || j == k || k == i)
                continue;

            for (int ri = 0; ri < 4; ri++)
                for (int rj = 0; rj < 4; rj++)
                    for (int rk = 0; rk < 4; rk++)
                        for (int pi = 0; pi < 4; pi++)
                            for (int pj = 0; pj < 4; pj++)
                                for (int pk = 0; pk < 4; pk++)
                                {
                                    (used[0], used[1], used[2]) = ((i, ri, pi), (j, rj, pj), (k, rk, pk));
                                    max = Math.Max(max, GetPoint(used));
                                }
        }

IO.WriteLine(max);

IO.Close();

int GetPoint((int, int, int)[] used)
{
    // Console.WriteLine(string.Join(" ", used));

    // Initialize
    for (int i = 0; i < 5; i++)
        for (int j = 0; j < 5; j++)
            caul[i, j] = new Cell(0, 'W');

    // Apply
    for (int u = 0; u < 3; u++)
    {
        var (num, rot, piv) = used[u];
        if (num == -1)
            continue;

        var cur = ing[num][rot];
        var (pr, pc) = pivots[piv];

        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                caul[i + pr, j + pc] += cur[i, j];
    }

    // Evaluate
    int r = 0, b = 0, g = 0, y = 0;
    for (int i = 0; i < 5; i++)
        for (int j = 0; j < 5; j++)
            switch(caul[i, j].Element)
            {
                case 'R': r += caul[i, j].Value; break;
                case 'B': b += caul[i, j].Value; break;
                case 'G': g += caul[i, j].Value; break;
                case 'Y': y += caul[i, j].Value; break;
                default: break;
            }

    return 7 * r + 5 * b + 3 * g + 2 * y;
}

record struct Cell(int Value, char Element)
{
    public static Cell operator +(Cell a, Cell b)
    {
        return new Cell(Math.Clamp(a.Value + b.Value, 0, 9), b.Element == 'W' ? a.Element : b.Element);
    }
}

static class IO
{
    static StreamReader R=new(new BufferedStream(Console.OpenStandardInput(),1024000));
    static StreamWriter W=new(new BufferedStream(Console.OpenStandardOutput(),1024000));
    public static void Close(){R.Close();W.Close();}
    public static void Write(object s)=>W.Write(s);
    public static void WriteLine(object s)=>W.WriteLine(s);
    public static int NextInt(){var(n,r,v)=(false,false,0);while(true){int c=R.Read();if(!r&&c=='-'){n=r=true;continue;}if('0'<=c&&c<='9'){v=v*10+(c-'0');r=true;continue;}if(r)break;}return n?-v:v;}
    public static long NextLong(){var(n,r,v)=(false,false,0L);while(true){int c=R.Read();if(!r&&c=='-'){n=r=true;continue;}if('0'<=c&&c<='9'){v=v*10+(c-'0');r=true;continue;}if(r)break;}return n?-v:v;}
    public static double NextDouble(){var(n,r,t,w,f)=(false,false,0,0,0d);while(true){int c=R.Read();if(!r&&c=='-'){n=r=true;continue;}if(c=='.'){t=1;r=true;continue;}if('0'<=c&&c<='9'){if(t==0)w=w*10+(c-'0');else f+=(c-'0')*1d/(t*=10);r=true;continue;}if(r)break;}return n?-(w+f):w+f;}
    public static string NextString(int m){var(r,l,v)=(false,0,new char[m]);while(true){int c=R.Read();if(!r&&(c==' '||c=='\n'||c=='\r'))continue;if(r&&(l==m||c==' '||c=='\n'||c=='\r'))break;v[l++]=(char)c;r=true;}return new(v,0,l);}
    public static char NextChar(){char v;while(true){int c=R.Read();if(c!=' '&&c!='\n'&&c!='\r'){v=(char)c;break;}}return v;}
}
#elif other2
import java.io.*;
import java.util.*;
public class Main {
	static class Ingredient {
		int q;
		char c;
		public Ingredient() {
			this.q = 0;
			this.c = 'W';
		}
	}
	static int n,max;
	static Ingredient[][][] arr;
	static Ingredient[][][] board;
	static boolean[] visit;
	static Ingredient[][] temp = new Ingredient[4][4];
	static int[] sum = new int['Z'];

	public static void main(String[] args) throws IOException{
		// TODO Auto-generated method stub
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		n = Integer.parseInt(br.readLine());
		board = new Ingredient[4][5][5];
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				for (int k = 0; k < 4; k++) {
					board[k][i][j] = new Ingredient();
				}
			}
		}
		arr = new Ingredient[n][4][4];
		StringTokenizer st;
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < 4; j++) {
				st = new StringTokenizer(br.readLine());
				for (int j2 = 0; j2 < 4; j2++) {
					arr[i][j][j2] = new Ingredient();
					arr[i][j][j2].q = Integer.parseInt(st.nextToken());
				}
			}
			for (int j = 0; j < 4; j++) {
				String s = br.readLine();
				for (int k = 0; k < 4; k++) {
					arr[i][j][k].c = s.charAt(2*k);
				}
			}
		}
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				temp[i][j] = new Ingredient();
			}
		}
		visit = new boolean[n];
		max = 0;
		f(0);
		System.out.println(max);
	}
	
	static void f(int depth) {
		if (depth == 3) {
			sum['R'] = 0;
			sum['B'] = 0;
			sum['G'] = 0;
			sum['Y'] = 0;
			for (int i = 0; i < 5; i++) {
				for (int j = 0; j < 5; j++) {
					sum[board[3][i][j].c] += board[3][i][j].q;
				}
			}
			max = Math.max(max, 7*sum['R']+5*sum['B']+3*sum['G']+2*sum['Y']);
			return;
		}
		for (int i = 0; i < n; i++) {
			if (visit[i]) continue;
			visit[i] = true;
			for (int dir = 0; dir < 4; dir++) {
				rotate(arr[i]);
				for (int dx = 0; dx < 2; dx++) {
					for (int dy = 0; dy < 2; dy++) {
						
						copy(board[depth],board[depth+1]);
						put(board[depth+1],arr[i],dx,dy);
						f(depth+1);
					}
				}
			}
			visit[i] = false;
		}
	}
	
	static void rotate(Ingredient[][] arr) {
		copy(arr, temp);
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				arr[j][3-i].q = temp[i][j].q;
				arr[j][3-i].c = temp[i][j].c;
			}
		}
	}
	
	static void copy(Ingredient[][] prev, Ingredient[][] next) {
		for (int i = 0; i < prev.length; i++) {
			for (int j = 0; j < prev.length; j++) {
				next[i][j].q = prev[i][j].q;
				next[i][j].c = prev[i][j].c;
			}
		}
	}
	
	static void put(Ingredient[][] board, Ingredient[][] arr, int x, int y) {
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				board[x+i][y+j].q = Math.min(Math.max(0, board[x+i][y+j].q + arr[i][j].q),9);
				if (arr[i][j].c != 'W') {
					board[x+i][y+j].c = arr[i][j].c;
				}
			}
		}
	}
	
	//debug
	static void show(Ingredient[][] arr) {
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < arr.length; i++) {
			for (int j = 0; j < arr.length; j++) {
				sb.append(arr[i][j].q).append(' ');
			}
			sb.append('\n');
		}
		for (int i = 0; i < arr.length; i++) {
			for (int j = 0; j < arr.length; j++) {
				sb.append(arr[i][j].c).append(' ');
			}
			sb.append('\n');
		}
		System.out.println(sb);
	}

}
#elif other3
// #include <iostream>
// #include <fstream>
// #include <iomanip>
// #include <vector>
// #include <algorithm>
// #include <bitset>
// #include <cmath>
// #include <deque>
// #include <functional>
// #include <queue>
// #include <string>
// #include <stack>
// #include <unordered_map>
// #include <unordered_set>
// #include <map>
// #include <set>
// #include <cstring>

using namespace std;

using ll          = long long;
using ld          = long double;
using uint        = unsigned int;
using ull         = unsigned long long;

using pii         = pair<int, int>;
using pll         = pair<ll, ll>;
using tiii        = tuple<int, int, int>;
using tlll        = tuple<ll, ll, ll>;

// #define mod7        1000000007
// #define mod9        1000000009

// #define forall(v)   v.begin(), v.end()
// #define prec(x)     setprecision(x) << fixed

using pic         = pair<int, char>;
using vvp         = vector<vector<pic>>;
int inf = 987654321;

int n, s['Z'];
int b[10][4][4][4][2];
int m[4][5][5][2];
bitset<10> visit;
char c;

int istates[][3] = {
        {0, 0, 0},
        {0, 1, 0},
        {1, 0, 0},
        {1, 1, 0}
};

int states[][3] = {
        {0, 0, 0},
        {0, 0, 1},
        {0, 0, 2},
        {0, 0, 3},
        {0, 1, 0},
        {0, 1, 1},
        {0, 1, 2},
        {0, 1, 3},
        {1, 0, 0},
        {1, 0, 1},
        {1, 0, 2},
        {1, 0, 3},
        {1, 1, 0},
        {1, 1, 1},
        {1, 1, 2},
        {1, 1, 3}
};

int eval() {
    s['R'] = s['G'] = s['B'] = s['Y'] = 0;

    for (int x = 0; x < 5; x++) {
        for (int y = 0; y < 5; y++) {
            s[m[3][x][y][1]] += m[3][x][y][0];
        }
    }

    return 7 * s['R'] + 5 * s['B'] + 3 * s['G'] + 2 * s['Y'];
}

void append(int dx, int dy, int i, int r, int lv) {
    memcpy(m[lv + 1], m[lv], sizeof(m[lv]));

    for (int x = 0; x < 4; x++) {
        for (int y = 0; y < 4; y++) {
            m[lv + 1][dx + x][dy + y][0] = min(max(m[lv + 1][dx + x][dy + y][0] + b[i][r][x][y][0], 0), 9);
            if (b[i][r][x][y][1] != 'W') m[lv + 1][dx + x][dy + y][1] = b[i][r][x][y][1];
        }
    }
}

int dfs(int x) {
    if (x == 3) return eval();

    int mx = -inf;
    for (int i = 0; i < n; i++) {
        if (visit[i]) continue;
        visit[i] = true;
        if (!x) {
            for (auto[dx, dy, d] : istates) {
                append(dx, dy, i, d, x);
                mx = max(mx, dfs(x + 1));
            }
        } else {
            for (auto[dx, dy, d] : states) {
                append(dx, dy, i, d, x);
                mx = max(mx, dfs(x + 1));
            }
        }
        visit[i] = false;
    }

    return mx;
}

int main() {
    cin.tie(nullptr);
    cout.tie(nullptr);
    ios_base::sync_with_stdio(false);

    cin >> n;

    for (int i = 0; i < n; i++) {
        for (int x = 0; x < 4; x++) {
            for (int y = 0; y < 4; y++) {
                cin >> b[i][0][x][y][0];
            }
        }
        for (int x = 0; x < 4; x++) {
            for (int y = 0; y < 4; y++) {
                cin >> c;
                b[i][0][x][y][1] = c;
            }
        }
        for (int d = 1; d <= 3; d++) {
            for (int x = 0; x < 4; x++) {
                for (int y = 0; y < 4; y++) {
                    b[i][d][x][y][0] = b[i][d - 1][3 - y][x][0];
                    b[i][d][x][y][1] = b[i][d - 1][3 - y][x][1];
                }
            }
        }
    }

    for (int x = 0; x < 5; x++) {
        for (int y = 0; y < 5; y++) {
            m[0][x][y][1] = 'W';
        }
    }

    cout << dfs(0);

    return 0;
}
#endif
}
