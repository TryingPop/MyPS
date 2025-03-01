using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : Hunger Games
    문제번호 : 11261번

    케이스 입력에서 입력 앞 뒤로 공백 문자가 있는거 같다
    인덱스 에러로 여러번 틀렸다

    Trim을 쓰니 이상없이 통과했다
    문제는 배낭 문제다

    배낭 문제는 바로 코드로 구현이 안되어 연습이 더 필요한 부분같다
*/

namespace BaekJoon.etc
{
    internal class etc_0184
    {

        static void Main184(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt(sr);

            int[] pack = new int[5_001];
            int[] weights;
            int[] values;

            while(test-- > 0)
            {

                Array.Fill(pack, -1);
                int weapons = ReadInt(sr);
                int maxWeight = ReadInt(sr);

                // 하나씩 읽어오고 싶었으나 공백문제로 제대로 못읽고 인덱스 에러만 떴다.
                weights = sr.ReadLine().Trim().Split(' ').Select(int.Parse).ToArray();
                values = sr.ReadLine().Trim().Split(' ').Select(int.Parse).ToArray();
                pack[0] = 0;
                int ret = 0;

                for (int i = 0; i < weapons; i++)
                {

                    // 최대 가치 찾기
                    for (int j = maxWeight; j >= 0; j--)
                    {

                        int next = j + weights[i];
                        if (pack[j] == -1 || next > maxWeight) continue;
                        int chk = pack[j] + values[i];
                        if (pack[next] < chk) 
                        {
                            
                            pack[next] = chk;
                            if (ret < chk) ret = chk;
                        }
                    }
                }

                sw.WriteLine(ret);
            }

            sw.Close();
            sr.Close();
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
