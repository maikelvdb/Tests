param (
    [Parameter(Mandatory = $true)]
    [string]$sourcePath,

    [Parameter(Mandatory = $true)]
    [string]$extendPath,

    [Parameter(Mandatory = $true)]
    [string]$savePath
)

function Convert-ToCustomObject {
    param (
        [Parameter(Mandatory = $true)]
        [string]$JsonFilePath
    )

    if (-Not (Test-Path $JsonFilePath)) {
        Write-Error "The file '$JsonFilePath' does not exist."
        return $null
    }

    try {
        $jsonContent = Get-Content -Path $JsonFilePath -Raw
        $customObject = $jsonContent | ConvertFrom-Json
        return $customObject
    } catch {
        Write-Error "Failed to convert JSON to object: $_"
        return $null
    }
}

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

function Convert-ToJson {
    param (
        [Parameter(Mandatory = $true)]
        [PSCustomObject]$CustomObject
    )

    try {
        $jsonOutput = $CustomObject | ConvertTo-Json -Depth 10
        return $jsonOutput
    } catch {
        Write-Error "Failed to convert object to JSON: $_"
        return $null
    }
}

function Save {
    param (
        [Parameter(Mandatory = $true)]
        [string]$JsonData,

        [Parameter(Mandatory = $true)]
        [string]$FilePath
    )

    try {
        if (Test-Path $FilePath) {
            Remove-Item -Path $FilePath -Force
        }

        $JsonData | Out-File -FilePath $FilePath -Encoding UTF8
        Write-Host "JSON data saved to $FilePath"
    } catch {
        Write-Error "Failed to save JSON to file: $_"
    }
}

$source = Convert-ToCustomObject -JsonFilePath $sourcePath
$extend = Convert-ToCustomObject -JsonFilePath $extendPath

if ($extend -ne $null) {
	$merged = Merge-Json -source $source -extend $extend
	$mergedJson = Convert-ToJson -CustomObject $merged
	
	Save -JsonData $mergedJson -FilePath $savePath
}
else {
	Write-Host "De verrijkings JSON is leeg"
}

