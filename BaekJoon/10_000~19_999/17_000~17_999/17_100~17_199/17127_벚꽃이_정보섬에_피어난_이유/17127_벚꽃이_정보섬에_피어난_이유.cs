using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 벚꽃이 정보섬에 피어난 이유
    문제번호 : 17127번

    구현, 브루트포스 알고리즘 문제다
    브루트 포스로 DFS 탐색을 이용했다
*/

namespace BaekJoon.etc
{
    internal class etc_0249
    {

        static void Main249(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int[] tree = new int[n];

            for (int i = 0; i < n; i++)
            {

                tree[i] = ReadInt(sr);
            }

            sr.Close();

            int[] group = new int[4];

            int ret = DFS(tree, group, 0, 0);
            Console.WriteLine(ret);
        }
        
        static int DFS(int[] _tree, int[] _group, int _cur, int _curTeam)
        {

            int ret = 0;
            if (_cur == _tree.Length)
            {

                for (int i = 0; i < 4; i++)
                {

                    int calc = _group[i];
                    // 그룹에 적어도 1개 포함되어야한다
                    if (calc == 0) return 0;
                    ret += _group[i];
                }

                return ret;
            }

            int before = _group[_curTeam];

            if (before == 0) _group[_curTeam] = _tree[_cur];
            else _group[_curTeam] *= _tree[_cur];

            // 인접한거 끼리 해야하므로 현재 팀이어간다
            int chk = DFS(_tree, _group, _cur + 1, _curTeam);
            if (ret < chk) ret = chk;
            _group[_curTeam] = before;

            if (_curTeam < 3)
            {

                // 다음팀 갈 수 있으면 다음팀으로 넘어간다
                _curTeam++;
                before = _group[_curTeam];
                if (before == 0) _group[_curTeam] = _tree[_cur];
                else _group[_curTeam] *= _tree[_cur];

                chk = DFS(_tree, _group, _cur + 1, _curTeam);
                if (ret < chk) ret = chk;
                _group[_curTeam] = before;
            }

            return ret;
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
