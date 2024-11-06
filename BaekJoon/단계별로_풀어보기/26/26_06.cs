using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 11. 21
이름 : 배성훈
내용 : 체스판 다시 칠하기 2
    문제번호 : 25682번

    가장 빠른 사람 풀이를 보니, 색칠 해야할 칸은 1로 기록하고,
    색칠 하면 안되는 칸을 0으로 기록했다

    그리고 합 계산을 이용해 풀어서 메모리 사용량도 적고, 속도도 빨라 보인다
*/

namespace BaekJoon._26
{
    internal class _26_06
    {

        static void Main6(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();



            ////////////
            /// 가로 연산
            /// 
            /// 입력값이
            /// info[0] = 4, info[2] = 3, i = 0에서
            /// BBWW 를 입력 받은 경우
            /// 
            /// i = 0 이므로 BWBW 로 칠해졌길 기대한다
            /// BBW 와 BWB와 서로 다른 것의 갯수는 2,
            /// BWW 와 WBW와 서로 다른 것의 갯수는 2,
            /// 
            /// replaceRaws[0] = { 2, 2 } 로 저장한다({ 2, 2 }는 new int[2] { 2, 2 }를 줄인 개인적인 표현이다.)
            /// 
            /// 동적 계획법을 이용해 풀었다
            ////////////
            int[][] replaceRaws = new int[info[0]][];
            bool[] temp = new bool[info[1] - info[2]];

            for (int i = 0; i < info[0]; i++)
            {

                // 입력
                char[] board = sr.ReadLine().ToCharArray();
                
                // 가로 연산
                int[] nums = new int[info[1] - info[2] + 1];
                
                int chk = 0;
                
                // 해당 칸이 B ? W 인지 확인용도
                char start = i % 2 == 0 ? 'B' : 'W';
                for (int j = 0; j < info[1]; j++)
                {

                    // 다른 경우 색칠해야한다
                    if (start != board[j])
                    {

                        chk++;

                        // 해당 번째가 색칠한 곳인지 확인
                        if (j < temp.Length)
                        {

                            temp[j] = true;
                        }
                    }

                    // 기록해야 하는지 판별
                    int idx = j - info[2] + 1;
                    if (idx >= 0)
                    {

                        // 기록
                        nums[idx] = chk;

                        // 해당 장소가 제거해야하는지 판별
                        if (idx < temp.Length)
                        {

                            // 앞에꺼만 빼주면 중간 과정은 다시 실행 안해도 되기에 확인해서 뺀다
                            // 동적 계획법 적용
                            if (temp[idx]) 
                            {

                                chk--; 
                                temp[idx] = false;
                            }
                        }
                    }

                    start = start == 'B' ? 'W' : 'B';
                }

                replaceRaws[i] = nums;
            }

            sr.Close();

            /////////////
            /// 세로 연산
            /// 앞에서 실행한 가로 연산의 값을 이용한 세로 연산
            /// 
            /// 4 4 3
            /// BBWW
            /// BBBB
            /// BBWW
            /// WBWB 
            /// 
            /// 를 입력 받은 경우
            /// 가로 연산을 이용해
            /// 
            /// replaceRaw는 
            /// replaceRaw[0] = { 2, 2 }
            /// replaceRaw[1] = { 1, 2 }
            /// replaceRaw[2] = { 2, 2 }
            /// replaceRaw[3] = { 0, 0 }
            /// 
            /// 이를 이용해 2개씩 연산한 값을 한다
            /// 0, 0 에서 시작한 3 X 3 보드에 색칠해야 하는 갯수는 2 + 1 + 2 = 5개,
            /// 0, 1 에서 시작한 3 X 3 보드에 색칠해야 하는 개수는 2 + 2 + 2 = 6개,
            /// 1, 0 에서 시작한 3 X 3 보드에 색칠해야 하는 개수는 1 + 2 + 0 = 3개,
            /// 1, 1 에서 시작한 3 X 3 보드에 색칠해야 하는 개수는 2 + 2 + 2 = 4개,
            /// 
            /// 그런데 9개 중에 5개를 색칠하는 것보다 9개 중에 4개를 색칠하는게 더 적으므로
            /// 0, 0 은 9 - 5 = 4개
            /// 0, 1 은 9 - 6 = 3개
            /// 로 변환 가능하다
            /// 이를 담는 것이 replace 이다
            /// replace[0] = { 4, 3 }
            /// replace[1] = { 3, 4 }
            /// 가 될 것이다
            /// 
            /// 마찬가지로 동적 계획법을 이용해 계산한다
            /////////////
            int[][] replace = new int[info[0] - info[2] + 1][];

            int[] calc = new int[replaceRaws[0].Length];
            int boardNum = info[2] * info[2];
            int min = boardNum;
            for (int i = 0; i < info[0]; i++)
            {

                for (int j = 0; j < calc.Length; j++)
                {

                    calc[j] += replaceRaws[i][j];
                }

                int idx = i - info[2] + 1;
                if (idx >= 0)
                {

                    replace[idx] = new int[calc.Length];
                    for (int j = 0; j < calc.Length; j++)
                    {

                        replace[idx][j] = calc[j] < boardNum - calc[j] ? calc[j] : boardNum - calc[j];
                        int chk = replace[idx][j];

                        if (chk < min) min = chk;
                        
                        // 동적 계획법 적용
                        calc[j] -= replaceRaws[idx][j];
                    }
                }
            }

            Console.WriteLine(min);
        }
    }
}
