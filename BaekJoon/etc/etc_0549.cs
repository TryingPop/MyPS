using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 22
이름 : 배성훈
내용 : 회의실 배정 4
    문제번호 : 19623번

    dp, 이분탐색, 좌표압축 문제다
    dp를 설정 하는게 관건인 문제다 
    설정을 못해서 다른 사람 풀이를 봤다

    /////////////////////////////////////////////////////////////////////////////////////
    시도한 방법으로는 
    DFS 탐색으로 정방향으로 진행하는데 시작지점에서 배정 가능한 해당 회의실을 배정할 때, 
    회의 시간에 할당 가능한 것들을 하나씩 살펴보면서, 
    해당 시간에 최대값을 dp에 담기게 시도했었다

    먼저 해당 시간에 대해 가장 빠르게 시행할 수 잇는 회의를 찾는다
    다음으로 -> 해당 회의를 시작해본다 -> 그러면 해당 회의를 종료한 시간으로 간다

    그리고 가장 빠른 회의를 찾는다 -> ... 이렇게 가장 빠르게 끝나는 회의로 간다
    이렇게 끝까지 다음 회가 없는 경우까지 간다

    그리고, 해당 회의를 더 진행할 수 없는 경우 탈출하는데,
    

    이전 단계로 오면, 다음 회의를 본다
    그리고, 다음 회의를 실행하면서 진행한다
    .. 구현을 잘못해서 그런지 시간초과가 뜬다;
    /////////////////////////////////////////////////////////////////////////////////////

    아이디어는 다음과 같다
    해당 종료시점을 기준으로 정렬하고,
    타이머를 진행시킨다
    그리고 가장 가까운 종료 회의시간을 찾고 현재 시간과 일치하는 경우
    회의전 최대값과, 이 회의를 진행했을 때, 최대값을 비교해 큰 값으로 갱신한다
    일치하지 않는 경우는 이전 시간의 최대값을 계승한다

    이렇게 끝까지 가면 남는게 최대값이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0549
    {

        static void Main549(string[] args)
        {

#if !first
            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            int n;
            (int s, int e, int w)[] arr;
            int[] dp;
            bool[] visit;
            Solve();

            void Solve()
            {

                Input();
                CompactPos();

                for (int i = 1; i < 2 * n; i++)
                {

                    int idx = FindEnd(i);
                    if (i != arr[idx].e) 
                    { 
                        
                        dp[i] = dp[i - 1];
                        continue;
                    }

                    dp[i] = Math.Max(dp[i - 1], dp[arr[idx].s] + arr[idx].w);
                }

                Console.WriteLine(dp[2 * n - 1]);
            }

            void Input()
            {

                n = ReadInt();
                arr = new (int s, int e, int w)[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), ReadInt(), ReadInt());
                }

                dp = new int[2 * n];
            }

            void CompactPos()
            {

                int[] pos = new int[2 * n];

                for (int i = 0; i < n; i++)
                {

                    pos[2 * i] = arr[i].s;
                    pos[2 * i + 1] = arr[i].e;
                }

                Array.Sort(pos);

                for (int i = 0; i < n; i++)
                {

                    arr[i].s = FindIdx(pos, arr[i].s);
                    arr[i].e = FindIdx(pos, arr[i].e);
                }

                Array.Sort(arr, (x, y) => x.e.CompareTo(y.e));
            }

            int FindIdx(int[] _sortedArr, int _chkVal)
            {

                int l = 0;
                int r = 2 * n - 1;

                while (l <= r)
                {

                    int mid = (l + r) / 2;

                    if (_sortedArr[mid] < _chkVal) l = mid + 1;
                    else r = mid - 1;
                }

                return r + 1;
            }

            int FindEnd(int _cur)
            {

                int l = 0;
                int r = arr.Length - 1;

                while(l <= r)
                {

                    int mid = (l + r) / 2;

                    if (arr[mid].e < _cur) l = mid + 1;
                    else r = mid - 1;
                }

                return r + 1;
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

#elif Wrong

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            int n;
            (int s, int e, int w)[] arr;
            int[] dp;
            bool[] visit;
            Solve();

            void Solve()
            {

                Input();
                CompactPos();

                int ret = DFS(0);
                Console.WriteLine(ret);
            }

            int DFS(int _idx = 0)
            {

                int s = FindStart(_idx);
                if (s >= n) return 0;

                if (visit[s]) return dp[s];
                int ret = 0;
                dp[s] = ret;

                for (int i = s; i < n; i++)
                {

                    ret = Math.Max(ret, DFS(arr[i].e) + arr[i].w);
                }

                visit[s] = true;
                dp[s] = ret;
                return ret;
            }

            void Input()
            {

                n = ReadInt();
                arr = new (int s, int e, int w)[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), ReadInt(), ReadInt());
                }

                dp = new int[n];
                visit = new bool[n];
            }

            void CompactPos()
            {

                int[] pos = new int[2 * n];

                for (int i = 0; i < n; i++)
                {

                    pos[2 * i] = arr[i].s;
                    pos[2 * i + 1] = arr[i].e;
                }

                Array.Sort(pos);

                for (int i = 0; i < n; i++)
                {

                    arr[i].s = FindIdx(pos, arr[i].s);
                    arr[i].e = FindIdx(pos, arr[i].e);
                }

                Array.Sort(arr, (x, y) => x.e.CompareTo(y.e));
            }

            int FindIdx(int[] _sortedArr, int _chkVal)
            {

                int l = 0;
                int r = 2 * n - 1;

                while (l <= r)
                {

                    int mid = (l + r) / 2;

                    if (_sortedArr[mid] < _chkVal) l = mid + 1;
                    else r = mid - 1;
                }

                return r + 1;
            }

            int FindStart(int _cur)
            {

                int l = 0;
                int r = arr.Length - 1;

                while (l <= r)
                {

                    int mid = (l + r) / 2;

                    if (arr[mid].s < _cur) l = mid + 1;
                    else r = mid - 1;
                }

                return r + 1;
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
#endif
        }
    }
#if other
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.StringTokenizer;

public class Main {
	static int[][] meetings;
	static int[] startingPoints;
	static int N;

	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringTokenizer st;
		N = Integer.parseInt(br.readLine());
		meetings = new int[N][3];
		startingPoints = new int[N + 1];
		for (int i = 0; i < N; i++) {
			st = new StringTokenizer(br.readLine());
			meetings[i][0] = Integer.parseInt(st.nextToken());
			meetings[i][1] = Integer.parseInt(st.nextToken());
			meetings[i][2] = Integer.parseInt(st.nextToken());
		}
		Arrays.sort(meetings, (o1, o2) -> o1[0] - o2[0]);
		for (int i = 0; i < N; i++) {
			startingPoints[i] = meetings[i][0];
		}
		startingPoints[N] = Integer.MAX_VALUE;

		int[] dp = new int[N + 1];
		for (int i = N - 1; i >= 0; i--) {
			int next = getNextStartingPoint(meetings[i][1]);
			dp[i] = Math.max(meetings[i][2] + dp[next], dp[i + 1]);
		}
		System.out.println(dp[0]);
	}
	
	private static int getNextStartingPoint(int end) {
		int n = Arrays.binarySearch(startingPoints, end);
		if (n<0) {
			n = -n-1;
		}
		return n;
	}
}

#elif other2
class PriorityQueue
  def initialize
    @arr = []
    @arr_size = 0
  end

  def <<(x)
    @arr_size += 1
    idx = @arr_size
    while idx != 1 && @arr[idx >> 1][0] > x[0]
      @arr[idx] = @arr[idx >> 1]
      idx >>= 1
    end
    @arr[idx] = x
  end

  def pop
    result = @arr[1]
    idx = 1
    item = @arr[@arr_size]
    @arr_size -= 1
    while idx << 1 <= @arr_size
      child = idx << 1
      child += 1 if child < @arr_size && @arr[child][0] > @arr[child + 1][0]
      break if @arr[child][0] >= item[0]
      @arr[idx] = @arr[child]
      idx = child
    end
    @arr[idx] = item
    result
  end

  def peek
    @arr[1]
  end

  def isEmpty
    @arr_size == 0
  end
end

N = gets.to_i
days = []
N.times do
  x, y, w = gets.split.map &:to_i
  days.push([x, y, w])
end

cur = PriorityQueue.new
prev = 0
days.sort_by! do |i, _, _| i end
days.each do |i, j, w|
  prev = [prev, cur.pop[1]].max while !cur.isEmpty && cur.peek[0] <= i
  cur << [j, prev+w]
end

result = prev
result = [result, cur.pop[1]].max until cur.isEmpty
puts result

#elif other3
import sys
from bisect import bisect_left
input = sys.stdin.readline

n = int(input())
nums = sorted([list(map(int,input().split())) for _ in range(n)], key = lambda x : x[1]) #끝나는 시간 정렬
dpStack = [0]
idxStack = [-1]

for s,e,v in nums:
    index = bisect_left(idxStack, s + 1) #오른쪽 경계값
    if dpStack[index - 1] + v <= dpStack[-1]: continue
    
    dpStack.append(dpStack[index - 1] + v)
    idxStack.append(e)

print(dpStack[-1])
#endif
}
