using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 6
이름 : 배성훈
내용 : 백조의 호수
    문제번호 : 3197번

    분리집합, BFS 문제다
    백조 섬끼리 이어졌는지 확인하는데 유니온 파인드 알고리즘을 썼다
    이외는 2개의 큐를 써서 얼음을 녹여 시뮬레이션 돌렸다
*/
namespace BaekJoon.etc
{
    internal class etc_0800
    {

        static void Main800(string[] args)
        {

            StreamReader sr;
            int row, col;
            int[][] board;
            bool[][] visit;
            Queue<(int r, int c)> q1;
            Queue<(int r, int c)> q2;

            int[] group;
            Stack<int> s;
            int[] dirR, dirC;

            int l1, l2;

            Solve();
            void Solve()
            {

                Input();

                int ret = 0;
                while(true)
                {

                    // 녹은 땅 확인
                    ChkGroup();

                    // 이어진지 확인
                    if (Find(l1) == Find(l2)) break;

                    // 안이어진 경우
                    // 날짜 추가
                    ret++;

                    // 인접한 얼음 녹이기
                    Melt();
                }

                Console.Write(ret);
            }

            void Melt()
            {

                while(q2.Count > 0)
                {

                    var node = q2.Dequeue();
                    board[node.r][node.c] = 0;

                    int curG = Find(PosToIdx(node.r, node.c));

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || board[nextR][nextC] == 1) continue;
                        int nextG = Find(PosToIdx(nextR, nextC));

                        Union(curG, nextG);
                    }

                    q1.Enqueue(node);
                }
            }

            void Union(int _a, int _b)
            {

                if (_b < _a)
                {

                    int temp = _a;
                    _a = _b;
                    _b = temp;
                }

                group[_b] = _a;
            }

            int Find(int _chk)
            {

                while (group[_chk] != _chk)
                {

                    s.Push(_chk);
                    _chk = group[_chk];
                }

                while(s.Count > 0)
                {

                    group[s.Pop()] = _chk;
                }

                return _chk;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            void ChkGroup()
            {

                while(q1.Count > 0)
                {

                    (int r, int c) node = q1.Dequeue();
                    if (visit[node.r][node.c]) continue;
                    visit[node.r][node.c] = true;

                    int curIdx = PosToIdx(node.r, node.c);
                    int curG = Find(curIdx);

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || visit[nextR][nextC]) continue;

                        if (board[nextR][nextC] == 1)
                        {

                            q2.Enqueue((nextR, nextC));
                            continue;
                        }

                        int nextIdx = PosToIdx(nextR, nextC);
                        int nextG = Find(nextIdx);

                        Union(curG, nextG);
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                visit = new bool[row][];

                l1 = -1;
                l2 = -1;

                q1 = new(row * col);
                q2 = new(row * col);

                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    visit[r] = new bool[col];

                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == 'X')
                        {

                            board[r][c] = 1;
                            continue;
                        }
                        else if (cur == 'L')
                        {

                            if (l1 == -1) l1 = PosToIdx(r, c);
                            else l2 = PosToIdx(r, c);
                        }

                        q1.Enqueue((r, c));
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                group = new int[row * col];
                for (int i = 1; i < group.Length; i++)
                {

                    group[i] = i;
                }

                s = new(100);

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
                sr.Close();
            }

            int PosToIdx(int _r, int _c)
            {

                return col * _r + _c;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c= sr.Read()) != -1 && c != ' ' && c != '\n')
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
    private static (int r, int c)[] move = { (0, 1), (1, 0), (0, -1), (-1, 0) };

    private static int r, c;
    private static bool[,] map;
    private static bool[,] visited;
    private static Queue<(int r, int c)> waterPosQueue;
    private static Queue<(int r, int c)> swanPosQueue;
    private static (int r, int c) swan;

    static PS()
    {
        string[] rc = Console.ReadLine().Split();
        r = int.Parse(rc[0]);
        c = int.Parse(rc[1]);

        string input;
        bool flag = false;
        map = new bool[r, c];
        visited = new bool[r, c];
        waterPosQueue = new();
        swanPosQueue = new();

        for (int i = 0; i < r; i++)
        {
            input = Console.ReadLine();
            
            for (int j = 0; j < c; j++)
            {
                switch (input[j])
                {
                    case '.':
                        map[i, j] = false;
                        waterPosQueue.Enqueue((i, j));
                        break;

                    case 'X':
                        map[i, j] = true;
                        break;

                    case 'L':
                        if (!flag)
                        {
                            swanPosQueue.Enqueue((i, j));
                            visited[i, j] = true;
                            flag = true;
                        }    
                        else
                        {
                            swan = (i, j);
                        }

                        waterPosQueue.Enqueue((i, j));
                        break;
                }
            }
        }
    }

    public static void Main()
    {
        int day = 0;

        while (!SearchSwan())
        {
            Melt();
            day++;
        }

        Console.Write(day);
    }

    private static bool SearchSwan()
    {
        Queue<(int r, int c)> queue = new();
        (int r, int c) curPos;
        (int r, int c) nextPos;

        while (swanPosQueue.Count > 0)
        {
            curPos = swanPosQueue.Dequeue();
            queue.Enqueue(curPos);
        }

        while (queue.Count > 0)
        {
            curPos = queue.Dequeue();

            if (curPos == swan)
                return true;

            foreach (var delta in move)
            {
                nextPos = (curPos.r + delta.r, curPos.c + delta.c);

                if (nextPos.r < 0 || nextPos.r >= r || nextPos.c < 0 || nextPos.c >= c)
                    continue;

                if (!visited[nextPos.r, nextPos.c])
                {
                    visited[nextPos.r, nextPos.c] = true;

                    if (!map[nextPos.r, nextPos.c])
                        queue.Enqueue(nextPos);
                    else
                        swanPosQueue.Enqueue(nextPos);
                }
            }
        }

        return false;
    }

    private static void Melt()
    {
        (int r, int c) curPos;
        (int r, int c) nextPos;

        int cnt = waterPosQueue.Count;

        while (cnt-- > 0)
        {
            curPos = waterPosQueue.Dequeue();

            foreach (var delta in move)
            {
                nextPos = (curPos.r + delta.r, curPos.c + delta.c);

                if (nextPos.r < 0 || nextPos.r >= r || nextPos.c < 0 || nextPos.c >= c)
                    continue;

                if (map[nextPos.r, nextPos.c])
                {
                    map[nextPos.r, nextPos.c] = false;
                    waterPosQueue.Enqueue(nextPos);
                }
            }
        }
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    //좌표 구조체
    struct Coordinate{
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }

    //호수 클래스
    class Lake
    {
        //.은 물, X는 얼음, L은 백조
        char[,] lakePointArray;
        int row, column;

        //백조가 여길 탐색했나?
        bool[,] visit;

        //x축과 y축 방향
        int[] directionX = { 0, 0, 1, -1 };
        int[] directionY = { 1, -1, 0, 0 };

        Coordinate swan;
        Queue<Coordinate> swanQueue;
        Queue<Coordinate> nextSwanQueue;
        Queue<Coordinate> waterQueue;
        Queue<Coordinate> nextWaterQueue;
                          
        public Lake(int row, int column)
        {
            lakePointArray = new char[row, column];
            visit = new bool[row, column];
            this.row = row;
            this.column = column;

            swanQueue = new Queue<Coordinate>();
            nextSwanQueue = new Queue<Coordinate>();
            waterQueue = new Queue<Coordinate>();
            nextWaterQueue = new Queue<Coordinate>();
        }

        //각 행을 입력받음       
        public void inputRow(char[] row, int rowIndex)
        {
            for (int columnIndex = 0; columnIndex < column; columnIndex++)
            {
                lakePointArray[rowIndex, columnIndex] = row[columnIndex];
            }
        }

        //모든 좌표를 입력 받은 후, 물의 좌표들을 waterQueue에 넣는 함수
        private void inputAllWaterInWaterQueue()
        {
            for(int rowInThisMethod = 0; rowInThisMethod < row; rowInThisMethod++)
            {
                for(int columnInThisMethod = 0; columnInThisMethod < column; columnInThisMethod++)
                {
                    if(lakePointArray[rowInThisMethod, columnInThisMethod] == '.'
                        || lakePointArray[rowInThisMethod, columnInThisMethod] == 'L')
                    {
                        waterQueue.Enqueue(new Coordinate(rowInThisMethod, columnInThisMethod));
                    }
                }
            }
        }

        //모든 좌표를 입력 받은 후, 백조 하나의 좌표를 swan에
        private void inputSwan()
        {
            for (int rowInThisMethod = 0; rowInThisMethod < row; rowInThisMethod++)
            {
                for (int columnInThisMethod = 0; columnInThisMethod < column; columnInThisMethod++)
                {
                    if (lakePointArray[rowInThisMethod, columnInThisMethod] == 'L')
                    {
                        swan = new Coordinate(rowInThisMethod, columnInThisMethod);
                        swanQueue.Enqueue(swan);
                        return;
                    }
                }
            }
        }

        //얼음 녹이는 함수. BFS
        //인접한 곳이 얼음이면 nextWaterQueue에 push 후 물로 변환
        private void meltIce()
        {
            while (waterQueue.Count != 0)
            {
                Coordinate waterQueueFront = waterQueue.Dequeue();
                int positionX = waterQueueFront.X;
                int positionY = waterQueueFront.Y;

                for (int i = 0; i < 4; i++)
                {
                    int nextPositionX = positionX + directionX[i];
                    int nextPositionY = positionY + directionY[i];

                    if (nextPositionX >= 0 && nextPositionY >= 0
                        && nextPositionX < row && nextPositionY < column)
                    {
                        if (lakePointArray[nextPositionX, nextPositionY] == 'X')
                        {
                            lakePointArray[nextPositionX, nextPositionY] = '.';
                            nextWaterQueue.Enqueue(new Coordinate(nextPositionX, nextPositionY));
                        }
                    }
                }
            }
        }
               
        //백조가 서로 만날 수 있는가. BFS
        //호수(미로)에서 미로찾기
        //만날 수 있으면 return true
        private bool successSeeEachOther()
        {
            while(swanQueue.Count != 0)
            {
                Coordinate swanFront = swanQueue.Dequeue();
                int positionX = swanFront.X;
                int positionY = swanFront.Y;
                for(int i = 0; i<4; i++)
                {
                    int nextPositionX = positionX + directionX[i];
                    int nextPositionY = positionY + directionY[i];

                    if(nextPositionX >= 0 && nextPositionY >= 0
                        && nextPositionX < row && nextPositionY < column)
                    {
                        if(visit[nextPositionX, nextPositionY] == false)                            
                        {
                            if(lakePointArray[nextPositionX, nextPositionY] == '.')
                            {
                                swanQueue.Enqueue(new Coordinate(nextPositionX, nextPositionY));
                            }
                            else if(lakePointArray[nextPositionX, nextPositionY] == 'X')
                            {
                                nextSwanQueue.Enqueue(new Coordinate(nextPositionX, nextPositionY));
                            }
                            else if(lakePointArray[nextPositionX, nextPositionY] == 'L')
                            {
                                return true;
                            }
                            visit[nextPositionX, nextPositionY] = true;
                        }
                    }
                }
            }


            return false;
        }         

        //백조가 만나는 데 며칠이 걸리나 계산
        public int calculateDay()
        {            
            inputSwan();
            visit[swan.X, swan.Y] = true;
            inputAllWaterInWaterQueue();

            int day = 0;

            while (true)
            {
                bool successMeet = successSeeEachOther();
                if(successMeet == false)
                {
                    meltIce();
                    day++;

                    swanQueue = new Queue<Coordinate>(nextSwanQueue);
                    waterQueue = new Queue<Coordinate>(nextWaterQueue);

                    nextSwanQueue.Clear();
                    nextWaterQueue.Clear();
                }
                else //백조 탐색 성공!
                {
                    break;
                }
            }

            return day;
        }
    }
       
    class Class1
    {
        static void Main()
        {
            int row, column;
            string[] rowColumnArrayForInput = new string[2];

            rowColumnArrayForInput = Console.ReadLine().Split();
            row = int.Parse(rowColumnArrayForInput[0]);
            column = int.Parse(rowColumnArrayForInput[1]);

            Lake lake = new Lake(row, column);

            for (int rowIndex = 0; rowIndex < row; rowIndex++)
            {
                char[] rowCharArray = Console.ReadLine().ToCharArray();
                lake.inputRow(rowCharArray, rowIndex);
            }

            int dayCount = lake.calculateDay();
            Console.Write(dayCount);
        }
    }
}
#endif
}
