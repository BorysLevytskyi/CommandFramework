using System;
using System.Collections.Generic;
using CommandFramework.Commands.Input;
using CommandFramework.Commands.ParameterBinding;

namespace CommandFramework.Commands.Class
{
	internal static class ClassParameterValueBinder
	{
		public static void SetPropertyValues(object instance, ICollection<PropertyParameter> definedParameters, IEnumerable<IParameterInput> inputParameters)
		{
			var values = new ParameterValuesList<PropertyParameter>(definedParameters);

			foreach (var inputPrm in inputParameters)
			{
				values.SetParameterValue(inputPrm);
			}

			var allValues = values.GetParameterValues();

			foreach (var defPrm in definedParameters)
			{
				object value;
				if (!allValues.TryGetValue(defPrm, out value))
				{
					if (defPrm.AllowsDefaultValue)
					{
						value = defPrm.DefaultValue;
					}
					else
					{
						throw new Exception($"{defPrm.Name} parameter value wasn't supplied");
					}
				}

				defPrm.Property.SetValue(instance, value);
			}
		}
	}
}