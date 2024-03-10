using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>
{
    T[] heap;
    int itemCount;

    public Heap(int _maxSize)
    {
        heap = new T[_maxSize];
    }

    public void Queue(T item)
    {
        item.HeapIndex = itemCount;
        heap[itemCount] = item;
        SortUp(item);
        itemCount++;
    }

    public T Peek()
    {
        return heap[0];
    }

    public T Dequeue()
    {
        T firstItem = heap[0];
        itemCount--;
        heap[0] = heap[itemCount];
        heap[0].HeapIndex = 0;
        SortDown(heap[0]);
        return firstItem;
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public int Count
    {
        get
        {
            return itemCount;
        }
    }

    public bool Contains(T item)
    {
        return Equals(item, heap[item.HeapIndex]);
    }

    void SortDown(T item)
    {
        while (true)
        {
            int leftChildIndex = item.HeapIndex * 2 + 1;
            int rightChildIndex = item.HeapIndex * 2 + 2;
            int swapIndex;

            if (leftChildIndex < itemCount)
            {
                swapIndex = leftChildIndex;

                if (rightChildIndex < itemCount)
                    if (heap[leftChildIndex].CompareTo(heap[rightChildIndex]) < 0)
                        swapIndex = rightChildIndex;
                if (item.CompareTo(heap[swapIndex]) < 0)
                    Swap(item, heap[swapIndex]);
                else
                    return;
            }
            else
                return;
        }
    }

    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;
        while (true)
        {
            T parentItem = heap[parentIndex];
            if (item.CompareTo(parentItem) > 0)
                Swap(item, parentItem);
            else
                break;
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    void Swap(T itemA, T itemB)
    {
        heap[itemA.HeapIndex] = itemB;
        heap[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}
