package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func main() {
	r, err := os.Open("input.txt")
	check(err)
	w, err := os.Create("output.txt")
	check(err)
	defer func() { err := w.Close(); check(err) }()

	scanner := bufio.NewScanner(r)
	scanner.Scan()

	result := 0

	var total, maxMass int
	fmt.Sscanf(scanner.Text(), "%d %d", &total, &maxMass)

	weights := make([]int, total)
	scanner.Scan()
	totalStr := scanner.Text()
	weightsStr := strings.Fields(totalStr)
	for i := 0; i < total; i++ {
		weights[i], err = strconv.Atoi(weightsStr[i])
		check(err)
	}
	// fmt.Println(total, maxMass, weights)

	dp := make([][]int, 2)
	for i := 0; i < 2; i++ {
		dp[i] = make([]int, maxMass+1)
	}

	for i := 0; i < total; i++ {
		weight := weights[i]
		for j := 0; j < maxMass+1; j++ {
			take, noTake := 0, 0

			if j < weight {
				dp[1][j] = dp[0][j]
				continue
			}

			// no take
			noTake = dp[0][j]
			take = weight + dp[0][j-weight]

			dp[1][j] = max(take, noTake)
		}
		// fmt.Println(dp[1])
		dp[0], dp[1] = dp[1], dp[0]
	}

	//fmt.Println(dp)

	result = dp[0][len(dp[0])-1]

	fmt.Fprintf(w, "%v\n", result)
}
