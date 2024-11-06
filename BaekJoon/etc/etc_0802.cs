using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 9 
이름 : 배성훈
내용 : 옥상 정원 꾸미기
    문제번호 : 6198번

    스택 문제다
    자기보다 큰 것만 자신을 볼 수 있다
    그리고 더 큰 빌딩으로 가려지면 자기보다 큰 경우라도 못본다

    그리고 a아파트가 볼 수 있는 옥상의 개수를 찾는게 아닌
    b 옥상을 볼 수 있는 아파트의 개수를 찾는 식으로 가면
    전형적인 스택문제가 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0802
    {

        static void Main802(string[] args)
        {

            StreamReader sr;

            int[] arr;
            int n;
            Stack<int> s;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                s = new(n);
                long ret = 0;
                for (int i = 0; i < n; i++)
                {

                    while(s.Count > 0 && s.Peek() <= arr[i])
                    {

                        s.Pop();
                    }

                    ret += s.Count;
                    s.Push(arr[i]);
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
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
            int n = int.Parse(input.ReadLine());
            Stack<int> stack = new();
            long answer = 0;
            for(int i = 0; i < n; i++)
            {
                int num = int.Parse(input.ReadLine());

                while(stack.Count > 0 && stack.Peek() <= num)
                {
                    stack.Pop();
                    answer += stack.Count;
                }
                stack.Push(num);
            }
            while (stack.Count > 0)
            {
                stack.Pop();
                answer += stack.Count;
            }

            output.Write(answer);

            input.Close();
            output.Close();
        }
    }
}
#elif other2
// #include <cstdio>
using namespace std;
// #define FOR(val, start, end) for(int val=(start); val<(end); ++val)

constexpr int SZ = 1<<19;
char *bp, buf[SZ];

inline char ReadChar() {
    if(bp == buf+SZ) fread(bp = buf, sizeof(char), SZ, stdin);
    return *bp++;
}
inline int ReadInt() {
    int ret = 0;
    for (char c = ReadChar(); c&16; ret = (ret<<1) + (ret<<3) + (c&15), c = ReadChar());
    return ret;
}

int H[80000], n;
long long ans;

inline int view_cnt(int idx) {
    int cnt = 0, tmp;
    FOR(i, idx+1, n) {
        if(H[i] >= H[idx]) break;
        tmp = view_cnt(i);
        cnt += tmp+1;
        i += tmp;
    }
    ans += cnt;
    return cnt;
}

int main() {
    //input
    fread(buf, sizeof(char), SZ, stdin); bp = buf;
    n = ReadInt();
    FOR(i, 0, n) H[i] = ReadInt();

    //func
    FOR(i, 0, n) {
        i += view_cnt(i);
    }
    printf("%lld\n", ans);
	return 0;
}
#endif
}
