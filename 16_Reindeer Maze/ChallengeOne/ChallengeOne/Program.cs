namespace ChallengeOne {
    class Program {
        private class Node(bool isTraversible, (int, int) position) {
            public (int, int) Position { get; } = position;
            public bool IsTraversible { get; } = isTraversible;
            public List<Node> Neighbours { get; } = new List<Node>();

            public Node Parent { get; private set; }

            public float G { get; private set; } = float.MaxValue;
            public float H { get; private set; } = float.MaxValue;
            public float F => G + H;

            public void SetParent(Node parent) => Parent = parent;
            public void SetG(float g) => G = g;
            public void SetH(float h) => H = h;

            public void AddNeighbour(Node node) => Neighbours.Add(node);

            public float GetDistance(Node neighbour) {
                return Math.Abs(Position.Item1 - neighbour.Position.Item1) +
                       Math.Abs(Position.Item2 - neighbour.Position.Item2);
            }
        }

        public static void Main() {
            var input = File.ReadAllText("Inputs/inputs.txt").Split('\n');
            var start = (-1, -1);
            var end = (-1, -1);

            for (int i = 0; i < input.Length; i++) {
                input[i] = input[i].TrimEnd();
            }

            for (int i = 0; i < input.Length; i++) {
                if (input[i].Contains('S')) start = (i, input[i].IndexOf('S'));
                if (input[i].Contains('E')) end = (i, input[i].IndexOf('E'));

                if (start != (-1, -1) && end != (-1, -1)) break;
            }

            var maze = BuildMaze(input, start, end);
            int score = DiscoverPath(maze, start, end);
            Console.WriteLine($"Lowest score to reach the end: {score}");
        }

        private static List<List<Node>> BuildMaze(string[] input, (int, int) start, (int, int) end) {
            var maze = new List<List<Node>>();

            for (int y = 0; y < input.Length; y++) {
                var row = new List<Node>();
                for (int x = 0; x < input[y].Length; x++) {
                    char c = input[y][x];
                    var isTraversable = c != '#';
                    var node = new Node(isTraversable, (x, y));

                    if (isTraversable) {
                        node.SetH(Math.Abs(x - end.Item1) + Math.Abs(y - end.Item2));
                    }

                    row.Add(node);
                }

                maze.Add(row);
            }

            for (int y = 0; y < maze.Count; y++) {
                for (int x = 0; x < maze[y].Count; x++) {
                    var node = maze[y][x];
                    if (!node.IsTraversible) continue;

                    foreach (var offset in new (int, int)[] { (0, -1), (0, 1), (-1, 0), (1, 0) }) {
                        var nx = x + offset.Item1;
                        var ny = y + offset.Item2;
                        if (ny >= 0 && ny < maze.Count && nx >= 0 && nx < maze[0].Count) {
                            node.AddNeighbour(maze[ny][nx]);
                        }
                    }
                }
            }

            return maze;
        }

        private static int DiscoverPath(List<List<Node>> maze, (int x, int y) start, (int x, int y) end) {
            var startNode = maze[start.y][start.x];
            var endNode = maze[end.y][end.x];

            var openSet = new List<(Node node, (int dx, int dy) facing)> { (startNode, Facing["right"]) };
            var closedSet = new HashSet<Node>();

            startNode.SetG(0);

            while (openSet.Any()) {
                // Get the node with the lowest F score
                var current = openSet.OrderBy(pair => pair.node.F).First();
                var currentNode = current.node;
                var currentFacing = current.facing;

                if (currentNode == endNode) {
                    return ReconstructPath(endNode);
                }

                openSet.Remove(current);
                closedSet.Add(currentNode);

                foreach (var neighbour in currentNode.Neighbours) {
                    if (!neighbour.IsTraversible || closedSet.Contains(neighbour)) continue;

                    // Calculate move direction
                    var moveDirection = (neighbour.Position.Item1 - currentNode.Position.Item1,
                        neighbour.Position.Item2 - currentNode.Position.Item2);

                    // Determine rotation cost
                    int rotationCost = 0;
                    var tempFacing = currentFacing; // Use a temporary facing for calculations
                    RotateTo(currentNode.Position, neighbour.Position, ref tempFacing, ref rotationCost);

                    // Calculate tentative G including movement and rotation costs
                    var tentativeG = currentNode.G + 1 + rotationCost;

                    if (tentativeG < neighbour.G) {
                        neighbour.SetParent(currentNode);
                        neighbour.SetG(tentativeG);
                        neighbour.SetH(neighbour.GetDistance(endNode));

                        if (!openSet.Any(pair => pair.node == neighbour)) {
                            openSet.Add((neighbour, tempFacing)); // Update facing for neighbour
                        }
                    }
                }
            }

            return -1;
        }

        private static readonly Dictionary<string, (int, int)> Facing = new Dictionary<string, (int, int)>() {
            { "up", (0, -1) },
            { "left", (-1, 0) },
            { "down", (0, 1) },
            { "right", (1, 0) }
        };
        
        private static int ReconstructPath(Node endNode) {
            int score = 0;
            var current = endNode;
            var facing = Facing["right"];
            while (current.Parent != null) {
                RotateTo(current.Position, current.Parent.Position, ref facing, ref score);
                score++;
                current = current.Parent;
            }

            return score;
        }

        private static void RotateTo((int x, int y) current, (int x, int y) next, ref (int, int) facing,
            ref int score) {
            var toFace = ((next.x - current.x), (next.y - current.y));
            
            while (facing != toFace) {
                if (facing == Facing["up"]) {
                    if (toFace == Facing["left"]) facing = Facing["left"];
                    else facing = Facing["right"];
                }
                else if (facing == Facing["right"]) {
                    if (toFace == Facing["up"]) facing = Facing["up"];
                    else facing = Facing["down"];
                }
                else if (facing == Facing["down"]) {
                    if (toFace == Facing["right"]) facing = Facing["right"];
                    else facing = Facing["left"];
                }
                else if (facing == Facing["left"]) {
                    if (toFace == Facing["down"]) facing = Facing["down"];
                    else facing = Facing["up"];
                }
                score += 1000;
            }
        }
    }
}