# C#

## 난이도 : 브론즈 4

## 알고리즘 분류
  - 구현

## 제한조건
  - 시간 제한 : 3초
  - 메모리 제한 : 256 MB

## 문제
Blind tea tasting is the skill of identifying a tea by using only your senses of smell and taste.<br/>
As part of the Ideal Challenge of Pure-Tea Consumers (ICPC), a local TV show is organized. During the show, a full teapot is prepared and five contestants are handed a cup of tea each. The participants must smell, taste and assess the sample so as to identify the tea type, which can be: (1) white tea; (2) green tea; (3) black tea; or (4) herbal tea. At the end, the answers are checked to determine the number of correct guesses.<br/>
Given the actual tea type and the answers provided, determine the number of contestants who got the correct answer.<br/>


## 입력
The first line contains an integer T representing the tea type (1 ≤ T ≤ 4). The second line contains five integers A, B, C, D and E, indicating the answer given by each contestant (1 ≤ A, B, C, D, E ≤ 4).<br/>


## 출력
Output a line with an integer representing the number of contestants who got the correct answer.<br/>


## 예제 입력
1<br/>
1 2 3 2 1<br/>


## 예제 출력
2<br/>


## 풀이
정답이 먼저 주어진다.<br/>
이후 5개의 원소에서 정답이 몇 개 있는지 세어서 출력하면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/11549