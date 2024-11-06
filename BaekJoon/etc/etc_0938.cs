using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 3
이름 : 배성훈
내용 : 유클리드 게임
    문제번호 : 4342번

    아이디어는 다음과 같다
    우선 a % b == 0이면, 해당턴에 바로 0을 만들 수 있어
    그 턴을 실행하는 사람이 무조건 이긴다
    
    먼저 a < 2 x b인 경우 b만 뺄 수 있다
    이는 시행이 고정된 것이므로 강제 진행하는 수 밖에 없다

    반면 a > 2 x b인 경우
    이는 해당 턴 유저가 승패 경우를 조절할 수 있다
    a > k x b인 k의 최대값을 찾자
    그러면 a - k x b = m이 된다
    만약 a, b로 시작하는 플레이어가 이기는 경우면
    그냥 최대값을 넣고 진행하면 된다
    반면 지는 경우면 b에 k - 1을 곱해 넘겨주면, 
    b + m, b가 되므로, 상대는 강제 진행할 수 밖에 없고
    이는 a, b를 상대 플레이어가 진행한 것으로 넘길 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0938
    {

        static void Main938(string[] args)
        {

            string A = "A wins\n";
            string B = "B wins\n";

            StreamReader sr;
            StreamWriter sw;

            int a, b;

            Solve();
            void Solve()
            {

                Init();

                while(Input())
                {

                    int turn = 0;
                    while (true)
                    {

                        if (a % b == 0 || a - b > b) break;
                        int temp = a % b;
                        a = b;
                        b = temp;
                        turn = turn == 1 ? 0 : 1;
                    }

                    if (turn == 0) sw.Write(A);
                    else sw.Write(B);
                }

                sr.Close();
                sw.Close();
            }

            bool Input()
            {

                a = ReadInt();
                b = ReadInt();

                if (a < b)
                {

                    int temp = a;
                    a = b;
                    b = temp;
                }

                return a != 0 || b != 0;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
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
// #include<cstdio>

int main(){
	int a,b,t,cnt;
	while(1){
		cnt=0;
		scanf("%d %d",&a,&b);
		if(!a)break;
		if(b>a){t=b;b=a;a=t;}
		while(b){
			cnt++;
			if(a-b>=b)break;
			t=a%b;a=b;b=t;
		}
		printf("%c wins\n",cnt&1?'A':'B');
	}
}
#elif other2
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var isWinningMove = new Dictionary<(int big, int smol), bool>();
        while (true)
        {
            var ab = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var big = ab.Max();
            var smol = ab.Min();

            if (big == 0 && smol == 0)
                break;

            var w = IsWinning(isWinningMove, big, smol);
            sw.WriteLine(w ? "A wins" : "B wins");
        }
    }

    private static bool IsWinning(Dictionary<(int big, int smol), bool> memo, int big, int smol)
    {
        if (memo.ContainsKey((big, smol)))
            return memo[(big, smol)];

        if (smol == 0)
            return false;

        var rem = big / smol;
        if (rem == 1)
        {
            memo[(big, smol)] = !IsWinning(memo, smol, big - smol);
        }
        else
        {
            memo[(big, smol)] = true;
        }

        return memo[(big, smol)];
    }
}

#endif
}
