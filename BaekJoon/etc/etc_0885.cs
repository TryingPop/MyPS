using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 18
이름 : 배성훈
내용 : 낚시왕
    문제번호 : 17143번

    구현, 시뮬레이션 문제다
    조건대로 구현했다
    상어 사냥꾼 이동 -> 상어 사냥
    그리고 상어들끼리 이동

    상어들끼리 이동은 동시이동이기에
    이동 전과 이동 후로 맵을 나눠서 했다
    
    그리고 첫 번째 예제를 보니, 상어 사냥꾼이 
    오른쪽 끝에 도달할 때 점수를 세었으므로 똑같이 끝에 도달하면 끝냈다
    
    이렇게 제출하니 이상없이 통과했다
    
    방향 전환을 구현하는데 시간이 많이 걸렸다
    col 이동으로 3개 예제로 확인하니
    주기는 2 * (col - 1) 이고 col - 1 보가 큰 경우 방향이 전환되었다
    row 역시 주기만 2 * (row - 1)로 바뀌는거라 유사하게 계산하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0885
    {

        static void Main885(string[] args)
        {

            StreamReader sr;
            int row, col, n;

            (int r, int c, int speed, int dir, int size)[] shark;
            int[][][] pool;
            int[] dirR, dirC;
            bool[] isDead;
            int cur;
            int ret;
            Solve();
            void Solve() 
            {

                Input();

                for (int c = 0; c < col; c++)
                {

                    KillShark();
                    SharkMove();
                }

                Console.Write(ret);
            }

            void KillShark()
            {

                for (int r = 0; r < row; r++)
                {

                    int chk = pool[0][r][cur];
                    if (chk == 0) continue;
                    pool[0][r][cur] = 0;
                    ret += chk;
                    isDead[chk] = true;
                    break;
                }

                cur = cur == col - 1 ? cur : cur + 1;
            }

            void SharkMove()
            {

                for (int i = 0; i < n; i++)
                {

                    if (isDead[shark[i].size]) continue;
                    pool[0][shark[i].r][shark[i].c] = 0;

                    int dir = shark[i].dir;
                    if (dir < 2)
                    {

                        int nextR = shark[i].r + dirR[dir] * shark[i].speed;
                        nextR %= 2 * (row - 1);
                        if (nextR < 0) nextR += 2 * (row - 1);

                        if (nextR < row - 1) shark[i].r = nextR;
                        else
                        {

                            shark[i].dir = dir == 0 ? 1 : 0;
                            shark[i].r = 2 * (row - 1) - nextR;
                        }
                    }
                    else
                    {

                        int nextC = shark[i].c + dirC[dir] * shark[i].speed;
                        nextC %= 2 * (col - 1);

                        if (nextC < 0) nextC += 2 * (col - 1);

                        if (nextC < col - 1)
                        {

                            shark[i].c = nextC;
                        }
                        else
                        {

                            shark[i].dir = dir == 2 ? 3 : 2;
                            shark[i].c = 2 * (col - 1) - nextC;
                        }
                    }

                    int chk = pool[1][shark[i].r][shark[i].c];
                    if (chk != 0)
                    {

                        if (shark[i].size < chk) isDead[shark[i].size] = true;
                        else 
                        { 
                            
                            isDead[chk] = true;
                            pool[1][shark[i].r][shark[i].c] = shark[i].size;
                        }
                    }
                    else pool[1][shark[i].r][shark[i].c] = shark[i].size;
                }

                int[][] temp = pool[0];
                pool[0] = pool[1];
                pool[1] = temp;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();
                n = ReadInt();

                pool = new int[2][][];
                for (int i = 0; i < 2; i++)
                {

                    pool[i] = new int[row][];
                    for (int j = 0; j < row; j++)
                    {

                        pool[i][j] = new int[col];
                    }
                }

                shark = new (int r, int c, int speed, int dir, int size)[n];
                for (int i = 0; i < n; i++)
                {

                    shark[i] = (ReadInt() - 1, ReadInt() - 1, ReadInt(), ReadInt() - 1, ReadInt());
                    if (shark[i].dir < 2)
                        shark[i].speed %= 2 * (row - 1);
                    else
                        shark[i].speed %= 2 * (col - 1);
                    pool[0][shark[i].r][shark[i].c] = shark[i].size;
                }

                cur = 0;
                dirR = new int[4] { -1, 1, 0, 0 };
                dirC = new int[4] { 0, 0, 1, -1 };
                isDead = new bool[10_001];
                ret = 0;
                sr.Close();
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

StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

int[] inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
int R = inputs[0];
int C = inputs[1];
int M = inputs[2];
List<Shark> sharks = new List<Shark>();

(int r, int c)[] finder = { (-1, 0), (1, 0), (0, 1), (0, -1) };
int[,] field = new int[R + 2, C + 2];
for (int i = 0; i <= R + 1; i++)
    for (int j = 0; j <= C + 1; j++)
        field[i, j] = -1;

for (int i = 1; i <= R; i++)
    for (int j = 1; j <= C; j++)
        field[i, j] = 0;

int[,] emptyField = (int[,])field.Clone();
sharks.Add(null);

for(int i=1; i<=M; i++)
{
    inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
    sharks.Add(new Shark(inputs));
    field[inputs[0], inputs[1]] = i;
}
int ans = 0;

for(int i=1; i<=C; i++)
{
    for(int j=1; j<=R; j++)
    {
        if(field[j,i] != 0)
        {
            ans += sharks[field[j, i]].z;
            field[j, i] = 0;
            break;
        }
    }

    int[,] nextField = (int[,])emptyField.Clone();
    for(int j=1; j<=R; j++)
    {
        for(int k=1; k<=C; k++)
        {
            if (field[j, k] == 0)
                continue;
            int sharkNum = field[j, k];
            int moveLeft = sharks[sharkNum].s;

            int width = (sharks[sharkNum].d <= 2) ? R: C;
            int pos = (sharks[sharkNum].d <= 2) ? j : k;
            bool positive = (sharks[sharkNum].d % 3 != 1) ? true : false;
            var nextPos = nextPosition(width, pos, positive, sharks[sharkNum].s);
            if (sharks[sharkNum].d <= 2)
            {
                int nextShark = nextField[nextPos.Item1, k];
                if(nextShark == 0 || sharks[nextShark].z < sharks[sharkNum].z)
                    nextField[nextPos.Item1, k] = sharkNum;
            }
            else
            {
                int nextShark = nextField[j, nextPos.Item1];
                if (nextShark == 0 || sharks[nextShark].z < sharks[sharkNum].z)
                    nextField[j, nextPos.Item1] = sharkNum;
            }
            if (sharks[sharkNum].d <= 2)
                sharks[sharkNum].d = 1 + (nextPos.Item2 ? 1 : 0);
            else
                sharks[sharkNum].d = 4 - (nextPos.Item2 ? 1 : 0);
        }
    }
    field = nextField;
}

sw.WriteLine(ans);


sr.Close();
sw.Close();


(int, bool) nextPosition(int width, int pos, bool positive, int speed)
{
    int curPos = (width - 1) + (pos - 1) * (positive ? 1 : -1);
    curPos += speed;
    curPos %= (width - 1) * 2;
    curPos -= (width - 1);
    return (Math.Abs(curPos) + 1, (curPos >= 0) ? true: false);
}

class Shark
{
    public int s, d, z;
    public Shark(int[] sharkInput)
    {
        this.s = sharkInput[2];
        this.d = sharkInput[3];
        this.z = sharkInput[4];
    }
}
#elif other2
using System;
using System.IO;

namespace _17143_낚시왕
{
    class Program
    {
        static int[] dx = { 0, 0, 0, 1, -1 };
        static int[] dy = { 0, -1, 1, 0, 0 };

        static int[,] map;

        static int R, C, M;
        static int[,] shark;
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());

            string[] input = sr.ReadLine().Split(' ');
            R = int.Parse(input[0]);
            C = int.Parse(input[1]);
            M = int.Parse(input[2]);

            map = new int[R, C];
            shark = new int[M + 1, 5];

            for (int i = 1; i <= M; i++)
            {
                string[] temp = sr.ReadLine().Split(' ');
                shark[i, 0] = int.Parse(temp[0]) - 1;
                shark[i, 1] = int.Parse(temp[1]) - 1;
                shark[i, 2] = int.Parse(temp[2]);
                shark[i, 3] = int.Parse(temp[3]);
                shark[i, 4] = int.Parse(temp[4]);
                map[shark[i, 0], shark[i, 1]] = i;
            }

            Console.WriteLine(Solves());
        }

        static int Solves()
        {
            //1. 낚시왕이 한칸씩 움직인다.(C번동안)
            //2. 낚시왕이 같은 열에 있는 상어중에 가장 y값이 작은값을 잡는다
            //3. 상어가 이동한다.
            int result = 0;
            for(int i = 0; i < C; i++)
            {
                result += Fishing(i);
                Move();
            }
            return result;
        }

        static int Fishing(int x)
        {
            for(int y = 0; y < R; y++)
            {
                if(map[y,x] > 0)
                {
                    int sharkNum = map[y, x];
                    shark[sharkNum, 0] = -1;
                    map[y, x] = 0; //물고기를 잡아서 그 위치를 비움
                    return shark[sharkNum, 4];
                }
            }
            return 0;
        }

        static void Move()
        {
            for (int i = 1; i <= M; i++)
            {
                if (shark[i, 0] != -1)
                {
                    SharkMove(i);
                }
            }
            Eat();
        }

        static void SharkMove(int sharkNum)
        {
            int state = shark[sharkNum, 3];
            int x = shark[sharkNum, 1];
            int y = shark[sharkNum, 0];
            int s = shark[sharkNum, 2];

            if (state == 1 || state == 2) //상하
            {
                int length = R + (R - 2);
                if(state == 1)//상
                {
                    int s_temp = s - y;
                    if(s_temp < 0)
                    {
                        y = y - s;
                    }
                    else
                    {
                        int nowY = s_temp % length;
                        if (nowY >= R) // 방향이 위로
                        {
                            y = (R - 1) - (nowY % (R - 1));
                        }
                        else //방향이 아래로
                        {
                            state = 2;
                            y = nowY;
                        }
                    }
                }
                else//하
                {
                    int nowY = (s + y) % length;
                    if(nowY >= R)
                    {
                        state = 1;
                        y = (R - 1) - (nowY % (R - 1));

                    }
                    else
                    {
                        y = nowY;
                    }
                }
            }
            else //좌우
            {
                int length = C + (C - 2);
                if (state == 4)//좌
                {
                    int s_temp = s - x;
                    if (s_temp < 0)
                    {
                        x = x - s;
                    }
                    else
                    {
                        int nowX = s_temp % length;
                        if (nowX >= C) // 방향이 좌로
                        {
                            x = (C - 1) - (nowX % (C - 1));
                        }
                        else //방향이 우래로
                        {
                            state = 3;
                            x = nowX;
                        }
                    }
                }
                else//좌
                {
                    int nowX = (s + x) % length;
                    if (nowX >= C)
                    {
                        state = 4;
                        x = (C - 1) - (nowX % (C - 1));
                    }
                    else
                    {
                        x = nowX;
                    }
                }
            }
            map[shark[sharkNum, 0], shark[sharkNum, 1]] = 0; //원래 자리 0으로 만듬
            shark[sharkNum, 0] = y;
            shark[sharkNum, 1] = x;
            shark[sharkNum, 3] = state;
            return;
        }
        static void Eat()
        {
            for (int i = 1; i <= M; i++)
            {
                if (shark[i, 0] != -1)
                {
                    int x = shark[i, 1];
                    int y = shark[i, 0];
                    if (map[y, x] == 0)
                    {
                        map[y, x] = i;
                    }
                    else
                    {
                        if (shark[map[y, x], 4] > shark[i, 4])
                        {
                            shark[i, 0] = -1;
                        }
                        else
                        {
                            shark[map[y, x], 0] = -1;
                            map[y, x] = i;
                        }
                    }
                }
            }
        }
    }
}

#endif
}
