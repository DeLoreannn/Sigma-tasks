using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task18
{
    static class QuickSortRecursion
    {
        static private void Swap<T>(ref T p, ref T t)
        {
            var temp = p;
            p = t;
            t = temp;
        }

        static private int Separation<T>(T[] array, int start, int end) where T : IComparable<T>
        {
            if (array[0] is AttributeSort)
            {
                var pivot = start - 1;
                int temp = pivot;
                for (var i = start; i < end; i++)
                {

                    if ((array[i].CompareTo(array[end]) < 0) && ((array[i] as AttributeSort).SortOrder == SortOrder.Ascending) && ((array[i] as AttributeSort).Condition == true))
                    {
                        ++temp;
                        if ((array[temp] as AttributeSort).Condition == true)
                        {
                            pivot++;
                            Swap(ref array[pivot], ref array[i]);
                        }
                        else
                            --temp;
                    }
                    else if ((array[i].CompareTo(array[end]) > 0) && ((array[i] as AttributeSort).SortOrder == SortOrder.Descending) && ((array[i] as AttributeSort).Condition == true))
                    {
                        ++temp;
                        if ((array[temp] as AttributeSort).Condition == true)
                        {
                            pivot++;
                            Swap(ref array[pivot], ref array[i]);
                        }
                        else
                            --temp;
                    }
                }

                pivot++;
                if (((array[pivot] as AttributeSort).Condition == true) && ((array[end] as AttributeSort).Condition == true))
                {
                    Swap(ref array[pivot], ref array[end]);
                }
                return pivot;
            }
            else
            {
                var pivot = start - 1;
                for (var i = start; i < end; i++)
                {
                    if (array[i].CompareTo(array[end]) < 0)
                    {
                        pivot++;
                        Swap(ref array[pivot], ref array[i]);
                    }
                }

                pivot++;
                Swap(ref array[pivot], ref array[end]);
                return pivot;
            }
        }

        //method for sorting from start to end
        static public T[] QuickSort<T>(T[] array, int start, int end) where T : IComparable<T>
        {
            if (start >= end)
            {
                return array;
            }
            int pivotIndex = Separation(array, start, end);
            QuickSort(array, start, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, end);

            return array;
        }

        //method with attribute and sort order
        static public T[] QuickSort<T>(T[] array, string attribute, int start, int end, SortOrder sortOrder) where T : AttributeSort
        {
            foreach (T item in array)
            {
                item.Attribute = attribute;
                item.SortOrder = sortOrder;
                item.Condition = true;
            }
            return QuickSort(array, start, end);
        }

        //method with attribute, sort order and additional condition
        static public T[] QuickSort<T>(T[] array, string attribute, int start, int end, SortOrder sortOrder, Func<T, bool> condition) where T : AttributeSort
        {
            foreach (T item in array)
            {
                item.Attribute = attribute;
                item.SortOrder = sortOrder;
                item.Condition = condition(item);
            }
            return QuickSort(array, start, end);
        }
    }
}
