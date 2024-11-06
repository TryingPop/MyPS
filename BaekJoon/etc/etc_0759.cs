using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 18
이름 : 배성훈
내용 : A와 B 2
    문제번호 : 12919번

    문자열, 브루트포스, 재귀 문제다

    가지치기 하면서 DFS 탐색으로 해결했다
    A에서 B를 만들려고 하다보니 시간초과가 났고,
    질문게시판을 보니 반대로 하면 된다는 조언글이 있어
    B에서 A를 만드는 식으로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0759
    {

        static void Main759(string[] args)
        {


            string strA, strB;
            int chkA, chkB;
            Solve();

            void Solve()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                strA = sr.ReadLine();
                strB = sr.ReadLine();
                sr.Close();

                chkA = 0;
                chkB = 0;
                for (int i = 0; i < strA.Length; i++)
                {

                    if (strA[i] == 'A') chkA++;
                    else chkB++;
                }

                int a = 0;
                int b = 0;
                for (int i = 0; i < strB.Length; i++)
                {

                    if (strB[i] == 'A') a++;
                    else b++;
                }

                bool ret = DFS(strB.Length - strA.Length, a, b, 0, strB.Length - 1, false);

                Console.Write(ret ? 1 : 0);
            }

            bool DFS(int _depth, int _a, int _b, int _s, int _e, bool _inv)
            {

                bool ret = false;

                if (_a < chkA || _b < chkB) return false;
                if (_depth == 0)
                {

                    ret = true;
                    if (_inv)
                    {

                        for (int i = 0; i < strA.Length; i++)
                        {

                            if (strA[i] == strB[_e - i]) continue;
                            return false;
                        }
                    }
                    else
                    {

                        for (int i = 0; i <strA.Length; i++)
                        {

                            if (strA[i] == strB[_s + i]) continue;
                            return false;
                        }
                    }

                    return ret;
                }

                int bIdx = _inv ? _s : _e;
                if (strB[bIdx] == 'A')
                {

                    if (_inv) ret = DFS(_depth - 1, _a - 1, _b, _s + 1, _e, _inv);
                    else ret = DFS(_depth - 1, _a - 1, _b, _s, _e - 1, _inv);

                    if (ret) return true;
                }

                int fIdx = _inv ? _e : _s;
                if (strB[fIdx] == 'B')
                {

                    if (_inv) ret = DFS(_depth - 1, _a, _b - 1, _s, _e - 1, !_inv);
                    else ret = DFS(_depth - 1, _a, _b - 1, _s + 1, _e, !_inv);
                }

                return ret;
            }
        }
    }

#if other
string s = IO.NextString(50), t = IO.NextString(50);

var buf = new char[50];
int res = 0;
var stack = new Stack<string>();
stack.Push(t);
while (stack.Count > 0)
{
    var cur = stack.Pop();

    if (cur.Length == s.Length)
    {
        if (cur == s)
        {
            res = 1;
            break;
        }
        else
            continue;
    }

    if (cur[^1] == 'A')
        stack.Push(cur[..^1]);

    if (cur[0] == 'B')
    {
        for (int i = 1; i < cur.Length; i++)
            buf[i - 1] = cur[^i];
        
        stack.Push(new string(buf, 0, cur.Length - 1));
    }
}

IO.WriteLine(res);

IO.Close();

static class IO
{
    static StreamReader R=new(new BufferedStream(Console.OpenStandardInput(),1024000));
    static StreamWriter W=new(new BufferedStream(Console.OpenStandardOutput(),1024000));
    public static void Close(){R.Close();W.Close();}
    public static void WriteLine(object s)=>W.WriteLine(s);
    public static string NextString(int m){var(r,l,v)=(false,0,new char[m]);while(true){int c=R.Read();if(!r&&(c==' '||c=='\n'||c=='\r'))continue;if(r&&(l==m||c==' '||c=='\n'||c=='\r'))break;v[l++]=(char)c;r=true;}return new(v,0,l);}
    
} 
#elif other2
using System;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main(string[] args)
        {
            string originalString = Console.ReadLine().Trim();
            string targetString = Console.ReadLine();

            if (IsConvertible(originalString, targetString))
            {
                Console.WriteLine(1);
            }
            else
            {
                Console.WriteLine(0);
            }
        }

        static bool IsConvertible(string originalString, string targetString)
        {
            int originalStringLength = originalString.Length;

            if (targetString.Length == originalStringLength)
            {
                if (targetString.Equals(originalString))
                {
                    return true;
                }
                return false;
            }

            if (targetString[targetString.Length - 1] == 'A')
            {
                if (IsConvertible(originalString, targetString.Substring(0, targetString.Length - 1)))
                {
                    return true;
                }
            }

            if (targetString[0] == 'B')
            {
                StringBuilder reversedString = new StringBuilder();
                reversedString.Append(targetString.Substring(1, targetString.Length - 1));
                if (IsConvertible(originalString, ReverseString(reversedString.ToString())))
                {
                    return true;
                }
            }
            return false;
        }

        static string ReverseString(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
#endif
}
