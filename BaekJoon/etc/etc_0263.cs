using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 17
이름 : 배성훈
내용 : 숫자 야구
    문제번호 : 2503번

    구현, 브루트 포스 문제다
    일반적으로 하던 숫자 야구인줄 알고 제출 했다가 한 번 틀렸다
    여기서는 0을 쓰지 않는다

    숫자를 설정하고 해당 숫자가 유효한지 확인했다
    유효하면 결과값 + 1해서 모든 경우를 하나씩 세어갔다
    세는데는 DFS 탐색과 백트래킹 알고리즘을 썼다
    DFS 말고 삼중 포문으로 해도된다
*/

namespace BaekJoon.etc
{
    internal class etc_0263
    {

        static void Main263(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            BaseBall[] game = new BaseBall[len];

            for (int i = 0; i < len; i++)
            {

                int num = ReadInt(sr);
                int strike = ReadInt(sr);
                int ball = ReadInt(sr);

                game[i].Set(num, strike, ball);
            }

            sr.Close();

            bool[] visit = new bool[10];
            int[] calc = new int[3];

            int ret = DFS(game, visit, calc, 0);

            Console.WriteLine(ret);
        }

        static int DFS(BaseBall[] _game, bool[] _visit, int[] _cur, int _depth)
        {

            if (_depth == 3)
            {

                for (int i = 0; i < _game.Length; i++)
                {

                    if (_game[i].ChkInvalid(_cur)) return 0;
                }

                return 1;
            }

            int ret = 0;
            for (int i = 1; i <10; i++)
            {

                if (_visit[i]) continue;
                _visit[i] = true;

                _cur[_depth] = i;
                ret += DFS(_game, _visit, _cur, _depth + 1);
                _visit[i] = false;
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

        struct BaseBall
        {

            private int n1;
            private int n2;
            private int n3;

            private int strike;
            private int ball;

            public bool ChkInvalid(int[] _chkNum)
            {

                int chkStrike = 0;
                int chkBall = 0;

                if (_chkNum[0] == n1) chkStrike++;
                else if (n1 == _chkNum[1] || n1 == _chkNum[2]) chkBall++;

                if (_chkNum[1] == n2) chkStrike++;
                else if (n2 == _chkNum[2] || n2 == _chkNum[0]) chkBall++;

                if (_chkNum[2] == n3) chkStrike++;
                else if (n3 == _chkNum[0] || n3 == _chkNum[1]) chkBall++;

                return ball != chkBall || strike != chkStrike;
            }

            public void Set(int _num, int _strike, int _ball)
            {

                n3 = _num % 10;
                _num /= 10;
                n2 = _num % 10;
                n1 = _num / 10;

                strike = _strike;
                ball = _ball;
            }
        }
    }
}
