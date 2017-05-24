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

    public static Dictionary<TKey, TValue> CloneCertainValues<TKey, TValue> ( this Dictionary<TKey, TValue> original, List<TKey> selectiveList ) {
        Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>( );
        foreach( TKey k in selectiveList ) {
            TValue val;
            if( original.TryGetValue( k, out val ) )
                ret.Add( k, val );
        }
        return ret;
    }
}
