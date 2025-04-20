using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 20
이름 : 배성훈
내용 : 저지
    문제번호 : 10774번

    그리디 구현 문제다
    문제 조건을 잘못 읽어 2번 틀렸다
    번호는 같아야하고, 사이즈는 그 이상이 되어야한다
    그래서 번호가 같아야하므로 입을 수 있다면 그 사람에게 바로 줘도 최대값에는 변화가 없다

    또한 알파벳 순으로 보면 L < M < S 이고 실제 사이즈는 S < M < L과 반대임을 알 수 있다
    그리고 L, M, S 각각에 '0'을 빼준 값은 100 미만이다
    그래서 100으로 바꿔 옷을 건내줬다고 했다

    이렇게 제출하니 이상없이 통과했다
    그러나 100만 입력이 들어오므로 시간은 128ms이다
*/

namespace BaekJoon.etc
{
    internal class etc_0292
    {

        static void Main292(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 1024 * 1024);

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            int[] jeans = new int[n + 1];

            for (int i = 1; i <= n; i++)
            {

                jeans[i] = ReadInt(sr);
            }

            int ret = 0;
            for (int i = 0; i < m; i++)
            {

                int type = ReadInt(sr);
                int idx = ReadInt(sr);

                if (jeans[idx] <= type)
                {

                    jeans[idx] = 100;
                    ret++;
                }
            }
            sr.Close();

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
