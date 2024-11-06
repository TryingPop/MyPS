using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 26
이름 : 배성훈
내용 : 점프 게임
    문제번호 : 15558번

    BFS 문제다
    힌트대로 BFS 탐색을 해서 풀었다
    문제 설명이 약간 부실하다

    'N번 칸보다 더 큰 칸으로 이동하는 경우에는 게임을 클리어한 것이다'라는 문구가 있는데,
    각 줄은 N칸, 칸은 위험한 칸과 안전한 칸이 있다고 한다
    즉 1 ~ N칸까지의 정보만 주어졌을 뿐, N + 1칸에 언급이 없다

    그래서 그냥 N + 1칸은 안전한 칸이라 생각하고 풀었는데 이상없이 통과되었다
*/

namespace BaekJoon.etc
{
    internal class etc_0627
    {

        static void Main627(string[] args)
        {

            StreamReader sr;
            int[] info;
            bool[,] visit;
            int[,] turn;

            string[] map;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                info = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                visit = new bool[2, info[0]];
                turn = new int[2, info[0]];

                map = new string[2];
                map[0] = sr.ReadLine();
                map[1] = sr.ReadLine();

                sr.Close();
                bool ret = BFS();

                Console.WriteLine(ret ? 1 : 0);
            }

            bool BFS()
            {

                Queue<(int r, int c)> q = new(info[0] * 2);
                q.Enqueue((0, 0));

                while(q.Count > 0)
                {

                    var node = q.Dequeue();
                    int cur = turn[node.r, node.c];
                    if (node.c - 1 > cur && !visit[node.r, node.c - 1] && map[node.r][node.c - 1] == '1')
                    {

                        visit[node.r, node.c - 1] = true;
                        turn[node.r, node.c - 1] = cur + 1;
                        q.Enqueue((node.r, node.c - 1));
                    }

                    if (node.c + 1 >= info[0]) return true;

                    if (!visit[node.r, node.c + 1] && node.c + 1 > cur && map[node.r][node.c + 1] == '1')
                    {

                        visit[node.r, node.c + 1] = true;
                        turn[node.r, node.c + 1] = cur + 1;
                        q.Enqueue((node.r, node.c + 1));
                    }

                    int nextR = (node.r + 1) % 2;
                    int nextC = node.c + info[1];
                    if (nextC >= info[0]) return true;

                    if (map[nextR][nextC] == '1' && !visit[nextR, nextC])
                    {

                        visit[nextR, nextC] = true;
                        turn[nextR, nextC] = cur + 1;
                        q.Enqueue((nextR, nextC));
                    }
                }

                return visit[0, info[0] - 1] || visit[1, info[0] - 1];
            }
        }
    }

#if other
using System;
class Program{
    static int N;
    static int K;
    static bool[,] M = new bool[2,100000];
    static bool move(int i,int j,int c){
        if(j>=N) return true;
        if(j<c) return false;
        if(M[i,j]){
            M[i,j] = false;
            c++;
            if(move(i>0?0:1,j+K,c)) return true;
            if(move(i,j+1,c)) return true;
            if(move(i,j-1,c)) return true;
            M[i,j] = true;
        }
        return false;
    }
    static void Main(String[] args){
        string[] s = Console.ReadLine().Split(" ");
        N = int.Parse(s[0]);
        K = int.Parse(s[1]);
        for(int i = 0;i<2;i++){
            char[] ss = Console.ReadLine().ToCharArray();
            for(int j = 0;j<N;j++){
                if(ss[j] == '1') M[i,j] = true;
            }
        }
        if(move(0,0,0))  Console.WriteLine(1);
        else  Console.WriteLine(0);
    }
}

#elif other2
namespace TestField
{
    internal class Program
    {
        static void Main()
        {
            string[] input = Console.ReadLine().Split();
            int n = int.Parse(input[0]);
            int k = int.Parse(input[1]);
            bool[,] map = new bool[n, 2];
            bool[,] visit = new bool[n, 2];
            string inputString1 = Console.ReadLine();
            string inputString2 = Console.ReadLine();
            for (int i = 0; i < n; i++)
            {
                if (inputString1[i] == '1')
                    map[i, 0] = true;
                if (inputString2[i] == '1')
                    map[i, 1] = true;
            }

            //너비 우선
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(new Node(0, 0, 0));
            bool isClear = false;
            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();
                if (node.x < node.turn)
                    continue;
                if (node.x >= n)
                {
                    isClear = true;
                    break;
                }
                if (!map[node.x, node.y])
                    continue;

                //방문햇던곳은 패스
                if (visit[node.x, node.y])
                    continue;
                else
                    visit[node.x, node.y] = true;

                queue.Enqueue(new Node(node.x + k, node.y ^ 1, node.turn + 1));
                queue.Enqueue(new Node(node.x + 1, node.y, node.turn + 1));
                queue.Enqueue(new Node(node.x - 1, node.y, node.turn + 1));

            }
            if (isClear)
                Console.WriteLine(1);
            else
                Console.WriteLine(0);
        }

        struct Node
        {
            //현재 위치 정보
            public int x;
            public int y;
            //현재 턴 정보
            public int turn;
            public Node(int x, int y, int turn)
            {
                this.x = x;
                this.y = y;
                this.turn = turn;
            }
        }
    }
}
#elif other3
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.StringTokenizer;

public class Main {

	static char[][] lines;
	static int N;
	static int clear = 0;
	static boolean[][] visited;
	public static void main(String[] args) throws Exception{
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringTokenizer st = new StringTokenizer(br.readLine());
		N = Integer.parseInt(st.nextToken());
		int K = Integer.parseInt(st.nextToken());
		lines = new char[2][N];
		visited = new boolean[2][N];
		
		for (int i = 0; i < 2; i++) {
			lines[i] = br.readLine().toCharArray();
		}
		dfs(0,0,0,K);
		System.out.println(clear);
	}

	private static void dfs(int i, int j, int time, int k) {
		if(clear == 1) {
			return;
		}
		visited[i][j] = true;
		if (check(i, j+1, time+1, k)) {
			visited[i][j+1] = true;
			dfs(i, j+1, time+1, k);
			visited[i][j+1] = false;
		}
		if (check(i, j-1, time+1, k)) {
			visited[i][j-1] = true;
			dfs(i, j-1, time+1, k);
			visited[i][j-1] = false;
		}
		if (check((i+1)%2, j+k, time+1, k)) {
			visited[(i+1)%2][j+k] = true;
			dfs((i+1)%2, j+k, time+1, k);
			visited[(i+1)%2][j+k] = false;
		}
	}

	private static boolean check(int i, int j, int time, int k) {
		if (j >= N || clear == 1)  {
			clear = 1;
			return false;
		}
		
		if (j < 0 || lines[i][j] == '0' || j < time || visited[i][j]) {
			return false; 
		}
		
		return true;
	}

}
#endif
}
