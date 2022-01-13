using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task18
{
    static class HeapSort
    {
        static private void Swap<T>(ref T p, ref T t)
        {
            var temp = p;
            p = t;
            t = temp;
        }

        //method for sorting
        static public void Sort<T>(T[] array) where T : IComparable<T>
        {
            int n = array.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(array, n, i, SortOrder.Ascending);

            for (int i = n - 1; i >= 0; i--)
            {
                Swap(ref array[0], ref array[i]);
                heapify(array, i, 0, SortOrder.Ascending);
            }
        }

        //method for sorting from start to end
        //with attribute and sort order
        static public void Sort<T>(T[] arr, string attribute, int start, int end, SortOrder sortOrder) where T : AttributeSort
        {
            foreach (T item in arr)
            {
                item.Attribute = attribute;
            }
            T[] additionalArray = new T[end - start];
            for (int i = start, j = 0; i < end; ++i, ++j)
            {
                additionalArray[j] = arr[i];
            }
            int n = additionalArray.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(additionalArray, n, i, sortOrder);

            for (int i = n - 1; i >= 0; i--)
            {
                Swap(ref additionalArray[0], ref additionalArray[i]);
                heapify(additionalArray, i, 0, sortOrder);
            }
            for (int i = start, j = 0; i < end; ++i, ++j)
            {
                arr[i] = additionalArray[j];
            }
        }

        //rebuilding the heap
        static private void heapify<T>(T[] arr, int n, int i, SortOrder sortOrder) where T : IComparable<T>
        {
            int element = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (sortOrder == SortOrder.Ascending)
            {
                if (left < n && (arr[left].CompareTo(arr[element]) > 0))
                    element = left;
                if (right < n && (arr[right].CompareTo(arr[element]) > 0))
                    element = right;
            }
            else if (sortOrder == SortOrder.Descending)
            {
                if (left < n && (arr[left].CompareTo(arr[element]) < 0))
                    element = left;

                if (right < n && (arr[right].CompareTo(arr[element]) < 0))
                    element = right;
            }

            if (element != i)
            {
                Swap(ref arr[i], ref arr[element]);
                heapify(arr, n, element, sortOrder);
            }
        }
    }
}
