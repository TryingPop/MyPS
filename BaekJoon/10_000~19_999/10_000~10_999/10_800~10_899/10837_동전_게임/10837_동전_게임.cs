using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 14
이름 : 배성훈
내용 : 동전 게임
    문제번호 : 10837번

    수학 문제다

    아이디어는 다음과 같다
    점수가 주어지면 점수의 가능성 여부이기에
    최선의 방법으로 점수를 획득한다고 가정했다(그리디)
    
    남은 턴과 현재 두 사람의 점수차를 누가 큰 가에 따라 비교한다
    그리디하게 가정했으므로 두 사람의 점수가 큰쪽에 따라 남은턴이 다르다

    뒷사람이 높은 경우는 앞사람은 이미 실행했으므로
    남은턴은 전체 - 높은 점수가 된다

    앞 사람이 점수가 높을 는 뒷사람의 남은 턴은 아직 1턴 더 있다
    그래서 전체 - 높은 점수 + 1 점이 남은 턴이 된다

    이제 남은 턴과 점수차를 비교한다
    바로 비교하면 점수차일 때, 게임의 종료 여부만 확인 된다

    그래서 가능한 점수와 동형으로 만들러면 남은 점수에 + 1 점을 
    해줘야 된다 그래야 성립가능 여부와 해집합이 동형이다

    아래는 해당 아이디어를 코드로 나타냈을 뿐이다
*/

namespace BaekJoon.etc
{
    internal class etc_0223
    {

        static void Main223(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int k = ReadInt(sr);

            int len = ReadInt(sr);

            for (int i = 0; i < len; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                bool possible = true;
                // 점수차 비교
                // f <= k && b <= k가 보장되어 있으므로
                // 같은 경우는 possible
                if (f > b)
                {

                    int diff = f - b;
                    // 뒷사람의 남은턴 + 1
                    // 그리고 점수 가능여부와 동형이 되기 위해 + 1
                    int remain = k - f + 2;

                    if (remain < diff) possible = false;
                }
                else if (f < b)
                {

                    int diff = b - f;
                    // 앞 사람의 남은턴
                    // 점수 가능 여부와 동형이 되기 위해 + 1
                    int remain = k - b + 1;

                    if (remain < diff) possible = false;
                }

                if (possible) sw.WriteLine(1);
                else sw.WriteLine(0);
            }

            sw.Close();
            sr.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c!= '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
