using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 23
이름 : 배성훈
내용 : 모노미노도미노
    문제번호 : 19235번

    구현, 시뮬레이션 문제다
    기존 블록을 고려하면서 진행해야한다
    줄이 사라지면 중력의 영향을 받는데
    1 x 2 짜리 블록은 양 쪽 중 하나 밑에 블록이 있으면 내려가지 못해
    해당 부분 처리가 중요하다

    많아야 높이 4번만 시행하면 되고 한번당 24개만 탐색하기에
    1칸씩 내리는 연산을 했다
    
    효율적인 방법을 고려해봐도 떠오르지 않았다;
    밑에서부터 진행하는 경우 -> 기존 방법과 같고

    열마다 진행하는 경우는 -> 오히려 코드가 더 복잡해지고 
    성능개선이 안되는 듯했다

    이후 해당부분 수정하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0992
    {

        static void Main992(string[] args)
        {

            int ROW = 6;
            int COL = 4;

            StreamReader sr;
            int n;
            int[][] green, blue;
            int ret1, ret2;

            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                n = ReadInt();
                ret1 = 0;
                ret2 = 0;

                for (int i = 0; i < n; i++)
                {

                    int t = ReadInt();
                    int r = ReadInt();
                    int c = ReadInt();

                    Insert(green, t, c);
                    if (t == 2) t = 3;
                    else if (t == 3) t = 2;
                    Insert(blue, t, r);
                }

                for (int r = 2; r < ROW; r++)
                {

                    for (int c = 0; c < COL; c++)
                    {

                        if (green[r][c] != 0) ret2++;
                        if (blue[r][c] != 0) ret2++;
                    }
                }

                Console.Write($"{ret1}\n{ret2}");
            }

            // 블록 놓기
            void Insert(int[][] _board, int _t, int _c)
            {

                if (_t == 1) Move1(_board, _c);
                if (_t == 2) Move2(_board, _c);
                else if (_t == 3) Move3(_board, _c);
            }

            // 1 x 1 블록
            void Move1(int[][] _board, int _c)
            {

                int mR = ROW - 1;

                for (int r = 2; r < ROW; r++)
                {

                    if (_board[r][_c] == 0) continue;
                    mR = r - 1;
                    break;
                }

                _board[mR][_c] = 1;

                while (ChkClear(_board)) Gravity(_board);

                if (ChkTop(_board)) Down(_board);
            }

            // 1 x 2 블록
            void Move2(int[][] _board, int _c)
            {

                int mR = ROW - 1;
                for (int r = ROW - 1; r >= 2; r--)
                {

                    if (_board[r][_c] != 0 || _board[r][_c + 1] != 0) mR = r - 1;
                }

                _board[mR][_c] = 2;
                _board[mR][_c + 1] = 3;

                while (ChkClear(_board)) Gravity(_board);

                if (ChkTop(_board)) Down(_board);
            }

            // 2 x 1 블록
            void Move3(int[][] _board, int _c)
            {

                int mR = ROW - 1;
                for (int r = 2; r < ROW; r++)
                {

                    if (_board[r][_c] == 0) continue;
                    mR = r - 1;
                    break;
                }

                _board[mR][_c] = 1;
                _board[mR - 1][_c] = 1;

                bool flag = false;
                while (ChkClear(_board)) Gravity(_board);

                while (ChkTop(_board)) Down(_board);
            }

            void Clear(int[][] _board, int _r)
            {

                for (int c = 0; c < COL; c++)
                {

                    _board[_r][c] = 0;
                }

                ret1++;
            }

            void Gravity(int[][] _board)
            {

                for (int i = 0; i < 4; i++)
                {

                    for (int r = ROW - 1; r >= 1; r--)
                    {

                        for (int c = 0; c < COL; c++)
                        {

                            if (_board[r][c] != 0) continue;
                            if (_board[r - 1][c] == 1)
                            {

                                _board[r][c] = _board[r - 1][c];
                                _board[r - 1][c] = 0;
                            }
                            else if (_board[r - 1][c] == 2 && _board[r][c + 1] == 0)
                            {

                                _board[r][c] = _board[r - 1][c];
                                _board[r][c + 1] = _board[r - 1][c + 1];
                                _board[r - 1][c] = 0;
                                _board[r - 1][c + 1] = 0;
                            }
                        }
                    }
                }
            }

            void Down(int[][] _board)
            {

                for (int r = ROW - 1; r >= 1; r--)
                {

                    for (int c = 0; c < COL; c++)
                    {

                        _board[r][c] = _board[r - 1][c];
                        _board[r - 1][c] = 0;
                    }
                }
            }

            bool ChkClear(int[][] _board)
            {

                bool ret = false;
                for (int r = 0; r < ROW; r++)
                {

                    bool flag = true;
                    for (int c = 0; c < COL; c++)
                    {

                        if (_board[r][c] != 0) continue;

                        flag = false;
                        break;
                    }

                    if (flag) 
                    { 
                        
                        Clear(_board, r);
                        ret = true;
                    }
                }

                return ret;
            }

            bool ChkTop(int[][] _board)
            {

                for (int c = 0; c < COL; c++)
                {

                    if (_board[1][c] != 0) return true;
                }

                return false;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                green = new int[ROW][];
                blue = new int[ROW][];

                for (int r = 0; r < ROW; r++)
                {

                    green[r] = new int[COL];
                    blue[r] = new int[COL];
                }
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
using System;
using System.Collections.Generic;
using System.Linq;

// CapitalLetters for class names and methods, camelCase for variable names.
// Write your code clearly enough so that it doesn't need to be commented, or at least, so that it rarely needs to be commented.

namespace Testpad
{
    public class BaekJoon19235
    {
        static Pos[] move = new Pos[4] { new Pos(0, 1), new Pos(1, 0), new Pos(-1, 0), new Pos(0, -1) };

        static int score = 0; // 얻은 점수

        static List<Pos> newBlocks = new List<Pos>(); // 추가하는 블럭들 좌표
        static int[,] map = new int[10, 10]; // 지도
        static Dictionary<int, bool> removeList = new Dictionary<int, bool>(); // 제거할 라인들 목록
        static List<Pos> thisBlocks = new List<Pos>(); // 이번 블럭들 좌표

        // 좌표
        struct Pos
        {
            public int x;
            public int y;

            public Pos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        static void Main()
        {
            int n = int.Parse(Console.ReadLine()); // 블럭을 놓은 횟수

            for (int i = 0; i < n; i++)
            {
                int[] inputs = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
                int x = inputs[2];
                int y = inputs[1];

                newBlocks.Clear();

                // 크기가 1×1인 블록을 놓은 경우
                if (inputs[0] == 1)
                {
                    newBlocks.Add(new Pos(x, y));
                }
                // 크기가 1×2인 블록을 놓은 경우
                else if (inputs[0] == 2)
                {
                    newBlocks.Add(new Pos(x, y));
                    newBlocks.Add(new Pos(x + 1, y));

                }
                // 크기가 2×1인 블록을 놓은 경우
                else
                {
                    newBlocks.Add(new Pos(x, y));
                    newBlocks.Add(new Pos(x, y + 1));
                }

                MoveDown(i + 1);
                MoveRight(i + 1);

                // 디버그용
                /*for (int a = 0; a < 10; a++)
                {
                    Console.WriteLine();
                    for (int b = 0; b < 10; b++)
                    {
                        Console.Write(map[b, a] + " ");
                    }
                }
                Console.WriteLine();*/
            }

            // 남은 블럭 수를 확인하고, 결과 출력
            int leftOver = 0; // 남은 블럭 개수

            for (int i = 0; i < 4; i++)
            {
                for (int j = 6; j < 10; j++)
                {
                    if (map[j, i] > 0)
                    {
                        leftOver++;
                    }
                }
            }

            for (int i = 6; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map[j, i] > 0)
                    {
                        leftOver++;
                    }
                }
            }

            Console.WriteLine(score);
            Console.WriteLine(leftOver);
        }

        // 아래쪽으로 블럭을 보내는 연산
        static void MoveDown(int blockNumber)
        {
            int holdAt = 0; // 멈추게 할 좌표

            for (int i = 1; i < 11; i++)
            {
                foreach (var item in newBlocks)
                {
                    // 다른 블럭과 겹치거나, 배열을 벗어날 경우 중단
                    try
                    {
                        if (map[item.x, item.y + i] != 0)
                        {
                            holdAt = i - 1;
                            break;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        holdAt = i - 1;
                        break;
                    }
                }

                if (holdAt != 0)
                {
                    break;
                }
            }

            // 블럭을 보내고, 이번에 보낸 블럭 번호를 넣음
            foreach (var item in newBlocks)
            {
                map[item.x, item.y + holdAt] = blockNumber;
            }

            // 테트리스처럼 블럭을 확인하고, 라인을 제거함                      
            RemoveLines(0);

            // 특수한 칸에 블럭이 있다면, 해당 칸수만큼 라인을 밀어버림
            int pushLength = 0; // 밀어버릴 라인 수
            for (int i = 4; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map[j, i] != 0)
                    {
                        pushLength++;
                        break;
                    }
                }
            }

            Push(pushLength, 0);
        }

        // 오른쪽으로 블럭을 보내는 연산
        static void MoveRight(int blockNumber)
        {
            int holdAt = 0; // 멈추게 할 좌표

            for (int i = 1; i < 11; i++)
            {
                foreach (var item in newBlocks)
                {
                    // 다른 블럭과 겹치거나, 배열을 벗어날 경우 중단
                    try
                    {
                        if (map[item.x + i, item.y] != 0)
                        {
                            holdAt = i - 1;
                            break;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        holdAt = i - 1;
                        break;
                    }
                }

                if (holdAt != 0)
                {
                    break;
                }
            }

            // 블럭을 보내고, 이번에 보낸 블럭 번호를 넣음
            foreach (var item in newBlocks)
            {
                map[item.x + holdAt, item.y] = blockNumber;
            }

            // 테트리스처럼 블럭을 확인하고, 라인을 제거함
            RemoveLines(1);

            // 특수한 칸에 블럭이 있다면, 해당 칸수만큼 라인을 밀어버림
            int pushLength = 0; // 밀어버릴 라인 수
            for (int i = 4; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map[i, j] != 0)
                    {
                        pushLength++;
                        break;
                    }
                }
            }

            Push(pushLength, 1);
        }

        // 특수 칸의 효과로, 라인을 밀어버리는 연산
        // type == 0이면 아래로, 아니면 오른쪽으로 밀어버림
        static void Push(int pushLength, int type)
        {
            if (type == 0)
            {
                if (pushLength == 1)
                {
                    for (int i = 9; i < 10; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            map[j, i] = 0;
                        }
                    }

                    for (int i = 8; i >= 5; i--)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            map[j, i + 1] = map[j, i];
                            map[j, i] = 0;
                        }
                    }
                }
                else if (pushLength == 2)
                {
                    for (int i = 8; i < 10; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            map[j, i] = 0;
                        }
                    }

                    for (int i = 7; i >= 4; i--)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            map[j, i + 2] = map[j, i];
                            map[j, i] = 0;
                        }
                    }
                }
            }
            else
            {
                if (pushLength == 1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 9; j < 10; j++)
                        {
                            map[j, i] = 0;
                        }
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 8; j >= 5; j--)
                        {
                            map[j + 1, i] = map[j, i];
                            map[j, i] = 0;
                        }
                    }
                }
                else if (pushLength == 2)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 8; j < 10; j++)
                        {
                            map[j, i] = 0;
                        }
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 7; j >= 4; j--)
                        {
                            map[j + 2, i] = map[j, i];
                            map[j, i] = 0;
                        }
                    }
                }
            }            
        }

        // 테트리스마냥 라인을 밀어버리는 연산
        // type == 0이면 아래로, 아니면 오른쪽으로 밀어버림
        static void RemoveLines(int type)
        {
            removeList.Clear();

            if (type == 0)
            {
                for (int i = 6; i < 10; i++)
                {
                    bool matched = true; // 해당 라인을 제거할지 여부

                    for (int j = 0; j < 4; j++)
                    {
                        if (map[j, i] == 0)
                        {
                            matched = false;
                            break;
                        }
                    }

                    if (matched == true)
                    {
                        removeList.Add(i, false);
                    }
                }

                while (removeList.Count > 0)
                {
                    foreach (var i in removeList)
                    {
                        score++;
                        for (int j = 0; j < 4; j++)
                        {
                            map[j, i.Key] = 0;
                        }
                    }

                    removeList.Clear();

                    // 남는 블럭들을 이동
                    for (int i = 9; i >= 4; i--)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            // 남은 블럭을 주위로, 같은 숫자의 블럭이 있는지 확인
                            if (map[j, i] != 0)
                            {
                                thisBlocks.Clear();
                                thisBlocks.Add(new Pos(j, i));

                                int blockNumber = map[j, i];
                                map[j, i] = 0;

                                for (int k = 0; k < 4; k++)
                                {
                                    int thisX = j + move[k].x;
                                    int thisY = i + move[k].y;

                                    try
                                    {
                                        if (map[thisX, thisY] == blockNumber)
                                        {
                                            thisBlocks.Add(new Pos(thisX, thisY));
                                            map[thisX, thisY] = 0;
                                            break; // 한 클러스터는 2개 이상일 수 없음
                                        }
                                    }
                                    catch (IndexOutOfRangeException)
                                    {

                                    }
                                }

                                bool col = false; // 충돌 여부
                                int holdAt = 0; // 멈추게 할 좌표

                                for (int k = 1; k < 11; k++)
                                {
                                    foreach (var item in thisBlocks)
                                    {
                                        // 다른 블럭과 겹치거나, 배열을 벗어날 경우 중단
                                        try
                                        {
                                            if (map[item.x, item.y + k] != 0)
                                            {
                                                holdAt = k - 1;
                                                col = true;
                                                break;
                                            }
                                        }
                                        catch (IndexOutOfRangeException)
                                        {
                                            holdAt = k - 1;
                                            col = true;
                                            break;
                                        }
                                    }

                                    if (col == true)
                                    {
                                        break;
                                    }
                                }

                                // 블럭을 보내고, 이번에 보낸 블럭 번호를 넣음
                                foreach (var item in thisBlocks)
                                {
                                    map[item.x, item.y + holdAt] = blockNumber;
                                }
                            }
                        }
                    }

                    for (int i = 6; i < 10; i++)
                    {
                        bool matched = true; // 해당 라인을 제거할지 여부

                        for (int j = 0; j < 4; j++)
                        {
                            if (map[j, i] == 0)
                            {
                                matched = false;
                                break;
                            }
                        }

                        if (matched == true)
                        {
                            removeList.Add(i, false);
                        }
                    }
                }
            }
            else
            {
                for (int i = 6; i < 10; i++)
                {
                    bool matched = true; // 해당 라인을 제거할지 여부

                    for (int j = 0; j < 4; j++)
                    {
                        if (map[i, j] == 0)
                        {
                            matched = false;
                            break;
                        }
                    }

                    if (matched == true)
                    {
                        removeList.Add(i, false);
                    }
                }

                while (removeList.Count > 0)
                {
                    foreach (var i in removeList)
                    {
                        score++;
                        for (int j = 0; j < 4; j++)
                        {
                            map[i.Key, j] = 0;
                        }
                    }

                    removeList.Clear();

                    // 남는 블럭들을 이동
                    for (int i = 9; i >= 4; i--)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            // 남은 블럭을 주위로, 같은 숫자의 블럭이 있는지 확인
                            if (map[i, j] != 0)
                            {
                                thisBlocks.Clear();
                                thisBlocks.Add(new Pos(i, j));

                                int blockNumber = map[i, j];
                                map[i, j] = 0;

                                for (int k = 0; k < 4; k++)
                                {
                                    int thisX = i + move[k].x;
                                    int thisY = j + move[k].y;

                                    try
                                    {
                                        if (map[thisX, thisY] == blockNumber)
                                        {
                                            thisBlocks.Add(new Pos(thisX, thisY));
                                            map[thisX, thisY] = 0;
                                            break; // 한 클러스터는 2개 이상일 수 없음
                                        }
                                    }
                                    catch (IndexOutOfRangeException)
                                    {

                                    }
                                }

                                bool col = false; // 충돌 여부
                                int holdAt = 0; // 멈추게 할 좌표

                                for (int k = 1; k < 11; k++)
                                {
                                    foreach (var item in thisBlocks)
                                    {
                                        // 다른 블럭과 겹치거나, 배열을 벗어날 경우 중단
                                        try
                                        {
                                            if (map[item.x + k, item.y] != 0)
                                            {
                                                holdAt = k - 1;
                                                col = true;
                                                break;
                                            }
                                        }
                                        catch (IndexOutOfRangeException)
                                        {
                                            holdAt = k - 1;
                                            col = true;
                                            break;
                                        }
                                    }

                                    if (col == true)
                                    {
                                        break;
                                    }
                                }

                                // 블럭을 보내고, 이번에 보낸 블럭 번호를 넣음
                                foreach (var item in thisBlocks)
                                {
                                    map[item.x + holdAt, item.y] = blockNumber;
                                }
                            }
                        }
                    }

                    for (int i = 6; i < 10; i++)
                    {
                        bool matched = true; // 해당 라인을 제거할지 여부

                        for (int j = 0; j < 4; j++)
                        {
                            if (map[i, j] == 0)
                            {
                                matched = false;
                                break;
                            }
                        }

                        if (matched == true)
                        {
                            removeList.Add(i, false);
                        }
                    }
                }
            }
        }
    }
}
#elif other2
using System.Linq;

void Solve(List<string> commands)
{
    List<Block> blocksHorizontal = new List<Block>();
    List<Block> blocksVertical = new List<Block>();
    int score = 0;

    foreach (var cmd in commands)
    {
        var commandData = cmd.Split(" ").Select(int.Parse).ToArray();
        int type = commandData[0];
        int y = commandData[1];
        int x = commandData[2];

        List<Block> newBlocksHorizontal = CreateNewBlock(type, y, x);
        blocksHorizontal.AddRange(newBlocksHorizontal);

        MoveToMostRight(newBlocksHorizontal[0]);
        int rightestTetrisX;
        while ((rightestTetrisX = TetrisHorizontal()) != -1)
        {
            blocksHorizontal.Sort((a, b) => -a.x.CompareTo(b.x)); // x desc sort
            foreach (var e in blocksHorizontal.Where(e => e.x < rightestTetrisX))
                MoveToMostRight(e);
        }

        int movedSpecialArea;
        while ((movedSpecialArea = MoveSpecialAreaHorizontal()) != 0)
        {
            foreach (var e in blocksHorizontal)
                e.SetPosition(e.y, e.x + movedSpecialArea);

            foreach (var e in (from block in blocksHorizontal where block.x > 9 select block).ToArray())
            {
                if (e.friend != null)
                {
                    e.friend!.friend = null;
                    e.friend = null;
                }
                blocksHorizontal.Remove(e);
            }
        }

        // 세로
        List<Block> newBlocksVertical = CreateNewBlock(type, y, x);
        blocksVertical.AddRange(newBlocksVertical);

        MoveToMostBottom(newBlocksVertical[0]);
        int mostBottomTetrisY;
        while ((mostBottomTetrisY = TetrisVertical()) != -1)
        {
            blocksVertical.Sort((a, b) => -a.y.CompareTo(b.y)); // y desc sort
            foreach (var e in blocksVertical.Where(e => e.y < mostBottomTetrisY))
                MoveToMostBottom(e);
        }

        while ((movedSpecialArea = MoveSpecialAreaVertical()) != 0)
        {
            foreach (var e in blocksVertical)
                e.SetPosition(e.y + movedSpecialArea, e.x);

            foreach (var e in (from block in blocksVertical where block.y > 9 select block).ToArray())
            {
                if (e.friend != null)
                {
                    e.friend!.friend = null;
                    e.friend = null;
                }
                blocksVertical.Remove(e);
            }
        }

        // Display(blocksHorizontal.Concat(blocksVertical), cmd);
    }

    Console.WriteLine(score);
    Console.WriteLine(blocksHorizontal.Count() + blocksVertical.Count());
    // Console.WriteLine(string.Join(", ", blocksHorizontal));
    // Console.WriteLine(string.Join(", ", blocksVertical));

    List<Block> CreateNewBlock(int type, int y, int x)
    {
        List<Block> newBlocks = new List<Block> { new Block(type, y, x) };
        if (type == 2)
            newBlocks.Add(new Block(type, y, x + 1, newBlocks[0]));
        else if (type == 3)
            newBlocks.Add(new Block(type, y + 1, x, newBlocks[0]));
        return newBlocks;
    }

    void MoveToMostRight(Block moveBlock)
    {
        if (moveBlock.type == 2 && moveBlock.friend != null)
        {
            Block rightBlock = moveBlock.x < moveBlock.friend!.x ? moveBlock.friend : moveBlock;
            int bePlacedX = blocksHorizontal.Where(e => e.y == rightBlock.y && rightBlock.x < e.x).GetMostLeftBlock()?.x - 1 ?? 9;
            rightBlock.SetPosition(rightBlock.y, bePlacedX);
            rightBlock.friend!.SetPosition(rightBlock.friend.y, bePlacedX - 1);
        }
        else if (moveBlock.type == 3 && moveBlock.friend != null)
        {
            int bePlacedX = blocksHorizontal.Where(e => moveBlock.x < e.x && (moveBlock.y == e.y || moveBlock.friend.y == e.y)).GetMostLeftBlock()?.x - 1 ?? 9;
            moveBlock.SetPosition(moveBlock.y, bePlacedX);
            moveBlock.friend!.SetPosition(moveBlock.friend.y, bePlacedX);
        }
        else
        {
            int bePlacedX = blocksHorizontal.Where(e => e.y == moveBlock.y && moveBlock.x < e.x).GetMostLeftBlock()?.x - 1 ?? 9;
            moveBlock.SetPosition(moveBlock.y, bePlacedX);
        }
    }

    int TetrisHorizontal()
    {
        int rightestTetrisX = -1;
        Dictionary<int, List<Block>> tetrisDic = new Dictionary<int, List<Block>>();
        blocksHorizontal.ForEach(e =>
        {
            if (!tetrisDic.ContainsKey(e.x))
                tetrisDic[e.x] = new List<Block>();
            tetrisDic[e.x].Add(e);
        });

        for (int i = 9; i >= 6; --i)
        {
            if (!tetrisDic.ContainsKey(i) || tetrisDic[i].Count() != 4)
                continue;

            foreach (var e in tetrisDic[i])
            {
                if (e.friend != null)
                {
                    e.friend!.friend = null;
                    e.friend = null;
                }

                blocksHorizontal.Remove(e);
            }
            ++score;

            if (rightestTetrisX == -1)
                rightestTetrisX = i;
        }

        return rightestTetrisX;
    }

    int MoveSpecialAreaHorizontal()
    {
        int movedSpecialArea = 0;
        if (blocksHorizontal.Any(e => e.x == 4))
            ++movedSpecialArea;
        if (blocksHorizontal.Any(e => e.x == 5))
            ++movedSpecialArea;

        return movedSpecialArea;
    }

    // 세로
    void MoveToMostBottom(Block moveBlock)
    {
        if (moveBlock.type == 3 && moveBlock.friend != null)
        {
            Block bottomBlock = moveBlock.y < moveBlock.friend!.y ? moveBlock.friend : moveBlock;
            int bePlacedY = blocksVertical.Where(e => e.x == bottomBlock.x && bottomBlock.y < e.y).GetMostTopBlock()?.y - 1 ?? 9;
            bottomBlock.SetPosition(bePlacedY, bottomBlock.x);
            bottomBlock.friend!.SetPosition(bePlacedY - 1, bottomBlock.friend!.x);
        }
        else if (moveBlock.type == 2 && moveBlock.friend != null)
        {
            int bePlacedY = blocksVertical.Where(e => moveBlock.y < e.y && (moveBlock.x == e.x || moveBlock.friend.x == e.x)).GetMostTopBlock()?.y - 1 ?? 9;
            moveBlock.SetPosition(bePlacedY, moveBlock.x);
            moveBlock.friend!.SetPosition(bePlacedY, moveBlock.friend!.x);
        }
        else
        {
            int bePlacedY = blocksVertical.Where(e => e.x == moveBlock.x && moveBlock.y < e.y).GetMostTopBlock()?.y - 1 ?? 9;
            moveBlock.SetPosition(bePlacedY, moveBlock.x);
        }
    }

    int TetrisVertical()
    {
        int mostBottomTetrixY = -1;
        Dictionary<int, List<Block>> tetrisDic = new Dictionary<int, List<Block>>();
        blocksVertical.ForEach(e =>
        {
            if (!tetrisDic.ContainsKey(e.y))
                tetrisDic[e.y] = new List<Block>();
            tetrisDic[e.y].Add(e);
        });

        for (int i = 9; i >= 6; --i)
        {
            if (!tetrisDic.ContainsKey(i) || tetrisDic[i].Count() != 4)
                continue;

            foreach (var e in tetrisDic[i])
            {
                if (e.friend != null)
                {
                    e.friend!.friend = null;
                    e.friend = null;
                }

                blocksVertical.Remove(e);
            }
            ++score;

            if (mostBottomTetrixY == -1)
                mostBottomTetrixY = i;
        }

        return mostBottomTetrixY;
    }

    int MoveSpecialAreaVertical()
    {
        int movedSpecialArea = 0;
        if (blocksVertical.Any(e => e.y == 4))
            ++movedSpecialArea;
        if (blocksVertical.Any(e => e.y == 5))
            ++movedSpecialArea;

        return movedSpecialArea;
    }


}

void Display(IEnumerable<Block> blocks, string command = "-1 -1 -1")
{
    var commandData = command.Split(" ").Select(int.Parse).ToArray();
    int cmdType = commandData[0];
    int cmdY = commandData[1];
    int cmdX = commandData[2];
    for (int i = 0; i < 10; ++i)
    {
        for (int j = 0; (i < 4 && j < 10) || (i >= 4 && j < 4); ++j)
        {
            if (cmdY == i && cmdX == j)
            {
                Console.Write($"{cmdType} ");
                continue;
            }

            Block? block = blocks.FirstOrDefault(e => e.y == i && e.x == j);
            Console.Write($"{(block != default ? block.type : 0)} ");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}



// Solve(new List<string> { "1 1 1", "2 3 0", "3 2 2", "3 2 3", "3 1 3", "2 0 0", "3 2 0", "3 1 2" });


var data = new List<string>();
int n = int.Parse(Console.ReadLine());
for (int i = 0; i < n; ++i)
{
    // var input = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

    data.Add(Console.ReadLine());
}
Solve(data);



public class Block
{
    public int y;
    public int x;
    public int type;

    public Block? friend;

    public Block() { }

    public Block(int type, int y, int x, Block? other = null)
    {
        this.type = type;
        this.y = y;
        this.x = x;
        if (other != null)
        {
            this.friend = other;
            other.friend = this;
        }
    }

    public void SetPosition(int y, int x)
    {
        this.y = y;
        this.x = x;
    }

    public override string ToString() => $"({type} y:{y} x:{x})";
}

public static class Extents
{
    public static Block? GetMostLeftBlock(this IEnumerable<Block> arr)
    {
        int mostLeft = int.MaxValue;
        Block? mostLeftBlock = null;
        foreach (var e in arr)
        {
            if (e.x < mostLeft)
            {
                mostLeft = e.x;
                mostLeftBlock = e;
            }
        }
        return mostLeftBlock;
    }

    public static Block? GetMostTopBlock(this IEnumerable<Block> arr)
    {
        int mostTop = int.MaxValue;
        Block? mostTopBlock = null;
        foreach (var e in arr)
        {
            if (e.y < mostTop)
            {
                mostTop = e.y;
                mostTopBlock = e;
            }
        }
        return mostTopBlock;
    }
}

#elif other3
// #include<stdio.h>
// #define rep(i,n) for(int i=0;i<(n);i++)
// #define repr(i,a,b) for(int i=(a);i<=(b);i++) 
int TC=1; 
// #define read getchar_unlocked
inline int RI();
const int R = 6, C = 4;
int ans ,board[2][R+1][C] ,CNT;
inline bool safe(int col){
	return col>=0 && col<C;
}

//row 행을 없애고 위 블럭들을 쉬프트 
inline void slide(int arr[R+1][C],int row){
	for(int i=row-1;i>=0; i--){
		for(int j=0;j<C;j++)
			arr[i+1][j] = arr[i][j] , arr[i][j] = 0;
	}
}

inline int min_row(int arr[R+1][C],int type,int col){
	int row=0,ans;
	if(type==1 || type==3){
		while(!arr[row][col])
			++row;
	}else if(type==2){
		while(!arr[row][col] && !arr[row][col+1])
			++row;
	}
	return row-1;
}


inline void put(int arr[R+1][C],int type,int col){
	int row = min_row(arr,type,col);
	int num = ++CNT;
	if(type==1){ // 1*1
		arr[row][col] = num;
	}else if(type==2){ // 1*2
		arr[row][col] = arr[row][col+1] = num;
	}else if(type==3){ // 2*1
		arr[row-1][col] = arr[row][col] = num;
	}
}

// 한 번에 한 행씩 처리할 것인지, 아니면 여러 행 한 번에 처리할 것인지 결정하자.
// 아무래도 여러 행 하는게 더 좋은 듯. 모든거 다 검사하자.
// 아래서부터 보다가 밑에 빈칸 있으면 내리기 
// res=0이면 변화 없는거임
inline int getPoint(int arr[R+1][C]){
	int res=0;
	for(int i=R-1;i>=2;){
		int flag=1;
		rep(j,C){
			if(!arr[i][j]){
				flag=0;
				break;
			}
		}
		if(flag){
			++ans;
			slide(arr,i);
			res=1;
		}else{
			--i;
		}
	}
	return res;
} 

inline void update(int arr[R+1][C]){
	rep(j,C){
		//떨어져야 할 블럭이 0번째 행에 있을 수 없다.
		//떨어져야 할 블럭이 생기는 경우는 이미 한 번 행이 사라졌을 때 뿐이므로.
		for(int i=R-2;i>=1;i--){
			if(!arr[i][j])
				continue;
			if(j+1<C && arr[i][j] == arr[i][j+1])
				continue;
			if(j && arr[i][j] == arr[i][j-1]){
				int t = i+1;
				while(arr[t][j]==0 && arr[t][j-1]==0){
					arr[t][j] = arr[t-1][j];
					arr[t][j-1] = arr[t-1][j-1];
					arr[t-1][j] = 0;
					arr[t-1][j-1] = 0;
					++t;
				}
			}else if(arr[i][j]==arr[i-1][j]){
				int t = i+1;
				while(arr[t][j]==0){
					arr[t][j] = arr[t-1][j];
					arr[t-1][j] = arr[t-2][j];
					arr[t-2][j] = 0;
					++t;
				}
			}else{
				int t = i+1;
				while(arr[t][j]==0){
					arr[t][j] = arr[t-1][j];
					arr[t-1][j] = 0;
					++t;
				}
			}
		}
	}
}

inline void chk01Row(int arr[R+1][C]){
	rep(i,2){
		rep(j,C){
			if(arr[i][j]){
				slide(arr,R-1);
				if(i==0) slide(arr,R-1);
				return;
			}
		}
	}
}

int countBoard(int arr[R+1][C]){
	int res=0;
	rep(i,R){
		rep(j,C){
			if(arr[i][j])
				++res;
		}
	}
	return res;
}
void setBoard(){
//	ini(board[0][0][0],board[1][R][C],0);
	rep(i,C)
		board[0][R][i]=board[1][R][i]=-1; //마지막 행에는 블럭이 차있다고 생각하자. 
}

void solve(){
	setBoard();
	int n; n = RI();
	auto routine = [&](int arr[R+1][C],int t,int c){
		put(arr,t,c);
		while(1){
			int res = getPoint(arr);
			if(!res) break;
			update(arr);
		}
		chk01Row(arr);
	};
	rep(i,n){
		int t,x,y; t=RI(); x=RI(); y=RI();
		routine(board[0],t,y);
		if(t!=1) t = 5 - t;
		routine(board[1],t,x);

		/*
		cout<<"Turn"<<i<<nl;
		repr(i,2,R-1) rep(j,C){
			cout<<board[0][i][j]<<sp;
			if(j==C-1) cout<<nl;
		}
		*/

	}
	printf("%d\n%d\n",ans,countBoard(board[0])+countBoard(board[1]));
}

int main(){
	repr(tc,1,TC){
		
		solve();
	}
	return 0;
}


// Solution
/*
이 문제는 빡시뮬레이션 문제다.
유의할 것은 두 판을 같이 생각할 필요 없이
한 판에 대한 것만 처리하고 나중에 따로 좌표계를 변환에서 
처리해주면 된다는 것

관찰 : 블럭을 넣기 전에 연한 칸은 항상 비어 있다.
 
필요한 함수
(번외) 슬라이드 함수  //완료 

1. 넣을 수 있는 최소 행을 찾는 함수 (검사 + while 문) //완료 
2. 실제로 배열에 넣는 함수 //완료 
while(ok){
3. 행이 가득 차 있을 경우 점수 획득 + 아래로 슬라이드 
4. 내릴 수 있을만큼 내릴 수 있는 함수
type 검사가 필수적이다. 
}
    5
3 8 4 4
5. 0,1 행에 하나라도 블럭이 있다면 slide 하는 함수
 

1번 함수를 구현해보자. 결국 블럭이 놓이는 열이 중요 
type 1 : 1*1 블럭
type 2 : 1*2 블럭
type 3 : 2*1 블럭 

type 1이랑 3은 한 번에 처리할 수 있다!

모든 반례에서 걸리는걸 보면
치밀하게 구현하는 게 얼마나 중요한지 알 수 있다.
*/
inline int RI(){
	char now=read();
	int sum=0,flag=1;
	while(now<=32) now=read();
	if(now=='-') flag=-1,now=read();
	while(now>=48){
		sum=sum*10+(now&15);
		now=read();
	}
	return flag*sum;
}

#endif
}
