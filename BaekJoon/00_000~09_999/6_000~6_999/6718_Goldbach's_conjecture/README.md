# C#

## 난이도 : 실버 1

## 알고리즘 분류
  - 수학
  - 브루트포스 알고리즘
  - 정수론
  - 소수 판정
  - 에라토스테네스의 체

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
For any even number n greater than or equal to 4, there exists at least one pair of prime numbers p1 and p2 such that<br/>
n = p1 + p2<br/>
This conjecture has not been proved nor refused yet. No one is sure whether this conjecture actually holds. However, one can find such a pair of prime numbers, if any, for a given even number. The problem here is to write a program that reports the number of all the pairs of prime numbers satisfying the condition in the conjecture for a given even number.<br/>
A sequence of even numbers is given as input. There can be many such numbers. Corresponding to each number, the program should output the number of pairs mentioned above. Notice that we are interested in the number of essentially different pairs and therefore you should not count (p1, p2) and (p2, p1) separately as two different pairs.<br/>

## 입력
An integer is given in each input line. You may assume that each integer is even, and is greater than or equal to 4 and less than 2^15. The end of the input is indicated by a number 0.<br/>

## 출력
Each output line should contain an integer number. No other characters should appear in the output.<br/>

## 예제 입력
6<br/>
10<br/>
12<br/>
0<br/>

## 예제 출력
1<br/>
2<br/>
1<br/>

## 풀이
해당 범위안의 모든 소수를 찾고 a가 소수일 때 찾을 수 n에 대해 n - a가 소수인지 확인하며 경우의 수를 세어 풀었다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/6718