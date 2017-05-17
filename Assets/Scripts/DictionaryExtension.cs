using System;
using System.Collections.Generic;

public static class DictionaryExtension
{
    public static Dictionary<TKey, TValue> CloneDictionaryCloningValues<TKey, TValue> ( this Dictionary<TKey, TValue> original ) where TValue : ICloneable {
        Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>( original.Count, original.Comparer );
        foreach( KeyValuePair<TKey, TValue> entry in original )
            ret.Add( entry.Key, (TValue)entry.Value.Clone( ) );
        return ret;
    }

}
