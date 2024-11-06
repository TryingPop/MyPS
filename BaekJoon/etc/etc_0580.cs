using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 20
이름 : 배성훈
내용 : 숨박꼭질 5
    문제번호 : 17071번

    BFS 문제다
    아이디어는 다음과 같다
    해당 위치에서 -1 로 이동하고  +1 로 이동하면 2초 뒤에 다시 원위치가 된다
    만약 -1의 방향이 0 미만이면 +1로 이동하고 -1로 순차적으로 이동하면 2초 뒤에 원 위치가된다
    그래서 시간을 홀수와 짝수로 구분한다

    그리고 해당 타일에 도착하는데 최소 홀수, 짝수 시간을 각각 구한다

    이제 동생이 시간 별로 이동할 때 찾는 사람이 동생보다 늦게 도착하는 경우
    찾는 사람은 해당 좌표에서 동생을 만나지 못한다 -> 최단 시간을 기록했기 때문!

    그리고 동시에 도착하는 경우면 만나는게 자명하다

    이제 x좌표에 찾는사람이 먼저 도착하는 경우만 보자
    만약 동생이 10초에 도착하고 찾는 사람이 2초에 도착한 경우
    찾는 사람이 x + 1, x로 이동하여 4, 6, 8, 10을 보내면 10초에 만날 수 있다
    (x + 1이 50만을 넘는다면 x -1, x로 이동한다)

    그리고 홀수시간도 똑같이 보면 동생이 9초에 도착하고 찾는 사람이 3초에 도착하면
    같은 이동방법으로 2초씩 보내다보면 x 좌표에서 9초에 만날 수 있다

    반면 x좌표에 10초에 도착하고 찾는 사람이 홀수는 7초, 짝수는 12초에 도착했다고 하자
    짝수 최소 시간이 12초이므로 못만난다
    홀수 시간인 7초에서 어떻게 이동해도 x좌표에 10초에 못있는다
    찾는 사람은 무조건 이동해야 하고, 만나는 짝수 시간 12초가 최소 시간이 되게 저장했기 때문이다

    이렇게 검증하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0580
    {

        static void Main580(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int[] bro = new int[500_001];
            int[,] visit = new int[500_001, 2];

            Queue<(int node, int t)> q = new(500_000);
            Solve();

            void Solve()
            {

                SetMove1();
                SetMove2();

                int ret = 1_000_000;
                for (int i = 0; i <= 500_000; i++)
                {

                    if (bro[i] == 0) continue;

                    ret = Math.Min(ret, GetMeetTime(i));
                }

                if (ret == 1_000_000) ret = 0;
                ret--;
                Console.WriteLine(ret);
            }

            int GetMeetTime(int _idx)
            {

                // 만나는 시간 확인
                int even = visit[_idx, 0];
                int odd = visit[_idx, 1];

                int min = even < odd ? even : odd;
                int max = even < odd ? odd : even;
                int chk = bro[_idx];

                if (min > chk) return 1_000_000;

                if ((chk - min) % 2 == 0 || max <= chk) return chk;
                return 1_000_000;
            }


            void SetMove1()
            {

                // 동생의 시간에 따른 이동
                int next = input[1];
                int t = 1;
                bro[next] = t;
                

                while(next + t <= 500_000)
                {

                    next = next + t;
                    t++;
                    bro[next] = t;
                }
            }

            void SetMove2()
            {

                // 찾는 사람의 이동
                q.Enqueue((input[0], 1));
                visit[input[0], 1] = 1;
                
                while(q.Count > 0)
                {

                    var node = q.Dequeue();

                    int time = node.t + 1;
                    for (int i = 0; i < 3; i++)
                    {

                        int next = Next(node.node, i);
                        // 맵을 벗어나거나 이미 이전에 최적루트로 방문한 경우면 끊기
                        if (next < 0 || next > 500_000 || visit[next, time % 2] > 0) continue;

                        visit[next, time % 2] = time;
                        q.Enqueue((next, time));
                    }
                }
            }

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
        }
    }

#if other
int[][] list = new int[][] { new int[500001], new int[500001] };
int[] Check;
HashSet<int> NextList = new HashSet<int> {};

string[] str = Console.ReadLine().Split();
int s = int.Parse(str[0]);
int e = int.Parse(str[1]);

if (s == e)
{
    Console.WriteLine("0");
    return;
}

//강제 수정 방지
list[0][s] = 1;
Check = new int[] { s };
int t = 0;

while (Check.Length != 0)
{
    foreach (int v in Check)
    {
        int p = list[t][v] + 1;
        t = 1 - t;
        if (v < 500000 && (list[t][v + 1] == 0 || p < list[t][v + 1]))
        {
            list[t][v + 1] = p;
            NextList.Add(v + 1);
        }
        if (v > 0 && (list[t][v - 1] == 0 || p < list[t][v - 1]))
        {
            list[t][v - 1] = p;
            NextList.Add(v - 1);
        }
        if (v <= 250000 && (list[t][v * 2] == 0 || p < list[t][v * 2]))
        {
            list[t][v * 2] = p;
            NextList.Add(v * 2);
        }
        t = 1 - t;
    }
    t = 1 - t;
    Check = new int[NextList.Count];
    NextList.CopyTo(Check);
    NextList.Clear();
}
for (int i = 1;; i++)
{
    e += i;
    if (e > 500000)
    {
        Console.WriteLine("-1");
        return;
    }
    int v = list[i % 2][e] - 1;
    if (v >= 0 && v <= i)
    {
        Console.WriteLine(i);
        return;
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {

        static void BFS(ref Queue<int> tempQueue, ref Dictionary<int, Dictionary<int, bool>> visitDic, Dictionary<string, int> tempDic,int target, int count, out int result)
        {
            result = -1;
            while (tempQueue.Count > 0)
            {
                target += count;
                if (target > 500000)
                    return;

                if (visitDic.ContainsKey(count % 2))
                {
                    if (visitDic[count % 2].ContainsKey(target))
                    {
                        if (visitDic[count % 2][target])
                        {
                            result = count;
                            return;
                        }
                    }
                }
                else
                {
                    visitDic.Add(count % 2, new Dictionary<int, bool>());
                }
                int queueCnt = tempQueue.Count; 
                for (int i = 0; i < queueCnt; i++)
                {
                    int x = tempQueue.Dequeue();

                    foreach (var tempValue in tempDic)
                    {
                        int tempX = x;
                        switch (tempValue.Key)
                        {
                            case "add":
                                tempX += tempValue.Value;
                                break;
                            case "mul":
                                tempX *= tempValue.Value;
                                break;
                            case "min":
                                tempX -= tempValue.Value;
                                break;
                        }
                        if (tempX < 0 || tempX > 500000)
                            continue;
                        if (tempX == target)
                        {
                            result = count;
                            return;
                        }
                        if (!visitDic[count % 2].ContainsKey(tempX))
                            visitDic[count % 2].Add(tempX, true);
                        else if (visitDic[count % 2][tempX])
                            continue;

                        tempQueue.Enqueue(tempX);
                    }
                }
                count++;
            }
        }
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] tempInput = input.Split(' ');
            int x = int.Parse(tempInput[0]);
            int target = int.Parse(tempInput[1]);
            Dictionary<string, int> tempDic = new Dictionary<string, int>();
            Dictionary<int, Dictionary<int, bool>> visitDic = new Dictionary<int, Dictionary<int, bool>>();
            tempDic.Add("add", 1);
            tempDic.Add("mul", 2);
            tempDic.Add("min", 1);
            Queue<int> tempQueue = new Queue<int>() ;
            if (x == target)
            {
                Console.WriteLine(0);
                return;
            }
            tempQueue.Enqueue(x);
            BFS(ref tempQueue, ref visitDic, tempDic, target, 1, out var result);
            Console.WriteLine(result.ToString());
        }
    }
}
#endif
}
