using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 1
이름 : 배성훈
내용 : 청소년 상어
    문제번호 : 19236번

    구현, 시뮬레이션, 백트래킹 문제다
    이전 좌표와 방향들을 보관해야한다
    저장할 변수들이 많고, 변수가 많아 복잠해지고 실수 찾는게 힘들었다

    아이디어는 다음과 같다
    먼저 물고기 이동 -> 상어 먹이 사냥 시작 -> 먹고 DFS 탐색 -> 더 못먹으면 원위치 작업 시작
    -> 상어와 먹이 원위치 -> 물고기 원위치 -> ... 해당 방법으로 했다

    물고기 원위치에는 물고기의 좌표 뿐만 아니라 이전에 바라보던 방향까지 있어야한다
    이렇게 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0423
    {

        static void Main423(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[,] board = new int[4, 4];
            
            (int r, int c, int dir, bool dead)[] fish = new (int r, int c, int dir, bool dead)[17];

            for (int i = 0; i < 4; i++)
            {

                for (int j = 0; j < 4; j++)
                {

                    int cur = ReadInt();
                    board[i, j] = cur;
                    fish[cur] = (i, j, ReadInt() - 1, false);
                }
            }

            sr.Close();
            int ret = board[0, 0];
            fish[ret].dead = true;
            fish[0].dir = fish[ret].dir;
            fish[0].dead = false;

            // 0 : 상어
            // -1 : 빈칸
            board[0, 0] = 0;

            int[] dirR = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] dirC = { 0, -1, -1, -1, 0, 1, 1, 1 };

            ret += DFS();
            Console.WriteLine(ret);

            int DFS()
            {

                // 이전 바라보는 방향 저장
                int dirs01 = fish[1].dir;
                int dirs02 = fish[2].dir;
                int dirs03 = fish[3].dir;
                int dirs04 = fish[4].dir;
                int dirs05 = fish[5].dir;
                int dirs06 = fish[6].dir;
                int dirs07 = fish[7].dir;
                int dirs08 = fish[8].dir;
                int dirs09 = fish[9].dir;
                int dirs10 = fish[10].dir;
                int dirs11 = fish[11].dir;
                int dirs12 = fish[12].dir;
                int dirs13 = fish[13].dir;
                int dirs14 = fish[14].dir;
                int dirs15 = fish[15].dir;
                int dirs16 = fish[16].dir;

                for (int i = 1; i <= 16; i++)
                {

                    // 고기 이동
                    if (fish[i].dead) continue;
                    int curR = fish[i].r;
                    int curC = fish[i].c;
                    int curDir = fish[i].dir;

                    // 방향 전환 필요?한지 확인
                    for (int j = 0; j < 8; j++)
                    {

                        int nextDir = (curDir + j) % 8;
                        int nextR = curR + dirR[nextDir];
                        int nextC = curC + dirC[nextDir];

                        // 못가거나 상어있으면 전환한다
                        if (ChkInvalidPos(nextR, nextC) || board[nextR, nextC] == 0) continue;

                        int temp = board[nextR, nextC];
                        if (temp > 0)
                        {

                            fish[temp].r = curR;
                            fish[temp].c = curC;
                        }

                        fish[i].r = nextR;
                        fish[i].c = nextC;
                        fish[i].dir = nextDir;

                        board[curR, curC] = temp;
                        board[nextR, nextC] = i;

                        // 이동하면 다음 방향 확인이 필요없다
                        break;
                    }
                }

                int ret = 0;
                for (int i = 1; i <= 3; i++)
                {

                    // 상어의 먹이 탐색 시작
                    int nextR = fish[0].r + i * dirR[fish[0].dir];
                    int nextC = fish[0].c + i * dirC[fish[0].dir];

                    // 이동 경계 벗어나면 탈출
                    if (ChkInvalidPos(nextR, nextC)) break;
                    int temp = board[nextR, nextC];
                    // 고기 있는 곳만 이동가능
                    if (temp == -1) continue;

                    // 고기 발견해서 먹는다!
                    int calc = temp;
                    int curR = fish[0].r;
                    int curC = fish[0].c;
                    int curDir = fish[0].dir;

                    board[nextR, nextC] = 0;
                    board[curR, curC] = -1;

                    fish[0].r = nextR;
                    fish[0].c = nextC;
                    fish[0].dir = fish[temp].dir;

                    fish[temp].dead = true;
                    calc += DFS();

                    board[nextR, nextC] = temp;
                    board[curR, curC] = 0;

                    fish[0].dir = curDir;
                    fish[0].r = curR;
                    fish[0].c = curC;

                    fish[temp].dead = false;

                    if (ret < calc) ret = calc;
                }

                for (int i = 16; i>= 1; i--)
                {

                    // 물고기 원 위치로
                    if (fish[i].dead) continue;

                    int moveDir = (4 + fish[i].dir) % 8;
                    int nextR = fish[i].r + dirR[moveDir];
                    int nextC = fish[i].c + dirC[moveDir];

                    // 상어인 경우면 이동안했다!
                    // 1 * 2이거나 2 * 1인 2칸짜리 맵인 경우!
                    if (ChkInvalidPos(nextR, nextC) || board[nextR, nextC] == 0) continue;

                    int temp = board[nextR, nextC];
                    if (temp > 0)
                    {

                        fish[temp].r = fish[i].r;
                        fish[temp].c = fish[i].c;
                    }

                    board[fish[i].r, fish[i].c] = temp;
                    fish[i].r = nextR;
                    fish[i].c = nextC;
                    board[nextR, nextC] = i;
                }

                // 기존 바라보던 방향을 다시 보게한다
                fish[1].dir = dirs01;
                fish[2].dir = dirs02;
                fish[3].dir = dirs03;
                fish[4].dir = dirs04;
                fish[5].dir = dirs05;
                fish[6].dir = dirs06;
                fish[7].dir = dirs07;
                fish[8].dir = dirs08;
                fish[9].dir = dirs09;
                fish[10].dir = dirs10;
                fish[11].dir = dirs11;
                fish[12].dir = dirs12;
                fish[13].dir = dirs13;
                fish[14].dir = dirs14;
                fish[15].dir = dirs15;
                fish[16].dir = dirs16;
                return ret;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_c < 0 || _r < 0 || _c >= 4 || _r >= 4) return true;
                return false;
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
        }
    }
#if other
public static class PS
{
    public struct Shark
    {
        public (int r, int c) pos;
        public int dir;
        public int cnt;

        public Shark((int r, int c) pos, int dir, int cnt)
        {
            this.pos = pos;
            this.dir = dir;
            this.cnt = cnt;
        }
    }

    public class Fish
    {
        public (int r, int c) pos;
        public int num;
        public int dir;
        public bool isDead = false;

        public Fish((int r, int c) pos, int num, int dir)
        {
            this.pos = pos;
            this.num = num;
            this.dir = dir;
        }
    }

    public struct FishCopy
    {
        public (int r, int c) pos;
        public int num;
        public int dir;
        public bool isDead;

        public FishCopy(Fish fish)
        {
            pos = fish.pos;
            num = fish.num;
            dir = fish.dir;
            isDead = fish.isDead;
        }
    }

    public static (int r, int c)[] dir = 
        { (-1, 0), (-1, -1), (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1) };

    public static Fish[,] map = new Fish[4, 4];
    public static Fish[] fishList = new Fish[17];
    public static int max = 0;

    public static Shark shark;

    static PS()
    {
        string[] input;

        for (int i = 0; i < 4; i++)
        {
            input = Console.ReadLine().Split();

            for (int j = 0; j < 4; j++)
            {
                map[i, j] = new Fish((i, j), int.Parse(input[2 * j]), int.Parse(input[2 * j + 1]) - 1);
                fishList[map[i, j].num] = map[i, j];
            }
        }

        shark = new Shark((0, 0), map[0, 0].dir, map[0, 0].num);
        map[0, 0].isDead = true;
    }

    public static void Main()
    {
        DFS();
        Console.Write(max);
    }

    public static void DFS()
    {
        (int r, int c) nextSharkPos = 
            (shark.pos.r + dir[shark.dir].r, shark.pos.c + dir[shark.dir].c);

        if (IsOutOfBound(nextSharkPos))
        {
            max = Math.Max(max, shark.cnt);
            return;
        }

        FishCopy[,] copy = Copy();
        Move();

        Shark sharkCopy = shark;
        bool canEat = false;

        do
        {
            if (!map[nextSharkPos.r, nextSharkPos.c].isDead)
            {
                canEat = true;

                map[nextSharkPos.r, nextSharkPos.c].isDead = true;
                shark.pos = nextSharkPos;
                shark.dir = map[nextSharkPos.r, nextSharkPos.c].dir;
                shark.cnt += map[nextSharkPos.r, nextSharkPos.c].num;

                DFS();

                map[nextSharkPos.r, nextSharkPos.c].isDead = false;
                shark.pos = sharkCopy.pos;
                shark.dir = sharkCopy.dir;
                shark.cnt = sharkCopy.cnt;
            }

            nextSharkPos =
                (nextSharkPos.r + dir[shark.dir].r, nextSharkPos.c + dir[shark.dir].c);

        } while (!IsOutOfBound(nextSharkPos));

        Paste(copy);

        if (!canEat)
            max = Math.Max(max, shark.cnt);
    }

    public static bool IsOutOfBound((int r, int c) pos)
    {
        return pos.r < 0 || pos.r >= 4 || pos.c < 0 || pos.c >= 4;
    }

    public static FishCopy[,] Copy()
    {
        FishCopy[,] copy = new FishCopy[4, 4];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                copy[i, j] = new FishCopy(map[i, j]);
            }
        }

        return copy;
    }

    public static void Move()
    {
        (int r, int c) nextFishPos;
        int prevDir;

        for (int i = 1; i <= 16; i++)
        {
            if (fishList[i].isDead)
                continue;

            prevDir = fishList[i].dir;

            do
            {
                nextFishPos =
                    (fishList[i].pos.r + dir[fishList[i].dir].r, fishList[i].pos.c + dir[fishList[i].dir].c);

                if (IsOutOfBound(nextFishPos) || nextFishPos == shark.pos)
                {
                    if (fishList[i].dir++ == 7)
                        fishList[i].dir = 0;
                }
                else
                {
                    (map[fishList[i].pos.r, fishList[i].pos.c], map[nextFishPos.r, nextFishPos.c]) =
                        (map[nextFishPos.r, nextFishPos.c], map[fishList[i].pos.r, fishList[i].pos.c]);

                    map[fishList[i].pos.r, fishList[i].pos.c].pos = fishList[i].pos;
                    map[nextFishPos.r, nextFishPos.c].pos = nextFishPos;

                    break;
                }

            } while (fishList[i].dir != prevDir);
        }
    }

    public static void Paste(FishCopy[,] copy)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                map[i, j] = fishList[copy[i, j].num];

                map[i, j].pos = copy[i, j].pos;
                map[i, j].num = copy[i, j].num;
                map[i, j].dir = copy[i, j].dir;
                map[i, j].isDead = copy[i, j].isDead;
            }
        }
    }
}
#elif other2
namespace Baekjoon;

using Coord = ValueTuple<int, int>;

public class Program
{
    private static void Main(string[] args)
    {
        var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        var map = new int[4 * 4];
        var dirByNumber = new int[4 * 4 + 1];
        for (int r = 0; r < 4; r++)
            for (int c = 0; c < 4; c++)
                dirByNumber[map[4 * r + c] = ScanInt(sr)] = ScanInt(sr);
        var zeroNum = map[0];
        var zeroDir = dirByNumber[zeroNum];
        map[0] = 0;
        var ret = Progress(dirByNumber, map, zeroDir, (0, 0)) + zeroNum;
        Console.Write(ret);
    }

    static int Progress(ReadOnlySpan<int> rDirByNumber, ReadOnlySpan<int> rMap, int sharkDir, Coord sharkPos)
    {
        Span<int> dirByNumber = stackalloc int[16 + 1];
        rDirByNumber.CopyTo(dirByNumber);
        Span<int> map = stackalloc int[4 * 4];
        rMap.CopyTo(map);
        Span<Coord> posByNumber = stackalloc Coord[16 + 1];
        for (int i = 0; i <= 16; i++)
            posByNumber[i] = (-1, -1);
        for (int r = 0; r < 4; r++)
            for (int c = 0; c < 4; c++)
                posByNumber[map[4 * r + c]] = (r, c);

        for (int i = 1; i <= 16; i++)
            if (posByNumber[i] != (-1, -1))
                Move(posByNumber[i], sharkPos, map, dirByNumber, posByNumber);

        var max = 0;
        while (true)
        {
            sharkPos = MoveCoord(sharkPos, sharkDir);
            (var r, var c) = sharkPos;
            if (!IsInBoundary(sharkPos))
                break;
            ref var curFishRef = ref map[4 * r + c];
            if (curFishRef == 0)
                continue;
            var curFish = curFishRef;
            var newSharkDir = dirByNumber[curFish];
            curFishRef = 0;
            dirByNumber[curFish] = 0;
            var curPoint = Progress(dirByNumber, map, newSharkDir, sharkPos) + curFish;
            curFishRef = curFish;
            dirByNumber[curFish] = newSharkDir;
            max = Math.Max(curPoint, max);
        }
        return max;

        static void Move(Coord position, Coord sharkPos, Span<int> map, Span<int> dirByNumber, Span<Coord> posByNumber)
        {
            (var r, var c) = position;
            ref var num = ref map[4 * r + c];
            if (num == 0)
                return;
            ref var dir = ref dirByNumber[num];
            while (true)
            {
                var planned = MoveCoord(position, dir);
                (var pr, var pc) = planned;
                if (IsInBoundary(planned) && planned != sharkPos)
                {
                    ref var plannedNum = ref map[4 * pr + pc];
                    if (plannedNum > 0)
                    {
                        (posByNumber[num], posByNumber[plannedNum]) = (posByNumber[plannedNum], posByNumber[num]);
                        (num, plannedNum) = (plannedNum, num);
                    }
                    else
                    {
                        posByNumber[num] = planned;
                        plannedNum = num;
                        num = 0;
                    }
                    break;
                }
                else
                    dir = (++dir - 1) % 8 + 1;
            }
        }
    }

    static bool IsInBoundary(Coord pos)
    {
        (var r, var c) = pos;
        return 0 <= r && r < 4 && 0 <= c && c < 4;
    }

    static Coord MoveCoord(Coord pos, int dir)
    {
        (var r, var c) = pos;
        (var addR, var addC) = dir switch
        {
            1 => (-1, 0),
            2 => (-1, -1),
            3 => (0, -1),
            4 => (1, -1),
            5 => (1, 0),
            6 => (1, 1),
            7 => (0, 1),
            8 => (-1, 1),
        };
        return (r + addR, c + addC);
    }

    static int ScanInt(StreamReader sr)
    {
        int c, n = 0;
        while (!((c = sr.Read()) is ' ' or '\n' or -1))
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
}
#endif
}
