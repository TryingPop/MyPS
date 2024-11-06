/*
날짜 : 2024. 6. 2
이름 : 배성훈
내용 : 틱택토
    문제번호 : 7682번

    구현 문제다
    게임 진행에서 나타날 수 있는 모든 경우는 3^9 < 20_000 이므로 모든 가능한 경우를 찾아서
    끝나는 경우만 기록했다 이후 상황이 주어지면 해당 상황으로 만들고 바로 결과를 내게 했다

    보드를 모두 채운 경우도 끝난 경우로 해줘야하는데 이를 안해줘서 1번 틀렸다;
    이후에 68ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0750
    {

        static void Main750(string[] args)
        {

            string END = "end";

            string YES = "valid\n";
            string NO = "invalid\n";

            StreamReader sr;
            StreamWriter sw;

            bool[] valid;
            Queue<(int n, bool isX)> q;
            int[] arr;

            Solve();
            void Solve()
            {

                Find();

                sr = new(Console.OpenStandardInput());
                sw = new(Console.OpenStandardOutput());

                while (true)
                {

                    string temp = sr.ReadLine();
                    if (temp == END) break;

                    int chk = StrToInt(temp);
                    if (valid[chk]) sw.Write(YES);
                    else sw.Write(NO);
                }

                sr.Close();
                sw.Close();
            }

            void Find()
            {

                // 3^9
                int len = 243 * 81;

                bool[] visit = new bool[len];
                valid = new bool[len];
                arr = new int[9];

                q = new(len);

                q.Enqueue((0, true));

                while(q.Count > 0)
                {

                    var cur = q.Dequeue();

                    IntToArr(cur.n);

                    for (int i = 0; i < 9; i++)
                    {

                        if (arr[i] != 0) continue;
                        arr[i] = cur.isX ? 1 : 2;

                        int next = ArrToInt();
                        if (!visit[next])
                        {

                            visit[next] = true;
                            if (IsEnd()) valid[next] = true;
                            else q.Enqueue((next, !cur.isX));
                        }

                        arr[i] = 0;
                    }
                }
            }

            bool IsEnd()
            {

                for (int i = 0; i < 3; i++)
                {

                    // 가로
                    if (arr[i * 3] != 0 && arr[i * 3] == arr[i * 3 + 1] && arr[i * 3] == arr[i * 3 + 2]) return true;

                    // 세로
                    if (arr[i] != 0 && arr[i] == arr[i + 3] && arr[i] == arr[i + 6]) return true;
                }

                // 대각
                if (arr[0] != 0 && arr[0] == arr[4] && arr[0] == arr[8]) return true;
                if (arr[2] != 0 && arr[2] == arr[4] && arr[2] == arr[6]) return true;

                for (int i = 0; i < 9; i++)
                {

                    if (arr[i] == 0) return false;
                }

                return true;
            }

            void IntToArr(int _n)
            {

                for (int i = 8; i >= 0; i--)
                {

                    arr[i] = _n % 3;
                    _n /= 3;
                }
            }

            int ArrToInt()
            {

                int ret = 0;
                for (int i = 0; i < 9; i++)
                {

                    ret = ret * 3 + arr[i];
                }

                return ret;
            }

            int StrToInt(string _str)
            {

                int ret = 0;
                for (int i = 0; i < _str.Length; i++)
                {

                    int cur = _str[i];
                    int add;
                    if (cur == '.') add = 0;
                    else if (cur == 'X') add = 1;
                    else add = 2;

                    ret = ret * 3 + add;
                }

                return ret;
            }
        }
    }

#if other
// BOJ_7682


using System.Text;

int[] dx = { 1, 0, -1, 0, 1, -1 };
int[] dy = { 0, 1, 0, -1, 1, 1 };
char[,] board = new char[3, 3];
StringBuilder sb = new StringBuilder();
while (true)
{
    string input = Console.ReadLine();
    if (input == "end")
        break;

    int xcount = 0;
    int ocount = 0;
    for (int i = 0; i < 9; i++)
    {
        if (input[i] == 'X')
            xcount++;
        else if (input[i] == 'O')
            ocount++;
    }

    if (xcount - ocount > 1 || xcount - ocount < 0)
    {
        sb.AppendLine("invalid");
        continue;
    }

    board = new char[3, 3];
    bool check = false;
    for (int i = 0; i < 3; i++)
    {
        for (int j = i * 3; j < (i + 1) * 3; j++)
        {
            board[i, j % 3] = input[j];
        }
    }

    for (int i = 0; i < 3; i++)
    {
        if ((IsSuccess('O', 0, i) || IsSuccess('O', i, 0)) && xcount == ocount)
            check = true;
    }

    if (check)
        sb.AppendLine("valid");
    else
    {
        bool oSuccess = false;
        bool xSuccess = false;
        for (int i = 0; i < 3; i++)
        {
            if ((IsSuccess('X', 0, i) || IsSuccess('X', i, 0)) && xcount > ocount)
                xSuccess = true;
            if (IsSuccess('O', 0, i) || IsSuccess('O', i, 0))
                oSuccess = true;
        }

        if (xSuccess && !oSuccess)
            sb.AppendLine("valid");

        else
        {
            check = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '.')
                        check = false;
                }
            }

            if (check && !xSuccess && !oSuccess)
                sb.AppendLine("valid");
            else
                sb.AppendLine("invalid");
        }
    }
}

Console.WriteLine(sb);

bool IsSuccess(char target, int x, int y)
{
    if (board[x, y] == '.' || board[x, y] != target)
        return false;
    for (int i = 0; i < 6; i++)
    {
        int dir = i;
        int count = 1;
        int curx = x;
        int cury = y;
        for (int j = 0; j < 3; j++)
        {
            int nx = curx + dx[dir];
            int ny = cury + dy[dir];
            if (nx < 0 || ny < 0 || nx >= 3 || ny >= 3)
                break;
            if (board[nx, ny] == target)
                count++;
            curx = nx;
            cury = ny;
        }

        if (count == 3)
            return true;
    }
    return false;
}
#elif other2
namespace boj_7682
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new(Console.OpenStandardInput());
            StreamWriter sw = new(Console.OpenStandardOutput());

            while (true)
            {
                string result = sr.ReadLine().TrimEnd();

                if (result == "end")
                {
                    sw.Close();
                    break;
                }
                    

                int xcount = 0;
                int ocount = 0;

                foreach (char c in result)
                {
                    if (c == 'X')
                        xcount++;

                    else if (c == 'O')
                        ocount++;
                }

                bool xwin = false;
                bool owin = false;

                for (int i = 0; i < 3; i++)
                {
                    if (string.Join("", result[i], result[i + 3], result[i + 6]) == "XXX")
                        xwin = true;

                    else if (string.Join("", result[i], result[i + 3], result[i + 6]) == "OOO")
                        owin = true;

                    if (string.Join("", result[i * 3], result[i * 3 + 1], result[i * 3 + 2]) == "XXX")
                        xwin = true;

                    else if (string.Join("", result[i * 3], result[i * 3 + 1], result[i * 3 + 2]) == "OOO")
                        owin = true;
                }

                if (string.Join("", result[0], result[4], result[8]) == "XXX")
                    xwin = true;

                else if (string.Join("", result[2], result[4], result[6]) == "XXX")
                    xwin = true;

                if (string.Join("", result[0], result[4], result[8]) == "OOO")
                    owin = true;

                else if (string.Join("", result[2], result[4], result[6]) == "OOO")
                    owin = true;


                if (xwin && owin)
                    sw.WriteLine("invalid");

                else if (xwin)
                {
                    if (xcount == ocount + 1)
                        sw.WriteLine("valid");
                    else
                        sw.WriteLine("invalid");
                }

                else if (owin)
                {
                    if (xcount == ocount)
                        sw.WriteLine("valid");
                    else
                        sw.WriteLine("invalid");
                }

                else
                {
                    if (xcount + ocount == 9 && xcount == ocount + 1)
                        sw.WriteLine("valid");
                    else
                        sw.WriteLine("invalid");
                }
            }
        }
    }
}
#elif other3
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            StringBuilder sb = new StringBuilder();
            string s = input.ReadLine();
            while (s != "end")
            {
                if (check(s))
                    sb.Append("valid\n");
                else
                    sb.Append("invalid\n");

                s = input.ReadLine();
            }
            output.Write(sb);            

            input.Close();
            output.Close();
        }
        static bool check(string s)
        {
            (int oc, int xc, bool os, bool xs) = cal(s);

            if (!os && !xs && oc + xc < 9) return false;
            if(xc < oc || xc - oc >= 2) return false;
            if(os && xc != oc) return false;
            if(xs && xc != oc + 1) return false;
            return true;
        }
        static (int,int,bool,bool) cal(string s)
        {
            int ocount = 0;
            int xcount = 0;
            bool os = false;
            bool xs = false;
            for (int i = 0; i < 3; i++)
            {
                int or = 0; int oc = 0;
                int xr = 0; int xc = 0; 
                for (int j = 0; j < 3; j++)
                {
                    if (s[i * 3 + j] == 'O') // 가로 확인
                        or++;
                    else if (s[i * 3 + j] == 'X')
                        xr++;
                    if (s[j * 3 + i] == 'O') // 세로 확인
                        oc++;
                    else if (s[j * 3 + i] == 'X')
                        xc++;
                }
                ocount += or;
                xcount += xr;
                if (or == 3 || oc == 3)
                    os = true;
                if (xr == 3 || xc == 3)
                    xs = true;
            }
            if (s[0] == s[4] && s[4] == s[8])
            {
                if (s[0] == 'O') os = true;
                else if (s[0] == 'X') xs = true;
            }
            if (s[2] == s[4] && s[4] == s[6])
            {
                if (s[2] == 'O') os = true;
                else if (s[2] == 'X') xs = true;
            }
            return (ocount, xcount, os, xs);
        }
    }
}
#endif
}
