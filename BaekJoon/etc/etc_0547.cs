using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 16
이름 : 배성훈
내용 : 축사 배정
    문제번호 : 2188번

    이분매칭 문제다
    아직 이분 매칭에 익숙치 않아 풀어본 3 문제 중 1개다

    아이디어는 546과 같지만 다시 적어본다
    먼저 해당 노드에 대해 갈 수 있는 노드를 찾는다
    찾은 경우 다음으로 넘어간다

    반면 갈려고하는 곳에 이미 다른 노드가 먼저 도착했다면
    먼저 도착했던 노드보고 다른 길로 가라고 시도한다
    그리고 다른 길로 갈 수 있다면 다른 길로 보내고 해당 노드의 탐색을 끝낸다
    반면 보낼 수 없다면 다른 길을 탐색하고 앞을 반복한다
    모든 경우를 했음에도 갈 수 없다면 해당 노드는 못잇는다고 결론 짓고 종료한다

    호프크로프트 카프 알고리즘을 적용해 봤다
    시간 복잡도는 정점의 개수를 V, 간선의 수를 E라하자
    그러면 O((root V) * E)가 된다

    처음에 A의 각 정점에 0의 단계를 부여하고 DFS로 최대한 매칭을 해준다
    일반적인 이분 매칭에서 B 노드의 재방문 초기화를 제외하고 최대한 매칭해준다
    
    매칭이 끝나면 다시 처음으로 돌아와서 레벨을 다시 부여해준다
    여기서 매칭되지 않은 점만 0의 단계를 부여하고
    매칭된 점은 매칭 안된 점에서 몇 단계 거쳐야 A로 오는 레벨을 부여한다

    그리고 다시 매칭을 해준다 여기서 만약 다음 단계인 경우 매칭을 시켜주고, 
    아니면 다른 점으로 이을 수 있는지 확인한다

    이렇게 갱신이 안되는 경우까지 매칭해준게 이분매칭이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0547
    {

        static void Main547(string[] args)
        {

#if first
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int m = ReadInt();

            int[][] line = new int[n + 1][];
            bool[] visit = new bool[m + 1];
            int[] match = new int[m + 1];

            int ret = 0;
            Solve();

            Console.WriteLine(ret);
            sr.Close();

            void Solve()
            {

                for (int i = 1; i <= n; i++)
                {

                    int len = ReadInt();
                    line[i] = new int[len];
                    for (int j = 0; j < len; j++)
                    {

                        line[i][j] = ReadInt();
                    }
                }

                for (int i = 1; i <= n; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret++;
                }
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < line[_n].Length; i++)
                {

                    int dst = line[_n][i];
                    if (visit[dst]) continue;
                    visit[dst] = true;

                    if (match[dst] == 0 || DFS(match[dst]))
                    {

                        match[dst] = _n;
                        return true;
                    }
                }

                return false;
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

#else

            int INF = 1_000_000_000;
            StreamReader sr;
            StreamWriter sw;

            int[] A;
            int[] B;
            bool[] visit;

            List<int>[] line;

            Queue<int> q;
            int[] lvl;

            int n, m;

            Solve();

            void Solve()
            {

                Init();

                int ret = 0;


                while (true)
                {

                    BFS();

                    int match = 0;
                    for (int i = 1; i <= n; i++)
                    {

                        if (!visit[i] && DFS(i)) match++;
                    }

                    if (match == 0) break;

                    ret += match;
                }

                sw.Write($"{ret}");
                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                line = new List<int>[n + 1];
                for (int i = 1; i < n + 1; i++)
                {

                    int len = ReadInt();
                    line[i] = new(len);
                    for (int j = 0; j < len; j++)
                    {

                        line[i].Add(ReadInt());
                    }
                }

                A = new int[n + 1];
                visit = new bool[n + 1];
                lvl = new int[n + 1];
                q = new(n + 1);

                B = new int[m + 1];

                Array.Fill(A, -1, 1, n);
                Array.Fill(B, -1, 1, m);
                Array.Fill(visit, false, 1, n);
            }

            void BFS()
            {

                ///
                /// A그룹의 정점에 단계 재부여
                /// 매칭 안된 점은 0
                /// 매칭 된 점은 다른 정점에서 줄타기로 해당 레벨로 올 수 있다면
                /// 단계 + 1이 된다
                ///

                for (int i = 1; i <= n; i++)
                {

                    // 매칭 안된 점들은 0인채로 시작
                    if (!visit[i])
                    {

                        lvl[i] = 0;
                        q.Enqueue(i);
                    }
                    // 이미 레벨이 매겨진 경우
                    else lvl[i] = INF;
                }

                // A에 레벨을 쌓아간다 
                while (q.Count > 0)
                {

                    int a = q.Dequeue();

                    for (int i = 0; i < line[a].Count; i++)
                    {

                        int b = line[a][i];
                        if (B[b] != -1 && lvl[B[b]] == INF)
                        {

                            lvl[B[b]] = lvl[a] + 1;
                            q.Enqueue(B[b]);
                        }
                    }
                }
            }

            bool DFS(int _a)
            {

                for (int i = 0; i < line[_a].Count; i++)
                {

                    int b = line[_a][i];
                    // 기존 이분 매칭에 다음 레벨인 경우도 잇는다
                    if (B[b] == -1 || lvl[B[b]] == lvl[_a] + 1 && DFS(B[b]))
                    {

                        visit[_a] = true;
                        A[_a] = b;
                        B[b] = _a;
                        return true;
                    }
                }

                return false;
            }

            int ReadInt()
            {

                int c, ret = 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c < '0' || c > '9') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
#endif
        }
    }
#if other
using System;
using System.Collections.Generic;

public class Program
{
    static List<int>[] adjacency;
    static bool[] visit;
    static int[] match;
    static void Main()
    {
        string[] nm = Console.ReadLine().Split(' ');
        int n = int.Parse(nm[0]), m = int.Parse(nm[1]);
        adjacency = new List<int>[n + 1];
        for (int i = 1; i <= n; i++)
        {
            adjacency[i] = new();
        }
        for (int i = 1; i <= n; i++)
        {
            int[] array = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            for (int j = 1; j <= array[0]; j++)
            {
                adjacency[i].Add(array[j]);
            }
        }
        visit = new bool[n + 1];
        match = new int[m + 1];
        int answer = 0;
        for (int i = 1; i <= n; i++)
        {
            if (DFS(i))
                answer++;
            Array.Fill(visit, false);
        }
        Console.Write(answer);
    }
    static bool DFS(int cur)
    {
        if (visit[cur])
            return false;
        visit[cur] = true;
        foreach (int next in adjacency[cur])
        {
            if (match[next] == 0 || DFS(match[next]))
            {
                match[next] = cur;
                return true;
            }
        }
        return false;
    }
}
#elif other2
using static IO;
public class IO{
public static IO Cin=new();
public static StreamReader reader=new(Console.OpenStandardInput());
public static StreamWriter writer=new(Console.OpenStandardOutput());
public static implicit operator string(IO _)=>reader.ReadLine();
public static implicit operator int(IO _)=>int.Parse(reader.ReadLine());
public static implicit operator string[](IO _)=>reader.ReadLine().Split();
public static implicit operator int[](IO _)=>Array.ConvertAll(reader.ReadLine().Split(),int.Parse);
public static implicit operator (int,int)(IO _){int[] a=Cin;return(a[0],a[1]);}
public static implicit operator (int,int,int)(IO _){int[] a=Cin;return(a[0],a[1],a[2]);}
public void Deconstruct(out int a,out int b){(int,int) r=Cin;(a,b)=r;}
public void Deconstruct(out int a,out int b,out int c){(int,int,int) r=Cin;(a,b,c)=r;}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    record LineInfo(int dot,int limit);
    public static void Coding() {
        (int cow_count,int barn) = Cin;
        int[][] cows = new int[cow_count][];
        for(int i=0;i<cow_count;i++) {
            int[] input = Cin;
            cows[i] = new int[input[0]];
            for(int x=0;x<input[0];x++) {
                cows[i][x] = input[x+1]-1;
            }
        }
        int?[] book = new int?[barn];
        bool[] visited;
        bool dfs(int me,int exclude) {
            if (visited[me]) return false;
            visited[me]=true;
            foreach(var place in cows[me]) {
                if (place == exclude) continue;
                if (book[place] is int other) {
                    if (dfs(other,place)) {
                        book[place] = me;
                        return true;
                    }
                    //양보 안되면 다른 장소 찾아보기.
                } else {
                    book[place] = me;
                    return true;
                }
            }
            return false;
        }

        for(int x=0;x<cow_count;x++) {
            visited = new bool[cow_count];
            dfs(x,-1);
        }

        Cout = book.Count(x => x is not null);
    }
}
#endif
}
