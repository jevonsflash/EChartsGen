///////////////////////////////////////////////////////////
//  ScaleLimit.cs
//  Implementation of the Class ScaleLimit
//  Generated by Enterprise Architect
//  Created on:      16-2��-2017 22:58:36
//  Original author: Doku
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace ECharts.Entities {
	public class ScaleLimit {
		

		public double min{
			get;
			set;
		}

		public double max{
			get;
			set;
		}

		/// 
		/// <param name="min"></param>
		public ScaleLimit Min(double min){
		     this.min=min;
		return this; 
		}

		/// 
		/// <param name="max"></param>
		public ScaleLimit Max(double max){
		     this.max=max;
		return this; 
		}

	}//end ScaleLimit

}//end namespace Entities