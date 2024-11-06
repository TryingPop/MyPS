using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 1
이름 : 배성훈
내용 : 간판
    문제번호 : 5534번

    문자열, 브루트포스 문제다
    4중 포문으로 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0668
    {

        static void Main668(string[] args)
        {

            StreamReader sr;
            Solve();
            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                int n = int.Parse(sr.ReadLine());
                string str = sr.ReadLine();

                int ret = 0;

                for (int i = 0; i < n; i++)
                {

                    string chk = sr.ReadLine();

                    bool find = false;
                    for (int j = 0; j < chk.Length; j++)
                    {

                        if (str[0] != chk[j]) continue;

                        for (int a = 1; a < chk.Length; a++)
                        {

                            find = true;
                            for (int b = 1; b < str.Length; b++)
                            {

                                int next = j + a * b;
                                if (next >= chk.Length || str[b] != chk[next]) 
                                {

                                    find = false;
                                    break; 
                                }
                            }

                            if (find) break;
                        }

                        if (find) break;
                    }

                    if (find) ret++;
                }

                sr.Close();
                Console.WriteLine(ret);
            }
        }
    }

#if other
import java.io.*;
import java.util.*;

public class Main {
	public static void main(String[] args) throws NumberFormatException, IOException {
		BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));

		int T = Integer.parseInt(reader.readLine());
		String str = reader.readLine();
		int strLen = str.length();
		int ans = 0;

		for (int itr = 0; itr < T; itr++) {
			String tmp = reader.readLine();
			int len = tmp.length();

			for (int i = 0; i < len; i++) {
				boolean flag = false;

				if (tmp.charAt(i) == str.charAt(0)) {
					for (int j = 1; i + (strLen - 1) * j < len; j++) {
						flag = true;

						for (int k = 1; k < strLen; k++) {
							if (tmp.charAt(i + j * k) != str.charAt(k)) {
								flag = false;
								break;
							}
						}

						if (flag)
							break;
					}
				}

				if (flag) {
					ans++;
					break;
				}
			}
		}

		System.out.println(ans);
	}
}
#endif
}
