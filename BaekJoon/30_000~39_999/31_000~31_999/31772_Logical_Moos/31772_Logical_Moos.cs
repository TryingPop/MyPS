using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 10
이름 : 배성훈
내용 : Logical Moos
    문제번호 : 31772번

    누적합, 구현 문제다.
    규칙을 보면 and는 피연산자가 모두 true일 때만 true이다
    그래서 양쪽 중 적어도 하나가 false이면 true는 불가능하다

    마찬가지로 or은 피연산자가 모두 false일 때만 false이다
    양쪽 중 적어도 하나가 true이면 false는 불가능하다.
    이렇게 불가능한 경우를 누적합 아이디어로 찾아주면 된다.

    and 연산을 먼저 적용하고 or 연산을 후순위로 한다.
    그래서 and 는 먼저 연산하기에 and끼리 그룹화를 하고
    각 그룹별로 false가 어디서부터 있는지 확인한다. 
    그러면 해당 false 구간이 일부라도 포함이 안되면 해당 영역은 무조건 false만 가능하다.

    그리고 or에도 똑같이 한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1102
    {

        static void Main1102(string[] args)
        {


            StreamReader sr;

            int INF = 1_000_000;
            int TRUE = 75343;
            int FALSE = 595723;
            int AND = 5572;
            int OR = 696;

            int[] bs;   // boolean statement
            int n, q;

            int[] group;
            int gLen;

            int[] ff, lf;   // first false, last false

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void GetRet()
            {

                // or 연산
                // true가 존재하는 구간 확인
                // 해당 구간이 적어도 하나라도 포함안되면 무조건 true만 가능하다
                int ft = INF;   // first true
                int lt = -1;    // last true
                for (int i = 0; i <= gLen; i++)
                {

                    if (ff[i] != INF) continue;

                    if (ft == INF) ft = i;
                    lt = i;
                }

                // 쿼리 수행
                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                while (q-- > 0)
                {

                    int l = ReadInt() - 1;
                    int r = ReadInt() - 1;

                    int ret = ReadInt();

                    int lg = group[l];
                    int rg = group[r];

                    // or에서 true를 만드는 영역을 모두 포함 못해 오로지 true만 가능
                    if (ft < lg || lt > rg) sw.Write(ret == TRUE ? 'Y' : 'N');
                    // or에서 true로 만드는 영역을 모두 포함했다.
                    // 이제 and에서 false 영역을 모두 포함하는지 판별
                    // 모두 포함 못하면 이 경우 무조건 false만 가능
                    else if (ret == TRUE) sw.Write(ff[lg] < l || lf[rg] > r ? 'N' : 'Y');
                    // 양쪽다 가능한 경우
                    else sw.Write('Y');
                }

                sw.Close();
            }

            void SetArr()
            {

                // and 끼리 그룹화
                gLen = 0;
                group = new int[n];
                for (int i = 0; i < n; i++)
                {

                    if (bs[i] == OR) gLen++;
                    else if ((i & 1) == 0) group[i] = gLen;
                }

                // and 연산에서 false가 나오면 무조건 false다!
                ff = new int[gLen + 1]; // first false
                lf = new int[gLen + 1]; // last false

                Array.Fill(ff, INF);
                Array.Fill(lf, -1);

                for (int i = 0; i < n; i += 2)
                {

                    // true면 넘긴다.
                    if (bs[i] == TRUE) continue;

                    // false면 찾아서 시작 지점과 끝 지점 저장
                    // 해당 영역과 겹치는 구간에서 변형 안된 구간이 남아있으면 무조건 false다
                    int g = group[i];
                    if (ff[g] == INF) ff[g] = i;
                    lf[g] = i;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput());
                n = ReadInt();
                q = ReadInt();

                bs = new int[n];
                for (int i = 0; i < n; i++)
                {

                    bs[i] = ReadInt();
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include <iostream>
using namespace std;

int n, q, tip[200002], lastorL[200002], lastorR[200002], lastfalseL[200002], lastfalseR[200002];
int evalL[200002], evalR[200002];

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);
    
    cin >> n >> q;
    for(int i = 1; i <= n; i++)
    {
        string s;
        cin >> s;
        if(s[0] == 'f')
            tip[i] = 0;
        if(s[0] == 't')
            tip[i] = 1;
        if(s[0] == 'a')
            tip[i] = 2;
        if(s[0] == 'o')
            tip[i] = 3;
    }
    
    // precompute lastor arrays
    for(int i = 1; i <= n; i++)
        if(tip[i] == 3)
            lastorL[i] = i;
        else
            lastorL[i] = lastorL[i-1];
    
    lastorR[n+1] = n+1;
    for(int i = n; i >= 1; i--)
        if(tip[i] == 3)
            lastorR[i] = i;
        else
            lastorR[i] = lastorR[i+1];
    
    // precompute lastfalse arrays
    lastfalseL[0] = -1;
    for(int i = 1; i <= n; i++)
        if(tip[i] == 0)
            lastfalseL[i] = i;
        else
            lastfalseL[i] = lastfalseL[i-1];
    
    lastfalseR[n+1] = n+2;
    for(int i = n; i >= 1; i--)
        if(tip[i] == 0)
            lastfalseR[i] = i;
        else
            lastfalseR[i] = lastfalseR[i+1];

    // precompute subexpressions for left/right
    int overall = 0;
    for(int i = 1; i <= n; )
    {
        int j = i+1;
        int val = tip[i];
        while(j <= n && tip[j] != 3)
        {
            val &= (tip[j+1]);
            j += 2;
        }
        if(val == 1)
            overall = 1;
        evalL[j] = overall;
        i = j+1;
    }
    
    int overall2 = 0;
    for(int i = n; i >= 1; )
    {
        int j = i-1;
        int val = tip[i];
        while(j >= 1 && tip[j] != 3)
        {
            val &= (tip[j-1]);
            j -= 2;
        }
        if(val == 1)
            overall2 = 1;
        evalR[j] = overall2;
        i = j-1;
    }
    
    // dealing with the cases and the queries
    for(int i = 1; i <= q; i++)
    {
        int L, R;
        string x;
        cin >> L >> R;
        cin >> x;
        
        int pL = lastorL[L];
        int pR = lastorR[R];
        
        if(evalL[pL] || evalR[pR])
        {
            if(x == "false")
                cout << "N";
            else
                cout << "Y";
        }
        else
        {
            if(x == "false")
                cout << "Y";
            else
                if(lastfalseL[L-1] >= pL || lastfalseR[R+1] <= pR)
                    cout << "N";
                else
                    cout << "Y";
        }
    }
    return 0;
}
#endif
}
