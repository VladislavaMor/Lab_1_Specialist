using System.Diagnostics;

namespace Lab_1._5
{
    public class TreeNode
    {
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
        public int Weight { get; set; }
    }

    internal class Program
    {
        static Random rnd = new Random();
        static long total;

        public static void CreateRandomTree(TreeNode node, int level)
        {
            node.Left = new TreeNode();
            node.Right = new TreeNode();
            node.Weight = rnd.Next(100);
            total += node.Weight;
            level--;
            if (level == 0)
            {
                node.Left.Weight = rnd.Next(100);
                node.Right.Weight = rnd.Next(100);
                total += node.Left.Weight;
                total += node.Right.Weight;
                return;
            }
            CreateRandomTree(node.Left, level);
            CreateRandomTree(node.Right, level);
        }
        public static long weightTree(TreeNode root)
        {
            return
                (long)root.Weight +
                (root.Left != null ? weightTree(root.Left) : 0) +
                (root.Right != null ? weightTree(root.Right) : 0);

        }
        public async static Task<long> weightTreeAsync(TreeNode root, int level = 3)
        {
            if(level == 0) return weightTree(root);
            int newlevel = level - 1;
            return
                (long)root.Weight +
                (root.Left != null ? await weightTreeAsync(root.Left, newlevel) : 0) +
                (root.Right != null ? await weightTreeAsync(root.Right, newlevel) : 0);
        }

        static void Main(string[] args)
        {
            int treeLevel = 26; 
            Console.WriteLine($"Starting tree creation with depth {treeLevel}...");
            TreeNode root = new TreeNode();
            CreateRandomTree(root, treeLevel);
            Console.WriteLine($"Tree created with total weight: {total}");

            ThreadPool.SetMinThreads(32, 32);
            ThreadPool.SetMaxThreads(64, 64);

            Stopwatch t1 = new Stopwatch();
            t1.Start();
            long r1 = weightTree(root);
            t1.Stop();
            Console.WriteLine($"Single weight: {r1} Time {t1.ElapsedMilliseconds}");

            Stopwatch t2 = new Stopwatch();
            t2.Start();
            long r2 = weightTreeAsync(root).Result;
            t2.Stop();
            Console.WriteLine($"Multi weight: {r2} Time {t2.ElapsedMilliseconds}");
        }
    }
}