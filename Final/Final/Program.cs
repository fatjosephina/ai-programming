using System;

namespace Final
{
    class NeuralNetWork
    {
        private Random _randomObj;

        public NeuralNetWork(int synapseMatrixColumns, int synapseMatrixLines)
        {
            SynapseMatrixColumns = synapseMatrixColumns;
            SynapseMatrixLines = synapseMatrixLines;

            _Init();
        }

        public int SynapseMatrixColumns { get; }
        public int SynapseMatrixLines { get; }
        public double[,] SynapsesMatrix { get; private set; }

        /// <summary>
        /// Initialize the ramdom object and the matrix of ramdon weights
        /// </summary>
        private void _Init()
        {
            // make sure that for every instance of the neural network we are geting the same random values
            _randomObj = new Random(1);
            _GenerateSynapsesMatrix();
        }

        /// <summary>
        /// Generate our matrix with the weight of the synapses
        /// </summary>
        private void _GenerateSynapsesMatrix()
        {
            SynapsesMatrix = new double[SynapseMatrixLines, SynapseMatrixColumns];

            for (var i = 0; i < SynapseMatrixLines; i++)
            {
                for (var j = 0; j < SynapseMatrixColumns; j++)
                {
                    SynapsesMatrix[i, j] = (2 * _randomObj.NextDouble()) - 1;
                }
            }
        }

        /// <summary>
        /// Calculate the sigmoid of a value
        /// </summary>
        /// <returns></returns>
        private double[,] _CalculateSigmoid(double[,] matrix)
        {

            int rowLength = matrix.GetLength(0);
            int colLength = matrix.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    var value = matrix[i, j];
                    matrix[i, j] = 1 / (1 + Math.Exp(value * -1));
                }
            }
            return matrix;
        }

        /// <summary>
        /// Calculate the sigmoid derivative of a value
        /// </summary>
        /// <returns></returns>
        private double[,] _CalculateSigmoidDerivative(double[,] matrix)
        {
            int rowLength = matrix.GetLength(0);
            int colLength = matrix.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    var value = matrix[i, j];
                    matrix[i, j] = value * (1 - value);
                }
            }
            return matrix;
        }

        /// <summary>
        /// Will return the outputs give the set of the inputs
        /// </summary>
        public double[,] Think(double[,] inputMatrix)
        {
            var productOfTheInputsAndWeights = MatrixDotProduct(inputMatrix, SynapsesMatrix);

            return _CalculateSigmoid(productOfTheInputsAndWeights);

        }

        /// <summary>
        /// Train the neural network to achieve the output matrix values
        /// </summary>
        public void Train(double[,] trainInputMatrix, double[,] trainOutputMatrix, int interactions)
        {
            // we run all the interactions
            for (var i = 0; i < interactions; i++)
            {
                // calculate the output
                var output = Think(trainInputMatrix);

                // calculate the error
                var error = MatrixSubtract(trainOutputMatrix, output);
                var curSigmoidDerivative = _CalculateSigmoidDerivative(output);
                var error_SigmoidDerivative = MatrixProduct(error, curSigmoidDerivative);

                // calculate the adjustment :) 
                var adjustment = MatrixDotProduct(MatrixTranspose(trainInputMatrix), error_SigmoidDerivative);

                SynapsesMatrix = MatrixSum(SynapsesMatrix, adjustment);
            }
        }

        /// <summary>
        /// Transpose a matrix
        /// </summary>
        /// <returns></returns>
        public static double[,] MatrixTranspose(double[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            double[,] result = new double[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }

        /// <summary>
        /// Sum one matrix with another
        /// </summary>
        /// <returns></returns>
        public static double[,] MatrixSum(double[,] matrixa, double[,] matrixb)
        {
            var rowsA = matrixa.GetLength(0);
            var colsA = matrixa.GetLength(1);

            var result = new double[rowsA, colsA];

            for (int i = 0; i < rowsA; i++)
            {
                for (int u = 0; u < colsA; u++)
                {
                    result[i, u] = matrixa[i, u] + matrixb[i, u];
                }
            }

            return result;
        }

        /// <summary>
        /// Subtract one matrix from another
        /// </summary>
        /// <returns></returns>
        public static double[,] MatrixSubtract(double[,] matrixa, double[,] matrixb)
        {
            var rowsA = matrixa.GetLength(0);
            var colsA = matrixa.GetLength(1);

            var result = new double[rowsA, colsA];

            for (int i = 0; i < rowsA; i++)
            {
                for (int u = 0; u < colsA; u++)
                {
                    result[i, u] = matrixa[i, u] - matrixb[i, u];
                }
            }

            return result;
        }

        /// <summary>
        /// Multiplication of a matrix
        /// </summary>
        /// <returns></returns>
        public static double[,] MatrixProduct(double[,] matrixa, double[,] matrixb)
        {
            var rowsA = matrixa.GetLength(0);
            var colsA = matrixa.GetLength(1);

            var result = new double[rowsA, colsA];

            for (int i = 0; i < rowsA; i++)
            {
                for (int u = 0; u < colsA; u++)
                {
                    result[i, u] = matrixa[i, u] * matrixb[i, u];
                }
            }

            return result;
        }

        /// <summary>
        /// Dot Multiplication of a matrix
        /// </summary>
        /// <returns></returns>
        public static double[,] MatrixDotProduct(double[,] matrixa, double[,] matrixb)
        {

            var rowsA = matrixa.GetLength(0);
            var colsA = matrixa.GetLength(1);

            var rowsB = matrixb.GetLength(0);
            var colsB = matrixb.GetLength(1);

            if (colsA != rowsB)
                throw new Exception("Matrices dimensions don't fit.");

            var result = new double[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    for (int k = 0; k < rowsB; k++)
                        result[i, j] += matrixa[i, k] * matrixb[k, j];
                }
            }
            return result;
        }

    }

    // Player stats represent health, attack, shield, magic, status effect, speed
    class Player
    {
        public double[,] score;
        public bool isAlive = true;

        public Player(double[,] newScenario)
        {
            var playerNeuralNetwork = new NeuralNetWork(1, 6);

            var trainingInputsPlayer = new double[,]
            {
                { 1, 0, 1, 0, 1, 0 },
                { 1, 0, 0, 1, 1, 1 },
                { 0, 1, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 1, 1},
                { 0, 0, 0, 0, 0, 0},
                { 1, 1, 1, 1, 0, 1},
                { 1, 1, 1, 1, 0, 0},
                { 0, 0, 0, 0, 0, 1},
                { 1, 1, 1, 1, 1, 1},
                { 0, 0, 1, 1, 1, 0 }
            };
            var trainingOutputsPlayer = NeuralNetWork.MatrixTranspose(new double[,] { { 1, 0, 0, 1, 0, 1, 1, 1, 1, 1 } });

            playerNeuralNetwork.Train(trainingInputsPlayer, trainingOutputsPlayer, 10000);

            score = playerNeuralNetwork.Think(newScenario);
        }
    }

    // Monster stats represent health, attack, evilness, intelligence, reputation, speed
    class Monster
    {
        public double[,] score;

        public Monster(double[,] newScenario)
        {
            var monsterNeuralNetwork = new NeuralNetWork(1, 6);

            var trainingInputsMonster = new double[,]
            {
                { 1, 0, 1, 0, 1, 0 },
                { 1, 0, 0, 1, 1, 1 },
                { 0, 1, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 1, 1},
                { 0, 0, 0, 0, 0, 0},
                { 1, 1, 1, 1, 0, 1},
                { 1, 1, 1, 1, 0, 0},
                { 0, 0, 0, 0, 0, 1},
                { 1, 1, 1, 1, 1, 1},
                { 0, 0, 1, 1, 1, 0 }
            };
            var trainingOutputsMonster = NeuralNetWork.MatrixTranspose(new double[,] { { 0, 0, 0, 1, 0, 1, 1, 0, 1, 0 } });

            monsterNeuralNetwork.Train(trainingInputsMonster, trainingOutputsMonster, 10000);

            score = monsterNeuralNetwork.Think(newScenario);
        }
    }

    class Program
    {
        static double[,] GenerateRandomDoubleMatrix()
        {
            Random rand = new Random();
            double[,] randomMatrix = new double[1, 6];
            for (int i = 0; i < 6; i++)
            {
                randomMatrix[0, i] = rand.Next(2);
            }

            return randomMatrix;
        }

        static double PrintMatrix(double[,] matrix)
        {
            int rowLength = matrix.GetLength(0);
            int colLength = matrix.GetLength(1);

            double doubleToPrint = 0;

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    doubleToPrint += Math.Round((matrix[i, j] * 100), 3);
                }
            }

            return doubleToPrint;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("|~|~|~|~|Welcome to Monster Maze!|~|~|~|~|\n");

            Console.WriteLine("First, a player will be generated, and then you will have the chance to defeat random monsters! The winner of each battle will be decided by comparing the matrices generated by each character's artificial neural network.\n");

            int enemiesKilled = 0;

            Player player = new Player(GenerateRandomDoubleMatrix());
            double playerScoreResult = PrintMatrix(player.score);
            Console.WriteLine("Generated a new player with a power score of {0}!", playerScoreResult);

            while (player.isAlive)
            {
                Console.WriteLine("Press any key to continue into the maze.");
                Console.ReadLine();

                Console.WriteLine("~~Enemies Defeated: {0}~~", enemiesKilled);

                Monster monster = new Monster(GenerateRandomDoubleMatrix());
                double monsterScoreResult = PrintMatrix(monster.score);
                Console.WriteLine("Generated a new monster with a power score of {0}!", monsterScoreResult);
                if (playerScoreResult <= monsterScoreResult)
                {
                    Console.WriteLine("The monster defeated you!");
                    player.isAlive = false;
                }
                else
                {
                    Console.WriteLine("Monster vanquished!");
                    enemiesKilled++;
                }
                Console.WriteLine("--------------------");
            }

            Console.WriteLine("You defeated {0} monsters!", enemiesKilled);
            Console.WriteLine("Press any key to exit.");
            Console.Read();
        }
    }
}
