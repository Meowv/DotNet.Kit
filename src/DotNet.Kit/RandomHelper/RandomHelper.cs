using System;

namespace DotNet.Kit
{
    public class RandomHelper
    {
        private int rep = 0;

        private Random _random;
        public RandomHelper()
        {
            _random = new Random();
        }

        /// <summary>
        /// 生成一个指定范围的随机整数，该随机数范围包括最小值，但不包括最大值
        /// </summary>
        /// <param name="minNum">最小值</param>
        /// <param name="maxNum">最大值</param>
        /// <returns></returns>
        public int GetRandomInt(int minNum,int maxNum)
        {
            return _random.Next(minNum, maxNum);
        }

        /// <summary>
        /// 生成一个0.0到1.0的随机小数
        /// </summary>
        /// <returns></returns>
        public double GetRandomDouble()
        {
            return _random.NextDouble();
        }

        /// <summary>
        /// 对一个数组进行随机排序
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="arr">需要随机排序的数组</param>
        public void GetRandomArray<T>(T[] arr)
        {
            int count = arr.Length;

            for (int i = 0; i < count; i++)
            {
                int targetIndex1 = GetRandomInt(0, arr.Length);
                int targetIndex2 = GetRandomInt(0, arr.Length);

                T temp;

                temp = arr[targetIndex1];
                arr[targetIndex1] = arr[targetIndex2];
                arr[targetIndex2] = temp;
            }
        }

        /// <summary>
        /// 生成随机不重复数字字符串    
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string GetRandomInt(int length)
        {
            var str = string.Empty;
            var num = DateTime.Now.Ticks + rep;
            rep++;

            var random = new Random(((int)(((ulong)num) & 0xffffffffL)) | ((int)(num >> rep)));
            for (int i = 0; i < length; i++)
            {
                int randomNum = random.Next();
                str = str + ((char)(0x30 + ((ushort)(randomNum % 10)))).ToString();
            }
            return str;
        }

        /// <summary>
        /// 生成随机字符串（数字和字母混和）  
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string GetRandomString(int length)
        {
            var str = string.Empty;
            var num = DateTime.Now.Ticks + rep;
            rep++;

            var random = new Random(((int)(((ulong)num) & 0xffffffffL)) | ((int)(num >> rep)));
            for (int i = 0; i < length; i++)
            {
                char ch;
                int randomNum = random.Next();
                if (randomNum % 2 == 0)
                {
                    ch = (char)(0x30 + ((ushort)(randomNum % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(randomNum % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }
    }
}
