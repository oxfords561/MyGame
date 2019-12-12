using System;
using System.IO;
using System.Runtime.InteropServices;

/// <summary>
/// Contains information about the file that is found 
/// by the FindFirstFile or FindNextFile functions.
/// </summary>
[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto), BestFitMapping(false)]
internal class WIN32_FIND_DATA
{
    public FileAttributes dwFileAttributes;
    public uint ftCreationTime_dwLowDateTime;
    public uint ftCreationTime_dwHighDateTime;
    public uint ftLastAccessTime_dwLowDateTime;
    public uint ftLastAccessTime_dwHighDateTime;
    public uint ftLastWriteTime_dwLowDateTime;
    public uint ftLastWriteTime_dwHighDateTime;
    public uint nFileSizeHigh;
    public uint nFileSizeLow;

    public string cFileName;


    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </returns>
    public override string ToString()
    {
        return "File name=" + cFileName;
    }
}