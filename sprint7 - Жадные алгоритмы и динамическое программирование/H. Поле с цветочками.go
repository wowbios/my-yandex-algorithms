package main

import (
	"bufio"
	"fmt"
	"os"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func readInput() (n, m int, data [][]int) {
	r, err := os.Open("input.txt")
	check(err)
	scanner := bufio.NewScanner(r)
	if scanner.Scan() {
		_, err := fmt.Sscanf(scanner.Text(), "%d %d", &n, &m)
		check(err)
	}
	data = make([][]int, n)
	for i := range data {
		data[i] = make([]int, m)
	}

	for i := 0; scanner.Scan() && i < n; i++ {
		for j, value := range scanner.Text() {
			data[i][j] = int(value - '0')
		}
	}

	err = scanner.Err()
	check(err)

	return n, m, data
}

func writeOutput(result int) {
	f, err := os.Create("output.txt")
	check(err)
	_, err = fmt.Fprint(f, result)
	check(err)
	defer func() { err := f.Close(); check(err) }()
}

func main() {
	n, m, data := readInput()
	dp := make([][]int, len(data))
	for i := 0; i < len(data); i++ {
		dp[i] = make([]int, len(data[i]))
	}

	for i := n - 1; i >= 0; i-- {
		for j := 0; j < m; j++ {
			// check left
			var left, bot int
			if j > 0 {
				left = dp[i][j-1]
			}
			if i < n-1 {
				bot = dp[i+1][j]
			}
			add := max(bot, left)
			dp[i][j] = data[i][j] + add
		}
	}
	fmt.Println(data)
	fmt.Println(dp)

	writeOutput(dp[0][m-1])
}
