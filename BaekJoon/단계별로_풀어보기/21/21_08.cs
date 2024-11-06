using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 19
이름 : 배성훈
내용 : 서로 다른 부분 문자열의 개수
    문제번호 : 11478번
*/

namespace BaekJoon._21
{
    internal class _21_08
    {

        static void Main8(string[] args)
        {

            string str = Console.ReadLine();

            HashSet<string> set = new HashSet<string>(1000000);
            HashSet<string> set2 = new HashSet<string>();
            // 느린 방법!
            for (int i = 1; i <= str.Length; i++)
            {

                for (int j = 0; j + i <= str.Length; j++)
                {

                    set.Add(str.Substring(j, i));
                }
            }

            // 다른 방법
            for (int i = 1; i <= str.Length; i++) 
            {

                foreach (string s in GetSubString(str, i))
                {

                    set2.Add(s);
                }
            }
            Console.WriteLine(set.Count);
            Console.WriteLine(set2.Count);

            /*
            // 문제를 잘못 이해해서 작성한 코드

            int[] nums = new int[26];

            for (int i = 0; i < input.Length; i++)
            {

                nums[input[i] - 'a']++;
            }

            long result = 1;
            long sum = input.Length;

            for (int i = 0; i < nums.Length; i++)
            {

                for (int j = 1; j <= nums[i]; j++) 
                {

                    result *= sum--;
                    result /= j;
                }
            }

            Console.WriteLine(result);
            */
        }

        public static IEnumerable<string> GetSubString(string str, int len)
        {

            /*
            // 다른 사람의 원본 코드
            char[] chars = str.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {

                int startIndex = i;
                int endIndex = i + len;

                if (endIndex > chars.Length)
                {

                    break;
                }


                char[] subChars = new char[len];

                for (int j = startIndex; j < endIndex; j++)
                {

                    subChars[j - i] = chars[j];
                }

                yield return new string(subChars);
            }
            */

            // 이 방법을 쓰니 속도 향상은 없다
            // 아마 Substring 메소드가 아닌 char[]로 넣어줘야 속도 향상이 있을거 같다
            for (int i = 0; i + len <= str.Length; i++)
            {

                yield return str.Substring(i, len);
            }
        }
    }
}

/*
// 람다식 안에는 yield를 사용할 수 없다
// 아래는 못쓰는 시도했지만 못쓰는 코드..
for (int i = 1; i <= str.Length; i++)
{

    Func<string, int, IEnumerable<string>> getString
        = (string str, int len) =>
        {

            char[] chars = str.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {

                int startIndex = i;
                int endIndex = i + len;
                if (endIndex > chars.Length) break;

                char[] subChars = new char[len];
                for (var j = startIndex; j < endIndex; j++)
                {

                    subChars[j - 1] = chars[j];
                }
                yield return new string(subChars);
            }
        };

                
}
*/

