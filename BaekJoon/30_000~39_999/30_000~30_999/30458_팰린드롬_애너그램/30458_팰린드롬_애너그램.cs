using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 17
이름 : 배성훈
내용 : 팰린드롬 애너그램
    문제번호 : 30458번

    주된 아이디어는 다음과 같다
    (n + 1) / 2의 원소(컴퓨터 나눗셈 연산)를 중심으로 좌 우를 구분한다
    만약 n이 짝수면 중심은 오른쪽에 포함한다!

    그리고 문제 조건에서 좌우에 원소 1개씩을 바꾸기에
    좌우 원소는 중앙을 제외하고 원하는 위치로 이동시킬 수 있다

    그래서 좌우 원소가 짝수로 나뉘어 떨어지는지 확인하면 되지 않을까 싶어
    짝수 갯수 판정으로 풀어 제출했다
    64ms로 이상없이 풀렸다

    에드혹 알고리즘으로 분류되어져 있다
    에드혹 알고리즘을 찾아보니
    일반화는 안되고! 해당 문제에 맞춰 푸는 것으르 의미
        -> 하드코딩??
    ...? 대부분 해당 유형이 정형화 되기 전에는 모든 유형이 에드혹 아닌가..??
    그냥 아이디어 찾아서 푸는 문제라 생각하자;
*/

namespace BaekJoon.etc
{
    internal class etc_0052
    {

        static void Main52(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(new BufferedStream(Console.OpenStandardInput())));

            int len = ReadInt(sr);

            int[] alphabet = new int[26];

            int half = len / 2;
            bool isOdd = (len & 1) == 1;

            for (int i = 0; i < half; i++)
            {

                int c = sr.Read() - 'a';

                alphabet[c]++;
            }

            if (isOdd) sr.Read();

            for (int i = 0; i < half;  i++)
            {

                int c = sr.Read() - 'a';

                alphabet[c]++;
            }

            sr.Close();

            bool able = true;
            for (int i = 0; i < 26; i++)
            {

                if (alphabet[i] % 2 == 0) continue;

                able = false;
                break;
            }

            if (able) Console.WriteLine("Yes");
            else Console.WriteLine("No");
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }
            return ret;
        }
    }
}
