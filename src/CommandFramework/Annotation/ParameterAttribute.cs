﻿using System;
using CommandFramework.Commands;

namespace CommandFramework.Annotation
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
	public class ParameterAttribute : Attribute
	{
		public ParameterAttribute()
		{
			PositionIndex = -1;
		}

		public ParameterAttribute(string name) : this()
		{
			_name = name;
		}

		public string Name
		{
			get { return _name; }
		}

		public int PositionIndex { get; set; }

		public string Description { get; set; }

		public string Synonyms { get; set; }

		private readonly string _name;

		public void SetCreateDescriptorValues(CommandParameterDescriptor descriptor)
		{
			if (!string.IsNullOrEmpty(Name))
			{
				descriptor.Name = Name;
			}

			if (!string.IsNullOrEmpty(Synonyms))
			{
				descriptor.Synonyms = Synonyms.Split(SynonymSplitSeparator, StringSplitOptions.RemoveEmptyEntries);
			}

			if (!string.IsNullOrEmpty(Description))
			{
				descriptor.Description = Description;
			}

			if (PositionIndex != -1)
			{
				descriptor.PositionIndex = PositionIndex;
			}

			if (_hasDefaultValue)
			{
				descriptor.DefaultValue = _defaultValue;
			}
		}

		public object DefaultValue
		{
			get { return _defaultValue; }
			set
			{
				_hasDefaultValue = true;
				_defaultValue = value;
			}
		}

		private static readonly char[] SynonymSplitSeparator = { ',', ' ' };
		private bool _hasDefaultValue;
		private object _defaultValue;
	}
}