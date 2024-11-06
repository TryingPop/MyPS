# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 구현
  - 그리디 알고리즘
  - 시뮬레이션

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
After the snowstorm, there is quite the abundance of fresh snow all around. To celebrate the end of the storm, a local school decides to make snow cones with the fresh snow. The school acquires large quantities of two different snow cone syrup flavors, and takes all the kids’ flavor requests. The children then line up and are handed their snow cones. But in all the chaos, the snow cones get mixed up, and the kids are not necessarily handed the flavors that they had requested. Fortunately, the correct numbers of each flavor are handed out, and so the kids decide to trade with each other to get the flavor they want.<br/>
The kids would like to minimize the time it takes until everyone has the flavor they want and thus want to trade as efficiently as possible. During a time step, neighboring kids in line may trade cones, and multiple trades may occur in parallel, but each kid may only participate in at most one trade in that time step.<br/>

## 입력
The first line is the number K of input data sets, followed by the K data sets, each with two lines. The first line in a data set is a sequence of N characters, 1 ≤ N ≤ 1000, indicating the flavors handed out. The second line indicates the flavors that were actually requested and is a sequence of the same N characters, but they may be out of order. The two flavors are always represented as the upper case letters ‘X’ and ‘O’. The counts of the two flavors in the first line will always equal the counts in the second line.<br/>

## 출력
For each data set, output “Data Set x:” on a line by itself, where x is its number. One the next line, output the minimum number of time steps that are required until each kid ends up with the correct flavor. Each data set should be followed by a blank line.<br/>

## 예제 입력
2<br/>
XXO<br/>
OXX<br/>
OXOX<br/>
XOXO<br/>


## 예제 출력
Data Set 1:<br/>
2<br/>
<br/>
Data Set 2:<br/>
1<br/>

## 풀이
조건대로 시뮬레이션 돌려 최적해를 찾으면 된다.<br/>
처음에는 그냥 O끼리 거리만 재면 되지 않을까 했고 이는 한 턴에 한 번만 움직일 수 있다에 막혔다.<br/>


	OOOXX
	XXOOO
	
	Output: 4


그래서 인접한거끼리 누적하면 괜찮지 않을까? 생각했고 제출하니 또 막혔다.<br/>


	XXXXXXXOOOOOOOO
	OOXXXOOOOOOXXXX
	
	Output: 11


이 경우 뒤에 있는 O 2개는 3칸 이동 후 앞에 있는 돌들에 막혀 멈춘다.<br/>
그래서 그냥 돌의 길이가 1000밖에 안되므로 N^2해도 100만이라 시뮬레이션 돌리자 생각했고 제출하니 통과했다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/7014