# C#

## 난이도 : 브론즈 2

## 알고리즘 분류
  - 구현
  - 문자열

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 512 MB

## 문제
문자열 S의 부분 문자열이란, 문자열의 연속된 일부를 의미한다.<br/>
예를 들어, "aek", "joo", "ekj"는 "baekjoon"의 부분 문자열이고, "bak", "p", "oone"는 부분 문자열이 아니다.<br/>
문자열 S와 P가 주어졌을 때, P가 S의 부분 문자열인지 아닌지 알아보자.<br/>

## 입력
첫째 줄에 문자열 S, 둘째 줄에 문자열 P가 주어진다. 두 문자열은 빈 문자열이 아니며, 길이는 100만을 넘지 않는다. 또, 알파벳 소문자로만 이루어져 있다.<br/>

## 출력
P가 S의 부분 문자열이면 1, 아니면 0을 출력한다.<br/>

## 예제 입력
baekjoon<br/>
aek<br/>

## 예제 출력
1<br/>

## 풀이
kmp 알고리즘을 직접 구현해 풀었다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/16916