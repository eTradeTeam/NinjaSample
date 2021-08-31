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
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators.Example
{
	public class IndicEx2 : Indicator
	{
		#region Global Variables Declaration
		
//		event_Struct testEvent;
		
		#endregion

		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				#region Indicator Configuration Parameters
				Description									= @"Enter the description for your new custom Indicator here.";
				Name										= "IndicEx2";
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
				NumIterations					= 1;
				#endregion
				
				#region Indicator Plot Configuration
				AddPlot(Brushes.Orange, "TestmethodPassStruct");
				#endregion
				
				
			}
			else if (State == State.Configure)
			{
				#region Global Variable - Initialization
				
				#endregion
			}
			else if (State == State.DataLoaded)
			{
				#region Global Series and Class - Instantiation & Initialization
				
				#endregion
			}
		}

		protected override void OnBarUpdate()
		{
			#region Main Calculation
			
			// -------------------------------------
			// Indicator Performance Checks
			// -------------------------------------
			#region Performance Checks
			// -------------------------------------------- Start Performance Check
			// Note:  10000 Ticks per millisecond
/*			
			
			int num_iterations = 500000;
			iCount += 1;
			
			start_Time = DateTime.Now.Ticks;
			for ( int i = 0; i< num_iterations; i++ ) { mTestMethod_A( CurrentBar, testEvent, testEvent, testEvent ); }
			end_Time = DateTime.Now.Ticks;
//			Print(string.Format("Method A ------- pass struct ----------- Iterations: {0,10:N0}  Duration: {1,6:F3}ms  BarNum: {2}", num_iterations, (end_Time - start_Time)/TimeSpan.TicksPerMillisecond, CurrentBar ));
			sum_mA += (end_Time - start_Time)/TimeSpan.TicksPerMillisecond;
			
			start_Time = DateTime.Now.Ticks;
			for ( int i = 0; i< num_iterations; i++ ) { mTestMethod_B( CurrentBar, testEvent, Median_Price, Median_Price2 ); }
			end_Time = DateTime.Now.Ticks;
//			Print(string.Format("Method B ------- pass Series ----------- Iterations: {0,10:N0}  Duration: {1,6:F3}ms  BarNum: {2}", num_iterations, (end_Time - start_Time)/TimeSpan.TicksPerMillisecond, CurrentBar ));
			sum_mB += (end_Time - start_Time)/TimeSpan.TicksPerMillisecond;
			
			start_Time = DateTime.Now.Ticks;
			Median_Price[0] = High[0];
			Median_Price2[0] = Low[0];
			for ( int i = 0; i< num_iterations; i++ ) { mTestMethod_C( CurrentBar, testEvent, ref Median_Price, ref Median_Price2 ); }
			end_Time = DateTime.Now.Ticks;
//			Print(string.Format("Method C ------- pass Series Reference - Iterations: {0,10:N0}  Duration: {1,6:F3}ms  BarNum: {2}", num_iterations, (end_Time - start_Time)/TimeSpan.TicksPerMillisecond, CurrentBar ));
			sum_mC += (end_Time - start_Time)/TimeSpan.TicksPerMillisecond;
			
			start_Time = DateTime.Now.Ticks;
			for ( int i = 0; i< num_iterations; i++ ) { mTestMethod_D( CurrentBar, testEvent, High, Low ); }
			end_Time = DateTime.Now.Ticks;
//			Print(string.Format("Method C ------- pass ISeries ---------- Iterations: {0,10:N0}  Duration: {1,6:F3}ms  BarNum: {2}", num_iterations, (end_Time - start_Time)/TimeSpan.TicksPerMillisecond, CurrentBar ));
			sum_mD += (end_Time - start_Time)/TimeSpan.TicksPerMillisecond;
			
			// A - struct
			// B - Series
			// C - Series Reference (not allowed for class-method)
			// D - ISeries
			// E - ISeries Reference - not allowed for either method or class-method
			
			start_Time = DateTime.Now.Ticks;
			for ( int i = 0; i< num_iterations; i++ ) { testEvent_Fast.cTestMethod_A( CurrentBar, testEvent, testEvent, testEvent ); }
			end_Time = DateTime.Now.Ticks;
//			Print(string.Format("Class Method A - pass struct ----------- Iterations: {0,10:N0}  Duration: {1,6:F3}ms  BarNum: {2}", num_iterations, (end_Time - start_Time)/TimeSpan.TicksPerMillisecond, CurrentBar ));
			sum_cA += (end_Time - start_Time)/TimeSpan.TicksPerMillisecond;
			
			start_Time = DateTime.Now.Ticks;
			Median_Price[0] = High[0];
			Median_Price2[0] = Low[0];
			for ( int i = 0; i< num_iterations; i++ ) { testEvent_Fast.cTestMethod_B( CurrentBar, testEvent, Median_Price, Median_Price2 ); }
			end_Time = DateTime.Now.Ticks;
//			Print(string.Format("Class Method B - pass Series ----------- Iterations: {0,10:N0}  Duration: {1,6:F3}ms  BarNum: {2}", num_iterations, (end_Time - start_Time)/TimeSpan.TicksPerMillisecond, CurrentBar ));
			sum_cB += (end_Time - start_Time)/TimeSpan.TicksPerMillisecond;
			
			start_Time = DateTime.Now.Ticks;
			for ( int i = 0; i< num_iterations; i++ ) { testEvent_Fast.cTestMethod_D( CurrentBar, testEvent, High, Low ); }
			end_Time = DateTime.Now.Ticks;
//			Print(string.Format("Class Method D - pass ISeries ---------- Iterations: {0,10:N0}  Duration: {1,6:F3}ms  BarNum: {2}", num_iterations, (end_Time - start_Time)/TimeSpan.TicksPerMillisecond, CurrentBar ));
			sum_cD += (end_Time - start_Time)/TimeSpan.TicksPerMillisecond;
			
			if (Array.Exists(summaryBars, element => element == iCount)) {
				Print ("\n\n ---------------------------------- End Summary: " + iCount + "Bars -----------------------------------" );
				Print(string.Format("Method A ------- pass struct ----------- Iterations: {0,10:N0}  Duration: {1,6:F3}ms  BarNum: {2}", num_iterations, (sum_mA / iCount), CurrentBar ));
				Print(string.Format("Method B ------- pass Series ----------- Iterations: {0,10:N0}  Duration: {1,6:F3}ms  BarNum: {2}", num_iterations, (sum_mB / iCount), CurrentBar ));
				Print(string.Format("Method C ------- pass Series Reference - Iterations: {0,10:N0}  Duration: {1,6:F3}ms  BarNum: {2}", num_iterations, (sum_mC / iCount), CurrentBar ));
				Print(string.Format("Method C ------- pass ISeries ---------- Iterations: {0,10:N0}  Duration: {1,6:F3}ms  BarNum: {2}", num_iterations, (sum_mD / iCount), CurrentBar ));
				Print(string.Format("Class Method A - pass struct ----------- Iterations: {0,10:N0}  Duration: {1,6:F3}ms  BarNum: {2}", num_iterations, (sum_cA / iCount), CurrentBar ));
				Print(string.Format("Class Method B - pass Series ----------- Iterations: {0,10:N0}  Duration: {1,6:F3}ms  BarNum: {2}", num_iterations, (sum_cB / iCount), CurrentBar ));
				Print(string.Format("Class Method D - pass ISeries ---------- Iterations: {0,10:N0}  Duration: {1,6:F3}ms  BarNum: {2}", num_iterations, (sum_cD / iCount), CurrentBar ));
				Print (" ---------------------------------- End Summary -----------------------------------" );
				
			}
*/			
			// -------------------------------------------- End Performance Check
			#endregion
			
			#endregion
		}
		
		#region Modify Indicator DisplayName
		
		public override string DisplayName
		{
		  get { return "Example Global Ind 2"; }
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
		private Example.IndicEx2[] cacheIndicEx2;
		public Example.IndicEx2 IndicEx2(int numIterations)
		{
			return IndicEx2(Input, numIterations);
		}

		public Example.IndicEx2 IndicEx2(ISeries<double> input, int numIterations)
		{
			if (cacheIndicEx2 != null)
				for (int idx = 0; idx < cacheIndicEx2.Length; idx++)
					if (cacheIndicEx2[idx] != null && cacheIndicEx2[idx].NumIterations == numIterations && cacheIndicEx2[idx].EqualsInput(input))
						return cacheIndicEx2[idx];
			return CacheIndicator<Example.IndicEx2>(new Example.IndicEx2(){ NumIterations = numIterations }, input, ref cacheIndicEx2);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.Example.IndicEx2 IndicEx2(int numIterations)
		{
			return indicator.IndicEx2(Input, numIterations);
		}

		public Indicators.Example.IndicEx2 IndicEx2(ISeries<double> input , int numIterations)
		{
			return indicator.IndicEx2(input, numIterations);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.Example.IndicEx2 IndicEx2(int numIterations)
		{
			return indicator.IndicEx2(Input, numIterations);
		}

		public Indicators.Example.IndicEx2 IndicEx2(ISeries<double> input , int numIterations)
		{
			return indicator.IndicEx2(input, numIterations);
		}
	}
}

#endregion
