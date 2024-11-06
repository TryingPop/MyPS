using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 3
이름 : 배성훈
내용 : 45도
    문제번호 : 2949번

    구현 문제다
    출력 형식으로 한 번 틀렸다, 알파벳으로 끝나야한다 즉, 뒤에 \n이 와야한다
    처음에는 뒤에서 알파벳이 언제 나오는지 확인할까 했으나, 
    string의 TrimEnd 메서드를 이용했다

    180도 뒤집어진 부분은 해당 배열의 인덱스를 끝에서 읽었다
    이렇게 구현하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0440
    {

        static void Main440(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int r = ReadInt();
            int c = ReadInt();

            string[] square = new string[r];
            for (int i = 0; i < r; i++)
            {

                square[i] = sr.ReadLine();
            }

            int degree = ReadInt() / 45;
            sr.Close();

            degree %= 8;
            int type = degree % 4;
            char[,] ret;
            if (type == 0)
            {

                ret = new char[r, c];
                if (degree == 0)
                {

                    for (int i = 0; i < r; i++)
                    {

                        for (int j = 0; j < c; j++)
                        {

                            ret[i, j] = square[i][j];
                        }
                    }
                }
                else
                {

                    for (int i = 0; i < r; i++)
                    {

                        for (int j = 0; j < c; j++)
                        {

                            ret[i, j] = square[r - 1 - i][c - 1 - j];
                        }
                    }
                }
            }
            else if (type % 4 == 2)
            {

                ret = new char[c, r];
                if (degree == 2)
                {

                    for (int i = 0; i < c; i++)
                    {

                        for (int j = 0; j < r; j++)
                        {

                            ret[i, r - 1 - j] = square[j][i];
                        }
                    }
                }
                else
                {

                    for (int i = 0; i < c; i++)
                    {

                        for (int j = 0; j < r; j++)
                        {

                            ret[c - 1 - i, j] = square[j][i];
                        }
                    }
                }
            }
            else if (type % 4 == 1)
            {

                ret = new char[r + c - 1, r + c - 1];
                if (degree == 1)
                {

                    for (int i = 0; i < r; i++)
                    {

                        for (int j = 0; j < c; j++)
                        {

                            ret[i + j, r - 1 - i + j] = square[i][j];
                        }
                    }
                }
                else
                {

                    for (int i = 0; i < r; i++)
                    {

                        for (int j = 0; j < c; j++)
                        {

                            ret[r + c - 2 - i - j, c - 1 - j + i] = square[i][j];
                        }
                    }
                }
            }
            else
            {

                ret = new char[r + c - 1, r + c - 1];
                if (degree == 3)
                {

                    for (int i = 0; i < r; i++)
                    {

                        for (int j = 0; j < c; j++)
                        {

                            ret[r - 1 - i + j, r + c - 2 - i - j] = square[i][j];
                        }
                    }
                }
                else
                {

                    for (int i = 0; i < r; i++)
                    {

                        for (int j = 0; j < c; j++)
                        {

                            ret[c - 1 - j + i, j + i] = square[i][j];                        
                        }
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < ret.GetLength(0); i++)
                {
                    string str = "";


                    for (int j = 0; j < ret.GetLength(1); j++)
                    {

                        if (ret[i, j] == '\0') str += " ";
                        else str += ret[i, j];
                    }

                    str = str.TrimEnd();
                    sw.WriteLine(str);
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
}
