using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 라디오
    문제번호 : 3135번

    수학, 그리디 문제다
    버튼 한 번만 누르면 원하는 저장된 채널로 바로 이동할 수 있기에
    저장된 버튼은 두 번 이상 누르면 최소값이 될 수 없다
    많아야 1번 누르고 나머지는 위 아래 버튼으로 채널을 이동해야한다

    현재 지점에서 위아래 버튼을 누른경우와
    저장된 버튼 1번 누른 경우를 모두 조사해 최소값을 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0253
    {

        static void Main253(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int a = ReadInt(sr);
            int b = ReadInt(sr);

            int len = ReadInt(sr);
            // 시작 지점에서 위 아래 누른 경우
            int ret = a - b;
            ret = ret < 0 ? -ret : ret;

            for (int i = 0; i < len; i++)
            {

                // 해당 저장된 채널로 이동해서 원하는 채널로 가는 경우
                int cur = ReadInt(sr);
                int diff = cur - b;
                diff = diff < 0 ? -diff : diff;

                diff++;
                if (ret > diff)
                {

                    // 가장 작은게 최소값
                    ret = diff;
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
