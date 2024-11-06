using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 25
이름 : 배성훈
내용 : 교환
    문제번호 : 1039번

    BFS 문제다
    아이디어는 다음과 같다
    먼저 탐색을 진행할 수 있는지 없는지부터 판별했다

    그리고 진행가능한 경우
    BFS로 k번 탐색 시키는데, k번 바꿨을 때 최대값을 확인한다
    만약 k번 이전에 최대값이 나오지 않는 경우 k턴째 찾은 최대값을 결과로 제출한다

    반면 k번 이전에 최대값이 나오는 경우, 탐색을 멈춘다
    이때, 결과는 최대값 또는 마지막 값 2개만 바꾼 값이 결과가 된다
    마지막 2개의 원소를 바꾼 결과가 나오는 경우인지 확인해서 바로 결과를 제출하는 식으로 코드를 구현했다

    풀 때 사용한 예제들이다
        421888 3
        124888 3
        13575 5
        312 2
*/

namespace BaekJoon.etc
{
    internal class etc_0615
    {

        static void Main615(string[] args)
        {

            string[] input = Console.ReadLine().Split();

            int change = int.Parse(input[1]);

            int len = input[0].Length;
            int[] arr = new int[len];
            int[] cnt = new int[10];

            for (int i = 0; i < len; i++)
            {

                int cur = input[0][i] - '0';
                arr[i] = cur;
                cnt[cur]++;
            }

            if (len == 1 || (len == 2 && arr[1] == 0))
            {

                Console.WriteLine(-1);
                return;
            }

            bool flag = false;
            int[] target = new int[len];
            bool[] fix = new bool[len];
            Set();
            
            int ret = BFS();
            Console.WriteLine(ret);

            void Set()
            {

                int idx = 0;
                for (int i = 9; i >= 0; i--)
                {

                    if (cnt[i] == 0) continue;
                    if (cnt[i] > 1) flag = true;
                    int cur = cnt[i];

                    for (int j = 0; j < cur; j++)
                    {

                        if (arr[idx] == i)
                        {

                            fix[idx] = true;
                            cnt[i]--;
                        }
                        target[idx] = i;
                        idx++;
                    }
                }
            }

            int BFS()
            {

                Queue<(int n, int t)> q = new();
                q.Enqueue((ArrToInt(arr), 0));

                int end = ArrToInt(target);
                int[] calc = new int[len];
                int ret = 0;
                bool find = false;
                int minTurn = 0;
                while(q.Count > 0)
                {

                    var node = q.Dequeue();
                    if (node.t == change) 
                    { 
                        
                        ret = node.n < ret ? ret : node.n;
                        continue;
                    }
                    else if (node.t == len) continue;

                    IntToArr(node.n, calc);

                    for (int i = 0; i < len; i++)
                    {

                        for (int j = i + 1; j < len; j++)
                        {

                            int temp = calc[i];
                            calc[i] = calc[j];
                            calc[j] = temp;

                            int next = ArrToInt(calc);
                            if (next == end) 
                            {

                                find = true;
                                minTurn = node.t + 1;
                                q.Clear();
                                break;
                            }
                            q.Enqueue((next, node.t + 1));

                            temp = calc[i];
                            calc[i] = calc[j];
                            calc[j] = temp;
                        }

                        if (find) break;
                    }
                }

                if (find)
                {

                    change -= minTurn;
                    change %= 2;

                    if (change == 1 && !flag)
                    {

                        int temp = target[len - 1];
                        target[len - 1] = target[len - 2];
                        target[len - 2] = temp;

                        end = ArrToInt(target);
                    }

                    ret = end;
                }
                return ret;
            }

            int ArrToInt(int[] _arr)
            {

                int ret = 0;
                for (int i = 0; i < len; i++)
                {

                    ret = ret * 10 + _arr[i];
                }

                return ret;
            }

            void IntToArr(int _n, int[] _arr)
            {

                for (int i = len - 1; i >= 0; i--)
                {

                    _arr[i] = _n % 10;
                    _n /= 10;
                }
            }
        }
    }

#if other
string[] input = Console.ReadLine().Split();
List<char> now = input[0].ToList(), max = now.ToList();
int res;
switch (now.Count)
{
    case 1:
        Console.WriteLine(-1);
        break;
    case 2:
        if (now[1] == '0') goto case 1;
        else goto default;
    default:
        max.Sort((char a, char b) => { return a == b ? 0 : a < b ? 1 : -1; });
        int leftCount = int.Parse(input[1]);
        res = 0;
        Change(leftCount, 0);
        //Console.WriteLine(string.Join("", max));
        Console.WriteLine(res);
        break;
}
void Change(int leftChange, int depth)
{
    char tmp;
    if (leftChange == 0 || depth >= now.Count) {
        
        if (leftChange % 2 == 1)
        {
            tmp = now[now.Count - 1];
            now[now.Count - 1] = now[now.Count - 2];
            now[now.Count - 2] = tmp;
            res = Math.Max(res, int.Parse(string.Join("", now)));
            tmp = now[now.Count - 2];
            now[now.Count - 2] = now[now.Count - 1];
            now[now.Count - 1] = tmp;
        }
        else res = Math.Max(res, int.Parse(string.Join("", now)));
        return;
    }
    if (now[depth] <= max[depth])
    {
        for (int i = depth + 1; i < now.Count; i++)
            if (now[i] == max[depth])
            {
                //Console.WriteLine( $"{depth} change {i}, {leftChange}");
                tmp = now[depth]; now[depth] = now[i]; now[i] = tmp;//숫자 2개 서로 바꾸기
                Change(leftChange - 1, depth + 1);
                tmp = now[i]; now[i] = now[depth]; now[depth] = tmp;//원상복귀
            }
    }
    Change(leftChange, depth + 1);
}
#elif other2
using System;
using System.Collections.Generic;

namespace Onekara
{
    internal class Program
    {
        static int borderValue = 1;
        static int m = 0;
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            string[] ss = s.Split();

            int n = int.Parse(ss[0]);
            int k = int.Parse(ss[1]);

            int tmp = n;
            for (int i=0; i<10; i++)
            {
                tmp /= 10;
                if (tmp == 0)
                {
                    m = i + 1;
                    break;
                }
                borderValue *= 10;
            }

            int ans = 0;

            List<int> possibles = new List<int>();
            possibles.Add(n);
            for(int i=0; i<k; i++)
            {
                List<int> nextPossible = new List<int>();

                ans = -1;
                foreach (int num in possibles)
                {
                    for(int p=0; p<m-1; p++)
                    {
                        for(int q=p+1; q<m; q++)
                        {
                            int nextNum = swap(num, p, q);
                            if(!nextPossible.Contains(nextNum))
                                nextPossible.Add(nextNum);

                            if (nextNum > ans)
                                ans = nextNum;
                        }
                    }
                }

                possibles = nextPossible;
            }


            Console.WriteLine(ans);
        }

        static int swap(int number, int p, int q)
        {
            int maxNum = -1;

            int expP = 1;
            int expQ = 1;

            for (int i = 0; i < p; i++)
            {
                expP *= 10;
            }
            for (int i = 0; i < q; i++)
            {
                expQ *= 10;
            }

            int digitP = (number / expP) % 10;
            int digitQ = (number / expQ) % 10;
            int diffPQ = digitP - digitQ;
            int nextValue = number - diffPQ * expP + diffPQ * expQ;
            if (nextValue < borderValue)
                nextValue = -1;

            return nextValue;
        }
    }
}
#endif
}
