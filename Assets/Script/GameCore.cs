using System;


class GameCore
    {//这里关于直接在字段这里声明却在构造函数里面new做出解释，如果声明的时候直接new就会导致在类被声明的时候就会使类里面的new调用从而占用内存，但是在声明在构造函数外，new在构造函数内就会在调用构造函数的时候才占用内存。
        public int[,] map;
        private int[] mergeArray;
        private int[] tempArray;
        private int[] originalArray;//合并前mergeArray

        public GameCore()
        {
            map = new int[4, 4];
            mergeArray = new int[4];
            tempArray = new int[4];
            emptyLocArray = new Location[16];
            random = new Random();
            originalArray = new int[4];
        }
        //public static void deZero(ref int[]x,out int[]arr)
        //{
        //    int k = 0;
        //    arr = new int[4];
        //    for(int i =0;i<arr.Length;i++)
        //    {
        //        if(x[i]!=0)
        //        {
        //            arr[k] = x[i];
        //            k++;
        //        }
        //    }
        //}这个方法的定义不报错，但是写的很复杂，有两个导致复杂的知识点没有好好记忆，首先数组本身是值类型不需要加ref了，本来就是对本省的值进行操作，其次，忘了Array.CopyTo这个方法,重写如下        
        public bool IsChange { get; set; }
        public  void deZero()
        {
            Array.Clear(tempArray, 0, tempArray.Length);
            int k = 0;
            for (int i = 0; i < mergeArray.Length; i++)
            {
                if (mergeArray[i] != 0)
                {
                    tempArray[k] = mergeArray[i];
                    k++;
                }
            }
            tempArray.CopyTo(mergeArray, 0);
        }
        public  void combine()
        {
            for (int i = 0; i < mergeArray.Length-1; i++)
            {
                if ( mergeArray[i] != 0&&mergeArray[i] == mergeArray[i+1] )//此地进行了一次自加，会导致i值得变化，但是这样可以方便判断，一二个如果相等就第二个再合并后为0和第三个非0必不等
                {
                    mergeArray[i ] += mergeArray[i+1];
                    mergeArray[i+1] = 0;
                }
            }
            //缺计分功能
        }
        public  void operation()
        {
            //记录合并数组在合并前的数据
            mergeArray.CopyTo(originalArray, 0);
            deZero();
            combine();
            deZero();
            for(int i =0;i<mergeArray.Length;i++)
            {
                if (mergeArray[i] != originalArray[i])
                {
                    IsChange = true;
                    break;
                }
            }
            
        }
        public  void move(direction x)
        {
            IsChange = false;
            switch (x)
            {
                case direction.up:
                    {
                        moveUp();
                        break;
                    }
                case direction.down:
                    {
                        moveDown();
                        break;
                    }
                case direction.left:
                    {
                        moveLeft();
                        break;
                    }
                case direction.right:
                    {
                        moveRight();
                        break;
                    }
            }
        }
        public  void moveUp()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    mergeArray[k] = map[k, i];
                }
                operation();
                for (int k = 0; k < 4; k++)
                {
                    map[k, i] = mergeArray[k];
                }
            }
        }
        public  void moveDown()
        {
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int k = 3; k >= 0; k--)
                {
                    mergeArray[index++] = map[k, i];
                }
                operation();
                index = 0;
                for (int k = 3; k >= 0; k--)
                {
                    map[k, i] = mergeArray[index++];
                }
                index = 0;
            }
        }
        public  void moveLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    mergeArray[k] = map[i, k];
                }
                operation();
                for (int k = 0; k < 4; k++)
                {
                    map[i, k] = mergeArray[k];
                }
            }
        }
        public  void moveRight()
        {
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int k = 3; k >= 0; k--)
                {
                    mergeArray[index++] = map[i, k];
                }
                operation();
                index = 0;
                for (int k = 3; k >= 0; k--)
                {
                    map[i, k] = mergeArray[index++];
                }
                index = 0;
            }
        }
        public  void show()
        {
            //Console.Clear();暂时
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    Console.Write(map[i, k] + "\t");
                }
                Console.WriteLine();
            }
        }
        //计算空位置
        Location[] emptyLocArray;
        int emptycount = 0;
        public void ComputeEmpty()
        {
            //每次计算空位置，先清空成员变量空位置数组
            Array.Clear(emptyLocArray, 0, emptyLocArray.Length);
            int index = 0;
            for(int r =0;r<4;r++)
            {
                for(int c=0;c<4;c++)
                {
                    if (this.map[r, c] == 0)
                        emptyLocArray[index++] = new Location(r, c);
                }
            }
            emptycount = index;
        }
        Random random;
        //创建新的数字
        public void CreateNewNumber(out int number,out Location loc)
        {
            if (emptycount > 0)
            {
                //产生新数字
                //1.随机在空数组中获取随机数
                int index = random.Next(0, emptycount);
                loc = emptyLocArray[index];
                //2.随机产生2或者4
                number=map[loc.RIndex, loc.CIndex] = random.Next(1, 101) <= 90 ? 2 : 4;
            ComputeEmpty();
        }
            else
        {
            number = -1;
            loc = new Location(0, 0);
        }
            
        }
        public bool IsOver()
        {
            //如果还有空位置
            if (emptycount > 0)
                return false;

            ////判断水平
            //for(int i=0;i<4;i++)
            //{
            //    for (int c =0;c<3;c++)
            //    {
            //        if (map[i, c] == map[i, c + 1])
            //            return false;
            //    }
            //}

            ////判断垂直
            //for(int i=0;i<4; i++)
            //{
            //    for(int c = 0;c<3;c++)
            //    {
            //        if (map[c, i] == map[c + 1, i])
            //            return false;
            //    }
            //}

            for(int i=0;i<4;i++)
            {
                for(int c=0;c<3;c++)
                {
                    if (map[i, c] == map[i, c + 1]||map[c,i]==map[c+1,i])
                        return false;
                }
            }
            return true;
        }
    }
