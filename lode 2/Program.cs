namespace Solution
{
    using System;
    using System.Collections.Generic;

    public class BattleshipField
    {
        public static ShipCell FindCell(int y, int x)
        {
            foreach (ShipCell cell in shipPieces)
            {
                if (cell.positionY == y && cell.positionX == x)
                {
                    return cell;
                }

            }
            throw new Exception("no cell found");
        }
        public static bool CountCells(ShipCell cell)

        {
            switch (cell.amountOfNeighbours,cell.neighboursOfNeighbours)
            {
                case (0,0):
                    {
                        cell00++; return true;
                        break;
                    }
                case (1, 1):
                    {
                        cell11++; return true;
                        break;
                    }
                case (1, 2):
                    {
                        cell12++; return true;
                        break;
                    }
                case (2, 2):
                    {
                        cell22++; return true;
                        break;
                    }
                    case (2,3):
                    {
                        cell23++;
                        return true;
                        break;
                    }
                    default : { return false; }
            }
        }

        static void Main()
        {
            int[,] field = new int[10, 10]
                     {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                      {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                      {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                      {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
            Console.WriteLine(BattleshipField.ValidateBattlefield(field));
        }
        public static int cell00 = 0;
        public static int cell11 = 0;
        public static int cell12 = 0;
        public static int cell22 = 0;
        public static int cell23 = 0;




        public static List<ShipCell> shipPieces = new List<ShipCell>();

        public static bool ValidateBattlefield(int[,] field)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (field[i, j] == 1)
                    {
                        shipPieces.Add(new ShipCell(i, j));
                    }
                }
            }
            foreach (ShipCell cell in shipPieces)
            {
                cell.IdentificateNeighbours(field);
                Console.WriteLine(cell.amountOfNeighbours);
                if (!cell.AllCornersFree(field))
                {
                    return false;
                }
            }
            foreach (ShipCell cell in shipPieces)
            {
                foreach (ShipCell neighbour in cell.neighbours)
                {
                    cell.neighboursOfNeighbours += neighbour.amountOfNeighbours;
                    Console.WriteLine(neighbour.amountOfNeighbours);
                }
            }
            int index = 1;
            foreach (ShipCell cell in shipPieces)
            {
                Console.WriteLine("cell num :" + index);
                Console.WriteLine(cell.amountOfNeighbours);
                Console.WriteLine(cell.neighboursOfNeighbours);
                Console.WriteLine();

                index++;
            }
            foreach(ShipCell cell in shipPieces)
            {
                if (!CountCells(cell))
                {  return false; }
            }
            if (cell00 == 4 && cell11 ==6 && cell12 ==6&& cell22 == 2 && cell23 ==2)
            {
                return true;
            }

            return false;
        }

    }


    public class ShipCell
    {
        public int positionX { get; }
        public int positionY { get; }
        public int amountOfNeighbours = 0;
        public int neighboursOfNeighbours = 0;
        public List<ShipCell> neighbours = new List<ShipCell>();

        public ShipCell(int Y, int X)
        {
            positionX = X;
            positionY = Y;
        }

        public void IdentificateNeighbours(int[,] field) //puts the neighbouring cells with a shipcell in the neighbours list 
        {   //up  // first number rows, second columns
            if (positionY > 0 && field[positionY - 1, positionX] == 1)
            {
                neighbours.Add(BattleshipField.FindCell(positionY - 1, positionX));
            }
            //down
            if (positionY < 9 && field[positionY + 1, positionX] == 1)
            {
                neighbours.Add(BattleshipField.FindCell(positionY + 1, positionX));
            }
            //left
            if (positionX > 0 && field[positionY, positionX - 1] == 1)
            {
                neighbours.Add(BattleshipField.FindCell(positionY, positionX - 1));
            }
            //right
            if (positionY < 9 && field[positionY, positionX + 1] == 1)
            {
                neighbours.Add(BattleshipField.FindCell(positionY, positionX + 1));
            }
            amountOfNeighbours = neighbours.Count;
        }
        public bool AllCornersFree(int[,] field) //checks for shipcells in diagonal touch with the cell
        {   // up left
            if (positionY > 0 && positionX > 0 && field[positionY - 1, positionX - 1] == 1)
            {
                return false;
            }
            //down right
            if (positionY < 9 && positionX < 9 && field[positionY + 1, positionX + 1] == 1)
            {
                return false;
            }
            // up right
            if (positionY > 0 && positionX < 9 && field[positionY - 1, positionX + 1] == 1)
            {
                return false;
            }
            //down left
            if (positionY < 9 && positionX > 0 && field[positionY + 1, positionX - 1] == 1)
            {
                return false;
            }
            return true;
        }


    }


}