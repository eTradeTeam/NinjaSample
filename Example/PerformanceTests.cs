#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
using NinjaTrader.NinjaScript.AddOns.ExampleAddon;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators.Example
{
	public partial class PerformanceTests : Indicator
	{
		#region Global Variables Declaration
		
		DateTime Indic_Start_Time = DateTime.Now;
		DateTime StartTime = DateTime.Now;
		DateTime EndTime = DateTime.Now;
		long elapsedTicks;
		double scaleFactor;
		
//		DateTime mTime_A = DateTime.Now.Ticks;
//		DateTime mTime_B = DateTime.Now.Ticks;
//		DateTime mTime_C = DateTime.Now.Ticks;
//		DateTime mTime_D = DateTime.Now.Ticks;
//		DateTime cTime_A = DateTime.Now.Ticks;
//		DateTime cTime_B = DateTime.Now.Ticks;
//		DateTime cTime_D = DateTime.Now.Ticks;
		
		double start_Time;
		double end_Time;
		int iCount;

		long sum_mA;
		double sum_mB;
		double sum_mC;
		double sum_mD;
		double sum_cA;
		double sum_cB;
		double sum_cD;
		
		double sum_Tot;
		
		int OnStateChangeCount = 0;
		int OnBarUpdateCount = 0;

		ExCommon.event_Struct testEvent;
		
		PerfClass testEvent_Fast;
		
		// --- Arrays
		int[] summaryBars;
		
//		// Series Definitions
		public Series<double> Median_Price;
		public Series<double> Median_Price2;
		#endregion

		protected override void OnStateChange()
		{
			OnStateChangeCount += 1;
			Print(DateTime.Now + ">>> Indicator: " + Name + "  OnStateChange: " + OnStateChangeCount + "  BarNum:" + CurrentBar + " >>> Current State is State."+State);
			
			if (State == State.SetDefaults)
			{
				#region Indicator Configuration Parameters
				Description									= @"Enter the description for your new custom Indicator here.";
				Name										= "PerformanceTests";
				Calculate									= Calculate.OnBarClose;
				IsOverlay									= false;
				DisplayInDataBox							= true;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= true;
				DrawVerticalGridLines						= true;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
				#endregion
				
				#region Indicator Default Input Values
				NumIterations					= 500000;
				#endregion
				
				#region Indicator Plot Configuration
				AddPlot(Brushes.Orange, "TestmethodPassStruct");
				#endregion
				
				
			}
			else if (State == State.Configure)
			{
				#region Global Variable - Initialization
				sum_mA = 0;
				sum_mB = 0;
				sum_mC = 0;
				sum_mD = 0;
				sum_cA = 0;
				sum_cB = 0;
				sum_cD = 0;
				sum_Tot = 0;
				iCount = 0;
				summaryBars = new int[6]{50, 100, 500, 1000, 5000, 5500};
				
				#endregion
			}
			else if (State == State.DataLoaded)
			{
				#region Global Series and Class - Instantiation & Initialization
				// Series Instantiation
				Median_Price = new Series<double>(this, MaximumBarsLookBack.Infinite);
				Median_Price2 = new Series<double>(this, MaximumBarsLookBack.Infinite);
				
				testEvent = new ExCommon.event_Struct { bar = 1, price = ( High[0] + Low[0] ) / 2, testType = ExCommon.PerformanceTestType.PassStruct};
				
				testEvent_Fast = new PerfClass( NumIterations );
				
				Indic_Start_Time = DateTime.Now;
				
				#endregion
			}
			else if (State == State.Realtime)
			{
				#region RealTime transition
				EndTime = DateTime.Now;
				sum_Tot += (EndTime.Ticks - Indic_Start_Time.Ticks);
				scaleFactor = 1/(10.0 * iCount);  // Microseconds per bar.  Each bar had NumIterations calculations

				Print ("\n ---------------------------------- End Summary: " + iCount + "Bars -----------------------------------" );
				Print(string.Format(" Duration: {0,8:F3}us for {1,8:N0} Iterations/bar averaged over {2} Bars", (sum_Tot * scaleFactor), NumIterations, CurrentBar ));
				Print (" ---------------------------------- End Summary -----------------------------------\n" );
				#endregion
			}
		}

		protected override void OnBarUpdate()
		{
//			OnBarUpdateCount += 1;
//			Print(DateTime.Now + "    Indicator: " + Name + "  OnBarUpdate: " + OnBarUpdateCount + "  BarNum:" + CurrentBar + " >>> Current State is State."+State);
			
			#region Main Calculation
			Median_Price[0] = ( High[0] + Low[0] + Open[0] + Close[0] * 2 ) / 5;
			Median_Price2[0] = Median_Price[0];
			if (CurrentBar < 1 ) return;
			
			
			// -------------------------------------
			// Indicator Performance Checks
			// -------------------------------------
			#region Performance Checks
			// -------------------------------------------- Start Performance Check
			// Note:  10000 Ticks per millisecond - 10 Ticks per microsecond
			
			iCount += 1;
			
			// ------------------------- Method Calls
			StartTime = DateTime.Now;
			for ( int i = 0; i< NumIterations; i++ ) { mTest_Pass_Structs( CurrentBar, testEvent, testEvent, testEvent ); }
			EndTime = DateTime.Now;
			sum_mA += (EndTime.Ticks - StartTime.Ticks);
			
			StartTime = DateTime.Now;
			for ( int i = 0; i< NumIterations; i++ ) { mTest_Pass_Series( CurrentBar, testEvent, Median_Price, Median_Price2 ); }
			EndTime = DateTime.Now;
			sum_mB += (EndTime.Ticks - StartTime.Ticks);
			
			StartTime = DateTime.Now;
			for ( int i = 0; i< NumIterations; i++ ) { mTest_Pass_SeriesRef( CurrentBar, testEvent, ref Median_Price, ref Median_Price2 ); }
			EndTime = DateTime.Now;
			sum_mC += (EndTime.Ticks - StartTime.Ticks);
			
			StartTime = DateTime.Now;
			for ( int i = 0; i< NumIterations; i++ ) { mTest_Pass_ISeries( CurrentBar, testEvent, High, Low ); }
			EndTime = DateTime.Now;
			sum_mD += (EndTime.Ticks - StartTime.Ticks);
			
			// A - struct
			// B - Series
			// C - Series Reference (not allowed for class-method)
			// D - ISeries
			// E - ISeries Reference - not allowed for either method or class-method
			
			// ------------------------- Class Method Calls
			
			// - A - cTest_Pass_Structs
			StartTime = DateTime.Now;
			for ( int i = 0; i< NumIterations; i++ ) { testEvent_Fast.cTest_Pass_Structs( CurrentBar, testEvent, testEvent, testEvent ); }
			EndTime = DateTime.Now;
			sum_cA += (EndTime.Ticks - StartTime.Ticks);
			
			// B - cTest_Pass_Series
			StartTime = DateTime.Now;
			for ( int i = 0; i< NumIterations; i++ ) { testEvent_Fast.cTest_Pass_Series( CurrentBar, testEvent, Median_Price, Median_Price2 ); }
			EndTime = DateTime.Now;
			sum_cB += (EndTime.Ticks - StartTime.Ticks);
			
			// - D - cTest_Pass_ISeries
			StartTime = DateTime.Now;
			for ( int i = 0; i< NumIterations; i++ ) { testEvent_Fast.cTest_Pass_ISeries( CurrentBar, testEvent, High, Low ); }
			EndTime = DateTime.Now;
			sum_cD += (EndTime.Ticks - StartTime.Ticks);
			
			scaleFactor = 1/(10.0 * iCount);  // Microseconds per bar.  Each bar had NumIterations calculations

			if (Array.Exists(summaryBars, element => element == iCount)) {
				Print ("\n ---------------------------------- End Summary: " + iCount + "Bars -----------------------------------" );
				Print(string.Format("Method A ------- pass struct ----------- Duration: {0,8:F3}us for {1,8:N0} Iterations/Bar  averaged over {2} Bars", (sum_mA * scaleFactor), NumIterations, CurrentBar )); // us =/10  ms= /10000
				Print(string.Format("Method B ------- pass Series ----------- Duration: {0,8:F3}us for {1,8:N0} Iterations/Bar  averaged over {2} Bars", (sum_mB * scaleFactor), NumIterations, CurrentBar ));
				Print(string.Format("Method C ------- pass Series Reference - Duration: {0,8:F3}us for {1,8:N0} Iterations/Bar  averaged over {2} Bars", (sum_mC * scaleFactor), NumIterations, CurrentBar ));
				Print(string.Format("Method C ------- pass ISeries ---------- Duration: {0,8:F3}us for {1,8:N0} Iterations/Bar  averaged over {2} Bars", (sum_mD * scaleFactor), NumIterations, CurrentBar ));
				Print(string.Format("Class Method A - pass struct ----------- Duration: {0,8:F3}us for {1,8:N0} Iterations/Bar  averaged over {2} Bars", (sum_cA * scaleFactor), NumIterations, CurrentBar ));
				Print(string.Format("Class Method B - pass Series ----------- Duration: {0,8:F3}us for {1,8:N0} Iterations/Bar  averaged over {2} Bars", (sum_cB * scaleFactor), NumIterations, CurrentBar ));
				Print(string.Format("Class Method D - pass ISeries ---------- Duration: {0,8:F3}us for {1,8:N0} Iterations/Bar  averaged over {2} Bars", (sum_cD * scaleFactor), NumIterations, CurrentBar ));
				Print (" ---------------------------------- End Summary -----------------------------------\n" );
			}
			
			// -------------------------------------------- End Performance Check
			#endregion
			
			#endregion
		}
		
		#region Modify Indicator DisplayName
		
		public override string DisplayName
		{
		  get { return "Performance Tests"; }
		}
		
		#endregion

		#region Input / Output Property Declarations
		
		#region Input Declarations
		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="NumIterations", Description="Number of Iterations to call each method in performance Tests", Order=1, GroupName="Parameters")]
		public int NumIterations
		{ get; set; }
		
		#endregion
		
		#region Output Declarations
		[Browsable(false)]
		[XmlIgnore]
		public Series<double> TestmethodPassStruct
		{
			get { return Values[0]; }
		}
		
		#endregion
		
		#endregion

	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private Example.PerformanceTests[] cachePerformanceTests;
		public Example.PerformanceTests PerformanceTests(int numIterations)
		{
			return PerformanceTests(Input, numIterations);
		}

		public Example.PerformanceTests PerformanceTests(ISeries<double> input, int numIterations)
		{
			if (cachePerformanceTests != null)
				for (int idx = 0; idx < cachePerformanceTests.Length; idx++)
					if (cachePerformanceTests[idx] != null && cachePerformanceTests[idx].NumIterations == numIterations && cachePerformanceTests[idx].EqualsInput(input))
						return cachePerformanceTests[idx];
			return CacheIndicator<Example.PerformanceTests>(new Example.PerformanceTests(){ NumIterations = numIterations }, input, ref cachePerformanceTests);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.Example.PerformanceTests PerformanceTests(int numIterations)
		{
			return indicator.PerformanceTests(Input, numIterations);
		}

		public Indicators.Example.PerformanceTests PerformanceTests(ISeries<double> input , int numIterations)
		{
			return indicator.PerformanceTests(input, numIterations);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.Example.PerformanceTests PerformanceTests(int numIterations)
		{
			return indicator.PerformanceTests(Input, numIterations);
		}

		public Indicators.Example.PerformanceTests PerformanceTests(ISeries<double> input , int numIterations)
		{
			return indicator.PerformanceTests(input, numIterations);
		}
	}
}

#endregion
