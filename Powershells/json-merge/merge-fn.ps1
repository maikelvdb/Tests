function Merge-Json( $source, $extend ){
    if( $source -is [PSCustomObject] -and $extend -is [PSCustomObject] ){

        # Ordered hashtable for collecting properties
        $merged = [ordered] @{}

        # Copy $source properties or overwrite by $extend properties recursively
        foreach( $Property in $source.PSObject.Properties ){
            if( $null -eq $extend.$($Property.Name) ){
                $merged[ $Property.Name ] = $Property.Value
            }
            else {
                $merged[ $Property.Name ] = Merge-Json $Property.Value $extend.$($Property.Name)
            }
        }

        # Add $extend properties
        foreach( $Property in $extend.PSObject.Properties ){
            if( $null -eq $source.$($Property.Name) ) {
                $merged[ $Property.Name ] = $Property.Value
            }
        }

        # Convert hashtable into PSCustomObject and output
        [PSCustomObject] $merged
    }
    elseif( $source -is [Collections.IList] -and $extend -is [Collections.IList] ){

        $maxCount = [Math]::Max( $source.Count, $extend.Count )

        [array] $merged = for( $i = 0; $i -lt $maxCount; ++$i ){
            if( $i -ge $source.Count ) { 
                # extend array is bigger than source array
                $extend[ $i ]
            }              
            elseif( $i -ge $extend.Count ) {
                # source array is bigger than extend array
                $source[ $i ]
            }
            else {
                # Merge the elements recursively
                Merge-Json $source[$i] $extend[$i]
            }
        }

        # Output merged array, using comma operator to prevent enumeration 
        , $merged
    }
    else{
        # Output extend object (scalar or different types)
        $extend
    }
}