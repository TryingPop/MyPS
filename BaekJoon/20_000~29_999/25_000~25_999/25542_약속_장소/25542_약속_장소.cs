using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 10
이름 : 배성훈
내용 : 약속 장소
    문제번호 : 25542번

    접근 방법을 못찾아서 검색했다
    하나의 기준을 정하고 해당 문자를 1개씩 바꿔가며 다른 것들을 비교한다
    그러면 20 * 26 * 20 * 20번 연산을 통해 조건에 맞는 문자열을 찾을 수 있다
    (최대 연산에 수는 전체 바꿀 문자열의 인덱스, 바꿀 문자 알파벳, 다른 문자열의 개수, 확인할 문자열의 인덱스)

    코드가 많이 복잡해 보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0012
    {

        static void Main12(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int chkLen = ReadInt(sr);
            int strLen = ReadInt(sr);

            // 기준 문자열
            string std = sr.ReadLine();

            // 이외 검증할 문자열
            string[] chk = new string[chkLen - 1];

            for (int i = 0; i < chkLen - 1; i++)
            {

                chk[i] = sr.ReadLine();
            }
            sr.Close();

            // 결과
            char[] result = new char[strLen];
            for (int i = 0; i < strLen; i++)
            {

                // 기준 문자열을 담는다
                result[i] = std[i];
            }

            // 성공?
            bool success = true;
            // 기준이 되는 문자열의 바꿀 인덱스
            for (int idx = 0; idx < strLen; idx++)
            {

                // 바꿀 알파벳
                for (char change = 'A'; change <= 'Z'; change++)
                {

                    // 해당 자리 알파벳 변경
                    result[idx] = change;
                    // 찾았는지 여부
                    success = true;
                    
                    // 검사 시작
                    for (int i = 0; i < chkLen - 1; i++)
                    {

                        // 해당 문자에서 기준과 다른 것들 개수
                        int diff = 0;
                        for (int j = 0; j < strLen; j++)
                        {

                            // 자리마다 비교 시작
                            if (result[j] != chk[i][j])
                            {

                                // 다른거 있으면 1개 추가
                                diff++;
                                // 한 문자에서 다른게 2개 이상 발견되면 탈출
                                if (diff > 1) break;
                            }
                        }

                        // 2개 이상인게 있으므로 다른 검증할 문자열은 검증 안하고 문자열을 바꾸러 간다
                        if (diff > 1) 
                        {

                            // 해당 경우 실패라 하고 문자열을 바꾸러 간다
                            success = false;
                            break; 
                        }
                    }

                    // 검증할 문자열을 모두 통과 했다
                    // 알파벳 바꾸지 않는다
                    if (success) break;
                }

                // 찾았으므로 해당 문자열을 반환하게 한다
                if (success) break;

                // 못찾았으므로 문자열 다음으로 넘어간다
                result[idx] = std[idx];
            }

            // 문자열을 찾은 경우
            if (success)
            {

                // 찾은 것을 반환
                for (int i = 0; i < strLen; i++)
                {

                    Console.Write(result[i]);
                }
            }
            // 못찾았다!
            else Console.Write("CALL FRIEND");
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;
            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                ret *= 10;
                ret += c - '0';
            }

            return ret;
        }
    }
}
