# C#

## 난이도 : 실버 2

## 알고리즘 분류
  - 수학
  - 구현
  - 많은 조건 분기

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Consider all positive integers written in the following manner (you can imagine an infinite spiral).<br/>
Your task is to determine the position (row,column) of a given number N, assuming that the center (number 1) has position (0,0). Rows are numbered from top to bottom, columns are numbered from left to right (for example, number 3 is at (1,1). Your program should output a string containing the position of N in the form (R,C) where R is the row and C is the column. R and C must not contain any leading zeroes.<br/>

## 입력
The first line of the input gives an integer T, which is the number of test cases.  Each test case contains an integer N (1 ≤ N<231).<br/>

## 출력
For each test case, output the position as described above. See sample output for further clarification.  <br/>

## 예제 입력
7<br/>
2<br/>
3<br/>
7<br/>
17<br/>
24<br/>
830<br/>
765409<br/>

## 예제 출력
(0,1)<br/>
(1,1)<br/>
(-1,-1)<br/>
(2,-2)<br/>
(-2,1)<br/>
(-14,3)<br/>
(-437,221)<br/>

## 풀이
나선 배치되는 것을 보면 N^2을 지나는 대각선이나 N x (N - 1)을 지나는 대각선과의 상대 거리를 이용해 찾았다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/9714