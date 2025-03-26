# C#

## 난이도 : 실버 2

## 알고리즘 분류
  - 구현

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Matheos the magician wishes to organise her considerable collection of squares into categories based on the amount of magical power that each one possesses. A square is a table of numbers with an equal number of rows and columns and each square has an associated sum (S), the sum of the first column. Matheos wishes to categorise them based on the following properties:<br/>
Not Magick : At least one row or column does not sum to S.<br/>
Semi-Magick Square : Each row and column sums to S but at least one of the diagonals doesn’t.<br/>
Weakly-Magick Square : All the rows, columns and diagonals sum to S.<br/>
Strongly-Magick Square : All the rows, columns and diagonals sum to S, and the numbers are distinct (i.e. no number is duplicated).<br/>
Magically-Magick Square : All the rows, columns and diagonals sum to S, the numbers are distinct and they are consecutive (i.e. they form a sequence with no gaps).<br/>
Thus<br/>

|2|3|
|:---:|:---:|
|3|2|

is a Semi-Magick square because each row and column adds up to 5, but the diagonals don’t.<br/>
and<br/>

|8|1|6|
|:---:|:---:|:---:|
|3|5|7|
|4|9|2|

is a Magically-Magick square because each row, column and diagonal adds up to 15, each number is distinct, and the numbers are consecutive (i.e. 1 2 3 4 5 6 7 8 9).<br/>
Your task is to help Matheos by writing a program to read and evaluate a sequence of squares. For each square your program is to output a message to say whether it is Magically-Magick, Strongly-Magick, Weakly-Magick, Semi-Magick, or has no magical properties at all.<br/>


## 입력
Input will consist of a sequence of squares. Each square starts with an integer, n, specifying the square’s size (number of rows and columns; 2 ≤ n ≤ 8) on a line by itself, followed by n lines, each with n integers. The sequence of squares is terminated by a line containing a zero (0).<br/>


## 출력
Output for each square is a single line starting with the word ‘Square’ followed by a space, a sequence number (starting at 1) and a colon (:). This is then followed by another space and then one of following massages ‘Magically-Magick Square’, ‘Strongly-Magick Square’, ‘Weakly-Magick Square’, ‘Semi-Magick Square’, or ‘Not a Magick Square’ as appropriate.<br/>


## 예제 입력
2<br/>
1 1<br/>
1 2<br/>
2<br/>
-2 -3<br/>
-3 -2<br/>
3<br/>
8 1 6<br/>
3 5 7<br/>
4 9 2<br/>
0<br/>


## 예제 출력
Square 1: Not a Magick Square<br/>
Square 2: Semi-Magick Square<br/>
Square 3: Magically-Magick Square<br/>


## 풀이
조건을 하나씩 풀어 판별하면 된다.<br/>
먼저 공통된것이 가로 세로 합이 모두 같은지 확인해야한다.<br/>
그래서 기준 0번 행을 잡고 0번행의 합과 같은지 확인했다.<br/>
여기서 1개라도 다른게 있으면 매직 사각형이 아니다.<br/>


다음으로 좌, 우 대각선을 판별했다.<br/>
여기서 다르다면 반 마법 사각형이다.<br/>


다음으로 강한 마법 사각형과 마법적인 마법 사각형을 보면 공통적으로 모든 원소가 다르다.<br/>
이는 사각형안의 원소들을 배열로 나열하고 정렬했을 때 1항부터 이전항과 다른지 모두 확인하면 된다.<br/>
여기서 같은게 발견되면 약한 마법 사각형이 된다.<br/>
그리고 1차이가 나는지 확인해 마법적 사각형인지 확인하면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/2189