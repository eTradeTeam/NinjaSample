#region Using declarations
using System;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace NinjaTrader.NinjaScript.AddOns.ExampleAddon
{
	// The Structures and Enumeratoins contained here will be available to all indicators using ...Example.Common
	public partial class ExCommon
	{
		// ---------------------------------
		// - Common Structures
		// ---------------------------------
		#region Structures
		
		public struct event_Struct
		{
			public int					bar;
			public double				price;
			public PerformanceTestType	testType;
		}
		
		public struct event_List
		{
			public int type;
			public List<event_Struct> events;
		}
		
		#endregion
		
		// ---------------------------------
		// - enumerations
		// ---------------------------------
		#region Performance Test Type Enumeration
		public enum PerformanceTestType
		{
			PassStruct,
			PassSeries,
			PassSeriesbyReference,
			PassISeries,
			PassISeriesbyReference
		}
		#endregion
		
	}
}
