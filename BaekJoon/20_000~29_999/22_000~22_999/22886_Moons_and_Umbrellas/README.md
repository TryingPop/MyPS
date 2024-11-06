# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 다이나믹 프로그래밍

## 제한조건
  - 시간 제한 : 10초
  - 메모리 제한 : 1024 MB

## 문제
#### Cody-Jamal is working on his latest piece of abstract art: a mural consisting of a row of waning moons and closed umbrellas. Unfortunately, greedy copyright trolls are claiming that waning moons look like an uppercase C and closed umbrellas look like a J, and they have a copyright on CJ and JC. Therefore, for each time CJ appears in the mural, Cody-Jamal must pay X, and for each time JC appears in the mural, he must pay Y.
#### Cody-Jamal is unwilling to let them compromise his art, so he will not change anything already painted. He decided, however, that the empty spaces he still has could be filled strategically, to minimize the copyright expenses.
#### For example, if CJ?CC? represents the current state of the mural, with C representing a waning moon, J representing a closed umbrella, and ? representing a space that still needs to be painted with either a waning moon or a closed umbrella, he could finish the mural as CJCCCC, CJCCCJ, CJJCCC, or CJJCCJ. The first and third options would require paying X+Y in copyrights, while the second and fourth would require paying 2⋅X+Y.
#### Given the costs X and Y and a string representing the current state of the mural, how much does Cody-Jamal need to pay in copyrights if he finishes his mural in a way that minimizes that cost?

## 입력
#### The first line of the input gives the number of test cases, T. T lines follow. Each line contains two integers X and Y and a string S representing the two costs and the current state of the mural, respectively.

## 출력
#### For each test case, output one line containing Case #x: y, where x is the test case number (starting from 1) and y is the minimum cost that Cody-Jamal needs to pay in copyrights for a finished mural.

## 제한
  - 1 ≤ T ≤ 100.
  - Each character of S is either C, J, or ?.
  - 1 ≤ the length of S ≤ 1000.
  - −100 ≤ X ≤ 100.
  - −100 ≤ Y ≤ 100.

## 예제 입력
4<br/>
2 3 CJ?CC?<br/>
4 2 CJCJ<br/>
1 3 C?J<br/>
2 5 ??J???<br/>

## 예제 출력
Case #1: 5<br/>
Case #2: 10<br/>
Case #3: 1<br/>
Case #4: 0<br/>

## 힌트
#### Sample Case #1 is the one explained in the problem statement. The minimum cost is X+Y=2+3=5.
#### In Sample Case #2, Cody-Jamal is already finished, so he does not have a choice. There are two CJs and one JC in his mural.
#### In Sample Case #3, substituting either C or J results in one CJ either from the second and third characters or the first and second characters, respectively.
#### In Sample Case #4, Cody-Jamal can finish his mural with all Js. Since that contains no instance of CJ nor JC, it yields no copyright cost.

## 문제 링크
https://www.acmicpc.net/problem/22886