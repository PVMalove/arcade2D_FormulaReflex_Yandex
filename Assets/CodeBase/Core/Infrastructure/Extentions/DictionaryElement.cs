﻿using System;
using System.Xml.Serialization;

namespace CodeBase.Core.Infrastructure.Extentions
{
    [Serializable]
    public class DictionaryElement<TKey, TValue>
    {
        [XmlElement("Key")]
        public TKey Key;
        [XmlElement("Value")]
        public TValue Value;

        public DictionaryElement()
        {

        }

        public DictionaryElement(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}