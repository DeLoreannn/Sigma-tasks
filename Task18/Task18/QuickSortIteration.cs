using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task18
{
    static class QuickSortIteration
    {
        static private void Swap<T>(ref T p, ref T t)
        {
            var temp = p;
            p = t;
            t = temp;
        }

        //method for sorting from start to end
        public static void QuickSort<T>(T[] array, int start, int end) where T : IComparable<T>
        {
            Stack stack = new Stack();
            T pivot;
            int pivotIndex = start, leftIndex = pivotIndex + 1, rightIndex = end;

            stack.Push(pivotIndex);
            stack.Push(rightIndex);

            int leftIndexOfSubSet, rightIndexOfSubset;

            while (stack.Count > 0)
            {
                rightIndexOfSubset = (int)stack.Pop();
                leftIndexOfSubSet = (int)stack.Pop();

                leftIndex = leftIndexOfSubSet + 1;
                pivotIndex = leftIndexOfSubSet;
                rightIndex = rightIndexOfSubset;

                pivot = array[pivotIndex];

                if (leftIndex > rightIndex)
                    continue;

                while (leftIndex < rightIndex)
                {
                    while ((leftIndex <= rightIndex) && (array[leftIndex].CompareTo(pivot) <= 0))
                        leftIndex++;
                    while ((leftIndex <= rightIndex) && (array[rightIndex].CompareTo(pivot) >= 0))
                        rightIndex--;
                    if (rightIndex >= leftIndex)
                        Swap(ref array[leftIndex], ref array[rightIndex]);
                }

                if (pivotIndex <= rightIndex)
                    if (array[pivotIndex].CompareTo(array[rightIndex]) > 0)
                        Swap(ref array[pivotIndex], ref array[rightIndex]);

                if (leftIndexOfSubSet < rightIndex)
                {
                    stack.Push(leftIndexOfSubSet);
                    stack.Push(rightIndex - 1);
                }

                if (rightIndexOfSubset > rightIndex)
                {
                    stack.Push(rightIndex + 1);
                    stack.Push(rightIndexOfSubset);
                }
            }
        }

        //method with attribute and sort order
        public static void QuickSort<T>(T[] array, string attribute, int start, int end, SortOrder sortOrder) where T : AttributeSort
        {
            foreach (T item in array)
            {
                item.Attribute = attribute;
                item.SortOrder = sortOrder;
                item.Condition = true;
            }
            Stack stack = new Stack();
            T pivot;
            int pivotIndex = start, leftIndex = pivotIndex + 1, rightIndex = end;

            stack.Push(pivotIndex);
            stack.Push(rightIndex);

            int leftIndexOfSubSet, rightIndexOfSubset;

            while (stack.Count > 0)
            {
                rightIndexOfSubset = (int)stack.Pop();
                leftIndexOfSubSet = (int)stack.Pop();

                leftIndex = leftIndexOfSubSet + 1;
                pivotIndex = leftIndexOfSubSet;
                rightIndex = rightIndexOfSubset;

                pivot = array[pivotIndex];

                if (leftIndex > rightIndex)
                    continue;
                if (array[0].SortOrder == SortOrder.Ascending)
                {
                    while (leftIndex < rightIndex)
                    {
                        while ((leftIndex <= rightIndex) && (array[leftIndex].CompareTo(pivot) <= 0))
                            leftIndex++;
                        while ((leftIndex <= rightIndex) && (array[rightIndex].CompareTo(pivot) >= 0))
                            rightIndex--;
                        if (rightIndex >= leftIndex)
                            Swap(ref array[leftIndex], ref array[rightIndex]);
                    }

                    if (pivotIndex <= rightIndex)
                        if (array[pivotIndex].CompareTo(array[rightIndex]) > 0)
                            Swap(ref array[pivotIndex], ref array[rightIndex]);
                }
                else if (array[0].SortOrder == SortOrder.Descending)
                {
                    while (leftIndex < rightIndex)
                    {
                        while ((leftIndex <= rightIndex) && (array[leftIndex].CompareTo(pivot) >= 0))
                            leftIndex++;
                        while ((leftIndex <= rightIndex) && (array[rightIndex].CompareTo(pivot) <= 0))
                            rightIndex--;
                        if (rightIndex >= leftIndex)
                            Swap(ref array[leftIndex], ref array[rightIndex]);
                    }

                    if (pivotIndex <= rightIndex)
                        if (array[pivotIndex].CompareTo(array[rightIndex]) < 0)
                            Swap(ref array[pivotIndex], ref array[rightIndex]);
                }

                if (leftIndexOfSubSet < rightIndex)
                {
                    stack.Push(leftIndexOfSubSet);
                    stack.Push(rightIndex - 1);
                }

                if (rightIndexOfSubset > rightIndex)
                {
                    stack.Push(rightIndex + 1);
                    stack.Push(rightIndexOfSubset);
                }
            }
        }
    }
}
