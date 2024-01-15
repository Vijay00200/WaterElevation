var inputArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11, 12,13,14,15,16,17,18,19,20,21,22,23,24 };

// Get the odd n square dimension from the input
int n = Get2DArrayDimension(inputArray.Length);

// create n*n matrix and result matrix, pad missing value
var oddSquareMatrix = new int[n, n];
var resultMatrix = new string[n, n];

//assign the value in matrix
for (int x = 0; x < n; x++)
{
    for (int y = 0; y < n; y++)
    {
        oddSquareMatrix[x, y] = GetInputArrayItems(inputArray, n, x, y);
        resultMatrix[x, y] = ".";
    }
}

// get the starting point for water flow mid of first row
var (row, col)  = new Tuple<int, int>(0, (n - 1) / 2);

var hasReachedEnd = false;

while (!hasReachedEnd)
{
    // Get the water path till it get stagnated
    (row, col) = GetWaterPath(row, col);

    // has reached end then exit
    if (row == n - 1)
    {
        hasReachedEnd = true;
        UpdateResultMatrix(row,col);
    }
    else
    {
        // increase waterlevel
        oddSquareMatrix[row, col] += 1;

        UpdateResultMatrix(row,col);
    }

}

// print result
Print2DArray(n, oddSquareMatrix);
Print2DArray(n, resultMatrix);

void UpdateResultMatrix(int x, int y)
{
    if(resultMatrix[x,y].ToString().Equals("."))
    {
        resultMatrix[x,y] = "W";
    }
    else
    {
        resultMatrix[x, y] = resultMatrix[x, y] + "W";
    }
}

(int, int) GetWaterPath(int x, int y)
{
    var elevation = oddSquareMatrix[x, y];

    var canflow = true;

    while (canflow)
    {
        // can flow down
        if (x + 1 < n && oddSquareMatrix[x + 1, y] <= elevation)
        {
            x += 1;
            oddSquareMatrix[x, y] = elevation;
        }

        // can flow left
        else if (y - 1 >= 0 && oddSquareMatrix[x, y - 1] <= elevation)
        {

            y -= 1;
            oddSquareMatrix[x, y] = elevation;
        }
        else
        {
            canflow = false;
        }
    }

    return (x, y);
}

int GetInputArrayItems(int[] inputArray, int n, int x, int y)
{
    if (inputArray.Length > (x * n) + y)
    {
        return inputArray[(x * n) + y];
    }

    return 0;
}


int Get2DArrayDimension(int length)
{
    int n = 1;
    for (int i = 1; i < 9; i++)
    {
        // odd number
        if (i % 2 != 0)
        {
            if (i * i >= length)
            {
                n = i;
            }
        }

        if (n > 1)
            break;
    }

    return n;
}

void Print2DArray<T>(int n, T[,] oddSquareMatrix)
{
    for (int x = 0; x < n; x++)
    {
        for (int y = 0; y < n; y++)
        {
            Console.Write(oddSquareMatrix[x, y]);
            Console.Write(" ");
        }

        Console.WriteLine();
    }
}