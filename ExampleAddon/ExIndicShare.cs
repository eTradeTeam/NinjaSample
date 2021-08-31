#region Using declarations
using System;
using System.Collections;
using NinjaTrader.NinjaScript.Indicators;
#endregion


namespace NinjaTrader.NinjaScript.AddOns.ExampleAddon
{
	public class IndicShare
	{
		// PerfClass persistent variables
		ExCommon.event_List eventList;	// list of events defined in exCommon
//		Series<double> FastSMA = new Series<double>(this, MaximumBarsLookBack.Infinite);
//		Series<double> SlowSMA;
		
		double Fast;
		double Slow;
		
		int maxListLen;
		
		public IndicShare ( int i_Max_List_Len )
		{
			maxListLen = i_Max_List_Len;
			eventList = new ExCommon.event_List();
//			SlowSMA = new Series<double>(this);
		}
		
		public void calc_event (	int i_FastLength,
									int i_SlowLength,
									ISeries<double> close )
		{
//			Fast = SMA( close, i_FastLength );
//			Fast = SMA( close, i_FastLength );
		}
		
		
	}
}
