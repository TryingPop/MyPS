# C#

## 난이도 : 실버 3

## 알고리즘 분류
  - 수학
  - 브루트포스 알고리즘

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
Mr. Port plans to start a new business renting one or more floors of the new skyscraper with one giga floors, MinatoHarukas. He wants to rent as many vertically adjacent floors as possible, because he wants to show advertisement on as many vertically adjacent windows as possible. The rent for one floor is proportional to the floor number, that is, the rent per month for the n-th floor is n times that of the first floor. Here, the ground floor is called the first floor in the American style, and basement floors are out of consideration for the renting. In order to help Mr. Port, you should write a program that computes the vertically adjacent floors satisfying his requirement and whose total rental cost per month is exactly equal to his budget.<br/>
For example, when his budget is 15 units, with one unit being the rent of the first floor, there are four possible rent plans, 1+2+3+4+5, 4+5+6, 7+8, and 15. For all of them, the sums are equal to 15. Of course in this example the rent of maximal number of the floors is that of 1+2+3+4+5, that is, the rent from the first floor to the fifth floor.<br/>

## 입력
The input consists of multiple datasets, each in the following format.<br/>


	b


A dataset consists of one line, the budget of Mr. Port b as multiples of the rent of the first floor. b  is a positive integer satisfying 1 < b < 10^9.<br/>
The end of the input is indicated by a line containing a zero. The number of datasets does not exceed 1000.<br/>

## 출력
For each dataset, output a single line containing two positive integers representing the plan with the maximal number of vertically adjacent floors with its rent price exactly equal to the budget of Mr. Port. The first should be the lowest floor number and the second should be the number of floors.<br/>

## 예제 입력
15<br/>
16<br/>
2<br/>
3<br/>
9699690<br/>
223092870<br/>
847288609<br/>
900660121<br/>
987698769<br/>
999999999<br/>
0<br/>

## 예제 출력
1 5<br/>
16 1<br/>
2 1<br/>
1 2<br/>
16 4389<br/>
129 20995<br/>
4112949 206<br/>
15006 30011<br/>
46887 17718<br/>
163837 5994<br/>

## 풀이
연속된 수열의 전체합은 가우스 일화로 알려진 방법을 사용하면<br/>
(연속된 수열의 합) = (양 끝값의 합) x (길이) / 2이다.<br/>
그래서 총합이 되는 값에 2배를 곱하고, 약수들을 찾았다.<br/>
그러면 약수는 길이 또는 양 끝값의 합이 된다.<br/>
여기서 길이는 짝수인데, 양 끝값의 합은 짝수가 될 수 없다.<br/>
그리고 두 변수를 이용해 가능한 시작지점과 길이를 찾았다.<br/>
이 중 최대 길이를 출력하니 통과했다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/16236