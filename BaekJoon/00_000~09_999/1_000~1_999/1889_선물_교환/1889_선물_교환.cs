using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 16
이름 : 배성훈
내용 : 선물 교환
    문제번호 : 1889번

    문제를 잘못 해석해서 틀렸다
    처음에는 그룹으로만 초대하면 되는줄 알았다;
    그런데 문제에서 요구하는건 초청 받는 사람에게 선물을 2개 받는 사람만 최대한 많이 초청해야한다
    모든 사람들을 초청했을 때, 강퇴하는 형식으로 계산한다

    그래서, 선물이 2개가 안되는 사람들을 모두 모아서 강퇴!
    다음으로, 강퇴가 된 사람들이 보내는 선물을 깎는다
    여기서 강퇴가 된 사람들이 보내는 선물을 받는 사람들을 조사해서 2개가 안되면 강퇴!

    그리고 강퇴가 된 사람에게 선물을 보내는 사람도 강퇴되어야한다!
    중복 방지 코드가 필수다!
*/

namespace BaekJoon.etc
{
    internal class etc_0044
    {

        static void Main44(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
#if Wrong
            Stack<int> s = new Stack<int>();

            int len = ReadInt(sr);

            int[] groups = new int[len + 1];

            for (int i = 1; i <= len; i++)
            {

                groups[i] = i;
            }

            for (int i = 0; i < len; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                int gf = Find(groups, f, s);
                int gb = Find(groups, b, s);

                if (gf == gb) continue;

                if (gb < gf) groups[gf] = gb;
                else groups[gb] = gf;
            }
            sr.Close();


            int[] nums = new int[len + 1];
            for (int i = 1; i <= len; i++)
            {

                int g = Find(groups, i, s);
                nums[g]++;
            }

            int max = 0;
            int maxGroup = -1;
            for (int i = 1; i <= len; i++)
            {

                if (nums[i] > max) 
                { 
                    
                    max = nums[i];
                    maxGroup = i;
                }
            }


            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(max);

                for (int i = 1; i <= len; i++)
                {

                    if (groups[i] == maxGroup)
                    {

                        sw.Write(i);
                        sw.Write(' ');
                    }
                }
            }
#endif

            // 단방향 간선으로 읽어야한다!
            int len = ReadInt(sr);
            int[,] give = new int[len + 1, 2];
            List<int>[] receive = new List<int>[len + 1];
            for (int i = 1; i <= len; i++)
            {

                receive[i] = new();
            }

            int[] present = new int[len + 1];

            for (int i = 1; i <= len; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                give[i, 0] = f;
                give[i, 1] = b;
                present[f]++;
                present[b]++;

                receive[f].Add(i);
                receive[b].Add(i);
            }

            sr.Close();

            bool[] ban = new bool[len + 1];

            int total = len;
            Queue<int> q = new Queue<int>();

            for (int i = 1; i <= len; i++)
            {

                if (present[i] < 2) 
                { 
                    
                    q.Enqueue(i);
                    ban[i] = true;
                }
            }

            while (q.Count > 0)
            {

                int node = q.Dequeue();
                total--;

                for (int i = 0; i < 2; i++)
                {

                    int chk = give[node, i];
                    present[chk]--;

                    if (!ban[chk] && present[chk] < 2)
                    {

                        q.Enqueue(chk); 
                        ban[chk] = true;
                    }
                }

                for (int i = 0; i < receive[node].Count; i++)
                {

                    int chk = receive[node][i];
                    if (ban[chk]) continue;

                    q.Enqueue(chk);
                    ban[chk] = true;
                }
            }

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            sw.WriteLine(total);

            if (total > 0)
            {

                for (int i = 1; i <= len; i++)
                {

                    if (ban[i]) continue;

                    sw.Write(i);
                    sw.Write(' ');
                }
            }
            sw.Close();
        }

#if Wrong
        static int Find(int[] _groups, int _chk, Stack<int> _calc)
        {

            while(_chk != _groups[_chk])
            {

                _calc.Push(_chk);
                _chk = _groups[_chk];
            }

            while(_calc.Count > 0)
            {

                _groups[_calc.Pop()] = _chk;
            }

            return _chk;
        }
#endif
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
