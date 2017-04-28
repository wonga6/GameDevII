using UnityEngine;
using System.Collections;

public class Pair<T1, T2> {

	// PUBLIC SECTION

	// default constructor
	public Pair() {}

	// constructor that takes in arguments
	public Pair(T1 first, T2 second) {
		this.first = first;
		this.second = second;
	}

	public T1 first { get; set; }
	public T2 second { get; set; }
}