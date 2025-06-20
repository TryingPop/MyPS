using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 9
이름 : 배성훈
내용 : The Dragon of Loowater
    문제번호 : 4228번

    그리디 정렬 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0167
    {

        static void Main167(string[] args)
        {

            string NO = "Loowater is doomed!";
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            List<int> dragon = new(2_000);
            List<int> knight = new(2_000);

            while(true)
            {

                int headLen = ReadInt(sr);
                int knightLen = ReadInt(sr);

                if (headLen == 0 && knightLen == 0) break;

                for (int i = 0; i < headLen; i++)
                {

                    dragon.Add(ReadInt(sr));
                }

                for (int i = 0; i < knightLen; i++)
                {

                    knight.Add(ReadInt(sr));
                }

                dragon.Sort();
                knight.Sort();

                int l = 0;
                int r = 0;

                int ret = 0;
                while(l < headLen && r < knightLen)
                {

                    if (knight[r] >= dragon[l])
                    {

                        // 기사가 용 사냥 가능하므로 잡는다
                        ret += knight[r++];
                        l++;
                    }
                    // 기사가 용사냥 불가능
                    else r++;

                }

                // 용을 다 잡은 경우
                if (l == headLen) sw.WriteLine(ret);
                // 용을 다 못잡은 경우
                else sw.WriteLine(NO);

                dragon.Clear();
                knight.Clear();
            }

            sr.Close();
            sw.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
