# C#

## 난이도 : 실버 4

## 알고리즘 분류
  - 수학
  - 브루트포스 알고리즘

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
  - 3 ≤ N ≤ 1000.


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
즉 길이 2는 항상 존재한다.<br/>


이제 가장 긴 길이가 되는 것을 찾는데 수의 범위가 1000으로 길이는 길어야 9를 넘기지 못한다.<br/>
실제로 x^9 + x^8 + ... + x^0 ≥ 2^9 + 2^8 + ... + 2^0 = 1023이다.<br/>


각 x에 대해 y(k) = x^k + x^(k-1) + x^(k-2) + ... + 1 ≤ 1_000인 모든 k에대해 y(k)의 값을 작은 값으로 갱신 해줬다.<br/>
값이 작을수록 길이가 긴 것은 순서공리로 자명하기 때문이다.<br/>


이렇게 1000이하의 모든 수를 찾고 쿼리에서는 저장된 배열에 있는 값을 출력했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/14292