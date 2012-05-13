﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateTimeExtensions.NaturalText.CultureStrategies {
	public class NaturalTextStrategyLocatorByName {
		private const string NATURAL_TIME_STRATEGY_NAME = "NaturalTimeStrategy";
		private const string NAMESPACE = "DateTimeExtensions.NaturalText.CultureStrategies";

		public static INaturalTimeStrategy LocateNaturalTimeStrategyForName(string name) {
			string strategyPrefix = name.ToUpperInvariant().Replace("-", "_");
			string strategyName = NAMESPACE + "." + strategyPrefix + NATURAL_TIME_STRATEGY_NAME;
			var naturalTimeStrategy = CreateObjectInstance<INaturalTimeStrategy>(strategyName);
			if (naturalTimeStrategy == null) {
				naturalTimeStrategy = new DefaultNaturalTimeStrategy();
			}
			return naturalTimeStrategy;
		}

		private static T CreateObjectInstance<T>(string typeName) {
			if (typeName == null) {
				throw new ArgumentNullException("typeName");
			}
			Type type = Type.GetType(typeName);
			if (type == null) {
				//throw new StrategyNotFoundException(string.Format("Type name '{0}' was not found.", typeName));
				return default(T);
			}
			T instance = (T)Activator.CreateInstance(type);
			if (instance == null) {
				//throw new StrategyNotFoundException(string.Format("Could not create a new instance of type '{0}'.", typeName));
				return default(T);
			}
			return instance;
		}		
	}
}