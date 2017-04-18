using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendRoguelike
{
    public class PriorityQueue<T>
        where T : INode
    {
        #region Private Fields

        private List<int> costs = new List<int>();
        private List<T> items = new List<T>();

        #endregion Private Fields

        #region Public Properties

        public int Count => items.Count;

        #endregion Public Properties

        #region Public Methods

        public T Dequeue()
        {
            T output = items[0];
            items[0] = items[Count - 1];
            costs[0] = costs[Count - 1];
            costs.RemoveAt(Count - 1);
            items.RemoveAt(Count - 1);
            if (Count > 0)
                ShuffleDown(0);
            return output;
        }

        public void Enqueue(T item)
        {
            // Not multithread-safe!
            int index = items.Count;
            items.Add(item);
            costs.Add(item.F);
            ShuffleUp(index);
        }

        #endregion Public Methods

        #region Private Methods

        private static int LeftIndex(int index)
        {
            return (index * 2) + 1;
        }

        private static int ParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        private static int RightIndex(int index)
        {
            return (index * 2) + 2;
        }

        private bool LeftCost(int index, out int item)
        {
            int leftIndex = LeftIndex(index);
            if (leftIndex < 0 || leftIndex >= costs.Count)
            {
                item = 0;
                return false;
            }
            else
            {
                item = costs[leftIndex];
                return true;
            }
        }

        private bool LeftItem(int index, out T item)
        {
            int leftIndex = LeftIndex(index);
            if (leftIndex < 0 || leftIndex >= items.Count)
            {
                item = default(T);
                return false;
            }
            else
            {
                item = items[leftIndex];
                return true;
            }
        }

        private bool ParentCost(int index, out int item)
        {
            int parentIndex = ParentIndex(index);
            if (parentIndex == index || parentIndex < 0 ||
                parentIndex >= costs.Count)
            {
                item = 0;
                return false;
            }
            else
            {
                item = costs[parentIndex];
                return true;
            }
        }

        private bool ParentItem(int index, out T item)
        {
            int parentIndex = ParentIndex(index);
            if (parentIndex < 0 || parentIndex >= items.Count)
            {
                item = default(T);
                return false;
            }
            else
            {
                item = items[parentIndex];
                return true;
            }
        }

        private bool RightCost(int index, out int item)
        {
            int rightIndex = RightIndex(index);
            if (rightIndex < 0 || rightIndex >= costs.Count)
            {
                item = 0;
                return false;
            }
            else
            {
                item = costs[rightIndex];
                return true;
            }
        }

        private bool RightItem(int index, out T item)
        {
            int rightIndex = RightIndex(index);
            if (rightIndex < 0 || rightIndex >= items.Count)
            {
                item = default(T);
                return false;
            }
            else
            {
                item = items[rightIndex];
                return true;
            }
        }

        private void ShuffleDown(int index)
        {
            bool done = false;
            int thisCost = costs[index];
            while (done == false)
            {
                int leftCost,
                    rightCost;
                bool leftCostFound = LeftCost(index, out leftCost),
                    rightCostFound = RightCost(index, out rightCost);
                // Older costs stay nearer the end.
                if (rightCostFound && rightCost <= thisCost &&
                    leftCostFound && rightCost <= leftCost)
                {
                    int rightIndex = RightIndex(index);
                    Swap(index, rightIndex);
                    index = rightIndex;
                }
                else if (leftCostFound && leftCost <= thisCost)
                {
                    int leftIndex = LeftIndex(index);
                    Swap(index, leftIndex);
                    index = leftIndex;
                }
                else
                {
                    done = true;
                }
            }
        }

        private void ShuffleUp(int index)
        {
            int parentCost;
            int thisCost = costs[index];
            // More recent items will take priority, if costs are equal. This
            // speeds up some uses of a priority queue.
            while (ParentCost(index, out parentCost) &&
                parentCost >= thisCost)
            {
                int parentIndex = ParentIndex(index);
                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        private void Swap(int index, int parentIndex)
        {
            SwapItems(index, parentIndex);
            SwapCosts(index, parentIndex);
        }

        private void SwapCosts(int index, int parentIndex)
        {
            var temp = costs[index]; costs[index] = costs[parentIndex]; costs[parentIndex] = temp;
        }

        private void SwapItems(int index, int parentIndex)
        {
            var temp = items[index]; items[index] = items[parentIndex]; items[parentIndex] = temp;
        }

        #endregion Private Methods
    }
}
