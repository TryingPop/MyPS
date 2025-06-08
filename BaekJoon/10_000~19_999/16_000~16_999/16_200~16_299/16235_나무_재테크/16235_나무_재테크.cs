using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 29
이름 : 배성훈
내용 : 나무 재테크
    문제번호 : 16235번

    시뮬레이션 문제다.
    시간 제한이 0.3초라 나름? 최적화에 신경 쓴 코드다.
*/

namespace BaekJoon.etc
{
    internal class etc_1652
    {

        static void Main1652(string[] args)
        {

            int n, m, k;
            int[][] board, summer, autumn, winter;
            PriorityQueue<(int age, int cnt), int>[][] tree;

            Input();

            GetRet();

            void GetRet()
            {

                int[] dirR = { -1, -1, 0, 1, 1, 1, 0, -1 }, dirC = { 0, 1, 1, 1, 0, -1, -1, -1 };
                (int age, int cnt)[] stk = new (int age, int cnt)[100];

                for (int i = 0; i < k; i++)
                {

                    Spring();

                    Other();
                }

                int ret = 0;

                // 나무 갯수 찾기
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        while (tree[i][j].Count > 0)
                        {

                            ret += tree[i][j].Dequeue().cnt;
                        }
                    }
                }

                Console.Write(ret);

                void Spring()
                {

                    // 토양의 양분 먹기 + 죽은 나무 확인 + 자라나야 하는 나무 확인
                    for (int r = 0; r < n; r++)
                    {

                        for (int c = 0; c < n; c++)
                        {

                            Feed(r, c);
                        }
                    }

                    void Feed(int _r, int _c)
                    {

                        PriorityQueue<(int age, int cnt), int> pq = tree[_r][_c];
                        ref int soil = ref board[_r][_c];
                        int stkLen = 0;

                        // 나무들에 양분 나눠주기
                        while (pq.Count > 0)
                        {

                            // 나무 나이, 갯수
                            (int age, int cnt) = pq.Dequeue();

                            // 나눠 준 경우
                            bool flag = false;
                            // 양분을 나눠줄 수 있는 갯수
                            int eat = soil / age;

                            if (eat < cnt)
                            {

                                if (eat > 0) flag = true;

                                // 죽은 나무의 양분 추가
                                summer[_r][_c] += (age / 2) * (cnt - eat);
                            }
                            else
                            {

                                // 양분을 모두 나눠줄 수 있다.
                                eat = cnt;
                                flag = true;
                            }

                            if (flag)
                            {

                                if (age % 5 == 4)
                                {

                                    // 봄에 5로 나눈 나머지가 4이면 가을에 인접한 땅에 나무가 자라난다.
                                    for (int dir = 0; dir < 8; dir++)
                                    {

                                        int nR = _r + dirR[dir];
                                        int nC = _c + dirC[dir];

                                        if (ChkInvalidPos(nR, nC)) continue;
                                        autumn[nR][nC] += eat;
                                    }
                                }

                                // 양분 먹은 나무만큼 토양에 양분 제거
                                soil -= age * eat;

                                // pq는 계속해서 빼내기만 하기에 살아남은 나무를 임시 보관소에 저장
                                stk[stkLen++] = (age + 1, eat);
                            }
                        }

                        // 살아남은 나무 다시 pq에 넣기
                        for (int i = 0; i < stkLen; i++)
                            pq.Enqueue(stk[i], stk[i].age);
                    }

                    bool ChkInvalidPos(int _r, int _c)
                        => _r < 0 || _c < 0 || _r >= n || _c >= n;
                }

                void Other()
                {

                    // 후연산
                    for (int r = 0; r < n; r++)
                    {

                        for (int c = 0; c < n; c++)
                        {

                            // 죽은 나무만큼 양분 추가
                            if (summer[r][c] > 0)
                            {

                                board[r][c] += summer[r][c];
                                summer[r][c] = 0;
                            }

                            // 나무가 자라나는만큼 자라나게 하기
                            if (autumn[r][c] > 0)
                            {

                                tree[r][c].Enqueue((1, autumn[r][c]), 1);
                                autumn[r][c] = 0;
                            }

                            board[r][c] += winter[r][c];
                        }
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();
                k = ReadInt();

                board = new int[n][];

                summer = new int[n][];
                autumn = new int[n][];
                winter = new int[n][];

                tree = new PriorityQueue<(int age, int cnt), int>[n][];

                for (int i = 0; i < n; i++)
                {

                    board[i] = new int[n];

                    summer[i] = new int[n];
                    autumn[i] = new int[n];
                    winter[i] = new int[n];

                    tree[i] = new PriorityQueue<(int age, int cnt), int>[n];
                    for (int j = 0; j < n; j++)
                    {

                        board[i][j] = 5;
                        winter[i][j] = ReadInt();
                        tree[i][j] = new(100);
                    }
                }

                for (int i = 0; i < m; i++)
                {

                    int r = ReadInt() - 1;
                    int c = ReadInt() - 1;
                    int age = ReadInt();
                    tree[r][c].Enqueue((age, 1), age);
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
        }
    }

#if other
using Point = System.ValueTuple<int, int>;
using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
int n = ScanInt(), m = ScanInt(), k = ScanInt();
var fields = new Place[n, n];
var a = new int[n, n];
for (int i = 0; i < n; i++)
    for (int j = 0; j < n; j++)
        fields[i, j] = new(ScanInt());

for (int i = 0; i < m; i++)
{
    int x = ScanInt() - 1, y = ScanInt() - 1, z = ScanInt();
    fields[x, y].Add(z);
}

var dirs = new Point[] { (0, 1), (0, -1), (1, 0), (-1, 0), (1, 1), (-1, -1), (1, -1), (-1, 1) };
for (int lapse = 0; lapse < k; lapse++)
{
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            fields[i, j].EatNutrition();

    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            ref var place = ref fields[i, j];
            foreach ((var ar, var ac) in dirs)
            {
                int nr = i + ar, nc = j + ac;
                if (!(0 <= nr && nr < n && 0 <= nc && nc < n))
                    continue;

                for (int o = 0; o < place.BreedableCount; o++)
                {
                    fields[nr, nc].Add(1);
                }
            }
            place.AddNutrition();
        }
}

var sum = 0;
for (int i = 0; i < n; i++)
    for (int j = 0; j < n; j++)
        sum += fields[i, j].Count;
Console.Write(sum);


int ScanInt()
{
    int c, n = 0;
    while (!((c = sr.Read()) is ' ' or '\n'))
    {
        if (c == '\r')
        {
            sr.Read();
            break;
        }
        n = 10 * n + c - '0';
    }
    return n;
}

public struct Place
{
    public readonly int AddingNutrient;
    public int Nutrient { get; private set; }
    public int BreedableCount { get; private set; }
    private readonly List<int> trees = new();

    public int Count => trees.Count;

    public void Add(int item) => trees.Add(item);
    public void AddNutrition() => Nutrient += AddingNutrient;
    public void EatNutrition()
    {
        int i = trees.Count - 1;
        for (; i >= 0 && Nutrient >= trees[i]; i--)
        {
            Nutrient -= trees[i];
        }
        i++;
        for (int j = 0; j < i; j++)
        {
            Nutrient += trees[j] / 2;
        }

        BreedableCount = 0;
        for (int j = 0; i + j < trees.Count; j++)
        {
            trees[j] = trees[i + j] + 1;
            if (trees[j] % 5 == 0)
                BreedableCount++;
        }
        trees.RemoveRange(trees.Count - i, i);
    }

    public Place(int addingNutrient) : this()
    {
        Nutrient = 5;
        AddingNutrient = addingNutrient;
    }
}
#endif
}
