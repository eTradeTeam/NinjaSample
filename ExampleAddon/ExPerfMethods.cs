#region Using declarations
using System;
using System.Collections;
#endregion


namespace NinjaTrader.NinjaScript.AddOns.ExampleAddon
{
    // ------------------ Class of performance methods
	public partial class PerformanceTests
    {
        #region ------ Performance Test Methods
        // A - struct
        // B - Series
        // C - Series Reference (not allowed for class-method)
        // D - ISeries
        // E - ISeries Reference - not allowed for either method or class-method
        
        // -------------------------------------
        // A - Events - mTest_Pass_Structs
        // -------------------------------------
        #region mTest_Pass_Structs (Events)
        public double mTest_Pass_Structs(	int i_CurrentBar,
                                    ExCommon.event_Struct i_event,
                                    ExCommon.event_Struct i_event2,
                                    ExCommon.event_Struct i_event3	)
        {
            ExCommon.event_Struct temp_Event;
            ExCommon.event_Struct itmd_Event;
            bool CalcIntermediateEvents = true;
            int temp = 1;
			return 2.5;
        }
        // -------------------------------------
        // Events - end mTest_Pass_Structs
        // -------------------------------------
        #endregion

        // -------------------------------------
        // B - Events - mTest_Pass_Series
        // -------------------------------------
        #region mTest_Pass_Series (Events)
        public double mTest_Pass_Series(	int i_CurrentBar,
        	    	                        ExCommon.event_Struct i_event,
            	    	                    Series<double> high,
                	    	                Series<double> low	)
        {
            ExCommon.event_Struct temp_Event;
            ExCommon.event_Struct itmd_Event;
            bool CalcIntermediateEvents = true;
            int temp = 1;
//			return high[0];
			return 2.0;
        }
        // -------------------------------------
        // Events - end mTest_Pass_Series
        // -------------------------------------
        #endregion

        // -------------------------------------
        // C - Events - mTest_Pass_SeriesRef
        // -------------------------------------
        #region mTest_Pass_SeriesRef (Events)
        public double mTest_Pass_SeriesRef(	int i_CurrentBar,
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
        // Events - end mTest_Pass_SeriesRef
        // -------------------------------------
        #endregion

        // -------------------------------------
        // D - Events - mTest_Pass_ISeries
        // -------------------------------------
        #region mTest_Pass_ISeries (Events)
        public double mTest_Pass_ISeries(	int i_CurrentBar,
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
        // Events - end mTest_Pass_ISeries
        // -------------------------------------
        #endregion

        // -------------------------------------
        // E - Events - mTest_Pass_ISeriesref  -- Will not compile
        // -------------------------------------
        #region mTest_Pass_ISeriesRef (Events)
//        public void mTest_Pass_ISeriesRef(	int i_CurrentBar,
//        	                	            ExCommon.event_Struct i_event,
//            	        	                ISeries<double> high,
//                		                    ISeries<double> low	)
//        {
//            ExCommon.event_Struct temp_Event;
//            ExCommon.event_Struct itmd_Event;
//            bool CalcIntermediateEvents = true;
//            int temp = 1;
//        }
        // -------------------------------------
        // Events - end mTest_Pass_ISeriesRef
        // -------------------------------------
        #endregion
		
        #endregion
    }
}
