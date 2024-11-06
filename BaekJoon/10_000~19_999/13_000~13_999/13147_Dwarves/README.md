# C#

## 난이도 : 골드 2

## 알고리즘 분류
  - 그래프 이론
  - 그래프 탐색
  - 위상 정렬
  - 방향 비순환 그래프

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
#### Once upon a time, there arose a huge discussion among the dwarves in Dwarfland. The government wanted to introduce an identity card for all inhabitants.
#### Most dwarves accept to be small, but they do not like to be measured. Therefore, the government allowed them to substitute the field “height” in their personal identity card with a field “relative dwarf size”. For producing the ID cards, the dwarves were being interviewed about their relative sizes. For some reason, the government suspects that at least one of the interviewed dwarves must have lied.
#### Can you help find out if the provided information proves the existence of at least one lying dwarf?

## 입력
#### The input consists of:
  - one line with an integer n (1 ≤ n ≤ 10^5), where n is the number of statements;
  - n lines describing the relations between the dwarves. Each relation is described by:
    - one line with “s1 < s2” or “s1 > s2”, telling whether dwarf s1 is smaller or taller than dwarf s2. s1 and s2 are two different dwarf names.
#### A dwarf name consists of at most 20 letters from “A” to “Z” and “a” to “z”. A dwarf name does not contain spaces. The number of dwarves does not exceed 10^4.

## 출력
#### Output “impossible” if the statements are not consistent, otherwise output “possible”.

## 예제 입력
3<br/>
Dori > Balin<br/>
Balin > Kili<br/>
Dori < Kili<br/>

## 예제 출력
impossible<br/>

## 문제 링크
https://www.acmicpc.net/problem/13147