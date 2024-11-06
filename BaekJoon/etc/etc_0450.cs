using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 4
이름 : 배성훈
내용 : 숨박꼭질 2
    문제번호 : 12851번

    BFS 문제다
    방문 여부를 처리 안해서 시간초과와, 
    다음 노드 처리 부분을 for문으로 매번 20만번씩 돌려 
    1초 걸리는 비효율적인 코드로 먼저 통과했다
    이후 20만번 연산 도는 대신 다음을 가리키는 큐를 하나 더 할당해서 풀었다

    아이디어는 다음과 같다
    찾는 사람이 숨은 사람보다 좌표가 큰 경우
    찾는 방향은 무조건 -1이 최단경로가 되고 유일하다!
    그래서 좌표가 작은 경우는 따로 처리했다

    그리고 숨은 사람의 좌표가 큰 경우는
    BFS 탐색을 통해서 해결했다

    모든 가능한 경우를 기록해야하기에
    다음껄 임시로 보관하는 큐를 따로 할당해서 해결했다

    이후에는 이상없이 68ms에 통과했다(20만번 보다는 15배 빠르다)
*/

namespace BaekJoon.etc
{
    internal class etc_0450
    {

        static void Main450(string[] args)
        {

            int[] go = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            
            if (go[0] >= go[1])
            {

                Console.WriteLine(go[0] - go[1]);
                Console.WriteLine(1);
                return;
            }

            int[] arr = new int[200_001];
            bool[] visited = new bool[200_001];

            Queue<int> q = new(100_000);
            Queue<int> n = new(100_000);
            q.Enqueue(go[0]);
            visited[go[0]] = true;
            arr[go[0]] = 1;
            int ret1 = 0;
            int ret2 = 0;
            for (int i = 1; i <= 100_000; i++)
            {

                while(q.Count > 0)
                {

                    var node = q.Dequeue();

                    if (node < go[1])
                    {

                        for (int j = 0; j < 3; j++)
                        {

                            int next = Next(node, j);
                            if (next < 0 || visited[next]) continue;
                            arr[next] += arr[node];
                            n.Enqueue(next);
                        }
                    }
                    else
                    {

                        if (!visited[node - 1])
                        {

                            arr[node - 1] += arr[node];
                            n.Enqueue(node - 1);
                        }
                    }
                }

                if (arr[go[1]] > 0)
                {

                    ret1 = i;
                    ret2 = arr[go[1]];
                    break;
                }

                while(n.Count > 0)
                {

                    var node = n.Dequeue();
                    if (visited[node]) continue;
                    visited[node] = true;
                    q.Enqueue(node);
                }
            }

            Console.WriteLine(ret1);
            Console.WriteLine(ret2);

            int Next(int _n, int _i)
            {

                switch (_i)
                {

                    case 0:
                        return _n - 1;

                    case 1:
                        return _n + 1;

                    default:
                        return _n * 2;
                }
            }
#if Slow
using System;
using System.Collections.Generic;

int[] go = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

if (go[0] >= go[1])
{

    Console.WriteLine(go[0] - go[1]);
    Console.WriteLine(1);
    return;
}

int[] arr = new int[200_001];
bool[] visited = new bool[200_001];

Queue<(int dst, int cnt)> q = new(200_000);
q.Enqueue((go[0], 1));
visited[go[0]] = true;

int ret1 = 0;
int ret2 = 0;
for (int i = 1; i <= 100_000; i++)
{

    while(q.Count > 0)
    {

        var node = q.Dequeue();

        if (node.dst < go[1])
        {

            if (!visited[node.dst + 1]) arr[node.dst + 1]+= node.cnt;
            if (!visited[node.dst * 2]) arr[node.dst * 2]+= node.cnt;
            if (node.dst > 0 && !visited[node.dst - 1]) arr[node.dst - 1] += node.cnt;
        }
        else
        {

            if (!visited[node.dst - 1]) arr[node.dst - 1] += node.cnt;
        }
    }

    if (arr[go[1]] > 0)
    {

        ret1 = i;
        ret2 = arr[go[1]];
        break;
    }

    for (int j = 0; j <= 200_000; j++)
    {

        if (arr[j] == 0) continue;
        visited[j] = true;
        q.Enqueue((j, arr[j]));
        arr[j] = 0;
    }
}

Console.WriteLine(ret1);
Console.WriteLine(ret2);
#endif
        }
    }

#if other
class Program
{
    static int max = 987654321;
    static void Main()
    {
        int[] pos = new int[100010];
        int[] NK = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int N = NK[0]; int K = NK[1];
        for (int i = 0; i < 100010; i++)
        {
            pos[i] = max;
        }
        int ans = 0;
        int time = max;
        Queue<int> que = new Queue<int>();
        que.Enqueue(N);
        pos[N] = 0;
        while (que.Count > 0)
        {
            int cur = que.Dequeue();

            if (time < pos[cur])
                break;

            if (cur == K)
            {
                ans++;
                time = pos[cur];
                continue;
            }
            if (cur - 1 >= 0 && pos[cur - 1] >= pos[cur] + 1)
            {
                pos[cur - 1] = pos[cur] + 1;
                que.Enqueue(cur - 1);
            }
            if (cur + 1 < 100010 && pos[cur + 1] >= pos[cur] + 1)
            {
                pos[cur + 1] = pos[cur] + 1;
                que.Enqueue(cur + 1);
            }
            if (cur * 2 < 100010 && pos[cur * 2] >= pos[cur] + 1)
            {
                pos[cur * 2] = pos[cur] + 1;
                que.Enqueue(cur * 2);
            }
        }
        Console.WriteLine(time);
        Console.WriteLine(ans);
    }
}
#elif other2
using System;
using System.Text;

public class Program
{
	private static int[] _deepLevelTable = new int[100010]; // 몇 개의 숫자(그래프)를 거쳐서 왔는가를 저장할 배열( 1 == 시작, 0 == 안 거쳐 왔다 )..
	private static int[] counts = new int[100010]; // 몇 개의 숫자(그래프)를 거쳐서 왔는가를 저장할 배열( 1 == 시작, 0 == 안 거쳐 왔다 )..
	private static int count = 0;

	static int BFS( int startPos, int endPos )
	{
		Queue<int> queue = new Queue<int>();

		queue.Enqueue( startPos );
		_deepLevelTable[startPos] = 1;

		int[] nextPoses = new int[3];
		bool isFind = false;
		int result = 0;
		while ( queue.Count > 0 )
		{
			int queueLength = queue.Count;
			for(int i = 0; i < queueLength; ++i )
			{
				int pos = queue.Dequeue();

				// 곱하기, 더하기, 빼기 순으로 저장..
				nextPoses[0] = pos * 2;
				nextPoses[1] = pos - 1;
				nextPoses[2] = pos + 1;

				for ( int j = 0; j < 3; ++j )
				{
					int nextPos = nextPoses[j];

					if ( 0 > nextPoses[j] || nextPoses[j] > 100000 )    // 범위 밖이라면..
						continue;
					if ( 0 != _deepLevelTable[nextPos] && _deepLevelTable[pos] + 1 != _deepLevelTable[nextPos] )   // 이미 방문한 곳이라면..
						continue;

					_deepLevelTable[nextPos] = _deepLevelTable[pos] + 1;
					counts[nextPos]++;

					if ( nextPos == endPos )
					{
						isFind = true;
						result = _deepLevelTable[pos] + 1;
						count = counts[nextPos];
					}

					queue.Enqueue( nextPos );
				}
			}

			if ( isFind )
				break;
		}

		return result - 1;
	}

	static void Main()
	{
		StreamReader sr = new StreamReader( new BufferedStream( Console.OpenStandardInput() ) );
		StreamWriter sw = new StreamWriter( new BufferedStream( Console.OpenStandardOutput() ) );

		string[] inputs = sr.ReadLine().Split();
		int startPos = int.Parse(inputs[0]);
		int endPos = int.Parse(inputs[1]);

		if ( startPos >= endPos )
		{
			sw.WriteLine( startPos - endPos );
			sw.WriteLine( 1 );
		}
		else
		{
			sw.WriteLine( BFS( startPos, endPos ) );
			sw.WriteLine( count );
		}	

		sr.Close();
		sw.Close();
	}
}
#endif
}
