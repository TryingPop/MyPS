using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 8
이름 : 배성훈
내용 : IOIOI
    문제번호 : 5525번

    문자열 문제다
    IOIOIO... 의 반복성되기에
    길이로 갯수를 확인할 수 있다

    그래서 길이를 잰 뒤에 
    연산으로 개수를 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0952
    {

        static void Main952(string[] args)
        {

            StreamReader sr;
            int n, m;
            int len;
            Solve();
            void Solve()
            {

                Input();

                int ret = 0;
                int match = 0;
                bool cur = true;
                for (int i = 0; i < m; i++)
                {

                    int c = sr.Read();
                    if (cur && c == 'I')
                    {

                        cur = false;
                        match++;
                        continue;
                    }
                    else if (!cur && c == 'O')
                    {

                        cur = true;
                        match++;
                        continue;
                    }

                    match -= len;
                    if (match > 0) ret += match / 2;
                    cur = !(c == 'I');
                    match = c == 'I' ? 1 : 0;
                }

                match -= len;
                if (match > 0) ret += match / 2;

                Console.Write(ret);
                sr.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                len = 2 * n - 1;
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
using System;
using System.IO;

var sr = new StreamReader(new BufferedStream(
    Console.OpenStandardInput()));

var n = ScanInt(sr);
_ = ScanInt(sr);
var s = sr.ReadLine()!;
sr.Close();
var ret = 0;

var curNum = -1;
var oMet = false;
foreach (var c in s)
{
    if (c == 'I')
    {
        if (oMet)
        {
            if (++curNum >= n)
                ret++;
        }
        else
            curNum = 0;
        oMet = false;
    }
    else
    {
        if (oMet)
        {
            curNum = -1;
            oMet = false;
        }
        else
            oMet = true;
    }
}
Console.Write(ret);

static int ScanInt(StreamReader sr)
{
    int c, ret = 0;
    while ((c = sr.Read()) != ' ' && c != '\n' && c != -1)
    {
        if (c == '\r')
        {
            sr.Read();
            break;
        }
        ret = 10 * ret + (c - '0');
    }
    return ret;
}
#endif
}
