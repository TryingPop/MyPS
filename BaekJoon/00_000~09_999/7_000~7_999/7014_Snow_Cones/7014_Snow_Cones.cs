using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 1
이름 : 배성훈
내용 : Snow Cones
    문제번호 : 7014번

    시뮬레이션, 그리디 문제다
    조건대로 시뮬레이션 돌려 최적해를 찾으면 된다.

    처음에는 그냥 O끼리 거리만 재면 되지 않을까 했고 이는 한 턴에 한 번만 움직일 수 있다에 막혔다
        OOOXX
        XXOOO

    4이다

    그래서 인접한거끼리 누적하면 괜찮지 않을까? 생각했고 제출하니 또 막혔다
        XXXXXXXOOOOOOOO
        OOXXXOOOOOOXXXX

    11이다
    이 경우 뒤에 있는 O 2개는 3칸 이동 후 앞에 있는 돌들에 막혀 멈춘다
    그래서 그냥 돌의 길이가 1000밖에 안되므로 N^2해도 100만이라 
    시뮬레이션 돌리자 생각했고 제출하니 통과했다;
*/

namespace BaekJoon.etc
{
    internal class etc_1090
    {

        static void Main1090(string[] args)
        {

            string HEAD = "Data Set ";
            string TAIL = ":\n";

            StreamReader sr;
            StreamWriter sw;

            int[] up, down;
            int[] uPos, dPos;
            int len, pLen;

            Solve();
            void Solve()
            {

                Init();

                int test = int.Parse(sr.ReadLine());
                for (int t = 1; t <= test; t++)
                {

                    Input();

                    Output(t, GetRet());
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                up = new int[1_000];
                down = new int[1_000];

                uPos = new int[1_001];
                dPos = new int[1_001];
            }

            void Output(int _t, int _ret)
            {

                if (_t != 1) sw.Write('\n');
                sw.Write(HEAD);
                sw.Write(_t);
                sw.Write(TAIL);
                sw.Write(_ret);
                sw.Write('\n');
            }

            void Input()
            {

                SetArr(up, uPos);
                SetArr(down, dPos);

                uPos[pLen] = len + 5;
                dPos[pLen] = len + 5;
                void SetArr(int[] _arr, int[] _pos)
                {

                    len = 0;
                    pLen = 0;
                    int c;
                    while ((c = sr.Read()) != '\n')
                    {

                        switch (c)
                        {

                            case '\r':
                                continue;

                            case 'O':
                                _pos[pLen++] = len;
                                _arr[len++] = 1;
                                continue;

                            case 'X':
                                _arr[len++] = 0;
                                continue;
                        }
                    }
                }
            }

            int GetRet()
            {

                int ret = 0;
                while (true)
                {

                    bool flag = true;

                    for (int i = 0; i < pLen; i++)
                    {

                        if (uPos[i] <= dPos[i]) continue;
                        else if (dPos[i + 1] == dPos[i] + 1) continue;
                        flag = false;

                        dPos[i]++;
                    }

                    for (int i = pLen - 1; i >= 0; i--)
                    {

                        if (dPos[i] <= uPos[i]) continue;
                        else if (i > 0 && dPos[i - 1] + 1 == dPos[i]) continue;
                        flag = false;

                        dPos[i]--;
                    }

                    if (flag) break;
                    ret++;
                }
                return ret;

                int NextIdx(int[] _arr, int _idx)
                {

                    _idx++;
                    while (_idx < len && _arr[_idx] == 0) { _idx++; }
                    return _idx;
                }
            }
        }
    }

#if other
// #include <stdio.h>
// #include <algorithm>
// #include <queue>
// #include <string.h>
using namespace std;

char s[1024], a[1024];
int n, arr[1024];
int main(){
    int t;
    scanf("%d", &t);
    for(int tt = 1; tt <= t; tt++){
        scanf("%s\n%s", s, a);
        n = strlen(s);
        queue<int> qo, qx;
        for(int j = 0; j < n; j++){
            if(a[j] == 'O') qo.push(j);
            else qx.push(j);
        }
        for(int j = 0; j < n; j++){
            if(s[j] == 'O'){
                arr[j] = qo.front(); qo.pop();
            }
            else{
                arr[j] = qx.front(); qx.pop();
            }
        }
        for(int ans = 0; ; ans++){
            bool changed = false;
            for(int i = 0; i < n - 1; i++){
                if(arr[i] > arr[i + 1]){
                    swap(arr[i], arr[i + 1]);
                    changed = true;
                    i++;
                }
            }
            if(!changed){
                printf("Data Set %d:\n%d\n\n", tt, ans);
                break;
            }
        }
    }
}
#endif
}
