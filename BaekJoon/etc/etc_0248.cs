using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 되돌리기
    문제번호 : 1360번

    구현 문제다
    그리디하게 접근했다.. (그리디 만능론?)
    undo의 경우를 보면 뒤에서부터 취소된다고 하면, 
    앞에서 부터 undo를 하는 것과 같은 기능을 한다
    
    그래서 명령을 모두 저장하고
    뒤에서부터 탐색한다

    그리고 명령이 취소됐는지 확인하는 변수,
    그리고 취소시킬 명령을 찾기 위해 명령 시간 변수를 설정했다

    이후 취소된 명령이 아니고, 취소시키는 명령이라면
    명령 시간이 정렬된 집합이므로 이진 탐색으로 멈출 값들을 찾아간다
    이후에 다시 생각해보니 일일히 멈추는게 싸보인다

    만약 누적합?과 비슷한 아이디어를 쓴다면 이진 탐색이 좋아보인다
    이후에는 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0248
    {

        static void Main248(string[] args)
        {

            string TYPE = "type";
            string UNDO = "undo";

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int test = int.Parse(sr.ReadLine());

            int[] time = new int[test];
            string[][] order = new string[test][];
            for (int i = 0; i < test; i++)
            {

                
                string[] temp = sr.ReadLine().Split(' ');
                order[i] = temp;
                time[i] = int.Parse(temp[2]);
            }

            // 뒤에서 부터 멈출거 조사
            bool[] inActive = new bool[test];
            for (int i = test - 1; i >= 0; i--)
            {

                if (inActive[i]) continue;

                if (order[i][0] == UNDO)
                {

                    int chkTime = time[i] - int.Parse(order[i][1]);

                    /*
                    // 이진 탐색으로 idx찾는다
                    // 다시 보니 해당 방법은 비효율적이라 주석 처리
                    int l = 0;
                    int r = i - 1;
                    while (l <= r)
                    {

                        int mid = (l + r) / 2;
                        if (time[mid] < chkTime) l = mid + 1;
                        else r = mid - 1;
                    }
                    int idx = r + 1;
                    for (int j = idx; j < i; j++)
                    {

                        inActive[j] = true;
                    }
                    */

                    for (int j = i - 1; j >= 0; j--)
                    {

                        if (time[j] < chkTime) break;
                        inActive[j] = true;
                    }
                }
            }

            // 결과값 얻기
            StringBuilder sb = new();
            for (int i = 0; i < test; i++)
            {

                if (inActive[i]) continue;

                if (order[i][0] == TYPE)
                {

                    sb.Append(order[i][1]);
                }
            }

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.Write(sb);
            }
        }
    }
}
