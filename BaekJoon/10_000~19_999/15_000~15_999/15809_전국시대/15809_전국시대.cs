using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 12
이름 : 배성훈
내용 : 전국시대
    문제번호 : 15809번

    유니온 파인드 문제다
    동맹이면 서로 유닛을 합치며 그룹을 묶었고
    전쟁이면 서로 유닛수를 줄이고 많은쪽에 그룹을 묶었다

    시간은 96ms로 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0212
    {

        static void Main212(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            (int group, int unit)[] groups = new (int group, int unit)[n + 1];

            for (int i = 1; i <= n; i++)
            {

                groups[i].group = i;
                groups[i].unit = ReadInt(sr);
            }

            Stack<int> s = new Stack<int>();

            for (int i = 0; i < m; i++)
            {

                int op = ReadInt(sr);

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                f = Find(groups, f, s);
                b = Find(groups, b, s);

                if (op == 1)
                {

                    // 동맹
                    // 작은 값으로 묶는다
                    if (b < f)
                    {

                        int temp = f;
                        f = b;
                        b = temp;
                    }

                    // 유닛 한쪽에 몰아준다
                    groups[f].unit += groups[b].unit;
                    groups[b].unit = 0;
                    groups[b].group = f;
                    continue;
                }

                // 전쟁
                if (groups[f].unit < groups[b].unit)
                {

                    // b가 항상 유닛수가 적게 수정
                    int temp = f;
                    f = b;
                    b = temp;
                }

                // 유닛 적은쪽의 값을 양쪽에 빼준다
                groups[f].unit -= groups[b].unit;
                groups[b].unit = 0;
                groups[b].group = f;
            }

            // 남은 국가 세기
            int ret = 0;
            for (int i = 1; i <= n; i++)
            {

                if (groups[i].unit != 0) ret++;
            }

            // 남은 유닛들 List로 정렬한다
            List<int> units = new List<int>(ret);

            for (int i = 1; i <= n; i++)
            {

                if (groups[i].unit != 0) units.Add(groups[i].unit);
            }

            units.Sort();

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(ret);
                for (int i = 0; i < units.Count; i++)
                {

                    sw.Write(units[i]);
                    sw.Write(' ');
                }
            }
        }

        static int Find((int group, int unit)[] _groups, int _chk, Stack<int> _s)
        {

            while(_chk != _groups[_chk].group)
            {

                _s.Push(_chk);
                _chk = _groups[_chk].group;
            }

            while(_s.Count > 0)
            {

                _groups[_s.Pop()].group = _chk;
            }

            return _chk;
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
