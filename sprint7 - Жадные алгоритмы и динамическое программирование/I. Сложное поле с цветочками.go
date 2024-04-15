package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
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

func writeOutput(result int, path string) {
	f, err := os.Create("output.txt")
	check(err)
	_, err = fmt.Fprintf(f, "%v\n", result)
	check(err)
	_, err = fmt.Fprint(f, path)
	check(err)
	defer func() { err := f.Close(); check(err) }()
}

func traversePath(n, m int, dp [][]int) string {
	var path strings.Builder
	i, j := 0, m-1
	for i >= 0 && j >= 0 {
		left, bot := -1, -1
		if j > 0 {
			left = dp[i][j-1]
		}
		if i < n-1 {
			bot = dp[i+1][j]
		}
		if left == bot && bot == -1 {
			break
		}
		if bot > left {
			path.WriteRune('U')
			i++
		} else {
			path.WriteRune('R')
			j--
		}
	}

	rns := []rune(path.String())
	for i, j := 0, len(rns)-1; i < j; i, j = i+1, j-1 {
		rns[i], rns[j] = rns[j], rns[i]
	}

	return string(rns)
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

	path := traversePath(n, m, dp)

	writeOutput(dp[0][m-1], path)
}
