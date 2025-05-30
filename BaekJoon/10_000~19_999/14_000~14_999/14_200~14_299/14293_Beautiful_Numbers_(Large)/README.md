# C#

## 난이도 : 골드 3

## 알고리즘 분류
  - 수학
  - 브루트포스 알고리즘
  - 이분 탐색

## 제한조건
  - 시간 제한 : 5초
  - 메모리 제한 : 512 MB

## 문제
We consider a number to be beautiful if it consists only of the digit 1 repeated one or more times. Not all numbers are beautiful, but we can make any base 10 positive integer beautiful by writing it in another base.<br/>
Given an integer N, can you find a base B (with B > 1) to write it in such that all of its digits become 1? If there are multiple bases that satisfy this property, choose the one that maximizes the number of 1 digits.<br/>


## 입력
The first line of the input gives the number of test cases, T. T test cases follow. Each test case consists of one line with an integer N.<br/>
Limits<br/>

  - 1 ≤ T ≤ 100.
  - 3 ≤ N ≤ 10^18.


## 출력
For each test case, output one line containing Case #x: y, where x is the test case number (starting from 1) and y is the base described in the problem statement.<br/>


## 예제 입력
2<br/>
3<br/>
13<br/>


## 예제 출력
Case #1: 2<br/>
Case #2: 3<br/>


## 힌트
In case #1, the optimal solution is to write 3 as 11 in base 2.<br/>
In case #2, the optimal solution is to write 13 as 111 in base 3. Note that we could also write 13 as 11 in base 12, but neither of those representations has as many 1s.<br/>


## 풀이
3 이상인 n에 대해 n - 1진법으로 표현하면 11로 표현가능하다.<br/>
그래서 언제나 해는 존재한다.<br/>


진법의 값이 작아질수록 길이가 길어진다.<br/>
그래서 1111로 되는게 존재한다면 값이 작은쪽으로 변형시키면 길이가 최대임이 보장된다.<br/>


이제 길이가 증가하는 경우를 찾아야 한다.<br/>
이는 이분 탐색으로 직접 연산을 하며 찾았다.<br/>
다만 길이 40인 경우 3을 확인하면 ulong 범위를 명백히 벗어난다.<br/>
그래서 끝 값의 범위를 런타임 전 전처리로 먼저 찾았다.<br/>


전처리로 찾은 길이와 연산에 쓰는 식이 잘못되어 2번 틀렸다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/14293