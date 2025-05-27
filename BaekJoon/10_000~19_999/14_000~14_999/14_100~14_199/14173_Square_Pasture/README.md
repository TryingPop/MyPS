# C#

## 난이도 : 브론즈 3

## 알고리즘 분류
  - 수학
  - 사칙연산

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
Farmer John has decided to update his farm to simplify its geometry. Previously, his cows grazed in two rectangular fenced-in pastures. Farmer John would like to replace these with a single square fenced-in pasture of minimum size that still covers all the regions of his farm that were previously enclosed by the former two fences.<br/>
Please help Farmer John figure out the minimum area he needs to make his new square pasture so that if he places it appropriately, it can still cover all the area formerly covered by the two older rectangular pastures. The square pasture should have its sides parallel to the x and y axes.<br/>


## 입력
The first line in the input file specifies one of the original rectangular pastures with four space-separated integers x1 y1 x2 y2, each in the range 0…10. The lower-left corner of the pasture is at the point (x1,y1), and the upper-right corner is at the point (x2,y2), where x2>x1 and y2>y1.<br/>
The second line of input has the same 4-integer format as the first line, and specifies the second original rectangular pasture. This pasture will not overlap or touch the first pasture.<br/>

## 출력
The output should consist of one line containing the minimum area required of a square pasture that would cover all the regions originally enclosed by the two rectangular pastures.<br/>

## 예제 입력
6 6 8 8<br/>
1 8 4 9<br/>

## 예제 출력
49<br/>

## 힌트
In the example above, the first original rectangle has corners (6,6) and (8,8). The second has corners at (1,8) and (4,9). By drawing a square fence of side length 7 with corners (1,6) and (8,13), the original areas can still be enclosed; moreover, this is the best possible, since it is impossible to enclose the original areas with a square of side length only 6. Note that there are several different possible valid placements for the square of side length 7, as it could have been shifted vertically a bit.<br/>

## 풀이
두 직사각형 전체를 포함하는 가장 작은 직사각형을 찾는다.<br/>
그리고 해당 직사각형을 포함하는 가장 작은 정사각형을 찾아 풀었다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/14173