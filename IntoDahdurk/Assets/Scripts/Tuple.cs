using UnityEngine;
using System.Collections;

public class Tuple<T1, T2, T3> {

	// PUBLIC SECTION

	// default constructor
	public Tuple() {}

	// constructor that takes in arguments
	public Tuple(T1 first, T2 second, T3 third) {
		this.first = first;
		this.second = second;
		this.third = third;
	}

	public T1 first { get; set; }
	public T2 second { get; set; }
	public T3 third { get; set; }
}