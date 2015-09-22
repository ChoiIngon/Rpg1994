using UnityEngine;
using System.Text.RegularExpressions;

public class RangeInt {
	private int min = 0;
	private int max = 0;
	private const string rangeExpr = "([0-9]+\\~[0-9]+)";
	private const string intExpr = "([0-9]+)";

	private static Regex RANGE_EXPR = new Regex(rangeExpr + "|" + intExpr);
	private static Regex INT_EXPR = new Regex(intExpr);

	public RangeInt()
	{
	}

	public RangeInt(string range) {
		SetValue (range);
	}
	
	public void SetValue(string range)
	{
		if (false == RangeInt.RANGE_EXPR.IsMatch (range)) {
			throw new System.Exception("invalid value");
		}
		MatchCollection matches = RangeInt.INT_EXPR.Matches (range);
		if (1 == matches.Count) {
			min = System.Convert.ToInt32 (matches [0].Value);
			max = min;
		} else if (2 == matches.Count) {
			min = System.Convert.ToInt32 (matches [0].Value);
			max = System.Convert.ToInt32 (matches [1].Value);

			if (min > max) {
				throw new System.Exception ("invalid range");
			}
		} else {
			throw new System.Exception("invalid value");
		}
	}

	public int GetValue() {
		if (min == max) {
			return min;
		}
		return Random.Range(min, max);
	}

	public static implicit operator int (RangeInt value) {
		return value.GetValue();
	}

	public override string ToString ()
	{
		return min.ToString () + "~" + max.ToString ();
	}
}
