using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 28
이름 : 배성훈
내용 : 컨테이너 재배치
    문제번호 : 25945번

    수학, 그리디, 정렬 문제다
    평균을 찾고 평균을 넘어가는 것만 재배치 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0644
    {

        static void Main644(string[] args)
        {

            StreamReader sr;

            int n;
            int[] arr;
            int chk1, chk2;

            Solve();

            void Solve()
            {

                Input();
                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (arr[i] <= chk1) continue;
                    if (chk2 > 0)
                    {

                        chk2--;
                        ret += arr[i] - (chk1 + 1);
                    }
                    else ret += arr[i] - chk1;
                }

                Console.WriteLine(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput());
                n = ReadInt();
                arr = new int[n];
                int total = 0;
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    arr[i] = cur;
                    total += cur;
                }

                chk1 = total / n;
                chk2 = total % n;
                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
var reader = new Reader();
var n = reader.NextInt();
var cont = new int[n];
for (int i = 0; i < n; i++)
    cont[i] = reader.NextInt();

int avg = cont.Sum() / n;
Array.Sort(cont);

int toMove = 0;
for (int i = n - 1; i >= 0; i--)
{
    if (cont[i] <= avg)
        break;

    toMove += cont[i] - avg - 1;
}

int toFill = 0;
for (int i = 0; i < n; i++)
{
    if (cont[i] >= avg)
        break;

    toFill += avg - cont[i];
}

Console.Write(Math.Max(toMove, toFill));

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
}
#endif
}
