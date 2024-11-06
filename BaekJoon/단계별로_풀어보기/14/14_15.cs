using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 12
이름 : 배성훈
내용 : LCS
    문제번호 : 9251번
*/

namespace BaekJoon._14
{
    internal class _14_15
    {

        static void Main15(string[] args)
        {

            string str1 = Console.ReadLine();
            string str2 = Console.ReadLine();


            #region 일반적인 풀이 방법
            /*
            int len1 = str1.Length;
            int len2 = str2.Length;

            int[,] memo = new int[len1 + 1, len2 + 1];  // 인덱스 i, j는 str1의 i 번째 위치와 str2 j번째 위치를 의미한다
                                                        // 값은 같은 문자열의 최장 길이
            int max = 0;

            for (int i = 0; i < len1; i++)
            {

                for (int j = 0; j < len2; j++)
                {

                    // 해당 위치에 문자열이 같은지 확인
                    if (str1[i] == str2[j])
                    {

                        // 같으면 각 문자열의 바로 앞위치에서의 최장 문자열 값에 + 1
                        memo[i + 1, j + 1] = memo[i, j] + 1;

                        // 가장긴 것 연산
                        if (max < memo[i + 1, j + 1])
                        {

                            max = memo[i + 1, j + 1];
                        }
                    }
                    else
                    {
                        
                        // str1의 위치는 그대로이고, str2의 위치를 앞으로 한칸 이동한 최장문자열의 길이나
                        // str2의 위치는 그대로이고, str1의 위치를 앞으로 한칸 이동한 최장문자열의 길이 중에
                        // 큰 값이 해당 위치에서 최장문자열의 길이가 된다
                        memo[i + 1, j + 1] = Math.Max(memo[i, j + 1], memo[i + 1, j]);
                    }
                }
            }
            */
            #endregion 일반적인 풀이 방법

            #region 다른사람 풀이 방법
            string big = str1.Length > str2.Length ? str1 : str2;
            string small = str1.Length > str2.Length ? str2 : str1;


            int[] memo = new int[big.Length];
            int max = 0;

            for (int i = 0; i < small.Length; i++)
            {

                // 이전에 같은 문자 확인 이게 Math.Max 역할!
                int preCommon = 0;
                for (int j = 0; j < big.Length; j++)
                {

                    // 이전 문자열의 값을 이어받는다
                    if (memo[j] > preCommon)
                    {

                        preCommon = memo[j];
                        continue;
                    }

                    // 같은 경우 common + 1
                    if (small[i] == big[j])
                    {

                        memo[j] = preCommon + 1;
                    }
                }
            }


            for (int i = 0; i < memo.Length; i++)
            {

                if (max < memo[i])
                {

                    max = memo[i];
                }
            }
            #endregion 다른사람 풀이 방법
            Console.WriteLine(max);
        }

        #region LCS 재귀함수
        /*
        public static int LCS(string str1, string str2)
        {

            // 어느 한쪽의 문자열이 비어있는 경우
            if (str1 == "" || str2 == "")   
            {
                
                // 어느 한쪽이 비워져 있어 공통 문자의 길이는 0
                return 0;
            }

            // 맨앞의 문자열이 같은 경우
            if (str1[str1.Length - 1] == str2[str2.Length - 1])
            // if (str1[0] == str2[0]) 
            {

                // 첫 문자 제외하고  + 1 추가 (같은것의 길이가 1이라는 의미)
                return LCS(str1.Remove(str1.Length - 1), str2.Remove(str2.Length - 1)) + 1;
                // return LCS(str1.Substring(1), str2.Substring(1)) + 1;
            }

            // 같지 않다면 최장 길이는
            // str1 맨앞의 문자를 제외한 나머지에 같은 것을 실행
            // str2 맨앞의 문자를 제외한 나머지에 같은 것을 실행
            // 한 값 중 최대값이 될것이다
            return Math.Max(LCS(str1, str2.Remove(str2.Length - 1)), LCS(str1.Remove(str1.Length - 1), str2));
            // return Math.Max(LCS(str1, str2.Substring(1)), LCS(str1.Substring(1), str2));
        }
        */
        #endregion LCS 재귀함수
    }
}
