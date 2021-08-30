#region Using declarations
using System;
using System.Collections;
#endregion


namespace NinjaTrader.NinjaScript.AddOns.ExampleAddon
{
//	public partial class Indicator
//	{
	    // ------------------ Class of performance methods
		public class PerfClass
		{
			// PerfClass persistent variables
			ExCommon.event_List eventList;	// list of events defined in ExCommon
			
			int maxListLen;
			
			public PerfClass ( int i_Max_List_Len )
			{
				maxListLen = i_Max_List_Len;
			}
			
			
			// -------------------------------------
			// Events - method Indicator Performance Validation - passing Series, ISeries, struct
			// -------------------------------------
			#region ------ Performance Test Methods
			// A - struct
			// B - Series
			// C - Series Reference (not allowed for class-method)
			// D - ISeries
			// E - ISeries Reference - not allowed for either method or class-method
			
			// -------------------------------------
			// A - Events - cTest_Pass_Structs
			// -------------------------------------
			#region cTest_Pass_Structs (Events)
			public double cTest_Pass_Structs(	int i_CurrentBar,
												ExCommon.event_Struct i_event,
												ExCommon.event_Struct i_event2,
												ExCommon.event_Struct i_event3 )
			{
				ExCommon.event_Struct temp_Event;
				ExCommon.event_Struct itmd_Event;
				bool CalcIntermediateEvents = true;
				int temp = 1;
				return 2.5;
			}
			// -------------------------------------
			// Events - end cTest_Pass_Structs
			// -------------------------------------
			#endregion

			// -------------------------------------
			// B - Events - cTest_Pass_Series
			// -------------------------------------
			#region cTest_Pass_Series (Events)
			public double cTest_Pass_Series(	int i_CurrentBar,
											ExCommon.event_Struct i_event,
											Series<double> high,
											Series<double> low	)
			{
				ExCommon.event_Struct temp_Event;
				ExCommon.event_Struct itmd_Event;
				bool CalcIntermediateEvents = true;
				int temp = 1;
				return high[0];
			}
			// -------------------------------------
			// Events - end cTest_Pass_Series
			// -------------------------------------
			#endregion

			// -------------------------------------
			// C - Events - cTest_Pass_SeriesRef -- This will cause errors when called.
			// -------------------------------------
			#region cTest_Pass_SeriesRef (Events)
			public double cTest_Pass_SeriesRef(	int i_CurrentBar,
												ExCommon.event_Struct i_event,
												ref Series<double> high,
												ref Series<double> low	)
			{
				ExCommon.event_Struct temp_Event;
				ExCommon.event_Struct itmd_Event;
				bool CalcIntermediateEvents = true;
				int temp = 1;
				return high[0];
			}
			// -------------------------------------
			// Events - end cTest_Pass_SeriesRef
			// -------------------------------------
			#endregion

			// -------------------------------------
			// D - Events - cTest_Pass_ISeries
			// -------------------------------------
			#region cTest_Pass_ISeries (Events)
			public double cTest_Pass_ISeries(	int i_CurrentBar,
												ExCommon.event_Struct i_event,
												ISeries<double> high,
												ISeries<double> low	)
			{
				ExCommon.event_Struct temp_Event;
				ExCommon.event_Struct itmd_Event;
				bool CalcIntermediateEvents = true;
				int temp = 1;
				return high[0];
			}
			// -------------------------------------
			// Events - end cTest_Pass_ISeries
			// -------------------------------------
			#endregion
			
	        // -------------------------------------
	        // E - Events - cTest_Pass_ISeriesref  -- Will not compile
	        // -------------------------------------
	        #region cTest_Pass_ISeriesRef (Events)
//	        public void cTest_Pass_ISeriesRef(	int i_CurrentBar,
//	        	                	            ExCommon.event_Struct i_event,
//	            	        	                ISeries<double> high,
//	                		                    ISeries<double> low	)
//	        {
//	            ExCommon.event_Struct temp_Event;
//	            ExCommon.event_Struct itmd_Event;
//	            bool CalcIntermediateEvents = true;
//	            int temp = 1;
//	        }
	        // -------------------------------------
	        // Events - end cTest_Pass_ISeriesRef
	        // -------------------------------------
	        #endregion
			
			// -------------------------------------
			// Events - end method Indicator Performance Validation
			#endregion
			
	    }
//	}
}
