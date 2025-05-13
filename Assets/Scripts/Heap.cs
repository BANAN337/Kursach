public class Heap<T> where T : IHeapItem<T>
{
    private T[] _items;

    public int Count { get; private set; }

    public Heap(int capacity)
    {
        _items = new T[capacity];
    }

    public void Add(T item)
    {
        item.HeapIndex = Count;
        _items[Count] = item;
        SortUp(item);
        Count++;
    }

    public T RemoveFirstItem()
    {
        var firstItem = _items[0];
        Count--;
        _items[0] = _items[Count];
        _items[0].HeapIndex = 0;
        SortDown(_items[0]);
        return firstItem;
    }

    public bool Contains(T item)
    {
        return Equals(_items[item.HeapIndex], item);
    }

    private void SortUp(T item)
    {
        var parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            var parentItem = _items[parentIndex];

            if (item.CompareTo(parentItem) > 0)
            {
                SwapItems(item, parentItem);
            }
            else
            {
                break;
            }
            
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    private void SortDown(T item)
    {
        while (true)
        {
            var leftChildIndex = item.HeapIndex * 2 + 1;
            var rightChildIndex = item.HeapIndex * 2 + 2;

            if (leftChildIndex < Count)
            {
                var swapIndex = leftChildIndex;
                if (rightChildIndex < Count)
                {
                    if (_items[leftChildIndex].CompareTo(_items[rightChildIndex]) < 0)
                    {
                        swapIndex = rightChildIndex;
                    }
                }

                if (item.CompareTo(_items[swapIndex]) < 0)
                {
                    SwapItems(item, _items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    private void SwapItems(T itemA, T itemB)
    {
        _items[itemA.HeapIndex] = itemB;
        _items[itemB.HeapIndex] = itemA;
        (itemA.HeapIndex, itemB.HeapIndex) = (itemB.HeapIndex, itemA.HeapIndex);
    }
}