using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 26
이름 : 배성훈
내용 : 거짓말
    문제번호 : 31091번

    거짓말 하는 사람을 i라 가정하고 거짓말 하는 사람이 정말 i명이 되는지 확인했다
    그리고 이를 bool 변수에 담았다

    3N의 메모리를 썼고, O(N)의 시간으로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0092
    {

        static void Main92(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            // plus[idx] : idx 이상을 외친 사람의 수
            int[] plus = new int[len + 1];
            // minus[idx] : idx 이하를 외친 사람의 수
            int[] minus = new int[len + 1];

            for (int i = 0; i < len; i++)
            {

                int chk = ReadInt(sr);

                if (chk > 0) plus[chk]++;
                else minus[-chk]++;
            }

            sr.Close();

            // plus와 minus의미 변경
            // plus[idx]
            // idx <= i인 모든 i에 대해 i명 이상을 외친 사람들의 합
            // minus[idx]
            // idx >= i인 모든 i에 대해 i명 이하를 외친 사람들의 합
            //
            // 전체가 5일 때,
            // plus[1] 은 1명 이상, 2명 이상, 3명 이상, 4명 이상, 5명 이상을 외친 사람들의 수를 합친것
            // minus[1]은 0명 이하, 1명 이하를 외친 사람들의 수를 합친 것
            for (int i = 0; i < len; i++)
            {

                minus[i + 1] += minus[i];
                plus[len - i - 1] += plus[len - i];
            }

            // lie[idx]는 idx명이 거짓말할 때 가능한지 여부
            bool[] lie = new bool[len + 1];
            int ret = 0;
            for (int i = 1; i < len; i++)
            {

                // i명의 거짓말 쟁이가 존재할 수 있는지 확인
                // i + 1명 이상 외친 경우 거짓말쟁이가 된다
                // i - 1명 이하 외친 경우 거짓말 쟁이다
                int chk = plus[i + 1] + minus[i - 1];
                if (chk != i) continue;

                lie[i] = true;
                ret++;
            }

            // 0명인 경우 1명 이상을 외친 사람이 0명이어야 한다
            lie[0] = plus[1] == 0;
            // len명이 거짓말 하는 경우면 len - 1 >= i인 모든 i에 대해 i이하의 사람들이 거짓말 하고 있다 외친 사람들이 거짓말 쟁이가 된다
            lie[len] = minus[len - 1] == len;

            if (lie[0]) ret++;
            if (lie[len]) ret++;

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(ret);

                for (int i = 0; i <= len; i++)
                {

                    if (!lie[i]) continue;

                    sw.Write(i);
                    sw.Write(' ');
                }
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            bool plus = true;
            int c, ret = 0;

            while ((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                else if (c == '-')
                {

                    plus = false;
                    continue;
                }

                ret = ret * 10 + c - '0';
            }

            return plus ? ret : -ret;
        }
    }
}
