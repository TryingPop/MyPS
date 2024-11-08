# C#

## 난이도 : 실버 2

## 알고리즘 분류
  - 브루트포스 알고리즘

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 64 MB

## 문제
웅찬이는 근성이 대단한 도둑이다. 그래서 금고를 털 때, 모든 조합을 눌러본다. 예를 들어 비밀번호가 3글자 라는 사실을 알 때, 000, 001, 002, 003, … 998, 999의 모든 조합을 눌러본다. 그러나 웅찬이는 선견지명이 있어서 비밀번호에 어떤 숫자가 들어가는지 일부 알 수 있다. 예를 들어 3글자 비밀번호에 0이 들어감을 안다면 999 와 같이 0이 들어가지 않는 수는 가능성이 없다. 그러나 000, 012, 030과 같은 수는 가능하다. 비밀번호의 길이와 선견지명으로 알게된 비밀번호의 일부 숫자가 주어질 때, 모든 가능한 비밀번호의 개수를 출력하는 프로그램을 작성하시오.<br/>

## 입력
첫줄에 비밀번호의 길이 n (1 ≤ n ≤ 7), 선견지명으로 알게된 비밀번호에 들어가는 수 m(0 ≤ m ≤ n) 이 주어지고, 둘째 줄에 m개의 서로 다른 숫자(0~9)가 주어진다. m이 0인 경우 둘째 줄은 주어지지 않는다.<br/>

## 출력
가능한 모든 비밀번호의 개수를 출력한다.<br/>

## 예제 입력
2 1<br/>
7<br/>

## 예제 출력
19<br/>

## 힌트
가능한 비밀번호의 조합은 07, 17, 27, 37, 47, 57, 67, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 87, 97이다.<br/>

## 풀이
자리수가 많아야 1000만 이므로 모든 수에 대해 해당 숫자가 있는지 탐색했다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/13908