using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphAll
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Вы хотите ввести свой список соседей, или использовать пример? " +
                              "1 - ввести свой" +
                              " 2 - использовать пример");

            var choice = int.Parse(Console.ReadLine());

            Graph graph = new Graph();

            if (choice == 1)
            {
                Console.WriteLine("Введите количество вершин графа");
                var count = int.Parse(Console.ReadLine());

                var adjList = new int[count][];

                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine("Введите размер текущей строки");
                    var c = int.Parse(Console.ReadLine());
                    adjList[i] = new int[c];
                    Console.WriteLine("Введите список соседей (строка {0} )", i);
                    var enterString = Console.ReadLine();
                    var massiveString = enterString.Split(' ');

                    for (int j = 0; j < massiveString.Length; j++)
                    {
                        adjList[i][j] = int.Parse(massiveString[j]);
                    }
                }

                graph.setDots(adjList);
            }
            else
            {
                int[][] adjList =
                {
                    new[] {1, 2},
                    new[] {0, 3, 4},
                    new[] {0, 6},
                    new[] {1, 5},
                    new[] {1},
                    new[] {3},
                    new[] {2, 7, 8},
                    new[] {6},
                    new[] {6}
                };
                graph.setDots(adjList);
            }


            Console.WriteLine("Матрица смежности - ");

            var adjMatr = graph.getGraphAsAdjacencyMatrix();

            for (var i = 0; i < adjMatr.GetLength(0); i++)
            {
                Console.WriteLine();
                for (var j = 0; j < adjMatr.GetLength(1); j++)
                {
                    Console.Write(adjMatr[i, j] + ", ");
                }
            }
            
            Console.WriteLine("\n Вершина || степень - ");
            var graphDegrees = graph.getEdgesDegrees();
            for (int i = 0; i < graphDegrees.Count; i++)
            {
                Console.WriteLine(" {0} || {1}", i, graphDegrees[i]);
            }

            var bfs = graph.bfs(0);


            Console.WriteLine(" \n Bfs result - ");
            foreach (var el in bfs)
            {
                Console.Write("{0}, ", el);
            }

            var dfs = graph.dfs(0);

            Console.WriteLine("\n Dfs result - ");
            foreach (var el in dfs)
            {
                Console.Write("{0}, ", el);
            }

            var waveRes = graph.waveAlgorithm(8, 0);

            Console.WriteLine("\n WaveAlgorithm result - ");
            foreach (var VARIABLE in waveRes)
            {
                Console.Write("{0}, ", VARIABLE);
            }

            Console.WriteLine("Алгоритм Дийкстра на другом примере - \n");
            int[,] graphDij =
            {
                {0, 6, 0, 0, 0, 0, 0, 9, 0},
                {6, 0, 9, 0, 0, 0, 0, 11, 0},
                {0, 9, 0, 5, 0, 6, 0, 0, 2},
                {0, 0, 5, 0, 9, 16, 0, 0, 0},
                {0, 0, 0, 9, 0, 10, 0, 0, 0},
                {0, 0, 6, 0, 10, 0, 2, 0, 0},
                {0, 0, 0, 16, 0, 2, 0, 1, 6},
                {9, 11, 0, 0, 0, 0, 1, 0, 5},
                {0, 0, 2, 0, 0, 0, 6, 5, 0}
            };

            var dist = Graph.Dijkstra(graphDij, 3, 9);

            Console.WriteLine("Вершина    Расстояние от источника");

            for (int i = 0; i < dist.Length; ++i)
                Console.WriteLine("{0}\t  {1}", i, dist[i]);
            

            Console.WriteLine("Шарнины графа - точки ");
            var hinges= graph.findHinges(0);
            foreach (var dot in hinges)
            {
                Console.Write("{0}, ",dot);
            }

            int[,] graphForEuler =
            {
                {0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0},
                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                {0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0},
                {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
                {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                {0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0}
            };
            
            var exampleGraph = new Graph(graphForEuler);
            
            Console.WriteLine("\n Проверка другого графа на Эйлеровость - \n");
            
            Console.Write("Граф Эйлеров - " + exampleGraph.isGraphEuler());
            
            Console.Write("\n Эйлеров цикл - \n");

            foreach (var v in exampleGraph.getEulerCycle())
            {
                Console.Write("{0}, ", v);
            }
        }
    }
}

class Graph
{
    private int[][] _dots;

    private int[,] _adjMatr;

    public Graph()
    {
    }

    public Graph(int[][] dots)
    {
        _dots = dots;
    }

    public Graph(int[,] adjMatr)
    {
        _adjMatr = adjMatr;
    }

    public void setDots(int[][] dots)
    {
        _dots = dots;
    }

    public void setAdjMatr(int[,] adjMatr)
    {
        _adjMatr = adjMatr;
    }

    public int[,] getGraphAsAdjacencyMatrix()
    {
        if (_adjMatr != null)
        {
            return _adjMatr;
        }

        var length = _dots.Length;

        var result = new int[length, length];

        for (var i = 0; i < _dots.Length; i++)
        {
            for (var j = 0; j < _dots[i].Length; j++)
            {
                var ind = _dots[i][j];
                result[i, ind] = 1;
            }
        }

        _adjMatr = result;

        return _adjMatr;

    }
    
    public List<int> bfs(int u)
    {
        var adjacencyMatrix = getGraphAsAdjacencyMatrix();
        var queue = new Queue<int>(); //Это очередь, хранящая номера вершин
        var visited = new bool[adjacencyMatrix.GetLength(0)]; //массив отмечающий посещённые вершины
        var bfsResult = new List<int>();


        visited[0] = true; //массив, хранящий состояние вершины(посещали мы её или нет)

        queue.Enqueue(u);
        while (queue.Count != 0)
        {
            u = queue.Dequeue();
            bfsResult.Add(u);

            for (var i = 0; i < adjacencyMatrix.GetLength(0); i++)
            {
                if (!Convert.ToBoolean(adjacencyMatrix[u, i])) continue;
                if (visited[i]) continue;
                visited[i] = true;
                queue.Enqueue(i);
            }
        }

        return bfsResult;
    }

    public List<int> dfs(int u)
    {
        var adjacencyMatrix = getGraphAsAdjacencyMatrix();
        var stack = new Stack<int>(); //Это стэк, хранящий номера вершин
        var visited = new bool[adjacencyMatrix.GetLength(0)]; //массив отмечающий посещённые вершины
        var dfsResult = new List<int>();


        visited[0] = true; //массив, хранящий состояние вершины(посещали мы её или нет)

        stack.Push(u);

        while (stack.Count != 0)
        {
            u = stack.Pop();
            dfsResult.Add(u);

            for (var i = adjacencyMatrix.GetLength(0) - 1; i >= 0; i--)
            {
                if (!Convert.ToBoolean(adjacencyMatrix[u, i])) continue;
                if (visited[i]) continue;
                visited[i] = true;
                stack.Push(i);
            }
        }

        return dfsResult;
    }
    public List<int> waveAlgorithm(int from, int to)
    {
        var adjacencyMatrix = getGraphAsAdjacencyMatrix();
        var queue = new Queue<int>(); //Это очередь, хранящая номера вершин
        var u = 0; //Точка, с которой начинаем
        var visited = new bool[adjacencyMatrix.GetLength(0)]; //массив отмечающий посещённые вершины
        var bfsResult = new List<int>();

        var wave = new int[adjacencyMatrix.GetLength(0)];

        visited[0] = true; //массив, хранящий состояние вершины(посещали мы её или нет)

        queue.Enqueue(u);

        while (queue.Count != 0)
        {
            u = queue.Dequeue();

            bfsResult.Add(u);


            for (var i = 0; i < adjacencyMatrix.GetLength(0); i++)
            {
                if (!Convert.ToBoolean(adjacencyMatrix[u, i])) continue;
                if (visited[i]) continue;
                visited[i] = true;
                queue.Enqueue(i);
                wave[i] = u;
            }
        }

        var result = new List<int>();
        var parent = wave[from];
        while (true)
        {
            result.Add(parent);
            parent = wave[parent];
            if (parent != to) continue;
            result.Add(to);
            break;
        }

        return result;
    }

    private static int minimumDistance(int[] distance, bool[] shortestPath, int verticesCount)
    {
        var min = int.MaxValue;
        var minIndex = 0;

        for (int v = 0; v < verticesCount; ++v)
        {
            if (shortestPath[v] == false && distance[v] <= min)
            {
                min = distance[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    public static int[] Dijkstra(int[,] graph, int source, int verticesCount)
    {
        var distance = new int[verticesCount];
        var shortestPath = new bool[verticesCount];

        for (var i = 0; i < verticesCount; ++i)
        {
            distance[i] = int.MaxValue;
            shortestPath[i] = false;
        }

        distance[source] = 0;

        for (var count = 0; count < verticesCount - 1; ++count)
        {
            var u = minimumDistance(distance, shortestPath, verticesCount);
            shortestPath[u] = true;

            for (var v = 0; v < verticesCount; ++v)
                if (!shortestPath[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue &&
                    distance[u] + graph[u, v] < distance[v])
                    distance[v] = distance[u] + graph[u, v];
        }

        return distance;
    }
    
    private int connectedComponentSearch() //Поиск кол-ва компонент связности
    {
        var dots = new List<int>();

        var compCount = 0;
        
        for (var i = 0; i < getGraphAsAdjacencyMatrix().GetLength(0); i++)
        {
            
            dots.Add(i);
            
        }
        
        while (dots.Count != 0)
        {
            var kek = bfs(dots[0]);
            
            foreach (var VARIABLE in kek)
            {
                dots.Remove(VARIABLE);
            }

            compCount++;
        }

        return compCount;
    }

    private int time;
    private List<bool> used_edges = new List<bool>();
    private List<int> tin = new List<int>();
    private List<int> minTin = new List<int>();
    private HashSet<int> hingesSet = new HashSet<int>();

    private void dfsToFindHinges(int s,  int lastEdge = -1)
    {
        used_edges[s] = true;
        time++;
        tin[s] = minTin[s] = time;
        var children = 0;

        for (var i = 0; i < _dots[s].Length; i++)
        {
            var to = _dots[s][i];

            if (to == lastEdge) continue;
            
            if (used_edges[to]) 
                minTin[s] = Math.Min(minTin[s], minTin[to]);
                
            else
            {
                dfsToFindHinges(to,s);

                minTin[s] = Math.Min(minTin[s], minTin[to]);

                if (minTin[to] >= tin[s] && lastEdge != -1)// Проверка на то, что вершина - не корень
                {
                    hingesSet.Add(s);
                }

                children++;
            }

        }

        if (lastEdge == -1 && children >= 2) 
            // Если мы в корне и детей больше чем два, тогда корень - шарник 
            hingesSet.Add(s);
    }
    
    public List<int> findHinges(int s)
    {
        for (var i = 0; i < getGraphAsAdjacencyMatrix().GetLength(0); i++)
        {
            used_edges.Add(false);
            tin.Add(0);
            minTin.Add(0);
        }

        time = 0;

        dfsToFindHinges(s);

        return hingesSet.ToList();
    }
    
    public Dictionary<int, int> getEdgesDegrees()
    { 
        var adjMatr = getGraphAsAdjacencyMatrix();
        
        var degrees = new Dictionary<int,int>();

        for (int i = 0; i < adjMatr.GetLength(0); i++)
        {
            var degree = 0;
            for (int j = 0; j < adjMatr.GetLength(0); j++)
            {
                if (adjMatr[i, j] != 0) degree++;
            }
            
            degrees.Add(i,degree);
        }

        return degrees;
    }

    public bool isGraphEuler()
    {
        var degrees = getEdgesDegrees();

        foreach (var degree in degrees.Values)
        {
            if (degree % 2 != 0) return false;
        }

        return true;
    }

    public List<int> getEulerCycle()
    {
        var cur = 0;
        
        var eulerCycle = new List<int>();
        
        var stack = new Stack<int>();
        stack.Push(cur);

        while (stack.Count != 0)
        {
            cur = stack.Peek();

            var nei = edgeHasNeighbor(cur);

            if (nei != -1)
            {
                stack.Push(nei);
            }
            else
            {
                stack.Pop();
                eulerCycle.Add(cur);
            }
        }

        return eulerCycle;
    }

    private int[,] graphCopy;
    
    private int edgeHasNeighbor(int edge)
    {

        if (graphCopy == null)
        {
            graphCopy = getGraphAsAdjacencyMatrix();
            for (int i = 0; i < graphCopy.GetLength(0); i++)
            {
                if (graphCopy[edge, i] != 0)
                {
                    graphCopy[edge, i] = 0;
                    graphCopy[i, edge] = 0;
                    return i;
                }
            }
        }
        else
        {
            for (int i = 0; i < graphCopy.GetLength(0); i++)
            {
                if (graphCopy[edge, i] != 0)
                {
                    graphCopy[edge, i] = 0;
                    graphCopy[i, edge] = 0;
                    return i;
                }
            }
        }

        return -1;
    }
}