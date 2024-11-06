using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 13
이름 : 배성훈
내용 : 알파벳
    문제번호 : 1987번

    DFS, 백트래킹 문제다
    처음에는 조건대로 그냥 방문 여부와 문자 사용여부를 함께 기록하며 진행하니 1024ms로 통과했다
    그런데, 방문 여부를 따로 안하고 문자 사용여부만해도 될거 같아 제출하니 740ms로 줄었다    
*/

namespace BaekJoon.etc
{
    internal class etc_0525
    {

        static void Main525(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int[] info = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            int[,] board = new int[info[0], info[1]];

            for (int i = 0; i < info[0]; i++)
            {

                for (int j = 0; j < info[1]; j++)
                {

                    board[i, j] = sr.Read() - 'A';
                }

                sr.ReadLine();
            }

            sr.Close();
            
            int use = 1 << board[0, 0];
            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };

            int ret = DFS(0, 0);
            Console.WriteLine(ret);

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= info[0] || _c >= info[1]) return true;
                return false;
            }

            int DFS(int _r, int _c)
            {

                int ret = 0;
                for (int i = 0; i < 4; i++)
                {

                    int nextR = _r + dirR[i];
                    int nextC = _c + dirC[i];

                    if (ChkInvalidPos(nextR, nextC)) continue;
                    int save = board[nextR, nextC];
                    if ((use & (1 << save)) != 0) continue;
                    use |= 1 << save;
                    int calc = DFS(nextR, nextC);
                    ret = ret < calc ? calc : ret;
                    use ^= 1 << save;
                }

                return ret + 1;
            }
        }
    }

#if other
var rc = Console.ReadLine().Split();
int R = int.Parse(rc[0]);
int C = int.Parse(rc[1]);

var board = new string[R];
for (int i = 0; i < R; i++)
    board[i] = Console.ReadLine();

var occured = new bool[26];
int max = 0;
Traverse(0, 0, occured, 1);

Console.Write(max);

void Traverse(int r, int c, bool[] occured, int count)
{
    int curChar = board[r][c] - 'A';
    occured[curChar] = true;

    if (r - 1 >= 0 && !occured[board[r - 1][c] - 'A'])
        Traverse(r - 1, c, occured, count + 1);

    if (r + 1 < R && !occured[board[r + 1][c] - 'A'])
        Traverse(r + 1, c, occured, count + 1);

    if (c - 1 >= 0 && !occured[board[r][c - 1] - 'A'])
        Traverse(r, c - 1, occured, count + 1);

    if (c + 1 < C && !occured[board[r][c + 1] - 'A'])
        Traverse(r, c + 1, occured, count + 1);

    max = Math.Max(max, count);
    occured[curChar] = false;
}
#elif other2
StreamReader rd = new StreamReader(Console.OpenStandardInput());
StreamWriter wr = new StreamWriter(Console.OpenStandardOutput());

int[] input = Array.ConvertAll(rd.ReadLine().Split(' '), int.Parse);
int row_num = input[0];
int column_num = input[1];
List<string> map = new();
bool[] dic = new bool[26];


for (int i = 0; i < row_num; i++)
{
    map.Add(rd.ReadLine());
}
int count = 1;
int max = 0;

dic[map[0][0] - 65] = true;
search(0, 0, count);


wr.WriteLine(max);
wr.Close();
void search(int x, int y, int count)
{
    if(x < row_num  || y < column_num)
    {
        if (x + 1 < row_num && !dic[map[x + 1][y] - 65])
        {
            dic[map[x + 1][y] - 65] = true;
            search(x + 1, y, count + 1);
        }
        if (y + 1 < column_num && !dic[map[x][y + 1] - 65])
        {
            dic[map[x][y + 1] - 65] =  true;
            search(x, y + 1, count + 1);
        }
        if (y - 1 >= 0 && !dic[map[x][y - 1] - 65])
        {
            dic[map[x][y - 1] - 65] = true;
            search(x, y - 1, count + 1);
        }
        if (x - 1 >= 0 && !dic[map[x - 1][y] - 65])
        {
            dic[map[x - 1][y] - 65] = true;
            search(x - 1, y, count + 1);
        }
    }
    dic[map[x][y] - 65] = false;
    max = Math.Max(count, max);
}
#endif
}
