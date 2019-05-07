using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting<T>
{
	public event Action<T, T> ChangeEvent;

	private T v { get; set; } = default(T);
	public T Value
	{
		get { return v; }
		set
		{
			var tmp = v;
			v = value;
			if(!Equals(v, tmp))
				ChangeEvent?.Invoke(v, tmp);
		}
	}

	public override string ToString()
	{
		return v.ToString();
	}
}
