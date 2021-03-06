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
namespace NinjaTrader.NinjaScript.Indicators
{
	public class TabajaraSMACandleForOB : Indicator
	{
		private SMA 				  smaValue;
		private Series<double> 		  smaDrawn;
		//Values for strategy builder
		private const double buyLine			= 1;//When line signal is to buy 
		private const double sellLine			= -1;//When line signal is to seel 
		private const double doNothingLine 		= 0;//When line signal is to do nothing
		/*
		private const double buyCandle			= 1; //When candle signal is to buy
		private const double sellCandle			= -1; //When candle signal is to seel
		private const double doNothingCandle 	= 0; //When candle signal is to do nothing
		*/
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Enter the description for your new custom Indicator here.";
				Name										= "TabajaraSMACandleForOB";
				Calculate									= Calculate.OnEachTick;
				IsOverlay									= true;
				DisplayInDataBox							= true;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= false;
				DrawVerticalGridLines						= false;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
				Period						= 20;
				AddPlot(Brushes.Transparent, "BuySeelDoNothing");
				AddPlot(Brushes.Yellow, "SMA");
				Plots[1].Width 				= 2;
				
			}
			else if (State == State.Configure)
			{
				smaDrawn 					= new Series<double>(this);
			}
			else if(State == State.DataLoaded)
			{
				smaValue 					= SMA(Close, Convert.ToInt32(Period));
			}
		}

		protected override void OnBarUpdate()
		{
			if(CurrentBars[0] < Period)
				return;
			
			Values[1][0]		= SMA(Close,Period)[0];
			smaDrawn[0] 		= SMA(Close,Period)[0];
			
			//Candles
			if(IsFalling(smaDrawn)
				&& (Close[0] < smaValue[0])
				&& (Close[0] < Close[1]))
			{
				//Value[0]			= sellCandle;
				BarBrush 			= Brushes.Red;
			}
			else if(IsRising(smaDrawn)
					&& (Close[0] > smaValue[0])
					&& (Close[0] > Close[1]))
			{
				//Value[0]			= buyCandle;
				BarBrush 			= Brushes.Green;
			}
			else
			{
				//Value[0] 			= doNothingCandle;
			}
			
			//Line SMA
			if(IsFalling(smaDrawn)
				&& (Close[0] < smaValue[0]))
			{
				Value[0]			= sellLine;
				PlotBrushes[1][0] 	= Brushes.Red;
			}
			else if(IsRising(smaDrawn)
					&& (Close[0] > smaValue[0]))
			{
				Value[0] 			= buyLine;
				PlotBrushes[1][0] 	= Brushes.Green;
			}
			else
			{
				Value[0]			= doNothingLine;
			}
			
		}

		#region Properties
		[NinjaScriptProperty]
		[Range(1, int.MaxValue)]
		[Display(Name="Period", Order=1, GroupName="Parameters")]
		public int Period
		{ get; set; }
		
		[Browsable(false)]
		[XmlIgnore]
		public Series<double> BuySeelDoNothing
		{
			get { return Values[0]; }
		}
		#endregion

	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private TabajaraSMACandleForOB[] cacheTabajaraSMACandleForOB;
		public TabajaraSMACandleForOB TabajaraSMACandleForOB(int period)
		{
			return TabajaraSMACandleForOB(Input, period);
		}

		public TabajaraSMACandleForOB TabajaraSMACandleForOB(ISeries<double> input, int period)
		{
			if (cacheTabajaraSMACandleForOB != null)
				for (int idx = 0; idx < cacheTabajaraSMACandleForOB.Length; idx++)
					if (cacheTabajaraSMACandleForOB[idx] != null && cacheTabajaraSMACandleForOB[idx].Period == period && cacheTabajaraSMACandleForOB[idx].EqualsInput(input))
						return cacheTabajaraSMACandleForOB[idx];
			return CacheIndicator<TabajaraSMACandleForOB>(new TabajaraSMACandleForOB(){ Period = period }, input, ref cacheTabajaraSMACandleForOB);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.TabajaraSMACandleForOB TabajaraSMACandleForOB(int period)
		{
			return indicator.TabajaraSMACandleForOB(Input, period);
		}

		public Indicators.TabajaraSMACandleForOB TabajaraSMACandleForOB(ISeries<double> input , int period)
		{
			return indicator.TabajaraSMACandleForOB(input, period);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.TabajaraSMACandleForOB TabajaraSMACandleForOB(int period)
		{
			return indicator.TabajaraSMACandleForOB(Input, period);
		}

		public Indicators.TabajaraSMACandleForOB TabajaraSMACandleForOB(ISeries<double> input , int period)
		{
			return indicator.TabajaraSMACandleForOB(input, period);
		}
	}
}

#endregion
