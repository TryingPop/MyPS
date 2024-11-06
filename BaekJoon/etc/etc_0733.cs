using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 28
이름 : 배성훈
내용 : 큐빙
    문제번호 : 5373번

    구현, 시뮬레이션 문제다
    전개도를 하나 설정하고 해당 전개도를 따라 회전시켰다
    회전 시키면 메인 1개, 그리고 그에 영향을 받는 4개의 서브 면이 존재한다
    메인의 경우 구현하기 쉬우나 서브 구현에 시간이 오래 걸렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0733
    {

        static void Main733(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            // 전개도 한개 고정
            //      B
            //      U
            //  L   F   R
            //      D
            int[][] cube;
            char[] color;
            int[] calc;

            int[][][] subIdx;
            char[] opArr;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    InitCube();

                    int len = ReadInt();

                    for (int i = 0; i < len; i++)
                    {

                        ReadOp(out int op, out bool isR);
                        
                        if (isR)
                        {

                            RotateMainR(op);
                            RotateSubR(op);
                        }
                        else
                        {

                            RotateMain(op);
                            RotateSub(op);
                        }
                    }

                    UColor();
                }

                sw.Close();
                sr.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                // F, U, R, B, D, L 순서
                cube = new int[6][];

                for (int i = 0; i < 6; i++)
                {

                    cube[i] = new int[9];
                }

                calc = new int[3];
                subIdx = new int[6][][];
                for (int i = 0; i < 6; i++)
                {

                    subIdx[i] = new int[4][];
                }

                opArr = new char[6] { 'F', 'U', 'R', 'B', 'D', 'L' };
                color = new char[6] { 'r', 'w', 'b', 'o', 'y', 'g' };

                                    // 면의 번호, 회전할 블록 번호
                // F
                subIdx[0][0] = new int[4] { 1, 6, 7, 8 };
                subIdx[0][1] = new int[4] { 2, 0, 3, 6 };
                subIdx[0][2] = new int[4] { 4, 2, 1, 0 };
                subIdx[0][3] = new int[4] { 5, 8, 5, 2 };

                // U
                subIdx[1][0] = new int[4] { 3, 6, 7, 8 };
                subIdx[1][1] = new int[4] { 2, 2, 1, 0 };
                subIdx[1][2] = new int[4] { 0, 2, 1, 0 };
                subIdx[1][3] = new int[4] { 5, 2, 1, 0 };

                // R
                subIdx[2][0] = new int[4] { 1, 2, 5, 8 };
                subIdx[2][1] = new int[4] { 3, 2, 5, 8 };
                subIdx[2][2] = new int[4] { 4, 2, 5, 8 };
                subIdx[2][3] = new int[4] { 0, 2, 5, 8 };

                // B
                subIdx[3][0] = new int[4] { 4, 6, 7, 8 };
                subIdx[3][1] = new int[4] { 2, 8, 5, 2 };
                subIdx[3][2] = new int[4] { 1, 2, 1, 0 };
                subIdx[3][3] = new int[4] { 5, 0, 3, 6 };

                // D
                subIdx[4][0] = new int[4] { 0, 6, 7, 8 };
                subIdx[4][1] = new int[4] { 2, 6, 7, 8 };
                subIdx[4][2] = new int[4] { 3, 2, 1, 0 };
                subIdx[4][3] = new int[4] { 5, 6, 7, 8 };

                // L
                subIdx[5][0] = new int[4] { 1, 0, 3, 6 };
                subIdx[5][1] = new int[4] { 0, 0, 3, 6 };
                subIdx[5][2] = new int[4] { 4, 0, 3, 6 };
                subIdx[5][3] = new int[4] { 3, 0, 3, 6 };
            }

            void InitCube()
            {

                for (int i = 0; i < 6; i++)
                {

                    for (int j = 0; j < 9; j++)
                    {

                        cube[i][j] = color[i];
                    }
                }
            }

            void ReadOp(out int _op, out bool _isR)
            {

                int c = sr.Read();
                _op = -1;
                for (int i = 0; i < 6; i++)
                {

                    if (opArr[i] != c) continue;
                    _op = i;
                    break;
                }

                c = sr.Read();
                if (c == '+') _isR = false;
                else _isR = true;

                if (sr.Read() == '\r') sr.Read();
            }

            void RotateMain(int _idx)
            {

                int temp = cube[_idx][0];
                cube[_idx][0] = cube[_idx][6];
                cube[_idx][6] = cube[_idx][8];
                cube[_idx][8] = cube[_idx][2];
                cube[_idx][2] = temp;

                temp = cube[_idx][1];
                cube[_idx][1] = cube[_idx][3];
                cube[_idx][3] = cube[_idx][7];
                cube[_idx][7] = cube[_idx][5];
                cube[_idx][5] = temp;
            }

            void RotateMainR(int _idx)
            {

                int temp = cube[_idx][0];
                cube[_idx][0] = cube[_idx][2];
                cube[_idx][2] = cube[_idx][8];
                cube[_idx][8] = cube[_idx][6];
                cube[_idx][6] = temp;

                temp = cube[_idx][1];
                cube[_idx][1] = cube[_idx][5];
                cube[_idx][5] = cube[_idx][7];
                cube[_idx][7] = cube[_idx][3];
                cube[_idx][3] = temp;
            }

            void RotateSub(int _idx)
            {

                int[][] useArr = subIdx[_idx];

                int area = useArr[3][0]; 
                int block;
                for (int j = 1; j < 4; j++)
                {

                    block = useArr[3][j];
                    calc[j - 1] = cube[area][block];
                }

                for (int i = 3; i >= 1; i--)
                {

                    int[] nextTemp = useArr[i];
                    int[] curTemp = useArr[i - 1];

                    for (int j = 1; j < 4; j++)
                    {

                        cube[nextTemp[0]][nextTemp[j]] = cube[curTemp[0]][curTemp[j]];
                    }
                }

                area = useArr[0][0];
                for (int j = 1; j < 4; j++)
                {

                    block = useArr[0][j];
                    cube[area][block] = calc[j - 1];
                }
            }

            void RotateSubR(int _idx)
            {

                int[][] useArr = subIdx[_idx];

                int area = useArr[0][0];
                int block;

                for (int j = 1; j < 4; j++)
                {

                    block = useArr[0][j];
                    calc[j - 1] = cube[area][block];
                }

                for (int i = 1; i < 4; i++)
                {

                    int[] nextTemp = useArr[i - 1];
                    int[] curTemp = useArr[i];

                    for (int j = 1; j < 4; j++)
                    {

                        cube[nextTemp[0]][nextTemp[j]] = cube[curTemp[0]][curTemp[j]];
                    }
                }

                area = useArr[3][0];
                for (int j = 1; j < 4; j++)
                {

                    block = useArr[3][j];
                    cube[area][block] = calc[j - 1];
                }
            }
            
            void UColor()
            {

                for (int i = 0; i < 9; i++)
                {

                    sw.Write((char)cube[1][i]);
                    if (i % 3 == 2) sw.Write('\n');
                }
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
        }
    }

#if other
public static class PS
{
    private static char[,,] cube;

    public static void Main()
    {
        StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));

        int t = int.Parse(Console.ReadLine());
        int n;
        string[] input;

        while (t-- > 0)
        {
            n = int.Parse(Console.ReadLine());
            input = Console.ReadLine().Split();

            Init();

            for (int i = 0; i < n; i++)
            {
                Turn(input[i]);
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sw.Write(cube[0, i, j]);
                }

                sw.Write('\n');
            }
        }

        sw.Close();
    }

    private static void Init()
    {
        cube = new char[6, 3, 3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                cube[0, i, j] = 'w';
                cube[1, i, j] = 'y';
                cube[2, i, j] = 'r';
                cube[3, i, j] = 'o';
                cube[4, i, j] = 'g';
                cube[5, i, j] = 'b';
            }
        }
    }

    private static void Turn(string instr)
    {
        switch (instr[0])
        {
            case 'U': TurnU(instr[1]); TurnSide(0, instr[1]); break;
            case 'D': TurnD(instr[1]); TurnSide(1, instr[1] == '+' ? '-' : '+'); break;
            case 'F': TurnF(instr[1]); TurnSide(2, instr[1]); break;
            case 'B': TurnB(instr[1]); TurnSide(3, instr[1]); break;
            case 'L': TurnL(instr[1]); TurnSide(4, instr[1]); break;
            case 'R': TurnR(instr[1]); TurnSide(5, instr[1]); break;
        }
    }

    private static void TurnU(char dir)
    {
        char[] temp = { cube[2, 0, 0], cube[2, 0, 1], cube[2, 0, 2] };

        switch (dir)
        {
            case '+':
                (cube[2, 0, 0], cube[2, 0, 1], cube[2, 0, 2]) = (cube[5, 0, 0], cube[5, 0, 1], cube[5, 0, 2]);
                (cube[5, 0, 0], cube[5, 0, 1], cube[5, 0, 2]) = (cube[3, 0, 0], cube[3, 0, 1], cube[3, 0, 2]);
                (cube[3, 0, 0], cube[3, 0, 1], cube[3, 0, 2]) = (cube[4, 0, 0], cube[4, 0, 1], cube[4, 0, 2]);
                (cube[4, 0, 0], cube[4, 0, 1], cube[4, 0, 2]) = (temp[0], temp[1], temp[2]);
                break;

            case '-':
                (cube[2, 0, 0], cube[2, 0, 1], cube[2, 0, 2]) = (cube[4, 0, 0], cube[4, 0, 1], cube[4, 0, 2]);
                (cube[4, 0, 0], cube[4, 0, 1], cube[4, 0, 2]) = (cube[3, 0, 0], cube[3, 0, 1], cube[3, 0, 2]);
                (cube[3, 0, 0], cube[3, 0, 1], cube[3, 0, 2]) = (cube[5, 0, 0], cube[5, 0, 1], cube[5, 0, 2]);
                (cube[5, 0, 0], cube[5, 0, 1], cube[5, 0, 2]) = (temp[0], temp[1], temp[2]);
                break;
        }
    }

    private static void TurnD(char dir)
    {
        char[] temp = { cube[2, 2, 0], cube[2, 2, 1], cube[2, 2, 2] };

        switch (dir)
        {
            case '+':
                (cube[2, 2, 0], cube[2, 2, 1], cube[2, 2, 2]) = (cube[4, 2, 0], cube[4, 2, 1], cube[4, 2, 2]);
                (cube[4, 2, 0], cube[4, 2, 1], cube[4, 2, 2]) = (cube[3, 2, 0], cube[3, 2, 1], cube[3, 2, 2]);
                (cube[3, 2, 0], cube[3, 2, 1], cube[3, 2, 2]) = (cube[5, 2, 0], cube[5, 2, 1], cube[5, 2, 2]);
                (cube[5, 2, 0], cube[5, 2, 1], cube[5, 2, 2]) = (temp[0], temp[1], temp[2]);
                break;

            case '-':
                (cube[2, 2, 0], cube[2, 2, 1], cube[2, 2, 2]) = (cube[5, 2, 0], cube[5, 2, 1], cube[5, 2, 2]);
                (cube[5, 2, 0], cube[5, 2, 1], cube[5, 2, 2]) = (cube[3, 2, 0], cube[3, 2, 1], cube[3, 2, 2]);
                (cube[3, 2, 0], cube[3, 2, 1], cube[3, 2, 2]) = (cube[4, 2, 0], cube[4, 2, 1], cube[4, 2, 2]);
                (cube[4, 2, 0], cube[4, 2, 1], cube[4, 2, 2]) = (temp[0], temp[1], temp[2]);
                break;
        }
    }

    private static void TurnF(char dir)
    {
        char[] temp = { cube[0, 2, 0], cube[0, 2, 1], cube[0, 2, 2] };

        switch (dir)
        {
            case '+':
                (cube[0, 2, 0], cube[0, 2, 1], cube[0, 2, 2]) = (cube[4, 2, 2], cube[4, 1, 2], cube[4, 0, 2]);
                (cube[4, 2, 2], cube[4, 1, 2], cube[4, 0, 2]) = (cube[1, 2, 2], cube[1, 2, 1], cube[1, 2, 0]);
                (cube[1, 2, 2], cube[1, 2, 1], cube[1, 2, 0]) = (cube[5, 0, 0], cube[5, 1, 0], cube[5, 2, 0]);
                (cube[5, 0, 0], cube[5, 1, 0], cube[5, 2, 0]) = (temp[0], temp[1], temp[2]);
                break;

            case '-':
                (cube[0, 2, 0], cube[0, 2, 1], cube[0, 2, 2]) = (cube[5, 0, 0], cube[5, 1, 0], cube[5, 2, 0]);
                (cube[5, 0, 0], cube[5, 1, 0], cube[5, 2, 0]) = (cube[1, 2, 2], cube[1, 2, 1], cube[1, 2, 0]);
                (cube[1, 2, 2], cube[1, 2, 1], cube[1, 2, 0]) = (cube[4, 2, 2], cube[4, 1, 2], cube[4, 0, 2]);
                (cube[4, 2, 2], cube[4, 1, 2], cube[4, 0, 2]) = (temp[0], temp[1], temp[2]);
                break;
        }
    }

    private static void TurnB(char dir)
    {
        char[] temp = { cube[0, 0, 0], cube[0, 0, 1], cube[0, 0, 2] };

        switch (dir)
        {
            case '+':
                (cube[0, 0, 0], cube[0, 0, 1], cube[0, 0, 2]) = (cube[5, 0, 2], cube[5, 1, 2], cube[5, 2, 2]);
                (cube[5, 0, 2], cube[5, 1, 2], cube[5, 2, 2]) = (cube[1, 0, 2], cube[1, 0, 1], cube[1, 0, 0]);
                (cube[1, 0, 2], cube[1, 0, 1], cube[1, 0, 0]) = (cube[4, 2, 0], cube[4, 1, 0], cube[4, 0, 0]);
                (cube[4, 2, 0], cube[4, 1, 0], cube[4, 0, 0]) = (temp[0], temp[1], temp[2]);
                break;

            case '-':
                (cube[0, 0, 0], cube[0, 0, 1], cube[0, 0, 2]) = (cube[4, 2, 0], cube[4, 1, 0], cube[4, 0, 0]);
                (cube[4, 2, 0], cube[4, 1, 0], cube[4, 0, 0]) = (cube[1, 0, 2], cube[1, 0, 1], cube[1, 0, 0]);
                (cube[1, 0, 2], cube[1, 0, 1], cube[1, 0, 0]) = (cube[5, 0, 2], cube[5, 1, 2], cube[5, 2, 2]);
                (cube[5, 0, 2], cube[5, 1, 2], cube[5, 2, 2]) = (temp[0], temp[1], temp[2]);
                break;
        }
    }

    private static void TurnL(char dir)
    {
        char[] temp = { cube[0, 0, 0], cube[0, 1, 0], cube[0, 2, 0] };

        switch (dir)
        {
            case '+':
                (cube[0, 0, 0], cube[0, 1, 0], cube[0, 2, 0]) = (cube[3, 2, 2], cube[3, 1, 2], cube[3, 0, 2]);
                (cube[3, 2, 2], cube[3, 1, 2], cube[3, 0, 2]) = (cube[1, 2, 0], cube[1, 1, 0], cube[1, 0, 0]);
                (cube[1, 2, 0], cube[1, 1, 0], cube[1, 0, 0]) = (cube[2, 0, 0], cube[2, 1, 0], cube[2, 2, 0]);
                (cube[2, 0, 0], cube[2, 1, 0], cube[2, 2, 0]) = (temp[0], temp[1], temp[2]);
                break;

            case '-':
                (cube[0, 0, 0], cube[0, 1, 0], cube[0, 2, 0]) = (cube[2, 0, 0], cube[2, 1, 0], cube[2, 2, 0]);
                (cube[2, 0, 0], cube[2, 1, 0], cube[2, 2, 0]) = (cube[1, 2, 0], cube[1, 1, 0], cube[1, 0, 0]);
                (cube[1, 2, 0], cube[1, 1, 0], cube[1, 0, 0]) = (cube[3, 2, 2], cube[3, 1, 2], cube[3, 0, 2]);
                (cube[3, 2, 2], cube[3, 1, 2], cube[3, 0, 2]) = (temp[0], temp[1], temp[2]);
                break;        
        }
    }

    private static void TurnR(char dir)
    {
        char[] temp = { cube[0, 0, 2], cube[0, 1, 2], cube[0, 2, 2] };

        switch (dir)
        {
            case '+':
                (cube[0, 0, 2], cube[0, 1, 2], cube[0, 2, 2]) = (cube[2, 0, 2], cube[2, 1, 2], cube[2, 2, 2]);
                (cube[2, 0, 2], cube[2, 1, 2], cube[2, 2, 2]) = (cube[1, 2, 2], cube[1, 1, 2], cube[1, 0, 2]);
                (cube[1, 2, 2], cube[1, 1, 2], cube[1, 0, 2]) = (cube[3, 2, 0], cube[3, 1, 0], cube[3, 0, 0]);
                (cube[3, 2, 0], cube[3, 1, 0], cube[3, 0, 0]) = (temp[0], temp[1], temp[2]);
                break;

            case '-':
                (cube[0, 0, 2], cube[0, 1, 2], cube[0, 2, 2]) = (cube[3, 2, 0], cube[3, 1, 0], cube[3, 0, 0]);
                (cube[3, 2, 0], cube[3, 1, 0], cube[3, 0, 0]) = (cube[1, 2, 2], cube[1, 1, 2], cube[1, 0, 2]);
                (cube[1, 2, 2], cube[1, 1, 2], cube[1, 0, 2]) = (cube[2, 0, 2], cube[2, 1, 2], cube[2, 2, 2]);
                (cube[2, 0, 2], cube[2, 1, 2], cube[2, 2, 2]) = (temp[0], temp[1], temp[2]);
                break;
        }
    }

    private static void TurnSide(int side, char dir)
    {
        char[] temp = { cube[side, 0, 0], cube[side, 0, 1], cube[side, 0, 2] };

        switch (dir)
        {
            case '+':
                (cube[side, 0, 0], cube[side, 0, 1], cube[side, 0, 2]) = (cube[side, 2, 0], cube[side, 1, 0], cube[side, 0, 0]);
                (cube[side, 2, 0], cube[side, 1, 0], cube[side, 0, 0]) = (cube[side, 2, 2], cube[side, 2, 1], cube[side, 2, 0]);
                (cube[side, 2, 2], cube[side, 2, 1], cube[side, 2, 0]) = (cube[side, 0, 2], cube[side, 1, 2], cube[side, 2, 2]);
                (cube[side, 0, 2], cube[side, 1, 2], cube[side, 2, 2]) = (temp[0], temp[1], temp[2]);
                break;

            case '-':
                (cube[side, 0, 0], cube[side, 0, 1], cube[side, 0, 2]) = (cube[side, 0, 2], cube[side, 1, 2], cube[side, 2, 2]);
                (cube[side, 0, 2], cube[side, 1, 2], cube[side, 2, 2]) = (cube[side, 2, 2], cube[side, 2, 1], cube[side, 2, 0]);
                (cube[side, 2, 2], cube[side, 2, 1], cube[side, 2, 0]) = (cube[side, 2, 0], cube[side, 1, 0], cube[side, 0, 0]);
                (cube[side, 2, 0], cube[side, 1, 0], cube[side, 0, 0]) = (temp[0], temp[1], temp[2]);
                break;
        }
    }
}
#elif other2
var reader = new Reader();
int t = reader.NextInt();
int n = 0;

var faces = new char[6][][] { FaceAlloc(), FaceAlloc(), FaceAlloc(), FaceAlloc(), FaceAlloc(), FaceAlloc() };
var (top, bottom, front, back, left, right) = (faces[0], faces[1], faces[2], faces[3], faces[4], faces[5]);

var tempFace = FaceAlloc();
var tempRow = new char[12];

var faceRotationCW = new (int r, int c)[3][] { 
    new (int, int)[3] { (2, 0), (1, 0), (0, 0) },
    new (int, int)[3] { (2, 1), (1, 1), (0, 1) },
    new (int, int)[3] { (2, 2), (1, 2), (0, 2) },
};
var faceRotationACW = new (int r, int c)[3][] { 
    new (int, int)[3] { (0, 2), (1, 2), (2, 2) },
    new (int, int)[3] { (0, 1), (1, 1), (2, 1) },
    new (int, int)[3] { (0, 0), (1, 0), (2, 0) },
};

using (var writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
while (t-- > 0)
{
    n = reader.NextInt();

    ResetCube();
    while (n-- > 0)
        Rotate(reader.NextChar(), reader.NextChar());

    PrintTop(writer);
}

char[][] FaceAlloc() => new char[3][] { new char[3], new char[3], new char[3] };

void ResetCube()
{
    for (int i = 0; i < 3; i++)
    {
        Array.Fill(top[i], 'w');
        Array.Fill(bottom[i], 'y');
        Array.Fill(front[i], 'r');
        Array.Fill(back[i], 'o');
        Array.Fill(left[i], 'g');
        Array.Fill(right[i], 'b');
    }
}

void Rotate(char side, char dir)
{
    switch (side)
    {
        case 'U':
            RotateFace(top, dir);
            RotateAdjacentRows(new (int, int, int)[12] {
                (2, 0, 2), (2, 0, 1), (2, 0, 0),
                (4, 0, 2), (4, 0, 1), (4, 0, 0),
                (3, 0, 2), (3, 0, 1), (3, 0, 0),
                (5, 0, 2), (5, 0, 1), (5, 0, 0)
            }, dir);
        break;

        case 'D':
            RotateFace(bottom, dir);
            RotateAdjacentRows(new (int, int, int)[12] {
                (2, 2, 0), (2, 2, 1), (2, 2, 2),
                (5, 2, 0), (5, 2, 1), (5, 2, 2),
                (3, 2, 0), (3, 2, 1), (3, 2, 2),
                (4, 2, 0), (4, 2, 1), (4, 2, 2)
            }, dir);
        break;

        case 'F':
            RotateFace(front, dir);
            RotateAdjacentRows(new (int, int, int)[12] {
                (0, 2, 0), (0, 2, 1), (0, 2, 2),
                (5, 0, 0), (5, 1, 0), (5, 2, 0),
                (1, 0, 2), (1, 0, 1), (1, 0, 0),
                (4, 2, 2), (4, 1, 2), (4, 0, 2)
            }, dir);
        break;

        case 'B':
            RotateFace(back, dir);
            RotateAdjacentRows(new (int, int, int)[12] {
                (0, 0, 2), (0, 0, 1), (0, 0, 0),
                (4, 0, 0), (4, 1, 0), (4, 2, 0),
                (1, 2, 0), (1, 2, 1), (1, 2, 2),
                (5, 2, 2), (5, 1, 2), (5, 0, 2)
            }, dir);
        break;

        case 'L':
            RotateFace(left, dir);
            RotateAdjacentRows(new (int, int, int)[12] {
                (0, 0, 0), (0, 1, 0), (0, 2, 0),
                (2, 0, 0), (2, 1, 0), (2, 2, 0),
                (1, 0, 0), (1, 1, 0), (1, 2, 0),
                (3, 2, 2), (3, 1, 2), (3, 0, 2)
            }, dir);
        break;

        case 'R':
            RotateFace(right, dir);
            RotateAdjacentRows(new (int, int, int)[12] {
                (0, 2, 2), (0, 1, 2), (0, 0, 2),
                (3, 0, 0), (3, 1, 0), (3, 2, 0),
                (1, 2, 2), (1, 1, 2), (1, 0, 2),
                (2, 2, 2), (2, 1, 2), (2, 0, 2)
            }, dir);
        break;
    }
}

void RotateFace(char[][] face, char dir)
{
    // copy face
    for (int i = 0; i < 3; i++)
        for (int j = 0; j < 3; j++)
            tempFace[i][j] = face[i][j];

    // rotate
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            var (r, c) = dir == '+' ? faceRotationCW[i][j] : faceRotationACW[i][j];
            face[i][j] = tempFace[r][c];
        }
    }
}

// pass in clockwise
void RotateAdjacentRows((int, int, int)[] rows, char dir)
{
    for (int i = 0; i < 12; i++)
    {
        var (f, r, c) = rows[i];
        tempRow[i] = faces[f][r][c];
    }

    int dif = dir == '+' ? +3 : -3;
    for (int i = 0; i < 12; i++)
    {
        var (df, dr, dc) = rows[Mod(i + dif, 12)];
        faces[df][dr][dc] = tempRow[i];
    }
}

void PrintTop(StreamWriter writer)
{
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
            writer.Write(top[i][j]);
        writer.WriteLine();
    }
}

int Mod(int n, int m) => (n % m + m) % m;

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
public char NextChar(){char v='\0';while(true){int c=R.Read();if(c!=' '&&c!='\n'&&c!='\r'){v=(char)c;break;}}return v;}
}
#elif other3
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    static List<char>[] cube = new List<char>[6];
    static void Main()
    {
        Dictionary<char, int> dic = new()
        {
            { 'U', 0 },
            { 'D', 5 },
            { 'F', 2 },
            { 'B', 4 },
            { 'L', 1 },
            { 'R', 3 }
        };
        int t = int.Parse(Console.ReadLine());
        StringBuilder sb = new();
        for (int i = 0; i < t; i++)
        {
            int n = int.Parse(Console.ReadLine());
            string[] q = Console.ReadLine().Split(' ');
            cube[0] = Enumerable.Repeat('w', 8).ToList();
            cube[1] = Enumerable.Repeat('g', 8).ToList();
            cube[2] = Enumerable.Repeat('r', 8).ToList();
            cube[3] = Enumerable.Repeat('b', 8).ToList();
            cube[4] = Enumerable.Repeat('o', 8).ToList();
            cube[5] = Enumerable.Repeat('y', 8).ToList();
            for (int j = 0; j < n; j++)
            {
                Rotate(dic[q[j][0]], q[j][1] == '+' ? 1 : 3);
            }
            sb.Append($"{cube[0][0]}{cube[0][1]}{cube[0][2]}\n{cube[0][7]}w{cube[0][3]}\n{cube[0][6]}{cube[0][5]}{cube[0][4]}\n");
        }
        sb.Remove(sb.Length - 1, 1);
        Console.Write(sb.ToString());
    }
    static void Rotate(int n, int count)
    {
        for (int i = 0; i < count; i++)
        {
            cube[n].Insert(0, cube[n][^1]);
            cube[n].RemoveAt(8);
            cube[n].Insert(0, cube[n][^1]);
            cube[n].RemoveAt(8);
            RotateAdjacent(n);
        }
    }
    static readonly int[][] adjacent =
    {
        new[] { 4, 2, 1, 0, 3, 2, 1, 0, 2, 2, 1, 0, 1, 2, 1, 0 },
        new[] { 0, 0, 7, 6, 2, 0, 7, 6, 5, 0, 7, 6, 4, 4, 3, 2 },
        new[] { 0, 6, 5, 4, 3, 0, 7, 6, 5, 2, 1, 0, 1, 4, 3, 2 },
        new[] { 0, 4, 3, 2, 4, 0, 7, 6, 5, 4, 3, 2, 2, 4, 3, 2 },
        new[] { 0, 2, 1, 0, 1, 0, 7, 6, 5, 6, 5, 4, 3, 4, 3, 2 },
        new[] { 2, 6, 5, 4, 3, 6, 5, 4, 4, 6, 5, 4, 1, 6, 5, 4 }
    };
    static Queue<char> q = new();
    static void RotateAdjacent(int n)
    {
        for (int i = 1; i <= 3; i++)
        {
            q.Enqueue(cube[adjacent[n][12]][adjacent[n][12 + i]]);
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                q.Enqueue(cube[adjacent[n][4 * i]][adjacent[n][4 * i + j]]);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                cube[adjacent[n][4 * i]][adjacent[n][4 * i + j]] = q.Dequeue();
            }
        }
    }
}
#elif other4
char[,,] cube = new char[5, 5, 5];

string[] line = Console.ReadLine().Split(' ');
int T = int.Parse(line[0]);
while (T-- > 0)
{
    InitCube(cube);
    line = Console.ReadLine().Split(' ');
    int N = int.Parse(line[0]);
    line = Console.ReadLine().Split(' ');
    for (int i = 0; i < N; i++)
    {
        switch (line[i][0])
        {
            case 'U':
                Up(cube, line[i][1]);
                break;
            case 'D':
                Down(cube, line[i][1]);
                break;
            case 'L':
                Left(cube, line[i][1]);
                break;
            case 'R':
                Right(cube, line[i][1]);
                break;
            case 'F':
                Front(cube, line[i][1]);
                break;
            case 'B':
                Back(cube, line[i][1]);
                break;
        }
    }
    Print(cube);
    //PrintBack(cube);
}


void InitCube(char[,,] c)
{
    // k = 0 윗면 White
    for (int i = 1; i <= 3; i++)
        for (int j = 1; j <= 3; j++)
            c[i, j, 0] = 'w';
    // k = 4 아랫면 Yellow
    for (int i = 1; i <= 3; i++)
        for (int j = 1; j <= 3; j++)
            c[i, j, 4] = 'y';
    // j = 0 왼쪽 Green
    for (int i = 1; i <= 3; i++)
        for (int k = 1; k <= 3; k++)
            c[i, 0, k] = 'g';
    // j = 4 오른쪽 Blue
    for (int i = 1; i <= 3; i++)
        for (int k = 1; k <= 3; k++)
            c[i, 4, k] = 'b';
    // i = 0 뒷면 Orange
    for (int j = 1; j <= 3; j++)
        for (int k = 1; k <= 3; k++)
            c[0, j, k] = 'o';
    // i = 4 앞면 Red
    for (int j = 1; j <= 3; j++)
        for (int k = 1; k <= 3; k++)
            c[4, j, k] = 'r';
}

void Print(char[,,] c)
{
    for (int i = 1; i <= 3; i++)
    {
        for (int j = 1; j <= 3; j++)
            Console.Write(c[i, j, 0]);
        Console.WriteLine();
    }
}
void PrintBack(char[,,] c)
{
    for (int k = 1; k <= 3; k++)
    { 
        for (int j = 3; j >= 1; j--)
            Console.Write(c[0, j, k]);
    Console.WriteLine();
}
}

void Up(char[,,] c, char t)
{
    if (t == '+')
    {
        // 정면: 뒤 2개 앞으로
        (c[1, 1, 0], c[1, 2, 0], c[1, 3, 0], c[2, 3, 0], c[3, 3, 0], c[3, 2, 0], c[3, 1, 0], c[2, 1, 0])
        = (c[3, 1, 0], c[2, 1, 0], c[1, 1, 0], c[1, 2, 0], c[1, 3, 0], c[2, 3, 0], c[3, 3, 0], c[3, 2, 0]);
        // 측면: 뒤 3개 앞으로
        (c[0, 1, 1], c[0, 2, 1], c[0, 3, 1], c[1, 4, 1], c[2, 4, 1], c[3, 4, 1], c[4, 3, 1], c[4, 2, 1], c[4, 1, 1], c[3, 0, 1], c[2, 0, 1], c[1, 0, 1])
        = (c[3, 0, 1], c[2, 0, 1], c[1, 0, 1], c[0, 1, 1], c[0, 2, 1], c[0, 3, 1], c[1, 4, 1], c[2, 4, 1], c[3, 4, 1], c[4, 3, 1], c[4, 2, 1], c[4, 1, 1]);
    }
    else if (t == '-')
    {
        // 정면: 앞 2개 뒤로
        (c[1, 1, 0], c[1, 2, 0], c[1, 3, 0], c[2, 3, 0], c[3, 3, 0], c[3, 2, 0], c[3, 1, 0], c[2, 1, 0])
        = (c[1, 3, 0], c[2, 3, 0], c[3, 3, 0], c[3, 2, 0], c[3, 1, 0], c[2, 1, 0], c[1, 1, 0], c[1, 2, 0]);
        // 측면: 앞 3개 뒤로
        (c[0, 1, 1], c[0, 2, 1], c[0, 3, 1], c[1, 4, 1], c[2, 4, 1], c[3, 4, 1], c[4, 3, 1], c[4, 2, 1], c[4, 1, 1], c[3, 0, 1], c[2, 0, 1], c[1, 0, 1])
        = (c[1, 4, 1], c[2, 4, 1], c[3, 4, 1], c[4, 3, 1], c[4, 2, 1], c[4, 1, 1], c[3, 0, 1], c[2, 0, 1], c[1, 0, 1], c[0, 1, 1], c[0, 2, 1], c[0, 3, 1]);
    }
    else
        throw new Exception("UP");
}
void Down(char[,,] c, char t)
{
    // 윗면과 반대로
    if (t == '-')
    {
        // 정면: 뒤 2개 앞으로
        (c[1, 1, 4], c[1, 2, 4], c[1, 3, 4], c[2, 3, 4], c[3, 3, 4], c[3, 2, 4], c[3, 1, 4], c[2, 1, 4])
        = (c[3, 1, 4], c[2, 1, 4], c[1, 1, 4], c[1, 2, 4], c[1, 3, 4], c[2, 3, 4], c[3, 3, 4], c[3, 2, 4]);
        // 측면: 뒤 3개 앞으로
        (c[0, 1, 3], c[0, 2, 3], c[0, 3, 3], c[1, 4, 3], c[2, 4, 3], c[3, 4, 3], c[4, 3, 3], c[4, 2, 3], c[4, 1, 3], c[3, 0, 3], c[2, 0, 3], c[1, 0, 3])
        = (c[3, 0, 3], c[2, 0, 3], c[1, 0, 3], c[0, 1, 3], c[0, 2, 3], c[0, 3, 3], c[1, 4, 3], c[2, 4, 3], c[3, 4, 3], c[4, 3, 3], c[4, 2, 3], c[4, 1, 3]);
    }
    else if (t == '+')
    {
        // 정면: 앞 2개 뒤로
        (c[1, 1, 4], c[1, 2, 4], c[1, 3, 4], c[2, 3, 4], c[3, 3, 4], c[3, 2, 4], c[3, 1, 4], c[2, 1, 4])
        = (c[1, 3, 4], c[2, 3, 4], c[3, 3, 4], c[3, 2, 4], c[3, 1, 4], c[2, 1, 4], c[1, 1, 4], c[1, 2, 4]);
        // 측면: 앞 3개 뒤로
        (c[0, 1, 3], c[0, 2, 3], c[0, 3, 3], c[1, 4, 3], c[2, 4, 3], c[3, 4, 3], c[4, 3, 3], c[4, 2, 3], c[4, 1, 3], c[3, 0, 3], c[2, 0, 3], c[1, 0, 3])
        = (c[1, 4, 3], c[2, 4, 3], c[3, 4, 3], c[4, 3, 3], c[4, 2, 3], c[4, 1, 3], c[3, 0, 3], c[2, 0, 3], c[1, 0, 3], c[0, 1, 3], c[0, 2, 3], c[0, 3, 3]);
    }
    else
        throw new Exception("Down");
}

void Left(char[,,] c, char t)
{
    if (t == '+')
    {
        // 정면: 뒤 2개 앞으로
        (c[1, 0, 1], c[2, 0, 1], c[3, 0, 1], c[3, 0, 2], c[3, 0, 3], c[2, 0, 3], c[1, 0, 3], c[1, 0, 2])
        = (c[1, 0, 3], c[1, 0, 2], c[1, 0, 1], c[2, 0, 1], c[3, 0, 1], c[3, 0, 2], c[3, 0, 3], c[2, 0, 3]);
        // 측면: 뒤 3개 앞으로
        (c[1, 1, 0], c[2, 1, 0], c[3, 1, 0], c[4, 1, 1], c[4, 1, 2], c[4, 1, 3], c[3, 1, 4], c[2, 1, 4], c[1, 1, 4], c[0, 1, 3], c[0, 1, 2], c[0, 1, 1])
        = (c[0, 1, 3], c[0, 1, 2], c[0, 1, 1], c[1, 1, 0], c[2, 1, 0], c[3, 1, 0], c[4, 1, 1], c[4, 1, 2], c[4, 1, 3], c[3, 1, 4], c[2, 1, 4], c[1, 1, 4]);
    }
    else if (t == '-')
    {
        // 정면: 앞 2개 뒤로
        (c[1, 0, 1], c[2, 0, 1], c[3, 0, 1], c[3, 0, 2], c[3, 0, 3], c[2, 0, 3], c[1, 0, 3], c[1, 0, 2])
        = (c[3, 0, 1], c[3, 0, 2], c[3, 0, 3], c[2, 0, 3], c[1, 0, 3], c[1, 0, 2], c[1, 0, 1], c[2, 0, 1]); 
        // 측면: 앞 3개 뒤로
        (c[1, 1, 0], c[2, 1, 0], c[3, 1, 0], c[4, 1, 1], c[4, 1, 2], c[4, 1, 3], c[3, 1, 4], c[2, 1, 4], c[1, 1, 4], c[0, 1, 3], c[0, 1, 2], c[0, 1, 1])
        = (c[4, 1, 1], c[4, 1, 2], c[4, 1, 3], c[3, 1, 4], c[2, 1, 4], c[1, 1, 4], c[0, 1, 3], c[0, 1, 2], c[0, 1, 1], c[1, 1, 0], c[2, 1, 0], c[3, 1, 0]);
    }
    else
        throw new Exception("LEFT");
}
void Right(char[,,] c, char t)
{
    if (t == '-')
    {
        // 정면: 뒤 2개 앞으로
        (c[1, 4, 1], c[2, 4, 1], c[3, 4, 1], c[3, 4, 2], c[3, 4, 3], c[2, 4, 3], c[1, 4, 3], c[1, 4, 2])
        = (c[1, 4, 3], c[1, 4, 2], c[1, 4, 1], c[2, 4, 1], c[3, 4, 1], c[3, 4, 2], c[3, 4, 3], c[2, 4, 3]);
        // 측면: 뒤 3개 앞으로
        (c[1, 3, 0], c[2, 3, 0], c[3, 3, 0], c[4, 3, 1], c[4, 3, 2], c[4, 3, 3], c[3, 3, 4], c[2, 3, 4], c[1, 3, 4], c[0, 3, 3], c[0, 3, 2], c[0, 3, 1])
        = (c[0, 3, 3], c[0, 3, 2], c[0, 3, 1], c[1, 3, 0], c[2, 3, 0], c[3, 3, 0], c[4, 3, 1], c[4, 3, 2], c[4, 3, 3], c[3, 3, 4], c[2, 3, 4], c[1, 3, 4]);
    }
    else if (t == '+')
    {
        // 정면: 앞 2개 뒤로
        (c[1, 4, 1], c[2, 4, 1], c[3, 4, 1], c[3, 4, 2], c[3, 4, 3], c[2, 4, 3], c[1, 4, 3], c[1, 4, 2])
        = (c[3, 4, 1], c[3, 4, 2], c[3, 4, 3], c[2, 4, 3], c[1, 4, 3], c[1, 4, 2], c[1, 4, 1], c[2, 4, 1]);
        // 측면: 앞 3개 뒤로
        (c[1, 3, 0], c[2, 3, 0], c[3, 3, 0], c[4, 3, 1], c[4, 3, 2], c[4, 3, 3], c[3, 3, 4], c[2, 3, 4], c[1, 3, 4], c[0, 3, 3], c[0, 3, 2], c[0, 3, 1])
        = (c[4, 3, 1], c[4, 3, 2], c[4, 3, 3], c[3, 3, 4], c[2, 3, 4], c[1, 3, 4], c[0, 3, 3], c[0, 3, 2], c[0, 3, 1], c[1, 3, 0], c[2, 3, 0], c[3, 3, 0]);
    }
    else
        throw new Exception("Right");
}

void Front(char[,,] c, char t)
{
    if (t == '+')
    {
        // 정면: 뒤 2개 앞으로
        (c[4, 1, 1], c[4, 2, 1], c[4, 3, 1], c[4, 3, 2], c[4, 3, 3], c[4, 2, 3], c[4, 1, 3], c[4, 1, 2])
        = (c[4, 1, 3], c[4, 1, 2], c[4, 1, 1], c[4, 2, 1], c[4, 3, 1], c[4, 3, 2], c[4, 3, 3], c[4, 2, 3]);
        // 측면: 뒤 3개 앞으로
        (c[3, 1, 0], c[3, 2, 0], c[3, 3, 0], c[3, 4, 1], c[3, 4, 2], c[3, 4, 3], c[3, 3, 4], c[3, 2, 4], c[3, 1, 4], c[3, 0, 3], c[3, 0, 2], c[3, 0, 1])
        = (c[3, 0, 3], c[3, 0, 2], c[3, 0, 1], c[3, 1, 0], c[3, 2, 0], c[3, 3, 0], c[3, 4, 1], c[3, 4, 2], c[3, 4, 3], c[3, 3, 4], c[3, 2, 4], c[3, 1, 4]);
    }
    else if (t == '-')
    {
        // 정면: 앞 2개 뒤로
        (c[4, 1, 1], c[4, 2, 1], c[4, 3, 1], c[4, 3, 2], c[4, 3, 3], c[4, 2, 3], c[4, 1, 3], c[4, 1, 2])
        = (c[4, 3, 1], c[4, 3, 2], c[4, 3, 3], c[4, 2, 3], c[4, 1, 3], c[4, 1, 2], c[4, 1, 1], c[4, 2, 1]);
        // 측면: 앞 3개 뒤로
        (c[3, 1, 0], c[3, 2, 0], c[3, 3, 0], c[3, 4, 1], c[3, 4, 2], c[3, 4, 3], c[3, 3, 4], c[3, 2, 4], c[3, 1, 4], c[3, 0, 3], c[3, 0, 2], c[3, 0, 1])
        = (c[3, 4, 1], c[3, 4, 2], c[3, 4, 3], c[3, 3, 4], c[3, 2, 4], c[3, 1, 4], c[3, 0, 3], c[3, 0, 2], c[3, 0, 1], c[3, 1, 0], c[3, 2, 0], c[3, 3, 0]);
    }
    else
        throw new Exception("Front");
}
void Back(char[,,] c, char t)
{
    if (t == '-')
    {
        // 정면: 뒤 2개 앞으로
        (c[0, 1, 1], c[0, 2, 1], c[0, 3, 1], c[0, 3, 2], c[0, 3, 3], c[0, 2, 3], c[0, 1, 3], c[0, 1, 2])
        = (c[0, 1, 3], c[0, 1, 2], c[0, 1, 1], c[0, 2, 1], c[0, 3, 1], c[0, 3, 2], c[0, 3, 3], c[0, 2, 3]);
        // 측면: 뒤 3개 앞으로
        (c[1, 1, 0], c[1, 2, 0], c[1, 3, 0], c[1, 4, 1], c[1, 4, 2], c[1, 4, 3], c[1, 3, 4], c[1, 2, 4], c[1, 1, 4], c[1, 0, 3], c[1, 0, 2], c[1, 0, 1])
        = (c[1, 0, 3], c[1, 0, 2], c[1, 0, 1], c[1, 1, 0], c[1, 2, 0], c[1, 3, 0], c[1, 4, 1], c[1, 4, 2], c[1, 4, 3], c[1, 3, 4], c[1, 2, 4], c[1, 1, 4]);
    }
    else if (t == '+')
    {
        // 정면: 앞 2개 뒤로
        (c[0, 1, 1], c[0, 2, 1], c[0, 3, 1], c[0, 3, 2], c[0, 3, 3], c[0, 2, 3], c[0, 1, 3], c[0, 1, 2])
        = (c[0, 3, 1], c[0, 3, 2], c[0, 3, 3], c[0, 2, 3], c[0, 1, 3], c[0, 1, 2], c[0, 1, 1], c[0, 2, 1]);
        // 측면: 앞 3개 뒤로
        (c[1, 1, 0], c[1, 2, 0], c[1, 3, 0], c[1, 4, 1], c[1, 4, 2], c[1, 4, 3], c[1, 3, 4], c[1, 2, 4], c[1, 1, 4], c[1, 0, 3], c[1, 0, 2], c[1, 0, 1])
        = (c[1, 4, 1], c[1, 4, 2], c[1, 4, 3], c[1, 3, 4], c[1, 2, 4], c[1, 1, 4], c[1, 0, 3], c[1, 0, 2], c[1, 0, 1], c[1, 1, 0], c[1, 2, 0], c[1, 3, 0]);
    }
    else
        throw new Exception("Back");
}
#endif
}
