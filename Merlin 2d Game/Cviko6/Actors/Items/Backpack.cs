using Merlin2d.Game.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Actors.Items
{
    public class Backpack : IInventory
    {
        private IItem[] items;
        private int capacity;
        private int position = 0;

        public Backpack(int capacity)
        {
            items = new IItem[capacity];
            this.capacity = capacity;
        }
        public void AddItem(IItem item)
        {
            items[position++] = item;
        }

        public int GetCapacity()
        {
            return capacity;
        }

        public IEnumerator<IItem> GetEnumerator()
        {
            for(int i = 0; i < position; i++)
            {
                yield return items[i];
            }
        }

        public IItem GetItem()
        {
            IItem item = items[0];
            position--;
            RemoveItem(index: 0);
            return item;
        }

        public void RemoveItem(IItem item)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(int index)
        {
            for(int i = index; i < position; i++)
            {
                items[i] = items[i + 1];
            }
            position--;
        }

        public void ShiftLeft()
        {
            IItem item = items[0];
            for(int i = 0; i < position; i++)
            {
                items[i] = items[i + 1];
                items[position] = item;
            }
        }

        public void ShiftRight()
        {
            IItem item = items[position];
            for (int i = position; i > 0; i--)
            {
                items[i] = items[i - 1];
                items[0] = item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
