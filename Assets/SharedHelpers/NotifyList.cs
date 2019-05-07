using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyList<T> : IEnumerable
{
	public Action<T> ItemAddedEvent;
	public Action<T> ItemRemovedEvent;

	private List<T> internalList = new List<T>();

	public void Add(T item)
	{
		internalList.Add(item);
		ItemAddedEvent?.Invoke(item);
	}

	public void Remove(T item)
	{
		internalList.Remove(item);
		ItemRemovedEvent?.Invoke(item);
	}

	public int IndexOf(T item)
	{
		return internalList.IndexOf(item);
	}

	public IEnumerator GetEnumerator()
	{
		return internalList.GetEnumerator();
	}
}
