using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 14
이름 : 배성훈
내용 : 좋은 날 싫은 날
    문제번호 : 17211번

    조건부 확률 문제다
    바로 이전 확률만 기록해서 풀 수 있으나
    모든 이전 값을 dp로 기록해 먼저 풀었다

    이전에 전부 기록하면 쉽게 풀 수 있는 문제를
    필요한거만 해서 접근하다가 시간을 엄청 먹고
    풀지도 못한 기억이 있기 때문이다

    그래서 먼저 전부 저장을 하고 필요한것만 추출하는
    연습을 하려고 주석으로 먼저 제출했다
*/

namespace BaekJoon.etc
{
    internal class etc_0227
    {

        static void Main227(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int cur = ReadInt(sr);

            float gTog = MathF.Round(ReadFloat(sr), 2);
            float gTos = MathF.Round(ReadFloat(sr), 2);

            float sTog = MathF.Round(ReadFloat(sr), 2);
            float sTos = MathF.Round(ReadFloat(sr), 2);

            sr.Close();

            // float[] good = new float[n + 1];
            // float[] sad = new float[n + 1];

            // good[1] = cur == 0 ? gTog : sTog;
            // sad[1] = cur == 0 ? gTos : sTos;

            // before Good
            float bG;
            float bS;

            // cur Good
            float cG = cur == 0 ? gTog : sTog;
            float cS = cur == 0 ? gTos : sTos;

            for (int i = 2; i <= n; i++)
            {

                // good[i] = MathF.Round(good[i - 1] * gTog + sad[i - 1] * sTog, 4);
                // sad[i] = MathF.Round(good[i - 1] * gTos + sad[i - 1] * sTos, 4);

                // 이전 Good, Sad 확률
                bG = cG;
                bS = cS;

                // 현재 Good, Sad 확률
                cG = MathF.Round(bG * gTog + bS * sTog, 4);
                cS = MathF.Round(bG * gTos + bS * sTos, 4);
            }

            int ret1 = (int)MathF.Round(cG * 1000);
            int ret2 = (int)MathF.Round(cS * 1000);
            // int ret1 = (int)MathF.Round(good[n] * 1000);
            // int ret2 = (int)MathF.Round(sad[n] * 1000);

            Console.WriteLine(ret1);
            Console.WriteLine(ret2);
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

        static float ReadFloat(StreamReader _sr)
        {

            int c;
            float ret = 0.0f;
            bool dot = false;

            float chk = 0.1f;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                else if (c == '.')
                {

                    dot = true;
                    continue;
                }


                if (dot) 
                { 
                    
                    ret = ret + chk * (c - '0');
                    chk *= 0.1f;
                }
                else ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
